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
    /// This class represents a collection of dice whose rolled value is determined by the sum of all
    /// individual die rolls in that collection.
    /// </summary>
    public class SumOfDice
    {
        private readonly NumericalDie[] dice;

        /// <summary>
        /// Creates a new instance of <see cref="SumOfDice"/>.
        /// </summary>
        /// <param name="dice">The dice that define this object.</param>
        public SumOfDice(IEnumerable<NumericalDie> dice)
        {
            Contract.Requires<ArgumentNullException>(dice != null);
            Contract.Requires<ArgumentException>(Contract.ForAll(dice, d => d != null));
            Contract.Requires<ArgumentException>(dice.Count() > 0);

            this.dice = dice.ToArray();
            ValueProbabilityPair[] probabilitySpecification = CreateProbabilityDistribution(this.dice);
            ProbabilityDistribution = new DiscreteValueProbabilityDistribution(probabilitySpecification);
        }

        /// <summary>
        /// The probability distribution of this collection of dice.
        /// </summary>
        public DiscreteValueProbabilityDistribution ProbabilityDistribution { get; private set; }


        private ValueProbabilityPair[] CreateProbabilityDistribution(NumericalDie[] dice)
        {
            IDictionary<int, double> runningDictionary = dice[0].ProbabilityDistribution.Specification.ToDictionary(p => p.Value, p => p.Probability);
            for(int i = 1; i < dice.Length; i++)
            {
                IDictionary<int, double> workingDictionary = new Dictionary<int, double>();
                foreach (KeyValuePair<int, double> pair in runningDictionary)
                {
                    foreach(ValueProbabilityPair secondPair in dice[i].ProbabilityDistribution.Specification)
                    {
                        int sum = pair.Key + secondPair.Value;
                        double probability = pair.Value * secondPair.Probability;
                        if (!workingDictionary.ContainsKey(sum))
                        {
                            workingDictionary[sum] = probability;
                        }
                        else
                        {
                            workingDictionary[sum] += probability;
                        }
                    }
                }
                runningDictionary = workingDictionary;
            }
            return runningDictionary.Select(kvp => new ValueProbabilityPair(kvp.Key, kvp.Value)).ToArray();
        }

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(ProbabilityDistribution != null);
        }
    }
}
