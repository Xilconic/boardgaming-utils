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

namespace Xilconic.BoardgamingUtils.Mathmatics
{
    /// <summary>
    /// Class representing a probability distribution for a <see cref="bool"/> value. Also known as
    /// a Bernoulli Distribution
    /// </summary>
    public sealed class BooleanProbabilityDistribution
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BooleanProbabilityDistribution"/> class.
        /// </summary>
        /// <param name="successProbability">The probability on success.</param>
        public BooleanProbabilityDistribution(double successProbability)
        {
            Debug.Assert(0.0 <= successProbability && successProbability <= 1.0, "The success probability must be in range [0.0, 1.0].");

            SuccessProbability = successProbability;
            FailureProbability = 1.0 - successProbability;

            Debug.Assert(0.0 <= FailureProbability && FailureProbability <= 1.0, "The failure probability must be in range [0.0, 1.0].");
        }

        /// <summary>
        /// Gets the probability on success.
        /// </summary>
        public double SuccessProbability { get; }

        /// <summary>
        /// Gets the probability on failure.
        /// </summary>
        public double FailureProbability { get; }
    }
}
