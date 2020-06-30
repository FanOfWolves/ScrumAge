using System;
using System.Collections.Generic;
using NUnit.Framework;
using ScrumageEngine.Objects.Items;
using System.Text;

namespace ScrumageWPF.Test {
	[TestFixture]
	class Pawn_Test {
		#region Test Pawns
		private static Pawn testPawn1;
		private static Pawn testPawn2;
		private static Pawn testPawn3;
		private static Pawn testPawn4;
		private static Pawn testPawn5;
		private static Pawn testPawn6;
		private static Pawn testPawn7;
		private static Pawn testPawn8;
		private static Pawn testPawn9;
		private static Pawn testPawn10;
		private static Pawn testPawn11;
		#endregion

		#region test player IDs
		private const Int32 PLAYER_ID_0 = 0;
		private const Int32 PLAYER_ID_1 = 1;
		private const Int32 PLAYER_ID_2 = 2;
		private const Int32 PLAYER_ID_3 = 3;
		private const Int32 PLAYER_ID_4 = 4;
		#endregion

		#region test pawn levels
		private const String PAWN_NAME_0 = "Full Stack";
		private const String PAWN_NAME_1 = "Front End";
		private const String PAWN_NAME_2 = "Back End";
		private const String PAWN_NAME_3 = "full stack";
		private const String PAWN_NAME_4 = "front end";
		private const String PAWN_NAME_5 = "back end";
		private const String PAWN_NAME_6 = "                full stack";
		private const String PAWN_NAME_7 = "FullStack";
		private const String PAWN_NAME_8 = "fullstack";
		private const String PAWN_NAME_9 = "whatever";
		#endregion

		#region Setup
		/// <summary>
		/// Set up test pawn fields.
		/// </summary>
		[OneTimeSetUp]
		public void Pawn_Setup() {
			testPawn1 = new Pawn(PLAYER_ID_0, PAWN_NAME_0);
			testPawn2 = new Pawn(PLAYER_ID_1, PAWN_NAME_1);
			testPawn3 = new Pawn(PLAYER_ID_2, PAWN_NAME_2);
			testPawn4 = new Pawn(PLAYER_ID_3, PAWN_NAME_3);
			testPawn5 = new Pawn(PLAYER_ID_4, PAWN_NAME_4);
			testPawn6 = new Pawn(PLAYER_ID_1, PAWN_NAME_5);
			testPawn7 = new Pawn(PLAYER_ID_2, PAWN_NAME_6);
			testPawn8 = new Pawn(PLAYER_ID_3, PAWN_NAME_7);
			testPawn9 = new Pawn(PLAYER_ID_4, PAWN_NAME_8);
			testPawn10 = new Pawn(PLAYER_ID_0, PAWN_NAME_9);
			testPawn11 = new Pawn();
		}
		#endregion

		/// <summary>
		/// Test constructors
		/// </summary>
		[Test]
		[Category("Constructors")]
		public void Test_Constructors() {
			Pawn constructorTestPawn = new Pawn(1, "Full Stack");
			Pawn constructorTestPawnDefault = new Pawn();
			Assert.That(constructorTestPawn, Is.EqualTo(new Pawn(1, "Full Stack")));
			Assert.That(constructorTestPawnDefault, Is.EqualTo(new Pawn()));
		}


		/// <summary>
		/// Test cost calculation
		/// </summary>
		/// <param name="pawnLevel">The pawn level</param>
		/// <param name="expected">The expected cost for that pawn</param>
		[Test]
		[Category("Cost Calculation")]
		//"Full Stack";
		[TestCase(PAWN_NAME_0, 2)]
		//"Front End";
		[TestCase(PAWN_NAME_1, 1)]
		//"Back End";
		[TestCase(PAWN_NAME_2, 1)]
		//"full stack";
		[TestCase(PAWN_NAME_3, 0)]
		//"front end";
		[TestCase(PAWN_NAME_4, 0)]
		//"back end";
		[TestCase(PAWN_NAME_5, 0)]
		//"                full stack";
		[TestCase(PAWN_NAME_6, 0)]
		//"FullStack";
		[TestCase(PAWN_NAME_7, 0)]
		//"fullstack";
		[TestCase(PAWN_NAME_8, 0)]
		//"whatever";
		[TestCase(PAWN_NAME_9, 0)]
		public void Test_CostCalculation(String pawnLevel, Int32 expected) {
			Pawn testPawn = new Pawn(1, pawnLevel);
			Assert.That(testPawn.PawnCost == expected);
		}

		/// <summary>
		/// Test equals
		/// </summary>
		[Test]
		[Category("Equals")]
		public void Test_Equals() {
			Pawn equalsTestPawn = new Pawn(1, "Full Stack");
			Assert.That(equalsTestPawn, Is.EqualTo(new Pawn(1, "Full Stack")));
			Assert.That(equalsTestPawn, Is.Not.EqualTo(new Pawn(1, "Back End")));
		}

	}
}
