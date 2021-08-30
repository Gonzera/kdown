using System.Collections.Generic;

namespace kdown
{
    public class DownloadModel
    {
        public List<string> urls {get; set;} 
        public List<string> filename { get; set; }
        public string folder { get; set; } //in the future we will get subfolders
        public string website { get; set;}
    }
}