using System;
using System.Collections.Generic;
using System.Text;
using ScrumageEngine.Objects.Humans;
using ScrumageEngine.Objects.Items;

namespace ScrumageEngine.BoardSpace {
	public class ResourceNode : Node { //D:

		private Resource nodeResource;
		
		
		public ResourceNode(Int32 nodeID, String nodeName):base(nodeID, nodeName) {

		}

		public override String DoAction(Player player) {
			//throw new NotImplementedException();
			return ""; // This gets added to log
		}
	}
}
