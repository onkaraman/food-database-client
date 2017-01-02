
namespace FoodDatabase.Core.Sessions
{
    /// <summary>
    /// Hold username and password of the logged in user.
    /// </summary>
    public class LoginData
    {
        public string Username { get; }
        public string Password { get; }

        public LoginData (string user, string pass)
        {
            Username = user;
            Password = pass;
        }
    }
}
