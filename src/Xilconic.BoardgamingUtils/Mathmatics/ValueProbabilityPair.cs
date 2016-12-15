using System;
using System.Diagnostics.Contracts;

namespace Xilconic.BoardgamingUtils.Mathmatics
{
    /// <summary>
    /// A tuple of some value with it's corresponding probability of occurrence.
    /// </summary>
    public sealed class ValueProbabilityPair
    {
        /// <summary>
        /// Creates a new instance of <see cref="ValueProbabilityPair"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="probability">The probability.</param>
        public ValueProbabilityPair(int value, double probability)
        {
            Contract.Requires<ArgumentOutOfRangeException>(!double.IsNaN(probability));
            Contract.Requires<ArgumentOutOfRangeException>(probability >= 0.0);
            Contract.Requires<ArgumentOutOfRangeException>(probability <= 1.0);

            Value = value;
            Probability = probability;
        }

        /// <summary>
        /// The probability that <see cref="Value"/> occurs.
        /// </summary>
        public double Probability { get; private set; }

        /// <summary>
        /// The value.
        /// </summary>
        public int Value { get; private set; }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(!double.IsNaN(Probability));
            Contract.Invariant(Probability >= 0.0);
            Contract.Invariant(Probability <= 1.0);
        }
    }
}
