using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bibo
{
	public partial class Nutzer
	{
		public List<Ausleihe> AusleiheList {
			get
			{
				List<Ausleihe> ausleiheListe = new List<Ausleihe>();

				using (var context = new BiboDBEntities())
				{
					var liste = context.Ausleihe.Where(x => (x.NutzerId == this.Id && x.Rueckgabe.Value == null));

					foreach (Ausleihe ausleihe in liste)
					{
						ausleihe.Buch = context.Buch.FirstOrDefault(x => x.Id == ausleihe.BuchId);
					}
					ausleiheListe = liste.ToList();
				}
				return ausleiheListe;
			}
		}
	}
}