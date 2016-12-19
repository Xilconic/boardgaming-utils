using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Xilconic.BoardgamingUtils.PseudoRandom;

namespace Xilconic.BoardgamingUtils.Test.TestDoubles
{
    /// <summary>
    /// Implementation of <see cref="IRandomNumberGenerator"/> that allows for predictable number
    /// generation, useful for unit testing.
    /// </summary>
    [ContractVerification(true)]
    internal class TestingRandomNumberGenerator : IRandomNumberGenerator
    {
        private readonly Queue<double> factorValues;

        /// <summary>
        /// Creates a new instance of <see cref="TestingRandomNumberGenerator"/>.
        /// </summary>
        public TestingRandomNumberGenerator()
        {
            factorValues = new Queue<double>();
        }

        /// <summary>
        /// Adds a collection of factors (all in the [0.0, 1.0] range) that are expected to be used.
        /// </summary>
        /// <param name="factors">The collection of factors.</param>
        public void AddFactorValues(IEnumerable<double> factors)
        {
            Contract.Requires<ArgumentNullException>(factors != null);
            Contract.Requires<ArgumentOutOfRangeException>(Contract.ForAll(factors, f => f >= 0.0));
            Contract.Requires<ArgumentOutOfRangeException>(Contract.ForAll(factors, f => f <= 1.0));

            foreach(double factor in factors)
            {
                factorValues.Enqueue(factor);
            }
        }

        /// <summary>
        /// Returns the next value in the range of [0.0, 1.0].
        /// </summary>
        public double NextFactor()
        {
            if (factorValues.Any())
            {
                double factor = factorValues.Dequeue();
                Contract.Assume(factor >= 0.0);
                Contract.Assume(factor <= 1.0);
                return factor;
            }
            else
            {
                throw new InvalidOperationException("Unexpected call to 'NextFactor()'. Did you add enough elements using 'AddFactorValues(IEnumerable<double>)'?");
            }
        }
    }
}
