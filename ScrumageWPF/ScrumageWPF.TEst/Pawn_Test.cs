using System;
using System.Collections.Generic;
using NUnit.Framework;
using ScrumageEngine.Objects.Items;
using System.Text;

namespace ScrumageWPF.Test {
	[TestFixture]
	class Pawn_Test {

		private Pawn testPawn1;
		private Pawn testPawn2;
		private Pawn testPawn3;
		private Pawn testPawn4;
		private Pawn testPawn5;
		private Pawn testPawn6;
		private Pawn testPawn7;
		private Pawn testPawn8;
		private Pawn testPawn9;
		private Pawn testPawn10;
		private Pawn testPawn11;

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

		[Test]
		[Category("Constructors")]
		public void Test_Constructors() {
			Pawn constructorTestPawn = new Pawn(1, "Full Stack");
			Assert.That(constructorTestPawn, Is.EqualTo(new Pawn(1, "Full Stack")));
		}

	}
}
