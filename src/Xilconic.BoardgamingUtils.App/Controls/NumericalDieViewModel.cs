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
using Xilconic.BoardgamingUtils.Dice;
using Xilconic.BoardgamingUtils.PseudoRandom;
using Xilconic.BoardgamingUtils.Mathmatics;
using System.ComponentModel;
using System.Diagnostics;

namespace Xilconic.BoardgamingUtils.App.Controls
{
    /// <summary>
    /// ViewModel for displaying the probability density function (pdf) of a <see cref="NumericalDie"/>.
    /// </summary>
    internal class NumericalDieViewModel : INotifyPropertyChanged
    {
        private readonly RandomNumberGenerator rng = new RandomNumberGenerator(Environment.TickCount);
        private NumericalDie die;

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Initializes a new instance of <see cref="NumericalDieViewModel"/>.
        /// </summary>
        public NumericalDieViewModel()
        {
            NumberOfSides = 6;
        }

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
                Debug.Assert(value > 0);

                die = new NumericalDie(value, rng);

                OnNotifyPropertyChanged(nameof(NumberOfSides));
                OnNotifyPropertyChanged(nameof(Distribution));
            }
        }

        public DiscreteValueProbabilityDistribution Distribution
        {
            get
            {
                return die.ProbabilityDistribution;
            }
        }

        private void OnNotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
