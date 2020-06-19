using System;
using System.Collections.Generic;
using System.Text;
using ScrumageEngine.Objects.Player;
using ScrumageEngine.Objects.Items;

namespace ScrumageEngine.BoardSpace {
    /// <summary>
    /// Node to obtain resources
    /// </summary>
    /// <seealso cref="ScrumageEngine.BoardSpace.Node" />
    public class ResourceNode : Node
    {
        #region Fields
        private readonly Resource nodeResource = null;
        private const Int32 RESOURCE_BASE_CHANCE = 20;
        private readonly Random resourceChanceCalculator;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceNode"/> class.
        /// </summary>
        /// <param name="nodeID">The node's identifier.</param>
        /// <param name="nodeName">The node's name.</param>
        /// <param name="nodeResource">The node's resource.</param>
        public ResourceNode(Int32 nodeID, String nodeName, Resource nodeResource) : base(nodeID, nodeName) {
            this.nodeResource = nodeResource;
            this.resourceChanceCalculator = new Random();
        }
        #endregion


        /// <summary>
        /// Gathers the player pawns from this node.
        /// </summary>
        /// <param name="playerId">The player identifier.</param>
        /// <returns>the player's pawns from this node</returns>
        private List<Pawn> GatherPlayerPawns(Int32 playerId) {
            List<Pawn> _playerPawns = Pawns.FindAll(_playerPawn => _playerPawn.PawnID == playerId);
            Pawns.RemoveAll(_playerPawn => _playerPawn.PawnID == playerId);
            return _playerPawns;
        }

        /// <summary>
        /// Rolls for resource.
        /// </summary>
        /// <param name="successChance">The success chance.</param>
        /// <returns>
        ///     <c>true</c> if player is to gain resource; otherwise, <c>false</c>.
        /// </returns>
        private Boolean RollForResource(Int32 successChance) {
            Int32 _result = this.resourceChanceCalculator.Next(0,101);
            return successChance <= _result;
        }

        /// <summary>
        /// Attempt to gain a resource from this node
        /// </summary>
        /// <param name="player">the player attempting to obtain the resource</param>
        /// <returns>a log indicating if the player acquired the resource or not</returns>
        public override String DoAction(Player player) {
            List<Pawn> _playerPawns = GatherPlayerPawns(player.PlayerID);
            
            Int32 _resourceAcquireChance = RESOURCE_BASE_CHANCE;
            foreach (Pawn _pawn in _playerPawns) {
                _resourceAcquireChance += this.nodeResource.GetChance(_pawn);
                player.GivePawn(_pawn);
            }

            Boolean _getResource = RollForResource(_resourceAcquireChance);
            if (_getResource == true) {
                player.AddResource(this.nodeResource.DeepCopy());
                return $"{player.PlayerName} has acquired a {this.nodeResource.Name}!";
            }
            else {
                return $"{player.PlayerName} has failed to obtain a {this.nodeResource.Name}";
            }
		}
	}
}
