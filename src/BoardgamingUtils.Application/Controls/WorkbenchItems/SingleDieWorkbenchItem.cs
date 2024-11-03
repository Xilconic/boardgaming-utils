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

using Xilconic.BoardgamingUtils.Dice;
using Xilconic.BoardgamingUtils.Mathematics;
using Xilconic.BoardgamingUtils.PseudoRandom;

namespace Xilconic.BoardgamingUtils.Application.Controls.WorkbenchItems;

/// <summary>
/// The workbench item for a single die.
/// </summary>
public class SingleDieWorkbenchItem : WorkbenchItemViewModel
{
    private readonly IRandomNumberGenerator _rng;

    private NumericalDie _die;

    /// <summary>
    /// Initializes a new instance of the <see cref="SingleDieWorkbenchItem"/> class.
    /// </summary>
    /// <param name="rng">The random number generator.</param>
    /// <param name="numberOfSides">The number of sides of the die.</param>
    public SingleDieWorkbenchItem(IRandomNumberGenerator rng, int numberOfSides = 6)
        : base("Single die", "Die Probabilities (pdf)", "Die face")
    {
        _rng = rng;
        _die = new NumericalDie(numberOfSides, rng);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="SingleDieWorkbenchItem"/> class.
    /// </summary>
    /// <param name="other">The object to be copied.</param>
    private SingleDieWorkbenchItem(SingleDieWorkbenchItem other)
        : this(other._rng, other.NumberOfSides)
    {
    }

    /// <summary>
    /// Gets or sets the number of sides of the single die.
    /// </summary>
    public int NumberOfSides
    {
        get => _die.NumberOfSides;
        set
        {
            _die = new NumericalDie(value, _rng);
            NotifyPropertyChanged(nameof(NumberOfSides));
            NotifyPropertyChanged(nameof(Distribution));
        }
    }

    /// <inheritdoc/>
    public override DiscreteValueProbabilityDistribution Distribution => _die.ProbabilityDistribution;

    /// <inheritdoc/>
    public override WorkbenchItemViewModel DeepClone()
    {
        return new SingleDieWorkbenchItem(this);
    }
}