using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using TweetSharp;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TechcrunchTweeter
{
    class Program
    {
        private static string api_key = "<api key>";
        private static string api_key_secret = "<secret api key>";
        private static string access_token = "<access token>";
        private static string access_token_secret = "<secret access token>";

        private static TwitterService service = new TwitterService(api_key, api_key_secret, access_token, access_token_secret);
        private static int currentImageID = 0;
        private static List<string> imageList = new List<string>();

        [STAThread]
        static void Main()
        {
            try
            {
                var driver = new ChromeDriver("C:/Users/mchugg01/source/repos/TechcrunchTweeter/TechcrunchTweeter/bin/Debug");
                Console.WriteLine($"<{DateTime.Now}> - Bot Started");

                var article = GetArticle(driver);

                var tweetText = GetTweetMessage(article);

                if (imageList.Count > 0)
                    SendMediaTweet(tweetText, currentImageID);
                else
                {
                    SendTweet(tweetText);
                }

                driver.Quit();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private static Article GetArticle(ChromeDriver driver)
        {
            var article = new Article();
            driver.Navigate().GoToUrl("https://techcrunch.com/");
            driver.Manage().Window.Maximize();

            driver.FindElement(By.ClassName("agree")).Click();
            article.ArticleTitle = driver.FindElement(By.ClassName("post-block__title__link")).Text;

            driver.FindElement(By.ClassName("post-block__title__link")).Click();

            article.ArticleUrl = driver.Url;

            try
            {
                article.Image = driver.FindElement(By.ClassName("article__featured-image"));
                if (article.Image != null)
                {
                    article.ImageAlt = article.Image.GetAttribute("alt");
                    article.ImageSrc = article.Image.GetAttribute("src");
                }
                //handle double quotes file name issue
                article.ImageAlt = article.ImageAlt.Replace("\"", "");

                using (WebClient client = new WebClient())
                {
                    client.DownloadFile(new Uri(article.ImageSrc), @"c:\tweetBot\" + article.ImageAlt + ".jpg");
                }

                imageList.Add("C:/tweetBot/" + article.ImageAlt + ".jpg");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return article;
        }

        private static string GetTweetMessage(Article article)
        {
            var tweetMessage = new Dictionary<int, string>();

            ConstructTweetOptions(article, tweetMessage);

            var rnd = new Random();
            var number = rnd.Next(1, 10);
            var tweet = tweetMessage[number];

            return tweet;
        }

        private static void ConstructTweetOptions(Article article, Dictionary<int, string> tweetMessage)
        {
            tweetMessage.Add(1, "Checkout this article: " + "'" + article.ArticleTitle + "'" + " from techcrunch here " + article.ArticleUrl);
            tweetMessage.Add(2, "New featured article -> " + "'" + article.ArticleTitle + "'" + " take a look here: " + article.ArticleUrl);
            tweetMessage.Add(3, "Good article from techcrunch today: " + "'" + article.ArticleTitle + "'" + " read it here: " + article.ArticleUrl);
            tweetMessage.Add(4, "This one is worth a read: " + "'" + article.ArticleTitle + "'" + " from techcrunch: " + article.ArticleUrl);
            tweetMessage.Add(5, article.ArticleTitle + "'" + " interested? take a look: " + article.ArticleUrl);
            tweetMessage.Add(6, "Start your morning with a good read: " + "'" + article.ArticleTitle + "'" + " here is the link: " + article.ArticleUrl);
            tweetMessage.Add(7, "Give this a read: " + "'" + article.ArticleTitle + "'" + " from techcrunch here " + article.ArticleUrl);
            tweetMessage.Add(8, "Morning, new article: " + "'" + article.ArticleTitle + "'" + " here " + article.ArticleUrl);
            tweetMessage.Add(9, article.ArticleTitle + "'" + " sounds good? check it out: " + article.ArticleUrl);
            tweetMessage.Add(10, article.ArticleTitle + "'" + " interesting one today, take a look: " + article.ArticleUrl);
        }

        private static void SendTweet(string _status)
        {
            service.SendTweet(new SendTweetOptions { Status = _status }, (tweet, response) =>
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"<{DateTime.Now}> - Tweet Sent");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"<ERROR>" + response.Error.Message);
                    Console.ResetColor();
                }
            });
        }

        private static void SendMediaTweet(string _status, int currentImageID)
        {
            using (var stream = new FileStream(imageList[currentImageID], FileMode.Open))
            {
                service.SendTweetWithMedia(new SendTweetWithMediaOptions
                {
                    Status = _status,
                    Images = new Dictionary<string, Stream> { { imageList[currentImageID], stream } }
                });

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"<{DateTime.Now}> - Tweet Sent");
                Console.ResetColor();
            }
        }
    }
}
