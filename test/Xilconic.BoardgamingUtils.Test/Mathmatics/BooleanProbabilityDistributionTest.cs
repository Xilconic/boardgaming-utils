using NUnit.Framework;
using Xilconic.BoardgamingUtils.Mathmatics;

namespace Xilconic.BoardgamingUtils.Test.Mathmatics
{
    [TestFixture]
    public class BooleanProbabilityDistributionTest
    {
        [TestCase(0.0)]
        [TestCase(0.7)]
        [TestCase(1.0)]
        public void Constructor_ExpectedValues(double succesProbability)
        {
            // Call
            var distribution = new BooleanProbabilityDistribution(succesProbability);

            // Assert
            Assert.AreEqual(succesProbability, distribution.SuccessProbability);
            Assert.AreEqual(1.0-succesProbability, distribution.FailureProbability);
        }
    }
}
