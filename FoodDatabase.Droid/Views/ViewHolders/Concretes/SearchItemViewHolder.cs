using System;
using System.Threading;
using Android;
using Android.App;
using Android.Widget;
using FoodDatabase.Core.API.Models.Item;
using FoodDatabase.Droid.Views.Adapters;
using FoodDatabase.Droid.Views.Widgets;
using UniversalImageLoader.Core;

namespace FoodDatabase.Views.ViewHolders.Concretes
{
    /// <summary>
    /// Holds the UI elements of a search item for caching.
    /// </summary>
    public class SearchItemViewHolder : GeneralViewHolder
    {
        public ImageView Thumbnail { get; set; }
        public MainTextView Name { get; set; }
        public MainTextView Producer { get; set; }
        public MainTextView Kcal { get; set; }
        public MainTextView Proteins { get; set; }
        public MainTextView Carbohydrates { get; set; }
        public MainTextView Sugar { get; set; }
        public MainTextView Fat { get; set; }

        /// <summary>
        /// Will apply the passed meal data to the UI elements.
        /// </summary>
        /// <param name="searchItem">Meal to apply for this view holder.</param>
        public void ApplyData(Item searchItem, Activity a)
        {
            ThreadPool.QueueUserWorkItem(o => loadThumbnail(searchItem, a));
            Thumbnail.SetImageResource(Resource.Color.Transparent);

            Name.Text = shortenName(searchItem.Description.name, 30);
            Producer.Text = shortenName(searchItem.Description.producer, 30);
            Kcal.Text = string.Format("{0}", searchItem.Data.kcal);
            Proteins.Text = string.Format("{0}g", searchItem.Data.protein_gram);
            Carbohydrates.Text = string.Format("{0}g", searchItem.Data.kh_gram);
            Sugar.Text = string.Format("{0}g", searchItem.Data.sugar_gram);
            Fat.Text = string.Format("{0}g", searchItem.Data.fat_gram);
        }

        /// <summary>
        /// Will load the thumbnail into the ImageView UI.
        /// </summary>
        private void loadThumbnail(Item searchItem, Activity a)
        {
            try
            {
                a.RunOnUiThread(() =>
                    {
                        ImageLoader imgLoader = ImageLoader.Instance;
                        imgLoader.DisplayImage(searchItem.thumbsrc, Thumbnail);
                    });
            }
            catch (Exception ex)
            {
                //TODO: Reporter here.
            }
        }

        /// <summary>
        /// Will return the passed item name in substringed if required.
        /// </summary>
        private string shortenName(string name, int cutLength)
        {
            if (name.Length > cutLength)
                return name.Substring(0, cutLength).Trim();
            return name.Trim();
        }

    }
}

