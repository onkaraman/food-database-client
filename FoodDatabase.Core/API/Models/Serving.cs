using System;
namespace FoodDatabase.Core.API.Models
{
    /// <summary>
    /// The serving option of an item.
    /// </summary>
    public class Serving
    {
        public string serving_id { get; set; }
        // Per hand
        public string name { get; set; }
        public string weight_gram { get; set; }
    }
}
