using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Web.Controllers
{
    public abstract class ControllerExtensions : Controller
    {
        public string RenderPartialViewToString(string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
            {
                viewName = ControllerContext.RouteData.GetRequiredString("action");
            }

            ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                // Find the partial view by its name and the current controller context.
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(this.ControllerContext, viewName);

                // Create a view context.
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);

                // Render the view using the StringWriter object.
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }

        protected override IAsyncResult BeginExecute(RequestContext requestContext, AsyncCallback callback, object state)
        {
            //string culture;
            switch (requestContext.HttpContext.Session["PreferredLanguage"] ?? "")
            {
                case "fr-ca":
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("fr-CA");
                    break;
                case "es-us":
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("es-US");
                    break;
                default:
                    break;
            }
            return base.BeginExecute(requestContext, callback, state);
        }
    }
}