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

using Xilconic.BoardgamingUtils.ToolboxApp.Controls;

namespace Xilconic.BoardgamingUtils.ToolboxApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml.
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var aboutWindow = new AboutWindow();
            aboutWindow.ShowDialog();
        }

        private void Change_to_single_die_statistics_mode(object sender, RoutedEventArgs e)
        {
            panel.Children.Clear();
            panel.Children.Add(new NumericalDieControl());
        }

        private void Change_to_sum_of_dice_statistics_mode(object sender, RoutedEventArgs e)
        {
            panel.Children.Clear();
            panel.Children.Add(new SumOfNumericalDiceControl());
        }
    }
}
