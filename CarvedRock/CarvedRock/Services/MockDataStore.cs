using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarvedRock.Models;

namespace CarvedRock.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        static List<Item> items;
        static List<string> categories;
        private static MockDataStore _dataStore;

        public MockDataStore()
        {
            if (categories == null)
            {
                categories = new List<string>()
                {
                    "Category 1",
                    "Category 2",
                    "Category 3",
                    "Category 4",
                    "Category 5"
                };

            }

            if (items == null)
            {
                items = new List<Item>()
                {
                    new Item { Id = Guid.NewGuid().ToString(), Text = "First item", Description="This is an item description.", Category="Category 1" },
                    new Item { Id = Guid.NewGuid().ToString(), Text = "Second item", Description="This is an item description." , Category="Category 2"},
                    new Item { Id = Guid.NewGuid().ToString(), Text = "Third item", Description="This is an item description.", Category="Category 3" },
                    new Item { Id = Guid.NewGuid().ToString(), Text = "Fourth item", Description="This is an item description.", Category="Category 4" },
                    new Item { Id = Guid.NewGuid().ToString(), Text = "Fifth item", Description="This is an item description." , Category="Category 1"},
                    new Item { Id = Guid.NewGuid().ToString(), Text = "Sixth item", Description="This is an item description." , Category="Category 2"},
                    new Item { Id = Guid.NewGuid().ToString(), Text = "Seventh item", Description="This is an item description." , Category="Category 3"},
                    new Item { Id = Guid.NewGuid().ToString(), Text = "Eighth item", Description="This is an item description." , Category="Category 4"},
                    new Item { Id = Guid.NewGuid().ToString(), Text = "Nineth item", Description="This is an item description." , Category="Category 1"},
                    new Item { Id = Guid.NewGuid().ToString(), Text = "Tenth item", Description="This is an item description." , Category="Category 2"},
                    new Item { Id = Guid.NewGuid().ToString(), Text = "Eleventh item", Description="This is an item description." , Category="Category 2"},
                    new Item { Id = Guid.NewGuid().ToString(), Text = "Twelfth item", Description="This is an item description." , Category="Category 3"},
                    new Item { Id = Guid.NewGuid().ToString(), Text = "Thirteenth item", Description="This is an item description." , Category="Category 4"},
                    new Item { Id = Guid.NewGuid().ToString(), Text = "Fourteenth item", Description="This is an item description." , Category="Category 4"},
                    new Item { Id = Guid.NewGuid().ToString(), Text = "Fifteenth item", Description="This is an item description." , Category="Category 4"},
                    new Item { Id = Guid.NewGuid().ToString(), Text = "Sixteenth item", Description="This is an item description." , Category="Category 3"},
                    new Item { Id = Guid.NewGuid().ToString(), Text = "Seventeenth item", Description="This is an item description." , Category="Category 3"},
                    new Item { Id = Guid.NewGuid().ToString(), Text = "Eightteenth item", Description="This is an item description." , Category="Category 2"},
                    new Item { Id = Guid.NewGuid().ToString(), Text = "Twentieth item", Description="This is an item description." , Category="Category 2"},
                    new Item { Id = Guid.NewGuid().ToString(), Text = "Twenty first item", Description="This is an item description." , Category="Category 1"}
                };
            }
        }
       
        public static MockDataStore Current { 
            get {
                if (_dataStore == null)
                    _dataStore = new MockDataStore();
                return _dataStore; 
            } 
        }
        public async Task<bool> AddItemAsync(Item item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }

        public async Task<List<string>> GetCategories()
        {
            return await Task.FromResult(categories);
        }

        public async Task<bool> AddCategoryAsync(string category)
        {
            categories.Add(category);
            return await Task.FromResult(true);
        }
    }
}