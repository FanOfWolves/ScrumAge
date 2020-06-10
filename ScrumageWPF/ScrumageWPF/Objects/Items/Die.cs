using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ScrumageEngine.Objects.Items {
	public class Die {
		/// <summary>
		/// Die's value.
		/// </summary>
		public int Value { get; }
		/// <summary>
		/// Die's face representation.
		/// </summary>
		private static char[][] DieFace = new char[5][];
		/// <summary>
		/// Overloaded Die Constructor, value passed in is value of the die.
		/// </summary>
		/// <param name="value">The value of the die.</param>
		public Die(int value) {
			Value = value;
			DieFace = CalcDieFace(value);
		}

		/// <summary>
		/// Inserts dots into the die's face based on the value
		/// </summary>
		/// <param name="dieVal">The value of the die</param>
		/// <returns>2D char array representation of the die.</returns>
		char[][] CalcDieFace(int dieVal) {
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

		/// <summary>
		/// Instanciates the die's face.
		/// </summary>
		/// <returns>2D char array of blank representation of a die.</returns>
		char[][] InitDieFace() {
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

		/// <summary>
		/// Creates a printable version of the die's face representation.
		/// </summary>
		/// <returns>A string representation of the die.</returns>
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
