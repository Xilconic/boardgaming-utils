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
using System.Collections.Generic;

using Xilconic.BoardgamingUtils.PseudoRandom;
using Xilconic.BoardgamingUtils.ToolboxApp.Controls.WorkbenchItems;

namespace Xilconic.BoardgamingUtils.ToolboxApp
{
    /// <summary>
    /// The ViewModel for <see cref="MainWindow"/>.
    /// </summary>
    internal class MainWindowViewModel
    {
        private readonly IRandomNumberGenerator rng = new RandomNumberGenerator((int)DateTime.Now.Ticks & 0x0000FFFF);

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.
        /// </summary>
        public MainWindowViewModel()
        {
            WorkbenchItems = new WorkbenchItemViewModel[] { new SingleDieWorkbenchItem(rng) };
        }

        /// <summary>
        /// Gets the workbench items available within the application.
        /// </summary>
        public IReadOnlyList<WorkbenchItemViewModel> WorkbenchItems { get; }
    }
}
