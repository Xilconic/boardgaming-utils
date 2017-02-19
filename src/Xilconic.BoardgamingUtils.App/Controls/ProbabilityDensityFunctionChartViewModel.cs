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
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Xilconic.BoardgamingUtils.Mathmatics;

namespace Xilconic.BoardgamingUtils.App.Controls
{
    /// <summary>
    /// ViewModel for plotting the probability density function (pdf) of a <see cref="DiscreteValueProbabilityDistribution"/>.
    /// </summary>
    internal class ProbabilityDensityFunctionChartViewModel
    {
        private DiscreteValueProbabilityDistribution probabilityDistribution;
        private readonly CategoryAxis horizontalAxis;
        private readonly LinearAxis verticalAxis;
        private readonly ColumnSeries valueSerie;

        /// <summary>
        /// Initializes a new instance of <see cref="ProbabilityDensityFunctionChartViewModel"/>.
        /// </summary>
        public ProbabilityDensityFunctionChartViewModel()
        {
            Items = new List<ValueProbabilityPair>();

            horizontalAxis = new CategoryAxis
            {
                Position = AxisPosition.Bottom,
                ItemsSource = Items,
                LabelField = nameof(ValueProbabilityPair.Value),
                AbsoluteMinimum = -1,
                IsZoomEnabled = false,
                IsPanEnabled = false,
                GapWidth = 0
            };

            verticalAxis = new LinearAxis
            {
                Title = "Probability",
                Position = AxisPosition.Left,
                Minimum = 0.0,
                AbsoluteMinimum = 0.0,
                Maximum = 1.0,
                AbsoluteMaximum = 1.0,
                IsZoomEnabled = false,
                IsPanEnabled = false,
                StringFormat = "p",
            };

            valueSerie = new ColumnSeries
            {
                ItemsSource = Items,
                ValueField = nameof(ValueProbabilityPair.Probability),
                FillColor = OxyColors.DarkCyan,
                TrackerFormatString = "{0}\n{1}: {2:p}"
            };

            PlotModel = new PlotModel
            {
                IsLegendVisible = false,
                Axes =
                {
                    horizontalAxis,
                    verticalAxis
                },
                Series =
                {
                    valueSerie
                }
            };

            Title = "Value Probabilities (pdf)";
            ValueName = "Value";
            Distribution = null;
        }

        /// <summary>
        /// The model for displaying the pdf.
        /// </summary>
        public PlotModel PlotModel { get; private set; }

        /// <summary>
        /// The name of the random variable being plotted.
        /// </summary>
        public string ValueName
        {
            get
            {
                return horizontalAxis.Title;
            }
            set
            {
                Contract.Requires<ArgumentException>(!string.IsNullOrWhiteSpace(value));

                horizontalAxis.Title = value;
                valueSerie.Title = $"{value} probability";
            }
        }

        /// <summary>
        /// The title of the plot.
        /// </summary>
        public string Title
        {
            get
            {
                return PlotModel.Title;
            }
            set
            {
                Contract.Requires<ArgumentException>(!string.IsNullOrWhiteSpace(value));

                PlotModel.Title = value;
            }
        }

        /// <summary>
        /// The distribution to be visualized.
        /// </summary>
        public DiscreteValueProbabilityDistribution Distribution
        {
            get
            {
                return probabilityDistribution;
            }
            set
            {
                probabilityDistribution = value;

                Items.Clear();

                if(probabilityDistribution != null)
                {
                    Items.AddRange(probabilityDistribution.Specification);
                    horizontalAxis.AbsoluteMaximum = probabilityDistribution.Specification.Count;
                }
                else
                {
                    horizontalAxis.AbsoluteMaximum = double.MaxValue;
                }
                

                PlotModel.InvalidatePlot(true);
            }
        }

        private List<ValueProbabilityPair> Items { get; }
    }
}
