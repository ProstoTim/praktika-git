using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dry_cleaning.sql_query
{
    public class EmployeeQuery
    {
        public static string UserLoginCheck { get { return "SELECT Login FROM Users WHERE Role = 'user' AND Client_id = @clientOnOrder;"; } }
        public static string GetLastAddedClient { get { return "SELECT TOP 1 client_id FROM OrderTable ORDER BY order_id DESC;"; } }

        public static string CreateNewUserClient { get { return @"
                    INSERT INTO Users(Login, Role, Client_id, Employee_id, Password)
                    VALUES (@NewUser,'user', @NewClient_id, NULL, @NewPassword);"; } }

        public static string DecryptPassword { get { return @"SELECT Password FROM Users WHERE Login = @UserLogin;"; } }
    }
}
