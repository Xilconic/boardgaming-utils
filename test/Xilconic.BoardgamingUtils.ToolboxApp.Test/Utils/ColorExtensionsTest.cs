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
namespace Xilconic.BoardgamingUtils.ToolboxApp.Test.Utils
{
    using System.Collections.Generic;
    using System.Drawing;
    using NUnit.Framework;
    using OxyPlot;
    using Xilconic.BoardgamingUtils.ToolboxApp.Utils;

    [TestFixture]
    public class ColorExtensionsTest
    {
        private static IEnumerable<Color> Colors
        {
            get
            {
                yield return Color.AliceBlue;
                yield return Color.Transparent;
                yield return Color.DarkOrchid;
                yield return Color.LightGreen;
                yield return Color.Sienna;
            }
        }

        [TestCaseSource(nameof(Colors))]
        public void ToOxyColor_VariousColors_PropertyConverted(Color color)
        {
            // Call
            OxyColor oxyColor = color.ToOxyColor();

            // Assert
            Assert.AreEqual(color.A, oxyColor.A);
            Assert.AreEqual(color.R, oxyColor.R);
            Assert.AreEqual(color.G, oxyColor.G);
            Assert.AreEqual(color.B, oxyColor.B);
        }
    }
}
