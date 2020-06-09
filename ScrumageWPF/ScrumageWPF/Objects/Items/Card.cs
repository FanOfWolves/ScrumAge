using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoneAgeEngine.Objects.Items {
	/// <summary>
	/// Card extends Item, Used to represent a Card within the game
	/// </summary>
	public class Card : Item{
		/// <summary>
		/// The Type of card that is being created
		/// </summary>
		public String CardType { get; set; }
		/// <summary>
		/// The requirements for the player to obtain this card
		/// </summary>
		public List<Item> Requirements { get; set; }
		/// <summary>
		/// Card overloaded constructor
		/// </summary>
		/// <param name="type">The type of card being created</param>
		/// <param name="name">The name of the card</param>
		/// <param name="message">The text for the card</param>
		public Card(String type, String name, String message) : base(name, message) {
			CardType = type;
			Name = name;
			Message = message;
			// Determine how to create reqs in implementation for game
		}
		/// <summary>
		/// ToString to be used to display the card
		/// </summary>
		/// <returns>String representation of the card</returns>
		public override String ToString() {
			return "Implement this ToString!!";
		}
	}
}
