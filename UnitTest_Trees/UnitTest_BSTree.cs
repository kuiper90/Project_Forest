using System;
using System.Collections.Generic;
using Trees;
using Xunit;

namespace UnitTest_Trees
{
    public class UnitTest_BSTree
    {
        [Fact]
        public void AddNode_InEmptyTree_ShouldBe_Root()
        {
            BSTree<int> tree = new BSTree<int>();
            tree.Add(10);
            Assert.True(tree.Root.Key == 10);
        }

        [Fact]
        public void AddNode_WithKey_SmallerThan_RootKey_ShouldBe_OnLeft()
        {
            BSTree<int> tree = new BSTree<int>();
            tree.Add(10);
            tree.Add(9);
            Assert.True(tree.Root.Key == 10);
            Assert.True(tree.Root.Left.Key == 9);
        }

        [Fact]
        public void AddNode_WithKey_EqualWith_RootKey_ShouldBe_OnRight()
        {
            BSTree<int> tree = new BSTree<int>();
            tree.Add(10);
            tree.Add(10);
            Assert.True(tree.Root.Key == 10);
            Assert.True(tree.Root.Right.Key == 10);
        }

        [Fact]
        public void AddNode_WithKey_HigherThan_RootKey_ShouldBe_OnRight()
        {
            BSTree<int> tree = new BSTree<int>();
            tree.Add(10);
            tree.Add(100);
            Assert.True(tree.Root.Key == 10);
            Assert.True(tree.Root.Right.Key == 100);
        }

        [Fact]
        public void AddNodes_Tree_ShouldBe_Sorted()
        {
            BSTree<int> tree = new BSTree<int>();
            tree.Add(10);
            tree.Add(5);
            tree.Add(7);
            tree.Add(9);
            tree.Add(20);
            tree.Add(6);
            tree.Add(1);
            tree.Add(13);
            tree.Add(53);
            tree.Add(100);
            List<BSTreeNode<int>> nodeList = tree.CopyToList(tree);
            
            Assert.True(nodeList[5].Key == 10);
            Assert.True(nodeList[9].Key == 100);
        }

        [Fact]
        public void AddOneNode_WithDefaultValue_Tree_ShouldBe_Sorted()
        {
            BSTree<int> tree = new BSTree<int>();
            tree.Add(10);
            tree.Add(5);
            tree.Add(7);
            tree.Add(9);
            tree.Add(20);
            tree.Add(6);
            tree.Add(1);
            tree.Add(13);
            tree.Add(53);
            tree.Add(100);
            tree.Add(0);
            List<BSTreeNode<int>> nodeList = tree.CopyToList(tree);

            Assert.True(nodeList[5].Key == 9);
            Assert.True(nodeList[9].Key == 53);
        }

        [Fact]
        public void GetNode_Should_ReturnNode()
        {
            BSTree<int> tree = new BSTree<int>();
            tree.Add(10);
            tree.Add(5);
            tree.Add(7);
            tree.Add(9);
            tree.Add(20);
            tree.Add(6);
            tree.Add(1);
            tree.Add(13);
            tree.Add(53);
            tree.Add(100);
            Assert.True(tree.GetNode(13).Key == 13);
        }

        [Fact]
        public void GetNonExistentKey_Should_ReturnDefault()
        {
            BSTree<int> tree = new BSTree<int>();
            tree.Add(10);
            tree.Add(5);
            tree.Add(7);
            tree.Add(9);
            tree.Add(20);
            tree.Add(6);
            tree.Add(1);
            tree.Add(13);
            tree.Add(53);
            tree.Add(100);
            var exception = Assert.Throws<KeyNotFoundException>(() => tree.GetNode(33));
            Assert.True(exception.Message == "Key not found.");
        }

        [Fact]
        public void CopyToList_ShouldAdd_AllNodes()
        {
            BSTree<int> tree = new BSTree<int>();
            tree.Add(10);
            tree.Add(5);
            tree.Add(7);
            tree.Add(9);
            tree.Add(20);
            tree.Add(6);
            tree.Add(1);
            tree.Add(13);
            tree.Add(53);
            tree.Add(100);
            List<BSTreeNode<int>> nodesList = tree.CopyToList(tree);
            Assert.True(nodesList.Count == 10);
        }

        [Fact]
        public void CopyToList_ShouldAdd_AddNodes_InOrder()
        {
            BSTree<int> tree = new BSTree<int>();
            tree.Add(10);
            tree.Add(5);
            tree.Add(7);
            tree.Add(9);
            tree.Add(20);
            tree.Add(6);
            tree.Add(1);
            tree.Add(13);
            tree.Add(53);
            tree.Add(100);
            List<BSTreeNode<int>> nodesList = tree.CopyToList(tree);
            Assert.True(nodesList.IndexOf(tree.GetNode(53)) == 8);
        }
    }
}
