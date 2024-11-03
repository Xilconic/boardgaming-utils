// Copyright (c) Bas des Bouvrie ("Xilconic"). All rights reserved.
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

using Xilconic.BoardgamingUtils.Mathematics;
using Xilconic.BoardgamingUtils.PseudoRandom;

namespace Xilconic.BoardgamingUtils.Dice;

/// <summary>
/// This class represents a collection of dice whose rolled value is determined by the sum of all
/// individual die rolls in that collection.
/// </summary>
public class SumOfDice : AbstractDie
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SumOfDice"/> class.
    /// </summary>
    /// <param name="dice">The dice that define this object.</param>
    /// <param name="rng">The random number generator.</param>
    public SumOfDice(IReadOnlyCollection<IAbstractDie> dice, IRandomNumberGenerator rng)
        : base(rng)
    {
        ArgumentNullException.ThrowIfNull(dice);
        if(dice.Count == 0) throw new ArgumentException("The collection of dice must contain at least 1 element.", nameof(dice));

        IAbstractDie[] diceCopy = dice.ToArray();
        ValueProbabilityPair[] probabilitySpecification = CreateProbabilityDistribution(diceCopy);
        ProbabilityDistribution = new DiscreteValueProbabilityDistribution(probabilitySpecification);
    }

    /// <inheritdoc/>
    public override DiscreteValueProbabilityDistribution ProbabilityDistribution { get; }

    private static ValueProbabilityPair[] CreateProbabilityDistribution(IAbstractDie[] dice)
    {
        IDictionary<int, Probability> runningDictionary = dice[0].ProbabilityDistribution.Specification.ToDictionary(
            p => p.Value, 
            p => p.Probability
        );
        for (int i = 1; i < dice.Length; i++)
        {
            var workingDictionary = new Dictionary<int, Probability>();
            foreach ((int dieValue, Probability probability) in runningDictionary)
            {
                foreach (ValueProbabilityPair secondPair in dice[i].ProbabilityDistribution.Specification)
                {
                    int sum = dieValue + secondPair.Value;
                    Probability probabilityOfBothDieRolls = probability * secondPair.Probability; // P(A & B) = P(A) * P(B)
                    if (!workingDictionary.TryAdd(sum, probabilityOfBothDieRolls))
                    {
                        workingDictionary[sum] += probabilityOfBothDieRolls;
                    }
                }
            }

            runningDictionary = workingDictionary;
        }

        return runningDictionary.Select(kvp => new ValueProbabilityPair(kvp.Key, kvp.Value)).ToArray();
    }
}