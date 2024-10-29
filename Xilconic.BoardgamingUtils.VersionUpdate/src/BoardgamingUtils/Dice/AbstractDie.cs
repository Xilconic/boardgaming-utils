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

using Xilconic.BoardgamingUtils.Mathematics;
using Xilconic.BoardgamingUtils.PseudoRandom;

namespace Xilconic.BoardgamingUtils.Dice;

/// <summary>
/// Abstract class for common implementation details for <see cref="IAbstractDie"/>.
/// </summary>
public abstract class AbstractDie : IAbstractDie
{
    private readonly IRandomNumberGenerator _rng;

    /// <summary>
    /// Initializes a new instance of the <see cref="AbstractDie"/> class.
    /// </summary>
    /// <param name="rng">The random number generator.</param>
    protected AbstractDie(IRandomNumberGenerator rng)
    {
        ArgumentNullException.ThrowIfNull(rng);
        _rng = rng;
    }

    /// <inheritdoc/>
    public abstract DiscreteValueProbabilityDistribution ProbabilityDistribution
    {
        get;
    }

    /// <inheritdoc/>
    public int Roll()
    {
        var probability = new Probability(_rng.NextFactor());
        return ProbabilityDistribution.GetValueAtCdf(probability);
    }
}