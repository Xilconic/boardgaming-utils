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

public class ThresholdCompareTests
{
    private readonly IRandomNumberGenerator _rngStub = Mock.Of<IRandomNumberGenerator>();

    [Theory]
    [InlineData(-234, ComparisonType.Greater)]
    [InlineData(5674, ComparisonType.GreaterOrEqual)]
    [InlineData(0, ComparisonType.Smaller)]
    [InlineData(12, ComparisonType.SmallerOrEqual)]
    public void WhenConstructingThenReturnObjectInExpectedState(
        int referenceValue,
        ComparisonType comparisonType)
    {
        var dieDistribution = new DiscreteValueProbabilityDistribution(
            [new ValueProbabilityPair(1, Probability.One)]
        );
        var dieMock = new Mock<IAbstractDie>();
        dieMock
            .Setup(d => d.ProbabilityDistribution)
            .Returns(dieDistribution);

        var thresholdCompare = new ThresholdCompare(dieMock.Object, comparisonType, referenceValue, _rngStub);

        thresholdCompare.Should().BeAssignableTo<IRollable<bool>>();
        thresholdCompare.Should().BeAssignableTo<IDiscreteBooleanRandomVariable>();

        thresholdCompare.ReferenceValue.Should().Be(referenceValue);
        thresholdCompare.ComparisonType.Should().Be(comparisonType);
    }

    [Theory]
    [InlineData(-1234, 1.0)]
    [InlineData(0, 1.0)]
    [InlineData(1, 0.75)]
    [InlineData(3, 0.75)]
    [InlineData(4, 0.25)]
    [InlineData(7, 0.25)]
    [InlineData(8, 0.0)]
    [InlineData(9, 0.0)]
    [InlineData(345897, 0.0)]
    public void GivenSomeDieForGreaterThanComparisonWhenGettingProbabilityDistributionThenReturnExpectedResult(
        int referenceValue,
        double expectedSuccessProbability)
    {
        var dieDistribution = new DiscreteValueProbabilityDistribution([
            new ValueProbabilityPair(1, new Probability(0.25)),
            new ValueProbabilityPair(4, new Probability(0.5)),
            new ValueProbabilityPair(8, new Probability(0.25))
        ]);
        var dieMock = new Mock<IAbstractDie>();
        dieMock
            .Setup(d => d.ProbabilityDistribution)
            .Returns(dieDistribution);

        var thresholdCompare = new ThresholdCompare(dieMock.Object, ComparisonType.Greater, referenceValue, _rngStub);

        BooleanProbabilityDistribution distribution = thresholdCompare.ProbabilityDistribution;

        distribution.SuccessProbability.Should().Be(new Probability(expectedSuccessProbability));
    }

    [Theory]
    [InlineData(-1234, 1.0)]
    [InlineData(0, 1.0)]
    [InlineData(1, 1.0)]
    [InlineData(3, 0.75)]
    [InlineData(4, 0.75)]
    [InlineData(7, 0.25)]
    [InlineData(8, 0.25)]
    [InlineData(9, 0.0)]
    [InlineData(345897, 0.0)]
    public void GivenSomeDieForGreaterThanOrEqualComparisonWhenGettingProbabilityDistributionThenReturnExpectedResult(
        int referenceValue, double expectedSuccessProbability)
    {
        var dieDistribution = new DiscreteValueProbabilityDistribution([
            new ValueProbabilityPair(1, new Probability(0.25)),
            new ValueProbabilityPair(4, new Probability(0.5)),
            new ValueProbabilityPair(8, new Probability(0.25))
        ]);
        var dieMock = new Mock<IAbstractDie>();
        dieMock
            .Setup(d => d.ProbabilityDistribution)
            .Returns(dieDistribution);

        var thresholdCompare = new ThresholdCompare(dieMock.Object, ComparisonType.GreaterOrEqual, referenceValue, _rngStub);

        BooleanProbabilityDistribution distribution = thresholdCompare.ProbabilityDistribution;

        distribution.SuccessProbability.Should().Be(new Probability(expectedSuccessProbability));
    }

    [Theory]
    [InlineData(-1234, 0.0)]
    [InlineData(0, 0.0)]
    [InlineData(1, 0.0)]
    [InlineData(3, 0.25)]
    [InlineData(4, 0.25)]
    [InlineData(7, 0.75)]
    [InlineData(8, 0.75)]
    [InlineData(9, 1.0)]
    [InlineData(345897, 1.0)]
    public void GivenSomeDieForSmallerThanComparisonWhenGettingProbabilityDistributionThenReturnExpectedResult(
        int referenceValue,
        double expectedSuccessProbability)
    {
        var dieDistribution = new DiscreteValueProbabilityDistribution([
            new ValueProbabilityPair(1, new Probability(0.25)),
            new ValueProbabilityPair(4, new Probability(0.5)),
            new ValueProbabilityPair(8, new Probability(0.25))
        ]);
        var dieMock = new Mock<IAbstractDie>();
        dieMock
            .Setup(d => d.ProbabilityDistribution)
            .Returns(dieDistribution);

        var thresholdCompare = new ThresholdCompare(dieMock.Object, ComparisonType.Smaller, referenceValue, _rngStub);

        BooleanProbabilityDistribution distribution = thresholdCompare.ProbabilityDistribution;

        distribution.SuccessProbability.Should().Be(new Probability(expectedSuccessProbability));
    }

    [Theory]
    [InlineData(-1234, 0.0)]
    [InlineData(0, 0.0)]
    [InlineData(1, 0.25)]
    [InlineData(3, 0.25)]
    [InlineData(4, 0.75)]
    [InlineData(7, 0.75)]
    [InlineData(8, 1.0)]
    [InlineData(9, 1.0)]
    [InlineData(345897, 1.0)]
    public void GivenSomeDieForSmallerThanOrEqualComparisonWhenGettingProbabilityDistributionThenReturnExpectedResult(
        int referenceValue, double expectedSuccessProbability)
    {
        var spec = new[]
        {
            new ValueProbabilityPair(1, new Probability(0.25)),
            new ValueProbabilityPair(4, new Probability(0.5)),
            new ValueProbabilityPair(8, new Probability(0.25)),
        };
        var dieDistribution = new DiscreteValueProbabilityDistribution(spec);
        var dieMock = new Mock<IAbstractDie>();
        dieMock
            .Setup(d => d.ProbabilityDistribution)
            .Returns(dieDistribution);

        var thresholdCompare = new ThresholdCompare(dieMock.Object, ComparisonType.SmallerOrEqual, referenceValue, _rngStub);

        BooleanProbabilityDistribution distribution = thresholdCompare.ProbabilityDistribution;

        distribution.SuccessProbability.Should().Be(new Probability(expectedSuccessProbability));
    }

    [Theory]
    [InlineData(ComparisonType.Greater)]
    [InlineData(ComparisonType.GreaterOrEqual)]
    [InlineData(ComparisonType.Smaller)]
    [InlineData(ComparisonType.SmallerOrEqual)]
    public void GivenSomeDieWhenRollingThenExpectedValueIsReturnedBasedOnRandomlyGeneratedFactor(
        ComparisonType comparisonType)
    {
        var spec = new[]
        {
            new ValueProbabilityPair(1, new Probability(0.25)),
            new ValueProbabilityPair(4, new Probability(0.5)),
            new ValueProbabilityPair(8, new Probability(0.25)),
        };
        var dieDistribution = new DiscreteValueProbabilityDistribution(spec);
        var dieMock = new Mock<IAbstractDie>();
        dieMock
            .Setup(d => d.ProbabilityDistribution)
            .Returns(dieDistribution);

        var rng = new TestingRandomNumberGenerator();

        const int referenceValue = 4;
        var thresholdCompare = new ThresholdCompare(dieMock.Object, comparisonType, referenceValue, rng);

        double successFactor = thresholdCompare.ProbabilityDistribution.SuccessProbability.AsFactor();
        var expectedValueAndProbabilities = new (bool IsSuccess, double Factor)[]
        {
            (true, 0.0),
            (true, successFactor - 0.1),
            (true, Factor: successFactor),
            (false, successFactor + 1e-6),
            (false, successFactor + 0.1),
            (false, 1.0 - 1e-6),
        };
        rng.AddFactorValues(expectedValueAndProbabilities.Select(t => t.Factor).ToArray());

        foreach ((bool isSuccess, _) in expectedValueAndProbabilities)
        {
            bool rolledResult = thresholdCompare.Roll();

            rolledResult.Should().Be(isSuccess);
        }
    }
}