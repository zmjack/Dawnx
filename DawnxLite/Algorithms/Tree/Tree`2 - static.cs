using NStandard;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dawnx.Algorithms.Tree
{
    public partial class Tree<TSelf, TModel> : ICloneable
        where TSelf : Tree<TSelf, TModel>, new()
    {
        public static TSelf Create<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : TModel, ITreeEntity
        {
            var pendingNodeQueue = new Queue<TSelf>();

            entities
                .Where(entity => entity.Parent is null)
                .OrderBy(entity => entity.Index)
                .Then(self =>
                {
                    foreach (var entity in self)
                    {
                        pendingNodeQueue.Enqueue(new TSelf
                        {
                            Id = entity.Id,
                            Model = entity,
                        }.Then(_ => new TSelf().CreateForProperties(_, entity)));
                    }
                });

            var node = new TSelf()
                .Then(_ => _.AddRange(pendingNodeQueue.ToArray()));

            while (pendingNodeQueue.Any())
            {
                var pendingNode = pendingNodeQueue.Dequeue();

                entities
                    .Where(entity => entity.Parent == pendingNode.Id)
                    .OrderBy(entity => entity.Index)
                    .Then(self =>
                    {
                        foreach (var entity in self)
                        {
                            var item = new TSelf
                            {
                                Id = entity.Id,
                                Model = entity,
                            }.Then(_ => new TSelf().CreateForProperties(_, entity));

                            pendingNode.Add(item);
                            pendingNodeQueue.Enqueue(item);
                        }
                    });
            }

            return node;
        }

    }
}
