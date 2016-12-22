using NUnit.Framework;
using Rhino.Mocks;
using System.Collections.ObjectModel;
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
            for(int i = 0; i < specification.Count; i++)
            {
                ValueProbabilityPair referencePair = specification[i];
                ValueProbabilityPair actualPair = distribution.Specification[i];
                Assert.AreEqual(referencePair.Probability, actualPair.Probability);
                Assert.AreEqual(referencePair.Value + modifierValue, actualPair.Value);
            }
        }
    }
}
