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
using FoodDatabase.Droid.Views.Controls;
using Android.Graphics;

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
        private MainTextView _description;
        private MainEditText _username;
        private MainEditText _password;
        private MainTextView _registerText;
        private Button _loginButton;

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
            _description = FindViewById<MainTextView>(Resource.Id.LoginDescription);
            _username = FindViewById<MainEditText>(Resource.Id.LoginUsername);
            _password = FindViewById<MainEditText>(Resource.Id.LoginPassword);
            _loginButton = FindViewById<Button>(Resource.Id.LoginButton);
            _loginButton.Typeface = Typeface.CreateFromAsset(Assets, "fonts/segoeui.ttf");
            _registerText = FindViewById<MainTextView>(Resource.Id.LoginRegisterText);

            _progBar.Visibility = ViewStates.Invisible;
        }

        /// <summary>
        /// Will localize the activity according to the user's device setting.
        /// </summary>
        private void localize()
        {
            _description.Text = Localization.Static.Raw("LoginTitle");
            _username.Hint = Localization.Static.Raw("UsernameHint");
            _password.Hint = Localization.Static.Raw("PasswordHint");
            _loginButton.Text = Localization.Static.Raw("LoginButton");
            _registerText.Text = Localization.Static.Raw("RegisterText");
        }

        /// <summary>
        /// Will assign the events of the controls to local methods.
        /// </summary>
        private void assigEvents()
        {
            _loginButton.Click += loginButtonClick;
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
