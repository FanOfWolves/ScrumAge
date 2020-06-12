using System;
using System.Collections.Generic;
using ScrumageEngine.Objects.Humans;
using ScrumageEngine.Objects.Items;
using System.Text;

namespace ScrumageEngine.BoardSpace {
	public class Game {
		public List<Player> Players { get; set; }
		public Board board = new Board();
		public String[] PawnLevels = { "Front End", "Back End", "Full Stack" };
		public Game(List<String> playerNames) {
			Players = InitPlayers(playerNames);
		}

		private List<Player> InitPlayers(List<String> playerNames) {
			List<Player> retPlayers = new List<Player>();
			for(int i = 0; i < playerNames.Count; i++) {
				retPlayers.Add(new Player(i + 1, playerNames[i]));
				// Whatever else needs to be done when players are created goes here.

			}
			return retPlayers;
		}

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
	}
}
