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
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using Xilconic.BoardgamingUtils.Dice;
using Xilconic.BoardgamingUtils.Mathmatics;
using Xilconic.BoardgamingUtils.PseudoRandom;

namespace Xilconic.BoardgamingUtils.App.Controls
{
    /// <summary>
    /// ViewModel for displaying the probability density function (pdf) of a <see cref="SumOfDice"/>
    /// for a single class of <see cref="NumericalDie"/>.
    /// </summary>
    internal class SumOfNumericalDiceViewModel : INotifyPropertyChanged
    {
        private readonly RandomNumberGenerator rng = new RandomNumberGenerator(Environment.TickCount);
        private SumOfDice diceSum;
        private readonly CategoryAxis horizontalAxis;
        private int numberOfDice = 2, numberOfSides = 6;

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Initializes a new instance of <see cref="SumOfNumericalDiceViewModel"/>.
        /// </summary>
        public SumOfNumericalDiceViewModel()
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
                IsPanEnabled = false,
                GapWidth = 0
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
                        IsPanEnabled = false,
                        StringFormat = "p",
                    }
                },
                Series =
                {
                    new ColumnSeries
                    {
                        Title = "Sum result probability",
                        ItemsSource = Items,
                        ValueField = nameof(ValueProbabilityPair.Probability),
                        FillColor = OxyColors.DarkCyan,
                        TrackerFormatString = "{0}\n{1}: {2:p}"
                    }
                }
            };

            NumberOfDice = 2;
            NumberOfSides = 6;
        }

        /// <summary>
        /// The number of dice in the collection.
        /// </summary>
        public int NumberOfDice
        {
            get
            {
                return numberOfDice;
            }
            set
            {
                Contract.Requires<ArgumentOutOfRangeException>(value > 0);

                numberOfDice = value;

                UpdateForNewSumOfDiceParameters();

                OnNotifyPropertyChanged(nameof(NumberOfDice));
            }
        }

        /// <summary>
        /// The number of sides the dice have.
        /// </summary>
        public int NumberOfSides
        {
            get
            {
                return numberOfSides;
            }
            set
            {
                Contract.Requires<ArgumentOutOfRangeException>(value > 0);

                numberOfSides = value;

                UpdateForNewSumOfDiceParameters();

                OnNotifyPropertyChanged(nameof(NumberOfSides));
            }
        }

        /// <summary>
        /// The model for displaying the pdf of the die.
        /// </summary>
        public PlotModel PlotModel { get; private set; }

        private List<ValueProbabilityPair> Items { get; }

        private void OnNotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void UpdateForNewSumOfDiceParameters()
        {
            diceSum = new SumOfDice(GenerateDice(numberOfDice, numberOfSides), rng);

            Items.Clear();
            Items.AddRange(diceSum.ProbabilityDistribution.Specification);

            horizontalAxis.AbsoluteMaximum = diceSum.ProbabilityDistribution.Specification.Count;

            PlotModel.InvalidatePlot(true);
        }

        private IEnumerable<NumericalDie> GenerateDice(int numberOfDice, int numberOfSides)
        {
            return Enumerable.Repeat(numberOfSides, numberOfDice)
                .Select(nrOfSides => new NumericalDie(nrOfSides, rng));
        }
    }
}
