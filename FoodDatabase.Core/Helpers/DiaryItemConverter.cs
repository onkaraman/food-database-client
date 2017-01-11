using FoodDatabase.Core.API.Models.Diary;
using FoodDatabase.Core.API.Models.Items;
using FoodDatabase.Core.Managers;
using FoodDatabase.Core.Patterns;

namespace FoodDatabase.Core.Helpers
{
    /// <summary>
    /// Will convert a diary item to a standard API item.
    /// </summary>
    public class DiaryItemConverter : LazyStatic<DiaryItemConverter>
    {
        public DiaryItemConverter() { }

        /// <summary>
        /// Will convert a diary element to a standard item in order to display it in 
        /// the detail activity.
        /// </summary>
        public Item ConvertToItem(DiaryElement de)
        {
            de.DiaryShortItem.Description.producer = string.Format("Added on {0}", de.DateInTime.ToString("dd.MM.yyyy"));
            de.DiaryShortItem.Description.group = "In your diary";

            Item i = new Item
            {
                id = de.id,
                Data = de.DiaryShortItem.Data,
                Description = de.DiaryShortItem.Description,
            };

            try
            {
                i.thumbsrc = PersistenceManager.Static.GetFirst(string.Format("{0}-img", de.DiaryShortItem.id)).Value;
                i.thumbsrclarge = i.thumbsrc;
            }
            catch { i.Description.group = "Added from other app."; }

            return i;
        }
    }
}
