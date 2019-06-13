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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using Xilconic.BoardgamingUtils.ToolboxApp.Controls;
using Xilconic.BoardgamingUtils.ToolboxApp.Controls.WorkbenchItems;

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

        private void ListViewTextBlock_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var workbenchItemViewModel = (WorkbenchItemViewModel)((TextBlock)sender).DataContext;
                DataObject dragDropData = new DataObject();
                dragDropData.SetData(typeof(WorkbenchItemViewModel), workbenchItemViewModel);
                DragDrop.DoDragDrop(this, dragDropData, DragDropEffects.Copy);
            }
        }

        private void WorkbenchCanvas_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(WorkbenchItemViewModel)))
            {
                WorkbenchItemViewModel workbenchItemViewModel = (WorkbenchItemViewModel)e.Data.GetData(typeof(WorkbenchItemViewModel));
                Point position = e.GetPosition(WorkbenchCanvas);

                CreateViewForWorkbenchItemAndAddToWorkbench(workbenchItemViewModel, position);
            }
        }

        private void CreateViewForWorkbenchItemAndAddToWorkbench(WorkbenchItemViewModel workbenchItem, Point position)
        {
            try
            {
                UserControl view = WorkbenchItemViewFactory.CreateView(workbenchItem);
                Canvas.SetLeft(view, position.X);
                Canvas.SetTop(view, position.Y);
                WorkbenchCanvas.Children.Add(view);
            }
            catch (NotImplementedException)
            {
                MessageBox.Show($"There is no visual representation available yet for the workbench item \"{workbenchItem.Name}\".");
            }
        }

        private void WorkbenchCanvas_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(WorkbenchItemViewModel)))
            {
                e.Effects = DragDropEffects.Copy;
            }
        }
    }
}
