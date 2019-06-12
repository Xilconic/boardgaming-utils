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
using System.Globalization;
using System.Windows.Controls;

using Xilconic.BoardgamingUtils.Dice;

namespace Xilconic.BoardgamingUtils.ToolboxApp.ValidationRules
{
    /// <summary>
    /// Validation rule for <see cref="NumericalDie.NumberOfSides"/>.
    /// </summary>
    public class NumberOfDieFacesRule : IntegerParsingRule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NumberOfDieFacesRule"/> class.
        /// </summary>
        public NumberOfDieFacesRule()
        {
            ParameterName = "Number of sides";
        }

        /// <inheritdoc/>
        protected override ValidationResult ValidateInteger(int numberOfDieFaces, CultureInfo cultureInfo)
        {
            if (numberOfDieFaces <= 0)
            {
                return new ValidationResult(false, $"{ParameterName} must be greater than zero.");
            }

            return ValidationResult.ValidResult;
        }
    }
}
