namespace Dawnx.Algorithms.Tree
{
    public class Tree<TModel> : Tree<Tree<TModel>, TModel>
    {
        public Tree() { }
        public Tree(TModel model) : base(model) { }
    }

}
