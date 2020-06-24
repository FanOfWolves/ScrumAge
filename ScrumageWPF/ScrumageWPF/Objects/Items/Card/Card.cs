using System;
//using ScrumageEngine.Objects.Player;

namespace ScrumageEngine.Objects.Items.Cards {
    /// <summary>
    /// Abstract class representing cards
    /// </summary>
    public abstract class Card {

        #region Fields

        private ResourceContainer cardRequirements;
        protected String cardName;
        protected String cardFormat;
        protected Int32 cardEndGameBonus;//TODO: Replace with CardBonus class?

        #endregion

        #region Constructors        
        /// <summary>
        /// Initializes a new instance of the <see cref="Card"/> class.
        /// </summary>
        public Card(String nameP, Int32[] resourcesCosts) {
	        this.cardName = nameP;
            this.cardRequirements = new ResourceContainer(resourcesCosts);
        }
		#endregion

		#region Display
		/// <summary>
		/// Not yet implemented
		/// </summary>
		/// <returns></returns>
		public abstract String Display();
        /// <summary>
        /// Not yet implemented
        /// </summary>
        /// <returns></returns>
        public override string ToString() {
            return $"{this.GetType().Name}: {cardName}\n----------------------\n{this.GetCardRequirements().ShowRequirements()}";
        }
        #endregion

        #region Getters
        /// <summary>
        /// Not yet implemented
        /// </summary>
        /// <returns></returns>
        public ResourceContainer GetCardRequirements() {
            return this.cardRequirements;//TODO: Shallow reference?
        }

		internal abstract string CardType();
		#endregion
	}
}
