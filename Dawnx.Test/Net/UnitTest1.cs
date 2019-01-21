using Dawnx.Definition;
using Dawnx.Net.Web;
using Dawnx.Net.Web.Processors;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using Xunit;

namespace Dawnx.Net.Test
{
    public class UnitTest1
    {
        private Dictionary<string, object> updata = new Dictionary<string, object>
        {
            ["str"] = "str",
            ["strs"] = new[] { "str1", "str2" },
            ["num"] = 1,
            ["nums"] = new[] { 2.1, 2.2 },
        };
        private Dictionary<string, object> upfiles = new Dictionary<string, object>
        {
            ["file"] = "Net/file.txt",
            ["files"] = new[] { "Net/file1.txt", "Net/file2.txt" },
        };

        private object updataObj = new
        {
            str = "str",
            strs = new[] { "str1", "str2" },
            num = 1,
            nums = new[] { 2.1, 2.2 },
        };

        [Fact]
        public void PostJsonTest()
        {
            Assert.Equal(
                "{\"str\":\"str\",\"strs\":[\"str1\",\"str2\"],\"num\":1,\"nums\":[2.1,2.2]}",
                Http.PostJson("http://dev.dawnx.net/Http/RestJson", updata));
            Assert.Equal(
                "{\"str\":\"str\",\"strs\":[\"str1\",\"str2\"],\"num\":1,\"nums\":[2.1,2.2]}",
                Http.PostJson("http://dev.dawnx.net/Http/RestJson", updataObj));
        }

        [Fact]
        public void RequestTest()
        {
            Assert.Equal(
                "{\"verb\":\"GET\",\"query\":{\"str\":[\"str\"],\"strs\":[\"str1\",\"str2\"],\"num\":[\"1\"],\"nums\":[\"2.1\",\"2.2\"]},\"form\":{},\"files\":{}}",
                Http.Get("http://dev.dawnx.net/Http", updata));
            Assert.Equal(
                "{\"verb\":\"POST\",\"query\":{},\"form\":{\"str\":[\"str\"],\"strs\":[\"str1\",\"str2\"],\"num\":[\"1\"],\"nums\":[\"2.1\",\"2.2\"]},\"files\":{}}",
                Http.Post("http://dev.dawnx.net/Http", updata));
            Assert.Equal(
                "{\"verb\":\"POST\",\"query\":{},\"form\":{\"str\":[\"str\"],\"strs\":[\"str1\",\"str2\"],\"num\":[\"1\"],\"nums\":[\"2.1\",\"2.2\"]},\"files\":{\"file\":[\"file.txt|7\"],\"files\":[\"file1.txt|5\",\"file2.txt|5\"]}}",
                Http.Up("http://dev.dawnx.net/Http", updata, upfiles));

            Assert.Equal(
                "{\"verb\":\"GET\",\"query\":{\"str\":[\"str\"],\"strs\":[\"str1\",\"str2\"],\"num\":[\"1\"],\"nums\":[\"2.1\",\"2.2\"]},\"form\":{},\"files\":{}}",
                Http.Get("http://dev.dawnx.net/Http", updataObj));
            Assert.Equal(
                "{\"verb\":\"POST\",\"query\":{},\"form\":{\"str\":[\"str\"],\"strs\":[\"str1\",\"str2\"],\"num\":[\"1\"],\"nums\":[\"2.1\",\"2.2\"]},\"files\":{}}",
                Http.Post("http://dev.dawnx.net/Http", updataObj));
            Assert.Equal(
                "{\"verb\":\"POST\",\"query\":{},\"form\":{\"str\":[\"str\"],\"strs\":[\"str1\",\"str2\"],\"num\":[\"1\"],\"nums\":[\"2.1\",\"2.2\"]},\"files\":{\"file\":[\"file.txt|7\"],\"files\":[\"file1.txt|5\",\"file2.txt|5\"]}}",
                Http.Up("http://dev.dawnx.net/Http", updata, upfiles));
        }

        [Fact]
        public void WebAccessCookiesTest()
        {
            var web = new HttpAccess();
            Assert.Equal("{\"SetTime1\":[\"1991-01-01\"],\"SetTime2\":[\"2012-04-16\"]}",
                web.Post("http://dev.dawnx.net/Http/SetCookies", new Dictionary<string, object>
                {
                    ["SetTime1"] = "1991-01-01",
                    ["SetTime2"] = "2012-04-16",
                }));

            Assert.Equal("1991-01-01", web.StateContainer.Cookies.GetCookies(new Uri("http://dev.dawnx.net"))["SetTime1"].Value);
            Assert.Equal("2012-04-16", web.StateContainer.Cookies.GetCookies(new Uri("http://dev.dawnx.net"))["SetTime2"].Value);
        }

        [Fact]
        public void WebAccessAuthLoginTest()
        {
            var web = new HttpAccess().Self(_ => _.AddProcessor(new DevLoginProcessor()));
            Assert.Equal("jack@zmland.com", web.Get("http://dev.dawnx.net/AuthInfo"));
        }

        public class DevLoginProcessor : LoginProcessor
        {
            public override HttpWebResponse LoginProcess(HttpAccess web, HttpWebResponse response)
            {
                var uri = response.ResponseUri;
                if (uri.LocalPath == "/Identity/Account/Login")
                {
                    string html;
                    using (var stream = response.GetResponseStream())
                    using (var reader = new StreamReader(stream))
                        html = reader.ReadToEnd();

                    var match_RequestToken = new Regex(@"<input name=""__RequestVerificationToken"".+? value=""(.+?)"".+?/>").Match(html);
                    if (match_RequestToken.Success)
                    {
                        return web.GetResponse(HttpVerb.POST, MimeType.APPLICATION_X_WWW_FORM_URLENCODED,
                            uri.AbsoluteUri,
                            new Dictionary<string, object>
                            {
                                ["Input.Email"] = "jack@zmland.com",
                                ["Input.Password"] = "123123",
                                ["__RequestVerificationToken"] = match_RequestToken.Groups[1].Value,
                            }, null);
                    }
                    else throw new FormatException();
                }
                else return null;
            }
        }

    }
}
