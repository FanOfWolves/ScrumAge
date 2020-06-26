﻿using System;
using System.CodeDom;
using ScrumageEngine.Objects.Items;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Collections;
using System.Linq;

namespace ScrumageEngine.Objects.Items {
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
			resourceDictionary = InitDictionary(new Int32[]{0, 0, 0, 0});

		}


		/// <summary>
		/// Constructor for resource container.
		/// </summary>
		/// <param name="reqs">An array of ints that represents values to initialize the container with.</param>
		public ResourceContainer(Int32[] reqs) {
			resourceDictionary = InitDictionary(reqs);
        }


		/// <summary>
		/// Initializes a dictionary 
		/// </summary>
		/// <param name="reqs"></param>
		/// <returns></returns>
		private Dictionary<Resource, Int32> InitDictionary(Int32[] reqs = null) {
			Dictionary<Resource, Int32> retDictionary = new Dictionary<Resource, Int32> {
				{ new Requirements(), reqs[0] },
				{ new Design(), reqs[1] },
				{ new Implementation(), reqs[2] },
				{ new Testing(), reqs[3] }
			};
			return retDictionary;
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
	        return this[neededResource] >= neededAmount;
        }

        /// <summary>
        /// Gets the resource types.
        /// </summary>
        /// <returns>an array of all resource types in this container</returns>
        public Resource[] GetResourceTypes() {
	        return resourceDictionary.Keys.ToArray();
        }

        /// <summary>
        /// Determines the amount of the specified resource in this container
        /// </summary>
        /// <param name="neededResource">The needed resource.</param>
        /// <returns>the amount of that resource in this container</returns>
        public Int32 GetResourceAmount(Resource neededResource) {
	        return this[neededResource];
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
	        if (this[neededResource] < neededAmount)
                return false;
	        this[neededResource] -= neededAmount;
            return true;
        }


		/// <summary>
		/// Indexer to allow a resource amount to be selected by an int value.
		/// </summary>
		/// <param name="i">The "index" in the dictionary</param>
		/// <returns>The type of resource at that "index"'s amount</returns>
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
			set {
				if(i == 0) {
					this[new Requirements()] = value;
					return;
				}
				if(i == 1) {
					this[new Design()] = value;
					return;
				}
				if(i == 2) {
					this[new Implementation()] = value;
					return;
				}
				if(i == 3) {
					this[new Testing()] = value;
					return;
				}
				throw new IndexOutOfRangeException();
			}
        }


		/// <summary>
		/// Allows a resource amount to be selected from the dictionary with just a resource
		/// </summary>
		/// <param name="r">The resource type</param>
		/// <returns>The selected resource's amount</returns>
		public Int32 this[Resource r] {
			get {
				return this.resourceDictionary[r];
			}
			set {
				this.resourceDictionary[r] = value;
			}
		}



		/// <summary>
		/// Greater Than or Equal operator overload to allow ease of comparing two ResourceContainers
		/// </summary>
		/// <param name="playerResP">The player's resource container.</param>
		/// <param name="reqsP">The resource container to compare it to.</param>
		/// <returns>True if all resource types in the player inventory are greater than or equal that of the second container.</returns>
        public static Boolean operator >=(ResourceContainer playerResP, ResourceContainer reqsP) {
			Boolean isGreaterEqual = true;
			foreach (Resource r in playerResP.resourceDictionary.Keys) {
				if (playerResP[r] < reqsP[r]) isGreaterEqual = false;
			}
			return isGreaterEqual;
		}


		/// <summary>
		/// Less Than or Equal operator overload to allow easy of comparing two ResourceContainer
		/// </summary>
		/// <param name="playerResP"></param>
		/// <param name="reqsP"></param>
		/// <returns>True if all resource types in the player inventory are less than or equal that of the second container.</returns>
		public static Boolean operator <=(ResourceContainer playerResP, ResourceContainer reqsP) {
			Boolean isLessEqual = true;
			foreach(Resource r in playerResP.resourceDictionary.Keys) {
				if(playerResP[r] > reqsP[r]) isLessEqual = false;
			}
			return isLessEqual;
		}


		/// <summary>
		/// Greater than overload to allow ease in comparing two resource containers.
		/// </summary>
		/// <param name="playerResP">Player resource node</param>
		/// <param name="reqsP">Comparing resource node</param>
		/// <returns>True if all player resources are higher value than comparison container.</returns>
		public static Boolean operator >(ResourceContainer playerResP, ResourceContainer reqsP) {
			Boolean isGreater = true;
			foreach(Resource r in playerResP.resourceDictionary.Keys) {
				if(playerResP[r] <= reqsP[r]) isGreater = false;
			}
			return isGreater;
		}

		/// <summary>
		/// Less than overload to allow ease in comparing two resource containers.
		/// </summary>
		/// <param name="playerResP">Player resource node</param>
		/// <param name="reqsP">Comparing resource node</param>
		/// <returns>True if all player resources are lower value than comparison container.</returns>
		public static Boolean operator <(ResourceContainer playerResP, ResourceContainer reqsP) {
			Boolean isLess = true;
			foreach(Resource r in playerResP.resourceDictionary.Keys) {
				if(playerResP[r] >= reqsP[r]) isLess = false;
			}
			return isLess;
		}

		public static ResourceContainer operator -(ResourceContainer playerResP, ResourceContainer reqsP) {
			for(int i = 0; i < reqsP.resourceDictionary.Count; i++) {
				playerResP[i] -= reqsP[i];
			}
			return playerResP;
		}

		public static ResourceContainer operator +(ResourceContainer playerResP, ResourceContainer otherP) {
			for(int i = 0; i < otherP.resourceDictionary.Count; i++) {
				playerResP[i] += otherP[i];
			}
			return playerResP;
		}

		public String ShowRequirements() {
			return $"Requirements:{this[0]}\n" +
				   $"Design:{this[1]}\n" +
				   $"Implementation:{this[2]}\n" +
				   $"Testing:{this[3]}\n";
		}
    }
}
