namespace CSE.Client.Models
{
    public class User
    {
        public User(
               string userName,
               string password,
               string alias)
        {
            UserName = userName;
            Password = password;
            Alias = alias;
        }

        public string UserName { get; }
        public string Password { get; }
        public string Alias { get; }
    }
}
