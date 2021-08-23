using System;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace kdown
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu();
        }

        static void Menu()
        {
            var redditScrapper = new RedditScrapper();
            var scrapper = new Scrapper();
            bool exit = false;
            int choice;
            string url;
            while(!exit)
            {
                System.Console.WriteLine("1 - FourChan");
                System.Console.WriteLine("2 - Reddit");
                choice = Convert.ToInt32(Console.ReadKey(true));
                System.Console.WriteLine("Enter the target Url: ");
                url = Convert.ToString(Console.ReadLine());
                switch(choice)
                {
                    case 1:
                        DownloadModel fourmodel = scrapper.FourChan(url);
                        Downloader.Download(fourmodel);
                        break;
                    case 2:
                        DownloadModel redditModel = redditScrapper.RedditModel(url, 50);
                        Downloader.Download(redditModel);
                        break;
                    default:
                        System.Console.WriteLine("1 or 2 :(");
                        break;
                }
            }
        }
    }
}
