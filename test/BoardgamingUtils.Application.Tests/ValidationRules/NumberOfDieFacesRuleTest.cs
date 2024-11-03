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

public class NumberOfDieFacesRuleTest
{
    private readonly CultureInfo _currentCulture = CultureInfo.CurrentCulture;
    private readonly NumberOfDieFacesRule _validationRule = new();

    [Fact]
    public void WhenConstructingReturnsObjectInExpectedState()
    {
        _validationRule.Should().BeAssignableTo<IntegerParsingRule>();

        _validationRule.ParameterName.Should().Be("Number of sides");
        _validationRule.ValidatesOnTargetUpdated.Should().BeFalse();
        _validationRule.ValidationStep.Should().Be(ValidationStep.RawProposedValue);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("    ")]
    public void GivenValueNullOrWhitespaceWhenValidatingThenReturnsInvalid(string invalidValue)
    {
        ValidationResult result = _validationRule.Validate(invalidValue, _currentCulture);

        result.IsValid.Should().BeFalse();
        result.ErrorContent.Should().Be("Number of sides must be specified.");
    }

    [Fact]
    public void GivenValueIsNotStringWhenValidatingThenReturnsInvalid()
    {
        ValidationResult result = _validationRule.Validate(new object(), _currentCulture);

        result.IsValid.Should().BeFalse();
        result.ErrorContent.Should().Be("Number of sides must be specified.");
    }

    [Theory]
    [InlineData(-1234346)]
    [InlineData(0)]
    public void GivenInvalidNumberOfDieSidesWhenValidatingThenReturnsInvalid(
        int invalidNumberOfSides)
    {
        ValidationResult result = _validationRule.Validate(invalidNumberOfSides.ToString(_currentCulture), _currentCulture);

        result.IsValid.Should().BeFalse();
        result.ErrorContent.Should().Be("Number of sides must be greater than zero.");
    }

    [Fact]
    public void GivenNumberOfDieSidesNotIntegerStringWhenValidatingThenReturnsInvalid()
    {
        ValidationResult result = _validationRule.Validate("A", _currentCulture);

        result.IsValid.Should().BeFalse();
        result.ErrorContent.Should().Be("Number of sides must be a whole number.");
    }

    [Theory]
    [InlineData(int.MaxValue)]
    [InlineData(int.MinValue)]
    public void GivenNumberOfDieSidesTooLargeWhenValidatingThenReturnsInvalid(
        int extremeValue)
    {
        string overflowingValue = extremeValue.ToString(_currentCulture) + "1";

        ValidationResult result = _validationRule.Validate(overflowingValue, _currentCulture);

        result.IsValid.Should().BeFalse();
        result.ErrorContent.Should().Be("Number of sides is too large or too small.");
    }

    [Theory]
    [InlineData(1)]
    [InlineData(20)]
    [InlineData(int.MaxValue)]
    public void GivenValidNumberOfDieSidesWhenValidatingThenReturnsValidResult(int value)
    {
        ValidationResult result = _validationRule.Validate(value.ToString(_currentCulture), _currentCulture);

        result.IsValid.Should().BeTrue();
        result.ErrorContent.Should().BeNull();
    }
}