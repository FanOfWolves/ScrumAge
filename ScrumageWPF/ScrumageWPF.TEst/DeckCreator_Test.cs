﻿using System;
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
        private List<String> testAgilityInfoList = new List<String>(4);
        private List<String> testArtifactInfoList = new List<String>(3);

        #region TestFields
        private String cardInfoArtifact1 = "Artifact:Customer Meeting Design:2,1,0,0";
        private String cardInfoArtifact2 = "Artifact:System Test Plan:0,1,1,1";
        private String cardInfoArtifact3 = "Artifact:Class Diagram:2,3,4,5";
        private String cardInfoAgility1 = "Agility:New Code Standards:1,1,1,1";
        private String cardInfoAgility2 = "Agility:New Law Passed:1,1,1,1";
        private String cardInfoAgility3 = "Agility:Temporary Budget Reduction:1,1,1,1";
        private String cardInfoAgility4 = "Agility:Update To A New Framework:1,1,1,1";
        #endregion

        #region Setup and Teardown
        [OneTimeSetUp]
        public void Class_SetUp() {
            #region Fill testCardInfoList
            testCardInfoList.Add(cardInfoArtifact1);
            testCardInfoList.Add(cardInfoArtifact2);
            testCardInfoList.Add(cardInfoArtifact3);
            testCardInfoList.Add(cardInfoAgility1);
            testCardInfoList.Add(cardInfoAgility2);
            testCardInfoList.Add(cardInfoAgility3);
            testCardInfoList.Add(cardInfoAgility4);
            #endregion

            #region Fill testAgilityInfoList
            testAgilityInfoList.Add(cardInfoAgility1);
            testAgilityInfoList.Add(cardInfoAgility2);
            testAgilityInfoList.Add(cardInfoAgility3);
            testAgilityInfoList.Add(cardInfoAgility4);
            #endregion

            #region Fill testArtifactInfoList
            testArtifactInfoList.Add(cardInfoArtifact1);
            testArtifactInfoList.Add(cardInfoArtifact2);
            testArtifactInfoList.Add(cardInfoArtifact3);
            #endregion
        }
        #endregion


        [Test]
        public void DeckCreator_ReadCards_ReturnsExpectedOutput() {
            List<String> outputList = DeckCreator.ReadCards();
            Assert.That(testCardInfoList, Is.EquivalentTo(outputList));
        }


        /// <summary>
        /// Checks that the deck creation methods can return all possible appropriate card info.
        /// </summary>
        [Test]
        public void DeckCreator_CreateAgilitysDeck_ReturnsAllPossibleCardInfo() {
            const Int32 TOTAL_CARDS = 100;
            List<String> returnedCards = DeckCreator.CreateAgilitysDeck(TOTAL_CARDS);
            Assert.That(returnedCards, Has.Some.EqualTo(testAgilityInfoList[0]));
            Assert.That(returnedCards, Has.Some.EqualTo(testAgilityInfoList[1]));
            Assert.That(returnedCards, Has.Some.EqualTo(testAgilityInfoList[2]));
            Assert.That(returnedCards, Has.Some.EqualTo(testAgilityInfoList[3]));
        }

    }
}
