namespace Dawnx.Algorithms.Graph
{
    public class GraphAnalysedPointLink<TPointModel, TRelationModel>
        where TPointModel : IGraphPoint
        where TRelationModel : IGraphRelation
    {
        public GraphAnalysedPoint<TPointModel, TRelationModel> Point;
        public TRelationModel RelationModel;
    }
}
