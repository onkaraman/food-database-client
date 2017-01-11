using System;
using FoodDatabase.Core.API.Accessors;
using FoodDatabase.Core.API.Models;
using FoodDatabase.Core.API.Parsers;
using FoodDatabase.Core.Helpers;
using NUnit.Framework;

namespace FoodDatabase.Core.Tests.API
{
    [TestFixture]
    public class DiaryTests
    {
        [Test]
        public async void GetTodaysDiary()
        {
            LoginData ld = new LoginData("QuadrigaKing", "jonny0011");
            string response = await APIAccessor.Static.DiaryGet(ld, DateTime.Today);
            Result result = APIParser.Static.Parse(response);
        }

        [Test]
        public async void DeleteDiaryItem()
        {
            LoginData ld = new LoginData("quadrigaking", "jonny0011");
            string diaryResponse = await APIAccessor.Static.DiaryGet(ld, DateTime.Today);
            Result diaryResult = APIParser.Static.Parse(diaryResponse);

            string uidToRemove = diaryResult.DiaryElements[0].diary_uid;
            string removeResponse = await APIAccessor.Static.DiaryRemove(ld, uidToRemove);

            diaryResponse = await APIAccessor.Static.DiaryGet(ld, DateTime.Today);
            diaryResult = APIParser.Static.Parse(diaryResponse);

            Assert.IsTrue(removeResponse.ToLower().Contains("success"));
            Assert.IsFalse(diaryResult.DiaryElements[0].diary_uid.Equals(uidToRemove));
        }

        [Test]
        public async void AddFoodCustomServing()
        {
            LoginData ld = new LoginData("QuadrigaKing", "jonny0011");
            string response = await APIAccessor.Static.DiaryAddItem(ld, "1", 100);
            Assert.IsTrue(response.ToLower().Contains("success"));
        }

        [Test]
        public async void AddFoodServing()
        {
            string response = await APIAccessor.Static.Search("Banane");
            Result result = APIParser.Static.Parse(response);

            LoginData ld = new LoginData("QuadrigaKing", "jonny0011");
            response = await APIAccessor.Static.DiaryAddItem(ld, "1", 0, result.Items[0].Servings[0].serving_id);
            Assert.IsTrue(response.ToLower().Contains("success"));
        }
    }
}
