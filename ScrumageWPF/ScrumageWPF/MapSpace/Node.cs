using System;
using System.Collections.Generic;
using System.Text;
using StoneAgeEngine.Objects;
using StoneAgeEngine.Objects.Humans;

namespace StoneAgeEngine.MapSpace {
	/// <summary>
	/// A Node is an area that can hold pawns outside of the player's inventory
	/// </summary>
	public class Node {
		/// <summary>
		/// The ID to use in identifying a node
		/// </summary>
		public int NodeID { get; set; } = 0;
		/// <summary>
		/// A name to be used to display the node
		/// </summary>
		public String NodeName { get; set; } = "";
		/// <summary>
		/// A description to be used to describe the node if needed
		/// </summary>
		public String NodeDesc { get; set; } = "";
		/// <summary>
		/// The list of pawns that can be held in the node
		/// </summary>
		public List<Pawn> Pawns { get; set; } = new List<Pawn>();
		/// <summary>
		/// Node default constructor, does not assign any values(can likely be scrapped)
		/// </summary>
		public Node() {
			//Just used as an initializer to avoid null pointers
		}
		/// <summary>
		/// Node Overloaded constructor, creates an instance of the Node
		/// </summary>
		/// <param name="nodeID">The ID used to identify the node</param>
		/// <param name="nodeName">The name of the node</param>
		/// <param name="nodeDesc">The node's description</param>
		public Node(int nodeID, String nodeName, String nodeDesc) : base() {
			NodeID = nodeID;
			NodeName = nodeName;
			NodeDesc = nodeDesc;
			Pawns = new List<Pawn>();
		}
		/// <summary>
		/// Takes a pawn from a node, requires both the pawn level and the owning player's ID
		/// </summary>
		/// <param name="pawnLevel">The level of the pawn</param>
		/// <param name="pawnID">The ID used to pair the pawn with the player</param>
		/// <returns>returns the pawn requested</returns>
		public Pawn TakePawnFromNode(String pawnLevel, int pawnID) {
			Pawn retPawn = null;
			foreach (Pawn i in Pawns) {
				if (i.PawnLevel.ToLower().Replace(" ", "").Equals(pawnLevel) && i.PawnID == pawnID) {
					retPawn = i;
					break;
				} else if (i.PawnLevel.ToLower().Replace(" ", "").Contains(pawnLevel) && i.PawnID == pawnID) {
					retPawn = i;
					break;
				}
			}
			return retPawn;
		}
		/// <summary>
		/// Adds a pawn to the node's inventory
		/// </summary>
		/// <param name="pawn">The pawn to be added</param>
		public void AddPawn(Pawn pawn) {
			Pawns.Add(pawn);
		}
		/// <summary>
		/// Determines of a pawn exists within the node.
		/// </summary>
		/// <param name="pawnLevel">The level of the pawn to be found</param>
		/// <param name="pawnID">The player's ID to be paired with the pawn.</param>
		/// <returns>True/false depending on if the Node has the pawn</returns>
		public bool HasPawn(String pawnLevel, int pawnID) {
			bool hasPawn = false;
			pawnLevel = pawnLevel.ToLower();
			foreach (Pawn pawn in Pawns) {
				if (pawn.PawnLevel.ToLower().Replace(" ", "").Equals(pawnLevel) && pawn.PawnID == pawnID) {
					hasPawn = true;
					break;
				} else if (pawn.PawnLevel.ToLower().Replace(" ", "").Contains(pawnLevel) && pawn.PawnID == pawnID) {
					hasPawn = true;
					break;
				} else {
					continue;
				}
			}
			return hasPawn;
		}
		/// <summary>
		/// Removes a pawn from the node's inventory.
		/// </summary>
		/// <param name="pawn">The pawn to be removed</param>
		public void RemovPawn(Pawn pawn) {
			Pawns.Remove(pawn);
		}
	}
}
