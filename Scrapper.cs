using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;


namespace kdown
{
    public class Scrapper
    {
        public DownloadModel FourChan(string target)
        {
            var model = new DownloadModel(){urls = new List<string>(), filename = new List<string>()};
            var html = new HtmlWeb().Load(target);
            var urls = html.DocumentNode.Descendants("a").Where(a => a.ParentNode.HasClass("fileText")).Select(s => s.GetAttributeValue("href", null)).ToArray();
            var fileName = html.DocumentNode.Descendants("a").Where(a => a.ParentNode.HasClass("fileText")).Select(e => e.InnerText).ToArray();
            string boardName = html.DocumentNode.Descendants("div").Where(d => d.HasClass("boardTitle")).FirstOrDefault().InnerText;
            string threadId = html.DocumentNode.Descendants("div").Where(d => d.HasClass("thread")).FirstOrDefault().Id;
            for(int i = 0;i < urls.Length;i++)
            {
                model.urls.Add("https:" + urls[i]);
                if(fileName[i] != null) //im not too sure if we will get a filename everytime from the html
                    model.filename.Add(fileName[i]);
            }
            model.website = "FourChan";
            model.folder = boardName + "/" + threadId;
            return model;
        }
    
        public DownloadModel Caravela(string target)
        {
            var model = new DownloadModel(){urls = new List<string>(), filename = new List<string>()};
            var html = new HtmlWeb().Load(target);
            var urls = html.DocumentNode.Descendants("a").Where(a => a.ParentNode.HasClass("fileinfo")).Select(s => s.GetAttributeValue("href", null)).ToArray();
            var fileName = html.DocumentNode.Descendants("a").Where(a => a.ParentNode.HasClass("fileinfo")).Select(s => s.InnerText).ToArray();
            string boardName = html.DocumentNode.Descendants("h1").Select(s => s.InnerText).FirstOrDefault();
            string threadId = html.DocumentNode.Descendants("div").Where(d => d.HasClass("thread")).FirstOrDefault().Id;

            for(int i = 0; i < urls.Length; i++)
            {
                if(!urls[i].Contains("https")){
                    model.urls.Add("https://1500chan.org/" + urls[i]);
                    model.filename.Add(fileName[i]);
                }
            }
            model.website = "Caravela";
            model.folder = boardName + "/" + threadId;

            return model;
        }
        
        public DownloadModel EromeGallery(string target)
        {
            var model = new DownloadModel(){urls = new List<string>(), filename = new List<string>()};
            var html = new HtmlWeb().Load(target);   
            var imgUrls = html.DocumentNode.Descendants().Where(s => s.HasClass("img")).Select(d =>  d.GetAttributeValue("data-src", null)).ToArray();
            var videoUrls = html.DocumentNode.Descendants().Where(s => s.GetAttributeValue("type", null) == "video/mp4").Select(d => d.GetAttributeValue("src", null)).Distinct().ToArray();
            model.folder = html.DocumentNode.Descendants("h1").Where(s => s.ParentNode.HasClass("col-sm-12")).FirstOrDefault().InnerText;
            for(int i = 0; i < imgUrls.Length; i++)
            {
                if(imgUrls[i] != null)
                    model.urls.Add(imgUrls[i]); 
            }

            model.urls.AddRange(videoUrls);
            model.website = "Erome";
            return model;
        }

    }
}