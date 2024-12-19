using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace order_accounting
{
    internal class User
    {

        public int id { get; set; }

        public string Login { get; set; }
        public string Password { get; set; }

    public User() { }

        public User(string Login, string Password)
        {
            this.Login = Login;
            this.Password = Password;
        }
    }
}
