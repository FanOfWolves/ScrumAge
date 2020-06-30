using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using ScrumageEngine.BoardSpace;
using ScrumageEngine.Objects.Items;
using ScrumageEngine.Objects.Player;
namespace ScrumageWPF.Test {

    /// <summary>
    /// Testing class for <see cref="BudgetNode"/>.
    /// </summary>
    [TestFixture]
    class BudgetNode_Test {

        private const Int32 BUDGET_NODE_PAWN_LIMIT = 1;
        private Node testNode;
        private Player testPlayer1;
        private Player testPlayer2;

        private String failedActionStr = " Failed to increase their budget. Reason: No Pawns";
        private String passedActionStr = " has 1 more budget!";

        /// <summary>
        /// One-time setup for this testing class.
        /// </summary>
        [OneTimeSetUp]
        public void ClassSetUp() {
            testNode = new BudgetNode(0, "testBudgetNode");

            testPlayer1 = new Player(1, "testPlayer1");
            testPlayer2 = new Player(2, "testPlayer2");
        }

        /// <summary>
        /// This method is called before each test
        /// </summary>
        [SetUp]
        public void TestSetUp() {
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
        public void TestTearDown() {
            testPlayer1.Pawns = null;
            testPlayer2.Pawns = null;
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

        #region BudgetNode_DoAction_PlayerGivenBudgetIfTheyHavePawnsInNode
        /// <summary>
        /// Asserts that <see cref="BudgetNode.DoAction(Player)"/> increases budget if player has pawn in the node.
        /// </summary>
        [Test]
        [Category("DoAction")]
        public void BudgetNode_DoAction_PlayerGivenBudgetIfTheyHavePawnsInNode() {
            this.testNode.AddPawn(testPlayer1.TakePawn("Back End"));
            Int32 originalBudget = testPlayer1.Budget;

            this.testNode.DoAction(testPlayer1);

            // Assert that Pawn has been removed.
            Assert.That(this.testNode.Pawns.Count, Is.EqualTo(0));
            // Assert that player1 did increase their budget.
            Assert.That(testPlayer1.Budget, Is.GreaterThan(originalBudget));

        }
        #endregion

        #region BudgetNode_DoAction_PlayerNotGivenBudgetIfTheyDoNotHavePawnsInNode
        /// <summary>
        /// Asserts that <see cref="BudgetNode.DoAction(Player)"/> does not increase budget if player does not have a pawn in the node.
        /// </summary>
        [Test]
        [Category("DoAction")]
        public void BudgetNode_DoAction_PlayerNotGivenBudgetIfTheyDoNotHavePawnsInNode() {
            this.testNode.AddPawn(testPlayer1.TakePawn("Back End"));

            Int32 originalBudget = testPlayer2.Budget;
            Pawn pawnInNode = this.testNode.Pawns[0];

            this.testNode.DoAction(testPlayer2);

            // Assert that Pawn has not been removed.
            Assert.That(this.testNode.Pawns[0], Is.SameAs(pawnInNode));
            // Assert that player2 did not increase their budget.
            Assert.That(testPlayer2.Budget, Is.EqualTo(originalBudget));

            // Return
            this.testNode.DoAction(testPlayer1);
        }
        #endregion

        #region BudgetNode_DoAction_IncreasesPlayerBudgetByCorrectAmount
        /// <summary>
        /// Asserts that <see cref="BudgetNode.DoAction(Player)"/> increases player budget by correct amount.
        /// </summary>
        [Test]
        [Category("DoAction")]
        public void BudgetNode_DoAction_IncreasesPlayerBudgetByCorrectAmount() {
            this.testNode.AddPawn(testPlayer1.TakePawn("Back End"));

            Int32 originalBudget = testPlayer1.Budget;
            Pawn currentPawnInNode = this.testNode.Pawns[0];

            this.testNode.DoAction(testPlayer1);
            Assert.That(testPlayer1.Budget, Is.EqualTo(originalBudget + 1));
        }
        #endregion

        #region BudgetNode_DoAction_ReturnsCorrectPawnsToPlayer
        /// <summary>
        /// Asserts that the pawn in the node is correctly returned to the player
        /// </summary>
        [Test]
        [Category("DoAction")]
        public void BudgetNode_DoAction_ReturnsCorrectPawnsToPlayer() {
            Int32 originalPawnCount = testPlayer1.Pawns.Count;

            this.testNode.AddPawn(testPlayer1.TakePawn("Back End"));

            this.testNode.DoAction(testPlayer1);

            Int32 newPawnCount = testPlayer1.Pawns.Count;

            Assert.That(newPawnCount, Is.EqualTo(originalPawnCount));
        }
        #endregion

        #region BudgetNode_DoAction_ReturnsCorrectStringOnFailure
        /// <summary>
        /// Asserts that <see cref="BudgetNode.DoAction(Player)"/> returns the correct string on action failure.
        /// </summary>
        [Test]
        [Category("DoAction")]
        public void BudgetNode_DoAction_ReturnsCorrectStringOnFailure() {
            this.testNode.AddPawn(testPlayer1.TakePawn("Back End"));

            String stringOutput = testNode.DoAction(testPlayer2);
            Assert.That(stringOutput, Is.EqualTo(testPlayer2.PlayerName + this.failedActionStr));
        }
        #endregion

        #region BudgetNode_DoAction_ReturnsCorrectStringOnSuccess
        /// <summary>
        /// Asserts that <see cref="BudgetNode.DoAction(Player)"/> returns the correct string on action success.
        /// </summary>
        [Test]
        [Category("DoAction")]
        public void BudgetNode_DoAction_ReturnsCorrectStringOnSuccess() {
            this.testNode.AddPawn(testPlayer1.TakePawn("Back End"));

            String stringOutput = testNode.DoAction(testPlayer1);
            Assert.That(stringOutput, Is.EqualTo(testPlayer1.PlayerName + this.passedActionStr));
        }
        #endregion
    }
}
