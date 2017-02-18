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
using System.Diagnostics.Contracts;

namespace Xilconic.BoardgamingUtils.Mathmatics
{
    /// <summary>
    /// Interface for a random variable that is an event that either holds (is <c>true</c>) or not
    /// (is <c>false</c>).
    /// </summary>
    [ContractClass(typeof(IDiscreteBooleanRandomVariableContract))]
    public interface IDiscreteBooleanRandomVariable
    {
        /// <summary>
        /// The probability distribution of the random variable.
        /// </summary>
        BooleanProbabilityDistribution ProbabilityDistribution { get; }
    }

    [ContractClassFor(typeof(IDiscreteBooleanRandomVariable))]
    internal abstract class IDiscreteBooleanRandomVariableContract : IDiscreteBooleanRandomVariable
    {
        public BooleanProbabilityDistribution ProbabilityDistribution
        {
            get
            {
                Contract.Ensures(Contract.Result<BooleanProbabilityDistribution>() != null);
                return default(BooleanProbabilityDistribution);
            }
        }
    }
}
