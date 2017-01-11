using System;
using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;
using FoodDatabase.Core.API.Models.Abstracts;
using FoodDatabase.Core.API.Models.Items;
using FoodDatabase.Core.Helpers;
using FoodDatabase.Droid.Views.ViewHolders.Concretes;

namespace FoodDatabase.Droid.Views.Adapters.Concretes
{
    public class ServingItemAdapter : GeneralAdapter
    {
        public ServingItemAdapter(List<Serving> servings, Context c)
        {
            context = c;
            items = new List<APIModel>();
            items.AddRange(servings);
        }

        public override long GetItemId(int position)
        {
            return items[position].id;
        }

        protected override View createView()
        {
            return activity.LayoutInflater.Inflate(Resource.Layout.ServingItem,
                parentView, false);
        }

        protected override GeneralViewHolder populateHolder(GeneralViewHolder _holder)
        {
            Serving item = (Serving)items[itemPosition];
            ServingItemViewHolder holder = (ServingItemViewHolder)_holder;
            if (item != null) holder.ApplyData(item, int.Parse(SessionHolder.Static.Item.Data.kcal));

            return holder;
        }

        protected override GeneralViewHolder setupView()
        {
            ServingItemViewHolder holder = new ServingItemViewHolder();
            holder.Name = convertView.FindViewById<TextView>(Resource.Id.ServingItemName);
            holder.Description = convertView.FindViewById<TextView>(Resource.Id.ServingItemDescription);
            holder.Kcal = convertView.FindViewById<TextView>(Resource.Id.ServingItemKcal);
            return holder;
        }
    }
}
