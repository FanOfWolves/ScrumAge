using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using ScrumageEngine.BoardSpace;

namespace ScrumageWPF.Test {
	[TestFixture]
	class Game_Test {
		public List<String> names = new List<String>();
		public Game game;

		#region Setup and Teardown
		[OneTimeSetUp]
		public void Game_Setup() {
			names.Add("Player 1");
			names.Add("Player 2");
			names.Add("Bob");
			names.Add("Amy");
			game = new Game(names);
		}

		[OneTimeTearDown]
		public void Game_TearDown() {
			game = null;
		}
		#endregion

	}
}
