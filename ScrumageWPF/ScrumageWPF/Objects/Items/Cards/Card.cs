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

        /// <summary>
        /// Attempt to purchase this card.
        /// </summary>
        /// <param name="payment">The payment offered by the player.</param>
        /// <param name="returnedCard">The returned card.</param>
        /// <returns>
        ///     <c>true</c> if payment was successful; Otherwise, <c>false</c>.
        /// </returns>
        public Boolean TryPayCost(ref ResourceContainer payment, out Card returnedCard) {
            returnedCard = null;
            Resource[] _typesInPayment = payment.GetResourceTypes();


            // Verify payment is enough for all required resources
            foreach(Resource _type in _typesInPayment) {
                Int32 _amountToPay = this.cardRequirements.GetResourceAmount(_type);
                Int32 _amountInPayment = payment.GetResourceAmount(_type);
                if (_amountInPayment >= _amountToPay)
                    continue;
                return false;
            }

            // Complete transaction
            foreach (Resource _type in _typesInPayment) {
                Int32 _amountToPay = this.cardRequirements.GetResourceAmount(_type);
                Int32 _amountInPayment = payment.GetResourceAmount(_type);
                payment.TakeResources(_type, _amountToPay);
            }

            // Give card
            returnedCard = this;
            return true;
        }
        #endregion
    }
}
