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
using FluentAssertions.Events;
using Moq;
using System.ComponentModel;
using Xilconic.BoardgamingUtils.Application.Controls;
using Xilconic.BoardgamingUtils.Dice;
using Xilconic.BoardgamingUtils.Mathematics;
using Xilconic.BoardgamingUtils.PseudoRandom;

namespace Xilconic.BoardgamingUtils.Application.Tests.Controls;

public class SumOfNumericalDiceViewModelTest
{
    private readonly SumOfNumericalDiceViewModel _viewModel = new();

    [Fact]
    public void WhenConstructingReturnsInstanceInExpectedState()
    {
        _viewModel.Should().BeAssignableTo<INotifyPropertyChanged>();
        _viewModel.NumberOfDice.Should().Be(2);
        _viewModel.NumberOfSides.Should().Be(6);
        AssertExpectedDistribution(
            _viewModel.NumberOfDice, 
            _viewModel.NumberOfSides, 
            _viewModel.Distribution);
    }

    [Fact]
    public void WhenSettingNewNumberOfSidesThenNotifyPropertyChange()
    {
        using IMonitor<SumOfNumericalDiceViewModel>? monitoredSubject = _viewModel.Monitor();
        
        _viewModel.NumberOfSides = 3;

        monitoredSubject.Should().Raise(nameof(SumOfNumericalDiceViewModel.PropertyChanged))
            .WithSender(_viewModel)
            .WithArgs<PropertyChangedEventArgs>(args => args.PropertyName == nameof(SumOfNumericalDiceViewModel.NumberOfSides));
    }

    [Fact]
    public void WhenSettingNewNumberOfSidesThenUpdatesDistributionAndNotifyPropertyChange()
    {
        using IMonitor<SumOfNumericalDiceViewModel>? monitoredSubject = _viewModel.Monitor();
        
        _viewModel.NumberOfSides = 3;
        
        monitoredSubject.Should().Raise(nameof(SumOfNumericalDiceViewModel.PropertyChanged))
            .WithSender(_viewModel)
            .WithArgs<PropertyChangedEventArgs>(args => args.PropertyName == nameof(SumOfNumericalDiceViewModel.Distribution));
        AssertExpectedDistribution(_viewModel.NumberOfDice, _viewModel.NumberOfSides, _viewModel.Distribution);
    }

    [Fact]
    public void WhenSettingNewNumberOfDiceThenRaiseNotifyPropertyChange()
    {
        using IMonitor<SumOfNumericalDiceViewModel>? monitoredSubject = _viewModel.Monitor();

        _viewModel.NumberOfDice = 3;

        monitoredSubject.Should().Raise(nameof(SumOfNumericalDiceViewModel.PropertyChanged))
            .WithSender(_viewModel)
            .WithArgs<PropertyChangedEventArgs>(args => args.PropertyName == nameof(SumOfNumericalDiceViewModel.NumberOfDice));
    }

    [Fact]
    public void WhenSettingNewNumberOfDiceThenUpdatesDistributionAndNotifyPropertyChange()
    {
        using IMonitor<SumOfNumericalDiceViewModel>? monitoredSubject = _viewModel.Monitor();

        _viewModel.NumberOfDice = 3;

        monitoredSubject.Should().Raise(nameof(SumOfNumericalDiceViewModel.PropertyChanged))
            .WithSender(_viewModel)
            .WithArgs<PropertyChangedEventArgs>(args => args.PropertyName == nameof(SumOfNumericalDiceViewModel.Distribution));
        AssertExpectedDistribution(_viewModel.NumberOfDice, _viewModel.NumberOfSides, _viewModel.Distribution);
    }

    private static void AssertExpectedDistribution(
        int numberOfDice,
        int numberOfSides,
        DiscreteValueProbabilityDistribution actualDistribution)
    {
        var rng = Mock.Of<IRandomNumberGenerator>();
        var dice = Enumerable.Repeat(numberOfSides, numberOfDice)
            .Select(nrOfSides => new NumericalDie(nrOfSides, rng))
            .ToArray();
        var sumOfDice = new SumOfDice(dice, rng);

        DiscreteValueProbabilityDistribution expectedDistribution = sumOfDice.ProbabilityDistribution;
        AssertEquals(expectedDistribution, actualDistribution);
    }

    private static void AssertEquals(
        DiscreteValueProbabilityDistribution expectedDistribution,
        DiscreteValueProbabilityDistribution actualDistribution)
    {
        actualDistribution.Specification.Should().HaveCount(expectedDistribution.Specification.Count);
        for (int i = 0; i < expectedDistribution.Specification.Count; i++)
        {
            actualDistribution.Specification[i].Should().BeEquivalentTo(expectedDistribution.Specification[i],
                "Because item at index {0} should have value {1} and actually is {2}",
                i, expectedDistribution.Specification[i], actualDistribution.Specification[i]);
        }
    }
}