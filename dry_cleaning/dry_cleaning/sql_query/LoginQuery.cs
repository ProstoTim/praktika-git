using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dry_cleaning.sql_query
{
    public class LoginQuery
    {
        public static string LoginCheck { get { return "SELECT Login FROM Users WHERE Login = @your_login"; } }

        public static string PasswordCheck { get { return "SELECT Password FROM Users WHERE Login = @your_login;"; } }

        public static string RoleCheck { get { return "SELECT Role FROM Users WHERE Login = @your_login"; } }

        public static string GetClientId { get {return "SELECT Client_id FROM Users WHERE Login = @your_login"; } }
    }
}
