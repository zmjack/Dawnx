using System;

namespace Dawnx.Algorithms.Graph
{
    public interface IGraphRelation
    {
        Guid PointA { get; }
        Guid PointB { get; }
        bool Directed { get; }
    }
}
