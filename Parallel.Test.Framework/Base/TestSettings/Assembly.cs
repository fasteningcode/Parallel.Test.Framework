using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parallel.Test.Framework.Base.TestSettings
{
    public class Assembly
    {
        public static string Directory
        {
            get
            {
                var codeBase = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
                var uri = new UriBuilder(codeBase);
                var path = Uri.UnescapeDataString(uri.Path);
                path = Path.GetDirectoryName(path);
                // ReSharper disable once PossibleNullReferenceException
                path = path.Replace("\\bin\\Release", ""); // Change it to the folder that you want. 
                path = path.Replace("\\bin\\Debug", "");
                path = path.Replace("\\bin\\Dev", "");
                path = path.Replace("\\bin\\x86\\Release", "");
                path = path.Replace("\\bin\\x86\\Debug", "");
                path = path.Replace("\\bin\\x86\\Dev", "");

                path = path.Replace("/bin/Release", ""); // Change it to the folder that you want. 
                path = path.Replace("/bin/Debug", "");
                path = path.Replace("/bin/Dev", "");
                path = path.Replace("/bin/x86/Release", "");
                path = path.Replace("/bin/x86/Debug", "");
                path = path.Replace("/bin/x86/Dev", "");


                path = path.Replace("\\", "/");

                return path;
            }
        }
    }
}
