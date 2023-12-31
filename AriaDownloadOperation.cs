﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.System;

namespace AriaSharp
{
    public class AriaDownloadOperation
    {
        private int lastProgress = 0;

        public string OutputPath { get; set; }
        public string Filename { get; set; }

        internal AriaDownloadOperation(Process process, string output, string name = "") 
        {
            if(!string.IsNullOrWhiteSpace(name))
            {
                string outputPath = Path.Combine(output, name);
                OutputPath = outputPath;
            }

            Init(process);
        }


        private async void Init(Process process)
        {
            Status dlStatus = Status.ProcessExited;
            while (!process.HasExited)
            {
                // Read a line from the StandardOutput stream
                string line = await process.StandardOutput.ReadLineAsync();

                if (line != null)
                {
                    // Get the percentage value
                    Regex percentageRegex = new(@"\d{1,3}%"); // will match only if a percentage is present on the string
                    var percentage = percentageRegex.Match(line);

                    if (percentage.Success)
                    {
                        int progress = int.Parse(percentage.Value.Replace("%", ""));
                        if(progress > lastProgress)
                        {
                            DownloadProgressChanged(this, new DownloadProgessChangedEventArgs(progress, Status.Downloading));
                        }
                    }

                    // Get the download state
                    Regex statusRegex = new(@"\([A-Z]{2,3}\):");
                    var status = statusRegex.Match(line);

                    if(status.Success)
                    {
                        string statusString = status.Value;
                        Status? statusEnum = null;

                        switch(statusString)
                        {
                            case "(OK):":
                                statusEnum = Status.Completed;
                                break;

                            case "(ERR):":
                                statusEnum = Status.Error;
                                break;
                        }

                        if(statusEnum != null)
                        {
                            DownloadProgressChanged(this, new DownloadProgessChangedEventArgs(100, (Status)statusEnum));
                            dlStatus = (Status)statusEnum;
                        }
                        
                    }
                }
            }

            DownloadProgressChanged(this, new DownloadProgessChangedEventArgs(0, dlStatus));
        }





        public class DownloadProgessChangedEventArgs
        {
            public DownloadProgessChangedEventArgs(int progress, Status status)
            {
                Progress = progress;
                Status = status;
            }

            public int Progress { get; set; }
            public Status Status { get; set; }
        }

        public delegate void DownloadProgessChangedEventHandler(AriaDownloadOperation sender, DownloadProgessChangedEventArgs args);

        public event DownloadProgessChangedEventHandler DownloadProgressChanged;



        public enum Status
        {
            Downloading,
            Completed,
            Error,
            ProcessExited
        }
    }
}
