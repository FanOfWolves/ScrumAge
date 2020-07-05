using System;
using ScrumageEngine.Objects.Items;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace ScrumageEngine.Objects.Items {
    /// <summary>
    /// Base class for resources.
    /// </summary>
    public abstract class Resource : IEquatable<Resource> {
		public String Name { get; private set; }
		public Int32 FrontEndChance { get; set; }
		public Int32 BackEndChance { get; set; }
		public Int32 FullStackChance { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Resource"/> class.
        /// </summary>
        /// <param name="name">The name of the resource</param>
        public Resource(String name) {
			Name = name;
        }

        /// <summary>
        /// Returns the change for any type of pawn the chance to successfully gather a resource.
        /// </summary>
        /// <param name="pawnP">The pawn to get the chance for.</param>
        /// <returns>The chance for that pawn.</returns>
        public Int32 GetChance(Pawn pawnP) {
            switch(pawnP.PawnType) {
                case "Full Stack":
                    return this.FullStackChance;
                case "Back End":
                    return this.BackEndChance;
                case "Front End":
                    return this.FrontEndChance;
                default:
                    return 0;
            }
        }


        /// <summary>
        /// Allows two resources to be compared.
        /// </summary>
        /// <param name="other">Resource to compare to</param>
        /// <returns>True if same type of resource, false if not.</returns>
        public Boolean Equals(Resource other) {
            if (other is null) return false;
            return this.Name == other.Name;
        }


        /// <summary>
        /// Allows two resources to be compared
        /// </summary>
        /// <param name="obj">Object to compare to</param>
        /// <returns>True of object is equal</returns>
        public override Boolean Equals(Object obj) {
            if (obj is null) return false;
            return Equals(obj as Resource);
        }


        /// <summary>
        /// returns a hash code for the object
        /// </summary>
        /// <returns>The hash code for the object</returns>
        public override Int32 GetHashCode() {
            return Name.GetHashCode();
        }


        /// <summary>
        /// == operator overload to allow two resources to be compared.
        /// </summary>
        /// <param name="left">First resource to be comapred</param>
        /// <param name="right">Second Resource to be compared</param>
        /// <returns>True of both resources are equal</returns>
        public static Boolean operator ==(Resource left, Resource right) {
            return left?.Name == right?.Name;
        }


        /// <summary>
        /// != operator overload to allow two resources to be checked as not equal
        /// </summary>
        /// <param name="left">First resource to be compared.</param>
        /// <param name="right">Second resource to be compared.</param>
        /// <returns>true of resources are not equal.</returns>
        public static Boolean operator !=(Resource left, Resource right) {
            return left?.Name != right?.Name;
        }


        /// <summary>
        /// Makes a deep copy of the resource.
        /// </summary>
        /// <returns>A deep copy of the resource</returns>
        public Resource DeepCopy() {
            Resource _copy = (Resource) this.MemberwiseClone();
            _copy.FrontEndChance = FrontEndChance;
            _copy.BackEndChance = BackEndChance;
            _copy.FullStackChance = FullStackChance;
            _copy.Name = Name;
            return _copy;
        }

    }
}
