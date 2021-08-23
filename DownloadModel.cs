using System.Collections.Generic;

namespace kdown
{
    public class DownloadModel
    {
        public List<string> urls {get; set;} 
        public List<string> filename { get; set; }
        public string folder { get; set; }
        public string website { get; set;}
    }
}