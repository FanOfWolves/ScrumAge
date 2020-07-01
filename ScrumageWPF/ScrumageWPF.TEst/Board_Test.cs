﻿using System;
using System.Collections.Generic;
using NUnit.Framework;
using ScrumageEngine.BoardSpace;
using ScrumageEngine.Objects.Items.Cards;
using ScrumageEngine.Objects.Items;
using ScrumageEngine.Objects.Player;
using System.Diagnostics.CodeAnalysis;

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


        private class NodeEqualityComparer : IEqualityComparer<Node> {
            public Boolean Equals( Node thisNode, Node thatNode) {
                return (thisNode.NodeID == thatNode.NodeID && thisNode.NodeName == thatNode.NodeName);
            }

            public Int32 GetHashCode( Node obj) {
                return obj.GetHashCode();
            }
        }


        #endregion


        #endregion

        #region Board Tests

        #region Category: Instantiation
        [Test]
        [Category("Instantiation")]
        public void Board_ConstructorInstantiatesCorrectly() {
            Board _board = new Board();
            Assert.That(testBoard.Nodes, Is.EquivalentTo(GetNeedNodes()).Using(new NodeEqualityComparer()));
        }
        #endregion

		#region Node Getters
        [Test]
        [Category("Getters")]
        [TestCase("Requirements")]
        [TestCase("Design")]
        [TestCase("Implementation")]
        [TestCase("Testing")]
        [TestCase("Technical Hut")]
        [TestCase("Budget Increase")]
        [TestCase("Interview")]
        [TestCase("Reassignment")]
        [TestCase("Agility 1")]
        [TestCase("Agility 2")]
        [TestCase("Agility 3")]
        [TestCase("Agility 4")]
        [TestCase("Artifact 1")]
        [TestCase("Artifact 2")]
        [TestCase("Artifact 3")]
        [TestCase("Artifact 4")]
        public void Nodes_By_Name(String nodeName) {
            Assert.That(nodeName == testBoard.GetNodeByName(nodeName).NodeName);
		}

        [Test]
        [Category("Getters")]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        [TestCase(9)]
        [TestCase(10)]
        [TestCase(11)]
        [TestCase(12)]
        [TestCase(13)]
        [TestCase(14)]
        [TestCase(15)]
        [TestCase(16)]
        [TestCase(17)]
        public void Nodes_By_ID(Int32 nodeID) {
            Assert.That(nodeID == testBoard.GetNodeByID(nodeID).NodeID);
        }
		#endregion

		#endregion

	}
}
