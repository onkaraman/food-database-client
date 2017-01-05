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
        private ProgressBar _progBar;
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
            assignEvents();
            getDiary();
        }

        /// <summary>
        /// Will connect the UI to the local fields of this activity.
        /// </summary>
        private void setupViews()
        {
            _progBar = FindViewById<ProgressBar>(Resource.Id.DiaryProgressBar);
            _title = FindViewById<TextView>(Resource.Id.DiaryTitle);
            _date = FindViewById<TextView>(Resource.Id.DiaryDate);
            _listView = FindViewById<ListView>(Resource.Id.DiaryListView);

            _progBar.Visibility = Android.Views.ViewStates.Invisible;
            _date.Text = _dateTime.ToString("dd.MM.yyyy");
        }

        /// <summary>
        /// Assigns the events of the controls to methods of this activity.
        /// </summary>
        private void assignEvents()
        {
            _date.Click += dateClick;
        }

        /// <summary>
        /// Will open the date picker for the user to see the diary of that day.
        /// </summary>
        private void dateClick(object sender, EventArgs e)
        {
            DateTime now = DateTime.UtcNow;
            DatePickerDialog diag = new DatePickerDialog(this, datePicked,
                now.Year, now.Month - 1, now.Day);
            diag.Show();
        }

        /// <summary>
        /// Once the user picked a date, the diary will be updated.
        /// </summary>
        private void datePicked(object sender, DatePickerDialog.DateSetEventArgs e)
        {
            _dateTime = e.Date;
            _date.Text = _dateTime.ToString("dd.MM.yyyy");
            getDiary();
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
                _progBar.Visibility = Android.Views.ViewStates.Visible;
                ThreadPool.QueueUserWorkItem(async o =>
                {
                    string response = await APIAccessor.Static.DiaryGet(SessionHolder.Static.LoginData, _dateTime);
                    var result = APIParser.Static.Parse(response);

                    RunOnUiThread(() =>
                    {
                        _listView.Adapter = new DiaryItemAdapter(result.DiaryElements, this);
                        _progBar.Visibility = Android.Views.ViewStates.Invisible;
                    });
                });
            }
        }

    }
}
