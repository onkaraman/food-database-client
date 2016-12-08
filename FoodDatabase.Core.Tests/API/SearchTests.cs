using System;
using FoodDatabase.Core.API.Accessors;
using FoodDatabase.Core.API.Models;
using FoodDatabase.Core.API.Parsers;
using NUnit.Framework;

namespace FoodDatabase.Core.Tests.API
{
    [TestFixture]
    public class SearchTests
    {
        [Test]
        public async void ResultCount()
        {
            string response = await APIAccessor.Static.Search("Banane");
            Result result = APIParser.Static.ParseSearch(response);
            Assert.IsTrue(result.Items.Count > 5);
        }

        [Test]
        public async void ResultContainsData()
        {
            string response = await APIAccessor.Static.Search("Banane");
            Result result = APIParser.Static.ParseSearch(response);

            Assert.IsTrue(result.Items[0].Data.amount.Length > 0);
            Assert.IsTrue(result.Items[0].Data.kcal.Length > 0);
            Assert.IsTrue(result.Items[0].Data.kh_gram.Length > 0);
        }

        [Test]
        public async void ResultContainsDescription()
        {
            string response = await APIAccessor.Static.Search("Banane");
            Result result = APIParser.Static.ParseSearch(response);

            Assert.IsTrue(result.Items[0].Description.name.Length > 0);
            Assert.IsTrue(result.Items[0].Description.producer.Length > 0);
            Assert.IsTrue(result.Items[0].Description.group.Length > 0);
        }
    
        [Test]
        public async void ResultHasServings()
        {
            string response = await APIAccessor.Static.Search("Banane");
            Result result = APIParser.Static.ParseSearch(response);

            Assert.IsTrue(result.Items[0].Servings.Count > 0);
        }
    }
}
