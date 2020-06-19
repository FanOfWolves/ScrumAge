﻿using System;
using System.Collections.Generic;
using ScrumageEngine.BoardSpace;
using ScrumageEngine.Exceptions;
using System.Text;
using ScrumageEngine.Objects.Items;
using ScrumageEngine.Objects.Items.Cards;

namespace ScrumageEngine.Objects.Player {
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

		private String playerName;
		public String PlayerName {
			get { return playerName; }
		}


		#region Inventory

		#region Pawns
		/// <summary>
        /// A list of the player's (currently held) pawn
        /// </summary>
        public List<Pawn> Pawns = new List<Pawn>();

		/// <summary>
		/// Gets or sets the total pawns the player owns (including those not currently with them).
		/// </summary>
		/// <value>
		/// The total pawns.
		/// </value>
		public Int32 TotalPawns { get; private set; }

		/// <summary>
		/// Gets the current pawns in this player's inventory
		/// </summary>
		/// <value>
		/// The current pawns.
		/// </value>
		public Int32 CurrentPawns {
            get { return this.Pawns.Count;}
        }

        /// <summary>
        /// Adds an already existing pawn Int32o the player's pawn inventory
        /// </summary>
        /// <param name="pawn">An existing pawn that the player is moving back</param>
        public void GivePawn(Pawn pawn) {
            this.Pawns.Add(pawn);
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
                Pawns.Remove(retPawn);
                return retPawn;
            }
            return retPawn;
        }

		public Boolean FinishedPhase { get; set; }

		/// <summary>
		/// Player overloaded constructor
		/// </summary>
		/// <param name="playerID">The player's ID</param>
		/// <param name="playerName">The player's name</param>
		public Player(Int32 playerID, String playerNameP) {
			PlayerID = playerID;
			playerName = playerNameP;
			FeaturePoints = 0;
			Budget = 1;
			Funds = Budget;
			FinishedPhase = false;
		}


        #region Agility Cards
        /// <summary>
        /// A list of the player's Feature cards to hold obtained cards
        /// </summary>
        public List<Card> Agility = new List<Card>();

        /// <summary>
        /// Adds a Feature card to the User's Feature inventory
        /// </summary>
        /// <param name="feature">The feature to be added</param>
        public void AddToAgility(Card feature) {
            this.Agility.Add(feature);
        }

        /// <summary>
        /// Removes a Feature card from the Features inventory
        /// </summary>
        /// <param name="feature">The Feature to be removed</param>
        public void RemoveFromAgility(Card agilityP) {
            this.Agility.Remove(agilityP);
        }
        #endregion

		/// <summary>
		/// Adds a User Story card Int32o the player's User Stories inventory
		/// </summary>
		/// <param name="userStory">The card to be added</param>
		public void AddToArtifacts(Card userStory) {
			this.Artifacts.Add(userStory);
		}


		/// <summary>
		/// Adds a Feature card to the User's Feature inventory
		/// </summary>
		/// <param name="feature">The feature to be added</param>
		public void AddToAgility(Card feature) {
			this.Agility.Add(feature);
		}


		/// <summary>
		/// Removes a card from the player's User Stories inventory
		/// </summary>
		/// <param name="userStory">The User Story to be removed</param>
		public void RemoveFromArtifacts(Card artifactP) {
			this.Artifacts.Remove(artifactP);
		}


        #endregion"






        /// <summary>
        /// A list of the player's User Story cards to hold obtained cards
        /// </summary>
        public List<Card> Artifacts = new List<Card>();

       

        #region Resources
        private ResourceContainer playerResources;

        /// <summary>
        /// Adds the resource to the player's ResourceContainer
        /// </summary>
        /// <param name="resource">The resource to be added</param>
        public void AddResource(Resource resource) {
            this.playerResources.AddResource(resource);
        }

        /// <summary>
        /// Pays resource from the player's resources.
        /// </summary>
        /// <param name="resource">The resource to be paid.</param>
        /// <param name="resourceAmount">The amount to be paid.</param>
        /// <returns>
        ///		<c>true</c> if player had enough resources; otherwise <c>false</c>
        /// </returns>
        public Boolean TakeResource(Resource resource, Int32 resourceAmount) {
            return this.playerResources.TakeResources(resource, resourceAmount);
        }

		#endregion

		#endregion

		#region Stats
		/// <summary>
        /// The amount of budget the player receives every turn
        /// </summary>
        public Int32 Budget { get; set; }

        /// <summary>
        /// The amount of funds that the player currently has to be used to deploy pawns
        /// </summary>
        public Int32 Funds { get; set; }

        /// <summary>
        /// The feature points represent the overall score for the player
        /// </summary>
        public Int32 FeaturePoints { get; set; } // Still need to determine how to calculate these

		#endregion


		/// <summary>
		/// Player overloaded constructor
		/// </summary>
		/// <param name="playerID">The player's ID</param>
		/// <param name="playerName">The player's name</param>
		public Player(Int32 playerID, String playerNameP) {
			PlayerID = playerID;
			playerName = playerNameP;
			FeaturePoints = 0;
			Budget = 1;
			Funds = Budget;
			this.playerResources = new ResourceContainer();
		}


		/// <summary>
		/// A group of functions that are used to Int32eract with Items, i.e. giving players pawn, cards, or checking if player has a certain type of pawn
		/// </summary>
		#region Item Related Methods
		


		


		


		/// <summary>
		/// Increases the budget of this Player instance
		/// </summary>
		/// <param name="additionalBudgetP">The additional budget to give this Player</param>
		public void IncreaseBudget(Int32 additionalBudgetP) {
			this.Budget += additionalBudgetP;
		}


		/// <summary>
		/// Gives player additional funds
		/// </summary>
		/// <param name="fundsToGiveP">The funds to give</param>
		public void GiveFunds(Int32 fundsToGiveP) {
			this.Funds += fundsToGiveP;
		}


		/// <summary>
		/// Creates a List Of Strings representation of the pawns in the player's inventory.
		/// </summary>
		/// <returns>A list of strings containing info on the current pawns in the player's inventory.</returns>
		public List<String> ListPawns() {
			List<String> retList = new List<String>();
			Pawns.ForEach(pawn => retList.Add(pawn.ToString()));
			return retList;
		}
		#endregion


		


	}
}
