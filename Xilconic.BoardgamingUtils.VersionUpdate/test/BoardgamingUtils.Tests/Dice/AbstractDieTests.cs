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
using Xilconic.BoardgamingUtils.Dice;
using Xilconic.BoardgamingUtils.Mathematics;
using Xilconic.BoardgamingUtils.PseudoRandom;
using Xilconic.BoardgamingUtils.Tests.TestDoubles;

namespace Xilconic.BoardgamingUtils.Tests.Dice;

public class AbstractDieTests
{
    [Theory]
    [InlineData(0.0, 0)]
    [InlineData(0.324, 0)]
    [InlineData(0.5, 0)]
    [InlineData(0.5 + 1e-6, 1)]
    [InlineData(0.75, 1)]
    [InlineData(1.0 - 1e-6, 1)]
    public void GivenRandomnessFactorWhenRollingReturnsExpectedValueFromDistribution(
        double rngValue,
        int expectedRollValue)
    {
        var specification = new[]
        {
            new ValueProbabilityPair(0, new Probability(0.5)),
            new ValueProbabilityPair(1, new Probability(0.5)),
        };
        var distribution = new DiscreteValueProbabilityDistribution(specification);

        var rng = new TestingRandomNumberGenerator();
        rng.AddFactorValues(rngValue);

        AbstractDie die = new TestAbstractDie(rng, distribution);

        int value = die.Roll();

        value.Should().Be(expectedRollValue);
    }

    private sealed class TestAbstractDie(IRandomNumberGenerator rng, DiscreteValueProbabilityDistribution distribution)
        : AbstractDie(rng)
    {
        public override DiscreteValueProbabilityDistribution ProbabilityDistribution
        {
            get;
        } = distribution;
    }
}