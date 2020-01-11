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
		[Route("s_dohdrtestlist/{TrxId?}")]
		[Route("Home/s_dohdrtestlist/{TrxId?}")]
		public async Task<IActionResult> s_dohdrtestlist()
		{

			// Create page object
			s_dohdrtest_List = new _s_dohdrtest_List(this);
			s_dohdrtest_List.Cache = _cache;

			// Run the page
			return await s_dohdrtest_List.Run();
		}

		// add
		[Route("s_dohdrtestadd/{TrxId?}")]
		[Route("Home/s_dohdrtestadd/{TrxId?}")]
		public async Task<IActionResult> s_dohdrtestadd()
		{

			// Create page object
			s_dohdrtest_Add = new _s_dohdrtest_Add(this);

			// Run the page
			return await s_dohdrtest_Add.Run();
		}

		// view
		[Route("s_dohdrtestview/{TrxId?}")]
		[Route("Home/s_dohdrtestview/{TrxId?}")]
		public async Task<IActionResult> s_dohdrtestview()
		{

			// Create page object
			s_dohdrtest_View = new _s_dohdrtest_View(this);

			// Run the page
			return await s_dohdrtest_View.Run();
		}

		// edit
		[Route("s_dohdrtestedit/{TrxId?}")]
		[Route("Home/s_dohdrtestedit/{TrxId?}")]
		public async Task<IActionResult> s_dohdrtestedit()
		{

			// Create page object
			s_dohdrtest_Edit = new _s_dohdrtest_Edit(this);

			// Run the page
			return await s_dohdrtest_Edit.Run();
		}

		// delete
		[Route("s_dohdrtestdelete/{TrxId?}")]
		[Route("Home/s_dohdrtestdelete/{TrxId?}")]
		public async Task<IActionResult> s_dohdrtestdelete()
		{

			// Create page object
			s_dohdrtest_Delete = new _s_dohdrtest_Delete(this);

			// Run the page
			return await s_dohdrtest_Delete.Run();
		}
	}
}
