﻿using System;
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
        /// <summary>
        /// The <see cref="Stack{T}"/> of <see cref="Card"/>s in this CardNode
        /// </summary>
        private Deck deck;


        public override Int32 MaxPawnLimit { get { return 1; } }
        #endregion

        #region Constructors        
        /// <summary>
        /// Initializes a new instance of the <see cref="CardNode"/> class.
        /// </summary>
        /// <param name="nodeId">The node identifier.</param>
        /// <param name="nodeName">Name of the node.</param>
        /// <param name="cardLevel">The level of the cards in this node</param>
        /// <param name="cards">The cards for this node</param>
        public CardNode(Int32 nodeId, String nodeName, Deck deck) : base(nodeId, nodeName) {
            this.deck = deck;
        }
        #endregion


        /// <summary>
        /// Indicates if this node is out of cards
        /// </summary>
        /// <returns>
        ///     <c>true</c> if node is out of cards; Otherwise, <c>false</c>
        /// </returns>
        public Boolean OutOfCards() {
            return deck.Count == 0;
        }

        /// <summary>
        /// Checks if player can afford the card on top of the stack
        /// </summary>
        /// <param name="playerP">The player</param>
        /// <returns>
        ///     <c>true</c> if player has enough to pay for card; otherwise, <c>false</c>
        /// </returns>
        private Boolean CheckCardCost(Player playerP) {
            return (playerP.GetPlayerResources() >= deck.Peek().GetCardRequirements());
        }

        /// <summary>
        /// Takes the top card from this node.
        /// </summary>
        /// <param name="playerP">The player to give the card to</param>
        /// <returns>the card from the top of the stack</returns>
        private Card TakeCard(Player playerP) {
            Card _theCard = deck.Draw();
            Resource[] _givenTypes = _theCard.GetCardRequirements().GetResourceTypes();
            /*foreach(Resource _resource in _givenTypes) {
                playerP.TakeResource(_resource, _theCard.GetCardRequirements()[_resource]);
            }*/
            playerP.playerResources -= _theCard.GetCardRequirements();
            return _theCard;
        }

        public Card TopCard() {
            return deck.Peek();
		}


        #region Inherited: Node Methods
        /// <summary>
        /// Attempt to take the top Card from this node
        /// </summary>
        /// <param name="playerP">the acting player</param>
        /// <returns>a string log denoting the acting player and the result</returns>
        public override String DoAction(Player playerP) {

            // Preliminary checks
            if(deck.Count == 0) return $"There are no cards left in {NodeName}.";
            if(Pawns.Count < 1) return $"{playerP.PlayerName} had no pawns in {NodeName} to claim a card.";
            Pawn _pawn = Pawns[0];
            if(_pawn.PawnID != playerP.PlayerID) return $"{playerP.PlayerName} had no pawns in {NodeName} to claim a card.";


            // return pawn
            playerP.GivePawn(_pawn);
            Pawns.Remove(_pawn);
            // Check required resources
            Boolean _gainCard = CheckCardCost(playerP);
            if(!_gainCard) {
                return $"{playerP.PlayerName} failed to obtain a card due to lack of resources.";
            }

            // Give card
            String cardType = "";
            cardType = deck.Peek().CardType();
            playerP.AddToCards(TakeCard(playerP));
            return $"{playerP.PlayerName} obtained a new {cardType} Card!";
        }
        #endregion
    }
}
