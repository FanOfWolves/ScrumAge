using System;
using System.Collections.Generic;
using System.Text;

namespace ScrumageEngine.Objects.Items.Cards {
    /// <summary>
    /// A card depicting Software Development Process stage artifacts.
    /// </summary>
    /// <seealso cref="ScrumageEngine.Objects.Items.Cards.Card" />
    class ArtifactCard : Card {

        /// <summary>
        /// Initializes a new instance of the <see cref="ArtifactCard"/> class.
        /// </summary>
        /// <param name="nameP">The name of this card</param>
        /// <param name="descP">The description of this card</param>
        /// <param name="costs">The resource costs of this card</param>
        public ArtifactCard(String nameP, String descP, Int32[] costs) : base(nameP, descP, costs) {

        }

    }
}
