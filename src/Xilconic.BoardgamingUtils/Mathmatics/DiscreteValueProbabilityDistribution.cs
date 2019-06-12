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

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace Xilconic.BoardgamingUtils.Mathmatics
{
    /// <summary>
    /// Class representing a discrete probability distribution of integer values.
    /// </summary>
    public sealed class DiscreteValueProbabilityDistribution
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DiscreteValueProbabilityDistribution"/> class.
        /// </summary>
        /// <param name="probabilitySpecification">The complete probability mass function specification.</param>
        public DiscreteValueProbabilityDistribution(IEnumerable<ValueProbabilityPair> probabilitySpecification)
        {
            Debug.Assert(probabilitySpecification != null, "The collection of probability-value pairs cannot be null.");
            Debug.Assert(probabilitySpecification.Count() > 0, "The collection of probability-value pairs must contain at least 1 element.");
            Debug.Assert(probabilitySpecification.All(element => element != null), "The collection of probability-value pairs cannot contain null elements.");
            Debug.Assert(
                Math.Abs(probabilitySpecification.Sum(p => p.Probability) - 1.0) < 1e-6,
                "The sum of all probabilities should be 1.");
            Debug.Assert(
                probabilitySpecification.Select(p => p.Value).Distinct().Count() == probabilitySpecification.Count(),
                "All values in the specification should be unique.");

            Specification = new ReadOnlyCollection<ValueProbabilityPair>(probabilitySpecification.OrderBy(p => p.Value).ToArray());

            Debug.Assert(Specification != null, "The probability specification should be initialized.");
            Debug.Assert(Specification.Count > 0, "The probability specification should contain at least 1 element.");
            Debug.Assert(Specification.All(pair => pair != null), "The probability specification should not contain null elements.");
            Debug.Assert(Math.Abs(Specification.Sum(p => p.Probability) - 1.0) < 1e-6, "The sum of all probabilities in the probability specification is 1.");
            Debug.Assert(Specification.Select(p => p.Value).Distinct().Count() == Specification.Count(), "All values in the probability specification are unique.");
            Debug.Assert(
                IsSpecificationInStrictIncreasingOrderBasedOnValueProbabilityPairValue(),
                "The probability specification should be ordered in a strict increasing collection on ValueProbabilityPair.Value.");
        }

        /// <summary>
        /// Gets the complete probability mass function specification of this distribution.
        /// </summary>
        /// <remarks>The elements have been ordered on <see cref="ValueProbabilityPair.Value"/> in
        /// ascending order.</remarks>
        public ReadOnlyCollection<ValueProbabilityPair> Specification { get; private set; }

        /// <summary>
        /// Samples the distribution at the given cdf probability and returns the corresponding value.
        /// </summary>
        /// <param name="probabilityValue">The probability.</param>
        /// <returns>The value corresponding with the cdf probability.</returns>
        /// <remarks>Limited precision of <see cref="double"/> can result in slight unexpected at
        /// boundaries between two elements in <see cref="Specification"/>.</remarks>
        public int GetValueAtCdf(double probabilityValue)
        {
            Debug.Assert(0.0 <= probabilityValue && probabilityValue <= 1.0, "The probability should be in range [0.0, 1.0].");

            double runningLowerProbabilityBracket = 0.0;
            foreach (ValueProbabilityPair pair in Specification)
            {
                if (runningLowerProbabilityBracket <= probabilityValue && probabilityValue <= runningLowerProbabilityBracket + pair.Probability)
                {
                    return pair.Value;
                }
                else
                {
                    runningLowerProbabilityBracket += pair.Probability;
                }
            }

            return Specification.Last().Value;
        }

        private bool IsSpecificationInStrictIncreasingOrderBasedOnValueProbabilityPairValue()
        {
            for (int i = 1; i < Specification.Count; i++)
            {
                if (Specification[i - 1].Value >= Specification[i].Value)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
