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
    /// ViewModel for configuring the probability density function (pdf) of a <see cref="SumOfDice"/>
    /// for a single class of <see cref="NumericalDie"/>.
    /// </summary>
    internal class SumOfNumericalDiceViewModel : INotifyPropertyChanged
    {
        private readonly RandomNumberGenerator rng = new RandomNumberGenerator(Environment.TickCount);
        private SumOfDice diceSum;
        private int numberOfDice = 2, numberOfSides = 6;

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Initializes a new instance of <see cref="SumOfNumericalDiceViewModel"/>.
        /// </summary>
        public SumOfNumericalDiceViewModel()
        {
            UpdateForNewSumOfDiceParameters();
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
                OnNotifyPropertyChanged(nameof(Distribution));
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
                OnNotifyPropertyChanged(nameof(Distribution));
            }
        }

        public DiscreteValueProbabilityDistribution Distribution
        {
            get
            {
                return diceSum.ProbabilityDistribution;
            }
        }

        private void OnNotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void UpdateForNewSumOfDiceParameters()
        {
            diceSum = new SumOfDice(GenerateDice(numberOfDice, numberOfSides), rng);
        }

        private IEnumerable<NumericalDie> GenerateDice(int numberOfDice, int numberOfSides)
        {
            return Enumerable.Repeat(numberOfSides, numberOfDice)
                .Select(nrOfSides => new NumericalDie(nrOfSides, rng));
        }
    }
}
