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

// API Controllers
namespace AspNetMaker2019.Controllers
{
	[ApiController]
	[Route("api/[controller]/")]
	[EnableCors("ApiCorsPolicy")]
	public abstract class ApiController : Controller
	{
		public static Lang Language = Language ?? new Lang();

		// Constructor
		public ApiController() => UseSession = !Empty(Param(Config.TokenName));
	}

	/// <summary>
	/// List records from a table
	/// </summary>
	/// <example>
	/// api/list/cars
	/// </example>

	public class ListController : ApiController
	{
		[HttpGet("{table}")]
		public async Task<IActionResult> List([FromRoute] string table)
		{
			if (Config.TableClassNames.TryGetValue(table, out string className)) {
				var obj = CreateInstance(className + "_List", new object[] { this });
				return await obj.Run();
			} else {
				return new JsonBoolResult(new { success = false, error = Language.Phrase("TableNotFound"), version = Config.ProductVersion }, false);
			}
		}
	}

	/// <summary>
	/// Get a record from a table
	/// </summary>
	/// <example>
	/// api/view/cars/1
	/// </example>

	public class ViewController : ApiController
	{
		[HttpGet("{table}/{*key}")]
		public async Task<IActionResult> Get([FromRoute] string table)
		{
			if (Config.TableClassNames.TryGetValue(table, out string className)) {
				var obj = CreateInstance(className + "_View", new object[] { this });
				return await obj.Run();
			} else {
				return new JsonBoolResult(new { success = false, error = Language.Phrase("TableNotFound"), version = Config.ProductVersion }, false);
			}
		}
	}

	/// <summary>
	/// Insert a record to a table by POST
	/// </summary>
	/// <example>
	/// api/add
	/// </example>

	public class AddController : ApiController
	{

		// Post
		[HttpPost]
		public async Task<IActionResult> Post([FromForm] string table) => await Add(table);

		// Post with route
		[HttpPost("{table}")]
		public async Task<IActionResult> PostWithRoute([FromRoute] string table) => await Add(table);

		// Add
		protected async Task<IActionResult> Add(string table)
		{
			if (Config.TableClassNames.TryGetValue(table, out string className)) {
				var obj = CreateInstance(className + "_Add", new object[] { this });
				return await obj.Run();
			} else {
				return new JsonBoolResult(new { success = false, error = Language.Phrase("TableNotFound"), version = Config.ProductVersion }, false);
			}
		}
	}

	/// <summary>
	/// Edit a record by POST
	/// </summary>
	/// <example>
	/// api/edit/cars/1
	/// </example>

	public class EditController : ApiController
	{
		[HttpPost("{table}/{*key}")]
		public async Task<IActionResult> Edit([FromRoute] string table)
		{
			if (Config.TableClassNames.TryGetValue(table, out string className)) {
				var obj = CreateInstance(className + "_Edit", new object[] { this });
				return await obj.Run();
			} else {
				return new JsonBoolResult(new { success = false, error = Language.Phrase("TableNotFound"), version = Config.ProductVersion }, false);
			}
		}
	}

	/// <summary>
	/// Delete a record from a table
	/// </summary>
	/// <example>
	/// api/delete/cars/1
	/// </example>

	public class DeleteController : ApiController
	{
		[HttpPost("{table}/{*key}")]
		public async Task<IActionResult> Delete([FromRoute] string table)
		{
			if (Config.TableClassNames.TryGetValue(table, out string className)) {
				var obj = CreateInstance(className + "_Delete", new object[] { this });
				return await obj.Run();
			} else {
				return new JsonBoolResult(new { success = false, error = Language.Phrase("TableNotFound"), version = Config.ProductVersion }, false);
			}
		}
	}

	/// <summary>
	/// Login by POST
	/// </summary>
	/// <example>
	/// api/login
	/// </example>

	public class LoginController : ApiController
	{
		protected string BuildToken(string userName, string userID, string parentUserID, int userLevelID)
		{
			var claims = new[] {
				new Claim(ClaimTypes.Name, userName),
				new Claim("userid", userID, ClaimValueTypes.String),
				new Claim("parentuserid", parentUserID, ClaimValueTypes.String),
				new Claim("userlevelid", Convert.ToString(userLevelID), ClaimValueTypes.Integer)
			};
			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:SecretKey"]));
			FieldInfo fi = typeof(SecurityAlgorithms).GetField(Configuration["Jwt:Algorithm"]);
			string algorithm = fi?.GetRawConstantValue().ToString() ?? SecurityAlgorithms.HmacSha256;
			var creds = new SigningCredentials(key, algorithm);
			var token = new JwtSecurityToken(issuer: Configuration["Jwt:Issuer"],
				audience: Configuration["Jwt:Audience"],
				claims: claims,
				expires: DateTime.Now.AddSeconds(ConvertToDouble(Configuration["Jwt:ExpireTimeAfterLogin"])),
				signingCredentials: creds
			);
			return new JwtSecurityTokenHandler().WriteToken(token);
		}
		[AllowAnonymous]
		[HttpPost]
		public async Task<IActionResult> Post([FromForm] LoginModel model)
		{

			// User profile
			Profile = new UserProfile();

			// Security
			Security = new AdvancedSecurity();
			bool validPwd = await Security.ValidateUser(model, false);

			// As an example, AuthService.CreateToken can return Jose.JWT.Encode(claims, YourTokenSecretKey, Jose.JwsAlgorithm.HS256);
			if (validPwd) {
				return Ok(new { JWT = BuildToken(model.Username, Security.CurrentUserID, Security.CurrentParentUserID, Security.CurrentUserLevelID) });
			} else {
				return BadRequest("Invalid username or password!");
			}
		}
	}

	/// <summary>
	/// Get a file
	/// </summary>
	/// <example>
	/// api/file/cars/Picture/1
	/// </example>

	[AllowAnonymous]
	public class FileController : ApiController
	{
		[HttpGet("{table}/{field}/{*key}")]
		public async Task<IActionResult> GetFile([FromRoute] string table, [FromRoute] string field, [FromRoute] string key)
		{
			var obj = new FileViewer(this);
			return await obj.GetFile(table, field, key);
		}
		[HttpGet("{fn}")]
		public IActionResult GetFile([FromRoute] string fn)
		{
			var obj = new FileViewer(this);
			return obj.GetFile(fn);
		}
	}

	/// <summary>
	/// File upload
	/// </summary>
	/// <example>
	/// api/upload
	/// </example>

	public class UploadController : ApiController
	{
		[HttpPost]
		[HttpPut]
		public async Task<IActionResult> Post()
		{
			var obj = new HttpUpload();
			return await obj.GetUploadedFiles();
		}
	}

	/// <summary>
	/// File upload with jQuery File Upload
	/// </summary>
	/// <example>
	/// api/jupload
	/// </example>

	public class JUploadController : ApiController
	{
		[HttpPost]
		[HttpPut]
		[HttpGet]
		public async Task<IActionResult> Post()
		{
			var obj = new UploadHandler(this);
			return await obj.Run();
		}
	}

	/// <summary>
	/// Session handler
	/// </summary>
	/// <example>
	/// api/session
	/// </example>

	[AllowAnonymous]
	public class SessionController : ApiController
	{
		[HttpGet]
		public IActionResult Get()
		{
			var obj = new SessionHandler(this);
			return obj.GetSession();
		}
	}

	/// <summary>
	/// Lookup (UpdateOption/ModalLookup/AutoSuggest/AutoFill)
	/// </summary>
	/// <example>
	/// api/lookup
	/// </example>

	[AllowAnonymous]
	public class LookupController : ApiController
	{
		[HttpPost]
		public async Task<IActionResult> Post([FromForm] string linkTable)
		{
			if (Config.TableClassNames.TryGetValue(linkTable, out string className)) {
				var obj = CreateInstance(className);
				return await obj.Lookup();
			} else if (typeof(SampleProject.Config).GetField("ReportClassNames") != null) {
				 Dictionary<string, string> names = (Dictionary<string, string>)typeof(SampleProject.Config).GetField("ReportClassNames").GetValue(null);
				 if (names.TryGetValue(linkTable, out string reportClassName)) {
					var obj = CreateInstance(reportClassName);
					return await obj.Lookup();
				 }
			}
			return new JsonBoolResult(new { success = false, error = Language.Phrase("TableNotFound"), version = Config.ProductVersion }, false);
		}
	}

	/// <summary>
	/// Import progress
	/// </summary>
	/// <example>
	/// api/progress/{token}
	/// </example>

	public class ProgressController : ApiController
	{
		private IMemoryCache _cache;
		public ProgressController(IMemoryCache memoryCache)
		{
			_cache = memoryCache;
		}
		[HttpGet("{token}")]
		public IActionResult Get([FromRoute] string token)
		{
			if (!Empty(token) && _cache.TryGetValue<string>(token, out string value))
				return Content(value, "application/json");
			return new EmptyResult();
		}
	}
}
