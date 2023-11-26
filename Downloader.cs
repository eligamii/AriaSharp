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
    public static class Downloader
    {
        /// <summary>
        /// Download a file using Aria2
        /// </summary>
        /// <param name="url">The source url of the file to download</param>
        /// <param name="outputDir">The outputDir folder</param>
        /// <param name="name">The name of the file to download</param>
        /// <returns>An AriaDownloadOperation containing the information of the download progress</returns>
        public static AriaDownloadOperation Download(string url, string outputDir, string name) 
        {
            AriaDownloadOptions options = new();
            options.Url.Value = url;
            options.Dir.Value = outputDir;
            options.Out.Value = name;

            return Download(options);

        }

        /// <summary>
        /// Download a file using Aria2
        /// </summary>
        /// <param name="url">The source url of the file to download</param>
        /// <param name="outputDir">The outputDir folder</param>
        /// <returns>An AriaDownloadOperation containing the information of the download progress</returns>
        public static AriaDownloadOperation Download(string url, string outputDir)
        {
            AriaDownloadOptions options = new();
            options.Url.Value = url;
            options.Dir.Value = outputDir;

            return Download(options);
            
        }

        /// <summary>
        /// Download a file using Aria2
        /// </summary>
        /// <param name="options">The options of the download</param>
        /// <returns>An AriaDownloadOperation containing the information of the download progress</returns>
        public static AriaDownloadOperation Download(AriaDownloadOptions options)
        {
            string aria2cPath = Path.Combine(Windows.ApplicationModel.Package.Current.InstalledLocation.Path, "AriaSharp", "Binary", "aria2c.exe");

            Process process = new();

            ProcessStartInfo startInfo = new ProcessStartInfo()
            {
                FileName = aria2cPath,
                Arguments = options.AsCommandLineArguments(),
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                UseShellExecute = false,
            };

            process.StartInfo = startInfo;
            process.Start();

            return new AriaDownloadOperation(process, options.Out.Value.ToString());
        }
    }
}
