namespace DimiAuto.Web.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Remote;
    using Xunit;

    public class CompareTest 
    {
        private readonly SeleniumServerFactory<Startup> server;
        private readonly RemoteWebDriver browser;

        // Be sure that selenium-server-standalone-3.141.59.jar is running
        public CompareTest()
        {
            this.server = new SeleniumServerFactory<Startup>();
            this.server.CreateClient();
            var opts = new ChromeOptions();
            opts.AddArguments("--ignore-certificate-errors", "--headless"); 
            this.browser = new RemoteWebDriver(opts);
        }

        [Fact]
        public void AllPageShouldHaveCompareListAfterChoosingaCar()
        {
            var a =this.server.RootUri;
            this.browser.Navigate().GoToUrl(this.server.RootUri + "/");
            this.browser.Navigate().GoToUrl(this.server.RootUri + "/Identity/Account/Login");
            this.browser.FindElementById("loginEmail").SendKeys("AdminUser@admin.bg");
            this.browser.FindElementById("loginpassword").SendKeys("123456");
            this.browser.FindElementById("loginBtn").Click();
            this.browser.Navigate().GoToUrl(this.server.RootUri + "/Home/All");
            this.browser.FindElementById("addToCompareBtn1").Click();

            Assert.True(this.browser.FindElementById("firstCarMakeModel").GetAttribute("src").Any());
        }
    }
}
