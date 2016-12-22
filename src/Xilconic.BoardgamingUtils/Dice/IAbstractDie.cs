using System.Diagnostics.Contracts;
using Xilconic.BoardgamingUtils.Mathmatics;

namespace Xilconic.BoardgamingUtils.Dice
{
    /// <summary>
    /// Represents an abstract die that can be rolled for a result according to its probability distribution.
    /// </summary>
    [ContractClass(typeof(IAbstractDieContract))]
    public interface IAbstractDie : IRollable<int>
    {
        /// <summary>
        /// The probability distribution corresponding to this die.
        /// </summary>
        DiscreteValueProbabilityDistribution ProbabilityDistribution { get; }
    }

    [ContractClassFor(typeof(IAbstractDie))]
    internal abstract class IAbstractDieContract : IAbstractDie
    {
        public DiscreteValueProbabilityDistribution ProbabilityDistribution
        {
            get
            {
                Contract.Ensures(Contract.Result<DiscreteValueProbabilityDistribution>() != null);
                return default(DiscreteValueProbabilityDistribution);
            }
        }

        public int Roll()
        {
            return default(int);
        }
    }
}
