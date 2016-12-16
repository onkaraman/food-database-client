using System;
using FoodDatabase.Core.API.Models.Items;
using FoodDatabase.Core.Patterns;

namespace FoodDatabase.Core.Sessions
{
    /// <summary>
    /// Will hold global session info regarding the logged in user.
    /// </summary>
    public class SessionHolder : LazyStatic<SessionHolder>
    {
        public SessionHolder() {}
        public Item Item { get; set; }
    }
}
