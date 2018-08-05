namespace Dawnx.Algorithms.Graph
{
    public interface IGraphPathNode<TPointModel, TRelationModel>
        where TPointModel : IGraphPoint
        where TRelationModel : IGraphRelation
    {
        GraphAnalysedPoint<TPointModel, TRelationModel> Point { get; }
        double Distance { get; }
    }

}
