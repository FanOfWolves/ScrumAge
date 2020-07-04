using System;
using System.Collections.Generic;
using ScrumageEngine.Objects.Player;
using System.Text;

namespace ScrumageWPF.Test.Utilities {
	public class PlayerEqualityComparer : IEqualityComparer<Player> {
		/// <summary>
		/// For determining if nodes are equal.
		/// </summary>
		/// <param name="thisNode">This node.</param>
		/// <param name="thatNode">That node.</param>
		/// <returns>
		///     <c>true</c> if nodes are equal; Otherwise, <c>false</c>.
		/// </returns>
		public Boolean Equals(Player thisPlayer, Player otherPlayer) {
			return (thisPlayer.PlayerID == otherPlayer.PlayerID && thisPlayer.PlayerName == otherPlayer.PlayerName);
		}

		/// <summary>
		/// Returns a hash code for the specified object.
		/// </summary>
		/// <param name="obj">The <see cref="T:System.Object" /> for which a hash code is to be returned.</param>
		/// <returns>
		/// A hash code for the specified object.
		/// </returns>
		public Int32 GetHashCode(Player player) {
			return player.GetHashCode();
		}
	}
}
