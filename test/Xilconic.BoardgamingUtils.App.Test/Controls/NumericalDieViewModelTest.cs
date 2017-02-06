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
            AssertViewModelForExpectedNumberOfDieSides(6, viewModel);
        }

        [TestCase(2)]
        [TestCase(20)]
        public void NumberOfSides_SetToDifferentValue_UpdatePlotModel(int newNumberOfSides)
        {
            // Setup
            var viewModel = new NumericalDieViewModel();

            int propertyChangedCallCount = 0;
            object propertyChangedSender = null;
            PropertyChangedEventArgs eventArgs = null;
            viewModel.PropertyChanged += (sender, args) =>
            {
                propertyChangedCallCount++;
                propertyChangedSender = sender;
                eventArgs = args;
            };

            // Call
            viewModel.NumberOfSides = newNumberOfSides;

            // Assert
            AssertViewModelForExpectedNumberOfDieSides(newNumberOfSides, viewModel);

            Assert.AreEqual(1, propertyChangedCallCount);
            Assert.AreSame(viewModel, propertyChangedSender);
            Assert.AreEqual(nameof(viewModel.NumberOfSides), eventArgs.PropertyName);
        }

        private void AssertViewModelForExpectedNumberOfDieSides(int expectedNumberofSidesOnDie, NumericalDieViewModel viewModel)
        {
            Assert.AreEqual(expectedNumberofSidesOnDie, viewModel.NumberOfSides);

            PlotModel model = viewModel.PlotModel;
            Assert.AreEqual("Die Probabilities (pdf)", model.Title);
            Assert.IsFalse(model.IsLegendVisible);

            Assert.AreEqual(2, model.Axes.Count);
            var horizontalAxis = (CategoryAxis)model.Axes[0];
            Assert.AreEqual("Die face", horizontalAxis.Title);
            Assert.AreEqual(AxisPosition.Bottom, horizontalAxis.Position);
            Assert.AreEqual(-1, horizontalAxis.AbsoluteMinimum);
            Assert.AreEqual(expectedNumberofSidesOnDie, horizontalAxis.AbsoluteMaximum);
            Assert.IsFalse(horizontalAxis.IsZoomEnabled);
            Assert.IsFalse(horizontalAxis.IsPanEnabled);
            AssertItemSource(horizontalAxis.ItemsSource, expectedNumberofSidesOnDie);
            Assert.AreEqual(nameof(ValueProbabilityPair.Value), horizontalAxis.LabelField);

            var verticalAxis = (LinearAxis)model.Axes[1];
            Assert.AreEqual("Probability", verticalAxis.Title);
            Assert.AreEqual(AxisPosition.Left, verticalAxis.Position);
            Assert.AreEqual(0.0, verticalAxis.AbsoluteMinimum);
            Assert.AreEqual(1.0, verticalAxis.AbsoluteMaximum);
            Assert.AreEqual(0.0, verticalAxis.Minimum);
            Assert.AreEqual(1.0, verticalAxis.Maximum);
            Assert.IsFalse(verticalAxis.IsZoomEnabled);
            Assert.IsFalse(verticalAxis.IsPanEnabled);

            Assert.AreEqual(1, model.Series.Count);
            ColumnSeries series = (ColumnSeries)model.Series[0];
            Assert.AreEqual("Die face probability", series.Title);
            AssertItemSource(series.ItemsSource, expectedNumberofSidesOnDie);
            Assert.AreEqual(nameof(ValueProbabilityPair.Probability), series.ValueField);
            Assert.AreEqual(OxyColors.DarkCyan, series.FillColor);
        }

        private void AssertItemSource(IEnumerable itemSource, int expectedNumberOfSidesOnDie)
        {
            var valueProbabilities = (IList<ValueProbabilityPair>)itemSource;

            Assert.AreEqual(expectedNumberOfSidesOnDie, valueProbabilities.Count);

            var expectedProbability = 1.0 / expectedNumberOfSidesOnDie;
            for(int i = 0; i < expectedNumberOfSidesOnDie; i++)
            {
                Assert.AreEqual(i + 1, valueProbabilities[i].Value);
                Assert.AreEqual(expectedProbability, valueProbabilities[i].Probability);
            }
        }
    }
}
