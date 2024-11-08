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

using System.Globalization;
using System.Windows.Controls;

namespace Xilconic.BoardgamingUtils.Application.ValidationRules;

/// <summary>
/// Defines a value range for the number of dice in a collection.
/// </summary>
public class PracticalNumberOfDiceLimitationRule : IntegerParsingRule
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PracticalNumberOfDiceLimitationRule"/> class.
    /// </summary>
    public PracticalNumberOfDiceLimitationRule()
    {
        ParameterName = "Number of dice";

        Minimum = 1;
        Maximum = 50;
    }

    /// <summary>
    /// Gets the minimum value that the number of faces has to be.
    /// </summary>
    public int Minimum { get; }

    /// <summary>
    /// Gets the maximum value that the number of faces can be.
    /// </summary>
    public int Maximum { get; }

    /// <inheritdoc/>
    protected override ValidationResult ValidateInteger(int value, CultureInfo cultureInfo)
    {
        if (value < Minimum || value > Maximum)
        {
            return new ValidationResult(false, $"{ParameterName} must be in range of [{Minimum}, {Maximum}].");
        }

        return ValidationResult.ValidResult;
    }
}