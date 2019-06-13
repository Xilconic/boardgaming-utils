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

using NUnit.Framework;
using Xilconic.BoardgamingUtils.Mathmatics;
using Xilconic.BoardgamingUtils.ToolboxApp.Controls.WorkbenchItems;

namespace Xilconic.BoardgamingUtils.App.Test
{
    [TestFixture]
    public class WorkbenchItemViewModelTests
    {
        [Test]
        public void Constructor_ExpectedValues()
        {
            // Setup
            string name = "some name";
            string title = "some title";
            string valueName = "some value name";

            // Call
            var viewModel = new SimpleWorkbenchItemViewModel(name, title, valueName);

            // Assert
            Assert.AreEqual(name, viewModel.Name);
            Assert.AreEqual(title, viewModel.Title);
            Assert.AreEqual(valueName, viewModel.ValueName);
        }

        private class SimpleWorkbenchItemViewModel : WorkbenchItemViewModel
        {
            public SimpleWorkbenchItemViewModel(string name, string title, string valueName)
                : base(name, title, valueName)
            {
            }

            public override DiscreteValueProbabilityDistribution Distribution
            {
                get
                {
                    return new DiscreteValueProbabilityDistribution(new[] { new ValueProbabilityPair(0, 1.0) });
                }
            }
        }
    }
}
