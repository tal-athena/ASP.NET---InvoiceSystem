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
		[Route("s_dodetltestlist/{TrxId?}")]
		[Route("Home/s_dodetltestlist/{TrxId?}")]
		public async Task<IActionResult> s_dodetltestlist()
		{

			// Create page object
			s_dodetltest_List = new _s_dodetltest_List(this);
			s_dodetltest_List.Cache = _cache;

			// Run the page
			return await s_dodetltest_List.Run();
		}

		// add
		[Route("s_dodetltestadd/{TrxId?}")]
		[Route("Home/s_dodetltestadd/{TrxId?}")]
		public async Task<IActionResult> s_dodetltestadd()
		{

			// Create page object
			s_dodetltest_Add = new _s_dodetltest_Add(this);

			// Run the page
			return await s_dodetltest_Add.Run();
		}

		// view
		[Route("s_dodetltestview/{TrxId?}")]
		[Route("Home/s_dodetltestview/{TrxId?}")]
		public async Task<IActionResult> s_dodetltestview()
		{

			// Create page object
			s_dodetltest_View = new _s_dodetltest_View(this);

			// Run the page
			return await s_dodetltest_View.Run();
		}

		// edit
		[Route("s_dodetltestedit/{TrxId?}")]
		[Route("Home/s_dodetltestedit/{TrxId?}")]
		public async Task<IActionResult> s_dodetltestedit()
		{

			// Create page object
			s_dodetltest_Edit = new _s_dodetltest_Edit(this);

			// Run the page
			return await s_dodetltest_Edit.Run();
		}

		// delete
		[Route("s_dodetltestdelete/{TrxId?}")]
		[Route("Home/s_dodetltestdelete/{TrxId?}")]
		public async Task<IActionResult> s_dodetltestdelete()
		{

			// Create page object
			s_dodetltest_Delete = new _s_dodetltest_Delete(this);

			// Run the page
			return await s_dodetltest_Delete.Run();
		}
	}
}
