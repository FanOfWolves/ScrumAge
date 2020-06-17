using System;
using System.Collections.Generic;
using System.Text;
using ScrumageEngine.Objects.Player;
using ScrumageEngine.Objects.Items;

namespace ScrumageEngine.BoardSpace {
    public class ResourceNode : Node
    {
        //D:

        private Resource nodeResource = null;
        private const Int32 RESOURCE_BASE_CHANCE = 20;
        private readonly Random resourceChanceCalculator;

        public ResourceNode(Int32 nodeID, String nodeName, Resource nodeResource) : base(nodeID, nodeName) {
            this.nodeResource = nodeResource;
            this.resourceChanceCalculator = new Random();   
        }

        private List<Pawn> GatherPlayerPawns(Int32 playerId) {
            List<Pawn> _playerPawns = Pawns.FindAll(_playerPawn => _playerPawn.PawnID == playerId);
            Pawns.RemoveAll(_playerPawn => _playerPawn.PawnID == playerId);
            return _playerPawns;
        }

        private Boolean RollForResource(Int32 successChance) {
            Int32 _result = this.resourceChanceCalculator.Next(0,101);
            return successChance <= _result;
        }


        public override String DoAction(Player player) {
            List<Pawn> _playerPawns = GatherPlayerPawns(player.PlayerID);
            
            Int32 _resourceAcquireChance = RESOURCE_BASE_CHANCE;
            foreach (Pawn pawn in _playerPawns) {
                _resourceAcquireChance += nodeResource.GetChance(pawn);
                player.GivePawn(pawn);
            }

            Boolean _getResource = RollForResource(_resourceAcquireChance);
            if (_getResource == true) {
                player.AddResource();
                //returns the did they do the do thing
                //player.acquireResource();

            }
            else {
                
            }

            return ""; // This gets added to log
		}
	}
}
