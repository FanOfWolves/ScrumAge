using System;
using System.Collections.Generic;
using StoneAgeEngine.MapSpace;
using System.Text;
using StoneAgeEngine.Objects.Humans;

namespace StoneAgeEngine.MapSpace {
	class Map {


		/* For Purposes of SE1, this class is just an example of how we would need to create a Board class, thus I'm not going to fully document everything in it. Essentially, we create the "board" in a similar
		 * class to this and then pass information back and forth between here and the GUI. This class is only for example.
		 */







		// This class is the content of the game, adding nodes, items, surroundings, creatures, etc here puts them into the game. Unless something is being done to the engine specifically, anything
		//      added needs to only be in this class to avoid functionality problems with the engine. Nothing in this class in concrete, and can/will be changed based on the design of the game. Current
		//      objects are only for example and testing. If you wish to add functionality to the engine, feel free to do so but BE CAREFUL!!!!!!! Ask Michael for help if need be.
		private static List<Node> nodesOnMap = new List<Node>();


		private static Node	trainingRoom = new Node(2, "TrainingRoom", "Pawns get trained here");
		public Map() {
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
		public static Node GetNodeByID(int nodeID) {
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
