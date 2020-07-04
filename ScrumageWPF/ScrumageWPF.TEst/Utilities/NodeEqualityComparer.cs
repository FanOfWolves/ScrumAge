using System;
using System.Collections.Generic;
using ScrumageEngine.BoardSpace;

using System.Text;

namespace ScrumageWPF.Test.Utilities {
	/// <summary>
	/// An Equality Comparer for comparing Nodes.
	/// </summary>
	/// <seealso cref="System.Collections.Generic.IEqualityComparer{ScrumageEngine.BoardSpace.Node}" />
	public class NodeEqualityComparer : IEqualityComparer<Node> {
		/// <summary>
		/// For determining if nodes are equal.
		/// </summary>
		/// <param name="thisNode">This node.</param>
		/// <param name="thatNode">That node.</param>
		/// <returns>
		///     <c>true</c> if nodes are equal; Otherwise, <c>false</c>.
		/// </returns>
		public Boolean Equals(Node thisNode, Node thatNode) {
			return (thisNode.NodeID == thatNode.NodeID && thisNode.NodeName == thatNode.NodeName);
		}

		/// <summary>
		/// Returns a hash code for the specified object.
		/// </summary>
		/// <param name="obj">The <see cref="T:System.Object" /> for which a hash code is to be returned.</param>
		/// <returns>
		/// A hash code for the specified object.
		/// </returns>
		public Int32 GetHashCode(Node obj) {
			return obj.GetHashCode();
		}
	}
}
