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
using System.Diagnostics;
using System.Linq;

using Xilconic.BoardgamingUtils.Mathmatics;
using Xilconic.BoardgamingUtils.PseudoRandom;

namespace Xilconic.BoardgamingUtils.Dice
{
    /// <summary>
    /// Class representing a modifier applied to some abstract die result.
    /// </summary>
    /// <remarks>Handling overflow and underflow is responsibility of caller.</remarks>
    public class DiceModifier : AbstractDie
    {
        private readonly IAbstractDie die;

        /// <summary>
        /// Initializes a new instance of the <see cref="DiceModifier"/> class.
        /// </summary>
        /// <param name="die">The die being modified.</param>
        /// <param name="modifierValue">The value of the modification.</param>
        /// <param name="rng">The random number generator.</param>
        public DiceModifier(IAbstractDie die, int modifierValue, IRandomNumberGenerator rng)
            : base(rng)
        {
            Debug.Assert(die != null, "The die cannot be null.");

            this.die = die;
            Modifier = modifierValue;
            ProbabilityDistribution = CreateProbabilityDistribution(die, modifierValue);
        }

        /// <summary>
        /// Gets the modifier applied to the given die.
        /// </summary>
        public int Modifier { get; }

        /// <inheritdoc/>
        public override DiscreteValueProbabilityDistribution ProbabilityDistribution { get; }

        private DiscreteValueProbabilityDistribution CreateProbabilityDistribution(IAbstractDie die, int modifier)
        {
            return new DiscreteValueProbabilityDistribution(die.ProbabilityDistribution.Specification
                .Select(p => new ValueProbabilityPair(p.Value + modifier, p.Probability)));
        }
    }
}
