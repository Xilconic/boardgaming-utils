using NUnit.Framework;
using System.Collections.ObjectModel;
using System.Linq;
using Xilconic.BoardgamingUtils.Mathmatics;

namespace Xilconic.BoardgamingUtils.Test.Mathmatics
{
    [TestFixture]
    public class DiscreteValueProbabilityDistributionTest
    {
        [Test]
        public void Constructor_ValidDistributionSpecified_ExpectedValues()
        {
            // Setup
            var valueProbabilityPairs = new[]
            {
                new ValueProbabilityPair(1, 0.5),
                new ValueProbabilityPair(2, 0.25),
                new ValueProbabilityPair(3, 0.10),
                new ValueProbabilityPair(4, 0.15),
            };

            // Call
            var distribution = new DiscreteValueProbabilityDistribution(valueProbabilityPairs);

            // Assert
            ReadOnlyCollection<ValueProbabilityPair> specification = distribution.Specification;
            Assert.AreEqual(valueProbabilityPairs.Length, specification.Count);
            foreach(ValueProbabilityPair expectedPair in valueProbabilityPairs)
            {
                ValueProbabilityPair pair = specification.Single(s => s.Value == expectedPair.Value);
                Assert.AreEqual(expectedPair.Probability, pair.Probability);
            }
        }
    }
}
