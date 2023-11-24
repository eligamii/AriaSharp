using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.Storage;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace AriaSharp
{
    public static class AriaSharpDownloader
    {
        /// <summary>
        /// Download a file using Aria2
        /// </summary>
        /// <param name="url">The source url of the file to download</param>
        /// <param name="output">The output folder</param>
        /// <param name="name">The name of the file to download</param>
        /// <returns>An AriaDownload containing the information of the download progress</returns>
        public static AriaDownload Download(string url, string output, string name) 
        {
            string aria2cPath = Path.Combine(Windows.ApplicationModel.Package.Current.InstalledLocation.Path, "AriaSharp", "Binary", "aria2c.exe");

            Process process = new();

            ProcessStartInfo startInfo = new ProcessStartInfo()
            {
                FileName = aria2cPath,
                ArgumentList = { "-d " + output, "-o " + name, url },
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                UseShellExecute = false,
            };


            process.StartInfo = startInfo;
            process.Start();

            return new AriaDownload(process, output, name);

        }

        public static AriaDownload Download(string url, string output)
        {
            string aria2cPath = Path.Combine(Windows.ApplicationModel.Package.Current.InstalledLocation.Path, "AriaSharp", "Binary", "aria2c.exe");

            Process process = new();

            ProcessStartInfo startInfo = new ProcessStartInfo()
            {
                FileName = aria2cPath,
                ArgumentList = { "-d " + output, "-o ", url },
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                UseShellExecute = false,
            };


            process.StartInfo = startInfo;
            process.Start();

            return new AriaDownload(process, output);
        }
    }
}
