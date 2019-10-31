using System;
using System.Collections.Generic;
using System.Linq;

namespace Dawnx.Algorithms.GraphAlgorithm
{
    public class GraphAnalysedPoint<TPointModel, TRelationModel>
        where TPointModel : IGraphPoint
        where TRelationModel : IGraphRelation
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public GraphAnalysedPointLink<TPointModel, TRelationModel>[] From { get; internal set; }
        public GraphAnalysedPointLink<TPointModel, TRelationModel>[] To { get; internal set; }
        public TPointModel Model { get; internal set; }

        public GraphAnalysedPointLink<TPointModel, TRelationModel> this[Guid id]
            => To.Single(to => to.Point.Id == id);

        public GraphAnalysedPointLink<TPointModel, TRelationModel> this[string name]
            => To.Single(to => to.Point.Name == name);
    }

    public static class DawnGraphAnalysedPoint
    {
        public static GraphAnalysedPoint<TPointModel, TRelationModel> Find<TPointModel, TRelationModel>(
            this IEnumerable<GraphAnalysedPoint<TPointModel, TRelationModel>> @this, Guid id)
            where TPointModel : IGraphPoint
            where TRelationModel : IGraphRelation
        {
            return @this.Single(point => point.Id == id);
        }

        public static GraphAnalysedPoint<TPointModel, TRelationModel> Find<TPointModel, TRelationModel>(
            this IEnumerable<GraphAnalysedPoint<TPointModel, TRelationModel>> @this, string name)
            where TPointModel : IGraphPoint
            where TRelationModel : IGraphRelation
        {
            return @this.Single(point => point.Name == name);
        }

    }

}
