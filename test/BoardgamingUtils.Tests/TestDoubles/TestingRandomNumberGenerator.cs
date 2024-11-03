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
using Xilconic.BoardgamingUtils.PseudoRandom;

namespace Xilconic.BoardgamingUtils.Tests.TestDoubles;

/// <summary>
/// Implementation of <see cref="IRandomNumberGenerator"/> that allows for predictable number
/// generation, useful for unit testing.
/// </summary>
public class TestingRandomNumberGenerator : IRandomNumberGenerator
{
    private readonly Queue<double> _factorValues;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="TestingRandomNumberGenerator"/> class.
    /// </summary>
    public TestingRandomNumberGenerator()
    {
        _factorValues = new Queue<double>();
    }
    
    /// <summary>
    /// Adds a collection of factors (all in the [0.0, 1.0) range) that are expected to be used.
    /// </summary>
    /// <param name="factors">The collection of factors.</param>
    public void AddFactorValues(params double[] factors)
    {
        ArgumentNullException.ThrowIfNull(factors);

        foreach (double factor in factors)
        {
            if (factor is < 0.0 or >= 1.0)
            {
                throw new ArgumentException("Cannot define values outside the [0.0, 1.0) range.", nameof(factors));
            }
            
            _factorValues.Enqueue(factor);
        }
    }
    
    /// <summary>
    /// Returns the next value in the range of [0.0, 1.0].
    /// </summary>
    /// <returns>The factor.</returns>
    public double NextFactor()
    {
        if (_factorValues.Count != 0)
        {
            return _factorValues.Dequeue();
        }

        throw new InvalidOperationException($"Unexpected call to '{nameof(NextFactor)}()'. Did you add enough elements using '{nameof(AddFactorValues)}(IEnumerable<double>)'?");
    }
}