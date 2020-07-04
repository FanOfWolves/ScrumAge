using System;
using System.Collections.Generic;
using NUnit.Framework;
using ScrumageEngine.BoardSpace;
using ScrumageEngine.Objects.Items;
using ScrumageEngine.Objects.Player;

namespace ScrumageWPF.Test
{
    class ReassignmentNode_Test
    {
        private const Int32 REASSIGNMENT_NODE_PAWN_LIMIT = 1;
        private Node testNode;
        private Player testPlayer1;
        private Player testPlayer2;

        private String failedActionStr = " Failed to increase their funds. Reason: No Pawns";
        private String passedActionStr = " has 1 more fund!";

        /// <summary>
        /// One-time setup for this testing class.
        /// </summary>
        [OneTimeSetUp]
        public void ClassSetUp()
        {
            testNode = new ReassignmentNode(0, "testReassignmentNode");

            testPlayer1 = new Player(1, "testPlayer1");
            testPlayer2 = new Player(2, "testPlayer2");
        }

        /// <summary>
        /// This method is called before each test
        /// </summary>
        [SetUp]
        public void TestSetUp()
        {
            testPlayer1.Pawns = new List<Pawn>(3);
            testPlayer2.Pawns = new List<Pawn>(3);

            testPlayer1.GivePawn(new Pawn(testPlayer1.PlayerID, "Back End"));
            testPlayer1.GivePawn(new Pawn(testPlayer1.PlayerID, "Front End"));
            testPlayer1.GivePawn(new Pawn(testPlayer1.PlayerID, "Full Stack"));

            testPlayer2.GivePawn(new Pawn(testPlayer2.PlayerID, "Back End"));
            testPlayer2.GivePawn(new Pawn(testPlayer2.PlayerID, "Front End"));
            testPlayer2.GivePawn(new Pawn(testPlayer2.PlayerID, "Full Stack"));

            testPlayer1.Budget = 1;
            testPlayer2.Budget = 1;
        }

        /// <summary>
        /// This method is called after each test
        /// </summary>
        [TearDown]
        public void TestTearDown()
        {
            testPlayer1.Pawns = null;
            testPlayer2.Pawns = null;
        }

        #region ReassignmentNode_InstantiatesCorrectly        
        /// <summary>
        /// Asserts that <see cref="ReassignmentNode.ReassignmentNode(Int32, String)"/> instantiates correctly.
        /// </summary>
        /// <param name="id">The identifier for the node.</param>
        /// <param name="name">The name for the node.</param>
        [Test]
        #region Test-Cases
        [TestCase(0, "Node1")]
        [TestCase(6, "Albert")]
        [TestCase(888, "Node943")]
        #endregion
        public void ReassignmentNode_InstantiatesCorrectly(Int32 id, String name)
        {
            Node node = new ReassignmentNode(id, name);
            Assert.That(node.NodeName == name);
            Assert.That(node.NodeID == id);
        }
        #endregion

        #region ReassignmentNode_GetMaxPawnLimitReturnsCorrectNumber        
        /// <summary>
        /// Asserts that <see cref="ReassignmentNode.MaxPawnLimit"/> returns the current number.
        /// </summary>
        [Test]
        public void ReassignmentNode_GetMaxPawnLimitReturnsCorrectNumber()
        {
            Assert.That(testNode.MaxPawnLimit, Is.EqualTo(REASSIGNMENT_NODE_PAWN_LIMIT));
        }
        #endregion

        #region ReassignmentNode_DoAction_PlayerGivenFundsIfTheyHavePawnsInNode
        /// <summary>
        /// Asserts that <see cref="ReassignmentNode.DoAction(Player)"/> increases funds if player has pawn in the node.
        /// </summary>
        [Test]
        [Category("DoAction")]
        public void ReassignmentNode_DoAction_PlayerGivenFundsIfTheyHavePawnsInNode() 
        {
            this.testNode.AddPawn(testPlayer1.TakePawn("Back End"));
            Int32 originalBudget = testPlayer1.Budget;

            this.testNode.DoAction(testPlayer1);

            // Assert that Pawn has been removed.
            Assert.That(this.testNode.Pawns.Count, Is.EqualTo(0));
            // Assert that player1 did increase their budget.
            Assert.That(testPlayer1.Budget, Is.GreaterThan(originalBudget));

        }
        #endregion

        #region ReassignmentNode_DoAction_PlayerNotGivenFundsIfTheyDoNotHavePawnsInNode
        /// <summary>
        /// Asserts that <see cref="ReassignmentNode.DoAction(Player)"/> does not increase funds if player does not have a pawn in the node.
        /// </summary>
        [Test]
        [Category("DoAction")]
        public void ReassignmentNode_DoAction_PlayerNotGivenFundsIfTheyDoNotHavePawnsInNode()
        {
            this.testNode.AddPawn(testPlayer1.TakePawn("Back End"));

            Int32 originalFunds = testPlayer2.Funds; 
            Pawn pawnInNode = this.testNode.Pawns[0];

            this.testNode.DoAction(testPlayer2);

            // Assert that Pawn has not been removed.
            Assert.That(this.testNode.Pawns[0], Is.SameAs(pawnInNode));
            // Assert that player2 did not increase their Funds.
            Assert.That(testPlayer2.Funds, Is.EqualTo(originalFunds));

            // Return
            this.testNode.DoAction(testPlayer1);
        }
        #endregion

        #region ReassignmentNode_DoAction_IncreasesPlayerFundsByCorrectAmount
        /// <summary>
        /// Asserts that <see cref="ReassignmentNode.DoAction(Player)"/> increases player Funds by correct amount.
        /// </summary>
        [Test]
        [Category("DoAction")]
        public void ReassignmentNode_DoAction_IncreasesPlayerFundsByCorrectAmount()
        {
            this.testNode.AddPawn(testPlayer1.TakePawn("Back End"));

            Int32 originalFunds = testPlayer1.Funds;
            Pawn currentPawnInNode = this.testNode.Pawns[0];

            this.testNode.DoAction(testPlayer1);
            Assert.That(testPlayer1.Funds, Is.EqualTo(originalFunds + 1));
        }
        #endregion

        #region ReassignmentNode_DoAction_ReturnsCorrectPawnsToPlayer
        /// <summary>
        /// Asserts that the pawn in the node is correctly returned to the player
        /// </summary>
        [Test]
        [Category("DoAction")]
        public void ReassignmentNode_DoAction_ReturnsCorrectPawnsToPlayer() 
        {
            Int32 originalPawnCount = testPlayer1.Pawns.Count;

            this.testNode.AddPawn(testPlayer1.TakePawn("Back End"));

            this.testNode.DoAction(testPlayer1);

            Int32 newPawnCount = testPlayer1.Pawns.Count;

            Assert.That(newPawnCount, Is.EqualTo(originalPawnCount));
        }
        #endregion

        #region ReassignmentNode_DoAction_ReturnsCorrectStringOnFailure
        /// <summary>
        /// Asserts that <see cref="ReassignmentNode.DoAction(Player)"/> returns the correct string on action failure.
        /// </summary>
        [Test]
        [Category("DoAction")]
        public void ReassignmentNode_DoAction_ReturnsCorrectStringOnFailure()
        {
            this.testNode.AddPawn(testPlayer1.TakePawn("Back End"));

            String stringOutput = testNode.DoAction(testPlayer2);
            Assert.That(stringOutput, Is.EqualTo(testPlayer2.PlayerName + this.failedActionStr));
        }
        #endregion

        #region ReassignmentNode_DoAction_ReturnsCorrectStringOnSuccess
        /// <summary>
        /// Asserts that <see cref="ReassignmentNode.DoAction(Player)"/> returns the correct string on action success.
        /// </summary>
        [Test]
        [Category("DoAction")]
        public void ReassignmentNode_DoAction_ReturnsCorrectStringOnSuccess()
        {
            this.testNode.AddPawn(testPlayer1.TakePawn("Back End")); 

            String stringOutput = testNode.DoAction(testPlayer1);
            Assert.That(stringOutput, Is.EqualTo(testPlayer1.PlayerName + this.passedActionStr));
        }
        #endregion
    }
}

