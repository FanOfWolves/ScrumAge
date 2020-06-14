using ScrumageEngine.Objects.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScrumageEngine.BoardSpace {
    public class HiringNode: Node {


        public HiringNode(Int32 nodeID, String nodeName): base(nodeID, nodeName) {
            
        }


      /*  private Pawn HirePawn(Int32 playerIdP) {
            Int32 randomNumber = 51;
			if(_result) {

			}
        }*/

        public override List<Pawn> DoAction(Int32 playerIdP) {
            List<Pawn> _pawnsToReturn = new List<Pawn>();
            
            foreach(Pawn pawn in base.Pawns) {
                if(pawn.PawnID != playerIdP) continue;
                
            }

            _pawnsToReturn.TrimExcess();
            return _pawnsToReturn;
        }
    }
}
