
using System.Collections.Generic;
using Trees;
using Xunit;
using static Trees.RBTreeNodeType;

namespace UnitTest_Trees
{
    public class UnitTest_RBTree
    {

        [Fact]
        public void AddNode_InEmptyTree_ShouldBe_Root()
        {
            RBTree<int> tree = new RBTree<int>();
            tree.Add(10);
            Assert.True(tree.Root.Key == 10);
        }

        [Fact]
        public void AddNode_WithKey_SmallerThan_RootKey_ShouldBe_OnLeft()
        {
            RBTree<int> tree = new RBTree<int>();
            tree.Add(10);
            tree.Add(9);
            Assert.True(tree.Root.Key == 10);
            Assert.True(tree.Root.Left.Key == 9);
        }

        [Fact]
        public void AddNode_WithKey_EqualWith_RootKey_ShouldBe_OnRight()
        {
            RBTree<int> tree = new RBTree<int>();
            tree.Add(10);
            tree.Add(10);
            Assert.True(tree.Root.Key == 10);
            Assert.True(tree.Root.Color == Black);
            Assert.True(tree.Root.Right.Key == 10);
            Assert.True(tree.Root.Right.Color == Red);
        }

        [Fact]
        public void AddNode_WithKey_HigherThan_RootKey_ShouldBe_OnRightSubtree()
        {
            RBTree<int> tree = new RBTree<int>();
            tree.Add(10);
            tree.Add(100);
            Assert.True(tree.Root.Key == 10);
            Assert.True(tree.Root.Color == Black);
            Assert.True(tree.Root.Right.Key == 100);
            Assert.True(tree.Root.Right.Color == Red);
        }

        [Fact]
        public void Add_RedNode_Should_TriggerRotations_AndRecoloring()
        {
            RBTree<int> tree = new RBTree<int>();
            for (int i = 0; i < 10; i++)
                tree.Add(i);
            tree.Add(10);
            Assert.True(tree.Root.Right.Right.Right.Right.Key == 10);
            Assert.True(tree.Root.Right.Right.Right.Key == 9);
            Assert.True(tree.Root.Right.Right.Right.Left.Key == 8);
            Assert.True(tree.Root.Right.Right.Right.Left.Color == Red);
            Assert.True(tree.Root.Right.Right.Left.Key == 6);
            Assert.True(tree.Root.Right.Right.Left.Color == Black);
            Assert.True(tree.Root.Right.Key == 5);
            Assert.True(tree.Root.Right.Color == Black);
            Assert.True(tree.Root.Left.Key == 1);
            Assert.True(tree.Root.Left.Color == Black);
        }

        [Fact]
        public void Delete_BlackNode_With_NoChildren_Should_TriggerRotations_AndRecoloring()
        {
            RBTree<int> tree = new RBTree<int>();
            for (int i = 0; i < 10; i++)
                tree.Add(i);
            tree.Delete(tree.GetNode(3));
            var exception = Assert.Throws<KeyNotFoundException>(() => tree.GetNode(3));
            Assert.True(exception.Message == "Key not found.");
        }

        [Fact]
        public void Delete_RedNode_With_NoChildren_Should_TriggerRotations_AndRecoloring()
        {
            RBTree<int> tree = new RBTree<int>();
            for (int i = 0; i < 10; i++)
                tree.Add(i);
            tree.Delete(tree.GetNode(7));
            var exception = Assert.Throws<KeyNotFoundException>(() => tree.GetNode(7));
            Assert.True(exception.Message == "Key not found.");
        }

        [Fact]
        public void Delete_BlackNode_With_Children_Should_TriggerRotations_AndRecoloring()
        {
            RBTree<int> tree = new RBTree<int>();
            for (int i = 0; i < 10; i++)
                tree.Add(i);
            tree.Delete(tree.GetNode(8));
            Assert.True(tree.Root.Right.Right.Right.Key == 9);
            Assert.True(tree.Root.Right.Right.Right.Color == Black);
            var exception = Assert.Throws<KeyNotFoundException>(() => tree.GetNode(8));
            Assert.True(exception.Message == "Key not found.");            
        }

        [Fact]
        public void Delete_RedNode_With_Children_Should_TriggerRotations_AndRecoloring()
        {
            RBTree<int> tree = new RBTree<int>();
            for (int i = 0; i < 10; i++)
                tree.Add(i);
            tree.Delete(tree.GetNode(2));
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
            RBTree<int> tree = new RBTree<int>();
            for (int i = 0; i < 10; i++)
                tree.Add(i);
            Assert.True(tree.GetNode(3).Key == 3);
        }

        [Fact]
        public void GetNonExistentKey_ShouldBe_False()
        {
            RBTree<int> tree = new RBTree<int>();
            for (int i = 0; i < 10; i++)
                tree.Add(i);
            Assert.False(tree.GetNode(3).Key == 10);
        }

        [Fact]
        public void GetNode_Should_ReturnNode()
        {
            RBTree<int> tree = new RBTree<int>();
            for (int i = 0; i < 10; i++)
                tree.Add(i);
            Assert.True(tree.GetNode(3).Key == 3);
        }

        [Fact]
        public void GetNonExistentKey_Should_ReturnDefault()
        {
            RBTree<int> tree = new RBTree<int>();
            for (int i = 0; i < 10; i++)
                tree.Add(i);
            var exception = Assert.Throws<KeyNotFoundException>(() => tree.GetNode(33));
            Assert.True(exception.Message == "Key not found.");
        }

        [Fact]
        public void RedBlackTree_Should_BeBalanced()
        {
            RBTree<int> tree = new RBTree<int>();
            for (int i = 0; i < 10; i++)
                tree.Add(i);
            Assert.True(tree.IsBalanced());
        }
    }
}