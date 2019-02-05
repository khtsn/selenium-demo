using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;

namespace SeleniumBasic.Common
{
    public class BaseCase
    {
        [TestInitialize]
        public void SetUp()
        {
            Console.WriteLine("Run test initialize");

            //start firefox
            FirefoxProfile profile = new FirefoxProfile();
            profile.SetPreference("webdriver.load.strategy", "unstable");
            Constant.WebDriver = new FirefoxDriver(profile);
            //maximize
            Constant.WebDriver.Manage().Window.Maximize();
        }

        [TestCleanup]
        public void TearDown()
        {
            Console.WriteLine("Run test cleanup");

            //close browser
            Constant.WebDriver.Quit();
        }
    }
}
