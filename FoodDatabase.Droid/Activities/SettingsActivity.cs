﻿using System;
using Android.App;
using Android.OS;
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
        private EditText _kcalEditText;
        private Button _kcalSaveButton;
        private TextView _loggedInAsText;
        private Button _logoutButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Settings);
            setupViews();
            checkLogin();
        }

        /// <summary>
        /// Will connect the fields to the views of the layout.
        /// </summary>
        private void setupViews()
        {
            _progBar = FindViewById<ProgressBar>(Resource.Id.SettingsProgressBar);
            _kcalEditText = FindViewById<EditText>(Resource.Id.SettingsDailyKcal);
            _kcalSaveButton = FindViewById<Button>(Resource.Id.SettingsSaveButton);
            _loggedInAsText = FindViewById<TextView>(Resource.Id.SettingsLoggedInAs);
            _logoutButton = FindViewById<Button>(Resource.Id.SettingsLogoutButton);

            _progBar.Visibility = Android.Views.ViewStates.Invisible;
        }

        /// <summary>
        /// Will check whether the user is logged in. If not, the logout button will be
        /// changed to a login button.
        /// </summary>
        private void checkLogin()
        {
            try
            {
                if (PersistenceManager.Static.GetFirst("username") != null)
                {
                    string username = PersistenceManager.Static.GetFirst("username").Value;
                    _loggedInAsText.Text = string.Format("Loggedn in {0}", username);
                }
            }
            catch (Exception)
            {
                _loggedInAsText.Text = "You are not loggedn in.";
                _logoutButton.Text = "Login";
                _doLogin = true;
            }
        }
    }
}
