using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;
//using OpenQA.Selenium.Appium.Interactions;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarvedRock.UITests
{
    [TestClass]
    public class WindowsUWPLongTest
    {
        [TestMethod]
        public void AddNewItemWithNewCategory()
        {
            var capabilities = new AppiumOptions();
            capabilities.AddAdditionalCapability(MobileCapabilityType.App, "8b831c56-bc54-4a8b-af94-a448f80118e7_sezxftbtgh66j!App");
            capabilities.AddAdditionalCapability(MobileCapabilityType.PlatformName, "Windows");
            capabilities.AddAdditionalCapability(MobileCapabilityType.DeviceName, "WindowsPC");

            var _appiumLocalService = new AppiumServiceBuilder().UsingAnyFreePort().Build();
            _appiumLocalService.Start();
            var driver = new WindowsDriver<WindowsElement>(_appiumLocalService, capabilities);

            // Create new Category item first
            var categoryButton = driver.FindElement(MobileBy.AccessibilityId("AddCategory"));
            categoryButton.Click();

            // fill out the form for a new category
            var categoryName = driver.FindElement(MobileBy.AccessibilityId("categoryName"));
            categoryName.Clear();
            categoryName.SendKeys("New category from automation");

            //save category
            var saveCategory = driver.FindElement(MobileBy.AccessibilityId("Save"));
            saveCategory.Click();

            var el1 = driver.FindElementByAccessibilityId("Add");
            el1.Click();

            var elItemText = driver.FindElementByAccessibilityId("ItemText");
            elItemText.Clear();
            elItemText.SendKeys("This is a new Item");

            var elItemDetail = driver.FindElementByAccessibilityId("ItemDescription");
            elItemDetail.Clear();
            elItemDetail.SendKeys("These are the details");

            var elItemCategory = driver.FindElement(MobileBy.AccessibilityId("ItemCategory"));
            elItemCategory.Click();

            var categoryListItem = elItemCategory.FindElement(By.Name("New category from automation"));
            categoryListItem.Click();

            var elSave = driver.FindElementByAccessibilityId("Save");
            elSave.Click();
            elSave.ClearCache();

            //wait for progress bar to disapear
            var wait = new DefaultWait<WindowsDriver<WindowsElement>>(driver)
            {
                Timeout = TimeSpan.FromSeconds(60),
                PollingInterval = TimeSpan.FromMilliseconds(500)
            };
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            wait.Until(d => d.FindElementByName("Second item"));

            var listview = driver.FindElementByAccessibilityId("ItemsListView");

            //now use wait to scroll untill we find item
            wait = new DefaultWait<WindowsDriver<WindowsElement>>(driver)
            {
                Timeout = TimeSpan.FromSeconds(60),
                PollingInterval = TimeSpan.FromMilliseconds(500)
            };
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));

            var elementfound = wait.Until(d =>
            {
                var input = new PointerInputDevice(PointerKind.Touch);
                ActionSequence FlickUp = new ActionSequence(input);
                FlickUp.AddAction(input.CreatePointerMove(listview, 0, 0, TimeSpan.Zero));
                FlickUp.AddAction(input.CreatePointerDown(MouseButton.Left));
                FlickUp.AddAction(input.CreatePointerMove(listview, 0, -300, TimeSpan.FromMilliseconds(200)));
                FlickUp.AddAction(input.CreatePointerUp(MouseButton.Left));
                driver.PerformActions(new List<ActionSequence>() { FlickUp });

                return d.FindElementByName("This is a new Item");
            });

            Assert.IsTrue(elementfound != null);

            driver.CloseApp();
        }

    }
}
