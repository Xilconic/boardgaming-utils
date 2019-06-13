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
using Xilconic.BoardgamingUtils.ToolboxApp.Controls.WorkbenchItems;

namespace Xilconic.BoardgamingUtils.ToolboxApp.Test.Controls.WorkbenchItems
{
    [TestFixture]
    public class SingleDieWorkbenchItemTest
    {
        [Test]
        public void Constructor_ExpectedValues()
        {
            // Call
            var workbenchItem = new SingleDieWorkbenchItem();

            // Assert
            Assert.IsInstanceOf<WorkbenchItemViewModel>(workbenchItem);
            Assert.AreEqual("Single die", workbenchItem.Name);

            Assert.AreEqual(6, workbenchItem.NumberOfSides);
        }

        [Test]
        public void NumberOfSides_SetNewValue_GetNewValue()
        {
            // Setup
            var workbenchItem = new SingleDieWorkbenchItem();

            int newNumberOfSides = 3;

            // Call
            workbenchItem.NumberOfSides = newNumberOfSides;

            // Assert
            Assert.AreEqual(newNumberOfSides, workbenchItem.NumberOfSides);
        }
    }
}
