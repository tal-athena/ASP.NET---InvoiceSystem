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
		[Route("UserLevelPermissionslist/{UserLevelID?}/{_TableName?}")]
		[Route("Home/UserLevelPermissionslist/{UserLevelID?}/{_TableName?}")]
		public async Task<IActionResult> UserLevelPermissionslist()
		{

			// Create page object
			UserLevelPermissions_List = new _UserLevelPermissions_List(this);
			UserLevelPermissions_List.Cache = _cache;

			// Run the page
			return await UserLevelPermissions_List.Run();
		}

		// add
		[Route("UserLevelPermissionsadd/{UserLevelID?}/{_TableName?}")]
		[Route("Home/UserLevelPermissionsadd/{UserLevelID?}/{_TableName?}")]
		public async Task<IActionResult> UserLevelPermissionsadd()
		{

			// Create page object
			UserLevelPermissions_Add = new _UserLevelPermissions_Add(this);

			// Run the page
			return await UserLevelPermissions_Add.Run();
		}

		// view
		[Route("UserLevelPermissionsview/{UserLevelID?}/{_TableName?}")]
		[Route("Home/UserLevelPermissionsview/{UserLevelID?}/{_TableName?}")]
		public async Task<IActionResult> UserLevelPermissionsview()
		{

			// Create page object
			UserLevelPermissions_View = new _UserLevelPermissions_View(this);

			// Run the page
			return await UserLevelPermissions_View.Run();
		}

		// edit
		[Route("UserLevelPermissionsedit/{UserLevelID?}/{_TableName?}")]
		[Route("Home/UserLevelPermissionsedit/{UserLevelID?}/{_TableName?}")]
		public async Task<IActionResult> UserLevelPermissionsedit()
		{

			// Create page object
			UserLevelPermissions_Edit = new _UserLevelPermissions_Edit(this);

			// Run the page
			return await UserLevelPermissions_Edit.Run();
		}

		// delete
		[Route("UserLevelPermissionsdelete/{UserLevelID?}/{_TableName?}")]
		[Route("Home/UserLevelPermissionsdelete/{UserLevelID?}/{_TableName?}")]
		public async Task<IActionResult> UserLevelPermissionsdelete()
		{

			// Create page object
			UserLevelPermissions_Delete = new _UserLevelPermissions_Delete(this);

			// Run the page
			return await UserLevelPermissions_Delete.Run();
		}
	}
}
