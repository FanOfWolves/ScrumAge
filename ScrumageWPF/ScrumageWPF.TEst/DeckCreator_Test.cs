using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using ScrumageEngine.Objects.Items.Cards;
namespace ScrumageWPF.Test {

    /// <summary>
    /// Testing suite for DeckCreator.cs
    /// </summary>
    [TestFixture]
    class DeckCreator_Test {
        private List<String> testCardInfoList = new List<String>(7);

        #region TestFields
        private String cardInfo1 = "Artifact:Customer Meeting Design:2,1,0,0\n";
        private String cardInfo2 = "Artifact:System Test Plan:0,1,1,1\n";
        private String cardInfo3 = "Artifact:Class Diagram:2,3,4,5\n";
        private String cardInfo4 = "Agility:New Code Standards:1,1,1,1\n";
        private String cardInfo5 = "Agility:New Law Passed:1,1,1,1\n";
        private String cardInfo6 = "Agility:Temporary Budget Reduction:1,1,1,1\n";
        private String cardInfo7 = "Agility:Update To A New Framework:1,1,1,1\n";
        #endregion

        #region Setup and Teardown
        [OneTimeSetUp]
        public void Class_SetUp() {
            testCardInfoList.Add(cardInfo1);
            testCardInfoList.Add(cardInfo2);
            testCardInfoList.Add(cardInfo3);
            testCardInfoList.Add(cardInfo4);
            testCardInfoList.Add(cardInfo5);
            testCardInfoList.Add(cardInfo6);
            testCardInfoList.Add(cardInfo7);
        }
        #endregion

        [Test]
        public void DeckCreator_ReadCards_ReturnsExpectedOutput() {
            List<String> outputList = DeckCreator.ReadCards();
            String test1 = testCardInfoList[0];
            String test2 = outputList[0];
            Assert.That(test1, Is.EqualTo(test2));
            //Assert.That(testCardInfoList, Is.EquivalentTo(outputList));
        }

    }
}
