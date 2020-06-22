using System;
using System.Collections.Generic;
using System.Text;
using ScrumageEngine.Objects.Items;
using ScrumageEngine.Objects.Items.Cards;
using ScrumageEngine.Objects.Player;

namespace ScrumageEngine.BoardSpace {
    /// <summary>
    /// Node that contains and gives <see cref="Card"/>s
    /// </summary>
    /// <seealso cref="ScrumageEngine.BoardSpace.Node" />
    class CardNode : Node {

        #region Fields
        private Stack<Card> nodeCards = new Stack<Card>();

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
        public CardNode(Int32 nodeId, String nodeName, Int32 cardLevel, IEnumerable<Card> cards) : base(nodeId, nodeName) {
            this.cardLevel = cardLevel;
            foreach(Card card in cards) {
                this.nodeCards.Push(card);
            }
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

        /// <summary>
        /// Checks if player can afford the card on top of the stack
        /// </summary>
        /// <param name="playerP">The player</param>
        /// <returns>
        ///     <c>true</c> if player has enough to pay for card; otherwise, <c>false</c>
        /// </returns>
        private Boolean CheckCardCost(Player playerP) {
            return (playerP.GetPlayerResources() >= this.nodeCards.Peek().GetCardRequirements());
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
        /// Takes the top card from this node.
        /// </summary>
        /// <param name="playerP">The player to give the card to</param>
        /// <returns>the card from the top of the stack</returns>
        private Card TakeCard(Player playerP) {
            Card _theCard = this.nodeCards.Pop();
            Resource[] _givenTypes = _theCard.GetCardRequirements().GetResourceTypes();
            foreach(Resource _resource in _givenTypes) {
                playerP.TakeResource(_resource, _theCard.GetCardRequirements()[_resource]);
            }
            return _theCard;
        }

        #region Inherited: Node Methods
        /// <summary>
        /// Attempt to take the top Card from this node
        /// </summary>
        /// <param name="playerP">the acting player</param>
        /// <returns>a string log denoting the acting player and the result</returns>
        public override String DoAction(Player playerP) {
            List<Pawn> _playerPawns = GatherPlayerPawns(playerP.PlayerID);
            foreach(Pawn _pawn in _playerPawns) {
                playerP.GivePawn(_pawn);
            }

            Boolean _gainCard = CheckCardCost(playerP);
            if(!_gainCard) {
                return $"{playerP.PlayerName} failed to obtain a card.";
            }

            playerP.AddToCards(TakeCard(playerP));
            return $"{playerP.PlayerName} obtained a new card!";
        }
        #endregion
    }
}
