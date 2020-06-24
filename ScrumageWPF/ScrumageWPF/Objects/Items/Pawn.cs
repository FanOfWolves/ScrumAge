using ScrumageEngine.BoardSpace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrumageEngine.Objects.Items {
	/// <summary>
	/// A pawn class that represents a player's piece on the board
	/// </summary>
	public class Pawn {

		#region Properties
		/// <summary>
		/// The pawn's ID will be the same as the owning player's ID, used to determine if a pawn is a players upon a move.
		/// </summary>
		public Int32 PawnID { get; set; }


		/// <summary>
		/// The level of the pawn used to determine if a pawn can do a specific task
		/// </summary>
		public String PawnType {
            get {
                return PawnType;
            }
            set {
                this.PawnType = value;
                PawnCost = CalcCost(value);
            }
        }


        /// <summary>
        /// The cost this pawn requires per turn
        /// </summary>
        public Int32 PawnCost { get; private set; }

        #endregion


		/// <summary>
		/// Default constructor, used to create a default pawn that represents a pawn of a specific type not being found. If this pawn is returned, handle as if no pawn is returned.
		/// </summary>
		public Pawn() {
			PawnID = 0;
			PawnType = "None";
		}


		/// <summary>
		/// Pawn overloaded constructor, used to create pawns owned by an existing player.
		/// </summary>
		/// <param name="id">The ID to match the player's ID</param>
		/// <param name="pawnType">The pawn's level to determine the roles of the pawn</param>
		public Pawn(Int32 id, String pawnType) {
			PawnID = id;
			PawnType = pawnType;
			PawnCost = CalcCost(PawnType);
		}


		/// <summary>
		/// Calculates the required cost per turn for the created pawn. Cost is determined by the type of pawn.
		/// </summary>
		/// <param name="PawnType">The type of pawn</param>
		/// <returns>How much this pawn costs per turn : Int32</returns>
		private Int32 CalcCost(String pawnType) {
            switch (pawnType) {
				case "Full Stack":
                    return 2;
                case "Back End":
                    return 1;
				case "Front End":
                    return 1;
				default:
                    throw new ArgumentException("Invalid pawn type specified.");
            }
		}


		/// <summary>
		/// Represents the pawn as a String
		/// </summary>
		/// <returns>The representative String : String</returns>
		public override String ToString() {
			return $"{PawnType}, {PawnID}";
		}
	}
}