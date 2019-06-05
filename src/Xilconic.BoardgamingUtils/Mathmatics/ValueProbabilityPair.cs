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

namespace Xilconic.BoardgamingUtils.Mathmatics
{
    /// <summary>
    /// A tuple of some value with it's corresponding probability of occurrence.
    /// </summary>
    public sealed class ValueProbabilityPair
    {
        /// <summary>
        /// Creates a new instance of <see cref="ValueProbabilityPair"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="probability">The probability.</param>
        public ValueProbabilityPair(int value, double probability)
        {
            Debug.Assert(!double.IsNaN(probability));
            Debug.Assert(0.0 <= probability && probability <= 1.0);

            Value = value;
            Probability = probability;
        }

        /// <summary>
        /// The probability that <see cref="Value"/> occurs.
        /// </summary>
        public double Probability { get; private set; }

        /// <summary>
        /// The value.
        /// </summary>
        public int Value { get; private set; }
    }
}
