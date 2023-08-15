using System.ComponentModel.DataAnnotations;

namespace Library
{
    public class Site
    {
        [Required]
        private string siteName;
        
        private string userName;

        [EmailAddress]
        private string email;

        [Required]
        private string password;

        public Site(string siteName, string userName, string email, string password)
        {
            this.siteName = siteName;
            this.userName = userName;
            this.email = email;
            this.password = password;
        }

        public Site(string siteName, string password) : this(siteName, "", "", password) { }
        public Site(string siteName, string data, string password) : this(siteName, password) 
        {
            if (Util.isValidEmail(data))
                this.email = data;
            else
                this.userName = data;
        }

        public override string ToString() 
        {
            return string.Format("%s => %s %s %s", siteName.ToUpper(), userName, email, password);
        }
    }
}
