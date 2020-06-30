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


        #region Category: Instantiation
        
        
        [Test]
        #region Test-Cases
        [TestCase(0, "Node1")]
        [TestCase(6, "Albert")]
        [TestCase(888, "Node943")]
        [TestCase()]
        [TestCase(0, null)]
        #endregion
        public void ResourceNode_ConstructorInstantiatesCorrectly(Int32 id, String name) {

        }
        
            
        #endregion




    }
}
