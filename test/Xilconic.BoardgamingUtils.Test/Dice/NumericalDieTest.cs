using NUnit.Framework;
using Rhino.Mocks;
using System;
using System.Linq;
using Xilconic.BoardgamingUtils.Dice;
using Xilconic.BoardgamingUtils.Mathmatics;
using Xilconic.BoardgamingUtils.PseudoRandom;
using Xilconic.BoardgamingUtils.Test.TestDoubles;

namespace Xilconic.BoardgamingUtils.Test.Dice
{
    [TestFixture]
    public class NumericalDieTest
    {
        [TestCase(1)]
        [TestCase(120)]
        public void Constructor_ExpectedValues(int numberOfSides)
        {
            // Setup
            var rng = MockRepository.GenerateStub<IRandomNumberGenerator>();

            // Call
            var die = new NumericalDie(numberOfSides, rng);

            // Assert
            Assert.IsInstanceOf<AbstractDie>(die);
            Assert.AreEqual(numberOfSides, die.NumberOfSides);
        }

        [TestCase(1)]
        [TestCase(6)]
        [TestCase(120)]
        public void ProbabilityDistribution_ForSomeNumberOfSides_DefinesUniformProbabilityDistribution(int numberOfSides)
        {
            // Setup
            var rng = MockRepository.GenerateStub<IRandomNumberGenerator>();

            var die = new NumericalDie(numberOfSides, rng);

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

        [Test]
        public void Roll_ReturnRandomlyGeneratedValuesAccordingToProbabilityDistribution()
        {
            // Setup
            const double uniformProbability = 1.0 / 6.0;
            var rngValueAndExpectedResults = new[]
            {
                Tuple.Create(0.0, 1),
                Tuple.Create(0.45*uniformProbability, 1),
                Tuple.Create(uniformProbability, 1),
                Tuple.Create(uniformProbability + 1e-6, 2),
                Tuple.Create(1.34*uniformProbability, 2),
                Tuple.Create(2*uniformProbability, 2),
                Tuple.Create(2*uniformProbability + 1e-6, 3),
                Tuple.Create(2.89*uniformProbability, 3),
                Tuple.Create(3*uniformProbability, 3),
                Tuple.Create(3*uniformProbability + 1e-6, 4),
                Tuple.Create(3.75*uniformProbability, 4),
                Tuple.Create(4*uniformProbability, 4),
                Tuple.Create(4*uniformProbability + 1e-6, 5),
                Tuple.Create(4.28*uniformProbability, 5),
                Tuple.Create(5*uniformProbability, 5),
                Tuple.Create(5*uniformProbability + 1e-6, 6),
                Tuple.Create(5.83*uniformProbability, 6),
                Tuple.Create(6*uniformProbability, 6)
            };
            var rng = new TestingRandomNumberGenerator();
            rng.AddFactorValues(rngValueAndExpectedResults.Select(t => t.Item1));

            var die = new NumericalDie(6, rng);

            foreach(Tuple<double, int> pair in rngValueAndExpectedResults)
            {
                // Call
                int dieResult = die.Roll();

                // Assert
                Assert.AreEqual(pair.Item2, dieResult,
                    $"Asserting for probability value {pair.Item1}.");
            }
        }
    }
}
