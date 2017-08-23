using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text;
using YourNichijou.Util;

namespace YourNichijou.Module
{
    class WebWatcher
    {
        public string SettingsFilePath { get; set; }
        public IEnumerable<string> Urls { get; set; }

        private IPAddress GetIpByDomain(string hostname)
        {
            try
            {
                IPHostEntry hostInfo = Dns.GetHostEntry(hostname);
                IPAddress[] IPAddr = hostInfo.AddressList;
                return IPAddr[0];
            }
            catch (Exception e)
            {
                Logger.WriteException(e);
                throw;
            }
        }

        public void Run()
        {
            if (SettingsFilePath is string)
            {
                throw new NotImplementedException();
            }
            else
            {
                foreach (var url in Urls)
                {
                    Console.Write($"hostname={url} ip={GetIpByDomain(url)} time=");
                    try
                    {
                        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://" + url + "/");
                        request.Timeout = 10000;

                        Stopwatch timer = new Stopwatch();
                        timer.Start();

                        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                        response.Close();

                        timer.Stop();
                        TimeSpan timeSpan = timer.Elapsed;
                        double time = timeSpan.TotalMilliseconds;

                        Console.WriteLine($"{time}ms");
                    }
                    catch (WebException e) when (e.Status == WebExceptionStatus.Timeout)
                    {
                        Console.WriteLine("timeout");
                    }
                    catch (WebException e) when (e.Status == WebExceptionStatus.ReceiveFailure)
                    {
                        Console.WriteLine("connection reset");
                    }
                    catch (WebException e)
                    {
                        Console.WriteLine("failed");
                    }

                }
            }
        }
    }
}
