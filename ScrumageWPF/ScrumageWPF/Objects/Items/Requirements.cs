using System;
using System.Collections.Generic;
using System.Text;

namespace ScrumageEngine.Objects.Items {
	/// <summary>
	/// Requirement Resource
	/// </summary>
	class Requirements : Resource {

        #region Fields
        private const Int32 fullStackChance = 20;
        private const Int32 frontEndChance = 20;
        private const Int32 backEndChance = 10;
        #endregion


        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Requirements"/> class.
        /// </summary>
        /// <param name="name">The name of the resource</param>
        public Requirements(String name): base(name) {

        }
        #endregion
    }
}
