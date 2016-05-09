using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;


namespace Bibo.Controllers
{
	public class BuchController : Controller
	{
		//
		// GET: /Home/


		//public ActionResult Index() //Darstellung der Gewünchsten Seite (mit Funktion zum Füllen einer Table) /Alt
		//{
		//	if (_list == null) _list = Generate_Buch();
		//	return View(_list);
		//}

		public ActionResult Index(string msg = "")
		{

			List<Buch> list = GetBuchList();

			ViewBag.Msg = msg;

			return View(list);
		}

		[HttpGet]
		public ActionResult New()
		{
			Buch buch = new Buch();
			return View(buch);
		}

		[HttpPost]
		public ActionResult New(Buch buch)
		{
			if (ModelState.IsValid)
			{
				using (var context = new BiboDBEntities())
				{
					context.Buch.Add(buch);
					context.SaveChanges();
				}
				return RedirectToAction("Index");
			}
			return View(buch);
		}

		public ActionResult Delete(int id)
		{
			string massage = "Löschen fehlgeschlagen!!!";
			using (var context = new BiboDBEntities())
			{
				Buch buch = context.Buch.FirstOrDefault(x => x.Id == id);
				if (buch != null)
				{
					context.Buch.Remove(buch);
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



		private static List<Buch> GetBuchList()
		{
			List<Buch> list = null;

			using (var context = new BiboDBEntities())
			{
				list = context.Buch.ToList();
			}
			return list;
		}


	}
}
