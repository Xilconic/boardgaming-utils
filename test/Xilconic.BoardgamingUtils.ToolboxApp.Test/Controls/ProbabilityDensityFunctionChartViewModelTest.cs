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
using System.Collections;
using System.Collections.Generic;

using NUnit.Framework;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using Xilconic.BoardgamingUtils.Mathmatics;
using Xilconic.BoardgamingUtils.ToolboxApp.Controls;

namespace Xilconic.BoardgamingUtils.ToolboxApp.Test.Controls
{
    [TestFixture]
    public class ProbabilityDensityFunctionChartViewModelTest
    {
        [Test]
        public void Constructor_ExpectedValues()
        {
            // Call
            var viewModel = new ProbabilityDensityFunctionChartViewModel();

            // Assert
            AssertPlotModelForExpectedDistribution(null, "Value Probabilities (pdf)", "Value", viewModel.PlotModel);
        }

        [Test]
        public void Title_SetValue_UpdatePlotModelTitle()
        {
            // Setup
            var viewModel = new ProbabilityDensityFunctionChartViewModel();

            const string newTitle = "A";

            // Call
            viewModel.Title = newTitle;

            // Assert
            Assert.AreEqual(newTitle, viewModel.Title);
            Assert.AreEqual(newTitle, viewModel.PlotModel.Title);
        }

        [Test]
        public void ValueName_SetValue_UpdateTitles()
        {
            // Setup
            var viewModel = new ProbabilityDensityFunctionChartViewModel();

            const string newName = "B";

            // Call
            viewModel.ValueName = newName;

            // Assert
            Assert.AreEqual(newName, viewModel.ValueName);
            Assert.AreEqual(newName, viewModel.PlotModel.Axes[0].Title);
            Assert.AreEqual(newName + " probability", viewModel.PlotModel.Series[0].Title);
        }

        [Test]
        public void Distribution_SetValue_UpdatePlotModel()
        {
            // Setup
            var viewModel = new ProbabilityDensityFunctionChartViewModel();

            var distribution = new DiscreteValueProbabilityDistribution(new[]
            {
                new ValueProbabilityPair(1, 0.25),
                new ValueProbabilityPair(2, 0.50),
                new ValueProbabilityPair(3, 0.25),
            });

            // Call
            viewModel.Distribution = distribution;

            // Assert
            AssertPlotModelForExpectedDistribution(distribution, viewModel.Title, viewModel.ValueName, viewModel.PlotModel);
        }

        private static void AssertItemSource(DiscreteValueProbabilityDistribution expectedDistribution, IEnumerable itemSource)
        {
            if (expectedDistribution == null)
            {
                CollectionAssert.IsEmpty(itemSource);
            }
            else
            {
                var valueProbabilities = (IList<ValueProbabilityPair>)itemSource;

                IList<ValueProbabilityPair> expectedProbabilities = expectedDistribution.Specification;

                Assert.AreEqual(expectedProbabilities.Count, valueProbabilities.Count);
                for (int i = 0; i < expectedProbabilities.Count; i++)
                {
                    Assert.AreEqual(expectedProbabilities[i].Value, valueProbabilities[i].Value);
                    Assert.AreEqual(expectedProbabilities[i].Probability, valueProbabilities[i].Probability);
                }
            }
        }

        private void AssertPlotModelForExpectedDistribution(
            DiscreteValueProbabilityDistribution expectedDistribution,
            string expectedTitle,
            string expectedValueName,
            PlotModel plotModel)
        {
            Assert.AreEqual(expectedTitle, plotModel.Title);
            Assert.IsFalse(plotModel.IsLegendVisible);

            Assert.AreEqual(2, plotModel.Axes.Count);
            var horizontalAxis = (CategoryAxis)plotModel.Axes[0];
            Assert.AreEqual(expectedValueName, horizontalAxis.Title);
            Assert.AreEqual(AxisPosition.Bottom, horizontalAxis.Position);
            Assert.AreEqual(-1, horizontalAxis.AbsoluteMinimum);
            Assert.AreEqual(expectedDistribution == null ? double.MaxValue : expectedDistribution.Specification.Count, horizontalAxis.AbsoluteMaximum);
            Assert.IsFalse(horizontalAxis.IsZoomEnabled);
            Assert.IsFalse(horizontalAxis.IsPanEnabled);
            Assert.AreEqual(0.0, horizontalAxis.GapWidth);
            AssertItemSource(expectedDistribution, horizontalAxis.ItemsSource);
            Assert.AreEqual(nameof(ValueProbabilityPair.Value), horizontalAxis.LabelField);

            var verticalAxis = (LinearAxis)plotModel.Axes[1];
            Assert.AreEqual("Probability", verticalAxis.Title);
            Assert.AreEqual(AxisPosition.Left, verticalAxis.Position);
            Assert.AreEqual(0.0, verticalAxis.AbsoluteMinimum);
            Assert.AreEqual(1.0, verticalAxis.AbsoluteMaximum);
            Assert.AreEqual(0.0, verticalAxis.Minimum);
            Assert.AreEqual(1.0, verticalAxis.Maximum);
            Assert.IsFalse(verticalAxis.IsZoomEnabled);
            Assert.IsFalse(verticalAxis.IsPanEnabled);
            Assert.AreEqual("p", verticalAxis.StringFormat);

            Assert.AreEqual(1, plotModel.Series.Count);
            ColumnSeries series = (ColumnSeries)plotModel.Series[0];
            Assert.AreEqual($"{expectedValueName} probability", series.Title);
            AssertItemSource(expectedDistribution, horizontalAxis.ItemsSource);
            Assert.AreEqual(nameof(ValueProbabilityPair.Probability), series.ValueField);
            Assert.AreEqual(OxyColors.DarkCyan, series.FillColor);
            Assert.AreEqual("{0}\n{1}: {2:p}", series.TrackerFormatString);
        }
    }
}
