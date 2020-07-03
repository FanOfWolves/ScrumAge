using System;
using System.Collections.Generic;
using NUnit.Framework;
using ScrumageEngine.Objects.Items.Cards;
using ScrumageWPF.Test.Utilities;

namespace ScrumageWPF.Test {

    /// <summary>
    /// Testing suite for Deck.cs
    /// </summary>
    class Deck_Test {
        private Deck testDeck;

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
        /// A setup called at beginning of this testing class.
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
        /// A setup called before every method
        /// </summary>
        [SetUp]
        public void SetUp() {
            testDeck = new Deck("Agility", 5);
        }
        #endregion

        #region Test Helper Methods        
        /// <summary>
        /// Creates a test card to be used for verifying returns.
        /// </summary>
        /// <param name="cardType">Type of the card.</param>
        /// <param name="cardName">Name of the card.</param>
        /// <param name="costs">The costs.</param>
        /// <returns>a Card used for testing.</returns>
        private Card CreateTestCard(String cardType, String cardName, Int32[] costs) {
            if(cardType == "Agility")
                return new AgilityCard(cardName, costs);
            else
                return new ArtifactCard(cardName, costs);
        }
        #endregion

        #region Category: Constructor        

        #region Deck_Constructor_Works
        /// <summary>
        /// Asserts that <see cref="Deck.Deck(String, Int32)"/> works.
        /// </summary>
        [Test]
        [Category("Constructor")]
        [TestCase("Agility", 2)]
        [TestCase("Artifact", 3)]
        [TestCase("Agility", 30)]
        public void Deck_Constructor_Works(String cardType, Int32 amount) {
            testDeck = new Deck(cardType, amount);
            Assert.That(testDeck.Cards.Count, Is.EqualTo(amount));
            while(testDeck.Cards.Count != 0) {
                Card topCard = testDeck.Cards.Pop();
                if(cardType == "Artifact") {
                    if(topCard.GetType() != typeof(ArtifactCard))
                        Assert.Fail();
                }
                else if(cardType == "Agility") {
                    if(topCard.GetType() != typeof(AgilityCard))
                        Assert.Fail();
                }
            }
            Assert.Pass();
        } 
        #endregion

        #endregion

        #region Category: Collection Creation

        #region Deck_ParseReqs_Works        
        /// <summary>
        /// Asserts that <see cref="Deck.ParseReqs(String)"/> works as expected.
        /// </summary>
        /// <param name="expectedCosts">The expected costs.</param>
        /// <param name="strInputCosts">The string input costs.</param>
        [Test]
        [Category("Collection Creation")]
        [TestCase(new Int32[] { 1, 0, 2, 4 }, "1,0,2,4")]
        [TestCase(new Int32[] { 0, 1, 0, 2 }, "0,1,0,2")]
        public void Deck_ParseReqs_Works(Int32[] expectedCosts, String strInputCosts) {
            Int32[] actualOutput = testDeck.ParseReqs(strInputCosts);
            Assert.That(actualOutput, Is.EquivalentTo(expectedCosts));
        } 
        #endregion

        #region Deck_MakeCard_Works
        /// <summary>
        /// Asserts that <see cref="Deck.MakeCard(String)"/> works.
        /// </summary>
        /// <param name="makeCardInput">The MakeCard input string.</param>
        /// <param name="cardType">Type of the card.</param>
        /// <param name="cardName">Name of the card.</param>
        /// <param name="costs">The costs.</param>
        [Test]
        [Category("Collection Creation")]
        [TestCase("Artifact:Class Diagram:2,3,4,5", "Artifact", "Class Diagram", new Int32[] { 2, 3, 4, 5 })]
        [TestCase("Agility:New Code Standards:1,1,1,1", "Agility", "New Code Standards", new Int32[] { 1, 1, 1, 1 })]
        [TestCase("Agility:New Law Passed:1,1,1,1", "Agility", "New Law Passed", new Int32[] { 1, 1, 1, 1 })]
        public void Deck_MakeCard_Works(String makeCardInput, String cardType, String cardName, Int32[] costs) {
            Card expectedCard = CreateTestCard(cardType, cardName, costs);
            Card actualCard = testDeck.MakeCard(makeCardInput);

            Assert.That(actualCard, Is.EqualTo(expectedCard).Using(new TestCardEqualityCompare()));
        } 
        #endregion

        #endregion

        #region Category: Proxy Methods

        #region Board_Draw_WorksIfCardInStack()        
        /// <summary>
        /// Asserts that <see cref="Deck.Draw"/> returns card on top of deck.
        /// </summary>
        [Test]
        [Category("Proxy Methods")]
        public void Board_Draw_WorksIfCardInStack() {
            testDeck = new Deck("Agility", 0);
            Card expectedCard = new AgilityCard("aCard", new Int32[] { 3, 2, 4, 1 });
            Card unexpectedCard = new AgilityCard("Wrong Card!", new Int32[] { 3, 2, 4, 1 });

            testDeck.Cards.Push(unexpectedCard);
            testDeck.Cards.Push(expectedCard);

            Card actualCard = testDeck.Draw();
            Assert.That(actualCard, Is.EqualTo(expectedCard).Using(new TestCardEqualityCompare()));
            Assert.That(testDeck.Count, Is.EqualTo(1));
        }
        #endregion

        #region Board_Draw_ThrowsExceptionIfNoCards        
        /// <summary>
        /// Asserts that <see cref="Deck.Draw"/> returns <see cref="System.Exception"/> if no cards on stack.
        /// </summary>
        [Test]
        [Category("Proxy Methods")]
        public void Board_Draw_ThrowsExceptionIfNoCards() {
            testDeck = new Deck("Agility", 0);

            Assert.Throws(typeof(System.Exception), new TestDelegate(() => testDeck.Draw()));
        }
        #endregion

        #region Board_Peek_WorksIfCardInStack        
        /// <summary>
        /// Asserts that <see cref="Deck.Peek"/> works as expected.
        /// </summary>
        [Test]
        public void Board_Peek_WorksIfCardInStack() {
            testDeck = new Deck("Agility", 0);
            Card expectedCard = new AgilityCard("aCard", new Int32[] { 3, 2, 4, 1 });
            Card unexpectedCard = new AgilityCard("Wrong Card!", new Int32[] { 3, 2, 4, 1 });

            testDeck.Cards.Push(unexpectedCard);
            testDeck.Cards.Push(expectedCard);

            Card actualCard = testDeck.Peek();
            Assert.That(actualCard, Is.EqualTo(expectedCard).Using(new TestCardEqualityCompare()));
        }
        #endregion

        #region Board_Peek_ThrowExceptionIfNoCards        
        /// <summary>
        /// Asserts that <see cref="Deck.Peek"/> throws <see cref="System.Exception"/> if no cards in stack.
        /// </summary>
        [Test]
        public void Board_Peek_ThrowsExceptionIfNoCards() {
            testDeck = new Deck("Agility", 0);

            Assert.Throws(typeof(System.Exception), new TestDelegate(() => testDeck.Peek()));
        }
        #endregion

        #endregion

    }
}
