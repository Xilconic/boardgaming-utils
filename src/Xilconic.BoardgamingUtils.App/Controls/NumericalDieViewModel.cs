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
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using Xilconic.BoardgamingUtils.Dice;
using Xilconic.BoardgamingUtils.PseudoRandom;
using Xilconic.BoardgamingUtils.Mathmatics;
using System.ComponentModel;
using System.Diagnostics.Contracts;

namespace Xilconic.BoardgamingUtils.App.Controls
{
    /// <summary>
    /// ViewModel for displaying the probability density function (pdf) of a <see cref="NumericalDie"/>.
    /// </summary>
    internal class NumericalDieViewModel : INotifyPropertyChanged
    {
        private readonly RandomNumberGenerator rng = new RandomNumberGenerator(Environment.TickCount);
        private NumericalDie die;
        private readonly CategoryAxis horizontalAxis;

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Initializes a new instance of <see cref="NumericalDieViewModel"/>.
        /// </summary>
        public NumericalDieViewModel()
        {
            Items = new List<ValueProbabilityPair>();

            horizontalAxis = new CategoryAxis
            {
                Title = "Die face",
                Position = AxisPosition.Bottom,
                ItemsSource = Items,
                LabelField = nameof(ValueProbabilityPair.Value),
                AbsoluteMinimum = -1,
                IsZoomEnabled = false,
                IsPanEnabled = false
            };

            PlotModel = new PlotModel
            {
                Title = "Die Probabilities (pdf)",
                IsLegendVisible = false,
                Axes =
                {
                    horizontalAxis,
                    new LinearAxis
                    {
                        Title = "Probability",
                        Position = AxisPosition.Left,
                        Minimum = 0.0,
                        AbsoluteMinimum = 0.0,
                        Maximum = 1.0,
                        AbsoluteMaximum = 1.0,
                        IsZoomEnabled = false,
                        IsPanEnabled = false
                    }
                },
                Series =
                {
                    new ColumnSeries
                    {
                        Title = "Die face probability",
                        ItemsSource = Items,
                        ValueField = nameof(ValueProbabilityPair.Probability),
                        FillColor = OxyColors.DarkCyan
                    }
                }
            };

            NumberOfSides = 6;
        }

        /// <summary>
        /// The model for displaying the pdf of the die.
        /// </summary>
        public PlotModel PlotModel { get; private set; }

        /// <summary>
        /// The number of sides the die has.
        /// </summary>
        public int NumberOfSides
        {
            get
            {
                return die.NumberOfSides;
            }
            set
            {
                Contract.Requires<ArgumentOutOfRangeException>(value > 0);

                die = new NumericalDie(value, rng);
                Items.Clear();
                Items.AddRange(die.ProbabilityDistribution.Specification);
                horizontalAxis.AbsoluteMaximum = value;

                OnNotifyPropertyChanged(nameof(NumberOfSides));
                PlotModel.InvalidatePlot(true);
            }
        }

        private List<ValueProbabilityPair> Items { get; }

        private void OnNotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
