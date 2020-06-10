using System;
using System.Collections.Generic;
using System.Text;

namespace ScrumageEngine.Objects {
	/// <summary>
	/// Item Class: Abstract class to inherit from when creating items for the game
	/// </summary>
    public abstract class Item {
		/// <summary>
		/// A Name holder for the item to ensure all created items get a name
		/// </summary>
        public String Name { get; set; }
		/// <summary>
		/// A message/description about the item to display to players for informational purposes
		/// </summary>
        public String Message { get; set; }
		/// <summary>
		/// Overloaded constructor for Item, used to base child objects with
		/// </summary>
		/// <param name="name">The name of the item</param>
		/// <param name="message">The message/Description for the item</param>
        public Item(String name, String message): base() {
            this.Name = name;
            this.Message = message;
        }
		/// <summary>
		/// overrided ToString for children to be required to implement their own version of a ToString
		/// </summary>
		/// <returns>A String representation of the card</returns>
		public abstract override String ToString();
	}
}
