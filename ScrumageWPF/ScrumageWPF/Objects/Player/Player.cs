using System;
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
		#region Properties
		/// <summary>
		/// An ID to be used for the player for easier identification than their name
		/// </summary>
		public Int32 PlayerID { get; set; }

        /// <summary>
        /// The entered name for the player.
        /// </summary>
		public String PlayerName { get; }

        /// <summary>
        /// A tracker for if the player has finished the current phase
        /// </summary>
		public Boolean FinishedPhase { get; set; }

		#endregion


		#region Constructor
		/// <summary>
		/// Player overloaded constructor
		/// </summary>
		/// <param name="playerID">The player's ID.</param>
		/// <param name="playerNameP">The player's name.</param>
		public Player(Int32 playerID, String playerNameP) {
            PlayerID = playerID;
            PlayerName = playerNameP;
            FeaturePoints = 0;
            Budget = 1;
            Funds = Budget;
            this.playerResources = new ResourceContainer(new Int32[] { 0, 0, 0, 0 });
            FinishedPhase = false;
        }
		#endregion

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
        /// Creates a List Of Strings representation of the pawns in the player's inventory.
        /// </summary>
        /// <returns>A list of strings containing info on the current pawns in the player's inventory.</returns>
        public List<String> ListPawns() {
            List<String> retList = new List<String>();
            Pawns.ForEach(pawn => retList.Add(pawn.ToString()));
            return retList;
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
        /// <param name="pawnType">The pawn level to be added</param>
        public void GivePawn(String pawnType) {
            this.Pawns.Add(new Pawn(PlayerID, pawnType));
        }

        /// <summary>
        /// Takes a pawn from the player based on a pawn level
        /// </summary>
        /// <param name="pawnType">The level of the pawn to be taken</param>
        /// <returns></returns>
        public Pawn TakePawn(String pawnType) {
            Pawn retPawn = new Pawn();
            if((retPawn = HasPawn(pawnType)) != new Pawn()) {
                Pawns.Remove(retPawn);
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
                }
                else if(pawn.PawnType.Contains(PawnType)) {
                    retPawn = pawn;
                    break;
                }
                else {
                    continue;
                }
            }
            return retPawn;
        }

        #endregion

        #region Cards
        /// <summary>
        /// A list of the player's User Story cards to hold obtained cards
        /// </summary>
        public List<Card> Artifacts = new List<Card>();

		/// <summary>
		/// A list of the player's Feature cards to hold obtained cards
		/// </summary>
		public List<Card> Agility = new List<Card>();

		/// <summary>
		/// Adds to player's cards
		/// </summary>
		/// <param name="card">The card to be added</param>
		public void AddToCards(Card card) {
            if(card.GetType() == typeof(AgilityCard)) {
                this.Agility.Add(card);
            }
            this.Artifacts.Add(card);
        }

        /// <summary>
        /// Removes a card from the player's User Stories inventory
        /// </summary>
        /// <param name="artifactP">The artifact card to be removed</param>
        public void RemoveFromArtifacts(Card artifactP) {
            this.Artifacts.Remove(artifactP);
        }

        /// <summary>
        /// Removes a Feature card from the Features inventory
        /// </summary>
        /// <param name="agilityP">The agility card to be removed</param>
        public void RemoveFromAgility(Card agilityP) {
            this.Agility.Remove(agilityP);
        }
        #endregion

        #region Resources        
        /// <summary>
        /// The player resources. <see cref="ResourceContainer"/>.
        /// </summary>
        public ResourceContainer playerResources;

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

        /// <summary>
        /// Get the resource container of the player
        /// </summary>
        /// <returns>the resource container of the player</returns>
        public ResourceContainer GetPlayerResources() {
            return this.playerResources;
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
        /// Adds the budget to funds.
        /// </summary>
        private void AddBudgetToFunds() {
            this.Funds += this.Budget;
        }

        /// <summary>
        /// Pays the pawns.
        /// </summary>
        /// <returns>
        ///     <c>true</c> if player was able to pay their pawns; otherwise <c>false</c>.
        /// </returns>
        public Boolean PayPawns() {
            AddBudgetToFunds();
            this.FinishedPhase = true;
            for (Int32 i = this.Pawns.Count-1; i > 0; i--) {
                if (this.Funds <= 0) {
                    this.Pawns.RemoveAt(i);
                }
                else if (this.Pawns.Count <= 2) {
                    this.FeaturePoints -= 10;
                    this.Funds = 0; //reset funds
                    return false;
                }
                this.Funds -= this.Pawns[i].PawnCost;
            }

            if (this.Funds < 0) this.Funds = 0;
            return true;
        }

        #endregion


    }
}
