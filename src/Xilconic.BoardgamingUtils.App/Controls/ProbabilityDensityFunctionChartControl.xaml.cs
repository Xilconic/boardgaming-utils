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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Xilconic.BoardgamingUtils.Mathmatics;

namespace Xilconic.BoardgamingUtils.App.Controls
{
    /// <summary>
    /// Interaction logic for ProbabilityDensityFunctionChartControl.xaml
    /// </summary>
    public partial class ProbabilityDensityFunctionChartControl : UserControl
    {
        private readonly ProbabilityDensityFunctionChartViewModel viewModel;

        public static readonly DependencyProperty DistributionProperty = 
            DependencyProperty.Register(nameof(Distribution), typeof(DiscreteValueProbabilityDistribution), 
                                        typeof(ProbabilityDensityFunctionChartControl),
                                        new PropertyMetadata(null, new PropertyChangedCallback(OnDistributionChanged)));

        private static void OnDistributionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ProbabilityDensityFunctionChartControl)d).viewModel.Distribution = (DiscreteValueProbabilityDistribution)e.NewValue;
        }

        public static readonly DependencyProperty ValueNameProperty =
            DependencyProperty.Register(nameof(ValueName), typeof(string),
                                        typeof(ProbabilityDensityFunctionChartControl),
                                        new PropertyMetadata("Value", new PropertyChangedCallback(OnValueNameChanged)));

        private static void OnValueNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ProbabilityDensityFunctionChartControl)d).viewModel.ValueName = (string)e.NewValue;
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(nameof(Title), typeof(string),
                                        typeof(ProbabilityDensityFunctionChartControl),
                                        new PropertyMetadata("Value Probabilities (pdf)", new PropertyChangedCallback(OnTitleChanged)));

        private static void OnTitleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ProbabilityDensityFunctionChartControl)d).viewModel.Title = (string)e.NewValue;
        }

        public ProbabilityDensityFunctionChartControl()
        {
            InitializeComponent();
            viewModel = (ProbabilityDensityFunctionChartViewModel)rootGrid.DataContext;
        }

        public DiscreteValueProbabilityDistribution Distribution
        {
            get
            {
                return (DiscreteValueProbabilityDistribution)GetValue(DistributionProperty);
            }
            set
            {
                SetValue(DistributionProperty, value);
            }
        }

        public string ValueName
        {
            get
            {
                return (string)GetValue(ValueNameProperty);
            }
            set
            {
                SetValue(ValueNameProperty, value);
            }
        }

        public string Title
        {
            get
            {
                return (string)GetValue(TitleProperty);
            }
            set
            {
                SetValue(TitleProperty, value);
            }
        }
    }
}
