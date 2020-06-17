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

        #region Inherited: Node Methods

        /// <summary>
        /// Attempt to take the top Card from this node
        /// </summary>
        /// <param name="playerP">the acting player</param>
        /// <returns>a string log denoting the acting player and the result</returns>
        public override String DoAction(Player playerP) {
            throw new NotImplementedException();

            
        }
        #endregion
    }
}
