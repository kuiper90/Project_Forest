using System.Collections.Generic;
using Trees;
using Xunit;
using static Trees.RBTreeNodeType;

namespace UnitTest_Trees
{
    public class UnitTest_RedBlackTree
    {
        [Fact]
        public void InsertNode_InEmptyTree_ShouldBe_Root()
        {
            RedBlackTree<int> tree = new RedBlackTree<int>();
            tree.Insert(10);
            Assert.True(tree.Root.Key == 10);
        }

        [Fact]
        public void InsertNode_WithKey_SmallerThan_RootKey_ShouldBe_OnLeft()
        {
            RedBlackTree<int> tree = new RedBlackTree<int>();
            tree.Insert(10);
            tree.Insert(9);
            Assert.True(tree.Root.Key == 10);
            Assert.True(tree.Root.Left.Key == 9);
        }

        [Fact]
        public void InsertNode_WithKey_EqualWith_RootKey_ShouldBe_OnRight()
        {
            RedBlackTree<int> tree = new RedBlackTree<int>();
            tree.Insert(10);
            tree.Insert(10);
            Assert.True(tree.Root.Key == 10);
            Assert.True(tree.Root.Color == Black);
            Assert.True(tree.Root.Left.Key == 10);
            Assert.True(tree.Root.Left.Color == Red);
        }

        [Fact]
        public void InsertNode_WithKey_HigherThan_RootKey_ShouldBe_OnRightSubtree()
        {
            RedBlackTree<int> tree = new RedBlackTree<int>();
            tree.Insert(10);
            tree.Insert(100);
            Assert.True(tree.Root.Key == 100);
            Assert.True(tree.Root.Color == Black);
            Assert.True(tree.Root.Left.Key == 10);
            Assert.True(tree.Root.Left.Color == Red);
        }

        [Fact]
        public void Insert_RedNode_Should_TriggerRotations_AndRecoloring()
        {
            RedBlackTree<int> tree = new RedBlackTree<int>();
            for (int i = 0; i < 10; i++)
                tree.Insert(i);
            tree.Insert(10);
            Assert.True(tree.Root.Right.Right.Key == 10);
            Assert.True(tree.Root.Right.Right.Color == Black);
            Assert.True(tree.Root.Right.Key == 9);
            Assert.True(tree.Root.Right.Color == Black);
            Assert.True(tree.Root.Right.Left.Key == 8);
            Assert.True(tree.Root.Right.Left.Color == Black);
            Assert.True(tree.Root.Left.Right.Right.Key == 6);
            Assert.True(tree.Root.Left.Right.Right.Color == Black);
            Assert.True(tree.Root.Left.Right.Key == 5);
            Assert.True(tree.Root.Left.Right.Color == Black);
            Assert.True(tree.Root.Left.Left.Key == 1);
            Assert.True(tree.Root.Left.Left.Color == Black);
            Assert.True(tree.Root.Left.Key == 3);
            Assert.True(tree.Root.Left.Color == Red);
        }

        [Fact]
        public void Delete_BlackNode_With_NoChildren_Should_TriggerRotations_AndRecoloring()
        {
            RedBlackTree<int> tree = new RedBlackTree<int>();
            for (int i = 0; i < 10; i++)
                tree.Insert(i);
            tree.Delete(tree.GetNode(3).Key);
            var exception = Assert.Throws<KeyNotFoundException>(() => tree.GetNode(3));
            Assert.True(exception.Message == "Key not found.");
        }

        [Fact]
        public void Delete_RedNode_With_NoChildren_Should_TriggerRotations_AndRecoloring()
        {
            RedBlackTree<int> tree = new RedBlackTree<int>();
            for (int i = 0; i < 10; i++)
                tree.Insert(i);
            tree.Delete(tree.GetNode(7).Key);
            var exception = Assert.Throws<KeyNotFoundException>(() => tree.GetNode(7));
            Assert.True(exception.Message == "Key not found.");
        }

        [Fact]
        public void Delete_BlackNode_With_Children_Should_TriggerRotations_AndRecoloring()
        {
            RedBlackTree<int> tree = new RedBlackTree<int>();
            for (int i = 0; i < 10; i++)
                tree.Insert(i);
            tree.Delete(tree.GetNode(8).Key);
            Assert.True(tree.Root.Right.Right.Key == 9);
            Assert.True(tree.Root.Right.Right.Color == Black);
            var exception = Assert.Throws<KeyNotFoundException>(() => tree.GetNode(8));
            Assert.True(exception.Message == "Key not found.");
        }

        [Fact]
        public void Delete_RedNode_With_Children_Should_TriggerRotations_AndRecoloring()
        {
            RedBlackTree<int> tree = new RedBlackTree<int>();
            for (int i = 0; i < 10; i++)
                tree.Insert(i);
            tree.Delete(tree.GetNode(2).Key);
            Assert.True(tree.Root.Left.Right.Key == 4);
            Assert.True(tree.Root.Left.Right.Color == Black);
            var exception = Assert.Throws<KeyNotFoundException>(() => tree.GetNode(2));
            Assert.True(exception.Message == "Key not found.");
            Assert.True(tree.Root.Left.Key == 3);
            Assert.True(tree.Root.Left.Color == Black);
        }

        [Fact]
        public void GetExistentKey_ShouldBe_True()
        {
            RedBlackTree<int> tree = new RedBlackTree<int>();
            for (int i = 0; i < 10; i++)
                tree.Insert(i);
            Assert.True(tree.GetNode(3).Key == 3);
        }

        [Fact]
        public void GetNonExistentKey_ShouldBe_False()
        {
            RedBlackTree<int> tree = new RedBlackTree<int>();
            for (int i = 0; i < 10; i++)
                tree.Insert(i);
            Assert.False(tree.GetNode(3).Key == 10);
        }

        [Fact]
        public void GetNode_Should_ReturnNode()
        {
            RedBlackTree<int> tree = new RedBlackTree<int>();
            for (int i = 0; i < 10; i++)
                tree.Insert(i);
            Assert.True(tree.GetNode(3).Key == 3);
        }

        [Fact]
        public void GetNonExistentKey_Should_ReturnDefault()
        {
            RedBlackTree<int> tree = new RedBlackTree<int>();
            for (int i = 0; i < 10; i++)
                tree.Insert(i);
            var exception = Assert.Throws<KeyNotFoundException>(() => tree.GetNode(33));
            Assert.True(exception.Message == "Key not found.");
        }
    }
}
