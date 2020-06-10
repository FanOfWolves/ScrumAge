using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ScrumageEngine.Objects.Items {
	class Die {
		public int Value { get; }
		private static char[][] DieFace = new char[5][];
		public Die(int value) {
			Value = value;
			DieFace = CalcDieFace(value);
		}

		static char[][] CalcDieFace(int dieVal) {
			char[][] dieFace = InitDieFace();
			if(dieVal == 0) return dieFace;
			bool isEven = dieVal % 2 == 0;
			if(isEven) {
				dieFace[1][2] = '*';
				dieFace[3][6] = '*';
				if(dieVal >= 4) {
					dieFace[3][2] = '*';
					dieFace[1][6] = '*';
				}
				if(dieVal == 6) {
					dieFace[2][2] = '*';
					dieFace[2][6] = '*';
				}
			} else {
				dieFace[2][4] = '*';
				if(dieVal >= 3) {
					dieFace[1][2] = '*';
					dieFace[3][6] = '*';
				}
				if(dieVal == 5) {
					dieFace[3][2] = '*';
					dieFace[1][6] = '*';
				}
			}
			return dieFace;
		}

		static char[][] InitDieFace() {
			String[] dieStart = Regex.Split((" -------\n" +
											 "|       |\n" +
											 "|       |\n" +
											 "|       |\n" +
											 " ------- "), $"[\n]");
			char[][] dieFace = new char[5][];
			int i = 0;
			while(i < dieStart.Length) {
				dieFace[i] = dieStart[i].ToCharArray();
				i++;
			}
			return dieFace;
		}

		public String DrawDie() {
			String dieString = "";
			int i = 0, j = 0;
			while(i < DieFace.Length) {
				while(j < DieFace[i].Length) {
					dieString += DieFace[i][j];
					j++;
				}
				j = 0;
				i++;
				dieString += '\n';
			}
			return dieString;
		}
	}
}
