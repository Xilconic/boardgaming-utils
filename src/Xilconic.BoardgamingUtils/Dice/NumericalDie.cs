using System;
using System.Diagnostics.Contracts;
using System.Linq;
using Xilconic.BoardgamingUtils.Mathmatics;
using Xilconic.BoardgamingUtils.PseudoRandom;

namespace Xilconic.BoardgamingUtils.Dice
{
    /// <summary>
    /// Class representing a die numbering from 1 to N where N is the number of sides the die has.
    /// </summary>
    public sealed class NumericalDie : IAbstractDie
    {
        private readonly IRandomNumberGenerator rng;

        /// <summary>
        /// Creates a new instance of <see cref="NumericalDie"/>.
        /// </summary>
        /// <param name="numberOfSides">The number of sides the die has.</param>
        public NumericalDie(int numberOfSides, IRandomNumberGenerator rng)
        {
            Contract.Requires<ArgumentOutOfRangeException>(numberOfSides > 0);
            Contract.Requires<ArgumentNullException>(rng != null, "rng");
            Contract.Ensures(NumberOfSides == numberOfSides);

            NumberOfSides = numberOfSides;
            ValueProbabilityPair[] dieProbabilities = CreateProbabilityDistribution(numberOfSides);
            ProbabilityDistribution = new DiscreteValueProbabilityDistribution(dieProbabilities);
            this.rng = rng;
        }

        /// <summary>
        /// The number of sides of this die.
        /// </summary>
        public int NumberOfSides { get; private set; }

        public DiscreteValueProbabilityDistribution ProbabilityDistribution { get; private set; }

        public int Roll()
        {
            Contract.Ensures(Contract.Result<int>() >= 1);
            Contract.Ensures(Contract.Result<int>() <= NumberOfSides);

            int rollResult = ProbabilityDistribution.GetValueAtCdf(rng.NextFactor());
            Contract.Assume(rollResult >= 1);
            Contract.Assume(rollResult <= NumberOfSides);
            return rollResult;
        }

        private ValueProbabilityPair[] CreateProbabilityDistribution(int numberOfSides)
        {
            Contract.Requires<ArgumentOutOfRangeException>(numberOfSides > 0);

            double uniformProbability = 1.0 / numberOfSides;
            return Enumerable.Range(1, numberOfSides)
                .Select(value => new ValueProbabilityPair(value, uniformProbability))
                .ToArray();
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(NumberOfSides > 0);
            Contract.Invariant(ProbabilityDistribution != null);
        }
    }
}
