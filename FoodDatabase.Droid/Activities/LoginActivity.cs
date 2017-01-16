using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using FoodDatabase.Core.API.Accessors;
using FoodDatabase.Core.Managers;
using FoodDatabase.Core.Security;
using FoodDatabase.Core.Helpers;
using FoodDatabase.Core.Localization;
using HockeyApp;

namespace FoodDatabase.Droid.Activities
{
    /// <summary>
    /// This activity logs the user into his account.
    /// </summary>
    [Activity(Label = "LoginActivity",
        WindowSoftInputMode = SoftInput.AdjustResize | SoftInput.StateHidden,
        ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait,
        ConfigurationChanges = Android.Content.PM.ConfigChanges.ScreenSize | Android.Content.PM.ConfigChanges.Orientation)]
    public class LoginActivity : Activity
    {
        private ProgressBar _progBar;
        private TextView _description;
        private EditText _username;
        private EditText _password;
        private Button _button;
        private TextView _registerText;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Login);
            setupViews();
            localize();
            assigEvents();
        }

        /// <summary>
        /// Will connect the UI fields with the controls of the layout.
        /// </summary>
        private void setupViews()
        {
            _progBar = FindViewById<ProgressBar>(Resource.Id.LoginProgressBar);
            _description = FindViewById<TextView>(Resource.Id.LoginDescription);
            _username = FindViewById<EditText>(Resource.Id.LoginUsername);
            _password = FindViewById<EditText>(Resource.Id.LoginPassword);
            _button = FindViewById<Button>(Resource.Id.LoginButton);
            _registerText = FindViewById<TextView>(Resource.Id.LoginRegisterText);

            _progBar.Visibility = ViewStates.Invisible;
            _username.Text = "quadrigaking";
            _password.Text = "jonny0011";
        }

        /// <summary>
        /// Will localize the activity according to the user's device setting.
        /// </summary>
        private void localize()
        {
            _description.Text = Localization.Static.Raw("LoginTitle");
            _username.Hint = Localization.Static.Raw("UsernameHint");
            _password.Hint = Localization.Static.Raw("PasswordHint");
            _button.Text = Localization.Static.Raw("LoginButton");
            _registerText.Text = Localization.Static.Raw("RegisterText");
        }

        /// <summary>
        /// Will assign the events of the controls to local methods.
        /// </summary>
        private void assigEvents()
        {
            _button.Click += loginButtonClick;
            _registerText.Click += registerTextClick;
        }

        /// <summary>
        /// Will attempt to login the user. If it won't crash, login is successful.
        /// </summary>
        private async void loginButtonClick(object sender, EventArgs e)
        {
            _progBar.Visibility = ViewStates.Visible;

            try
            {
                string response = await APIAccessor.Static.Login(_username.Text, _password.Text);
                LoginData ld = new LoginData(_username.Text, _password.Text);
                SessionManager.Static.LoginData = ld;

                PersistenceManager.Static.Add("username", _username.Text);
                PersistenceManager.Static.Add("password", Encrypter.Static.Encrypt(_password.Text));
                PersistenceManager.Static.PersistAllToDB();


                if (SessionManager.Static.FromServing) StartActivity(typeof(ServingsActivity));
                else if (SessionManager.Static.FromDiary) StartActivity(typeof(DiaryActivity));
                else StartActivity(typeof(MainActivity));

            }
            catch (Exception ex)
            {
                MetricsManager.TrackEvent(string.Format("{0}\n{1}", ex.Message, ex.StackTrace));
                showAlertDialog(Localization.Static.Raw("LoginError"));
            }

            _progBar.Visibility = ViewStates.Invisible;
        }

        /// <summary>
        /// Will open an external website to register for an account.
        /// </summary>
        private void registerTextClick(object sender, EventArgs e)
        {
            Intent i = new Intent(Intent.ActionView);
            i.SetData(Android.Net.Uri.Parse("http://fddb.mobi/register/?lang=de"));
            StartActivity(i);
        }

        /// <summary>
        /// Will show an alert dialog with the passed message as its content.
        /// </summary>
        /// <param name="msg">Message.</param>
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
