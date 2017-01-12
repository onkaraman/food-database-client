using System;
using Android.Widget;
using FoodDatabase.Core.Persistence.Models;
using FoodDatabase.Droid.Views.Adapters;

namespace FoodDatabase.Droid.Views.ViewHolders.Concretes
{
    /// <summary>
    /// Holds information of a recent search.
    /// </summary>
    public class RecentSearchViewHolder : GeneralViewHolder
    {
        public TextView Name { get; set; }

        public void ApplyData(SimpleDBItem item)
        {
            Name.Text = item.Value;
        }
    }
}
