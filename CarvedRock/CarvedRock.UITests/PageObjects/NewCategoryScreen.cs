using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarvedRock.UITests.PageObjects
{
    public class NewCategoryScreen
    {
        AndroidDriver<AppiumWebElement> _driver;
        public NewCategoryScreen(AndroidDriver<AppiumWebElement> driver)
        {
            _driver = driver;
        }

        internal CarvedRockApplication AddNewCategory(string category)
        {

            // fill out the form for a new category
            var categoryName = _driver.FindElement(MobileBy.AccessibilityId("categoryName"));
            categoryName.Clear();
            categoryName.SendKeys(category);

            //save category
            var saveCategory = _driver.FindElement(MobileBy.AccessibilityId("Save"));
            saveCategory.Click();

            return new CarvedRockApplication(_driver);
        }
    }
}
