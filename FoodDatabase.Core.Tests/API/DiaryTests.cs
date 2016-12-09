using System;
using FoodDatabase.Core.API.Accessors;
using FoodDatabase.Core.API.Models.Item;
using FoodDatabase.Core.API.Parsers;
using NUnit.Framework;

namespace FoodDatabase.Core.Tests.API
{
    [TestFixture]
    public class DiaryTests
    {
        [Test]
        public async void GetTodaysDiary()
        {
            string response = await APIAccessor.Static.DiaryGet("QuadrigaKing", "jonny0011", DateTime.Today);
            Result result = APIParser.Static.Parse(response);
        }

        [Test]
        public async void DeleteDiaryItem()
        {
            string diaryResponse = await APIAccessor.Static.DiaryGet("QuadrigaKing", "jonny0011", DateTime.Today);
            Result diaryResult = APIParser.Static.Parse(diaryResponse);

            string uidToRemove = diaryResult.DiaryElements[0].diary_uid;
            string removeResponse = await APIAccessor.Static.DiaryRemove("QuadrigaKing", "jonny0011", uidToRemove);

            diaryResponse = await APIAccessor.Static.DiaryGet("QuadrigaKing", "jonny0011", DateTime.Today);
            diaryResult = APIParser.Static.Parse(diaryResponse);

            Assert.IsTrue(removeResponse.ToLower().Contains("success"));
            Assert.IsFalse(diaryResult.DiaryElements[0].diary_uid.Equals(uidToRemove));
        }

        [Test]
        public async void AddFoodCustomServing()
        {
            string response = await APIAccessor.Static.DiaryAddItem("QuadrigaKing", "jonny0011", "1", 100);
            Assert.IsTrue(response.ToLower().Contains("success"));
        }

        [Test]
        public async void AddFoodServing()
        {
            string response = await APIAccessor.Static.Search("Banane");
            Result result = APIParser.Static.Parse(response);

            response = await APIAccessor.Static.DiaryAddItem("QuadrigaKing", "jonny0011", "1", 0, 
                                                               result.Items[0].Servings[0].serving_id);
            Assert.IsTrue(response.ToLower().Contains("success"));
        }
    }
}
