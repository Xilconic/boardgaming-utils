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

using NUnit.Framework;
using Xilconic.BoardgamingUtils.ToolboxApp.ValidationRules;

namespace Xilconic.BoardgamingUtils.ToolboxApp.Test.ValidationRules
{
    [TestFixture]
    public class PracticalNumberOfDieFacesLimitationRuleTest
    {
        [Test]
        public void Constructor_ExpectedValues()
        {
            // Call
            var validationRule = new PracticalNumberOfDieFacesLimitationRule();

            // Assert
            Assert.IsInstanceOf<IntegerParsingRule>(validationRule);

            Assert.IsFalse(validationRule.ValidatesOnTargetUpdated);
            Assert.AreEqual(ValidationStep.RawProposedValue, validationRule.ValidationStep);

            Assert.AreEqual("Number of sides", validationRule.ParameterName);

            Assert.AreEqual(1, validationRule.Minimum);
            Assert.AreEqual(200, validationRule.Maximum);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("    ")]
        public void Validate_ValueNullOrWhitespace_ReturnInvalid(string invalidValue)
        {
            // Setup
            var validationRule = new PracticalNumberOfDieFacesLimitationRule();

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
            var validationRule = new PracticalNumberOfDieFacesLimitationRule();

            // Call
            ValidationResult result = validationRule.Validate(new object(), CultureInfo.CurrentCulture);

            // Assert
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual("Number of sides must be specified.", result.ErrorContent);
        }

        [TestCase(-1234346)]
        [TestCase(0)]
        [TestCase(201)]
        [TestCase(2346895)]
        public void Validate_InvalidNumberOfDieSides_ReturnInvalid(int invalidNumberOfSides)
        {
            // Setup
            var validationRule = new PracticalNumberOfDieFacesLimitationRule();

            var cultureInfo = CultureInfo.CurrentCulture;

            // Call
            ValidationResult result = validationRule.Validate(invalidNumberOfSides.ToString(cultureInfo), cultureInfo);

            // Assert
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual("Number of sides must be in range of [1, 200].", result.ErrorContent);
        }

        [Test]
        public void Validate_NumberOfDieSidesNotIntegerString_ReturnInvalid()
        {
            // Setup
            var validationRule = new PracticalNumberOfDieFacesLimitationRule();

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
            var validationRule = new PracticalNumberOfDieFacesLimitationRule();

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
        [TestCase(200)]
        public void Validate_ValidNumberOfDieSides_ReturnValidResult(int value)
        {
            // Setup
            var validationRule = new PracticalNumberOfDieFacesLimitationRule();

            CultureInfo cultureInfo = CultureInfo.CurrentCulture;

            // Call
            ValidationResult result = validationRule.Validate(value.ToString(cultureInfo), cultureInfo);

            // Assert
            Assert.IsTrue(result.IsValid);
            Assert.IsNull(result.ErrorContent);
        }
    }
}
