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
	[AutoValidateAntiforgeryToken]
	public partial class HomeController : Controller
	{
		private IMemoryCache _cache;

		// Constructor
		public HomeController(ILogger<HomeController> logger, IMemoryCache memoryCache)
		{
			_cache = memoryCache;
			Logger = logger;
			UseSession = true;
		}

		// menu
		[Route("ewmenu")]
		[Route("Home/ewmenu")]
		public IActionResult ewmenu() => View();

		// ewemail
		[Route("ewemail")]
		[Route("Home/ewemail")]
		public IActionResult ewemail() => View();

		// personaldata
		[Route("PersonalData/{cmd?}")]
		[Route("Home/PersonalData/{cmd?}")]
		public async Task<IActionResult> PersonalData()
		{

			// Create page object
			_personaldata = new __personaldata(this);

			// Run the page
			return await _personaldata.Run();
		}

		// login
		[Route("login")]
		[Route("Home/login")]
		public async Task<IActionResult> login()
		{

			// Create page object
			_login = new __login(this);

			// Run the page
			return await _login.Run();
		}

		// userpriv
		[Route("userpriv/{UserLevelID?}")]
		[Route("Home/userpriv/{UserLevelID?}")]
		public async Task<IActionResult> userpriv()
		{

			// Create page object
			_userpriv = new __userpriv(this);

			// Run the page
			return await _userpriv.Run();
		}

		// logout
		[Route("logout")]
		[Route("Home/logout")]
		public async Task<IActionResult> logout()
		{

			// Create page object
			_logout = new __logout(this);

			// Run the page
			return await _logout.Run();
		}

		// index
		[Route("")]
		[Route("Index")]
		[Route("Home/Index")]
		public async Task<IActionResult> Index()
		{

			// Create page object
			_index = new __index(this);

			// Run the page
			return await _index.Run();
		}

		// error
		[Route("Error")]
		[Route("Home/Error")]
		public async Task<IActionResult> Error()
		{

			// Create page object
			_error = new __error(this);

			// Run the page
			return await _error.Run();
		}

		// Dispose
		protected override void Dispose(bool disposing) {
			try {
				base.Dispose(disposing);
			} finally {
				CurrentPage?.Terminate();
			}
		}
	}
}
