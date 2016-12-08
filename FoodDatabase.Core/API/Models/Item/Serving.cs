using System;
using System.Xml.Serialization;

namespace FoodDatabase.Core.API.Models.Item
{
    /// <summary>
    /// The serving option of an item.
    /// </summary>
    public class Serving
    {
        public string serving_id { get; set; }
        public string name { get; set; }
        public string weight_gram { get; set; }
    }
}
