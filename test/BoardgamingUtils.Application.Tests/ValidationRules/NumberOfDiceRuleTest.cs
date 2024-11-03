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

public class NumberOfDiceRuleTest
{
    private readonly NumberOfDiceRule _validationRule = new();
    private readonly CultureInfo _cultureInfo = CultureInfo.CurrentCulture;

    [Fact]
    public void WhenConstructingInstanceHasExpectedState()
    {
        _validationRule.Should().BeAssignableTo<IntegerParsingRule>();

        _validationRule.ParameterName.Should().Be("Number of dice");
        _validationRule.ValidatesOnTargetUpdated.Should().BeFalse();
        _validationRule.ValidationStep.Should().Be(ValidationStep.RawProposedValue);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("    ")]
    public void GivenValueNullOrWhitespaceWhenValidatingThenReturnsInvalid(string invalidValue)
    {
        ValidationResult result = _validationRule.Validate(invalidValue, _cultureInfo);

        result.IsValid.Should().BeFalse();
        result.ErrorContent.Should().Be("Number of dice must be specified.");
    }

    [Fact]
    public void GivenValueIsNotStringWhenValidatingThenReturnsInvalid()
    {
        ValidationResult result = _validationRule.Validate(new object(), _cultureInfo);

        result.IsValid.Should().BeFalse();
        result.ErrorContent.Should().Be("Number of dice must be specified.");
    }

    [Theory]
    [InlineData(-1234346)]
    [InlineData(0)]
    public void GivenInvalidNumberOfDieSidesWhenValidatingThenReturnsInvalid(
        int invalidNumberOfSides)
    {
        ValidationResult result = _validationRule.Validate(invalidNumberOfSides.ToString(_cultureInfo), _cultureInfo);

        result.IsValid.Should().BeFalse();
        result.ErrorContent.Should().Be("Number of dice must be greater than zero.");
    }

    [Fact]
    public void GivenNumberOfDieSidesNotIntegerStringWhenValidatingThenReturnsInvalid()
    {
        ValidationResult result = _validationRule.Validate("A", CultureInfo.CurrentCulture);

        result.IsValid.Should().BeFalse();
        result.ErrorContent.Should().Be("Number of dice must be a whole number.");
    }

    [Theory]
    [InlineData(int.MaxValue)]
    [InlineData(int.MinValue)]
    public void GivenNumberOfDieSidesTooLargeWhenValidatingThenReturnsInvalid(
        int extremeValue)
    {
        string overflowingValue = extremeValue.ToString(_cultureInfo) + "1";

        ValidationResult result = _validationRule.Validate(overflowingValue, _cultureInfo);

        result.IsValid.Should().BeFalse();
        result.ErrorContent.Should().Be("Number of dice is too large or too small.");
    }

    [Theory]
    [InlineData(1)]
    [InlineData(20)]
    [InlineData(int.MaxValue)]
    public void GivenValidNumberOfDieSidesWhenValidatingThenReturnsValidResult(int value)
    {
        ValidationResult result = _validationRule.Validate(value.ToString(_cultureInfo), _cultureInfo);

        result.IsValid.Should().BeTrue();
        result.ErrorContent.Should().BeNull();
    }
}