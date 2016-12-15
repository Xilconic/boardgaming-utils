using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Xilconic.BoardgamingUtils.Mathmatics
{
    /// <summary>
    /// Class representing a discrete probability distribution of integer values.
    /// </summary>
    public sealed class DiscreteValueProbabilityDistribution
    {
        /// <summary>
        /// Creates a new instance of <see cref="DiscreteValueProbabilityDistribution"/>.
        /// </summary>
        /// <param name="probabilitySpecification">The specification of the probability distribution.</param>
        public DiscreteValueProbabilityDistribution(IEnumerable<ValueProbabilityPair> probabilitySpecification)
        {
            Contract.Requires<ArgumentNullException>(probabilitySpecification != null);
            Contract.Requires<ArgumentException>(probabilitySpecification.Count() > 0);
            Contract.Requires<ArgumentException>(probabilitySpecification.All(p => p != null));
            Contract.Requires<ArgumentException>(Math.Abs(probabilitySpecification.Sum(p => p.Probability) - 1.0) < 1e-6,
                "The sum of all probabilities should be 1.");
            Contract.Requires<ArgumentException>(probabilitySpecification.Select(p => p.Value).Distinct().Count() == probabilitySpecification.Count(),
                "All values in the specification should be unique.");

            Specification = new ReadOnlyCollection<ValueProbabilityPair>(probabilitySpecification.ToArray());
        }

        /// <summary>
        /// The complete specification of the probability distribution.
        /// </summary>
        public ReadOnlyCollection<ValueProbabilityPair> Specification { get; private set; }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(Specification != null);
            Contract.Invariant(Specification.Count > 0);
            Contract.Invariant(Specification.All(p => p != null));
            Contract.Invariant(Math.Abs(Specification.Sum(p => p.Probability) - 1.0) < 1e-6);
            Contract.Invariant(Specification.Select(p => p.Value).Distinct().Count() == Specification.Count());
        }
    }
}
