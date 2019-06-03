using System;
using System.Collections.Generic;
using static Trees.RBTreeNodeType;

namespace Trees
{
    public class RBTreeNode<TKey> where TKey : IComparable<TKey>
    {
        public TKey Key;

        public RBTreeNodeType Color;

        public int HashKey;

        public RBTreeNode<TKey> Left { get; set; }

        public RBTreeNode<TKey> Right { get; set; }

        public RBTreeNode<TKey> Parent { get; set; }

        public static RBTreeNode<TKey> CreateNode(TKey key, RBTreeNodeType color)
            => new RBTreeNode<TKey>(key, color);

        public override int GetHashCode() => HashKey;

        internal RBTreeNode()
        {
            Color = Black;
            Parent = Left = Right = null;
        }

        public RBTreeNode(TKey key, RBTreeNodeType color) : this()
        {
            Key = key;
            Color = color;
            HashKey = key.GetHashCode();
            Parent = Left = Right = RBTree<TKey>.Leaf;
        }
    }
}