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
using NUnit.Framework;
using System.Globalization;
using System.Windows.Controls;
using Xilconic.BoardgamingUtils.App.ValidationRules;

namespace Xilconic.BoardgamingUtils.App.Test.ValidationRules
{
    [TestFixture]
    public class NumberOfDieFacesRuleTest
    {
        [Test]
        public void Constructor_ExpectedValues()
        {
            // Call
            var validationRule = new NumberOfDieFacesRule();

            // Assert
            Assert.IsInstanceOf<IntegerParsingRule>(validationRule);

            Assert.AreEqual("Number of sides", validationRule.ParameterName);

            Assert.IsFalse(validationRule.ValidatesOnTargetUpdated);
            Assert.AreEqual(ValidationStep.RawProposedValue, validationRule.ValidationStep);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("    ")]
        public void Validate_ValueNullOrWhitespace_ReturnInvalid(string invalidValue)
        {
            // Setup
            var validationRule = new NumberOfDieFacesRule();

            // Call
            ValidationResult result = validationRule.Validate(invalidValue, CultureInfo.CurrentCulture);

            // Assert
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual("Number of sides must be specified.", result.ErrorContent);
        }

        [Test]
        public void Validate_ValueIsNotString_ReturnInvalid()
        {
            // Setup
            var validationRule = new NumberOfDieFacesRule();

            // Call
            ValidationResult result = validationRule.Validate(new object(), CultureInfo.CurrentCulture);

            // Assert
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual("Number of sides must be specified.", result.ErrorContent);
        }

        [TestCase(-1234346)]
        [TestCase(0)]
        public void Validate_InvalidNumberOfDieSides_ReturnInvalid(int invalidNumberOfSides)
        {
            // Setup
            var validationRule = new NumberOfDieFacesRule();

            var cultureInfo = CultureInfo.CurrentCulture;

            // Call
            ValidationResult result = validationRule.Validate(invalidNumberOfSides.ToString(cultureInfo), cultureInfo);

            // Assert
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual("Number of sides must be greater than zero.", result.ErrorContent);
        }

        [Test]
        public void Validate_NumberOfDieSidesNotIntegerString_ReturnInvalid()
        {
            // Setup
            var validationRule = new NumberOfDieFacesRule();

            // Call
            ValidationResult result = validationRule.Validate("A", CultureInfo.CurrentCulture);

            // Assert
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual("Number of sides must be a whole number.", result.ErrorContent);
        }

        [TestCase(int.MaxValue)]
        [TestCase(int.MinValue)]
        public void Validate_NumberOfDieSidesTooLarge_ReturnInvalid(int extremeValue)
        {
            // Setup
            var validationRule = new NumberOfDieFacesRule();

            CultureInfo cultureInfo = CultureInfo.CurrentCulture;
            string overflowingValue = extremeValue.ToString(cultureInfo) + "1";

            // Call
            ValidationResult result = validationRule.Validate(overflowingValue, cultureInfo);

            // Assert
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual("Number of sides is too large or too small.", result.ErrorContent);
        }

        [TestCase(1)]
        [TestCase(20)]
        [TestCase(int.MaxValue)]
        public void Validate_ValidNumberOfDieSides_ReturnValidResult(int value)
        {
            // Setup
            var validationRule = new NumberOfDieFacesRule();

            CultureInfo cultureInfo = CultureInfo.CurrentCulture;

            // Call
            ValidationResult result = validationRule.Validate(value.ToString(cultureInfo), cultureInfo);

            // Assert
            Assert.IsTrue(result.IsValid);
            Assert.IsNull(result.ErrorContent);
        }
    }
}