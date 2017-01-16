using System.Collections.Generic;
using Android.Content;
using Android.Widget;
using Android.Views;
using FoodDatabase.Core.API.Models.Abstracts;
using FoodDatabase.Core.API.Models.Items;
using FoodDatabase.Views.ViewHolders.Concretes;
using FoodDatabase.Droid.Views.Controls;

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
                parentView, false);
        }
    
        protected override GeneralViewHolder populateHolder(GeneralViewHolder _holder)
        {
            Item item = (Item)items[itemPosition];
            SearchItemViewHolder holder = (SearchItemViewHolder)_holder;
            if (item != null) holder.ApplyData(item, activity);

            return holder;
        }

        protected override GeneralViewHolder setupView()
        {
            SearchItemViewHolder holder = new SearchItemViewHolder();
            holder.Thumbnail = convertView.FindViewById<ImageView>(Resource.Id.SearchItemThumbnail);
            holder.Name = convertView.FindViewById<MainTextView>(Resource.Id.SearchItemName);
            holder.Producer = convertView.FindViewById<MainTextView>(Resource.Id.SearchItemProducer);
            holder.Kcal = convertView.FindViewById<MainTextView>(Resource.Id.SearchItemKcals);
            holder.Proteins = convertView.FindViewById<MainTextView>(Resource.Id.SearchItemProteins);
            holder.Carbohydrates = convertView.FindViewById<MainTextView>(Resource.Id.SearchItemCHs);
            holder.Sugar = convertView.FindViewById<MainTextView>(Resource.Id.SearchItemSugar);
            holder.Fat = convertView.FindViewById<MainTextView>(Resource.Id.SearchItemFat);
            return holder;
        }

    }
}

