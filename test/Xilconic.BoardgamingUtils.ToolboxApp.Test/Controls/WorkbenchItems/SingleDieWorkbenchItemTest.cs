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
using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;
using Rhino.Mocks;
using Xilconic.BoardgamingUtils.PseudoRandom;
using Xilconic.BoardgamingUtils.ToolboxApp.Controls.WorkbenchItems;
using Xilconic.BoardgamingUtils.ToolboxApp.Test.TestingUtils;

namespace Xilconic.BoardgamingUtils.ToolboxApp.Test.Controls.WorkbenchItems
{
    [TestFixture]
    public class SingleDieWorkbenchItemTest
    {
        private IRandomNumberGenerator rngStub;

        [SetUp]
        public void SetUp()
        {
            rngStub = MockRepository.GenerateStub<IRandomNumberGenerator>();
        }

        [Test]
        public void Constructor_ExpectedValues()
        {
            // Call
            var workbenchItem = new SingleDieWorkbenchItem(rngStub);

            // Assert
            Assert.IsInstanceOf<WorkbenchItemViewModel>(workbenchItem);
            Assert.AreEqual("Single die", workbenchItem.Name);

            Assert.AreEqual(6, workbenchItem.NumberOfSides);
            Assert.AreEqual("Die Probabilities (pdf)", workbenchItem.Title);
            Assert.AreEqual("Die face", workbenchItem.ValueName);
            Assert.AreEqual(6, workbenchItem.Distribution.Specification.Count);
            CollectionAssert.AreEqual(Enumerable.Range(1, 6), workbenchItem.Distribution.Specification.Select(pair => pair.Value));
            CollectionAssert.AreEqual(Enumerable.Repeat(1.0 / 6, 6), workbenchItem.Distribution.Specification.Select(pair => pair.Probability));
        }

        [Test]
        public void NumberOfSides_SetNewValue_GetNewValue()
        {
            // Setup
            var workbenchItem = new SingleDieWorkbenchItem(rngStub);

            int newNumberOfSides = 4;

            var receivedEvents = new List<RecievedPropertyChangedEvent>();
            workbenchItem.PropertyChanged += (s, e) =>
            {
                receivedEvents.Add(new RecievedPropertyChangedEvent
                {
                    Sender = s,
                    PropertyName = e.PropertyName,
                    CallCount = 1,
                });
            };

            // Call
            workbenchItem.NumberOfSides = newNumberOfSides;

            // Assert
            Assert.AreEqual(newNumberOfSides, workbenchItem.NumberOfSides);
            Assert.AreEqual(newNumberOfSides, workbenchItem.Distribution.Specification.Count);
            CollectionAssert.AreEqual(Enumerable.Range(1, newNumberOfSides), workbenchItem.Distribution.Specification.Select(pair => pair.Value));
            CollectionAssert.AreEqual(Enumerable.Repeat(1.0 / newNumberOfSides, newNumberOfSides), workbenchItem.Distribution.Specification.Select(pair => pair.Probability));

            Assert.AreEqual(2, receivedEvents.Count);
            RecievedPropertyChangedEvent actualEvent = receivedEvents[0];
            Assert.AreEqual(workbenchItem, actualEvent.Sender);
            Assert.AreEqual(nameof(workbenchItem.NumberOfSides), actualEvent.PropertyName);
            Assert.AreEqual(1, actualEvent.CallCount);

            actualEvent = receivedEvents[1];
            Assert.AreEqual(workbenchItem, actualEvent.Sender);
            Assert.AreEqual(nameof(workbenchItem.Distribution), actualEvent.PropertyName);
            Assert.AreEqual(1, actualEvent.CallCount);
        }

        [Test]
        public void DeepClone_Always_ReturnsDeepCopy()
        {
            // Setup
            var workbenchItem = new SingleDieWorkbenchItem(rngStub)
            {
                NumberOfSides = 2,
            };

            // Call
            WorkbenchItemViewModel clone = workbenchItem.DeepClone();

            // Assert
            Assert.IsInstanceOf<SingleDieWorkbenchItem>(clone);
            var clonedSingleDieWorkbenchItem = (SingleDieWorkbenchItem)clone;
            Assert.AreNotSame(workbenchItem, clonedSingleDieWorkbenchItem);
            Assert.AreEqual(workbenchItem.Name, clonedSingleDieWorkbenchItem.Name);
            Assert.AreEqual(workbenchItem.ValueName, clonedSingleDieWorkbenchItem.ValueName);
            Assert.AreEqual(workbenchItem.Title, clonedSingleDieWorkbenchItem.Title);
            Assert.AreEqual(workbenchItem.Distribution.Specification.Count, clonedSingleDieWorkbenchItem.Distribution.Specification.Count);
            CollectionAssert.AreEqual(workbenchItem.Distribution.Specification.Select(pair => pair.Value), clonedSingleDieWorkbenchItem.Distribution.Specification.Select(pair => pair.Value));
            CollectionAssert.AreEqual(workbenchItem.Distribution.Specification.Select(pair => pair.Probability), clonedSingleDieWorkbenchItem.Distribution.Specification.Select(pair => pair.Probability));
        }
    }
}
