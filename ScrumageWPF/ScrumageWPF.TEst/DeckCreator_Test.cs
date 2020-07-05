using System;
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
        private String cardInfoArtifact4 = "Artifact:Component Diagram:1,1,1,0";
        private String cardInfoArtifact5 = "Artifact:Specifications Document:2,1,0,0";
        private String cardInfoArtifact6 = "Artifact:UI-Prototype:1,1,1,0";
        private String cardInfoArtifact7 = "Artifact:End-User Manual:1,1,0,1";
        private String cardInfoArtifact8 = "Artifact:Scrum Report:1,0,0,0";
        private String cardInfoArtifact9 = "Artifact:Architecture Diagram:1,1,0,0";
        private String cardInfoArtifact10 = "Artifact:Package Diagram:0,1,1,0";
        private String cardInfoArtifact11 = "Artifact:Research Targets:1,1,0,0";
        private String cardInfoArtifact12 = "Artifact:Scrum Master Report:0,1,1,0";
        private String cardInfoArtifact13 = "Artifact:Some other random document:1,1,0,0";
        private String cardInfoArtifact14 = "Artifact:My humps:0,1,1,0";
        private String cardInfoArtifact15 = "Artifact:Product Owner Report:1,1,0,0";
        private String cardInfoAgility1 = "Agility:New Code Standards:1,1,1,1";
        private String cardInfoAgility2 = "Agility:New Law Passed:1,1,1,1";
        private String cardInfoAgility3 = "Agility:Temporary Budget Reduction:1,1,1,1";
        private String cardInfoAgility4 = "Agility:Update To A New Framework:1,1,1,1";
        private String cardInfoAgility5 = "Agility:Budget Reduction:0,1,1,1";
        private String cardInfoAgility6 = "Agility:Change in Market Trends:1,1,0,0";
        private String cardInfoAgility7 = "Agility:Cost Benefit Analysis:0,0,1,1";
        private String cardInfoAgility8 = "Agility:Robot Uprising:0,1,1,0";
        private String cardInfoAgility9 = "Agility:Customer Dispute:0,1,1,0";
        private String cardInfoAgility10 = "Agility:Lead developer quits:0,0,2,0";
        private String cardInfoAgility11 = "Agility:New Project Manager:0,2,0,0";
        private String cardInfoAgility12 = "Agility:Deadline Shift:0,0,1,1";
        private String cardInfoAgility13 = "Agility:Global Pandemic:0,0,1,1";
        private String cardInfoAgility14 = "Agility:Out of coffee:0,0,2,0";
        private String cardInfoAgility15 = "Agility:Change in requirements:2,1,0,0";





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
            testCardInfoList.Add(cardInfoArtifact4);
            testCardInfoList.Add(cardInfoArtifact5);
            testCardInfoList.Add(cardInfoArtifact6);
            testCardInfoList.Add(cardInfoArtifact7);
            testCardInfoList.Add(cardInfoArtifact8);
            testCardInfoList.Add(cardInfoArtifact9);
            testCardInfoList.Add(cardInfoArtifact10);
            testCardInfoList.Add(cardInfoArtifact11);
            testCardInfoList.Add(cardInfoArtifact12);
            testCardInfoList.Add(cardInfoArtifact13);
            testCardInfoList.Add(cardInfoArtifact14);
            testCardInfoList.Add(cardInfoArtifact15);
            testCardInfoList.Add(cardInfoAgility1);
            testCardInfoList.Add(cardInfoAgility2);
            testCardInfoList.Add(cardInfoAgility3);
            testCardInfoList.Add(cardInfoAgility4);
            testCardInfoList.Add(cardInfoAgility5);
            testCardInfoList.Add(cardInfoAgility6);
            testCardInfoList.Add(cardInfoAgility7);
            testCardInfoList.Add(cardInfoAgility8);
            testCardInfoList.Add(cardInfoAgility9);
            testCardInfoList.Add(cardInfoAgility10);
            testCardInfoList.Add(cardInfoAgility11);
            testCardInfoList.Add(cardInfoAgility12);
            testCardInfoList.Add(cardInfoAgility13);
            testCardInfoList.Add(cardInfoAgility14);
            testCardInfoList.Add(cardInfoAgility15);
            #endregion

            #region Fill testAgilityInfoList
            testAgilityInfoList.Add(cardInfoAgility1);
            testAgilityInfoList.Add(cardInfoAgility2);
            testAgilityInfoList.Add(cardInfoAgility3);
            testAgilityInfoList.Add(cardInfoAgility4);
            testAgilityInfoList.Add(cardInfoAgility5);
            testAgilityInfoList.Add(cardInfoAgility6);
            testAgilityInfoList.Add(cardInfoAgility7);
            testAgilityInfoList.Add(cardInfoAgility8);
            testAgilityInfoList.Add(cardInfoAgility9);
            testAgilityInfoList.Add(cardInfoAgility10);
            testAgilityInfoList.Add(cardInfoAgility11);
            testAgilityInfoList.Add(cardInfoAgility12);
            testAgilityInfoList.Add(cardInfoAgility13);
            testAgilityInfoList.Add(cardInfoAgility14);
            testAgilityInfoList.Add(cardInfoAgility15);
            #endregion

            #region Fill testArtifactInfoList
            testArtifactInfoList.Add(cardInfoArtifact1);
            testArtifactInfoList.Add(cardInfoArtifact2);
            testArtifactInfoList.Add(cardInfoArtifact3);
            testArtifactInfoList.Add(cardInfoArtifact4);
            testArtifactInfoList.Add(cardInfoArtifact5);
            testArtifactInfoList.Add(cardInfoArtifact6);
            testArtifactInfoList.Add(cardInfoArtifact7);
            testArtifactInfoList.Add(cardInfoArtifact8);
            testArtifactInfoList.Add(cardInfoArtifact9);
            testArtifactInfoList.Add(cardInfoArtifact10);
            testArtifactInfoList.Add(cardInfoArtifact11);
            testArtifactInfoList.Add(cardInfoArtifact12);
            testArtifactInfoList.Add(cardInfoArtifact13);
            testArtifactInfoList.Add(cardInfoArtifact14);
            testArtifactInfoList.Add(cardInfoArtifact15);
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

            foreach(String output in outputList) {
                Boolean isExpected = testCardInfoList.Contains(output);
                if(!isExpected)
                    Assert.Fail();
            }
            Assert.Pass();
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