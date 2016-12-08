using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace FoodDatabase.Core.API.Models.Item
{
    [XmlRoot("result")]
    public class Result
    {
        [XmlArray("items")]
        [XmlArrayItem("item")]
        public List<Item> Items { get; set; }
    }
}
