using System;
using System.Collections.Generic;
using static Trees.RBTreeNodeType;

namespace Trees
{
    public class RBTree<TKey> where TKey : IComparable<TKey>
    {
        public static RBTreeNode<TKey> Leaf;

        public RBTreeNode<TKey> Root { get; private set; }

        public RBTree()
        {
            Leaf = new RBTreeNode<TKey>();
            Root = Leaf;
        }

        public RBTreeNode<TKey> GetNode(TKey key)
        {
            try
            {
                int hashKey = key.GetHashCode();
                RBTreeNode<TKey> node = Root;

                do
                {
                    if (node.HashKey == hashKey)
                        return node;
                    node = hashKey < node.HashKey ? node.Left : node.Right;
                } while (true);
            }
            catch (NullReferenceException)
            {
                throw new KeyNotFoundException("Key not found.");
            }
        }

        public void Add(TKey key)
        {
            RBTreeNode<TKey> newNode = RBTreeNode<TKey>.CreateNode(key, Red);
            Insert(newNode);
        }

        private void Insert(RBTreeNode<TKey> node)
        {
            if (IsEmpty())
            {
                Root = node;
                Root.Color = Black;
                return;
            }
            else
            {
                var workNode = Leaf;
                var currentNode = Root;

                while (currentNode != Leaf)
                {
                    workNode = currentNode;
                    currentNode = node.HashKey < currentNode.HashKey ? currentNode.Left : currentNode.Right;
                }
                node.Parent = workNode;
                if (node.HashKey < workNode.HashKey)
                    workNode.Left = node;
                else
                    workNode.Right = node;
                node.Left = Leaf;
                node.Right = Leaf;
                node.Color = Red;
                if (node.Parent.Parent == null)
                    return;
                InsertFixup(node);
            }
        }

        private void InsertFixup(RBTreeNode<TKey> node)
        {
            while (node.Parent.Color == Red)
            {
                if (node.Parent == node.Parent.Parent.Left)
                {
                    var y = node.Parent.Parent.Right;

                    if (y.Color == Red)
                    {
                        y.Color = Black;
                        node.Parent.Color = Black;
                        node.Parent.Parent.Color = Red;
                        node = node.Parent.Parent;
                    }
                    else
                    {
                        if (node == node.Parent.Right)
                        {
                            node = node.Parent;
                            RotateLeft(node);
                        }
                        node.Parent.Color = Black;
                        node.Parent.Parent.Color = Red;
                        RotateRight(node.Parent.Parent);
                    }
                }
                else
                {
                    var y = node.Parent.Parent.Left;

                    if (y.Color == Red)
                    {
                        y.Color = Black;
                        node.Parent.Color = Black;
                        node.Parent.Parent.Color = Red;
                        node = node.Parent.Parent;
                    }
                    else
                    {
                        if (node == node.Parent.Left)
                        {
                            node = node.Parent;
                            RotateRight(node);
                        }

                        node.Parent.Color = Black;
                        node.Parent.Parent.Color = Red;
                        RotateLeft(node.Parent.Parent);
                    }
                }
                if (node == Root)
                    break;
            }
            Root.Color = Black;
        }

        private void RotateLeft(RBTreeNode<TKey> node)
        {
            var y = node.Right;
            node.Right = y.Left;

            if (y.Left != Leaf)
                y.Left.Parent = node;
            y.Parent = node.Parent;
            if (node.Parent == Leaf)
                Root = y;
            else if (node == node.Parent.Left)
                node.Parent.Left = y;
            else
                node.Parent.Right = y;
            y.Left = node;
            node.Parent = y;
        }

        private void RotateRight(RBTreeNode<TKey> node)
        {
            var y = node.Left;
            node.Left = y.Right;

            if (y.Right != Leaf)
                y.Right.Parent = node;
            y.Parent = node.Parent;
            if (node.Parent == Leaf)
                Root = y;
            else if (node == node.Parent.Left)
                node.Parent.Left = y;
            else
                node.Parent.Right = y;

            y.Right = node;
            node.Parent = y;
        }

        public bool ContainsKey(TKey key)
        {
            try
            {
                var node = GetNode(key);
                return node != null;
            }
            catch (KeyNotFoundException)
            {
                return false;
            }
        }

        public RBTreeNode<TKey> Minimum(RBTreeNode<TKey> node)
        {
            if (IsEmpty())
                throw new Exception("Tree is empty.");
            while (node.Left != Leaf)
                node = node.Left;
            return node;
        }

        public RBTreeNode<TKey> Maximum(RBTreeNode<TKey> node)
        {
            if (IsEmpty())
                throw new Exception("Tree is empty.");
            while (node.Right != Leaf)
                node = node.Right;
            return node;
        }

        public RBTreeNode<TKey> Predecessor(RBTreeNode<TKey> currentNode)
        {
            if (currentNode.Right != Leaf)
                return Maximum(currentNode.Left);
            RBTreeNode<TKey> node = currentNode.Parent;
            while (node != Leaf && currentNode == node.Left)
            {
                currentNode = node;
                node = node.Parent;
            }
            return node;
        }

        public RBTreeNode<TKey> Successor(RBTreeNode<TKey> currentNode)
        {
            if (currentNode.Right != Leaf)
                return Minimum(currentNode.Right);
            RBTreeNode<TKey> node = currentNode.Parent;
            while (node != Leaf && currentNode == node.Right)
            {
                currentNode = node;
                node = node.Parent;
            }
            return node;
        }

        private void Transplant(RBTreeNode<TKey> first, RBTreeNode<TKey> second)
        {
            if (first.Parent == Leaf)
                Root = second;
            else if (first == first.Parent.Left)
                first.Parent.Left = second;
            else
                first.Parent.Right = second;
            second.Parent = first.Parent;
        }

        public void Delete(RBTreeNode<TKey> deleteNode)
        {
            RBTreeNode<TKey> linkedNode, workNode = deleteNode;
            var workNodeOriginalColor = workNode.Color;

            if (deleteNode.Left == Leaf)
            {
                linkedNode = deleteNode.Right;
                Transplant(deleteNode, deleteNode.Right);
            }
            else if (deleteNode.Right == Leaf)
            {
                linkedNode = deleteNode.Left;
                Transplant(deleteNode, deleteNode.Left);
            }
            else
            {
                workNode = Minimum(deleteNode.Right);
                workNodeOriginalColor = workNode.Color;
                linkedNode = workNode.Right;

                if (workNode.Parent == deleteNode)
                    linkedNode.Parent = workNode;
                else
                {
                    Transplant(workNode, workNode.Right);
                    workNode.Right = deleteNode.Right;
                    workNode.Right.Parent = workNode;
                }
                Transplant(deleteNode, workNode);
                workNode.Left = deleteNode.Left;
                workNode.Left.Parent = workNode;
                workNode.Color = deleteNode.Color;
            }

            if (workNode.Color == Black)
                DeleteFixup(linkedNode);
        }

        public bool Contains(RBTreeNode<TKey> node)
        {
            return ContainsKey(node.Key);
        }

        public bool IsEmpty()
        {
            return Root == Leaf;
        }

        private void DeleteFixup(RBTreeNode<TKey> linkedNode)
        {
            RBTreeNode<TKey> node;

            while (linkedNode != Root && linkedNode.Color == Black)
            {
                if (linkedNode == linkedNode.Parent.Left)
                {
                    node = linkedNode.Parent.Right;
                    if (node.Color == Red)
                    {
                        node.Color = Black;
                        linkedNode.Parent.Color = Red;
                        RotateLeft(linkedNode.Parent);
                        node = linkedNode.Parent.Right;
                    }
                    if (node.Left.Color == Black
                                && node.Right.Color == Black)
                    {
                        node.Color = Red;
                        linkedNode = linkedNode.Parent;
                    }
                    else if (node.Right.Color == Black)
                    {
                        node.Left.Color = Black;
                        node.Color = Red;
                        RotateRight(node);
                        node = node.Parent.Right;
                    }
                    node.Color = linkedNode.Parent.Color;
                    node.Parent.Color = Black;
                    node.Right.Color = Black;
                    RotateLeft(linkedNode.Parent);
                    linkedNode = Root;

                }
                else
                {
                    node = linkedNode.Parent.Left;
                    if (node.Color == Red)
                    {
                        node.Color = Black;
                        linkedNode.Parent.Color = Red;
                        RotateRight(linkedNode.Parent);
                        node = linkedNode.Parent.Left;
                    }
                    if (node.Right.Color == Black
                                && node.Left.Color == Black)
                    {
                        node.Color = Red;
                        linkedNode = linkedNode.Parent;
                    }
                    else if (node.Left.Color == Black)
                    {
                        node.Right.Color = Black;
                        node.Color = Red;
                        RotateLeft(node);
                        node = node.Parent.Left;
                        node.Color = linkedNode.Parent.Color;
                        node.Parent.Color = Black;
                        node.Left.Color = Black;
                        RotateRight(linkedNode.Parent);
                        linkedNode = Root;
                    }
                }
            }
            linkedNode.Color = Black;
        }

        private void Traverse(RBTreeNode<TKey> node, List<RBTreeNode<TKey>> nodesList)
        {
            if (node.Right != Leaf)
                Traverse(node.Right, nodesList);
            nodesList.Add(node);
            if (node.Left != Leaf)
                Traverse(node.Left, nodesList);
        }

        public int Height(RBTreeNode<TKey> node)
        {
            if (node == null)
                return 0;

            int lHeight = Height(node.Left);
            int rHeight = Height(node.Right);

            if (lHeight > rHeight)
                return lHeight + 1;
            else
                return rHeight + 1;
        }

        public bool IsBalanced()
        {
            int blackHeight = 0;
            RBTreeNode<TKey> node = Root;

            while (node != null)
            {
                if (node.Color != Red)
                    blackHeight++;
                node = node.Left;
            }
            return IsBalanced(Root, blackHeight);
        }

        private bool IsBalanced(RBTreeNode<TKey> node, int blackHeight)
        {
            if (node == null)
                return blackHeight == 0;
            if (node.Color != Red)
                blackHeight--;
            return IsBalanced(node.Left, blackHeight) && IsBalanced(node.Right, blackHeight);
        }

        public List<RBTreeNode<TKey>> CopyToList(RBTree<TKey> tree)
        {
            List<RBTreeNode<TKey>> nodesList = new List<RBTreeNode<TKey>>();

            Traverse(tree.Root, nodesList);
            return nodesList;
        }
    }
}