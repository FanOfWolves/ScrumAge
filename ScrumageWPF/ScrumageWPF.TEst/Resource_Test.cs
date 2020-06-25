using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using NUnit.Framework;
using ScrumageEngine.Objects;
using ScrumageEngine.BoardSpace;
using ScrumageEngine.Objects.Items;
namespace ScrumageWPF.Test {
    
    [TestFixture]
    public class Resource_Test {
        private List<Resource> resList = new List<Resource>(6);

        [SetUp]
        public void Resource_Setup() {
            Require
        }

        [Test]
        [Category("IEquatable<T> Interface")]
        public void Resource_EqualsAnotherBasedOnTypeAlone() {
            Assert.Fail();
        }

        [Test]
        [Category("Operators")]
        public void Resource_EqualsAnotherBasedOnNameAloneViaEqualityOperator() {

        }

        [Test]
        [Category("Operators")]
        public void Resource_DoesNotEqualAnotherBasedOnNameAloneViaInequalityOperator() {

        }

        [Test]
        [Category("Other")]
        public void Resource_PerfomsDeepCopy() {

        }


        [Test]
        [Category("Hierarchy")]
        public void Resource_GetsSubclassResourceRates() {
            Assert.Fail();
        }

        [Test]
        [Category("Hierarchy")]
        public void Resource_SetsNameCor
        
    }
}
