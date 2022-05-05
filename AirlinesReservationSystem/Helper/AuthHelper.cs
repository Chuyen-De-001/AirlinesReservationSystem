using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AirlinesReservationSystem.Helper;
using AirlinesReservationSystem.Models;

namespace AirlinesReservationSystem.Helper
{
    public class AuthHelper
    {
        public static Model1 db = new Model1();
        public static void setIdentity(User user)
        {
            HttpContext.Current.Session["loginSesstion"] = user;
        }

        public static User getIdentity()
        {
            try
            {
                User session = (User)HttpContext.Current.Session["loginSesstion"];
                if (session != null)
                {
                    User user = db.Users.Find(session.id);
                    return user;
                }
            }
            catch
            { }
            return null;
        }

        public static void removeIdentity()
        {
            HttpContext.Current.Session["loginSesstion"] = null;
        }

        public static bool isLogin()
        {
            if (HttpContext.Current.Session["loginSesstion"] == null)
            {
                return false;
            }
            return true;
        }

        //Kiểm tra user đang đăng nhập có quyền admin hay không.
        //user.quyen == 1
        public static bool isAdmin(User user)
        {
            if (user.user_type == 1)
            {
                return true;
            }
            return false;
        }
    }
}
}