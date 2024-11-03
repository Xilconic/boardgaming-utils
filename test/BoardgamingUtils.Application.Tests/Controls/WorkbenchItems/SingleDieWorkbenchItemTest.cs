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
using Xilconic.BoardgamingUtils.Application.Controls.WorkbenchItems;
using Xilconic.BoardgamingUtils.Mathematics;
using Xilconic.BoardgamingUtils.PseudoRandom;

namespace Xilconic.BoardgamingUtils.Application.Tests.Controls.WorkbenchItems;

public class SingleDieWorkbenchItemTest
{
    private readonly IRandomNumberGenerator _rngStub = Mock.Of<IRandomNumberGenerator>();
    private readonly SingleDieWorkbenchItem _workbenchItem;

    public SingleDieWorkbenchItemTest()
    {
        _workbenchItem = new SingleDieWorkbenchItem(_rngStub);
    }

    [Fact]
    public void WhenConstructingReturnsInstanceInExpectedState()
    {
        _workbenchItem.Should().BeAssignableTo<WorkbenchItemViewModel>();
        _workbenchItem.Name.Should().Be("Single die");

        _workbenchItem.NumberOfSides.Should().Be(6);
        _workbenchItem.Title.Should().Be("Die Probabilities (pdf)");
        _workbenchItem.ValueName.Should().Be("Die face");
        
        var expectedProbability = new Probability(1.0 / 6.0);
        var expectedSpecification = new ValueProbabilityPair[]{
            new(1, expectedProbability),
            new(2, expectedProbability),
            new(3, expectedProbability),
            new(4, expectedProbability),
            new(5, expectedProbability),
            new(6, expectedProbability),
        };
        _workbenchItem.Distribution.Specification.Should().BeEquivalentTo(expectedSpecification);
    }

    [Fact]
    public void WhenSettingNewNumberOfSidesThenDistributionHasBeenUpdated()
    {
        int newNumberOfSides = 4;

        using IMonitor<SingleDieWorkbenchItem>? monitoredSubject = _workbenchItem.Monitor();

        _workbenchItem.NumberOfSides = newNumberOfSides;

        _workbenchItem.NumberOfSides.Should().Be(newNumberOfSides);
        
        var expectedProbability = new Probability(1.0 / 4.0);
        var expectedSpecification = new ValueProbabilityPair[]{
            new(1, expectedProbability),
            new(2, expectedProbability),
            new(3, expectedProbability),
            new(4, expectedProbability),
        };
        _workbenchItem.Distribution.Specification.Should().BeEquivalentTo(expectedSpecification);

        monitoredSubject.Should().Raise(nameof(SingleDieWorkbenchItem.PropertyChanged))
            .WithSender(_workbenchItem)
            .WithArgs<PropertyChangedEventArgs>(args => args.PropertyName == nameof(SingleDieWorkbenchItem.NumberOfSides));

        monitoredSubject.Should().Raise(nameof(SingleDieWorkbenchItem.PropertyChanged))
            .WithSender(_workbenchItem)
            .WithArgs<PropertyChangedEventArgs>(args => args.PropertyName == nameof(SingleDieWorkbenchItem.Distribution));
    }

    [Fact]
    public void WhenDeepCloningReturnsDeepCopy()
    {
        _workbenchItem.NumberOfSides = 2;

        WorkbenchItemViewModel clone = _workbenchItem.DeepClone();

        var clonedWorkbenchItem = clone.Should().BeOfType<SingleDieWorkbenchItem>().Which;
        clonedWorkbenchItem.Should().NotBeSameAs(_workbenchItem);
        clonedWorkbenchItem.Name.Should().Be(_workbenchItem.Name);
        clonedWorkbenchItem.ValueName.Should().Be(_workbenchItem.ValueName);
        clonedWorkbenchItem.Title.Should().Be(_workbenchItem.Title);
        clonedWorkbenchItem.Distribution.Specification.Should().BeEquivalentTo(_workbenchItem.Distribution.Specification);
    }
}