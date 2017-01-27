using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Xilconic.BoardgamingUtils.App
{
    internal class AboutWindowViewModel
    {
        public AboutWindowViewModel()
        {
            var executingAssembly = Assembly.GetExecutingAssembly();
            Version = executingAssembly.GetName().Version;
            var versionInfo = FileVersionInfo.GetVersionInfo(executingAssembly.Location);
            Company = versionInfo.CompanyName;
        }

        public Version Version
        {
            get;
        }

        public string Company
        {
            get;
        }
    }
}
