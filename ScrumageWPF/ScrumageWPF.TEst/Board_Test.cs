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



        #endregion

    }
}
