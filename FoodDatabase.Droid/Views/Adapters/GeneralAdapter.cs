using System;
using System.Collections.Generic;
using System.Diagnostics;
using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using FoodDatabase.Core.API.Models.Abstracts;

namespace FoodDatabase.Droid.Views.Adapters
{
	public class GeneralAdapter : BaseAdapter
	{
        protected List<APIModel> items;
        protected View _convertView;
        protected ViewGroup _parentView;
        protected int itemPosition;
        protected Context context;
        protected Activity activity { get {return (Activity) context;}}

        public override int Count
        {
            get 
			{ 
                if (items != null) return items.Count;
                return 0;
			}
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }
            
        public override long GetItemId(int position)
        {
            try
            {
                //if (position == 0) position = 1;
                return items[position].id;
            }
            catch(Exception e)
            {
                Debugger.Break();
                Console.WriteLine(e.StackTrace);
            }
            return 0;
        }

        /// <summary>
        /// Returns the View of a Participant.
        /// Tries to use convertView as a View already rendered.
        /// If the View hasn't been rendered yet, it gets rendered and stored in its
        /// ViewHolder to keep things efficient.
        /// </summary>
        public override View GetView(int position, View convertView, ViewGroup parent)
		{
			GeneralViewHolder holder = null;

            try
            {
    			_convertView = convertView;
    			_parentView = parent;
    			itemPosition = position;

    			if (_convertView != null)
                {
                    holder = _convertView.Tag as GeneralViewHolder;
                }
                else
                {
                    _convertView = createView();
                    holder = setupView();
                    _convertView.Tag = holder;
                }

                populateHolder(holder);
                return _convertView;
            }
            catch (Exception ex)
            {
                Console.WriteLine("GeneralAdapter ex: " + ex.Message);
                //TODO: Report error.
            }

            return null;
		}

        /// <summary>
        /// Inflates the layout.
        /// </summary>
        protected virtual View createView()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sets up the view holder.
        /// </summary>
        protected virtual GeneralViewHolder setupView()
        {
            throw new NotImplementedException();
        }
    
        /// <summary>
        /// Will load data into the viewholder.
        /// </summary>
        protected virtual GeneralViewHolder populateHolder(GeneralViewHolder _holder)
        {
            throw new NotImplementedException();
        }
    }
}

