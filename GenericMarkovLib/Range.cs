using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericMarkovLib
{
    internal class Range<T>
    {
        public Node<T> Value { get; }
        public int From { get; }
        public int To { get; }

        public Range(Node<T> value, int from, int to)
        {
            Value = value;
            From = from;
            To = to;
        }

        public bool Contains(int value)
        {
            return value >= From && value < To;
        }
    }
}
