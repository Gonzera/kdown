using System.Net;
using System.IO;
using System;
using System.Linq; 

namespace kdown
{
    public static class Downloader
    {
        public static void Download(DownloadModel model)
        {
            var web = new WebClient();
            string fullPath;
            string path = model.website + "/" + model.folder.Replace("/", ""); 
            if(!Directory.Exists(path))
                Directory.CreateDirectory(path);

            for(int i = 0; i < model.urls.Count; i++)
            {
                if(model.urls[i] != null && !bDownloaded(model.urls[i]))
                {
                    if(model.website == "Reddit")
                        model.urls[i] = model.urls[i].Replace("amp;", ""); //weird bug with reddit previews url
                    if(model.filename.Count > 0) //i'm lazy so in some ocasions we won't have filenames from the download model
                        fullPath = path  + "/" + model.filename[i];
                    else
                        fullPath = path + "/" + coolFilename(model.urls[i]); //in this case we just get the filename from the url 
                    System.Console.WriteLine("Downloading: {0}", model.urls[i]);
                    web.DownloadFile(model.urls[i], fullPath);
                    LogUrl(model.urls[i]);
                }
            }
        }
        public static void LogUrl(string url)
        {
            string path = ".downloadedUrls.txt";
            File.AppendAllText(path, url + Environment.NewLine);
        }

        private static bool bDownloaded(string url)
        {
            var log = File.ReadAllLines(".downloadedUrls.txt");
            if(log.Contains(url)) //i <3 linq
                return true;
            return false;
        }

        private static string coolFilename(string url)
        {
            string filename = url.Substring(url.LastIndexOf('/') + 1);

            return filename;
        }
    }
}