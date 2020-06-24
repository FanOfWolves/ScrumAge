﻿using System;

namespace ScrumageEngine.Objects.Items.Cards {
    /// <summary>
    /// A card depicting Software Development Process stage artifacts.
    /// </summary>
    /// <seealso cref="Card" />
    class ArtifactCard : Card {

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ArtifactCard"/> class.
        /// </summary>
        /// <param name="nameP">The name of this card</param>
        /// <param name="descP">The description of this card</param>
        /// <param name="costs">The resource costs of this card</param>
        public ArtifactCard(String nameP, Int32[] costs) : base(nameP, costs) {

        }


        #endregion

        public override String Display() {
            throw new NotImplementedException();
        }

		internal override String CardType() {
            return "Artifact";
		}

		/*        public override string ToString() {
					throw new NotImplementedException();
				}*/
	}
}
