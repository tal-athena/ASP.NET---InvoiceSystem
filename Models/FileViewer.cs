// ASP.NET Maker 2019
// Copyright (c) 2019 e.World Technology Limited. All rights reserved.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Ganss.XSS;
using ImageMagick;
using MimeDetective.InMemory;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using static AspNetMaker2019.Models.SampleProject;

// Models
namespace AspNetMaker2019.Models {

	// Partial class
	public partial class SampleProject {

		/// <summary>
		/// File Viewer class
		/// </summary>

		public class FileViewer
		{

			// Constructor
			public FileViewer(Controller controller = null) { // DN
				if (controller != null)
					Controller = controller;
			}

			/// <summary>
			/// Output file by file name
			/// </summary>
			/// <returns>Action result</returns>

			public IActionResult GetFile(string fn)
			{

				// Get parameters
				string sessionId = Get("session");
				bool resize = Get<bool>("resize");
				int width = Get<int>("width");
				int height = Get<int>("height");
				bool download = Query.TryGetValue("download", out StringValues d) ? ConvertToBool(d) : true; // Download by default
				if (width == 0 && height == 0 && resize) {
					width = Config.ThumbnailDefaultWidth;
					height = Config.ThumbnailDefaultHeight;
				}
				if (Security == null)
					Security = new AdvancedSecurity();
				bool validRequest = Security.IsLoggedIn; // Logged in

				// Reject invalid request
				if (!validRequest)
					return JsonBoolResult.FalseResult;

				// If using session (internal request), file path is always encrypted.
				// If not (external request), DO NOT support external request for file path.

				string key = Config.RandomKey + sessionId;
				fn = (UseSession) ? Decrypt(fn, key) : "";
				if (FileExists(fn)) {
					Response.Clear();
					string ext = Path.GetExtension(fn).Replace(".", "").ToLower();
					string ct = ContentType(fn);
					if (Config.ImageAllowedFileExtensions.Contains(ext, StringComparer.OrdinalIgnoreCase)) {
						if (width > 0 || height > 0)
							return Controller.File(ResizeFileToBinary(fn, ref width, ref height), ct, Path.GetFileName(fn));
						else
							return Controller.PhysicalFile(fn, ct, Path.GetFileName(fn));
					} else if (Config.DownloadAllowedFileExtensions.Contains(ext, StringComparer.OrdinalIgnoreCase)) {
						return Controller.PhysicalFile(fn, ct, Path.GetFileName(fn));
					}
				}
				return JsonBoolResult.FalseResult;
			}

			/// <summary>
			/// Output file by table name, field name and primary key
			/// </summary>
			/// <returns>Action result</returns>

			public async Task<IActionResult> GetFile(string table, string field, string recordkey)
			{

				// Get parameters
				string sessionId = Get("session");
				bool resize = Get<bool>("resize");
				int width = Get<int>("width");
				int height = Get<int>("height");
				bool download = Query.TryGetValue("download", out StringValues d) ? ConvertToBool(d) : true; // Download by default
				if (width == 0 && height == 0 && resize) {
					width = Config.ThumbnailDefaultWidth;
					height = Config.ThumbnailDefaultHeight;
				}

				// Get table object
				string tableName = "";
				dynamic tbl = null;
				if (!Empty(table)) {
					tbl = CreateTable(table);
					tableName = tbl.Name;
				}
				if (Empty(tableName) || Empty(field) || Empty(recordkey))
					return JsonBoolResult.FalseResult;
				bool validRequest = false;
				if (Security == null)
					Security = new AdvancedSecurity();
				if (Security.IsLoggedIn)
					Security.TablePermission_Loading();
				Security.LoadCurrentUserLevel(Config.ProjectId + tableName);
				if (Security.IsLoggedIn)
					Security.TablePermission_Loaded();
				validRequest = Security.CanList || Security.CanView || Security.CanDelete; // With permission
				if (validRequest) {
					Security.UserID_Loading();
					await Security.LoadUserID();
					Security.UserID_Loaded();
				}

				// Reject invalid request
				if (!validRequest)
					return JsonBoolResult.FalseResult;
				return await tbl.GetFileData(field, recordkey, resize, width, height);
			}
		}
	}
}
