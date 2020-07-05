using System;
using System.Collections.Generic;
using static ScrumageEngine.Objects.Items.Cards.DeckCreator;
using System.Text;


namespace ScrumageEngine.Objects.Items.Cards {
	/// <summary>
	/// A container for <see cref="Card"/> objects. 
	/// </summary>
	class Deck {
		private Stack<Card> Cards;
		public Int32 Count { get { return Cards.Count; } }


		/// <summary>
		/// Initializes a new instance of the <see cref="Deck"/> class.
		/// </summary>
		/// <param name="type">The card type of the new Deck.</param>
		/// <param name="count">The number of cards to be in the new Deck.</param>
		public Deck(String type, Int32 count) {
			Cards = CreateStack(type, count);
		}

		/// <summary>
		/// Creates the stack.
		/// </summary>
		/// <param name="type">The card type of the Card objects to return.</param>
		/// <param name="count">The number of Card objects to be in the returned stack.</param>
		/// <returns>a <see cref="Stack{Card}"/> collection.</returns>
		private Stack<Card> CreateStack(String type, Int32 count) {
			Stack<Card> retStack = new Stack<Card>();
			if(type == "Agility") {
				foreach(String cardInfo in CreateAgilitysDeck(count)) {
					retStack.Push(MakeCard(cardInfo));
				}
			}else if(type == "Artifact") {
				foreach(String cardInfo in CreateArtifactsDeck(count)) {
					retStack.Push(MakeCard(cardInfo));
				}
			}
			return retStack;
		}

		/// <summary>
		/// Creates and returns a new <see cref="Card"/> object.
		/// </summary>
		/// <param name="cardInfo">The card information to be used to create the card.</param>
		/// <returns>a new Card.</returns>
		private Card MakeCard(String cardInfo) {
			String[] cardArray = cardInfo.Split(":");
			Int32[] cardReqs = ParseReqs(cardArray[2]);
			if(cardArray[0] == "Artifact") return new ArtifactCard(cardArray[1], cardReqs);
			else if(cardArray[0] == "Agility") return new AgilityCard(cardArray[1], cardReqs);
			else return new AgilityCard("Error card", new Int32[] { 0, 0, 0, 0 });
		}

		/// <summary>
		/// Parses the required costs for a Card.
		/// </summary>
		/// <param name="reqs">The required <see cref="Resource"/> costs of a Card.</param>
		/// <returns>an array of the different Resource costs.</returns>
		private Int32[] ParseReqs(String reqs) {
			String[] reqsStrArray = reqs.Split(",");
			Int32[] reqsIntArray = new Int32[reqsStrArray.Length];
			for(Int32 i = 0; i < reqsStrArray.Length; i++) {
				reqsIntArray[i] = Int32.Parse(reqsStrArray[i]);
			}
			return reqsIntArray;
		}

		/// <summary>
		/// Prints the Cards to the console.
		/// </summary>
		public void PrintDeck() {
			foreach(Card card in Cards) {
				Console.WriteLine(card);
			}
		}

		/// <summary>
		/// Removes and returns the "top" Card from this Deck.
		/// </summary>
		/// <returns>the "top" Card.</returns>
		/// <exception cref="Exception">Thrown when this Deck has no Card to return.</exception>
		public Card Draw() {
			if(Count > 0)
				return Cards.Pop();
			else throw new Exception();
		}

		/// <summary>
		/// Returns the "top" Card from this Deck.
		/// </summary>
		/// <returns>the "top" Card.</returns>
		/// <exception cref="Exception">Thrown when this Deck has no Card to return.</exception>
		public Card Peek() {
			if(Count > 0)
				return Cards.Peek();
			else throw new Exception();
		}
	}
}
