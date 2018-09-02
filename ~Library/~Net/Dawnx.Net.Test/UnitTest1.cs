using Dawnx.Net.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
            ["file"] = "file.txt",
            ["files"] = new[] { "file1.txt", "file2.txt" },
        };

        [Fact]
        public void PostJsonTest()
        {
            Assert.Equal(
                "{\"verb\":\"GET\",\"query\":{\"str\":[\"str\"],\"strs\":[\"str1\",\"str2\"],\"num\":[\"1\"],\"nums\":[\"2.1\",\"2.2\"]},\"form\":{},\"files\":{}}",
                Web.PostJson("http://localhost:52420/Http/RestJson", updata));
        }

        [Fact]
        public void RequestTest()
        {
            Assert.Equal(
                "{\"verb\":\"GET\",\"query\":{\"str\":[\"str\"],\"strs\":[\"str1\",\"str2\"],\"num\":[\"1\"],\"nums\":[\"2.1\",\"2.2\"]},\"form\":{},\"files\":{}}",
                Web.Get("http://dev.dawnx.net/Http", updata));
            Assert.Equal(
                "{\"verb\":\"POST\",\"query\":{},\"form\":{\"str\":[\"str\"],\"strs\":[\"str1\",\"str2\"],\"num\":[\"1\"],\"nums\":[\"2.1\",\"2.2\"]},\"files\":{}}",
                Web.Post("http://dev.dawnx.net/Http", updata));
            Assert.Equal(
                "{\"verb\":\"POST\",\"query\":{},\"form\":{\"str\":[\"str\"],\"strs\":[\"str1\",\"str2\"],\"num\":[\"1\"],\"nums\":[\"2.1\",\"2.2\"]},\"files\":{\"file\":[\"file.txt|7\"],\"files\":[\"file1.txt|5\",\"file2.txt|5\"]}}",
                Web.Up("http://dev.dawnx.net/Http", updata, upfiles));
        }

        [Fact]
        public void WebAccessCookiesTest()
        {
            var web = new WebAccess();
            Assert.Equal("{\"SetTime1\":[\"1991-01-01\"],\"SetTime2\":[\"2012-04-16\"]}",
                web.Post("http://dev.dawnx.net/Http/SetCookies", new Dictionary<string, object>
                {
                    ["SetTime1"] = "1991-01-01",
                    ["SetTime2"] = "2012-04-16",
                }));

            Assert.Equal("1991-01-01", web.StateContainer.Cookies.GetCookies(new Uri("http://dev.dawnx.net"))["SetTime1"].Value);
            Assert.Equal("2012-04-16", web.StateContainer.Cookies.GetCookies(new Uri("http://dev.dawnx.net"))["SetTime2"].Value);
        }

    }
}
