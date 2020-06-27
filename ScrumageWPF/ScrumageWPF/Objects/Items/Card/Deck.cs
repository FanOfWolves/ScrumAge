using System;
using System.Collections.Generic;
using static ScrumageEngine.Objects.Items.Cards.DeckCreator;
using System.Text;


namespace ScrumageEngine.Objects.Items.Cards {
	public class Deck {
		private Stack<Card> Cards;
		public Int32 Count { get { return Cards.Count; } }
		public Deck(String type, Int32 count) {
			Cards = CreateStack(type, count);
		}

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


		public Card MakeCard(String cardInfo) {
			String[] cardArray = cardInfo.Split(":");
			Int32[] cardReqs = ParseReqs(cardArray[2]);
			if(cardArray[0] == "Artifact") return new ArtifactCard(cardArray[1], cardReqs);
			else if(cardArray[0] == "Agility") return new AgilityCard(cardArray[1], cardReqs);
			else return new AgilityCard("Error card", new Int32[] { 0, 0, 0, 0 });					// Maybe throw exception?
		}

		public Int32[] ParseReqs(String reqs) {
			String[] reqsStrArray = reqs.Split(",");
			Int32[] reqsIntArray = new Int32[reqsStrArray.Length];
			for(Int32 i = 0; i < reqsStrArray.Length; i++) {
				reqsIntArray[i] = Int32.Parse(reqsStrArray[i]);
			}
			return reqsIntArray;
		}

		public void PrintDeck() {
			foreach(Card card in Cards) {
				Console.WriteLine(card);
			}
		}

		public Card Draw() {
			if(Count > 0)
				return Cards.Pop();
			else throw new Exception();
		}

		public Card Peek() {
			if(Count > 0)
				return Cards.Peek();
			else throw new Exception();
		}
	}
}
