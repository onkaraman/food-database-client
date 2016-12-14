using System;
namespace FoodDatabase.Core.API.Models
{
    /// <summary>
    /// Contains statistical data regarding a search result.
    /// </summary>
    public class Stats
    {
        public string numitemsfound { get; set; }
        public string startfrom { get; set; }
    }
}
