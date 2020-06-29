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
        /// <summary>
        /// Assert that ">=" operator behaves correctly.
        /// </summary>
        /// <param name="res1Contents">The contents of the left-side container.</param>
        /// <param name="res2Contents">The contents of the right-side container.</param>
        /// <param name="ExpectedResult">The expected boolean result for the assert statement.</param>
        [Test]
        [Category("Operators")]
        #region Test-Cases
        [TestCase(new Int32[] { 1, 1, 1, 1 }, new Int32[] { 0, 0, 0, 0 }, true)]     // Greater-than
        [TestCase(new Int32[] { 2, 3, 2, 2 }, new Int32[] { 2, 2, 2, 2 }, true)]     // Greater-than and equal-to
        [TestCase(new Int32[] { 0, 0, 0, 0 }, new Int32[] { 0, 0, 0, 0 }, true)]     // Equal-to, all zero
        [TestCase(new Int32[] { 2, 3, 6, 9 }, new Int32[] { 2, 3, 6, 9 }, true)]     // Equal-to
        [TestCase(new Int32[] { 1, 2, 3, 4 }, new Int32[] { 4, 3, 2, 1 }, false)]    // Ensure order matters
        [TestCase(new Int32[] { 2, 4, 1, 1 }, new Int32[] { 2, 4, 2, 2 }, false)]    // Less-than and equal-to
        [TestCase(new Int32[] { 3, 5, 2, 1 }, new Int32[] { 4, 6, 3, 2 }, false)]    // All less-than
        #endregion
        public void ResourceContainer_GreaterThanOrEqualToComparesCorrectly(Int32[] res1Contents, Int32[] res2Contents, Boolean ExpectedResult) {
            ResourceContainer res1 = new ResourceContainer(res1Contents);
            ResourceContainer res2 = new ResourceContainer(res2Contents);
            
            Assert.That(res1 >= res2, Is.EqualTo(ExpectedResult));
        }

        #endregion

        #region ResourceContainer_LessThanOrEqualToComparesCorrectly        
        /// <summary>
        /// Assert that "<=" operator behaves correctly.
        /// </summary>
        /// <param name="res1Contents">The contents of the left-side container.</param>
        /// <param name="res2Contents">The contents of the right-side container.</param>
        /// <param name="ExpectedResult">The expected boolean result for the assert statement.</param>
        [Test]
        [Category("Operators")]
        #region Test-Cases
        [TestCase(new Int32[] { 1, 1, 1, 1 }, new Int32[] { 0, 0, 0, 0 }, false)]   // Greater-than
        [TestCase(new Int32[] { 2, 3, 2, 2 }, new Int32[] { 2, 2, 2, 2 }, false)]   // Greater-than and equal-to
        [TestCase(new Int32[] { 0, 0, 0, 0 }, new Int32[] { 0, 0, 0, 0 }, true)]    // Equal-to, all zero
        [TestCase(new Int32[] { 2, 3, 6, 9 }, new Int32[] { 2, 3, 6, 9 }, true)]    // Equal-to
        [TestCase(new Int32[] { 1, 2, 3, 4 }, new Int32[] { 4, 3, 2, 1 }, false)]   // Ensure order matters
        [TestCase(new Int32[] { 2, 4, 1, 1 }, new Int32[] { 2, 4, 2, 2 }, true)]    // Less-than and equal-to
        [TestCase(new Int32[] { 3, 5, 2, 1 }, new Int32[] { 4, 6, 3, 2 }, true)]    // All less-than 
        #endregion
        public void ResourceContainer_LessThanOrEqualToComparesCorrectly(Int32[] res1Contents, Int32[] res2Contents, Boolean ExpectedResult) {
            ResourceContainer res1 = new ResourceContainer(res1Contents);
            ResourceContainer res2 = new ResourceContainer(res2Contents);

            Assert.That(res1 <= res2, Is.EqualTo(ExpectedResult));
        }


        #endregion

        #region ResourceContainer_GreaterThanComparesCorrectly
        /// <summary>
        /// Assert that ">" operator behaves correctly.
        /// </summary>
        /// <param name="res1Contents">The contents of the left-side container.</param>
        /// <param name="res2Contents">The contents of the right-side container.</param>
        /// <param name="ExpectedResult">The expected boolean result for the assert statement.</param>
        [Test]
        [Category("Operators")]
        #region Test-Cases
        [TestCase(new Int32[] { 1, 1, 1, 1 }, new Int32[] { 0, 0, 0, 0 }, true)]     // Greater-than
        [TestCase(new Int32[] { 2, 3, 2, 2 }, new Int32[] { 2, 2, 2, 2 }, false)]     // Greater-than and equal-to
        [TestCase(new Int32[] { 0, 0, 0, 0 }, new Int32[] { 0, 0, 0, 0 }, false)]    // Equal-to, all zero
        [TestCase(new Int32[] { 2, 3, 6, 9 }, new Int32[] { 2, 3, 6, 9 }, false)]    // Equal-to
        [TestCase(new Int32[] { 1, 2, 3, 4 }, new Int32[] { 4, 3, 2, 1 }, false)]    // Ensure order matters
        [TestCase(new Int32[] { 2, 4, 1, 1 }, new Int32[] { 2, 4, 2, 2 }, false)]    // Less-than and equal-to
        [TestCase(new Int32[] { 3, 5, 2, 1 }, new Int32[] { 4, 6, 3, 2 }, false)]    // All less-than 
        #endregion
        public void ResourceContainer_GreaterThanComparesCorrectly(Int32[] res1Contents, Int32[] res2Contents, Boolean ExpectedResult) {
            ResourceContainer res1 = new ResourceContainer(res1Contents);
            ResourceContainer res2 = new ResourceContainer(res2Contents);

            Assert.That(res1 > res2, Is.EqualTo(ExpectedResult));
        }
        #endregion

        #region ResourceContainer_LessThanComparesCorrectly        
        /// <summary>
        /// Assert that "<" operator behaves correctly.
        /// </summary>
        /// <param name="res1Contents">The contents of the left-side container.</param>
        /// <param name="res2Contents">The contents of the right-side container.</param>
        /// <param name="ExpectedResult">The expected boolean result for the assert statement.</param>
        [Test]
        [Category("Operators")]
        #region Test-Cases
        [TestCase(new Int32[] { 1, 1, 1, 1 }, new Int32[] { 0, 0, 0, 0 }, false)]   // Greater-than
        [TestCase(new Int32[] { 2, 3, 2, 2 }, new Int32[] { 2, 2, 2, 2 }, false)]   // Greater-than and equal-to
        [TestCase(new Int32[] { 0, 0, 0, 0 }, new Int32[] { 0, 0, 0, 0 }, false)]   // Equal-to, all zero
        [TestCase(new Int32[] { 2, 3, 6, 9 }, new Int32[] { 2, 3, 6, 9 }, false)]   // Equal-to
        [TestCase(new Int32[] { 1, 2, 3, 4 }, new Int32[] { 4, 3, 2, 1 }, false)]   // Ensure order matters
        [TestCase(new Int32[] { 2, 4, 1, 1 }, new Int32[] { 2, 4, 2, 2 }, false)]   // Less-than and equal-to
        [TestCase(new Int32[] { 3, 5, 2, 1 }, new Int32[] { 4, 6, 3, 2 }, true)]    // All less-than 
        #endregion
        public void ResourceContainer_LessThanComparesCorrectly(Int32[] res1Contents, Int32[] res2Contents, Boolean ExpectedResult) {
            ResourceContainer res1 = new ResourceContainer(res1Contents);
            ResourceContainer res2 = new ResourceContainer(res2Contents);

            Assert.That(res1 < res2, Is.EqualTo(ExpectedResult));
        }
        #endregion

        #region ResourceContainer_ComparisonOperatorsDoNotAlterState        
        /// <summary>
        /// Assert that comparison operators do not alter the contents of the compared containers.
        /// </summary>
        /// <param name="res1Contents">The contents of the left-side container.</param>
        /// <param name="res2Contents">The contents of the right-side container.</param>
        [Test]
        [Category("Operators")]
        #region Test-Cases
        [TestCase(new Int32[] { 1, 1, 1, 1 }, new Int32[] { 0, 0, 0, 0 })]
        [TestCase(new Int32[] { 2, 3, 2, 2 }, new Int32[] { 2, 2, 2, 2 })]   
        [TestCase(new Int32[] { 0, 0, 0, 0 }, new Int32[] { 0, 0, 0, 0 })]    
        #endregion
        public void ResourceContainer_ComparisonOperatorsDoNotAlterState(Int32[] res1Contents, Int32[] res2Contents) {
            ResourceContainer res1 = new ResourceContainer(res1Contents);
            ResourceContainer res2 = new ResourceContainer(res2Contents);
            if(res1 >= res2 || res1 > res2 || res1 <= res2 || res1 < res2)
                ;// Intentionally left blank

            Assert.That(res1[new Requirements()] == res1Contents[0]);
            Assert.That(res1[new Design()] == res1Contents[1]);
            Assert.That(res1[new Implementation()] == res1Contents[2]);
            Assert.That(res1[new Testing()] == res1Contents[3]);
            Assert.That(res2[new Requirements()] == res2Contents[0]);
            Assert.That(res2[new Design()] == res2Contents[1]);
            Assert.That(res2[new Implementation()] == res2Contents[2]);
            Assert.That(res2[new Testing()] == res2Contents[3]);
        }
        #endregion

        #region ResourceContainer_MinusOperatorRemovesFromThisResourceContainer        
        /// <summary>
        /// Assert that "-" operator removes resources from left-side container.
        /// </summary>
        /// <param name="res1Contents">The contents of the left-side container.</param>
        /// <param name="res2Contents">The contents of the right-side container.</param>
        [Test]
        [Category("Operators")]
        #region Test-Cases
        [TestCase(new Int32[] { 1, 3, 1, 4 }, new Int32[] { 1, 0, 1, 2 })]
        [TestCase(new Int32[] { 1, 3, 1, 4 }, new Int32[] { 0, 0, 0, 0 })] 
        #endregion
        public void ResourceContainer_MinusOperatorRemovesFromThisResourceContainer(Int32[] res1Contents, Int32[] res2Contents) {
            Int32[] result = new Int32[4];
            for(Int32 i = 0; i < 4; i++) {
                result[i] = res1Contents[i] - res2Contents[i];
            }

            ResourceContainer res1 = new ResourceContainer(res1Contents);
            ResourceContainer res2 = new ResourceContainer(res2Contents);

            res1 -= res2;

            Assert.That(res1[new Requirements()] == result[0]);
            Assert.That(res1[new Design()] == result[1]);
            Assert.That(res1[new Implementation()] == result[2]);
            Assert.That(res1[new Testing()] == result[3]);
        }
        #endregion

        #region ResourceContainer_MinusOperatorDoesNotRemoveFromOtherResourceContainer        
        /// <summary>
        /// Asserts that "-" operator does not remove from right-side resource container.
        /// </summary>
        /// <param name="res1Contents">The contents of the left-side container.</param>
        /// <param name="res2Contents">The contents of the right-side container.</param>
        [Test]
        [Category("Operators")]
        #region Test-Cases
        [TestCase(new Int32[] { 1, 3, 1, 4 }, new Int32[] { 1, 0, 1, 2 })]
        [TestCase(new Int32[] { 1, 3, 1, 4 }, new Int32[] { 0, 0, 0, 0 })] 
        #endregion
        public void ResourceContainer_MinusOperatorDoesNotRemoveFromOtherResourceContainer(Int32[] res1Contents, Int32[] res2Contents) {
            Int32[] result = new Int32[4];
            for(Int32 i = 0; i < 4; i++) {
                result[i] = res1Contents[i] - res2Contents[i];
            }

            ResourceContainer res1 = new ResourceContainer(res1Contents);
            ResourceContainer res2 = new ResourceContainer(res2Contents);

            res1 -= res2;

            Assert.That(res2[new Requirements()] == res2Contents[0]);
            Assert.That(res2[new Design()] == res2Contents[1]);
            Assert.That(res2[new Implementation()] == res2Contents[2]);
            Assert.That(res2[new Testing()] == res2Contents[3]);
        }

        #endregion

        #region ResourceContainer_AdditionOperatorAddsToThisResourceContainer        
        /// <summary>
        /// Asserts that "+" operator adds content to the left-side resource container.
        /// </summary>
        /// <param name="res1Contents">The contents of the left-side container.</param>
        /// <param name="res2Contents">The contents of the right-side container.</param>
        [Test]
        [Category("Operators")]
        #region Test-Cases
        [TestCase(new Int32[] { 1, 3, 1, 4 }, new Int32[] { 1, 0, 1, 2 })]
        [TestCase(new Int32[] { 1, 3, 1, 4 }, new Int32[] { 0, 0, 0, 0 })] 
        #endregion
        public void ResourceContainer_AdditionOperatorAddsToThisResourceContainer(Int32[] res1Contents, Int32[] res2Contents) {
            Int32[] result = new Int32[4];
            for(Int32 i = 0; i < 4; i++) {
                result[i] = res1Contents[i] + res2Contents[i];
            }

            ResourceContainer res1 = new ResourceContainer(res1Contents);
            ResourceContainer res2 = new ResourceContainer(res2Contents);

            res1 += res2;

            Assert.That(res1[new Requirements()] == result[0]);
            Assert.That(res1[new Design()] == result[1]);
            Assert.That(res1[new Implementation()] == result[2]);
            Assert.That(res1[new Testing()] == result[3]);
        }
        #endregion

        #region ResourceContainer_AdditionOperatorDoesNotAddToOtherResourceContainer        
        /// <summary>
        /// Asserts that "+" operator does not alter the right-side resource container.
        /// </summary>
        /// <param name="res1Contents">The contents of the left-side container.</param>
        /// <param name="res2Contents">The contents of the right-side container.</param>
        [Test]
        [Category("Operators")]
        #region Test-Cases
        [TestCase(new Int32[] { 1, 3, 1, 4 }, new Int32[] { 1, 0, 1, 2 })]
        [TestCase(new Int32[] { 1, 3, 1, 4 }, new Int32[] { 0, 0, 0, 0 })] 
        #endregion
        public void ResourceContainer_AdditionOperatorDoesNotAddToOtherResourceContainer(Int32[] res1Contents, Int32[] res2Contents) {

            ResourceContainer res1 = new ResourceContainer(res1Contents);
            ResourceContainer res2 = new ResourceContainer(res2Contents);

            res1 += res2;

            Assert.That(res2[new Requirements()] == res2Contents[0]);
            Assert.That(res2[new Design()] == res2Contents[1]);
            Assert.That(res2[new Implementation()] == res2Contents[2]);
            Assert.That(res2[new Testing()] == res2Contents[3]);
        }
        #endregion

        #endregion

        #region Category: Collection Manipulation
        #region ResourceContainer_TakeResources_RemovesSpecifiedResourceByAmount
        /// <summary>
        /// Asserts that <see cref="ResourceContainer.TakeResources(Resource, Int32)"/> performs as expected.
        /// </summary>
        /// <param name="resourceType">Type of the resource.</param>
        /// <param name="amountToTake">The amount to take.</param>
        /// <param name="expectedBoolean">Expected TakeResource return boolean.</param>
        /// <exception cref="NotImplementedException">Invalid test case.</exception>
        [Test]
        [Category("Collection Manipulation")]
        #region Test-Cases
        [TestCase(0, 0, true)]
        [TestCase(0, 2, true)]
        [TestCase(0, 3, true)]
        [TestCase(0, 4, false)]
        [TestCase(1, 2, true)]
        [TestCase(1, 3, false)]
        [TestCase(2, 0, true)]
        [TestCase(2, 1, true)]
        [TestCase(2, 2, false)]
        [TestCase(3, 4, false)]
        [TestCase(3, 0, true)]
        #endregion
        public void ResourceContainer_TakeResources_RemovesSpecifiedResourceByAmount(Int32 resourceType, Int32 amountToTake, Boolean expectedBoolean) {
            Resource whatToTake = null;
            if(resourceType == 0)
                whatToTake = new Requirements();
            else if(resourceType == 1)
                whatToTake = new Design();
            else if(resourceType == 2)
                whatToTake = new Implementation();
            else if(resourceType == 3)
                whatToTake = new Testing();
            else
                throw new NotImplementedException("Invalid test case.");

            Int32[] originalContents = { 3, 2, 1, 0 };
            ResourceContainer testContainer = new ResourceContainer(originalContents);

            Boolean didTake = testContainer.TakeResources(whatToTake, amountToTake);

            Assert.That(didTake, Is.EqualTo(expectedBoolean));
            if(expectedBoolean == true)
                Assert.That(testContainer[whatToTake], Is.EqualTo(originalContents[resourceType] - amountToTake));
            else {
                Assert.That(testContainer[new Requirements()], Is.EqualTo(originalContents[0]));
                Assert.That(testContainer[new Design()], Is.EqualTo(originalContents[1]));
                Assert.That(testContainer[new Implementation()], Is.EqualTo(originalContents[2]));
                Assert.That(testContainer[new Testing()], Is.EqualTo(originalContents[3]));
            }
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
