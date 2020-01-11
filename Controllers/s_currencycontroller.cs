// ASP.NET Maker 2019
// Copyright (c) 2019 e.World Technology Limited. All rights reserved.

using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using AspNetMaker2019.Models;
using static AspNetMaker2019.Models.SampleProject;

// Controllers
namespace AspNetMaker2019.Controllers
{

	// Partial class
	public partial class HomeController : Controller
	{

		// list
		[Route("s_currencylist/{Id?}")]
		[Route("Home/s_currencylist/{Id?}")]
		public async Task<IActionResult> s_currencylist()
		{

			// Create page object
			s_currency_List = new _s_currency_List(this);
			s_currency_List.Cache = _cache;

			// Run the page
			return await s_currency_List.Run();
		}

		// add
		[Route("s_currencyadd/{Id?}")]
		[Route("Home/s_currencyadd/{Id?}")]
		public async Task<IActionResult> s_currencyadd()
		{

			// Create page object
			s_currency_Add = new _s_currency_Add(this);

			// Run the page
			return await s_currency_Add.Run();
		}

		// view
		[Route("s_currencyview/{Id?}")]
		[Route("Home/s_currencyview/{Id?}")]
		public async Task<IActionResult> s_currencyview()
		{

			// Create page object
			s_currency_View = new _s_currency_View(this);

			// Run the page
			return await s_currency_View.Run();
		}

		// edit
		[Route("s_currencyedit/{Id?}")]
		[Route("Home/s_currencyedit/{Id?}")]
		public async Task<IActionResult> s_currencyedit()
		{

			// Create page object
			s_currency_Edit = new _s_currency_Edit(this);

			// Run the page
			return await s_currency_Edit.Run();
		}

		// delete
		[Route("s_currencydelete/{Id?}")]
		[Route("Home/s_currencydelete/{Id?}")]
		public async Task<IActionResult> s_currencydelete()
		{

			// Create page object
			s_currency_Delete = new _s_currency_Delete(this);

			// Run the page
			return await s_currency_Delete.Run();
		}
	}
}
