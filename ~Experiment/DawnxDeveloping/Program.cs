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
    public class TreeEntity : ITreeEntity
    {
        public Guid Id { get; set; }

        public long Index { get; set; }

        public Guid? Parent { get; set; }

        public string Content { get; set; }
    }

    class Program
    {


        static void Main(string[] args)
        {
            var s = @"# A1
## A2
### A3
a3333";
            var parents = new Stack<Guid?>();
            var entities = s.GetPureLines().Select((v, i) =>
            {
                var entity = new TreeEntity
                {
                    Id = Guid.NewGuid(),
                    Index = i,
                    Parent = parents.Count > 0 ? parents.For(_ => _.Peek()) : null,
                };

                if (v.StartsWith("#"))
                {
                    if (parents.Any())
                        parents.Clear();
                    parents.Push();
                }
            });


        }

    }
}
#endif
