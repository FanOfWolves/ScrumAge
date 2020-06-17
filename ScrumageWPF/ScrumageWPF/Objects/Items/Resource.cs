using System;

using System.Collections.Generic;
using System.Text;

namespace ScrumageEngine.Objects.Items {
    /// <summary>
    /// Base class for resources.
    /// </summary>
    public abstract class Resource {
		public String Name { get; set; }
		public Int32 FrontEndChance { get; set; }
		public Int32 BackEndChance { get; set; }
		public Int32 FullStackChance { get; set; }
        private const Int32 fullStackChance = 0;
        private const Int32 frontEndChance = 0;
        private const Int32 backEndChance = 0;


        /// <summary>
        /// Initializes a new instance of the <see cref="Resource"/> class.
        /// </summary>
        /// <param name="name">The name of the resource</param>
        public Resource(String name) {
			Name = name;
			FrontEndChance = 0;
			BackEndChance = 0;
			FullStackChance = 0;
		}


        /// <summary>
        /// Determines the chance based on the type of pawn passed in.
        /// </summary>
        /// <param name="pawnP">The pawn being used to attempt the resource collection.</param>
        /// <returns>The amount of resources collected.</returns>
        public virtual Int32 GetChance(Pawn pawnP) {
            switch(pawnP.PawnType) {
                case "Full Stack":
                    return fullStackChance;
                case "Back End":
                    return backEndChance;
                case "Front End":
                    return frontEndChance;
                default:
                    return 0;
            }
        }
    }
}
