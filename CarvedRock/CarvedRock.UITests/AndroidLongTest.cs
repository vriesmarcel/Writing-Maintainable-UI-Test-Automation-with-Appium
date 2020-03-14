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
            var itemName = "New item";
            var detail = "Detail text";
            var category = "New Category";
            // Arrange, Act and Assert
            var carvedrockApplication = new CarvedRockApplication();
            Assert.IsTrue(carvedrockApplication.Start()
                .NavigateToNewCategory()
                .AddNewCategory(category)
                .NavigateToNewItem()
                .AddNewItemWithNewCategory(itemName, detail, category)
                .IsElementOnHomeScreen(itemName));

            carvedrockApplication.StopApp();
        }
        [TestMethod]
        public void AddNewItem()
        {
            var itemName = "New item";
            var detail = "Detail text";
            // Arrange, Act
            var carvedrockApplication = new CarvedRockApplication();
            var homescreen = carvedrockApplication.Start()
                .NavigateToNewItem()
                .AddNewItem(itemName, detail);

            // Assert
            Assert.IsTrue(homescreen.IsElementOnHomeScreen(itemName));

            carvedrockApplication.StopApp();

        }
        [TestMethod]
        public void MasterDetail()
        {
            // Arrange
            var carvedrockApplication = new CarvedRockApplication();
            carvedrockApplication.Start();

            // Act
            var detail = carvedrockApplication.SelectItemOnHomescreen("Second item");

            // Assert
            Assert.IsTrue(detail.IsDetailShown("Second item"));

            // Act
            detail.NavigateBack();

            // Assert
            Assert.IsTrue(carvedrockApplication.IsElementOnHomeScreen("Fourth item"));

            carvedrockApplication.StopApp();
        }

  
  
 

    
        
    }
}
