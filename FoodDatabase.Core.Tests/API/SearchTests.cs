using System;
using FoodDatabase.Core.API.Accessors;
using FoodDatabase.Core.API.Parsers;
using NUnit.Framework;

namespace FoodDatabase.Core.Tests.API
{
    [TestFixture]
    public class SearchTests
    {
        [Test]
        public async void TestSimpleQuery()
        {
            string res = await APIAccessor.Static.Search("Banane");
            APIParser.Static.ParseSearch(res);
        }
    }
}
