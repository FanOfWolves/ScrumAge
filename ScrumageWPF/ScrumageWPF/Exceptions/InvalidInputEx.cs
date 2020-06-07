using System;
using System.Collections.Generic;
using System.Text;

namespace StoneAgeEngine.Exceptions {
	/// <summary>
	/// An exception that gets thrown whenever the user gives an invalid input.
	/// </summary>
	class InvalidInputEx : Exception {
		/// <summary>
		/// The message that is outputted to the user.
		/// </summary>
		public new String Message { get; set; }
		/// <summary>
		/// Overloaded Constructor, brings in the pre-split player input.
		/// </summary>
		/// <param name="inputArr">The user's input split into an array by each part of the input.</param>
		public InvalidInputEx(String[] inputArr) {
			Message = GetMessage(inputArr);
		}

		/// <summary>
		/// Determines what message needs to be outputted by the user based on the user's input.
		/// </summary>
		/// <param name="inputArr">The array of the user's input.</param>
		/// <returns>The message to be outputted to the user.</returns>
		public String GetMessage(String[] inputArr) {
			return "Implement how to generate this message";
		}
	}
}
