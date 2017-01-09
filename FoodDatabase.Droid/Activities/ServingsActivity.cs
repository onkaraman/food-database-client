using System;
using Android.App;
using Android.OS;
using Android.Text;
using Android.Views;
using Android.Widget;
using FoodDatabase.Core.API.Accessors;
using FoodDatabase.Core.Managers;
using FoodDatabase.Core.Sessions;
using FoodDatabase.Droid.Views.Adapters.Concretes;

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
        private TextView _name;
        private TextView _description;
        private TextView _servingPresetDescription;
        private ListView _listView;
        private EditText _customServing;
        private Button _button;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Servings);
            setupViews();
            applyData();
            assignEvents();
        }

        /// <summary>
        /// Connects the UI controls to the fields of this activity.
        /// </summary>
        private void setupViews()
        {
            _progBar = FindViewById<ProgressBar>(Resource.Id.ServingProgressBar);
            _name = FindViewById<TextView>(Resource.Id.ServingFoodName);
            _description = FindViewById<TextView>(Resource.Id.ServingDescription);
            _servingPresetDescription = FindViewById<TextView>(Resource.Id.ServingPresetDescription);
            _listView = FindViewById<ListView>(Resource.Id.ServingListView);
            _customServing = FindViewById<EditText>(Resource.Id.ServingCustomEditText);
            _button = FindViewById<Button>(Resource.Id.ServingAddButton);

            _progBar.Visibility = ViewStates.Invisible;
        }

        /// <summary>
        /// Will apply the data of the session item to the controls of this activity.
        /// </summary>
        private void applyData()
        {
            _name.Text = SessionHolder.Static.Item.Description.name;
            _listView.Adapter = new ServingItemAdapter(SessionHolder.Static.Item.Servings, this);
        }

        /// <summary>
        /// Will assign the events of the controls to methods.
        /// </summary>
        private void assignEvents()
        {
            _listView.ItemClick += servingsClick;
            _button.Click += addButtonClick;
        }

        /// <summary>
        /// Will add the selected serving to the diary.
        /// </summary>
        private async void servingsClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            if (e == null) return;
            checkLogin();

            var serving = SessionHolder.Static.Item.Servings[e.Position];

            try
            {
                PersistenceManager.Static.AddAndPersist(string.Format("{0}-img", SessionHolder.Static.Item.id),
                                                        SessionHolder.Static.Item.thumbsrc);
                
                string response = await APIAccessor.Static.DiaryAddItem(SessionHolder.Static.LoginData,
                                            SessionHolder.Static.Item.id.ToString(), 0, serving.id.ToString());
                StartActivity(typeof(MainActivity));
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
            if (SessionHolder.Static.LoginData == null)
            {
                SessionHolder.Static.FromServing = true;
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
                PersistenceManager.Static.AddAndPersist(string.Format("{0}-img", SessionHolder.Static.Item.id),
                                        SessionHolder.Static.Item.thumbsrc);
                
                string response = await APIAccessor.Static.DiaryAddItem(SessionHolder.Static.LoginData, "1", 100);
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
