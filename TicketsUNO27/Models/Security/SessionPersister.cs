using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TicketsUNO27.Models.Security
{
    public static class SessionPersister
    {
        static string UserNameSessionVar = "UserName";
        static string PhotoSessionVar = "Photo";
        static string UserIdSessionVar = "Id";
        static string UserPointOfCareSessionVar = "PointOfCare";
        static string CurrentCareSessionVar = "CurrentPointOfCare";

        public static bool HasRol(string Role)
        {
            if (Id != null)
            {
                AccountsModel am = new AccountsModel();
                CustomPrincipal mp = new CustomPrincipal(am.find(Id));
                return mp.IsInRole(Role);
            }
            else
            {
                return false;
            }
        }


        public static string UserName
        {
            get
            {
                HttpContext.Current.Session.Timeout = 24 * 60;
                if (HttpContext.Current == null)
                    return string.Empty;
                var sessionvar = HttpContext.Current.Session[UserNameSessionVar];
                if (sessionvar != null)
                    return sessionvar as string;
                return null;
            }
            set
            {
                HttpContext.Current.Session.Timeout = 24 * 60;
                HttpContext.Current.Session[UserNameSessionVar] = value;
            }
        }

        public static string Photo
        {
            get
            {
                HttpContext.Current.Session.Timeout = 24 * 60;
                if (HttpContext.Current == null)
                    return string.Empty;
                var sessionvar = HttpContext.Current.Session[PhotoSessionVar];
                if (sessionvar != null)
                    return sessionvar as string;
                return null;
            }
            set
            {
                HttpContext.Current.Session.Timeout = 24 * 60;
                HttpContext.Current.Session[PhotoSessionVar] = value;
            }
        }




        public static Guid? Id
        {
            get
            {
                if (HttpContext.Current == null)
                    return null;
                HttpContext.Current.Session.Timeout = 24 * 60;
                var sessionvar = HttpContext.Current.Session[UserIdSessionVar];
                if (sessionvar != null)
                    return sessionvar as Guid?;
                return null;
            }
            set
            {
                HttpContext.Current.Session.Timeout = 24 * 60;
                HttpContext.Current.Session["Id"] = value;
            }
        }


    }

}