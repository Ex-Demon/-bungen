using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;


namespace Bibo.Controllers
{
	[Authorize(Users = "Admin")]
    public class AusleiheController : Controller
    {
		public ActionResult Index()
        {
			List<Ausleihe> list = GetAusleiheList();
			return View(list);
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

		[HttpGet]
		public ActionResult New(int buchid)
		{
			
			Ausleihe ausleihe = new Ausleihe();

			using (var contest = new BiboDBEntities())
			{
				ausleihe.Buch = contest.Buch.FirstOrDefault(x => x.Id == buchid);
			}

			ausleihe.Von = DateTime.Today;
			ausleihe.Bis = DateTime.Today.AddDays(30);

			return View(ausleihe);
		}

		[HttpPost]
		public ActionResult New(Ausleihe ausleihe)
		{
			if (ModelState.IsValid)
			{
				using (var context = new BiboDBEntities())
				{
					context.Ausleihe.Add(ausleihe);
					context.SaveChanges();
				}
				return RedirectToAction("Index");
			}
			return View(ausleihe);

		}

		private static List<Ausleihe> GetAusleiheList()
		{
			List<Ausleihe> list = null;

			using (var context = new BiboDBEntities())
			{
				list = context.Ausleihe.ToList();

				foreach (Ausleihe ausleihe in list)
				{
					ausleihe.Buch = context.Buch.FirstOrDefault(x => x.Id == ausleihe.BuchId);
					ausleihe.Nutzer = context.Nutzer.FirstOrDefault(x => x.Id == ausleihe.NutzerId);
				}
			}
			return list;
		}


    }
}
