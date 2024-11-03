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
using System.Windows.Input;
using Xilconic.BoardgamingUtils.Application.Controls.WorkbenchItems;
using Xilconic.BoardgamingUtils.ToolboxApp.Controls.WorkbenchItems;

namespace Xilconic.BoardgamingUtils.Application.Controls;

public partial class WorkbenchControl : UserControl
{
    /// <summary>
    /// Gets the dependency property of the selected Workbench item.
    /// </summary>
    public static readonly DependencyProperty SelectedWorkbenchItemProperty = DependencyProperty.Register(
        nameof(SelectedWorkbenchItem),
        typeof(WorkbenchItemViewModel),
        typeof(WorkbenchControl),
        new UIPropertyMetadata(new NullWorkbenchItemViewModel()));
    
    private WorkbenchItemUserControl? _selectedWorkbenchItemUserControl;
    
    public WorkbenchControl()
    {
        InitializeComponent();
    }
    
    /// <summary>
    /// Gets or sets the selected <see cref="WorkbenchItemViewModel"/> in the workbench.
    /// </summary>
    internal WorkbenchItemViewModel SelectedWorkbenchItem
    {
        get => (WorkbenchItemViewModel)GetValue(SelectedWorkbenchItemProperty);
        set => SetValue(SelectedWorkbenchItemProperty, value);
    }
    
    private void ListViewTextBlock_MouseMove(object sender, MouseEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed)
        {
            var textBlock = (TextBlock)sender;
            var viewModel = (WorkbenchItemViewModel)textBlock.DataContext;
            
            var dragDropData = new DataObject();
            dragDropData.SetData(typeof(WorkbenchItemViewModel), viewModel);
            
            DragDrop.DoDragDrop(this, dragDropData, DragDropEffects.Copy);
        }
    }
    
    private void WorkbenchCanvas_Drop(object sender, DragEventArgs e)
    {
        if (e.Data.GetDataPresent(typeof(WorkbenchItemViewModel)))
        {
            WorkbenchItemViewModel templateViewModel = (WorkbenchItemViewModel)e.Data.GetData(typeof(WorkbenchItemViewModel))!;
            WorkbenchItemViewModel workbenchItemViewModel = templateViewModel.DeepClone();
            Point position = e.GetPosition(WorkbenchCanvas);

            WorkbenchItemUserControl? createdView = CreateViewForWorkbenchItemAndAddToWorkbench(workbenchItemViewModel, position);
            if (createdView != null)
            {
                SelectWorkbenchItem(workbenchItemViewModel, createdView);
            }
        }
    }
    
    private WorkbenchItemUserControl? CreateViewForWorkbenchItemAndAddToWorkbench(WorkbenchItemViewModel workbenchItem, Point position)
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

#pragma warning disable S2325
    private void WorkbenchCanvasDragOver(object sender, DragEventArgs e)
#pragma warning restore S2325
    {
        if (e.Data.GetDataPresent(typeof(WorkbenchItemViewModel)))
        {
            e.Effects = DragDropEffects.Copy;
        }
    }
    
    private void WorkbenchCanvas_MouseUp(object sender, MouseButtonEventArgs e)
    {
        if (e.Source is WorkbenchItemUserControl workbenchItemUserControl)
        {
            // Implement some kind of selection highlight
            SelectWorkbenchItem(workbenchItemUserControl.WorkbenchItem, workbenchItemUserControl);
        }
    }

    private void SelectWorkbenchItem(WorkbenchItemViewModel workbenchItem, WorkbenchItemUserControl? correspondingView)
    {
        SelectedWorkbenchItem = workbenchItem;

        if (_selectedWorkbenchItemUserControl != null)
        {
            _selectedWorkbenchItemUserControl.IsSelected = false;
        }

        _selectedWorkbenchItemUserControl = correspondingView;
        if (_selectedWorkbenchItemUserControl != null)
        {
            _selectedWorkbenchItemUserControl.IsSelected = true;
        }
    }

    private void ClearWorkbenchSelection()
    {
        SelectWorkbenchItem((WorkbenchItemViewModel)SelectedWorkbenchItemProperty.DefaultMetadata.DefaultValue, null);
    }

    private void WindowKeyUp(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Delete && _selectedWorkbenchItemUserControl != null)
        {
            WorkbenchCanvas.Children.Remove(_selectedWorkbenchItemUserControl);
            ClearWorkbenchSelection();
        }
    }
}