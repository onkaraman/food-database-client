using System;
using FoodDatabase.Core.API.Accessors;
using FoodDatabase.Core.API.Models;
using FoodDatabase.Core.API.Parsers;
using NUnit.Framework;

namespace FoodDatabase.Core.Tests.API
{
    [TestFixture]
    public class DiaryTests
    {
        [Test]
        public async void AddFoodCustomServing()
        {
            string response = await APIAccessor.Static.AddItemToDiary("QuadrigaKing", "jonny0011", "1", 100);
            Assert.IsTrue(response.ToLower().Contains("success"));
        }

        [Test]
        public async void AddFoodServing()
        {
            string response = await APIAccessor.Static.Search("Banane");
            Result result = APIParser.Static.ParseSearch(response);

            response = await APIAccessor.Static.AddItemToDiary("QuadrigaKing", "jonny0011", "1", 0, 
                                                               result.Items[0].Servings[0].serving_id);
            Assert.IsTrue(response.ToLower().Contains("success"));
        }
    }
}
