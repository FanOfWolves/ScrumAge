using System;
using System.CodeDom;
using ScrumageEngine.Objects.Items;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Collections;

namespace ScrumageEngine.Objects.Player {
    /// <summary>
    /// Handles the player's resources
    /// </summary>
    public class ResourceContainer{

        #region Fields

        private Dictionary<Resource, Int32> resourceDictionary;

        #endregion

		/// <summary>
		/// Initializes a new instance of the <see cref="ResourceContainer"/> class.
		/// </summary>
		public ResourceContainer() {
            resourceDictionary = new Dictionary<Resource, Int32>();
        }

        /// <summary>
        /// Adds a resource to this container.
        /// </summary>
        /// <param name="newResource">The new resource to add</param>
        public void AddResource(Resource newResource) {
	        this[newResource]++;
        }

        /// <summary>
        /// Determines whether this container has the indicated amount of resources.
        /// </summary>
        /// <param name="neededResource">The needed resource.</param>
        /// <param name="neededAmount">The needed amount of this resource.</param>
        /// <returns>
        ///   <c>true</c> if it has enough of the specified resource; otherwise, <c>false</c>.
        /// </returns>
        public Boolean HasResourceAmount(Resource neededResource, Int32 neededAmount) {
            Boolean _hasResource = this.resourceDictionary.TryGetValue(neededResource, out Int32 _amountHere);
            if (!_hasResource) {
                return false;
            }
            else {
                return _amountHere >= neededAmount;
            }
        }

        /// <summary>
        /// Gets the resource types.
        /// </summary>
        /// <returns>an array of all resource types in this container</returns>
        public Resource[] GetResourceTypes() {
            List<Resource> _outputTypes = new List<Resource>();
            foreach(KeyValuePair<Resource, Int32> res in this.resourceDictionary) {
                _outputTypes.Add(res.Key);
            }
            _outputTypes.TrimExcess();
            return _outputTypes.ToArray();
        }

        /// <summary>
        /// Determines the amount of the specified resource in this container
        /// </summary>
        /// <param name="neededResource">The needed resource.</param>
        /// <returns>the amount of that resource in this container</returns>
        public Int32 GetResourceAmount(Resource neededResource) {
            Boolean _hasResource = this.resourceDictionary.TryGetValue(neededResource, out Int32 _amountHere);
            if (_hasResource) {
                return _amountHere;
            }
            else {
                return 0;
            }
        }

        /// <summary>
        /// Subtracts the amount of the specified resource in this container.
        /// </summary>
        /// <param name="neededResource">The needed resource.</param>
        /// <param name="neededAmount">The needed amount.</param>
        /// <returns>
        ///     <c>true</c> if resource payment successful; otherwise, <c>false</c>.
        /// </returns>
        public Boolean TakeResources(Resource neededResource, Int32 neededAmount) {
            Boolean _hasResource = this.resourceDictionary.TryGetValue(neededResource, out Int32 _amountHere);
            if (!_hasResource || _amountHere < neededAmount)
                return false;
            this.resourceDictionary[neededResource] = _amountHere - neededAmount;
            return true;
        }

        private Int32 this[Int32 i] {
			get {
				if(i == 0)
					return this[new Requirements()];
				if(i == 1)
					return this[new Design()];
				if(i == 2)
					return this[new Implementation()];
				if (i == 3)
					return this[new Testing()];
				throw new IndexOutOfRangeException();
			}
		}

		public Int32 this[Resource r] {
			get {
				return resourceDictionary[r];
			}
			set {
				resourceDictionary[r] = value;
			}
		}




        public static Boolean operator >=(ResourceContainer playerResP, ResourceContainer reqsP) {
			Boolean isGreaterEqual = true;
			foreach (Resource r in playerResP.resourceDictionary.Keys) {
				if (playerResP[r] < reqsP[r]) isGreaterEqual = false;
			}
			return isGreaterEqual;
		}

		public static Boolean operator <=(ResourceContainer playerResP, ResourceContainer reqsP) {
			Boolean isLess = true;
			foreach(Resource r in playerResP.resourceDictionary.Keys) {
				if(playerResP[r] < reqsP[r]) isLess = false;
			}
			return isLess;
		}

		public static Boolean operator >(ResourceContainer playerResP, ResourceContainer reqsP) {
			Boolean isGreaterEqual = true;
			foreach(Resource r in playerResP.resourceDictionary.Keys) {
				if(playerResP[r] <= reqsP[r]) isGreaterEqual = false;
			}
			return isGreaterEqual;
		}

		public static Boolean operator <(ResourceContainer playerResP, ResourceContainer reqsP) {
			Boolean isLess = true;
			foreach(Resource r in playerResP.resourceDictionary.Keys) {
				if(playerResP[r] >= reqsP[r]) isLess = false;
			}
			return isLess;
		}
    }
}
