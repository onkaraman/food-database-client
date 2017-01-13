using System;
using FoodDatabase.Core.API.Models.Items;
using FoodDatabase.Core.Patterns;

namespace FoodDatabase.Core.Helpers
{
    /// <summary>
    /// Will hold global session info regarding the logged in user.
    /// </summary>
    public class SessionManager : LazyStatic<SessionManager>
    {
        public bool FromDiary;
        public bool FromServing;
        public LoginData LoginData { get; set; }
        public Item Item { get; set; }

        public SessionManager() { }
    }
}
