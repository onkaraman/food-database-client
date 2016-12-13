using System.Collections.Generic;
using Android.Content;
using Android.Widget;
using Android.Views;
using FoodDatabase.Core.API.Models.Abstracts;
using FoodDatabase.Core.API.Models.Item;

namespace FoodDatabase.Droid.Views.Adapters.Concretes
{
    public class SearchItemAdapter : GeneralAdapter
    {
        public SearchItemAdapter(List<Item> searchItems, Context c)
        {
            context = c;
            items = new List<APIModel>();
            items.AddRange(searchItems);
        }

        public override long GetItemId(int position)
        {
            return items[position].id;
        }

        protected override View createView()
        {
            return activity.LayoutInflater.Inflate(Resource.Layout.SearchItem,
                _parentView, false);
        }
    
        protected override GeneralViewHolder populateHolder(GeneralViewHolder _holder)
        {
            Meal meal = (Meal)items[itemPosition];
            SearchItemViewHolder holder = (SearchItemViewHolder)_holder;
            if (meal != null) holder.ApplyData(meal, activity);

            return holder;
        }

        protected override GeneralViewHolder setupView()
        {
            SearchItemViewHolder holder = new SearchItemViewHolder();
            holder.Thumbnail = _convertView.FindViewById<BlockingImageView>(Resource.Id.searchItemThumbnail);
            holder.SaleMinutes = _convertView.FindViewById<HummusTextView>(Resource.Id.searchItemSaleMinutes);
            holder.Name = _convertView.FindViewById<HummusTextView>(Resource.Id.searchItemName);
            holder.LocationIcon = _convertView.FindViewById<ImageView>(Resource.Id.searchItemLocationIcon);
            holder.MealIcon = _convertView.FindViewById<ImageView>(Resource.Id.searchItemMealIcon);
            holder.LocationLabel = _convertView.FindViewById<HummusTextView>(Resource.Id.searchItemLocationLabel);
            holder.AmountLabel = _convertView.FindViewById<HummusTextView>(Resource.Id.searchItemAmountLabel);
            holder.PriceLabel = _convertView.FindViewById<HummusTextView>(Resource.Id.searchItemPriceLabel);
            return holder;
        }

    }
}

