using NUnit.Framework;
using Ncfe.NcfeCodeTestCore;

namespace NcfeUnitTest
{
    [TestFixture]
    public class LearnerFromThirdPartyTests
    {
        private LearnerDataAccess _learnerDataAccess;
        [SetUp]
        public void Setup()
        {
            _learnerDataAccess = new LearnerDataAccess();
        }

        [Test]
        [TestCase(1)]
        public void TestGetValidLearnerFromThirdParty(int learnerId)
        {
            var LearnerFromThirdParty = _learnerDataAccess.LoadLearner(learnerId);
            Assert.IsNotNull(LearnerFromThirdParty);
        }
    }
}
