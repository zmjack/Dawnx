using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Dawnx.Data.Test
{
    public class AppRegistryStore : RegistryStore
    {
    }

    public class AppRegistry : Registry<AppRegistry>
    {
        public virtual string Name { get; set; }

        public virtual int Age { get; set; }

        public virtual string NickName { get; set; } = "haha";

        public virtual string Address { get; set; }
    }

    public class AppRegistryTest
    {
        [Fact]
        public void Test1()
        {
            var regs = new AppRegistryStore[]
            {
                new AppRegistryStore { Item = "Person1", Key = "Name", Value = "zmjack" },
                new AppRegistryStore { Item = "Person1", Key = "Age", Value = "28" },
                new AppRegistryStore { Item = "Person2", Key = "Name", Value = "ashe" },
                new AppRegistryStore { Item = "Person2", Key = "Age", Value = "27" },
            };

            var zmjack = AppRegistry.Connect(regs, "Person1");
            zmjack.Age = 999;

            Assert.Equal("Person1", zmjack.GetItemString());
            Assert.Equal(999, zmjack.Age);
            Assert.Equal("haha", zmjack.NickName);
            Assert.Null(zmjack.Address);

            Assert.Throws<KeyNotFoundException>(() => zmjack.NickName = "new");
            Assert.Equal("999", regs.FirstOrDefault(x => x.Key == nameof(AppRegistry.Age))?.Value);
        }

    }
}
