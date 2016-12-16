using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Renderscripts;
using Android.Widget;
using FoodDatabase.Core.Sessions;
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

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Detail);
            setupViews();
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

            _name.Text = SessionHolder.Static.Item.Description.name;
            _producer.Text = SessionHolder.Static.Item.Description.producer;
            _group.Text = SessionHolder.Static.Item.Description.group;
            _nutritionTitle.Text = string.Format("Nutritional data for {0} {1}",
                                                 SessionHolder.Static.Item.Data.amount,
                                                 SessionHolder.Static.Item.Data.GetMeasureUnit());

            loadThumbails();
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
            else
            {
                _thumbnail.SetImageResource(Resource.Drawable.detaildefault);
            }
            //_thumbnailBlurred.SetImageBitmap(createBlurredImage(25, SessionHolder.Static.Item.thumbsrclarge));
        }

        private Bitmap createBlurredImage(int radius, string imageUrl)
        {
            Bitmap originalBitmap = ImageLoader.Instance.LoadImageSync(imageUrl); 
            Bitmap blurredBitmap = Bitmap.CreateBitmap(originalBitmap);

            RenderScript rs = RenderScript.Create(this);
            Allocation input = Allocation.CreateFromBitmap(rs, originalBitmap, Allocation.MipmapControl.MipmapFull, AllocationUsage.Script);
            Allocation output = Allocation.CreateTyped(rs, input.Type);

            // Load up an instance of the specific script that we want to use.
            ScriptIntrinsicBlur script = ScriptIntrinsicBlur.Create(rs, Element.U8_4(rs));
            script.SetInput(input);
            script.SetRadius(radius);
            script.ForEach(output);
            output.CopyTo(blurredBitmap);

            return blurredBitmap;
        }
    }
}
