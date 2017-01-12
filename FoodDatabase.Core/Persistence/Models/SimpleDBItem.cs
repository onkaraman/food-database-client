using System;
using System.Collections.Generic;
using FoodDatabase.Core.API.Models.Abstracts;

namespace FoodDatabase.Core.Persistence.Models
{
    public class SimpleDBItem : APIModel
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public DateTime TimeStamp { get; set; }

        public SimpleDBItem() {}

        public SimpleDBItem(string key, string value)
        {
            id = key.GetHashCode();
            Key = key;
            Value = value;
            TimeStamp = DateTime.Now;
        }
    }
}
