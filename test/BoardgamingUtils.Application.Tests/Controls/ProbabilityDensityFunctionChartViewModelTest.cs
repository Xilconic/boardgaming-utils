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

using FluentAssertions;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System.Collections;
using Xilconic.BoardgamingUtils.Application.Controls;
using Xilconic.BoardgamingUtils.Mathematics;

namespace Xilconic.BoardgamingUtils.Application.Tests.Controls;

public class ProbabilityDensityFunctionChartViewModelTest
{
    private readonly ProbabilityDensityFunctionChartViewModel _viewModel = new();

    [Fact]
    public void WhenConstructingReturnsInstanceInExpectedState()
    {
        AssertPlotModelForExpectedDistribution(null, "Value Probabilities (pdf)", "Value", _viewModel.PlotModel);
    }

    [Fact]
    public void WhenSettingTitleThenUpdatePlotModelTitle()
    {
        const string newTitle = "A";

        _viewModel.Title = newTitle;

        _viewModel.Title.Should().Be(newTitle);
        _viewModel.PlotModel.Title.Should().Be(newTitle);
    }

    [Fact]
    public void WhenSettingValueNameThenUpdateTitles()
    {
        const string newName = "B";

        _viewModel.ValueName = newName;
        
        _viewModel.ValueName.Should().Be(newName);
        _viewModel.PlotModel.Axes[0].Title.Should().Be(newName);
        _viewModel.PlotModel.Series[0].Title.Should().Be($"{newName} probability");
    }

    [Fact]
    public void WhenSettingDistributionThenUpdatePlotModel()
    {
        var distribution = new DiscreteValueProbabilityDistribution([
            new ValueProbabilityPair(1, new Probability(0.25)),
            new ValueProbabilityPair(2, new Probability(0.50)),
            new ValueProbabilityPair(3, new Probability(0.25))
        ]);

        _viewModel.Distribution = distribution;

        AssertPlotModelForExpectedDistribution(distribution, _viewModel.Title, _viewModel.ValueName, _viewModel.PlotModel);
    }

    private static void AssertItemSource(DiscreteValueProbabilityDistribution? expectedDistribution, IEnumerable itemSource)
    {
        if (expectedDistribution == null)
        {
            AssertThatSequenceIsEmpty(itemSource);
        }
        else
        {
            var valueProbabilities = itemSource.Should().BeAssignableTo<IList<ValueProbabilityPair>>().Which;

            IReadOnlyList<ValueProbabilityPair> expectedProbabilities = expectedDistribution.Specification;

            valueProbabilities.Should().HaveCount(expectedProbabilities.Count);
            for (int i = 0; i < expectedProbabilities.Count; i++)
            {
                valueProbabilities[i].Should().BeEquivalentTo(expectedProbabilities[i],
                    "Because element at index {0} should have value of {1} and actually is {2}",
                    i, expectedProbabilities[i], valueProbabilities[i]);
            }
        }
    }

    private static void AssertThatSequenceIsEmpty(IEnumerable itemSource)
    {
        IEnumerator enumerator = itemSource.GetEnumerator();
        using var _ = enumerator as IDisposable; // perform Safe-cast disposable of enumerator
        enumerator.MoveNext().Should().BeFalse();
    }

    private static void AssertPlotModelForExpectedDistribution(
        DiscreteValueProbabilityDistribution? expectedDistribution,
        string expectedTitle,
        string expectedValueName,
        PlotModel plotModel)
    {
        plotModel.Title.Should().Be(expectedTitle);
        plotModel.IsLegendVisible.Should().BeFalse();

        plotModel.Axes.Should().HaveCount(2);
        var horizontalAxis = (CategoryAxis)plotModel.Axes[0];
        horizontalAxis.Title.Should().Be(expectedValueName);
        horizontalAxis.AbsoluteMinimum.Should().Be(-1);
        if (expectedDistribution == null)
        {
            horizontalAxis.AbsoluteMaximum.Should().Be(double.MaxValue);
        }
        else
        {
            horizontalAxis.AbsoluteMaximum.Should().Be(expectedDistribution.Specification.Count);
        }
        horizontalAxis.IsZoomEnabled.Should().BeFalse();
        horizontalAxis.IsPanEnabled.Should().BeFalse();
        horizontalAxis.GapWidth.Should().Be(0);
        AssertItemSource(expectedDistribution, horizontalAxis.ItemsSource);
        horizontalAxis.LabelField.Should().Be(nameof(ValueProbabilityPair.Value));
        horizontalAxis.Key.Should().Be("Value");

        var verticalAxis = (LinearAxis)plotModel.Axes[1];
        verticalAxis.Title.Should().Be("Probability");
        verticalAxis.AbsoluteMinimum.Should().Be(0);
        verticalAxis.AbsoluteMaximum.Should().Be(1);
        verticalAxis.Minimum.Should().Be(0);
        verticalAxis.Maximum.Should().Be(1);
        verticalAxis.IsZoomEnabled.Should().BeFalse();
        verticalAxis.IsZoomEnabled.Should().BeFalse();
        verticalAxis.StringFormat.Should().Be("p", "Because we want to render the probabilities as percentages.");

        plotModel.Series.Should().HaveCount(1);
        BarSeries series = (BarSeries)plotModel.Series[0];
        series.Title.Should().Be($"{expectedValueName} probability");
        AssertItemSource(expectedDistribution, horizontalAxis.ItemsSource);
        series.ValueField.Should().Be(nameof(ValueProbabilityPair.Probability));
        series.FillColor.Should().Be(OxyColors.DarkCyan);
        series.TrackerFormatString.Should().Be("{0}\n{1}: {2:p}");
        series.XAxisKey.Should().Be(verticalAxis.Key);
        series.YAxisKey.Should().Be(horizontalAxis.Key);
    }
}