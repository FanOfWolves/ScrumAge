using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ScrumageEngine.Objects.Items.Cards {
	/// <summary>
	/// A static class used to provide <see cref="Deck"/> objects with Card information to be used in constructing the Deck.
	/// See also <seealso cref="Card"/>.
	/// </summary>
	public static class DeckCreator {
		private static List<String> cardsInfo = ReadCards();
		private static List<String> artifactsInfo = PopulateArtifactsInfo();
		private static List<String> agilitysInfo = PopulateAgilityInfo();
		private static Random r = new Random();

		/// <summary>
		/// Reads in Card information from a designated file.
		/// </summary>
		/// <returns>A list of all lines.</returns>
		private static List<String> ReadCards() {
			try {
				String[] cardInfo = System.IO.File.ReadAllText("../../../../ScrumageWPF/Content/Cards/CardData.txt").Split("\r\n");
				return new List<String>(cardInfo);
			} catch(FileNotFoundException) {
				return new List<String>();
			}
		}

		/// <summary>
		/// Returns a list of all card information for <see cref="ArtifactCard"/> entries.
		/// </summary>
		/// <returns>A list of all Card information for <see cref="ArtifactCard"/> entries.</returns>
		private static List<String> PopulateArtifactsInfo() {
			List<String> retList = new List<String>();
			String cardType = "";
			foreach(String card in cardsInfo) {
				cardType = card.Split(":")[0];
				if(cardType == "Artifact") retList.Add(card);
			}
			return retList;
		}

		/// <summary>
		/// Returns a list of all card information for <see cref="AgilityCard"/> entries.
		/// </summary>
		/// <returns>A list of all Card information for <see cref="AgilityCard"/> entries.</returns>
		private static List<String> PopulateAgilityInfo() {
			List<String> retList = new List<String>();
			String cardType = "";
			foreach(String card in cardsInfo) {
				cardType = card.Split(":")[0];
				if(cardType == "Agility") retList.Add(card);
			}
			return retList;
		}

		/// <summary>
		/// Returns a list of card information to be used for populating a <see cref="Deck"/> of <see cref="ArtifactCard"/> objects.
		/// </summary>
		/// <param name="numOfCards">The number of cards.</param>
		/// <returns>a list of card information.</returns>
		public static List<String> CreateArtifactsDeck(Int32 numOfCards) {
			List<String> retString = new List<String>();
			for(Int32 i = 0; i < numOfCards; i++) {
				retString.Add(artifactsInfo[r.Next(artifactsInfo.Count)]);
			}
			return retString;
		}

		/// <summary>
		/// Returns a list of card information to be used for populating a <see cref="Deck"/> of <see cref="AgilityCard"/> objects.
		/// </summary>
		/// <param name="numOfCards">The number of cards.</param>
		/// <returns>a list of card information.</returns>
		public static List<String> CreateAgilitysDeck(Int32 numOfCards) {
			List<String> retString = new List<String>();
			for(Int32 i = 0; i < numOfCards; i++) {
				retString.Add(agilitysInfo[r.Next(agilitysInfo.Count)]);
			}
			return retString;
		}
	}
}
