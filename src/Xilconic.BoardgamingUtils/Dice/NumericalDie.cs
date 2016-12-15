using System;
using System.Diagnostics.Contracts;
using System.Linq;
using Xilconic.BoardgamingUtils.Mathmatics;

namespace Xilconic.BoardgamingUtils.Dice
{
    /// <summary>
    /// Class representing a die numbering from 1 to N where N is the number of sides the die has.
    /// </summary>
    public sealed class NumericalDie
    {
        /// <summary>
        /// Creates a new instance of <see cref="NumericalDie"/>.
        /// </summary>
        /// <param name="numberOfSides">The number of sides the die has.</param>
        public NumericalDie(int numberOfSides)
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

        /// <summary>
        /// The probability distribution of this die.
        /// </summary>
        public DiscreteValueProbabilityDistribution ProbabilityDistribution { get; private set; }

        private ValueProbabilityPair[] CreateProbabilityDistribution(int numberOfSides)
        {
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
