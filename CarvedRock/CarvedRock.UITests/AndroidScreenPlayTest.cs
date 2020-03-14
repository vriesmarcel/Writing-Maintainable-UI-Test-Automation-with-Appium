using CarvedRock.UITests.ScreenPlay;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarvedRock.UITests
{
    [TestClass]
    public class AndroidScreenPlayTest
    {
        [TestMethod]
        public void AddNewItemUsingScreenplay()
        {
            var item = new Item()
            {
                Name = "New Item",
                Detail = "Item Detail",
                Category = "Category 1"
            };

            Given.TheApplicationIsStarted()
                .And().IAmOnTheHomeScreen()
                .When().IAddANewItem(item)
                .Then().TheItemIsVisibleOnTheHomeScreen(item);

        }
    }
}
