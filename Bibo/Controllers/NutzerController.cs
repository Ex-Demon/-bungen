using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;


namespace Bibo.Controllers
{
	[Authorize]
	public class NutzerController : Controller
	{


		[Authorize(Users = "Admin")]
		
		public ActionResult Index(string msg = "")
		{

			List<Nutzer> list = GetNutzerList();

			//if (list == null || !list.Any())
			//{
			//	Generate_Nutzer();
			//	//GetNutzerList();
			//	list = GetNutzerList();

			//}
			if (msg.Length > 0) ViewBag.Msg = msg;

			return View(list);
		}

		[Authorize(Users = "Admin")]
		[HttpGet]
		public ActionResult New()
		{
			Nutzer nutzer = new Nutzer();
			nutzer.Geburtsdatum = new DateTime(1900,1,1);
			return View(nutzer);
		}

		[Authorize(Users = "Admin")]
		[HttpPost]
		public ActionResult New(Nutzer nutzer)
		{
			if (ModelState.IsValid)
			{
				nutzer.IsAktiv = false;

				using (var context = new BiboDBEntities())
				{
					context.Nutzer.Add(nutzer);
					context.SaveChanges();
				}
				return RedirectToAction("Index");
			}
			return View(nutzer);
		}

//		[HttpPost]
//		public JsonResult anotheruser(string Login)


		[Authorize(Users = "Admin")]
		public ActionResult Delete(int id)
		{
			string massage = "Löschen fehlgeschlagen!!!";
			using (var context = new BiboDBEntities())
			{
				Nutzer nutzer = context.Nutzer.FirstOrDefault(x => x.Id == id);
				if (nutzer != null) // && (nutzer.Ausleihelist == null || !nutzer.Ausleihelist.Any()))
				{
					context.Nutzer.Remove(nutzer);
					context.SaveChanges();
					massage = "Löschen erfolgreich!!!";
				}
				else
				{
					massage = "Löschen fehlgeschlagen";
				}
			}
			return RedirectToAction("Index", new { msg = massage });
		}

		[Authorize(Users = "Admin")]
		private static List<Nutzer> GetNutzerList()
		{
			List<Nutzer> list = null;

			using (var context = new BiboDBEntities())
			{
				list = context.Nutzer.ToList();
			}
			return list;
		}

		public ActionResult AusleiheOverview(int nutzerid)
		{
			// Nutzer ID wird überprüft, damit keine Fremdzugriff entsteht
			if (!@Session["user_Login"].ToString().Equals("Admin") && !@Session["user_ID"].ToString().Equals(nutzerid.ToString()))
			{
				return RedirectToAction("Notfound", "Home");
			}


			Nutzer nutzer = null;
			using (var context = new BiboDBEntities())
			{
				nutzer = context.Nutzer.FirstOrDefault((x => x.Id == nutzerid));

				if (nutzer != null) return View(nutzer);
			}
			return RedirectToAction("Index");
		}

		public ActionResult Return(int id)
		{
			using (var context = new BiboDBEntities())
			{
				Ausleihe ausleihe = context.Ausleihe.FirstOrDefault(x => x.Id == id);

				if (ausleihe != null)
				{
					ausleihe.Rueckgabe = DateTime.Today;
					context.SaveChanges();
				}
			}
			return RedirectToAction("Index");
		}

		//alt !!!

		//private static List<Nutzer> _list = null;

		//public ActionResult Index() //Darstellung der Gewünchsten Seite (mit Funktion zum Füllen einer Table)
		//{
		//	if (_list == null) _list = Generate_Nutzer();
		//	return View(_list);
		//}

		//[HttpGet]
		//public ActionResult New()
		//{
		//	Nutzer nutzer = new Nutzer();
		//	return View(nutzer);
		//}

		//[HttpPost]
		//public ActionResult New(Nutzer nutzer)
		//{
		//	if (ModelState.IsValid)
		//	{
		//		int lastID = _list.Last().Id;
		//		nutzer.Id = lastID + 1;
		//		_list.Add(nutzer);
		//		return RedirectToAction("Index");
		//	}
		//	return View(nutzer);
		//}

		//public ActionResult Delete(int id)
		//{
		//	Nutzer nutzer = _list.Where(x => x.Id == id).FirstOrDefault();
		//	if (nutzer != null) _list.Remove(nutzer);
		//	return RedirectToAction("Index");
		//}

		//private static List<Nutzer> Generate_Nutzer()
		//{
		//	List<Nutzer> nutzerliste = new List<Nutzer>();

		//	nutzerliste.Add(new Nutzer {Vorname = "Hans", Name = "Goethe", Geburtsdatum = new DateTime(1979, 10, 01) });
		//	nutzerliste.Add(new Nutzer {Vorname = "Franz", Name = "Faust", Geburtsdatum = new DateTime(1920, 02, 29) });
		//	nutzerliste.Add(new Nutzer {Vorname = "Kranz", Name = "Finger", Geburtsdatum = new DateTime(1819, 12, 24) });
		//	return nutzerliste;
		//}

	}
}
