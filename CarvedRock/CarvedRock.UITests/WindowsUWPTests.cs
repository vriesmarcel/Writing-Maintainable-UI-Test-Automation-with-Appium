using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.MultiTouch;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Appium.Windows.Enums;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Interactions.Internal;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CarvedRock.UITests
{
    [TestClass]
    public class WindowsUWPTests
    {
        static TestContext ctx;
        [ClassInitialize]
        static public void Initialize(TestContext context)
        {
            ctx = context;
        }

  
        [TestMethod]
        public void ScrollToEndOfListUsingPointerInputDevice()
        {
            var driver = startApp();
            var ListView = driver.FindElement(MobileBy.ClassName("ListView"));
           // ListView.GetAttribute
            // set start point
            FlickUp(driver, ListView);

            Thread.Sleep(3000);

            FlickUp(driver, ListView);

 
            Thread.Sleep(3000);

            driver.CloseApp();

        }

        [TestMethod]
        public void CheckMasterDetailAndBack()
        {
            var driver = startApp(); 
            
            // tap on second item
            var el1 = driver.FindElementByName("Second item");
  
            el1.Click();
            var el2 = driver.FindElementByAccessibilityId("ItemText");
            Assert.IsTrue(el2.Text == "Second item");

            var backButton = driver.FindElementByAccessibilityId("Back");
            backButton.Click();

            var el3 = driver.FindElementByName("Fourth item");
            Assert.IsTrue(el3 != null);

            driver.CloseApp();

        }

        [TestMethod]
        public void AddNewItem()
        {
            var driver = startApp();
            
            // tap on second item
            var el1 = driver.FindElementByAccessibilityId("Add");
            el1.Click();
           
            var elItemText = driver.FindElementByAccessibilityId("ItemText");
            elItemText.Clear();
            elItemText.SendKeys("This is a new Item");

            var elItemDetail = driver.FindElementByAccessibilityId("ItemDescription");
            elItemDetail.Clear();
            elItemDetail.SendKeys("These are the details");

            var elSave = driver.FindElementByAccessibilityId("Save");
            elSave.Click();

            WaitForProgressbarToDisapear(driver);

            var listview = driver.FindElementByAccessibilityId("ItemsListView");

            var wait = new DefaultWait<WindowsDriver<WindowsElement>>(driver)
            {
                Timeout = TimeSpan.FromSeconds(60),
                PollingInterval = TimeSpan.FromMilliseconds(500)
            };
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));

            var elementfound = wait.Until(d =>
            {
                FlickUp(driver, listview);
                return d.FindElementByName("This is a new Item");
            });

            Assert.IsTrue(elementfound!=null);
            driver.Close();
        }

        private void WaitForProgressbarToDisapear(WindowsDriver<WindowsElement> driver)
        {
            var wait = new DefaultWait<WindowsDriver<WindowsElement>>(driver)
            {
                Timeout = TimeSpan.FromSeconds(60),
                PollingInterval = TimeSpan.FromMilliseconds(500)
            };
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            wait.Until(d => d.FindElementByName("Second item"));
        }

        public WindowsDriver<WindowsElement> startApp()
        {

            var capabilities = new AppiumOptions();
            capabilities.AddAdditionalCapability(MobileCapabilityType.App, "8b831c56-bc54-4a8b-af94-a448f80118e7_sezxftbtgh66j!App");
            capabilities.AddAdditionalCapability(MobileCapabilityType.PlatformName, "Windows");
            capabilities.AddAdditionalCapability(MobileCapabilityType.DeviceName, "WindowsPC");

            var _appiumLocalService = new AppiumServiceBuilder().UsingAnyFreePort().Build();
            _appiumLocalService.Start();
            var driver = new WindowsDriver<WindowsElement>(_appiumLocalService, capabilities);
            //var driver = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723/wd/hub"), capabilities);
            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            return driver;
        }

        private void CreateScreenshot(WindowsDriver<WindowsElement>  driver)
        {
            var screenshot = driver.GetScreenshot();
            screenshot.SaveAsFile("startScreen.png", OpenQA.Selenium.ScreenshotImageFormat.Png);
            ctx.AddResultFile("startScreen.png");
        }

        private void FlickUp(WindowsDriver<WindowsElement> driver, AppiumWebElement element)
        {
            var input = new PointerInputDevice(PointerKind.Touch);
            ActionSequence FlickUp = new ActionSequence(input);
            FlickUp.AddAction(input.CreatePointerMove(element, 0, 0, TimeSpan.Zero));
            FlickUp.AddAction(input.CreatePointerDown(MouseButton.Left));
            FlickUp.AddAction(input.CreatePointerMove(element, 0, -300, TimeSpan.FromMilliseconds(200)));
            FlickUp.AddAction(input.CreatePointerUp(MouseButton.Left));
            driver.PerformActions(new List<ActionSequence>() { FlickUp });
        }
    }
}

