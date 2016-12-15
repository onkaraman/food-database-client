using System;
using System.Threading;
using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using FoodDatabase.Core.API.Accessors;
using FoodDatabase.Core.API.Parsers;
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
        private EditText _searchField;
        private ListView _listView;
        private ProgressBar _progBar;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);
            setupViews();
            assignEvents();
        }

        /// <summary>
        /// Will connect the fields to the views of the layout.
        /// </summary>
        private void setupViews()
        {
            _progBar = FindViewById<ProgressBar>(Resource.Id.MainProgressBar);
            _searchField = FindViewById<EditText>(Resource.Id.MainEditText);
            _listView = FindViewById<ListView>(Resource.Id.MainListView);

            _progBar.Visibility = Android.Views.ViewStates.Invisible;
        }

        /// <summary>
        /// Will assign events to the views of the layout.
        /// </summary>
        private void assignEvents()
        {
            _searchField.KeyPress += searchFieldKeyPress;
        }

        /// <summary>
        /// Will handle the user input if he wants to initiate a search.
        /// </summary>
        private void searchFieldKeyPress(object sender, Android.Views.View.KeyEventArgs e)
        {
            if (e.KeyCode == Android.Views.Keycode.Enter)
            {
                search(_searchField.Text);
            }
        }

        /// <summary>
        /// Will start a search for the passed query. Before listing the result,
        /// the result will be re-searched until more result items are found.
        /// </summary>
        private void search(string query)
        {
            _progBar.Visibility = Android.Views.ViewStates.Visible;

            ThreadPool.QueueUserWorkItem(async o =>
            {
                string response = await APIAccessor.Static.Search(query);
                var result = APIParser.Static.Parse(response);

                for (int i = 1; i <= 10; i+=1)
                {
                    response = await APIAccessor.Static.Search(query, (i*10).ToString());
                    result.Items.AddRange(APIParser.Static.Parse(response).Items);
                }

                RunOnUiThread(() =>
                {
                    _listView.Adapter = new SearchItemAdapter(result.Items, this);
                    _progBar.Visibility = Android.Views.ViewStates.Invisible;
                });
            });
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

