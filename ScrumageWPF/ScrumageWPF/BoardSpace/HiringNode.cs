using ScrumageEngine.Objects.Humans;
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

        public override String DoAction(Player playerP) {
            int playerID = playerP.PlayerID;
            foreach(Pawn pawn in base.Pawns) {
                if(pawn.PawnID != playerID) continue;
                
            }
            return "";

        }
	}
}
