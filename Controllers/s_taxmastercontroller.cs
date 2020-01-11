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
		[Route("s_taxmasterlist/{Id?}")]
		[Route("Home/s_taxmasterlist/{Id?}")]
		public async Task<IActionResult> s_taxmasterlist()
		{

			// Create page object
			s_taxmaster_List = new _s_taxmaster_List(this);
			s_taxmaster_List.Cache = _cache;

			// Run the page
			return await s_taxmaster_List.Run();
		}

		// add
		[Route("s_taxmasteradd/{Id?}")]
		[Route("Home/s_taxmasteradd/{Id?}")]
		public async Task<IActionResult> s_taxmasteradd()
		{

			// Create page object
			s_taxmaster_Add = new _s_taxmaster_Add(this);

			// Run the page
			return await s_taxmaster_Add.Run();
		}

		// view
		[Route("s_taxmasterview/{Id?}")]
		[Route("Home/s_taxmasterview/{Id?}")]
		public async Task<IActionResult> s_taxmasterview()
		{

			// Create page object
			s_taxmaster_View = new _s_taxmaster_View(this);

			// Run the page
			return await s_taxmaster_View.Run();
		}

		// edit
		[Route("s_taxmasteredit/{Id?}")]
		[Route("Home/s_taxmasteredit/{Id?}")]
		public async Task<IActionResult> s_taxmasteredit()
		{

			// Create page object
			s_taxmaster_Edit = new _s_taxmaster_Edit(this);

			// Run the page
			return await s_taxmaster_Edit.Run();
		}

		// delete
		[Route("s_taxmasterdelete/{Id?}")]
		[Route("Home/s_taxmasterdelete/{Id?}")]
		public async Task<IActionResult> s_taxmasterdelete()
		{

			// Create page object
			s_taxmaster_Delete = new _s_taxmaster_Delete(this);

			// Run the page
			return await s_taxmaster_Delete.Run();
		}
	}
}
