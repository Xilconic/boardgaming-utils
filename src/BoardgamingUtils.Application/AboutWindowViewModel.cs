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
using System.Reflection;

namespace Xilconic.BoardgamingUtils.Application;

/// <summary>
/// The ViewModel for the About window.
/// </summary>
public class AboutWindowViewModel
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AboutWindowViewModel"/> class.
    /// </summary>
    public AboutWindowViewModel()
    {
        var executingAssembly = Assembly.GetExecutingAssembly();
        Version = executingAssembly.GetName().Version ?? new Version("Unknown");
        var versionInfo = FileVersionInfo.GetVersionInfo(executingAssembly.Location);
        Company = versionInfo.CompanyName ?? "Unknown";
    }

    /// <summary>
    /// Gets the version of the application.
    /// </summary>
    public Version Version
    {
        get;
    }

    /// <summary>
    /// Gets the company of the application.
    /// </summary>
    public string Company
    {
        get;
    }
}