using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Xunit;
using Dawnx.AspNetCore;
using System.Threading;
using System.Threading.Tasks;
using Dawnx.Utilities;

namespace Dawnx.AspNetCore.Test
{
    public class TrackTests
    {
        [Fact]
        public void Test1()
        {
            using (var context = new ApplicationDbContext())
            {
                var model = new SimpleModel
                {
                    ForTrim = "   127.0.0.* ",
                    ForLower = "Dawnx",
                    ForUpper = "Dawnx",
                    ForCondensed = "  Welcome to   Dawnx  ",
                };
                context.SimpleModels.Add(model);
                context.SaveChanges();

                Assert.Equal("127.0.0.*", model.ForTrim);
                Assert.Equal("dawnx", model.ForLower);
                Assert.Equal("DAWNX", model.ForUpper);
                Assert.Equal("Welcome to Dawnx", model.ForCondensed);
                Assert.Equal(@"127\.0\.0\.(?:[1-2]\d(?<!2[6-9])\d(?<!25[6-9])|\d\d|[0-9])", model.Automatic);
            }

        }

    }
}
