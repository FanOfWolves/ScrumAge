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
        private Game testGame;

        [SetUp]
        public void SetUp() {
            List<String> list = new List<String>(2);
            list.Add("p1");
            list.Add("p2");

            this.testGame = new Game(list);


        }

        /// <summary>
        /// Asserts that <see cref="InputHandler.ClearInputs"/> clears RecentInputs
        /// </summary>
        [Test]
        public void InputHandler_ClearInputs_Works() {
            for(Int32 i = 0; i < 5; i++) {
                InputHandler.RecentInputs.Add("Sample Text");
            }
            InputHandler.ClearInputs();
        }

        /// <summary>
        /// Asserts that <see cref="InputHandler.RecordInputs(String)"/> does not go over limit.
        /// </summary>
        [Test] 
        public void InputHandler_RecordInputs_DoesNotGoOverMaxEntries() {
            List<String> inputs = new List<String>();

            for(Int32 i = 30; i > 0; i--) {
                inputs.Add($"newInput{i}");
            }

            foreach(String input in inputs) {
                InputHandler.RecordInputs(input);
            }

            Assert.That(InputHandler.RecentInputs.Count, Is.EqualTo(30));
            
            InputHandler.RecordInputs($"newInput0");

            Assert.That(InputHandler.RecentInputs.Count, Is.EqualTo(30));
        }


        [Test]
        public void InputHandler_MovePawn_ChecksIfNodeIsFull() {

        }
    }
}
