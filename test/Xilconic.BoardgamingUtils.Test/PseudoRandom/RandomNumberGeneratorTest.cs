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
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xilconic.BoardgamingUtils.PseudoRandom;

namespace Xilconic.BoardgamingUtils.Test.PseudoRandom
{
    [TestFixture]
    public class RandomNumberGeneratorTest
    {
        [Test]
        [TestCase(0)]
        [TestCase(123456789)]
        public void NextFactor_WithKnownSeed_ReturnValueBetweenZeroAndOne(int seed)
        {
            // Setup
            var dotNetRandom = new Random(seed);
            var rng = new RandomNumberGenerator(seed);

            // Call
            double factor = rng.NextFactor();

            // Assert
            Assert.AreEqual(dotNetRandom.NextDouble(), factor);
        }
    }
}
