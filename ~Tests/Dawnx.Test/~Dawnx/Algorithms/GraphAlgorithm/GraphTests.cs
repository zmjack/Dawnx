using Dawnx.Ranges;
using System;
using System.Linq;
using Xunit;

namespace Dawnx.Algorithms.GraphAlgorithm.Test
{
    public class GraphTests
    {
        [Fact]
        public void Test1()
        {
            var points = IntegerRange.Create(7)
                .Select(i => new GraphPoint { Id = Guid.NewGuid(), Name = $"v{i}" })
                .ToArray();

            var relations = new[]
            {
                (1, 6, true, 100),
                (1, 5, true, 30),
                (1, 3, true, 10),
                (2, 3, true, 5),
                (3, 4, true, 50),
                (4, 6, true, 10),
                (5, 6, true, 60),
                (5, 4, true, 20),
            }
            .Select(def => new MyGraphRelation
            {
                PointA = points[def.Item1].Id,
                PointB = points[def.Item2].Id,
                Directed = def.Item3,
                Distance = def.Item4,
            });

            var graph = Graph<GraphPoint, MyGraphRelation>.Create(points, relations);
            Assert.Equal(3, graph["v1"].To.Length);
            Assert.Single(graph["v2"].To);
            Assert.Single(graph["v3"].To);
            Assert.Single(graph["v4"].To);
            Assert.Equal(2, graph["v5"].To.Length);
            Assert.Empty(graph["v6"].To);

            Assert.Empty(graph["v1"].From);
            Assert.Empty(graph["v2"].From);
            Assert.Equal(2, graph["v3"].From.Length);
            Assert.Equal(2, graph["v4"].From.Length);
            Assert.Single(graph["v5"].From);
            Assert.Equal(3, graph["v6"].From.Length);

            var result = graph.SearchPath(
                Dijkstra<GraphPoint, MyGraphRelation>.GetShortestPath,
                r => r.Distance, "v1", "v6");
            Assert.Equal(new[] { "v1", "v5", "v4", "v6" }, result.Select(x => x.Point.Name));
        }

        public class MyGraphRelation : IGraphRelation
        {
            public Guid PointA { get; set; }

            public Guid PointB { get; set; }

            public bool Directed { get; set; }

            public int Distance { get; set; }
        }

    }
}
