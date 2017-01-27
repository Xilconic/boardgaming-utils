﻿// This file is part of Boardgaming Utils.
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
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xilconic.BoardgamingUtils.Dice;
using Xilconic.BoardgamingUtils.PseudoRandom;
using Xilconic.BoardgamingUtils.Mathmatics;
using Xilconic.BoardgamingUtils.Test.TestDoubles;

namespace Xilconic.BoardgamingUtils.Test.Dice
{
    [TestFixture]
    public class AbstractDieTest
    {
        [Test]
        public void Roll_ReturnValueBasedOnProbabilityDistribution()
        {
            // Setup
            var spec = new[]
            {
                new ValueProbabilityPair(0, 0.5),
                new ValueProbabilityPair(1, 0.5)
            };
            var distribution = new DiscreteValueProbabilityDistribution(spec);

            var expectedValueAndProbabilities = new[]
            {
                Tuple.Create(0, 0.0),
                Tuple.Create(0, 0.324),
                Tuple.Create(0, 0.5),
                Tuple.Create(1, 0.5+1e-6),
                Tuple.Create(1, 0.75),
                Tuple.Create(1, 1.0),
            };

            var rng = new TestingRandomNumberGenerator();
            rng.AddFactorValues(expectedValueAndProbabilities.Select(t => t.Item2));

            AbstractDie die = new TestAbstractDie(rng, distribution);

            foreach(Tuple<int, double> tuple in expectedValueAndProbabilities)
            {
                // Call
                int rollResult = die.Roll();

                // Assert
                Assert.AreEqual(tuple.Item1, rollResult);
            }
        }

        private class TestAbstractDie : AbstractDie
        {
            public TestAbstractDie(IRandomNumberGenerator rng, DiscreteValueProbabilityDistribution distribution) : base(rng)
            {
                ProbabilityDistribution = distribution;
            }

            public override DiscreteValueProbabilityDistribution ProbabilityDistribution
            {
                get;
            }
        }
    }
}
