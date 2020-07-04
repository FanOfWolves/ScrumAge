using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Navigation;
using ScrumageEngine.Objects;
using ScrumageEngine.Objects.Player;
using ScrumageEngine.Objects.Items;

namespace ScrumageEngine.BoardSpace {
	/// <summary>
	/// A Node is an area that can hold pawns outside of the player's inventory
	/// </summary>
	public abstract class Node {
		/// <summary>
		/// The ID to use in identifying a node
		/// </summary>
		public Int32 NodeID { get; set; } = 0;

		/// <summary>
		/// A name to be used to display the node
		/// </summary>
		public String NodeName { get; set; } = "";

		/// <summary>
		/// The list of pawns that can be held in the node
		/// </summary>
		public List<Pawn> Pawns { get; set; }

		public Int32 NumberOfPawns { get { return this.Pawns.Count; } }


		private Int32 maxPawnLimit = 7;
		public virtual Int32 MaxPawnLimit { get { return maxPawnLimit; } }

		/// <summary>
		/// Ats the maximum pawns.
		/// </summary>
		/// <returns></returns>
		public Boolean AtMaxPawns() {
			if(Pawns.Count >= MaxPawnLimit) {
				return true;
			}
			return false;
		}

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
		public Node(Int32 nodeID, String nodeName) : base() {
			NodeID = nodeID;
			NodeName = nodeName;
			Pawns = new List<Pawn>();
		}

        /// <summary>
        /// Gathers the player pawns from this node.
        /// </summary>
        /// <param name="playerIdP">The player identifier.</param>
        /// <returns>the player's pawns from this node.</returns>
		public List<Pawn> GatherPlayerPawns(Int32 playerIdP) {
            List<Pawn> _playerPawns = Pawns.FindAll(_playerPawn => _playerPawn.PawnID == playerIdP);
            Pawns.RemoveAll(_playerPawn => _playerPawn.PawnID == playerIdP);
            return _playerPawns;
		}

        /// <summary>
        /// Returns the pawns (if any) to player.
        /// </summary>
        /// <param name="pawnsToReturnP">The pawns to return.</param>
        /// <param name="playerP">The player.</param>
        public void ReturnPawnsToPlayer(List<Pawn> pawnsToReturnP, Player playerP) {
            foreach(Pawn _pawn in pawnsToReturnP) {
                playerP.GivePawn(_pawn);
            }
        }

		/// <summary>
		/// Takes a pawn from a node, requires both the pawn level and the owning player's ID
		/// </summary>
		/// <param name="PawnType">The level of the pawn</param>
		/// <param name="pawnID">The ID used to pair the pawn with the player</param>
		/// <returns>returns the pawn requested</returns>
		public Pawn TakePawnFromNode(String PawnType, Int32 pawnID) {
			Pawn retPawn = new Pawn();
			foreach (Pawn i in Pawns) {
				if (i.PawnType.ToLower().Replace(" ", "").Equals(PawnType) && i.PawnID == pawnID) {
					retPawn = i;
					break;
				} else if (i.PawnType.ToLower().Replace(" ", "").Contains(PawnType) && i.PawnID == pawnID) {
					retPawn = i;
					break;
				}
			}
			RemovePawn(retPawn);
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
		/// <param name="PawnType">The level of the pawn to be found</param>
		/// <param name="pawnID">The player's ID to be paired with the pawn.</param>
		/// <returns>True/false depending on if the Node has the pawn</returns>
		public Boolean HasPawn(String PawnType, Int32 pawnID) {
			Boolean hasPawn = false;
			PawnType = PawnType.ToLower();
			foreach (Pawn pawn in Pawns) {
				if (pawn.PawnType.ToLower().Replace(" ", "").Equals(PawnType) && pawn.PawnID == pawnID) {
					hasPawn = true;
					break;
				} else if (pawn.PawnType.ToLower().Replace(" ", "").Contains(PawnType) && pawn.PawnID == pawnID) {
					hasPawn = true;
					break;
				} else {
					continue;
				}
			}
			return hasPawn;
		}


		/// <summary>
		/// Determines of a pawn exists within the node.
		/// </summary>
		/// <param name="PawnType">The level of the pawn to be found</param>
		/// <param name="pawnID">The player's ID to be paired with the pawn.</param>
		/// <returns>True/false depending on if the Node has the pawn</returns>
		public Boolean HasPawn(Int32 pawnID) {
			Boolean hasPawn = false;
			foreach(Pawn pawn in Pawns) {
				if(pawn.PawnID == pawnID) {
					hasPawn = true;
					break;
				} else if(pawn.PawnID == pawnID) {
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
		public void RemovePawn(Pawn pawn) {
			Pawns.Remove(pawn);
		}
		/// <summary>
		/// List pawns for current node
		/// </summary>
		/// <returns>List of pawn types</returns>
		public List<String> ListPawns() {
			List<String> retList = new List<String>();
			Pawns.ForEach(pawn => retList.Add(pawn.ToString()));
			return retList;
		}

		/// <summary>
		/// Action that the node runs every phase
		/// </summary>
		/// <returns></returns>
		public abstract String DoAction(Player player);
	}
}
