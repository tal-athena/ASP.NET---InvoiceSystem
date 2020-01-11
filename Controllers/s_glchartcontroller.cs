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
		[Route("s_glchartlist/{Id?}")]
		[Route("Home/s_glchartlist/{Id?}")]
		public async Task<IActionResult> s_glchartlist()
		{

			// Create page object
			s_glchart_List = new _s_glchart_List(this);
			s_glchart_List.Cache = _cache;

			// Run the page
			return await s_glchart_List.Run();
		}

		// add
		[Route("s_glchartadd/{Id?}")]
		[Route("Home/s_glchartadd/{Id?}")]
		public async Task<IActionResult> s_glchartadd()
		{

			// Create page object
			s_glchart_Add = new _s_glchart_Add(this);

			// Run the page
			return await s_glchart_Add.Run();
		}

		// view
		[Route("s_glchartview/{Id?}")]
		[Route("Home/s_glchartview/{Id?}")]
		public async Task<IActionResult> s_glchartview()
		{

			// Create page object
			s_glchart_View = new _s_glchart_View(this);

			// Run the page
			return await s_glchart_View.Run();
		}

		// edit
		[Route("s_glchartedit/{Id?}")]
		[Route("Home/s_glchartedit/{Id?}")]
		public async Task<IActionResult> s_glchartedit()
		{

			// Create page object
			s_glchart_Edit = new _s_glchart_Edit(this);

			// Run the page
			return await s_glchart_Edit.Run();
		}

		// delete
		[Route("s_glchartdelete/{Id?}")]
		[Route("Home/s_glchartdelete/{Id?}")]
		public async Task<IActionResult> s_glchartdelete()
		{

			// Create page object
			s_glchart_Delete = new _s_glchart_Delete(this);

			// Run the page
			return await s_glchart_Delete.Run();
		}
	}
}
