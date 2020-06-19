using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Navigation;

namespace ScrumageEngine.Objects.Items.Cards {
    /// <summary>
    /// A card depicting the handling of change in requirements.
    /// </summary>
    /// <seealso cref="ScrumageEngine.Objects.Items.Cards.Card" />
    class AgilityCard : Card {

        #region Fields        
        /// <summary>
        /// The card level. Determines resource costs and benefits.
        /// </summary>
        private Int32 cardLevel = 0;

        /// <summary>
        /// The immediate bonus for obtaining this card
        /// </summary>
        private Int32 ImmediateBonus = 0;//TODO: Replace with ImmediateBonus class?
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="AgilityCard"/> class.
        /// </summary>
        /// <param name="nameP">The card name.</param>
        /// <param name="descP">The card description.</param>
        /// <param name="costs">The resource costs of this card.</param>
        public AgilityCard(String nameP, String descP, Int32[] costs) : base(nameP, descP, costs) {
            this.cardLevel = 1;
            this.ImmediateBonus = 0;
        }
        #endregion
    }
}
