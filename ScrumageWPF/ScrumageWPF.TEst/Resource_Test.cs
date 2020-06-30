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

        #region Resource_EqualsAnotherBasedOnTypeAloneViaResource        
        /// <summary>
        /// Asserts that <see cref="Resource.Equals(Resource)"/> correctly identifies same resource types.
        /// </summary>
        [Test]
        [Category("IEquatable<T> Interface")]
        public void Resource_EqualsAnotherBasedOnTypeAloneViaResource() {
            // Equality should be based on type, not memory location.
            Assert.That(this.resList[REQ], Is.EqualTo(this.resList[REQ2]));
        } 
        #endregion

        #region Resource_TwoDifferentResourceTypesShouldNotEqual        
        /// <summary>
        /// Asserts that <see cref="Resource.Equals(Resource)"/> correctly identifies different resource types.
        /// </summary>
        [Test]
        [Category("IEquatable<T> Interface")]
        public void Resource_TwoDifferentResourceTypesShouldNotEqualViaResource() {
            Resource res1 = new Requirements();
            Resource res2 = new Design();

            Assert.That(res1, Is.Not.EqualTo(res2));
            Assert.That(res1, Is.Not.SameAs(res2));
        }
        #endregion

        #region Resource_ResourceEqualsHandlesNulls
        /// <summary>
        /// Asserts that <see cref="Resource.Equals(Resource)"/> correctly handles null input.
        /// </summary>
        [Test]
        [Category("IEquatable<T> Interface")]
        public void Resource_ResourceEqualsHandlesNulls() {
            Resource res1 = new Requirements();
            Resource res2 = null;
            Assert.That(res1.Equals(res2), Is.False);
        }
        #endregion

        #region Resource_TwoDifferentResourceTypesShouldNotEqualViaObject        
        /// <summary>
        /// Asserts that <see cref="Resource.Equals(Object)"/> correctly identifies resources of different types.
        /// </summary>
        [Test]
        [Category("IEquatable<T> Interface")]
        public void Resource_TwoDifferentResourceTypesShouldNotEqualViaObject() {
            Object res1 = new Requirements();
            Object res2 = null;
            Assert.That(res1.Equals(res2), Is.False);
        }
        #endregion

        #region Resource_EqualsAnotherBasedOnTypeAloneViaObject        
        /// <summary>
        /// Asserts that <see cref="Resource.Equals(Object)"/> correctly identifies resources of same type.
        /// </summary>
        [Test]
        [Category("IEquatable<T> Interface")]
        public void Resource_EqualsAnotherBasedOnTypeAloneViaObject() {
            // Equality should be based on type, not memory location.
            Object res1 = new Requirements();
            Object res2 = new Requirements();
            Assert.That(res1.Equals(res2), Is.True);
        }

        #endregion

        #region Resource_ObjectEqualsHandlesNulls
        /// <summary>
        /// Asserts that <see cref="Resource.Equals(Object)"/> correctly handles null input.
        /// </summary>
        [Test]
        [Category("IEquatable<T> Interface")]
        public void Resource_ObjectEqualsHandlesNulls() {
            Object res1 = new Requirements();
            Object res2 = null;
            Assert.That(res1.Equals(res2), Is.False);
        }
        #endregion

        #endregion

        #region Category: Operators

        #region Resource_EqualsAnotherBasedOnNameAloneViaEqualityOperator        
        /// <summary>
        /// Asserts that "==" operator compares <see cref="Resource.Name"/> properties correctly.
        /// </summary>
        [Test]
        [Category("Operators")]
        public void Resource_EqualsAnotherBasedOnNameAloneViaEqualityOperator() {
            Assert.That(this.resList[REQ] == this.resList[REQ2]);
            Assert.That(this.resList[REQ] == this.resList[DES], Is.False);
            Assert.That(this.resList[REQ] == null, Is.False);
            Assert.That(null == this.resList[REQ], Is.False);
        }
        #endregion

        #region Resource_DoesNotEqualAnotherBasedOnNameAloneViaInequalityOperator
        /// <summary>
        /// Asserts that "!=" operator compares <see cref="Resource.Name"/> properties correctly.
        /// </summary>
        [Test]
        [Category("Operators")]
        public void Resource_DoesNotEqualAnotherBasedOnNameAloneViaInequalityOperator() {
            Assert.That(this.resList[REQ] != this.resList[DES], Is.True);
            Assert.That(this.resList[REQ] != this.resList[REQ2], Is.False);
            Assert.That(this.resList[REQ] != null, Is.True);
            Assert.That(null != this.resList[REQ], Is.True);
        } 
        #endregion

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

        [Test]
        [Category("Hierarchy")]
        public void Resource_GetChanceReturnsCorrectResourceChanceRates() {
            Pawn fullPawn = new Pawn(1,"Full Stack");
            Pawn frontPawn = new Pawn(1, "Front End");
            Pawn backPawn = new Pawn(1, "Back End");

            Assert.That(this.resList[REQ].GetChance(fullPawn), Is.EqualTo(REQ_FULL_STACK_CHANCE));
            Assert.That(this.resList[REQ].GetChance(frontPawn), Is.EqualTo(REQ_FRONT_END_CHANCE));
            Assert.That(this.resList[REQ].GetChance(backPawn), Is.EqualTo(REQ_BACK_END_CHANCE));

            Assert.That(this.resList[DES].GetChance(fullPawn), Is.EqualTo(DESIGN_FULL_STACK_CHANCE));
            Assert.That(this.resList[DES].GetChance(frontPawn), Is.EqualTo(DESIGN_FRONT_END_CHANCE));
            Assert.That(this.resList[DES].GetChance(backPawn), Is.EqualTo(DESIGN_BACK_END_CHANCE));

            Assert.That(this.resList[IMP].GetChance(fullPawn), Is.EqualTo(IMP_FULL_STACK_CHANCE));
            Assert.That(this.resList[IMP].GetChance(frontPawn), Is.EqualTo(IMP_FRONT_END_CHANCE));
            Assert.That(this.resList[IMP].GetChance(backPawn), Is.EqualTo(IMP_BACK_END_CHANCE));

            Assert.That(this.resList[TES].GetChance(fullPawn), Is.EqualTo(TEST_FULL_STACK_CHANCE));
            Assert.That(this.resList[TES].GetChance(frontPawn), Is.EqualTo(TEST_FRONT_END_CHANCE));
            Assert.That(this.resList[TES].GetChance(backPawn), Is.EqualTo(TEST_BACK_END_CHANCE));
        }

        #endregion

    }
}
