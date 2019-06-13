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

using System.Diagnostics;

namespace Xilconic.BoardgamingUtils.ToolboxApp
{
    /// <summary>
    /// The ViewModel representing a workbench item.
    /// </summary>
    internal class WorkbenchItemViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WorkbenchItemViewModel"/> class.
        /// </summary>
        /// <param name="name">The name of the workbench item.</param>
        public WorkbenchItemViewModel(string name)
        {
            Debug.Assert(name != null, "The name of the workbench item cannot be null.");
            Name = name;
        }

        /// <summary>
        /// Gets the name of the workbench item.
        /// </summary>
        public string Name { get; }
    }
}
