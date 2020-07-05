using ScrumageEngine.Objects.Player;
using ScrumageEngine.Objects.Items;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Channels;

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
        /// <summary>
        /// UpgradeNode constructor with base from parent Node class.
        /// </summary>
        /// <param name="nodeID">The node's ID.</param>
        /// <param name="nodeName">The node's name.</param>
        public UpgradeNode(Int32 nodeID, String nodeName) : base(nodeID, nodeName) { 

            
        }
        #endregion



        /// <summary>
        /// Upgrades a pawn and returns the upgraded pawn to the owning player.
        /// </summary>
        /// <param name="playerP">The player that owns the pawn.</param>
        /// <returns>A string to be logged in the Sprint Log.</returns>
        public override String DoAction(Player playerP) {
            Int32 _playerID = playerP.PlayerID;
            List<Pawn> _playerPawns = GatherPlayerPawns(_playerID);

            if (_playerPawns.Count < 1) {
                return $"{playerP.PlayerName} failed to upgrade a pawn. Reason: No pawns.";
            }

            for(Int32 _currentPawn = 0; _currentPawn < _playerPawns.Count; _currentPawn++) {
               _playerPawns[_currentPawn].PawnType = "Full Stack";
            }

            ReturnPawnsToPlayer(_playerPawns, playerP);

            return $"{playerP.PlayerName} retrieved {_playerPawns.Count} Full Stack pawn from the Technical Hut!";
        }
    }
}
