using Android.App;
using Android.OS;
using Android.Widget;
using FoodDatabase.Core.Sessions;
using FoodDatabase.Droid.Views.Adapters.Concretes;
using UniversalImageLoader.Core;

namespace FoodDatabase.Droid.Activities
{
    /// <summary>
    /// This activity contains detailed data regarding the item.
    /// It will display all nutrional facts and offer an option to 
    /// add this item to the user's diary.
    /// </summary>
    [Activity(Label = "DetailActivity",
      WindowSoftInputMode = Android.Views.SoftInput.AdjustResize,
      ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class DetailActivity : Activity
    {
        private ImageView _thumbnail;
        private ImageView _thumbnailBlurred;
        private TextView _name;
        private TextView _producer;
        private TextView _group;
        private TextView _nutritionTitle;
        private ListView _listView;
        private Button _addButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Detail);
            setupViews();
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
            _name = FindViewById<TextView>(Resource.Id.DetailName);
            _producer = FindViewById<TextView>(Resource.Id.DetailProducer);
            _group = FindViewById<TextView>(Resource.Id.DetailGroup);
            _nutritionTitle = FindViewById<TextView>(Resource.Id.DetailNutritionTitle);
            _listView = FindViewById<ListView>(Resource.Id.DetaiListView);
            _addButton = FindViewById<Button>(Resource.Id.DetailAddButton);
        }

        /// <summary>
        /// Will load the data into the UI controls.
        /// </summary>
        private void applyData()
        {
            _name.Text = SessionHolder.Static.Item.Description.name;
            _producer.Text = SessionHolder.Static.Item.Description.producer;
            _group.Text = SessionHolder.Static.Item.Description.group;
            _nutritionTitle.Text = string.Format("Nutritional data for {0}{1}",
                                                 SessionHolder.Static.Item.Data.amount,
                                                 SessionHolder.Static.Item.Data.GetMeasureUnit());
            _listView.Adapter = new NutritionItemAdapter(SessionHolder.Static.Item.Data.SubdataAsList(),
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
            if (SessionHolder.Static.Item.thumbsrclarge.Length > 3)
            {
                ImageLoader.Instance.DisplayImage(SessionHolder.Static.Item.thumbsrclarge, _thumbnail);
            }
            else if (SessionHolder.Static.Item.thumbsrc.Length > 3)
            {
                ImageLoader.Instance.DisplayImage(SessionHolder.Static.Item.thumbsrc, _thumbnail);
            }
            else
            {
                _thumbnail.SetImageResource(Resource.Drawable.detaildefault);
            }
        }
    
    }
}
