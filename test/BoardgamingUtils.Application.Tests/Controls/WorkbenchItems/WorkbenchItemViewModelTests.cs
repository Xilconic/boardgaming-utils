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
using System.ComponentModel;
using Xilconic.BoardgamingUtils.Application.Controls.WorkbenchItems;
using Xilconic.BoardgamingUtils.Mathematics;

namespace Xilconic.BoardgamingUtils.Application.Tests.Controls.WorkbenchItems;

public class WorkbenchItemViewModelTests
{
    [Fact]
    public void WhenConstructingReturnsInstanceInExpectedState()
    {
        const string name = "some name";
        const string title = "some title";
        const string valueName = "some value name";

        var viewModel = new SimpleWorkbenchItemViewModel(name, title, valueName);

        viewModel.Should().BeAssignableTo<INotifyPropertyChanged>();

        viewModel.Name.Should().Be(name);
        viewModel.Title.Should().Be(title);
        viewModel.ValueName.Should().Be(valueName);
    }

    [Fact]
    public void WhenRaisingNotifyPropertyChangedThenPropertyChangedEventRaisedForGivenProperty()
    {
        var viewModel = new SimpleWorkbenchItemViewModel("A", "B", "C");
        using IMonitor<SimpleWorkbenchItemViewModel>? monitoredSubject = viewModel.Monitor();

        const string expectedPropertyName = "A";

        viewModel.TestRaiseNotifyPropertyChanged(expectedPropertyName);

        monitoredSubject.Should().Raise(nameof(SingleDieWorkbenchItem.PropertyChanged))
            .WithSender(viewModel)
            .WithArgs<PropertyChangedEventArgs>(args => args.PropertyName == expectedPropertyName);
    }

    private sealed class SimpleWorkbenchItemViewModel : WorkbenchItemViewModel
    {
        public SimpleWorkbenchItemViewModel(string name, string title, string valueName)
            : base(name, title, valueName)
        {
        }

        public override DiscreteValueProbabilityDistribution Distribution => throw new NotImplementedException();

        public override WorkbenchItemViewModel DeepClone()
        {
            throw new NotImplementedException();
        }

        public void TestRaiseNotifyPropertyChanged(string propertyName)
        {
            NotifyPropertyChanged(propertyName);
        }
    }
}