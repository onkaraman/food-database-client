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
        }
    }
}
