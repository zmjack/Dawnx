using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dawnx.Algorithms.Tree
{
    public class Tree : Tree<Tree, string>
    {
        public Tree() { }
        public Tree(string model) : base(model) { }

        public override string Key { get => Model; set => Model = value; }
    }

    public class Tree<TModel> : Tree<Tree<TModel>, TModel>
    {
        public Tree() { }
        public Tree(TModel model) : base(model) { }
    }

    public class Tree<TDerivedClass, TModel> : ICloneable
        where TDerivedClass : Tree<TDerivedClass, TModel>, new()
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
        public TDerivedClass Parent { get; private set; }
        public TModel Model { get; set; }

        private string _Key;
        public virtual string Key { get => _Key; set => _Key = value; }

        public bool IsTree => Children.Any();
        public bool IsLeaf => !Children.Any();
        public bool IsRoot => Parent is null;

        public HashSet<TDerivedClass> Children { get; private set; } = new HashSet<TDerivedClass>();
        public IEnumerable<TDerivedClass> Trees => Children.Where(x => x.IsTree);
        public IEnumerable<TDerivedClass> Leafs => Children.Where(x => x.IsLeaf);

        public IEnumerable<TDerivedClass> RecursiveChildren
        {
            get
            {
                foreach (var child in Children)
                {
                    yield return child;

                    if (child.IsTree)
                        foreach (var child_ in child.RecursiveChildren)
                            yield return child_;
                }
            }
        }
        public IEnumerable<TDerivedClass> RecursiveTrees
        {
            get
            {
                foreach (var tree in Trees)
                {
                    yield return tree;

                    if (tree.IsTree)
                        foreach (var tree_ in tree.RecursiveTrees)
                            yield return tree_;
                }
            }
        }
        public IEnumerable<TDerivedClass> RecursiveLeafs
        {
            get
            {
                foreach (var child in Children)
                {
                    if (child.IsTree)
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
                var path = new Stack<TDerivedClass>();
                path.Push(this as TDerivedClass);
                for (var node = this; node.Parent != null; node = node.Parent)
                    path.Push(node.Parent);

                return string.Join("/", path.Select(node => node.Key));
            }
        }

        public void Add(string key, TDerivedClass node)
        {
            node.Key = key;
            Add(node);
        }
        public void Add(TDerivedClass node)
        {
            node.Parent = this as TDerivedClass;
            Children.Add(node);
        }
        public void AddRange(TDerivedClass[] nodes)
        {
            nodes.AsParallel().ForAll(node => node.Parent = this as TDerivedClass);
            nodes.Each(node => Add(node));
        }

        public TDerivedClass Clone() => (this as ICloneable).Clone() as TDerivedClass;
        object ICloneable.Clone()
        {
            return new TDerivedClass
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

        public TDerivedClass this[string key]
        {
            get => Children.First(child => child.Key == key);
            set { value.Key = key; Add(value); }
        }

        protected virtual void CreateForProperties(TDerivedClass node, object entity) { }

        public static TDerivedClass Create<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : TModel, ITreeEntity
        {
            var pendingNodeQueue = new Queue<TDerivedClass>();

            entities
                .Where(entity => entity.Parent is null)
                .OrderBy(entity => entity.Index)
                .Each(entity =>
                {
                    pendingNodeQueue.Enqueue(new TDerivedClass
                    {
                        Id = entity.Id,
                        Model = entity,
                    }.Self(_ => new TDerivedClass().CreateForProperties(_, entity)));
                });

            var node = new TDerivedClass()
                .Self(_ => _.AddRange(pendingNodeQueue.ToArray()));

            while (pendingNodeQueue.Any())
            {
                var pendingNode = pendingNodeQueue.Dequeue();

                entities
                    .Where(entity => entity.Parent == pendingNode.Id)
                    .OrderBy(entity => entity.Index)
                    .Each(entity =>
                    {
                        var item = new TDerivedClass
                        {
                            Id = entity.Id,
                            Model = entity,
                        }.Self(_ => new TDerivedClass().CreateForProperties(_, entity));

                        pendingNode.Add(item);
                        pendingNodeQueue.Enqueue(item);
                    });
            }

            return node;
        }

    }
}
