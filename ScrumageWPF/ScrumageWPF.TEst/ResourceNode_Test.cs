using System;
using System.Collections.Generic;
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

        #region Fields
        private Player testPlayer1;
        private Player testPlayer2;

        private const Int32 RESOURCE_NODE_PAWN_LIMIT = 7;

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
        #endregion

        #region Testing Suite Helper Methods

        #region Setup and Teardown        
        /// <summary>
        /// Setup this testing class.
        /// </summary>
        [OneTimeSetUp]
        public void ResourceNode_ClassSetUp() {
            testPlayer1 = new Player(1, "testPlayer1");
            testPlayer2 = new Player(2, "testPlayer2");
        }

        /// <summary>
        /// Cleanup this testing class.
        /// </summary>
        [OneTimeTearDown]
        public void ResourceNode_ClassTearDown() {
            testPlayer1 = null;
            testPlayer2 = null;
        }

        /// <summary>
        /// Setup before each testing method.
        /// </summary>
        [SetUp]
        public void ResourceNode_MethodSetUp() {
            testPlayer1.GivePawn(new Pawn(testPlayer1.PlayerID, "Back End"));
            testPlayer1.GivePawn(new Pawn(testPlayer1.PlayerID, "Front End"));
            testPlayer1.GivePawn(new Pawn(testPlayer1.PlayerID, "Full Stack"));
            testPlayer1.GivePawn(new Pawn(testPlayer1.PlayerID, "Full Stack"));
            testPlayer1.GivePawn(new Pawn(testPlayer1.PlayerID, "Full Stack"));
            testPlayer1.GivePawn(new Pawn(testPlayer1.PlayerID, "Full Stack"));
            testPlayer1.GivePawn(new Pawn(testPlayer1.PlayerID, "Full Stack"));
            testPlayer1.GivePawn(new Pawn(testPlayer1.PlayerID, "Full Stack"));

            testPlayer2.GivePawn(new Pawn(testPlayer2.PlayerID, "Back End"));
            testPlayer2.GivePawn(new Pawn(testPlayer2.PlayerID, "Front End"));
            testPlayer2.GivePawn(new Pawn(testPlayer2.PlayerID, "Full Stack"));
            testPlayer2.GivePawn(new Pawn(testPlayer2.PlayerID, "Full Stack"));
            testPlayer2.GivePawn(new Pawn(testPlayer2.PlayerID, "Full Stack"));
            testPlayer2.GivePawn(new Pawn(testPlayer2.PlayerID, "Full Stack"));
            testPlayer2.GivePawn(new Pawn(testPlayer2.PlayerID, "Full Stack"));
            testPlayer2.GivePawn(new Pawn(testPlayer2.PlayerID, "Full Stack"));

            testPlayer1.playerResources = new ResourceContainer();
            testPlayer1.playerResources = new ResourceContainer();
        }

        /// <summary>
        /// Cleanup after each testing method.
        /// </summary>
        [TearDown]
        public void ResourceNode_MethodTearDown() {
            this.testPlayer1.Pawns.Clear();
            this.testPlayer2.Pawns.Clear();
        }
        #endregion

        #region Private test methods        
        /// <summary>
        /// References a private <see cref="ResourceNode"/> object to be used for testing.
        /// </summary>
        /// <param name="type">The resource type of the needed node.</param>
        /// <returns></returns>
        private ResourceNode CreateResourceNode(Type resourceType) {
            if(resourceType == typeof(Requirements))
                return new ResourceNode(0, "requirementsNode", new Requirements());
            else if(resourceType == typeof(Design))
                return new ResourceNode(1, "designNode", new Design());
            else if(resourceType == typeof(Implementation))
                return new ResourceNode(2, "implementationNode", new Implementation());
            else
                return new ResourceNode(3, "testingNode", new Testing());
        }

        /// <summary>
        /// Creates a <see cref="Resource"/> based on a given <see cref="System.Type"/>.
        /// </summary>
        /// <param name="type">The resource type.</param>
        /// <returns>a new Resource subclass.</returns>
        private Resource CreateResource(Type type) {
            if(type == typeof(Requirements))
                return new Requirements();
            else if(type == typeof(Design))
                return new Design();
            else if(type == typeof(Implementation))
                return new Implementation();
            else if(type == typeof(Testing))
                return new Testing();
            return null;
        }

        /// <summary>
        /// Returns the amount of each resource in a <see cref="ResourceContainer"/> object.
        /// </summary>
        /// <param name="player">The player whose Resource Container we want.</param>
        /// <returns>an array of the values of the resources.</returns>
        private Int32[] GetResourceContainerContents(Player player) {
            Int32[] containerContents = new Int32[4];
            containerContents[0] = player.GetPlayerResources()[new Requirements()];
            containerContents[1] = player.GetPlayerResources()[new Design()];
            containerContents[2] = player.GetPlayerResources()[new Implementation()];
            containerContents[3] = player.GetPlayerResources()[new Testing()];
            return containerContents;
        }

        /// <summary>
        /// Gets the success rate for a given <see cref="Pawn.PawnType"/> for a particular <see cref="Resource"/> type.
        /// </summary>
        /// <param name="pawn">The pawn to check.</param>
        /// <param name="type">The resource type.</param>
        /// <returns>The success rate for the pawn type for a particular resource.</returns>
        private Int32 GetChance(String pawnType, Type type) {
            if(type == typeof(Requirements)) {
                if(pawnType == "Back End")
                    return REQ_BACK_END_CHANCE;
                else if(pawnType == "Front End")
                    return REQ_FRONT_END_CHANCE;
                return REQ_FULL_STACK_CHANCE;
            }
            else if(type == typeof(Design)) {
                if(pawnType == "Back End")
                    return DES_BACK_END_CHANCE;
                else if(pawnType == "Front End")
                    return DES_FRONT_END_CHANCE;
                return DES_FULL_STACK_CHANCE;
            }
            else if(type == typeof(Implementation)) {
                if(pawnType == "Back End")
                    return IMP_BACK_END_CHANCE;
                else if(pawnType == "Front End")
                    return IMP_FRONT_END_CHANCE;
                return IMP_FULL_STACK_CHANCE;
            }
            else {
                if(pawnType == "Back End")
                    return TES_BACK_END_CHANCE;
                else if(pawnType == "Front End")
                    return TES_FRONT_END_CHANCE;
                return TES_FULL_STACK_CHANCE;
            }
        }
        #endregion

        #endregion

        #region ResourceNode Tests

        #region Category: Instantiation

        #region ResourceNode_ConstructorInstantiatesCorrectly        
        /// <summary>
        /// Asserts that <see cref="ResourceNode.ResourceNode(Int32, String, Resource)"/> instantiates correctly.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name.</param>
        /// <param name="resourceType">Type of the resource.</param>
        [Test]
        [Category("Instantiation")]
        #region Test-Cases
        [TestCase(0, "Node1", typeof(Requirements))]
        [TestCase(6, "Albert", typeof(Design))]
        [TestCase(888, "Node943", typeof(Implementation))]
        [TestCase(1, "null", typeof(Testing))]
        #endregion
        public void ResourceNode_ConstructorInstantiatesCorrectly(Int32 id, String name, Type resourceType) {
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
        [Category("Instantiation")]
        #region Test-Cases
        [TestCase(0, null, null)]
        [TestCase(0, "Albert", null)]
        [TestCase(0, null, typeof(Design))]
        #endregion
        public void ResourceNode_ConstructorCorrectlyHandlesNull(Int32 id, String name, Type resourceType) {
            Resource? theResource = CreateResource(resourceType);

            Assert.That(new ResourceNode(id, name, theResource), Throws.InstanceOf(typeof(ArgumentNullException)));
        }
        #endregion

        #endregion

        #region Category: ResourceNode Unique Methods       

        #region ResourceNode_RollForResource_SuccessRateMustBeAboveOrEqualToRandomRollToSucceed
        /// <summary>
        /// Asserts that <see cref="ResourceNode.RollForResource(Int32)"/> requires roll to be lower than success rate.
        /// </summary>
        /// <param name="successRate">The success rate.</param>
        /// <param name="expectedResult">The expected result of roll.</param>
        [Test]
        [Category("ResourceNode Unique Methods")]
        [TestCase(100, true)] // Always succeed
        [TestCase(0, false)]  // Always fail
        public void ResourceNode_RollForResource_SuccessRateMustBeAboveOrEqualToRandomRollToSucceed(Int32 successRate, Boolean expectedResult) {
            ResourceNode sampleNode = CreateResourceNode(typeof(Requirements));
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
        [Category("ResourceNode Unique Methods")]
        #region Test-Cases
        [TestCase("Full Stack", typeof(Requirements))]
        [TestCase("Back End", typeof(Requirements))]
        [TestCase("Front End", typeof(Requirements))]
        [TestCase("Full Stack", typeof(Design))]
        #endregion
        public void ResourceNode_AccumulateResourceChances_ReturnsCorrectValue(String pawnType, Type resourceType) {
            ResourceNode sampleNode = CreateResourceNode(resourceType);
            Int32 expectedChance = 20 + GetChance(pawnType, resourceType);

            Int32 returnedChance = sampleNode.AccumulateResourceChances(new Pawn[] { new Pawn(0, pawnType) });

            Assert.That(returnedChance, Is.EqualTo(expectedChance));
        }
        #endregion

        #endregion

        #region Category: General DoAction

        #region ResourceNode_DoAction_DoesNotReturnPawnsToIncorrectPlayer        
        /// <summary>
        /// Asserts that <see cref="ResourceNode.DoAction(Player)"/> does not return node pawns to incorrect players.
        /// </summary>
        [Test]
        [Category("General DoAction")]
        public void ResourceNode_DoAction_DoesNotReturnPawnsToIncorrectPlayer() {
            ResourceNode sampleNode = CreateResourceNode(typeof(Requirements));
            Int32 originalPawnCount2 = testPlayer2.Pawns.Count;

            sampleNode.AddPawn(testPlayer1.TakePawn("Back End"));
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
            ResourceNode sampleNode = CreateResourceNode(typeof(Requirements));
            Int32 originalPawnCount = testPlayer1.Pawns.Count;

            sampleNode.AddPawn(testPlayer1.TakePawn("Back End"));
            sampleNode.AddPawn(testPlayer1.TakePawn("Front End"));
            sampleNode.AddPawn(testPlayer1.TakePawn("Full Stack"));

            sampleNode.DoAction(testPlayer1);

            Int32 newPawnCount = testPlayer1.Pawns.Count;

            Assert.That(newPawnCount, Is.EqualTo(originalPawnCount));
        }
        #endregion

        #endregion

        #region Category: ResourceNode.DoAction

        #region ResourceNode_DoAction_ReturnsCorrectStringOnNoPawnError
        /// <summary>
        /// Asserts that <see cref="ResourceNode.DoAction(Player)"/> returns the correct string on action failure.
        /// </summary>
        [Test]
        [Category("ResourceNode.DoAction")]
        #region Test-Cases
        [TestCase(typeof(Requirements))]
        [TestCase(typeof(Design))]
        [TestCase(typeof(Implementation))]
        [TestCase(typeof(Testing))]
        #endregion
        public void ResourceNode_DoAction_ReturnsCorrectStringOnNoPawnError(Type resourceType) {
            ResourceNode sampleNode = CreateResourceNode(resourceType);

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
        [Category("ResourceNode.DoAction")]
        #region Test-Cases
        [TestCase(typeof(Requirements))]
        [TestCase(typeof(Design))]
        [TestCase(typeof(Implementation))]
        [TestCase(typeof(Testing))]
        #endregion

        public void ResourceNode_DoAction_ReturnsCorrectStringOnSuccessRollOrFailRoll(Type resourceType) {
            ResourceNode sampleNode = CreateResourceNode(resourceType);

            sampleNode.AddPawn(testPlayer1.TakePawn("Back End"));

            String stringOutput = sampleNode.DoAction(testPlayer1);

            Int32[] gainedResources = GetResourceContainerContents(testPlayer1);

            if(gainedResources.Any<Int32>(_val => _val > 0))
                Assert.That(stringOutput, Is.EqualTo($"{testPlayer1.PlayerName} has acquired a {sampleNode.nodeResource.Name}!"));
            else
                Assert.That(stringOutput, Is.EqualTo($"{testPlayer1.PlayerName} has failed to obtain a {sampleNode.nodeResource.Name}"));
        }

        #endregion

        #region ResourceNode_DoAction_AddsCorrectResourceToPlayer        
        /// <summary>
        /// Asserts that <see cref="ResourceNode.DoAction(Player)"/> adds the correct resource to the player.
        /// </summary>
        /// <param name="resourceType">Type of the resource.</param>
        [Test]
        [Category("ResourceNode.DoAction")]
        #region Test-Cases
        [TestCase(typeof(Requirements))]
        [TestCase(typeof(Design))]
        [TestCase(typeof(Implementation))]
        [TestCase(typeof(Testing))]
        #endregion
        public void ResourceNode_DoAction_AddsCorrectResourceToPlayer(Type resourceType) {
            ResourceNode sampleNode = CreateResourceNode(resourceType);
            Resource expectedResource = CreateResource(resourceType);

            // Guarantee successful roll
            for(Int32 i = 0; i < 6; i++) {
                sampleNode.AddPawn(testPlayer1.TakePawn("Full Stack"));
            }

            sampleNode.DoAction(testPlayer1);

            Int32 totalResourcesCount = testPlayer1.playerResources.ResourceDictionary.Values.ToList<Int32>().Sum();

            // Assert that the expected resource has been obtained.
            Assert.That(testPlayer1.playerResources.ResourceDictionary[expectedResource], Is.EqualTo(1));
            // Assert that no other resource has been altered.
            Assert.That(totalResourcesCount, Is.EqualTo(1));
        }
        #endregion

        #region ResourceNode_DoAction_ReturnsDeepCopyOfNodeResource      
        /// <summary>
        /// Asserts that <see cref="ResourceNode.DoAction(Player)"/> returns a deep copy of <see cref="ResourceNode.nodeResource"/>.\
        /// See also <seealso cref="Resource.DeepCopy"/>.
        /// </summary>
        /// <param name="resourceType">Type of the resource.</param>
        [Test]
        [Category("ResourceNode.DoAction")]
        #region Test-Cases
        [TestCase(typeof(Requirements))]
        [TestCase(typeof(Design))]
        [TestCase(typeof(Implementation))]
        [TestCase(typeof(Testing))]
        #endregion
        public void ResourceNode_DoAction_ReturnsDeepCopyOfNodeResource(Type resourceType) {
            ResourceNode sampleNode = CreateResourceNode(resourceType);
            Resource indexResource = CreateResource(resourceType);

            // Guarantee successful roll
            for(Int32 i = 0; i < 6; i++) {
                sampleNode.AddPawn(testPlayer1.TakePawn("Full Stack"));
            }

            sampleNode.DoAction(testPlayer1);
            Assert.That(testPlayer1.playerResources.ResourceDictionary[indexResource], Is.Not.SameAs(sampleNode.nodeResource));
        }
        #endregion

        #endregion

        #region Category: Other

        #region ResourceNode_GetMaxPawnLimitReturnsCorrectNumber        
        /// <summary>
        /// Asserts that <see cref="ResourceNode.MaxPawnLimit"/> returns the current number.
        /// </summary>
        [Test]
        [Category("Other")]
        #region Test-Cases
        [TestCase(typeof(Requirements))]
        [TestCase(typeof(Design))]
        [TestCase(typeof(Implementation))]
        [TestCase(typeof(Testing))]
        #endregion
        public void ResourceNode_GetMaxPawnLimitReturnsCorrectNumber(Type resourceType) {
            ResourceNode sampleNode = CreateResourceNode(resourceType);
            Assert.That(sampleNode.MaxPawnLimit, Is.EqualTo(RESOURCE_NODE_PAWN_LIMIT));
        }
        #endregion

        #endregion 

        #endregion

    }
}
