using ScrumageEngine.Objects.Humans;
using ScrumageEngine.Objects.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScrumageEngine.BoardSpace
{
    /// <summary>
    /// Upgrades pawns to Full Stack type
    /// </summary>
    /// <seealso cref="ScrumageEngine.BoardSpace.Node" />
    class UpgradeNode : Node
    {
        #region Fields

        #endregion


        #region Constructors
        public UpgradeNode(Int32 nodeID, String nodeName) : base(nodeID, nodeName) { 

            
        }

        /// <summary>
        /// Default Constructor: Initializes a new instance of the <see cref="UpgradeNode"/> class.
        /// </summary>
        public UpgradeNode() {

		}
        #endregion

        /// <summary>
        /// Upgrades a pawn and returns the upgraded pawn to the owning player.
        /// </summary>
        /// <param name="playerP">The player that owns the pawn.</param>
        /// <returns>A string to be logged in the Sprint Log.</returns>
        public override String DoAction(Player playerP) {
            Int32 _playerID = playerP.PlayerID;
           
            for(Int32 i = 0; i < base.Pawns.Count; i++) {
                
                //!!TODO: Reformat these conditionals
                if(base.Pawns[i].PawnID != _playerID) {
                    continue;
                }
                if(base.Pawns[i].PawnType == "Back End" || base.Pawns[i].PawnType == "Front End") {
                    base.Pawns[i].PawnType = "Full Stack";
                    playerP.GivePawn(base.Pawns[i]);
                    base.Pawns.Remove(base.Pawns[i]);
                    return $"{playerP.PlayerName} retrieved Full Stack pawn from Technical Hut";
                }
            }
            return $"{playerP.PlayerName} failed to upgrade a pawn";
        }
    }
}
