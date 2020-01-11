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
		[Route("s_servicetypelist/{Id?}")]
		[Route("Home/s_servicetypelist/{Id?}")]
		public async Task<IActionResult> s_servicetypelist()
		{

			// Create page object
			s_servicetype_List = new _s_servicetype_List(this);
			s_servicetype_List.Cache = _cache;

			// Run the page
			return await s_servicetype_List.Run();
		}

		// add
		[Route("s_servicetypeadd/{Id?}")]
		[Route("Home/s_servicetypeadd/{Id?}")]
		public async Task<IActionResult> s_servicetypeadd()
		{

			// Create page object
			s_servicetype_Add = new _s_servicetype_Add(this);

			// Run the page
			return await s_servicetype_Add.Run();
		}

		// view
		[Route("s_servicetypeview/{Id?}")]
		[Route("Home/s_servicetypeview/{Id?}")]
		public async Task<IActionResult> s_servicetypeview()
		{

			// Create page object
			s_servicetype_View = new _s_servicetype_View(this);

			// Run the page
			return await s_servicetype_View.Run();
		}

		// edit
		[Route("s_servicetypeedit/{Id?}")]
		[Route("Home/s_servicetypeedit/{Id?}")]
		public async Task<IActionResult> s_servicetypeedit()
		{

			// Create page object
			s_servicetype_Edit = new _s_servicetype_Edit(this);

			// Run the page
			return await s_servicetype_Edit.Run();
		}

		// delete
		[Route("s_servicetypedelete/{Id?}")]
		[Route("Home/s_servicetypedelete/{Id?}")]
		public async Task<IActionResult> s_servicetypedelete()
		{

			// Create page object
			s_servicetype_Delete = new _s_servicetype_Delete(this);

			// Run the page
			return await s_servicetype_Delete.Run();
		}
	}
}
