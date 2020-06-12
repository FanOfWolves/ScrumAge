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

		/// <summary>
		/// This determines if the command has valid syntax then 
		/// </summary>
		/// <param name="playerInput">The input to be handled</param>
		/// <param name="player">The player that inputted</param>
		public static void HandleInput(String playerInput, Player player, Game game, List<Pawn> pawns) {
			RecordInputs(playerInput);
			playerInput = playerInput.ToLower().Replace(" the", ""); // In case of console user input, the word "the" means nothing
			String[] inputArr = playerInput.Split(' ');              // Split the input by space
			try {
				if(inputArr.Length >= 2) {                           // If the the input is only one word, no argument was given which is invalid based on this handling
					DetermineCommand(inputArr, player, game, pawns);                // This is where magic happens, this function takes each part of the input and does the specified action
				} else {
					throw new InvalidInputEx(inputArr);              // If the command is invalid, throw an exception
				}
			} catch(InvalidInputEx ex) {
				WriteLine(ex.GetMessage(inputArr));                 // Change this line to however we tell user something went wrong
			}
		}

		// Also, possible replacements for the string-as-command system that is currently being used might be needed.
		/// <summary>
		/// Determines command + args based on a string array passed in.
		/// </summary>
		/// <param name="inputArr">The array of command and args</param>
		/// <param name="player">The player that did the input</param>
		/// <param name="board">The state of the board</param>
		private static void DetermineCommand(String[] inputArr, Player player, Game game, List<Pawn> pawns) {
			switch(inputArr[0]) {
				case "add": {   // Command
						if(inputArr[1] == "pawn") // Arg 1
							GivePlayerPawn(inputArr, player, game); // Action Helper Function
						else if(inputArr[1] == "card")
							GivePlayerCard(inputArr, player);
						break;
					}
				case "move": {
						if(inputArr[1] == "pawn") {
							MovePawn(pawns, player, game.board.GetNodeByName($"{inputArr[2]} {inputArr[3]}"));
						}
						break;
					}
				case "roll": {
						if(inputArr[1] == "dice") {
							int dieCount = Int32.Parse(inputArr[2]);
							game.board.dice = RollDice(dieCount);
						}
						break;
					}

				default:
					throw new InvalidInputEx(inputArr);
			}
		}

		private static void GivePlayerCard(String[] inputArr, Player player) {	// Replace with inherited Card children?
			if(inputArr[2] == "artifact") { // Arg 2
				player.AddToFeatures(new Card("Artifact", "Name", "Would be a feature card")); // Also these should be existing cards when those are created
			} else if(inputArr[2] == "agility") {
				player.AddToUserStories(new Card("Agility", "Name", "Would be a story card"));
			}
		}

		private static void GivePlayerPawn(String[] inputArr, Player player, Game game) {
			if(inputArr.Length == 2)
				player.GivePawn(game.PawnLevels[Rand.Next(3)]);
			else if(inputArr.Length == 3)
				player.GivePawn(game.PawnLevels[int.Parse(inputArr[2])]);
			else if(inputArr.Length == 4)
				player.GivePawn($"{inputArr[2]} {inputArr[3]}");
		}


		/// <summary>
		/// Roll a specified number of dice and return them as a List.
		/// </summary>
		/// <param name="diceCount">The number of dice to roll.</param>
		/// <returns>A list of the dice.</returns>
		private static List<Die> RollDice(int diceCount) {
			List<Die> dice = new List<Die>();
			for(int i = 0; i < diceCount; i++) {
				dice.Add(new Die((Rand.Next(6) + 1)));
			}
			return dice;
		}

		private static void MovePawn(List<Pawn> pawns, Player player, Node node) {
			if(PlayerOwnsAll(pawns, player.PlayerID)) {
				foreach(Pawn p in pawns) {
					player.TakePawn(p);
					node.AddPawn(p);
				}
			}
		}

		private static Boolean PlayerOwnsAll(List<Pawn> pawns, int playerID) {
			foreach(Pawn p in pawns) {
				if(p.PawnID != playerID) {
					return false;
				}
			}
			return true;
		}
	}
}