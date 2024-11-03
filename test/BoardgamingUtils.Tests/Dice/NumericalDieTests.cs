// Copyright (c) Bas des Bouvrie ("Xilconic"). All rights reserved.
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

using FluentAssertions;
using Moq;
using Xilconic.BoardgamingUtils.Dice;
using Xilconic.BoardgamingUtils.Mathematics;
using Xilconic.BoardgamingUtils.PseudoRandom;
using Xilconic.BoardgamingUtils.Tests.TestDoubles;

namespace Xilconic.BoardgamingUtils.Tests.Dice;

public class NumericalDieTests
{
    [Theory]
    [InlineData(int.MinValue)]
    [InlineData(0)]
    public void GivenInvalidNumberOfSidesWhenConstructingThenThrowArgumentOutOfRangeException(
        int invalidNumberOfSides)
    {
        var rng = Mock.Of<IRandomNumberGenerator>();

        Func<NumericalDie> call = () => new NumericalDie(invalidNumberOfSides, rng);

        var expectedMessage =
            $"""
             Must be at least 1. (Parameter 'numberOfSides')
             Actual value was {invalidNumberOfSides}.
             """;
        call.Should().Throw<ArgumentOutOfRangeException>()
            .WithParameterName("numberOfSides")
            .WithMessage(expectedMessage);
    }
    
    [Theory]
    [InlineData(1)]
    [InlineData(120)]
    public void WhenConstructingThenDieHasExpectedState(
        int numberOfSides)
    {
        var rng = Mock.Of<IRandomNumberGenerator>();

        var die = new NumericalDie(numberOfSides, rng);

        die.Should().BeAssignableTo<AbstractDie>();
        die.NumberOfSides.Should().Be(numberOfSides);
    }

    [Theory]
    [InlineData(1, 1)]
    [InlineData(6, 1.0/6.0)]
    [InlineData(120, 1.0/120.0)]
    public void WhenGettingProbabilityDistributionThenTheDieHasUniformDiscreteDistributionForAllSides(
        int numberOfSides,
        double expectedProbabilityValue)
    {
        var rng = Mock.Of<IRandomNumberGenerator>();

        var die = new NumericalDie(numberOfSides, rng);

        DiscreteValueProbabilityDistribution distribution = die.ProbabilityDistribution;

        var expectedProbability = new Probability(expectedProbabilityValue);
        var expectedValue = 1;
        foreach (ValueProbabilityPair pair in distribution.Specification)
        {
            pair.Probability.Equals(expectedProbability, 1e-6).Should().BeTrue(
                "Because the probability is expected to be {0}, with actual value being {1}.", 
                expectedProbability, pair.Probability
            );
            pair.Value.Should().Be(expectedValue);

            expectedValue++;
        }
    }

    [Fact]
    public void WhenRollingThenReturnRandomlyGeneratedValuesAccordingToProbabilityDistribution()
    {
        const double uniformProbability = 1.0 / 6.0;
        var rngValueAndExpectedResults = new (double RngValue, int ExpectedRolledNumber)[]
        {
            (0.0, 1),
            (0.45 * uniformProbability, 1),
            (uniformProbability, 1),
            (uniformProbability + 1e-6, 2),
            (1.34 * uniformProbability, 2),
            (2 * uniformProbability, 2),
            ((2 * uniformProbability) + 1e-6, 3),
            (2.89 * uniformProbability, 3),
            (3 * uniformProbability, 3),
            ((3 * uniformProbability) + 1e-6, 4),
            (3.75 * uniformProbability, 4),
            (4 * uniformProbability, 4),
            ((4 * uniformProbability) + 1e-6, 5),
            (4.28 * uniformProbability, 5),
            (5 * uniformProbability, 5),
            ((5 * uniformProbability) + 1e-6, 6),
            (5.83 * uniformProbability, 6),
            (6 * uniformProbability - 1e-6, 6),
        };
        var rng = new TestingRandomNumberGenerator();
        rng.AddFactorValues(rngValueAndExpectedResults.Select(t => t.RngValue).ToArray());

        var die = new NumericalDie(6, rng);

        foreach ((_, int expectedRolledNumber) in rngValueAndExpectedResults)
        {
            int dieResult = die.Roll();

            dieResult.Should().Be(expectedRolledNumber);
        }
    }
}