using System.Diagnostics.Contracts;

namespace Xilconic.BoardgamingUtils.PseudoRandom
{
    [ContractClass(typeof(IRandomNumberGeneratorContract))]
    public interface IRandomNumberGenerator
    {
        /// <summary>
        /// Generates a new peusdo-random number in the range [0, 1).
        /// </summary>
        /// <returns>The randomly generated number.</returns>
        double NextFactor();
    }

    [ContractClassFor(typeof(IRandomNumberGenerator))]
    internal abstract class IRandomNumberGeneratorContract : IRandomNumberGenerator
    {
        public double NextFactor()
        {
            Contract.Ensures(Contract.Result<double>() >= 0.0);
            Contract.Ensures(Contract.Result<double>() <= 1.0);
            return default(double);
        }
    }
}
