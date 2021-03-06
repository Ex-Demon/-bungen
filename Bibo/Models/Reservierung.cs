﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Bibo
{
	public partial class Reservierung
	{
		[NotMapped]
		public Nutzer Nutzer
		{
			get
			{
				Nutzer nutzer = null;

				using (var context = new BiboDBEntities())
				{
					nutzer = context.Nutzer.FirstOrDefault(x => x.Id == this.NutzerId);
				}

				return nutzer;
			}
		}

		[NotMapped]
		public Buch Buch
		{
			get
			{
				Buch buch = null;

				using (var context = new BiboDBEntities())
				{
					buch = context.Buch.FirstOrDefault(x => x.Id == this.BuchId);
				}

				return buch;
			}
		}
	}
}