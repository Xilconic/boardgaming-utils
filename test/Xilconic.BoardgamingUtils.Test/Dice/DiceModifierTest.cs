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
using System.Collections.ObjectModel;

using NUnit.Framework;
using Rhino.Mocks;
using Xilconic.BoardgamingUtils.Dice;
using Xilconic.BoardgamingUtils.Mathmatics;
using Xilconic.BoardgamingUtils.PseudoRandom;

namespace Xilconic.BoardgamingUtils.Test.Dice
{
    [TestFixture]
    public class DiceModifierTest
    {
        [TestCase(-506409)]
        [TestCase(54987)]
        public void Constructor_ExpectedValues(int modifierValue)
        {
            // Setup
            var rng = MockRepository.GenerateStub<IRandomNumberGenerator>();
            var die = MockRepository.GenerateStub<IAbstractDie>();
            die.Stub(d => d.ProbabilityDistribution)
               .Return(new DiscreteValueProbabilityDistribution(new[] { new ValueProbabilityPair(0, 1.0) }));

            // Call
            var modifier = new DiceModifier(die, modifierValue, rng);

            // Assert
            Assert.IsInstanceOf<AbstractDie>(modifier);
            Assert.AreEqual(modifierValue, modifier.Modifier);
        }

        [TestCase(-123)]
        [TestCase(456)]
        public void ProbabilityDistribution_AddingValueTo1D6_ReturnExpectedDistribution(int modifierValue)
        {
            // Setup
            var rng = MockRepository.GenerateStub<IRandomNumberGenerator>();
            var die = new NumericalDie(6, rng);

            var modifier = new DiceModifier(die, modifierValue, rng);

            // Call
            DiscreteValueProbabilityDistribution distribution = modifier.ProbabilityDistribution;

            // Assert
            ReadOnlyCollection<ValueProbabilityPair> specification = die.ProbabilityDistribution.Specification;
            Assert.AreEqual(specification.Count, distribution.Specification.Count);
            for (int i = 0; i < specification.Count; i++)
            {
                ValueProbabilityPair referencePair = specification[i];
                ValueProbabilityPair actualPair = distribution.Specification[i];
                Assert.AreEqual(referencePair.Probability, actualPair.Probability);
                Assert.AreEqual(referencePair.Value + modifierValue, actualPair.Value);
            }
        }
    }
}
