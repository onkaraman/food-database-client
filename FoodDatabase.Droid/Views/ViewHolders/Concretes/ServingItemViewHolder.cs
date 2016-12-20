using Android.Widget;
using FoodDatabase.Core.API.Models.Items;
using FoodDatabase.Droid.Views.Adapters;

namespace FoodDatabase.Droid.Views.ViewHolders.Concretes
{
    /// <summary>
    /// Holds information for a serving UI list element.
    /// </summary>
    public class ServingItemViewHolder : GeneralViewHolder
    {
        public TextView Name { get; set; }
        public TextView Description { get; set; }
        public TextView Kcal { get; set; }

        public void ApplyData(Serving s, int kcal100)
        {
            Name.Text = s.name;
            Description.Text = string.Format("{0}g", s.weight_gram);
            Kcal.Text = ((kcal100 / 100) * int.Parse(s.weight_gram)).ToString();
        }
    }
}
