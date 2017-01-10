using System;
using Android.App;
using Android.OS;
using Android.Views.InputMethods;
using Android.Widget;
using FoodDatabase.Core.Managers;
using FoodDatabase.Core.Security;
using FoodDatabase.Core.Sessions;

namespace FoodDatabase.Droid.Activities
{
    /// <summary>
    /// This activity shows the user various options to tweak for the app.
    /// </summary>
    [Activity(Label = "SettingsActivity",
              WindowSoftInputMode = Android.Views.SoftInput.AdjustPan,
      ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class SettingsActivity : Activity
    {
        private bool _doLogin;
        private ProgressBar _progBar;
        private EditText _kcalEdit;
        private Button _kcalSaveButton;
        private TextView _loggedInAs;
        private Button _logoutButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Settings);
            setupViews();
            assignEvents();
            reloadKcal();
            reloadlogin();
        }

        /// <summary>
        /// Will connect the fields to the views of the layout.
        /// </summary>
        private void setupViews()
        {
            _progBar = FindViewById<ProgressBar>(Resource.Id.SettingsProgressBar);
            _kcalEdit = FindViewById<EditText>(Resource.Id.SettingsDailyKcal);
            _kcalSaveButton = FindViewById<Button>(Resource.Id.SettingsSaveButton);
            _loggedInAs = FindViewById<TextView>(Resource.Id.SettingsLoggedInAs);
            _logoutButton = FindViewById<Button>(Resource.Id.SettingsLogoutButton);

            _progBar.Visibility = Android.Views.ViewStates.Invisible;
        }

        /// <summary>
        /// Will assign the events for the controls of this activity.
        /// </summary>
        private void assignEvents()
        {
            _kcalSaveButton.Click += kcalSaveButtonClick;
            _logoutButton.Click += logoutButtonClick;
        }

        /// <summary>
        /// Will reload the persisted setting of the user.
        /// </summary>
        private void reloadKcal()
        {
            try
            {
                _kcalEdit.Text = PersistenceManager.Static.GetFirst("kcal").Value;
            }
            catch (Exception)
            {
                _kcalEdit.Text = "2000";
            }
        }

        /// <summary>
        /// Will reload the data for the logged in user.
        /// </summary>
        private void reloadlogin()
        {
            try
            {
                string username = PersistenceManager.Static.GetFirst("username").Value;
                _loggedInAs.Text = string.Format("Loggedn in {0}", username);
            }
            catch (Exception)
            {
                _loggedInAs.Text = "You are not logged in.";
                _logoutButton.Text = "Login";
                _doLogin = true;
            }
        }

        /// <summary>
        /// Will persist the typed kcals to the db.
        /// </summary>
        private void kcalSaveButtonClick(object sender, EventArgs e)
        {
            if (_kcalEdit.Text.Length > 0)
            {
                PersistenceManager.Static.AddAndPersist("kcal", _kcalEdit.Text);
                hideKeyboard();
            }
        }

        /// <summary>
        /// Will check whether the user is logged in. If not, the logout button will be
        /// changed to a login button.
        /// </summary>
        private void logoutButtonClick(object sender, EventArgs e)
        {
            if (_doLogin)
            {
                StartActivity(typeof(LoginActivity));
            }
            else
            {
                PersistenceManager.Static.Delete(PersistenceManager.Static.GetFirst("username"));
                PersistenceManager.Static.Delete(PersistenceManager.Static.GetFirst("password"));
                StartActivity(typeof(MainActivity));
            }
        }

        private void hideKeyboard()
        {
            if (CurrentFocus != null)
            {
                InputMethodManager imm = (InputMethodManager)GetSystemService(InputMethodService);
                imm.HideSoftInputFromWindow(CurrentFocus.WindowToken, 0);
            }
        }

    }
}
