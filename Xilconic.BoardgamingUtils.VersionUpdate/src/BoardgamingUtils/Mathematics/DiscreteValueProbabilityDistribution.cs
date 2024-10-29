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

using System.Collections.ObjectModel;

namespace Xilconic.BoardgamingUtils.Mathematics;

/// <summary>
/// Class representing a discrete probability distribution of integer values.
/// </summary>
public class DiscreteValueProbabilityDistribution
{
    private readonly ValueProbabilityPair[] _orderedProbabilitySpecification;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="DiscreteValueProbabilityDistribution"/> class.
    /// </summary>
    /// <param name="probabilitySpecification">The complete probability mass function specification.</param>
    public DiscreteValueProbabilityDistribution(
        IReadOnlyCollection<ValueProbabilityPair> probabilitySpecification)
    {
        ArgumentNullException.ThrowIfNull(probabilitySpecification);
        ThrowIfEmpty(probabilitySpecification);
        ThrowIfSumOfProbabilitiesIsNotOne(probabilitySpecification);
        ThrowIfHasDuplicateValues(probabilitySpecification);

        _orderedProbabilitySpecification = probabilitySpecification.OrderBy(p => p.Value).ToArray();
    }

    private static void ThrowIfHasDuplicateValues(IReadOnlyCollection<ValueProbabilityPair> probabilitySpecification)
    {
        var numberOfUniqueValues = probabilitySpecification.Select(p => p.Value).Distinct().Count();
        if (numberOfUniqueValues != probabilitySpecification.Count)
        {
            throw new ArgumentException("All values in the specification should be unique.", nameof(probabilitySpecification));
        }
    }

    private static void ThrowIfSumOfProbabilitiesIsNotOne(IReadOnlyCollection<ValueProbabilityPair> probabilitySpecification)
    {
        try
        {
            Probability totalProbability = probabilitySpecification.Aggregate(
                Probability.Zero, 
                (current, pair) => current + pair.Probability
            );
            if (!totalProbability.Equals(Probability.One, 1e-6))
            {
                throw new ArgumentException("The sum of all probabilities should be 1.", nameof(probabilitySpecification));
            }
        }
        catch (ArgumentException)
        {
            throw new ArgumentException("The sum of all probabilities should be 1.", nameof(probabilitySpecification));
        }
    }

    private static void ThrowIfEmpty(IReadOnlyCollection<ValueProbabilityPair> probabilitySpecification)
    {
        if (probabilitySpecification.Count == 0)
        {
            throw new ArgumentException(
                "The collection of probability-value pairs must contain at least 1 element.", 
                nameof(probabilitySpecification)
            );
        }
    }

    /// <summary>
    /// Gets the complete probability mass function specification of this distribution.
    /// </summary>
    /// <remarks>The elements have been ordered on <see cref="ValueProbabilityPair.Value"/> in
    /// ascending order.</remarks>
    public IReadOnlyCollection<ValueProbabilityPair> Specification => _orderedProbabilitySpecification;
    
    /// <summary>
    /// Samples the distribution at the given cdf probability and returns the corresponding value.
    /// </summary>
    /// <param name="probabilityValue">The probability.</param>
    /// <returns>The value corresponding with the cdf probability.</returns>
    /// <remarks>Limited precision of <see cref="double"/> can result in slight unexpected at
    /// boundaries between two elements in <see cref="Specification"/>.</remarks>
    public int GetValueAtCdf(Probability probabilityValue)
    {
        var runningLowerProbabilityBracket = new Probability(0.0);
        foreach (ValueProbabilityPair pair in Specification)
        {
            if (runningLowerProbabilityBracket <= probabilityValue && probabilityValue <= runningLowerProbabilityBracket + pair.Probability)
            {
                return pair.Value;
            }
            else
            {
                runningLowerProbabilityBracket += pair.Probability;
            }
        }

        return _orderedProbabilitySpecification[^1].Value;
    }
}