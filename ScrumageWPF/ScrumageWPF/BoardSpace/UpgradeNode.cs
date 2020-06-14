using ScrumageEngine.Objects.Humans;
using ScrumageEngine.Objects.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScrumageEngine.BoardSpace
{
    class UpgradeNode : Node
    {
        #region Fields

        #endregion

        #region Constructors
        public UpgradeNode(Int32 nodeID, String nodeName) : base(nodeID, nodeName) { 

            
        }
        #endregion

        public override List<Pawn> DoAction(Int32 playerIdP) {
            List<Pawn> _pawnsToReturn = new List<Pawn>();

            foreach(Pawn pawn in base.Pawns) {
                //!!TODO: Reformat these conditionals
                if(pawn.PawnID != playerIdP) {
                    continue;
                }
                if(pawn.PawnType == "Back End" || pawn.PawnType == "Front End") {
                    pawn.PawnType = "Full Stack";
                }

                _pawnsToReturn.Add(pawn);
                base.Pawns.Remove(pawn);
            }

            _pawnsToReturn.TrimExcess();
            return _pawnsToReturn;
        }
    }
}
