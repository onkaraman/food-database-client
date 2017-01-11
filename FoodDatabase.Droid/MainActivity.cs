using System;
using System.Collections.Generic;
using System.Threading;
using ActionRadar.Core.Managers;
using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using FoodDatabase.Core.API.Accessors;
using FoodDatabase.Core.API.Models.Items;
using FoodDatabase.Core.API.Parsers;
using FoodDatabase.Core.Managers;
using FoodDatabase.Core.Security;
using FoodDatabase.Core.Helpers;
using FoodDatabase.Droid.Activities;
using FoodDatabase.Droid.Persistence;
using FoodDatabase.Droid.Views.Adapters.Concretes;
using UniversalImageLoader.Core;

namespace FoodDatabase.Droid
{
    /// <summary>
    /// The  main activity contains the search funtionality and access to settings
    /// and user diary.
    /// </summary>
    [Activity(Label = "Food Database",
          MainLauncher = true,
          Icon = "@drawable/icon",
          ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class MainActivity : Activity
    {
        private ImageView _menuButton;
        private ImageView _settingsButton;
        private List<Item> _items;
        private EditText _searchField;
        private ListView _listView;
        private ProgressBar _progBar;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);
            DBManager.Static.Init(new DBConnection());
            checkLogin();
            setupViews();
            assignEvents();
        }

        /// <summary>
        /// Will check and apply login data if given.
        /// </summary>
        private void checkLogin()
        {
            try
            {
                string username = PersistenceManager.Static.GetFirst("username").Value;
                string password = Encrypter.Static.Decrypt(PersistenceManager.Static.GetFirst("password").Value);
                SessionHolder.Static.LoginData = new LoginData(username, password);
            }
            catch (Exception)
            {
                //TODO: Handle
            }
        }

        /// <summary>
        /// Will connect the fields to the views of the layout.
        /// </summary>
        private void setupViews()
        {
            _progBar = FindViewById<ProgressBar>(Resource.Id.MainProgressBar);
            _menuButton = FindViewById<ImageView>(Resource.Id.MainMenuIcon);
            _settingsButton = FindViewById<ImageView>(Resource.Id.MainMenuSettings);
            _searchField = FindViewById<EditText>(Resource.Id.MainEditText);
            _listView = FindViewById<ListView>(Resource.Id.MainListView);
            _progBar.Visibility = ViewStates.Invisible;
        }

        /// <summary>
        /// Will assign events to the views of the layout.
        /// </summary>
        private void assignEvents()
        {
            _menuButton.Click += menuButtonClick;
            _settingsButton.Click += settingsButtonClick;
            _searchField.KeyPress += searchFieldKeyPress;
            _listView.ItemClick += listItemClick;
        }

        /// <summary>
        /// Will handle the user input if he wants to initiate a search.
        /// </summary>
        private void searchFieldKeyPress(object sender, View.KeyEventArgs e)
        {
            if (e.KeyCode == Keycode.Enter)
            {
                search(_searchField.Text);
            }
        }

        /// <summary>
        /// Will open the detail activity to show detailed information regarding the
        /// selected food item.
        /// </summary>
        private void listItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            if (e == null) return;
            SessionHolder.Static.Item = _items[e.Position];
            SessionHolder.Static.FromDiary = false;
            SessionHolder.Static.FromServing = false;
            StartActivity(typeof(DetailActivity));
        }

        /// <summary>
        /// Will start a search for the passed query. Before listing the result,
        /// the result will be re-searched until more result items are found.
        /// </summary>
        private void search(string query)
        {
            _progBar.Visibility = ViewStates.Visible;
            hideKeyboard();

            PersistenceManager.Static.AddAndPersist("search", query);

            ThreadPool.QueueUserWorkItem(async o =>
            {
                string response = await APIAccessor.Static.Search(query);
                var result = APIParser.Static.Parse(response);

                for (int i = 1; i <= 10; i += 1)
                {
                    response = await APIAccessor.Static.Search(query, (i * 10).ToString());
                    result.Items.AddRange(APIParser.Static.Parse(response).Items);
                }

                _items = result.Items;

                RunOnUiThread(() =>
                {
                    _listView.Adapter = new SearchItemAdapter(result.Items, this);
                    _progBar.Visibility = ViewStates.Invisible;
                });
            });
        }

        private void hideKeyboard()
        {
            if (CurrentFocus != null)
            {
                InputMethodManager imm = (InputMethodManager)GetSystemService(InputMethodService);
                imm.HideSoftInputFromWindow(CurrentFocus.WindowToken, 0);
            }
        }

        /// <summary>
        /// Will open the diary activity.
        /// </summary>
        private void menuButtonClick(object sender, EventArgs e)
        {
            StartActivity(typeof(DiaryActivity));
        }

        /// <summary>
        /// Will open the settings activity.
        /// </summary>
        private void settingsButtonClick(object sender, EventArgs e)
        {
            StartActivity(typeof(SettingsActivity));
        }
    }

    [Application]
    public class UILApplication : Application
    {
        protected UILApplication(IntPtr javaReference,
            JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }
        public override void OnCreate()
        {
            base.OnCreate();

            DisplayImageOptions options = new DisplayImageOptions.Builder()
                .CacheOnDisk(true)
                .CacheInMemory(true)
                .ResetViewBeforeLoading()
                .ShowImageOnFail(Resource.Color.blue)
                .ShowImageForEmptyUri(Resource.Drawable.defaultsearchthumbnail)
                .BitmapConfig(Bitmap.Config.Rgb565)
                .Build();

            var config = new ImageLoaderConfiguration.Builder(ApplicationContext)
                .DefaultDisplayImageOptions(options)
                .DiskCacheExtraOptions(300, 300, null)
                .DiskCacheFileCount(10)
                .Build();

            // Initialize ImageLoader with configuration.
            ImageLoader.Instance.Init(config);
        }
    }
}

