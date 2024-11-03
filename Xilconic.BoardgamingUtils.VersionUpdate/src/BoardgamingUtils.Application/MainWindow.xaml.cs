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
using Xilconic.BoardgamingUtils.Application.Controls;

namespace Xilconic.BoardgamingUtils.Application;

/// <summary>
/// Interaction logic for MainWindow.xaml
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

// S2325 states private methods that do not use local state should be static, but static methods cannot be databound as click handler for buttons.
#pragma warning disable S2325 
    private void MenuItemClick(object sender, RoutedEventArgs e)
#pragma warning restore S2325
    {
        var aboutWindow = new AboutWindow();
        aboutWindow.ShowDialog();
    }

    private void ChangeMainPanelToSumOfDiceStatistics(object sender, RoutedEventArgs e)
    {
        MainPanel.Children.Clear();
        MainPanel.Children.Add(new SumOfNumericalDiceControl());
    }

    private void ChangeMainPanelToWorkbench(object sender, RoutedEventArgs e)
    {
        MainPanel.Children.Clear();
        MainPanel.Children.Add(new WorkbenchControl());
    }
}