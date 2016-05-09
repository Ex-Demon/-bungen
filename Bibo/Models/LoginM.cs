using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Bibo.Models
{
	public class LoginM
	{

	[Required]
	public string Login { get; set; }

	[Required]
	public string Passwort { get; set; }

	}
}