using System;
using System.Collections.Generic;
using System.Linq;

namespace Dawnx.Algorithms.GraphAlgorithm
{
    public static class Dijkstra<TPointModel, TRelationModel>
        where TPointModel : IGraphPoint
        where TRelationModel : IGraphRelation
    {
        private class DijkstraNode : IGraphPathNode<TPointModel, TRelationModel>
        {
            public GraphAnalysedPoint<TPointModel, TRelationModel> Point { get; internal set; }
            public bool Passed { get; internal set; }
            public double Distance { get; internal set; }
            public DijkstraNode From { get; internal set; }
        }

        public static IGraphPathNode<TPointModel, TRelationModel>[] GetShortestPath(
            Graph<TPointModel, TRelationModel> graph,
            Func<TRelationModel, double> algorithm,
            string from, string to)
        {
            var nodes = graph.Points.Select(point =>
            {
                return new DijkstraNode
                {
                    Point = point,
                    Passed = false,
                    Distance = double.PositiveInfinity,
                };
            }).ToArray();

#pragma warning disable IDE0042
            var take = nodes
                .Single(node => node.Point.Name == from)
                .Then(_ => _.Distance = 0);
#pragma warning restore IDE0042

            do
            {
                foreach (var pointTo in take.Point.To)
                {
                    var nodeTo = nodes.Single(node => node.Point.Id == pointTo.Point.Id);
                    var distance = take.Distance + algorithm(pointTo.RelationModel);

                    if (distance < nodeTo.Distance)
                    {
                        nodeTo.Distance = distance;
                        nodeTo.From = take;
                    }
                }
                take.Passed = true;

                if (take.Point.Name == to) break;
            }
            while ((take = nodes.Where(node => !node.Passed)?.OrderBy(node => node.Distance).First()) != null);

            var path = new Stack<DijkstraNode>();
            var path_NodeTake = nodes.First(node => node.Point.Name == to);

            do { path.Push(path_NodeTake); }
            while ((path_NodeTake = path_NodeTake.From) != null);

            return path.ToArray();
        }

    }
}
