using System;
using System.Collections.Generic;
using ScrumageEngine.Objects.Player;
using ScrumageEngine.Objects.Items;
using System.Text;
using System.Windows;
using ScrumageEngine.Objects.Items.Cards;

namespace ScrumageEngine.BoardSpace {
	public class Game {

		#region Properties
		/// <summary>
		/// The players in this Game
		/// </summary>
		/// <value>
		/// The players.
		/// </value>
		private List<Player> Players { get; }


		/// <summary>
		/// The game board
		/// </summary>
		private Board board = new Board();


		/// <summary>
		/// The types of acceptable <see cref="Pawn"/>s
		/// </summary>
		private String[] PawnTypes = { "Front End", "Back End", "Full Stack" };


		/// <summary>
		/// The current phase. 1 = Placement, 2 = Action, 3 = Payment
		/// </summary>
		public Int32 phase = 1;


		/// <summary>
		/// The <see cref="Random"/> object for all <see cref="Game"/>s
		/// </summary>
		private static Random Rand = new Random();


		/// <summary>
		/// The current <see cref="Player"/>'s <see cref="Player.PlayerID"/>
		/// </summary>
		public Int32 currentPlayerIndex = 0;
		#endregion

		#region Constructor and initializer
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
		#endregion

		#region Getters
		#region Nodes
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
		/// Get names of all <see cref="Node"/>s in game instance's <see cref="Board"/>.
		/// See also <seealso cref="Node.NodeName"/>.
		/// </summary>
		/// <returns>List of node names</returns>
		public List<String> GetNodeNames() {
			List<String> nodeNames = new List<String>();
			board.GetAllNodes().ForEach(node =>
			{
				nodeNames.Add(node.NodeName);
			});
			return nodeNames;
		}


		/// <summary>
		/// Get list of <see cref="Pawn"/>s at current <see cref="Node"/> by Node's name.
		/// See also <seealso cref="Node.NodeName"/>.
		/// </summary>
		/// <param name="nodeNameP">the name of the node we want.</param>
		/// <returns>list of Pawns at indicated Node</returns>
		public List<String> GetNodePawns(String nodeNameP) {
			return board.ListNodePawns(nodeNameP);
		}
		#endregion
		#region Players
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
		/// Get <see cref="Player"/> name attribute by player ID.
		/// <seealso cref="Player.PlayerName"/>
		/// </summary>
		/// <param name="playerIDP">the id of the player whose name we want</param>
		/// <returns>Player name in string</returns>
		public String GetPlayerNameByID(Int32 playerIDP) {
			return GetPlayerByID(playerIDP).PlayerName;
		}
		#endregion

		/// <summary>
		/// Get the specified <seealso cref="Pawn.PawnType"/>.
		/// See also <seealso cref="Game.PawnTypes"/>.
		/// </summary>
		/// <param name="indexP">indexer index.</param>
		/// <returns>string - a pawn type</returns>
		public String GetPawnType(Int32 indexP) {
			return PawnTypes[indexP];
		}
		#endregion

		#region Phase Functions
		/// <summary>
		/// Cycles through players allowing them to move pawn one move at a time until all pawns have been moved
		/// </summary>
		/// <returns>
		///		<c>true</c> if phase one is complete. Otherwise, <c>false</c> and sets turn to next unfinished player.
		/// </returns>
		private Boolean PhaseOne() { // UPDATE GUI PHASE BOX!
			if(AllPawnsMoved()) {
				phase = 2;
				currentPlayerIndex = 0;
				ResetPlayers();
				return true;
			} else {
				if(Players[currentPlayerIndex].CurrentPawns == 0) {
					Players[currentPlayerIndex].FinishedPhase = true;
				}
				if(++currentPlayerIndex >= Players.Count) currentPlayerIndex = 0;
				while(Players[currentPlayerIndex].FinishedPhase || Players[currentPlayerIndex].CurrentPawns == 0) {
					if(++currentPlayerIndex >= Players.Count) currentPlayerIndex = 0;
				}
			}
			return false;
		}


		/// <summary>
		/// Cycles through players allowing them to activate nodes one node at a time until all players have done all possible activations
		/// </summary>
		/// <returns></returns>
		private Boolean PhaseTwo() {
			if(AllPlayersDone()) {
				phase = 3;
				currentPlayerIndex = 0;
				ResetPlayers();
				return true;
			} else {
				if(++currentPlayerIndex >= Players.Count) currentPlayerIndex = 0;
				while(PlayerDoneWithActions(Players[currentPlayerIndex]) || Players[currentPlayerIndex].FinishedPhase) {
					if(++currentPlayerIndex >= Players.Count) currentPlayerIndex = 0;
				}
				return false;
			}
		}

		/// <summary>
		/// Cycles through players, forcing them to pay for the sprint costs.
		/// Note: All players have one round in this phase.
		/// </summary>
		/// <returns>
		///		<c>true</c> if all players have finished and sets game state; Otherwise, <c>false</c>.
		/// </returns>
		private Boolean PhaseThree() {
            if (AllPlayersPaid()) {
                this.phase = 1; //TODO: Perhaps go to an intermediate phase where we check if we are done with game?
                this.currentPlayerIndex = 0;
                ResetPlayers();
                return true;
            }
            this.currentPlayerIndex++;
            return false;
        }

		/// <summary>
		/// Pays the pawns for the current player.
		/// </summary>
		/// <param name="paymentLog">The payment log, detailing the payment.</param>
		/// <returns>
		///		<c>true</c> if payment successful; otherwise, <c>false</c>.
		/// </returns>
		public Boolean PayPawns(out String paymentLog) {
            Boolean paidCost = Players[this.currentPlayerIndex].PayPawns();
            if (paidCost) {
                paymentLog = $"{Players[this.currentPlayerIndex].PlayerName} paid off their costs.";
			}
            else paymentLog = $"{Players[this.currentPlayerIndex].PlayerName} could not pay off their cost! -10 points";
            return CheckPhase(this.phase);
        }

		/// <summary>
		/// Checks if all players have paid their sprint costs.
		/// </summary>
		/// <returns>
		///		<c>true</c> if all players have finished this phase; otherwise <c>false</c>.
		/// </returns>
		private Boolean AllPlayersPaid() {
			return this.Players.TrueForAll(player => player.FinishedPhase == true);
		}

		/// <summary>
		/// Checks current game phase and returns phase method
		/// </summary>
		/// <param name="phase"></param>
		/// <returns>Method for phase</returns>
		private Boolean CheckPhase(Int32 phase) {
			if(phase == 1) return PhaseOne();
			else if(phase == 2) return PhaseTwo();
			else if(phase == 3) return PhaseThree();
			return false;
		}

        /// <summary>
		/// Resets all of the players' FinishedPhase 
		/// </summary>
		private void ResetPlayers() {
			foreach(Player p in Players) {
				p.FinishedPhase = false;
			}
		}


		/// <summary>
		/// Checks to see if all pawns have been moved
		/// </summary>
		/// <returns>
		///		<c>true</c> if all players have moved all their pawns; otherwise, <c>false</c>
		/// </returns>
		private Boolean AllPawnsMoved() {
			Boolean allPlayersFinished = true;
			foreach(Player p in Players) {
				if(p.Pawns.Count > 0) {
					allPlayersFinished = false;
				}
			}
			return allPlayersFinished;
		}

		/// <summary>
		/// Checks if the current player has finished all possible actions, if so changes thayers FinishedPhase property.
		/// </summary>
		/// <param name="playerP">The current player.</param>
		/// <returns>True if player is finished, false if not.</returns>
		private Boolean PlayerDoneWithActions(Player playerP) {
			foreach(Node node in board.GetAllNodes()) {
				if(node.HasPawn(playerP.PlayerID)) return false;
			}
			playerP.FinishedPhase = true;
			return true;
		}


		/// <summary>
		/// Iterates through all players and checks to see if all players have finished all actions.
		/// </summary>
		/// <returns>True if all players have finished.</returns>
		private Boolean AllPlayersDone() {
			foreach(Player p in Players) {
				if(!PlayerDoneWithActions(p) || !p.FinishedPhase) return false;
			}
			return true;
		}


		/// <summary>
		/// Checks if all players have finished actions, external call.
		/// </summary>
		/// <returns>True if all players have finished actions</returns>
		internal Boolean CheckPlayerActions() {
			return AllPlayersDone();
		}



		#endregion

		#region Dice Methods


		/// <summary>
		/// Uses a global random to roll a specified number of dice.
		/// </summary>
		/// <param name="diceCount">The number of dice wish to be rolled.</param>
		/// <param name="rand">A global random.</param>
		internal void RollDice(Int32 diceCount) {
			board.ClearDice();
			board.RollDice(diceCount, Rand);
		}

		/// <summary>
		/// Show graphical representatiion of die faces
		/// </summary>
		/// <returns>list of strings representing die faces</returns>
		public List<String> ShowDice() {
			return board.ShowDice();
		}


		/// <summary>
		/// Represents current dice values in a single string for the log.
		/// </summary>
		/// <returns>String of current dice values.</returns>
		internal String DiceValues() {
			return board.DiceValues();
		}
		#endregion

		#region Actions

		/// <summary>
		/// Creates a <see cref="Pawn"/> for a <see cref="Player"/>, adding it to their inventory.
		/// </summary>
		/// <param name="playerIDP">the id of the player to give the pawn</param>
		/// <param name="pawnTypeP">the pawn type to be given</param>
		/// <returns>string: log with pawn name</returns>
		public String GivePlayerPawn(Int32 playerIDP, String pawnTypeP) {
			String[] inputArr = pawnTypeP.Split(' ');
			Player player = GetPlayerByID(playerIDP);
			String pawnType = "";
			if(inputArr[0] == "") {
				pawnType = GetPawnType(Rand.Next(2));
				player.GivePawn(pawnType);
			} else if(inputArr.Length == 1) {
				pawnType = GetPawnType(Int32.Parse(inputArr[0]));
				player.GivePawn(pawnType);
			} else if(inputArr.Length == 2) {
				pawnType = $"{inputArr[0]} {inputArr[1]}";
				player.GivePawn(pawnType);
			}
			return $"{player.PlayerName} received a {pawnType} pawn";
		}


		/// <summary>
		/// Move <see cref="Pawn"/> from player's inventory to <see cref="Node"/> location
		/// </summary>
		/// <param name="pawnsP">list of pawn strings to be moved.</param>
		/// <param name="playerIDP">the acting player's id.</param>
		/// <param name="nodeName">the name of the Node to move Pawns to.</param>
		/// <returns>
		///		<c>true</c> if moved;otherwise <c>false</c>
		/// </returns>
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


		/// <summary>
		/// Perform the action of the specified <see cref="Node"/>. Uses <seealso cref="Node.DoAction(Player)"/>.
		/// </summary>
		/// <param name="nodeNameP">the name of the Node whose action we are using.</param>
		/// <param name="playerIDP">the id of the acting player</param>
		/// <returns>string for console log of what has changed</returns>
		public Boolean DoAction(String nodeNameP, Int32 playerIDP, out String nodeLog) {
			nodeLog = GetNodeByName(nodeNameP).DoAction(GetPlayerByID(playerIDP));
			return CheckPhase(phase);
		}
		#endregion

		#region Player Tracking	


		/// <summary>
		/// Get specified <see cref="Player"/>'s pawns by their player id
		/// </summary>
		/// <param name="playerIDP">the ID of the player to be returned.</param>
		/// <returns>List of pawns currently used by the player</returns>
		public List<String> GetPlayerPawns(Int32 playerIDP) {
			return GetPlayerByID(playerIDP).ListPawns();
		}


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
