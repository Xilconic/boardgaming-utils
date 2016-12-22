using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xilconic.BoardgamingUtils.Mathmatics;
using Xilconic.BoardgamingUtils.PseudoRandom;

namespace Xilconic.BoardgamingUtils.Dice
{
    /// <summary>
    /// Abstract class for common implementation details for <see cref="IAbstractDie"/>.
    /// </summary>
    public abstract class AbstractDie : IAbstractDie
    {
        private readonly IRandomNumberGenerator rng;

        /// <summary>
        /// Creates a new instance of <see cref="AbstractDie"/>.
        /// </summary>
        /// <param name="rng">The random number generator.</param>
        public AbstractDie(IRandomNumberGenerator rng)
        {
            Contract.Requires<ArgumentNullException>(rng != null);
            this.rng = rng;
        }

        public abstract DiscreteValueProbabilityDistribution ProbabilityDistribution
        {
            get;
        }

        public int Roll()
        {
            return ProbabilityDistribution.GetValueAtCdf(rng.NextFactor());
        }
    }
}
