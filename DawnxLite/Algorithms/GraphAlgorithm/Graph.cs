using Dawnx.Algorithms.GraphAlgorithm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dawnx.Algorithms
{
    public class Graph<TPointModel, TRelationModel> : Graph<Graph<TPointModel, TRelationModel>, TPointModel, TRelationModel>
        where TPointModel : IGraphPoint
        where TRelationModel : IGraphRelation
    { }

    public partial class Graph<TDerivedClass, TPointModel, TRelationModel>
        where TDerivedClass : Graph<TDerivedClass, TPointModel, TRelationModel>, new()
        where TPointModel : IGraphPoint
        where TRelationModel : IGraphRelation
    {
        public bool Processed { get; private set; }
        public GraphAnalysedPoint<TPointModel, TRelationModel>[] Points { get; private set; }
        public HashSet<TPointModel> PendingPoints { get; private set; } = new HashSet<TPointModel>();
        public HashSet<TRelationModel> PendingRelations { get; private set; } = new HashSet<TRelationModel>();

        public GraphAnalysedPoint<TPointModel, TRelationModel> this[Guid Id]
            => Points.Single(point => point.Id == Id);

        public GraphAnalysedPoint<TPointModel, TRelationModel> this[string name]
            => Points.Single(point => point.Name == name);

        public void Process()
        {
            var pendingPoints = PendingPoints.ToArray();
            var pendingRelations = PendingRelations.ToArray();

            var points = PendingPoints.Select(point => new GraphAnalysedPoint<TPointModel, TRelationModel>
            {
                Id = point.Id,
                Name = point.Name,
                Model = point,
            }).ToArray();

            foreach (var pendingPoint in pendingPoints)
            {
                var pendingPoint_Relations = pendingRelations
                    .Where(relation => relation.PointA == pendingPoint.Id || relation.PointB == pendingPoint.Id);

                var relations_PendingPointAsPointB = pendingPoint_Relations.Where(
                    relation => relation.PointB == pendingPoint.Id && relation.Directed);
                points.Find(pendingPoint.Id).Then(_ =>
                {
                    _.From = relations_PendingPointAsPointB.Select(relation =>
                    {
                        return new GraphAnalysedPointLink<TPointModel, TRelationModel>
                        {
                            Point = points.Find(relation.PointA),
                            RelationModel = relation,
                        };
                    }).ToArray();
                });

                var relations_else = pendingPoint_Relations.Where(
                    relation => !(relation.PointB == pendingPoint.Id && relation.Directed));
                points.Find(pendingPoint.Id).Then(_ =>
                {
                    _.To = relations_else.Select(relation =>
                    {
                        if (relation.PointA == pendingPoint.Id)
                        {
                            return new GraphAnalysedPointLink<TPointModel, TRelationModel>
                            {
                                Point = points.Find(relation.PointB),
                                RelationModel = relation,
                            };
                        }
                        else
                        {
                            return new GraphAnalysedPointLink<TPointModel, TRelationModel>
                            {
                                Point = points.Find(relation.PointA),
                                RelationModel = relation,
                            };
                        }
                    }).ToArray();
                });
            }

            Points = points;
            Processed = true;
        }

        public void CheckInstance()
        {
            if (!Processed) throw new NotSupportedException("The graph has not be processed yet.");
        }

        public IGraphPathNode<TPointModel, TRelationModel>[] SearchPath(
            Func<TDerivedClass, Func<TRelationModel, double>, string, string, IGraphPathNode<TPointModel, TRelationModel>[]> searchFunction,
            Func<TRelationModel, double> algorithm, string from, string to)
        {
            CheckInstance();
            return searchFunction(this as TDerivedClass, algorithm, from, to);
        }

        public static TDerivedClass Create<TPoint, TRelation>(IEnumerable<TPoint> points, IEnumerable<TRelation> relations)
            where TPoint : TPointModel, IGraphPoint
            where TRelation : TRelationModel, IGraphRelation
        {
            return new TDerivedClass().Then(_ =>
            {
                _.PendingPoints = new HashSet<TPointModel>(points as IEnumerable<TPointModel>);
                _.PendingRelations = new HashSet<TRelationModel>(relations as IEnumerable<TRelationModel>);
                _.Process();
            });
        }

    }
}
