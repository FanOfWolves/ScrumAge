using System;
using System.Collections.Generic;
using System.Text;

namespace ScrumageEngine.Objects.Items {
	/// <summary>
	/// 
	/// </summary>
	public class Design : Resource {
        private const Int32 fullStackChance = 20;
        private const Int32 frontEndChance = 20;
        private const Int32 backEndChance = 10;


        /// <summary>
        /// Initializes a new instance of the <see cref="Design"/> class.
        /// </summary>
        /// <param name="name">The name of the resource</param>
        public Design(String name):base(name) {
            Name = name;
            FullStackChance = fullStackChance;
            FrontEndChance = frontEndChance;
            BackEndChance = backEndChance;
		}
	}
}