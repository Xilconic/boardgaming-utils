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

using FluentAssertions;
using System.Globalization;
using System.Windows.Controls;
using Xilconic.BoardgamingUtils.Application.ValidationRules;

namespace Xilconic.BoardgamingUtils.Application.Tests.ValidationRules;

public class IntegerParsingRuleTest
{
    private readonly SimpleIntegerParsingRule _validationRule;

    public IntegerParsingRuleTest()
    {
        _validationRule = new SimpleIntegerParsingRule();
    }

    [Fact]
    public void WhenConstructingThenObjectHasExpectedState()
    {
        _validationRule.Should().BeAssignableTo<ValidationRule>();

        _validationRule.ValidatesOnTargetUpdated.Should().BeFalse();
        _validationRule.ValidationStep.Should().Be(ValidationStep.RawProposedValue);
        _validationRule.ParameterName.Should().Be("Value");
    }

    [Theory]
    [InlineData("A")]
    [InlineData("A parameter description text")]
    public void WhenSettingParameterNameThenParameterNameIsSet(string newParamName)
    {
        _validationRule.ParameterName = newParamName;

        _validationRule.ParameterName.Should().Be(newParamName);
    }

    [Theory]
    [InlineData("A", null)]
    [InlineData("param", "")]
    [InlineData("name", "    ")]
    public void GivenValueIsNullOrWhitespaceWhenValidatingThenReturnsInvalid(
        string paramName, 
        string invalidValue)
    {
        _validationRule.ParameterName = paramName;

        ValidationResult result = _validationRule.Validate(invalidValue, CultureInfo.CurrentCulture);

        result.IsValid.Should().BeFalse();
        result.ErrorContent.Should().Be($"{paramName} must be specified.");
    }

    [Theory]
    [InlineData("Test")]
    [InlineData("One two three")]
    public void GivenValueIsNotStringWhenValidatingThenReturnsInvalid(
        string paramName)
    {
        _validationRule.ParameterName = paramName;

        ValidationResult result = _validationRule.Validate(new object(), CultureInfo.CurrentCulture);

        result.IsValid.Should().BeFalse();
        result.ErrorContent.Should().Be($"{paramName} must be specified.");
    }

    [Theory]
    [InlineData("B")]
    [InlineData("Z")]
    public void GivenNumberOfDieSidesNotIntegerStringWhenValidatingThenReturnsInvalid(string paramName)
    {
        _validationRule.ParameterName = paramName;

        ValidationResult result = _validationRule.Validate("A", CultureInfo.CurrentCulture);

        result.IsValid.Should().BeFalse();
        result.ErrorContent.Should().Be($"{paramName} must be a whole number.");
    }
    
    [Theory]
    [InlineData("param", int.MaxValue)]
    [InlineData("value", int.MinValue)]
    public void GivenTooLargeValueWhenValidatingReturnsInvalid(
        string paramName,
        int extremeValue)
    {
        _validationRule.ParameterName = paramName;

        CultureInfo cultureInfo = CultureInfo.CurrentCulture;
        string overflowingValue = extremeValue.ToString(cultureInfo) + "1";

        ValidationResult result = _validationRule.Validate(overflowingValue, cultureInfo);

        result.IsValid.Should().BeFalse();
        result.ErrorContent.Should().Be($"{paramName} is too large or too small.");
    }

    [Theory]
    [InlineData(int.MaxValue)]
    [InlineData(20)]
    [InlineData(int.MinValue)]
    public void GivenParseableIntegerValueWhenValidatingReturnsValid(int validValue)
    {
        CultureInfo cultureInfo = CultureInfo.CurrentCulture;
        string someValue = validValue.ToString(cultureInfo);

        ValidationResult result = _validationRule.Validate(someValue, cultureInfo);

        result.IsValid.Should().BeTrue();
        _validationRule.TemplateMethodCalled.Should().BeTrue();
    }

    private sealed class SimpleIntegerParsingRule : IntegerParsingRule
    {
        public bool TemplateMethodCalled { get; private set; }

        protected override ValidationResult ValidateInteger(int value, CultureInfo cultureInfo)
        {
            TemplateMethodCalled = true;
            return ValidationResult.ValidResult;
        }
    }
}