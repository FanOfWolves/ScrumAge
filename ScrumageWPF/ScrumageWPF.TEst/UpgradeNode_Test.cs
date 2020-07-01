using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using ScrumageEngine.BoardSpace;
using ScrumageEngine.Objects.Player;
using ScrumageEngine.Objects.Items;

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

        #endregion

        #region Category: Other

        #endregion

        #endregion

    }
}
