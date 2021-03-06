﻿// Copyright (c) Bas des Bouvrie ("Xilconic"). All rights reserved.
// This file is part of Boardgaming Utils.
//
// Boardgaming Utils is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// Boardgaming Utils is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with Boardgaming Utils. If not, see <http://www.gnu.org/licenses/>.
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using Xilconic.BoardgamingUtils.Mathmatics;
using Xilconic.BoardgamingUtils.PseudoRandom;

namespace Xilconic.BoardgamingUtils.Dice
{
    /// <summary>
    /// This class represents a collection of dice whose rolled value is determined by the sum of all
    /// individual die rolls in that collection.
    /// </summary>
    public class SumOfDice : AbstractDie
    {
        private readonly IAbstractDie[] dice;

        /// <summary>
        /// Initializes a new instance of the <see cref="SumOfDice"/> class.
        /// </summary>
        /// <param name="dice">The dice that define this object.</param>
        /// <param name="rng">The random number generator.</param>
        public SumOfDice(IEnumerable<IAbstractDie> dice, IRandomNumberGenerator rng)
            : base(rng)
        {
            Debug.Assert(dice != null, "The collection of dice cannot be null.");
            Debug.Assert(dice.All(die => die != null), "The collection of dice cannot contain null.");
            Debug.Assert(dice.Count() > 0, "The collection of dice must contain at least 1 element.");

            this.dice = dice.ToArray();
            ValueProbabilityPair[] probabilitySpecification = CreateProbabilityDistribution(this.dice);
            ProbabilityDistribution = new DiscreteValueProbabilityDistribution(probabilitySpecification);
        }

        /// <inheritdoc/>
        public override DiscreteValueProbabilityDistribution ProbabilityDistribution { get; }

        private ValueProbabilityPair[] CreateProbabilityDistribution(IAbstractDie[] dice)
        {
            IDictionary<int, double> runningDictionary = dice[0].ProbabilityDistribution.Specification.ToDictionary(p => p.Value, p => p.Probability);
            for (int i = 1; i < dice.Length; i++)
            {
                IDictionary<int, double> workingDictionary = new Dictionary<int, double>();
                foreach (KeyValuePair<int, double> pair in runningDictionary)
                {
                    foreach (ValueProbabilityPair secondPair in dice[i].ProbabilityDistribution.Specification)
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
    }
}
