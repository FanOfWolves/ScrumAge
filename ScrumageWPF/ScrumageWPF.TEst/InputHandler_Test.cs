using System;
using System.Collections.Generic;
using System.Text;
using ScrumageEngine.InputLogic;
using ScrumageEngine.BoardSpace;
using ScrumageEngine.Objects.Player;
using ScrumageEngine.Objects.Items;
using NUnit.Framework;

namespace ScrumageWPF.Test {
    /// <summary>
    /// Testing class for <see cref="InputHandler"/>.
    /// </summary>
    [TestFixture]
    class InputHandler_Test {



        /// <summary>
        /// Asserts that <see cref="InputHandler.ClearInputs"/> clears RecentInputs
        /// </summary>
        [Test]
        public void InputHandler_ClearInputs_Works() {
            for(Int32 i = 0; i < 5; i++) {
                InputHandler.RecentInputs.Add("Sample Text");
            }
            InputHandler.ClearInputs();
            Assert.That(InputHandler.RecentInputs.Count, Is.EqualTo(0));
        }


    }
}
