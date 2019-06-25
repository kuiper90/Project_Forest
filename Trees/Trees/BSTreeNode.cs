using System;

namespace Trees
{
    public class BSTreeNode<TKey> : IComparable<BSTreeNode<TKey>> where TKey : IComparable<TKey>
    {
        public TKey Key;

        public BSTreeNode<TKey> Left { get; set; }

        public BSTreeNode<TKey> Right { get; set; }

        public BSTreeNode<TKey> Parent { get; set; }

        public static BSTreeNode<TKey> CreateNode(TKey key)
            => new BSTreeNode<TKey>(key);

        internal BSTreeNode()
        {
            Left = Right = null;
            Key = default(TKey);
        }

        public BSTreeNode(TKey key) : this()
        {
            Key = key;
            Parent = Left = Right = BSTree<TKey>.Leaf;
        }

        public int CompareTo(BSTreeNode<TKey> node)
        {
            return this.Key.CompareTo(node.Key);
        }
    }
}
