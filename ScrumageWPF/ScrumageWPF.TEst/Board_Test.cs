using System;
using System.Collections.Generic;
using NUnit.Framework;
using ScrumageEngine.BoardSpace;
using ScrumageEngine.Objects.Items.Cards;
using ScrumageEngine.Objects.Items;
using ScrumageEngine.Objects.Player;

namespace ScrumageWPF.Test {

    /// <summary>
    /// Testing class for <see cref="ScrumageEngine.BoardSpace.Board"/>.
    /// </summary>
    [TestFixture]
    class Board_Test {
        #region Fields
        Board testBoard;

        #endregion

        #region Test Class Helper Methods

        #region SetUp and TearDown
        [OneTimeSetUp]
        public void Board_Test_SetUp() {
            testBoard = new Board();
        }

        [OneTimeTearDown]
        public void Board_Test_TearDown() {
            testBoard = null;
        }
        #endregion

        #region Helper Methods

        private List<Node> GetNeedNodes() {
            List<Node> neededNodes = new List<Node>(16);
            neededNodes.Add(new ResourceNode(1, "Requirements", new Requirements()));
            neededNodes.Add(new ResourceNode(2, "Design", new Design()));
            neededNodes.Add(new ResourceNode(3, "Implementation", new Implementation()));
            neededNodes.Add(new ResourceNode(4, "Testing", new Testing()));
            neededNodes.Add(new UpgradeNode(5, "Technical Hut"));
            neededNodes.Add(new BudgetNode(6, "Budget Increase"));
            neededNodes.Add(new HiringNode(7, "Interview"));
            neededNodes.Add(new ReassignmentNode(8, "Reassignment"));
            neededNodes.Add(new CardNode(9, "Agility 1", new Deck("Agility", 10)));
            neededNodes.Add(new CardNode(10, "Agility 2", new Deck("Agility", 10)));
            neededNodes.Add(new CardNode(11, "Agility 3", new Deck("Agility", 10)));
            neededNodes.Add(new CardNode(12, "Agility 4", new Deck("Agility", 10)));
            neededNodes.Add(new CardNode(13, "Artifact 1", new Deck("Artifact", 10)));
            neededNodes.Add(new CardNode(14, "Artifact 2", new Deck("Artifact", 10)));
            neededNodes.Add(new CardNode(15, "Artifact 3", new Deck("Artifact", 10)));
            neededNodes.Add(new CardNode(16, "Artifact 4", new Deck("Artifact", 10)));
            return neededNodes;
        }
        #endregion


        #endregion

        #region Board Tests

        #region Category: Instantiation
        [Test]
        [Category("Instantiation")]
        public void Board_ConstructorInstantiatesCorrectly() {
            Board _board = new Board();
            Assert.That(testBoard.Nodes, Is.EquivalentTo(GetNeedNodes()));
        }
        #endregion



        #endregion

    }
}
