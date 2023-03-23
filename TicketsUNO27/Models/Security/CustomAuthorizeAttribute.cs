using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TicketsUNO27.Models.Security
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //SessionPersister.UserName = "USUARIO DE PRUEBA CRISTIAN";
            //SessionPersister.Id = new Guid("74F53F27-5344-49B0-91BD-21A295F1EACC");
            HttpContext.Current.Session.Timeout = 24 * 60;
            if (SessionPersister.Id == null)
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { controller = "Accounts", action = "index" }));
            else
            {
                if (Roles != "")
                {


                    AccountsModel am = new AccountsModel();
                    CustomPrincipal mp = new CustomPrincipal(am.find(SessionPersister.Id));
                    if (!mp.IsInRole(Roles))
                        filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { Controller = "Accounts", action = "AccessDenied" }));
                }
            }
            //base.OnAuthorization(filterContext);
        }
    }
}