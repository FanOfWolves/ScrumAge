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
        /// <summary>
        /// The resource of this <see cref="ResourceNode"/>.
        /// </summary>
        public readonly Resource nodeResource = null;

        /// <summary>
        /// The base success rate of acquiring a resource from this node.
        /// </summary>
        public const Int32 RESOURCE_BASE_CHANCE = 20;

        /// <summary>
        /// The resource chance calculator. Created by the constructor
        /// </summary>
        public readonly Random resourceChanceCalculator;
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
        /// Rolls for resource.
        /// </summary>
        /// <param name="successChance">The success chance.</param>
        /// <returns>
        ///     <c>true</c> if player is to gain resource; otherwise, <c>false</c>.
        /// </returns>
        private Boolean RollForResource(Int32 successChance) {
            Int32 _result = this.resourceChanceCalculator.Next(1,101);
            return successChance >= _result;
        }

        /// <summary>
        /// Accumulates the resource chances.
        /// </summary>
        /// <param name="playerPawnsP">The player pawns.</param>
        /// <returns>the resource roll chance.</returns>
        public Int32 AccumulateResourceChances(IEnumerable<Pawn> playerPawnsP) {
            Int32 _resourceAcquireChance = RESOURCE_BASE_CHANCE;
            foreach(Pawn _pawn in playerPawnsP) {
                _resourceAcquireChance += this.nodeResource.GetChance(_pawn);
            }
            return _resourceAcquireChance;
        }
        
        /// <summary>
        /// Attempt to gain a resource from this node
        /// </summary>
        /// <param name="playerP">the player attempting to obtain the resource.</param>
        /// <returns>a log indicating if the player acquired the resource or not</returns>
        public override String DoAction(Player playerP) {
            
            List<Pawn> _playerPawns = base.GatherPlayerPawns(playerP.PlayerID);

            if (_playerPawns.Count < 1) {
                return $"{playerP.PlayerName} has failed to obtain a {this.nodeResource.Name}. Reason: No Pawns.";
            }

            Int32 _resourceAcquireChance = AccumulateResourceChances(_playerPawns);
            Boolean _getResource = RollForResource(_resourceAcquireChance);

            ReturnPawnsToPlayer(_playerPawns, playerP);

            if (_getResource) {
                playerP.AddResource(this.nodeResource.DeepCopy());
                return $"{playerP.PlayerName} has acquired a {this.nodeResource.Name}!";
            }
            
            return $"{playerP.PlayerName} has failed to obtain a {this.nodeResource.Name}";
        }
	}
}
