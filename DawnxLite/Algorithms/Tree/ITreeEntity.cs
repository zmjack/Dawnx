using System;

namespace Dawnx.Algorithms.Tree
{
    public interface ITreeEntity
    {
        Guid Id { get; }
        long Index { get; }
        Guid? Parent { get; }
    }

}
