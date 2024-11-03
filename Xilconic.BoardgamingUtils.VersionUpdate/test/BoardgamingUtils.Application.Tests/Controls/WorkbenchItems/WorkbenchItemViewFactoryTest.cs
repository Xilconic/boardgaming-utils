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
using Moq;
using Xilconic.BoardgamingUtils.Application.Controls.WorkbenchItems;
using Xilconic.BoardgamingUtils.Mathematics;
using Xilconic.BoardgamingUtils.PseudoRandom;
using Xilconic.BoardgamingUtils.ToolboxApp.Controls.WorkbenchItems;

namespace Xilconic.BoardgamingUtils.Application.Tests.Controls.WorkbenchItems;

public class WorkbenchItemViewFactoryTest
{
    private readonly IRandomNumberGenerator _rngStub = Mock.Of<IRandomNumberGenerator>();

    [StaFact]
    public void GivenSingleDieWorkbenchItemWhenCreatingViewReturnsSingleDieViewItem()
    {
        var workbenchItem = new SingleDieWorkbenchItem(_rngStub);

        WorkbenchItemUserControl control = WorkbenchItemViewFactory.CreateView(workbenchItem);

        var singleDieViewItem = control.Should().BeOfType<SingleDieViewItem>().Which;
        singleDieViewItem.SingleDieWorkbenchItem.Should().Be(workbenchItem);
    }

    [StaFact]
    public void GivenUnsupportedWorkbenchViewModelWhenCreatingViewThrowNotImplementedException()
    {
        Func<WorkbenchItemUserControl> call = () => WorkbenchItemViewFactory.CreateView(new SomeUnsupportedWorkbenchItem());

        call.Should().Throw<NotImplementedException>();
    }

    private sealed class SomeUnsupportedWorkbenchItem : WorkbenchItemViewModel
    {
        public SomeUnsupportedWorkbenchItem()
            : base("1", "2", "3")
        {
        }

        public override DiscreteValueProbabilityDistribution Distribution => 
            new([new ValueProbabilityPair(0, new Probability(1.0))]);

        public override WorkbenchItemViewModel DeepClone()
        {
            throw new NotImplementedException();
        }
    }
}