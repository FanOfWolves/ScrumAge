using System;
using System.Collections.Generic;
using ScrumageEngine.BoardSpace;
using System.Text;
using ScrumageEngine.Objects.Player;
using ScrumageEngine.Objects.Items;
using ScrumageEngine.Objects.Items.Cards;
using static ScrumageEngine.Objects.Items.Cards.DeckCreator;

namespace ScrumageEngine.BoardSpace {
	public class Board {





		// This class is the content of the game, adding nodes, items, surroundings, creatures, etc here puts them into the game. Unless something is being done to the engine specifically, anything
		//      added needs to only be in this class to avoid functionality problems with the engine. Nothing in this class in concrete, and can/will be changed based on the design of the game. Current
		//      objects are only for example and testing. If you wish to add functionality to the engine, feel free to do so but BE CAREFUL!!!!!!! Ask Michael for help if need be.

		/// <summary>
		/// The list of <see cref="Node"/> objects on this board
		/// </summary>
		public List<Node> Nodes = new List<Node>();

		/// <summary>
		/// The list of <see cref="Die"/> objects on this board
		/// </summary>
		/// TODO Edit XML Comment Template for dice
		public List<Die> Dice = new List<Die>();



		private Node Resource1 = new ResourceNode(1, "Requirements", new Requirements());
		private Node Resource2 = new ResourceNode(2, "Design", new Design());
		private Node Resource3 = new ResourceNode(3, "Implementation", new Implementation());
		private Node Resource4 = new ResourceNode(4, "Testing", new Testing());
		private Node TechnicalHut = new UpgradeNode(5, "Technical Hut");
		private Node BudgetIncrease = new BudgetNode(6, "Budget Increase");
		private Node Interview = new HiringNode(7, "Interview");
		private Node Reassignment = new ReassignmentNode(8, "Reassignment");
		private Node AgilityNode1 = new CardNode(9, "Agility 1", new Deck("Agility", 10));
		private Node AgilityNode2 = new CardNode(10, "Agility 2", new Deck("Agility", 10));
		private Node AgilityNode3 = new CardNode(11, "Agility 3", new Deck("Agility", 10));
		private Node AgilityNode4 = new CardNode(12, "Agility 4", new Deck("Agility", 10));
		private Node ArtifactNode1 = new CardNode(13, "Artifact 1", new Deck("Artifact", 10));
		private Node ArtifactNode2 = new CardNode(14, "Artifact 2", new Deck("Artifact", 10));
		private Node ArtifactNode3 = new CardNode(15, "Artifact 3", new Deck("Artifact", 10));
		private Node ArtifactNode4 = new CardNode(16, "Artifact 4", new Deck("Artifact", 10));


		/// <summary>
		/// Initializes the board, if board has not been created, adds nodes to board.
		/// </summary>
		public Board() {
			if(Nodes.Count == 0) {
				InitBoard(Nodes);
			}
		}

		/// <summary>
		/// Adds all nodes to the list of nodes.
		/// </summary>
		/// <param name="nodesOnMap"></param>
		public void InitBoard(List<Node> nodesOnMap) {// To add nodes to the map, create the node in vars
													  // then add it to the passed list, nodeName(2digID)
			nodesOnMap.Add(Resource1);
			nodesOnMap.Add(Resource2);
			nodesOnMap.Add(Resource3);
			nodesOnMap.Add(Resource4);
			nodesOnMap.Add(TechnicalHut);
			nodesOnMap.Add(BudgetIncrease);
			nodesOnMap.Add(Interview);
			nodesOnMap.Add(Reassignment);
			nodesOnMap.Add(AgilityNode1);
			nodesOnMap.Add(AgilityNode2);
			nodesOnMap.Add(AgilityNode3);
			nodesOnMap.Add(AgilityNode4);
			nodesOnMap.Add(ArtifactNode1);
			nodesOnMap.Add(ArtifactNode2);
			nodesOnMap.Add(ArtifactNode3);
			nodesOnMap.Add(ArtifactNode4);
		}

		/// <summary>
		/// Obtains a node based on the node's name.
		/// </summary>
		/// <param name="nodeName">The name of the node to be found.</param>
		/// <returns></returns>
		public Node GetNodeByName(String nodeName) {
			foreach(Node node in Nodes) {
				if(node.NodeName.ToLower() == nodeName.ToLower()) {
					return node;
				}
			}
			return null;
		}


		/// <summary>
		/// Obtains a node based on ID, can be used to easily get a node.
		/// </summary>
		/// <param name="nodeID">The ID of the node to be found</param>
		/// <returns>The node if node exists, default node if it does not.</returns>
		public Node GetNodeByID(Int32 nodeID) {
			Node retNode = null;
			foreach(Node node in Nodes) {
				if(node.NodeID == nodeID) {
					retNode = node;
				}
			}
			return retNode;
		}


		/// <summary>
		/// Clears dice object
		/// </summary>
		public void ClearDice() {
			Dice.Clear();
		}

		/// <summary>
		/// Rolls number of dice
		/// </summary>
		/// <param name="diceCount">Num dice to be rolled</param>
		/// <param name="rand">Random object for dice roll</param>
		public void RollDice(Int32 diceCount, Random rand) {
			for(Int32 i = 0; i < diceCount; i++) {
				Dice.Add(new Die(rand.Next(6) + 1));
			}
		}

		/// <summary>
		/// Shows a GUI representation of the die face
		/// </summary>
		/// <returns>List of die graphics for display on GUI</returns>
		public List<String> ShowDice() {
			List<String> retList = new List<String>();
			Dice.ForEach(die => retList.Add(die.DrawDie()));
			return retList;
		}

		/// <summary>
		/// List current nodes
		/// </summary>
		/// <param name="nodeNameP">Name of node</param>
		/// <returns>List of all pawns at node</returns>
		public List<String> ListNodePawns(String nodeNameP) {
			return GetNodeByName(nodeNameP).ListPawns();
		}

		/// <summary>
		/// Returns number of pawns in node
		/// </summary>
		/// <param name="nodeNameP">Current node</param>
		/// <returns>Number of pawns</returns>
		public Int32 GetMaxPawnsInNode(String nodeNameP) {
			return GetNodeByName(nodeNameP).MaxPawnLimit;
		}

		/// <summary>
		/// REturns current pawn count in node
		/// </summary>
		/// <param name="nodeNameP">name of node</param>
		/// <returns>Number of pawns currently at Node</returns>
		public Int32 GetPawnCountInNode(String nodeNameP) {
			return GetNodeByName(nodeNameP).NumberOfPawns;
		}


		/// <summary>
		/// Represents current dice values in a single string for the log.
		/// </summary>
		/// <returns>String of current dice values.</returns>
		internal String DiceValues() {
			String retString = "";
			foreach(Die d in Dice) {
				retString += d.Value + " ";
			}
			return retString;
		}
	}
}
