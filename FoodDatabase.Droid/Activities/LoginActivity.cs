using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FoodDatabase.Core.API.Accessors;
using FoodDatabase.Core.Sessions;

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

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Login);
            setupViews();
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

            _progBar.Visibility = ViewStates.Invisible;
            _username.Text = "quadrigaking";
            _password.Text = "jonny0011";
        }

        /// <summary>
        /// Will assign the events of the controls to local methods.
        /// </summary>
        private void assigEvents()
        {
            _button.Click += buttonClick;
        }

        /// <summary>
        /// Will attempt to login the user. If it won't crash, login is successful.
        /// </summary>
        private async void buttonClick(object sender, EventArgs e)
        {
            _progBar.Visibility = ViewStates.Visible;

            try
            {
                string response = await APIAccessor.Static.Login(_username.Text, _password.Text);
                LoginData ld = new LoginData(_username.Text, _password.Text);
                SessionHolder.Static.LoginData = ld;

                if (SessionHolder.Static.FromServing) StartActivity(typeof(ServingsActivity));
                else if (SessionHolder.Static.FromDiary) StartActivity(typeof(DiaryActivity));

            }
            catch 
            {
                showAlertDialog("Login error. Please try again.");
            }

            _progBar.Visibility = ViewStates.Invisible;
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
            alert.SetPositiveButton("OK",(senderAlert, args) => { });
            alert.Show();
        }
    }
}
