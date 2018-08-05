using Dawnx.AspNetCore.Algorithms;
using Xunit;

namespace Dawnx.AspNetCore.Test.Algorithms
{
    public class PaginationTest
    {
        [Fact]
        public void Test1()
        {
            Assert.Equal(new[] { 1, 2, 3, 4 }, Pagination.GetRange(1, 5, 4, false));
            Assert.Equal(new[] { 1, 2, 3, 4 }, Pagination.GetRange(2, 5, 4, false));
            Assert.Equal(new[] { 1, 2, 3, 4 }, Pagination.GetRange(3, 5, 4, true));
            Assert.Equal(new[] { 2, 3, 4, 5 }, Pagination.GetRange(3, 5, 4, false));
            Assert.Equal(new[] { 2, 3, 4, 5 }, Pagination.GetRange(4, 5, 4, true));
        }

    }
}
