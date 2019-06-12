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
namespace Xilconic.BoardgamingUtils.ToolboxApp.ValidationRules
{
    using System.Globalization;
    using System.Windows.Controls;

    /// <summary>
    /// Validation rule for the number of dice going into a collection.
    /// </summary>
    public class NumberOfDiceRule : IntegerParsingRule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NumberOfDiceRule"/> class.
        /// </summary>
        public NumberOfDiceRule()
        {
            ParameterName = "Number of dice";
        }

        /// <inheritdoc/>
        protected override ValidationResult ValidateInteger(int numberOfDice, CultureInfo cultureInfo)
        {
            if (numberOfDice <= 0)
            {
                return new ValidationResult(false, $"{ParameterName} must be greater than zero.");
            }

            return ValidationResult.ValidResult;
        }
    }
}
