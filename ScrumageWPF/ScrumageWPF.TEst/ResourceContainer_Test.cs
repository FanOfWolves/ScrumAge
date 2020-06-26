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
        #endregion

        #region Category: Operators
        [Test]
        [Category("Operators")]
        public void ResourceContainer_ComparisonOperatorsCorrectlyStateIfContainerHasEnoughResources() {

        }

        [Test]
        [Category("Operators")]
        public void ResourceContainer_MinusOperatorRemovesFromThisResourceContainer() {

        }

        [Test]
        [Category("Operators")]
        public void ResourceContainer_AdditionOperatorAddsToThisResourceContainer() {

        }
        #endregion

        #region Category: Dictionary Handling
        [Test]
        [Category("Dictionary Handling")]
        public void ResourceContainer_AddsToExisting() {

        }

        [Test]
        [Category("Dictionary Handling")]
        public void ResourceContainer_DoesNotRemoveResourceClass() {

        }

        [Test]
        [Category("Dictionary Handling")]
        public void ResourceContainer_IntegerIndexerRetrievesResourceAmount() {

        }
        #endregion

        #region Category: IEnumerable<T> Interface
        [Test]
        [Category("IEnumerable<T> Interface")]
        public void ResourceContainer_IsIEnumerable() {

        }
        #endregion

        #region Category: Referencing and Equality
        [Test]
        [Category("Referencing and Equality")]
        public void ResourceContainer_ReturnsDeepCopyNotOriginalResource() {

        }
        #endregion

        #region Category: Other

        #endregion
    }
}
