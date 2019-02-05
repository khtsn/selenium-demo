using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SeleniumBasic.Common
{
    class Constant
    {
        public static IWebDriver WebDriver;
        public static WebDriverWait webDriverWait;

        public const string HomePageURL = "http://qa.kyrz.net/Page/HomePage.cshtml";
        public const string MailinatorPageURL = "http://www.mailinator.com/inbox2.jsp?public_to={0}#/#public_maildirdiv";

        public const string ValidUsername = "khanh.tran@logigear.com";
        public const string ValidPassword = "12345678";
    }
}
