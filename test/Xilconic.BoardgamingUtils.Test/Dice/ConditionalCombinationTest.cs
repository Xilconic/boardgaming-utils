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
using Xilconic.BoardgamingUtils.Dice;
using Xilconic.BoardgamingUtils.Mathmatics;

namespace Xilconic.BoardgamingUtils.Test.Dice
{
    [TestFixture]
    public class ConditionalCombinationTest
    {
        [Test]
        public void Constructor_ValidArguments_ExpectedValues()
        {
            // Setup
            var randomVariable = MockRepository.GenerateStub<IDiscreteBooleanRandomVariable>();
            randomVariable.Stub(rv => rv.ProbabilityDistribution).Return(new BooleanProbabilityDistribution(1.0));
            var trueCase = MockRepository.GenerateStub<IDiscreteIntegerRandomVariable>();
            trueCase.Stub(rv => rv.ProbabilityDistribution).Return(new DiscreteValueProbabilityDistribution(new[] { new ValueProbabilityPair(1, 1.0) }));
            var falseCase = MockRepository.GenerateStub<IDiscreteIntegerRandomVariable>();
            falseCase.Stub(rv => rv.ProbabilityDistribution).Return(new DiscreteValueProbabilityDistribution(new[] { new ValueProbabilityPair(2, 1.0) }));

            // Call
            var conditional = new ConditionalCombination(randomVariable, trueCase, falseCase);

            // Assert
            Assert.IsInstanceOf<IDiscreteIntegerRandomVariable>(conditional);
        }

        [Test]
        public void ProbabilityDistribution_ValidArguments_ReturnExpectedResult()
        {
            // Setup
            var randomVariable = MockRepository.GenerateStub<IDiscreteBooleanRandomVariable>();
            randomVariable.Stub(rv => rv.ProbabilityDistribution)
                .Return(new BooleanProbabilityDistribution(0.25));
            var trueCase = MockRepository.GenerateStub<IDiscreteIntegerRandomVariable>();
            trueCase.Stub(rv => rv.ProbabilityDistribution)
                .Return(new DiscreteValueProbabilityDistribution(new[]
                {
                    new ValueProbabilityPair(1, 0.25),
                    new ValueProbabilityPair(2, 0.75),
                }));
            var falseCase = MockRepository.GenerateStub<IDiscreteIntegerRandomVariable>();
            falseCase.Stub(rv => rv.ProbabilityDistribution)
                .Return(new DiscreteValueProbabilityDistribution(new[]
                {
                    new ValueProbabilityPair(2, 0.5),
                    new ValueProbabilityPair(4, 0.5)
                }));

            var conditional = new ConditionalCombination(randomVariable, trueCase, falseCase);

            // Call
            DiscreteValueProbabilityDistribution distribution = conditional.ProbabilityDistribution;

            // Assert
            Assert.AreEqual(3, distribution.Specification.Count);
            var probabilitiesAndValues = new Tuple<double, int>[]
            {
                Tuple.Create(0.25*0.25, 1),
                Tuple.Create(0.25*0.75 + 0.75*0.5, 2),
                Tuple.Create(0.75*0.5, 4)
            };
            for(int i = 0; i < probabilitiesAndValues.Length; i++)
            {
                Tuple<double, int> expectedProbabilityAndValue = probabilitiesAndValues[i];
                ValueProbabilityPair actualProbabilityAndValue = distribution.Specification[i];

                Assert.AreEqual(expectedProbabilityAndValue.Item1, actualProbabilityAndValue.Probability);
                Assert.AreEqual(expectedProbabilityAndValue.Item2, actualProbabilityAndValue.Value);
            }
        }
    }
}
