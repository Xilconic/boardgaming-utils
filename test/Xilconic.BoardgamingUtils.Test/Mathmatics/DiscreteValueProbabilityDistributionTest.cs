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
using System.Collections.ObjectModel;
using System.Linq;
using Xilconic.BoardgamingUtils.Mathmatics;

namespace Xilconic.BoardgamingUtils.Test.Mathmatics
{
    [TestFixture]
    public class DiscreteValueProbabilityDistributionTest
    {
        [Test]
        public void Constructor_ValidDistributionSpecified_ExpectedValues()
        {
            // Setup
            var valueProbabilityPairs = new[]
            {
                new ValueProbabilityPair(1, 0.5),
                new ValueProbabilityPair(2, 0.25),
                new ValueProbabilityPair(3, 0.10),
                new ValueProbabilityPair(4, 0.15),
            };

            // Call
            var distribution = new DiscreteValueProbabilityDistribution(valueProbabilityPairs);

            // Assert
            ReadOnlyCollection<ValueProbabilityPair> specification = distribution.Specification;
            Assert.AreEqual(valueProbabilityPairs.Length, specification.Count);
            foreach(ValueProbabilityPair expectedPair in valueProbabilityPairs)
            {
                ValueProbabilityPair pair = specification.Single(s => s.Value == expectedPair.Value);
                Assert.AreEqual(expectedPair.Probability, pair.Probability);
            }
        }

        [TestCase(0.0, 1)]
        [TestCase(0.123, 1)]
        [TestCase(0.5, 1)]
        [TestCase(0.5+1e-6, 2)]
        [TestCase(0.67, 2)]
        [TestCase(0.75, 2)]
        [TestCase(0.75+1e-6, 3)]
        [TestCase(0.7999, 3)]
        [TestCase(0.85, 3)]
        [TestCase(0.85+1e-6, 4)]
        [TestCase(0.953, 4)]
        [TestCase(1.0, 4)]
        public void GetValueAtCdf_ForWholeSpectrum_ReturnExpectedValuesAccoringToDistribution(double probability, int expectedValue)
        {
            // Setup
            var valueProbabilityPairs = new[]
            {
                new ValueProbabilityPair(1, 0.5),
                new ValueProbabilityPair(2, 0.25),
                new ValueProbabilityPair(3, 0.10),
                new ValueProbabilityPair(4, 0.15-1e-8),
            };
            var distribution = new DiscreteValueProbabilityDistribution(valueProbabilityPairs);

            // Call
            int sampleValue = distribution.GetValueAtCdf(probability);

            // Assert
            Assert.AreEqual(expectedValue, sampleValue);
        }
    }
}
