using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Photo_Gallery.Models
{
    public class User
    {
        public string Username { get; set; }
        public SecureString Password { get; set; }
        public SecureString ConfirmPassword { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}