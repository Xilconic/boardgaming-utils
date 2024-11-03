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
namespace Xilconic.BoardgamingUtils.Mathematics;

/// <summary>
/// A tuple of some value with its corresponding probability of occurrence.
/// </summary>
public record struct ValueProbabilityPair
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ValueProbabilityPair"/> class.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="probability">The probability.</param>
    public ValueProbabilityPair(int value, Probability probability)
    {
        Value = value;
        Probability = probability;
    }

    /// <summary>
    /// Gets the probability that <see cref="Value"/> occurs.
    /// </summary>
    public Probability Probability { get; }

    /// <summary>
    /// Gets the value.
    /// </summary>
    public int Value { get; }
};