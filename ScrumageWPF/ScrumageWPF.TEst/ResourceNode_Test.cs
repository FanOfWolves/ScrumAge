using System;
using System.Collections.Generic;
using System.Text;
using ScrumageEngine.BoardSpace;
using ScrumageEngine.Objects.Items;
using ScrumageEngine.Objects.Player;
using NUnit.Framework;
namespace ScrumageWPF.Test {

    /// <summary>
    /// Testing class for <see cref="ResourceNode"/>
    /// </summary>
    [TestFixture]
    class ResourceNode_Test {

        private const Int32 REQ = 0;
        private const Int32 DES = 1;
        private const Int32 IMP = 2;
        private const Int32 TES = 3;


        private Resource CreateResource(Int32 type) {
            if(type == REQ)
                return new Requirements();
            else if(type == DES)
                return new Design();
            else if(type == IMP)
                return new Implementation();
            return new Testing();
        }


        #region Category: Instantiation
        
        
        [Test]
        #region Test-Cases
        [TestCase(0, "Node1", typeof(Implementation))]
        [TestCase(6, "Albert", typeof(Implementation))]
        [TestCase(888, "Node943", typeof(Implementation))]

        #endregion
        public void ResourceNode_ConstructorInstantiatesCorrectly(Int32 id, String name, Resource theResource) {
            Node testNode = new ResourceNode(id, name, theResource);
            Assert.That(theResource, Is.EqualTo(new Implementation()));
        }
        
            


        #endregion




    }
}
