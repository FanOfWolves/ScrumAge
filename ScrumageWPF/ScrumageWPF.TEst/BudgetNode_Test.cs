using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using ScrumageEngine.BoardSpace;
namespace ScrumageWPF.Test {

    [TestFixture]
    class BudgetNode_Test {
        private const Int32 BUDGET_NODE_PAWN_LIMIT = 1;
        private Node testNode;
        /// <summary>
        /// One-time setup for this testing class.
        /// </summary>
        [OneTimeSetUp]
        public void ClassSetUp() {
            testNode = new BudgetNode(0, "testBudgetNode");
        }

        #region BudgetNode_InstantiatesCorrectly        
        /// <summary>
        /// Asserts that <see cref="BudgetNode.BudgetNode(Int32, String)"/> instantiates correctly.
        /// </summary>
        /// <param name="id">The identifier for the node.</param>
        /// <param name="name">The name for the node.</param>
        [Test]
        #region Test-Cases
        [TestCase(0, "Node1")]
        [TestCase(6, "Albert")]
        [TestCase(888, "Node943")]
        #endregion
        public void BudgetNode_InstantiatesCorrectly(Int32 id, String name) {
            Node node = new BudgetNode(id, name);
            Assert.That(node.NodeName == name);
            Assert.That(node.NodeID == id);
        }
        #endregion

        #region BudgetNode_GetMaxPawnLimitReturnsCorrectNumber        
        /// <summary>
        /// Asserts that <see cref="BudgetNode.MaxPawnLimit"/> returns the current number.
        /// </summary>
        [Test]
        public void BudgetNode_GetMaxPawnLimitReturnsCorrectNumber() {
            Assert.That(testNode.MaxPawnLimit, Is.EqualTo(BUDGET_NODE_PAWN_LIMIT));
        }
        #endregion
    }
}
