using NUnit.Framework;

namespace ScrumageWPF.Test {
    
    
    [TestFixture]
    public class NameOfClassToBeTested_Tests {
        
        // This method is run at the beginning of this testing class
        [OneTimeSetUp]
        public void ClassSetup() {
        }

        // This method is run at the end of this testing class
        [OneTimeTearDown]
        public void ClassTearDown() {
        }

        // This method is run before each [Test] in this class
        [SetUp]
        public void Setup() {
        }

        // This method is run after each [Test] in this class
        [TearDown]
        public void TearDown() {
        }


        // A test to be run
        [Test]
        public void Test1() {
            Assert.Pass();
        }
    }
}