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
using Xilconic.BoardgamingUtils.Mathmatics;
using Xilconic.BoardgamingUtils.PseudoRandom;

namespace Xilconic.BoardgamingUtils.ToolboxApp.Controls.WorkbenchItems
{
    /// <summary>
    /// The workbench item for a single die.
    /// </summary>
    internal class SingleDieWorkbenchItem : WorkbenchItemViewModel
    {
        private readonly NumericalDie die;

        /// <summary>
        /// Initializes a new instance of the <see cref="SingleDieWorkbenchItem"/> class.
        /// </summary>
        /// <param name="rng">The random number generator.</param>
        public SingleDieWorkbenchItem(IRandomNumberGenerator rng)
            : base("Single die", "Die Probabilities (pdf)", "Die face")
        {
            die = new NumericalDie(6, rng);
        }

        /// <summary>
        /// Gets or sets the number of sides of the single die.
        /// </summary>
        public int NumberOfSides { get; set; } = 6;

        /// <inheritdoc/>
        public override DiscreteValueProbabilityDistribution Distribution
        {
            get
            {
                return die.ProbabilityDistribution;
            }
        }
    }
}
