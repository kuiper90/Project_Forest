using System;
using System.Collections.Generic;

namespace Trees
{
    public class AVLTreeNode<TKey>
    {
        public TKey Key;

        public int Height { get; set; }

        public int Size { get; set; }

        public AVLTreeNode<TKey> Left { get; set; }

        public AVLTreeNode<TKey> Right { get; set; }

        public AVLTreeNode(TKey key, int height, int size)
        {
            this.Key = key;
            this.Size = size;
            this.Height = height;
        }
    }
}
