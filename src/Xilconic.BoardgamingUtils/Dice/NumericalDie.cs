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
using System.Linq;
using Xilconic.BoardgamingUtils.Mathmatics;
using Xilconic.BoardgamingUtils.PseudoRandom;

namespace Xilconic.BoardgamingUtils.Dice
{
    /// <summary>
    /// Class representing a die numbering from 1 to N where N is the number of sides the die has.
    /// </summary>
    public sealed class NumericalDie : AbstractDie
    {

        /// <summary>
        /// Creates a new instance of <see cref="NumericalDie"/>.
        /// </summary>
        /// <param name="numberOfSides">The number of sides the die has.</param>
        /// <param name="rng">The random number generator.</param>
        public NumericalDie(int numberOfSides, IRandomNumberGenerator rng) : base(rng)
        {
            Contract.Requires<ArgumentOutOfRangeException>(numberOfSides > 0);
            Contract.Ensures(NumberOfSides == numberOfSides);

            NumberOfSides = numberOfSides;
            ValueProbabilityPair[] dieProbabilities = CreateProbabilityDistribution(numberOfSides);
            ProbabilityDistribution = new DiscreteValueProbabilityDistribution(dieProbabilities);
        }

        /// <summary>
        /// The number of sides of this die.
        /// </summary>
        public int NumberOfSides { get; private set; }

        public override DiscreteValueProbabilityDistribution ProbabilityDistribution { get; }

        private ValueProbabilityPair[] CreateProbabilityDistribution(int numberOfSides)
        {
            Contract.Requires<ArgumentOutOfRangeException>(numberOfSides > 0);

            double uniformProbability = 1.0 / numberOfSides;
            return Enumerable.Range(1, numberOfSides)
                .Select(value => new ValueProbabilityPair(value, uniformProbability))
                .ToArray();
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(NumberOfSides > 0);
        }
    }
}
