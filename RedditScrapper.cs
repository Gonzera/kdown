using System.Net.Http;
using System.Collections.Generic;
using System.Threading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace kdown
{
    public class RedditScrapper //we need to use some json parsing for reddit
    {                           //so it will get it's scrapper
         public DownloadModel RedditModel(string url, int depth)
         {
             DownloadModel model = ParseJson(depth, url);
             string[] splited = url.Split("/r/");

             model.website = "Reddit";
             model.folder = splited[1].Replace("/", ""); 

             return model;
         }

        private string UrlBuild(string url, string after)
        {
            string apiUrl = url + "/.json?limit=100&after=" + after;
            return apiUrl;
        }

        private static JObject RequestJson(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var response = new HttpClient().Send(request);
            var content = response.Content.ReadAsStringAsync(); 
            var resul = JObject.Parse(JsonConvert.SerializeObject(content));
            return resul;
        }

        private static JObject RedditJson(string url)
        {
            var response = RequestJson(url);
            string result = response["Result"].ToString(); //idk im retarded
            JObject reddit = JObject.Parse(result);
            return reddit;
        }

        private DownloadModel ParseJson(int depth, string url)
        {
            DownloadModel model = new DownloadModel(){urls = new List<string>(), filename = new List<string>()};
            string after = "";
            int count = 0;
            do
            {
                string coolUrl = UrlBuild(url, after);
                JObject json = RedditJson(coolUrl);
                for(int i = 0; i < ((JArray)json["data"]["children"]).Count; i++)
                {
                    if(((string)json["data"]["children"][i]["kind"]) == "t3")
                    {
                        if(((string?)json["data"]["children"][i]["data"]["post_hint"]) == "image")
                        {
                            if(!json["data"]["children"][i]["data"]["url_overridden_by_dest"].ToString().Contains("gallery"))
                            {
                                string mediaUrl = ((string)json["data"]["children"][i]["data"]["url_overridden_by_dest"]);
                                model.urls.Add(mediaUrl);
                                model.filename.Add(mediaUrl.Substring(mediaUrl.LastIndexOf('/') + 1)); //OwO
                            }
                        }
                    }
                }
                after = json["data"]["after"].ToString();
                if(after == "" && count > 0)
                    break;//if we hit the end before depth ends we break the loop
                count++;
                Thread.Sleep(2000);
            }while(count <= depth);
            return model;
        }
    }

}