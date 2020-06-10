using System;
using System.Collections.Generic;
using ScrumageEngine.MapSpace;
using System.Text;
using ScrumageEngine.Objects.Humans;
using ScrumageEngine.Objects.Items;

namespace ScrumageEngine.MapSpace {
	public class Board {





		// This class is the content of the game, adding nodes, items, surroundings, creatures, etc here puts them into the game. Unless something is being done to the engine specifically, anything
		//      added needs to only be in this class to avoid functionality problems with the engine. Nothing in this class in concrete, and can/will be changed based on the design of the game. Current
		//      objects are only for example and testing. If you wish to add functionality to the engine, feel free to do so but BE CAREFUL!!!!!!! Ask Michael for help if need be.
		private List<Node> nodesOnMap = new List<Node>();
		public Player p1 = new Player(1, "bla"); // Players maybe moved to a "Game" class?
		public List<Die> dice = new List<Die>();


		private Node trainingRoom = new Node(2, "TrainingRoom", "Pawns get trained here");
		public Board() {
			if (nodesOnMap.Count == 0) {
				InitMap(nodesOnMap);
			}
		}
		public void InitMap(List<Node> nodesOnMap) {// To add nodes to the map, create the node in vars
													// then add it to the passed list, nodeName(2digID)
			nodesOnMap.Add(trainingRoom);
		}
		/// <summary>
		/// Obtains a node based on the node's name.
		/// </summary>
		/// <param name="nodeName">The name of the node to be found.</param>
		/// <returns></returns>
		public Node GetNodeByName(String nodeName) {
			Node retNode = new Node();
			switch (nodeName) {
				case "TrainingRoom":
					retNode = trainingRoom;
					break;
			}
			return retNode;
		}
		public List<Node> GetAllNodes() {
			return nodesOnMap;
		}

		/// <summary>
		/// Obtains a node based on ID, can be used to easily get a node.
		/// </summary>
		/// <param name="nodeID">The ID of the node to be found</param>
		/// <returns>The node if node exists, default node if it does not.</returns>
		public Node GetNodeByID(int nodeID) {
			Node retNode = new Node();
			foreach (Node node in nodesOnMap) {
				if (node.NodeID == nodeID) {
					retNode = node;
				}
			}
			return retNode;
		}
	}

}
