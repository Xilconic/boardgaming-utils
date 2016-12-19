using NUnit.Framework;
using Rhino.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xilconic.BoardgamingUtils.Dice;
using Xilconic.BoardgamingUtils.Mathmatics;
using Xilconic.BoardgamingUtils.PseudoRandom;

namespace Xilconic.BoardgamingUtils.Test.Dice
{
    [TestFixture]
    public class SumOfDiceTest
    {
        [Test]
        public void ProbabilityDistribution_OneD3_ReturnExpectedDistribution()
        {
            // Setup
            var rng = MockRepository.GenerateStub<IRandomNumberGenerator>();
            var dice = new[]
            {
                new NumericalDie(3, rng),
            };

            var sumOf2D6 = new SumOfDice(dice);

            // Call
            DiscreteValueProbabilityDistribution distribution = sumOf2D6.ProbabilityDistribution;

            // Assert
            var expectedProbabilityValuePairs = new[]
            {
                Tuple.Create(1, 1.0/3.0),
                Tuple.Create(2, 1.0/3.0),
                Tuple.Create(3, 1.0/3.0),
            };
            Assert.AreEqual(expectedProbabilityValuePairs.Length, distribution.Specification.Count);
            for (int i = 0; i < expectedProbabilityValuePairs.Length; i++)
            {
                Tuple<int, double> expectedPair = expectedProbabilityValuePairs[i];
                ValueProbabilityPair actualPair = distribution.Specification[i];

                Assert.AreEqual(expectedPair.Item1, actualPair.Value);
                Assert.AreEqual(expectedPair.Item2, actualPair.Probability, 1e-6);
            }
        }

        [Test]
        public void ProbabilityDistribution_TwoD6_ReturnExpectedDistribution()
        {
            // Setup
            var rng = MockRepository.GenerateStub<IRandomNumberGenerator>();
            var dice = new[]
            {
                new NumericalDie(6, rng),
                new NumericalDie(6, rng)
            };

            var sumOf2D6 = new SumOfDice(dice);

            // Call
            DiscreteValueProbabilityDistribution distribution = sumOf2D6.ProbabilityDistribution;

            // Assert
            var expectedProbabilityValuePairs = new[]
            {
                Tuple.Create(2, 1.0/36.0),
                Tuple.Create(3, 2.0/36.0),
                Tuple.Create(4, 3.0/36.0),
                Tuple.Create(5, 4.0/36.0),
                Tuple.Create(6, 5.0/36.0),
                Tuple.Create(7, 6.0/36.0),
                Tuple.Create(8, 5.0/36.0),
                Tuple.Create(9, 4.0/36.0),
                Tuple.Create(10, 3.0/36.0),
                Tuple.Create(11, 2.0/36.0),
                Tuple.Create(12, 1.0/36.0),
            };
            Assert.AreEqual(expectedProbabilityValuePairs.Length, distribution.Specification.Count);
            for(int i = 0; i < expectedProbabilityValuePairs.Length; i++)
            {
                Tuple<int, double> expectedPair = expectedProbabilityValuePairs[i];
                ValueProbabilityPair actualPair = distribution.Specification[i];

                Assert.AreEqual(expectedPair.Item1, actualPair.Value);
                Assert.AreEqual(expectedPair.Item2, actualPair.Probability, 1e-6);
            }
        }
    }
}
