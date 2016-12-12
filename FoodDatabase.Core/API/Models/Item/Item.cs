using System.Collections.Generic;
using System.Xml.Serialization;
using FoodDatabase.Core.API.Models.Abstracts;

namespace FoodDatabase.Core.API.Models.Item
{
    /// <summary>
    /// This class represents a food item of the API.
    /// </summary>
    public class Item : APIModel
    {
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

        [XmlElement("description")]
        public Description Description { get; set; }

        [XmlArray("servings")]
        [XmlArrayItem("serving")]
        public List<Serving> Servings { get; set; }

        public override string ToString()
        {
            return string.Format("#{0} {1} from {2}", id, Description.name, Description.producer);
        }
    }
}
