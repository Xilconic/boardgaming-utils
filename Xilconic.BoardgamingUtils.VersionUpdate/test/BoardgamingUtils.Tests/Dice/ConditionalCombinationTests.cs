using FluentAssertions;
using Moq;
using Xilconic.BoardgamingUtils.Dice;
using Xilconic.BoardgamingUtils.Mathematics;

namespace Xilconic.BoardgamingUtils.Tests.Dice;

public class ConditionalCombinationTests
{
    [Fact]
    public void WhenConstructingThenInstanceHasExpectedState()
    {
        var randomVariableMock = new Mock<IDiscreteBooleanRandomVariable>();
        randomVariableMock
            .Setup(rv => rv.ProbabilityDistribution)
            .Returns(new BooleanProbabilityDistribution(Probability.One));
        
        var trueCaseMock = new Mock<IDiscreteIntegerRandomVariable>();
        trueCaseMock
            .Setup(rv => rv.ProbabilityDistribution)
            .Returns(new DiscreteValueProbabilityDistribution([new ValueProbabilityPair(1, Probability.One)]));
        
        var falseCaseMock = new Mock<IDiscreteIntegerRandomVariable>();
        falseCaseMock
            .Setup(rv => rv.ProbabilityDistribution)
            .Returns(new DiscreteValueProbabilityDistribution([new ValueProbabilityPair(2, Probability.One)]));

        var conditional = new ConditionalCombination(randomVariableMock.Object, trueCaseMock.Object, falseCaseMock.Object);

        conditional.Should().BeAssignableTo<IDiscreteIntegerRandomVariable>();
    }

    [Fact]
    public void GivenValidArgumentsWhenGettingProbabilityDistributionThenReturnExpectedResult()
    {
        var randomVariableMock = new Mock<IDiscreteBooleanRandomVariable>();
        randomVariableMock
            .Setup(rv => rv.ProbabilityDistribution)
            .Returns(new BooleanProbabilityDistribution(new Probability(0.25)));
        
        var trueCaseMock = new Mock<IDiscreteIntegerRandomVariable>();
        trueCaseMock
            .Setup(rv => rv.ProbabilityDistribution)
            .Returns(new DiscreteValueProbabilityDistribution([
                new ValueProbabilityPair(1, new Probability(0.25)),
                new ValueProbabilityPair(2, new Probability(0.75))
            ]));
        
        var falseCaseMock = new Mock<IDiscreteIntegerRandomVariable>();
        falseCaseMock
            .Setup(rv => rv.ProbabilityDistribution)
            .Returns(new DiscreteValueProbabilityDistribution([
                new ValueProbabilityPair(2, new Probability(0.5)),
                new ValueProbabilityPair(4, new Probability(0.5))
            ]));

        var conditional = new ConditionalCombination(randomVariableMock.Object, trueCaseMock.Object, falseCaseMock.Object);

        DiscreteValueProbabilityDistribution distribution = conditional.ProbabilityDistribution;

        distribution.Specification.Should().HaveCount(3);
        var probabilitiesAndValues = new []
        {
            new ValueProbabilityPair(1, new Probability(0.25 * 0.25)),
            new ValueProbabilityPair(2, new Probability((0.25 * 0.75) + (0.75 * 0.5))),
            new ValueProbabilityPair(4, new Probability(0.75 * 0.5)),
        };
        for (int i = 0; i < probabilitiesAndValues.Length; i++)
        {
            ValueProbabilityPair expectedProbabilityAndValue = probabilitiesAndValues[i];
            ValueProbabilityPair actualProbabilityAndValue = distribution.Specification[i];

            actualProbabilityAndValue.Should().Be(expectedProbabilityAndValue);
        }
    }
}