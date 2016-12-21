using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xilconic.BoardgamingUtils.Mathmatics;

namespace Xilconic.BoardgamingUtils.Dice
{
    /// <summary>
    /// Represents an abstract die that can be rolled for a result according to its probability distribution.
    /// </summary>
    [ContractClass(typeof(IAbstractDieContract))]
    public interface IAbstractDie
    {
        /// <summary>
        /// The probability distribution corresponding to this die.
        /// </summary>
        DiscreteValueProbabilityDistribution ProbabilityDistribution { get; }

        /// <summary>
        /// Rolls the die for a result.
        /// </summary>
        /// <returns>The rolled result.</returns>
        int Roll();
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
            Contract.Ensures(Contract.Exists(ProbabilityDistribution.Specification, p => p.Value == Contract.Result<int>()));
            return default(int);
        }
    }
}
