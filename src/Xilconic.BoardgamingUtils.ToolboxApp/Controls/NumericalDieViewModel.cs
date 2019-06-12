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
using System;
using System.ComponentModel;
using System.Diagnostics;

using Xilconic.BoardgamingUtils.Dice;
using Xilconic.BoardgamingUtils.Mathmatics;
using Xilconic.BoardgamingUtils.PseudoRandom;

namespace Xilconic.BoardgamingUtils.ToolboxApp.Controls
{
    /// <summary>
    /// ViewModel for displaying the probability density function (pdf) of a <see cref="NumericalDie"/>.
    /// </summary>
    internal class NumericalDieViewModel : INotifyPropertyChanged
    {
        private readonly RandomNumberGenerator rng = new RandomNumberGenerator(Environment.TickCount);
        private NumericalDie die;

        /// <summary>
        /// Initializes a new instance of the <see cref="NumericalDieViewModel"/> class.
        /// </summary>
        public NumericalDieViewModel()
        {
            NumberOfSides = 6;
        }

        /// <inheritdoc/>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets the number of sides the die has.
        /// </summary>
        public int NumberOfSides
        {
            get
            {
                return die.NumberOfSides;
            }

            set
            {
                Debug.Assert(value > 0, "The number of sides of a die must be 1 or greater.");

                die = new NumericalDie(value, rng);

                OnNotifyPropertyChanged(nameof(NumberOfSides));
                OnNotifyPropertyChanged(nameof(Distribution));
            }
        }

        /// <summary>
        /// Gets the probability distribution of the die.
        /// </summary>
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
