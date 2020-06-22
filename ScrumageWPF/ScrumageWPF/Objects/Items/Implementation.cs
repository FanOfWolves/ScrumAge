using System;
using System.Collections.Generic;
using System.Text;

namespace ScrumageEngine.Objects.Items {

    /// <summary>
    /// Implementation Resource
    /// </summary>
    public class Implementation : Resource {

        #region Fields
        private const String RESOURCE_NAME = "Implementation";
        private const Int32 FRONT_END_CHANCE = 10;
        private const Int32 BACK_END_CHANCE = 20;
        private const Int32 FULL_STACK_CHANCE = 20;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Implementation"/> class.
        /// </summary>
        public Implementation(): base(RESOURCE_NAME) {
            this.FrontEndChance = FRONT_END_CHANCE;
            this.BackEndChance = BACK_END_CHANCE;
            this.FullStackChance = FULL_STACK_CHANCE;
        }
        #endregion
    }
}
