using System;
using FoodDatabase.Core.API.Models.Diary;
using FoodDatabase.Core.API.Models.Items;
using FoodDatabase.Core.Patterns;

namespace FoodDatabase.Core.PlatformHelpers
{
    /// <summary>
    /// Will convert a diary item to a standard API item.
    /// </summary>
    public class DiaryItemConverter : LazyStatic<DiaryItemConverter>
    {
        public DiaryItemConverter(){}

        public Item ConvertToItem(DiaryShortItem ds, string producer="", string group="")
        {
            Item i = new Item();
            i.Data = ds.Data;
            i.Description = ds.Description;
            i.Description.producer = producer;
            i.Description.group = group;
            i.id = ds.id;
            return i;
        }
    }
}
