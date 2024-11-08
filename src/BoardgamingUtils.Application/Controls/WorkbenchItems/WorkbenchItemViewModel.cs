﻿// Copyright (c) Bas des Bouvrie ("Xilconic"). All rights reserved.
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

using System.ComponentModel;
using System.Diagnostics;
using Xilconic.BoardgamingUtils.Mathematics;

namespace Xilconic.BoardgamingUtils.Application.Controls.WorkbenchItems;

/// <summary>
/// The ViewModel representing a workbench item.
/// </summary>
public abstract class WorkbenchItemViewModel : INotifyPropertyChanged
{
    /// <summary>
    /// Initializes a new instance of the <see cref="WorkbenchItemViewModel"/> class.
    /// </summary>
    /// <param name="name">The name of the workbench item.</param>
    /// <param name="title">The title of the random variable encapsulated by the workbench item.</param>
    /// <param name="valueName">The name of the random variable encapsulated by the workbench item.</param>
    protected WorkbenchItemViewModel(string name, string title, string valueName)
    {
        Debug.Assert(name != null, "The name of the workbench item cannot be null.");
        Name = name;
        Title = title;
        ValueName = valueName;
    }

    /// <inheritdoc/>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Gets the name of the workbench item.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Gets the probability distribution of the workbench item.
    /// </summary>
    public abstract DiscreteValueProbabilityDistribution? Distribution { get; }

    /// <summary>
    /// Gets the title description of the <see cref="Distribution"/>.
    /// </summary>
    public string Title { get; }

    /// <summary>
    /// Gets the name of the random variable of the <see cref="Distribution"/>.
    /// </summary>
    public string ValueName { get; }

    /// <summary>
    /// Return a deep clone of this instance.
    /// </summary>
    /// <returns>The clone.</returns>
    public abstract WorkbenchItemViewModel DeepClone();

    /// <summary>
    /// Indicate that a property has changed by raising the <see cref="PropertyChanged"/> event.
    /// </summary>
    /// <param name="propertyName">The name of the property.</param>
    protected void NotifyPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}