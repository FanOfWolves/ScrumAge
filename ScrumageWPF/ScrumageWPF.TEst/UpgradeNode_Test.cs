using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using ScrumageEngine.BoardSpace;
using ScrumageEngine.Objects.Player;
using ScrumageEngine.Objects.Items;
using NUnit.Framework.Internal;

namespace ScrumageWPF.Test {

    /// <summary>
    /// Testing class for <see cref="ScrumageEngine.BoardSpace.UpgradeNode"/>.
    /// See also <seealso cref="ScrumageEngine.BoardSpace.Node"/>.
    /// </summary>
    [TestFixture]
    class UpgradeNode_Test {

        #region Fields
        private Player testPlayer1;
        private Player testPlayer2;

        private UpgradeNode testNode;

        private const String BE = "Back End";
        private const String FE = "Front End";
        private const String FS = "Full Stack";

        #endregion

        #region Testing Class Helper Methods

        #region Setup and Teardown

        /// <summary>
        /// One-time setup of this testing class.
        /// </summary>
        [OneTimeSetUp]
        public void UpgradeNode_Test_SetUp() {
            testPlayer1 = new Player(1, "playerOne");
            testPlayer2 = new Player(2, "playerTwo");
            testNode = new UpgradeNode(0, "upgradeNode");
        }

        /// <summary>
        /// One-time cleanup of this testing class.
        /// </summary>
        [OneTimeTearDown]
        public void UpgradeNode_Test_TearDown() {
            testPlayer1 = null;
            testPlayer2 = null;
            testNode = null;
        }

        /// <summary>
        /// A setup performed before each test method.
        /// </summary>
        [SetUp]
        public void Method_SetUp() {
            testPlayer1.Pawns = new List<Pawn>(3);
            testPlayer2.Pawns = new List<Pawn>(3);

            testPlayer1.GivePawn(new Pawn(testPlayer1.PlayerID, "Back End"));
            testPlayer1.GivePawn(new Pawn(testPlayer1.PlayerID, "Front End"));
            testPlayer1.GivePawn(new Pawn(testPlayer1.PlayerID, "Full Stack"));

            testPlayer2.GivePawn(new Pawn(testPlayer2.PlayerID, "Back End"));
            testPlayer2.GivePawn(new Pawn(testPlayer2.PlayerID, "Front End"));
            testPlayer2.GivePawn(new Pawn(testPlayer2.PlayerID, "Full Stack"));
        }

        /// <summary>
        /// A cleanup performed after each test method.
        /// </summary>
        [TearDown]
        public void Method_TearDown() {
            testNode.Pawns.Clear();
            testPlayer1.Pawns.Clear();
            testPlayer2.Pawns.Clear();
        }

        #endregion

        #endregion

        #region UpgradeNode Tests

        #region Category: Instantiation

        #region UpgradeNode_ConstructorInstantiatesCorrectly        
        /// <summary>
        /// Asserts thats <see cref="UpgradeNode.UpgradeNode(Int32, String)"/> instantiates correctly.
        /// </summary>
        /// <param name="id">The identifier for the node.</param>
        /// <param name="name">The name for the node.</param>
        [Test]
        [Category("Instantiation")]
        #region Test-Cases
        [TestCase(0, "Node1")]
        [TestCase(6, "Albert")]
        [TestCase(888, "Node943")]
        #endregion
        public void UpgradeNode_ConstructorInstantiatesCorrectly(Int32 id, String name) {
            Node createdNode1 = new UpgradeNode(id, name);
            UpgradeNode createdNode2 = new UpgradeNode(id, name);
            Assert.That(createdNode1, Has.Property("Pawns").Count.EqualTo(0).
                                      And.Property("NodeID").EqualTo(id).
                                      And.Property("NodeName").EqualTo(name));
            Assert.That(createdNode2, Has.Property("Pawns").Count.EqualTo(0).
                                      And.Property("NodeID").EqualTo(id).
                                      And.Property("NodeName").EqualTo(name));
        }
        #endregion

        #endregion

        #region Category: UpgradeNode.DoAction

        #endregion

        #region Category: General DoAction

        #region UpgradeNode_DoAction_DoesNotReturnPawnsToIncorrectPlayer        
        /// <summary>
        /// Asserts that <see cref="UpgradeNode.DoAction(Player)"/> does not return node pawns to incorrect players.
        /// </summary>
        [Test]
        [Category("General DoAction")]
        public void UpgradeNode_DoAction_DoesNotReturnPawnsToIncorrectPlayer() {
            Int32 originalPlayerPawnCount1 = testPlayer1.Pawns.Count;

            testNode.AddPawn(testPlayer2.TakePawn("Back End"));
            testNode.AddPawn(testPlayer2.TakePawn("Front End"));

            Int32 originalNodePawnCount = testNode.Pawns.Count;

            testNode.DoAction(testPlayer1);
            // Assert that node contains only pawns belonging to testPlayer2.
            Assert.That(testNode.Pawns, Has.All.Property("PawnID").EqualTo(2));
            
            // Assert no change in testNode or testPlayer1
            Assert.That(testNode, Has.Property("Pawns").Count.EqualTo(originalNodePawnCount));
            Assert.That(testPlayer1.Pawns.Count, Is.EqualTo(originalPlayerPawnCount1));
        }
        #endregion

        #region  UpgradeNode_DoAction_ReturnsCorrectPawnsToPlayer
        /// <summary>
        /// Asserts that the pawn in the node is correctly returned to the player
        /// </summary>
        [Test]
        [Category("General DoAction")]
        public void UpgradeNode_DoAction_ReturnsCorrectPawnsToCorrectPlayer() {
            Int32 originalPlayerPawnCount1 = testPlayer1.Pawns.Count;
            Int32 originalPlayerPawnCount2 = testPlayer2.Pawns.Count;
            testNode.AddPawn(testPlayer1.TakePawn("Back End"));
            testNode.AddPawn(testPlayer2.TakePawn("Back End"));
            testNode.AddPawn(testPlayer2.TakePawn("Front End"));

            testNode.DoAction(testPlayer1);
            testNode.DoAction(testPlayer2);

            Int32 newPawnCount1 = testPlayer1.Pawns.Count;
            Int32 newPawnCount2 = testPlayer2.Pawns.Count;

            // Assert that testPlayer1 has received a pawn back.
            Assert.That(newPawnCount1, Is.EqualTo(originalPlayerPawnCount1));
            // Assert that testPlayer2 has received two pawns back.
            Assert.That(newPawnCount2, Is.EqualTo(originalPlayerPawnCount2));

            // Assert that testNode removes its pawns.
            Assert.That(testNode.Pawns.Count, Is.EqualTo(0));
        }
        #endregion

        #endregion

        #region Category: Other

        #endregion

        #endregion

    }
}
