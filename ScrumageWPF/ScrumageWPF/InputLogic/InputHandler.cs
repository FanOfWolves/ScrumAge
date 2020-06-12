using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;
using ScrumageEngine.BoardSpace;
using static ScrumageEngine.BoardSpace.Board;
using ScrumageEngine.Exceptions;
using ScrumageEngine.Objects.Humans;
using ScrumageEngine.Objects.Items;




/*
 * WE NEED TO PLAN OUT WHERE INSTANCES WILL ACTUALLY BE! Should only the board be instanciated on the GUI? Should the board + players be on the GUI? Should
 * nothing be on the GUI? Etc. This will be discussed in the  part of the first meeting on June 10@11 AM with the developers!
 * */



namespace ScrumageEngine.InputLogic {
	/// <summary>
	/// Static class that handles input from the user, send information into this as a space delimited String(Command Argument Arguemnt ...)
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
		/// A random to be used in returning values based on inputs(use this as gloval random)
		/// </summary>
		private static Random Rand = new Random();
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

		public static void GivePlayerCard(String cardType, Player player) { // Replace with inherited Card children?
			RecordInputs($"{player.PlayerName} took {cardType}");
			if(cardType == "artifact") { // Arg 2
				player.AddToFeatures(new Card("Artifact", "Name", "Would be a feature card")); // Also these should be existing cards when those are created
			} else if(cardType == "agility") {
				player.AddToUserStories(new Card("Agility", "Name", "Would be a story card"));
			}
		}

		public static void GivePlayerPawn(Player player, Game game, String input = "") {
			RecordInputs($"{ player.PlayerName} received pawn");
			String[] inputArr = input.Split(' ');
			if(inputArr[0] == "")
				player.GivePawn(game.PawnLevels[Rand.Next(2)]);
			else if(inputArr.Length == 1)
				player.GivePawn(game.PawnLevels[int.Parse(inputArr[0])]);
			else if(inputArr.Length == 2)
				player.GivePawn($"{inputArr[0]} {inputArr[1]}");
		}


		/// <summary>
		/// Roll a specified number of dice and return them as a List.
		/// </summary>
		/// <param name="diceCount">The number of dice to roll.</param>
		/// <returns>A list of the dice.</returns>
		public static List<Die> RollDice(int diceCount) {
			RecordInputs($"{diceCount} dice rolled");
			List<Die> dice = new List<Die>();
			for(int i = 0; i < diceCount; i++) {
				dice.Add(new Die((Rand.Next(6) + 1)));
			}
			return dice;
		}

		public static void MovePawn(List<Pawn> pawns, Player player, Node node) {
			if(PlayerOwnsAll(pawns, player.PlayerID)) {
				foreach(Pawn p in pawns) {
					player.TakePawn(p);
					node.AddPawn(p);
				}
			}
		}

		public static Boolean PlayerOwnsAll(List<Pawn> pawns, int playerID) {
			foreach(Pawn p in pawns) {
				if(p.PawnID != playerID) {
					return false;
				}
			}
			return true;
		}
	}
}