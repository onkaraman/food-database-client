using System;
using Android.App;
using Android.OS;
using Android.Views.InputMethods;
using Android.Widget;
using FoodDatabase.Core.Managers;
using Android.Content;
using FoodDatabase.Core.Helpers;
using FoodDatabase.Core.Localization;

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
        private Button _saveButton;
        private TextView _dailyKcalDescription;
        private TextView _title;
        private TextView _loggedInAs;
        private TextView _versionNumber;
        private TextView _contact;
        private Button _logoutButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Settings);
            setupViews();
            localize();
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
            _title = FindViewById<TextView>(Resource.Id.SettingsTitle);
            _dailyKcalDescription = FindViewById<TextView>(Resource.Id.SettingsDailyKcalDescription);
            _kcalEdit = FindViewById<EditText>(Resource.Id.SettingsDailyKcal);
            _saveButton = FindViewById<Button>(Resource.Id.SettingsSaveButton);
            _loggedInAs = FindViewById<TextView>(Resource.Id.SettingsLoggedInAs);
            _logoutButton = FindViewById<Button>(Resource.Id.SettingsLogoutButton);
            _versionNumber = FindViewById<TextView>(Resource.Id.SettingsVersionInfo);
            _versionNumber.Text = getVersionNumber();
            _contact = FindViewById<TextView>(Resource.Id.SettingsContact);

            _progBar.Visibility = Android.Views.ViewStates.Invisible;
        }

        /// <summary>
        /// Will localize this activity according to the user's device settings.
        /// </summary>
        private void localize()
        {
            _title.Text = Localization.Static.Raw("SettingsTitle");
            _dailyKcalDescription.Text = Localization.Static.Raw("DailyKcal");
            _saveButton.Text = Localization.Static.Raw("SaveButton");
            _contact.Text = Localization.Static.Raw("ContactSupport");
        }

        /// <summary>
        /// Will assign the events for the controls of this activity.
        /// </summary>
        private void assignEvents()
        {
            _saveButton.Click += kcalSaveButtonClick;
            _logoutButton.Click += logoutButtonClick;
            _contact.Click += contactClick;
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
                _loggedInAs.Text = string.Format(Localization.Static.Raw("LoggedInAs"), username);
                _logoutButton.Text = Localization.Static.Raw("LogoutButton");
            }
            catch (Exception)
            {
                _loggedInAs.Text = Localization.Static.Raw("NotLoggedIn");
                _logoutButton.Text = Localization.Static.Raw("LoginButton");
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
                StartActivity(typeof(MainActivity));
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
                SessionManager.Static.LoginData = null;
                StartActivity(typeof(MainActivity));
            }
        }

        /// <summary>
        /// Will hide the soft keyboard of the device.
        /// </summary>
        private void hideKeyboard()
        {
            if (CurrentFocus != null)
            {
                InputMethodManager imm = (InputMethodManager)GetSystemService(InputMethodService);
                imm.HideSoftInputFromWindow(CurrentFocus.WindowToken, 0);
            }
        }

        /// <summary>
        /// Will return a string with the version number of this app.
        /// </summary>
        /// <returns>The version number.</returns>
        private string getVersionNumber()
        {
            var code = PackageManager.GetPackageInfo(PackageName, 0).VersionName;
            return string.Format("Food Database {0}", code);
        }

        /// <summary>
        /// Will open the local email client to write a support mail.
        /// </summary>
        private void contactClick(object sender, EventArgs e)
        {
            Intent email = new Intent(Intent.ActionSend);
            email.PutExtra(Intent.ExtraEmail, new string[] { "service@areondev.de" });
            email.PutExtra(Intent.ExtraSubject, new string[] { "Food Database App" });
            email.SetType("message/rfc822");
            StartActivity(email);
        }
    }
}
