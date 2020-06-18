using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;
using ScrumageEngine.BoardSpace;
using ScrumageEngine.Exceptions;
using System.Linq.Expressions;




/*
 * WE NEED TO PLAN OUT WHERE INSTANCES WILL ACTUALLY BE! Should only the board be instanciated on the GUI? Should the board + players be on the GUI? Should
 * nothing be on the GUI? Etc. This will be discussed in the  part of the first meeting on June 10@11 AM with the developers!
 * */



namespace ScrumageEngine.InputLogic {
	/// <summary>
	/// Static class that handles input from the user, send information Int32o this as a space delimited String(Command Argument Arguemnt ...)
	/// </summary>
	public static class InputHandler {


		/// <summary>
		/// The most recent input that is handled
		/// </summary>
		private static String mostRecentInput;


		/// <summary>
		/// A list of inputs handled, length specified in RecordInputs function
		/// </summary>
		private static List<String> recentInputs = new List<String>();


		/// <summary>
		/// Returns the current state of the recent inputs
		/// </summary>
		/// <returns>A list of the recent inputs</returns>
		public static List<String> GetRecentInputs() {
			return recentInputs;
		}


		/// <summary>
		/// If getting any console input, this will read the input and record it(This is used with the system console in case we need it).
		/// </summary>
		/// <returns>The input after recording</returns>
		public static String PlayerInput() {
			mostRecentInput = ReadLine();
			RecordInputs(mostRecentInput);
			return mostRecentInput;
		}


		// We can do things like "Player must do this within so many inputs or else fail" or "Player has done this too recently"
		/// <summary>
		/// Records the inputs up to the given number of inputs.
		/// </summary>
		/// <param name="mostRecentInput">The input that needs to be recorded.</param>
		public static void RecordInputs(String mostRecentInput) {
			if(recentInputs.Count < 20) {                            // This number is the max that is to be recorded
				recentInputs.Insert(0, mostRecentInput);
			} else {                                                // If the input list is already full
				recentInputs.RemoveAt(19);                           // Remove the last input(total number -1)
				recentInputs.Insert(0, mostRecentInput);            // Then put the new input at the top
			}
		}


		/// <summary>
		/// Gives the player the specified type of card.
		/// </summary>
		/// <param name="cardType">"artifact"/"agility" for the type of card wanted.</param>
		/// <param name="player">The player requesting the card.</param>
		public static void GivePlayerCard(Game game, String cardTypeP, Int32 playerIDP) { // Replace with inherited Card children?
			game.GivePlayerCard(playerIDP, cardTypeP); // Also these should be existing cards when those are created
			RecordInputs($"{game.GetPlayerNameByID(playerIDP)} took {cardTypeP}");
		}


		/// <summary>
		/// Gives the player a pawn based on an input
		/// </summary>
		/// <param name="player">The player requesting the pawn.</param>
		/// <param name="game">Current State of the game.</param>
		/// <param name="input">The type of pawn if specification is needed.</param>
		public static void GivePlayerPawn(Game gameP, int playerIDP, String inputP = "") {
			String logString = gameP.GivePlayerPawn(playerIDP, inputP);
			RecordInputs(logString);

		}


		/// <summary>
		/// Roll a specified number of dice and return them as a List.
		/// </summary>
		/// <param name="diceCount">The number of dice to roll.</param>
		/// <returns>A list of the dice.</returns>
		public static void RollDice(Game gameP, Int32 diceCountP) {
			gameP.RollDice(diceCountP);
			RecordInputs($"{diceCountP} dice rolled, Results: {gameP.DiceValues()}"); // Add results
		}

		/// <summary>
		/// Moves a pawn from the player's inventory to the specified node
		/// </summary>
		/// <param name="pawns">The list of pawns selected by the player</param>
		/// <param name="player">The player requesting the move</param>
		/// <param name="node">The node the player is trying to move to</param>
		public static Boolean MovePawn(Game gameP, List<String> pawnsP, Int32 playerIDP, String nodeNameP) {
			var player = gameP.GetPlayerByID(playerIDP);
			var node = gameP.GetNodeByName(nodeNameP);
			Int32 _newTotalOfPawnsInNode = pawnsP.Count + node.NumberOfPawns;
			Boolean _nodeFull = _newTotalOfPawnsInNode > node.MaxPawnLimit;
			if(pawnsP.Count > 0 && !_nodeFull) {
				return gameP.MovePawn(pawnsP, playerIDP, nodeNameP);
				RecordInputs($"{player.PlayerName} moved {ListPawns(pawnsP)} to {nodeNameP}");
			}else if(pawnsP.Count == 0) {
				RecordInputs($"{player.PlayerName} tried to move pawns that weren't theirs!");
				throw new MovePawnException("You cannot move another player's pawns.");
			}else if(_nodeFull) {
				RecordInputs($"{player.PlayerName} tried to move too many pawns to {nodeNameP}");
				throw new MovePawnException($"You are moving too many pawns to {nodeNameP}");
			}

			return false;
		}


		/// <summary>
		/// Lists all of the pawns in a collection.
		/// </summary>
		/// <param name="pawns">The pawns to be listed.</param>
		/// <returns>Comma separated pawn representation.</returns>
		private static String ListPawns(List<String> pawnsP) {
			String retString = "";
			foreach(String p in pawnsP) {
				retString += p + ", ";
			}
			return retString;
		}


		/// <summary>
		/// Calls the selected node's DoAction function.
		/// </summary>
		/// <param name="player">The player that selected the node.</param>
		/// <param name="node">The node that was selected in the GUI.</param>
		public static void ActivateNode(Game gameP, int playerIDP, String nodeNameP) {
			String nodeLog = gameP.DoAction(nodeNameP, playerIDP);
			RecordInputs(nodeLog);
		}
	}
}