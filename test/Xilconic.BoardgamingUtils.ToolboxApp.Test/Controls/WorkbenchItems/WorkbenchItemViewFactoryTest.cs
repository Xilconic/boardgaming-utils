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
using System;
using System.Threading;

using NUnit.Framework;
using Rhino.Mocks;
using Xilconic.BoardgamingUtils.Mathmatics;
using Xilconic.BoardgamingUtils.PseudoRandom;
using Xilconic.BoardgamingUtils.ToolboxApp.Controls.WorkbenchItems;

namespace Xilconic.BoardgamingUtils.ToolboxApp.Test.Controls.WorkbenchItems
{
    [Apartment(ApartmentState.STA)]
    [TestFixture]
    public class WorkbenchItemViewFactoryTest
    {
        private IRandomNumberGenerator rngStub;

        [SetUp]
        public void SetUp()
        {
            rngStub = MockRepository.GenerateStub<IRandomNumberGenerator>();
        }

        [Test]
        public void CreateView_ForSingleDieWorkbenchItem_SingleDieViewItemCreated()
        {
            // Setup
            var workbenchItem = new SingleDieWorkbenchItem(rngStub);

            // Call
            WorkbenchItemUserControl control = WorkbenchItemViewFactory.CreateView(workbenchItem);

            // Assert
            Assert.IsInstanceOf<SingleDieViewItem>(control);
            var singleDieViewItem = (SingleDieViewItem)control;
            Assert.AreSame(workbenchItem, singleDieViewItem.SingleDieWorkbenchItem);
        }

        [Test]
        public void CreateView_ForUnsupportedWorkbenchViewModel_ThrowNotImplementedException()
        {
            // Call
            TestDelegate call = () => WorkbenchItemViewFactory.CreateView(new SomeUnsupportedWorkbenchItem());

            // Assert
            Assert.Throws<NotImplementedException>(call);
        }

        private class SomeUnsupportedWorkbenchItem : WorkbenchItemViewModel
        {
            public SomeUnsupportedWorkbenchItem()
                : base("1", "2", "3")
            {
            }

            public override DiscreteValueProbabilityDistribution Distribution
            {
                get
                {
                    return new DiscreteValueProbabilityDistribution(new[] { new ValueProbabilityPair(0, 1.0) });
                }
            }

            public override WorkbenchItemViewModel DeepClone()
            {
                throw new NotImplementedException();
            }
        }
    }
}
