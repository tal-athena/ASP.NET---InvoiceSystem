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
		[Route("s_employeelist/{Id?}")]
		[Route("Home/s_employeelist/{Id?}")]
		public async Task<IActionResult> s_employeelist()
		{

			// Create page object
			s_employee_List = new _s_employee_List(this);
			s_employee_List.Cache = _cache;

			// Run the page
			return await s_employee_List.Run();
		}

		// add
		[Route("s_employeeadd/{Id?}")]
		[Route("Home/s_employeeadd/{Id?}")]
		public async Task<IActionResult> s_employeeadd()
		{

			// Create page object
			s_employee_Add = new _s_employee_Add(this);

			// Run the page
			return await s_employee_Add.Run();
		}

		// view
		[Route("s_employeeview/{Id?}")]
		[Route("Home/s_employeeview/{Id?}")]
		public async Task<IActionResult> s_employeeview()
		{

			// Create page object
			s_employee_View = new _s_employee_View(this);

			// Run the page
			return await s_employee_View.Run();
		}

		// edit
		[Route("s_employeeedit/{Id?}")]
		[Route("Home/s_employeeedit/{Id?}")]
		public async Task<IActionResult> s_employeeedit()
		{

			// Create page object
			s_employee_Edit = new _s_employee_Edit(this);

			// Run the page
			return await s_employee_Edit.Run();
		}

		// delete
		[Route("s_employeedelete/{Id?}")]
		[Route("Home/s_employeedelete/{Id?}")]
		public async Task<IActionResult> s_employeedelete()
		{

			// Create page object
			s_employee_Delete = new _s_employee_Delete(this);

			// Run the page
			return await s_employee_Delete.Run();
		}
	}
}
