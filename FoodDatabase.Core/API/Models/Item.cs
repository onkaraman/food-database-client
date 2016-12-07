using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace FoodDatabase.Core.API.Models
{
    /// <summary>
    /// This class represents a food item of the API.
    /// </summary>
    public class Item
    {
        public string id { get; set; }
        public string markedfordeletion { get; set; }
        public string thumbsrc { get; set; }
        public string thumbsrclarge { get; set; }
        public string foodrank { get; set; }
        public string ratings_num { get; set; }
        public string ratings_avg_perc { get; set; }
        public string producerid { get; set; }
        public string groupid { get; set; }
        public string productcode_ean { get; set; }
        public string datasource { get; set; }

        [XmlElement("data")]
        public Data Data { get; set; }

        [XmlArray("servings")]
        [XmlArrayItem("serving")]
        public List<Serving> Servings { get; set; }
    }
}
