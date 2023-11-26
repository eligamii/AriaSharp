using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AriaSharp
{
    public class AriaDownloadOption
    {
        internal AriaDownloadOption(Type type, string optString, object value = null) 
        {
            _value = value;
            _valueType = type;
            _optionString = optString;
        }

        private object _value = null;
        private Type _valueType = typeof(object);
        private string _optionString = string.Empty;
        public object Value 
        {
            get { return _value; }
            set
            {
                if(value != null) 
                {
                    if (value.GetType() == _valueType)
                    {
                        _value = value;
                    }
                    else
                    {
                        throw new Exception($"The type '{value.GetType()}' is not supported by this option.\nThe value should be in type '{_valueType}'.\nSee more at https://aria2.github.io/manual/en/html/aria2c.html#cmdoption{_optionString.Replace("--", "-")}");
                    }
                }
            }
        }

        /// <summary>
        /// Convert the AriaDownloadOption to a string in the format '--option value '
        /// </summary>
        /// <example>
        /// <code>
        /// AriaSharpdownloadOptions options = new() { Dir.Value =  }
        /// AriaSharpdownloadOptions.Dir.AsCommandLineArgument();
        /// </code>
        /// </example>
        public string AsCommandLineArgument()
        {
            return Value != null ? $"{_optionString} {Value.ToString().ToLower()} " : string.Empty;
        }
    }


    public class AriaDownloadOptions // WIP
    {
        public string AsCommandLineArguments()
        {
            var properties = typeof(AriaDownloadOptions).GetProperties();
            string optionsString = string.Empty;

            foreach(var property in properties)
            {
                AriaDownloadOption option = (AriaDownloadOption)property.GetValue(this, null);
                optionsString += option.AsCommandLineArgument();
            }

            return optionsString;
        }

        /// <summary>
        /// The url of the file to downlaod
        /// </summary>
        public AriaDownloadOption Url { get; set; } = new(typeof(string), "");

        /// <summary>
        /// The directory to store the downloaded files 
        /// </summary>
        public AriaDownloadOption Dir { get; set; } = new(typeof(string), "--dir");
        /// <summary>
        /// The file name of the downloaded file
        /// </summary>
        public AriaDownloadOption Out { get; set; } = new(typeof(string), "--out");
        /// <summary>
        /// The file name of the log file
        /// </summary>
        public AriaDownloadOption Log { get; set; } = new(typeof(string), "--log");
        /// <summary>
        /// Set the maximum number of parallel downloads for every queue item (Default is 5)
        /// </summary>
        public AriaDownloadOption MaxConcurentDownloads { get; set; } = new(typeof(int), "--max-concurrent-downloads", 5);
        /// <summary>
        /// Continue downloading a partially downloaded file. Use this option to resume a download started by a web browser or another program which downloads files sequentially from the beginning.
        /// </summary>
        public AriaDownloadOption Continue { get; set; } = new(typeof(bool), "--continue");
        /// <summary>
        /// Set number of tries. (O means unlimited and default is 5)
        /// </summary>
        public AriaDownloadOption MaxTries { get; set; } = new(typeof(int), "--max-tries", 5);



    }
}
