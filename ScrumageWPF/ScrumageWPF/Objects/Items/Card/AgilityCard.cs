using System;

namespace ScrumageEngine.Objects.Items.Cards {
    /// <summary>
    /// A card depicting the handling of change in requirements.
    /// </summary>
    /// <seealso cref="Card" />
    public class AgilityCard : Card {

        #region Fields        

        /// <summary>
        /// The immediate bonus for obtaining this card
        /// </summary>
        public Int32 ImmediateBonus { get; set; } = 0;//TODO: Replace with ImmediateBonus class?
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="AgilityCard"/> class.
        /// </summary>
        /// <param name="nameP">The card name.</param>
        /// <param name="descP">The card description.</param>
        /// <param name="costs">The resource costs of this card.</param>
        public AgilityCard(String nameP, Int32[] costs) : base(nameP, costs) {
            this.ImmediateBonus = 0;
        }
        #endregion
	}
}
