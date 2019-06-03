using System;
using System.Collections.Generic;

namespace Trees
{
    public class AVLTree<TKey> where TKey : IComparable<TKey>
    {
        public AVLTreeNode<TKey> Root { get; private set; }

        public bool IsEmpty()
        {
            return Root == null;
        }

        public int Size()
        {
            return Size(Root);
        }

        private int Size(AVLTreeNode<TKey> x)
        {
            if (x == null)
                return 0;
            return x.Size;
        }

        public int Height()
        {
            return Height(Root);
        }

        private int Height(AVLTreeNode<TKey> x)
        {
            if (x == null) return -1;
            return x.Height;
        }

        public AVLTreeNode<TKey> GetNode(TKey key)
        {
            if (key == null)
                throw new ArgumentNullException("Input key is null.");
            AVLTreeNode<TKey> x = GetNode(Root, key);
            if (x == null)
                throw new KeyNotFoundException("Key not found.");
            return x;
        }

        private AVLTreeNode<TKey> GetNode(AVLTreeNode<TKey> x, TKey key)
        {
            if (x == null)
                return null;
            int cmp = key.CompareTo(x.Key);

            if (cmp < 0)
                return GetNode(x.Left, key);
            else if (cmp > 0)
                return GetNode(x.Right, key);
            else
                return x;
        }

        public bool Contains(TKey key)
        {
            return GetNode(Root, key) != null;
        }

        public void Insert(TKey key)
        {
            if (key == null)
                throw new ArgumentNullException("Input key is null.");
            Root = Insert(Root, key);
        }

        private AVLTreeNode<TKey> Insert(AVLTreeNode<TKey> x, TKey key)
        {
            if (x == null)
                return new AVLTreeNode<TKey>(key, 0, 1);
            int cmp = key.CompareTo(x.Key);

            if (cmp <= 0)
            {
                x.Left = Insert(x.Left, key);
            }
            else if (cmp > 0)
            {
                x.Right = Insert(x.Right, key);
            }
            x.Size = 1 + Size(x.Left) + Size(x.Right);
            x.Height = 1 + Math.Max(Height(x.Left), Height(x.Right));
            return Balance(x);
        }

        private AVLTreeNode<TKey> Balance(AVLTreeNode<TKey> x)
        {
            if (BalanceFactor(x) < -1)
            {
                if (BalanceFactor(x.Right) > 0)
                {
                    x.Right = RotateRight(x.Right);
                }
                x = RotateLeft(x);
            }
            else if (BalanceFactor(x) > 1)
            {
                if (BalanceFactor(x.Left) < 0)
                {
                    x.Left = RotateLeft(x.Left);
                }
                x = RotateRight(x);
            }
            return x;
        }

        private int BalanceFactor(AVLTreeNode<TKey> x)
        {
            return Height(x.Left) - Height(x.Right);
        }

        private AVLTreeNode<TKey> RotateRight(AVLTreeNode<TKey> x)
        {
            AVLTreeNode<TKey> y = x.Left;

            x.Left = y.Right;
            y.Right = x;
            y.Size = x.Size;
            x.Size = 1 + Size(x.Left) + Size(x.Right);
            x.Height = 1 + Math.Max(Height(x.Left), Height(x.Right));
            y.Height = 1 + Math.Max(Height(y.Left), Height(y.Right));
            return y;
        }

        private AVLTreeNode<TKey> RotateLeft(AVLTreeNode<TKey> x)
        {
            AVLTreeNode<TKey> y = x.Right;
            x.Right = y.Left;
            y.Left = x;
            y.Size = x.Size;
            x.Size = 1 + Size(x.Left) + Size(x.Right);
            x.Height = 1 + Math.Max(Height(x.Left), Height(x.Right));
            y.Height = 1 + Math.Max(Height(y.Left), Height(y.Right));
            return y;
        }

        public void Delete(TKey key)
        {
            if (key == null)
                throw new ArgumentNullException("Input key is null.");
            if (!Contains(key))
                return;
            Root = Delete(Root, key);
        }

        private AVLTreeNode<TKey> Delete(AVLTreeNode<TKey> x, TKey key)
        {
            int cmp = key.CompareTo(x.Key);

            if (cmp < 0)
            {
                x.Left = Delete(x.Left, key);
            }
            else if (cmp > 0)
            {
                x.Right = Delete(x.Right, key);
            }
            else
            {
                if (x.Left == null)
                    return x.Right;
                else if (x.Right == null)
                    return x.Left;
                else
                {
                    AVLTreeNode<TKey> y = x;
                    x = Min(y.Right);
                    x.Right = DeleteMin(y.Right);
                    x.Left = y.Left;
                }
            }
            x.Size = 1 + Size(x.Left) + Size(x.Right);
            x.Height = 1 + Math.Max(Height(x.Left), Height(x.Right));
            return Balance(x);
        }

        public void DeleteMin()
        {
            if (IsEmpty())
                throw new KeyNotFoundException("Key not found.");
            Root = DeleteMin(Root);
        }

        private AVLTreeNode<TKey> DeleteMin(AVLTreeNode<TKey> x)
        {
            if (x.Left == null)
                return x.Right;
            x.Left = DeleteMin(x.Left);
            x.Size = 1 + Size(x.Left) + Size(x.Right);
            x.Height = 1 + Math.Max(Height(x.Left), Height(x.Right));
            return Balance(x);
        }

        public void DeleteMax()
        {
            if (IsEmpty())
                throw new KeyNotFoundException("Key not found.");
            Root = DeleteMax(Root);
        }

        private AVLTreeNode<TKey> DeleteMax(AVLTreeNode<TKey> x)
        {
            if (x.Right == null)
                return x.Left;
            x.Right = DeleteMax(x.Right);
            x.Size = 1 + Size(x.Left) + Size(x.Right);
            x.Height = 1 + Math.Max(Height(x.Left), Height(x.Right));
            return Balance(x);
        }

        public TKey Min()
        {
            if (IsEmpty())
                throw new KeyNotFoundException("Key not found.");
            return Min(Root).Key;
        }

        private AVLTreeNode<TKey> Min(AVLTreeNode<TKey> x)
        {
            if (x.Left == null)
                return x;
            return Min(x.Left);
        }

        public TKey Max()
        {
            if (IsEmpty())
                throw new KeyNotFoundException("Key not found.");
            return Max(Root).Key;
        }

        private AVLTreeNode<TKey> Max(AVLTreeNode<TKey> x)
        {
            if (x.Right == null)
                return x;
            return Max(x.Right);
        }

        public List<AVLTreeNode<TKey>> CopyToList()
        {
            List<AVLTreeNode<TKey>> nodesList = new List<AVLTreeNode<TKey>>();
            CopyToList(Root, nodesList);
            return nodesList;
        }

        private void CopyToList(AVLTreeNode<TKey> x, List<AVLTreeNode<TKey>> nodesList)
        {
            if (x == null)
                return;
            CopyToList(x.Left, nodesList);
            nodesList.Add(x);
            CopyToList(x.Right, nodesList);
        }
    }
}