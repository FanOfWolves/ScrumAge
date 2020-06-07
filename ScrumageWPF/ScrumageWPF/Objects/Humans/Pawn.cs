using StoneAgeEngine.MapSpace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoneAgeEngine.Objects.Humans {
	/// <summary>
	/// A pawn class that represents a player's piece on the board
	/// </summary>
	class Pawn {
		/// <summary>
		/// The pawn's ID will be the same as the owning player's ID, used to determine if a pawn is a players upon a move.
		/// </summary>
		public int PawnID { get; set; }
		/// <summary>
		/// The level of the pawn used to determine if a pawn can do a specific task
		/// </summary>
		public String PawnLevel { get; set; }
		/// <summary>
		/// The cost this pawn requires per turn
		/// </summary>
		public int Cost { get; set; }
		/// <summary>
		/// Default constructor, used to create a default pawn that represents a pawn of a specific type not being found. If this pawn is returned, handle as if no pawn is returned.
		/// </summary>
		public Pawn() {
			PawnID = 0;
			PawnLevel = "None";
		}
		/// <summary>
		/// Pawn overloaded constructor, used to create pawns owned by an existing player.
		/// </summary>
		/// <param name="id">The ID to match the player's ID</param>
		/// <param name="pawnLevel">The pawn's level to determine the roles of the pawn</param>
		public Pawn(int id, String pawnLevel) {
			PawnID = id;
			PawnLevel = pawnLevel;
			Cost = CalcCost(PawnLevel);
		}
		/// <summary>
		/// Calculates the required cost per turn for the created pawn. Cost is determined by the type of pawn.
		/// </summary>
		/// <param name="pawnLevel">The type of pawn</param>
		/// <returns>How much this pawn costs per turn : int</returns>
		private int CalcCost(String pawnLevel) {
			return 0;
		}
		/// <summary>
		/// Represents the pawn as a String
		/// </summary>
		/// <returns>The representative String : String</returns>
		public override String ToString() {
			return $"{PawnLevel}, {PawnID}";
		}
	}
}