﻿// Copyright (c) Bas des Bouvrie ("Xilconic"). All rights reserved.
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
using System.ComponentModel;
using System.Linq;

using NUnit.Framework;
using Rhino.Mocks;
using Xilconic.BoardgamingUtils.Dice;
using Xilconic.BoardgamingUtils.Mathmatics;
using Xilconic.BoardgamingUtils.PseudoRandom;
using Xilconic.BoardgamingUtils.ToolboxApp.Controls;

namespace Xilconic.BoardgamingUtils.ToolboxApp.Test.Controls
{
    [TestFixture]
    public class SumOfNumericalDiceViewModelTest
    {
        [Test]
        public void Constructor_ExpectedValues()
        {
            // Call
            var viewModel = new SumOfNumericalDiceViewModel();

            // Assert
            Assert.IsInstanceOf<INotifyPropertyChanged>(viewModel);
            Assert.AreEqual(2, viewModel.NumberOfDice);
            Assert.AreEqual(6, viewModel.NumberOfSides);
            AssertExpectedDistribution(viewModel.NumberOfDice, viewModel.NumberOfSides, viewModel.Distribution);
        }

        [Test]
        public void NumberOfSides_SetNewValue_NotifyPropertyChange()
        {
            // Setup
            var propertyChangedCalls = new Dictionary<string, int>();

            var viewModel = new SumOfNumericalDiceViewModel();
            viewModel.PropertyChanged += (s, e) =>
            {
                if (!propertyChangedCalls.ContainsKey(e.PropertyName))
                {
                    propertyChangedCalls[e.PropertyName] = 0;
                }

                propertyChangedCalls[e.PropertyName]++;

                Assert.AreSame(viewModel, s);
            };

            // Call
            viewModel.NumberOfSides = 3;

            // Assert
            Assert.AreEqual(1, propertyChangedCalls[nameof(SumOfNumericalDiceViewModel.NumberOfSides)]);
        }

        [Test]
        public void NumberOfSides_SetNewValue_UpdatesDistributionAndNotifyPropertyChange()
        {
            // Setup
            var propertyChangedCalls = new Dictionary<string, int>();

            var viewModel = new SumOfNumericalDiceViewModel();
            viewModel.PropertyChanged += (s, e) =>
            {
                if (!propertyChangedCalls.ContainsKey(e.PropertyName))
                {
                    propertyChangedCalls[e.PropertyName] = 0;
                }

                propertyChangedCalls[e.PropertyName]++;

                Assert.AreSame(viewModel, s);
            };

            // Call
            viewModel.NumberOfSides = 3;

            // Assert
            Assert.AreEqual(1, propertyChangedCalls[nameof(SumOfNumericalDiceViewModel.Distribution)]);
            AssertExpectedDistribution(viewModel.NumberOfDice, viewModel.NumberOfSides, viewModel.Distribution);
        }

        [Test]
        public void NumberOfDice_SetNewValue_NotifyPropertyChange()
        {
            // Setup
            var propertyChangedCalls = new Dictionary<string, int>();

            var viewModel = new SumOfNumericalDiceViewModel();
            viewModel.PropertyChanged += (s, e) =>
            {
                if (!propertyChangedCalls.ContainsKey(e.PropertyName))
                {
                    propertyChangedCalls[e.PropertyName] = 0;
                }

                propertyChangedCalls[e.PropertyName]++;

                Assert.AreSame(viewModel, s);
            };

            // Call
            viewModel.NumberOfDice = 3;

            // Assert
            Assert.AreEqual(1, propertyChangedCalls[nameof(SumOfNumericalDiceViewModel.NumberOfDice)]);
        }

        [Test]
        public void NumberOfDice_SetNewValue_UpdatesDistributionAndNotifyPropertyChange()
        {
            // Setup
            var propertyChangedCalls = new Dictionary<string, int>();

            var viewModel = new SumOfNumericalDiceViewModel();
            viewModel.PropertyChanged += (s, e) =>
            {
                if (!propertyChangedCalls.ContainsKey(e.PropertyName))
                {
                    propertyChangedCalls[e.PropertyName] = 0;
                }

                propertyChangedCalls[e.PropertyName]++;

                Assert.AreSame(viewModel, s);
            };

            // Call
            viewModel.NumberOfDice = 3;

            // Assert
            Assert.AreEqual(1, propertyChangedCalls[nameof(SumOfNumericalDiceViewModel.Distribution)]);
            AssertExpectedDistribution(viewModel.NumberOfDice, viewModel.NumberOfSides, viewModel.Distribution);
        }

        private static void AssertExpectedDistribution(int numberOfDice, int numberOfSides, DiscreteValueProbabilityDistribution actualDistribution)
        {
            var rng = MockRepository.GenerateStub<IRandomNumberGenerator>();
            IEnumerable<NumericalDie> dice = Enumerable.Repeat(numberOfSides, numberOfDice)
                .Select(nrOfSides => new NumericalDie(nrOfSides, rng));
            var sumOfDice = new SumOfDice(dice, rng);

            DiscreteValueProbabilityDistribution expectedDistribution = sumOfDice.ProbabilityDistribution;
            AssertEquals(expectedDistribution, actualDistribution);
        }

        private static void AssertEquals(DiscreteValueProbabilityDistribution expectedDistribution, DiscreteValueProbabilityDistribution actualDistribution)
        {
            Assert.AreEqual(expectedDistribution.Specification.Count, actualDistribution.Specification.Count);
            for (int i = 0; i < expectedDistribution.Specification.Count; i++)
            {
                Assert.AreEqual(expectedDistribution.Specification[i].Value, actualDistribution.Specification[i].Value);
                Assert.AreEqual(expectedDistribution.Specification[i].Probability, actualDistribution.Specification[i].Probability);
            }
        }
    }
}
