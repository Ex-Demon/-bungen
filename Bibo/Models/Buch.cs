using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using System.Web;

namespace Bibo
{
	public partial class Buch
	{
		[NotMapped]
		public DateTime? AusgeliehenBis
		{
			get
			{
				DateTime? ausgeliehenBis = null;

				using (var context = new BiboDBEntities())
				{
					Ausleihe ausleihe = context.Ausleihe.FirstOrDefault(x => (x.BuchId == this.Id && x.Rueckgabe.Value == null));

					if (ausleihe != null) ausgeliehenBis = ausleihe.Bis;
				}
			return ausgeliehenBis;
			}

		}

		public Reservierung Reservierung
		{
			get
			{
				Reservierung reservierung = null;

				using (var context = new BiboDBEntities())
				{
					reservierung = context.Reservierung.FirstOrDefault(x => (x.BuchId == this.Id && x.GueltigBis < DateTime.Today));
				}

				return reservierung;
			}
		}

	}
}