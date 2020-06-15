using ScrumageEngine.Objects.Humans;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScrumageEngine.BoardSpace {
    /// <summary>
    /// Provides temporary funds to Players
    /// </summary>
    /// <seealso cref="ScrumageEngine.BoardSpace.Node" />
    class ReassignmentNode : Node {

        public override Int32 MaxPawnLimit { get { return 4; } }


        /// <summary>
        /// Instantiates a new instance of the <see cref = "ReassignmentNode"/> class
        /// </summary>
        /// <param name="nodeIDP">the node ID</param>
        /// <param name="nodeNameP">the name of the node</param>
        #region Constructors
        public ReassignmentNode(Int32 nodeIDP, String nodeNameP):base(nodeIDP, nodeNameP) {
            
        }


        /// <summary>
        /// Gives funds to a player per number of their pawns in the node
        /// </summary>
        /// <param name="playerP">The player to give funds</param>
        /// <returns>the message log denoting number of funds acquired and which player recieved them</returns>
        public override String DoAction(Player playerP) {
            if(base.Pawns.Count == 0)
                return null; //!!TODO: EmptyNodeException()

            Int32 _playerID = playerP.PlayerID;
            Boolean _playerHasPawnsHere =  base.Pawns.Exists(_playerPawn => _playerPawn.PawnID == _playerID);
            if(!_playerHasPawnsHere)
                return null; //!!TODO: NoPlayerPawnsException()

            Int32 _numberOfFunds = 0;
            for(Int32 _pawnIndex = base.Pawns.Count - 1; _pawnIndex >= 0; _pawnIndex--) {
                if(base.Pawns[_pawnIndex].PawnID != _playerID)
                    continue;
                _numberOfFunds++;

                playerP.GivePawn(base.Pawns[_pawnIndex]);
                base.Pawns.RemoveAt(_pawnIndex);
            }
            
            playerP.GiveFunds(_numberOfFunds);
            return $"{playerP.PlayerName} has recieved {_numberOfFunds} additional funds from reassignment!";
        }
        #endregion

    }
}
