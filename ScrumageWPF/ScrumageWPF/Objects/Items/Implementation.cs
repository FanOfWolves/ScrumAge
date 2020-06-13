using System;
using System.Collections.Generic;
using System.Text;

namespace ScrumageEngine.Objects.Items {

    /// <summary>
    /// Implementation Resource
    /// </summary>
    public class Implementation : Resource {

        #region Fields
        private const Int32 frontEndChance = 10;
        private const Int32 backEndChance = 20;
        private const Int32 fullStackChance = 20;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Implementation"/> class.
        /// </summary>
        /// <param name="name">The name of the resource</param>
        public Implementation(String name): base(name) {

        }
        #endregion
    }
}
