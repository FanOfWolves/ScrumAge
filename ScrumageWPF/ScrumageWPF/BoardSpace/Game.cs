﻿using System;
using System.Collections.Generic;
using ScrumageEngine.Objects.Player;
using ScrumageEngine.Objects.Items;
using System.Text;
using System.Windows;
using ScrumageEngine.Objects.Items.Cards;

namespace ScrumageEngine.BoardSpace {
	public class Game {
		private List<Player> Players { get; }
		private Board board = new Board();
		private String[] PawnTypes = {"Front End", "Back End", "Full Stack"};
		private Int32 phase = 1;
		private static Random Rand = new Random(); // Maybe move this to Game?
		public int currentPlayerIndex = 0;

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
		/// Return node object from ID
		/// </summary>
		/// <param name="nodeIDP">ID of node</param>
		/// <returns>Node object</returns>
		public Node GetNodeByID(Int32 nodeIDP) {
			return board.GetNodeByID(nodeIDP);
		}
		/// <summary>
		/// Retrieve Player ID
		/// </summary>
		/// <param name="playerIDP">Player Id set at start of game</param>
		/// <returns>Player object</returns>
		public Player GetPlayerByID(Int32 playerIDP) {
			return Players.Find(player => player.PlayerID == playerIDP);
		}
		/// <summary>
		/// Get all current player objects
		/// </summary>
		/// <returns>List of player objects</returns>
		public List<Player> GetAllPlayers() {
			return Players;
		}
		/// <summary>
		/// Checks current game phase and returns phase method
		/// </summary>
		/// <param name="phase"></param>
		/// <returns>Method for phase</returns>
		private Boolean CheckPhase(Int32 phase) {
			if (phase == 1) return PhaseOne();
			else if (phase == 2) return PhaseTwo();
			else if (phase == 3) return PhaseThree();
			return false;
		}

		/// <summary>
		/// Verifies if all pawns have been moved. Returns true if all pawns have been placed.
		/// </summary>
		/// <returns>boolean 1 if all pawns have been moved</returns>
		private Boolean PhaseOne() { // UPDATE GUI PHASE BOX!
			if (AllPawnsMoved()) {
				phase = 2;
				currentPlayerIndex = 0;
				return true;
			} else {
				if (Players[currentPlayerIndex].CurrentPawns == 0) {
					Players[currentPlayerIndex].FinishedPhase = true;
				}
				if (++currentPlayerIndex >= Players.Count) currentPlayerIndex = 0;
				while(Players[currentPlayerIndex].FinishedPhase || Players[currentPlayerIndex].CurrentPawns == 0) {
					if(++currentPlayerIndex >= Players.Count) currentPlayerIndex = 0;
				}
			}
			return false;
		}

        /// <summary>
        /// Boolean if all pawns have been moved
        /// </summary>
        /// <return>Boolean true if all pawns have moved from player inventory list</return>
        private Boolean AllPawnsMoved() {
			Boolean allPlayersFinished = true;
			foreach (Player p in Players) {
				if (p.Pawns.Count > 0) {
					allPlayersFinished = false;
				}
			}
			return allPlayersFinished;
		}
        /// <summary>
        /// Not implemented
        /// </summary>
        /// <returns></returns>
        private Boolean PhaseTwo() {
			return true;
		}
        /// <summary>
        /// Not implemented
        /// </summary>
        /// <returns></returns>
        private Boolean PhaseThree() {
			return true;
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
		}

		public Boolean MovePawn(List<String> pawnsP, Int32 playerIDP, String nodeName) {
			Player player = GetPlayerByID(playerIDP);
			Pawn pawn;
			Node node = GetNodeByName(nodeName);
			foreach(String p in pawnsP) {
				pawn = player.TakePawn(p.Split(',')[0]);
				node.AddPawn(pawn);
			}
			return CheckPhase(phase);
		}

		public String DoAction(String nodeNameP, Int32 playerIDP) {
			CheckPhase(phase);
			return GetNodeByName(nodeNameP).DoAction(GetPlayerByID(playerIDP));
		}


		#region Player Tracking		
		/// <summary>
		/// Gets the player agility cards.
		/// </summary>
		/// <param name="playerId">The player identifier.</param>
		/// <returns>a list of the player's cards</returns>
		public List<Card> GetPlayerAgilityCards(Int32 playerId) {
            return this.Players.Find(_player => _player.PlayerID == playerId).Agility;
        }

		/// <summary>
		/// Gets the player artifact cards.
		/// </summary>
		/// <param name="playerId">The player identifier.</param>
		/// <returns>a list of the player's cards</returns>
		public List<Card> GetPlayerArtifactCards(Int32 playerId) {
			return this.Players.Find(_player => _player.PlayerID == playerId).Artifacts;
		}

		/// <summary>
		/// Gets the player resources.
		/// </summary>
		/// <param name="playerIdP">The player identifier.</param>
		/// <returns>the player's resources</returns>
		public ResourceContainer GetPlayerResources(Int32 playerIdP) {
            return GetPlayerByID(playerIdP).GetPlayerResources();
        }

        #endregion

	}
}
