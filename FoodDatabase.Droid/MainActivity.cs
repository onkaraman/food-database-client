using Android.App;
using Android.Widget;
using Android.OS;

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
            
        }

        /// <summary>
        /// Will assign events to the views of the layout.
        /// </summary>
        private void assignEvents()
        {
            
        }
    }
}

