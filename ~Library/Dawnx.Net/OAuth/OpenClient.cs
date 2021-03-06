﻿using Dawnx.Net.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NStandard;
using NStandard.Flows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Dawnx.Net.OAuth
{
    public class OpenClient : HttpAccess
    {
        private string _AccessToken;
        private string _RefreshToken;

        public HttpContext HttpContext { get; }
        public OpenDiscoveryResult DiscoveryResult { get; }
        public string Authorization { get; }
        public string TokenType { get; private set; }
        public string AccessToken
        {
            get => _AccessToken;
            private set
            {
                var regex = new Regex(@"^([^\.]+)\.([^\.]+)\.([^\.]+)$");
                var match = regex.Match(value);
                if (match.Success)
                {
                    _AccessToken = value;

                    Header = JsonConvert.DeserializeObject(match.Groups[1].Value.Flow(StringFlow.FromUrlSafeBase64)) as JToken;        // ALGORITHM & TOKEN TYPE
                    Payload = JsonConvert.DeserializeObject(match.Groups[2].Value.Flow(StringFlow.FromUrlSafeBase64)) as JToken;       // DATA

                    //TODO: Not supported yet
                    //Signature = JsonConvert.DeserializeObject(
                    //    Base64Utility.ConvertUrlBase64ToBase64(
                    //        match.Groups[3].Value).Base64Decode()) as JToken;       // Verify Signature

                    if (!(HttpContext is null))
                        HttpContext.AuthenticateAsync(null).Result.Properties.UpdateTokenValue("access_token", value);
                }
                else throw new ArgumentException("Invalid AccessToken.");
            }
        }
        public string RefreshToken
        {
            get => _RefreshToken;
            private set
            {
                _RefreshToken = value;
                if (!(HttpContext is null))
                    HttpContext.AuthenticateAsync(null).Result.Properties.UpdateTokenValue("refresh_token", value);
            }
        }
        public IOpenAuth OpenAuth { get; }

        public JToken Header { get; private set; }
        public JToken Payload { get; private set; }
        public JToken Signature { get; private set; }

        public string Issuer => Payload["iss"].Value<string>();
        public string[] ApiScopes => Payload["scope"].Values<string>().ToArray();
        public DateTime NotValidBeforeUTC => Payload?
            .For(_ => DateTimeEx.FromUnixSeconds(_["nbf"].Value<int>())) ?? DateTimeEx.UnixMinValue();
        public DateTime ExpirationTimeUTC => Payload?
            .For(_ => DateTimeEx.FromUnixSeconds(_["exp"].Value<int>())) ?? DateTimeEx.UnixMinValue();

        public bool IsAccessTokenValid => DateTime.UtcNow.For(_ => NotValidBeforeUTC <= _ && _ <= ExpirationTimeUTC);

        public OpenClient(string authorization, string accessToken, string refreshToken, string tokenType)
        {
            Authorization = authorization;
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            TokenType = tokenType;
            DiscoveryResult = new OpenDiscoveryClient(Issuer).Discovery();
        }

        public OpenClient(string authority, IOpenAuth openAuth)
        {
            OpenAuth = openAuth;
            Authorization = openAuth.Authorization;
            DiscoveryResult = new OpenDiscoveryClient(authority).Discovery();
        }

        public OpenClient(OpenAuth.ClientCredentialsAuth openAuth, HttpContext context)
            : this(openAuth.Authorization,
                  context.AuthenticateAsync(null).Result.Properties.GetTokenValue("access_token"),
                  context.AuthenticateAsync(null).Result.Properties.GetTokenValue("refresh_token"),
                  context.AuthenticateAsync(null).Result.Properties.GetTokenValue("token_type"))
        {
            HttpContext = context;
        }

        public void RefreshTokens()
        {
            if (!(RefreshToken is null))
            {
                var token = PostFor(DiscoveryResult.TokenEndPointUrl, new
                {
                    grant_type = "refresh_token",
                    refresh_token = RefreshToken,
                });
                AccessToken = token["access_token"].Value<string>();
                RefreshToken = token["refresh_token"]?.Value<string>();
                TokenType = token["token_type"].Value<string>();

                var auth = HttpContext.AuthenticateAsync(null).Result;
                auth.Properties.StoreTokens(new[]
                {
                    new AuthenticationToken { Name = "access_token", Value = AccessToken },
                    new AuthenticationToken { Name = "refresh_token", Value = RefreshToken },
                    new AuthenticationToken { Name = "token_type", Value = TokenType },
                });
                HttpContext.SignInAsync(auth.Principal, auth.Properties).Wait();

                return;
            }
            else
            {
                if (!(OpenAuth is null))
                {
                    var token = PostFor(DiscoveryResult.TokenEndPointUrl, OpenAuth.RequestBody);
                    AccessToken = token["access_token"].Value<string>();
                    RefreshToken = token["refresh_token"]?.Value<string>();
                    TokenType = token["token_type"].Value<string>();
                    return;
                }
                else throw new InvalidOperationException("AccessToken is expired and it's cannot be refreshed.");
            }
        }

        public override bool Ready(string method, string enctype, string url, Dictionary<string, object> updata, Dictionary<string, object> upfiles)
        {
            if (url == DiscoveryResult.TokenEndPointUrl)
            {
                WithHeaders(new Dictionary<string, string>
                {
                    ["Authorization"] = $"Basic {Authorization}",
                });
                return true;
            }

            if (!IsAccessTokenValid)
                RefreshTokens();

            if (IsAccessTokenValid)
            {
                WithHeaders(new Dictionary<string, string>
                {
                    ["Authorization"] = $"{TokenType} {AccessToken}",
                });
                return true;
            }
            else return false;
        }

    }

}
