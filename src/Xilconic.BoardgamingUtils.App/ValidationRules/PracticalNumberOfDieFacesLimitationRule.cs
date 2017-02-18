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

namespace Xilconic.BoardgamingUtils.App.ValidationRules
{
    /// <summary>
    /// Defines a value range for the number of faces for an <see cref="NumericalDie"/>.
    /// </summary>
    public class PracticalNumberOfDieFacesLimitationRule : IntegerParsingRule
    {
        /// <summary>
        /// Initializes a new instance of <see cref="PracticalNumberOfDieFacesLimitationRule"/>.
        /// </summary>
        public PracticalNumberOfDieFacesLimitationRule()
        {
            ParameterName = "Number of sides";

            Minimum = 1;
            Maximum = 200;
        }

        /// <summary>
        /// The minimum value that the number of faces has to be.
        /// </summary>
        public int Minimum { get; private set; }

        /// <summary>
        /// The maximum value that the number of faces can be.
        /// </summary>
        public int Maximum { get; private set; }
        

        protected override ValidationResult ValidateInteger(int numberOfDieFaces, CultureInfo cultureInfo)
        {
            if (numberOfDieFaces < Minimum || numberOfDieFaces > Maximum)
            {
                return new ValidationResult(false, $"{ParameterName} must be in range of [{Minimum}, {Maximum}].");
            }
            return ValidationResult.ValidResult;
        }
    }
}
