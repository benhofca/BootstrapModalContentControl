using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            if(HttpContext.Current.Request.QueryString.Count > 0)
            {
                bool doRedirect = false;
                var qs = HttpUtility.ParseQueryString(Request.Url.Query);

                string lang = (HttpContext.Current.Request.QueryString["l"] ?? "").ToLower();

                if (!string.IsNullOrEmpty(lang))
                {
                    SetLanguage(lang);
                    qs.Remove("l");
                    doRedirect = true;
                }

                if (doRedirect)
                {
                    string url = Request.Url.AbsolutePath + ((qs.Count > 0) ? "?" + qs.ToString() : "");
                    Response.Redirect(url);
                }
            }
        }
        private void SetLanguage(string lang)
        {
            Session["PreferredLanguage"] = lang;
            
            HttpCookie plc = Context.Request.Cookies.Get("PreferredLanguage");
            if (plc == null)
            {
                plc = new HttpCookie("PreferredLanguage", lang);
            }
            else
            {
                plc.Value = lang;
            }
            plc.Expires = DateTime.Now.AddMonths(2);
#if DEBUG
            plc.Secure = false;
#else
            plc.Secure = true;
#endif
            Context.Response.Cookies.Set(plc);

            /*
            if (Request.IsAuthenticated)
            {
                try
                {
                    Data.DatabaseEntities db = new Data.DatabaseEntities();
                    System.Web.Security.MembershipUser CurrentUser = System.Web.Security.Membership.GetUser();
                    //Set user prefrences...
                }
                catch (Exception)
                {
                }
            }
            */
        }
    }
}
