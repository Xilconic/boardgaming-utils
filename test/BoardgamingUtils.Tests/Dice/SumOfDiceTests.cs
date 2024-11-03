using FluentAssertions;
using Moq;
using Xilconic.BoardgamingUtils.Dice;
using Xilconic.BoardgamingUtils.Mathematics;
using Xilconic.BoardgamingUtils.PseudoRandom;
using Xilconic.BoardgamingUtils.Tests.TestDoubles;

namespace Xilconic.BoardgamingUtils.Tests.Dice;

public class SumOfDiceTests
{
    private readonly IRandomNumberGenerator _rngStub = Mock.Of<IRandomNumberGenerator>();

    [Fact]
    public void GivenNoDiceWhenConstructingThenThrowArgumentException()
    {
        Func<SumOfDice> call = () => new SumOfDice(Array.Empty<NumericalDie>(), _rngStub);
        
        call.Should().Throw<ArgumentException>()
            .WithParameterName("dice")
            .WithMessage("The collection of dice must contain at least 1 element. (Parameter 'dice')");
    }
    
    [Fact]
    public void WhenConstructingThenSumOfDiceIsInExpectedState()
    {
        var dieMock = new Mock<IAbstractDie>();
        dieMock
            .Setup(d => d.ProbabilityDistribution)
            .Returns(new DiscreteValueProbabilityDistribution([new ValueProbabilityPair(0, Probability.One)]));

        var dice = new[] { dieMock.Object };

        var sumOfDice = new SumOfDice(dice, _rngStub);

        sumOfDice.Should().BeAssignableTo<IAbstractDie>();
    }

    [Fact]
    public void GivenSingleD3WhenGettingProbabilityDistributionThenReturnExpectedDistribution()
    {
        var rng = Mock.Of<IRandomNumberGenerator>();
        var dice = new[] { new NumericalDie(3, rng) };
        var sumOf1d3 = new SumOfDice(dice, rng);

        DiscreteValueProbabilityDistribution distribution = sumOf1d3.ProbabilityDistribution;

        var expectedUniformProbability = new Probability(1.0 / 3.0);
        var expectedProbabilityValuePairs = new ValueProbabilityPair[]
        {
            new(1, expectedUniformProbability),
            new(2, expectedUniformProbability),
            new(3, expectedUniformProbability),
        };
        distribution.Specification.Should().HaveCount(expectedProbabilityValuePairs.Length);
        for (int i = 0; i < expectedProbabilityValuePairs.Length; i++)
        {
            var expectedPair = expectedProbabilityValuePairs[i];
            ValueProbabilityPair actualPair = distribution.Specification[i];

            actualPair.Value.Should().Be(expectedPair.Value);
            actualPair.Probability.Equals(expectedPair.Probability, 1e-6).Should().BeTrue("Because expected probability of {0} should be the same as actual probability of {1}", expectedPair.Probability, actualPair.Probability);
        }
    }

    [Fact]
    public void GivenTwoD6WhenGettingProbabilityDistributionThenReturnExpectedDistribution()
    {
        var rng = Mock.Of<IRandomNumberGenerator>();
        var dice = new[]
        {
            new NumericalDie(6, rng),
            new NumericalDie(6, rng),
        };

        var sumOf2D6 = new SumOfDice(dice, rng);

        DiscreteValueProbabilityDistribution distribution = sumOf2D6.ProbabilityDistribution;

        var expectedProbabilityValuePairs = new ValueProbabilityPair[]
        {
            new(2,  new Probability(1.0 / 36.0)),
            new(3,  new Probability(2.0 / 36.0)),
            new(4,  new Probability(3.0 / 36.0)),
            new(5,  new Probability(4.0 / 36.0)),
            new(6,  new Probability(5.0 / 36.0)),
            new(7,  new Probability(6.0 / 36.0)),
            new(8,  new Probability(5.0 / 36.0)),
            new(9,  new Probability(4.0 / 36.0)),
            new(10, new Probability( 3.0 / 36.0)),
            new(11, new Probability( 2.0 / 36.0)),
            new(12, new Probability( 1.0 / 36.0)),
        };
        distribution.Specification.Should().HaveCount(expectedProbabilityValuePairs.Length);
        for (int i = 0; i < expectedProbabilityValuePairs.Length; i++)
        {
            var expectedPair = expectedProbabilityValuePairs[i];
            ValueProbabilityPair actualPair = distribution.Specification[i];

            actualPair.Value.Should().Be(expectedPair.Value);
            actualPair.Probability.Equals(expectedPair.Probability, 1e-6).Should().BeTrue("Because expected probability of {0} should be the same as actual probability of {1}", expectedPair.Probability, actualPair.Probability);
        }
    }

    [Fact]
    public void GivenTwoD6WhenRollingReturnRandomlyGeneratedValueAccordingToProbabilityDistribution()
    {
        var rngExpectedValueAndProbability = new (int ExpectedValue, double RngValue)[]
        {
            (2, 0.0),
            (2, 0.68 / 36.0),
            (2, 1.0 / 36.0),
            (3, (1.0 + 1e-6) / 36.0),
            (3, 1.234 / 36.0),
            (3, 3.0 / 36.0),
            (4, (3.0 + 1e-6) / 36.0),
            (4, 4.768 / 36.0),
            (4, 6.0 / 36.0),
            (5, (6.0 + 1e-6) / 36.0),
            (5, 8.96 / 36.0),
            (5, 10.0 / 36.0),
            (6, (10.0 + 1e-6) / 36.0),
            (6, 12.5678 / 36.0),
            (6, 15.0 / 36.0),
            (7, (15.0 + 1e-6) / 36.0),
            (7, 19.856 / 36.0),
            (7, 21.0 / 36.0),
            (8, (21.0 + 1e-6) / 36.0),
            (8, 23.68 / 36.0),
            (8, 26.0 / 36.0),
            (9, (26.0 + 1e-6) / 36.0),
            (9, 29.56 / 36.0),
            (9, 30.0 / 36.0),
            (10, (30.0 + 1e-6) / 36.0),
            (10, 31.34 / 36.0),
            (10, 33.0 / 36.0),
            (11, (33.0 + 1e-6) / 36.0),
            (11, 34.76 / 36.0),
            (11, 35.0 / 36.0),
            (12, (35.0 + 1e-6) / 36.0),
            (12, 35.956 / 36.0),
            (12, 1.0 - 1e-6),
        };
        var rng = new TestingRandomNumberGenerator();
        rng.AddFactorValues(rngExpectedValueAndProbability.Select(t => t.RngValue).ToArray());

        var rngStub = Mock.Of<IRandomNumberGenerator>();
        var dice = new[]
        {
            new NumericalDie(6, rngStub),
            new NumericalDie(6, rngStub),
        };
        
        var sumOf2D6 = new SumOfDice(dice, rng);

        foreach ((int expectedValue, _) in rngExpectedValueAndProbability)
        {
            int dieResult = sumOf2D6.Roll();

            dieResult.Should().Be(expectedValue);
        }
    }
}