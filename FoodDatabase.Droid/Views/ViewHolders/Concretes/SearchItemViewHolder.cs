using System;
using Android.Widget;
using System.Threading;
using Android.App;
using FoodDatabase.Droid.Views.Adapters;
using Android;
using FoodDatabase.Droid.Views.Widgets;
using FoodDatabase.Core.API.Models.Item;

namespace FoodDatabase.Views.ViewHolders.Concretes
{
    /// <summary>
    /// Holds the UI elements of a search item for caching.
    /// </summary>
    public class SearchItemViewHolder : GeneralViewHolder
    {
        public ImageView Thumbnail { get; set; }
        public MainTextView SaleMinutes { get; set; }
        public MainTextView Name { get; set; }
        public ImageView LocationIcon { get; set; }
        public ImageView MealIcon { get; set; }
        public MainTextView LocationLabel { get; set; }
        public MainTextView AmountLabel { get; set; }
        public MainTextView PriceLabel { get; set; }

        /// <summary>
        /// Will apply the passed meal data to the UI elements.
        /// </summary>
        /// <param name="searchItem">Meal to apply for this view holder.</param>
        public void ApplyData(Item searchItem, Activity a)
        {
            ThreadPool.QueueUserWorkItem(o => loadThumbnail(searchItem, a));
            SaleMinutes.Text = searchItem.RemainingSaleMinutes();
            Thumbnail.SetImageResource(Resource.Color.Transparent);

            int cut = 90;
            if (searchItem.name.Length > cut) Name.Text = searchItem.name.Substring(0, cut).Trim();
            else Name.Text = searchItem.name.Trim();

            Name.MakeBold();

            LocationLabel.Text = String.Format("{0} / {1}",
                searchItem.location.city_name, UnitCalculator.Static.CalcDistance(searchItem));

            AmountLabel.Text = String.Format(Localization.Static.Raw(ResourceKeyNames.Static.PortionsLeft), searchItem.amount);

            PriceLabel.Text = String.Format("{0} {1}",
                String.Format("{0:0.00#}", searchItem.price),
                Localization.Static.Raw(ResourceKeyNames.Static.CurrencySymbol));
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
                        imgLoader.DisplayImage(searchItem.image_url, Thumbnail);
                    });
            }
            catch (Exception ex)
            {
                //TODO: Reporter here.
            }
        }

    }
}

