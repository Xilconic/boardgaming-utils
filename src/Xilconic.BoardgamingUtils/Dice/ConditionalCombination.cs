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
using System.Diagnostics;
using System.Linq;
using Xilconic.BoardgamingUtils.Mathmatics;

namespace Xilconic.BoardgamingUtils.Dice
{
    /// <summary>
    /// Class that uses one scenario if some random variable is <c>true</c>, and another scenario if that
    /// random variable is <c>false</c>.
    /// </summary>
    public class ConditionalCombination : IDiscreteIntegerRandomVariable
    {
        /// <summary>
        /// Creates a new instance of <see cref="ConditionalCombination"/>.
        /// </summary>
        /// <param name="condition">The conditional event.</param>
        /// <param name="trueCase">The scenario to be used if <paramref name="condition"/> is <c>true</c>.</param>
        /// <param name="falseCase">The scenario to be used if <paramref name="condition"/> is <c>false</c>.</param>
        public ConditionalCombination(IDiscreteBooleanRandomVariable condition,
            IDiscreteIntegerRandomVariable trueCase, IDiscreteIntegerRandomVariable falseCase)
        {
            Debug.Assert(condition != null);
            Debug.Assert(trueCase != null);
            Debug.Assert(falseCase != null);

            ProbabilityDistribution = GetProbabilityDistribution(condition, trueCase, falseCase);
        }

        private static DiscreteValueProbabilityDistribution GetProbabilityDistribution(IDiscreteBooleanRandomVariable condition,
            IDiscreteIntegerRandomVariable trueCase, IDiscreteIntegerRandomVariable falseCase)
        {
            IDictionary<int, double> valueAndProbabilities = trueCase.ProbabilityDistribution.Specification
                .ToDictionary(pair => pair.Value, pair => pair.Probability * condition.ProbabilityDistribution.SuccessProbability);
            foreach(ValueProbabilityPair pair in falseCase.ProbabilityDistribution.Specification)
            {
                double probabilityComponent = condition.ProbabilityDistribution.FailureProbability * pair.Probability;
                if (valueAndProbabilities.ContainsKey(pair.Value))
                {
                    valueAndProbabilities[pair.Value] += probabilityComponent;
                }
                else
                {
                    valueAndProbabilities[pair.Value] = probabilityComponent;
                }
            }
            return new DiscreteValueProbabilityDistribution(valueAndProbabilities.Select(vp => new ValueProbabilityPair(vp.Key, vp.Value)));
        }

        public DiscreteValueProbabilityDistribution ProbabilityDistribution { get; }
    }
}
