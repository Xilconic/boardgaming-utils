using System;

namespace Xilconic.BoardgamingUtils.PseudoRandom
{
    /// <summary>
    /// Class responsible for generating (pseudo)random numbers.
    /// </summary>
    public class RandomNumberGenerator
    {
        private readonly Random random;

        /// <summary>
        /// Creates a new instance of <see cref="RandomNumberGenerator"/>, initialized with a given
        /// seed.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public RandomNumberGenerator(int seed)
        {
            random = new Random(seed);
        }

        public double NextFactor()
        {
            return random.NextDouble();
        }
    }
}
