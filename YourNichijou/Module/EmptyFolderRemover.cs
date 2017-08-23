using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using YourNichijou.Util;

namespace YourNichijou.Module
{
    class EmptyFolderRemover
    {
        public string Path { get; set; }

        public void Run()
        {
            RemoveEmptyFolder(Path);
            
            Console.WriteLine("Enjoy your day!");
            Console.ReadLine();
        }

        private void RemoveEmptyFolder(string path)
        {
            var currDir = new DirectoryInfo(path);
            RemoveEmptyFolderRecurse(currDir);
            Console.WriteLine("Done.");
        }

        private void RemoveEmptyFolderRecurse(DirectoryInfo directory)
        {
            try
            {
                var subDirs = directory.GetDirectories("*.*", SearchOption.TopDirectoryOnly);
                if (subDirs.Length != 0)
                {
                    foreach (var subDir in subDirs)
                    {
                        RemoveEmptyFolderRecurse(subDir);
                    }
                }

                //whether is the deepest folder or not, it both needs check empty:
                //for deepest folder, check if it needs to delete
                //for not deepest folder, after all sub folders checked, check the top folder
                if (IsFolderEmpty(directory))
                {
                    try
                    {
                        Console.WriteLine($"Removing {directory.FullName}");
                        directory.Delete(false);
                    }
                    catch (Exception e)
                    {
                        Logger.WriteException(e);
                    }
                }
            }
            catch (Exception e)
            {
                Logger.WriteException(e);
            }
        }

        private bool IsFolderEmpty(DirectoryInfo directory)
        {
            FileSystemInfo[] subFiles = directory.GetFileSystemInfos();
            if (subFiles.Length == 0)
            {
                return true;
            }
            return false;
        }
    }
}
