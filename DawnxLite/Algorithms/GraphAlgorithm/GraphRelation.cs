using System;

namespace Dawnx.Algorithms.GraphAlgorithm
{
    public class GraphRelation : IGraphRelation
    {
        public Guid PointA { get; set; }

        public Guid PointB { get; set; }

        public bool Directed { get; set; }
    }
}
