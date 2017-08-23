using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace YourNichijou.Util
{
    [Verb("emptyfolder", HelpText = "Remove all empty folder")]
    class EmptyFolderRemoverOptions
    {
        [Option('p', "path", Required = true, HelpText = "Path to check")]
        public string Path { get; set; }
    }

    [Verb("webwatcher", HelpText = "Watch your web connection")]
    class WebWatcherOptions
    {
        [Option('u', "urls", HelpText = "Urls to check")]
        public IEnumerable<string> Urls { get; set; }

        [Option('s', "settings", HelpText = "Json file to specific urls")]
        public string SettingsFilePath { get; set; }
    }
}
