using System;
using System.IO;

namespace kdown
{
    class Program
    {
        static void Main(string[] args)
        {
            if(!File.Exists(".downloadedUrls.txt"))   //the file used to keep track of what we already downloaded
                File.CreateText(".downloadedUrls.txt");

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
                System.Console.WriteLine("3 - 1500Chan");
                System.Console.WriteLine("0 - Leave :(");
                choice = int.Parse(Console.ReadKey(true).KeyChar.ToString());

                switch(choice)
                {
                    case 1:
                        System.Console.Write("Enter the target Url: ");
                        url = Convert.ToString(Console.ReadLine());
                        DownloadModel fourmodel = scrapper.FourChan(url);
                        Downloader.Download(fourmodel);
                        break;
                    case 2:
                        System.Console.Write("Enter the target Url: ");
                        url = Convert.ToString(Console.ReadLine());
                        DownloadModel redditModel = redditScrapper.RedditModel(url, 50);
                        Downloader.Download(redditModel);
                        break;
                    case 3:
                        System.Console.Write("Enter the target Url: ");
                        url = Convert.ToString(Console.ReadLine());
                        DownloadModel caravela = scrapper.Caravela(url);
                        Downloader.Download(caravela);
                        break;
                    case 0:
                        exit = true;
                        break;
                    default:
                        System.Console.WriteLine("1 or 2 :(");
                        break;
                }
            }
        }
    }
}




/*
TODO LIST:
Reddit Gallery Support/subfolders
Url queue/load multiple urls
1500chan support - Done
MultiThreading

*/