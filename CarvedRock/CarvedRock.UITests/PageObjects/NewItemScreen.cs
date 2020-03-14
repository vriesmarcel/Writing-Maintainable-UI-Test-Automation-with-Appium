using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarvedRock.UITests.PageObjects
{
    public class NewItemScreen
    {
        AndroidDriver<AppiumWebElement> _driver;
        public NewItemScreen(AndroidDriver<AppiumWebElement> driver)
        {
            _driver = driver;
        }

        internal CarvedRockApplication AddNewItem(string itemName, string detail)
        {
 

            var elItemText = _driver.FindElementByAccessibilityId("ItemText");
            elItemText.Clear();
            elItemText.SendKeys(itemName);

            var elItemDetail = _driver.FindElementByAccessibilityId("ItemDescription");
            elItemDetail.Clear();
            elItemDetail.SendKeys(detail);

            var elSave = _driver.FindElementByAccessibilityId("Save");
            elSave.Click();

            WaitForProgressBar();

            return new CarvedRockApplication(_driver);
        }

        internal CarvedRockApplication AddNewItemWithNewCategory(string itemName, string detail, string category)
        {

            var elItemText = _driver.FindElementByAccessibilityId("ItemText");
            elItemText.Clear();
            elItemText.SendKeys(itemName);

            var elItemDetail = _driver.FindElementByAccessibilityId("ItemDescription");
            elItemDetail.Clear();
            elItemDetail.SendKeys(detail);

            var elItemCategory = _driver.FindElement(MobileBy.AccessibilityId("ItemCategory_Container"));
            elItemCategory.Click();

            var picker = _driver.FindElement(By.Id("android:id/contentPanel"));
            var categoryListItems = picker.FindElements(By.ClassName("android.widget.TextView"));
            foreach (var categoryElement in categoryListItems)
            {
                if (categoryElement.Text == category)
                    categoryElement.Click();
            }


            var elSave = _driver.FindElementByAccessibilityId("Save");
            elSave.Click();

            WaitForProgressBar();

            return new CarvedRockApplication(_driver);

        }

        internal void WaitForProgressBar()
        {
            var wait = new DefaultWait<AndroidDriver<AppiumWebElement>>(_driver)
            {
                Timeout = TimeSpan.FromSeconds(60),
                PollingInterval = TimeSpan.FromMilliseconds(500)
            };
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));

            wait.Until(d => d.FindElement(MobileBy.AccessibilityId("Second item")));

        }

    }
}
