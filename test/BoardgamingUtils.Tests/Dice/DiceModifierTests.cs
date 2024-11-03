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

namespace Xilconic.BoardgamingUtils.Tests.Dice;

public class DiceModifierTests
{
    private readonly IRandomNumberGenerator _rngStub = Mock.Of<IRandomNumberGenerator>();

    [Theory]
    [InlineData(-506409)]
    [InlineData(54987)]
    public void WhenConstructingThenCreatedObjectHasExpectedState(int modifierValue)
    {
        var dieMock = new Mock<IAbstractDie>();
        dieMock
            .Setup(d => d.ProbabilityDistribution)
            .Returns(new DiscreteValueProbabilityDistribution([new ValueProbabilityPair(0, new Probability(1.0))]));

        var modifier = new DiceModifier(dieMock.Object, modifierValue, _rngStub);

        modifier.Should().BeAssignableTo<AbstractDie>();
        modifier.Modifier.Should().Be(modifierValue);
    }

    [Theory]
    [InlineData(-123)]
    [InlineData(456)]
    public void GivenSomeNumericalDieWhenGettingProbabilityDistributionThenReturnsExpectedDistribution(
        int modifierValue)
    {
        var die = new NumericalDie(6, _rngStub);

        var modifier = new DiceModifier(die, modifierValue, _rngStub);

        DiscreteValueProbabilityDistribution distribution = modifier.ProbabilityDistribution;

        IReadOnlyList<ValueProbabilityPair> specification = die.ProbabilityDistribution.Specification;
        distribution.Specification.Should().HaveCount(specification.Count, "Because a die modifier does not change the number of possible values that can be generated.");
        for (int i = 0; i < specification.Count; i++)
        {
            ValueProbabilityPair referencePair = specification[i];
            ValueProbabilityPair actualPair = distribution.Specification[i];
            actualPair.Should().Be(new ValueProbabilityPair(referencePair.Value + modifierValue, referencePair.Probability));
        }
    }
}