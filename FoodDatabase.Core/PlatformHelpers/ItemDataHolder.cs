using System;
using FoodDatabase.Core.API.Models.Items;

namespace FoodDatabase.Core.PlatformHelpers
{
    /// <summary>
    /// This class holds item data for ListView purposes.
    /// </summary>
    public class ItemDataHolder
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string Unit { get; set; }
    }
}
