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
namespace Xilconic.BoardgamingUtils.Mathematics;

/// <summary>
/// Denotes a probability.
/// </summary>
public readonly record struct Probability : IComparable<Probability>, IComparable
{
    private const double MaxValue = 1.0;
    
    public static readonly Probability Zero = new(0);
    public static readonly Probability One = new(1);
    
    private readonly double _value;

    /// <param name="value">The probability value.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is <see cref="double.NaN"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="value"/> is not in range [0.0, 1.0].</exception>
    public Probability(double value)
    {
        if (double.IsNaN(value))
        {
            throw new ArgumentException("Probability cannot be 'NaN'.", nameof(value));
        }

        if(value is < 0.0 or > MaxValue)
        {
            throw new ArgumentOutOfRangeException(nameof(value), "Probability must be in range [0.0, 1.0].");            
        }
        
        _value = value;
    }
    
    /// <exception cref="ArgumentException">Thrown when adding the probabilities would exceed the [0.0, 1.0] range.</exception>
    public Probability Add(Probability other) => this + other;
    /// <exception cref="ArgumentException">Thrown when adding the probabilities would exceed the [0.0, 1.0] range.</exception>
    public static Probability operator +(Probability a, Probability b)
    {
        var intendedValue = a._value+ b._value;
        if (intendedValue > MaxValue)
        {
            throw new ArgumentException($"Adding these two probabilities would result in '{intendedValue}, which exceeds the max allowed value of '{MaxValue}'.");
        }
        
        return new Probability(intendedValue);
    }

    public int CompareTo(Probability other) => _value.CompareTo(other._value);
    public int CompareTo(object? obj)
    {
        if (obj is null)
        {
            return 1;
        }

        return obj is Probability other ? 
            CompareTo(other) : 
            throw new ArgumentException($"Object must be of type {nameof(Probability)}.", nameof(obj));
    }

    public static bool operator <(Probability left, Probability right) => left.CompareTo(right) < 0;
    public static bool operator >(Probability left, Probability right) => left.CompareTo(right) > 0;
    public static bool operator <=(Probability left, Probability right) => left.CompareTo(right) <= 0;
    public static bool operator >=(Probability left, Probability right) => left.CompareTo(right) >= 0;

    public bool Equals(Probability other, double margin) => Math.Abs(other._value - _value) <= margin;
}