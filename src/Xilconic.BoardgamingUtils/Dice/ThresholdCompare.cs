// This file is part of Boardgaming Utils.
//
// Boardgaming Utilsis free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// Boardgaming Utilsis distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with Boardgaming Utils. If not, see <http://www.gnu.org/licenses/>.
using System;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using Xilconic.BoardgamingUtils.Mathmatics;
using Xilconic.BoardgamingUtils.PseudoRandom;

namespace Xilconic.BoardgamingUtils.Dice
{
    /// <summary>
    /// Class that compares the result of an <see cref="IAbstractDie"/> to some other value.
    /// </summary>
    public class ThresholdCompare : IRollable<bool>, IDiscreteBooleanRandomVariable
    {
        private IAbstractDie die;
        private readonly IRandomNumberGenerator rng;

        /// <summary>
        /// Creates a new instance of <see cref="ThresholdCompare"/>.
        /// </summary>
        /// <param name="die">The die whose result should be compared.</param>
        /// <param name="comparisonType">How the result of <paramref name="die"/> should be compared.</param>
        /// <param name="referenceValue">To what value <paramref name="die"/> should be compared.</param>
        /// <param name="rng">The random number generator.</param>
        public ThresholdCompare(IAbstractDie die, ComparisonType comparisonType, int referenceValue,
            IRandomNumberGenerator rng)
        {
            Contract.Requires<ArgumentNullException>(die != null);
            Contract.Requires<InvalidEnumArgumentException>(comparisonType == ComparisonType.Greater ||
                comparisonType == ComparisonType.GreaterOrEqual || comparisonType == ComparisonType.Smaller ||
                comparisonType == ComparisonType.SmallerOrEqual);
            Contract.Requires<ArgumentNullException>(rng != null);

            this.die = die;
            ComparisonType = comparisonType;
            ReferenceValue = referenceValue;
            this.rng = rng;

            double successProbability = CreateProbabilityDistribution(die, comparisonType, referenceValue);
            ProbabilityDistribution = new BooleanProbabilityDistribution(successProbability);
        }

        /// <summary>
        /// The value used to compare the die result with.
        /// </summary>
        public int ReferenceValue { get; }

        /// <summary>
        /// The probability distribution of the die passing the comparison or not.
        /// </summary>
        public BooleanProbabilityDistribution ProbabilityDistribution { get; }

        /// <summary>
        /// The type of comparison performed.
        /// </summary>
        public ComparisonType ComparisonType { get; }

        public bool Roll()
        {
            return rng.NextFactor() <= ProbabilityDistribution.SuccessProbability;
        }

        private double CreateProbabilityDistribution(IAbstractDie die, ComparisonType comparisonType, int referenceValue)
        {
            Contract.Ensures(Contract.Result<double>() >= 0.0);
            Contract.Ensures(Contract.Result<double>() <= 1.0);

            Func<int, bool> comparitor = GetComparitor(comparisonType, referenceValue);
            return die.ProbabilityDistribution.Specification
                .Where(p => comparitor(p.Value))
                .Sum(p => p.Probability);
        }

        private Func<int, bool> GetComparitor(ComparisonType comparisonType, int referenceValue)
        {
            switch (comparisonType)
            {
                case ComparisonType.Greater:
                    return i => i > referenceValue;
                case ComparisonType.GreaterOrEqual:
                    return i => i >= referenceValue;
                case ComparisonType.Smaller:
                    return i => i < referenceValue;
                case ComparisonType.SmallerOrEqual:
                    return i => i <= referenceValue;
                default:
                    throw new InvalidEnumArgumentException(nameof(comparisonType), (int)comparisonType, typeof(ComparisonType));
            }
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(ProbabilityDistribution != null);
        }
    }
}
