using LaTirelire.Models;
using LaTirelire.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace LaTirelire
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
        }

        protected void Session_Start()
        {
            Session["panier"] = new Commande();
            Session["contexte"] = new TireBase(); // contexte qui existe tant que la session existe : 
            //plus simple pour les écritures dans la base (évite les problèmes de tracking dus aux multiples instances de repository/contexte) 
        }

    }
}
