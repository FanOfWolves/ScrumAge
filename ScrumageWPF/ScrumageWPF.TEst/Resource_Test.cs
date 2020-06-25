using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using NUnit.Framework;
using ScrumageEngine.Objects;
using ScrumageEngine.BoardSpace;
using ScrumageEngine.Objects.Items;
namespace ScrumageWPF.Test {

    [TestFixture]
    public class Resource_Test {
        private List<Resource> resList = new List<Resource>(6);

        #region resList Indexors
        private const Int32 REQ = 0;
        private const Int32 DES = 1;
        private const Int32 IMP = 2;
        private const Int32 TES = 3;
        private const Int32 REQ2 = 4;
        private const Int32 DES2 = 5;
        #endregion

        [OneTimeSetUp]
        public void Resource_Setup() {
            Resource req = new Requirements();
            Resource req2 = new Requirements();
            Resource des = new Design();
            Resource imp = new Implementation();
            Resource tes = new Testing();

            this.resList.Add(req);
            this.resList.Add(des);
            this.resList.Add(imp);
            this.resList.Add(tes);
            this.resList.Add(req);
            this.resList.Add(req2);
            
        }

        #region Category: IEquatable<T> Interface
        [Test]
        [Category("IEquatable<T> Interface")]
        public void Resource_EqualsAnotherBasedOnTypeAlone() {
            // Equality should be based on type, no memory location.
            Assert.That(this.resList[REQ], Is.EqualTo(this.resList[REQ2]));
        }

        [Test]
        [Category("IEquatable<T> Interface")]
        public void Resource_TwoDifferentResourceTypesShouldNotEqual() {
            Resource res1 = new Requirements();
            Resource res2 = res1;
            res2.Name = "SomethingElse";

            Assert.AreNotEqual(res1, res2);
        }

        #endregion

        #region Category: Operators
        [Test]
        [Category("Operators")]
        public void Resource_EqualsAnotherBasedOnNameAloneViaEqualityOperator() {

        }

        [Test]
        [Category("Operators")]
        public void Resource_DoesNotEqualAnotherBasedOnNameAloneViaInequalityOperator() {

        }
        #endregion

        #region Category: Other
        [Test]
        [Category("Other")]
        public void Resource_PerfomsDeepCopy() {

        }
        #endregion

        #region Category: Hierarchy
        [Test]
        [Category("Hierarchy")]
        public void Resource_GetsSubclassResourceRates() {
            Assert.Fail();
        } 
        #endregion

    }
}
