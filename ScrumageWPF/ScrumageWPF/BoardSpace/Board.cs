using System;
using System.Collections.Generic;
using ScrumageEngine.BoardSpace;
using System.Text;
using ScrumageEngine.Objects.Player;
using ScrumageEngine.Objects.Items;
using ScrumageEngine.Objects.Items.Cards;

namespace ScrumageEngine.BoardSpace {
	public class Board {





		// This class is the content of the game, adding nodes, items, surroundings, creatures, etc here puts them into the game. Unless something is being done to the engine specifically, anything
		//      added needs to only be in this class to avoid functionality problems with the engine. Nothing in this class in concrete, and can/will be changed based on the design of the game. Current
		//      objects are only for example and testing. If you wish to add functionality to the engine, feel free to do so but BE CAREFUL!!!!!!! Ask Michael for help if need be.
		private List<Node> Nodes = new List<Node>();
		private List<Die> dice = new List<Die>();
		private Stack<Card> Artifacts = new Stack<Card>();
		private Stack<Card> Agility = new Stack<Card>();

		private Node Resource1 = new ResourceNode(1, "Requirements", new Requirements());
		private Node Resource2 = new ResourceNode(2, "Design", new Design());
		private Node Resource3 = new ResourceNode(3, "Implementation", new Implementation());
		private Node Resource4 = new ResourceNode(4, "Testing", new Testing());
		private Node TechnicalHut = new UpgradeNode(5, "Technical Hut");
		private Node BudgetIncrease = new BudgetNode(6, "Budget Increase");
		private Node Interview = new HiringNode(7, "Interview Node");
		private Node Reassignment = new ReassignmentNode(8, "Reassignment Node");
		private Card TestCard1 = new Card("artifact", "Test Artifact", new []{4,3,2,1});
		private Card TestCard2 = new Card("agility", "Test Agility", new []{1,2,3,4});


		/// <summary>
		/// Initializes the board, if board has not been created, adds nodes to board.
		/// </summary>
		public Board() {
			if (Nodes.Count == 0) {
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
			Artifacts.Push(TestCard1);
			Agility.Push(TestCard2);
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
		/// Retrive list of all nodes
		/// </summary>
		/// <returns>List of node objects</returns>
		public List<Node> GetAllNodes() {
			return Nodes;
		}
		/// <summary>
		/// Get artifact card deck
		/// </summary>
		/// <returns>Stack of Artifact card objects</returns>
		public Stack<Card> GetArtifacts() {
			return Artifacts;
		}
		/// <summary>
		/// Get agility card stack
		/// </summary>
		/// <returns>Stack of Agility card objects</returns>
		public Stack<Card> GetAgility() {
			return Agility;
		}
		/// <summary>
		/// Pull top artifact card from deck
		/// </summary>
		/// <returns>Card object from top of artifact stack</returns>
		public Card GetTopArtifact() {
			return Artifacts.Pop();
		}
		/// <summary>
		/// Pull top agility card from deck
		/// </summary>
		/// <returns>Card object from top of stack</returns>
		public Card GetTopAgility() {
			return Agility.Pop();
		}

		/// <summary>
		/// Obtains a node based on ID, can be used to easily get a node.
		/// </summary>
		/// <param name="nodeID">The ID of the node to be found</param>
		/// <returns>The node if node exists, default node if it does not.</returns>
		public Node GetNodeByID(Int32 nodeID) {
			Node retNode = null;
			foreach (Node node in Nodes) {
				if (node.NodeID == nodeID) {
					retNode = node;
				}
			}
			return retNode;
		}
		/// <summary>
		/// Get dice currently in list
		/// </summary>
		/// <returns>list of dice objects</returns>
		public List<Die> GetDice() {
			return dice;
		}
		/// <summary>
		/// Clears dice object
		/// </summary>
		public void ClearDice() {
			dice.Clear();
		}
		/// <summary>
		/// Rolls number of dice
		/// </summary>
		/// <param name="diceCount">Num dice to be rolled</param>
		/// <param name="rand">Random object for dice roll</param>
		public void RollDice(int diceCount, Random rand) {
			for(Int32 i = 0; i < diceCount; i++) {
				dice.Add(new Die(rand.Next(6) + 1));
			}
		}
		/// <summary>
		/// Shows a GUI representation of the die face
		/// </summary>
		/// <returns>List of die graphics for display on GUI</returns>
		public List<String> ShowDice() {
			List<String> retList = new List<String>();
			dice.ForEach(die => retList.Add(die.DrawDie()));
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
		public int GetMaxPawnsInNode(String nodeNameP) {
			return GetNodeByName(nodeNameP).MaxPawnLimit;
		}
		/// <summary>
		/// REturns current pawn count in node
		/// </summary>
		/// <param name="nodeNameP">name of node</param>
		/// <returns>Number of pawns currently at Node</returns>
		public int GetPawnCountInNode(String nodeNameP) {
			return GetNodeByName(nodeNameP).NumberOfPawns;
		}
	}

}
