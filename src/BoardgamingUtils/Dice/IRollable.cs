﻿// Copyright (c) Bas des Bouvrie ("Xilconic"). All rights reserved.
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
/// Interface for an object that can be rolled for a result.
/// </summary>
/// <typeparam name="T">The rolled result type.</typeparam>
public interface IRollable<out T>
    where T : struct
{
    /// <summary>
    /// Rolls for a randomly generated result value.
    /// </summary>
    /// <returns>The rolled result.</returns>
    T Roll();
}