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
        /// Copy Constructor. Initializes a new instance of the <see cref="Resource"/> class.
        /// </summary>
        /// <param name="other">The original Resource to copy.</param>
        public Resource(Resource other) {
            Name = other.Name;
            FrontEndChance = other.FrontEndChance;
            BackEndChance = other.BackEndChance;
            FullStackChance = other.FullStackChance;
        }

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

        public Boolean Equals(Resource other) {
            if (other is null) return false;
            return this.Name == other.Name;
        }

        public override Boolean Equals(Object obj) {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == this.GetType() && Equals((Resource) obj);
        }

        public override Int32 GetHashCode() {
            return (Name != null ? Name.GetHashCode() : 0);
        }



        public static Boolean operator ==(Resource left, Resource right) {
            return left?.Name == right?.Name;
        }

        public static Boolean operator !=(Resource left, Resource right) {
            return left?.Name != right?.Name;
        }

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
