using ScrumageEngine.Objects.Player;
using ScrumageEngine.Objects.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScrumageEngine.BoardSpace {
    /// <summary>
    /// Provides players with new Pawns based on chance
    /// </summary>
    /// <seealso cref="ScrumageEngine.BoardSpace.Node" />
    public class HiringNode: Node {

        #region Fields
        private const Int32 FRONT_END_UPPER_HIRING_RANGE = 45;
        private const Int32 BACK_END_UPPER_HIRING_RANGE = 90;
        private readonly Random randomHiringGenerator;
        #endregion

        public override Int32 MaxPawnLimit { get { return 2; } }

        #region Constructors
        /// <summary>
        /// Instantiates a new instance of the <see cref = "HiringNode"/> class
        /// Base: <seealso cref = "Node"/> class
        /// </summary>
        /// <param name="nodeID">the ID of the new Node</param>
        /// <param name="nodeName">the name of the new Node</param>
        public HiringNode(Int32 nodeID, String nodeName): base(nodeID, nodeName) {
            this.randomHiringGenerator = new Random();
        }
        #endregion

        /// <summary>
        /// Randomly determines which pawn to return.
        /// </summary>
        /// <param name="playerIDP"></param>
        private Pawn HirePawn(Int32 playerIDP) {
            Int32 _hiringResult = randomHiringGenerator.Next(0, 101);

			if(_hiringResult <= FRONT_END_UPPER_HIRING_RANGE) // 0  - 45 : Front End
                return new Pawn(playerIDP, "Front End");     

            if(_hiringResult <= BACK_END_UPPER_HIRING_RANGE)  // 46 - 90 : Back End
                return new Pawn(playerIDP, "Back End");      

            return new Pawn(playerIDP, "Full Stack");         // 91 - 100: Full Stack
        }


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
        /// Returns the pawns (if any) to player.
        /// </summary>
        /// <param name="pawnsToReturnP">The pawns to return.</param>
        /// <param name="playerP">The player.</param>
        private void ReturnPawnsToPlayer(List<Pawn> pawnsToReturnP, Player playerP) {
            foreach (Pawn _pawn in pawnsToReturnP) {
                playerP.GivePawn(_pawn);
            }
        }

        /// <summary>
        /// Randomly assigns the current Player a Pawn if they have allocated a Pawn to this Node.
        /// Inherited from <seealso cref = "Node"/>.
        /// </summary>
        /// <param name="playerP">the Player attempting to hire a Pawn</param>
        /// <returns>a message log denoting the Pawn that was hired and the Player that hired it</returns>
        public override String DoAction(Player playerP) {
            Int32 _playerID = playerP.PlayerID;
            List<Pawn> _playerPawns = GatherPlayerPawns(_playerID);
            if (_playerPawns.Count < 2) {
                // Return pawns to player
                ReturnPawnsToPlayer(_playerPawns, playerP);
                return $"{playerP.PlayerName} failed to hire more developers. Reason: Need at least 2 Pawns.";
            }
            else {
                Pawn _hiredPawn = HirePawn(_playerID);
                ReturnPawnsToPlayer(_playerPawns, playerP);
                playerP.GivePawn(_hiredPawn);
                return $"{playerP.PlayerName} has hired a {_hiredPawn.PawnType} developer!";
            }
        }
    }
}
