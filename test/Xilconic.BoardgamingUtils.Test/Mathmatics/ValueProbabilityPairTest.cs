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
using NUnit.Framework;
using Xilconic.BoardgamingUtils.Mathmatics;

namespace Xilconic.BoardgamingUtils.Test.Mathmatics
{
    [TestFixture]
    public static class ValueProbabilityPairTest
    {
        [Combinatorial]
        public static void Constructor_ValidArguments_ExpectedValue(
            [Values(int.MinValue, -5467, 0, 89754, int.MaxValue)]int value,
            [Values(0.0, 0.345, 1.0)]double probability)
        {
            // Call
            var pair = new ValueProbabilityPair(value, probability);

            // Setup
            Assert.AreEqual(value, pair.Value);
            Assert.AreEqual(probability, pair.Probability);
        }
    }
}
