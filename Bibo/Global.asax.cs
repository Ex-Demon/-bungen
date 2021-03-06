﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace Bibo
{
	// Hinweis: Anweisungen zum Aktivieren des klassischen Modus von IIS6 oder IIS7 
	// finden Sie unter "http://go.microsoft.com/?LinkId=9394801".

	public class MvcApplication : System.Web.HttpApplication
	{

		protected void Session_Start(object sender, EventArgs e)
		{
			System.Diagnostics.Debug.WriteLine("Session_Start");

			if (Context.Session != null)
			{
				if (Context.Session.IsNewSession)
				{
					string sCookieHeader = Request.Headers["Cookie"];
					if ((null != sCookieHeader) && (sCookieHeader.IndexOf("ASP.NET_SessionId") >= 0))
					{
						FormsAuthentication.SignOut();
						Response.Redirect("/Home/Index?sessionExpired=true");
					}
				}
			}

		}

		protected void Session_End(object sender, EventArgs e)
		{
			System.Diagnostics.Debug.WriteLine("Session_End");
			Session.Abandon();
		}

		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();

			WebApiConfig.Register(GlobalConfiguration.Configuration);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
		}
	}
}