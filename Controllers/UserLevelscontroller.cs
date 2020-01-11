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
		[Route("UserLevelslist/{UserLevelID?}")]
		[Route("Home/UserLevelslist/{UserLevelID?}")]
		public async Task<IActionResult> UserLevelslist()
		{

			// Create page object
			UserLevels_List = new _UserLevels_List(this);
			UserLevels_List.Cache = _cache;

			// Run the page
			return await UserLevels_List.Run();
		}

		// add
		[Route("UserLevelsadd/{UserLevelID?}")]
		[Route("Home/UserLevelsadd/{UserLevelID?}")]
		public async Task<IActionResult> UserLevelsadd()
		{

			// Create page object
			UserLevels_Add = new _UserLevels_Add(this);

			// Run the page
			return await UserLevels_Add.Run();
		}

		// view
		[Route("UserLevelsview/{UserLevelID?}")]
		[Route("Home/UserLevelsview/{UserLevelID?}")]
		public async Task<IActionResult> UserLevelsview()
		{

			// Create page object
			UserLevels_View = new _UserLevels_View(this);

			// Run the page
			return await UserLevels_View.Run();
		}

		// edit
		[Route("UserLevelsedit/{UserLevelID?}")]
		[Route("Home/UserLevelsedit/{UserLevelID?}")]
		public async Task<IActionResult> UserLevelsedit()
		{

			// Create page object
			UserLevels_Edit = new _UserLevels_Edit(this);

			// Run the page
			return await UserLevels_Edit.Run();
		}

		// delete
		[Route("UserLevelsdelete/{UserLevelID?}")]
		[Route("Home/UserLevelsdelete/{UserLevelID?}")]
		public async Task<IActionResult> UserLevelsdelete()
		{

			// Create page object
			UserLevels_Delete = new _UserLevels_Delete(this);

			// Run the page
			return await UserLevels_Delete.Run();
		}
	}
}
