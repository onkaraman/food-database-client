using Android.App;
using Android.Widget;
using FoodDatabase.Core.PlatformHelpers;
using FoodDatabase.Droid.Views.Adapters;

namespace FoodDatabase.Droid.Views.ViewHolders.Concretes
{
    /// <summary>
    /// Holds the UI element for a nutrition item inside the detail layout.
    /// </summary>
    public class NutritionItemViewHolder : GeneralViewHolder
    {
        public TextView Name { get; set; }
        public TextView Value { get; set; }

        public void ApplyData(ItemDataHolder idh)
        {
            Name.Text = idh.Name;
            Value.Text = string.Format("{0}{1}", idh.Value, idh.Unit);
        }
    }
}
