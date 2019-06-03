using System;
using System.Collections.Generic;
using static Trees.RBTreeNodeType;

namespace Trees
{
    public class RedBlackTreeNode<TKey> where TKey : IComparable<TKey>
    {
        public TKey Key;

        public RBTreeNodeType Color;

        public RedBlackTreeNode<TKey> Left { get; set; }

        public RedBlackTreeNode<TKey> Right { get; set; }

        public static RedBlackTreeNode<TKey> CreateNode(TKey key, RBTreeNodeType color)
            => new RedBlackTreeNode<TKey>(key, color);

        internal RedBlackTreeNode()
        {
            Color = Black;
            Left = Right = null;
        }

        public RedBlackTreeNode(TKey key, RBTreeNodeType color) : this()
        {
            Key = key;
            Color = color;
        }
    }
}
