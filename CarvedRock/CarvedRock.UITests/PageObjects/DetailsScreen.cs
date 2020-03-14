using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarvedRock.UITests.PageObjects
{
    public class DetailsScreen
    {
        AndroidDriver<AppiumWebElement> _driver;
        public DetailsScreen(AndroidDriver<AppiumWebElement> driver)
        {
            _driver = driver;
        }

        internal CarvedRockApplication NavigateBack()
        {
            _driver.PressKeyCode(AndroidKeyCode.Back);
            return new CarvedRockApplication(_driver);
        }

        internal bool IsDetailShown(string itemName)
        {
            var el2 = _driver.FindElement(MobileBy.AccessibilityId("ItemText"));
            return el2.Text == itemName;

        }
    }
}
