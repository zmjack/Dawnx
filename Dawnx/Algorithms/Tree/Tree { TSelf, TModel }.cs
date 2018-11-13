using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dawnx.Algorithms.Tree
{
    public partial class Tree<TSelf, TModel> : ICloneable
        where TSelf : Tree<TSelf, TModel>, new()
    {
        public Tree()
        {
            var modelType = typeof(TModel);
            if (modelType.IsClass)
            {
                switch (modelType.FullName)
                {
                    case string s when s == typeof(string).FullName:
                        break;

                    default:
                        Model = (TModel)modelType.GetConstructor(new Type[0]).Invoke(null);
                        _Key = Model?.GetHashCode().ToString();
                        break;
                }
            }
        }
        public Tree(TModel model)
        {
            Model = model;
        }

        public Guid Id { get; set; }
        public TSelf Parent { get; private set; }
        public TModel Model { get; set; }

        private string _Key;
        public virtual string Key { get => _Key; set => _Key = value; }

        public bool IsTreeNode => Children.Any();
        public bool IsLeafNode => !Children.Any();
        public bool IsRoot => Parent is null;

        public HashSet<TSelf> Children { get; private set; } = new HashSet<TSelf>();
        public IEnumerable<TSelf> Trees => Children.Where(x => x.IsTreeNode);
        public IEnumerable<TSelf> Leafs => Children.Where(x => x.IsLeafNode);

        public IEnumerable<TSelf> RecursiveChildren
        {
            get
            {
                foreach (var child in Children)
                {
                    yield return child;

                    if (child.IsTreeNode)
                        foreach (var child_ in child.RecursiveChildren)
                            yield return child_;
                }
            }
        }
        public IEnumerable<TSelf> RecursiveTrees
        {
            get
            {
                foreach (var tree in Trees)
                {
                    yield return tree;

                    if (tree.IsTreeNode)
                        foreach (var tree_ in tree.RecursiveTrees)
                            yield return tree_;
                }
            }
        }
        public IEnumerable<TSelf> RecursiveLeafs
        {
            get
            {
                foreach (var child in Children)
                {
                    if (child.IsTreeNode)
                        foreach (var leaf in child.RecursiveLeafs)
                            yield return leaf;
                    else yield return child;
                }
            }
        }

        public string Path
        {
            get
            {
                var path = new Stack<TSelf>();
                path.Push(this as TSelf);
                for (var node = this; node.Parent != null; node = node.Parent)
                    path.Push(node.Parent);

                return string.Join("/", path.Select(node => node.Key));
            }
        }

        public void Add(string key, TSelf node)
        {
            node.Key = key;
            Add(node);
        }
        public void Add(TSelf node)
        {
            node.Parent = this as TSelf;
            Children.Add(node);
        }
        public void AddRange(TSelf[] nodes)
        {
            nodes.AsParallel().ForAll(node => node.Parent = this as TSelf);
            nodes.Each(node => Add(node));
        }

        public TSelf Clone() => (this as ICloneable).Clone() as TSelf;
        object ICloneable.Clone()
        {
            return new TSelf
            {
                Id = Id,
                Model = Model,
            };
        }

        public string Description
        {
            get
            {
                var tree = new StringBuilder();
                if (!IsRoot)
                    tree.AppendLine(Key);

                foreach (var child in Children)
                {
                    if (!IsRoot)
                        tree.AppendLine("  " + child.Description.Replace("\r\n", "\r\n  "));
                    else tree.AppendLine(child.Description);
                }

                if (tree.Length > 0)
                    return tree.ToString().Slice(0, -2);
                else return "";
            }
        }

        public TSelf this[string key]
        {
            get => Children.First(child => child.Key == key);
            set { value.Key = key; Add(value); }
        }

        protected virtual void CreateForProperties(TSelf node, object entity) { }

    }
}
