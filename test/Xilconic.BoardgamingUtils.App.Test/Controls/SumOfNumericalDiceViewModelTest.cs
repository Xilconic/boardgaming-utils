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
using Rhino.Mocks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xilconic.BoardgamingUtils.App.Controls;
using Xilconic.BoardgamingUtils.Dice;
using Xilconic.BoardgamingUtils.Mathmatics;
using Xilconic.BoardgamingUtils.PseudoRandom;

namespace Xilconic.BoardgamingUtils.App.Test.Controls
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
            AssertViewModelForExpectedNumberOfDice(2, 6, viewModel);
        }

        private static void AssertViewModelForExpectedNumberOfDice(int expectedNumberOfDice, 
            int expectedNumberOfSides, 
            SumOfNumericalDiceViewModel viewModel)
        {
            Assert.AreEqual(expectedNumberOfDice, viewModel.NumberOfDice);
            Assert.AreEqual(expectedNumberOfSides, viewModel.NumberOfSides);

            PlotModel model = viewModel.PlotModel;
            Assert.AreEqual("Dice Probabilities (pdf)", model.Title);
            Assert.IsFalse(model.IsLegendVisible);

            Assert.AreEqual(2, model.Axes.Count);
            var horizontalAxis = (CategoryAxis)model.Axes[0];
            Assert.AreEqual("Sum", horizontalAxis.Title);
            Assert.AreEqual(AxisPosition.Bottom, horizontalAxis.Position);
            Assert.AreEqual(-1, horizontalAxis.AbsoluteMinimum);
            Assert.AreEqual(expectedNumberOfSides * expectedNumberOfDice - expectedNumberOfDice + 1, horizontalAxis.AbsoluteMaximum);
            Assert.IsFalse(horizontalAxis.IsZoomEnabled);
            Assert.IsFalse(horizontalAxis.IsPanEnabled);
            Assert.AreEqual(0.0, horizontalAxis.GapWidth);
            AssertItemSource(horizontalAxis.ItemsSource, expectedNumberOfDice, expectedNumberOfSides);
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
            Assert.AreEqual("p", verticalAxis.StringFormat);

            Assert.AreEqual(1, model.Series.Count);
            ColumnSeries series = (ColumnSeries)model.Series[0];
            Assert.AreEqual("Sum result probability", series.Title);
            AssertItemSource(horizontalAxis.ItemsSource, expectedNumberOfDice, expectedNumberOfSides);
            Assert.AreEqual(nameof(ValueProbabilityPair.Probability), series.ValueField);
            Assert.AreEqual(OxyColors.DarkCyan, series.FillColor);
            Assert.AreEqual("{0}\n{1}: {2:p}", series.TrackerFormatString);
        }

        private static void AssertItemSource(IEnumerable itemSource, int expectedNumberOfDice, int expectedNumberOfSidesOnDie)
        {
            var valueProbabilities = (IList<ValueProbabilityPair>)itemSource;

            var rng = MockRepository.GenerateStub<IRandomNumberGenerator>();
            var dice = Enumerable.Repeat(expectedNumberOfSidesOnDie, expectedNumberOfDice)
                .Select(nrOfSides => new NumericalDie(nrOfSides, rng));
            var diceSum = new SumOfDice(dice, rng);
            IList<ValueProbabilityPair> expectedProbabilities = diceSum.ProbabilityDistribution.Specification;

            Assert.AreEqual(expectedProbabilities.Count, valueProbabilities.Count);
            for(int i = 0; i < expectedProbabilities.Count; i++)
            {
                Assert.AreEqual(expectedProbabilities[i].Value, valueProbabilities[i].Value);
                Assert.AreEqual(expectedProbabilities[i].Probability, valueProbabilities[i].Probability);
            }
        }
    }
}
