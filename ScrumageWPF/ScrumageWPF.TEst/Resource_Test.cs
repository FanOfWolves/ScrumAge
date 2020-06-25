using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using NUnit.Framework;
using ScrumageEngine.Objects;
using ScrumageEngine.BoardSpace;
using ScrumageEngine.Objects.Items;
namespace ScrumageWPF.Test {

    /// <summary>
    /// For testing the Resource class and its subclasses
    /// </summary>
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

        #region Resource Success Chances
        private const Int32 DESIGN_FULL_STACK_CHANCE = 20;
        private const Int32 DESIGN_FRONT_END_CHANCE = 15;
        private const Int32 DESIGN_BACK_END_CHANCE = 15;
        private const Int32 TEST_FULL_STACK_CHANCE = 25;
        private const Int32 TEST_FRONT_END_CHANCE = 5;
        private const Int32 TEST_BACK_END_CHANCE = 5;
        private const Int32 REQ_FULL_STACK_CHANCE = 20;
        private const Int32 REQ_FRONT_END_CHANCE = 20;
        private const Int32 REQ_BACK_END_CHANCE = 10;
        private const Int32 IMP_FULL_STACK_CHANCE = 20;
        private const Int32 IMP_FRONT_END_CHANCE = 10;
        private const Int32 IMP_BACK_END_CHANCE = 20;
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
            Resource res2 = new Design();
            
            Assert.That(res1, Is.Not.EqualTo(res2));
            Assert.That(res1, Is.Not.SameAs(res2));
        }

        #endregion

        #region Category: Operators
        [Test]
        [Category("Operators")]
        public void Resource_EqualsAnotherBasedOnNameAloneViaEqualityOperator() {
            Assert.That(this.resList[REQ] == this.resList[REQ2]);
        }

        [Test]
        [Category("Operators")]
        public void Resource_DoesNotEqualAnotherBasedOnNameAloneViaInequalityOperator() {
            Assert.That(this.resList[REQ] != this.resList[DES]);
        }
        #endregion

        #region Category: Other
        [Test]
        [Category("Other")]
        public void Resource_PerformsDeepCopy() {
            Resource res1 = new Requirements();
            Resource res2 = res1.DeepCopy();
            Assert.That(res1, Is.Not.SameAs(res2));
            Assert.That(res1, Is.EqualTo(res2));
        }
        #endregion

        #region Category: Hierarchy
        [Test]
        [Category("Hierarchy")]
        public void Resource_GetsSubclassResourceRates() {
            Assert.That(this.resList[REQ].BackEndChance == REQ_BACK_END_CHANCE);
            Assert.That(this.resList[REQ].FrontEndChance == REQ_FRONT_END_CHANCE);
            Assert.That(this.resList[REQ].FullStackChance == REQ_FULL_STACK_CHANCE);            
            Assert.That(this.resList[DES].BackEndChance == DESIGN_BACK_END_CHANCE);
            Assert.That(this.resList[DES].FrontEndChance == DESIGN_FRONT_END_CHANCE);
            Assert.That(this.resList[DES].FullStackChance == DESIGN_FULL_STACK_CHANCE);
            Assert.That(this.resList[TES].BackEndChance == TEST_BACK_END_CHANCE);
            Assert.That(this.resList[TES].FrontEndChance == TEST_FRONT_END_CHANCE);
            Assert.That(this.resList[TES].FullStackChance == TEST_FULL_STACK_CHANCE);
            Assert.That(this.resList[IMP].BackEndChance == IMP_BACK_END_CHANCE);
            Assert.That(this.resList[IMP].FrontEndChance == IMP_FRONT_END_CHANCE);
            Assert.That(this.resList[IMP].FullStackChance == IMP_FULL_STACK_CHANCE);
        } 
        #endregion

    }
}
