﻿// This file is part of Boardgaming Utils.
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
using OxyPlot;
using System.Diagnostics.Contracts;
using System.Drawing;

namespace Xilconic.BoardgamingUtils.App.Utils
{
    /// <summary>
    /// Extension methods for <see cref="Color"/>.
    /// </summary>
    internal static class ColorExtensions
    {
        /// <summary>
        /// Converts a <see cref="Color"/> instance into a oxyplot compliant color.
        /// </summary>
        /// <param name="color">The color to convert.</param>
        /// <returns>The converted color.</returns>
        [Pure]
        internal static OxyColor ToOxyColor(this Color color)
        {
            return OxyColor.FromArgb(color.A, color.R, color.G, color.B);
        }
    }
}