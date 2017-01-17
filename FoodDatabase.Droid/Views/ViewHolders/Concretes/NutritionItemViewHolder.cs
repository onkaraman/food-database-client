using Android.App;
using Android.Widget;
using FoodDatabase.Core.Helpers;
using FoodDatabase.Droid.Views.Adapters;
using FoodDatabase.Droid.Views.Controls;

namespace FoodDatabase.Droid.Views.ViewHolders.Concretes
{
    /// <summary>
    /// Holds the UI element for a nutrition item inside the detail layout.
    /// </summary>
    public class NutritionItemViewHolder : GeneralViewHolder
    {
        public MainTextView Name { get; set; }
        public MainTextView Value { get; set; }

        public void ApplyData(ItemDataHolder idh)
        {
            Name.Text = idh.Name;
            Value.Text = string.Format("{0}{1}", idh.Value, idh.Unit);
        }
    }
}
