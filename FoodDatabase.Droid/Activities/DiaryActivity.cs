using System;
using System.Threading;
using Android.App;
using Android.OS;
using Android.Widget;
using FoodDatabase.Core.API.Accessors;
using FoodDatabase.Core.API.Parsers;
using FoodDatabase.Core.Sessions;
using FoodDatabase.Droid.Views.Adapters.Concretes;

namespace FoodDatabase.Droid.Activities
{
    /// <summary>
    /// This activity shows the diary of the logged in user 
    /// in which he can see everything s/he has taken today.
    /// </summary>
    [Activity(Label = "DiaryActivity",
          MainLauncher = true,
          Icon = "@drawable/icon",
          ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class DiaryActivity : Activity
    {
        private DateTime _dateTime;
        private TextView _title;
        private TextView _date;
        private ListView _listView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Diary);
            _dateTime = DateTime.Today;
            setupViews();
            getDiary();
        }

        /// <summary>
        /// Will connect the UI to the local fields of this activity.
        /// </summary>
        private void setupViews()
        {
            _title = FindViewById<TextView>(Resource.Id.DiaryTitle);
            _date = FindViewById<TextView>(Resource.Id.DiaryDate);
            _listView = FindViewById<ListView>(Resource.Id.DiaryListView);

            _date.Text = _dateTime.ToString("dd.MM.yyyy");
        }

        /// <summary>
        /// Will fetch and show the diary of the local date time.
        /// </summary>
        private void getDiary()
        {
            if (SessionHolder.Static.LoginData == null)
            {
                SessionHolder.Static.FromDiary = true;
                StartActivity(typeof(LoginActivity));
            }
            else
            {
                ThreadPool.QueueUserWorkItem(async o =>
                {
                    string response = await APIAccessor.Static.DiaryGet(SessionHolder.Static.LoginData, _dateTime);
                    var result = APIParser.Static.Parse(response);

                    RunOnUiThread(() =>
                    {
                        _listView.Adapter = new DiaryItemAdapter(result.DiaryElements, this);
                    });
                });
            }
        }

    }
}
