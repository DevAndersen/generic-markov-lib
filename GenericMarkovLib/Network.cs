using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericMarkovLib
{
    public class Network<T>
    {
        private List<Node<T>> nodes = new List<Node<T>>();
        private Node<T> currentNode;
        private Random rand;

        public Network(T[] values, Random rand)
        {
            this.rand = rand;
            InitNodes(values);
            ConnectNodes(values);
            currentNode = nodes[rand.Next(nodes.Count)];
        }

        public Network(T[] values, int seed) : this(values, new Random(seed))
        {

        }

        public Network(T[] values) : this(values, new Random())
        {

        }

        private void InitNodes(T[] values)
        {
            foreach (T value in values)
            {
                bool alreadyExists = false;
                foreach (Node<T> existingNode in nodes)
                {
                    if (existingNode.Value.Equals(value))
                    {
                        alreadyExists = true;
                    }
                }
                if (!alreadyExists)
                {
                    Node<T> newNode = new Node<T>(value, rand);
                    nodes.Add(newNode);
                }
            }
        }

        private void ConnectNodes(T[] values)
        {
            for (int i = 0; i < values.Length - 1; i++)
            {
                T value = values[i];
                T nextValue = values[i + 1];

                Node<T> nodeWithNextValue = null;

                foreach (Node<T> node in nodes)
                {
                    if (node.Value.Equals(nextValue))
                    {
                        nodeWithNextValue = node;
                    }
                }

                foreach (Node<T> node in nodes)
                {
                    if (node.Value.Equals(value))
                    {
                        node.AddOrStrengthenLink(nodeWithNextValue);
                    }
                }
            }
        }

        public T Next()
        {
            if (nodes.Count == 0)
            {
                throw new ArgumentOutOfRangeException("No nodes in network.");
            }

            if (HasNext())
            {
                T values = currentNode.Value;
                if (!currentNode.HasLinks())
                {
                    currentNode = null;
                }
                else
                {
                    currentNode = currentNode.NextNode();
                }
                return values;
            }
            else
            {
                throw new ArgumentOutOfRangeException("The current node has no child nodes.");
            }
        }

        public bool HasNext()
        {
            return currentNode != null;
        }
    }
}
