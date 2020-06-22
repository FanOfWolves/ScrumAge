using ScrumageEngine.Objects.Player;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScrumageEngine.BoardSpace {


    /// <summary>
    /// Provides budget to players that place their pawns here
    /// </summary>
    /// <seealso cref="ScrumageEngine.BoardSpace.Node" />
    class BudgetNode : Node {

        public override Int32 MaxPawnLimit { get { return 4; } }

        #region Constructors        
        /// <summary>
        /// Initializes a new instance of the <see cref="BudgetNode"/> class.
        /// </summary>
        /// <param name="nodeIDP">The node id</param>
        /// <param name="nodeNameP">The node name</param>
        public BudgetNode(Int32 nodeIDP, String nodeNameP):base(nodeIDP, nodeNameP) {

        }
        #endregion

        /// <summary>
        /// Increases budget of player parameter by number of their pawns in this Node
        /// </summary>
        /// <param name="playerP">The player who wants to increase their budget</param>
        /// <returns>a message log denoting amount of budget increase and the player given it.</returns>
        public override String DoAction(Player playerP) {
            if(base.Pawns.Count == 0)
                return null; //!!TODO: EmptyNodeException()

            Int32 _playerID = playerP.PlayerID;
            Boolean _playerHasPawnsHere = base.Pawns.Exists(_playerPawn => _playerPawn.PawnID == _playerID);
            if(!_playerHasPawnsHere)
                return null; //!!TODO: NoPlayerPawnsException()

            Int32 _budgetIncrease = 0;
            // Increase budget per pawn belong to playerP
            for(Int32 _pawnIndex = base.Pawns.Count - 1; _pawnIndex >= 0; _pawnIndex--) {
                if(base.Pawns[_pawnIndex].PawnID == _playerID)
                    continue;
                _budgetIncrease++;

                playerP.GivePawn(base.Pawns[_pawnIndex]);
                base.Pawns.RemoveAt(_pawnIndex);
            }

            playerP.IncreaseBudget(_budgetIncrease);
            return $"{playerP.PlayerName} has {_budgetIncrease} more budget!";
        }

    }
}
