using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;
using FoodDatabase.Core.API.Models.Abstracts;
using FoodDatabase.Core.Persistence.Models;
using FoodDatabase.Droid.Views.ViewHolders.Concretes;

namespace FoodDatabase.Droid.Views.Adapters.Concretes
{
    public class RecentSearchAdapter : GeneralAdapter
    {
        public RecentSearchAdapter(List<SimpleDBItem> dbitems, Context c)
        {
            context = c;
            items = new List<APIModel>();
            items.AddRange(dbitems);
        }

        public override long GetItemId(int position)
        {
            return items[position].id;
        }

        protected override View createView()
        {
            return activity.LayoutInflater.Inflate(Resource.Layout.RecentSearchItem,
                parentView, false);
        }

        protected override GeneralViewHolder populateHolder(GeneralViewHolder _holder)
        {
            SimpleDBItem item = (SimpleDBItem)items[itemPosition];
            RecentSearchViewHolder holder = (RecentSearchViewHolder)_holder;
            if (item != null) holder.ApplyData(item);

            return holder;
        }

        protected override GeneralViewHolder setupView()
        {
            RecentSearchViewHolder holder = new RecentSearchViewHolder();
            holder.Name = convertView.FindViewById<TextView>(Resource.Id.RecentSearchName);
            return holder;
        }
    }
}
