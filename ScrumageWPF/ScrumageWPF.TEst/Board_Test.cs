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

        #endregion

        #region Board Tests

        #region Category: Instantiation
        [Test]
        [Category("Instantiation")]
        public void Board_ConstructorInstantiatesCorrectly() {

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
