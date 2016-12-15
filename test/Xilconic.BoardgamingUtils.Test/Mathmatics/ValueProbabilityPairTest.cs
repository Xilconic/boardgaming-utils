using NUnit.Framework;
using Xilconic.BoardgamingUtils.Mathmatics;

namespace Xilconic.BoardgamingUtils.Test.Mathmatics
{
    [TestFixture]
    public class ValueProbabilityPairTest
    {
        [Combinatorial]
        public void Constructor_ValidArguments_ExpectedValue(
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
