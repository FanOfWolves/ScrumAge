using System;
using System.Collections.Generic;
using NUnit.Framework;
using ScrumageEngine.BoardSpace;
using ScrumageEngine.Objects.Items.Cards;
using ScrumageEngine.Objects.Items;
using ScrumageEngine.Objects.Player;
using System.Diagnostics.CodeAnalysis;
using ScrumageWPF.Test.Utilities;
using System.Text;
namespace ScrumageWPF.Test {
	[TestFixture]
	class Game_Test {
		public List<String> names = new List<String>();
		public List<Node> expectedNodes;
		public Game game;
		/// <summary>
		/// The types of acceptable <see cref="Pawn"/>s
		/// </summary>
		private String[] PawnTypes = { "Front End", "Back End", "Full Stack" };

		#region Setup and Teardown
		[OneTimeSetUp]
		public void Game_Setup() {
			names.Add("Player 1");
			names.Add("Player 2");
			names.Add("Bob");
			names.Add("Amy");
			game = new Game(names);
			expectedNodes = GetNeededNodes();
		}

		[OneTimeTearDown]
		public void Game_TearDown() {
			game = null;
		}
		#endregion

		#region Constructor Test
		[Test]
		public void CheckForPlayers() {
			Assert.That(game.Players, Has.One.EqualTo(new Player(1, "Player 1")).Using(new PlayerEqualityComparer()));
			Assert.That(game.Players, Has.One.EqualTo(new Player(2, "Player 2")).Using(new PlayerEqualityComparer()));
			Assert.That(game.Players, Has.One.EqualTo(new Player(3, "Bob")).Using(new PlayerEqualityComparer()));
			Assert.That(game.Players, Has.One.EqualTo(new Player(4, "Amy")).Using(new PlayerEqualityComparer()));
		}
		#endregion

		#region Getters Tests
		[Test]
		public void Test_Existing_Nodes_By_Name() {
			foreach(Node expectedNode in expectedNodes) {
				Assert.That(game.GetNodeByName(expectedNode.NodeName), Is.EqualTo(expectedNode).Using(new NodeEqualityComparer()));
			}
		}

		[Test]
		public void Test_Existing_Nodes_By_ID() {
			foreach(Node expectedNode in expectedNodes) {
				Assert.That(game.GetNodeByID(expectedNode.NodeID), Is.EqualTo(expectedNode).Using(new NodeEqualityComparer()));
			}
		}

		[Test]
		public void Test_Get_Node_Names() {
			List<String> nodeNames = game.GetNodeNames();
			for(int i = 0; i < nodeNames.Count; i++) {
				Assert.That(nodeNames[i] == expectedNodes[i].NodeName);
			}
		}

		[Test]
		public void Test_Get_Player_By_ID() {
			Assert.That(game.GetPlayerByID(1), Is.EqualTo(new Player(1, "Player 1")).Using(new PlayerEqualityComparer()));
			Assert.That(game.GetPlayerByID(2), Is.EqualTo(new Player(2, "Player 2")).Using(new PlayerEqualityComparer()));
			Assert.That(game.GetPlayerByID(3), Is.EqualTo(new Player(3, "Bob")).Using(new PlayerEqualityComparer()));
			Assert.That(game.GetPlayerByID(4), Is.EqualTo(new Player(4, "Amy")).Using(new PlayerEqualityComparer()));
		}
		#endregion

		#region Action Tests
		[Test]
		public void Test_Pay_Pawns() {
			Int32 expectedFunds = game.Players[0].Funds;
			Player p = game.Players[0];
			p.Pawns.ForEach(pawn => { expectedFunds -= pawn.PawnCost; });
			expectedFunds += p.Budget;
			String testLog = "";
			game.currentPlayerIndex = 0; // Ensure we are on the right player
			game.PayPawns(out testLog);
			Assert.That(p.Funds == expectedFunds);
		}
		#endregion

		#region Dice tests

		#endregion

		#region Helpers
		/// <summary>
		/// Get expected nodes, for testing.
		/// </summary>
		/// <returns>returns a list of nodes.</returns>
		private List<Node> GetNeededNodes() {
			List<Node> neededNodes = new List<Node>(16);
			neededNodes.Add(new ResourceNode(1, "Requirements", new Requirements()));
			neededNodes.Add(new ResourceNode(2, "Design", new Design()));
			neededNodes.Add(new ResourceNode(3, "Implementation", new Implementation()));
			neededNodes.Add(new ResourceNode(4, "Testing", new Testing()));
			neededNodes.Add(new UpgradeNode(5, "Technical Hut"));
			neededNodes.Add(new BudgetNode(6, "Budget Increase"));
			neededNodes.Add(new HiringNode(7, "Interview"));
			neededNodes.Add(new ReassignmentNode(8, "Reassignment"));
			neededNodes.Add(new CardNode(9, "Agility 1", new Deck("Agility", 10)));
			neededNodes.Add(new CardNode(10, "Agility 2", new Deck("Agility", 10)));
			neededNodes.Add(new CardNode(11, "Agility 3", new Deck("Agility", 10)));
			neededNodes.Add(new CardNode(12, "Agility 4", new Deck("Agility", 10)));
			neededNodes.Add(new CardNode(13, "Artifact 1", new Deck("Artifact", 10)));
			neededNodes.Add(new CardNode(14, "Artifact 2", new Deck("Artifact", 10)));
			neededNodes.Add(new CardNode(15, "Artifact 3", new Deck("Artifact", 10)));
			neededNodes.Add(new CardNode(16, "Artifact 4", new Deck("Artifact", 10)));
			return neededNodes;
		}

		#endregion
	}
}
