using System;
using System.Xml.Serialization;
using FoodDatabase.Core.API.Models.Item;

namespace FoodDatabase.Core.API.Models.Diary
{
    /// <summary>
    /// Contains data for a diary item.
    /// </summary>
    public class DiaryShortItem
    {
        public string itemid { get; set; }

        [XmlElement("data")]
        public Data Data { get; set; }

        [XmlElement("description")]
        public Description Description { get; set; }

        public override string ToString()
        {
            return string.Format("#{0} {1} from {2}", itemid, 
                                 Description.name, Description.producer);
        }
    }
}
