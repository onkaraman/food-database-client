using System;
using System.Threading;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using FoodDatabase.Core.API.Accessors;
using FoodDatabase.Core.API.Models.Diary;
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
              WindowSoftInputMode = Android.Views.SoftInput.AdjustPan,
      ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class DiaryActivity : Activity
    {
        private int _kcals;
        private string[] _contextMenuItems;
        private Core.API.Models.Result _result;
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
            _contextMenuItems = new string[] { "Delete" };

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
            RegisterForContextMenu(_listView);

            _progBar.Visibility = ViewStates.Invisible;
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
        /// Will open the context menu on a list item to give the user
        /// the option to delete an item.
        /// </summary>
        public override void OnCreateContextMenu(IContextMenu menu, View v, IContextMenuContextMenuInfo menuInfo)
        {
            if (v.Id == Resource.Id.DiaryListView)
            {
                var info = (AdapterView.AdapterContextMenuInfo) menuInfo;
                menu.SetHeaderTitle(_result.DiaryElements[info.Position].DiaryShortItem.Description.name);
                var menuItems = new string[] { "Delete" };

                for (var i = 0; i < menuItems.Length; i++)
                    menu.Add(Menu.None, i, i, menuItems[i]);
            }
        }

        /// <summary>
        /// Will detect the selected context menu item for the diary item.
        /// </summary>
        public override bool OnContextItemSelected(IMenuItem item)
        {
            var info = (AdapterView.AdapterContextMenuInfo)item.MenuInfo;
            var menuItemIndex = item.ItemId;
            var listItemName = _contextMenuItems[menuItemIndex];

            deleteSelectedItem(info.Position);
            return true;
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
                _progBar.Visibility = ViewStates.Visible;
                ThreadPool.QueueUserWorkItem(async o =>
                {
                    string response = await APIAccessor.Static.DiaryGet(SessionHolder.Static.LoginData, _dateTime);
                    _result = APIParser.Static.Parse(response);

                    RunOnUiThread(() =>
                    {
                        _listView.Adapter = new DiaryItemAdapter(_result.DiaryElements, this);
                        countKcals();
                        _progBar.Visibility = ViewStates.Invisible;
                    });
                });
            }
        }

        /// <summary>
        /// Will delete the selected item from the diary.
        /// </summary>
        /// <param name="index">Selected item from the list view.</param>
        private void deleteSelectedItem(int index)
        {
            _progBar.Visibility = ViewStates.Visible;

            try
            {
                string uid = _result.DiaryElements[index].diary_uid;

                ThreadPool.QueueUserWorkItem(async o =>
                {
                    string removeResponse = await APIAccessor.Static.DiaryRemove(SessionHolder.Static.LoginData, uid);

                    RunOnUiThread(() =>
                    {
                        _progBar.Visibility = ViewStates.Invisible;
                        getDiary();
                    });
                });
            }
            catch (Exception)
            {
                //TODO: Report
            }
        }

        /// <summary>
        /// Will go through every diary item and add its kcals to the daily summary.
        /// </summary>
        private void countKcals()
        {
            foreach (DiaryElement dsi in _result.DiaryElements)
            {
                _kcals += dsi.DiaryShortItem.Data.kcal_diary;
            }
        }

        public override bool OnKeyDown(Keycode keyCode, KeyEvent e)
        {
            if (e.KeyCode == Keycode.Back)
            {
                StartActivity(typeof(MainActivity));
                return true;
            }
            return base.OnKeyDown(keyCode, e);
        }
    }
}
