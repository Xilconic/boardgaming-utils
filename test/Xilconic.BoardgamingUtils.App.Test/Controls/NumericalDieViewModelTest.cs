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
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Xilconic.BoardgamingUtils.App.Controls;
using Xilconic.BoardgamingUtils.Mathmatics;
using System;
using Xilconic.BoardgamingUtils.Dice;
using Xilconic.BoardgamingUtils.PseudoRandom;
using Rhino.Mocks;

namespace Xilconic.BoardgamingUtils.App.Test.Controls
{
    [TestFixture]
    public class NumericalDieViewModelTest
    {
        [Test]
        public void Constructor_ExpectedValues()
        {
            // Call
            var viewModel = new NumericalDieViewModel();

            // Assert
            Assert.IsInstanceOf<INotifyPropertyChanged>(viewModel);
            Assert.AreEqual(6, viewModel.NumberOfSides);
            AssertExpectedDistribution(viewModel.NumberOfSides, viewModel.Distribution);
        }

        [Test]
        public void NumberOfSides_SetNewValue_NotifyPropertyChange()
        {
            // Setup
            var propertyChangedCalls = new Dictionary<string, int>();

            var viewModel = new NumericalDieViewModel();
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
            Assert.AreEqual(1, propertyChangedCalls[nameof(NumericalDieViewModel.NumberOfSides)]);
        }

        [Test]
        public void NumberOfSides_SetNewValue_UpdatesDistributionAndNotifyPropertyChange()
        {
            // Setup
            var propertyChangedCalls = new Dictionary<string, int>();

            var viewModel = new NumericalDieViewModel();
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
            Assert.AreEqual(1, propertyChangedCalls[nameof(NumericalDieViewModel.Distribution)]);
            AssertExpectedDistribution(viewModel.NumberOfSides, viewModel.Distribution);
        }

        private void AssertExpectedDistribution(int numberOfSides, DiscreteValueProbabilityDistribution actualDistribution)
        {
            var rng = MockRepository.GenerateStub<IRandomNumberGenerator>();
            var die = new NumericalDie(numberOfSides, rng);

            DiscreteValueProbabilityDistribution expectedDistribution = die.ProbabilityDistribution;
            AssertEquals(expectedDistribution, actualDistribution);
        }

        private void AssertEquals(DiscreteValueProbabilityDistribution expectedDistribution, DiscreteValueProbabilityDistribution actualDistribution)
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
