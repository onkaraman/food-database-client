using System;
using FoodDatabase.Core.API.Models.Abstracts;
using FoodDatabase.Core.API.Models.Items;

namespace FoodDatabase.Core.Sessions
{
    /// <summary>
    /// This class holds item data for ListView purposes.
    /// </summary>
    public class ItemDataHolder : APIModel
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string Unit { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1}{2}", Name, Value, Unit);
        }
    }
}
