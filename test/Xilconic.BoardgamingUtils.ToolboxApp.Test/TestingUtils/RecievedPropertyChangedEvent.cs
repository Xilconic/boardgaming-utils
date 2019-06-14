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
namespace Xilconic.BoardgamingUtils.ToolboxApp.Test.TestingUtils
{
    /// <summary>
    /// Holds data received from <see cref="INotifyPropertyChanged.PropertyChanged"/> events.
    /// </summary>
    internal class RecievedPropertyChangedEvent
    {
        /// <summary>
        /// Gets or sets the sender of the event.
        /// </summary>
        public object Sender { get; set; }

        /// <summary>
        /// Gets or sets the name of the changed property.
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// Gets or sets the number of times the event was fired.
        /// </summary>
        public int CallCount { get; set; }
    }
}