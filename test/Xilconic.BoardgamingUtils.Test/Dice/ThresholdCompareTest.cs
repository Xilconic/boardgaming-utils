// This file is part of Boardgaming Utils.
//
// Boardgaming Utilsis free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// Boardgaming Utilsis distributed in the hope that it will be useful,
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
    public class ThresholdCompareTest
    {
        [TestCase(-234, ComparisonType.Greater)]
        [TestCase(5674, ComparisonType.GreaterOrEqual)]
        [TestCase(0, ComparisonType.Smaller)]
        [TestCase(12, ComparisonType.SmallerOrEqual)]
        public void Constructor_ExpectedValues(int referenceValue, ComparisonType comparisonType)
        {
            // Setup
            var rng = MockRepository.GenerateStub<IRandomNumberGenerator>();
            var die = MockRepository.GenerateStub<IAbstractDie>();
            die.Stub(d => d.ProbabilityDistribution).Return(new DiscreteValueProbabilityDistribution(new[] { new ValueProbabilityPair(1, 1.0) }));

            // Call
            var thresholdCompare = new ThresholdCompare(die, comparisonType, referenceValue, rng);

            // Assert
            Assert.IsInstanceOf<IRollable<bool>>(thresholdCompare);
            Assert.IsInstanceOf<IDiscreteBooleanRandomVariable>(thresholdCompare);

            Assert.AreEqual(referenceValue, thresholdCompare.ReferenceValue);
            Assert.AreEqual(comparisonType, thresholdCompare.ComparisonType);
        }

        [TestCase(-1234, 1.0)]
        [TestCase(0, 1.0)]
        [TestCase(1, 0.75)]
        [TestCase(3, 0.75)]
        [TestCase(4, 0.25)]
        [TestCase(7, 0.25)]
        [TestCase(8, 0.0)]
        [TestCase(9, 0.0)]
        [TestCase(345897, 0.0)]
        public void ProbabilityDistribution_DieGreaterThanReferenceValue_ReturnExpectedResult(
            int referenceValue, double expectedSuccessProbability)
        {
            // Setup
            var rng = MockRepository.GenerateStub<IRandomNumberGenerator>();

            var spec = new[] 
            {
                new ValueProbabilityPair(1, 0.25),
                new ValueProbabilityPair(4, 0.5),
                new ValueProbabilityPair(8, 0.25),
            };
            var die = MockRepository.GenerateStub<IAbstractDie>();
            die.Stub(d => d.ProbabilityDistribution).Return(new DiscreteValueProbabilityDistribution(spec));

            var thresholdCompare = new ThresholdCompare(die, ComparisonType.Greater, referenceValue, rng);

            // Call
            BooleanProbabilityDistribution distribution = thresholdCompare.ProbabilityDistribution;

            // Assert
            Assert.AreEqual(expectedSuccessProbability, distribution.SuccessProbability);
        }

        [TestCase(-1234, 1.0)]
        [TestCase(0, 1.0)]
        [TestCase(1, 1.0)]
        [TestCase(3, 0.75)]
        [TestCase(4, 0.75)]
        [TestCase(7, 0.25)]
        [TestCase(8, 0.25)]
        [TestCase(9, 0.0)]
        [TestCase(345897, 0.0)]
        public void ProbabilityDistribution_DieGreaterThanOrEqualToReferenceValue_ReturnExpectedResult(
            int referenceValue, double expectedSuccessProbability)
        {
            // Setup
            var rng = MockRepository.GenerateStub<IRandomNumberGenerator>();

            var spec = new[]
            {
                new ValueProbabilityPair(1, 0.25),
                new ValueProbabilityPair(4, 0.5),
                new ValueProbabilityPair(8, 0.25),
            };
            var die = MockRepository.GenerateStub<IAbstractDie>();
            die.Stub(d => d.ProbabilityDistribution).Return(new DiscreteValueProbabilityDistribution(spec));

            var thresholdCompare = new ThresholdCompare(die, ComparisonType.GreaterOrEqual, referenceValue, rng);

            // Call
            BooleanProbabilityDistribution distribution = thresholdCompare.ProbabilityDistribution;

            // Assert
            Assert.AreEqual(expectedSuccessProbability, distribution.SuccessProbability);
        }

        [TestCase(-1234, 0.0)]
        [TestCase(0, 0.0)]
        [TestCase(1, 0.0)]
        [TestCase(3, 0.25)]
        [TestCase(4, 0.25)]
        [TestCase(7, 0.75)]
        [TestCase(8, 0.75)]
        [TestCase(9, 1.0)]
        [TestCase(345897, 1.0)]
        public void ProbabilityDistribution_DieSmallerThanReferenceValue_ReturnExpectedResult(
            int referenceValue, double expectedSuccessProbability)
        {
            // Setup
            var rng = MockRepository.GenerateStub<IRandomNumberGenerator>();

            var spec = new[]
            {
                new ValueProbabilityPair(1, 0.25),
                new ValueProbabilityPair(4, 0.5),
                new ValueProbabilityPair(8, 0.25),
            };
            var die = MockRepository.GenerateStub<IAbstractDie>();
            die.Stub(d => d.ProbabilityDistribution).Return(new DiscreteValueProbabilityDistribution(spec));

            var thresholdCompare = new ThresholdCompare(die, ComparisonType.Smaller, referenceValue, rng);

            // Call
            BooleanProbabilityDistribution distribution = thresholdCompare.ProbabilityDistribution;

            // Assert
            Assert.AreEqual(expectedSuccessProbability, distribution.SuccessProbability);
        }

        [TestCase(-1234, 0.0)]
        [TestCase(0, 0.0)]
        [TestCase(1, 0.25)]
        [TestCase(3, 0.25)]
        [TestCase(4, 0.75)]
        [TestCase(7, 0.75)]
        [TestCase(8, 1.0)]
        [TestCase(9, 1.0)]
        [TestCase(345897, 1.0)]
        public void ProbabilityDistribution_DieSmallerThanOrEqualToReferenceValue_ReturnExpectedResult(
            int referenceValue, double expectedSuccessProbability)
        {
            // Setup
            var rng = MockRepository.GenerateStub<IRandomNumberGenerator>();

            var spec = new[]
            {
                new ValueProbabilityPair(1, 0.25),
                new ValueProbabilityPair(4, 0.5),
                new ValueProbabilityPair(8, 0.25),
            };
            var die = MockRepository.GenerateStub<IAbstractDie>();
            die.Stub(d => d.ProbabilityDistribution).Return(new DiscreteValueProbabilityDistribution(spec));

            var thresholdCompare = new ThresholdCompare(die, ComparisonType.SmallerOrEqual, referenceValue, rng);

            // Call
            BooleanProbabilityDistribution distribution = thresholdCompare.ProbabilityDistribution;

            // Assert
            Assert.AreEqual(expectedSuccessProbability, distribution.SuccessProbability);
        }

        [TestCase(ComparisonType.Greater)]
        [TestCase(ComparisonType.GreaterOrEqual)]
        [TestCase(ComparisonType.Smaller)]
        [TestCase(ComparisonType.SmallerOrEqual)]
        public void Roll_BasedOnProbabilityDistribution_ReturnExpectedResult(ComparisonType comparisonType)
        {
            // Setup
            var spec = new[]
            {
                new ValueProbabilityPair(1, 0.25),
                new ValueProbabilityPair(4, 0.5),
                new ValueProbabilityPair(8, 0.25),
            };
            var die = MockRepository.GenerateStub<IAbstractDie>();
            die.Stub(d => d.ProbabilityDistribution).Return(new DiscreteValueProbabilityDistribution(spec));

            var rng = new TestingRandomNumberGenerator();

            const int referenceValue = 4;
            var thresholdCompare = new ThresholdCompare(die, comparisonType, referenceValue, rng);

            double successProbability = thresholdCompare.ProbabilityDistribution.SuccessProbability;
            var expectedValueAndProbabilities = new[]
            {
                Tuple.Create(true, 0.0),
                Tuple.Create(true, successProbability-0.1),
                Tuple.Create(true, successProbability),
                Tuple.Create(false, successProbability+1e-6),
                Tuple.Create(false, successProbability+0.1),
                Tuple.Create(false, 1.0),
            };
            rng.AddFactorValues(expectedValueAndProbabilities.Select(t => t.Item2));

            foreach (Tuple<bool, double> expectedValueAndProbability in expectedValueAndProbabilities)
            {
                // Call
                bool rolledResult = thresholdCompare.Roll();

                // Assert
                Assert.AreEqual(rolledResult, expectedValueAndProbability.Item1);
            }
        }
    }
}
