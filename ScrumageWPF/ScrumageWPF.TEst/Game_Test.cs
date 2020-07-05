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
using System.Collections;

namespace ScrumageWPF.Test {
	/// <summary>
	/// Tests for the Game class
	/// </summary>
	[TestFixture]
	class Game_Test {
		public List<String> names = new List<String>();
		public List<Node> expectedNodes;
		public Game game;

		#region Setup and Teardown
		/// <summary>
		/// Set up the game between each test
		/// </summary>
		[SetUp]
		public void Game_Setup() {
			names.Add("Player 1");
			names.Add("Player 2");
			names.Add("Bob");
			names.Add("Amy");
			game = new Game(names);
			expectedNodes = GetNeededNodes();
		}

		/// <summary>
		/// Tear down the game after each test
		/// </summary>
		[TearDown]
		public void Game_TearDown() {
			game = null;
		}
		#endregion

		#region Constructor Test
		/// <summary>
		/// Ensure that the game was created with all of the players asked for
		/// </summary>
		[Test]
		public void CheckForPlayers() {
			Assert.That(game.Players, Has.One.EqualTo(new Player(1, "Player 1")).Using(new PlayerEqualityComparer()));
			Assert.That(game.Players, Has.One.EqualTo(new Player(2, "Player 2")).Using(new PlayerEqualityComparer()));
			Assert.That(game.Players, Has.One.EqualTo(new Player(3, "Bob")).Using(new PlayerEqualityComparer()));
			Assert.That(game.Players, Has.One.EqualTo(new Player(4, "Amy")).Using(new PlayerEqualityComparer()));
		}
		#endregion

		#region Getters Tests
		/// <summary>
		/// Ensure that the game was created with all of the nodes it should have and they are obtainable through GetNodeByName
		/// </summary>
		[Test]
		public void Test_ExistingNodesByName() {
			foreach(Node expectedNode in expectedNodes) {
				Assert.That(game.GetNodeByName(expectedNode.NodeName), Is.EqualTo(expectedNode).Using(new NodeEqualityComparer()));
			}
		}


		/// <summary>
		/// Ensure that the game was created with all of the nodes it should have and they are obtainable through GetNodeByID
		/// </summary>
		[Test]
		public void Test_ExistingNodesByID() {
			foreach(Node expectedNode in expectedNodes) {
				Assert.That(game.GetNodeByID(expectedNode.NodeID), Is.EqualTo(expectedNode).Using(new NodeEqualityComparer()));
			}
		}


		/// <summary>
		/// Ensure all node names are obtainable into a List<String>
		/// </summary>
		[Test]
		public void Test_GetNodeNames() {
			List<String> nodeNames = game.GetNodeNames();
			for(int i = 0; i < nodeNames.Count; i++) {
				Assert.That(nodeNames[i] == expectedNodes[i].NodeName);
			}
		}


		/// <summary>
		/// Ensure all players are obtainaible through GetPlayerByID
		/// </summary>
		[Test]
		public void Test_GetPlayerByID() {
			Assert.That(game.GetPlayerByID(1), Is.EqualTo(new Player(1, "Player 1")).Using(new PlayerEqualityComparer()));
			Assert.That(game.GetPlayerByID(2), Is.EqualTo(new Player(2, "Player 2")).Using(new PlayerEqualityComparer()));
			Assert.That(game.GetPlayerByID(3), Is.EqualTo(new Player(3, "Bob")).Using(new PlayerEqualityComparer()));
			Assert.That(game.GetPlayerByID(4), Is.EqualTo(new Player(4, "Amy")).Using(new PlayerEqualityComparer()));
		}
		#endregion

		#region Action Tests

		/// <summary>
		/// Ensure players can pay their pawns and that the funds are correctly calculated
		/// </summary>
		[Test]
		public void Test_PayPawns() {
			Int32 expectedFunds = game.Players[0].Funds;
			Player p = game.Players[0];
			p.Pawns.ForEach(pawn => { expectedFunds -= pawn.PawnCost; });
			expectedFunds += p.Budget;
			String testLog = "";
			game.currentPlayerIndex = 0; // Ensure we are on the right player
			game.PayPawns(out testLog);
			Assert.That(p.Funds == expectedFunds);
		}


		/// <summary>
		/// Ensure that the players can be given pawns
		/// </summary>
		[Test]
		public void Test_GivePlayerPawn() { // Assume player 1 since they will always exist, and is newly instanced player
			Int32 playerID = 1;
			game.GivePlayerPawn(playerID, "Full Stack");
			Pawn testPawn = new Pawn(playerID, "Full Stack");
			Assert.That(game.Players[0].Pawns, Has.Member(testPawn).Using(new TestPawnEqualityCompare()));
		}


		/// <summary>
		/// Ensure that the players can move pawns into nodes and that the pawn is taken from them as well.
		/// </summary>
		[Test]
		public void Test_MovePawn() {
			Int32 playerID = 1;
			List<String> pawnsToMove = new List<String>();
			pawnsToMove.Add(game.GetPlayerByID(playerID).Pawns[0].ToString());
			game.MovePawn(pawnsToMove, playerID, "Technical Hut");
			Assert.That(game.GetNodeByName("Technical Hut").Pawns, Has.Member(new Pawn(1, "Front End")).Using(new TestPawnEqualityCompare()));
			Assert.That(game.GetPlayerByID(playerID).Pawns.Count == 4);
		}
		#endregion

		#region Dice tests
		/// <summary>
		/// Ensure the player can roll dice
		/// </summary>
		/// <param name="numOfDice">The number of dice to roll</param>
		[Test]
		#region Test Cases
		[TestCase(1)]
		[TestCase(2)]
		[TestCase(3)]
		[TestCase(4)]
		[TestCase(5)]
		[TestCase(6)]
		[TestCase(7)]
		#endregion
		public void Test_RollDice(Int32 numOfDice) {
			game.RollDice(numOfDice);
			Assert.That(game.board.Dice.Count == numOfDice);
		}


		/// <summary>
		/// Ensure that the representations of the dice are accurate to those currently on the board.
		/// </summary>
		/// <param name="diceValues">The values to be used in testing dice</param>
		[Test]
		#region Test Cases
		[TestCase(new Int32[] { 1,2,3,4,5,6})]
		[TestCase(new Int32[] { 2,2,5,4,5,3})]
		[TestCase(new Int32[] { 1,1,1,1,1,1})]
		[TestCase(new Int32[] { 0,0,0,0,0,0})]
		#endregion
		public void Test_ShowDice(Int32[] diceValues) {
			List<Die> testDice = new List<Die>();
			foreach(Int32 i in diceValues)
				testDice.Add(new Die(i));
			game.board.Dice = testDice;
			List<String> expectedDiceRepresentations = ShowDice(testDice);
			List<String> actualDiceRepresentations = game.ShowDice();
			Assert.That(ListCompare(expectedDiceRepresentations, actualDiceRepresentations));
		}


		/// <summary>
		/// Ensure that the values of dice can be returned in a string format instead of a die display
		/// </summary>
		/// <param name="diceValues">The values to be used for testing</param>
		[Test]
		#region Test Cases
		[TestCase(new Int32[] { 1, 2, 3, 4, 5, 6 })]
		[TestCase(new Int32[] { 2, 2, 5, 4, 5, 3 })]
		[TestCase(new Int32[] { 1, 1, 1, 1, 1, 1 })]
		[TestCase(new Int32[] { 0, 0, 0, 0, 0, 0 })]
		#endregion
		public void Test_DiceValues(Int32[] diceValues) {
			List<Die> testDice = new List<Die>();
			foreach(Int32 i in diceValues)
				testDice.Add(new Die(i));
			game.board.Dice = testDice;
			String expectedDiceRepresentations = DiceValues(testDice);
			String actualDiceRepresentations = game.DiceValues();
			Assert.That(expectedDiceRepresentations == actualDiceRepresentations);
		}



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

		/// <summary>
		/// Shows a GUI representation of the die face
		/// </summary>
		/// <returns>List of die graphics for display on GUI</returns>
		public List<String> ShowDice(List<Die> dice) {
			List<String> retList = new List<String>();
			dice.ForEach(die => retList.Add(die.DrawDie()));
			return retList;
		}

		/// <summary>
		/// Represents current dice values in a single string for the log.
		/// </summary>
		/// <returns>String of current dice values.</returns>
		public String DiceValues(List<Die> dice) {
			String retString = "";
			foreach(Die d in dice) {
				retString += d.Value + " ";
			}
			return retString;
		}


		/// <summary>
		/// Compare two lists of Strings to ensure contents are the same(in the same order as well)
		/// </summary>
		/// <param name="listA">First List for comparison.</param>
		/// <param name="listB">Second List for comparison.</param>
		/// <returns>True if same/false if not</returns>
		public Boolean ListCompare(List<String> listA, List<String> listB) {
			if(listA.Count != listB.Count) return false;
			for(int i = 0; i < listA.Count; i++)
				if(listA[i] != listB[i]) return false;
			return true;
		}
		#endregion
	}
}
