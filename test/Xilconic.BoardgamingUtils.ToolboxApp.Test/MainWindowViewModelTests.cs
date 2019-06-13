﻿// This file is part of Boardgaming Utils.
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

namespace Xilconic.BoardgamingUtils.App.Test
{
    [TestFixture]
    public class MainWindowViewModelTests
    {
        [Test]
        public void Constructor_ExpectedValues()
        {
            // Call
            var viewModel = new MainWindowViewModel();

            // Assert
            Assert.AreEqual(1, viewModel.WorkbenchItems.Count);
            Assert.AreEqual("Die", viewModel.WorkbenchItems[0].Name);
        }
    }
}