using System.Collections.Generic;

namespace OvOv.Core.Models.Blogs
{
    public class CreateBlogDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string NameSting { get; set; }

        public List<string> Tags { get; set; }

    }
}
