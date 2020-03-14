using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarvedRock.UITests.ScreenPlay
{
    internal class Then
    {
        CarvedRockApplication _app;

        public Then(CarvedRockApplication app)
        {
            _app = app;
        }

        internal void TheItemIsVisibleOnTheHomeScreen(Item item)
        {
            Assert.IsTrue(_app.IsElementOnHomeScreen(item.Name));
        }
    }
}
