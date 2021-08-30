using System.Linq;
using System.Collections.Generic;
using HtmlAgilityPack;

namespace kdown
{
    public static class Navigator
    {
        public static void EromeProfile(string target)
        {
            Scrapper scrapper = new Scrapper();
            int page = 1;
            var html = new HtmlWeb().Load(EromePages(target, page));
            string profileName = target.Substring(target.LastIndexOf('/') + 1);
            do
            {
                string[] albumsUrl = html.DocumentNode.Descendants("a").Where(a => a.HasClass("album-link")).Select(s => s.GetAttributeValue("href", null)).ToArray();
                foreach(string album in albumsUrl)
                {
                    DownloadModel model = scrapper.EromeGallery(album);
                    model.folder = profileName + '_' + model.folder;
                    Downloader.Download(model);
                }
                page++;
                html = new HtmlWeb().Load(EromePages(target, page));
            }while(html.DocumentNode.SelectSingleNode("//div[@id='albums']").InnerHtml.Contains('d'));
            
        }

        public static string EromePages(string url, int page)
        {
            return url + "?page=" + page.ToString();
        }
    }
}