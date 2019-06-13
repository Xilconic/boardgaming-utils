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

namespace Xilconic.BoardgamingUtils.ToolboxApp.Controls.WorkbenchItems
{
    /// <summary>
    /// Creates Views for <see cref="WorkbenchItemViewModel"/> implementations.
    /// </summary>
    internal static class WorkbenchItemViewFactory
    {
        /// <summary>
        /// Creates a View based on a <see cref="WorkbenchItemViewModel"/>.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        /// <returns>The view.</returns>
        /// <exception cref="NotImplementedException">Thrown when <paramref name="viewModel"/> is of an unknown type.</exception>
        internal static WorkbenchItemUserControl CreateView(WorkbenchItemViewModel viewModel)
        {
            switch (viewModel)
            {
                case SingleDieWorkbenchItem singleDieWorkbenchItem: return new SingleDieViewItem
                {
                    SingleDieWorkbenchItem = singleDieWorkbenchItem,
                };
                default: throw new NotImplementedException($"No View has been made available for the object of type: {viewModel.GetType().FullName}.");
            }
        }
    }
}
