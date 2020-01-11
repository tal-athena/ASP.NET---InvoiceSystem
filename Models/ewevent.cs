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
		/// Global user code
		/// </summary>
		/// <summary>
		/// Static constructor
		/// </summary>

		static SampleProject()
		{
			var provider = new FileExtensionContentTypeProvider();
			FileOptions = new StaticFileOptions()
			{
				ContentTypeProvider = provider
			};

			// ContentType Mapping event
			ContentType_Mapping(provider.Mappings);

			// Class Init event
			Class_Init();
		}

		/// <summary>
		/// Global events
		/// </summary>
		// ContentType Mapping event

		public static void ContentType_Mapping(IDictionary<string, string> mappings) {

			// Example:
			//mappings[".image"] = "image/png"; // Add new mappings
			//mappings[".rtf"] = "application/x-msdownload"; // Replace an existing mapping
			//mappings.Remove(".mp4"); // Remove MP4 videos

		}

		// Class Init event
		public static void Class_Init() {

			// Enter your code here
		}

		// Page Loading event
		public static void Page_Loading() {

			// Enter your code here
		}

		// Page Rendering event
		public static void Page_Rendering() {

			//Log("Page Rendering");
		}

		// Page Unloaded event
		public static void Page_Unloaded() {

			// Enter your code here
		}

		// Personal Data Downloading event
		public static void PersonalData_Downloading(Dictionary<string, object> row) {

			//Log("PersonalData Downloading");
		}

		// Personal Data Deleted event
		public static void PersonalData_Deleted(Dictionary<string, object> row) {

			//Log("PersonalData Deleted");
		}

		// AuditTrail Inserting event
		public static bool AuditTrail_Inserting(Dictionary<string, object> rsnew) {
			return true;
		}

		/// <summary>
		/// DatabaseConnection class 
		/// </summary>

		public class DatabaseConnection<N, M, R, T> : DatabaseConnectionBase<N, M, R, T>
			where N : DbConnection
			where M : DbCommand
			where R : DbDataReader
		{

			// Constructor
			public DatabaseConnection(string dbid) : base(dbid)
			{
			}

			// Constructor
			public DatabaseConnection() : base()
			{
			}
		}

		/// <summary>
		/// Advanced Security class
		/// </summary>

		public class AdvancedSecurity : AdvancedSecurityBase {

			// Constructor
			public AdvancedSecurity() : base() {
			}
		}

		/// <summary>
		/// Menu class
		/// </summary>

		public class Menu : MenuBase {

			// Constructor
			public Menu(object menuId, bool isRoot = false, bool isNavbar = false, string languageFolder = null) : base(menuId, isRoot, isNavbar, languageFolder) {
			}

			// Render
			public override async Task<string> ToJson() {
				Menu_Rendering();
				return await base.ToJson();
			}
		}
	}
}
