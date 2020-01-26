using System;

namespace Sequence
{
    public interface SequenceNode
    {
        void Play();
        bool IsComplete();
    }
}