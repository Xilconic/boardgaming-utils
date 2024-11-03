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

using System.ComponentModel;
using Xilconic.BoardgamingUtils.Mathematics;
using Xilconic.BoardgamingUtils.PseudoRandom;

namespace Xilconic.BoardgamingUtils.Dice;

/// <summary>
/// Class that compares the result of an <see cref="IAbstractDie"/> to some other value.
/// </summary>
public class ThresholdCompare : IRollable<bool>, IDiscreteBooleanRandomVariable
{
    private readonly IRandomNumberGenerator _rng;

    /// <summary>
    /// Initializes a new instance of the <see cref="ThresholdCompare"/> class.
    /// </summary>
    /// <param name="die">The die whose result should be compared.</param>
    /// <param name="comparisonType">How the result of <paramref name="die"/> should be compared.</param>
    /// <param name="referenceValue">To what value <paramref name="die"/> should be compared.</param>
    /// <param name="rng">The random number generator.</param>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="comparisonType"/> is not either <see cref="Dice.ComparisonType.Greater"/>,
    /// <see cref="Dice.ComparisonType.GreaterOrEqual"/>, <see cref="Dice.ComparisonType.Smaller"/> or <see cref="Dice.ComparisonType.SmallerOrEqual"/>.
    /// </exception>
    public ThresholdCompare(IAbstractDie die, ComparisonType comparisonType, int referenceValue, IRandomNumberGenerator rng)
    {
        ArgumentNullException.ThrowIfNull(die);
        ArgumentNullException.ThrowIfNull(rng);
        if (comparisonType != ComparisonType.Greater && comparisonType != ComparisonType.GreaterOrEqual &&
            comparisonType != ComparisonType.Smaller && comparisonType != ComparisonType.SmallerOrEqual)
        {
            throw new ArgumentException("The comparison type must be a supported type.", nameof(comparisonType));
        }

        ComparisonType = comparisonType;
        ReferenceValue = referenceValue;
        _rng = rng;

        Probability successProbability = CreateProbabilityDistribution(die, comparisonType, referenceValue);
        ProbabilityDistribution = new BooleanProbabilityDistribution(successProbability);
    }

    /// <summary>
    /// Gets the value used to compare the die result with.
    /// </summary>
    public int ReferenceValue { get; }

    /// <summary>
    /// Gets the probability distribution of the die passing the comparison or not.
    /// </summary>
    public BooleanProbabilityDistribution ProbabilityDistribution { get; }

    /// <summary>
    /// Gets the type of comparison performed.
    /// </summary>
    public ComparisonType ComparisonType { get; }

    /// <inheritdoc/>
    public bool Roll()
    {
        return new Probability(_rng.NextFactor()) <= ProbabilityDistribution.SuccessProbability;
    }

    private static Func<int, bool> GetComparison(ComparisonType comparisonType, int referenceValue)
    {
        switch (comparisonType)
        {
            case ComparisonType.Greater: return i => i > referenceValue;
            case ComparisonType.GreaterOrEqual: return i => i >= referenceValue;
            case ComparisonType.Smaller: return i => i < referenceValue;
            case ComparisonType.SmallerOrEqual: return i => i <= referenceValue;
            default: throw new InvalidEnumArgumentException(nameof(comparisonType), (int)comparisonType, typeof(ComparisonType));
        }
    }

    private static Probability CreateProbabilityDistribution(IAbstractDie die, ComparisonType comparisonType, int referenceValue)
    {
        Func<int, bool> compareValueToThreshold = GetComparison(comparisonType, referenceValue);
        Probability result = die.ProbabilityDistribution.Specification
            .Where(p => compareValueToThreshold(p.Value))
            .Aggregate(Probability.Zero, (current, pair) => current + pair.Probability);

        return result;
    }
}