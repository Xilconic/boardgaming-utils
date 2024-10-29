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

public class ProbabilityTests
{
    [Theory]
    [InlineData(double.NaN)]
    public void GivenInvalidValuesWhenConstructingThenThrowsArgumentException(
        double invalidProbabilityValue)
    {
        Func<Probability> call = () => new Probability(invalidProbabilityValue);

        call.Should().Throw<ArgumentException>()
            .WithMessage("Probability cannot be 'NaN'. (Parameter 'value')");
    }

    [Theory]
    [InlineData(double.NegativeInfinity)]
    [InlineData(double.PositiveInfinity)]
    [InlineData(-0.0000000001)]
    [InlineData(1.0000000001)]
    public void GivenValueOutsideValidDomainWhenConstructingThenThrowsArgumentOutOfRangeException(
        double invalidProbabilityValue)
    {
        Func<Probability> call = () => new Probability(invalidProbabilityValue);

        call.Should().Throw<ArgumentException>()
            .WithMessage("Probability must be in range [0.0, 1.0]. (Parameter 'value')");
    }

    [Theory]
    [InlineData(0.0)]
    [InlineData(0.5)]
    [InlineData(1.0)]
    public void GivenValidValueWhenConstructingThenDoesNotThrow(
        double validProbabilityValue)
    {
        Func<Probability> call = () => new Probability(validProbabilityValue);

        call.Should().NotThrow();
    }

    [Fact]
    public void GivenTwoEqualProbabilitiesWhenComparingEqualityReturnsTrue()
    {
        var probability1 = new Probability(0.5);
        var probability2 = new Probability(0.5);

        probability1.Equals(probability2).Should().BeTrue();
        probability2.Equals(probability1).Should().BeTrue();
        
        probability1.GetHashCode().Should().Be(
            probability2.GetHashCode(), 
            "Microsoft design guidelines for equality dictates it should return the same value when 2 instances are considered equal."
        );
    }

    [Fact]
    public void GivenTwoDifferentProbabilitiesWhenComparingEqualityReturnsFalse()
    {
        var probability1 = new Probability(0.3);
        var probability2 = new Probability(0.5);

        probability1.Equals(probability2).Should().BeFalse();
        probability2.Equals(probability1).Should().BeFalse();
    }

    [Fact]
    public void GivenTwoProbabilitiesWhenAddingReturnsSumOfTwoValues()
    {
        var probability1 = new Probability(0.4);
        var probability2 = new Probability(0.6);

        var expectedProbability = new Probability(1.0);
        (probability1 + probability2).Should().Be(expectedProbability);
        (probability2 + probability1).Should().Be(expectedProbability);
        probability1.Add(probability2).Should().Be(expectedProbability);
        probability2.Add(probability1).Should().Be(expectedProbability);
    }

    [Fact]
    public void GivenTwoProbabilitiesAddingToAboveOneThrowsArgumentException()
    {
        var probability1 = new Probability(0.6);
        var probability2 = new Probability(0.6);

        Func<Probability> call = () => probability1 + probability2;
        call.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void GivenNullWhenComparingAgainstProbabilityThenReturnPositive()
    {
        Probability.Zero.CompareTo(null).Should().BeGreaterThan(0, "Because anything is always greater than 'null'.");
        Probability.One.CompareTo(null).Should().BeGreaterThan(0, "Because anything is always greater than 'null'.");
    }

    [Fact]
    public void GivenNonProbabilityWhenComparingAgainstProbabilityThrowsArgumentException()
    {
        var probability = Probability.Zero;
        
        Func<int> call = () => probability.CompareTo(new object());

        call.Should().Throw<ArgumentException>()
            .WithParameterName("obj")
            .WithMessage("Object must be of type Probability. (Parameter 'obj')");
    }

    [Fact]
    public void GivenTwoEqualProbabilitiesWhenComparingAgainstEachOtherReturnsExpectedValues()
    {
        var probability1 = new Probability(0.5);
        var probability2 = new Probability(0.5);
        object probability2AsObject = probability2;
        
        probability1.CompareTo(probability2).Should().Be(0);
        probability1.CompareTo(probability2AsObject).Should().Be(0);
        (probability1 < probability2).Should().BeFalse();
        (probability1 <= probability2).Should().BeTrue();
        (probability1 > probability2).Should().BeFalse();
        (probability1 >= probability2).Should().BeTrue();
    }
    
    [Fact]
    public void GivenTwoDifferentProbabilitiesWhenComparingAgainstEachOtherReturnsExpectedValues()
    {
        var probability1 = new Probability(0.2);
        var probability2 = new Probability(0.5);
        object probability1AsObject = probability1;
        object probability2AsObject = probability2;
        
        probability1.CompareTo(probability2).Should().BeLessThan(0);
        probability1.CompareTo(probability2AsObject).Should().BeLessThan(0);
        
        (probability1 < probability2).Should().BeTrue();
        (probability1 <= probability2).Should().BeTrue();
        (probability1 > probability2).Should().BeFalse();
        (probability1 >= probability2).Should().BeFalse();
        
        probability2.CompareTo(probability1).Should().BeGreaterThan(0);
        probability2.CompareTo(probability1AsObject).Should().BeGreaterThan(0);
        
        (probability2 < probability1).Should().BeFalse();
        (probability2 <= probability1).Should().BeFalse();
        (probability2 > probability1).Should().BeTrue();
        (probability2 >= probability1).Should().BeTrue();
    }

    [Fact]
    public void GivenTwoCloselySimilarProbabilitiesWhenEqualityCheckingWithMarginReturnsTrue()
    {
        var probability1 = new Probability(0.5);
        var probability2 = new Probability(0.5 + 1e-7);

        var isEqual = probability1.Equals(probability2, 1e-6);
        
        isEqual.Should().BeTrue();
    }
    
    [Fact]
    public void GivenTwoSlightlyTooDifferentProbabilitiesWhenEqualityCheckingWithMarginReturnsFalse()
    {
        var probability1 = new Probability(0.5);
        var probability2 = new Probability(0.5 + 1e-5);

        var isEqual = probability1.Equals(probability2, 1e-6);
        
        isEqual.Should().BeFalse();
    }

    [Fact]
    public void WhenToStringThenReturnStringOfFactor()
    {
        var probability = new Probability(0.234);
        
        var text = probability.ToString();
        
        text.Should().Be("0.234");
    }
}