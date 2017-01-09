using System;
using FoodDatabase.Core.API.Models.Abstracts;

namespace FoodDatabase.Core.Persistence.Models
{
    public class SimpleDBItem : APIModel
    {
        public string Key { get; }
        public string Value { get; }

        public SimpleDBItem() {}

        public SimpleDBItem(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }
}
