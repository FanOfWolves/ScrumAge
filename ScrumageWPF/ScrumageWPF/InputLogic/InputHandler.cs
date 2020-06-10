using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;
using ScrumageEngine.MapSpace;
using static ScrumageEngine.MapSpace.Board;
using ScrumageEngine.Exceptions;
using ScrumageEngine.Objects.Humans;
using ScrumageEngine.Objects.Items;




/*
 * WE NEED TO PLAN OUT WHERE INSTANCES WILL ACTUALLY BE! Should only the board be instanciated on the GUI? Should the board + players be on the GUI? Should
 * nothing be on the GUI? Etc. This will be discussed in the design part of the first meeting on June 10@11 AM with the developers!
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

		// This property will likely be moved to the Board class when that is created in implementation
		private static String[] PawnLevels = {"Front End", "Back End", "Full Stack"};
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
			if (recentInputs.Count < 5) {							// This number is the max that is to be recorded
				recentInputs.Insert(0, mostRecentInput);
			} else {												// If the input list is already full
				recentInputs.RemoveAt(4);							// Remove the last input(total number -1)
				recentInputs.Insert(0, mostRecentInput);			// Then put the new input at the top
			}
		}

		/// <summary>
		/// This determines if the command has valid syntax then 
		/// </summary>
		/// <param name="playerInput">The input to be handled</param>
		/// <param name="player">The player that inputted</param>
		public static void HandleInput(String playerInput, Player player, Board board) {
			RecordInputs(playerInput);
			playerInput = playerInput.ToLower().Replace(" the", ""); // In case of console user input, the word "the" means nothing
			String[] inputArr = playerInput.Split(' ');				 // Split the input by space
			try {
				if (inputArr.Length >= 2) {							 // If the the input is only one word, no argument was given which is invalid based on this handling
					DetermineCommand(inputArr, player, board);				 // This is where magic happens, this function takes each part of the input and does the specified action
				} else {
					throw new InvalidInputEx(inputArr);				 // If the command is invalid, throw an exception
				}
			} catch (InvalidInputEx ex) {
				WriteLine(ex.GetMessage(inputArr));					// Change this line to however we tell user something went wrong
			}
		}

		// Also, possible replacements for the string-as-command system that is currently being used might be needed.
		/// <summary>
		/// Determines command + args based on a string array passed in.
		/// </summary>
		/// <param name="inputArr">The array of command and args</param>
		/// <param name="player">The player that did the input</param>
		/// <param name="board">The state of the board</param>
		private static void DetermineCommand(String[] inputArr, Player player, Board board) {
			switch (inputArr[0]) {
				case "add": {	// Command

						/*
						 * Don't do this in implementation! Each case should get a helper function!!!!!
						 * 
						 */
						if(inputArr[1] == "pawn") { // Arg 1
							player.GivePawn(PawnLevels[Rand.Next(3)]);
						}else if(inputArr[1] == "card") {
							if(inputArr[2] == "feature") { // Arg 2
								player.AddToFeatures(new Card("Type", "Name", "Would be a feature card"));
							}else if(inputArr[2] == "story") {
								player.AddToUserStories(new Card("Type", "Name", "Would be a story card"));
							}
						}
						break;
				}
				case "move": {
						if(inputArr[1] == "pawn") {
							Pawn tempPawn = player.TakePawn(inputArr[2]);
							board.GetNodeByID(1).AddPawn(tempPawn);
						}
						break;
					}
				case "roll": {
						if(inputArr[1] == "dice") {
							int dieCount = Int32.Parse(inputArr[2]);

							//board.dice = RollDice(dieCount);
						}
						break;
					}

				default:
					throw new InvalidInputEx(inputArr);
			}
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
	}
}