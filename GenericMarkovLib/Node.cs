using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericMarkovLib
{
    internal class Node<T>
    {
        public T Value { get; }
        private List<KeyValuePair<Node<T>, int>> links = new List<KeyValuePair<Node<T>, int>>();
        private Random rand;

        public Node(T value, Random rand)
        {
            Value = value;
            this.rand = rand;
        }

        public void AddOrStrengthenLink(Node<T> node)
        {
            bool linkDetected = false;
            for (int i = 0; i < links.Count; i++)
            {
                KeyValuePair<Node<T>, int> link = links[i];
                if (link.Value.Equals(node.Value))
                {
                    linkDetected = true;
                    links[i] = new KeyValuePair<Node<T>, int>(link.Key, link.Value + 1);
                }
            }
            if (!linkDetected)
            {
                links.Add(new KeyValuePair<Node<T>, int>(node, 1));
            }
        }

        public Node<T> NextNode()
        {
            int total = 0;
            List<Range<T>> ranges = new List<Range<T>>();

            foreach (KeyValuePair<Node<T>, int> link in links)
            {
                ranges.Add(new Range<T>(link.Key, total, total + link.Value));
                total += link.Value;
            }

            int roll = rand.Next(total);

            foreach (Range<T> range in ranges)
            {
                if (range.Contains(roll))
                {
                    return range.Value;
                }
            }
            throw new IndexOutOfRangeException("Roll fell outside ranges.");
        }

        public bool HasLinks()
        {
            return links.Count != 0;
        }

        public override bool Equals(object objOther)
        {
            if (objOther is Node<T> other)
            {
                return Value.Equals(other.Value);
            }
            return false;
        }

        public override int GetHashCode()
        {
            var hashCode = 1985199848;
            hashCode = hashCode * -1521134295 + EqualityComparer<T>.Default.GetHashCode(Value);
            hashCode = hashCode * -1521134295 + EqualityComparer<List<KeyValuePair<Node<T>, int>>>.Default.GetHashCode(links);
            return hashCode;
        }
    }
}
