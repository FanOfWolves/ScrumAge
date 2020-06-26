using System;
using System.Collections.Generic;
using System.Printing;
using System.Runtime.CompilerServices;
using System.Text;
using NUnit.Framework;
using ScrumageEngine.Objects.Items;
using ScrumageEngine.Objects.Player;
using ScrumageEngine.BoardSpace;
using ScrumageEngine;

namespace ScrumageWPF.Test {

    /// <summary>
    /// Tests for the ResourceContainer.cs class
    /// </summary>
    [TestFixture]
    class ResourceContainer_Test {

        #region Category: Instantiation
        [Test]
        [Category("Instantiation")]
        public void ResourceContainer_DefaultInstantiatesCorrectly() {
            ResourceContainer con1 = new ResourceContainer();
            Assert.That(con1[new Requirements()] == 0);
            Assert.That(con1[new Design()] == 0);
            Assert.That(con1[new Implementation()] == 0);
            Assert.That(con1[new Testing()] == 0);
        }

        [Test]
        [Category("Instantiation")]
        [TestCase(0, 0, 0, 0)]
        [TestCase(1, 0, 0, 0)]
        [TestCase(9, 4, 5, 6)]
        public void ResourceContainer_ParameterInstantiatesCorrectly(Int32 req, Int32 des, Int32 imp, Int32 tes) {
            ResourceContainer container = new ResourceContainer(new Int32[] { req, des, imp, tes });
            Assert.That(container[new Requirements()] == req);
            Assert.That(container[new Design()] == des);
            Assert.That(container[new Implementation()] == imp);
            Assert.That(container[new Testing()] == tes);
        }

        #endregion

        #region Category: Operators

        #region ResourceContainer_ComparisonOperatorsStateIfContainerHasEnoughResources
        [Test]
        [Category("Operators")]
        [TestCase(new Int32[] { 0, 0, 0, 0 }, new Int32[] { 0, 0, 0, 0 })]
        [TestCase(new Int32[] { 1, 1, 1, 1 }, new Int32[] { 0, 0, 0, 1 })]
        [TestCase(new Int32[] { 2, 3, 2, 2 }, new Int32[] { 2, 2, 2, 2 })]

        [TestCase(new Int32[] { 3, 5, 3, 3 }, new Int32[] { 2, 4, 2, 2 })]

        public void ResourceContainer_ComparisonOperatorsStateIfContainerHasEnoughResources(Int32[] inventory, Int32[] cost) {
            ResourceContainer payment = new ResourceContainer(inventory);
            ResourceContainer toPay = new ResourceContainer(cost);

            Assert.That(payment >= toPay);
        }
        #endregion

        #region ResourceContainer_ComparisonOperatorsStateIfContainerDoesNotHaveEnoughResources
        [Test]
        [TestCase(new Int32[] { 0, 0, 0, 0 }, new Int32[] { 1, 1, 0, 0 })]
        [TestCase(new Int32[] { 0, 0, 0, 0 }, new Int32[] { 1, 1, 1, 1 })]
        [TestCase(new Int32[] { 1, 1, 1, 1 }, new Int32[] { 1, 1, 1, 2 })]
        [TestCase(new Int32[] { 2, 3, 2, 2 }, new Int32[] { 2, 4, 2, 2 })]
        public void ResourceContainer_ComparisonOperatorsStateIfContainerDoesNotHaveEnoughResources(Int32[] inventory, Int32[] cost) {
            ResourceContainer payment = new ResourceContainer(inventory);
            ResourceContainer toPay = new ResourceContainer(cost);

            Assert.That(payment <= toPay);
        }
        #endregion

        #region ResourceContainer_ComparisonOperatorsDoNotAlterState
        [Test]
        [Category("Operators")]
        [TestCase(new Int32[] { 0, 0, 0, 0 }, new Int32[] { 0, 0, 0, 0 })]
        [TestCase(new Int32[] { 1, 1, 1, 1 }, new Int32[] { 0, 0, 0, 1 })]
        [TestCase(new Int32[] { 2, 3, 2, 2 }, new Int32[] { 2, 2, 2, 2 })]
        [TestCase(new Int32[] { 2, 3, 2, 2 }, new Int32[] { 2, 4, 2, 2 })]
        [TestCase(new Int32[] { 3, 5, 3, 3 }, new Int32[] { 2, 4, 2, 2 })]
        [TestCase(new Int32[] { 1, 1, 1, 1 }, new Int32[] { 1, 1, 1, 2 })]
        public void ResourceContainer_ComparisonOperatorsDoNotAlterState(Int32[] inventory, Int32[] cost) {
            ResourceContainer payment = new ResourceContainer(inventory);
            ResourceContainer toPay = new ResourceContainer(cost);
            if(payment >= toPay)
                ;

            Assert.That(payment[new Requirements()] == inventory[0]);
            Assert.That(payment[new Design()] == inventory[1]);
            Assert.That(payment[new Implementation()] == inventory[2]);
            Assert.That(payment[new Testing()] == inventory[3]);
            Assert.That(toPay[new Requirements()] == cost[0]);
            Assert.That(toPay[new Design()] == cost[1]);
            Assert.That(toPay[new Implementation()] == cost[2]);
            Assert.That(toPay[new Testing()] == cost[3]);
        }
        #endregion

        #region ResourceContainer_MinusOperatorRemovesFromThisResourceContainer
        [Test]
        [Category("Operators")]
        [TestCase(new Int32[] { 1, 3, 1, 4 }, new Int32[] { 1, 0, 1, 2 })]
        [TestCase(new Int32[] { 1, 3, 1, 4 }, new Int32[] { 0, 0, 0, 0 })]
        public void ResourceContainer_MinusOperatorRemovesFromThisResourceContainer() {

        }
        #endregion

        #region ResourceContainer_AdditionOperatorAddsToThisResourceContainer
        [Test]
        [Category("Operators")]
        public void ResourceContainer_AdditionOperatorAddsToThisResourceContainer() {

        }
        #endregion

        #endregion

        #region Category: Dictionary Handling

        #region ResourceContainer_AddsToExisting
        [Test]
        [Category("Dictionary Handling")]
        public void ResourceContainer_AddsToExisting() {

        }
        #endregion

        #region ResourceContainer_DoesNotRemoveResourceClass
        [Test]
        [Category("Dictionary Handling")]
        public void ResourceContainer_DoesNotRemoveResourceClass() {

        }
        #endregion

        #region ResourceContainer_IntegerIndexerRetrievesResourceAmount
        [Test]
        [Category("Dictionary Handling")]
        public void ResourceContainer_IntegerIndexerRetrievesResourceAmount() {

        }
        #endregion

        #endregion

        #region Category: IEnumerable<T> Interface

        #region ResourceContainer_IsIEnumerable
        [Test]
        [Category("IEnumerable<T> Interface")]
        public void ResourceContainer_IsIEnumerable() {

        }
        #endregion

        #endregion

        #region Category: Referencing and Equality

        #region ResourceContainer_ReturnsDeepCopyNotOriginalResource
        [Test]
        [Category("Referencing and Equality")]
        public void ResourceContainer_ReturnsDeepCopyNotOriginalResource() {

        } 
        #endregion

        #endregion

        #region Category: Other

        #endregion
    }
}
