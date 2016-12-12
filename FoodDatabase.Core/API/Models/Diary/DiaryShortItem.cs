using System;
using System.Xml.Serialization;
using FoodDatabase.Core.API.Models.Abstracts;
using FoodDatabase.Core.API.Models.Item;

namespace FoodDatabase.Core.API.Models.Diary
{
    /// <summary>
    /// Contains data for a diary item.
    /// </summary>
    public class DiaryShortItem : APIModel
    {
        public string itemid
        {
            get { return id.ToString(); }
            set { id = int.Parse(value); }
        }

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
