using System;

namespace ScrumageEngine.Objects.Items.Cards {
    /// <summary>
    /// Abstract class representing cards
    /// </summary>
    public abstract class Card {

        #region Fields

        public ResourceContainer CardRequirements { get; set; }
        public String CardName { get; set; }
        public String CardFormat { get; set; }
        public Int32 CardEndGameBonus { get; set; }//TODO: Replace with CardBonus class?

        #endregion

        #region Constructors        
        /// <summary>
        /// Initializes a new instance of the <see cref="Card"/> class.
        /// </summary>
        public Card(String nameP, Int32[] resourcesCosts) {
	        this.CardName = nameP;
            this.CardRequirements = new ResourceContainer(resourcesCosts);
        }
		#endregion

		#region Display
        /// <summary>
        /// Not yet implemented
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            return $"{this.GetType().Name}: {CardName}\n----------------------\n{CardRequirements.ShowRequirements()}";
        }
        #endregion

        #region Getters
        internal String CardType() {
            return this.GetType().Name;
        }
		#endregion
	}
}
