using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using Xunit;

namespace Dawnx.Net.Test
{
    public class UnitTest1
    {
        //    var host = new WebHostBuilder()
        //        .UseKestrel()
        //        .UseStartup<Startup>()
        //        .Build();

        //    host.Run();

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
        public void GoGetTest()
        {
#if RunTestWeb
            Assert.Equal(JsonConvert.SerializeObject(updata),
                Web.Get("http://localhost:5000/Web/Go", updata));
            Assert.Equal(JsonConvert.SerializeObject(updata),
                Web.Post("http://localhost:5000/Web/Go", updata));
            Assert.Equal(
                @"{""data"":{""str"":""str"",""strs"":[""str1"",""str2""],""num"":1,""nums"":[2.1,2.2]},""file"":""file.txt"",""files"":[""file1.txt"",""file2.txt""]}",
                Web.Up("http://localhost:5000/Web/Up", updata, upfiles));
#endif
        }

        [Fact]
        public void WebAccessCookiesTest()
        {
#if RunTestWeb
            var web = new WebAccess();
            Assert.Equal("OK", web.Get("http://localhost:5000/Web/CookieTest"));

            Assert.Equal("1991-01-01", web.StateContainer.Cookies.GetCookies(new Uri("http://localhost:5000"))["SetTime1"].Value);
            Assert.Equal("2012-04-16", web.StateContainer.Cookies.GetCookies(new Uri("http://localhost:5000"))["SetTime2"].Value);
#endif
        }

    }
}
