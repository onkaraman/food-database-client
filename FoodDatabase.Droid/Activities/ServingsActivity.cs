using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FoodDatabase.Core.Sessions;
using FoodDatabase.Droid.Views.Adapters.Concretes;

namespace FoodDatabase.Droid.Activities
{
    /// <summary>
    /// This activity offers the user to add an amount of the
    /// selected food to his diary.
    /// </summary>
    [Activity(Label = "ServingsActivity",
      WindowSoftInputMode = SoftInput.AdjustResize,
      ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class ServingsActivity : Activity
    {
        private TextView _name;
        private TextView _description;
        private TextView _servingPresetDescription;
        private ListView _listView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Servings);
            setupViews();
            applyData();
        }

        /// <summary>
        /// Connects the UI controls to the fields of this activity.
        /// </summary>
        private void setupViews()
        {
            _name = FindViewById<TextView>(Resource.Id.ServingFoodName);
            _description = FindViewById<TextView>(Resource.Id.ServingDescription);
            _servingPresetDescription = FindViewById<TextView>(Resource.Id.ServingPresetDescription);
            _listView = FindViewById<ListView>(Resource.Id.ServingListView);
        }

        /// <summary>
        /// Will apply the data of the session item to the controls of this activity.
        /// </summary>
        private void applyData()
        {
            _name.Text = SessionHolder.Static.Item.Description.name;
            _listView.Adapter = new ServingItemAdapter(SessionHolder.Static.Item.Servings, this);
        }
    }
}
