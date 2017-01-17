using Android.App;
using Android.OS;
using Android.Widget;
using FoodDatabase.Core.Managers;
using FoodDatabase.Core.Helpers;
using FoodDatabase.Droid.Views.Adapters.Concretes;
using UniversalImageLoader.Core;
using FoodDatabase.Core.Localization;
using FoodDatabase.Droid.Views.Controls;

namespace FoodDatabase.Droid.Activities
{
    /// <summary>
    /// This activity contains detailed data regarding the item.
    /// It will display all nutrional facts and offer an option to 
    /// add this item to the user's diary.
    /// </summary>
    [Activity(Label = "DetailActivity",
              WindowSoftInputMode = Android.Views.SoftInput.AdjustPan,
      ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class DetailActivity : Activity
    {
        private ImageView _thumbnail;
        private ImageView _thumbnailBlurred;
        private MainTextView _name;
        private MainTextView _producer;
        private MainTextView _group;
        private MainTextView _nutritionTitle;
        private ListView _listView;
        private Button _addButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Detail);
            setupViews();
            localize();
            detectAddButtonVisibility();
            applyData();
            assignEvents();
        }

        /// <summary>
        /// Will connect the controls of the layout to the fields of this activity.
        /// </summary>
        private void setupViews()
        {
            _thumbnail = FindViewById<ImageView>(Resource.Id.DetailThumbnail);
            _thumbnailBlurred = FindViewById<ImageView>(Resource.Id.DetailThumbnailBlur);
            _name = FindViewById<MainTextView>(Resource.Id.DetailName);
            _producer = FindViewById<MainTextView>(Resource.Id.DetailProducer);
            _group = FindViewById<MainTextView>(Resource.Id.DetailGroup);
            _nutritionTitle = FindViewById<MainTextView>(Resource.Id.DetailNutritionTitle);
            _listView = FindViewById<ListView>(Resource.Id.DetaiListView);
            _addButton = FindViewById<Button>(Resource.Id.DetailAddButton);
        }

        /// <summary>
        /// Will localize the UI according to the user's device settings.
        /// </summary>
        private void localize()
        {
            _addButton.Text = Localization.Static.Raw("AddButton");
        }

        /// <summary>
        /// Will hide the add button if this detail activity was called from the diary.
        /// </summary>
        private void detectAddButtonVisibility()
        {
            if (SessionManager.Static.FromDiary) _addButton.Enabled = false;
            else _addButton.Enabled = true;
        }

        /// <summary>
        /// Will load the data into the UI controls.
        /// </summary>
        private void applyData()
        {
            _name.Text = SessionManager.Static.Item.Description.name;
            _producer.Text = SessionManager.Static.Item.Description.producer;
            _group.Text = SessionManager.Static.Item.Description.group;
            _nutritionTitle.Text = string.Format(Localization.Static.Raw("NutrionalDataFor"),
                                                 SessionManager.Static.Item.Data.amount,
                                                 SessionManager.Static.Item.Data.GetMeasureUnit());
            _listView.Adapter = new NutritionItemAdapter(SessionManager.Static.Item.Data.SubdataAsList(),
                                             this);
            loadThumbails();
        }

        /// <summary>
        /// Will assign events for this UI elements of this activity.
        /// </summary>
        private void assignEvents()
        {
            _addButton.Click += addButtonClick;
        }

        private void addButtonClick(object sender, System.EventArgs e)
        {
            StartActivity(typeof(ServingsActivity));
        }

        /// <summary>
        /// Will load the thumbnails of this food item and display an
        /// alternate image if that is impossible.
        /// </summary>
        private void loadThumbails()
        {
            try
            {
                if (SessionManager.Static.Item.thumbsrclarge.Length > 3)
                {
                    ImageLoader.Instance.DisplayImage(SessionManager.Static.Item.thumbsrclarge, _thumbnail);
                }
                else if (SessionManager.Static.Item.thumbsrc.Length > 3)
                {
                    ImageLoader.Instance.DisplayImage(SessionManager.Static.Item.thumbsrc, _thumbnail);
                }
            }
            catch
            {
                _thumbnail.SetImageResource(Resource.Drawable.detaildefault);
            }
        }
    
    }
}
