using System;
using System.Collections.Generic;
using System.Text;

namespace ScrumageEngine.Objects.Items {
	/// <summary>
	/// Requirement Resource
	/// </summary>
	public class Requirements : Resource {

        #region Fields
        private const String RESOURCE_NAME = "Requirements";
        private const Int32 FULL_STACK_CHANCE = 20;
        private const Int32 FRONT_END_CHANCE = 20;
        private const Int32 BACK_END_CHANCE = 10;
        #endregion


        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Requirements"/> class.
        /// </summary>
        public Requirements(): base(RESOURCE_NAME) {
            base.FrontEndChance = FRONT_END_CHANCE;
            base.BackEndChance = BACK_END_CHANCE;
            base.FullStackChance = FULL_STACK_CHANCE;
        }
        #endregion
    }
}
