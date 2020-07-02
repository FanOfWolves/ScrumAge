using System;
using System.Collections.Generic;
using ScrumageEngine.Objects.Items.Cards;

namespace ScrumageWPF.Test.Utilities {

    /// <summary>
    /// Used for comparing <see cref="Card"/> for testing purposes.
    /// </summary>
    /// <seealso cref="System.Collections.Generic.IEqualityComparer{ScrumageEngine.Objects.Items.Cards.Card}" />
    internal class TestCardEqualityCompare : IEqualityComparer<Card> {
        
        /// <summary>
        /// Checks if card1 and card2 are the same for testing purposes.
        /// </summary>
        /// <param name="card1">The card1.</param>
        /// <param name="card2">The card2.</param>
        /// <returns>
        ///     <c>true</c> if they are the same; Otherwise, <c>false</c>.
        /// </returns>
        Boolean IEqualityComparer<Card>.Equals(Card card1, Card card2) {
            if(card1.CardName != card2.CardName)
                return false;
            if(card1.GetType() != card2.GetType())
                return false;
            if(new EqualityCompareTestResourceContainers().Equals(card1.CardRequirements, card2.CardRequirements) == false)
                return false;
            return true;
        }

        /// <summary>
        /// Returns a hash code for the specified object.
        /// </summary>
        /// <param name="obj">The <see cref="T:System.Object" /> for which a hash code is to be returned.</param>
        /// <returns>
        /// A hash code for the specified object.
        /// </returns>
        /// <exception cref="NotImplementedException">not implemented.</exception>
        Int32 IEqualityComparer<Card>.GetHashCode(Card obj) => throw new NotImplementedException();
    }
}
