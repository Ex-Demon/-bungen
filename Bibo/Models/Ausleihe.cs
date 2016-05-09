using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bibo
{
	public partial class Ausleihe
	{
		[NotMapped]
		public Bibo.Nutzer Nutzer { get; set; }

		[NotMapped]
		public Bibo.Buch Buch { get; set; }

		//[NotMapped]	alt1
		//public List<Nutzer> NutzerList { get; set; }

		[NotMapped]
		public IEnumerable<SelectListItem> NutzerList { get; set; }
		

		//public Ausleihe()  alt 1
		//{
		//	using (var context = new BiboDBEntities())
		//	{
		//		NutzerList = context.Nutzer.ToList();
		//	}
		//}

		public Ausleihe()
		{
			using (var context = new BiboDBEntities())
			{
				NutzerList = GetNutzerSelectList();
			}
		}

		private List<SelectListItem> GetNutzerSelectList()
		{
			List<Nutzer> nutzerlist = null;

			using (var context = new BiboDBEntities())
			{
				nutzerlist = context.Nutzer.ToList();
			}

			var items = new List<SelectListItem>();

			foreach (var nutzer in nutzerlist)
			{
				var item = new SelectListItem();
				item.Value = nutzer.Id.ToString();
				item.Text = nutzer.Name + "," + nutzer.Vormane;
				items.Add(item);
			}
			return items;
		}
	}
}