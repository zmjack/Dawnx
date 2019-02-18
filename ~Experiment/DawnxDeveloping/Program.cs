#if !USE
using System.Linq;
using System.Collections.Generic;
using System;
using Dawnx.Diagnostics;
using Dawnx.Con;
using System.Threading;
using Dawnx;
using Dawnx.Algorithms.Tree;

namespace DawnxDevloping
{
    public class MyTree : Tree<MyTree, TreeEntity>
    {
        public override string Key => Model.Content;
    }

    public class TreeEntity : ITreeEntity
    {
        public Guid Id { get; set; }

        public long Index { get; set; }

        public Guid? Parent { get; set; }

        public int TitleLevel { get; set; }

        public string Content { get; set; }
    }

    class Program
    {


        static void Main(string[] args)
        {
            var str = @"# A1
## A2
### A3
a3333
b1234
# B2
213
1234";
            var parents = new Stack<Guid?>();
            var entities = str.GetPureLines().Select((v, i) =>
            {
                var entity = new TreeEntity
                {
                    Id = Guid.NewGuid(),
                    Index = i,
                };

                var titleLevel = v.Project("^(#+) ")?.Length ?? 0;

                switch (titleLevel)
                {
                    case int l when l > 0:
                        if (parents.Count < titleLevel - 1) throw new ArgumentException("Argument is invalid.");

                        if (parents.Count >= titleLevel - 1)
                        {
                            for (int j = 0; parents.Count > titleLevel - 1; j++)
                                parents.Pop();

                            entity.Content = v.Substring(titleLevel + 1);
                            entity.TitleLevel = titleLevel;
                            entity.Parent = parents.Any() ? parents.Peek() : null;
                            parents.Push(entity.Id);
                        }
                        break;

                    default:
                        entity.Content = v;
                        entity.TitleLevel = titleLevel;
                        entity.Parent = parents.Peek();
                        break;
                }

                return entity;
            }).ToArray();

            var tree = MyTree.Create(entities);
        }

    }
}
#endif
