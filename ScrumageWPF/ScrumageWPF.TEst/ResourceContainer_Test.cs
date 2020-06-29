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

        #region Fields



        #endregion


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

        #region ResourceContainer_GreaterThanOrEqualToComparesCorrectly
        [Test]
        [Category("Operators")]
        [TestCase(new Int32[] { 1, 1, 1, 1 }, new Int32[] { 0, 0, 0, 0 }, true)]     // Greater-than
        [TestCase(new Int32[] { 2, 3, 2, 2 }, new Int32[] { 2, 2, 2, 2 }, true)]     // Greater-than and equal-to
        [TestCase(new Int32[] { 0, 0, 0, 0 }, new Int32[] { 0, 0, 0, 0 }, true)]     // Equal-to, all zero
        [TestCase(new Int32[] { 2, 3, 6, 9 }, new Int32[] { 2, 3, 6, 9 }, true)]     // Equal-to
        [TestCase(new Int32[] { 1, 2, 3, 4 }, new Int32[] { 4, 3, 2, 1 }, false)]    // Ensure order matters
        [TestCase(new Int32[] { 3, 5, 2, 1 }, new Int32[] { 2, 4, 2, 2 }, false)]    // Less-than and equal-to
        [TestCase(new Int32[] { 3, 5, 2, 1 }, new Int32[] { 4, 6, 3, 2 }, false)]    // All less-than


        public void ResourceContainer_GreaterThanOrEqualToComparesCorrectly(Int32[] inventory, Int32[] cost, Boolean ExpectedResult) {
            ResourceContainer payment = new ResourceContainer(inventory);
            ResourceContainer toPay = new ResourceContainer(cost);
            
            Assert.That(payment >= toPay, Is.EqualTo(ExpectedResult));
        }

        #endregion

        #region ResourceContainer_GreaterThanOrEqualToComparesCorrectly
        [Test]
        [Category("Operators")]
        [TestCase(new Int32[] { 1, 1, 1, 1 }, new Int32[] { 0, 0, 0, 0 }, false)]   // Greater-than
        [TestCase(new Int32[] { 2, 3, 2, 2 }, new Int32[] { 2, 2, 2, 2 }, false)]   // Greater-than and equal-to
        [TestCase(new Int32[] { 0, 0, 0, 0 }, new Int32[] { 0, 0, 0, 0 }, true)]   // Equal-to, all zero
        [TestCase(new Int32[] { 2, 3, 6, 9 }, new Int32[] { 2, 3, 6, 9 }, true)]    // Equal-to
        [TestCase(new Int32[] { 1, 2, 3, 4 }, new Int32[] { 4, 3, 2, 1 }, true)]    // Ensure order matters
        [TestCase(new Int32[] { 3, 5, 2, 1 }, new Int32[] { 2, 4, 2, 2 }, true)]    // Less-than and equal-to
        [TestCase(new Int32[] { 3, 5, 2, 1 }, new Int32[] { 4, 6, 3, 2 }, true)]    // All less-than
        public void ResourceContainer_LessThanOrEqualToComparesCorrectly(Int32[] inventory, Int32[] cost, Boolean ExpectedResult) {
            ResourceContainer payment = new ResourceContainer(inventory);
            ResourceContainer toPay = new ResourceContainer(cost);

            Assert.That(payment <= toPay, Is.EqualTo(ExpectedResult));
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
        public void ResourceContainer_MinusOperatorRemovesFromThisResourceContainer(Int32[] inventory, Int32[] cost) {
            Int32[] result = new Int32[4];
            for(Int32 i = 0; i < 4; i++) {
                result[i] = inventory[i] - cost[i];
            }

            ResourceContainer weHave = new ResourceContainer(inventory);
            ResourceContainer toPay = new ResourceContainer(cost);

            weHave -= toPay;

            Assert.That(weHave[new Requirements()] == result[0]);
            Assert.That(weHave[new Design()] == result[1]);
            Assert.That(weHave[new Implementation()] == result[2]);
            Assert.That(weHave[new Testing()] == result[3]);
        }
        #endregion

        #region ResourceContainer_MinusOperatorDoesNotRemoveFromOtherResourceContainer
        [Test]
        [Category("Operators")]
        [TestCase(new Int32[] { 1, 3, 1, 4 }, new Int32[] { 1, 0, 1, 2 })]
        [TestCase(new Int32[] { 1, 3, 1, 4 }, new Int32[] { 0, 0, 0, 0 })]
        public void ResourceContainer_MinusOperatorDoesNotRemoveFromOtherResourceContainer(Int32[] inventory, Int32[] cost) {
            Int32[] result = new Int32[4];
            for(Int32 i = 0; i < 4; i++) {
                result[i] = inventory[i] - cost[i];
            }

            ResourceContainer weHave = new ResourceContainer(inventory);
            ResourceContainer toPay = new ResourceContainer(cost);

            weHave -= toPay;

            Assert.That(toPay[new Requirements()] == cost[0]);
            Assert.That(toPay[new Design()] == cost[1]);
            Assert.That(toPay[new Implementation()] == cost[2]);
            Assert.That(toPay[new Testing()] == cost[3]);
        }

        #endregion

        #region ResourceContainer_AdditionOperatorAddsToThisResourceContainer
        [Test]
        [Category("Operators")]
        [TestCase(new Int32[] { 1, 3, 1, 4 }, new Int32[] { 1, 0, 1, 2 })]
        [TestCase(new Int32[] { 1, 3, 1, 4 }, new Int32[] { 0, 0, 0, 0 })]
        public void ResourceContainer_AdditionOperatorAddsToThisResourceContainer(Int32[] inventory, Int32[] added) {
            Int32[] result = new Int32[4];
            for(Int32 i = 0; i < 4; i++) {
                result[i] = inventory[i] + added[i];
            }

            ResourceContainer weHave = new ResourceContainer(inventory);
            ResourceContainer weGet = new ResourceContainer(added);

            weHave += weGet;

            Assert.That(weHave[new Requirements()] == result[0]);
            Assert.That(weHave[new Design()] == result[1]);
            Assert.That(weHave[new Implementation()] == result[2]);
            Assert.That(weHave[new Testing()] == result[3]);
        }
        #endregion

        #region ResourceContainer_AdditionOperatorDoesNotAddToOtherResourceContainer
        [Test]
        [Category("Operators")]
        [TestCase(new Int32[] { 1, 3, 1, 4 }, new Int32[] { 1, 0, 1, 2 })]
        [TestCase(new Int32[] { 1, 3, 1, 4 }, new Int32[] { 0, 0, 0, 0 })]
        public void ResourceContainer_AdditionOperatorDoesNotAddToOtherResourceContainer(Int32[] inventory, Int32[] added) {

            ResourceContainer weHave = new ResourceContainer(inventory);
            ResourceContainer weGet = new ResourceContainer(added);

            weHave += weGet;

            Assert.That(weGet[new Requirements()] == added[0]);
            Assert.That(weGet[new Design()] == added[1]);
            Assert.That(weGet[new Implementation()] == added[2]);
            Assert.That(weGet[new Testing()] == added[3]);
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

        #region Category: Display        

        #region ResourceContainer_ShowRequirements_Parameterized
        /// <summary>
        /// Asserts that <see cref="ResourceContainer.ShowRequirements"/> correctly displays its contents.
        /// Uses parameterized constructor.
        /// </summary>
        /// <param name="resourceAmounts">The resource amounts to initialize the container.</param>
        [Test]
        [Category("Display")]
        [TestCase(new Int32[] { 1, 3, 4, 0 })]      // Normal check
        [TestCase(new Int32[] { 10, 10, 10, 10 })]  // Checking how it handles double digit numbers
        [TestCase(new Int32[] { 0, 0, 0, 0 })]      // All zero
        public void ResourceContainer_ShowRequirements_Parameterized(Int32[] resourceAmounts) {
            String expectedOutput = $"Requirements:{resourceAmounts[0]}\n" +
                                    $"Design:{resourceAmounts[1]}\n" +
                                    $"Implementation:{resourceAmounts[2]}\n" +
                                    $"Testing:{resourceAmounts[3]}\n";

            ResourceContainer testContainer = new ResourceContainer(resourceAmounts);

            Assert.That(expectedOutput, Is.EqualTo(testContainer.ShowRequirements()));
        }
        #endregion

        #region ResourceContainer_ShowRequirements_Default
        /// <summary>
        /// Asserts that <see cref="ResourceContainer.ShowRequirements"/> correctly displays its contents.
        /// Uses default constructor.
        /// </summary>
        [Test]
        [Category("Display")]
        public void ResourceContainer_ShowRequirements_Default() {
            String expectedOutput = $"Requirements:0\n" +
                                    $"Design:0\n" +
                                    $"Implementation:0\n" +
                                    $"Testing:0\n";

            ResourceContainer testContainer = new ResourceContainer();

            Assert.That(expectedOutput, Is.EqualTo(testContainer.ShowRequirements()));
        } 
        #endregion

        #endregion
    }
}
