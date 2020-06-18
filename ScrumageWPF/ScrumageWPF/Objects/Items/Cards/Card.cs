﻿using System;
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
    public class Card {

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
        public Card(String nameP, String descriptionP, Int32[] resourcesCosts) {
	        this.cardName = nameP;
	        this.cardDesc = descriptionP;
            this.cardRequirements = new ResourceContainer(resourcesCosts);
        }
        #endregion


        public String Display() {
	        return "Will be abstract";
        }

        public override String ToString() {
	        return "Will be abstract";
        }
        public ResourceContainer GetCardRequirements() {
            return this.cardRequirements;//TODO: Shallow reference?
        }
    }
}
