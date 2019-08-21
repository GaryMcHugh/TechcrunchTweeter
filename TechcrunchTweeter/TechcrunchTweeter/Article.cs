using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace TechcrunchTweeter
{
    public class Article
    {
        public IWebElement Image { get; set; }
        public string ImageSrc { get; set; }
        public string ImageAlt { get; set; }
        public string ArticleTitle { get; set; }
        public string ArticleUrl { get; set; }
    }
}
