using FoodDatabase.Core.Persistence.Models;
using FoodDatabase.Droid.Views.Adapters;
using FoodDatabase.Droid.Views.Controls;

namespace FoodDatabase.Droid.Views.ViewHolders.Concretes
{
    /// <summary>
    /// Holds information of a recent search.
    /// </summary>
    public class RecentSearchViewHolder : GeneralViewHolder
    {
        public MainTextView Name { get; set; }

        public void ApplyData(SimpleDBItem item)
        {
            Name.Text = item.Value;
        }
    }
}
