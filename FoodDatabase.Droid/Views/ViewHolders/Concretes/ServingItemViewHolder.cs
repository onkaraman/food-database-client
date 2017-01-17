using System;
using FoodDatabase.Core.API.Models.Items;
using FoodDatabase.Core.Helpers;
using FoodDatabase.Droid.Views.Adapters;
using FoodDatabase.Droid.Views.Controls;

namespace FoodDatabase.Droid.Views.ViewHolders.Concretes
{
    /// <summary>
    /// Holds information for a serving UI list element.
    /// </summary>
    public class ServingItemViewHolder : GeneralViewHolder
    {
        public MainTextView Name { get; set; }
        public MainTextView Description { get; set; }
        public MainTextView Kcal { get; set; }

        public void ApplyData(Serving s, int kcal100)
        {
            Name.Text = HTMLCleaner.Static.Replace(s.name);
            Description.Text = string.Format("{0}g", s.weight_gram);
            double _kcal = (((double)kcal100 / 100) * int.Parse(s.weight_gram));
            Kcal.Text = Math.Round(_kcal).ToString();
        }
    }
}
