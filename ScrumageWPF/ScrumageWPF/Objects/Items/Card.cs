using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrumageEngine.Objects.Items {
	/// <summary>
	/// Card extends Item, Used to represent a Card within the game
	/// </summary>
	public abstract class Card{

		#region Fields
		/// <summary>
        /// Name of the card, a way to represent which card will be viewed when card info label is selected.
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// The Type of card that is being created
        /// </summary>
        public String CardType { get; set; }

        /// <summary>
        /// The requirements for the player to obtain this card
        /// </summary>
        public List<Resource> cardRequirements { get; set; }
		#endregion


		#region Constructors
		/// <summary>
		/// Card overloaded constructor
		/// </summary>
		/// <param name="type">The type of card being created</param>
		/// <param name="name">The name of the card</param>
		public Card(String type, String name){
			CardType = type;
			// Determine how to create reqs in implementation for game
		}
		#endregion


		#region Class Converters
		/// <summary>
        /// ToString to be used to display the card
        /// </summary>
        /// <returns>String representation of the card</returns>
        public override String ToString() {
            return Name;
        }
        #endregion

	}
}
