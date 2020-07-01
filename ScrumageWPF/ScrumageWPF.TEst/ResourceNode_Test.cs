using System;
using System.Collections.Generic;
using System.Text;
using ScrumageEngine.BoardSpace;
using ScrumageEngine.Objects.Items;
using ScrumageEngine.Objects.Player;
using NUnit.Framework;
using System.Linq;

namespace ScrumageWPF.Test {

    /// <summary>
    /// Testing class for <see cref="ResourceNode"/>
    /// </summary>
    [TestFixture]
    class ResourceNode_Test {

        private ResourceNode reqNode;
        private ResourceNode desNode;
        private ResourceNode impNode;
        private ResourceNode tesNode;

        private Player testPlayer1;
        private Player testPlayer2;
        
        private Int32 RESOURCE_NODE_PAWN_LIMIT = 7;

        #region Resource Success Chances
        private const Int32 DES_FULL_STACK_CHANCE = 20;
        private const Int32 DES_FRONT_END_CHANCE = 15;
        private const Int32 DES_BACK_END_CHANCE = 15;
        private const Int32 TES_FULL_STACK_CHANCE = 25;
        private const Int32 TES_FRONT_END_CHANCE = 5;
        private const Int32 TES_BACK_END_CHANCE = 5;
        private const Int32 REQ_FULL_STACK_CHANCE = 20;
        private const Int32 REQ_FRONT_END_CHANCE = 20;
        private const Int32 REQ_BACK_END_CHANCE = 10;
        private const Int32 IMP_FULL_STACK_CHANCE = 20;
        private const Int32 IMP_FRONT_END_CHANCE = 10;
        private const Int32 IMP_BACK_END_CHANCE = 20;
        #endregion

        private const Int32 REQ = 0;
        private const Int32 DES = 1;
        private const Int32 IMP = 2;
        private const Int32 TES = 3;
        private const Int32 NUL = 4;

        [OneTimeSetUp]
        public void ResourceNode_ClassSetUp() {
            reqNode = new ResourceNode(0, "requirementsNode", new Requirements());
            desNode = new ResourceNode(1, "designNode", new Design());
            impNode = new ResourceNode(2, "implementationNode", new Implementation());
            tesNode = new ResourceNode(3, "testingNode", new Testing());

            testPlayer1 = new Player(1, "testPlayer1");
            testPlayer2 = new Player(2, "testPlayer2");
        }

        [OneTimeTearDown]
        public void ResourceNode_ClassTearDown() {
            reqNode = null;
            desNode = null;
            impNode = null;
            tesNode = null;
            testPlayer1 = null;
            testPlayer2 = null;
        }

        [SetUp]
        public void ResourceNode_MethodSetUp() {
            testPlayer1.GivePawn(new Pawn(testPlayer1.PlayerID, "Back End"));
            testPlayer1.GivePawn(new Pawn(testPlayer1.PlayerID, "Front End"));
            testPlayer1.GivePawn(new Pawn(testPlayer1.PlayerID, "Full Stack"));
            testPlayer1.GivePawn(new Pawn(testPlayer1.PlayerID, "Front End"));
            testPlayer1.GivePawn(new Pawn(testPlayer1.PlayerID, "Front End"));

            testPlayer2.GivePawn(new Pawn(testPlayer2.PlayerID, "Back End"));
            testPlayer2.GivePawn(new Pawn(testPlayer2.PlayerID, "Front End"));
            testPlayer2.GivePawn(new Pawn(testPlayer2.PlayerID, "Full Stack"));
            testPlayer2.GivePawn(new Pawn(testPlayer2.PlayerID, "Front End"));
            testPlayer2.GivePawn(new Pawn(testPlayer2.PlayerID, "Front End"));

            testPlayer1.playerResources = new ResourceContainer();
            testPlayer1.playerResources = new ResourceContainer();
        }

        [TearDown]
        public void ResourceNode_MethodTearDown() {
            this.reqNode.Pawns.Clear();
            this.desNode.Pawns.Clear();
            this.impNode.Pawns.Clear();
            this.tesNode.Pawns.Clear();
            this.testPlayer1.Pawns.Clear();
            this.testPlayer2.Pawns.Clear();
        }

        #region Category: Instantiation

        #region ResourceNode_ConstructorInstantiatesCorrectly        
        /// <summary>
        /// Asserts that <see cref="ResourceNode.ResourceNode(Int32, String, Resource)"/> instantiates correctly.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name.</param>
        /// <param name="resourceType">Type of the resource.</param>
        [Test]
        #region Test-Cases
        [TestCase(0, "Node1", REQ)]
        [TestCase(6, "Albert", DES)]
        [TestCase(888, "Node943", DES)]
        [TestCase(1, "null", IMP)]
        #endregion
        public void ResourceNode_ConstructorInstantiatesCorrectly(Int32 id, String name, Int32 resourceType) {
            Resource? theResource = CreateResource(resourceType);

            ResourceNode testNode = new ResourceNode(id, name, theResource);
            Assert.That(testNode.NodeID, Is.EqualTo(id));
            Assert.That(testNode.NodeName, Is.EqualTo(name));
            Assert.That(testNode.nodeResource, Is.EqualTo(theResource));
        } 
        #endregion

        #region ResourceNode_ConstructorCorrectlyHandlesNull        
        /// <summary>
        /// Asserts that <see cref="ResourceNode.ResourceNode(Int32, String, Resource)"/> correctly handles null input.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name.</param>
        /// <param name="resourceType">Type of the resource.</param>
        [Test]
        #region Test-Cases
        [TestCase(0, null, NUL)]
        [TestCase(0, "Albert", NUL)]
        [TestCase(0, null, DES)]
        #endregion
        public void ResourceNode_ConstructorCorrectlyHandlesNull(Int32 id, String name, Int32 resourceType) {
            Resource? theResource = CreateResource(resourceType);

            Assert.That(new ResourceNode(id, name, theResource), Throws.InstanceOf(typeof(ArgumentNullException)));
        }
        #endregion

        #endregion

        #region ResourceNode_GetMaxPawnLimitReturnsCorrectNumber        
        /// <summary>
        /// Asserts that <see cref="ResourceNode.MaxPawnLimit"/> returns the current number.
        /// </summary>
        [Test]
        #region Test-Cases
        [TestCase(typeof(Requirements))]
        [TestCase(typeof(Design))]
        [TestCase(typeof(Implementation))]
        [TestCase(typeof(Testing))] 
        #endregion
        public void ResourceNode_GetMaxPawnLimitReturnsCorrectNumber(Type resourceType) {
            ResourceNode sampleNode = GetTestNode(resourceType);
            Assert.That(sampleNode.MaxPawnLimit, Is.EqualTo(RESOURCE_NODE_PAWN_LIMIT));
        }
        #endregion

        #region Private test methods

        private ResourceNode GetTestNode(Type type) {
            if(type == typeof(Requirements))
                return this.reqNode;
            else if(type == typeof(Design))
                return this.desNode;
            else if(type == typeof(Implementation))
                return this.impNode;
            else if(type == typeof(Testing))
                return this.tesNode;
            else
                return null;
        }

        private Resource CreateResource(Int32 type) {
            if(type == REQ)
                return new Requirements();
            else if(type == DES)
                return new Design();
            else if(type == IMP)
                return new Implementation();
            else if(type == TES)
                return new Testing();
            return null;
        }

        private Int32[] GetResourceContainerContents(Player player) {
            Int32[] containerContents = new Int32[4];
            containerContents[0] = player.GetPlayerResources()[new Requirements()];
            containerContents[1] = player.GetPlayerResources()[new Design()];
            containerContents[2] = player.GetPlayerResources()[new Implementation()];
            containerContents[3] = player.GetPlayerResources()[new Testing()];
            return containerContents;
        }

        private Int32 GetChance(Pawn pawn, Type type) {
            if(type == typeof(Requirements)) {
                if(pawn.PawnType == "Back End")
                    return REQ_BACK_END_CHANCE;
                else if(pawn.PawnType == "Front End")
                    return REQ_FRONT_END_CHANCE;
                return REQ_FULL_STACK_CHANCE;
            }
            else if(type == typeof(Design)) {
                if(pawn.PawnType == "Back End")
                    return DES_BACK_END_CHANCE;
                else if(pawn.PawnType == "Front End")
                    return DES_FRONT_END_CHANCE;
                return DES_FULL_STACK_CHANCE;
            }
            else if(type == typeof(Implementation)) {
                if(pawn.PawnType == "Back End")
                    return IMP_BACK_END_CHANCE;
                else if(pawn.PawnType == "Front End")
                    return IMP_FRONT_END_CHANCE;
                return IMP_FULL_STACK_CHANCE;
            }
            else {
                if(pawn.PawnType == "Back End")
                    return TES_BACK_END_CHANCE;
                else if(pawn.PawnType == "Front End")
                    return TES_FRONT_END_CHANCE;
                return TES_FULL_STACK_CHANCE;
            }
        }
        #endregion

        #region Category: Subclass Methods        

        #region ResourceNode_RollForResource_SuccessRateMustBeAboveOrEqualToRandomRollToSucceed
        /// <summary>
        /// Asserts that <see cref="ResourceNode.RollForResource(Int32)"/> requires roll to be lower than success rate.
        /// </summary>
        /// <param name="successRate">The success rate.</param>
        /// <param name="expectedResult">The expected result of roll.</param>
        [Test]
        [TestCase(100, true)] // Always succeed
        [TestCase(0, false)]  // Always fail
        public void ResourceNode_RollForResource_SuccessRateMustBeAboveOrEqualToRandomRollToSucceed(Int32 successRate, Boolean expectedResult) {
            ResourceNode sampleNode = reqNode;
            Boolean[] distribution = new Boolean[10000];
            for(Int32 i = 0; i < 10000; i++) {
                distribution[i] = sampleNode.RollForResource(successRate);
            }
            Assert.That(distribution, Has.All.EqualTo(expectedResult));
        }
        #endregion

        #region ResourceNode_AccumulateResourceChances_ReturnsCorrectValue        
        /// <summary>
        /// Asserts that <see cref="ResourceNode.AccumulateResourceChances(IEnumerable{Pawn})"/> returns current value.
        /// </summary>
        /// <param name="pawnType">Type of the pawn.</param>
        /// <param name="resourceType">Type of the resource.</param>
        [Test]
        #region Test-Cases
        [TestCase("Full Stack", typeof(Requirements))]
        [TestCase("Back End", typeof(Requirements))]
        [TestCase("Front End", typeof(Requirements))]
        [TestCase("Full Stack", typeof(Design))]  
        #endregion
        public void ResourceNode_AccumulateResourceChances_ReturnsCorrectValue(String pawnType, Type resourceType) {
            ResourceNode sampleNode = GetTestNode(resourceType);
            Pawn testPawn = new Pawn(0, pawnType);
            Int32 expectedChance = 20 + GetChance(testPawn, resourceType);
            
            Int32 returnedChance = sampleNode.AccumulateResourceChances(new Pawn[] { testPawn });

            Assert.That(returnedChance, Is.EqualTo(expectedChance));
        }
        #endregion

        #endregion

        #region ResourceNode_DoAction_DoesNotReturnPawnsToIncorrectPlayer        
        /// <summary>
        /// Asserts that <see cref="ResourceNode.DoAction(Player)"/> does not return node pawns to incorrect players.
        /// </summary>
        [Test]
        [Category("General DoAction")]
        public void ResourceNode_DoAction_DoesNotReturnPawnsToIncorrectPlayer() {
            ResourceNode sampleNode = reqNode;
            Int32 originalPawnCount2 = testPlayer2.Pawns.Count;

            sampleNode.AddPawn(testPlayer1.TakePawn("Back End"));
            sampleNode.AddPawn(testPlayer1.TakePawn("Front End"));
            sampleNode.AddPawn(testPlayer1.TakePawn("Front End"));
            sampleNode.AddPawn(testPlayer1.TakePawn("Full Stack"));

            Int32 originalNodePawnCount = sampleNode.Pawns.Count;

            sampleNode.DoAction(testPlayer2);

            // Assert no change in sampleNode or testPlayer2
            Assert.That(sampleNode.Pawns.Count, Is.EqualTo(originalNodePawnCount));
            Assert.That(testPlayer2.Pawns.Count, Is.EqualTo(originalPawnCount2));
        }
        #endregion

        #region ResourceNode_DoAction_ReturnsCorrectPawnsToPlayer
        /// <summary>
        /// Asserts that the pawn in the node is correctly returned to the player
        /// </summary>
        [Test]
        [Category("General DoAction")]
        public void ResourceNode_DoAction_ReturnsCorrectPawnsToCorrectPlayer() {
            ResourceNode sampleNode = reqNode;
            Int32 originalPawnCount = testPlayer1.Pawns.Count;

            sampleNode.AddPawn(testPlayer1.TakePawn("Back End"));
            sampleNode.AddPawn(testPlayer1.TakePawn("Front End"));
            sampleNode.AddPawn(testPlayer1.TakePawn("Front End"));
            sampleNode.AddPawn(testPlayer1.TakePawn("Full Stack"));

            sampleNode.DoAction(testPlayer1);

            Int32 newPawnCount = testPlayer1.Pawns.Count;

            Assert.That(newPawnCount, Is.EqualTo(originalPawnCount));
        }
        #endregion

        #region ResourceNode_DoAction_ReturnsCorrectStringOnNoPawnError
        /// <summary>
        /// Asserts that <see cref="ResourceNode.DoAction(Player)"/> returns the correct string on action failure.
        /// </summary>
        [Test]
        #region Test-Cases
        [TestCase(typeof(Requirements))]
        [TestCase(typeof(Design))]
        [TestCase(typeof(Implementation))]
        [TestCase(typeof(Testing))]
        #endregion
        [Category("General DoAction")]
        public void ResourceNode_DoAction_ReturnsCorrectStringOnNoPawnError(Type resourceType) {
            ResourceNode sampleNode = GetTestNode(resourceType);
            
            sampleNode.AddPawn(testPlayer1.TakePawn("Back End"));

            String stringOutput = sampleNode.DoAction(testPlayer2);
            
            Assert.That(stringOutput, Is.EqualTo($"{testPlayer2.PlayerName} has failed to obtain a {sampleNode.nodeResource.Name}. Reason: No Pawns."));
        }
        #endregion

        #region ResourceNode_DoAction_ReturnsCorrectStringOnSuccess
        /// <summary>
        /// Asserts that <see cref="ResourceNode.DoAction(Player)"/> returns the correct string on roll success or failure.
        /// </summary>
        [Test]
        #region ResourceNode_DoAction_ReturnsCorrectStringOnSuccessRollOrFailRoll
        [TestCase(typeof(Requirements))]
        [TestCase(typeof(Design))]
        [TestCase(typeof(Implementation))]
        [TestCase(typeof(Testing))] 
        #endregion
        [Category("General DoAction")]
        public void ResourceNode_DoAction_ReturnsCorrectStringOnSuccessRollOrFailRoll(Type resourceType) {
            ResourceNode sampleNode = GetTestNode(resourceType);

            sampleNode.AddPawn(testPlayer1.TakePawn("Back End"));

            String stringOutput = sampleNode.DoAction(testPlayer1);

            Int32[] gainedResources = GetResourceContainerContents(testPlayer1);

            if(gainedResources.Any<Int32>(_val => _val > 0))
                Assert.That(stringOutput, Is.EqualTo($"{testPlayer1.PlayerName} has acquired a {sampleNode.nodeResource.Name}!"));
            else
                Assert.That(stringOutput, Is.EqualTo($"{testPlayer1.PlayerName} has failed to obtain a {sampleNode.nodeResource.Name}"));
        }

        #endregion

    }
}
