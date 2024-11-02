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

using System.Windows;
using System.Windows.Controls;
using Xilconic.BoardgamingUtils.Mathematics;

namespace Xilconic.BoardgamingUtils.Application.Controls
{
    /// <summary>
    /// Interaction logic for ProbabilityDensityFunctionChartControl.xaml.
    /// </summary>
    public partial class ProbabilityDensityFunctionChartControl : UserControl
    {
        /// <summary>
        /// Gets the dependency property for the probability distribution displayed in the chart.
        /// </summary>
        public static readonly DependencyProperty DistributionProperty = DependencyProperty.Register(
                nameof(Distribution),
                typeof(DiscreteValueProbabilityDistribution),
                typeof(ProbabilityDensityFunctionChartControl),
                new PropertyMetadata(null, new PropertyChangedCallback(OnDistributionChanged)));

        /// <summary>
        /// Gets the dependency property for the name of the variable displayed in the chart.
        /// </summary>
        public static readonly DependencyProperty ValueNameProperty = DependencyProperty.Register(
            nameof(ValueName),
            typeof(string),
            typeof(ProbabilityDensityFunctionChartControl),
            new PropertyMetadata("Value", new PropertyChangedCallback(OnValueNameChanged)));

        /// <summary>
        /// Gets the dependency property for the title of the chart.
        /// </summary>
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
            nameof(Title),
            typeof(string),
            typeof(ProbabilityDensityFunctionChartControl),
            new PropertyMetadata("Value Probabilities (pdf)", new PropertyChangedCallback(OnTitleChanged)));

        private readonly ProbabilityDensityFunctionChartViewModel viewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProbabilityDensityFunctionChartControl"/> class.
        /// </summary>
        public ProbabilityDensityFunctionChartControl()
        {
            InitializeComponent();
            viewModel = (ProbabilityDensityFunctionChartViewModel)rootGrid.DataContext;
        }

        /// <summary>
        /// Gets or sets the probability distribution displayed in the chart.
        /// </summary>
        public DiscreteValueProbabilityDistribution Distribution
        {
            get => (DiscreteValueProbabilityDistribution)GetValue(DistributionProperty);
            set => SetValue(DistributionProperty, value);
        }

        /// <summary>
        /// Gets or sets the name of the variable displayed in the chart.
        /// </summary>
        public string ValueName
        {
            get => (string)GetValue(ValueNameProperty);
            set => SetValue(ValueNameProperty, value);
        }

        /// <summary>
        /// Gets or sets the title of the chart.
        /// </summary>
        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        private static void OnDistributionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ProbabilityDensityFunctionChartControl)d).viewModel.Distribution = (DiscreteValueProbabilityDistribution)e.NewValue;
        }

        private static void OnValueNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ProbabilityDensityFunctionChartControl)d).viewModel.ValueName = (string)e.NewValue;
        }

        private static void OnTitleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ProbabilityDensityFunctionChartControl)d).viewModel.Title = (string)e.NewValue;
        }
    }
}
