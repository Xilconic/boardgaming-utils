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
namespace Xilconic.BoardgamingUtils.ToolboxApp.Test.ValidationRules
{
    using System.Globalization;
    using System.Windows.Controls;
    using NUnit.Framework;
    using Xilconic.BoardgamingUtils.ToolboxApp.ValidationRules;

    [TestFixture]
    public class IntegerParsingRuleTest
    {
        [Test]
        public void Constructor_ExpectedValues()
        {
            // Setup
            var validationRule = new SimpleIntegerParsingRule();

            // Assert
            Assert.IsInstanceOf<ValidationRule>(validationRule);

            Assert.IsFalse(validationRule.ValidatesOnTargetUpdated);
            Assert.AreEqual(ValidationStep.RawProposedValue, validationRule.ValidationStep);

            Assert.AreEqual("Value", validationRule.ParameterName);
        }

        [TestCase("A")]
        [TestCase("A parameter description text")]
        public void ParameterName_SetNewValue_GetNewlySetValue(string newParamName)
        {
            // Setup
            var validationRule = new SimpleIntegerParsingRule();

            // Call
            validationRule.ParameterName = newParamName;

            // Assert
            Assert.AreEqual(newParamName, validationRule.ParameterName);
        }

        [TestCase("A", null)]
        [TestCase("param", "")]
        [TestCase("name", "    ")]
        public void Validate_ValueNullOrWhitespace_ReturnInvalid(string paramName, string invalidValue)
        {
            // Setup
            var validationRule = new SimpleIntegerParsingRule
            {
                ParameterName = paramName,
            };

            // Call
            ValidationResult result = validationRule.Validate(invalidValue, CultureInfo.CurrentCulture);

            // Assert
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual($"{paramName} must be specified.", result.ErrorContent);
        }

        [TestCase("Test")]
        [TestCase("One two three")]
        public void Validate_ValueIsNotString_ReturnInvalid(string paramName)
        {
            // Setup
            var validationRule = new SimpleIntegerParsingRule
            {
                ParameterName = paramName,
            };

            // Call
            ValidationResult result = validationRule.Validate(new object(), CultureInfo.CurrentCulture);

            // Assert
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual($"{paramName} must be specified.", result.ErrorContent);
        }

        [TestCase("B")]
        [TestCase("Z")]
        public void Validate_NumberOfDieSidesNotIntegerString_ReturnInvalid(string paramName)
        {
            // Setup
            var validationRule = new SimpleIntegerParsingRule
            {
                ParameterName = paramName,
            };

            // Call
            ValidationResult result = validationRule.Validate("A", CultureInfo.CurrentCulture);

            // Assert
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual($"{paramName} must be a whole number.", result.ErrorContent);
        }

        [TestCase("param", int.MaxValue)]
        [TestCase("value", int.MinValue)]
        public void Validate_NumberOfDieSidesTooLarge_ReturnInvalid(string paramName, int extremeValue)
        {
            // Setup
            var validationRule = new SimpleIntegerParsingRule
            {
                ParameterName = paramName,
            };

            CultureInfo cultureInfo = CultureInfo.CurrentCulture;
            string overflowingValue = extremeValue.ToString(cultureInfo) + "1";

            // Call
            ValidationResult result = validationRule.Validate(overflowingValue, cultureInfo);

            // Assert
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual($"{paramName} is too large or too small.", result.ErrorContent);
        }

        [TestCase(int.MaxValue)]
        [TestCase(20)]
        [TestCase(int.MinValue)]
        public void Validate_NumberOfDieSidesTooLarge_ReturnInvalid(int extremeValue)
        {
            // Setup
            var validationRule = new SimpleIntegerParsingRule();

            CultureInfo cultureInfo = CultureInfo.CurrentCulture;
            string overflowingValue = extremeValue.ToString(cultureInfo);

            // Call
            ValidationResult result = validationRule.Validate(overflowingValue, cultureInfo);

            // Assert
            Assert.IsTrue(result.IsValid);
            Assert.IsTrue(validationRule.TemplateMethodCalled);
        }

        private class SimpleIntegerParsingRule : IntegerParsingRule
        {
            public bool TemplateMethodCalled { get; private set; }

            protected override ValidationResult ValidateInteger(int value, CultureInfo cultureInfo)
            {
                TemplateMethodCalled = true;
                return ValidationResult.ValidResult;
            }
        }
    }
}
