using System;
using System.Collections.Generic;
using System.Text;

namespace ScrumageEngine.Objects.Items {
	/// <summary>
	/// Design Resource
	/// </summary>
	public class Design : Resource {

        #region Fields
        private const String RESOURCE_NAME = "Design";
        private const Int32 FULL_STACK_CHANCE = 20;
        private const Int32 FRONT_END_CHANCE = 15;
        private const Int32 BACK_END_CHANCE = 15;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Design"/> class.
        /// </summary>
        public Design():base(RESOURCE_NAME) {
            this.FullStackChance = FULL_STACK_CHANCE;
            this.FrontEndChance = FRONT_END_CHANCE;
            this.BackEndChance = BACK_END_CHANCE;
        }
        #endregion 
    }
}