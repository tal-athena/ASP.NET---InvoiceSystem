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
		[Route("s_serviceslist/{Id?}")]
		[Route("Home/s_serviceslist/{Id?}")]
		public async Task<IActionResult> s_serviceslist()
		{

			// Create page object
			s_services_List = new _s_services_List(this);
			s_services_List.Cache = _cache;

			// Run the page
			return await s_services_List.Run();
		}

		// add
		[Route("s_servicesadd/{Id?}")]
		[Route("Home/s_servicesadd/{Id?}")]
		public async Task<IActionResult> s_servicesadd()
		{

			// Create page object
			s_services_Add = new _s_services_Add(this);

			// Run the page
			return await s_services_Add.Run();
		}

		// view
		[Route("s_servicesview/{Id?}")]
		[Route("Home/s_servicesview/{Id?}")]
		public async Task<IActionResult> s_servicesview()
		{

			// Create page object
			s_services_View = new _s_services_View(this);

			// Run the page
			return await s_services_View.Run();
		}

		// edit
		[Route("s_servicesedit/{Id?}")]
		[Route("Home/s_servicesedit/{Id?}")]
		public async Task<IActionResult> s_servicesedit()
		{

			// Create page object
			s_services_Edit = new _s_services_Edit(this);

			// Run the page
			return await s_services_Edit.Run();
		}

		// delete
		[Route("s_servicesdelete/{Id?}")]
		[Route("Home/s_servicesdelete/{Id?}")]
		public async Task<IActionResult> s_servicesdelete()
		{

			// Create page object
			s_services_Delete = new _s_services_Delete(this);

			// Run the page
			return await s_services_Delete.Run();
		}
	}
}
