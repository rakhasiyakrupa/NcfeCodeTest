using NUnit.Framework;
using Ncfe.NcfeCodeTestCore;
using Services = Ncfe.NcfeCodeTestCore.Services;

namespace NcfeUnitTest
{
    [TestFixture]
    public class LearnerServiceTests
    {

        #region Declaration

        private Services.LearnerService _learnerService;

        #endregion

        [SetUp]
        public void Setup()
        {
            var archivedDataService = new ArchivedDataService();
            var failoverRepository = new FailoverRepository();
            var learnerDataAccess = new LearnerDataAccess();
            _learnerService = new Services.LearnerService(archivedDataService, failoverRepository, learnerDataAccess);
        }

        [Test]
        [TestCase(1, true)]
        public void TestReturnValidArchivedLearner(int learnerId, bool isLearnerArchived)
        {
            var actualArchiveLearner = _learnerService.GetLearner(learnerId, isLearnerArchived);
            Assert.IsNotNull(actualArchiveLearner);
        }

        [Test]
        [TestCase(0, true)]
        public void TestReturnInValidArchivedLearner(int learnerId, bool isLearnerArchived)
        {
            var actualArchiveLearner = _learnerService.GetLearner(learnerId, isLearnerArchived);
            Assert.IsNull(actualArchiveLearner);
        }
    }
}
