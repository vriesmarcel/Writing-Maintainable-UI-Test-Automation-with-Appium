using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarvedRock.UITests.ScreenPlay
{
    internal class When
    {
       CarvedRockApplication _app;

        public When(CarvedRockApplication app)
        {
            _app = app;
        }

        public When IAddANewItem(Item item)
        {
            _app.NavigateToNewItem()
                 .AddNewItem(item.Name, item.Detail);
            return new When(_app);
        }

        public Then Then()
        {
            return new Then(_app);
        }


    }
}
