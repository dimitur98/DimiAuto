namespace DimiAuto.Web.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using DimiAuto.Common;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Remote;
    using Xunit;

    public class CompareCarsTest
    {
        private readonly SeleniumServerFactory<Startup> server;
        private readonly RemoteWebDriver browser;

        // Be sure that selenium-server-standalone-3.141.59.jar is running
        public CompareCarsTest()
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
            this.browser.Navigate().GoToUrl(this.server.RootUri + "/Identity/Account/Login");
            this.browser.FindElementById("loginEmail").SendKeys("AdminUser@admin.bg");
            this.browser.FindElementById("loginPassword").SendKeys("123456");
            this.browser.FindElementById("loginBtn").Click();
            this.browser.Navigate().GoToUrl(this.server.RootUri + "/Home/All");
            this.browser.FindElementById("addToCompareBtn1").Click();

            Assert.True(this.browser.FindElementById("firstCar").Displayed);
            Assert.True(this.browser.FindElementById("firstCarMakeModel").Displayed);
            Assert.True(this.browser.FindElementById("firstCarMakeModel").Text.Length > 0);
        }

        [Fact]
        public void CompareCarsBetweenTwoPages()
        {
            this.browser.Navigate().GoToUrl(this.server.RootUri + "/Identity/Account/Login");
            this.browser.FindElementById("loginEmail").SendKeys("AdminUser@admin.bg");
            this.browser.FindElementById("loginPassword").SendKeys("123456");
            this.browser.FindElementById("loginBtn").Click();
            this.browser.Navigate().GoToUrl(this.server.RootUri + "/Home/All");
            this.browser.FindElementById("addToCompareBtn1").Click();
            this.browser.FindElementById("nextPage").Click();
            this.browser.FindElementById("addToCompareBtn1").Click();
            this.browser.FindElementById("compareCars").Click();

            Assert.Equal(this.server.RootUri + "/Ad/Compare", this.browser.Url);
            Assert.True(this.browser.FindElementById("firstCar").Text.Length > 0);
            Assert.True(this.browser.FindElementById("secondCar").Text.Length > 0);
            Assert.NotEqual(this.browser.FindElementById("firstCar").Text, this.browser.FindElementById("secondCar").Text);
        }

        [Fact]
        public void SameCarShouldNotAddToComparerListTwice()
        {
            this.browser.Navigate().GoToUrl(this.server.RootUri + "/Identity/Account/Login");
            this.browser.FindElementById("loginEmail").SendKeys("AdminUser@admin.bg");
            this.browser.FindElementById("loginPassword").SendKeys("123456");
            this.browser.FindElementById("loginBtn").Click();
            this.browser.Navigate().GoToUrl(this.server.RootUri + "/Home/All");
            this.browser.FindElementById("addToCompareBtn1").Click();
            this.browser.FindElementById("addToCompareBtn1").Click();

            Assert.False(this.browser.FindElementById("secondCar").Displayed);
        }
    }
}
