﻿// This file is part of Boardgaming Utils.
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

namespace Xilconic.BoardgamingUtils.PseudoRandom
{
    /// <summary>
    /// Class responsible for generating (pseudo)random numbers.
    /// </summary>
    public class RandomNumberGenerator
    {
        private readonly Random random;

        /// <summary>
        /// Creates a new instance of <see cref="RandomNumberGenerator"/>, initialized with a given
        /// seed.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public RandomNumberGenerator(int seed)
        {
            random = new Random(seed);
        }

        public double NextFactor()
        {
            return random.NextDouble();
        }
    }
}
