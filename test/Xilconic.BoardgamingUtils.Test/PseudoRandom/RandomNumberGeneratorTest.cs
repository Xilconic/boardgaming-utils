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
