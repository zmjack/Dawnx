using Dawnx.Analysises;
using Dawnx.AspNetCore.Reflection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Dawnx.AspNetCore.Test
{
    public class IViewModelTest
    {
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
        public void Test1()
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
        }

        [Fact]
        public void CsvTest()
        {
            var csv = new Csv<BasicTypeConverterForModel>(
                @"RentStartDate,RentEndDate
2012-04-16,20120416222325");
            var models = csv.ToArray<VModel>();

            Assert.Equal("2012/4/16 22:23:25", models[0].RentEndDate.ToString());
        }
    }
}
