using Android.App;
using Android.Widget;
using Android.OS;
using System;
using Android.Runtime;
using UniversalImageLoader.Core;
using Android.Graphics;
using System.Threading;
using FoodDatabase.Core.API.Accessors;
using FoodDatabase.Core.API.Parsers;
using FoodDatabase.Droid.Views.Adapters.Concretes;

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

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Main);
            setupViews();
            assignEvents();
            search("Pizza");
        }

        /// <summary>
        /// Will connect the fields to the views of the layout.
        /// </summary>
        private void setupViews()
        {
            _searchField = FindViewById<EditText>(Resource.Id.MainEditText);
            _listView = FindViewById<ListView>(Resource.Id.MainListView);
        }

        /// <summary>
        /// Will assign events to the views of the layout.
        /// </summary>
        private void assignEvents()
        {
            
        }

        private void search(string query)
        {
            ThreadPool.QueueUserWorkItem(async o =>
            {
                string response = await APIAccessor.Static.Search(query);
                var result = APIParser.Static.Parse(response);

                RunOnUiThread(() =>
                {
                    _listView.Adapter = new SearchItemAdapter(result.Items, this);
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
                //.CacheInMemory(true)
                .ShowImageOnFail(Resource.Color.blue)
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

