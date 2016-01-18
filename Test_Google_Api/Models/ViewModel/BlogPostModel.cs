using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Test_Google_Api.Models.ViewModel
{
    public class BlogPostModel
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }
        public string Categories { get; set; }
        public string ImagePath { get; set; }
    }
}