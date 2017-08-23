using CommandLine;
using CommandLine.Text;
using System;
using YourNichijou.Module;
using YourNichijou.Util;

namespace YourNichijou
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<EmptyFolderRemoverOptions, WebWatcherOptions>(args)
                .MapResult
                (
                    (EmptyFolderRemoverOptions opts) =>
                    {
                        new EmptyFolderRemover() { Path = opts.Path }.Run();
                        return 0;
                    },
                    (WebWatcherOptions opts) =>
                    {
                        if (opts.Urls != null)
                            new WebWatcher() { Urls = opts.Urls }.Run();
                        else if (opts.SettingsFilePath != null)
                            new WebWatcher() { SettingsFilePath = opts.SettingsFilePath }.Run();
                        else
                            Logger.WriteException(new Exception("need at least a parameter"));
                        return 0;
                    },
                    errs => 1
                );
        }
    }
}
