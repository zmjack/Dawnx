using Dawnx.Analysises;
using System.ComponentModel;
using System.Data;
using System.Linq;
using Xunit;

namespace Dawnx.Test.Analysises
{
    public class CsvTest
    {
        public class Entity
        {
            [DisplayName("Class A")]
            public string A { get; set; }

            public int B { get; set; }

            [DisplayName("Class C1")]
            public string C1 { get; set; }

        }

        [Fact]
        public void ToTable()
        {
            var csv = new Csv(
                @"Class A,B,C1
11,12,13
21,22,23");
            var table = csv.ToTable();

            Assert.Equal(csv.Titles, table.Columns.OfType<DataColumn>().Select(col => col.ColumnName));
        }

        [Fact]
        public void ToArray()
        {
            var csv = new Csv(
                @"Class A,B,C1
11,12,13
21,22,23");
            var entities = csv.ToArray<Entity>();

            Assert.Equal("11", entities[0].A);
            Assert.Equal(12, entities[0].B);
            Assert.Null(entities[0].C1);

            Assert.Equal("21", entities[1].A);
            Assert.Equal(22, entities[1].B);
            Assert.Null(entities[1].C1);
        }

    }
}
