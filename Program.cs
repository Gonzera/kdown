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
                System.Console.WriteLine("4 - Erome Gallery");
                System.Console.WriteLine("5 - Erome Profile");
                System.Console.WriteLine("0 - Leave :(");
                choice = int.Parse(Console.ReadKey(true).KeyChar.ToString());

                switch(choice)
                {
                    case 1:
                        url = GetInput();
                        DownloadModel fourmodel = scrapper.FourChan(url);
                        Downloader.Download(fourmodel);
                        break;
                    case 2:
                        url = GetInput();
                        DownloadModel redditModel = redditScrapper.RedditModel(url, 50);
                        Downloader.Download(redditModel);
                        break;
                    case 3:
                        url = GetInput();
                        DownloadModel caravela = scrapper.Caravela(url);
                        Downloader.Download(caravela);
                        break;
                    case 4:
                        url = GetInput();
                        DownloadModel ero = scrapper.EromeGallery(url);
                        Downloader.Download(ero);
                        break;
                    case 5:
                        url = GetInput();
                        Navigator.EromeProfile(url);
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

        static string GetInput()
        {
            string url;
            System.Console.Write("Enter the target Url: ");
            url = Convert.ToString(Console.ReadLine());
            return url;
        }
    }
}




/*
TODO LIST:
Reddit Gallery Support/subfolders

Url queue/load multiple urls
MultiThreading
Identify website from url
Remove menu and get stuff from parameters 


*/