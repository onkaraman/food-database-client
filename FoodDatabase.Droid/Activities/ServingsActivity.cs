using System;
using Android.App;
using Android.OS;
using Android.Text;
using Android.Views;
using Android.Widget;
using FoodDatabase.Core.API.Accessors;
using FoodDatabase.Core.Managers;
using FoodDatabase.Core.Helpers;
using FoodDatabase.Droid.Views.Adapters.Concretes;
using FoodDatabase.Core.Localization;
using FoodDatabase.Droid.Views.Controls;
using Android.Graphics;

namespace FoodDatabase.Droid.Activities
{
    /// <summary>
    /// This activity offers the user to add an amount of the
    /// selected food to his diary.
    /// </summary>
    [Activity(Label = "ServingsActivity",
        WindowSoftInputMode = SoftInput.AdjustResize|SoftInput.StateHidden,
        ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait,
        ConfigurationChanges = Android.Content.PM.ConfigChanges.ScreenSize|Android.Content.PM.ConfigChanges.Orientation)]
    public class ServingsActivity : Activity
    {
        private ProgressBar _progBar;
        private MainTextView _name;
        private MainTextView _description;
        private MainTextView _servingPresetDescription;
        private MainTextView _servingCustomDescription;
        private MainTextView _servingUnit;
        private MainEditText _customServing;
        private ListView _listView;
        private Button _addButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Servings);
            setupViews();
            localize();
            applyData();
            assignEvents();
        }

        /// <summary>
        /// Connects the UI controls to the fields of this activity.
        /// </summary>
        private void setupViews()
        {
            _progBar = FindViewById<ProgressBar>(Resource.Id.ServingProgressBar);
            _name = FindViewById<MainTextView>(Resource.Id.ServingFoodName);
            _description = FindViewById<MainTextView>(Resource.Id.ServingDescription);
            _servingPresetDescription = FindViewById<MainTextView>(Resource.Id.ServingPresetDescription);
            _servingCustomDescription = FindViewById<MainTextView>(Resource.Id.ServingCustomDescription);
            _listView = FindViewById<ListView>(Resource.Id.ServingListView);
            _customServing = FindViewById<MainEditText>(Resource.Id.ServingCustomEditText);
            _servingUnit = FindViewById<MainTextView>(Resource.Id.ServingCustomUnit);
            _addButton = FindViewById<Button>(Resource.Id.ServingAddButton);
            _addButton.Typeface = Typeface.CreateFromAsset(Assets, "fonts/segoeui.ttf");
            _progBar.Visibility = ViewStates.Invisible;
        }

        /// <summary>
        /// Will apply the data of the session item to the controls of this activity.
        /// </summary>
        private void applyData()
        {
            _name.Text = SessionManager.Static.Item.Description.name;
            _listView.Adapter = new ServingItemAdapter(SessionManager.Static.Item.Servings, this);
        }

        /// <summary>
        /// Will assign the events of the controls to methods.
        /// </summary>
        private void assignEvents()
        {
            _listView.ItemClick += servingsClick;
            _customServing.TextChanged += customServingTextChanged;
            _addButton.Click += addButtonClick;
        }

        /// <summary>
        /// Will localize the activity according to the user's device.
        /// </summary>
        private void localize()
        {
            _description.Text = Localization.Static.Raw("ServingDescription");
            _servingPresetDescription.Text = Localization.Static.Raw("PresetDescription");
            _servingCustomDescription.Text = Localization.Static.Raw("SelectCustomDescription");
            _addButton.Text = Localization.Static.Raw("AddButton");
        }

        /// <summary>
        /// Will hide the custom unit symbol when the custom serving length is too much.
        /// </summary>
        private void customServingTextChanged(object sender, TextChangedEventArgs e)
        {
            if (_customServing.Text.Length > 5) _servingUnit.Visibility = ViewStates.Invisible;
            else _servingUnit.Visibility = ViewStates.Visible;
        }

        /// <summary>
        /// Will add the selected serving to the diary.
        /// </summary>
        private async void servingsClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            if (e == null) return;
            checkLogin();

            var serving = SessionManager.Static.Item.Servings[e.Position];

            try
            {
                PersistenceManager.Static.AddAndPersist(string.Format("{0}-img", SessionManager.Static.Item.id),
                                                        SessionManager.Static.Item.thumbsrc);
                
                string response = await APIAccessor.Static.DiaryAddItem(SessionManager.Static.LoginData,
                                            SessionManager.Static.Item.id.ToString(), 0, serving.id.ToString());
                StartActivity(typeof(DiaryActivity));
            }
            catch (Exception)
            {
                showAlertDialog("Couldn't add serving to diary.");
            }

        }

        /// <summary>
        /// Will check whether the current user is logged into his account.
        /// If so, the user will be redirected to the login activity.
        /// </summary>
        private void checkLogin()
        {
            if (SessionManager.Static.LoginData == null)
            {
                SessionManager.Static.FromServing = true;
                StartActivity(typeof(LoginActivity));
            }
        }

        /// <summary>
        /// Will first check if the user is logged in. If so, the custom serving will be added.
        /// Otherwise the user will be redirected to the login activity.
        /// </summary>
        private async void addButtonClick(object sender, System.EventArgs e)
        {
            checkLogin();
            _progBar.Visibility = ViewStates.Visible;

            if (_customServing.Text.Length > 0)
            {
                PersistenceManager.Static.AddAndPersist(string.Format("{0}-img", SessionManager.Static.Item.id),
                                        SessionManager.Static.Item.thumbsrc);
                
                string response = await APIAccessor.Static.DiaryAddItem(SessionManager.Static.LoginData, 
                                                                        SessionManager.Static.Item.id.ToString(), 
                                                                        int.Parse(_customServing.Text));
                StartActivity(typeof(MainActivity));
            }
            else
            {
                showAlertDialog("You need to enter a custom amount first.");
            }
        }

        /// <summary>
        /// Will show an alert dialog with the passed message as its content.
        /// </summary>
        private void showAlertDialog(string msg)
        {
            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            alert.SetTitle(Resource.String.app_name);
            alert.SetMessage(msg);
            alert.SetPositiveButton("OK", (senderAlert, args) => { });
            alert.Show();
        }
    }
}
