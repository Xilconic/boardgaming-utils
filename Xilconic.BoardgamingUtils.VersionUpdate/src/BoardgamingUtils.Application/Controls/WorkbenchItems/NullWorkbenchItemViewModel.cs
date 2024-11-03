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

using Xilconic.BoardgamingUtils.Mathematics;

namespace Xilconic.BoardgamingUtils.Application.Controls.WorkbenchItems;

/// <summary>
/// An instance of <see cref="WorkbenchItemViewModel"/> representing a 'nothing'.
/// </summary>
internal sealed class NullWorkbenchItemViewModel : WorkbenchItemViewModel
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NullWorkbenchItemViewModel"/> class.
    /// </summary>
    public NullWorkbenchItemViewModel()
        : base(string.Empty, "<no workbench item selected>", "<no workbench item selected>")
    {
    }

    /// <inheritdoc/>
    public override DiscreteValueProbabilityDistribution? Distribution => null;

    /// <inheritdoc/>
    public override WorkbenchItemViewModel DeepClone()
    {
        throw new NotImplementedException();
    }
}