using System;
namespace FoodDatabase.Core.API.Models
{
    /// <summary>
    /// Contains the general description of a food item.
    /// </summary>
    public class Description
    {
        public string name { get; set; }
        public string option { get; set; }
        public string producer { get; set; }
        public string group { get; set; }
        public string imagedescription { get; set; }
        public string imageauthor { get; set; }
    }
}
