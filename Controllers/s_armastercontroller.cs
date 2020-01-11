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
		[Route("s_armasterlist/{Id?}")]
		[Route("Home/s_armasterlist/{Id?}")]
		public async Task<IActionResult> s_armasterlist()
		{

			// Create page object
			s_armaster_List = new _s_armaster_List(this);
			s_armaster_List.Cache = _cache;

			// Run the page
			return await s_armaster_List.Run();
		}

		// add
		[Route("s_armasteradd/{Id?}")]
		[Route("Home/s_armasteradd/{Id?}")]
		public async Task<IActionResult> s_armasteradd()
		{

			// Create page object
			s_armaster_Add = new _s_armaster_Add(this);

			// Run the page
			return await s_armaster_Add.Run();
		}

		// view
		[Route("s_armasterview/{Id?}")]
		[Route("Home/s_armasterview/{Id?}")]
		public async Task<IActionResult> s_armasterview()
		{

			// Create page object
			s_armaster_View = new _s_armaster_View(this);

			// Run the page
			return await s_armaster_View.Run();
		}

		// edit
		[Route("s_armasteredit/{Id?}")]
		[Route("Home/s_armasteredit/{Id?}")]
		public async Task<IActionResult> s_armasteredit()
		{

			// Create page object
			s_armaster_Edit = new _s_armaster_Edit(this);

			// Run the page
			return await s_armaster_Edit.Run();
		}

		// delete
		[Route("s_armasterdelete/{Id?}")]
		[Route("Home/s_armasterdelete/{Id?}")]
		public async Task<IActionResult> s_armasterdelete()
		{

			// Create page object
			s_armaster_Delete = new _s_armaster_Delete(this);

			// Run the page
			return await s_armaster_Delete.Run();
		}
	}
}
