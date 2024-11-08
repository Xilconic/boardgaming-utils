﻿// Copyright (c) Bas des Bouvrie ("Xilconic"). All rights reserved.
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

using Xilconic.BoardgamingUtils.Mathematics;
using Xilconic.BoardgamingUtils.PseudoRandom;

namespace Xilconic.BoardgamingUtils.Dice;

/// <summary>
/// Class representing a die numbering from 1 to N where N is the number of sides the die has.
/// </summary>
public class NumericalDie : AbstractDie
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NumericalDie"/> class.
    /// </summary>
    /// <param name="numberOfSides">The number of sides the die has.</param>
    /// <param name="rng">The random number generator.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="numberOfSides"/> is not at least 1.</exception>
    public NumericalDie(int numberOfSides, IRandomNumberGenerator rng)
        : base(rng)
    {
        if(numberOfSides <= 0) throw new ArgumentOutOfRangeException(nameof(numberOfSides), numberOfSides, "Must be at least 1.");
        
        NumberOfSides = numberOfSides;
        ValueProbabilityPair[] dieProbabilities = CreateProbabilityDistribution(numberOfSides);
        ProbabilityDistribution = new DiscreteValueProbabilityDistribution(dieProbabilities);
    }

    /// <summary>
    /// Gets the number of sides of this die.
    /// </summary>
    public int NumberOfSides { get; }

    /// <inheritdoc/>
    public override DiscreteValueProbabilityDistribution ProbabilityDistribution { get; }

    private static ValueProbabilityPair[] CreateProbabilityDistribution(int numberOfSides)
    {
        var uniformProbability = new Probability(1.0 / numberOfSides);
        return Enumerable.Range(1, numberOfSides)
            .Select(value => new ValueProbabilityPair(value, uniformProbability))
            .ToArray();
    }
}