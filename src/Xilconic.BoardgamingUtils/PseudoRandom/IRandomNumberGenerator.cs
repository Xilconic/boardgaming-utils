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
using System.Diagnostics.Contracts;

namespace Xilconic.BoardgamingUtils.PseudoRandom
{
    [ContractClass(typeof(IRandomNumberGeneratorContract))]
    public interface IRandomNumberGenerator
    {
        /// <summary>
        /// Generates a new peusdo-random number in the range [0, 1).
        /// </summary>
        /// <returns>The randomly generated number.</returns>
        double NextFactor();
    }

    [ContractClassFor(typeof(IRandomNumberGenerator))]
    internal abstract class IRandomNumberGeneratorContract : IRandomNumberGenerator
    {
        public double NextFactor()
        {
            Contract.Ensures(Contract.Result<double>() >= 0.0);
            Contract.Ensures(Contract.Result<double>() <= 1.0);
            return default(double);
        }
    }
}
