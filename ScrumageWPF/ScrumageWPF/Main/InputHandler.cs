using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;
using StoneAgeEngine.MapSpace;
using static StoneAgeEngine.MapSpace.Map;
using StoneAgeEngine.Exceptions;
using StoneAgeEngine.Objects.Humans;

namespace StoneAgeEngine {
	/// <summary>
	/// Static class that handles input from the user, send information into this as a space delimited String(Command Argument Arguemnt ...)
	/// </summary>
	static class InputHandler {
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
		public static void HandleInput(String playerInput, Player player) {
			RecordInputs(playerInput);
			playerInput = playerInput.ToLower().Replace(" the", ""); // In case of console user input, the word "the" means nothing
			String[] inputArr = playerInput.Split(' ');				 // Split the input by space
			try {
				if (inputArr.Length >= 2) {							 // If the the input is only one word, no argument was given which is invalid based on this handling
					DetermineCommand(inputArr, player);				 // This is where magic happens, this function takes each part of the input and does the specified action
				} else {
					throw new InvalidInputEx(inputArr);				 // If the command is invalid, throw an exception
				}
			} catch (InvalidInputEx ex) {
				WriteLine(ex.GetMessage(inputArr));
			}
		}

		/// <summary>
		/// Determines the command being 
		/// </summary>
		/// <param name="inputArr">The array of each component of the command</param>
		/// <param name="player"></param>
		private static void DetermineCommand(String[] inputArr, Player player) {
			switch (inputArr[0]) {
				case "add": {
						if(inputArr[1] == "pawn") {
							player.GivePawn(PawnLevels[Rand.Next(3)]);
						}else if(inputArr[1] == "card") {
							if(inputArr[2] == "feature") {

							}else if(inputArr[2] == "story") {

							}
						}
						break;
				}
				case "move": {

						if(inputArr[1] == "pawn") {
							// Use currently selected pawn's level
						}
						break;
					}

				default:
					throw new InvalidInputEx(inputArr);
			}
		}
		public static String RejoinInput(String[] arr, int startIndex) {
			String retStr = "";
			for (int i = startIndex; i < arr.Length; i++) {
				retStr += " " + arr[i];
			}
			return retStr;
		}
	}
}