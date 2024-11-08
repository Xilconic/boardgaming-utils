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

using System.Diagnostics;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using Xilconic.BoardgamingUtils.Mathematics;

namespace Xilconic.BoardgamingUtils.Application.Controls;

/// <summary>
/// ViewModel for plotting the probability density function (pdf) of a <see cref="DiscreteValueProbabilityDistribution"/>.
/// </summary>
public class ProbabilityDensityFunctionChartViewModel
{
    private readonly CategoryAxis _horizontalAxis;
    private readonly BarSeries _valueSeries;

    private DiscreteValueProbabilityDistribution? _probabilityDistribution;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProbabilityDensityFunctionChartViewModel"/> class.
    /// </summary>
    public ProbabilityDensityFunctionChartViewModel()
    {
        Items = new List<ValueProbabilityPair>();

        _horizontalAxis = new CategoryAxis
        {
            ItemsSource = Items,
            LabelField = nameof(ValueProbabilityPair.Value),
            AbsoluteMinimum = -1,
            IsZoomEnabled = false,
            IsPanEnabled = false,
            GapWidth = 0,
            Key = "Value",
        };

        LinearAxis verticalAxis = new()
        {
            Title = "Probability",
            Minimum = 0.0,
            AbsoluteMinimum = 0.0,
            Maximum = 1.0,
            AbsoluteMaximum = 1.0,
            IsZoomEnabled = false,
            IsPanEnabled = false,
            StringFormat = "p",
            Key = "Probability"
        };
            
        _valueSeries = new BarSeries
        {
            ItemsSource = Items,
            ValueField = nameof(ValueProbabilityPair.Probability),
            FillColor = OxyColors.DarkCyan,
            TrackerFormatString = "{0}\n{1}: {2:p}",
            XAxisKey = verticalAxis.Key,
            YAxisKey = _horizontalAxis.Key,
        };

        PlotModel = new PlotModel
        {
            IsLegendVisible = false,
            Axes =
            {
                _horizontalAxis,
                verticalAxis,
            },
            Series =
            {
                _valueSeries,
            },
        };

        Title = "Value Probabilities (pdf)";
        ValueName = "Value";
        Distribution = null;
    }

    /// <summary>
    /// Gets the model for displaying the pdf.
    /// </summary>
    public PlotModel PlotModel { get; }

    /// <summary>
    /// Gets or sets the name of the random variable being plotted.
    /// </summary>
    public string ValueName
    {
        get => _horizontalAxis.Title;
        set
        {
            Debug.Assert(!string.IsNullOrWhiteSpace(value), "The name of the value cannot be null or empty.");

            _horizontalAxis.Title = value;
            _valueSeries.Title = $"{value} probability";
        }
    }

    /// <summary>
    /// Gets or sets the title of the plot.
    /// </summary>
    public string Title
    {
        get => PlotModel.Title;
        set
        {
            Debug.Assert(!string.IsNullOrWhiteSpace(value), "The title of the plot cannot be null or empty.");

            PlotModel.Title = value;
        }
    }

    /// <summary>
    /// Gets or sets the distribution to be visualized.
    /// </summary>
    public DiscreteValueProbabilityDistribution? Distribution
    {
        get => _probabilityDistribution;
        set
        {
            _probabilityDistribution = value;

            Items.Clear();

            if (_probabilityDistribution != null)
            {
                Items.AddRange(_probabilityDistribution.Specification);
                _horizontalAxis.AbsoluteMaximum = _probabilityDistribution.Specification.Count;
            }
            else
            {
                _horizontalAxis.AbsoluteMaximum = double.MaxValue;
            }

            PlotModel.InvalidatePlot(true);
        }
    }

    private List<ValueProbabilityPair> Items { get; }
}