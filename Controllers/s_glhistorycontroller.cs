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
		[Route("s_glhistorylist/{Id?}")]
		[Route("Home/s_glhistorylist/{Id?}")]
		public async Task<IActionResult> s_glhistorylist()
		{

			// Create page object
			s_glhistory_List = new _s_glhistory_List(this);
			s_glhistory_List.Cache = _cache;

			// Run the page
			return await s_glhistory_List.Run();
		}

		// add
		[Route("s_glhistoryadd/{Id?}")]
		[Route("Home/s_glhistoryadd/{Id?}")]
		public async Task<IActionResult> s_glhistoryadd()
		{

			// Create page object
			s_glhistory_Add = new _s_glhistory_Add(this);

			// Run the page
			return await s_glhistory_Add.Run();
		}

		// view
		[Route("s_glhistoryview/{Id?}")]
		[Route("Home/s_glhistoryview/{Id?}")]
		public async Task<IActionResult> s_glhistoryview()
		{

			// Create page object
			s_glhistory_View = new _s_glhistory_View(this);

			// Run the page
			return await s_glhistory_View.Run();
		}

		// edit
		[Route("s_glhistoryedit/{Id?}")]
		[Route("Home/s_glhistoryedit/{Id?}")]
		public async Task<IActionResult> s_glhistoryedit()
		{

			// Create page object
			s_glhistory_Edit = new _s_glhistory_Edit(this);

			// Run the page
			return await s_glhistory_Edit.Run();
		}

		// delete
		[Route("s_glhistorydelete/{Id?}")]
		[Route("Home/s_glhistorydelete/{Id?}")]
		public async Task<IActionResult> s_glhistorydelete()
		{

			// Create page object
			s_glhistory_Delete = new _s_glhistory_Delete(this);

			// Run the page
			return await s_glhistory_Delete.Run();
		}
	}
}
