using Dawnx.Analysises;
using Dawnx.AspNetCore.Reflection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Dawnx.AspNetCore.Test
{
    public class IEntityTest
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
