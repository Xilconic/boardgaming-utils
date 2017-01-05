using System.Diagnostics.Contracts;

namespace Xilconic.BoardgamingUtils.Mathmatics
{
    /// <summary>
    /// Interface for a random variable that is an event that either holds (is <c>true</c>) or not
    /// (is <c>false</c>).
    /// </summary>
    [ContractClass(typeof(IDiscreteBooleanRandomVariableContract))]
    public interface IDiscreteBooleanRandomVariable
    {
        /// <summary>
        /// The probability distribution of the random variable.
        /// </summary>
        BooleanProbabilityDistribution ProbabilityDistribution { get; }
    }

    [ContractClassFor(typeof(IDiscreteBooleanRandomVariable))]
    internal abstract class IDiscreteBooleanRandomVariableContract : IDiscreteBooleanRandomVariable
    {
        public BooleanProbabilityDistribution ProbabilityDistribution
        {
            get
            {
                Contract.Ensures(Contract.Result<BooleanProbabilityDistribution>() != null);
                return default(BooleanProbabilityDistribution);
            }
        }
    }
}
