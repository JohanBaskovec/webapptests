using System;
using System.Collections.ObjectModel;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

namespace webapptests.e2e
{
    public class BlogArticleTests
    {
        
        [Test]
        public void Test()
        {
            RemoteWebDriver driver = new ChromeDriver();
            driver.Url = "http://localhost:8080/blog";
            IWebElement titleElement = driver.FindElementById("Title");
            IWebElement contentElement = driver.FindElementById("Content");
            titleElement.SendKeys("Hello");
            contentElement.SendKeys("World");
            IWebElement sendButton = driver.FindElementById("submit-button");
            sendButton.Click();

            ReadOnlyCollection<IWebElement> articleTitles = driver.FindElementsByClassName("article-title");
            ReadOnlyCollection<IWebElement> articleContents = driver.FindElementsByClassName("article-content");
            Assert.AreEqual("Hello", articleTitles[2].Text);
            Assert.AreEqual("World", articleContents[2].Text);
        }
    }
}