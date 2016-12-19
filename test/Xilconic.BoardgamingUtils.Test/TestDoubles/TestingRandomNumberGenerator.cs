using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using Xilconic.BoardgamingUtils.PseudoRandom;

namespace Xilconic.BoardgamingUtils.Test.TestDoubles
{
    [ContractVerification(true)]
    internal class TestingRandomNumberGenerator : IRandomNumberGenerator
    {
        private readonly Queue<double> factorValues;

        public TestingRandomNumberGenerator()
        {
            factorValues = new Queue<double>();
        }

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
