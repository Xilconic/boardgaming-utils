// This file is part of Boardgaming Utils.
//
// Boardgaming Utilsis free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// Boardgaming Utilsis distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with Boardgaming Utils. If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Diagnostics.Contracts;
using Xilconic.BoardgamingUtils.Mathmatics;
using Xilconic.BoardgamingUtils.PseudoRandom;

namespace Xilconic.BoardgamingUtils.Dice
{
    /// <summary>
    /// Abstract class for common implementation details for <see cref="IAbstractDie"/>.
    /// </summary>
    public abstract class AbstractDie : IAbstractDie
    {
        private readonly IRandomNumberGenerator rng;

        /// <summary>
        /// Creates a new instance of <see cref="AbstractDie"/>.
        /// </summary>
        /// <param name="rng">The random number generator.</param>
        public AbstractDie(IRandomNumberGenerator rng)
        {
            Contract.Requires<ArgumentNullException>(rng != null);
            this.rng = rng;
        }

        public abstract DiscreteValueProbabilityDistribution ProbabilityDistribution
        {
            get;
        }

        public int Roll()
        {
            return ProbabilityDistribution.GetValueAtCdf(rng.NextFactor());
        }
    }
}
