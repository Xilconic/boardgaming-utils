using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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
            Contract.Requires<ArgumentNullException>(condition != null);
            Contract.Requires<ArgumentNullException>(trueCase != null);
            Contract.Requires<ArgumentNullException>(falseCase != null);

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
