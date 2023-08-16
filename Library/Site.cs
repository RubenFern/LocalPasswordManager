using System.Text;

namespace Library
{
    public class Site
    {
        public int Id { get; set; }
        public string SiteName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public byte[] Password { get; set; }

        public Site(int id, string siteName, string userName, string email, byte[] password)
        {
            this.Id = id;
            this.SiteName = siteName;
            this.UserName = userName;
            this.Email = email;
            this.Password = password;
        }

        public Site() : this(-1, "", "", "", new byte[] { }) { }
        public Site(string siteName, byte[] password) : this(-1, siteName, "", "", password) { }
        public Site(string siteName, string data, byte[] password) : this(siteName, password) 
        {
            if (Util.IsValidEmail(data))
                this.Email = data;
            else
                this.UserName = data;
        }

        public override string ToString() 
        {
            return $"{SiteName.ToUpper()}\n\tUsername: {UserName}\n\tEmail: {Email}\n\tPassword: {Encoding.UTF8.GetString(Password)}";
        }
    }
}
