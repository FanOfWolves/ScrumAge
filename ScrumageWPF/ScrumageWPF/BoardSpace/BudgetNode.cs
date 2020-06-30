using ScrumageEngine.Objects.Player;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScrumageEngine.BoardSpace {


    /// <summary>
    /// Provides budget to players that place their pawns here
    /// </summary>
    /// <seealso cref="ScrumageEngine.BoardSpace.Node" />
    public class BudgetNode : Node {

        /// <summary>
        /// Gets the maximum pawn limit for this Node.
        /// </summary>
        /// <value>
        /// The maximum pawn limit.
        /// </value>
        public override Int32 MaxPawnLimit { get { return 1; } }

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
            Int32 _playerID = playerP.PlayerID;
            Boolean _playerHasPawnsHere = base.Pawns.Exists(_playerPawn => _playerPawn.PawnID == _playerID);
            if(!_playerHasPawnsHere || Pawns.Count == 0) {
                return $"{playerP.PlayerName} Failed to increase their budget. Reason: No Pawns";
            }
            if(Pawns[0].PawnID == playerP.PlayerID) {
                playerP.IncreaseBudget(1);
                playerP.GivePawn(Pawns[0]);
                Pawns.Remove(Pawns[0]);
            }
            return $"{playerP.PlayerName} has 1 more budget!";
        }

    }
}
