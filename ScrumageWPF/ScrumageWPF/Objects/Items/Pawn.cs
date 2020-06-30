using ScrumageEngine.BoardSpace;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrumageEngine.Objects.Items {
	/// <summary>
	/// A pawn class that represents a player's piece on the board
	/// </summary>
	public class Pawn : IEquatable<Pawn> {

		#region Properties
		/// <summary>
		/// The pawn's ID will be the same as the owning player's ID, used to determine if a pawn is a players upon a move.
		/// </summary>
		public Int32 PawnID { get; }


        /// <summary>
        /// The level of the pawn used to determine if a pawn can do a specific task
        /// </summary>
        private String pawnType;
		public String PawnType {
            get {
                return pawnType;
            }
            set {
                this.pawnType = value;
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
                    return 0;
            }
		}


		/// <summary>
		/// Represents the pawn as a String
		/// </summary>
		/// <returns>The representative String : String</returns>
		public override String ToString() {
			return $"{PawnType}, {PawnID}";
		}

		/// <summary>
		/// Compares the current instance of a Pawn with a generic object.
		/// </summary>
		/// <param name="obj">The object to compare.</param>
		/// <returns>True if same/false if different.</returns>
		public override Boolean Equals(object obj) {
			return base.Equals(obj);
		}

		/// <summary>
		/// Returns the HashCode for the current instance of the Pawn.
		/// </summary>
		/// <returns>Hash Code for the Pawn.</returns>
		public override int GetHashCode() {
			return base.GetHashCode();
		}

		/// <summary>
		/// Compare current instance of Pawn with another Pawn
		/// </summary>
		/// <param name="other">Another Pawn to be compared to</param>
		/// <returns>true if same/false if different.</returns>
		public Boolean Equals([AllowNull] Pawn other) {
			if(this.PawnID == other.PawnID && this.PawnType == other.PawnType)
				return true;
			else return false;
		}
	}
}