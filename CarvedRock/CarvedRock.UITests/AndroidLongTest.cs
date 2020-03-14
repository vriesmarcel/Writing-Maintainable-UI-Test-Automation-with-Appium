using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    [TestClass]
    public class AndroidLongTest
    {
        [TestMethod]
        public void AddNewItemWithNewCategory()
        {
            // Arrange
            var carvedrockApplication = new CarvedRockApplication();
            carvedrockApplication.Start();


            // Act
            carvedrockApplication.AddNewCategory();
            //create new item
            carvedrockApplication.CreateNewItemWithNewCategory();

            //wait for progress bar to disapear
            carvedrockApplication.WaitForProgressBar();

            var elementfound = carvedrockApplication.IsElementOnHomeScreen("This is a new Item");

            // Assert
            Assert.IsTrue(elementfound);

            carvedrockApplication.StopApp();
        }
        [TestMethod]
        public void AddNewItem()
        {
            // Arrange
            var carvedrockApplication = new CarvedRockApplication();
            carvedrockApplication.Start();

            // Act
            carvedrockApplication.AddNewItem();

            //wait for progress bar to disapear
            carvedrockApplication.WaitForProgressBar();


            var elementfound = carvedrockApplication.IsElementOnHomeScreen("This is a new Item");

            // Assert
            Assert.IsTrue(elementfound);

            carvedrockApplication.StopApp();

        }
        [TestMethod]
        public void MasterDetail()
        {
            // Arrange
            var carvedrockApplication = new CarvedRockApplication();
            carvedrockApplication.Start();

            // Act
            carvedrockApplication.SelectItemOnHomescreen("Second item");

            // Assert
            Assert.IsTrue(carvedrockApplication.IsDetailShown("Second item"));

            // Act
            carvedrockApplication.NavigateBack();

            // Assert
            Assert.IsTrue(carvedrockApplication.IsElementOnHomeScreen("Fourth item"));

            carvedrockApplication.StopApp();
        }

  
  
 

    
        
    }
}
