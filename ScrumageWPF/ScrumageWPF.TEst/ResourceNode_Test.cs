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
        private const Int32 NUL = 4;


        private Resource CreateResource(Int32 type) {
            if(type == REQ)
                return new Requirements();
            else if(type == DES)
                return new Design();
            else if(type == IMP)
                return new Implementation();
            else if(type == TES)
                return new Testing();
            return null;
        }


        #region Category: Instantiation


        [Test]
        #region Test-Cases
        [TestCase(0, "Node1", REQ)]
        [TestCase(6, "Albert", DES)]
        [TestCase(888, "Node943", DES)]
        [TestCase(1, "null", IMP)]
        #endregion
        public void ResourceNode_ConstructorInstantiatesCorrectly(Int32 id, String name, Int32 resourceType) {
            Resource? theResource = CreateResource(resourceType);

            ResourceNode testNode = new ResourceNode(id, name, theResource);
            Assert.That(testNode.NodeID, Is.EqualTo(id));
            Assert.That(testNode.NodeName, Is.EqualTo(name));
            Assert.That(testNode.nodeResource, Is.EqualTo(theResource));
        }

        [Test]
        [TestCase(0, null, NUL)]
        [TestCase(0, "Albert", NUL)]
        [TestCase(0, null, DES)]
        public void ResourceNode_ConstructorCorrectlyHandlesNull(Int32 id, String name, Int32 resourceType) {
            Resource? theResource = CreateResource(resourceType);

            Assert.That(new ResourceNode(id, name, theResource), Throws.InstanceOf(typeof(ArgumentNullException)));
        }


        #endregion




    }
}
