using System;
using FoodDatabase.Core.API.Accessors;
using NUnit.Framework;

namespace FoodDatabase.Core.Tests.API
{
    [TestFixture]
    public class UserAccountTests
    {
        [Test]
        public async void Login()
        {
            string response = await APIAccessor.Static.Login("QuadrigaKing", "jonny0011");
        }

        [Test]
        public async void LoginWrongData()
        {
            try
            {
                string response = await APIAccessor.Static.Login("QuadrigaKing", "j1212");
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
        }
    }
}
