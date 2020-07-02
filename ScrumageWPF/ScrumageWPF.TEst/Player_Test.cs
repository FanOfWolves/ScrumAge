using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using ScrumageEngine.Objects.Items;
using ScrumageEngine.Objects.Items.Cards;
using ScrumageEngine.Objects.Player;

namespace ScrumageWPF.Test
{
    [TestFixture]
    class Player_Test
    {

        private Int32 PLAYER_ID = 1;
        private string PLAYER_NAME = "Unit Test";

        private Player player;



        [OneTimeSetUp]
        protected void OneTimeSetup()
        {
            
        }

        [SetUp]
        protected void SetUp()
        {
            player = new Player(PLAYER_ID, PLAYER_NAME);

            player.Budget = 0;
        }


        [Test]
        [Category("Constructors")]
        public void Test_Constructors()
        {
            Assert.IsNotNull(player);
            Assert.IsTrue(player.PlayerID == PLAYER_ID);
            Assert.IsTrue(player.PlayerName == PLAYER_NAME);
        }

        [Test]
        [TestCase(50, 50, 100)]
        [TestCase(100, -50, 50)]
        [TestCase(50, -50, 0)]
        public void Test_AddBudgetToFunds(int a, int b, int c)
        {

            player.Funds = a;
            player.Budget = b;

            player.AddBudgetToFunds();

            Assert.AreEqual(c, player.Funds);
        }

        [Test]
        public void Test_AddResource()
        {
            Resource req = new Requirements();
            Resource des = new Design();

            player.AddResource(req);

            Assert.IsTrue(player.playerResources[req] == 1);

            player.AddResource(des);
            player.AddResource(des);

            Assert.IsTrue(player.playerResources[des] == 2);
        }

        public void Test_TakeResources()
        {
            Assert.Fail();
        }

        public void Test_AddToCards()
        {
            Assert.Fail();
        }

        [TestCase(50, 50, 100)]
        [TestCase(0, 0, 0)]
        [TestCase(-50, 0, -50)]
        public void Test_GiveFunds(int a, int b, int expected)
        {
            // Reset funds
            player.Funds = 0;

            player.GiveFunds(a);
            player.GiveFunds(b);

            Assert.AreEqual(expected, player.Funds);
        }

        [Test]
        public void Test_GivePawns()
        {
            var player = new Player(PLAYER_ID, PLAYER_NAME);

            // Check if GivePawn(String)
            player.GivePawn("Unit Test Pawn");
            Assert.IsTrue((player.HasPawn("Unit Test Pawn").PawnType == "Unit Test Pawn"));
            
            // Check if GivePawn(Pawn)
            var pawn = new Pawn();
            pawn.PawnType = "Unit Test Pawn 2";
            player.GivePawn(pawn);
            Assert.IsTrue(player.HasPawn(pawn.PawnType).PawnType == pawn.PawnType);

            // Check if not GivePawn(String)
            var pawnInvalid = player.HasPawn("UnitTest Null");
            Assert.IsTrue(pawnInvalid.PawnType == "None");

        }


        [Test]
        public void Test_HasPawn()
        {
            var player = new Player(PLAYER_ID, PLAYER_NAME);

            player.GivePawn("Unit Test Pawn");
            Assert.IsTrue((player.HasPawn("Unit Test Pawn").PawnType == "Unit Test Pawn"));
        }

        [Test]
        [TestCase(-20, -20)]
        [TestCase(20, 20)]
        [TestCase(70, 70)]
        public void Test_IncreaseBudget(int a, int expected)
        {
            player.Budget = 0;

            player.IncreaseBudget(a);

            Assert.AreEqual(expected, player.Budget);
        }

        [Test]
        public void Test_ListPawns()
        {
            Assert.IsTrue(player.Pawns.Count == player.ListPawns().Count);

            player.GivePawn(new Pawn());

            Assert.IsTrue(player.Pawns.Count == player.ListPawns().Count);
        }

        [Test]
        public void Test_PayPawns()
        {
            var validatedPlayer = new Player(PLAYER_ID, PLAYER_NAME);

            validatedPlayer.GivePawn(new Pawn());
            validatedPlayer.GivePawn(new Pawn());

            // 2 pawns or less = no pay
            Assert.IsFalse(validatedPlayer.PayPawns());

            player.Funds = 0;
            // Test removal of pawns if no funds
            validatedPlayer.GivePawn("Backend");
            validatedPlayer.GivePawn("Backend");
            validatedPlayer.GivePawn("Backend");
            validatedPlayer.PayPawns();

            Assert.AreEqual(0, validatedPlayer.Pawns.Count);


            // Set funds to 6, - 4 (CalcCosted pawns) == 2 expected funds
            var secondPlayer = new Player(PLAYER_ID, PLAYER_NAME);

            secondPlayer.GivePawn(new Pawn().PawnType = "Backend");
            secondPlayer.GivePawn(new Pawn().PawnType = "Frontend");
            secondPlayer.GivePawn(new Pawn().PawnType = "Fullstack");
            secondPlayer.PayPawns();

            Assert.IsTrue(secondPlayer.PayPawns());

        }

        [Test]
        public void Test_TakePawns()
        {
            var player = new Player(PLAYER_ID, PLAYER_NAME);

            // No Pawn Exists
            Assert.IsTrue(player.TakePawn("No Pawn").PawnType == "None");

            // Pawn Exists
            var pawn = new Pawn();
            pawn.PawnType = "ValidPawn";
            player.GivePawn(pawn);

            Assert.IsTrue(player.TakePawn("ValidPawn").Equals(pawn));

        }

        public void Test_RemoveFromAgility()
        {
            
        }

        public void Test_RemoveFromArtifacts()
        {

        }

        [Test]
        public void Test_FinishedPhase()
        {
            player.FinishedPhase = true;

            Assert.IsTrue(player.FinishedPhase);

            player.FinishedPhase = false;

            Assert.IsFalse(player.FinishedPhase);
        }


    }
}
