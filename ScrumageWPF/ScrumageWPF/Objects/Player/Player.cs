﻿using System;
using System.Collections.Generic;
using ScrumageEngine.BoardSpace;
using ScrumageEngine.Exceptions;
using System.Text;
using ScrumageEngine.Objects.Items;

namespace ScrumageEngine.Objects.Humans {
	/// <summary>
	/// Player class used to represent and retain information about a player
	/// </summary>
	public class Player {
		/// <summary>
		/// An ID to be used for the player for easier identification than their name
		/// </summary>
		public Int32 PlayerID { get; set; }
		/// <summary>
		/// The player's name to be used as a representation on the board
		/// </summary>
		public String PlayerName { get; set; }
		/// <summary>
		/// A list of the player's (currently held) pawn
		/// </summary>
		public List<Pawn> Pawns = new List<Pawn>();
		/// <summary>
		/// A list of the player's User Story cards to hold obtained cards
		/// </summary>
		public List<Card> UserStories = new List<Card>();
		/// <summary>
		/// A list of the player's Feature cards ti hold obtained cards
		/// </summary>
		public List<Card> FeatureCards = new List<Card>();
		/// <summary>
		/// The amount of budget the player recieves every turn
		/// </summary>
		public Int32 Budget { get; set; }
		/// <summary>
		/// The amount of funds that the player currently has to be used to deploy pawns
		/// </summary>
		public Int32 Funds { get; set; }
		/// <summary>
		/// The feature poInt32s represent the overall score for the player
		/// </summary>
		public Int32 FeaturePoInt32s { get; set; } // Still need to determine how to calculate these
		/// <summary>
		/// Player overloaded constructor
		/// </summary>
		/// <param name="playerID">The player's ID</param>
		/// <param name="playerName">The player's name</param>
		public Player(Int32 playerID, String playerName) {
			PlayerID = playerID;
			PlayerName = playerName;
			FeaturePoInt32s = 0;
			Budget = 1;
			Funds = Budget;
		}
		/// <summary>
		/// A group of functions that are used to Int32eract with Items, i.e. giving players pawn, cards, or checking if player has a certain type of pawn
		/// </summary>
		#region Item Related Methods
		/// <summary>
		/// Adds an already existing pawn Int32o the player's pawn inventory
		/// </summary>
		/// <param name="pawn">An existing pawn that the player is moving back</param>
		public void GivePawn(Pawn pawn) {
			this.Pawns.Add(pawn);
		}
		/// <summary>
		/// Adds a User Story card Int32o the player's User Stories inventory
		/// </summary>
		/// <param name="userStory">The card to be added</param>
		public void AddToUserStories(Card userStory) {
			this.UserStories.Add(userStory);
		}
		/// <summary>
		/// Adds a Feature card to the User's Feature inventory
		/// </summary>
		/// <param name="feature">The feature to be added</param>
		public void AddToFeatures(Card feature) {
			this.FeatureCards.Add(feature);
		}
		/// <summary>
		/// Removes a pawn rom the user's pawn inventory
		/// </summary>
		/// <param name="pawn">The pawn to be removed</param>
		public void TakePawn(Pawn pawn) {
			this.Pawns.Remove(pawn);
		}
		/// <summary>
		/// Removes a card from the player's User Stories inventory
		/// </summary>
		/// <param name="userStory">The User Story to be removed</param>
		public void RemoveFromUserStories(Card userStory) {
			this.UserStories.Remove(userStory);
		}
		/// <summary>
		/// Removes a Feature card from the Features inventory
		/// </summary>
		/// <param name="feature">The Feature to be removed</param>
		public void RemoveFromFeatures(Card feature) {
			this.FeatureCards.Remove(feature);
		}
		/// <summary>
		/// Gives a pawn of a specified level(a NEW pawn) to the player
		/// </summary>
		/// <param name="PawnType">The pawn level to be added</param>
		public void GivePawn(String PawnType) {
			Pawns.Add(new Pawn(PlayerID, PawnType));
		}
		/// <summary>
		/// Takes a pawn from the player based on a pawn level
		/// </summary>
		/// <param name="PawnType">The level of the pawn to be taken</param>
		/// <returns></returns>
		public Pawn TakePawn(String PawnType) {
			Pawn retPawn = new Pawn();
			if((retPawn = HasPawn(PawnType)) != new Pawn()) {
				return retPawn;
			}
			return retPawn;
		}
		/// <summary>
		/// Determines if a player has a pawn of a specified level then returns that pawn. If pawn of that level is not found, returns default "none" pawn.
		/// </summary>
		/// <param name="PawnType">The pawn level to be found</param>
		/// <returns></returns>
		public Pawn HasPawn(String PawnType) {
			Pawn retPawn = new Pawn();
			PawnType = PawnType.ToLower();
			foreach(Pawn pawn in Pawns) {
				if(pawn.PawnType.ToLower().Equals(PawnType)) {
					retPawn = pawn;
					break;
				} else if(pawn.PawnType.Contains(PawnType)) {
					retPawn = pawn;
					break;
				} else {
					continue;
				}
			}
			return retPawn;
		}

		/// <summary>
		/// Gives player additional funds
		/// </summary>
		/// <param name="fundsToGiveP">The funds to give</param>
		public void GiveFunds(Int32 fundsToGiveP) {
			this.Funds += fundsToGiveP;
		}

		#endregion
	}
}
