using System;
using System.Collections.Generic;
using ScrumageEngine.Objects.Humans;
using ScrumageEngine.Objects.Items;
using System.Text;

namespace ScrumageEngine.BoardSpace {
	public class Game {
		public List<Player> Players { get; set; }
		public Board board = new Board();
		public String[] PawnTypes = { "Front End", "Back End", "Full Stack" };
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
			for(Int32 i = 0; i < playerNames.Count; i++) {
				retPlayers.Add(new Player(i+1, playerNames[i]));
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
        /// Returns a given node by name
        /// </summary>
        /// <param name="nodeName"></param>
        /// <returns></returns>
		public Node GetNodeByName(String nodeName) {
			Node retNode = board.GetNodeByName(nodeName);

			// Keep track of what just happened
			//			--> Check if all players have moved all pawns
			//					--> If so, move to next phase
			//					--> If not, move to next player with false CompletedPhase
			// Call the function in Board
			CheckPlayerPawns();
			CheckPhase();
			return retNode;
		}

		private void CheckPlayerPawns() {
			// If currentPlayer.Pawns.Items.Count == 0
				// set DoneWithPhase = true;
		}

		private void CheckPhase() {
			foreach(Player player in Players) {
				// If player has moved all pawns,
					// Set DoneWithPhased
				
			}
		}

		/// <summary>
		/// Uses a global random to roll a specified number of dice.
		/// </summary>
		/// <param name="diceCount">The number of dice wish to be rolled.</param>
		/// <param name="rand">A global random.</param>
		internal void RollDice(Int32 diceCount, Random rand) {
			board.dice.Clear();
			for(Int32 i = 0; i < diceCount; i++) {
				board.dice.Add(new Die(rand.Next(6) + 1));
			}
		}

		/// <summary>
		/// Represents current dice values in a single string for the log.
		/// </summary>
		/// <returns>String of current dice values.</returns>
		internal String DiceValues() {
			String retString = "";
			foreach(Die d in board.dice) {
				retString += d.Value + " ";
			}
			return retString;
		}
	}
}
