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
using System;
using System.Globalization;
using System.Windows.Controls;

namespace Xilconic.BoardgamingUtils.App.ValidationRules
{
    /// <summary>
    /// Base class for parsing an <see cref="int"/> value.
    /// </summary>
    public abstract class IntegerParsingRule : ValidationRule
    {
        public string ParameterName { get; set; } = "Value";

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var numberOfDieFacesText = value as string;
            if (string.IsNullOrWhiteSpace(numberOfDieFacesText))
            {
                return new ValidationResult(false, $"{ParameterName} must be specified.");
            }

            try
            {
                int integer = int.Parse(numberOfDieFacesText, NumberStyles.Integer, cultureInfo);
                return ValidateInteger(integer, cultureInfo);
            }
            catch (FormatException)
            {
                return new ValidationResult(false, $"{ParameterName} must be a whole number.");
            }
            catch (OverflowException)
            {
                return new ValidationResult(false, $"{ParameterName} is too large or too small.");
            }
        }

        /// <summary>
        /// Validates the integer.
        /// </summary>
        /// <param name="value">The value to be validated.</param>
        /// <param name="cultureInfo">The <see cref="CultureInfo"/> corresponding to <paramref name="value"/>.</param>
        /// <returns>The <see cref="ValidationResult"/>.</returns>
        protected abstract ValidationResult ValidateInteger(int value, CultureInfo cultureInfo);
    }
}
