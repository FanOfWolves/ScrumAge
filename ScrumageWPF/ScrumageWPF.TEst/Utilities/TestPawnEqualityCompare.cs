using System;
using System.Collections.Generic;
using System.Text;
using ScrumageEngine.Objects.Items;

namespace ScrumageWPF.Test.Utilities {
    /// <summary>
    /// Used for comparing <see cref="Pawn"/> for testing purposes.
    /// </summary>
    /// <seealso cref="System.Collections.Generic.IEqualityComparer{ScrumageEngine.Objects.Items.Pawn}"/>
    class TestPawnEqualityCompare : IEqualityComparer<Pawn> {

        /// <summary>
        /// Checks if 2 pawns are equal for testing purposes.
        /// </summary>
        /// <param name="pawn1">The pawn1.</param>
        /// <param name="pawn2">The pawn2.</param>
        /// <returns>
        ///     <c>true</c> if pawns equal; Otherwise, <c>false</c>.
        /// </returns>
        Boolean IEqualityComparer<Pawn>.Equals(Pawn pawn1, Pawn pawn2) {
            return pawn1.PawnID == pawn2.PawnID && pawn1.PawnType == pawn2.PawnType;
        }

        /// <summary>
        /// Returns a hash code for the specified object.
        /// </summary>
        /// <param name="obj">The <see cref="T:System.Object" /> for which a hash code is to be returned.</param>
        /// <returns>
        /// A hash code for the specified object.
        /// </returns>
        /// <exception cref="NotImplementedException"></exception>
        Int32 IEqualityComparer<Pawn>.GetHashCode(Pawn obj) => throw new NotImplementedException();
    }
}
