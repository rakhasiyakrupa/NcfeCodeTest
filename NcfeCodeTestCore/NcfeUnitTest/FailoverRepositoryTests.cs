using Ncfe.NcfeCodeTestCore;
using NUnit.Framework;

namespace NcfeUnitTest
{
    [TestFixture]
    public class FailoverRepositoryTests
    {
        private FailoverRepository _subjectToTest;
        [SetUp]
        public void Setup()
        {
            _subjectToTest = new FailoverRepository();
        }

        [Test]
        public void TestGetFailOverEntries()
        {
            var failoverEntries = _subjectToTest.GetFailOverEntries();
            Assert.IsNotNull(failoverEntries);
        }
    }
}
