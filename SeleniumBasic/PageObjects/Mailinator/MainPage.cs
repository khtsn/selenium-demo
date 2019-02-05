using OpenQA.Selenium;
using SeleniumBasic.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumBasic.PageObjects.Mailinator
{
    public class MainPage
    {
        static readonly By _txtInboxField = By.XPath("//input[@id='inboxfield']");
        static readonly By _btnGo = By.XPath("//button[@type='button' and @class='btn btn-dark']");

        public IWebElement TxtInboxField
        {
            get { return Constant.WebDriver.FindElement(_txtInboxField); }
        }
        public IWebElement BtnGo
        {
            get { return Constant.WebDriver.FindElement(_btnGo); }
        }

        public MainPage Open()
        {
            Constant.WebDriver.Navigate().GoToUrl("https://www.mailinator.com");
            //Constant.WebDriver.Navigate().Refresh();
            //Constant.WebDriver.Navigate().GoToUrl("https://www.mailinator.com");
            return this;
        }
        public IndexPage GoToInbox()
        {
            TxtInboxField.SendKeys("khanhtestemail1");
            BtnGo.Click();
            return new IndexPage();
        }
    }
}
