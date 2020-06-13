using System;
using System.Collections.Generic;
using System.Text;

namespace ScrumageEngine.Objects.Items {
	class Testing: Resource {
        private const Int32 fullStackChance = 25;
        private const Int32 frontEndChance = 5;
        private const Int32 backEndChance = 5;

        /// <summary>
        /// Initializes a new instance of the <see cref="Testing"/> class.
        /// </summary>
        /// <param name="name">The name of the resource</param>
        public Testing(String name) : base(name)
        {
            Name = name;
            FullStackChance = fullStackChance;
            FrontEndChance = frontEndChance;
            BackEndChance = backEndChance;
        }

        public override Int32 GetChance(Pawn p)
        {
            throw new NotImplementedException();
        }
    }
}
