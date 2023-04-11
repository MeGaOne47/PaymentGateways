using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PaymentGateways
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "PayPalCreatePayment",
                url: "PayPal/CreatePayment",
                defaults: new { controller = "PayPal", action = "CreatePayment" }
            );

            routes.MapRoute(
                name: "PayPalReturn",
                url: "PayPal/Return",
                defaults: new { controller = "PayPal", action = "Return" }
            );

            routes.MapRoute(
                name: "PayPalCancel",
                url: "PayPal/Cancel",
                defaults: new { controller = "PayPal", action = "Cancel" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
