using System;
using System.Collections.Generic;

namespace Trees
{
    public class BSTree<TKey> where TKey : IComparable<TKey>
    {
        public BSTreeNode<TKey> Root { get; private set; }

        public static BSTreeNode<TKey> Leaf;

        private IComparer<TKey> comparer;

        public BSTree()
        {
            Leaf = new BSTreeNode<TKey>();
            Root = Leaf;
        }

        public int Compare(TKey firstKey, TKey secondKey)
        {
            if (comparer == null)
                return ((IComparable<TKey>)firstKey).CompareTo(secondKey);
            else
                return comparer.Compare(firstKey, secondKey);
        }

        public BSTreeNode<TKey> GetNode(TKey inputKey)
        {
            BSTreeNode<TKey> node = Root;

            try
            {
                do
                {
                    if (Compare(inputKey, node.Key) == 0)
                        return node;
                    node = Compare(inputKey, node.Key) > 0 ? node.Right : node.Left;
                } while (true);
            }
            catch (NullReferenceException)
            {
                throw new KeyNotFoundException("Key not found.");
            }
        }

        protected BSTreeNode<TKey> Search(BSTreeNode<TKey> node, TKey inputKey)
        {
            int c = 0;

            if (node == Leaf || (c = inputKey.CompareTo(node.Key)) == 0)
                return node;
            return c < 0 ? Search(node.Left, inputKey) : Search(node.Right, inputKey);
        }

        public BSTreeNode<TKey> GetNode(BSTreeNode<TKey> node, TKey inputKey)
        {
            try
            {
                return Search(node, inputKey);
            }
            catch (NullReferenceException)
            {
                throw new KeyNotFoundException("Key not found.");
            }
        }

        public BSTreeNode<TKey> Minimum(BSTreeNode<TKey> node)
        {
            while (node.Left != Leaf)
                node = node.Left;
            return node;
        }

        public BSTreeNode<TKey> Maximum(BSTreeNode<TKey> node)
        {
            while (node.Right != Leaf)
                node = node.Right;
            return node;
        }

        public BSTreeNode<TKey> Successor(BSTreeNode<TKey> thisNode)
        {
            if (thisNode.Right != Leaf)
                return Minimum(thisNode.Right);
            BSTreeNode<TKey> node = thisNode.Parent;
            while (node != Leaf && thisNode == node.Right)
            {
                thisNode = node;
                node = node.Parent;
            }
            return node;
        }

        public BSTreeNode<TKey> Predecessor(BSTreeNode<TKey> thisNode)
        {
            if (thisNode.Left != Leaf)
                return Maximum(thisNode.Left);
            BSTreeNode<TKey> node = thisNode.Parent;
            while (node != Leaf && thisNode == node.Left)
            {
                thisNode = node;
                node = node.Parent;
            }
            return node;
        }

        public bool IsEmpty()
        {
            return Root == Leaf;
        }

        public void Add(TKey key)
        {
            BSTreeNode<TKey> newNode = BSTreeNode<TKey>.CreateNode(key);
            Insert(newNode);
        }

        public bool ContainsKey(TKey key)
        {
            try
            {
                BSTreeNode<TKey> node = GetNode(key);
                return node != null;
            }
            catch (KeyNotFoundException)
            {
                return false;
            }
        }

        private void Insert(BSTreeNode<TKey> node)
        {
            BSTreeNode<TKey> y = Leaf;
            BSTreeNode<TKey> x = Root;

            while (x != Leaf)
            {
                y = x;
                if (node.CompareTo(x) < 0)
                    x = x.Left;
                else
                    x = x.Right;
            }
            node.Parent = y;
            if (IsEmpty())
                Root = node;
            else if (node.CompareTo(y) < 0)
                y.Left = node;
            else
                y.Right = node;
        }

        public void Transplant(BSTreeNode<TKey> first, BSTreeNode<TKey> second)
        {
            if (first.Parent == Leaf)
                Root = second;
            else if (first == first.Parent.Left)
                first.Parent.Left = second;
            else
                first.Parent.Right = second;
            if (second != Leaf)
                second.Parent = first.Parent;
        }

        public void Delete(BSTreeNode<TKey> node)
        {
            BSTreeNode<TKey> y = Leaf;

            if (node.Left == Leaf)
                Transplant(node, node.Right);
            else if (node.Right == Leaf)
                Transplant(node, node.Left);
            else
                y = Minimum(node.Right);
            if (y.Parent != node)
            {
                Transplant(y, y.Right);
                y.Right = node.Right;
                y.Right.Parent = y;
            }
            Transplant(node, y);
            y.Left = node.Left;
            y.Left.Parent = y;
        }

        protected void InOrderWalk(BSTreeNode<TKey> node, List<BSTreeNode<TKey>> nodesList)
        {
            if (node != Leaf)
            {
                InOrderWalk(node.Left, nodesList);
                nodesList.Add(node);
                InOrderWalk(node.Right, nodesList);
            }
        }

        public List<BSTreeNode<TKey>> CopyToList(BSTree<TKey> tree)
        {
            List<BSTreeNode<TKey>> nodesList = new List<BSTreeNode<TKey>>();

            InOrderWalk(tree.Root, nodesList);
            return nodesList;
        }

        public int Rank(TKey key)
        {
            int nodeRank = 0;
            return Rank(key, Root, ref nodeRank);
        }

        private int Rank(TKey key, BSTreeNode<TKey> node, ref int nodeRank)
        {
            if (node == Leaf)
                return 0;

            int cmp = key.CompareTo(node.Key);

            if (cmp < 0)
            {
                nodeRank++;
                return Rank(key, node.Left, ref nodeRank);
            }
            else if (cmp > 0)
            {
                nodeRank++;
                return Rank(key, node.Right, ref nodeRank);
            }
            else
                return nodeRank;
        }
    }
}