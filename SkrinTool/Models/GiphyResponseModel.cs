using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkrinTool.Models
{
    public class GiphyResponseModel
    {
        public List<GiphyItem> data { get; set; }
    }

    public class GiphyItem
    {
        public GiphyImages Images { get; set; }
    }

    public class GiphyImages
    {
        public GiphyImage fixed_height_small { get; set; }
    }

    public class GiphyImage
    {
        public string Url { get; set; }
    }
}
