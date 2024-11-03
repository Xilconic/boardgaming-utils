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

namespace Xilconic.BoardgamingUtils.Application.Controls.WorkbenchItems;

/// <summary>
/// A user control for displaying information encapsulated by an instance of <see cref="WorkbenchItemViewModel"/>.
/// </summary>
public class WorkbenchItemUserControl : UserControl
{
    /// <summary>
    /// Gets the dependency property of the Workbench item.
    /// </summary>
    public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register(
        nameof(IsSelected),
        typeof(bool),
        typeof(WorkbenchItemUserControl),
        new UIPropertyMetadata(false));

    /// <summary>
    /// Gets or sets a value indicating whether this control is selected in the workbench area.
    /// </summary>
    public bool IsSelected
    {
        get => (bool)GetValue(IsSelectedProperty);
        set => SetValue(IsSelectedProperty, value);
    }

    /// <summary>
    /// Gets the displayed <see cref="WorkbenchItemViewModel"/>.
    /// </summary>
    internal virtual WorkbenchItemViewModel WorkbenchItem { get; }
}