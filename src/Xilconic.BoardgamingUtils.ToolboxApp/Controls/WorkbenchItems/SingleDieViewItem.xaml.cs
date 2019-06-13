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

namespace Xilconic.BoardgamingUtils.ToolboxApp.Controls.WorkbenchItems
{
    /// <summary>
    /// Interaction logic for SingleDieViewItem.xaml.
    /// </summary>
    public partial class SingleDieViewItem : WorkbenchItemUserControl
    {
        /// <summary>
        /// Gets the dependency property of the Workbench item.
        /// </summary>
        public static readonly DependencyProperty SingleDieWorkbenchItemProperty = DependencyProperty.Register(
            nameof(SingleDieWorkbenchItem),
            typeof(SingleDieWorkbenchItem),
            typeof(SingleDieViewItem));

        /// <summary>
        /// Initializes a new instance of the <see cref="SingleDieViewItem"/> class.
        /// </summary>
        public SingleDieViewItem()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the workbench item.
        /// </summary>
        internal SingleDieWorkbenchItem SingleDieWorkbenchItem
        {
            get
            {
                return (SingleDieWorkbenchItem)GetValue(SingleDieWorkbenchItemProperty);
            }

            set
            {
                // TODO: Implement behavior similar to NumericalDieControl (Committing values, validation, etc)
                SetValue(SingleDieWorkbenchItemProperty, value);
            }
        }

        /// <inheritdoc/>
        internal override WorkbenchItemViewModel WorkbenchItem => SingleDieWorkbenchItem;
    }
}
