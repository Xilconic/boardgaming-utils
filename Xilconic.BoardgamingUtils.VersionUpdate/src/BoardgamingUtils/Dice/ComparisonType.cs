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
namespace Xilconic.BoardgamingUtils.Dice;

/// <summary>
/// Denotes the type of comparison performed by <see cref="ThresholdCompare"/>.
/// </summary>
public enum ComparisonType
{
    /// <summary>
    /// Success = <see cref="IAbstractDie"/> result is greater than the reference value.
    /// </summary>
    Greater,

    /// <summary>
    /// Success = <see cref="IAbstractDie"/> result is greater than or equal to the reference value.
    /// </summary>
    GreaterOrEqual,

    /// <summary>
    /// Success = <see cref="IAbstractDie"/> result is less than the reference value.
    /// </summary>
    Smaller,

    /// <summary>
    /// Success = <see cref="IAbstractDie"/> result is less than or equal to the reference value.
    /// </summary>
    SmallerOrEqual,
}