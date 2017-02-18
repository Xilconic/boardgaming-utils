// This file is part of Boardgaming Utils.
//
// Boardgaming Utils is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// Boardgaming Utils is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with Boardgaming Utils. If not, see <http://www.gnu.org/licenses/>.
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
    public class SumOfDiceTest
    {
        [Test]
        public void Constructor_ExpectedValues()
        {
            // Setup
            var rng = MockRepository.GenerateStub<IRandomNumberGenerator>();
            var die = MockRepository.GenerateStub<IAbstractDie>();
            die.Stub(d => d.ProbabilityDistribution).Return(new DiscreteValueProbabilityDistribution(new[] { new ValueProbabilityPair(0, 1.0) }));

            var dice = new[]
            {
                die
            };

            // Call
            var sumOfDice = new SumOfDice(dice, rng);

            // Setup
            Assert.IsInstanceOf<IAbstractDie>(sumOfDice);
        }

        [Test]
        public void ProbabilityDistribution_OneD3_ReturnExpectedDistribution()
        {
            // Setup
            var rng = MockRepository.GenerateStub<IRandomNumberGenerator>();
            var dice = new[]
            {
                new NumericalDie(3, rng),
            };

            var sumOf2D6 = new SumOfDice(dice, rng);

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

            var sumOf2D6 = new SumOfDice(dice, rng);

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

        [Test]
        public void Roll_ReturnRamdomlyGeneratedValueAccordingToProbabilityDistribution()
        {
            // Setup
            var rngExpectedValueAndProbability = new[]
            {
                Tuple.Create(2, 0.0),
                Tuple.Create(2, 0.68/36.0),
                Tuple.Create(2, 1.0/36.0),
                Tuple.Create(3, (1.0+1e-6)/36.0),
                Tuple.Create(3, 1.234/36.0),
                Tuple.Create(3, 3.0/36.0),
                Tuple.Create(4, (3.0+1e-6)/36.0),
                Tuple.Create(4, 4.768/36.0),
                Tuple.Create(4, 6.0/36.0),
                Tuple.Create(5, (6.0+1e-6)/36.0),
                Tuple.Create(5, 8.96/36.0),
                Tuple.Create(5, 10.0/36.0),
                Tuple.Create(6, (10.0+1e-6)/36.0),
                Tuple.Create(6, 12.5678/36.0),
                Tuple.Create(6, 15.0/36.0),
                Tuple.Create(7, (15.0+1e-6)/36.0),
                Tuple.Create(7, 19.856/36.0),
                Tuple.Create(7, 21.0/36.0),
                Tuple.Create(8, (21.0+1e-6)/36.0),
                Tuple.Create(8, 23.68/36.0),
                Tuple.Create(8, 26.0/36.0),
                Tuple.Create(9, (26.0+1e-6)/36.0),
                Tuple.Create(9, 29.56/36.0),
                Tuple.Create(9, 30.0/36.0),
                Tuple.Create(10, (30.0+1e-6)/36.0),
                Tuple.Create(10, 31.34/36.0),
                Tuple.Create(10, 33.0/36.0),
                Tuple.Create(11, (33.0+1e-6)/36.0),
                Tuple.Create(11, 34.76/36.0),
                Tuple.Create(11, 35.0/36.0),
                Tuple.Create(12, (35.0+1e-6)/36.0),
                Tuple.Create(12, 35.956/36.0),
                Tuple.Create(12, 1.0)
            };
            var rng = new TestingRandomNumberGenerator();
            rng.AddFactorValues(rngExpectedValueAndProbability.Select(t => t.Item2));

            var rngStub = MockRepository.GenerateStub<IRandomNumberGenerator>();

            var dice = new[]
            {
                new NumericalDie(6, rngStub),
                new NumericalDie(6, rngStub)
            };
            var sumOf2D6 = new SumOfDice(dice, rng);

            foreach(Tuple<int, double> pair in rngExpectedValueAndProbability)
            {
                // Call
                int dieResult = sumOf2D6.Roll();

                // Assert
                Assert.AreEqual(pair.Item1, dieResult,
                    $"Asserting for probability value {pair.Item2}.");
            }
        }
    }
}
