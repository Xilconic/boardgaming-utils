using NUnit.Framework;
using System.Linq;
using Xilconic.BoardgamingUtils.Dice;
using Xilconic.BoardgamingUtils.Mathmatics;

namespace Xilconic.BoardgamingUtils.Test.Dice
{
    [TestFixture]
    public class NumericalDieTest
    {
        [TestCase(1)]
        [TestCase(120)]
        public void Constructor_ExpectedValues(int numberOfSides)
        {
            // Call
            var die = new NumericalDie(numberOfSides);

            // Assert
            Assert.AreEqual(numberOfSides, die.NumberOfSides);
        }

        [TestCase(1)]
        [TestCase(6)]
        [TestCase(120)]
        public void ProbabilityDistribution_ForSomeNumberOfSides_DefinesUniformProbabilityDistribution(int numberOfSides)
        {
            // Setup
            var die = new NumericalDie(numberOfSides);

            // Call
            DiscreteValueProbabilityDistribution distribution = die.ProbabilityDistribution;

            // Assert
            double uniformProbability = 1.0 / numberOfSides;
            foreach(ValueProbabilityPair pair in distribution.Specification)
            {
                Assert.AreEqual(uniformProbability, pair.Probability);
            }
            CollectionAssert.AreEquivalent(Enumerable.Range(1, numberOfSides), distribution.Specification.Select(p => p.Value));
        }
    }
}
