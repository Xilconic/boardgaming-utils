using System.Diagnostics.Contracts;

namespace Xilconic.BoardgamingUtils.Mathmatics
{
    /// <summary>
    /// Interface for a random variable that is an event that can result in various whole numbers.
    /// </summary>
    [ContractClass(typeof(IDiscreteIntegerRandomVariableContract))]
    public interface IDiscreteIntegerRandomVariable
    {
        /// <summary>
        /// The probability distribution of the random variable.
        /// </summary>
        DiscreteValueProbabilityDistribution ProbabilityDistribution { get; }
    }

    [ContractClassFor(typeof(IDiscreteIntegerRandomVariable))]
    internal abstract class IDiscreteIntegerRandomVariableContract : IDiscreteIntegerRandomVariable
    {
        public DiscreteValueProbabilityDistribution ProbabilityDistribution
        {
            get
            {
                Contract.Ensures(Contract.Result<DiscreteValueProbabilityDistribution>() != null);
                return default(DiscreteValueProbabilityDistribution);
            }
        }
    }
}
