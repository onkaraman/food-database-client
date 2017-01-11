using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;
using FoodDatabase.Core.API.Models.Abstracts;
using FoodDatabase.Core.Helpers;
using FoodDatabase.Droid.Views.ViewHolders.Concretes;

namespace FoodDatabase.Droid.Views.Adapters.Concretes
{
    public class NutritionItemAdapter : GeneralAdapter
    {
        public NutritionItemAdapter(List<ItemDataHolder> nutritionData, Context c)
        {
            context = c;
            items = new List<APIModel>();
            items.AddRange(nutritionData);
        }

        public override long GetItemId(int position)
        {
            return items[position].id;
        }

        protected override View createView()
        {
            return activity.LayoutInflater.Inflate(Resource.Layout.NutritionItem,
                parentView, false);
        }

        protected override GeneralViewHolder populateHolder(GeneralViewHolder _holder)
        {
            ItemDataHolder item = (ItemDataHolder)items[itemPosition];
            NutritionItemViewHolder holder = (NutritionItemViewHolder)_holder;
            if (item != null) holder.ApplyData(item);

            return holder;
        }

        protected override GeneralViewHolder setupView()
        {
            NutritionItemViewHolder holder = new NutritionItemViewHolder();
            holder.Name = convertView.FindViewById<TextView>(Resource.Id.NutritionItemName);
            holder.Value = convertView.FindViewById<TextView>(Resource.Id.NutritionItemValue);
            return holder;
        }
    }
}
