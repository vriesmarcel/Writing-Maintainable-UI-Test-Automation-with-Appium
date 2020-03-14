using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarvedRock.UITests.ScreenPlay
{
    internal class Given
    {
        CarvedRockApplication _app;

        public Given(CarvedRockApplication app)
        {
            _app = app;
        }
        
        public static Given TheApplicationIsStarted()
        {
            CarvedRockApplication app = new CarvedRockApplication();
            app.Start();
            return new Given(app);
        }

        internal Given IAmOnTheHomeScreen()
        {
            return this;
        }

        internal Given And()
        {
            return this;
        }
     
        internal When When()
        {
            return new When(_app);
        }

    }
}
