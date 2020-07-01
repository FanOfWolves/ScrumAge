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
        }

        #endregion

        #endregion

        #region UpgradeNode Tests

        #region Category: Instantiation

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
