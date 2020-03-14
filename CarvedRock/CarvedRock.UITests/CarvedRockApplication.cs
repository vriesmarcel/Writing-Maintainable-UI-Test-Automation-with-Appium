using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Service;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;

namespace CarvedRock.UITests
{
    internal class CarvedRockApplication
    {
        AndroidDriver<AppiumWebElement> _driver;
        public CarvedRockApplication()
        {
        }

        internal void Start()
        {
            _driver = StartApp();
        }

        internal void StopApp()
        {
            _driver.CloseApp();
        }
        private static AndroidDriver<AppiumWebElement> StartApp()
        {
            System.Environment.SetEnvironmentVariable("ANDROID_HOME", @"C:\Program Files (x86)\Android\android-sdk");
            System.Environment.SetEnvironmentVariable("JAVA_HOME", @"C:\Program Files\Android\jdk\microsoft_dist_openjdk_1.8.0.25\bin");

            var capabilities = new AppiumOptions();
            // automatic start of the emulator if not running
            capabilities.AddAdditionalCapability(AndroidMobileCapabilityType.Avd, "demo_device");
            capabilities.AddAdditionalCapability(AndroidMobileCapabilityType.AvdArgs, "-no-boot-anim -no-snapshot-load");
            capabilities.AddAdditionalCapability(MobileCapabilityType.FullReset, true);
            // connecting to a device or emulator
            capabilities.AddAdditionalCapability(MobileCapabilityType.DeviceName, "2471736c36037ece");
            capabilities.AddAdditionalCapability(MobileCapabilityType.AutomationName, "UiAutomator2");
            // specifyig which app we want to install and launch
            var currentPath = Directory.GetCurrentDirectory();
            Console.WriteLine($"Current path: {currentPath}");
            var packagePath = Path.Combine(currentPath, @"..\..\..\AppsToTest\com.fluentbytes.carvedrock-x86.apk");
            packagePath = Path.GetFullPath(packagePath);
            Console.WriteLine($"Package path: {packagePath}");
            capabilities.AddAdditionalCapability(MobileCapabilityType.App, packagePath);

            capabilities.AddAdditionalCapability(AndroidMobileCapabilityType.AppPackage, "com.fluentbytes.carvedrock");
            capabilities.AddAdditionalCapability(AndroidMobileCapabilityType.AppActivity, "crc641782d5af3c9cf50a.MainActivity");

            var _appiumLocalService = new AppiumServiceBuilder().UsingAnyFreePort().Build();
            _appiumLocalService.Start(); ;
            var driver = new AndroidDriver<AppiumWebElement>(_appiumLocalService, capabilities);
            return driver;
        }

       

        internal void NavigateBack()
        {
            _driver.PressKeyCode(AndroidKeyCode.Back);
        }

        internal bool IsDetailShown(string itemName)
        {
            var el2 = _driver.FindElement(MobileBy.AccessibilityId("ItemText"));
            return el2.Text == itemName;

        }

        internal void SelectItemOnHomescreen(string itemName)
        {
            var el1 = _driver.FindElement(MobileBy.AccessibilityId(itemName));
            el1.Click();
        }

        internal bool IsElementOnHomeScreen(string elementText)
        {
            var listview = _driver.FindElementByAccessibilityId("ItemsListView");

            //now use wait to scroll untill we find item

            Func<AppiumWebElement> FindElementAction = () =>
            {
                // find all text views
                // check if the text matches
                var elements = _driver.FindElementsByClassName("android.widget.TextView");
                foreach (var textView in elements)
                {
                    if (textView.Text == elementText)
                        return textView;
                }
                return null;
            };

            var wait2 = new DefaultWait<AndroidDriver<AppiumWebElement>>(_driver)
            {
                Timeout = TimeSpan.FromSeconds(60),
                PollingInterval = TimeSpan.FromMilliseconds(1000)
            };
            wait2.IgnoreExceptionTypes(typeof(NoSuchElementException));
            AppiumWebElement elementfound = null;

            elementfound = wait2.Until(d =>
            {
                var input = new PointerInputDevice(PointerKind.Touch);
                ActionSequence FlickUp = new ActionSequence(input);
                FlickUp.AddAction(input.CreatePointerMove(listview, 0, 0, TimeSpan.Zero));
                FlickUp.AddAction(input.CreatePointerDown(MouseButton.Left));

                FlickUp.AddAction(input.CreatePointerMove(listview, 0, -600, TimeSpan.FromMilliseconds(200)));
                FlickUp.AddAction(input.CreatePointerUp(MouseButton.Left));
                _driver.PerformActions(new List<ActionSequence>() { FlickUp });
                return FindElementAction();
            });
            return elementfound!=null;

        }

        internal void AddNewItem()
        {
            var el1 = _driver.FindElementByAccessibilityId("Add");
            el1.Click();

            var elItemText = _driver.FindElementByAccessibilityId("ItemText");
            elItemText.Clear();
            elItemText.SendKeys("This is a new Item");

            var elItemDetail = _driver.FindElementByAccessibilityId("ItemDescription");
            elItemDetail.Clear();
            elItemDetail.SendKeys("These are the details");

            var elSave = _driver.FindElementByAccessibilityId("Save");
            elSave.Click();

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

        internal void CreateNewItemWithNewCategory()
        {
            var el1 = _driver.FindElementByAccessibilityId("Add");
            el1.Click();

            var elItemText = _driver.FindElementByAccessibilityId("ItemText");
            elItemText.Clear();
            elItemText.SendKeys("This is a new Item");

            var elItemDetail = _driver.FindElementByAccessibilityId("ItemDescription");
            elItemDetail.Clear();
            elItemDetail.SendKeys("These are the details");

            var elItemCategory = _driver.FindElement(MobileBy.AccessibilityId("ItemCategory_Container"));
            elItemCategory.Click();

            var picker = _driver.FindElement(By.Id("android:id/contentPanel"));
            var categoryListItems = picker.FindElements(By.ClassName("android.widget.TextView"));
            foreach (var categoryElement in categoryListItems)
            {
                if (categoryElement.Text == "New category from automation")
                    categoryElement.Click();
            }


            var elSave = _driver.FindElementByAccessibilityId("Save");
            elSave.Click();

        }

        internal void AddNewCategory()
        {
                // Create new Category item first
                var categoryButton = _driver.FindElement(MobileBy.AccessibilityId("AddCategory"));
                categoryButton.Click();

                // fill out the form for a new category
                var categoryName = _driver.FindElement(MobileBy.AccessibilityId("categoryName"));
                categoryName.Clear();
                categoryName.SendKeys("New category from automation");

                //save category
                var saveCategory = _driver.FindElement(MobileBy.AccessibilityId("Save"));
                saveCategory.Click();
   
        }
    }
}