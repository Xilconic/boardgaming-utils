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
        /// Gets the dependency property of the selected Workbench item.
        /// </summary>
        public static readonly DependencyProperty SelectedWorkbenchItemProperty = DependencyProperty.Register(
            nameof(SelectedWorkbenchItem),
            typeof(WorkbenchItemViewModel),
            typeof(MainWindow),
            new UIPropertyMetadata(new NullWorkbenchItemViewModel()));

        private WorkbenchItemUserControl selectedWorkbenchItemUserControl;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the selected <see cref="WorkbenchItemViewModel"/> in the workbench.
        /// </summary>
        internal WorkbenchItemViewModel SelectedWorkbenchItem
        {
            get
            {
                return (WorkbenchItemViewModel)GetValue(SelectedWorkbenchItemProperty);
            }

            set
            {
                SetValue(SelectedWorkbenchItemProperty, value);
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var aboutWindow = new AboutWindow();
            aboutWindow.ShowDialog();
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
                WorkbenchItemViewModel workbenchItemViewModel = ((WorkbenchItemViewModel)e.Data.GetData(typeof(WorkbenchItemViewModel))).DeepClone();
                Point position = e.GetPosition(WorkbenchCanvas);

                WorkbenchItemUserControl createdView = CreateViewForWorkbenchItemAndAddToWorkbench(workbenchItemViewModel, position);
                if (createdView != null)
                {
                    SelectWorkbenchItem(workbenchItemViewModel, createdView);
                }
            }
        }

        private WorkbenchItemUserControl CreateViewForWorkbenchItemAndAddToWorkbench(WorkbenchItemViewModel workbenchItem, Point position)
        {
            try
            {
                WorkbenchItemUserControl view = WorkbenchItemViewFactory.CreateView(workbenchItem);
                Canvas.SetLeft(view, position.X);
                Canvas.SetTop(view, position.Y);
                WorkbenchCanvas.Children.Add(view);

                return view;
            }
            catch (NotImplementedException)
            {
                MessageBox.Show($"There is no visual representation available yet for the workbench item \"{workbenchItem.Name}\".");
                return null;
            }
        }

        private void WorkbenchCanvas_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(WorkbenchItemViewModel)))
            {
                e.Effects = DragDropEffects.Copy;
            }
        }

        private void WorkbenchCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var workbenchItemUserControl = e.Source as WorkbenchItemUserControl;
            if (workbenchItemUserControl != null)
            {
                // TODO: Implement some kind of selection highlight
                SelectWorkbenchItem(workbenchItemUserControl.WorkbenchItem, workbenchItemUserControl);
            }
        }

        private void SelectWorkbenchItem(WorkbenchItemViewModel workbenchItem, WorkbenchItemUserControl correspondingView)
        {
            SelectedWorkbenchItem = workbenchItem;

            if (selectedWorkbenchItemUserControl != null)
            {
                selectedWorkbenchItemUserControl.IsSelected = false;
            }

            selectedWorkbenchItemUserControl = correspondingView;
            if (selectedWorkbenchItemUserControl != null)
            {
                selectedWorkbenchItemUserControl.IsSelected = true;
            }
        }

        private void ClearWorkbenchSelection()
        {
            SelectWorkbenchItem((WorkbenchItemViewModel)SelectedWorkbenchItemProperty.DefaultMetadata.DefaultValue, null);
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete && selectedWorkbenchItemUserControl != null)
            {
                WorkbenchCanvas.Children.Remove(selectedWorkbenchItemUserControl);
                ClearWorkbenchSelection();
            }
        }
    }
}
