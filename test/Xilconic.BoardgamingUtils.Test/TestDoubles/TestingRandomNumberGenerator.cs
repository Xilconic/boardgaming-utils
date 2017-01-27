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
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Xilconic.BoardgamingUtils.PseudoRandom;

namespace Xilconic.BoardgamingUtils.Test.TestDoubles
{
    /// <summary>
    /// Implementation of <see cref="IRandomNumberGenerator"/> that allows for predictable number
    /// generation, useful for unit testing.
    /// </summary>
    [ContractVerification(true)]
    internal class TestingRandomNumberGenerator : IRandomNumberGenerator
    {
        private readonly Queue<double> factorValues;

        /// <summary>
        /// Creates a new instance of <see cref="TestingRandomNumberGenerator"/>.
        /// </summary>
        public TestingRandomNumberGenerator()
        {
            factorValues = new Queue<double>();
        }

        /// <summary>
        /// Adds a collection of factors (all in the [0.0, 1.0] range) that are expected to be used.
        /// </summary>
        /// <param name="factors">The collection of factors.</param>
        public void AddFactorValues(IEnumerable<double> factors)
        {
            Contract.Requires<ArgumentNullException>(factors != null);
            Contract.Requires<ArgumentOutOfRangeException>(Contract.ForAll(factors, f => f >= 0.0));
            Contract.Requires<ArgumentOutOfRangeException>(Contract.ForAll(factors, f => f <= 1.0));

            foreach(double factor in factors)
            {
                factorValues.Enqueue(factor);
            }
        }

        /// <summary>
        /// Returns the next value in the range of [0.0, 1.0].
        /// </summary>
        public double NextFactor()
        {
            if (factorValues.Any())
            {
                double factor = factorValues.Dequeue();
                Contract.Assume(factor >= 0.0);
                Contract.Assume(factor <= 1.0);
                return factor;
            }
            else
            {
                throw new InvalidOperationException("Unexpected call to 'NextFactor()'. Did you add enough elements using 'AddFactorValues(IEnumerable<double>)'?");
            }
        }
    }
}
