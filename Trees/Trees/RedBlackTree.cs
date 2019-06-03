using System;
using System.Collections.Generic;
using static Trees.RBTreeNodeType;

namespace Trees
{
    public class RedBlackTree<TKey> where TKey : IComparable<TKey>
    {
        public RedBlackTreeNode<TKey> Root { get; private set; }

        public bool IsEmpty()
        {
            return Root == null;
        }

        public void FlipColors(RedBlackTreeNode<TKey> node)
        {
            node.Color = Red;
            node.Left.Color = Black;
            node.Right.Color = Black;
        }

        private bool IsRed(RedBlackTreeNode<TKey> node)
        {
            if (node == null)
                return false;
            return node.Color == Red;
        }

        public void Insert(TKey key)
        {
            Root = InsertNode(Root, key);
            Root.Color = Black;
        }

        private RedBlackTreeNode<TKey> InsertNode(RedBlackTreeNode<TKey> node, TKey key)
        {
            if (node == null)
                return new RedBlackTreeNode<TKey>(key, Red);
            int cmp = key.CompareTo(node.Key);

            if (cmp <= 0)
                node.Left = InsertNode(node.Left, key);
            else if (cmp > 0)
                node.Right = InsertNode(node.Right, key);
            if (IsRed(node.Right) && !IsRed(node.Left))
                node = RotateLeft(node);
            if (IsRed(node.Left) && IsRed(node.Left.Left))
                node = RotateRight(node);
            if (IsRed(node.Left) && IsRed(node.Right))
                FlipColors(node);
            return node;
        }

        public TKey Minimum()
        {
            return Minimum(Root).Key;
        }

        private RedBlackTreeNode<TKey> Minimum(RedBlackTreeNode<TKey> node)
        {
            if (node.Left == null)
                return node;
            return Minimum(node.Left);
        }

        public TKey Maximum()
        {
            return Maximum(Root).Key;
        }

        private RedBlackTreeNode<TKey> Maximum(RedBlackTreeNode<TKey> node)
        {
            if (node.Right == null)
                return node;
            return Maximum(node.Right);
        }

        public RedBlackTreeNode<TKey> RotateLeft(RedBlackTreeNode<TKey> node)
        {
            RedBlackTreeNode<TKey> x = node.Right;

            node.Right = x.Left;
            x.Left = node;
            x.Color = node.Color;
            node.Color = Red;
            return x;
        }

        public RedBlackTreeNode<TKey> RotateRight(RedBlackTreeNode<TKey> node)
        {
            RedBlackTreeNode<TKey> x = node.Left;

            node.Left = x.Right;
            x.Right = node;
            x.Color = node.Color;
            node.Color = Red;
            return x;
        }

        private RedBlackTreeNode<TKey> MoveRedRight(RedBlackTreeNode<TKey> node)
        {
            node.Color = Red;
            node.Left.Color = Black;
            node.Right.Color = Black;
            if (!IsRed(node.Left.Left))
                node = RotateRight(node);
            return node;
        }

        private RedBlackTreeNode<TKey> MoveRedLeft(RedBlackTreeNode<TKey> node)
        {
            FlipColors(node);
            if (IsRed(node.Right.Left))
            {
                node.Right = RotateRight(node.Right);
                node = RotateLeft(node);
            }
            return node;
        }

        private RedBlackTreeNode<TKey> Balance(RedBlackTreeNode<TKey> node)
        {
            if (IsRed(node.Right))
                node = RotateLeft(node);
            if (IsRed(node.Right) && !IsRed(node.Left))
                node = RotateLeft(node);
            if (IsRed(node.Left) && IsRed(node.Left.Left))
                node = RotateRight(node);
            if (IsRed(node.Left) && IsRed(node.Right))
                FlipColors(node);
            return node;
        } 

        private void DeleteMin()
        {
            if (!IsRed(Root.Left) && !IsRed(Root.Right))
                Root.Color = Red;
            Root = DeleteMin(Root);
            if (IsEmpty())
                Root.Color = Black;
        }

        private RedBlackTreeNode<TKey> DeleteMin(RedBlackTreeNode<TKey> node)
        {
            if (node.Left == null)
                return null;
            if (!IsRed(node.Left) && !IsRed(node.Left.Left))
                node = MoveRedLeft(node);
            node.Left = DeleteMin(node.Left);
            return Balance(node);
        }

        private void DeleteMax()
        {
            if (!IsRed(Root.Left) && !IsRed(Root.Right))
                Root.Color = Red;
            Root = DeleteMax(Root);
            if (IsEmpty())
                Root.Color = Black;
        }

        private RedBlackTreeNode<TKey> DeleteMax(RedBlackTreeNode<TKey> node)
        {
            if (IsRed(node.Left))
                node = RotateRight(node);
            if (node.Right == null)
                return null;
            if (!IsRed(node.Right) && !IsRed(node.Right.Left))
                node = MoveRedRight(node);
            node.Right = DeleteMax(node.Right);
            return Balance(node);
        }

        private RedBlackTreeNode<TKey> Delete(RedBlackTreeNode<TKey> node, TKey key)
        {
            if (node == null)
                return null;
            int cmp = key.CompareTo(node.Key);

            if (cmp < 0)
                node.Left = Delete(node.Left, key);
            else if (cmp > 0)
                node.Right = Delete(node.Right, key);
            else
            {
                if (node.Right == null)
                    return node.Left;
                if (node.Left == null)
                    return node.Right;

                RedBlackTreeNode<TKey> t = node;
                node = Minimum(t.Right);
                node.Right = DeleteMin(t.Right);
                node.Left = t.Left;
            }
            return node;
        }

        public void Delete(TKey key)
        {
            if (!IsRed(Root.Left) && !IsRed(Root.Right))
                Root.Color = Red;
            Root = DeleteNode(Root, key);
            if (!IsEmpty())
                Root.Color = Black;
        }

        private RedBlackTreeNode<TKey> DeleteNode(RedBlackTreeNode<TKey> node, TKey key)
        {
            if (key.CompareTo(node.Key) < 0)
            {
                if (!IsRed(node.Left) && !IsRed(node.Left.Left))
                    node = MoveRedLeft(node);
                node.Left = Delete(node.Left, key);
            }
            else
            {
                if (IsRed(node.Left))
                    node = RotateRight(node);
                if (key.CompareTo(node.Key) == 0 && (node.Right == null))
                    return null;
                if (!IsRed(node.Right) && !IsRed(node.Right.Left))
                    node = MoveRedRight(node);
                if (key.CompareTo(node.Key) == 0)
                {
                    node.Key = Minimum(node.Right).Key;
                    node.Right = DeleteMin(node.Right);
                }
                else node.Right = Delete(node.Right, key);
            }
            return Balance(node);
        }

        public RedBlackTreeNode<TKey> GetNode(TKey key)
        {
            try
            {
                RedBlackTreeNode<TKey> node = Root;

                do
                {
                    if (key.CompareTo(node.Key) == 0)
                        return node;
                    node = key.CompareTo(node.Key) < 0 ? node.Left : node.Right;
                } while (true);
            }
            catch (NullReferenceException)
            {
                throw new KeyNotFoundException("Key not found.");
            }
        }
    }
}
