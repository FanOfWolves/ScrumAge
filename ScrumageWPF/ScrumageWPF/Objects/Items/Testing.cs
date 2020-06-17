using System;
using System.Collections.Generic;
using System.Text;

namespace ScrumageEngine.Objects.Items {
    /// <summary>
    /// Testing Resource
    /// </summary>
    /// <seealso cref="ScrumageEngine.Objects.Items.Resource" />
    class Testing : Resource {
        private const String RESOURCE_NAME = "Testing";
        private const Int32 FULL_STACK_CHANCE = 25;
        private const Int32 FRONT_END_CHANCE = 5;
        private const Int32 BACK_END_CHANCE = 5;

        /// <summary>
        /// Initializes a new instance of the <see cref="Testing"/> class.
        /// </summary>
        public Testing() : base(RESOURCE_NAME)
        {
            this.FullStackChance = FULL_STACK_CHANCE;
            this.FrontEndChance = FRONT_END_CHANCE;
            this.BackEndChance = BACK_END_CHANCE;
        }
	}
}
