using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AriaSharp
{
    public class AriaSharpDownloadOptions // WIP
    {
        /// <summary>
        /// The directory to store the downloaded file.
        /// </summary>
        public string Dir { get; set; }
        /// <summary>
        /// The file name of the downloaded file. It is always relative to the directory given in Dir option.
        /// </summary>
        public string Out { get; set; }
    }
}
