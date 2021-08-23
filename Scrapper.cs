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
                model.urls.Add(urls[i]);
                if(fileName[i] != null) //im not too sure if we will get a filename everytime from the html
                    model.filename.Add(fileName[i]);
            }
            model.website = "FourChan";
            model.folder = boardName + "/" + threadId;
            return model;
        }
    
    }
}