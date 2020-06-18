using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Navigation;
using ScrumageEngine.Objects.Player;

namespace ScrumageEngine.Objects.Items.Cards {
    /// <summary>
    /// Abstract class representing cards
    /// </summary>
    abstract class Card {

        #region Fields

        private ResourceContainer cardRequirements;
        private String cardDesc;
        private String cardName;
        private String cardFormat;
        private Int32 cardEndGameBonus;//TODO: Replace with CardBonus class?

        #endregion

        #region Constructors        
        /// <summary>
        /// Initializes a new instance of the <see cref="Card"/> class.
        /// </summary>
        public Card(ResourceContainer cardRequirementsP) {
            this.cardRequirements = cardRequirementsP;
        }
        #endregion

        #region Methods

        public abstract String Display();
        public abstract override String ToString();
        public ResourceContainer GetCardRequirements() {
            return this.cardRequirements;//TODO: Shallow reference?
        }

        public Boolean TryPayCost(ResourceContainer payment) {
            for (Int32 i = 0; i < 4; i++) {
                Int32 _amountHere = this.cardRequirements.GetResourceAmount()
            }
        }
        #endregion
    }
}
