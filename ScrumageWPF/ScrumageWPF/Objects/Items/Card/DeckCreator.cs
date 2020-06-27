using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ScrumageEngine.Objects.Items.Cards {
	public static class DeckCreator {
		private static List<String> cardsInfo = ReadCards();
		private static List<String> artifactsInfo = PopulateArtifactsInfo();
		private static List<String> agilitysInfo = PopulateAgilityInfo();
		private static Random r = new Random();
		public static List<String> ReadCards() {
			try {
				String[] cardInfo = System.IO.File.ReadAllText("../../../../ScrumageWPF/Objects/Items/Card/CardData.txt").Split("\r\n");
				return new List<String>(cardInfo);
			} catch(FileNotFoundException) {
				return new List<String>();
			} //TODO: DirectoryNotFoundException
		}

		private static List<String> PopulateArtifactsInfo() {
			List<String> retList = new List<String>();
			String cardType = "";
			foreach(String card in cardsInfo) {
				cardType = card.Split(":")[0];
				if(cardType == "Artifact") retList.Add(card);
			}
			return retList;
		}

		private static List<String> PopulateAgilityInfo() {
			List<String> retList = new List<String>();
			String cardType = "";
			foreach(String card in cardsInfo) {
				cardType = card.Split(":")[0];
				if(cardType == "Agility") retList.Add(card);
			}
			return retList;
		}

		public static List<String> CreateArtifactsDeck(Int32 numOfCards) {
			List<String> retString = new List<String>();
			for(Int32 i = 0; i < numOfCards; i++) {
				retString.Add(artifactsInfo[r.Next(artifactsInfo.Count - 1)]);
			}
			return retString;
		}

		public static List<String> CreateAgilitysDeck(Int32 numOfCards) {
			List<String> retString = new List<String>();
			for(Int32 i = 0; i < numOfCards; i++) {
				retString.Add(agilitysInfo[r.Next(agilitysInfo.Count - 1)]);
			}
			return retString;
		}

		public static void TestPrint() {
			foreach(String card in artifactsInfo) {
				Console.WriteLine(card);
			}
			Console.WriteLine("-----------------------");
			foreach(String card in agilitysInfo) {
				Console.WriteLine(card);
			}
		}
	}
}
