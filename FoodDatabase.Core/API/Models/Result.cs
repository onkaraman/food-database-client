using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using FoodDatabase.Core.API.Models.Diary;
using FoodDatabase.Core.API.Models.Items;

namespace FoodDatabase.Core.API.Models
{
    [XmlRoot("result")]
    public class Result
    {
        [XmlElement("stats")]
        public Stats Stats { get; set; }

        [XmlArray("items")]
        [XmlArrayItem("item")]
        public List<Item> Items { get; set; }

        [XmlElement("diaryelement")]
        public List<DiaryElement> DiaryElements { get; set; }
    }
}
