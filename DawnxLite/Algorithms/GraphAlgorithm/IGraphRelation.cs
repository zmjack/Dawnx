using System;

namespace Dawnx.Algorithms.GraphAlgorithm
{
    public interface IGraphRelation
    {
        Guid PointA { get; }
        Guid PointB { get; }
        bool Directed { get; }
    }
}
