using ScrumageEngine.Objects.Player;
using System;
using System.Collections.Generic;
using System.Text;
using ScrumageEngine.Objects.Items;

namespace ScrumageEngine.BoardSpace {
    /// <summary>
    /// Provides temporary funds to Players
    /// </summary>
    /// <seealso cref="ScrumageEngine.BoardSpace.Node" />
    class ReassignmentNode : Node {

        public override Int32 MaxPawnLimit { get { return 500; } }


        /// <summary>
        /// Instantiates a new instance of the <see cref = "ReassignmentNode"/> class
        /// </summary>
        /// <param name="nodeIDP">the node ID</param>
        /// <param name="nodeNameP">the name of the node</param>
        #region Constructors
        public ReassignmentNode(Int32 nodeIDP, String nodeNameP):base(nodeIDP, nodeNameP) {
            
        }
        #endregion
        /// <summary>
        /// Gives funds to a player per number of their pawns in the node.
        /// </summary>
        /// <param name="playerP">The player to give funds.</param>
        /// <returns>the message log denoting number of funds acquired and which player received them</returns>
        public override String DoAction(Player playerP) {
            Int32 _playerID = playerP.PlayerID;
            List<Pawn> _playerPawns = GatherPlayerPawns(_playerID);

            if (_playerPawns.Count < 1) {
                return $"{playerP.PlayerName} Failed to increase their funds. Reason: No Pawns.";
            }

            Int32 _numberOfFunds = _playerPawns.Count;
            playerP.GiveFunds(_numberOfFunds);

            ReturnPawnsToPlayer(_playerPawns, playerP);

            return $"{playerP.PlayerName} has received {_numberOfFunds} additional funds from reassignment!";
        }
    }
}
