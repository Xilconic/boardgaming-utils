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
using Xilconic.BoardgamingUtils.Mathematics;

namespace Xilconic.BoardgamingUtils.Tests.Mathematics;

public class DiscreteValueProbabilityDistributionTests
{
    [Fact]
    public void GivenEmptyDistributionSpecifiedWhenConstructingThenThrowArgumentException()
    {
        var valueProbabilityPairs = Array.Empty<ValueProbabilityPair>();
         
        Func<DiscreteValueProbabilityDistribution> call = () => new DiscreteValueProbabilityDistribution(valueProbabilityPairs);

        call.Should().Throw<ArgumentException>()
            .WithParameterName("probabilitySpecification")
            .WithMessage("The collection of probability-value pairs must contain at least 1 element. (Parameter 'probabilitySpecification')");
    }

    [Fact]
    public void GivenIncompleteDistributionSpecifiedWhenConstructingThenThrowArgumentException()
    {
        var valueProbabilityPairs = new[]
        {
            new ValueProbabilityPair(1, new Probability(0.5))
        };
         
        Func<DiscreteValueProbabilityDistribution> call = () => new DiscreteValueProbabilityDistribution(valueProbabilityPairs);

        call.Should().Throw<ArgumentException>()
            .WithParameterName("probabilitySpecification")
            .WithMessage("The sum of all probabilities should be 1. (Parameter 'probabilitySpecification')");
    }
    
    [Fact]
    public void GivenOverspecifiedDistributionWhenConstructingThenThrowArgumentException()
    {
        var valueProbabilityPairs = new[]
        {
            new ValueProbabilityPair(1, new Probability(0.5)),
            new ValueProbabilityPair(2, new Probability(0.5)),
            new ValueProbabilityPair(3, new Probability(0.5))
        };
         
        Func<DiscreteValueProbabilityDistribution> call = () => new DiscreteValueProbabilityDistribution(valueProbabilityPairs);

        call.Should().Throw<ArgumentException>()
            .WithParameterName("probabilitySpecification")
            .WithMessage("The sum of all probabilities should be 1. (Parameter 'probabilitySpecification')");
    }

    [Fact]
    public void GivenDuplicateValuesInDistributionWhenConstructingThenThrowArgumentException()
    {
        var valueProbabilityPairs = new[]
        {
            new ValueProbabilityPair(1, new Probability(0.5)),
            new ValueProbabilityPair(1, new Probability(0.5))
        };
         
        Func<DiscreteValueProbabilityDistribution> call = () => new DiscreteValueProbabilityDistribution(valueProbabilityPairs);

        call.Should().Throw<ArgumentException>()
            .WithParameterName("probabilitySpecification")
            .WithMessage("All values in the specification should be unique. (Parameter 'probabilitySpecification')");
    }
    
    [Fact]
    public void GivenValidDistributionSpecifiedWhenConstructingThenSpecificationReturnsExpectedData()
    {
        var valueProbabilityPairs = new []
        {
            new ValueProbabilityPair(1, new Probability(0.5)),
            new ValueProbabilityPair(2, new Probability(0.25)),
            new ValueProbabilityPair(3, new Probability(0.10)),
            new ValueProbabilityPair(4, new Probability(0.15)),
        };

        var distribution = new DiscreteValueProbabilityDistribution(valueProbabilityPairs);

        IReadOnlyCollection<ValueProbabilityPair> specification = distribution.Specification;
        distribution.Specification.Should().HaveCount(valueProbabilityPairs.Length);
        
        foreach (ValueProbabilityPair expectedPair in valueProbabilityPairs)
        {
            specification.Should().ContainSingle(pair =>
                pair.Value.Equals(expectedPair.Value) &&
                pair.Probability.Equals(expectedPair.Probability));
        }
    }

    [Theory]
    [InlineData(0.0, 1)]
    [InlineData(0.123, 1)]
    [InlineData(0.5, 1)]
    [InlineData(0.5 + 1e-6, 2)]
    [InlineData(0.67, 2)]
    [InlineData(0.75, 2)]
    [InlineData(0.75 + 1e-6, 3)]
    [InlineData(0.7999, 3)]
    [InlineData(0.85, 3)]
    [InlineData(0.85 + 1e-6, 4)]
    [InlineData(0.953, 4)]
    [InlineData(1.0, 4)]
    public void GivenSomeProbabilityDistributionWhenGetValueAtCdfForWholeSpectrumReturnExpectedValuesAccordingToDistribution(
        double probability, int expectedValue)
    {
        var valueProbabilityPairs = new[]
        {
            new ValueProbabilityPair(1, new Probability(.5)),
            new ValueProbabilityPair(2, new Probability(.25)),
            new ValueProbabilityPair(3, new Probability(.10)),
            new ValueProbabilityPair(4, new Probability(.15 - 1e-8)),
        };
        var distribution = new DiscreteValueProbabilityDistribution(valueProbabilityPairs);

        int sampleValue = distribution.GetValueAtCdf(new Probability(probability));

        sampleValue.Should().Be(expectedValue);
    }
}