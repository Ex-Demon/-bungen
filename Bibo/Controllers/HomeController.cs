using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Bibo.Models;

namespace Bibo.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

		
        public ActionResult Index()
        {
			if (@Session["user_ID"] == null)
			{
				FormsAuthentication.SignOut();
			}
			
	        if (!Request.IsAuthenticated)
	        {
		        return RedirectToAction("Login");
	        }
            return View();
        }

		[Authorize(Users = "Admin")]
		public ActionResult Reset()
		{
			using (var context = new BiboDBEntities())
			{
				var allNutzer = context.Nutzer;
				context.Nutzer.RemoveRange(allNutzer);

				var allBuch = context.Buch;
				context.Buch.RemoveRange(allBuch);

				var allAusleihe = context.Ausleihe;
				context.Ausleihe.RemoveRange(allAusleihe);

				context.SaveChanges();
			}

			Generate_Nutzer();
			Generate_Buch();

			return RedirectToAction("Index");
		}

		private static void Generate_Buch()
		{
			Buch Buch1 = new Buch { Titel = "Faust", Autor = "Goethe", ISBN = "541-688" };
			Buch Buch2 = new Buch { Titel = "Hand", Autor = "Finger", ISBN = "875-648" };
			Buch Buch3 = new Buch { Titel = "Windows10", Autor = "Gate", ISBN = "876-588" };

			//Zugriff auf Datenbank
			using (var context = new BiboDBEntities())
			{
				//Daten hinzufügen
				context.Buch.Add(Buch1);
				context.Buch.Add(Buch2);
				context.Buch.Add(Buch3);

				context.SaveChanges();
			}

		}

		private static void Generate_Nutzer()
		{
			Nutzer nutzer1 = new Nutzer { Vormane = "Hans", Name = "Goethe", Geburtsdatum = new DateTime(1979, 10, 01), Login = "Hans", Passwort = "geheim2", IsAktiv = false };
			Nutzer nutzer2 = new Nutzer { Vormane = "Franz", Name = "Faust", Geburtsdatum = new DateTime(1920, 02, 29), Login = "Franz", Passwort = "geheim", IsAktiv = false };
			Nutzer nutzer3 = new Nutzer { Vormane = "Kranz", Name = "Finger", Geburtsdatum = new DateTime(1819, 12, 24), Login = "Kranz", Passwort = "geheim", IsAktiv = false };
			Nutzer nutzer4 = new Nutzer { Vormane = "Master", Name = "Master", Geburtsdatum = new DateTime(1819, 12, 24), Login = "Admin", Passwort = "admin", IsAktiv = false };

			//Zugriff auf Datenbank
			using (var context = new BiboDBEntities())
			{
				//Daten hinzufügen
				context.Nutzer.Add(nutzer1);
				context.Nutzer.Add(nutzer2);
				context.Nutzer.Add(nutzer3);
				context.Nutzer.Add(nutzer4);


				context.SaveChanges();
			}

		}

		[HttpGet]
	    public ActionResult Login()
	    {
			LoginM loginM = new LoginM();

			return View(loginM);
	    }

		[HttpPost]
		public ActionResult Login(LoginM model)
		{

			string login = model.Login;
			string password = model.Passwort;

			Nutzer nutzer = null;

			using (var context = new BiboDBEntities())
			{
				nutzer = context.Nutzer.FirstOrDefault(x => (x.Login == login && x.Passwort == password));
			}

			if (nutzer == null)
			{
				model = new LoginM();
				ViewBag.Msg = "Ey du kommst hier net rein !";

				return View(model);
			}

			//Login korrekt
			FormsAuthentication.SetAuthCookie(nutzer.Login, false);

			Session["user_Login"] = nutzer.Login;
			Session["user_ID"] = nutzer.Id;

			return RedirectToAction("Index");
		}

	    public ActionResult Notfound()
	    {
		    Response.StatusCode = 404;
		    return View("Notfound");
	    }

	    public ActionResult Logout()
	    {
			FormsAuthentication.SignOut();

		    return RedirectToAction("Login");
	    }

			
		//T000: anahand der Daten aus dem model Objekt dern zugehörigen Nutzer aus der DB holen
		//	return RedirectToAction("Index", "Nutzer");
    }
}
