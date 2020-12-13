using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ShottbowingAccess;

namespace ShottBowing
{
    public class BowingSecurity
    {

        public static bool login(string username, string password)
        {
            using (ShottDB_BWEntities ent = new ShottDB_BWEntities())
            {
               return ent.aspnet_Users.Any(e => e.UserName.Equals(username,StringComparison.OrdinalIgnoreCase) && e.LoweredUserName==password);
            }

        }

        public static int GetUser(string username)
        {
            using (ShottDB_BWEntities ent = new ShottDB_BWEntities())
            {
                return 5;//ent.aspnet_Users.Any(e => e.UserName.Equals(username));
            }

        }
    }
}