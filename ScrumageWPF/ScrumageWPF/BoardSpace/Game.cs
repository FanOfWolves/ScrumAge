using System;
using System.Collections.Generic;
using ScrumageEngine.Objects.Player;
using ScrumageEngine.Objects.Items;
using System.Text;

namespace ScrumageEngine.BoardSpace {
	public class Game {
		private List<Player> Players { get; }
		private Board board = new Board();
		private String[] PawnTypes = {"Front End", "Back End", "Full Stack"};
		private Int32 phase = 1;
		private static Random Rand = new Random(); // Maybe move this to Game?

		/// <summary>
		/// Game constructor
		/// </summary>
		/// <param name="playerNames">List of the player Names</param>
		public Game(List<String> playerNames) {
			Players = InitPlayers(playerNames);
		}

		/// <summary>
		/// Initializes players and returns them as a list.
		/// </summary>
		/// <param name="playerNames">The list of names for the players.</param>
		/// <returns>A list of the players.</returns>
		private List<Player> InitPlayers(List<String> playerNames) {
			List<Player> retPlayers = new List<Player>();
			for (Int32 i = 0; i < playerNames.Count; i++) {
				retPlayers.Add(new Player(i + 1, playerNames[i]));
				// Whatever else needs to be done when players are created goes here.
				retPlayers[i].GivePawn("Front End");
				retPlayers[i].GivePawn("Front End");
				retPlayers[i].GivePawn("Back End");
				retPlayers[i].GivePawn("Back End");
				retPlayers[i].GivePawn("Back End");
			}

			return retPlayers;
		}

		/// <summary>
		/// Returns a given node by name.
		/// </summary>
		/// <param name="nodeName">Name of the node to be returned.</param>
		/// <returns>The node requested.</returns>
		public Node GetNodeByName(String nodeName) {
			return board.GetNodeByName(nodeName);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="nodeIDP"></param>
		/// <returns></returns>
		public Node GetNodeByID(Int32 nodeIDP) {
			return board.GetNodeByID(nodeIDP);
		}

		public Player GetPlayerByID(Int32 playerIDP) {
			return Players.Find(player => player.PlayerID == playerIDP);
		}

		public List<Player> GetAllPlayers() {
			return Players;
		}

		private void CheckPlayerPawns() {
			// If currentPlayer.Pawns.Items.Count == 0
			// set DoneWithPhase = true;
		}

		private void CheckPhase(Int32 phase) {
			if (phase == 1) PhaseOne();
			else if (phase == 2) PhaseTwo();
			else if (phase == 3) PhaseThree();
		}

		private void PhaseOne() { }

		private void PhaseTwo() {
			throw new NotImplementedException();
		}

		private void PhaseThree() {
			throw new NotImplementedException();
		}

		/// <summary>
		/// Uses a global random to roll a specified number of dice.
		/// </summary>
		/// <param name="diceCount">The number of dice wish to be rolled.</param>
		/// <param name="rand">A global random.</param>
		internal void RollDice(Int32 diceCount) {
			board.ClearDice();
			board.RollDice(diceCount, Rand);
		}

		public List<String> ShowDice() {
			return board.ShowDice();
		}

		// Move this to Board and call that
		/// <summary>
		/// Represents current dice values in a single string for the log.
		/// </summary>
		/// <returns>String of current dice values.</returns>
		internal String DiceValues() {
			String retString = "";
			foreach (Die d in board.GetDice()) {
				retString += d.Value + " ";
			}

			return retString;
		}

		public void GivePlayerCard(Int32 playerIDP, String cardTypeP) {
			Player player = GetPlayerByID(playerIDP);
			if (cardTypeP == "artifact") {
				player.AddToArtifacts(board.GetTopArtifact());
			}else if (cardTypeP == "agility") {
				player.AddToAgility(board.GetTopAgility());
			}
		}

		public String GetPawnType(Int32 indexP) {
			return PawnTypes[indexP];
		}

		public List<String> GetNodeNames() {
			List<String> nodeNames = new List<String>();
			board.GetAllNodes().ForEach(node =>
			{
				nodeNames.Add(node.NodeName);
			});
			return nodeNames;
		}

		public List<String> GetPlayerPawns(Int32 playerIDP) {
			return GetPlayerByID(playerIDP).ListPawns();
		}

		public List<String> GetNodePawns(String nodeNameP) {
			return board.ListNodePawns(nodeNameP);
		}

		public String GetPlayerNameByID(Int32 playerIDP) {
			return GetPlayerByID(playerIDP).PlayerName;
		}

		public String GivePlayerPawn(Int32 playerIDP, String pawnTypeP) {
			String[] inputArr = pawnTypeP.Split(' ');
			Player player = GetPlayerByID(playerIDP);
			String pawnType = "";
			if (inputArr[0] == "") {
				pawnType = GetPawnType(Rand.Next(2));
				player.GivePawn(pawnType);
			} else if (inputArr.Length == 1) {
				pawnType = GetPawnType(Int32.Parse(inputArr[0]));
				player.GivePawn(pawnType);
			} else if (inputArr.Length == 2) {
				pawnType = $"{inputArr[0]} {inputArr[1]}";
				player.GivePawn(pawnType);
			}
			return $"{player.PlayerName} received a {pawnType} pawn";
			//return "";
		}

		public void MovePawn(List<String> pawnsP, Int32 playerIDP, String nodeName) {
			Player player = GetPlayerByID(playerIDP);
			Pawn pawn;
			Node node = GetNodeByName(nodeName);
			foreach(String p in pawnsP) {
				pawn = player.TakePawn(p.Split(',')[0]);
				node.AddPawn(pawn);
			}
		}

		public String DoAction(String nodeNameP, Int32 playerIDP) {
			return GetNodeByName(nodeNameP).DoAction(GetPlayerByID(playerIDP));
		}
	}
}
