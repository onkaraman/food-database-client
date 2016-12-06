using System;
using System.Collections.Generic;

namespace FoodDatabase.Core.API.Models
{
    /// <summary>
    /// This class represents a food item of the API.
    /// </summary>
    public class Item
    {
        public string id { get; set; }
        public string markedfordeletion { get; set; }
        // Per hand
        public string thumbsrc { get; set; }
        // Per hand
        public string thumbsrclarge { get; set; }
        public string foodrank { get; set; }
        public string ratings_num { get; set; }
        public string ratings_avg_perc { get; set; }
        public string producerid { get; set; }
        public string groupid { get; set; }
        // Per hand
        public string productcode_ean { get; set; }
        public string datasource { get; set; }
        public Data data { get; set; }
        public List<Serving> servings { get; set; }
    }
}
