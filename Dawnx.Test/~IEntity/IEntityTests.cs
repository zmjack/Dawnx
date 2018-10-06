using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Dawnx.Test
{
    public class IEntityTests
    {
        public class Entity : IEntity
        {
            public string String { get; set; }
            public int Int { get; set; }
        }

        [Fact]
        public void Test1()
        {
            var a = new Entity() { String = "123", Int = 1 };
            var b = new Entity().For(_ => _.Accept(a));
            var c = new Entity().For(_ => _.Accept(a, m => new { m.String }));
            var d = new Entity().For(_ => _.AcceptBut(a, m => new { m.String }));

            Assert.Equal("123", b.String);
            Assert.Equal(1, b.Int);

            Assert.Equal("123", c.String);
            Assert.Equal(0, c.Int);

            Assert.Null(d.String);
            Assert.Equal(1, d.Int);
        }

        public class MyEntity : IEntity<MyEntity>
        {
            public string Class { get; set; }
            public string Name { get; set; }
            public string Comment { get; set; }
        }

        [Fact]
        public void Test2()
        {
            var entity1 = new MyEntity
            {
                Class = "123",
                Name = "aaa",
            };
            var entity2 = new MyEntity();
            entity2.Accept(entity1, m => new { m.Class });
            entity2.Accept(entity1, m => m.Comment);

            Assert.Equal(entity2.Class, entity1.Class);
            Assert.Equal(entity2.Comment, entity1.Comment);
            Assert.NotEqual(entity2.Name, entity1.Name);
        }

        [Fact]
        public void Test3()
        {
            var entity1 = new MyEntity
            {
                Class = "123",
                Name = "aaa",
            };

            var dict = entity1.ToDisplayDictionary(x => x.Name);
        }

        public class VModel : IEntity<VModel>
        {
            public enum ESex
            {
                [Display(Name = "Man")] Male,
                [Display(Name = "Woman")] Female,
            }

            [Display(Name = "Street Name")]
            public string Street { get; set; }

            public string Name { get; set; }

            public ESex Sex { get; set; }

            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
            public DateTime RentStartDate { get; set; }

            [DisplayFormat(DataFormatString = "{0:yyyyMMdd}{0:HHmmss}")]
            public DateTime RentEndDate { get; set; }

            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
            public DateTime? Reservation { get; set; }

            public VModel Link { get; set; }
        }

        [Fact]
        public void Test4()
        {
            var model = new VModel()
            {
                RentStartDate = DateTime.Parse("2018-1-2 11:22:33"),
                RentEndDate = DateTime.Parse("2018-1-2 11:22:33"),
            };

            var models = new VModel[0] as IEnumerable<VModel>;

            Assert.Equal("Street Name", model.DisplayName(_ => _.Street));
            Assert.Equal("Name", model.DisplayName(_ => _.Name));
            Assert.Equal("Man", model.Display(_ => _.Sex));

            Assert.Equal("2018-01-02", model.Display(_ => _.RentStartDate));
            Assert.Equal("20180102112233", model.Display(_ => _.RentEndDate));

            Assert.Equal("Street Name", models.DisplayName(_ => _.Street));

            Assert.Equal("", model.Display(_ => _.Reservation));

            var dict = model.ToDisplayDictionary(x => new { x.RentStartDate, x.RentEndDate });
            var dict2 = model.ToDisplayDictionary();
        }

    }
}
