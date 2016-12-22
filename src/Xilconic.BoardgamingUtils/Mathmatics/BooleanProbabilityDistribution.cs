using System;
using System.Diagnostics.Contracts;

namespace Xilconic.BoardgamingUtils.Mathmatics
{
    /// <summary>
    /// Class representing a probability distribution for a <see cref="bool"/> value. Also known as
    /// a Bernoulli Distribution
    /// </summary>
    public sealed class BooleanProbabilityDistribution
    {
        /// <summary>
        /// Creates a new instance of <see cref="BooleanProbabilityDistribution"/>.
        /// </summary>
        /// <param name="successProbability">The probability on success.</param>
        public BooleanProbabilityDistribution(double successProbability)
        {
            Contract.Requires<ArgumentOutOfRangeException>(successProbability >= 0.0);
            Contract.Requires<ArgumentOutOfRangeException>(successProbability <= 1.0);

            SuccessProbability = successProbability;
            FailureProbability = 1.0 - successProbability;
        }

        /// <summary>
        /// The probability on success.
        /// </summary>
        public double SuccessProbability { get; }

        /// <summary>
        /// The probability on failure.
        /// </summary>
        public double FailureProbability { get; }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(SuccessProbability >= 0.0);
            Contract.Invariant(SuccessProbability <= 1.0);
            Contract.Invariant(FailureProbability >= 0.0);
            Contract.Invariant(FailureProbability <= 1.0);
        }
    }
}
