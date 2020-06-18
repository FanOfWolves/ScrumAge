using System;
using System.Collections.Generic;
using System.Text;
using ScrumageEngine.Objects.Items;
using ScrumageEngine.Objects.Player;

namespace ScrumageEngine.BoardSpace {
    /// <summary>
    /// Node that contains and gives <see cref="Card"/>s
    /// </summary>
    /// <seealso cref="ScrumageEngine.BoardSpace.Node" />
    class CardNode : Node {

        #region Fields
        private List<Card> nodeCards;

        private readonly Int32 cardLevel;
        #endregion

        #region Constructors        
        /// <summary>
        /// Initializes a new instance of the <see cref="CardNode"/> class.
        /// </summary>
        /// <param name="nodeId">The node identifier.</param>
        /// <param name="nodeName">Name of the node.</param>
        /// <param name="cardLevel">The level of the cards in this node</param>
        /// <param name="cards">The cards for this node</param>
        public CardNode(Int32 nodeId, String nodeName, Int32 cardLevel, List<Card> cards):base(nodeId, nodeName) {
            this.cardLevel = cardLevel;
            this.nodeCards = cards;     //TODO: Check if we need to do a deep copy
        }
        #endregion


        /// <summary>
        /// Indicates if this node is out of cards
        /// </summary>
        /// <returns>
        ///     <c>true</c> if node is out of cards; Otherwise, <c>false</c>
        /// </returns>
        public Boolean OutOfCards() {
            return this.nodeCards.Count == 0;
        }

        //This method is related to the Dictionary shit that Michael is doing on Card.cs
        private Boolean CheckCardCost(ResourceContainer playerResources) {
            throw new NotImplementedException();
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


        #region Inherited: Node Methods
        /// <summary>
        /// Attempt to take the top Card from this node
        /// </summary>
        /// <param name="playerP">the acting player</param>
        /// <returns>a string log denoting the acting player and the result</returns>
        public override String DoAction(Player playerP) {
            return "Card Node Not Implemented";
        }
        #endregion
    }
}
