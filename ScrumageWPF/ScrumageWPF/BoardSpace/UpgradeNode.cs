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

        public override Int32 MaxPawnLimit { get { return 1; } }

        #region Constructors
        public UpgradeNode(Int32 nodeID, String nodeName) : base(nodeID, nodeName) { 

            
        }
        #endregion

        //Assumes does not pass Full Stack
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
