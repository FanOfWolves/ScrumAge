using System;
using System.Collections.Generic;
using ScrumageEngine.BoardSpace;
using System.Text;
using ScrumageEngine.Objects.Humans;
using ScrumageEngine.Objects.Items;

namespace ScrumageEngine.BoardSpace {
	public class Board {





		// This class is the content of the game, adding nodes, items, surroundings, creatures, etc here puts them Int32o the game. Unless something is being done to the engine specifically, anything
		//      added needs to only be in this class to avoid functionality problems with the engine. Nothing in this class in concrete, and can/will be changed based on the design of the game. Current
		//      objects are only for example and testing. If you wish to add functionality to the engine, feel free to do so but BE CAREFUL!!!!!!! Ask Michael for help if need be.
		private List<Node> Nodes = new List<Node>();
		public List<Die> dice = new List<Die>();



		private Node Resource1 = new Node(2, "Resource 1", "");
		private Node Resource2 = new Node(3, "Resource 2", "");
		private Node Resource3 = new Node(2, "Resource 3", "");
		private Node Resource4 = new Node(2, "Resource 4", "");
		private Node TechnicalHut = new Node(1, "Technical Hut", "Pawns get trained here");
		private Node BudgetIncrease = new Node(1, "Budget Increase", "Pawns get trained here");
		private Node Int32erview = new Node(1, "Int32erview Node", "Pawns get trained here");
		private Node Reassignment = new Node(1, "Reassignment Node", "Pawns get trained here");
		public Board() {
			if (Nodes.Count == 0) {
				InitMap(Nodes);
			}
		}

		/// <summary>
		/// Adds all nodes to the list of nodes.
		/// </summary>
		/// <param name="nodesOnMap"></param>
		public void InitMap(List<Node> nodesOnMap) {// To add nodes to the map, create the node in vars
													// then add it to the passed list, nodeName(2digID)
			nodesOnMap.Add(Resource1);
			nodesOnMap.Add(Resource2);
			nodesOnMap.Add(Resource3);
			nodesOnMap.Add(Resource4);
			nodesOnMap.Add(TechnicalHut);
			nodesOnMap.Add(BudgetIncrease);
			nodesOnMap.Add(Int32erview);
			nodesOnMap.Add(Reassignment);
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
			return new Node();
		}
		public List<Node> GetAllNodes() {
			return Nodes;
		}

		/// <summary>
		/// Obtains a node based on ID, can be used to easily get a node.
		/// </summary>
		/// <param name="nodeID">The ID of the node to be found</param>
		/// <returns>The node if node exists, default node if it does not.</returns>
		public Node GetNodeByID(Int32 nodeID) {
			Node retNode = new Node();
			foreach (Node node in Nodes) {
				if (node.NodeID == nodeID) {
					retNode = node;
				}
			}
			return retNode;
		}
	}

}
