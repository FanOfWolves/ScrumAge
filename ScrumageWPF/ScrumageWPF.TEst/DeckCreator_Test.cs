﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NUnit.Framework;
using ScrumageEngine.Objects.Items.Cards;
namespace ScrumageWPF.Test {

    /// <summary>
    /// Testing suite for <see cref="DeckCreator"/>
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
        /// <summary>
        /// One time setup of testing class.
        /// </summary>
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
        
        /// <summary>
        /// One time cleanup of testing class.
        /// </summary>
        [OneTimeTearDown]
        public void Class_TearDown() {
            this.testCardInfoList.ForEach(_string => _string = null);
            this.testCardInfoList.Clear();
            this.testAgilityInfoList.Clear();
            this.testArtifactInfoList.Clear();
            this.testAgilityInfoList = null;
            this.testArtifactInfoList = null;
            this.testCardInfoList = null;
        }
        #endregion

        /// <summary>
        /// Asserts that <see cref="DeckCreator.ReadCards"/> returns expected list of strings.
        /// </summary>
        [Test]
        public void DeckCreator_ReadCards_ReturnsExpectedOutput() {
            List<String> outputList = DeckCreator.ReadCards();
            Assert.That(testCardInfoList, Is.EquivalentTo(outputList));
        }

        #region Category: Public Deck Creation

        #region DeckCreator_CreateAgilitysDeck_ReturnsAllPossibleCardInfo
        /// <summary>
        /// Checks that the agility deck creation method can return all possible agility card info.
        /// </summary>
        [Test]
        [Category("Public Deck Creation")]
        public void DeckCreator_CreateAgilitysDeck_ReturnsAllPossibleCardInfo() {
            const Int32 TOTAL_CARDS = 100;
            List<String> returnedCards = DeckCreator.CreateAgilitysDeck(TOTAL_CARDS);
            Assert.That(returnedCards, Has.Some.EqualTo(testAgilityInfoList[0]));
            Assert.That(returnedCards, Has.Some.EqualTo(testAgilityInfoList[1]));
            Assert.That(returnedCards, Has.Some.EqualTo(testAgilityInfoList[2]));
            Assert.That(returnedCards, Has.Some.EqualTo(testAgilityInfoList[3]));
        } 
        #endregion

        #region DeckCreator_CreateArtifactsDeck_ReturnAllPossibleCardInfo
        /// <summary>
        /// Checks that the artifact deck creation method can return all possible artifact card info.
        /// </summary>
        [Test]
        [Category("Public Deck Creation")]
        public void DeckCreator_CreateArtifactsDeck_ReturnAllPossibleCardInfo() {
            const Int32 TOTAL_CARDS = 100;
            List<String> returnedCards = DeckCreator.CreateArtifactsDeck(TOTAL_CARDS);
            Assert.That(returnedCards, Has.Some.EqualTo(testArtifactInfoList[0]));
            Assert.That(returnedCards, Has.Some.EqualTo(testArtifactInfoList[1]));
            Assert.That(returnedCards, Has.Some.EqualTo(testArtifactInfoList[2]));
        }
        #endregion

        #endregion
    }
}