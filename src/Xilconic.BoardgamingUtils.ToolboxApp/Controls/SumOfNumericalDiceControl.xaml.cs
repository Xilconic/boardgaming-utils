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
namespace Xilconic.BoardgamingUtils.ToolboxApp.Controls
{
    using System.Windows.Controls;
    using System.Windows.Input;

    /// <summary>
    /// Interaction logic for SumOfNumericalDiceControl.xaml.
    /// </summary>
    public partial class SumOfNumericalDiceControl : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SumOfNumericalDiceControl"/> class.
        /// </summary>
        public SumOfNumericalDiceControl()
        {
            InitializeComponent();
        }

        private static void CommitValueInTexBoxField(TextBox textBox)
        {
            textBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var textBox = (TextBox)sender;
                CommitValueInTexBoxField(textBox);
                e.Handled = true;
            }
        }
    }
}
