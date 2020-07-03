using System;
using System.Collections.Generic;
using ScrumageEngine.Objects.Items;

namespace ScrumageWPF.Test.Utilities {

    /// <summary>
    /// Used for comparing <see cref="ResourceContainer"/> objects for testing purposes.
    /// </summary>
    /// <seealso cref="System.Collections.Generic.IEqualityComparer{ResourceContainer}" />
    internal class TestResourceContainerEqualityCompare : IEqualityComparer<ResourceContainer> {
        /// <summary>
        /// Checks if two <see cref="ResourceContainer"/> objects are equal for the purpose of testing.
        /// </summary>
        /// <param name="container1">The container1.</param>
        /// <param name="container2">The container2.</param>
        /// <returns>
        ///     <c>true</c> if same for testing purposes; Otherwise, <c>false</c>.
        /// </returns>
        public Boolean Equals(ResourceContainer container1, ResourceContainer container2) {
            Resource[] container1Resources = container1.GetResourceTypes();
            Resource[] container2Resources = container2.GetResourceTypes();

            if(container1Resources.Length != container2Resources.Length)
                return false;

            foreach(Resource res in container1Resources) {
                if(!container1[res].Equals(container2[res]))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>
        ///     A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        Int32 IEqualityComparer<ResourceContainer>.GetHashCode(ResourceContainer obj) => throw new NotImplementedException();
    }
}
