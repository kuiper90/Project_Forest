using System;
using System.Collections.Generic;
using Trees;
using Xunit;

namespace UnitTest_Trees
{
    public class UnitTest_AVLTree
    {
        [Fact]
        public void InsertNode_InEmptyTree_ShouldBe_Root()
        {
            AVLTree<int> tree = new AVLTree<int>();
            tree.Insert(10);
            Assert.True(tree.Root.Key == 10);
        }

        [Fact]
        public void InsertNode_WithKey_SmallerThan_RootKey_ShouldBe_OnLeft()
        {
            AVLTree<int> tree = new AVLTree<int>();
            tree.Insert(10);
            tree.Insert(9);
            Assert.True(tree.Root.Key == 10);
            Assert.True(tree.Root.Left.Key == 9);
        }

        [Fact]
        public void InsertNode_WithKey_EqualWith_RootKey_ShouldBe_OnRight()
        {
            AVLTree<int> tree = new AVLTree<int>();
            tree.Insert(10);
            tree.Insert(10);
            Assert.True(tree.Root.Key == 10);
            Assert.True(tree.Root.Left.Key == 10);
        }

        [Fact]
        public void InsertNode_WithKey_HigherThan_RootKey_ShouldBe_OnRight()
        {
            AVLTree<int> tree = new AVLTree<int>();
            tree.Insert(10);
            tree.Insert(100);
            Assert.True(tree.Root.Key == 10);
            Assert.True(tree.Root.Right.Key == 100);
        }

        [Fact]
        public void InsertNodes_Tree_ShouldBe_Sorted()
        {
            AVLTree<int> tree = new AVLTree<int>();
            tree.Insert(10);
            tree.Insert(5);
            tree.Insert(7);
            tree.Insert(9);
            tree.Insert(20);
            tree.Insert(6);
            tree.Insert(1);
            tree.Insert(13);
            tree.Insert(53);
            tree.Insert(100);
            Assert.True(tree.Root.Key == 7);
            Assert.True(tree.Root.Right.Right.Right.Key == 100);
        }

        [Fact]
        public void GetMin_Should_Return_MinKey()
        {
            AVLTree<int> tree = new AVLTree<int>();
            tree.Insert(10);
            tree.Insert(5);
            tree.Insert(7);
            tree.Insert(9);
            tree.Insert(20);
            tree.Insert(6);
            tree.Insert(1);
            tree.Insert(13);
            tree.Insert(53);
            tree.Insert(100);
            Assert.True(tree.Min() == 1);
        }

        [Fact]
        public void GetMin_FromEmptyTree_Should_ThrowException()
        {
            AVLTree<int> tree = new AVLTree<int>();
            var exception = Assert.Throws<KeyNotFoundException>(() => tree.Min());
            Assert.True(exception.Message == "Key not found.");
        }

        [Fact]
        public void GetMax_Should_Return_MaxKey()
        {
            AVLTree<int> tree = new AVLTree<int>();
            tree.Insert(10);
            tree.Insert(5);
            tree.Insert(7);
            tree.Insert(9);
            tree.Insert(20);
            tree.Insert(6);
            tree.Insert(1);
            tree.Insert(13);
            tree.Insert(53);
            tree.Insert(100);
            Assert.True(tree.Max() == 100);
        }

        [Fact]
        public void GetMax_FromEmptyTree_Should_ThrowException()
        {
            AVLTree<int> tree = new AVLTree<int>();
            var exception = Assert.Throws<KeyNotFoundException>(() => tree.Max());
            Assert.True(exception.Message == "Key not found.");
        }

        [Fact]
        public void GetNode_Should_ReturnNode()
        {
            AVLTree<int> tree = new AVLTree<int>();
            tree.Insert(10);
            tree.Insert(5);
            tree.Insert(7);
            tree.Insert(9);
            tree.Insert(20);
            tree.Insert(6);
            tree.Insert(1);
            tree.Insert(13);
            tree.Insert(53);
            tree.Insert(100);
            Assert.True(tree.GetNode(13).Key == 13);
        }

        [Fact]
        public void GetNonExistentKey_Should_ReturnDefault()
        {
            AVLTree<int> tree = new AVLTree<int>();
            tree.Insert(10);
            tree.Insert(5);
            tree.Insert(7);
            tree.Insert(9);
            tree.Insert(20);
            tree.Insert(6);
            tree.Insert(1);
            tree.Insert(13);
            tree.Insert(53);
            tree.Insert(100);
            var exception = Assert.Throws<KeyNotFoundException>(() => tree.GetNode(33));
            Assert.True(exception.Message == "Key not found.");
        }

        [Fact]
        public void CopyToList_ShouldInsert_AllNodes()
        {
            AVLTree<int> tree = new AVLTree<int>();
            tree.Insert(10);
            tree.Insert(5);
            tree.Insert(7);
            tree.Insert(9);
            tree.Insert(20);
            tree.Insert(6);
            tree.Insert(1);
            tree.Insert(13);
            tree.Insert(53);
            tree.Insert(100);
            List<AVLTreeNode<int>> nodesList = tree.CopyToList();
            Assert.True(nodesList.Count == 10);
        }

        [Fact]
        public void CopyToList_ShouldInsert_InsertNodes_InOrder()
        {
            AVLTree<int> tree = new AVLTree<int>();
            tree.Insert(10);
            tree.Insert(5);
            tree.Insert(7);
            tree.Insert(9);
            tree.Insert(20);
            tree.Insert(6);
            tree.Insert(1);
            tree.Insert(13);
            tree.Insert(53);
            tree.Insert(100);
            List<AVLTreeNode<int>> nodesList = tree.CopyToList();
            Assert.True(nodesList.IndexOf(tree.GetNode(53)) == 8);
        }
    }
}
