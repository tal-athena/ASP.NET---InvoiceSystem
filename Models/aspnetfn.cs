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

		// Current HttpContext
		public static HttpContext HttpContext => HttpContextAccessor.HttpContext;

		// Hosting environment
		public static IHostingEnvironment HostingEnvironment;

		// HttpContext accessor
		private static IHttpContextAccessor HttpContextAccessor;

		// Configuration
		public static IConfiguration Configuration;

		// Antiforgery
		public static IAntiforgery Antiforgery;

		// Configure HttpContext accessor and Hosting environment
		public static void Configure(IHttpContextAccessor httpContextAccessor, IHostingEnvironment env, IConfiguration config, IAntiforgery antiforgery) {
			HttpContextAccessor = httpContextAccessor;
			HostingEnvironment = env;
			Configuration = config;
			Antiforgery = antiforgery;
			Session = new HttpSession();
			Cookie = new HttpCookies();
		}

		/// <summary>
		/// Get web root path
		/// </summary>

		public static string WebRootPath => HostingEnvironment.WebRootPath;

		// Is development
		public static bool IsDevelopment() => HostingEnvironment.IsDevelopment();

		// Get allowed origins
		public static string[] GetOrigins(string origin) {
			if (Empty(origin) || SameString(origin, "*"))
				return new string[] { "*" };
			var origins = origin.Split(',').Select(x => x.Trim());
			if (origins.Any(x => x == "*"))
				return new string[] { "*" };
			else
				return origins.ToArray();
		}

		/// <summary>
		/// Is API request
		/// </summary>
		/// <returns>Whether or not the request is an API request</returns>

		public static bool IsApi(HttpContext context = null) => (Controller != null) ?
			SameString(Controller.GetType().BaseType.ToString(), Config.ProjectNamespace + ".Controllers.ApiController") :
			(context ?? HttpContext).Request.Path.StartsWithSegments(new PathString(AppPath("api")));

		/// <summary>
		/// Get value from Form or Query
		/// </summary>
		/// <param name="name">Name of parameter</param>
		/// <returns>The parameter value or empty string</returns>

		public static string Param(string name) {
			if (Form.TryGetValue(name, out StringValues sv)) {
				return sv;
			} else if (Query.TryGetValue(name, out StringValues qv)) {
				return qv;
			}
			return "";
		}

		/// <summary>
		/// Get value from Form or Query
		/// </summary>
		/// <param name="name">Name of parameter</param>
		/// <typeparam name="T">Type of the returned value</typeparam>
		/// <returns>The parameter value or default of <c>T</c></returns>

		public static T Param<T>(string name) {
			if (Form.TryGetValue(name, out StringValues sv)) {
				return ChangeType<T>(sv.ToString());
			} else if (Query.TryGetValue(name, out StringValues qv)) {
				return ChangeType<T>(qv.ToString());
			}
			return default(T);
		}

		// IAspNetMakerPage interface // DN
		public interface IAspNetMakerPage {
			Task<IActionResult> Run();
			IActionResult Terminate(string url = "");
		}

		// HttpContext data
		public static HttpDataDictionary HttpData => HttpContext.Items.TryGetValue("__Data", out object data)
			? (HttpDataDictionary)data
			: (HttpDataDictionary)(HttpContext.Items["__Data"] = new HttpDataDictionary());

		/// <summary>
		/// Current view (RazorPage)
		/// </summary>
		/// <value>The currrent view (RazorPage)</value>

		public static RazorPage View {
			get => HttpData.Get<RazorPage>("__View");
			set => HttpData["__View"] = value;
		}

		/// <summary>
		/// Get current view output
		/// </summary>
		/// <param name="clear">Clear view output or not</param>
		/// <returns>View output</returns>

		public static async Task<string> GetViewOutput(string viewName = "", bool clear = true) {
			if (Controller == null)
				return null;
			var context = new ActionContext(HttpContext, Controller.RouteData, new ActionDescriptor());
			var originalBody = Response.Body;
			string result;
			try {
				using (var memoryStream = new MemoryStream()) {
					Response.Body = memoryStream;
					if (!Empty(viewName))
						await Controller.View(viewName).ExecuteResultAsync(context);
					else
						await Controller.View().ExecuteResultAsync(context);
					memoryStream.Seek(0, SeekOrigin.Begin);
					using (var reader = new StreamReader(memoryStream)) {
						result = await reader.ReadToEndAsync();
					}
				}
			} finally {
				Response.Body = originalBody;
			}
			if (clear && result != null)
				Response.Clear();
			return result;
		}

		/// <summary>
		/// Current controller
		/// </summary>
		/// <value>The current controller</value>

		public static Controller Controller {
			get => HttpData.Get<Controller>("__Controller");
			set => HttpData["__Controller"] = value;
		}

		/// <summary>
		/// Current logger
		/// </summary>
		/// <value>Logger of the current controller</value>

		public static ILogger Logger {
			get => HttpData.Get<ILogger>("__Logger");
			set => HttpData["__Logger"] = value;
		}

		/// <summary>
		/// Log (debug)
		/// </summary>
		/// <param name="message">Message to log</param>
		/// <param name="args">Arguments for the message</param>

		public static void Log(string message, params object[] args) => LogDebug(message, args);

		/// <summary>
		/// Log objects (debug)
		/// </summary>
		/// <param name="args">Objects to log</param>

		public static void Log(params object[] args) {
			foreach (object value in args)
				LogDebug(JsonConvert.SerializeObject(value, Newtonsoft.Json.Formatting.Indented,
					new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
		}

		// Log debug
		public static void LogDebug(string message, params object[] args) => Logger?.LogDebug(message, args);

		// Log trace
		public static void LogTrace(string message, params object[] args) => Logger?.LogTrace(message, args);

		// Log info
		public static void LogInformation(string message, params object[] args) => Logger?.LogInformation(message, args);

		// Log warning
		public static void LogWarning(string message, params object[] args) => Logger?.LogWarning(message, args);

		// Log error
		public static void LogError(string message, params object[] args) => Logger?.LogError(message, args);

		// Log critical
		public static void LogCritical(string message, params object[] args) => Logger?.LogCritical(message, args);

		/// <summary>
		/// Route data values
		/// </summary>

		public static IDictionary<string, object> RouteValues => Controller?.RouteData.Values;

		/// <summary>
		/// Attribute class
		/// </summary>

		public class Attributes : Dictionary<string, string>
		{

			// Indexer
			public new string this[string key]
			{
				get => TryGetValue(key, out string value) ? value : "";
				set => base[key] = value;
			}

			// Add range
			public void AddRange(IDictionary<string, string> dict)
			{
				if (dict != null) {
					foreach (var (key, value) in dict)
						this[key] = value;
				}
			}

			// Append
			public void Append(string key, string value) => this[key] += value;

			// Prepend
			public void Prepend(string key, string value) => this[key] = value + this[key];

			// Concat
			public void Concat(string key, string value, string sep) => this[key] = Concatenate(this[key], value, sep);

			/// <summary>
			/// Append CSS class name(s) to the "class" attribute
			/// </summary>
			/// <param name="value">CSS class name(s) to append</param>

			public void AppendClass(string value) => this["class"] = Concatenate(this["class"], value, " ");

			/// <summary>
			/// Prepend CSS class name(s) to the "class" attribute
			/// </summary>
			/// <param name="value">class name(s) to prepend</param>

			public void PrependClass(string value) => this["class"] = Concatenate(value, this["class"], " ");

			// Get an attribute value and remove it from dictionary
			public string Extract(string key)
			{
				if (TryGetValue(key, out string val)) {
					base.Remove(key);
					return !Empty(val) ? val : null; // Returns null if empty
				}
				return null; // Returns null if key does not exist
			}
		}

		// HttpData dictionary // DN
		public class HttpDataDictionary : Dictionary<string, object>
		{

			// Indexer
			public new object this[string key]
			{
				get => TryGetValue(key, out object value) ? value : null;
				set => base[key] = value;
			}

			// Get
			public T Get<T>(string key) => TryGetValue(key, out object value) ? (T)value : default(T);

			// Get or create
			public T GetOrCreate<T>(string key) where T : new() => TryGetValue(key, out object value) ? (T)value : (T)(base[key] = new T());
		}

		// Phrase dictionary class (for use in class Lang only) // DN
		public sealed class PhraseDictionary<TKey, TValue> : Dictionary<TKey, TValue>
		{
			public new TValue this[TKey key]
			{
				get => TryGetValue(key, out TValue value) ? value : default(TValue);
				set => base[key] = value;
			}
		}

		// Authentication provider
		public class AuthenticationProvider
		{
			public bool Enabled;
			public string Id;
			public string Color;
			public string Secret;
		}

		// HttpContext.User
		public static ClaimsPrincipal User => HttpContext.User;

		/// <summary>
		/// Is authenticated
		/// </summary>
		/// <returns>Whether the user is authenticated</returns>

		public static bool IsAuthenticated() => User.Identity.IsAuthenticated;

		/// <summary>
		/// Request
		/// </summary>

		public static HttpRequest Request => HttpContext.Request;

		/// <summary>
		/// Response
		/// </summary>

		public static HttpResponse Response => HttpContext.Response;

		/// <summary>
		/// Form collection
		/// </summary>

		public static IFormCollection Form => Request.HasFormContentType ? Request.Form : FormCollection.Empty;

		/// <summary>
		/// Query collection
		/// </summary>

		public static IQueryCollection Query => Request.Query;

		/// <summary>
		/// Form file collection
		/// </summary>

		public static IFormFileCollection Files => Form.Files;

		/// <summary>
		/// Is HTTP POST
		/// </summary>
		/// <returns>Whether request is HTTP POST</returns>

		public static bool IsPost() => SameText(Request.Method, "POST");

		/// <summary>
		/// Is HTTP GET
		/// </summary>
		/// <returns>Whether request is HTTP GET</returns>

		public static bool IsGet() => SameText(Request.Method, "GET");

		/// <summary>
		/// Compare objects as string (case-senstive)
		/// </summary>
		/// <param name="v1">Object</param>
		/// <param name="v2">Object</param>
		/// <returns>Whether the two objects are same string or not</returns>

		public static bool SameString(object v1, object v2)
		{
			if (v1 is DateTime || v2 is DateTime) {
				return SameDate(v1, v2);
			} else {
				return String.Equals(Convert.ToString(v1).Trim(), Convert.ToString(v2).Trim());
			}
		}

		/// <summary>
		/// Compare objects as string (case insensitive)
		/// </summary>
		/// <param name="v1">Object</param>
		/// <param name="v2">Object</param>
		/// <returns>Whether the two objects are same string or not</returns>

		public static bool SameText(object v1, object v2)
		{
			if (v1 is DateTime || v2 is DateTime) {
				return SameDate(v1, v2);
			} else {
				return String.Equals(Convert.ToString(v1).Trim(), Convert.ToString(v2).Trim(), StringComparison.OrdinalIgnoreCase);
			}
		}

		/// <summary>
		/// Compare objects as integer
		/// </summary>
		/// <param name="v1">Object</param>
		/// <param name="v2">Object</param>
		/// <returns>Whether the two objects are same integer</returns>

		public static bool SameInteger(object v1, object v2)
		{
			try {
				return (Convert.ToInt32(v1) == Convert.ToInt32(v2));
			} catch {
				return false;
			}
		}

		/// <summary>
		/// Compare objects as DateTime
		/// </summary>
		/// <param name="v1">Object</param>
		/// <param name="v2">Object</param>
		/// <returns>Whether the two objects are same DateTime</returns>

		public static bool SameDate(object v1, object v2)
		{
			try {
				return (Convert.ToDateTime(v1) == Convert.ToDateTime(v2));
			} catch {
				return false;
			}
		}

		/// <summary>
		/// Check if empty string/object
		/// </summary>
		/// <param name="value">Value to check</param>
		/// <returns>Whether the value is empty</returns>

		public static bool Empty(object value) => value == null || Convert.IsDBNull(value) || String.IsNullOrWhiteSpace(Convert.ToString(value));

		/// <summary>
		/// Check if DBNull
		/// </summary>
		/// <param name="value">Value to check</param>
		/// <returns>Whether the value is DBNull</returns>

		public static bool IsDBNull(object value) => Convert.IsDBNull(value);

		/// <summary>
		/// Convert object to 32-bit integer
		/// </summary>
		/// <param name="value">Value to convert</param>
		/// <returns>Integer value or 0 if failure</returns>

		public static int ConvertToInt(object value)
		{
			try {
				return Convert.ToInt32(value);
			} catch {
				return 0;
			}
		}

		/// <summary>
		/// Convert object to 64-bit integer
		/// </summary>
		/// <param name="value">Value to convert</param>
		/// <returns>Integer value or 0 if failure</returns>

		public static long ConvertToInt64(object value)
		{
			try {
				return Convert.ToInt64(value);
			} catch {
				return 0;
			}
		}

		/// <summary>
		/// Convert object to double
		/// </summary>
		/// <param name="value">Value to convert</param>
		/// <returns>Double value or 0 if failure</returns>

		public static double ConvertToDouble(object value)
		{
			try {
				return Convert.ToDouble(value);
			} catch {
				return 0;
			}
		}

		/// <summary>
		/// Convert object to decimal
		/// </summary>
		/// <param name="value">Value to convert</param>
		/// <returns>Decimal value or 0 if failure</returns>

		public static decimal ConvertToDecimal(object value)
		{
			try {
				return Convert.ToDecimal(value);
			} catch {
				return 0;
			}
		}

		/// <summary>
		/// Convert object to DateTime
		/// </summary>
		/// <param name="value">Value to convert</param>
		/// <returns>DateTime value or DateTime.MinValue if failure</returns>

		public static DateTime ConvertToDateTime(object value)
 		{
 			try {
 				if (value is TimeSpan)
 					return DateTime.Parse(Convert.ToString(value));
 				else
 					return Convert.ToDateTime(value);
 			} catch {
				return DateTime.MinValue;
			}
 		}

		/// <summary>
		/// Convert object to TimeSpan
		/// </summary>
		/// <param name="value">Value to convert</param>
		/// <returns>TimeSpan value or TimeSpan.MinValue if failure</returns>

		public static TimeSpan ConvertToTimeSpan(object value)
 		{
 			try {
 				if (value is TimeSpan)
 					return (TimeSpan)value;
 			} catch {
				return TimeSpan.MinValue;
			}
			return TimeSpan.MinValue;
 		}

		/// <summary>
		/// Convert object to bool
		/// </summary>
		/// <param name="value">Value to convert</param>
		/// <returns>Boolean value or false if failure</returns>

		public static bool ConvertToBool(object value)
		{
			try {
				if (IsNumeric(value)) {
					return ConvertToInt(value) != 0;
				} else if (value is string) {
					return SameText(value, "y") || SameText(value, "t") || SameText(value, "true");
				} else {
					return Convert.ToBoolean(value);
				}
			} catch {
				return false;
			}
		}

		/// <summary>
		/// Convert input value to boolean value for SQL paramater
		/// </summary>
		/// <param name="value">Value to convert</param>
		/// <param name="trueValue">True value</param>
		/// <param name="falseValue">False value</param>
		/// <returns>Object with value convertible to boolean value</returns>

		public static object ConvertToBool(object value, string trueValue, string falseValue)
		{
			object res = value;
			if (!SameString(value, trueValue) && !SameString(value, falseValue))
				res = !Empty(value) ? trueValue : falseValue;
			if (IsNumeric(res)) // Convert to int so it can be converted to bool if necessary
				res = ConvertToInt(res);
			return res;
		}

		/// <summary>
		/// Create and fill an array
		/// </summary>
		/// <param name="count">Number of items in array</param>
		/// <param name="val">Initial value</param>
		/// <typeparam name="T">Data type of the initial value</typeparam>
		/// <returns>Array filled with intial value</returns>

		public static T[] InitArray<T>(int count, T val)
		{
			var ar = new T[count];
			for (int i = 0; i < count; i++)
				ar[i] = val;
			return ar;
		}

		/// <summary>
		/// Prepend CSS class name
		/// </summary>
		/// <param name="attr">The "class" attribute value</param>
		/// <param name="classname">The class name(s) to prepend</param>
		/// <returns>The result "class" attribute value</returns>

		public static string PrependClass(string attr, string classname) => !Empty(classname) && !Empty(attr)
			? String.Join(" ", classname.Trim().Split(' ').Union(attr.Trim().Split(' ')).Where(x => !Empty(x)))
			: attr;

		/// <summary>
		/// Append CSS class name
		/// </summary>
		/// <param name="attr">The "class" attribute value</param>
		/// <param name="classname">The class name(s) to append</param>
		/// <returns>The result "class" attribute value</returns>

		public static string AppendClass(string attr, string classname) => !Empty(classname) && !Empty(attr)
			? String.Join(" ", attr.Trim().Split(' ').Union(classname.Trim().Split(' ')).Where(x => !Empty(x)))
			: attr;

		/// <summary>
		/// Remove CSS class name(s) from a "class" attribute value
		/// </summary>
		/// <param name="attr">The "class" attribute value</param>
		/// <param name="classname">The class name(s) to remove</param>
		/// <returns>The result "class" attribute value</returns>

		public static string RemoveClass(string attr, string classname)=> !Empty(classname) && !Empty(attr)
			? String.Join(" ", attr.Trim().Split(' ').Except(classname.Trim().Split(' ')).Where(x => !Empty(x)))
			: attr;

		// Get numeric formatting information
		public static async Task<JObject> LocaleConvert()
		{
			string langid = CurrentLanguage;
			string localefile = langid + ".json";
			if (!FileExists(ServerMapPath(Config.LocaleFolder) + localefile)) // Locale file not found, try lowercase file name
				localefile = langid.ToLower() + ".json";
			if (!FileExists(ServerMapPath(Config.LocaleFolder) + localefile)) // Locale file not found, fall back to English ("en") locale
				localefile = "en.json";
			return (JObject)JsonConvert.DeserializeObject(await FileReadAllText(ServerMapPath(Config.LocaleFolder + localefile)));
		}

		/// <summary>
		/// Get internal date format (e.g. "yyyy/mm/dd"")
		/// </summary>
		/// <param name="dateFormat">
		/// Date format:
		/// 5 - Ymd (default)
		/// 6 - mdY
		/// 7 - dmY
		/// 9/109 - YmdHis/YmdHi
		/// 10/110 - mdYHis/mdYHi
		/// 11/111 - dmYHis/dmYHi
		/// 12 - ymd
		/// 13 - mdy
		/// 14 - dmy
		/// 15/115 - ymdHis/ymdHi
		/// 16/116 - mdyHis/mdyHi
		/// 17/117 - dmyHis/dmyHi
		/// </param>
		/// <returns>The date format as string, e.g. "yyyy/mm/dd"</returns>

		public static string GetDateFormat(object dateFormat)
		{
			var sep = DateSeparator;
			if (IsNumeric(dateFormat)) {
				int dateFormatId = ConvertToInt(dateFormat);
				if (dateFormatId > 100) // Format without seconds
					dateFormatId -= 100;
				switch (dateFormatId) {
					case 5:
					case 9:
						return $"yyyy{sep}mm{sep}dd";
					case 6:
					case 10:
						return $"mm{sep}dd{sep}yyyy";
					case 7:
					case 11:
						return $"dd{sep}mm{sep}yyyy";
					case 12:
					case 15:
						return $"yy{sep}mm{sep}dd";
					case 13:
					case 16:
						return $"mm{sep}dd{sep}yy";
					case 14:
					case 17:
						return $"dd{sep}mm{sep}yy";
				}
			} else if (!Empty(dateFormat)) {
				switch (dateFormat.ToString().Substring(0, 3)) {
					case "Ymd":
						return $"yyyy{sep}mm{sep}dd";
					case "mdY":
						return $"mm{sep}dd{sep}yyyy";
					case "dmY":
						return $"dd{sep}mm{sep}yyyy";
					case "ymd":
						return $"yy{sep}mm{sep}dd";
					case "mdy":
						return $"mm{sep}dd{sep}yy";
					case "dmy":
						return $"dd{sep}mm{sep}yy";
				}
			}
			return $"yyyy{sep}mm{sep}dd";
		}

		/// <summary>
		/// Get internal date format as ID
		/// </summary>
		/// <param name="dateFormat">
		/// Date format:
		/// 5 - Ymd (default)
		/// 6 - mdY
		/// 7 - dmY
		/// 9/109 - YmdHis/YmdHi
		/// 10/110 - mdYHis/mdYHi
		/// 11/111 - dmYHis/dmYHi
		/// 12 - ymd
		/// 13 - mdy
		/// 14 - dmy
		/// 15/115 - ymdHis/ymdHi
		/// 16/116 - mdyHis/mdyHi
		/// 17/117 - dmyHis/dmyHi
		/// </param>
		/// <returns>The date format as integer</returns>

		public static int GetDateFormatId(object dateFormat)
		{
			if (IsNumeric(dateFormat)) {
				int dateFormatId = ConvertToInt(dateFormat);
				if ((new List<int> { 5, 6, 7, 9, 109, 10, 110, 11, 111, 12, 13, 14, 15, 115, 16, 116, 17, 117 }).Contains(dateFormatId))
					return dateFormatId;
			} else if (!Empty(dateFormat)) {
				switch (dateFormat.ToString()) {
					case "Ymd":
						return 5;
					case "mdY":
						return 6;
					case "dmY":
						return 7;
					case "YmdHis":
						return 9;
					case "YmdHi":
						return 109;
					case "mdYHis":
						return 10;
					case "mdYHi":
						return 110;
					case "dmYHis":
						return 11;
					case "dmYHi":
						return 111;
					case "ymd":
						return 12;
					case "mdy":
						return 13;
					case "dmy":
						return 14;
					case "ymdHis":
						return 15;
					case "ymdHi":
						return 115;
					case "mdyHis":
						return 16;
					case "mdyHi":
						return 116;
					case "dmyHis":
						return 17;
					case "dmyHi":
						return 117;
				}
			}
			return 5;
		}

		// Add message
		public static void AddMessage(ref string msg, string newmsg)
		{
			if (!Empty(newmsg)) {
				if (!Empty(msg))
					msg += "<br>";
				msg += newmsg;
			}
		}

		// Add messages by "<br>" and return the combined message // DN
		public static string AddMessage(string msg, string newmsg)
		{
			if (!Empty(newmsg)) {
				if (!Empty(msg))
					msg += "<br>";
				msg += newmsg;
			}
			return msg;
		}

		// Add filter
		public static void AddFilter(ref string filter, string newfilter)
		{
			if (Empty(newfilter))
				return;
			if (!Empty(filter)) {
				filter = "(" + filter + ") AND (" + newfilter + ")";
			} else {
				filter = newfilter;
			}
		}

		// Add filters by "AND" and return the combined filter
		public static string AddFilter(string filter, string newfilter)
		{
			if (Empty(newfilter))
				return filter;
			if (!Empty(filter)) {
				filter = "(" + filter + ") AND (" + newfilter + ")";
			} else {
				filter = newfilter;
			}
			return filter;
		}

		/// <summary>
		/// Get current user IP
		/// </summary>
		/// <returns>IP4 address</returns>

		public static string CurrentUserIpAddress()
		{
			var ipaddr = HttpContext.Connection.RemoteIpAddress?.ToString() ?? HttpContext.Connection.LocalIpAddress?.ToString();
			if (Empty(ipaddr) || ipaddr == "::1") { // No remote or local IP address or IPv6 enabled machine, check if localhost
				ipaddr = GetIP4Address(Request.Host.ToString().Split(':')[0]);
				if (ipaddr == "127.0.0.1")
					return ipaddr;
			}
			return ipaddr; // Unknown
		}

		/// <summary>
		/// Is local
		/// </summary>
		/// <returns>Whether current user is local (e.g. 127.0.0.1)</returns>

		public static bool IsLocal() => HttpContext.Connection.LocalIpAddress == HttpContext.Connection.RemoteIpAddress || CurrentUserIpAddress() == "127.0.0.1";

		// Get IPv4 Address
		public static string GetIP4Address(string host)
		{
			string ipaddr = String.Empty;
			try {
				foreach (IPAddress ipa in Dns.GetHostAddresses(host)) {
					if (ipa.AddressFamily.ToString() == "InterNetwork") {
						ipaddr = ipa.ToString();
						break;
					}
				}
			} catch {}
			return ipaddr;
		}

		// Get current date in default date format
		public static string CurrentDate(int namedformat)
		{
			if ((new List<int> {5, 9, 12, 15}).Contains(namedformat)) {
				return FormatDateTime(DateTime.Today, 5);
			} else if ((new List<int> {6, 10, 13, 16}).Contains(namedformat)) {
				return FormatDateTime(DateTime.Today, 6);
			} else if ((new List<int> {7, 11, 14, 17}).Contains(namedformat)) {
				return FormatDateTime(DateTime.Today, 7);
			}
			return FormatDateTime(DateTime.Today, 5);
		}

		/// <summary>
		/// Get current date in Ymd (default) format
		/// </summary>
		/// <returns>Current date</returns>

		public static string CurrentDate() => CurrentDate(-1);

		/// <summary>
		/// Get current time in hh:mm:ss format
		/// </summary>
		/// <returns>Current time</returns>

		public static string CurrentTime() => DateTime.Now.ToString("HH':'mm':'ss");

		/// <summary>
		/// Get current date with current time in hh:mm:ss format
		/// </summary>
		/// <param name="namedformat">Date format</param>
		/// <returns>Current date/time</returns>

		public static string CurrentDateTime(int namedformat)
		{
			if ((new List<int> {5, 9, 12, 15}).Contains(namedformat)) {
				return FormatDateTime(DateTime.Now, 9);
			} else if ((new List<int> {6, 10, 13, 16}).Contains(namedformat)) {
				return FormatDateTime(DateTime.Now, 10);
			} else if ((new List<int> {7, 11, 14, 17}).Contains(namedformat)) {
				return FormatDateTime(DateTime.Now, 11);
			}
			return FormatDateTime(DateTime.Now, 9);
		}

		/// <summary>
		/// Get current date in default date format with current time in hh:mm:ss format
		/// </summary>
		/// <returns>Current date/time</returns>

		public static string CurrentDateTime() => CurrentDateTime(-1);

		// HTML Sanitizer
		public static HtmlSanitizer Sanitizer = new HtmlSanitizer();

		/// <summary>
		/// Remove XSS
		/// </summary>
		/// <param name="html">Input HTML</param>
		/// <returns>Sanitized HTML</returns>

		public static string RemoveXss(object html)
		{

			// Handle empty value
			if (Empty(html))
				return "";
			if (Config.RemoveXss)
				return Sanitizer.Sanitize(Convert.ToString(html));
			return Convert.ToString(html);
		}

		// Get session timeout time (seconds)
		public static int SessionTimeoutTime() {
			int mlt = 0;
			if (Config.SessionTimeout > 0) // User specified timeout time
				mlt = Config.SessionTimeout * 60;
			if (mlt <= 0)
				mlt = 1200; // Default (1200s = 20min)
			return mlt - 30; // Add some safety margin
		}

		/// <summary>
		/// Get client variable
		/// </summary>
		/// <param name="name">Name of the variable</param>
		/// <returns>Value of the variable</returns>

		public static object GetClientVar(string name) => !Empty(name) && ClientVariables.TryGetValue(name, out object obj) ? obj : null;

		/// <summary>
		/// Set client variable
		/// </summary>
		/// <param name="name">Name of the variable</param>
		/// <param name="value">Value of the variable</param>

		public static void SetClientVar(string name, object value) {
			if (!Empty(name))
				ClientVariables[name] = value;
		}

		/// <summary>
		/// Is remote path
		/// </summary>
		/// <param name="path">Path</param>
		/// <returns>Whether path is remote</returns>

		public static bool IsRemote(string path) => Regex.IsMatch(path, Config.RemoteFilePattern);

		/// <summary>
		/// Get current user name
		/// </summary>
		/// <returns>Current user name</returns>

		public static string CurrentUserName() => Security?.CurrentUserName ?? Session.GetString(Config.SessionUserName);

		/// <summary>
		/// Get current user ID
		/// </summary>
		/// <returns>Current User ID</returns>

		public static string CurrentUserID() => Security?.CurrentUserID ?? Session.GetString(Config.SessionUserId);

		/// <summary>
		/// Get current parent user ID
		/// </summary>
		/// <returns>Current parent user ID</returns>

		public static string CurrentParentUserID() => Security?.CurrentParentUserID ?? Session.GetString(Config.SessionParentUserId);

		/// <summary>
		/// Get current user level
		/// </summary>
		/// <returns>Current user level ID</returns>

		public static int CurrentUserLevel() => Security?.CurrentUserLevelID ?? Session.GetInt(Config.SessionUserLevelId);

		/// <summary>
		/// Get current user level list
		/// </summary>
		/// <returns>Current user level ID list as comma separated values</returns>

		public static string CurrentUserLevelList() => Security?.UserLevelList() ?? Session.GetString(Config.SessionUserLevelList);

		/// <summary>
		/// Get Current user info
		/// </summary>
		/// <param name="fldname">Field name</param>
		/// <returns>Field value</returns>

		public static object CurrentUserInfo(string fldname)
		{
			object info = null;
			if (Profile.TryGetValue(fldname, out string value))
				return value;
			if (Security != null) {
				info = Security.CurrentUserInfo(fldname);
			} else if (!Empty(Config.UserTable) && !IsSysAdmin() && UserTableConn != null) {
				var user = CurrentUserName();
				if (!Empty(user))
					info = UserTableConn.ExecuteScalar("SELECT " + QuotedName(fldname, Config.UserTableDbId) + " FROM " + Config.UserTable + " WHERE " +
						Config.UserNameFilter.Replace("%u", AdjustSql(user, Config.UserTableDbId)));
			}
			if (info == null && IsAuthenticated())
				info = User.FindFirst(fldname)?.Value;
			return info;
		}

		/// <summary>
		/// Get current page ID
		/// </summary>
		/// <returns>Current page ID</returns>

		public static string CurrentPageID() => CurrentPage?.PageID ?? "";

		// Check if user password expired
		public static bool IsPasswordExpired() => SameString(Session[Config.SessionStatus], "passwordexpired");

		// Check if user password reset
		public static bool IsPasswordReset() => SameString(Session[Config.SessionStatus], "passwordreset");

		// Set session password expired
		public void SetSessionPasswordExpired() => Session[Config.SessionStatus] = "passwordexpired";

		// Check if user is logging in (after changing password)
		public static bool IsLoggingIn() => SameString(Session[Config.SessionStatus], "loggingin");

		/// <summary>
		/// Is Logged In
		/// </summary>
		/// <returns>Whether current user is logged in</returns>

		public static bool IsLoggedIn() => SameString(Session[Config.SessionStatus], "login");

		// Is auto login (login with option "Auto login until I logout explicitly")
		public static bool IsAutoLogin() => SameString(Session[Config.SessionUserLoginType], "a");

		// Get current page heading
		public static string CurrentPageHeading() {
			if (Config.PageTitleStyle != "Title") {
				string heading = CurrentPage?.PageHeading;
				if (!Empty(heading))
					return heading;
			}
			return Language.ProjectPhrase("BodyTitle");
		}

		// Get current page subheading
		public static string CurrentPageSubheading() {
			string heading = "";
			if (Config.PageTitleStyle != "Title")
				heading = CurrentPage?.PageSubheading;
			return heading;
		}

		// Set up login status
		public static void SetupLoginStatus() {
			LoginStatus["isLoggedIn"] = IsLoggedIn();
			LoginStatus["currentUserName"] = CurrentUserName();
			LoginStatus["logoutUrl"] = GetUrl("logout");
			LoginStatus["logoutText"] = Language.Phrase("Logout");
			LoginStatus["loginUrl"] = GetUrl("login");
			LoginStatus["loginText"] = Language.Phrase("Login");
			LoginStatus["canLogin"] = !IsLoggedIn() && !Convert.ToString(LoginStatus["loginUrl"]).EndsWith(CurrentUrl());
			LoginStatus["canLogout"] = IsLoggedIn();
			LoginStatus["hasPersonalData"] = IsLoggedIn() && !IsSysAdmin();
			LoginStatus["personalDataUrl"] = GetUrl("PersonalData");
			LoginStatus["personalDataText"] = Language.Phrase("PersonalDataBtn");
		}

		/// <summary>
		/// Is Export
		/// </summary>
		/// <param name="format">Export format. If unspecified, any format.</param>
		/// <returns>Whether the page is in export mode</returns>

		public static bool IsExport(string format = "") => Empty(format) ? !Empty(ExportType) : SameText(ExportType, format);

		/// <summary>
		/// Is System Admin
		/// </summary>
		/// <returns>Whether the current user is hard-coded system administrator</returns>

		public static bool IsSysAdmin() => Session.GetInt(Config.SessionSysAdmin) == 1;

		/// <summary>
		/// Is Admin
		/// </summary>
		/// <returns>Whether the current user is system administrator (hard-coded or of Administrator User Level)</returns>

		public static bool IsAdmin() => Security?.IsAdmin ?? IsSysAdmin();

		/// <summary>
		/// Get current master table object
		/// </summary>
		/// <value>The master table object</value>

		public static dynamic CurrentMasterTable {
			get {
				string MasterTableName = CurrentPage?.CurrentMasterTable;
				return (!Empty(MasterTableName) && HttpData.TryGetValue(MasterTableName, out object table)) ? (dynamic)table : null;
			}
		}

		/// <summary>
		/// Current language ID
		/// </summary>

		public static string CurrentLanguageID => CurrentLanguage;

		/// <summary>
		/// Current project ID
		/// </summary>

		public static string CurrentProjectID => CurrentPage?.ProjectID ?? Config.ProjectId;

		/// <summary>
		/// Encrypt with key (TripleDES)
		/// </summary>
		/// <param name="data">Data to encrypt</param>
		/// <param name="key">key</param>
		/// <returns>Encrypted data</returns>

		public static string Encrypt(object data, string key) {
			if (Empty(data))
				return "";
			byte[] results = {};
			try {
				var hashProvider = new MD5CryptoServiceProvider();
				var bytes = hashProvider.ComputeHash(Encoding.UTF8.GetBytes(key));
				var algorithm = new TripleDESCryptoServiceProvider {
					Key = bytes,
					Mode = CipherMode.ECB,
					Padding = PaddingMode.PKCS7
				};
				try {
					var dataToEncrypt = Encoding.UTF8.GetBytes(Convert.ToString(data));
					var encryptor = algorithm.CreateEncryptor();
					results = encryptor.TransformFinalBlock(dataToEncrypt, 0, dataToEncrypt.Length);
				} finally {
					algorithm.Clear();
					hashProvider.Clear();
				}
			} catch {}
			return Convert.ToBase64String(results).Replace("+", "-").Replace("/", "_").Replace("=", ""); // Remove padding
		}

		/// <summary>
		/// Encrypt with random key (TripleDES)
		/// </summary>
		/// <param name="data">Data to encrypt</param>
		/// <returns>Encrypted data</returns>

		public static string Encrypt(object data) => Encrypt(data, Config.RandomKey);

		/// <summary>
		/// Decrypt with key (TripleDES)
		/// </summary>
		/// <param name="data">Data to decrypt</param>
		/// <param name="key">key</param>
		/// <returns>Decrypted data</returns>

		public static string Decrypt(object data, string key) {
			if (Empty(data))
				return "";
			byte[] result = {};
			try {
				var hashProvider = new MD5CryptoServiceProvider();
				var bytes = hashProvider.ComputeHash(Encoding.UTF8.GetBytes(key));
				var algorithm = new TripleDESCryptoServiceProvider {
					Key = bytes,
					Mode = CipherMode.ECB,
					Padding = PaddingMode.PKCS7
				};
				var str = Convert.ToString(data).Replace("-", "+").Replace("_", "/");
				var len = str.Length;
				if (len % 4 != 0)
					str = str.PadRight(len + 4 - len % 4, '='); // Add padding
				try {
					var dataToDecrypt = Convert.FromBase64String(str);
					var decryptor = algorithm.CreateDecryptor();
					result = decryptor.TransformFinalBlock(dataToDecrypt, 0, dataToDecrypt.Length);
				} finally {
					algorithm.Clear();
					hashProvider.Clear();
				}
			} catch {}
			return Encoding.UTF8.GetString(result);
		}

		/// <summary>
		/// Decrypt with random key (TripleDES)
		/// </summary>
		/// <param name="data">Data to decrypt</param>
		/// <returns>Decrypted data</returns>

		public static string Decrypt(object data) => Decrypt(data, Config.RandomKey);

		/// <summary>
		/// AES encryption class
		/// </summary>

		public class Aes
		{
			public static byte[] AesEncrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
			{
				byte[] encryptedBytes = null;
				byte[] saltBytes = passwordBytes;
				using (MemoryStream ms = new MemoryStream())
				{
					using (RijndaelManaged AES = new RijndaelManaged())
					{
						AES.KeySize = 256;
						AES.BlockSize = 128;
						var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
						AES.Key = key.GetBytes(AES.KeySize / 8);
						AES.IV = key.GetBytes(AES.BlockSize / 8);
						AES.Mode = CipherMode.CBC;
						using (CryptoStream cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
						{
							cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
						}
						encryptedBytes = ms.ToArray();
					}
				}
				return encryptedBytes;
			}
			public static byte[] AesDecrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
			{
				byte[] decryptedBytes = null;
				byte[] saltBytes = passwordBytes;
				using (MemoryStream ms = new MemoryStream())
				{
					using (RijndaelManaged AES = new RijndaelManaged())
					{
						AES.KeySize = 256;
						AES.BlockSize = 128;
						var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
						AES.Key = key.GetBytes(AES.KeySize / 8);
						AES.IV = key.GetBytes(AES.BlockSize / 8);
						AES.Mode = CipherMode.CBC;
						using (CryptoStream cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
						{
							cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
						}
						decryptedBytes = ms.ToArray();
					}
				}
				return decryptedBytes;
			}
			public static string Encrypt(string input, string password)
			{

				// Get the bytes of the string
				byte[] bytesToBeEncrypted = Encoding.UTF8.GetBytes(input);
				byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

				// Hash the password with SHA256
				passwordBytes = SHA256.Create().ComputeHash(passwordBytes);
				byte[] bytesEncrypted = AesEncrypt(bytesToBeEncrypted, passwordBytes);
				return Convert.ToBase64String(bytesEncrypted).Replace("+", "-").Replace("/", "_").Replace("=", ""); // Remove padding
			}
			public static string Decrypt(string input, string password)
			{
				try {
					string inputBase64 = Convert.ToString(input).Replace("-", "+").Replace("_", "/");
					int len = inputBase64.Length;
					if (len % 4 != 0)
						inputBase64 = inputBase64.PadRight(len + 4 - len % 4, '='); // Add padding

					// Get the bytes of the string
					byte[] bytesToBeDecrypted = Convert.FromBase64String(inputBase64);
					byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
					passwordBytes = SHA256.Create().ComputeHash(passwordBytes);
					byte[] bytesDecrypted = AesDecrypt(bytesToBeDecrypted, passwordBytes);
					string result = Encoding.UTF8.GetString(bytesDecrypted);
					return result;
				} catch {
					return input;
				}
			}
		}

		/// <summary>
		/// Encrypt with key (AES)
		/// </summary>
		/// <param name="input">String to encrypt</param>
		/// <param name="password">Password</param>
		/// <returns>Encrypted string</returns>

		public static string AesEncrypt(string input, string password) => Aes.Encrypt(input, password);

		/// <summary>
		/// Encrypt with random key as password (AES)
		/// </summary>
		/// <param name="input">String to encrypt</param>
		/// <returns>Encrypted string</returns>

		public static string AesEncrypt(string input) => Aes.Encrypt(input, Config.RandomKey);

		/// <summary>
		/// Decrypt with key (AES)
		/// </summary>
		/// <param name="input">String to decrypt</param>
		/// <param name="password">Password</param>
		/// <returns>Decrypted string</returns>

		public static string AesDecrypt(string input, string password) => Aes.Decrypt(input, password);

		/// <summary>
		/// Decrypt with random key as password (AES)
		/// </summary>
		/// <param name="input">String to decrypt</param>
		/// <returns>Decrypted string</returns>

		public static string AesDecrypt(string input) => Aes.Decrypt(input, Config.RandomKey);

		/// <summary>
		/// Save byte array to file
		/// </summary>
		/// <param name="folder">Folder</param>
		/// <param name="fn">File name</param>
		/// <param name="filedata">File data</param>
		/// <returns>Whether the action is successful</returns>

		public static async Task<bool> SaveFile(string folder, string fn, byte[] filedata) {
			if (CreateFolder(folder)) {
				try {
					await FileWriteAllBytes(IncludeTrailingDelimiter(folder, true) + fn, filedata);
					return true;
				} catch {
					if (Config.Debug)
						throw;
					return false;
				}
			}
			return false;
		}

		/// <summary>
		/// Save string to file
		/// </summary>
		/// <param name="folder">Folder</param>
		/// <param name="fn">File name</param>
		/// <param name="filedata">File data</param>
		/// <returns>Whether the action is successful</returns>

		public static async Task<bool> SaveFile(string folder, string fn, string filedata) {
			if (CreateFolder(folder)) {
				try {
					await FileWriteAllText(IncludeTrailingDelimiter(folder, true) + fn, filedata);
					return true;
				} catch {
					if (Config.Debug)
						throw;
					return false;
				}
			}
			return false;
		}

		// Read global debug message
		public static string GetDebugMessage() {
			var msg = DebugMessage;
			DebugMessage = "";
			return (Empty(ExportType) && !Empty(msg)) ? "<div class=\"card card-danger ew-debug\"><div class=\"card-header\"><h3 class=\"card-title\">Debug</h3><div class=\"card-tools\"><button type=\"button\" class=\"btn btn-tool\" data-widget=\"collapse\"><i class=\"fa fa-minus\"></i></button></div></div><div class=\"card-body\">" + msg + "</div></div>" : "";
		}

		// Show global debug message // DN
		public static IHtmlContent ShowDebugMessage() {
			var str = Config.Debug ? GetDebugMessage() : "";
			return new HtmlString(str);
		}

		// Write global debug message // DN
		public static void SetDebugMessage(string v) {
			string[] ar = Regex.Split(v.Trim(), @"<(hr|br)>");
			v = String.Join("; ", ar.ToList().Where(s => !Empty(s)).Select(s => s.Trim()));
			DebugMessage = AddMessage(DebugMessage, "<p><samp>" + GetElapsedTime() + ": " + v + "</samp></p>");
		}

		// Save global debug message
		public static void SaveDebugMessage() {
			if (Config.Debug)
				Session[Config.SessionDebugMessage] = DebugMessage;
		}

		// Load global debug message
		public static void LoadDebugMessage() {
			if (Config.Debug) {
				DebugMessage = Session.GetString(Config.SessionDebugMessage);
				Session[Config.SessionDebugMessage] = "";
			}
		}

		// Permission denied message
		public static string DeniedMessage() => Language.Phrase("NoPermission").Replace("%s", ScriptName());

		/// <summary>
		/// Language class
		/// </summary>

		public class Lang
		{
			public string LanguageId;
			public Dictionary<string, dynamic> Phrases;
			public string LanguageFolder = Config.LanguageFolder; // DN
			public string Template = ""; // JsRender template
			public string Method = "prependTo"; // JsRender template method
			public string Target = ".navbar-nav.ml-auto"; // JsRender template target
			public string Type = "LI"; // LI/DROPDOWN (for used with top Navbar) or SELECT/RADIO (NOT for used with top Navbar)

			// Constructor
			public Lang(string langfolder = "", string langid = "")
			{
				if (!Empty(langfolder))
					LanguageFolder = langfolder;

				// Set up file list
				LoadFileList().GetAwaiter().GetResult();

				// Set up language id
				if (!Empty(langid)) { // Set up language id
					LanguageId = langid;
					Session[Config.SessionLanguageId] = LanguageId;
				} else if (!Empty(Param("language"))) {
					LanguageId = Param("language");
					Session[Config.SessionLanguageId] = LanguageId;
				} else if (!Empty(Session[Config.SessionLanguageId])) {
					LanguageId = Session.GetString(Config.SessionLanguageId);
				} else {
					LanguageId = Config.LanguageDefaultId;
				}
				CurrentLanguage = LanguageId;
				LoadLanguage(LanguageId);

				// Call Language Load event
				Language_Load();
			}

			#pragma warning disable 1998

			// Load language file list
			private async Task LoadFileList()
			{
				Config.LanguageFile = Config.LanguageFile.Select(lang => (dynamic)new {
					Id = lang.Id,
					File = lang.File,
					Desc = LoadFileDesc(MapPath(LanguageFolder + lang.File))
				}).ToList();
			}

			#pragma warning restore 1998

			// Load language file description
			private string LoadFileDesc(string file)
			{
				using (var reader = new XmlTextReader(file)) {
					reader.WhitespaceHandling = WhitespaceHandling.None;
					while (!reader.EOF && reader.Read()) {
						if (reader.IsStartElement() && reader.Name == "ew-language")
							return reader.GetAttribute("desc");
					}
				}
				return "";
			}

			// Load language file
			private void LoadLanguage(string id)
			{
				string fileName = GetFileName(id);
				if (Empty(fileName))
					fileName = GetFileName(Config.LanguageDefaultId);
				if (Empty(fileName))
					return;
				var key = Config.ProjectName + "_" + fileName.Replace(WebRootPath, "").Replace(".xml", "").Replace(Config.PathDelimiter, "_");
				if (Session[key] != null) {
					Phrases = Session.GetValue<Dictionary<string, dynamic>>(key);
				} else {
					Phrases = XmlToDictionary(fileName);
					Session.SetValue(key, Phrases);
				}

				// Set up locale / currency format for language
				JObject locale = LocaleConvert().GetAwaiter().GetResult(); // Sync
				DecimalPoint = locale["decimal_point"].Value<string>();
				ThousandsSeparator = locale["thousands_sep"].Value<string>();
				MonetaryDecimalPoint = locale["mon_decimal_point"].Value<string>();
				MonetaryThousandsSeparator = locale["mon_thousands_sep"].Value<string>();
				CurrencySymbol = locale["currency_symbol"].Value<string>();
				PositiveSign = locale["positive_sign"].Value<string>(); // Note: positive_sign can be empty.
				NegativeSign = locale["negative_sign"].Value<string>();
				FracDigits = locale["frac_digits"].Value<int>();
				CurrencySymbolPrecedesPositive = locale["p_cs_precedes"].Value<int>();
				CurrencySymbolSpacePositive = locale["p_sep_by_space"].Value<int>();
				CurrencySymbolPrecedesNegative = locale["n_cs_precedes"].Value<int>();
				CurrencySymbolSpaceNegative = locale["n_sep_by_space"].Value<int>();
				PositiveSignPosition = locale["p_sign_posn"].Value<int>();
				NegativeSignPosition = locale["n_sign_posn"].Value<int>();
				DateSeparator = locale["date_sep"].Value<string>();
				TimeSeparator = locale["time_sep"].Value<string>();
				DateFormat = GetDateFormat(locale["date_format"]);
				DateFormatId = GetDateFormatId(locale["date_format"]);
				SetupNumberFormatInfo();
			}

			// Convert XML to dictionary
			private Dictionary<string, dynamic> XmlToDictionary(string file) => XElementToDictionary(XElement.Load(file));

			// Convert XElement to dictionary
			private Dictionary<string, dynamic> XElementToDictionary(XElement el)
			{
				var dict = new Dictionary<string, dynamic>();
				if (el.HasElements) {
					foreach (var e in el.Elements()) {
						var name = e.Name.LocalName;
						var id = e.Attribute("id")?.Value;
						if (!dict.ContainsKey(name))
							dict.Add(name, new PhraseDictionary<string, dynamic>());
						if (id != null && !e.HasElements && e.Name.LocalName == "phrase") { // phrase
							var d = e.Attributes().Where(attr => attr.Name.LocalName != "id").ToDictionary(attr => attr.Name.LocalName, attr => attr.Value);
							dict[name].Add(id, d);
						} else if (id != null && e.HasElements) { // table, field, menu
							dict[name].Add(id, XElementToDictionary(e));
						} else if (id == null && e.HasElements) { // global, project
							dict[name] = XElementToDictionary(e);
						}
					}
				}
				return dict;
			}

			// Get language file name
			private string GetFileName(string id)
			{
				var file = Config.LanguageFile.FirstOrDefault(lang => lang.Id == id)?.File;
				if (file != null)
					return MapPath(LanguageFolder + file);
				return "";
			}

			// Get phrase
			public string Phrase(string id, bool useText = false)
			{
				if (Empty(id))
					return "";
				var attrs = PhraseAttrs(id);
				if (attrs == null)
					return "";
				attrs.TryGetValue("class", out string imageClass);
				if (!attrs.TryGetValue("value", out string text))
					text = id; // Return the id if phrase not found
				if (!useText && !Empty(imageClass)) {
					return "<i data-phrase=\"" + id + "\" class=\"" + imageClass + "\" data-caption=\"" + HtmlEncode(text) + "\"></i>";
				} else {
					return text;
				}
			}

			// Set phrase
			public void SetPhrase(string id, string value) => SetPhraseAttr(id, "value", value);

			// Get project phrase
			public string ProjectPhrase(string id) => Phrases["project"]["phrase"][id.ToLower()]?["value"] ?? "";

			// Set project phrase
			public void SetProjectPhrase(string id, string value) => Phrases["project"]["phrase"][id.ToLower()]["value"] = value;

			// Get menu phrase
			public string MenuPhrase(string menuId, string id) => Phrases["project"]["menu"][menuId.ToLower()]?["phrase"][id.ToLower()]?["value"] ?? "";

			// Set menu phrase
			public void SetMenuPhrase(string menuId, string id, string value) => Phrases["project"]["menu"][menuId.ToLower()]["phrase"][id.ToLower()]["value"] = value;

			// Get table phrase
			public string TablePhrase(string tblVar, string id) => Phrases["project"]["table"][tblVar.ToLower()]?["phrase"][id.ToLower()]?["value"] ?? "";

			// Set table phrase
			public void SetTablePhrase(string tblVar, string id, string value) => Phrases["project"]["table"][tblVar.ToLower()]["phrase"][id.ToLower()]["value"] = value;

			// Get field phrase
			public string FieldPhrase(string tblVar, string fldVar, string id) => Phrases["project"]["table"][tblVar.ToLower()]?["field"][fldVar.ToLower()]?["phrase"]?[id.ToLower()]?["value"] ?? "";

			// Set field phrase
			public void SetFieldPhrase(string tblVar, string fldVar, string id, string value) => Phrases["project"]["table"][tblVar.ToLower()]["field"][fldVar.ToLower()]["phrase"][id.ToLower()]["value"] = value;

			// Get chart phrase // DNR
			public string ChartPhrase(string tblVar, string fldVar, string id) => Phrases["project"]["table"][tblVar.ToLower()]?["chart"][fldVar.ToLower()]?["phrase"]?[id.ToLower()]?["value"] ?? "";

			// Set chart phrase // DNR
			public void SetChartPhrase(string tblVar, string fldVar, string id, string value) => Phrases["project"]["table"][tblVar.ToLower()]["chart"][fldVar.ToLower()]["phrase"][id.ToLower()]["value"] = value;

			// Get phrase attributes
			public Dictionary<string, string> PhraseAttrs(string id) => Phrases["global"]["phrase"][id.ToLower()];

			// Get phrase attribute
			public string PhraseAttr(string id, string name = "value")
			{
				var attrs = PhraseAttrs(id);
				return (attrs != null && attrs.TryGetValue(name, out string value)) ? value : "";
			}

			// Set phrase attribute
			public void SetPhraseAttr(string id, string name, string value)
			{
				var attrs = PhraseAttrs(id);
				if (attrs != null)
					attrs[name.ToLower()] = value;
			}

			// Get phrase class
			public string PhraseClass(string id) => PhraseAttr(id, "class");

			// Set phrase attribute
			public void SetPhraseClass(string id, string value) => SetPhraseAttr(id, "class", value);

			// Output dictionary as JSON
			public async Task<string> ToJson(bool client = true)
			{
				if (client) {
					Dictionary<string, string> dict = ((Dictionary<string, dynamic>)Phrases["global"]["phrase"])
						.Where(kvp => kvp.Value.TryGetValue("client", out string value) && value == "1")
						.ToDictionary(kvp => kvp.Key, kvp => (string)kvp.Value["value"]);
					var sw = new StringWriter();
					using (var writer = new JsonTextWriter(sw)) {
						await writer.WriteStartObjectAsync();
						foreach (var (key, value) in dict) {
							await writer.WritePropertyNameAsync(key);
							await writer.WriteValueAsync(value);
						}
						await writer.WriteEndObjectAsync();
					}
					return sw.ToString();
				} else { // All phrase (for debugging)
					return JsonConvert.SerializeObject(Phrases, Newtonsoft.Json.Formatting.Indented);
				}
			}

			// Output client phrases as JavaScript
			public async Task<string> ToScript()
			{
				SetClientVar("languages", new Dictionary<string, object> { {"languages", GetLanguages()} });
				return "ew.language = new ew.Language(" + await ToJson() + ");";
			}

			// Get language info
			public List<Dictionary<string, object>> GetLanguages()
			{
				return (Config.LanguageFile.Count <= 1) ? new List<Dictionary<string, object>> {} :
					Config.LanguageFile.Select(lang => {
						var id = lang.Id;
						var desc = (Phrase(id) != "") ? Phrase(id) : lang.Desc;
						var selected = (id == CurrentLanguage);
						return new Dictionary<string, object> {
							{"id", id},
							{"desc", desc},
							{"selected", selected}
						};
					}).ToList();
			}

			// Get template
			public string GetTemplate()
			{
				if (Template == "") {
					if (SameText(Type, "LI")) { // LI template (for used with top Navbar)
						return "{{for languages}}<li class=\"nav-item\"><a href=\"#\" class=\"nav-link{{if selected}} active{{/if}} ew-tooltip\" title=\"{{>desc}}\" onclick=\"ew.setLanguage(this);\" data-language=\"{{:id}}\">{{:id}}</a></li>{{/for}}";
					} else if (SameText(Type, "DROPDOWN")) { // DROPDOWN template (for used with top Navbar)
						return "<li class=\"nav-item dropdown\"><a href=\"#\" class=\"nav-link\" data-toggle=\"dropdown\"><i class=\"fa fa-globe ew-icon\"></i></span></a><div class=\"dropdown-menu dropdown-menu-lg dropdown-menu-right\">{{for languages}}<a href=\"#\" class=\"dropdown-item{{if selected}} active{{/if}}\" onclick=\"ew.setLanguage(this);\" data-language=\"{{:id}}\">{{>desc}}</a>{{/for}}</div></li>";
					} else if (SameText(Type, "SELECT")) { // SELECT template (NOT for used with top Navbar)
						return "<div class=\"ew-language-option\"><select class=\"form-control\" id=\"ew-language\" name=\"ew-language\" onchange=\"ew.setLanguage(this);\">{{for languages}}<option value=\"{{:id}}\"{{if selected}} selected{{/if}}>{{:desc}}</option>{{/for}}</select></div>";
					} else if (SameText(Type, "RADIO")) { // RADIO template (NOT for used with top Navbar)
						return "<div class=\"ew-language-option\"><div class=\"btn-group\" data-toggle=\"buttons\">{{for languages}}<input type=\"radio\" name=\"ew-language\" id=\"ew-Language-{{:id}}\" autocomplete=\"off\" onchange=\"ew.setLanguage(this);{{if selected}} checked{{/if}}\" value=\"{{:id}}\"><label class=\"btn btn-default ew-tooltip\" for=\"ew-language-{{:id}}\" data-container=\"body\" data-placement=\"bottom\" title=\"{{>desc}}\">{{:id}}</label>{{/for}}</div></div>";
					}
				}
				return Template;
			}

			// Language Load event
			public void Language_Load() {

				// Example:
				//SetPhrase("SaveBtn", "Save Me"); // Refer to language XML file for phrase IDs

			}
		}

		/// <summary>
		/// XML document class
		/// </summary>

		public class XmlDoc : XmlDocument
		{
			public string RootTagName = "table";
			public string DetailTagName = "";
			public string RowTagName = "row";
			public XmlElement Table;
			public XmlElement SubTable;
			public XmlElement Row;
			public XmlElement Field;

			// Load
			public new XmlDocument Load(string fileName) {
				try {
					base.Load(fileName);
					return this;
				} catch {
					return null;
				}
			}

			// Get attribute
			public string GetAttribute(XmlElement element, string name) => element?.GetAttribute(name) ?? "";

			// Set attribute
			public void SetAttribute(XmlElement element, string name, string value) => element?.SetAttribute(name, value);

			// Add root
			public void AddRoot(string rootname) {
				RootTagName = XmlTagName(rootname);
				Table = CreateElement(RootTagName);
				AppendChild(Table);
			}

			// Add row
			public void AddRow(string tableTagName = "", string rowname = "") {
				if (!Empty(rowname))
					RowTagName = XmlTagName(rowname);
				Row = CreateElement(RowTagName);
				if (Empty(tableTagName)) {
					Table?.AppendChild(Row);
				} else {
					if (Empty(DetailTagName) || !SameString(DetailTagName, tableTagName)) {
						DetailTagName = XmlTagName(tableTagName);
						SubTable = CreateElement(DetailTagName);
						Table.AppendChild(SubTable);
					}
					SubTable?.AppendChild(Row);
				}
			}

			// Add field
			public void AddField(string name, object value) {
				Field = CreateElement(XmlTagName(name));
				Row.AppendChild(Field);
				Field.AppendChild(CreateTextNode(Convert.ToString(value)));
			}

			// Append XML element to target element // DN
			public void AppendChild(XmlElement parent, XmlElement child) {
				if (child != null)
					parent?.AppendChild(child);
			}

			// Append XML element to root // DN
			public void AppendChildToRoot(XmlElement child) => AppendChild(Table, child);
		}

		/// <summary>
		/// HTML to Text class
		/// </summary>

		public class HtmlToText
		{

			// Static data tables
			protected static Dictionary<string, string> _tags;
			protected static HashSet<string> _ignoreTags;

			// Instance variables
			protected TextBuilder _text;
			protected string _html;
			protected int _pos;

			// Static constructor (one time only)
			static HtmlToText()
			{
				_tags = new Dictionary<string, string>();
				_tags.Add("address", "\n");
				_tags.Add("blockquote", "\n");
				_tags.Add("div", "\n");
				_tags.Add("dl", "\n");
				_tags.Add("fieldset", "\n");
				_tags.Add("form", "\n");
				_tags.Add("h1", "\n");
				_tags.Add("/h1", "\n");
				_tags.Add("h2", "\n");
				_tags.Add("/h2", "\n");
				_tags.Add("h3", "\n");
				_tags.Add("/h3", "\n");
				_tags.Add("h4", "\n");
				_tags.Add("/h4", "\n");
				_tags.Add("h5", "\n");
				_tags.Add("/h5", "\n");
				_tags.Add("h6", "\n");
				_tags.Add("/h6", "\n");
				_tags.Add("p", "\n");
				_tags.Add("/p", "\n");
				_tags.Add("table", "\n");
				_tags.Add("/table", "\n");
				_tags.Add("ul", "\n");
				_tags.Add("/ul", "\n");
				_tags.Add("ol", "\n");
				_tags.Add("/ol", "\n");
				_tags.Add("/li", "\n");
				_tags.Add("br", "\n");
				_tags.Add("/td", "\t");
				_tags.Add("/tr", "\n");
				_tags.Add("/pre", "\n");
				_ignoreTags = new HashSet<string>();
				_ignoreTags.Add("script");
				_ignoreTags.Add("noscript");
				_ignoreTags.Add("style");
				_ignoreTags.Add("object");
			}

			// Convert HTML to plain text
			public string Convert(string html)
			{

				// Initialize state variables
				_text = new TextBuilder();
				_html = html;
				_pos = 0;

				// Process input
				while (!EndOfText)
				{
					if (Peek() == '<')
					{

						// HTML tag
						string tag = ParseTag(out bool selfClosing);

						// Handle special tag cases
						if (tag == "body")
						{

							// Discard content before <body>
							_text.Clear();
						}
						else if (tag == "/body")
						{

							// Discard content after </body>
							_pos = _html.Length;
						}
						else if (tag == "pre")
						{

							// Enter preformatted mode
							_text.Preformatted = true;
							EatWhitespaceToNextLine();
						}
						else if (tag == "/pre")
						{

							// Exit preformatted mode
							_text.Preformatted = false;
						}
						if (_tags.TryGetValue(tag, out string value))
							_text.Write(value);
						if (_ignoreTags.Contains(tag))
							EatInnerContent(tag);
					}
					else if (Char.IsWhiteSpace(Peek()))
					{

						// Whitespace (treat all as space)
						_text.Write(_text.Preformatted ? Peek() : ' ');
						MoveAhead();
					}
					else
					{

						// Other text
						_text.Write(Peek());
						MoveAhead();
					}
				}

				// Return result
				return HtmlDecode(_text.ToString());
			}

			// Eats all characters that are part of the current tag and returns information about that tag
			protected string ParseTag(out bool selfClosing)
			{
				string tag = String.Empty;
				selfClosing = false;
				if (Peek() == '<')
				{
					MoveAhead();

					// Parse tag name
					EatWhitespace();
					int start = _pos;
					if (Peek() == '/')
						MoveAhead();
					while (!EndOfText && !Char.IsWhiteSpace(Peek()) &&
						Peek() != '/' && Peek() != '>')
						MoveAhead();
					tag = _html.Substring(start, _pos - start).ToLower();

					// Parse rest of tag
					while (!EndOfText && Peek() != '>')
					{
						if (Peek() == '"' || Peek() == '\'')
							EatQuotedValue();
						else
						{
							if (Peek() == '/')
								selfClosing = true;
							MoveAhead();
						}
					}
					MoveAhead();
				}
				return tag;
			}

			// Consumes inner content from the current tag
			protected void EatInnerContent(string tag)
			{
				string endTag = "/" + tag;
				while (!EndOfText)
				{
					if (Peek() == '<')
					{

						// Consume a tag
						if (ParseTag(out bool selfClosing) == endTag)
							return;

						// Use recursion to consume nested tags
						if (!selfClosing && !tag.StartsWith("/"))
							EatInnerContent(tag);
					}
					else MoveAhead();
				}
			}

			// Returns true if the current position is at the end of the string
			protected bool EndOfText => (_pos >= _html.Length);

			// Safely returns the character at the current position
			protected char Peek() => (_pos < _html.Length) ? _html[_pos] : (char)0;

			// Safely advances to current position to the next character
			protected void MoveAhead() => _pos = Math.Min(_pos + 1, _html.Length);

			// Moves the current position to the next non-whitespace character
			protected void EatWhitespace()
			{
				while (Char.IsWhiteSpace(Peek()))
					MoveAhead();
			}

			// Moves the current position to the next non-whitespace character or the start of the next line, whichever comes first
			protected void EatWhitespaceToNextLine()
			{
				while (Char.IsWhiteSpace(Peek()))
				{
					char c = Peek();
					MoveAhead();
					if (c == '\n')
						break;
				}
			}

			// Moves the current position past a quoted value
			protected void EatQuotedValue()
			{
				char c = Peek();
				if (c == '"' || c == '\'')
				{

					// Opening quote
					MoveAhead();

					// Find end of value
					int start = _pos;
					_pos = _html.IndexOfAny(new char[] { c, '\r', '\n' }, _pos);
					if (_pos < 0)
						_pos = _html.Length;
					else
						MoveAhead(); // Closing quote
				}
			}

			// A StringBuilder class that helps eliminate excess whitespace
			protected class TextBuilder
			{
				private StringBuilder _text;
				private StringBuilder _currLine;
				private int _emptyLines;
				private bool _preformatted;

				// Construction
				public TextBuilder()
				{
					_text = new StringBuilder();
					_currLine = new StringBuilder();
					_emptyLines = 0;
					_preformatted = false;
				}

				// Normally, extra whitespace characters are discarded. If this property is set to true, extra whitespace characters are passed through unchanged.
				public bool Preformatted
				{
					get
					{
						return _preformatted;
					}
					set
					{
						if (value)
						{

							// Clear line buffer if changing to
							// preformatted mode

							if (_currLine.Length > 0)
								FlushCurrLine();
							_emptyLines = 0;
						}
						_preformatted = value;
					}
				}

				// Clears all current text
				public void Clear()
				{
					_text.Length = 0;
					_currLine.Length = 0;
					_emptyLines = 0;
				}

				// Writes the given string to the output buffer
				public void Write(string s)
				{
					foreach (char c in s)
						Write(c);
				}

				// Writes the given character to the output buffer.
				public void Write(char c)
				{
					if (_preformatted)
					{

						// Write preformatted character
						_text.Append(c);
					}
					else
					{
						if (c == '\r')
						{

							// Ignore carriage returns. We'll process '\n' if it comes next
						}
						else if (c == '\n')
						{

							// Flush current line
							FlushCurrLine();
						}
						else if (Char.IsWhiteSpace(c))
						{

							// Write single space character
							int len = _currLine.Length;
							if (len == 0 || !Char.IsWhiteSpace(_currLine[len - 1]))
								_currLine.Append(' ');
						}
						else
						{

							// Add character to current line
							_currLine.Append(c);
						}
					}
				}

				// Appends the current line to output buffer
				protected void FlushCurrLine()
				{

					// Get current line
					string line = _currLine.ToString().Trim();

					// Determine if line contains non-space characters
					string tmp = line.Replace("&nbsp;", String.Empty);
					if (tmp.Length == 0)
					{

						// An empty line
						_emptyLines++;
						if (_emptyLines < 2 && _text.Length > 0)
							_text.AppendLine(line);
					}
					else
					{

						// A non-empty line
						_emptyLines = 0;
						_text.AppendLine(line);
					}

					// Reset current line
					_currLine.Length = 0;
				}

				// Returns the current output as a string.
				public override string ToString()
				{
					if (_currLine.Length > 0)
						FlushCurrLine();
					return _text.ToString();
				}
			}
		}

		/// <summary>
		/// Email class
		/// </summary>

		public class Email
		{
			public string Sender = ""; // Sender
			public string Recipient = ""; // Recipient
			public string Cc = ""; // Cc
			public string Bcc = ""; // Bcc
			public string Subject = ""; // Subject
			public string Format = "HTML"; // Format
			public string Content = ""; // Content
			public List<string> Attachments = new List<string>(); // Attachments
			public List<string> EmbeddedImages = new List<string>(); // Embedded image
			public string Charset = Config.EmailCharset; // Charset
			public string SendError = ""; // Send error description
			public bool EnableSsl = SameText(Config.SmtpSecureOption, "SSL"); // Send secure option
			public SmtpClient Mailer = null;

			// Load email from template
			public async Task Load(string fn, string langId = "")
			{
				langId = Empty(langId) ? CurrentLanguage : langId;
				string wrk = "";
				int pos = fn.LastIndexOf('.');
				if (pos > -1) {
					string name = fn.Substring(0, pos); // Get file name
					string ext = fn.Substring(pos+1); // Get file extension
					string path = IncludeTrailingDelimiter(Config.EmailTemplatePath, true); // Get file path
					var list = !Empty(langId) ? new List<string> { "_" + langId, "-" + langId, "" } : new List<string>();
					string file = list.Select(suffix => path + name + suffix + "." + ext).FirstOrDefault(f => FileExists(MapPath(f)));
					if (file == null)
						return;
					wrk = await LoadText(file); // Load template file content
					if (wrk.StartsWith("\xEF\xBB\xBF")) // UTF-8 BOM
						wrk = wrk.Substring(3);
					string id = name + "_content";
					if (wrk.Contains(id)) { // Replace content
						file = path + id + "." + ext;
						if (FileExists(MapPath(file))) {
							string content = await LoadText(file);
							if (content.StartsWith("\xEF\xBB\xBF")) // UTF-8 BOM
								content = content.Substring(3);
							wrk = wrk.Replace("<!--" + id + "-->", content);
						}
					}
				}
				wrk = wrk.Replace("\r\n", "\n").Replace("\r", "\n").Replace("\n", "\r\n"); // Convert line breaks
				if (!Empty(wrk)) {
					int i = wrk.IndexOf("\r\n\r\n"); // Locate header and mail content
					if (i > 0) {
						string header = wrk.Substring(0, i + 1);
						Content = wrk.Substring(i + 2);
						string[] ar = header.Split('\n');
						foreach (string item in ar) {
							i = item.IndexOf(":");
							if (i > 0)
							{
								string name = item.Substring(0, i).Trim().ToLower();
								string value = item.Substring(i + 1).Trim();
								switch (name)
								{
									case "subject":
										Subject = value;
										break;
									case "from":
										Sender = value;
										break;
									case "to":
										Recipient = value;
										break;
									case "cc":
										Cc = value;
										break;
									case "bcc":
										Bcc = value;
										break;
									case "format":
										Format = value;
										break;
								}
							}
						}
					}
				}
			}

			// Replace sender
			public void ReplaceSender(string sender, string senderName = "") => Sender = Sender.Replace("<!--$From-->", Empty(senderName) ? sender : senderName + " <" + sender + ">");

			// Replace recipient
			public void ReplaceRecipient(string recipient, string recipientName = "") => Recipient = Recipient.Replace("<!--$To-->", Empty(recipientName) ? recipient : recipientName + " <" + recipient + ">");

			// Method to add recipient
			public void AddRecipient(string recipient, string recipientName = "")
			{
				if (!Empty(recipient))
					Recipient = Concatenate(Recipient, Empty(recipientName) ? recipient : recipientName + " <" + recipient + ">", ",");
			}

			// Add cc email
			public void AddCc(string cc, string ccName = "")
			{
				if (!Empty(cc))
					Cc = Concatenate(Cc, Empty(ccName) ? cc : ccName + " <" + cc + ">", ",");
			}

			// Add bcc email
			public void AddBcc(string bcc, string bccName = "")
			{
				if (!Empty(bcc))
					Bcc = Concatenate(Bcc, Empty(bccName) ? bcc : bccName + " <" + bcc + ">", ",");
			}

			// Replace subject
			public void ReplaceSubject(string subject) => Subject = Subject.Replace("<!--$Subject-->", subject);

			// Replace content
			public void ReplaceContent(string find, string replaceWith) => Content = Content.Replace(find, replaceWith);

			// Method to add embedded image
			public void AddEmbeddedImage(string image) {
				if (!Empty(image))
					EmbeddedImages.Add(image);
			}

			// Method to add attachment
			public void AddAttachment(string fileName) {
				if (!Empty(fileName))
					Attachments.Add(fileName);
			}

			// Send email
			public bool Send()
			{
				bool send = SendEmail(Sender, Recipient, Cc, Bcc, Subject, Content, Format, Charset, EnableSsl, Attachments, EmbeddedImages, Mailer);
				if (!send)
					SendError = EmailError; // Send error description
				return send;
			}

			// Send email
			public async Task<bool> SendAsync()
			{
				bool send = await SendEmailAsync(Sender, Recipient, Cc, Bcc, Subject, Content, Format, Charset, EnableSsl, Attachments, EmbeddedImages, Mailer);
				if (!send)
					SendError = EmailError; // Send error description
				return send;
			}
		}

		/// <summary>
		/// Pager item class
		/// </summary>

		public class PagerItem
		{
			public string Text;
			public int Start;
			public bool Enabled;

			// Constructor
			public PagerItem(int start, string text, bool enabled)
			{
				Text = text;
				Start = start;
				Enabled = enabled;
			}

			// Constructor
			public PagerItem() {}
		}

		/// <summary>
		/// Pager class
		/// </summary>

		public abstract class Pager
		{
			public PagerItem NextButton;
			public PagerItem FirstButton;
			public PagerItem PrevButton;
			public PagerItem LastButton;
			public int PageSize;
			public int FromIndex;
			public int ToIndex;
			public int RecordCount;
			public int Range;
			public bool Visible = true;
			public bool AutoHidePager = true;
			public bool AutoHidePageSizeSelector = true;
			public bool UsePageSizeSelector = true;
			public string PageSizes;
			public string ItemPhraseId = "Record";
			private bool PageSizeAll = false;

			// Constructor
			public Pager(int fromIndex, int pageSize, int recordCount, string pageSizes = "", int range = 10, bool? autoHidePager = null, bool? autoHidePageSizeSelector = null, bool? usePageSizeSelector = null)
			{
				AutoHidePager = autoHidePager ?? Config.AutoHidePager;
				AutoHidePageSizeSelector = autoHidePageSizeSelector ?? Config.AutoHidePageSizeSelector;
				UsePageSizeSelector = usePageSizeSelector ?? true;
				FromIndex = fromIndex;
				PageSize = pageSize;
				RecordCount = recordCount;
				Range = range;
				PageSizes = pageSizes;

				// Handle page size = 0
				if (PageSize == 0)
					PageSize = RecordCount;

				// Handle page size = -1 (ALL)
				if (PageSize == -1) {
					PageSizeAll = true;
					PageSize = RecordCount;
				}
			}

			// Render
			public virtual HtmlString Render()
			{
				string html = "";
				if (PageSize >= 1 && RecordCount > 0) {

					// Record nos.
					html += $@"<div class=""ew-pager ew-rec"">
	<span>{Language.Phrase(ItemPhraseId)} {FromIndex} {Language.Phrase("To")} {ToIndex} {Language.Phrase("Of")} {RecordCount}</span>
</div>";

					// Page size selector
					if (UsePageSizeSelector && !Empty(PageSizes) && (!AutoHidePageSizeSelector || Visible)) {
						var pageSizes = PageSizes.Split(',');
						var options = pageSizes.Select(pageSize => {
							if (Int32.TryParse(pageSize, out int ps) && ps > 0) {
								return $@"<option value=""{pageSize}""" + (PageSize == ps ? " selected" : "") + $">{pageSize}</option>";
							} else {
								return @"<option value=""ALL""" + (PageSizeAll ? " selected" : "") + $">{Language.Phrase("AllRecords")}</option>";
							}
						});
						html += @"<div class=""ew-pager"">";
						html += $@"<select name=""{Config.TableRecordsPerPage}"" class=""form-control-sm ew-tooltip"" onchange=""this.form.submit();"">{String.Join("\t\t", options)}</select></div>";
					}
				}
				return new HtmlString(html);
			}
		}

		/// <summary>
		/// Class for Numeric pager
		/// </summary>

		public class NumericPager : Pager
		{
			public List<PagerItem> Items = new List<PagerItem>();
			public int ButtonCount = 0;

			// Constructor
			public NumericPager(int fromIndex, int pageSize, int recordCount, string pageSizes = "", int range = 10, bool? autoHidePager = null, bool? autoHidePageSizeSelector = null, bool? usePageSizeSelector = null) :
				base(fromIndex, pageSize, recordCount, pageSizes, range, autoHidePager, autoHidePageSizeSelector, usePageSizeSelector)
			{
				if (AutoHidePager && fromIndex == 1 && recordCount <= pageSize)
					Visible = false;
				FirstButton = new PagerItem();
				PrevButton = new PagerItem();
				NextButton = new PagerItem();
				LastButton = new PagerItem();
				Init();
			}

			// Init pager
			public void Init()
			{
				if (FromIndex > RecordCount)
					FromIndex = RecordCount;
				ToIndex = FromIndex + PageSize - 1;
				if (ToIndex > RecordCount)
					ToIndex = RecordCount;
				SetupNumericPager();

				// Update button count
				if (FirstButton.Enabled)
					ButtonCount++;
				if (PrevButton.Enabled)
					ButtonCount++;
				if (NextButton.Enabled)
					ButtonCount++;
				if (LastButton.Enabled)
					ButtonCount++;
			}

			// Add pager item
			private void AddPagerItem(int startIndex, string text, bool enabled) => Items.Add(new PagerItem(startIndex, text, enabled));

			// Setup pager items
			private void SetupNumericPager()
			{
				bool hasPrev, noNext;
				int dy2, dx2, y, x, dx1, dy1, ny, tempIndex;
				if (RecordCount > PageSize) {
					noNext = (RecordCount < (FromIndex + PageSize));
					hasPrev = (FromIndex > 1);

					// First Button
					tempIndex = 1;
					FirstButton.Start = tempIndex;
					FirstButton.Enabled = (FromIndex > tempIndex);

					// Prev Button
					tempIndex = FromIndex - PageSize;
					if (tempIndex < 1)
						tempIndex = 1;
					PrevButton.Start = tempIndex;
					PrevButton.Enabled = hasPrev;

					// Page links
					if (hasPrev | !noNext) {
						x = 1;
						y = 1;
						dx1 = ((FromIndex - 1) / (PageSize * Range)) * PageSize * Range + 1;
						dy1 = ((FromIndex - 1) / (PageSize * Range)) * Range + 1;
						if ((dx1 + PageSize * Range - 1) > RecordCount) {
							dx2 = (RecordCount / PageSize) * PageSize + 1;
							dy2 = (RecordCount / PageSize) + 1;
						} else {
							dx2 = dx1 + PageSize * Range - 1;
							dy2 = dy1 + Range - 1;
						}
						while (x <= RecordCount) {
							if (x >= dx1 & x <= dx2) {
								AddPagerItem(x, Convert.ToString(y), FromIndex != x);
								x = x + PageSize;
								y = y + 1;
							}
							else if (x >= (dx1 - PageSize * Range) & x <= (dx2 + PageSize * Range)) {
								if (x + Range * PageSize < RecordCount) {
									AddPagerItem(x, y + "-" + (y + Range - 1), true);
								} else {
									ny = (RecordCount - 1) / PageSize + 1;
									if (ny == y) {
										AddPagerItem(x, Convert.ToString(y), true);
									} else {
										AddPagerItem(x, y + "-" + ny, true);
									}
								}
								x = x + Range * PageSize;
								y = y + Range;
							} else {
								x = x + Range * PageSize;
								y = y + Range;
							}
						}
					}

					// Next Button
					NextButton.Start = FromIndex + PageSize;
					tempIndex = FromIndex + PageSize;
					NextButton.Start = tempIndex;
					NextButton.Enabled = !noNext;

					// Last Button
					tempIndex = ((RecordCount - 1) / PageSize) * PageSize + 1;
					LastButton.Start = tempIndex;
					LastButton.Enabled = (FromIndex < tempIndex);
				}
			}

			// Render
			public override HtmlString Render()
			{
				string html = "", href = AppPath(CurrentPageName());
				if (RecordCount > 0 && Visible) {
					if (FirstButton.Enabled)
						html += $@"<li class=""page-item""><a class=""page-link"" href=""{href}?start={FirstButton.Start}"">{Language.Phrase("PagerFirst")}</a></li>";
					if (PrevButton.Enabled)
						html += $@"<li class=""page-item""><a class=""page-link"" href=""{href}?start={PrevButton.Start}"">{Language.Phrase("PagerPrevious")}</a></li>";
					foreach (var PagerItem in Items)
						html += $@"<li class=""page-item{(PagerItem.Enabled ? "" : " active")}""><a class=""page-link"" href=""{(PagerItem.Enabled ? href + "?start=" + PagerItem.Start : "#")}"">{PagerItem.Text}</a></li>";
					if (NextButton.Enabled)
						html += $@"<li class=""page-item""><a class=""page-link"" href=""{href}?start={NextButton.Start}"">{Language.Phrase("PagerNext")}</a></li>";
					if (LastButton.Enabled)
						html += $@"<li class=""page-item""><a class=""page-link"" href=""{href}?start={LastButton.Start}"">{Language.Phrase("PagerLast")}</a></li>";
					html = $@"<div class=""ew-pager"">
	<div class=""ew-numeric-page"">
		<ul class=""pagination"">
			{html}
		</ul>
	</div>
</div>";
				}
				return new HtmlString(html + base.Render().Value);
			}
		}

		/// <summary>
		/// Class for PrevNext pager
		/// </summary>

		public class PrevNextPager : Pager
		{
			public int PageCount;
			public int CurrentPageNumber;

			// Constructor
			public PrevNextPager(int fromIndex, int pageSize, int recordCount, string pageSizes = "", int range = 10, bool? autoHidePager = null, bool? autoHidePageSizeSelector = null, bool? usePageSizeSelector = null) :
				base(fromIndex, pageSize, recordCount, pageSizes, range, autoHidePager, autoHidePageSizeSelector, usePageSizeSelector)
			{
				FirstButton = new PagerItem();
				PrevButton = new PagerItem();
				NextButton = new PagerItem();
				LastButton = new PagerItem();
				Init();
			}

			// Method to init pager
			public void Init()
			{
				int tempIndex;
				if (PageSize > 0) {
					CurrentPageNumber = (FromIndex - 1) / PageSize + 1;
					PageCount = (RecordCount - 1) / PageSize + 1;
					if (AutoHidePager && PageCount == 1)
						Visible = false;
					if (FromIndex > RecordCount)
						FromIndex = RecordCount;
					ToIndex = FromIndex + PageSize - 1;
					if (ToIndex > RecordCount)
						ToIndex = RecordCount;

					// First Button
					tempIndex = 1;
					FirstButton.Start = tempIndex;
					FirstButton.Enabled = (tempIndex != FromIndex);

					// Prev Button
					tempIndex = FromIndex - PageSize;
					if (tempIndex < 1)
						tempIndex = 1;
					PrevButton.Start = tempIndex;
					PrevButton.Enabled = (tempIndex != FromIndex);

					// Next Button
					tempIndex = FromIndex + PageSize;
					if (tempIndex > RecordCount)
						tempIndex = FromIndex;
					NextButton.Start = tempIndex;
					NextButton.Enabled = (tempIndex != FromIndex);

					// Last Button
					tempIndex = ((RecordCount - 1) / PageSize) * PageSize + 1;
					LastButton.Start = tempIndex;
					LastButton.Enabled = (tempIndex != FromIndex);
				}
			}

			// Render
			public override HtmlString Render()
			{
				string html = "", href = AppPath(CurrentPageName());
				if (RecordCount > 0 && Visible) {
					string firstBtn, prevBtn, nextBtn, lastBtn;
					if (FirstButton.Enabled) {
						firstBtn = $@"<a class=""btn btn-default"" title=""{Language.Phrase("PagerFirst")}"" data-start=""{FirstButton.Start}"" href=""{href}?start={FirstButton.Start}""><span class=""icon-first ew-icon""></span></a>";
					} else {
						firstBtn = $@"<a class=""btn btn-default disabled"" title=""{Language.Phrase("PagerFirst")}""><span class=""icon-first ew-icon""></span></a>";
					}
					if (PrevButton.Enabled) {
						prevBtn = $@"<a class=""btn btn-default"" title=""{Language.Phrase("PagerPrevious")}"" data-start=""{PrevButton.Start}"" href=""{href}?start={PrevButton.Start}""><span class=""icon-prev ew-icon""></span></a>";
					} else {
						prevBtn = $@"<a class=""btn btn-default disabled"" title=""{Language.Phrase("PagerPrevious")}""><span class=""icon-prev ew-icon""></span></a>";
					}
					if (NextButton.Enabled) {
						nextBtn = $@"<a class=""btn btn-default"" title=""{Language.Phrase("PagerNext")}"" data-start=""{NextButton.Start}"" href=""{href}?start={NextButton.Start}""><span class=""icon-next ew-icon""></span></a>";
					} else {
						nextBtn = $@"<a class=""btn btn-default disabled"" title=""{Language.Phrase("PagerNext")}""><span class=""icon-next ew-icon""></span></a>";
					}
					if (LastButton.Enabled) {
						lastBtn = $@"<a class=""btn btn-default"" title=""{Language.Phrase("PagerLast")}"" data-start=""{LastButton.Start}"" href=""{href}?start={LastButton.Start}""><span class=""icon-last ew-icon""></span></a>";
					} else {
						lastBtn = $@"<a class=""btn btn-default disabled"" title=""{Language.Phrase("PagerLast")}""><span class=""icon-last ew-icon""></span></a>";
					}
					html += $@"<div class=""ew-pager"">
	<span>{Language.Phrase("Page")}&nbsp;</span>
	<div class=""ew-prev-next"">
		<div class=""input-group input-group-sm"">
			<div class=""input-group-prepend"">
				<!-- first page button -->
				{firstBtn}
				<!-- previous page button -->
				{prevBtn}
			</div>
			<!-- current page number -->
			<input class=""form-control"" type=""text"" data-pagesize=""{PageSize}"" data-pagecount=""{PageCount}"" name=""{Config.TablePageNumber}"" value=""{CurrentPageNumber}"">
			<div class=""input-group-append"">
				<!-- next page button -->
				{nextBtn}
				<!-- last page button -->
				{lastBtn}
			</div>
		</div>
	</div>
	<span>&nbsp;{Language.Phrase("Of")}&nbsp;{PageCount}</span>
	<div class=""clearfix""></div>
</div>";
				}
				return new HtmlString(html + base.Render().Value);
			}
		}

		/// <summary>
		/// BreadcrumbLink class
		/// </summary>

		public class BreadcrumbLink { // DN
			public string Id;
			public string Title;
			public string Url;
			public string ClassName;
			public string TableVar;
			public bool IsCurrent;

			// Constructor
			public BreadcrumbLink(string id, string title, string url, string classname, string tablevar = "", bool current = false) {
				Id = id;
				Title = title;
				Url = url;
				ClassName = classname;
				TableVar = tablevar;
				IsCurrent = current;
			}
		}

		/// <summary>
		/// Breadcrumb class
		/// </summary>

		public class Breadcrumb {
			public List<BreadcrumbLink> Links = new List<BreadcrumbLink>();
			public List<BreadcrumbLink> SessionLinks = new List<BreadcrumbLink>();
			public bool Visible = true;
			public static string CssClass = "breadcrumb float-sm-right ew-breadcrumbs";

			// Constructor
			public Breadcrumb() => Links.Add(new BreadcrumbLink("home", "HomePage", "Index", "ew-home")); // Home

			// Check if an item exists
			protected bool Exists(string pageid, string table, string pageurl) => Links.Any(link => pageid == link.Id && table == link.TableVar && pageurl == link.Url);

			// Add breadcrumb
			public void Add(string pageid, string pagetitle, string pageurl, string pageurlclass = "", string table = "", bool current = false) {

				// Load session links
				LoadSession();

				// Get list of master tables
				var mastertable = new List<string>();
				if (!Empty(table)) {
					var tablevar = table;
					while (!Empty(Session[Config.ProjectName + "_" + tablevar + "_" + Config.TableMasterTable])) {
						tablevar = Session.GetString(Config.ProjectName + "_" + tablevar + "_" + Config.TableMasterTable);
						if (mastertable.Contains(tablevar))
							break;
						mastertable.Add(tablevar);
					}
				}

				// Add master links first
				foreach (var link in SessionLinks) {
					if (mastertable.Contains(link.TableVar) && link.Id == "list") {
						if (link.Url == pageurl)
							break;
						if (!Exists(link.Id, link.TableVar, link.Url)) // DN
							Links.Add(new BreadcrumbLink(link.Id, link.Title, link.Url, link.ClassName, link.TableVar, false));
					}
				}

				// Add this link
				if (!Exists(pageid, table, pageurl))
					Links.Add(new BreadcrumbLink(pageid, pagetitle, pageurl, pageurlclass, table, current));

				// Save session links
				SaveSession();
			}

			// Save links to Session
			public void SaveSession() => Session.SetValue(Config.SessionBreadcrumb, Links); // DN

			// Load links from Session
			public void LoadSession() { // DN
				if (Session[Config.SessionBreadcrumb] != null)
					SessionLinks = Session.GetValue<List<BreadcrumbLink>>(Config.SessionBreadcrumb);
			}

			// Load language phrase
			protected string LanguagePhrase(string title, string table, bool current) {
				var wrktitle = (title == table) ? Language.TablePhrase(title, "TblCaption") : Language.Phrase(title);
				if (current)
					wrktitle = "<span id=\"ew-page-caption\">" + wrktitle + "</span>";
				return wrktitle;
			}

			// Render
			public void Render() {
				if (!Visible || Config.PageTitleStyle == "" || Config.PageTitleStyle == "None")
					return;
				var nav = "<ol class=\"" + CssClass + "\">";
				if (IsList(Links)) {
					var cnt = Links.Count;
					if (Config.PageTitleStyle == "Caption") {

						//Write("<div class=\"ew-page-title\">" + LanguagePhrase(Links[cnt-1].Title, Links[cnt-1].TableVar, Links[cnt-1].IsCurrent) + "</div>");
						return;
					} else {
						for (var i = 0; i < cnt; i++) {
							var link = Links[i];
							var url = link.Url;
							if (i < cnt - 1) {
								nav += "<li class=\"breadcrumb-item\" id=\"ew-breadcrumb" + (i + 1) + "\">";
							} else {
								nav += "<li class=\"breadcrumb-item active\" id=\"ew-breadcrumb" + (i + 1) + "\" class=\"active\">";
								url = ""; // No need to show URL for current page
							}
							var text = LanguagePhrase(link.Title, link.TableVar, link.IsCurrent);
							var title = HtmlTitle(text);
							if (!Empty(url)) {
								nav += "<a href=\"" + GetUrl(url) + "\""; // Output the URL as is // DN
								if (!Empty(title) && title != text)
									nav += " title=\"" + HtmlEncode(title) + "\"";
								if (link.ClassName != "")
									nav += " class=\"" + link.ClassName + "\"";
								nav += ">" + text + "</a>";
							} else {
								nav += text;
							}
							nav += "</li>";
						}
					}
				}
				nav += "</ol>";
				Write(nav);
			}
		}

		/// <summary>
		/// Common class for table and report
		/// </summary>

		public class DbTableBase {
			public string TableVar = "";
			public string Name = "";
			public string Type = "";
			private string _tableCaption = null;
			public string DbId = "DB"; // Table database ID
			public bool UseSelectLimit = true;
			public bool Visible = true;
			public Dictionary<string, DbField> Fields { get; } = new Dictionary<string, DbField>(); // DN
			public List<Dictionary<string, object>> Rows = new List<Dictionary<string, object>>(); // Data for Custom Template DN
			public string Export = ""; // Export
			public string CustomExport = ""; // Custom export
			public bool ExportAll;
			public int ExportPageBreakCount; // Page break per every n record (PDF only)
			public string ExportPageOrientation; // Page orientation (PDF only)
			public string ExportPageSize; // Page size (PDF only)
			public float[] ExportColumnWidths; // Column widths (PDF only) // DN
			public string ExportExcelPageOrientation; // Page orientation (Excel only)
			public string ExportExcelPageSize; // Page size (Excel only)
			public bool SendEmail; // Send email
			public bool ImportInsertOnly = Config.ImportInsertOnly; // Import by insert only
			public bool ImportUseTransaction = Config.ImportUseTransaction; // Import use transaction
			public BasicSearch BasicSearch; // Basic search
			public string CurrentFilter = ""; // Current filter
			public string CurrentOrder = ""; // Current order
			public string CurrentOrderType = ""; // Current order type
			public int RowType; // Row type
			public string CssClass = ""; // CSS class
			public string CssStyle = ""; // CSS style
			public Attributes RowAttrs = new Attributes(); // DN
			public string CurrentAction = ""; // Current action
			public string LastAction = ""; // Last action
			public int UserIdAllowSecurity = 0; // User ID Allow
			public string Command = "";
			private Dictionary<int, string> _pageCaption = new Dictionary<int, string>();
			public int Count = 0; // Record count (as detail table)

			// Update Table
			public string UpdateTable = "";

			// Protected fields (Temp data) // DN
			protected string sqlWrk;
			protected string filterWrk;
			protected string whereWrk;
			protected string curVal;
			protected List<Dictionary<string, object>> rswrk;
			protected string[] arWrk;
			protected List<object> listWrk;
			protected Func<string> lookupFilter;

			// Build filter from array
			public string ArrayToFilter(IDictionary<string, object> row) {
				string filter = "";
				foreach (var (name, value) in row) {
					var fld = FieldByName(name);
					AddFilter(ref filter, QuotedName(fld.Name, DbId) + '=' + QuotedValue(value, fld.DataType, DbId));
				}
				return filter;
			}

			// Reset attributes for table object
			public void ResetAttributes() {
				CssClass = "";
				CssStyle = "";
				RowAttrs.Clear();
				foreach (var (key, fld) in Fields)
					fld.ResetAttributes();
			}

			// Setup field titles
			public void SetupFieldTitles() {
				foreach (var (key, fld) in Fields.Where(kvp => !Empty(kvp.Value.Title))) {
					fld.EditAttrs["data-toggle"] = "tooltip";
					fld.EditAttrs["title"] = HtmlEncode(fld.Title);
				}
			}

			// Get form values (for validation)
			public Dictionary<string, string> GetFormValues() => Fields.ToDictionary(kvp => kvp.Value.Name, kvp => kvp.Value.FormValue); // Use field name

			// Get field values
			public Dictionary<string, object> GetFieldValues(string name) {
				return Fields.ToDictionary(kvp => kvp.Value.Name, kvp => { // Use field name
					PropertyInfo pi = kvp.Value.GetType().GetProperty(name); // Property
					if (pi != null)
						return pi.GetValue(kvp.Value, null);
					FieldInfo fi = kvp.Value.GetType().GetField(name); // Field
					if (fi != null)
						return fi.GetValue(kvp.Value);
					return null;
				});
			}

			// Get field cell attributes
			public Dictionary<string, string> FieldCellAttributes() => Fields.ToDictionary(kvp => kvp.Value.Param, kvp => kvp.Value.CellAttributes); // Use Parm

			// Get field DB values for Custom Template
			public Dictionary<string, object> CustomTemplateFieldValues() {
				return Fields.Where(kvp => Config.CustomTemplateDataTypes.Contains(kvp.Value.DataType)).ToDictionary(kvp => kvp.Value.Param, kvp => { // Use Parm
					var fld = kvp.Value;
					if (fld.DbValue is string value && value.Length > Config.DataStringMaxLength)
						return value.Substring(0, Config.DataStringMaxLength);
					else
						return fld.DbValue;
				});
			}

			// Table caption
			public string Caption {
				get => _tableCaption ?? Language.TablePhrase(TableVar, "TblCaption");
				set => _tableCaption = value;
			}

			// Set page caption
			public void SetPageCaption(int page, string v) => _pageCaption[page] = v;

			// Page caption
			public string PageCaption(int page) {
				_pageCaption.TryGetValue(page, out string caption);
				if (!Empty(caption)) {
					return caption;
				} else {
					caption = Language.TablePhrase(TableVar, "TblPageCaption" + page.ToString());
					if (Empty(caption))
						caption = "Page " + page.ToString();
					return caption;
				}
			}

			// Add URL parameter
			public string UrlParm(string parm = "") {
				var result = "";
				if (!Empty(parm)) {
					if (!Empty(result))
						result += "&";
					result += parm;
				}
				return result;
			}

			// Row Styles
			public string RowStyles {
				get {
					string att = "";
					string style = CssStyle;
					if (RowAttrs.TryGetValue("style", out string s) && !Empty(s))
						style += " " + s;
					string cls = CssClass;
					if (RowAttrs.TryGetValue("class", out string c) && !Empty(c))
						cls += " " + c;
					if (!Empty(style))
						att += " style=\"" + style.Trim() + "\"";
					if (!Empty(cls))
						att += " class=\"" + cls.Trim() + "\"";
					return att;
				}
			}

			// Row Attribute
			public string RowAttributes => !IsExport()
					? RowAttrs.Where(attr => !SameText(attr.Key, "style") && !SameText(attr.Key, "class") && !Empty(attr.Value))
						.Aggregate(RowStyles, (att, attr) => att + " " + attr.Key + "=\"" + attr.Value.Trim() + "\"")
					: RowStyles;

			// Get field object by name
			public DbField FieldByName(string name) => Fields.FirstOrDefault(kvp => kvp.Key == name).Value;

			// Get field object by parm
			public DbField FieldByParm(string parm) => Fields.FirstOrDefault(kvp => kvp.Value.Param == parm).Value;

			// Export
			public bool IsExport(string format = "") => Empty(format) ? !Empty(Export) : SameText(Export, format);

			// Get field visibility
			public virtual bool GetFieldVisibility(string name) => FieldByName(name)?.Visible ?? false;

			/// <summary>
			/// Set use lookup cache
			/// </summary>
			/// <param name="useLookupCache">Use lookup cache or not</param>

			public void SetUseLookupCache(bool useLookupCache)
			{
				foreach (var (key, fld) in Fields)
					fld.UseLookupCache = useLookupCache;
			}

			/// <summary>
			/// Set lookup cache count
			/// </summary>
			/// <param name="count">The maximum number of options to cache</param>

			public void SetLookupCacheCount(int count) {
				foreach (var (key, fld) in Fields)
					fld.LookupCacheCount = count;
			}
		}

		/// <summary>
		/// class for table
		/// </summary>

		public class DbTable : DbTableBase {
			public string CurrentMode = ""; // Current mode
			public string UpdateConflict; // Update conflict
			public string EventName = ""; // Event name
			public bool EventCancelled; // Event cancelled
			public string CancelMessage = ""; // Cancel message
			public bool AllowAddDeleteRow = false; // Allow add/delete row
			public bool ValidateKey = true; // Validate key
			public bool DetailAdd; // Allow detail add
			public bool DetailEdit; // Allow detail edit
			public bool DetailView; // Allow detail view
			public bool ShowMultipleDetails; // Show multiple details
			public int GridAddRowCount;
			public Dictionary<string, string> CustomActions = new Dictionary<string, string>(); // Custom actions

			// Check current action
			// Display

			public bool IsShow => CurrentAction == "show";

			// Add
			public bool IsAdd => CurrentAction == "add";

			// Copy
			public bool IsCopy => CurrentAction == "copy";

			// Edit
			public bool IsEdit => CurrentAction == "edit";

			// Delete
			public bool IsDelete => CurrentAction == "delete";

			// Confirm
			public bool IsConfirm => CurrentAction == "confirm";

			// Overwrite
			public bool IsOverwrite => CurrentAction == "overwrite";

			// Cancel
			public bool IsCancel => CurrentAction == "cancel";

			// Grid add
			public bool IsGridAdd => CurrentAction == "gridadd";

			// Grid edit
			public bool IsGridEdit => CurrentAction == "gridedit";

			// Add/Copy/Edit/GridAdd/GridEdit
			public bool IsAddOrEdit => IsAdd || IsCopy || IsEdit || IsGridAdd || IsGridEdit;

			// Insert
			public bool IsInsert => CurrentAction == "insert";

			// Update
			public bool IsUpdate => CurrentAction == "update";

			// Grid update
			public bool IsGridUpdate => CurrentAction == "gridupdate";

			// Grid insert
			public bool IsGridInsert => CurrentAction == "gridinsert";

			// Grid overwrite
			public bool IsGridOverwrite => CurrentAction == "gridoverwrite";

			// Import
			public bool IsImport => CurrentAction == "import";

			// Search
			public bool IsSearch => CurrentAction == "search";

			// Cancelled
			public bool IsCanceled => LastAction == "cancel" && CurrentAction == "";

			// Inline inserted
			public bool IsInlineInserted => LastAction == "insert" && CurrentAction == "";

			// Inline updated
			public bool IsInlineUpdated => LastAction == "update" && CurrentAction == "";

			// Grid updated
			public bool IsGridUpdated => LastAction == "gridupdate" && CurrentAction == "";

			// Grid inserted
			public bool IsGridInserted => LastAction == "gridinsert" && CurrentAction == "";

			// Inline-Add row
			public bool IsInlineAddRow => IsAdd && RowType == Config.RowTypeAdd;

			// Inline-Copy row
			public bool IsInlineCopyRow => IsCopy && RowType == Config.RowTypeAdd;

			// Inline-Edit row
			public bool IsInlineEditRow => IsEdit && RowType == Config.RowTypeEdit;

			// Inline-Add/Copy/Edit row
			public bool IsInlineActionRow => IsInlineAddRow || IsInlineCopyRow || IsInlineEditRow;

			// Export Return Page
			public string ExportReturnUrl {
				get => Session.TryGetValue(Config.ProjectName + "_" + TableVar + "_" + Config.TableExportReturnUrl, out string url) ? url : CurrentPageName();
				set => Session[Config.ProjectName + "_" + TableVar + "_" + Config.TableExportReturnUrl] = value;
			}

			// Records per page
			private int _recordsPerPage = 0; // DN
			public int RecordsPerPage {
				get {
					return UseSession ? Session.GetInt(Config.ProjectName + "_" + TableVar + "_" + Config.TableRecordsPerPage) : _recordsPerPage;
				}
				set {
					_recordsPerPage = value;
					Session.SetInt(Config.ProjectName + "_" + TableVar + "_" + Config.TableRecordsPerPage, value);
				}
			}

			// Start record number
			private int _startRecordNumber = 0; // DN
			public int StartRecordNumber {
				get {
					return UseSession ? Session.GetInt(Config.ProjectName + "_" + TableVar + "_" + Config.TableStartRec) : _startRecordNumber;
				}
				set {
					_startRecordNumber = value;
					Session.SetInt(Config.ProjectName + "_" + TableVar + "_" + Config.TableStartRec, value);
				}
			}

			// Search Highlight Name
			public string HighlightName => TableVar + "_Highlight";

			// Search highlight value // DN
			public string Highlight(DbField fld) {
				List<string> kwlist = BasicSearch.KeywordList();
				if (Empty(BasicSearch.Type)) // Auto, remove ALL "OR"
					kwlist.Remove("OR");
				List<string> oprs = new List<string>(new string[]{ "=", "LIKE", "STARTS WITH", "ENDS WITH" }); // Valid operators for highlight
				if (oprs.Contains(fld.AdvancedSearch.GetValue("z"))) {
					string akw = fld.AdvancedSearch.GetValue();
					if (akw.Length > 0)
						kwlist.Add(akw);
				}
				if (oprs.Contains(fld.AdvancedSearch.GetValue("w"))) {
					string akw = fld.AdvancedSearch.GetValue("y");
					if (akw.Length > 0)
						kwlist.Add(akw);
				}
				string src = Convert.ToString(fld.ViewValue);
				if (kwlist.Count == 0)
					return src;
				int pos1 = 0;
				int pos2 = 0;
				string val = "";
				string src1 = "";
				foreach (Match match in Regex.Matches(src, @"<([^>]*)>", RegexOptions.IgnoreCase)) {
					pos2 = match.Index;
					if (pos2 > pos1) {
						src1 = src.Substring(pos1, pos2-pos1);
						val += Highlight(kwlist, src1);
					}
					val += match.Value;
					pos1 = pos2 + match.Value.Length;
				}
				pos2 = src.Length;
				if (pos2 > pos1) {
					src1 = src.Substring(pos1, pos2-pos1);
					val += Highlight(kwlist, src1);
				}
				return val;
			}

			// Highlight keyword
			public string Highlight(List<string> kwlist, string src) {
				string pattern = "";
				foreach (var kw in kwlist)
					pattern += (pattern == "" ? "" : "|") + Regex.Escape(kw);
				if (pattern == "")
					return src;
				pattern = "(" + pattern + ")";
				string dest = "<span class=\"" + HighlightName + " ew-highlight-search\">$1</span>";
				if (Config.HighlightCompare) {
					src = Regex.Replace(src, pattern, dest, RegexOptions.IgnoreCase);
				} else {
					src = Regex.Replace(src, pattern, dest);
				}
				return src;
			}

			// Search WHERE clause
			private string _sessionSearchWhere = ""; // DN
			public string SessionSearchWhere {
				get {
					return UseSession ? Session.GetString(Config.ProjectName + "_" + TableVar + "_" + Config.TableSearchWhere) : _sessionSearchWhere;
				}
				set {
					_sessionSearchWhere = value;
					Session[Config.ProjectName + "_" + TableVar + "_" + Config.TableSearchWhere] = value;
				}
			}

			// Session WHERE Clause
			private string _sessionWhere = ""; // DN
			public string SessionWhere {
				get {
					return UseSession ? Session.GetString(Config.ProjectName + "_" + TableVar + "_" + Config.TableWhere) : _sessionWhere;
				}
				set {
					_sessionWhere = value;
					Session[Config.ProjectName + "_" + TableVar + "_" + Config.TableWhere] = value;
				}
			}

			// Session ORDER BY
			private string _orderBy = ""; // DN
			public string SessionOrderBy {
				get {
					return UseSession ? Session.GetString(Config.ProjectName + "_" + TableVar + "_" + Config.TableOrderBy) : _orderBy;
				}
				set {
					_orderBy = value;
					Session[Config.ProjectName + "_" + TableVar + "_" + Config.TableOrderBy] = value;
				}
			}

			// Get key from session
			public object GetKey(string fldparm) => Session[Config.ProjectName + "_" + TableVar + "_" + Config.TableKey + "_" + fldparm];

			// Save key to session
			public void SetKey(string fldparm, object v) => Session[Config.ProjectName + "_" + TableVar + "_" + Config.TableKey + "_" + fldparm] = v;
		}

		/// <summary>
		/// Field class of T
		/// </summary>
		/// <typeparam name="T">Field data type</typeparam>

		public class DbField<T> : DbField { // DN
			public T DbType;
		}

		/// <summary>
		/// Field class
		/// </summary>

		public class DbField {
			public string TableName; // Table name
			public string TableVar; // Table variable name
			public string SourceTableVar; // Source Table variable name (for Report only)
			public string Name; // Field name
			public string FieldVar; // Field variable name
			public string Param; // Field parameter name
			public string Expression; // Field expression (used in SQL)
			public string BasicSearchExpression; // Field expression (used in basic search SQL)
			public bool IsCustom = false; // Custom field
			public bool IsVirtual; // Virtual field
			public string VirtualExpression; // Virtual field expression (used in ListSql)
			public bool ForceSelection; // Autosuggest force selection
			public bool SelectMultiple; // Select multiple
			public bool VirtualSearch; // Search as virtual field
			public string DefaultErrorMessage; // Default error message
			public object VirtualValue; // Virtual field value
			public object TooltipValue; // Field tooltip value
			public int TooltipWidth = 0; // Field tooltip width
			public int Type; // Field type (ADO data type)
			public int DataType; // Field type (ASP.NET Maker data type)
			public string BlobType; // For Oracle only
			public bool Visible = true; // Visible
			public string ViewTag = ""; // View Tag
			public string HtmlTag; // Html Tag
			public bool IsDetailKey = false; // Field is detail key
			public bool IsAutoIncrement = false; // Autoincrement field (FldAutoIncrement)
			public bool IsPrimaryKey = false; // Primary key (FldIsPrimaryKey)
			public bool IsForeignKey = false; // Foreign key (Master/Detail key)
			public bool IsEncrypt = false; // Field is encrypted
			public bool Nullable = true; // Nullable
			public bool Required = false; // Required
			public AdvancedSearch AdvancedSearch; // Advanced Search Object
			public HttpUpload Upload; // Upload Object
			public int DateTimeFormat; // Date time format
			public string CssStyle = ""; // CSS style
			public string CssClass = ""; // CSS class
			public string ImageAlt = ""; // Image alt
			public int ImageWidth = 0; // Image width
			public int ImageHeight = 0; // Image height
			public bool ImageResize = false; // Image resize
			public bool IsBlobImage = false; // Is blob image
			public object ViewCustomAttributes;
			public object EditCustomAttributes;
			public object LinkCustomAttributes; // Link custom attributes
			public string CustomMessage = ""; // Custom message
			public string CellCssClass = ""; // Cell CSS class
			public string CellCssStyle = ""; // Cell CSS style
			public object CellCustomAttributes;
			public string HeaderCellCssClass = ""; // Header cell (<th>) CSS class
			public string FooterCellCssClass = ""; // Footer cell (<td> in <tfoot>) CSS class
			public string MultiUpdate = ""; // Multi update
			public object OldValue; // Old Value
			public object ConfirmValue; // Confirm Value
			public object CurrentValue; // Current value
			public object ViewValue; // View value
			public object EditValue; // Edit value
			public object EditValue2; // Edit value 2 (search)
			public object HrefValue; // Href value
			public object ExportHrefValue; // Href value for export
			private string _formValue; // Form value
			private string _queryValue; // QueryString value
			protected object _dbValue; // Database value
			public bool Sortable = true;
			private string _uploadPath = Config.UploadDestPath; // Upload path // DN
			private string _oldUploadPath = Config.UploadDestPath; // Old upload path (for deleting old image) // DN
			public string UploadAllowedFileExtensions = Config.UploadAllowedFileExtensions; // Allowed file extensions
			public int UploadMaxFileSize = Config.MaxFileSize; // Upload max file size
			public int UploadMaxFileCount = Config.MaxFileCount; // Upload max file count
			public bool UploadMultiple = false; // Multiple Upload
			public Attributes CellAttrs = new Attributes(); // Cell attributes
			public Attributes EditAttrs = new Attributes(); // Edit Attributes
			public Attributes ViewAttrs = new Attributes(); // View Attributes
			public Attributes LinkAttrs = new Attributes(); // Link custom attributes
			public Lookup<DbField> Lookup = null;
			public int OptionCount = 0;
			public bool UseLookupCache = Config.UseLookupCache; // Use lookup cache
			public int LookupCacheCount = Config.LookupCacheCount; // Lookup cache count
			public bool UsePleaseSelect;
			public string PleaseSelectText;
			public int Count = 0; // Count
			public double Total = 0; // Total
			public string TrueValue = "1";
			public string FalseValue = "0";
			public bool Disabled; // Disabled
			public bool ReadOnly; // ReadOnly
			public bool TruncateMemoRemoveHtml; // Remove Html from Memo field
			public string CustomMsg = ""; // Custom message
			public bool IsUpload; // DN
			public bool UseColorbox = Config.UseColorbox; // Use Colorbox
			public object DisplayValueSeparator = ", "; // String or String[]
			public bool Exportable = true;
			public bool AutoFillOriginalValue = Config.AutoFillOriginalValue;
			public string RequiredErrorMessage = Language.Phrase("EnterRequiredField"); // DN
			public Func<string> GetSelectFilter { get; set; } // DN
			public Func<object> GetSearchDefault { get; set; } // DN
			public Func<object> GetSearchDefault2 { get; set; } // DN
			public Func<object> GetEditCustomAttributes { get; set; } // DN
			public Func<object> GetViewCustomAttributes { get; set; } // DN
			public Func<object> GetLinkCustomAttributes { get; set; } // DN
			public Func<object> GetAutoUpdateValue { get; set; } // DN
			public Func<object> GetDefault { get; set; } // DN
			public Func<object> GetHiddenValue { get; set; } // DN
			public Func<string> GetUploadPath { get; set; } // DN
			public Func<object> GetServerValidateArguments { get; set; } // DN
			public Func<string> GetLinkPrefix { get; set; } // DN
			public Func<string> GetLinkSuffix { get; set; } // DN

			// Table object
			public DbTableBase Table { get; set; } // DN

			// Init // DN
			public virtual void Init(DbTableBase table) {
				DataType = FieldDataType(Type);
				Param = FieldVar.Substring(2); // Remove "x_"
				AdvancedSearch = new AdvancedSearch(TableVar, Param);
				if (IsUpload)
					Upload = new HttpUpload(this);
				Table = table;
			}

			// Field parm // DN
			internal string FldParm => FieldVar.Substring(2);

			// Field encryption/decryption required
			public bool UseEncryption { // DN
				get {
					bool encrypt = IsEncrypt && (DataType == Config.DataTypeString || DataType == Config.DataTypeMemo) &&
						!IsPrimaryKey && !IsAutoIncrement && !IsForeignKey;

					// Do not encrypt username/password/userid/userlevel/profile/activate/email fields in user table
					var arName = new List<string> { Config.LoginUsernameFieldName, Config.LoginPasswordFieldName, Config.UserIdFieldName, Config.UserLevelFieldName, Config.UserProfileFieldName, Config.RegisterActivateFieldName, Config.UserEmailFieldName };
					if (encrypt && (TableName == Config.UserTableName && arName.Contains(Name)))
						encrypt = false;
					return encrypt;
				}
			}

			// Upload path // DN
			public string UploadPath {
				get => IncludeTrailingDelimiter(_uploadPath, false);
				set => _uploadPath = value;
			}

			// Old upload path // DN
			public string OldUploadPath {
				get => IncludeTrailingDelimiter(_oldUploadPath, false);
				set => _oldUploadPath = value;
			}

			// Is BLOB field
			public bool IsBlob => DataType == Config.DataTypeBlob;

			// Place holder
			private string _placeHolder = "";
			public string PlaceHolder {
				get => (ReadOnly || EditAttrs.ContainsKey("readonly")) ? "" : _placeHolder;
				set => _placeHolder = value;
			}

			// Field caption
			private string _caption = null;
			public string Caption {
				get => _caption ?? Language.FieldPhrase(TableVar, Param, "FldCaption");
				set => _caption = value;
			}

			// Field title
			public string Title => Language.FieldPhrase(TableVar, Param, "FldTitle");

			// Field alt
			public string Alt => Language.FieldPhrase(TableVar, Param, "FldAlt");

			// Field error msg
			public string ErrorMessage {
				get {
					string errMsg = Language.FieldPhrase(TableVar, Param, "FldErrMsg");
					if (Empty(errMsg))
						errMsg = DefaultErrorMessage + " - " + Caption;
					return errMsg;
				}
			}

			// Field option value
			public string TagValue(int i) => Language.FieldPhrase(TableVar, Param, "FldTagValue" + i.ToString());

			// Field option caption
			public string TagCaption(int i) => Language.FieldPhrase(TableVar, Param, "FldTagCaption" + i.ToString());

			// Set field visibility
			public void SetVisibility() => Visible = Table.GetFieldVisibility(Name);

			// Check if multiple selection
			public bool IsMultiSelect => SameText(HtmlTag, "SELECT") && SelectMultiple || SameText(HtmlTag, "CHECKBOX");

			// Field lookup cache option
			public OptionValues LookupCacheOption(string val)
			{
				if (Empty(val) || Empty(Lookup) || Lookup.Options.Count == 0)
					return null;
				OptionValues res = new OptionValues();
				if (IsMultiSelect) { // Multiple options
					var arwrk = val.Split(',');
					foreach (string wrk in arwrk) {
						if (Lookup.Options.TryGetValue(wrk.Trim(), out Dictionary<string, object> ar)) { // Lookup data found in cache
							res.Add(DisplayValue(ar.Values.ToList()));
						} else {
							res.Add(val); // Not found, use original value
						}
					}
				} else {
					if (Lookup.Options.TryGetValue(val, out Dictionary<string, object> ar)) { // Lookup data found in cache
						res.Add(DisplayValue(ar.Values.ToList()));
					} else {
						res.Add(val); // Not found, use original value
					}
				}
				return res;
			}

			// Lookup options
			public List<Dictionary<string, object>> LookupOptions => Lookup?.Options?.Values.ToList() ?? new List<Dictionary<string, object>>();

			// Field option caption by option value
			public string OptionCaption(string val) {
				for (var i = 0; i < OptionCount; i++) {
					if (val == TagValue(i + 1)) {
						val = Empty(TagCaption(i + 1)) ? val : TagCaption(i + 1);
						break;
					}
				}
				return val;
			}

			// Get field user options as array
			public List<Dictionary<string, object>> Options(bool pleaseSelect = false) { // DN
				var list = new List<Dictionary<string, object>>();
				if (pleaseSelect) // Add "Please Select"
					list.Add(new Dictionary<string, object> { {"lf", ""}, {"df", Language.Phrase("PleaseSelect")} });
				for (var i = 0; i < OptionCount; i++) {
					var value = TagValue(i + 1);
					var caption = !Empty(TagCaption(i + 1)) ? TagCaption(i + 1) : value;
					list.Add(new Dictionary<string, object> { {"lf", value}, {"df", caption} });
				}
				return list;
			}

			// Href path
			private string _hrefPath = Config.UploadHrefPath; // Href path (for download)
			public string HrefPath {
				get => MapPath(false, !Empty(_hrefPath) ? _hrefPath : UploadPath);
				set => _hrefPath = value;
			}

			// Physical upload path
			public string PhysicalUploadPath => ServerMapPath(UploadPath);

			// Old Physical upload path
			public string OldPhysicalUploadPath => ServerMapPath(OldUploadPath);

			// Get select options HTML // DN
			public virtual IHtmlContent SelectOptionListHtml(string name = "") {
				var str = "";
				var isEmpty = true;
				var curValue = CurrentPage.RowType == Config.RowTypeSearch ? (name.StartsWith("y") ? AdvancedSearch.SearchValue2 : AdvancedSearch.SearchValue) : CurrentValue;
				string[] curValues = null;
				if (IsList(EditValue)) {
					var rows = (List<Dictionary<string, object>>)EditValue;
					if (SelectMultiple) {
						curValues = !Empty(curValue) ? Convert.ToString(curValue).Split(',') : new string[] {};
						foreach (var row in rows) {
							var list = row.Values.ToList();
							var selected = false;
							for (int i = 0; i < curValues.Length; i++) {
								if (SameString(list[0], curValues[i])) {
									curValues[i] = null; // Marked for removal
									selected = true;
									isEmpty = false;
									break;
								}
							}
							if (!selected)
								continue;
							for (var i = 1; i < list.Count; i++)
								list[i] = RemoveHtml(Convert.ToString(list[i]));
							str += "<option value=\"" + HtmlEncode(list[0]) + "\" selected>" + DisplayValue(list) + "</option>";
						}
					} else {
						if (UsePleaseSelect)
							str += "<option value=\"\">" + Language.Phrase("PleaseSelect") + "</option>";
						foreach (var row in rows) {
							var list = row.Values.ToList();
							if (SameString(curValue, list[0]))
								isEmpty = false;
							else
								continue;
							for (var i = 1; i < list.Count; i++)
								list[i] = RemoveHtml(Convert.ToString(list[i]));
							str += "<option value=\"" + HtmlEncode(list[0]) + "\" selected>" + DisplayValue(list) + "</option>";
						}
					}
					if (SelectMultiple) {
						for (var i = 0; i < curValues.Length; i++) {
							if (curValues[i] != null)
								str += "<option value=\"" + HtmlEncode(curValues[i]) + "\" selected>" + curValues[i] + "</option>";
						}
					} else {
						if (isEmpty && !Empty(curValue))
							str += "<option value=\"" + HtmlEncode(curValue) + "\" selected>" + curValue + "</option>";
					}
				}
				if (isEmpty)
					OldValue = "";
				return new HtmlString(str);
			}

			// Get radio buttons HTML // DN
			public virtual IHtmlContent RadioButtonListHtml(bool isDropdown, string name, int page = -1) {
				var isEmpty = true;
				var curValue = CurrentPage.RowType == Config.RowTypeSearch ? (name.StartsWith("y") ? AdvancedSearch.SearchValue2 : AdvancedSearch.SearchValue) : CurrentValue;
				var str = "";
				if (IsList(EditValue)) {
					var rows = (List<Dictionary<string, object>>)EditValue;
					for (var j = 0; j < rows.Count; j++) {
						var list = rows[j].Values.ToList();
						if (SameString(curValue, list[0]))
							isEmpty = false;
						else
							continue;
						var html = "<input type=\"radio\" data-table=\"" + TableVar + "\" data-field=\"" + FieldVar + "\"" +
							((page > -1) ? " data-page=\"" + page.ToString() + "\"" : "") +
							" name=\"" + name + "\" id=\"" + name + "_" + j.ToString() + "\"" +
							" data-value-separator=\"" + DisplayValueSeparatorAttribute + "\"" +
							" value=\"" + HtmlEncode(list[0]) + "\" checked" + EditAttributes + ">";
							html += "<label class=\"form-check-label\" for=\"" + name + "_" + j.ToString() + "\">" + DisplayValue(list) + "</label>";
							str += "<div class=\"form-check\">" + html + "</div>";
					}
					if (isEmpty && !Empty(curValue)) {
						var html = "<input type=\"radio\" data-table=\"" + TableVar + "\" data-field=\"" + FieldVar + "\"" +
							((page > -1) ? " data-page=\"" + page.ToString() + "\"" : "") +
							" name=\"" + name + "\" id=\"" + name + "_" + Convert.ToString(rows.Count) + "\"" +
							" data-value-separator=\"" + DisplayValueSeparatorAttribute + "\"" +
							" value=\"" + HtmlEncode(curValue) + "\" checked" + EditAttributes + ">";
						html += "<label class=\"form-check-label\" for=\"" + name + "_" + Convert.ToString(rows.Count) + "\">" + curValue + "</label>";
						str += "<div class=\"form-check\">" + html + "</div>";
					}
				}
				if (isEmpty)
					OldValue = "";
				return new HtmlString(str);
			}

			// Get checkboxes HTML // DN
			public virtual IHtmlContent CheckBoxListHtml(bool isDropdown, string name, int page = -1) {
				var isEmpty = true;
				var curValue = CurrentPage.RowType == Config.RowTypeSearch ? (name.StartsWith("y") ? AdvancedSearch.SearchValue2 : AdvancedSearch.SearchValue) : CurrentValue;
				var str = "";
				string[] curValues = null;
				if (IsList(EditValue)) {
					var rows = (List<Dictionary<string, object>>)EditValue;
					curValues = !Empty(curValue) ? Convert.ToString(curValue).Split(',') : new string[] {};
					for (var j = 0; j < rows.Count; j++) {
						var list = rows[j].Values.ToList();
						var selected = false;
						for (var i = 0; i < curValues.Length; i++) {
							if (SameString(list[0], curValues[i])) {
								curValues[i] = null; // Marked for removal
								selected = true;
								isEmpty = false;
								break;
							}
						}
						if (!selected)
							continue;
						var html = "<input type=\"checkbox\" class=\"form-check-input\" data-table=\"" + TableVar + "\" data-field=\"" + FieldVar + "\"" +
							((page > -1) ? " data-page=\"" + page.ToString() + "\"" : "") +
							" name=\"" + name + "\" id=\"" + name + "_" + j.ToString() + "\"" +
							" data-value-separator=\"" + DisplayValueSeparatorAttribute + "\"" +
							" value=\"" + HtmlEncode(list[0]) + "\" checked" + EditAttributes + ">";
						html += "<label class=\"form-check-label\" for=\"" + name + "_" + j.ToString() + "\">" + DisplayValue(list) + "</label>"; // Note: No spacing within the LABEL tag
						str += "<div class=\"form-check\">" + html + "</div>";
					}
					for (var i = 0; i < curValues.Length; i++) {
						if (curValues[i] != null) {
							var html = "<input type=\"checkbox\" class=\"form-check-input\" data-table=\"" + TableVar + "\" data-field=\"" + FieldVar + "\"" +
								((page > -1) ? " data-page=\"" + page.ToString() + "\"" : "") +
								" name=\"" + name + "\" id=\"" + name + "_" + i.ToString() + "\" value=\"" + HtmlEncode(curValues[i]) + "\" checked" +
								" data-value-separator=\"" + DisplayValueSeparatorAttribute + "\"" +
								EditAttributes + ">";
							html += "<label class=\"form-check-label\" for=\"" + name + "_" + i.ToString() + "\">" + curValues[i] + "</label>";
							str += "<div class=\"form-check\">" + html + "</div>";
						}
					}
				}
				if (isEmpty)
					OldValue = "";
				return new HtmlString(str);
			}

			/// <summary>
			/// Get display field value separator
			/// </summary>
			/// <param name="idx">Display field index (1|2|3)</param>
			/// <returns>The separator for the index</returns>

			protected string GetDisplayValueSeparator(int idx) {
				if (IsList(DisplayValueSeparator)) {
					List<string> sep = (List<string>)DisplayValueSeparator;
					return (idx < sep.Count) ? sep[idx - 1] : null;
				} else { // string
					string sep = Convert.ToString(DisplayValueSeparator);
					return (sep != "") ? sep : ", ";
				}
			}

			// Get display field value separator as attribute value
			public string DisplayValueSeparatorAttribute =>
				IsList(DisplayValueSeparator) ? ConvertToJson(DisplayValueSeparator) : Convert.ToString(DisplayValueSeparator);

			/// <summary>
			/// Get display value (for lookup field)
			/// </summary>
			/// <param name="list">List of object to be displayed</param>
			/// <returns>The display value of the lookup field</returns>

			public string DisplayValue(List<object> list) {
				var val = Convert.ToString(list[1]); // Display field 1
				for (var i = 2; i <= 4; i++) { // Display field 2 to 4
					var sep = GetDisplayValueSeparator(i - 1);
					if (sep == null || i >= list.Count) // No separator, break
						break;
					if (!Empty(list[i]))
						val += sep + Convert.ToString(list[i]);
				}
				return val;
			}

			// Reset attributes for field object
			public void ResetAttributes() {
				CssStyle = "";
				CssClass = "";
				CellCssStyle = "";
				CellCssClass = "";
				CellAttrs.Clear();
				EditAttrs.Clear();
				ViewAttrs.Clear();
				LinkAttrs.Clear();
			}

			// View Attributes
			public string ViewAttributes {
				get {
					var viewattrs = ViewAttrs;
					if (ViewTag == "IMAGE")
						viewattrs["alt"] = (ImageAlt.Trim() != "") ? ImageAlt.Trim() : ""; // IMG tag requires alt attribute
					string attrs = ""; // Custom attributes
					if (IsDictionary(ViewCustomAttributes)) { // Custom attributes as array
						var d = (Dictionary<string, string>)ViewCustomAttributes;
						foreach (var (key, value) in d) { // Duplicate attributes
							if (viewattrs.ContainsKey(key)) { // Duplicate attributes
								if (key == "style" || key.StartsWith("on")) // "style" and events
									viewattrs.Concat(key, value, ";");
								else // "class" and others
									viewattrs.Concat(key, value, " ");
							} else {
								viewattrs[key] = value;
							}
						}
					} else {
						attrs = Convert.ToString(ViewCustomAttributes);
					}
					string att = "", style = "", cls = CssClass;
					if (ViewTag == "IMAGE" && ImageWidth > 0 && (!ImageResize || ImageHeight <= 0))
						style += "width: " + ImageWidth + "px; ";
					if (ViewTag == "IMAGE" && ImageHeight > 0 && (!ImageResize || ImageWidth <= 0))
						style += "height: " + ImageHeight + "px; ";
					if (viewattrs.ContainsKey("style"))
						viewattrs.Concat("style", style + CssStyle.Trim(), ";");
					else
						if (!Empty(style + CssStyle)) viewattrs.Add("style", style + CssStyle.Trim());
					if (viewattrs.ContainsKey("class"))
						viewattrs.Concat("class", CssClass, " ");
					else
						if (!Empty(CssClass)) viewattrs.Add("class", CssClass);
					foreach (var (key, value) in viewattrs) {
						if (!Empty(key) && (!Empty(value) || IsBooleanAttribute(key))) { // Allow boolean attributes, e.g. "disabled"
							att += " " + key;
							if (!Empty(value))
								att += "=\"" + value.Trim() + "\"";
						} else if (key == "alt" && Empty(value)) { // Allow alt="" since it is a required attribute
							att += " alt=\"\"";
						}
					}
					if (!Empty(attrs)) // Custom attributes as string
						att += " " + attrs;
					return att;
				}
			}

			// Edit Attributes
			public string EditAttributes {
				get {
					string att = "";
					string style = CssStyle;
					string cls = CssClass;
					var editattrs = EditAttrs;
					string attrs = ""; // Custom attributes
					if (IsDictionary(EditCustomAttributes)) { // Custom attributes as array
						var d = (Dictionary<string, string>)EditCustomAttributes;
						foreach (var (key, value) in d) { // Duplicate attributes
							if (editattrs.ContainsKey(key)) { // Duplicate attributes
								if (key == "style" || key.StartsWith("on")) // "style" and events
									editattrs.Concat(key, value, ";");
								else // "class" and others
									editattrs.Concat(key, value, " ");
							} else {
								editattrs[key] = value;
							}
						}
					} else {
						attrs = Convert.ToString(EditCustomAttributes);
					}
					if (editattrs.ContainsKey("style"))
						editattrs.Concat("style", CssStyle, ";");
					else
						if (!Empty(CssStyle)) editattrs.Add("style", CssStyle);
					if (editattrs.ContainsKey("class"))
						editattrs.Concat("class", CssClass, " ");
					else
						if (!Empty(CssClass)) editattrs.Add("style", CssClass);
					if (Disabled)
						editattrs["disabled"] = "true";
					if (ReadOnly) {// For TEXT/PASSWORD/TEXTAREA only
						if ((new List<string> {"TEXT", "PASSWORD", "TEXTAREA"}).Contains(HtmlTag)) { // Elements support readonly
							editattrs["readonly"] = "true";
						} else { // Elements do not support readonly
							editattrs["disabled"] = "true";
							editattrs["data-readonly"] = "1";
							editattrs["class"] = AppendClass(editattrs["class"], "disabled");
						}
					}
					foreach (var (key, value) in editattrs) {
						if (!Empty(key) && !Empty(value) || IsBooleanAttribute(key)) { // Allow boolean attributes, e.g. "disabled"
							att += " " + key;
							if 	(!IsBooleanAttribute(key) && !Empty(value))
								att += "=\"" + value.Trim() + "\"";
						}
					}
					if (!Empty(attrs)) // Custom attributes as string
						att += " " + attrs;
					return att;
				}
			}

			// Link attributes
			public string LinkAttributes {
				get {
					string att = "";
					var linkattrs = LinkAttrs;
					string attrs = ""; // Custom attributes
					if (IsDictionary(LinkCustomAttributes)) { // Custom attributes as array
						var d = (Dictionary<string, string>)LinkCustomAttributes;
						foreach (var (key, value) in d) { // Duplicate attributes
							if (linkattrs.ContainsKey(key)) { // Duplicate attributes
								if (key == "style" || key.StartsWith("on")) // "style" and events
									linkattrs.Concat(key, value, ";");
								else // "class" and others
									linkattrs.Concat(key, value, " ");
							} else {
								linkattrs[key] = value;
							}
						}
					} else {
						attrs = Convert.ToString(LinkCustomAttributes);
					}
					string href = Convert.ToString(HrefValue).Trim();
					if (!Empty(href))
						linkattrs["href"] = href;
					foreach (var (key, value) in linkattrs) {
						if (!Empty(key) && (!Empty(value) || IsBooleanAttribute(key))) { // Allow boolean attributes, e.g. "disabled"
							att += " " + key;
							if (!Empty(value))
								att += "=\"" + value.Trim() + "\"";
						}
					}
					if (!Empty(attrs)) // Custom attributes as string
						att += " " + attrs;
					return att;
				}
			}

			// Header cell CSS class
			public string HeaderCellClass => AppendClass("ew-table-header-cell", HeaderCellCssClass);

			// Footer cell CSS class
			public string FooterCellClass => AppendClass("ew-table-footer-cell", FooterCellCssClass);

			// Add CSS class to all cells
			public void AddClass(string className) {
				CellCssClass = AppendClass(CellCssClass, className);
				HeaderCellCssClass = AppendClass(HeaderCellCssClass, className);
				FooterCellCssClass = AppendClass(FooterCellCssClass, className);
			}

			// Delete CSS class from all cells
			public void DeleteClass(string className) {
				CellCssClass = RemoveClass(CellCssClass, className);
				HeaderCellCssClass = RemoveClass(HeaderCellCssClass, className);
				FooterCellCssClass = RemoveClass(FooterCellCssClass, className);
			}

			// Cell Styles (Used in export)
			public string CellStyles {
				get {
					string att = "";
					string style = CellCssStyle;
					string cls = CellCssClass;
					if (CellAttrs.ContainsKey("style") && !Empty(CellAttrs["style"]))
						style += " " + CellAttrs["style"];
					if (CellAttrs.ContainsKey("class") && !Empty(CellAttrs["class"]))
						cls += " " + CellAttrs["class"];
					if (!Empty(style))
						att += " style=\"" + style.Trim() + "\"";
					if (!Empty(cls))
						att += " class=\"" + cls.Trim() + "\"";
					return att;
				}
			}

			// Cell Attributes
			public string CellAttributes {
				get {
					string att = "";
					var cellattrs = CellAttrs;
					string attrs = ""; // Custom attributes
					if (IsDictionary(CellCustomAttributes)) { // Custom attributes as array
						var d = (Dictionary<string, string>)CellCustomAttributes;
						foreach (var (key, value) in d) { // Duplicate attributes
							if (cellattrs.ContainsKey(key)) { // Duplicate attributes
								if (key == "style" || key.StartsWith("on")) // "style" and events
									cellattrs.Concat(key, value, ";");
								else // "class" and others
									cellattrs.Concat(key, value, " ");
							} else {
								cellattrs[key] = value;
							}
						}
					} else {
						attrs = Convert.ToString(CellCustomAttributes);
					}
					if (cellattrs.ContainsKey("style"))
						cellattrs.Concat("style", CellCssStyle, ";");
					else
						if (!Empty(CellCssStyle)) cellattrs.Add("style", CellCssStyle);
					if (cellattrs.ContainsKey("class"))
						cellattrs.Concat("class", CellCssClass, " ");
					else
						if (!Empty(CellCssClass)) cellattrs.Add("class", CellCssClass);
					foreach (var (key, value) in cellattrs) {
						if (!Empty(key)&& (!Empty(value)) || IsBooleanAttribute(key)) { // Allow boolean attributes, e.g. "disabled"
							att += " " + key;
							if (!Empty(value))
								att += "=\"" + value.Trim() + "\"";
						}
					}
					if (!Empty(attrs)) // Custom attributes as string
						att += " " + attrs;
					return att;
				}
			}

			// Sort Attributes
			public string Sort {
				get => Session.GetString(Config.ProjectName + "_" + TableVar + "_" + Config.TableSort + "_" + FieldVar);
				set {
					if (!SameText(Session[Config.ProjectName + "_" + TableVar + "_" + Config.TableSort + "_" + FieldVar], value))
						Session[Config.ProjectName + "_" + TableVar + "_" + Config.TableSort + "_" + FieldVar] = value;
				}
			}

			// Get View value
			public string GetViewValue() => (ViewValue is IHtmlValue v) ? v.ToHtml() : Convert.ToString(ViewValue);

			// Export original value
			public bool ExportOriginalValue = Config.ExportOriginalValue;

			// Export field image
			public bool ExportFieldImage = Config.ExportFieldImage;

			// Export Caption
			public string ExportCaption => Config.ExportFieldCaption ? Caption : Name;

			// Export Value
			public string ExportValue => ExportOriginalValue ? Convert.ToString(CurrentValue) : Convert.ToString(ViewValue);

			// Get temp image
			public async Task<string> GetTempImage() {
				if (IsBlob) {
					if (!IsDBNull(Upload.DbValue)) { // DN
						byte[] wrkdata = (byte[])Upload.DbValue;
						if (ImageResize) {
							int wrkwidth = ImageWidth;
							int wrkheight = ImageHeight;
							ResizeBinary(ref wrkdata, ref wrkwidth, ref wrkheight);
						}
						return TempImage(wrkdata);
					}
				} else {
					string wrkfile = Convert.ToString(Upload.DbValue);
					if (Empty(wrkfile))
						wrkfile = Convert.ToString(CurrentValue);
					if (!Empty(wrkfile)) {
						if (!UploadMultiple) {
							string imagefn = PhysicalUploadPath + wrkfile;
							if (FileExists(imagefn)) {
								if (ImageResize) {
									int wrkwidth = ImageWidth;
									int wrkheight = ImageHeight;
									byte[] wrkdata = ResizeFileToBinary(imagefn, ref wrkwidth, ref wrkheight);
									return TempImage(wrkdata);
								} else {
									if (IsRemote(imagefn))
										return TempImage(await FileReadAllBytes(imagefn));
									else
										return wrkfile;
								}
							}
						 } else {
							var tmpfiles = wrkfile.Split(Config.MultipleUploadSeparator);
							var tmpimage = "";
							foreach (var tmpfile in tmpfiles) {
								if (!Empty(tmpfile)) {
									string imagefn = PhysicalUploadPath + tmpfile;
									if (!FileExists(imagefn))
										continue;
									if (ImageResize) {
										int wrkwidth = ImageWidth;
										int wrkheight = ImageHeight;
										byte[] wrkdata = ResizeFileToBinary(imagefn, ref wrkwidth, ref wrkheight);
										if (!Empty(tmpimage))
											tmpimage += ",";
										tmpimage += TempImage(wrkdata);
									} else {
										if (!Empty(tmpimage))
											tmpimage += ",";
										if (IsRemote(imagefn))
											tmpimage += TempImage(await FileReadAllBytes(imagefn));
										else
											tmpimage += tmpfile;
									}
								}
							}
							return tmpimage;
						}
					}
				}
				return "";
			}

			// Set form value
			public void SetFormValue(string value, bool current = true) {
				if (Config.RemoveXss && DataType != Config.DataTypeXml)
					value = RemoveXss(value);
				if (!Empty(value) && DataType == Config.DataTypeNumber && !IsNumeric(value)) // Check data type
					_formValue = null;
				else
					_formValue = value;
				if (current)
					CurrentValue = _formValue;
			}

			// Form value
			public string FormValue {
				get => _formValue;
				set => SetFormValue(value);
			}

			// Set query value
			public void SetQueryValue(string value, bool current = true) {
				if (Config.RemoveXss)
					value = RemoveXss(value);
				if (!Empty(value) && DataType == Config.DataTypeNumber && !IsNumeric(value)) // Check data type
					_queryValue = null;
				else
					_queryValue = value;
				if (current)
					CurrentValue = _queryValue;
			}

			// Query value
			public string QueryValue {
				get => _queryValue;
				set => SetQueryValue(value);
			}

			// Set DB value
			public void SetDbValue(object value = null, bool current = true) {
				_dbValue = value;
				if (UseEncryption && !Empty(value))
					value = AesDecrypt(Convert.ToString(value), Config.EncryptionKey);
				if (current)
					CurrentValue = value;
			}

			// DB value
			public object DbValue {
				get => _dbValue;
				set => _dbValue = value;
			}

			// Session Value
			public object SessionValue {
				get => Session.GetValue(Config.ProjectName + "_" + TableVar + "_" + FieldVar + "_SessionValue"); // DN
				set => Session.SetValue(Config.ProjectName + "_" + TableVar + "_" + FieldVar + "_SessionValue", value); // DN
			}

			// Reverse sort
			public string ReverseSort() => (Sort == "ASC") ? "DESC" : "ASC";

			// Set database value
			public void SetDbValue(Dictionary<string, object> rs, object value, object def, bool skip)
			{
				bool skipUpdate = (skip || !Visible || Disabled);
				if (skipUpdate) {
					rs.Remove(Name);
					return;
				}
				OldValue = _dbValue; // Save old DbValue in OldValue
				switch (Type) {
					case 2:
					case 3:
					case 16:
					case 17:
					case 18:
					case 19:
					case 20:
					case 21:

						// Int
						_dbValue = IsNumeric(value) ? ConvertType(value, Type) : def;
						break;
					case 5:
					case 6:
					case 14:
					case 131:
					case 139:

						// Double
						value = ConvertToFloatString(value);
						_dbValue = IsNumeric(value) ? ConvertType(value, Type) : def;
						break;
					case 4:

						// Single
						value = ConvertToFloatString(value);
						_dbValue = IsNumeric(value) ? ConvertType(value, Type) : def;
						break;
					case 7:
					case 133:
					case 135:

						// Date
						_dbValue = IsDate(value) ? Convert.ToDateTime(value) : def;
						break;
					case 134:
					case 145:

						// Time
						_dbValue = TimeSpan.TryParse(Convert.ToString(value), out TimeSpan ts) ? ts : def;
						break;
					case 146:

						// DateTimeOffset
						_dbValue = DateTimeOffset.TryParse(Convert.ToString(value), out DateTimeOffset dt) ? dt : def;
						break;
					case 201:
					case 203:
					case 129:
					case 130:
					case 200:
					case 202:

						// String
						_dbValue = Config.RemoveXss ? RemoveXss(Convert.ToString(value)) : Convert.ToString(value);
						_dbValue = (Convert.ToString(_dbValue) == "") ? def : (UseEncryption ? AesEncrypt(Convert.ToString(_dbValue), Config.EncryptionKey) : _dbValue);
						break;
					case 141:

						// XML
						_dbValue = !Empty(value) ? value : def;
						break;
					case 128:
					case 204:
					case 205:

						// Binary
						_dbValue = IsDBNull(value) ? def : value;
						break;
					case 72:

						// GUID
						_dbValue = (!Empty(value) && CheckGuid(Convert.ToString(value).Trim())) ? value : def;
						break;
					default:
						_dbValue = value;
						break;
				}
				rs[Name] = _dbValue;
			}
		}

		/// <summary>
		/// IHtmlValue interface (Value that can be converted to HTML and string)
		/// </summary>

		public interface IHtmlValue
		{
			string ToHtml();
		}

		/// <summary>
		/// Class OptionValues
		/// </summary>

		public class OptionValues : IHtmlValue
		{
			public List<string> Values = new List<string>();

			// Constructor
			public OptionValues(List<string> list = null) {
				if (list != null)
					Values = list;
			}

			// Add value
			public void Add(string value) => Values.Add(value);

			// Convert to HTML
			public string ToHtml() => OptionsHtml(Values) ?? ToString();

			// Convert to string (MUST return a string value)
			public override string ToString() => String.Join(Config.OptionSeparator, Values);
		}

		/// <summary>
		/// JsonResult with a boolean Result property
		/// </summary>

		public class JsonBoolResult : JsonResult
		{
			public bool Result;
			public static JsonBoolResult FalseResult = new JsonBoolResult(new { success = false, version = Config.ProductVersion }, false);

			// Constructor
			public JsonBoolResult(object value, bool result) : base(value) => Result = result;

			// Implicit operator
			public static implicit operator bool(JsonBoolResult me) => me.Result;
		}

		/// <summary>
		/// Lookup class
		/// </summary>

		public class Lookup<T>
			where T : DbField
		{
			public string LookupType = "";
			public Dictionary<string, Dictionary<string, object>> Options = new Dictionary<string, Dictionary<string, object>>();
			public string Template = "";
			public string CurrentFilter = "";
			public string UserSelect = "";
			public string UserFilter = "";
			public string UserOrderBy = "";
			public List<string> FilterValues = new List<string>();
			public string SearchValue = "";
			public int PageSize = -1;
			public int Offset = -1;
			public string ModalLookupSearchType = "";
			public bool KeepCrLf = false;
			public Func<string> LookupFilter;
			public string Name = "";
			public string LinkTable = "";
			public bool Distinct = false;
			public string LinkField = "";
			public List<string> DisplayFields = new List<string>();
			public List<string> ParentFields = new List<string>();
			public List<string> ChildFields = new List<string>();
			public Dictionary<string, string> FilterFields = new Dictionary<string, string>();
			public List<string> FilterFieldVars = new List<string>();
			public List<string> AutoFillSourceFields = new List<string>();
			public List<string> AutoFillTargetFields = new List<string>();
			public dynamic Table;
			public bool FormatAutoFill = false;
			public bool UseParentFilter = false;
			public bool SelectOffset = true; // DN // Handle SelectOffset
			public bool UseHtmlDecode = true;

			/// <summary>
			/// Constructor for the Lookup class
			/// </summary>
			/// <param name="name">name</param>
			/// <param name="linkTable">Link table</param>
			/// <param name="distinct">Distinct</param>
			/// <param name="linkField">Link field</param>
			/// <param name="displayFields">Display fields</param>
			/// <param name="parentFields">Parent fields</param>
			/// <param name="childFields">Child fields</param>
			/// <param name="filterFields">Filter fields</param>
			/// <param name="filterFieldVars">Filter field variables</param>
			/// <param name="autoFillSourceFields">AutoFill source fields</param>
			/// <param name="autoFillTargetFields">AutoFill target fields</param>
			/// <param name="orderBy">Order By clause</param>
			/// <param name="template">Option template</param>

			public Lookup(string name, string linkTable, bool distinct, string linkField, List<string> displayFields = null, List<string> parentFields = null,
				List<string> childFields = null, List<string> filterFields = null, List<string> filterFieldVars = null, List<string> autoFillSourceFields = null, List<string> autoFillTargetFields = null, string orderBy = "", string template = "")
			{
				Name = name;
				LinkTable = linkTable;
				Distinct = distinct;
				LinkField = linkField;
				DisplayFields = displayFields;
				ParentFields = parentFields;
				ChildFields = childFields;
				if (filterFields != null)
					FilterFields = filterFields.ToDictionary(f => f, f => "="); // Default filter operator
				FilterFieldVars = filterFieldVars;
				AutoFillSourceFields = autoFillSourceFields;
				AutoFillTargetFields = autoFillTargetFields;
				UserOrderBy = orderBy;
				Template = template;
			}

			/// <summary>
			/// Get lookup SQL based on current filter/lookup filter, call Lookup_Selecting if necessary
			/// </summary>
			/// <param name="useParentFilter">Use parent filter</param>
			/// <param name="currentFilter">Current filter</param>
			/// <param name="lookupFilter">Lookup filter</param>
			/// <param name="tbl">Table object</param>
			/// <returns>Lookup SQL</returns>

			public string GetSql(bool useParentFilter = true, string currentFilter = "", Func<string> lookupFilter = null, dynamic tbl = null)
			{
				UseParentFilter = useParentFilter; // Save last call
				CurrentFilter = currentFilter;
				LookupFilter = lookupFilter; // Save last call
				if (!Empty(tbl)) {
					SetUserSql(useParentFilter);
					if (tbl.Fields.TryGetValue(Name, out T fld))
						tbl.Lookup_Selecting(fld, ref UserFilter); // Call Lookup Selecting
					ResetUserSql(useParentFilter);
				}
				if (lookupFilter != null) // Add lookup filter as part of user filter
					AddFilter(ref UserFilter, lookupFilter()); // DN
				return GetSqlPart("", true, useParentFilter);
			}

			/// <summary>
			/// Set options
			/// </summary>
			/// <param name="options">
			/// Options, e.g. new Dictionary&lt;string, List&lt;object&gt;&gt; {
			/// 	{"lv1", new List&lt;object&gt; {"lv1", "dv", "dv2", "dv3", "dv4"}},
			/// 	{"lv2", new List&lt;object&gt; {"lv2", "dv", "dv2", "dv3", "dv4"}}
			/// }
			/// </param>
			/// <returns>Whether the options are setup successfully</returns>

			public bool SetOptions(Dictionary<string, List<object>> options)
			{
				var keys = new List<string> { "lf", "df", "df2", "df3", "df4", "ff", "ff2", "ff3", "ff4" };
				Options = options.Where(kvp => kvp.Value.Count() >= 2)
					.ToDictionary(kvp => kvp.Key, kvp => keys.Zip(kvp.Value, (key, values) => (key, values)).ToDictionary(t => t.Item1, t => t.Item2));
				return true;
			}

			/// <summary>
			/// Set options
			/// </summary>
			/// <param name="options">
			/// Options. List of dictionary returned by ExecuteRows()
			/// </param>
			/// <returns>Whether the options are setup successfully</returns>

			public bool SetOptions(List<Dictionary<string, object>> options) =>
				SetOptions(options.ToDictionary(d => d.Values.First().ToString(), d => d.Values.ToList()));

			/// <summary>
			/// Set options
			/// </summary>
			/// <param name="options">
			/// Options, e.g. new List&lt;List&lt;object&gt;&gt; {
			/// 	new List&lt;object&gt; {"1", "option1", "df1", "df2", "df3" },
			/// 	new List&lt;object&gt; {"2", "option2", "df1", "df2" },
			/// 	new List&lt;object&gt; {"3", "option3", "df1", "df3", "df3a" },
			/// }
			/// </param>
			/// <returns>Whether the options are setup successfully</returns>

			public bool SetOptions(List<List<object>> options) => SetOptions(options.ToDictionary(list => Convert.ToString(list[0]), list => list));

			/// <summary>
			/// Set options
			/// </summary>
			/// <param name="options">
			/// Options, e.g. new List&lt;object[]&gt; {
			/// 	new object[] {"1", "option1", "df1", "df2", "df3" },
			/// 	new object[] {"2", "option2", "df1", "df2" },
			/// 	new object[] {"3", "option3", "df1", "df3", "df3a" },
			/// }
			/// </param>
			/// <returns>Whether the options are setup successfully</returns>

			public bool SetOptions(List<object[]> options) => SetOptions(options.ToDictionary(list => Convert.ToString(list[0]), list => list.ToList()));

			/// <summary>
			/// Set filter field operator
			/// </summary>
			/// <param name="name">Filter field name</param>
			/// <param name="opr">Filter search operator</param>

			public void SetFilterOperator(string name, string opr)
			{
				if (FilterFields.ContainsKey(name) && IsValidOperator(opr))
					FilterFields[name] = opr;
			}

			/// <summary>
			/// Get user parameters as hidden tag
			/// </summary>
			/// <param name="name">variable name</param>
			/// <returns>The hidden tag HTML markup</returns>

			public string GetParamTag(string name)
			{
				var page = CurrentGrid ?? CurrentPage;
				string sql = GetSql(UseParentFilter, CurrentFilter, LookupFilter, page); // Call Lookup_Selecting again based on last setting
				var ar = new Dictionary<string, string>();
				if (!Empty(UserSelect))
					ar.Add("s", Encrypt(UserSelect));
				if (!Empty(UserFilter))
					ar.Add("f", Encrypt(UserFilter));
				if (!Empty(UserOrderBy))
					ar.Add("o", Encrypt(UserOrderBy));
				if (ar.Count > 0)
					return "<input type=\"hidden\" id=\"" + name + "\" name=\"" + name + "\" value=\"" + String.Join("&", ar.Select(kvp => kvp.Key + "=" + kvp.Value)) + "\">";
				return "";
			}

			/// <summary>
			/// Output properties as JSON
			/// </summary>
			/// <returns>JSON string</returns>

			public string ToClientList() => ConvertToJson(new Dictionary<string, object> {
				{"linkTable", LinkTable},
				{"linkField", LinkField},
				{"ajax", !Empty(LinkTable)},
				{"autoFill", AutoFillSourceFields.Count > 0},
				{"distinct", Distinct},
				{"displayFields", DisplayFields},
				{"parentFields", ParentFields},
				{"childFields", ChildFields},
				{"filterFields", FilterFields.Keys},
				{"filterFieldVars", FilterFieldVars},
				{"filterOperators", FilterFields.Values},
				{"autoFillSourceFields", AutoFillSourceFields},
				{"autoFillTargetFields", AutoFillTargetFields},
				{"options", new Dictionary<string, object>()},
				{"template", Template}
			});

			/// <summary>
			/// Execute SQL and output JSON response
			/// </summary>
			/// <returns>The lookup result as JSON</returns>

			public virtual async Task<JsonBoolResult> ToJson()
			{
				var tbl = GetTable();
				if (Empty(tbl))
					return JsonBoolResult.FalseResult;
				string sql = GetSql();
				string orderBy = UserOrderBy;
				int pageSize = PageSize;
				int offset = Offset;
				var rs = await GetRecordset(sql, orderBy, pageSize, offset);
				if (!IsList(rs))
					return JsonBoolResult.FalseResult;
				int totalCnt;
				if (pageSize > 0) {
					totalCnt = await tbl.TryGetRecordCount(sql);
				} else {
					totalCnt = rs?.Count ?? 0;
				}

				// Skip offset for MSSQL 2008 // DN
				if (offset > 0 && !SelectOffset)
					rs.RemoveRange(0, offset);

				// Output
				foreach (var row in rs) {
					var keys = row.Keys.ToList();
					T field;
					string key = keys[0];
					if (tbl.Fields.TryGetValue(LinkField, out field))
						field.SetDbValue(row[key]);
					for (var i = 1; i < row.Count; i++) {
						key = keys[i];
						string str = Convert.ToString(row[key]);
						if (!KeepCrLf)
							str = str.Replace("\r\n", " ").Replace("\r", " ").Replace("\n", " ").Replace("\t", " ");
						row[key] = str;
						if (SameText(LookupType, "autofill")) {
							if (tbl.Fields.TryGetValue(AutoFillSourceFields[i - 1], out field))
								field.SetDbValue(row[key]);
						} else {
							if (tbl.Fields.TryGetValue(DisplayFields[i - 1], out field))
								field.SetDbValue(row[key]);
						}
					}
					if (SameText(LookupType, "autofill")) {
						if (FormatAutoFill) { // Format auto fill
							tbl.RowType = Config.RowTypeEdit;
							await tbl.RenderEditRow();
							for (int i = 1; i < row.Count; i++) {
								key = keys[i];
								if (tbl.Fields.TryGetValue(AutoFillSourceFields[i - 1], out field))
									row[key] = field.AutoFillOriginalValue ? field.CurrentValue : ((IsList(field.EditValue) || field.EditValue == null) ? field.CurrentValue : field.EditValue);
							}
						}
					} else if (LookupType != "unknown") { // Format display fields for known lookup type
						tbl.RowType = Config.RowTypeView;
						await tbl.RenderListRow();
						for (int i = 1; i < row.Count; i++) {
							key = keys[i];
							if (tbl.Fields.TryGetValue(DisplayFields[i - 1], out field)) {
								string suffix = (i > 1) ? i.ToString() : "";
								if (field.Lookup != null || field.HtmlTag == "FILE")
									row[key] = field.CurrentValue;
								else
									row[key] = field.ViewValue;
								row["df" + suffix] = row[key];
							}
						}
					}
				}
				var result = new Dictionary<string, object> { {"result", "OK"}, {"records", rs}, {"totalRecordCount", totalCnt}, {"version", Config.ProductVersion} };
				if (Config.RemoveXss && UseHtmlDecode)
					result["records"] = rs.Select(row => row.ToDictionary(kvp => kvp.Key, kvp => HtmlDecode(kvp.Value)));
				if (Config.Debug)
					result["sql"] = sql;
				return new JsonBoolResult(result, true);
			}

			/// <summary>
			/// Check if filter operator is valid
			/// </summary>
			/// <param name="opr">Operator, e.g. '&lt;', '&gt;'</param>
			/// <returns>Whether the operator is valid</returns>

			protected bool IsValidOperator(string opr) => (new List<string> { "=", "<>", "<", "<=", ">", ">=" }).Contains(opr);

			/// <summary>
			/// Return a part of the lookup SQL
			/// </summary>
			/// <param name="part">The part of the SQL: "select", "where", "orderby", or "" (the complete SQL)</param>
			/// <param name="isUser">Whether the UserSelect, CurrentFilter and UserFilter should be used</param>
			/// <param name="useParentFilter">Whether the ParentFilter should be used</param>
			/// <returns>The lookup SQL</returns>

			protected string GetSqlPart(string part = "", bool isUser = true, bool useParentFilter = true)
			{
				var tbl = GetTable();
				if (tbl == null)
					return "";

				// Set up SELECT ... FROM ...
				string dbid = tbl.DbId;
				string select = "SELECT";
				if (Distinct)
					select += " DISTINCT";

				// Set up link field
				if (!tbl.Fields.TryGetValue(LinkField, out T linkField))
					return "";
				select += " " + linkField.Expression;
				if (LookupType != "unknown") // Known lookup types
					select += " AS " + QuotedName("lf", dbid);

				// Set up lookup fields
				int i;
				var lookupCnt = 0;
				if (SameText(LookupType, "autofill")) {
					for (i = 0; i < AutoFillSourceFields.Count; i++) {
						var autoFillSourceField = AutoFillSourceFields[i];
						if (!tbl.Fields.TryGetValue(autoFillSourceField, out T af)) {
							select += ", '' AS " + QuotedName("af" + i, dbid);
						} else {
							select += ", " + af.Expression + " AS " + QuotedName("af" + i, dbid);
							if (!af.AutoFillOriginalValue) {
								FormatAutoFill = true;
							}
						}
						lookupCnt++;
					}
				} else {
					for (i = 0; i < DisplayFields.Count; i++) {
						var displayField = DisplayFields[i];
						if (!tbl.Fields.TryGetValue(displayField, out T df)) {
							select += ", '' AS " + QuotedName("df" + ((i == 0) ? "" : Convert.ToString(i + 1)), dbid);
						} else {
							select += ", " + "LTrim(Rtrim(" + df.Expression + "))"; // Wilson 2019 06 18 df.Expression; 
							if (LookupType != "unknown") // Known lookup types
								select += " AS " + QuotedName("df" + ((i == 0) ? "" : Convert.ToString(i + 1)), dbid);
						}
						lookupCnt++;
					}
					if (!useParentFilter) {
						i = 0;
						foreach (var (key, value) in FilterFields) {
							if (!tbl.Fields.TryGetValue(key, out T filterField)) {
								select += ", '' AS " + QuotedName("ff" + ((i == 0) ? "" : Convert.ToString(i + 1)), dbid);
							} else {
								var filterOpr = value;
								select += ", " + filterField.Expression;
								if (LookupType != "unknown") // Known lookup types
									select += " AS " + QuotedName("ff" + ((i == 0) ? "" : Convert.ToString(i + 1)), dbid);
							}
							i++;
							lookupCnt++;
						}
					}
				}
				if (lookupCnt == 0)
					return "";
				select += " FROM " + tbl.SqlFrom;

				// User SELECT
				if (UserSelect != "" && isUser)
					select = UserSelect;

				// Set up WHERE
				string where = Convert.ToString(tbl.Invoke("ApplyUserIDFilters", new object[] { "" }));

				// Set up current filter
				int cnt = FilterValues.Count;
				if (cnt > 0) {
					var val = FilterValues[0];
					if (!Empty(val))
						AddFilter(ref where, GetFilter(linkField, "=", val, tbl.DbId));
				}

				// Set up parent filters
				if (useParentFilter) {
					i = 1;
					foreach (var (key, value) in FilterFields) {
						if (!Empty(key)) {
							var filterOpr = value;
							if (!tbl.Fields.TryGetValue(key, out T filterField))
								return "";
							if (cnt <= i) {
								AddFilter(ref where, "1=0"); // Disallow
							} else {
								string val = Convert.ToString(FilterValues[i]);
								AddFilter(ref where, GetFilter(filterField, filterOpr, val, tbl.DbId));
							}
						}
						i++;
					}
				}

				// Set up search
				if (!Empty(SearchValue)) {

					// Set up modal lookup search
					if (SameText(LookupType, "modal")) {
						var flds = DisplayFields.Where(fieldName => !Empty(fieldName)).Select(fieldName => ((T)tbl.Fields[fieldName]).Expression).ToList();
						AddFilter(ref where, GetModalSearchFilter(SearchValue, flds));
					} else if (SameText(LookupType, "autosuggest")) {
						if (Config.AutoSuggestForAllFields) {
							var filters = DisplayFields.Where(fieldName => !Empty(fieldName)).Select(fieldName => GetAutoSuggestFilter(SearchValue, tbl.Fields[fieldName]));
							AddFilter(ref where, String.Join(" OR ", filters));
						} else {
							var displayField = tbl.Fields[DisplayFields[0]];
							AddFilter(ref where, GetAutoSuggestFilter(SearchValue, displayField));
						}
					}
				}

				// Add filters
				if (!Empty(CurrentFilter) && isUser)
					AddFilter(ref where, CurrentFilter);

				// User Filter
				if (!Empty(UserFilter) && isUser)
					AddFilter(ref where, UserFilter);

				// Set up ORDER BY
				string orderBy = UserOrderBy;

				// Return SQL part
				if (part == "select") {
					return select;
				} else if (part == "where") {
					return where;
				} else if (part == "orderby") {
					return orderBy;
				} else {
					string sql = select;
					string dbType = GetConnectionType(tbl.DbId);
					if (!Empty(where))
						sql += " WHERE " + where;
					if (!Empty(orderBy))
						if (dbType == "MSSQL")
							sql += " /*BeginOrderBy*/ORDER BY " + orderBy + "/*EndOrderBy*/";
						else
							sql += " ORDER BY " + orderBy;
					return sql;
				}
			}

			/// <summary>
			/// Get filter
			/// </summary>
			/// <param name="fld">Search field object</param>
			/// <param name="opr">Search operator</param>
			/// <param name="val">Search value</param>
			/// <param name="dbid">Database ID</param>
			/// <returns>Search filter (SQL "where" part)</returns>

			protected string GetFilter(T fld, string opr, string val, string dbid)
			{
				bool validValue = !Empty(val);
				string[] values = val.Split(Config.LookupFilterValueSeparator);
				if (fld.DataType == Config.DataTypeNumber) // Validate numeric fields
					validValue = values.All(value => IsNumeric(value));
				if (validValue) { // Allow
					if (opr == "=") { // Use the IN operator
						var result = values.Select(value => QuotedValue(value, fld.DataType, dbid));
						return fld.Expression + " IN (" + String.Join(", ", result) + ")";
					} else { // Custom operator
						return values.Aggregate("", (where, value) => AddFilter(where, fld.Expression + opr + QuotedValue(value, fld.DataType, dbid)));
					}
				} else {
					return "1=0"; // Disallow
				}
			}

			/// <summary>
			/// Set up UserSelect/UserFilter
			/// </summary>
			/// <param name="useParentFilter">Whether the ParentFilter should be used</param>

			protected void SetUserSql(bool useParentFilter = false)
			{
				UserSelect = GetSqlPart("select", false, useParentFilter);
				UserFilter = GetSqlPart("where", false, useParentFilter);
			}

			/// <summary>
			/// Reset UserSelect/UserFilter if not changed
			/// </summary>
			/// <param name="useParentFilter">Whether the ParentFilter should be used</param>

			protected void ResetUserSql(bool useParentFilter = false)
			{
				if (UserSelect == GetSqlPart("select", false, useParentFilter))
					UserSelect = "";
				if (UserFilter == GetSqlPart("where", false, useParentFilter))
					UserFilter = "";
			}

			/// <summary>
			/// Get table object
			/// </summary>
			/// <returns>Instance of table object</returns>

			protected virtual dynamic GetTable()
			{
				if (Empty(LinkTable))
					return null;
				if (Empty(Table))
					Table = CreateTable(LinkTable);
				return Table;
			}

			/// <summary>
			/// Get recordset
			/// </summary>
			/// <param name="sql">SQL to execute</param>
			/// <param name="orderBy">ORDER BY clause</param>
			/// <param name="pageSize">Page size</param>
			/// <param name="offset">Offset</param>
			/// <returns>List of records as dictionary</returns>

			protected async Task<List<Dictionary<string, object>>> GetRecordset(string sql, string orderBy, int pageSize, int offset)
			{
				var tbl = GetTable();
				if (Empty(tbl))
					return null;
				using (var conn = await _GetConnectionAsync(tbl.DbId)) {
					if (pageSize > 0) {
						var dbType = GetConnectionType(tbl.DbId);
						SelectOffset = conn.SelectOffset; // Check if SelectOffset is used // DN
						if (SameString(dbType, "MSSQL"))
							return await conn.GetRowsAsync(await conn.SelectLimit(sql, pageSize, offset, !Empty(orderBy)));
						else
							return await conn.GetRowsAsync(await conn.SelectLimit(sql, pageSize, offset));
					} else {
						return await conn.GetRowsAsync(await conn.GetDataReaderAsync(sql));
					}
				}
			}

			/// <summary>
			/// Get Auto-Suggest filter
			/// </summary>
			/// <param name="sv">Search value</param>
			/// <param name="fld">Field object</param>
			/// <returns>The WHERE clause</returns>

			protected string GetAutoSuggestFilter(string sv, T fld)
			{
				if (Config.AutoSuggestForAllFields)
					return GetCastFieldForLike(fld) + Like(QuotedValue("%" + sv + "%", Config.DataTypeString, Table.DbId));
				else
					return GetCastFieldForLike(fld) + Like(QuotedValue(sv + "%", Config.DataTypeString, Table.DbId));
			}

			/// <summary>
			/// Get CAST SQL for LIKE operator
			/// </summary>
			/// <param name="fld">Field object</param>
			/// <returns>The LIKE part of SQL</returns>

			protected string GetCastFieldForLike(T fld)
			{
				string expr = fld.Expression;
				if (fld.DataType == Config.DataTypeDate || fld.DataType == Config.DataTypeTime) // Date/Time field
					return CastDateFieldForLike(expr, fld.DateTimeFormat, Table.DbId);
				var dbType = GetConnectionType(Table.DbId);
				if (SameString(dbType, "MSSQL") && fld.DataType == Config.DataTypeNumber)
					return "CAST(" + expr + " AS NVARCHAR)";
				if (SameString(dbType, "POSTGRESQL") && fld.DataType != Config.DataTypeString)
					return "CAST(" + expr + " AS varchar(255))";
				return expr;
			}

			/// <summary>
			/// Get modal search filter
			/// </summary>
			/// <param name="sv">Search value</param>
			/// <param name="flds">Field expressions</param>
			/// <returns>Filter part of SQL for modal search</returns>

			protected string GetModalSearchFilter(string sv, List<string> flds)
			{
				if (EmptyString(sv))
					return "";
				string searchStr = "";
				string search = sv.Trim();
				string searchType = ModalLookupSearchType;
				if (searchType != "=") {
					var ar = new List<string>();

					// Match quoted keywords (i.e.: "...")
					var matches = Regex.Matches(search, @"""([^""]*)""", RegexOptions.IgnoreCase);
					if (matches.Count > 0) {
						foreach (Match match in matches) {
							int p = search.IndexOf(match.Groups[0].Value);
							string str = search.Substring(0, p);
							search = search.Substring(p + match.Groups[0].Length);
							if (str.Trim().Length > 0)
								ar.AddRange(str.Trim().Split(' '));
							ar.Add(match.Groups[1].Value); // Save quoted keyword
						}
					}

					// Match individual keywords
					if (search.Trim().Length > 0)
						ar.AddRange(search.Trim().Split(' '));

					// Search keyword in any fields
					if (searchType == "OR" || searchType == "AND") {
						string searchFilter = "";
						foreach (var keyword in ar) {
							if (keyword != "") {
								searchFilter = GetModalSearchSql(new List<string> { keyword }, flds);
								if (searchFilter != "") {
									if (searchStr != "")
										searchStr += " " + searchType + " ";
									searchStr += "(" + searchFilter + ")";
								}
							}
						}
					} else {
						searchStr = GetModalSearchSql(ar, flds);
					}
				} else {
					searchStr = GetModalSearchSql(new List<string> { search }, flds);
				}
				return searchStr;
			}

			/// <summary>
			/// Get modal search SQL
			/// </summary>
			/// <param name="keywords">Keywords</param>
			/// <param name="flds">Field expressions</param>
			/// <returns>WHERE part of SQL</returns>

			protected string GetModalSearchSql(List<string> keywords, List<string> flds) =>
				flds.Where(fldExpr => !Empty(fldExpr)).Aggregate("", (where, fldExpr) => BuildModalSearchSql(where, fldExpr, keywords));

			/// <summary>
			/// Build and set up modal search SQL
			/// </summary>
			/// <param name="where">WHERE clause</param>
			/// <param name="fldExpr">Field expressions</param>
			/// <param name="keywords">Keywords</param>

			protected string BuildModalSearchSql(string where, string fldExpr, List<string> keywords) // DN
			{
				var searchType = ModalLookupSearchType;
				string defCond = (searchType == "OR") ? "OR" : "AND";
				var arSql = new List<string>(); // List<string> for SQL parts
				var arCond = new List<string>(); // List<string> for search conditions
				int j = 0; // Number of SQL parts
				foreach (string kw in keywords) {
					string result = kw.Trim();
					string[] arwrk;
					if (!Empty(Config.BasicSearchIgnorePattern)) {
						result = Regex.Replace(result, Config.BasicSearchIgnorePattern, "\\", RegexOptions.IgnoreCase);
						arwrk = result.Split('\\');
					} else {
						arwrk = new string[] { result };
					}
					foreach (string keyword in arwrk) {
						if (!Empty(keyword)) {
							string wrk = "";
							if (keyword == "OR" && searchType == "") {
								if (j > 0)
									arCond[j-1] = "OR";
							} else {
								wrk = fldExpr + Like(QuotedValue("%" + keyword + "%", Config.DataTypeString, Table.DbId), Table.DbId);
							}
							if (!Empty(wrk)) {
								arSql.Add(wrk);
								arCond.Add(defCond);
								j += 1;
							}
						}
					}
				}
				int cnt = arSql.Count;
				bool quoted = false;
				string sql = "";
				if (cnt > 0) {
					for (int i = 0; i < cnt-1; i++) {
						if (arCond[i] == "OR") {
							if (!quoted)
								sql += "(";
							quoted = true;
						}
						sql += arSql[i];
						if (quoted && arCond[i] != "OR") {
							sql += ")";
							quoted = false;
						}
						sql += " " + arCond[i] + " ";
					}
					sql += arSql[cnt-1];
					if (quoted)
						sql += ")";
				}
				if (!Empty(sql)) {
					if (!Empty(where))
						where += " OR ";
					where += "(" + sql + ")";
				}
				return where;
			}
		}

		/// <summary>
		/// List Options class
		/// </summary>

		public class ListOptions
		{
			public List<ListOption> Items = new List<ListOption>();
			public string CustomItem = "";
			public string Tag = "td";
			public string TagClassName = "";
			public string TableVar = "";
			public string RowCnt = "";
			public string ScriptType = "block";
			public string ScriptId = "";
			public string ScriptClassName = "";
			public string JavaScript = "";
			public int RowSpan = 1;
			public bool UseDropDownButton = false;
			public bool UseButtonGroup = false;
			public string ButtonClass = "";
			public string GroupOptionName = "button";
			public string DropDownButtonPhrase = "";
			public bool UseImageAndText = false;

			// Check visible
			public bool Visible => Items.Any(item => item.Visible);

			// Check group option visible
			public bool GroupOptionVisible
			{
				get {
					int cnt = 0;
					foreach (var item in Items) {
						if (item.Name != GroupOptionName &&
							(item.Visible && item.ShowInDropDown && UseDropDownButton ||
							item.Visible && item.ShowInButtonGroup && UseButtonGroup)) {
								cnt++;
							if (UseDropDownButton && cnt > 1)
								return true;
							else if (UseButtonGroup)
								return true;
						}
					}
					return false;
				}
			}

			// Add and return a new option
			public ListOption Add(string name)
			{
				ListOption item = new ListOption(name);
				item.Parent = this;
				Items.Add(item);
				return item;
			}

			// Load default settings
			public void LoadDefault()
			{
				CustomItem = "";
				Items.ForEach(item => item.Body = "");
			}

			// Hide all options
			public void HideAllOptions(List<string> list = null) => Items.ForEach(item => { if (list == null || !list.Contains(item.Name)) item.Visible = false; });

			// Show all options
			public void ShowAllOptions() => Items.ForEach(item => item.Visible = true);

			/// <summary>
			/// Get item by name
			/// </summary>
			/// <param name="name">Name of item. Predefined names: view/edit/copy/delete/detail_DetailTable/userpermission/checkbox</param>
			/// <returns>ListOption</returns>

			public ListOption GetItem(string name) => Items.Find(item => item.Name == name);

			// Get item by name
			public ListOption this[string name] => GetItem(name);

			// Get item by index
			public ListOption this[int index] => Items[index];

			// Get item index by name (predefined names: view/edit/copy/delete/detail_<DetailTable>/userpermission/checkbox)
			public int GetItemIndex(string name) => Items.FindIndex(item => item.Name == name);

			// Move item to position
			public void MoveItem(string name, int pos)
			{
				int newpos = pos;
				if (newpos < 0) // If negative, count from the end
					newpos = Items.Count + newpos;
				if (newpos < 0) {
					newpos = 0;
				} else if (newpos > Items.Count) {
					newpos = Items.Count;
				}
				var currentItem = GetItem(name);
				int oldpos = GetItemIndex(name);
				if (oldpos > -1 && newpos != oldpos) {
					Items.RemoveAt(oldpos); // Remove old item
					if (oldpos < newpos)
						newpos--; // Adjust new position
					Items.Insert(newpos, currentItem); // Insert new item
				}
			}

			// Render list options
			public void Render(string part, string pos = "", object rowCnt = null, string scriptType = "block", string scriptId = "", string scriptClass = "") =>
				Write(RenderHtml(part, pos, rowCnt, scriptType, scriptId, scriptClass));

			// Render list options body
			public IHtmlContent RenderBody(string pos = "", object rowCnt = null, string scriptType = "block", string scriptId = "", string scriptClass = "") =>
				new HtmlString(RenderHtml("body", pos, rowCnt, scriptType, scriptId, scriptClass));

			// Render list options header
			public IHtmlContent RenderHeader(string pos = "") => new HtmlString(RenderHtml("header", pos));

			// Render list options footer
			public IHtmlContent RenderFooter(string pos = "") => new HtmlString(RenderHtml("footer", pos));

			// Render list options as HTML
			public string RenderHtml(string part, string pos = "", object rowCnt = null, string scriptType = "block", string scriptId = "", string scriptClass = "") {
				var groupitem = GetItem(GroupOptionName);
				if (Empty(CustomItem) && groupitem != null && ShowPosition(groupitem.OnLeft, pos)) {
					if (UseDropDownButton) { // Render dropdown
						var buttonValue = "";
						int cnt = 0;
						foreach (var item in Items) {
							if (item.Name != GroupOptionName && item.Visible) {
								if (item.ShowInDropDown) {
									buttonValue += item.Body;
									cnt++;
								} else if (item.Name == "listactions") { // Show listactions as button group
									item.Body = RenderButtonGroup(item.Body);
								}
							}
						}
						if (cnt < 1 || cnt == 1 && !buttonValue.Contains("dropdown-menu")) { // No item to show in dropdown or only one item without dropdown menu
							UseDropDownButton = false; // No need to use drop down button
						} else {
							groupitem.Body = RenderDropDownButton(buttonValue, pos);
							groupitem.Visible = true;
						}
					}
					if (!UseDropDownButton && UseButtonGroup) { // Render button group
						var visible = false;
						var buttonGroups = new Dictionary<string, string>();
						foreach (var item in Items) {
							if (item.Name != GroupOptionName && item.Visible && !Empty(item.Body)) {
								if (item.ShowInButtonGroup) {
									visible = true;
									var buttonValue = item.Body;
									if (!buttonGroups.ContainsKey(item.ButtonGroupName))
										buttonGroups[item.ButtonGroupName] = "";
									buttonGroups[item.ButtonGroupName] += buttonValue;
								} else if (item.Name == "listactions") { // Show listactions as button group
									item.Body = RenderButtonGroup(item.Body);
								}
							}
						}
						groupitem.Body = "";
						foreach (var (key, buttonGroup) in buttonGroups)
							groupitem.Body += RenderButtonGroup(buttonGroup);
						if (visible)
							groupitem.Visible = true;
					}
				}
				if (!Empty(scriptId)) {
					return Output(part, pos, rowCnt, "block", scriptId, scriptClass) + // Original block for ew.showTemplates
						Output(part, pos, rowCnt, "blocknotd", scriptId) +
						Output(part, pos, rowCnt, "single", scriptId);
				} else {
					return Output(part, pos, rowCnt, scriptType, scriptId, scriptClass);
				}
			}

			// Get custom template script tag
			protected string CustomScriptTag(string scriptId, string scriptType, string scriptClass, string rowCnt = "") {
				var id = "_" + scriptId;
				if (!Empty(rowCnt))
					id = rowCnt + id;
				id = "tp" + scriptType + id;
				return "<script id=\"" + id + "\"" + (!Empty(scriptClass) ? " class=\"" + scriptClass + "\"" : "") + " type=\"text/html\">";
			}

			// Output list options
			protected string Output(string part, string pos = "", object rowCnt = null, string scriptType = "block", string scriptId = "", string scriptClass = "") {
				RowCnt = Convert.ToString(rowCnt);
				ScriptType = scriptType;
				ScriptId = scriptId;
				ScriptClassName = scriptClass;
				JavaScript = "";
				string result = "";
				if (!Empty(scriptId)) {
					Tag = (scriptType == "block") ? "td" : "span";
					if (scriptType == "block") {
						if (part == "header")
							result += CustomScriptTag(scriptId, "oh", scriptClass);
						else if (part == "body")
							result += CustomScriptTag(scriptId, "ob", scriptClass, RowCnt);
						else if (part == "footer")
							result += CustomScriptTag(scriptId, "of", scriptClass);
					} else if (scriptType == "blocknotd") {
						if (part == "header")
							result += CustomScriptTag(scriptId, "o2h", scriptClass);
						else if (part == "body")
							result += CustomScriptTag(scriptId, "o2b", scriptClass, RowCnt);
						else if (part == "footer")
							result += CustomScriptTag(scriptId, "o2f", scriptClass);
						result += "<span>";
					}
				} else {
					Tag = (!Empty(pos) && pos != "bottom") ? "td" : "div";
				}
				if (!Empty(CustomItem)) {
					ListOption opt = null;
					int cnt = 0;
					foreach (var item in Items) {
						if (ShowItem(item, scriptId, pos))
							cnt++;
						if (item.Name == CustomItem)
							opt = item;
					}
					var useButtonGroup = UseButtonGroup; // Backup options
					UseButtonGroup = true; // Show button group for custom item
					if (opt != null && cnt > 0) {
						if (!Empty(scriptId) || ShowPosition(opt.OnLeft, pos)) {
							result += opt.Render(part, cnt);
						} else {
							result += opt.Render("", cnt);
						}
					}
					UseButtonGroup = useButtonGroup; // Restore options
				} else {
					foreach (var item in Items) {
						if (ShowItem(item, scriptId, pos))
							result += item.Render(part, 1);
					}
				}
				if ((scriptType == "block" || scriptType == "blocknotd") && !Empty(scriptId)) {
					if (scriptType == "blocknotd")
						result += "</span>";
					result += "</script>";
					if (!Empty(JavaScript))
						result += JavaScript;
				}
				return result;
			}

			// Show item
			private bool ShowItem(ListOption item, string scriptId, string pos)
			{
				var show = item.Visible && (!Empty(scriptId) || ShowPosition(item.OnLeft, pos));
				if (show)
					if (UseDropDownButton)
						show = (item.Name == GroupOptionName || !item.ShowInDropDown);
					else if (UseButtonGroup)
						show = (item.Name == GroupOptionName || !item.ShowInButtonGroup);
				return show;
			}

			// Show position
			private bool ShowPosition(bool onLeft, string pos) => (onLeft && pos == "left") || (!onLeft && pos == "right") || pos == "" || pos == "bottom";

			/// <summary>
			/// Concat options
			/// </summary>
			/// <param name="pattern">Regular expression pattern for matching the option names, e.g. '/^detail_/'</param>
			/// <param name="separator">Separator</param>
			/// <returns>The concatenated HTML</returns>

			public string Concat(string pattern, string separator = "") => String.Join(separator, Items.Where(item => Regex.IsMatch(item.Name, pattern) && !Empty(item.Body)));

			/// <summary>
			/// Merge options to the first option
			/// </summary>
			/// <param name="pattern">Regular expression pattern for matching the option names, e.g. '/^detail_/'</param>
			/// <param name="separator">Separator</param>
			/// <returns>The first option</returns>

			public ListOption Merge(string pattern, string separator = "") {
				var items = Items.Where(item => Regex.IsMatch(item.Name, pattern));
				var first = items.FirstOrDefault();
				if (first != null)
					first.Body = Concat(pattern, separator);
				items.Skip(1).ToList().ForEach(item => item.Visible = false);
				return first;
			}

			// Get button group link
			public string RenderButtonGroup(string body) {

				// Get all inputs
				// format: <input type="hidden" ...>

				var inputs = new List<string>();
				foreach (Match match in Regex.Matches(body, @"<input\s+type=['""]hidden['""]\s+([^>]*)>", RegexOptions.IgnoreCase)) {
					body = body.Replace(match.Value, "");
					inputs.Add(match.Value);
				}

				// Get all buttons
				// format: <div class="btn-group">...</div>

				var btns = new List<string>();
				foreach (Match match in Regex.Matches(body, @"<div\s+class\s*=\s*['""]btn-group([^'""]*)['""]([^>]*)>([\s\S]*?)</div\s*>", RegexOptions.IgnoreCase)) {
					body = body.Replace(match.Value, "");
					btns.Add(match.Value);
				}
				var links = "";

				// Get all links/buttons
				// format: <a ...>...</a> / <button ...>...</button>

				foreach (Match match in Regex.Matches(body, @"<(a|button)([^>]*)>([\s\S]*?)</(a|button)\s*>", RegexOptions.IgnoreCase)) {
					string tag = match.Groups[1].Value, cls = "", attrs = "";
					var m = Regex.Match(match.Groups[2].Value, @"\s+class\s*=\s*['""]([\s\S]*?)['""]", RegexOptions.IgnoreCase);
					if (m.Success) { // Match class="class"
						cls = m.Groups[1].Value;
						attrs = match.Groups[2].Value.Replace(m.Value, "");
					} else {
						attrs = match.Groups[2].Value;
					}
					var caption = match.Groups[3].Value;
					if (!cls.Contains("btn btn-default"))
						cls = PrependClass(cls, "btn btn-default"); // Prepend button classes
					if (!Empty(ButtonClass))
						cls = AppendClass(cls, ButtonClass); // Append button classes
					attrs = " class=\"" + cls + "\" " + attrs;
					var link = "<" + tag + attrs + ">" + caption + "</" + tag + ">";
					links += link;
				}
				var btngroup = "";
				if (!Empty(links))
					btngroup = "<div class=\"btn-group btn-group-sm ew-btn-group\">" + links + "</div>";
				foreach (var btn in btns)
					btngroup += btn;
				foreach (var input in inputs)
					btngroup += input;
				return btngroup;
			}

			// Render drop down button
			public string RenderDropDownButton(string body, string pos) {

				// Get all inputs
				// format: <input type="hidden" ...>

				var inputs = new List<string>();
				foreach (Match match in Regex.Matches(body, @"<input\s+type=['""]hidden['""]\s+([^>]*)>", RegexOptions.IgnoreCase)) {
					body = body.Replace(match.Value, "");
					inputs.Add(match.Value);
				}

				// Remove <div class="d-none ew-preview">...</div>
				var previewlinks = "";
				foreach (Match match in Regex.Matches(body, @"<div\s+class\s*=\s*['""]d-none\s+ew-preview['""]>([\s\S]*?)(<div([^>]*)>([\s\S]*?)</div\s*>)+([\s\S]*?)</div\s*>", RegexOptions.IgnoreCase)) {
					body = body.Replace(match.Value, "");
					previewlinks += match.Value;
				}

				// Remove toggle button first <button ... data-toggle="dropdown">...</button>
				foreach (Match match in Regex.Matches(body, @"<button\s+([\s\S]*?)data-toggle\s*=\s*['""]dropdown['""]\s*>([\s\S]*?)<\/button\s*>", RegexOptions.IgnoreCase)) {
					body = body.Replace(match.Value, "");
				}

				// Get all links/buttons <a ...>...</a> / <button ...>...</button>
				var matches = Regex.Matches(body, @"<(a|button)([^>]*)>([\s\S]*?)</(a|button)\s*>", RegexOptions.IgnoreCase);
				if (matches.Count == 0)
					return "";
				string links = "", submenulink = "", submenulinks = "";
				var submenu = false;
				foreach (Match match in matches) {
					string tag = match.Groups[1].Value, action = "", caption = "", cls = "", attrs = "";
					var submatches = Regex.Match(match.Value, @"\s+data-action\s*=\s*['""]([\s\S]*?)['""]", RegexOptions.IgnoreCase);
					if (submatches.Success) // Match data-action='action'
						action = submatches.Groups[1].Value;
					submatches = Regex.Match(match.Groups[2].Value, @"\s+class\s*=\s*['""]([\s\S]*?)['""]", RegexOptions.IgnoreCase);
					if (submatches.Success) { // Match class='class'
						cls = Regex.Replace(submatches.Groups[1].Value, @"btn[\S]*\s+", "", RegexOptions.IgnoreCase);
						attrs = match.Groups[2].Value.Replace(submatches.Value, "");
					} else {
						attrs = match.Groups[2].Value;
					}
					attrs = Regex.Replace(attrs, @"\s+title\s*=\s*['""]([\s\S]*?)['""]", "", RegexOptions.IgnoreCase); // Remove title='title'
					submatches = Regex.Match(attrs, @"\s+data-caption\s*=\s*['""]([\s\S]*?)['""]", RegexOptions.IgnoreCase);
					if (submatches.Success) // Match data-caption='caption'
						caption = submatches.Groups[1].Value;
					cls = AppendClass(cls, "dropdown-item");
					attrs = " class=\"" + cls + "\" " + attrs;
					if (SameText(tag, "button")) // Add href for button
						attrs += " href=\"javascript:void(0);\"";
					submatches = Regex.Match(match.Groups[3].Value, @"<i([^>]*)>([\s\S]*?)<\/i\s*>", RegexOptions.IgnoreCase); // Inner HTML contains <i> tag
					var subsubmatches = Regex.Match(submatches.Groups[1].Value, @"\s+class\s*=\s*['""]([\s\S]*?)['""]", RegexOptions.IgnoreCase); // The <i> tag has 'class' attribute
					if (!Empty(caption) && // Has caption
						submatches.Success && // Inner HTML contains <i> tag
						subsubmatches.Success && // The <i> tag has 'class' attribute
						Regex.Match(subsubmatches.Groups[1].Value, @"\bew-icon\b", RegexOptions.IgnoreCase).Success) { // The classes contains 'ew-icon' => icon
							cls = subsubmatches.Groups[1].Value;
							cls = AppendClass(cls, "mr-2"); // Add margin-right
						caption = submatches.Groups[0].Value.Replace(subsubmatches.Groups[1].Value, cls) + caption;
					}
					if (UseImageAndText && !Empty(caption)) { // Image and text
						var submatch = Regex.Match(match.Groups[3].Value, @"<img([^>]*)>", RegexOptions.IgnoreCase); // <img> tag
						if (submatch.Success)
							caption = submatch.Value + "&nbsp;&nbsp;" + caption;
						submatch = Regex.Match(match.Groups[3].Value, @"<span([^>]*)>([\s\S]*?)<\/span\s*>", RegexOptions.IgnoreCase); // <span class='class'></span> tag
						if (submatch.Success)
							if (Regex.IsMatch(submatch.Groups[1].Value, @"\s+class\s*=\s*['""]([\s\S]*?)['""]", RegexOptions.IgnoreCase)) // Match class='class'
								caption = submatch.Value + "&nbsp;&nbsp;" + caption;
					}
					if (Empty(caption))
						caption = match.Groups[3].Value;
					string link = "<a" + attrs + ">" + caption + "</a>";
					if (action == "list") { // Start new submenu
						if (submenu) { // End previous submenu
							if (!Empty(submenulinks)) { // Set up submenu
								links += "<li class=\"dropdown-submenu\">" + submenulink + "<ul class=\"dropdown-menu\">" + submenulinks + "</ul></li>";
							} else {
								links += "<li>" + submenulink + "</li>";
							}
						}
						submenu = true;
						submenulink = link;
						submenulinks = "";
					} else {
						if (Empty(action) && submenu) { // End previous submenu
							if (!Empty(submenulinks)) { // Set up submenu
								links += "<li class=\"dropdown-submenu\">" + submenulink + "<ul class=\"dropdown-menu\">" + submenulinks + "</ul></li>";
							} else {
								links += "<li>" + submenulink + "</li>";
							}
							submenu = false;
						}
						if (submenu)
							submenulinks += "<li>" + link + "</li>";
						else
							links += "<li>" + link + "</li>";
					}
				}
				var btndropdown = "";
				if (!Empty(links)) {
					if (submenu) { // End previous submenu
						if (!Empty(submenulinks)) { // Set up submenu
							links += "<li class=\"dropdown-submenu\">" + submenulink + "<ul class=\"dropdown-menu\">" + submenulinks + "</ul></li>";
						} else {
							links += "<li>" + submenulink + "</li>";
						}
					}
					var btnclass = "dropdown-toggle btn btn-default";
					if (!Empty(ButtonClass))
						btnclass = AppendClass(btnclass, ButtonClass);
					string buttontitle = HtmlTitle(DropDownButtonPhrase);
					buttontitle = (DropDownButtonPhrase != buttontitle) ? " title=\"" + buttontitle + "\"" : "";
					string button = "<button class=\"" + btnclass + "\"" + buttontitle + " data-toggle=\"dropdown\">" + DropDownButtonPhrase + "</button>" +
						"<ul class=\"dropdown-menu " + ((pos == "right") ? "dropdown-menu-right " : "") + "ew-menu\">" + links + "</ul>";
					if (pos == "bottom") // Use dropup
						btndropdown = "<div class=\"btn-group btn-group-sm dropup ew-btn-dropdown\">" + button + "</div>";
					else
						btndropdown = "<div class=\"btn-group btn-group-sm ew-btn-dropdown\">" + button + "</div>";
				}
				foreach (var input in inputs)
					btndropdown += input;
				btndropdown += previewlinks;
				return btndropdown;
			}

			// Hide detail items for dropdown
			public void HideDetailItemsForDropDown() {
				var showDetail = false;
				if (UseDropDownButton)
					showDetail = Items.Any(item => item.Name != GroupOptionName && item.Visible && item.ShowInDropDown && !item.Name.StartsWith("detail_"));
				if (!showDetail)
					Items.ForEach(item => { if (item.Name.StartsWith("detail_")) item.Visible = false; });
			}
		}

		/// <summary>
		/// List option class
		/// </summary>

		public class ListOption
		{
			public string Name = "";
			public bool OnLeft = false;
			public string CssStyle = "";
			public string CssClass = "";
			public bool Visible = true;
			public string Header = "";
			public string Body = "";
			public string Footer = "";
			public string Tag = "td";
			public string Separator = "";
			public ListOptions Parent;
			public bool ShowInButtonGroup = true;
			public bool ShowInDropDown = true;
			public string ButtonGroupName = "_default";

			// Constructor
			public ListOption(string name) => Name = name;

			// Clear
			public void Clear() => Body = "";

			// Move
			public void MoveTo(int pos) => Parent.MoveItem(Name, pos);

			// Render
			public string Render(string part, int colSpan = 1) {
				var tagclass = Parent.TagClassName;
				var value = "";
				if (part == "header") {
					if (tagclass == "") tagclass = "ew-list-option-header";
					value = Header;
				} else if (part == "body") {
					if (tagclass == "") tagclass = "ew-list-option-body";
					if (Parent.Tag != "td")
						tagclass = AppendClass(tagclass, "ew-list-option-separator");
					value = Body;
				} else if (part == "footer") {
					if (tagclass == "") tagclass = "ew-list-option-footer";
					value = Footer;
				} else {
					value = part;
				}
				if (Empty(value) && Parent.Tag == "span" && Empty(Parent.ScriptId))
					return "";
				var res = !Empty(value) ? value : "&nbsp;";
				tagclass = AppendClass(tagclass, CssClass);
				var attrs = new Attributes() {{"class", tagclass}, {"style", CssStyle}, {"data-name", Name}}; // DN
				if (SameText(Parent.Tag, "td") && Parent.RowSpan > 1)
					attrs["rowspan"] = Convert.ToString(Parent.RowSpan);
				if (SameText(Parent.Tag, "td") && colSpan > 1)
					attrs["colspan"] = Convert.ToString(colSpan);
				var name = Parent.TableVar + "_" + Name;
				if (Name != Parent.GroupOptionName) {
					if (!(new List<string> {"checkbox", "rowcnt"}).Contains(Name)) {
						if (Parent.UseButtonGroup && ShowInButtonGroup) {
							res = Parent.RenderButtonGroup(res);
							if (OnLeft && SameText(Parent.Tag, "td") && colSpan > 1)
								res = "<div class=\"text-right\">" + res + "</div>";
						}
					}
					if (part == "header")
						res = "<span id=\"elh_" + name + "\" class=\"" + name + "\">" + res + "</span>";
					else if (part == "body")
						res = "<span id=\"el" + Parent.RowCnt + "_" + name + "\" class=\"" + name + "\">" + res + "</span>";
					else if (part == "footer")
						res = "<span id=\"elf_" + name + "\" class=\"" + name + "\">" + res + "</span>";
				}

				// remove string tag = (Parent.Tag == "td" && part == "header") ? "th" : Parent.Tag;
				var tag = (Parent.Tag == "td" && part == "header") ? "th" : Parent.Tag;
				if (Parent.UseButtonGroup && ShowInButtonGroup)
					attrs["class"] = AppendClass(attrs["class"], "text-nowrap");
				res = HtmlElement(tag, attrs, res);
				if (!Empty(Parent.ScriptId)) {
					var js = ExtractScript(ref res, Parent.ScriptClassName + "_js");
					if (Parent.ScriptType == "single") {
						if (part == "header")
							res = "<script id=\"tpoh_" + Parent.ScriptId + "_" + Name + "\" type=\"text/html\">" + res + "</script>";
						else if (part == "body")
							res = "<script id=\"tpob" + Parent.RowCnt + "_" + Parent.ScriptId + "_" + Name + "\" type=\"text/html\">" + res + "</script>";
						else if (part == "footer")
							res = "<script id=\"tpof_" + Parent.ScriptId + "_" + Name + "\" type=\"text/html\">" + res + "</script>";
					}
					if (!Empty(js))
						if (Parent.ScriptType == "single")
							res += js;
						else
							Parent.JavaScript += js;
				}
				return res;
			}
		}

		/// <summary>
		/// ListOptions Dictionary
		/// </summary>

		public class ListOptionsDictionary : Dictionary<string, ListOptions>
		{

			// ForEach
			public ListOptionsDictionary ForEach(Action<ListOptions> action) {
				Values.ToList().ForEach(action);
				return this;
			}

			// Render all options
			public IHtmlContent Render(string part, string pos = "") =>
				new HtmlString(Values.Aggregate("", (output, opt) => output + opt.RenderHtml(part, pos)));

			// Render all options body
			public IHtmlContent RenderBody(string pos = "") => Render("body", pos);
		}

		/// <summary>
		/// List actions class
		/// </summary>

		public class ListActions {
			public Dictionary<string, ListAction> Items = new Dictionary<string, ListAction>();

			// Add and return a new action
			public ListAction Add(string name, ListAction action) {
				Items[name] = action;
				return action;
			}

			// Add and return a new action
			public ListAction Add(string name, string caption, bool allow = true, string method = Config.ActionPostback, string select = Config.ActionMultiple, string confirmMsg = "", string icon = "fa fa-star ew-icon", string success = "") {
				var item = new ListAction(name, caption, allow, method, select, confirmMsg, icon, success);
				Items[name] = item;
				return item;
			}

			// Add multiple actions by name and caption
			public void Add(Dictionary<string, string> actions) {
				foreach (var (key, action) in actions)
					Add(key, action);
			}

			// Get item by name
			public ListAction GetItem(string name) => Items.TryGetValue(name, out ListAction action) ? action : null;

			// Indexer
			public ListAction this[string index] => GetItem(index);
		}

		/// <summary>
		/// List action class
		/// </summary>

		public class ListAction {
			public string Action = "";
			public string Caption = "";
			public bool Allowed = true;
			public string Method = Config.ActionPostback; // Post back (p) / Ajax (a)
			public string Select = Config.ActionMultiple; // Multiple (m) / Single (s)
			public string ConfirmMessage = "";
			public string Icon = "fa fa-star ew-icon"; // Icon
			public string Success = ""; // JavaScript callback function name

			// Constructor
			public ListAction(string action, string caption, bool allowed = true, string method = Config.ActionPostback, string select = Config.ActionMultiple, string confirmMsg = "", string icon = "fa fa-star ew-icon", string success = "") {
				Action = action;
				Caption = caption;
				Allowed = allowed;
				Method = method;
				Select = select;
				ConfirmMessage = confirmMsg;
				Icon = icon;
				Success = success;
			}

			// To JSON
			public string ToJson(bool htmlencode = false) {
				var d = new Dictionary<string, string> { {"msg", ConfirmMessage}, {"action", Action}, {"method", Method}, {"select", Select}, {"success", Success} };
				var json = ConvertToJson(d);
				if (htmlencode)
					json = HtmlEncode(json);
				return json;
			}
		}

		/// <summary>
		/// Sub pages class
		/// </summary>

		public class SubPages {
			public bool Justified = false;
			public bool Fill = false;
			public bool Vertical = false;
			public string Align = "left"; // "left" or "center" or "right"
			public string Style = ""; // "tabs" or "pills" or "" (panels)
			public string Classes = ""; // Other CSS classes
			public string Parent = "false"; // For Style = "" only, if a selector is provided, then all collapsible elements under the specified parent will be closed when a collapsible item is shown.
			public Dictionary<object, SubPage> Items = new Dictionary<object, SubPage>();
			public List<string> ValidKeys = new List<string>();

			// Get nav style
			public string NavStyle {
				get {
					string style = "";
					if (!Empty(Style))
						style += "nav nav-" + Style;
					if (Justified)
						style += " nav-justified";
					if (Fill)
						style += " nav-fill";
					if (SameText(Align, "center")) {
						style += " justify-content-center";
					} else if (SameText(Align, "right")) {
						style += " justify-content-end";
					}
					if (Vertical)
						style += " flex-column";
					if (!Empty(Classes))
						style += " " + Classes;
					return style;
				}
			}

			// Check if a page is active
			public bool IsActive(object k) => SameString(ActivePageIndex, k);

			// Get page classes
			public string PageStyle(object k) {
				if (IsActive(k)) { // Active page
					if (!Empty(Style)) // "tabs" or "pills"
						return " active show";
					else // accordion
						return " show"; // .collapse does not use .active
				}
				var item = GetItem(k);
				if (item != null) { // "tabs" or "pills"
					if (!item.Visible)
						return " d-none ew-hidden";
					else if (item.Disabled && Style != "")
						return " disabled ew-disabled";
				}
				return "";
			}

			// Get count
			public int Count => Items.Count;

			// Add item by name
			public SubPage Add(object k) {
				var item = new SubPage();
				if (!Empty(k))
					Items.Add(k, item);
				return item;
			}

			// Get item by key
			public SubPage GetItem(object k) => Items.TryGetValue(k, out SubPage p) ? p : null;

			// Active page index
			public object ActivePageIndex => Items.FirstOrDefault(kvp => (ValidKeys.Count == 0 || ValidKeys.Contains(kvp.Key)) && // Return first active page
				kvp.Value.Visible && !kvp.Value.Disabled && kvp.Value.Active && (!IsNumeric(kvp.Key) || ConvertToInt(kvp.Key) != 0)).Key ??
				Items.FirstOrDefault(kvp => (ValidKeys.Count == 0 || ValidKeys.Contains(kvp.Key)) && // If not found, return first visible page
				kvp.Value.Visible && !kvp.Value.Disabled && (!IsNumeric(kvp.Key) || ConvertToInt(kvp.Key) != 0)).Key;

			// Indexer
			public SubPage this[object index] => GetItem(index);
		}

		/// <summary>
		/// Sub page class
		/// </summary>

		public class SubPage {
			public bool Active = false;
			public bool Visible = true; // If false, add class "d-none", for tabs/pills only
			public bool Disabled = false; // If true, add class "disabled", for tabs/pills only
		}

		/// <summary>
		/// CSS parser
		/// </summary>

		public class CssParser {
			public Dictionary<string, Dictionary<string, string>> Css;

			// Constructor
			public CssParser() => Css = new Dictionary<string, Dictionary<string, string>>();

			// Clear all styles
			public void Clear() {
				foreach (var dict in Css.Values)
					dict.Clear();
				Css.Clear();
			}

			// add a section
			public void Add(string key, string codestr) {
				key = key.ToLower().Trim();
				if (key == "")
					return;
				codestr = codestr.ToLower();
				if (!Css.ContainsKey(key))
					Css[key] = new Dictionary<string, string>();
				string[] codes = codestr.Split(';');
				if (codes.Length > 0) {
					foreach (string code in codes) {
						string[] arCode = code.Split(':');
						string codekey = arCode[0];
						string codevalue = "";
						if (arCode.Length > 1)
							codevalue = arCode[1];
						if (codekey.Length > 0)
							Css[key][codekey.Trim()] = codevalue.Trim();
					}
				}
			}

			// explode a string into two
			private (string, string) Explode(string str, char sep) {
				string[] ar = str.Split(sep);
				return (ar[0], (ar.Length > 1) ? ar[1] : "");
			}

			// Get a style
			public string Get(string key, string property) {
				key = key.ToLower();
				property = property.ToLower();
				string tag = "", subtag = "", cls = "", id = "";
				(tag, subtag) = Explode(key, ':');
				(tag, cls) = Explode(tag, '.');
				(tag, id) = Explode(tag, '#');
				string result = "";
				foreach (var (t, _) in Css) {
					string _tag = t, _subtag = "", _cls = "", _id = "";
					(_tag, _subtag) = Explode(_tag, ':');
					(_tag, _cls) = Explode(_tag, '.');
					(_tag, _id) = Explode(_tag, '#');
					bool tagmatch = (tag == _tag || _tag.Length == 0);
					bool subtagmatch = (subtag == _subtag || _subtag.Length == 0);
					bool classmatch = (cls == _cls || _cls.Length == 0);
					bool idmatch = (id == _id);
					if (tagmatch && subtagmatch && classmatch && idmatch) {
						string temp = _tag;
						if (temp.Length > 0 && _cls.Length > 0) {
							temp += "." + _cls;
						} else if (temp.Length == 0) {
							temp = "." + _cls;
						}
						if (temp.Length > 0 && _subtag.Length > 0) {
							temp += ":" + _subtag;
						} else if (temp.Length == 0) {
							temp = ":" + _subtag;
						}
						if (Css[temp].TryGetValue(property, out string val))
							result = val;
					}
				}
				return result;
			}

			// Get section as dictionary
			public Dictionary<string, string> GetSection(string key) {
				key = key.ToLower();
				string tag = "", subtag = "", cls = "", id = "";
				(tag, subtag) = Explode(key, ':');
				(tag, cls) = Explode(tag, '.');
				(tag, id) = Explode(tag, '#');
				var result = new Dictionary<string, string>();
				foreach (var (t, _) in Css) {
					string _tag = t, _subtag = "", _cls = "", _id = "";
					(_tag, _subtag) = Explode(_tag, ':');
					(_tag, _cls) = Explode(_tag, '.');
					(_tag, _id) = Explode(_tag, '#');
					bool tagmatch = (tag == _tag || _tag.Length == 0);
					bool subtagmatch = (subtag == _subtag || _subtag.Length == 0);
					bool classmatch = (cls == _cls || _cls.Length == 0);
					bool idmatch = (id == _id);
					if (tagmatch && subtagmatch && classmatch && idmatch) {
						string temp = _tag;
						if (temp.Length > 0 && _cls.Length > 0) {
							temp += "." + _cls;
						} else if (temp.Length == 0) {
							temp = "." + _cls;
						}
						if (temp.Length > 0 && _subtag.Length > 0) {
							temp += ":" + _subtag;
						} else if (temp.Length == 0) {
							temp = ":" + _subtag;
						}
						foreach (var (k, v) in Css[temp])
							result[k] = v;
					}
				}
				return result;
			}

			// Get section as string
			public string GetSectionString(string key) =>
				GetSection(key).Aggregate("", (result, kvp) => result + kvp.Key + ":" + kvp.Value + ";");

			// Parse string
			public bool ParseString(string str) {
				Clear();

				// Remove comments
				str = Regex.Replace(str, @"\/\*(.*)?\*\/", "");

				// Parse the CSS code
				var parts = str.Split('}');
				if (parts.Length > 0) {
					foreach (string part in parts) {
						var (keystr, codestr) = Explode(part, '{');
						var keys = keystr.Split(',');
						keys.Where(key => key.Length > 0).ToList().ForEach(key => {
							string result = key.Replace("\n", "").Replace("\r", "").Replace("\\", ""); // Only support non-alphanumeric characters
							Add(result, codestr.Trim());
						});
					}
				}
				return (Css.Count > 0);
			}

			// Get CSS string
			public string GetCss() {
				string result = "";
				foreach (var (k, d) in Css) {
					result += k + " {\n";
					foreach (var (key, value) in d)
						result += "\t" + key + ": " + value + ";\n";
					result += "}\n\n";
				}
				return result;
			}
		}

		/// <summary>
		/// Is local URL
		/// </summary>
		/// <param name="url">URL</param>
		/// <returns>Whether URL is local</returns>

		public static bool IsLocalUrl(string url) {
			if (string.IsNullOrEmpty(url))
				return false;

			// Allows "/" or "/foo" but not "//" or "/\".
			if (url[0] == '/') {

				// url is exactly "/"
				if (url.Length == 1)
					return true;

				// url doesn't start with "//" or "/\"
				if (url[1] != '/' && url[1] != '\\')
					return true;
				return false;
			}

			// Allows "foo" relative url.
			if (!IsAbsoluteUrl(url))
				return true;
			return false;
		}

		/// <summary>
		/// Get claim value
		/// </summary>
		/// <param name="type">Claim type</param>
		/// <returns>Claim value</returns>

		public static string ClaimValue(string type) => User.Claims.First(d => SameText(type, d.Type)).Value;

		/// <summary>
		/// Get connection object
		/// </summary>
		/// <param name="dbid">Database ID</param>
		/// <returns>DatabaseConnection instance</returns>

		public static dynamic GetConnection(string dbid = "DB") { // DN
			dbid = Db(dbid)["id"];
			if (dbid != null) {
				if (Connections.TryGetValue(dbid, out dynamic c)) {
					return c;
				} else {
					var conn = CreateConnection(dbid);
					Connections[dbid] = conn;
					return conn;
				}
			}
			return null;
		}

		/// <summary>
		/// Get connection object
		/// </summary>
		/// <param name="dbid">Database ID</param>
		/// <returns>DatabaseConnection instance</returns>

		public static async Task<dynamic> GetConnectionAsync(string dbid = "DB") { // DN
			dbid = Db(dbid)["id"];
			if (dbid != null) {
				if (Connections.TryGetValue(dbid, out dynamic c)) {
					return c;
				} else {
					var conn = await CreateConnectionAsync(dbid);
					Connections[dbid] = conn;
					return conn;
				}
			}
			return null;
		}

		// Get connection object
		private static dynamic _GetConnection(string dbid = "DB") { // DN
			dbid = Db(dbid)["id"];
			if (dbid != null) {
				if (_Connections.TryGetValue(dbid, out dynamic c)) {
					return c;
				} else {
					var conn = CreateConnection(dbid);
					_Connections[dbid] = conn;
					return conn;
				}
			}
			return null;
		}

		// Get connection object
		private static async Task<dynamic> _GetConnectionAsync(string dbid = "DB") { // DN
			dbid = Db(dbid)["id"];
			if (dbid != null) {
				if (_Connections.TryGetValue(dbid, out dynamic c)) {
					return c;
				} else {
					var conn = await CreateConnectionAsync(dbid);
					_Connections[dbid] = conn;
					return conn;
				}
			}
			return null;
		}

		/// <summary>
		/// DbHelper (alias of GetConnection)
		/// </summary>
		/// <param name="dbid">Database ID</param>
		/// <returns>DatabaseConnection instance</returns>

		public static dynamic DbHelper(string dbid = "DB") => GetConnection(dbid);

		/// <summary>
		/// Get database connection info
		/// </summary>
		/// <param name="dbid">Database ID</param>
		/// <returns>ConfigurationSection of connection info</returns>

		public static ConfigurationSection Db(string dbid = "DB") {
			if (Empty(dbid))
 				dbid = "DB";
			return (ConfigurationSection)Configuration.GetSection("Databases:" + dbid); // Result will not be null
		}

		/// <summary>
		/// Get connection type
		/// </summary>
		/// <param name="dbid">Database ID</param>
		/// <returns>Database type</returns>

		public static string GetConnectionType(string dbid = "DB") {
			var db = Db(dbid);
			string type = db["type"] ?? "";
			if (SameText(type, "ODBC") && ConvertToBool(db["msaccess"]))
				type = "ACCESS";
			return type;
		}

		// Create a new connection
		protected static dynamic _CreateConnection(string dbid = "DB") {
			string dbtype = GetConnectionType(dbid);
			dynamic c = null;
			if (SameText(dbtype, "MSSQL")) {
				c = CreateInstance("DatabaseConnection`4", new object[] {dbid}, new Type[] {typeof(SqlConnection), typeof(SqlCommand), typeof(SqlDataReader), typeof(SqlDbType)}) ??
					new DatabaseConnectionBase<SqlConnection, SqlCommand, SqlDataReader, SqlDbType>(dbid);
			}
			return c;
		}

		/// <summary>
		/// Create a new connection
		/// </summary>
		/// <param name="dbid">Database ID</param>
		/// <returns></returns>

		public static dynamic CreateConnection(string dbid = "DB") {
			dynamic c = _CreateConnection(dbid);
			c.Init(dbid);
			return c;
		}

		// Create a new connection
		public static async Task<dynamic> CreateConnectionAsync(string dbid = "DB") {
			dynamic c = _CreateConnection(dbid);
			await c.InitAsync(dbid);
			return c;
		}

		// Create Advanced Security
		public static dynamic CreateSecurity() => CreateInstance("AdvancedSecurity") ?? new AdvancedSecurityBase();

		// Get last insert ID SQL // DN
		public static string GetLastInsertIdSql(string dbid = "DB") {
			string dbtype = GetConnectionType(dbid);
			if (SameText(dbtype, "MYSQL")) {
				return "SELECT LAST_INSERT_ID()";
			} else if (SameText(dbtype, "SQLITE")) { // SQLite
				return "SELECT LAST_INSERT_ROWID()";
			} else if (SameText(dbtype, "ACCESS") || SameText(dbtype, "MSSQL")) {
				return "SELECT @@Identity";
			} else {
				return "";
			}
		}

		// Get SQL parameter symbol // DN
		public static string GetSqlParamSymbol(string dbid = "DB") {
			string dbtype = GetConnectionType(dbid);
			if (SameText(dbtype, "MYSQL") || SameText(dbtype, "MSSQL") || SameText(dbtype, "SQLITE")) {
				return "@";
			} else if (SameText(dbtype, "POSTGRESQL") || SameText(dbtype, "ORACLE")) {
				return ":";
			} else {
				return "?";
			}
		}

		// Close database connections
		public static void CloseConnections(bool closeAll = true) {
			if (closeAll) {
				foreach (var (key, conn) in Connections)
					conn?.Close();
				Connections.Clear();
				foreach (var (key, conn) in _Connections)
					conn?.Close();
				_Connections.Clear();
			}
			Conn?.Close();
			Conn = null;
		}

		// Get a row as Dictionary<string, object> from data reader // DN
		public static Dictionary<string, object> GetDictionary(DbDataReader dr) {
			if (dr != null) {
				var dict = new Dictionary<string, object>();
				for (int i = 0; i < dr.FieldCount; i++) {
					try {
						if (!Empty(dr.GetName(i))) {
							dict[dr.GetName(i)] = dr[i];
						} else {
							dict[i.ToString()] = dr[i]; // Convert index to string as key
						}
					} catch {}
				}
				return dict;
			}
			return null;
		}

		/// <summary>
		/// Database connection base class
		/// </summary>
		/// <typeparam name="N">DbConnection</typeparam>
		/// <typeparam name="M">DbCommand</typeparam>
		/// <typeparam name="R">DbDataReader</typeparam>
		/// <typeparam name="T">Field data type</typeparam>

		public class DatabaseConnectionBase<N, M, R, T> : IDisposable
			where N : DbConnection
			where M : DbCommand
			where R : DbDataReader
		{
			private bool _disposed = false;
			public string ConnectionString;
			public N Conn;
			public ConfigurationSection Info;
			public string DbId;
			private N _conn;
			private DbTransaction _trans;
			private List<M> _command = new List<M>();
			private List<R> _dataReader = new List<R>();

			// Constructor
			public DatabaseConnectionBase(string dbid) => Init(dbid);

			// Constructor
			public DatabaseConnectionBase(): this("DB") {}

			// Overrides Object.Finalize
			~DatabaseConnectionBase() => Dispose(false);

			// Init
			public virtual void Init(string dbid)
			{
				Info = Db(dbid);
				if (Info != null) {
					DbId = Info["id"];
					ConnectionString = Info["connectionstring"];
					Conn = OpenConnection();
				}
			}

			// Init
			public virtual async Task InitAsync(string dbid)
			{
				Info = Db(dbid);
				if (Info != null) {
					DbId = Info["id"];
					ConnectionString = Info["connectionstring"];
					Conn = await OpenConnectionAsync();
				}
			}

			// Get data type
			public T GetDataType(object dt) => (T)dt;

			// Access
			public bool IsAccess => IsOdbc && ConvertToBool(Info["msaccess"]);

			// ODBC
			public bool IsOdbc => typeof(N).ToString().Contains(".OdbcConnection");

			// Microsoft SQL Server
			public bool IsMsSql => typeof(N).ToString().Contains(".SqlConnection");

			// Microsoft SQL Server >= 2012
			private bool? _sql2012 = null;
			public bool IsMsSql2012
			{
				get {
					if (!IsMsSql)
						return false;
					if (_sql2012 == null) {
						var m = Regex.Match(Convert.ToString(ExecuteScalar("SELECT @@version")), @"Microsoft SQL Server (\d+)");
						_sql2012 = m.Success && Convert.ToInt32(m.Groups[1].Value) >= 2012;
					}
					return _sql2012 == true;
				}
			}

			// MySQL
			public bool IsMySql => typeof(N).ToString().Contains(".MySqlConnection");

			// PostgreSQL
			public bool IsPostgreSql => typeof(N).ToString().Contains(".NpgsqlConnection");

			// Oracle
			public bool IsOracle => typeof(N).ToString().Contains(".OracleConnection");

			// Oracle >= 12.1
			private bool? _oracle12 = null;
			public bool IsOracle12
			{
				get {
					if (!IsOracle)
						return false;
					if (_oracle12 == null) {
						var m = Regex.Match(Convert.ToString(ExecuteScalar("SELECT * FROM v$version WHERE banner LIKE 'Oracle%'")), @"Release (\d+\.\d+)");
						_oracle12 = m.Success && Convert.ToDouble(m.Groups[1].Value) >= 12.1;
					}
					return _oracle12 == true;
				}
			}

			// Sqlite
			public bool IsSqlite => typeof(N).ToString().Contains(".SqliteConnection");

			/// <summary>
			/// Select limit
			/// </summary>
			/// <param name="sql">SQL to execute</param>
			/// <param name="nrows">Number of rows</param>
			/// <param name="offset">Offset</param>
			/// <param name="hasOrderBy">SQL has ORDER BY clause or now</param>
			/// <returns>Task that returns data reader</returns>

			public async Task<R> SelectLimit(string sql, int nrows = -1, int offset = -1, bool hasOrderBy = false)
			{
				string offsetPart, limitPart, originalSql = sql;
				if (IsMsSql) {
					if (IsMsSql2012) { // Microsoft SQL Server >= 2012
						if (!hasOrderBy)
							sql += " ORDER BY @@version"; // Dummy ORDER BY clause
						if (offset > -1)
							sql += " OFFSET " + Convert.ToString(offset) + " ROWS";
						if (nrows > 0) {
							if (offset < 0)
								sql += " OFFSET 0 ROWS";
							sql += " FETCH NEXT " + Convert.ToString(nrows) + " ROWS ONLY";
						}
					} else { // Select top
						if (nrows > 0) {
							if (offset > 0)
								nrows += offset;
							sql = Regex.Replace(sql, @"(^\s*SELECT\s+(DISTINCT)?)", @"$1 TOP " + Convert.ToString(nrows) + " ", RegexOptions.IgnoreCase); // DN
						}
					}
				} else if (IsMySql) {
					offsetPart = (offset >= 0) ? Convert.ToString(offset) + "," : "";
					limitPart = (nrows < 0) ? "18446744073709551615" : Convert.ToString(nrows);
					sql += " LIMIT " + offsetPart + limitPart;
				} else if (IsPostgreSql || IsSqlite) {
					offsetPart = (offset >= 0) ? " OFFSET " + Convert.ToString(offset) : "";
					limitPart = (nrows >= 0) ? " LIMIT " + Convert.ToString(nrows) : "";
					sql += limitPart + offsetPart;
				} else if (IsOracle) { // Select top
					if (IsOracle12) { // Oracle >= 12.1
						if (offset > -1)
							sql += " OFFSET " + Convert.ToString(offset) + " ROWS";
						if (nrows > 0)
							sql += " FETCH NEXT " + Convert.ToString(nrows) + " ROWS ONLY";
					} else {
						if (nrows > 0) {
							if (offset > 0)
								nrows += offset;
							sql = "SELECT * FROM (" + sql + ") WHERE ROWNUM <= " + Convert.ToString(nrows);
						}
					}
				} else if (IsAccess) { // Select top
					if (nrows > 0) {
						if (offset > 0)
							nrows += offset;
						sql = Regex.Replace(sql, @"(^\s*SELECT\s+(DISTINCTROW|DISTINCT)?)", @"$1 TOP " + Convert.ToString(nrows) + " ", RegexOptions.IgnoreCase); // DN
					}
				}
				if (!SelectOffset) {
					return await GetDataReaderAsync(sql) ?? await GetDataReaderAsync(originalSql); // If SQL fails due to being too complex, use original SQL.
				} else {
					return await GetDataReaderAsync(sql);
				}
			}

			// Supports select offset
			public bool SelectOffset => IsMySql || IsPostgreSql || IsMsSql2012 || IsOracle12 || IsSqlite;

			/// <summary>
			/// Execute SQL for the main connection
			/// </summary>
			/// <param name="sql">SQL to execute</param>
			/// <returns>The number of rows affected</returns>

			public int Execute(string sql)
			{
				using (var cmd = GetCommand(sql)) {
					return cmd.ExecuteNonQuery();
				}
			}

			/// <summary>
			/// Execute SQL for the main connection (async)
			/// </summary>
			/// <param name="sql">SQL to execute</param>
			/// <returns>Task that returns the number of rows affected</returns>

			public async Task<int> ExecuteAsync(string sql)
			{
				using (var cmd = GetCommand(sql)) {
					return await cmd.ExecuteNonQueryAsync();
				}
			}

			/// <summary>
			/// Execute SQL for a specified connection
			/// </summary>
			/// <param name="sql">SQL to execute</param>
			/// <param name="c">The connection to use</param>
			/// <returns>The number of rows affected</returns>

			public int Execute(string sql, DbConnection c)
			{
				using (var cmd = (M)Activator.CreateInstance(typeof(M), new object[] { sql, c })) {
					return cmd.ExecuteNonQuery();
				}
			}

			/// <summary>
			/// Execute SQL for a specified connection (async)
			/// </summary>
			/// <param name="sql">SQL to execute</param>
			/// <param name="c">The connection to use</param>
			/// <returns>Task that returns the number of rows affected</returns>

			public async Task<int> ExecuteAsync(string sql, DbConnection c)
			{
				using (var cmd = (M)Activator.CreateInstance(typeof(M), new object[] { sql, c })) {
					return await cmd.ExecuteNonQueryAsync();
				}
			}

			/// <summary>
			/// Execute SQL for the secondary connection
			/// </summary>
			/// <param name="sql">SQL to execute</param>
			/// <returns>The number of rows affected</returns>

			public int ExecuteNonQuery(string sql)
			{
				using (var cmd = OpenCommand(sql)) { // Use secondary connection
					return cmd.ExecuteNonQuery();
				}
			}

			/// <summary>
			/// Execute SQL for the secondary connection (async)
			/// </summary>
			/// <param name="sql">SQL to execute</param>
			/// <returns>Task that returns the number of rows affected</returns>

			public async Task<int> ExecuteNonQueryAsync(string sql)
			{
				using (var cmd = await OpenCommandAsync(sql)) { // Use secondary connection
					return await cmd.ExecuteNonQueryAsync();
				}
			}

			/// <summary>
			/// Execute SQL and return first value of first row
			/// </summary>
			/// <param name="sql">SQL to execute</param>
			/// <param name="main">Use main connection or not</param>
			/// <returns>The first column of the first row</returns>

			public object ExecuteScalar(string sql, bool main = false)
			{
				if (main) {
					using (var cmd = GetCommand(sql)) { // Use main connection
						return cmd.ExecuteScalar();
					}
				} else {
					using (var cmd = OpenCommand(sql)) { // Use secondary connection
						return cmd.ExecuteScalar();
					}
				}
			}

			/// <summary>
			/// Execute SQL and return first value of first row (async)
			/// </summary>
			/// <param name="sql">SQL to execute</param>
			/// <param name="main">Use main connection or not</param>
			/// <returns>Task that returns the first column of the first row</returns>

			public async Task<object> ExecuteScalarAsync(string sql, bool main = false)
			{
				if (main) {
					using (var cmd = GetCommand(sql)) { // Use main connection
						return await cmd.ExecuteScalarAsync();
					}
				} else {
					using (var cmd = await OpenCommandAsync(sql)) { // Use secondary connection
						return await cmd.ExecuteScalarAsync();
					}
				}
			}

			/// <summary>
			/// Executes the query, and returns the row(s) as JSON
			/// </summary>
			/// <param name="sql">SQL to execute</param>
			/// <param name="options">
			/// Dictionary of options:
			/// "firstOnly" (bool) Returns the first row only
			/// "array" (bool) Returns the result as array
			/// </param>
			/// <returns>JSON string</returns>

			public string ExecuteJson(string sql, Dictionary<string, object> options = null)
			{
				options = options ?? new Dictionary<string, object>();
				bool firstOnly = options.TryGetValue("firstOnly", out object first) && ConvertToBool(first);
				bool array = options.TryGetValue("array", out object ar) && ConvertToBool(ar);
				if (firstOnly) {
					Dictionary<string, object> row = GetRow(sql);
					if (array)
						return ConvertToJson(row.Values);
					else
						return ConvertToJson(row);
				} else {
					List<Dictionary<string, object>> rows = GetRows(sql);
					if (array)
						return ConvertToJson(rows.Select(d => d.Values));
					else
						return ConvertToJson(rows);
				}
			}

			/// <summary>
			/// Executes the query, and returns the row(s) as JSON (async)
			/// </summary>
			/// <param name="sql">SQL to execute</param>
			/// <param name="options">
			/// Dictionary of options:
			/// "firstOnly" (bool) Returns the first row only
			/// "array" (bool) Returns the result as array
			/// </param>
			/// <returns>Tasks that returns JSON string</returns>

			public async Task<string> ExecuteJsonAsync(string sql, Dictionary<string, object> options = null)
			{
				options = options ?? new Dictionary<string, object>();
				bool firstOnly = options.TryGetValue("firstOnly", out object first) && ConvertToBool(first);
				bool array = options.TryGetValue("array", out object ar) && ConvertToBool(ar);
				if (firstOnly) {
					Dictionary<string, object> row = await GetRowAsync(sql);
					if (array)
						return ConvertToJson(row.Values);
					else
						return ConvertToJson(row);
				} else {
					List<Dictionary<string, object>> rows = await GetRowsAsync(sql);
					if (array)
						return ConvertToJson(rows.Select(d => d.Values));
					else
						return ConvertToJson(rows);
				}
			}

			/// <summary>
			/// Get last insert ID
			/// </summary>
			/// <returns>The last ID</returns>

			public object GetLastInsertId()
			{
				var sql = GetLastInsertIdSql(DbId);
				if (sql != "")
					return ExecuteScalar(sql, true); // Use main connection
				return System.DBNull.Value;
			}

			/// <summary>
			/// Get last insert ID (async)
			/// </summary>
			/// <returns>Task that returns the last ID</returns>

			public async Task<object> GetLastInsertIdAsync()
			{
				var sql = GetLastInsertIdSql(DbId);
				if (sql != "")
					return await ExecuteScalarAsync(sql, true); // Use main connection
				return System.DBNull.Value;
			}

			// Get data reader
			public R GetDataReader(string sql)
			{
				try {
					var cmd = GetCommand(sql); // Use main connection
					return (R)cmd.ExecuteReader();
				} catch {
					if (Config.Debug)
						throw;
					return null;
				}
			}

			// Get data reader
			public async Task<R> GetDataReaderAsync(string sql)
			{
				try {
					var cmd = GetCommand(sql); // Use main connection
					return (R)await cmd.ExecuteReaderAsync();
				} catch {
					if (Config.Debug)
						throw;
					return null;
				}
			}

			// Get a new connection
			public virtual async Task<N> OpenConnectionAsync()
			{
				var connstr = GetConnectionString(Info);
				if (IsSqlite) {
					var relpath = Info["relpath"];
					var dbname = Info["dbname"];
					if (Empty(relpath)) {
						connstr += AppRoot() + dbname;
					} else if (Path.IsPathRooted(relpath)) { // Physical path
						connstr += relpath + dbname;
					} else { // Relative to wwwroot
						connstr += ServerMapPath(relpath) + dbname;
					}
					connstr = connstr.Replace("\\", "/");
					ConnectionString = connstr;
				}
				Database_Connecting(ref connstr);
				var c = (N)Activator.CreateInstance(typeof(N), new object[] { connstr });
				await c.OpenAsync();
				string timezone = Info["timezone"] ?? Config.DbTimeZone;
				if (IsOracle) {
					if (Info["schema"] != "")
						await ExecuteAsync("ALTER SESSION SET CURRENT_SCHEMA = " + QuotedName(Info["schema"], Info["id"]), c); // Set current schema
					await ExecuteAsync("ALTER SESSION SET NLS_TIMESTAMP_FORMAT = 'yyyy-mm-dd hh24:mi:ss'", c);
					await ExecuteAsync("ALTER SESSION SET NLS_TIMESTAMP_TZ_FORMAT = 'yyyy-mm-dd hh24:mi:ss'", c);
					if (timezone != "")
						await ExecuteAsync("ALTER SESSION SET TIME_ZONE = '" + timezone + "'", c);
				} else if (IsMsSql && DateFormatId > 0) { // DN
					await ExecuteAsync("SET DATEFORMAT ymd", c);
				} else if (IsPostgreSql) {
					if (Info["schema"] != "")
						await ExecuteAsync("SET search_path TO " + QuotedName(Info["schema"], Info["id"]), c); // Set current schema
					if (timezone != "")
						await ExecuteAsync("SET TIME ZONE '" + timezone + "'", c);
				} else if (IsMySql && timezone != "") {
					await ExecuteAsync("SET time_zone = '" + timezone + "'", c);
				}
				Database_Connected(c);
				return c;
			}

			// Get a new connection
			public virtual N OpenConnection() => OpenConnectionAsync().GetAwaiter().GetResult();

			// Get command
			public M GetCommand(string sql)
			{
				if (Config.Debug)
					SetDebugMessage(sql);
				var cmd = (M)Activator.CreateInstance(typeof(M), new object[] { sql, Conn });
				if (_trans != null)
					cmd.Transaction = _trans;
				return cmd;
			}

			// Get a new command
			public M OpenCommand(string sql)
			{
				try {
					_conn = _conn ?? OpenConnection(); // Use secondary connection
					if (Config.Debug)
						SetDebugMessage(sql);
					var cmd = (M)Activator.CreateInstance(typeof(M), new object[] { sql, _conn });
					_command.Add(cmd);
					return cmd;
				} catch {
					if (Config.Debug)
						throw;
					return null;
				}
			}

			// Get a new command
			public async Task<M> OpenCommandAsync(string sql)
			{
				try {
					_conn = _conn ?? await OpenConnectionAsync(); // Use secondary connection
					if (Config.Debug)
						SetDebugMessage(sql);
					var cmd = (M)Activator.CreateInstance(typeof(M), new object[] { sql, _conn });
					_command.Add(cmd);
					return cmd;
				} catch {
					if (Config.Debug)
						throw;
					return null;
				}
			}

			/// <summary>
			/// Get command for stored procedure
			/// </summary>
			/// <param name="name">Name of stored procedure</param>
			/// <returns>Command</returns>

			public M GetStoredProcCommand(string name)
			{
				if (Config.Debug)
					SetDebugMessage(name);
				var cmd = (M)Activator.CreateInstance(typeof(M), new object[] { name, Conn });
				cmd.CommandType = CommandType.StoredProcedure;
				if (_trans != null)
					cmd.Transaction = _trans;
				return cmd;
			}

			/// <summary>
			/// Get a new command for stored procedure (async)
			/// </summary>
			/// <param name="name">Name of stored procedure</param>
			/// <returns>Task that returns Command</returns>

			public async Task<M> OpenStoredProcCommandAsync(string name)
			{
				try {
					_conn = _conn ?? await OpenConnectionAsync(); // Use secondary connection
					if (Config.Debug)
						SetDebugMessage(name);
					var cmd = (M)Activator.CreateInstance(typeof(M), new object[] { name, _conn });
					cmd.CommandType = CommandType.StoredProcedure;
					_command.Add(cmd);
					return cmd;
				} catch {
					if (Config.Debug)
						throw;
					return null;
				}
			}

			/// <summary>
			/// Get a new command for stored procedure
			/// </summary>
			/// <param name="name">Name of stored procedure</param>
			/// <returns>Command</returns>

			public M OpenStoredProcCommand(string name) => OpenStoredProcCommandAsync(name).GetAwaiter().GetResult();

			// Get a new data reader
			public R OpenDataReader(string sql)
			{
				try {
					var cmd = OpenCommand(sql); // Use secondary connection
					var r = (R)cmd.ExecuteReader();
					_dataReader.Add(r);
					return r;
				} catch {
					if (Config.Debug)
						throw;
					return null;
				}
			}

			// Get a new data reader
			public async Task<R> OpenDataReaderAsync(string sql)
			{
				try {
					var cmd = await OpenCommandAsync(sql); // Use secondary connection
					var r = (R)await cmd.ExecuteReaderAsync();
					_dataReader.Add(r);
					return r;
				} catch {
					if (Config.Debug)
						throw;
					return null;
				}
			}

			/// <summary>
			/// Get a row from data reader
			/// </summary>
			/// <param name="dr">Data reader</param>
			/// <returns>Row as dictionary</returns>

			public Dictionary<string, object> GetRow(DbDataReader dr) => GetDictionary(dr);

			/// <summary>
			/// Get a row by SQL
			/// </summary>
			/// <param name="sql">SQL to execute</param>
			/// <param name="main">Use main connection or not</param>
			/// <returns>Row as dictionary</returns>

			public Dictionary<string, object> GetRow(string sql, bool main = false)
			{
				using (var dr = main ? GetDataReader(sql) : OpenDataReader(sql)) {
					if (dr != null && dr.Read())
						return GetRow(dr);
				}
				return null;
			}

			/// <summary>
			/// Get a row by SQL (async)
			/// </summary>
			/// <param name="sql">SQL to execute</param>
			/// <param name="main">Use main connection or not</param>
			/// <returns>Task that returns row as dictionary</returns>

			public async Task<Dictionary<string, object>> GetRowAsync(string sql, bool main = false)
			{
				using (var dr = main ? await GetDataReaderAsync(sql) : await OpenDataReaderAsync(sql)) {
					if (dr != null && await dr.ReadAsync())
						return GetRow(dr);
				}
				return null;
			}

			/// <summary>
			/// Get a row from data reader
			/// </summary>
			/// <param name="dr">Data reader</param>
			/// <returns>Row as array</returns>

			public string[] GetArray(DbDataReader dr)
			{
				if (dr != null && dr.FieldCount > 0) {
					var ar = new string[dr.FieldCount];
					for (int i = 0; i < dr.FieldCount; i++)
						ar[i] = Convert.ToString(dr[i]);
					return ar;
				}
				return null;
			}

			/// <summary>
			/// Get rows from data reader
			/// </summary>
			/// <param name="dr">Data reader</param>
			/// <returns>Rows as list of dictionary</returns>

			public List<Dictionary<string, object>> GetRows(DbDataReader dr)
			{
				var rows = new List<Dictionary<string, object>>();
				while (dr != null && dr.Read())
					rows.Add(GetRow(dr));
				return rows;
			}

			/// <summary>
			/// Get rows from data reader (async)
			/// </summary>
			/// <param name="dr">Data reader</param>
			/// <returns>Task that returns rows as list of dictionary</returns>

			public async Task<List<Dictionary<string, object>>> GetRowsAsync(DbDataReader dr)
			{
				var rows = new List<Dictionary<string, object>>();
				while (dr != null && await dr.ReadAsync())
					rows.Add(GetRow(dr));
				return rows;
			}

			/// <summary>
			/// Get rows by SQL
			/// </summary>
			/// <param name="sql">SQL to execute</param>
			/// <param name="main">Use main connection or not</param>
			/// <returns>Rows as list of dictionary</returns>

			public List<Dictionary<string, object>> GetRows(string sql, bool main = false)
			{
				using (var dr = main ? GetDataReader(sql) : OpenDataReader(sql)) {
					if (dr != null)
						return GetRows(dr);
				}
				return null;
			}

			/// <summary>
			/// Get rows by SQL (async)
			/// </summary>
			/// <param name="sql">SQL to execute</param>
			/// <param name="main">Use main connection or not</param>
			/// <returns>Task that returns rows as list of dictionary</returns>

			public async Task<List<Dictionary<string, object>>> GetRowsAsync(string sql, bool main = false)
			{
				using (var dr = main ? await GetDataReaderAsync(sql) : await OpenDataReaderAsync(sql)) {
					if (dr != null)
						return await GetRowsAsync(dr);
				}
				return null;
			}

			/// <summary>
			/// Get rows by SQL
			/// </summary>
			/// <param name="sql">SQL to execute</param>
			/// <returns>Rows as list of array</returns>

			public List<string[]> GetArrays(string sql)
			{
				using (var dr = OpenDataReader(sql)) {
					if (dr != null) {
						var rows = new List<string[]>();
						while (dr.Read())
							rows.Add(GetArray(dr));
						return rows;
					}
				}
				return null;
			}

			/// <summary>
			/// Get rows by SQL (async)
			/// </summary>
			/// <param name="sql">SQL to execute</param>
			/// <returns>Task that returns rows as list of array</returns>

			public async Task<List<string[]>> GetArraysAsync(string sql)
			{
				using (var dr = await OpenDataReaderAsync(sql)) {
					if (dr != null) {
						var rows = new List<string[]>();
						while (await dr.ReadAsync())
							rows.Add(GetArray(dr));
						return rows;
					}
				}
				return null;
			}

			// Get rows as List<DbDataRecord> by SQL
			public List<DbDataRecord> GetRecords(string sql)
			{
				using (var dr = OpenDataReader(sql)) {
					if (dr != null) {
						var list = new List<DbDataRecord>();
						foreach (DbDataRecord record in dr)
							list.Add(record);
						return list;
					}
				}
				return null;
			}

			// Get first row as DbDataRecord by SQL
			public DbDataRecord GetRecord(string sql)
			{
				using (var dr = OpenDataReader(sql)) {
					if (dr != null) {
						foreach (DbDataRecord r in dr)
							return r;
					}
				}
				return null;
			}

			// Get count (by dataset)
			public int GetCount(string sql)
			{
				int cnt = 0;
				using (var dr = OpenDataReader(sql)) {
					if (dr != null) {
						while (dr.Read())
							cnt++;
					}
				}
				return cnt;
			}

			// Begin transaction
			public void BeginTrans()
			{
				try {
					if (IsOracle || IsSqlite)
						_trans = Conn.BeginTransaction();
					else
						_trans = Conn.BeginTransaction(IsolationLevel.ReadUncommitted);
				} catch {
					if (Config.Debug)
						throw;
				}
			}

			// Commit transaction
			public void CommitTrans()
			{
				try {
					_trans?.Commit();
				} catch {
					if (Config.Debug)
						throw;
				}
			}

			// Rollback transaction
			public void RollbackTrans()
			{
				try {
					_trans?.Rollback();
				} catch {
					if (Config.Debug)
						throw;
				}
			}

			// Concat // DN
			public string Concat(params string[] list)
			{
				if (IsAccess) {
					return String.Join(" & ", list);
				} else if (IsMsSql) {
					return String.Join(" + ", list);
				} else if (IsOracle || IsPostgreSql) {
					return String.Join(" || ", list);
				} else {
					return "CONCAT(" + String.Join(", ", list) + ")";
				}
			}

			// Table CSS class name
			public string TableClass = "table table-bordered ew-db-table";

			/// <summary>
			/// Get result in HTML table by SQL
			/// </summary>
			/// <param name="sql">SQL to execute</param>
			/// <param name="options">
			/// Dictionary of options:
			/// "fieldcaption" (bool|Dictionary)
			///   true: Use caption and use language object, or
			///   false: Use field names directly, or
			///   Dictionary of fieid caption for looking up field caption by field name
			/// "horizontal" (bool) Whether HTML table is horizontal, default: false
			/// "tablename" (string|List&lt;string&gt;) Table name(s) for the language object
			/// "tableclass" (string) CSS class names of the table, default: "table table-bordered ew-db-table"
			/// </param>
			/// <returns>HTML string as IHtmlContent</returns>

			public IHtmlContent ExecuteHtml(string sql, Dictionary<string, object> options = null)
			{
				using (var rs = OpenDataReader(sql)) {
					return ExecuteHtml(rs, options);
				}
			}

			/// <summary>
			/// Get result in HTML table by SQL (async)
			/// </summary>
			/// <param name="sql">SQL to execute</param>
			/// <param name="options">
			/// Dictionary of options:
			/// "fieldcaption" (bool|Dictionary)
			///   true: Use caption and use language object, or
			///   false: Use field names directly, or
			///   Dictionary of fieid caption for looking up field caption by field name
			/// "horizontal" (bool) Whether HTML table is horizontal, default: false
			/// "tablename" (string|List&lt;string&gt;) Table name(s) for the language object
			/// "tableclass" (string) CSS class names of the table, default: "table table-bordered ew-db-table"
			/// </param>
			/// <returns>Task that returns HTML string as IHtmlContent</returns>

			public async Task<IHtmlContent> ExecuteHtmlAsync(string sql, Dictionary<string, object> options = null)
			{
				using (var rs = await OpenDataReaderAsync(sql)) {
					return await ExecuteHtmlAsync(rs, options);
				}
			}

			// Get result in HTML table by DbCommand (see below for options)
			public IHtmlContent ExecuteHtml(DbCommand cmd, Dictionary<string, object> options = null)
			{
				using (var rs = cmd.ExecuteReader()) {
					return ExecuteHtml(rs, options);
				}
			}

			// Get result in HTML table by DbCommand (see below for options)
			public async Task<IHtmlContent> ExecuteHtmlAsync(DbCommand cmd, Dictionary<string, object> options = null)
			{
				using (var rs = await cmd.ExecuteReaderAsync()) {
					return await ExecuteHtmlAsync(rs, options);
				}
			}

			/// <summary>
			/// Get result in HTML table (async)
			/// </summary>
			/// <param name="dr">Data reader</param>
			/// <param name="options">
			/// Dictionary of options:
			/// "fieldcaption" (bool|Dictionary)
			///   true: Use caption and use language object, or
			///   false: Use field names directly, or
			///   Dictionary of fieid caption for looking up field caption by field name
			/// "horizontal" (bool) Whether HTML table is horizontal, default: false
			/// "tablename" (string|List&lt;string&gt;) Table name(s) for the language object
			/// "tableclass" (string) CSS class names of the table, default: "table table-bordered ew-db-table"
			/// </param>
			/// <returns>Task that returns IHtmlContent</returns>

			public async Task<IHtmlContent> ExecuteHtmlAsync(DbDataReader dr, Dictionary<string, object> options = null)
			{
				if (dr == null || !dr.HasRows || dr.FieldCount < 1)
					return null;
				options = options ?? new Dictionary<string, object>();
				var horizontal = options.TryGetValue("horizontal", out object horiz) && ConvertToBool(horiz);
				string html = "", vhtml = "";
				int cnt = 0;
				string classname = (options.TryGetValue("tableclass", out object tc) && !Empty(tc)) ? Convert.ToString(tc) : TableClass;
				if (await dr.ReadAsync()) { // First row
					cnt++;

					// Vertical table
					vhtml = "<table class=\"" + classname + "\"><tbody>";
					for (var i = 0; i < dr.FieldCount; i++) {
						vhtml += "<tr>";
						vhtml += "<td>" + GetFieldCaption(dr.GetName(i)) + "</td>";
						vhtml += "<td>" + Convert.ToString(dr[i]) + "</td></tr>";
					}
					vhtml += "</tbody></table>";

					// Horizontal table
					html = "<table class=\"" + classname + "\">";
					html += "<thead><tr>";
					for (var i = 0; i < dr.FieldCount; i++)
						html += "<th>" + GetFieldCaption(dr.GetName(i)) + "</th>";
					html += "</tr></thead>";
					html += "<tbody>";
					html += "<tr>";
					for (var i = 0; i < dr.FieldCount; i++)
						html += "<td>" + Convert.ToString(dr[i]) + "</td>";
					html += "</tr>";
				}
				while (await dr.ReadAsync()) { // Other rows
					cnt++;
					html += "<tr>";
					for (var i = 0; i < dr.FieldCount; i++)
						html += "<td>" + Convert.ToString(dr[i]) + "</td>";
					html += "</tr>";
				}
				if (html != "")
					html += "</tbody></table>";
				var str = (cnt > 1 || horizontal) ? html : vhtml;
				return new HtmlString(str);

				// Local function to get field caption
				string GetFieldCaption(string key) {
					string caption = "";
					bool hasTableName = options.TryGetValue("tablename", out object tablename) && tablename != null;
					bool useFieldCaption = options.TryGetValue("fieldcaption", out object captions) && captions != null;
					if (useFieldCaption) {
						if (captions is Dictionary<string, string> d) {
							caption = d[key];
						} else if (hasTableName && !Empty(Language)) {
							if (tablename is IEnumerable<string> names) {
								foreach (string name in names) {
									caption = Language.FieldPhrase(name, key, "FldCaption");
									if (!Empty(caption))
										break;
								}
							} else if (tablename is string name) {
								caption = Language.FieldPhrase(name, key, "FldCaption");
							}
						}
					}
					return !Empty(caption) ? caption : key;
				}
			}

			/// <summary>
			/// Get result in HTML table (async)
			/// </summary>
			/// <param name="dr">Data reader</param>
			/// <param name="options">
			/// Dictionary of options:
			/// "fieldcaption" (bool|Dictionary)
			///   true: Use caption and use language object, or
			///   false: Use field names directly, or
			///   Dictionary of fieid caption for looking up field caption by field name
			/// "horizontal" (bool) Whether HTML table is horizontal, default: false
			/// "tablename" (string|List&lt;string&gt;) Table name(s) for the language object
			/// "tableclass" (string) CSS class names of the table, default: "table table-bordered ew-db-table"
			/// </param>
			/// <returns>HTML string as IHtmlContent</returns>

			public IHtmlContent ExecuteHtml(DbDataReader dr, Dictionary<string, object> options = null) =>
				ExecuteHtmlAsync(dr, options).GetAwaiter().GetResult();

			// Close
			public void Close()
			{
				using (_trans) {} // Dispose
				using (Conn) {} // Dispose
				using (_conn) {
					foreach (R dr in _dataReader) {
						using (dr) {} // Dispose
					}
					foreach (M cmd in _command) {
						using (cmd) {} // Dispose
					}
				}
			}

			// Releases all resources used by this object
			public void Dispose()
			{
				GC.SuppressFinalize(this);
				this.Dispose(true);
			}

			// Custom Dispose method to clean up unmanaged resources
			protected virtual void Dispose(bool disposing)
			{
				if (_disposed)
					return;
				if (disposing)
					Close();
				_disposed = true;
			}

			#pragma warning disable 162

			// Get connecton string from connection info
			public string GetConnectionString(ConfigurationSection Info) {
				var connectionString = Info["connectionstring"];
				string uid = Info["username"];
				string pwd = Info["password"];
				if (Config.EncryptionEnabled) {
					uid = AesDecrypt(uid, Config.EncryptionKey);
					pwd = AesDecrypt(pwd, Config.EncryptionKey);
				}
				return connectionString.Replace("{uid}", uid).Replace("{pwd}", pwd);
			}

			#pragma warning restore 162

			// Database Connecting event
			public virtual void Database_Connecting(ref string connstr) {

				// Check Info["id"] for database ID if more than one database
			}

			// Database Connected event
			public virtual void Database_Connected(DbConnection conn) {

				//Execute("Your SQL", conn);
			}
		}

		// Resize binary to thumbnail (interpolation deprecated)
		public static bool ResizeBinary(ref byte[] filedata, ref int width, ref int height, int interpolation, List<Action<MagickImage>> plugins = null) =>
			ResizeBinary(ref filedata, ref width, ref height, plugins);

		// Resize binary to thumbnail
		public static bool ResizeBinary(ref byte[] filedata, ref int width, ref int height, List<Action<MagickImage>> plugins = null) {
			try {
				MagickImage img = new MagickImage(filedata);
				GetResizeDimension(img.Width, img.Height, ref width, ref height);
				if (width > 0 && height > 0) {
					MagickGeometry size = new MagickGeometry(width, height);
					size.IgnoreAspectRatio = Config.ResizeIgnoreAspectRatio;
					size.Less = Config.ResizeLess;
					img.Resize(size);
				}
				if (IsList(plugins))
					plugins.ForEach(p => p(img));
				filedata = img.ToByteArray();
				width = img.Width;
				height = img.Height;
				return true;
			} catch {

				//if (Config.Debug) throw;
				return false;
			}
		}

		// Resize file to thumbnail file (interpolation deprecated)
		public static bool ResizeFile(string fn, string tn, ref int width, ref int height, int interpolation) =>
			ResizeFile(fn, tn, ref width, ref height);

		// Resize file to thumbnail file
		public static bool ResizeFile(string fn, ref int width, ref int height) =>
			ResizeFile(fn, fn, ref width, ref height);

		// Resize file to thumbnail file
		public static bool ResizeFile(string fn, string tn, ref int width, ref int height) {
			if (!FileExists(fn))
				return false;
			try {
				MagickImage img = new MagickImage(fn);
				GetResizeDimension(img.Width, img.Height, ref width, ref height);
				if (width > 0 && height > 0) {
					MagickGeometry size = new MagickGeometry(width, height);
					size.IgnoreAspectRatio = Config.ResizeIgnoreAspectRatio;
					size.Less = Config.ResizeLess;
					img.Resize(size);
					img.Write(tn);
					width = img.Width;
					height = img.Height;
				} else {
					FileCopy(fn, tn, false); // No resize, just use the original file
				}
				return true;
			} catch {

				//if (Config.Debug) throw;
				return false;
			}
		}

		// Resize file to binary (interpolation deprecated)
		public static byte[] ResizeFileToBinary(string fn, ref int width, ref int height, int interpolation) =>
			ResizeFileToBinary(fn, ref width, ref height);

		// Resize file to binary
		public static byte[] ResizeFileToBinary(string fn, ref int width, ref int height) {
			if (!FileExists(fn))
				return null;
			try {
				MagickImage img = new MagickImage(fn);
				if (width > 0 || height > 0) {
					MagickGeometry size = new MagickGeometry(width, height);
					size.IgnoreAspectRatio = Config.ResizeIgnoreAspectRatio;
					size.Less = Config.ResizeLess;
					img.Resize(size);
					width = img.Width;
					height = img.Height;
				}
				return img.ToByteArray();
			} catch {

				//if (Config.Debug) throw;
				return null;
			}
		}

		// Set up resize width/height
		private static void GetResizeDimension(int imageWidth, int imageHeight, ref int resizeWidth, ref int resizeHeight) {
			if (resizeWidth <= 0) { // maintain aspect ratio
				resizeWidth = ConvertToInt(imageWidth * resizeHeight / imageHeight);
			} else if (resizeHeight <= 0) { // maintain aspect ratio
				resizeHeight = ConvertToInt(imageHeight * resizeWidth / imageWidth);
			}
		}

		/// <summary>
		/// Basic Search class
		/// </summary>

		public class BasicSearch
		{
			public string TableVar = "";
			public bool BasicSearchAnyFields = Config.BasicSearchAnyFields;
			public string KeywordDefault = "";
			public string TypeDefault = "";
			private string _prefix = "";
			private string _keyword = "";
			private string _type = "";

			// Constructor
			public BasicSearch(string tblvar) {
				TableVar = tblvar;
				_prefix = Config.ProjectName + "_" + tblvar + "_";
			}

			// Session variable name
			protected string GetSessionName(string suffix) => _prefix + suffix;

			// Load default
			public void LoadDefault() {
				_keyword = KeywordDefault;
				_type = TypeDefault;
				if (Session[GetSessionName(Config.TableBasicSearchType)] == null && !Empty(TypeDefault)) // Save default to session
					SessionType = TypeDefault;
			}

			// Unset session
			public void UnsetSession() {
				Session.Remove(GetSessionName(Config.TableBasicSearchType));
				Session.Remove(GetSessionName(Config.TableBasicSearch));
			}

			// Isset session
			public bool IssetSession => Session[GetSessionName(Config.TableBasicSearch)] != null;

			// Session Keyword
			public string SessionKeyword {
				get => Convert.ToString(Session[GetSessionName(Config.TableBasicSearch)]);
				set {
					_keyword = value;
					Session[GetSessionName(Config.TableBasicSearch)] = value;
				}
			}

			// Set Keyword
			public void SetKeyword(string value, bool save = true) {
				if (Config.RemoveXss)
					value = RemoveXss(value);
				_keyword = value;
				if (save)
					SessionKeyword = value;
			}

			// Keyword
			public string Keyword {
				get => _keyword;
				set => SetKeyword(value, false);
			}

			// Type
			public string SessionType {
				get => Convert.ToString(Session[GetSessionName(Config.TableBasicSearchType)]);
				set {
					_type = value;
					Session[GetSessionName(Config.TableBasicSearchType)] = value;
				}
			}

			// Set type
			public void SetType(string value, bool save = true) {
				if (Config.RemoveXss)
					value = RemoveXss(value);
				_type = value;
				if (save)
					SessionType = value;
			}

			// Type
			public string Type {
				get => _type;
				set => SetType(value, false);
			}

			// Get type name
			public string TypeName {
				get {
					switch (SessionType) {
						case "=": return Language.Phrase("QuickSearchExact");
						case "AND": return Language.Phrase("QuickSearchAll");
						case "OR": return Language.Phrase("QuickSearchAny");
						default: return Language.Phrase("QuickSearchAuto");
					}
				}
			}

			// Get short type name
			public string TypeNameShort {
				get {
					string typname;
					switch (SessionType) {
						case "=": typname = Language.Phrase("QuickSearchExactShort"); break;
						case "AND": typname = Language.Phrase("QuickSearchAllShort"); break;
						case "OR": typname = Language.Phrase("QuickSearchAnyShort"); break;
						default: typname = Language.Phrase("QuickSearchAutoShort"); break;
					}
					if (!Empty(typname))
						typname += "&nbsp;";
					return typname;
				}
			}

			// Get keyword list
			public List<string> KeywordList(bool def = false) {
				string searchKeyword = def ? KeywordDefault : Keyword;
				string searchType = def ? TypeDefault : Type;
				List<string> ar = new List<string>();
				if (!Empty(searchKeyword)) {
					string search = searchKeyword.Trim();
					if (!SameString(searchType, "=")) {

						// Match quoted keywords (i.e.: "...")
						foreach (Match match in Regex.Matches(search, @"""([^""]*)""", RegexOptions.IgnoreCase)) {
							int p = match.Index;
							string str = search.Substring(0, p);
							search = search.Substring(p + match.Length);
							if (str.Trim().Length > 0)
								ar.AddRange(str.Trim().Split(' '));
							ar.Add(match.Groups[1].Value); // Save quoted keyword
						}

						// Match individual keywords
						if (search.Trim().Length > 0)
							ar.AddRange(search.Trim().Split(' '));
					} else {
						ar.Add(search);
					}
				}
				return ar;
			}

			// save
			public void Save() {
				SessionKeyword = _keyword;
				SessionType = _type;
			}

			// Load
			public void Load() {
				_keyword = SessionKeyword;
				_type = SessionType;
			}

			// Convert to JSON
			public string ToJson() {
				var d = new Dictionary<string, string>();
				if (!Empty(_keyword)) {
					d.Add(Config.TableBasicSearch, _keyword);
					d.Add(Config.TableBasicSearchType, _type);
				}
				return ConvertToJson(d);
			}
		}

		/// <summary>
		/// Advanced Search class
		/// </summary>

		public class AdvancedSearch
		{
			private string _searchValue = null; // Search value
			private string _searchOperator = "="; // Search operator
			private string _searchCondition = "AND"; // Search condition
			private string _searchValue2 = null; // Search value 2
			private string _searchOperator2 = "="; // Search operator 2
			public string TableVar = "";
			public string FieldVar = ""; // FldParm
			public object ViewValue = null; // View value
			public object ViewValue2 = null; // View value 2
			public string SearchValueDefault = null; // Search value default
			public string SearchOperatorDefault = ""; // Search operator default
			public string SearchConditionDefault = ""; // Search condition default
			public string SearchValue2Default = null; // Search value 2 default
			public string SearchOperator2Default = ""; // Search operator 2 default
			protected string _prefix = "";
			protected string _suffix = "";

			// Constructor
			public AdvancedSearch(string tblvar, string fldvar) {
				TableVar = tblvar;
				FieldVar = fldvar;
				_prefix = Config.ProjectName + "_" + tblvar + "_" + Config.TableAdvancedSearch + "_";
				_suffix = "_" + fldvar;
			}

			// Set SearchValue
			public string SearchValue {
				get => _searchValue;
				set {
					if (Config.RemoveXss)
						value = RemoveXss(value);
					_searchValue = value;
				}
			}

			// Set SearchOperator
			public string SearchOperator {
				get => _searchOperator;
				set {
					if (IsValidOperator(value))
						_searchOperator = value;
				}
			}

			// Set SearchCondition
			public string SearchCondition {
				get => _searchCondition;
				set {
					if (Config.RemoveXss)
						value = RemoveXss(value);
					_searchCondition = value;
				}
			}

			// Set SearchValue2
			public string SearchValue2 {
				get => _searchValue2;
				set {
					if (Config.RemoveXss)
						value = RemoveXss(value);
					_searchValue2 = value;
				}
			}

			// Set SearchOperator2
			public string SearchOperator2 {
				get => _searchOperator2;
				set {
					if (IsValidOperator(value))
						_searchOperator2 = value;
				}
			}

			// Session variable name
			public string GetSessionName(string infix) => _prefix + infix + _suffix;

			// Unset session
			public void UnsetSession() {
				Session.Remove(GetSessionName("x"));
				Session.Remove(GetSessionName("z"));
				Session.Remove(GetSessionName("v"));
				Session.Remove(GetSessionName("y"));
				Session.Remove(GetSessionName("w"));
			}

			// Isset session
			public bool IssetSession => Session[GetSessionName("x")] != null || Session[GetSessionName("y")] != null;

			// Save to session
			public void Save() {
				if (!SameString(Session[GetSessionName("x")], SearchValue))
					Session[GetSessionName("x")] = SearchValue;
				if (!SameString(Session[GetSessionName("y")], SearchValue2))
					Session[GetSessionName("y")] = SearchValue2;
				if (!SameString(Session[GetSessionName("z")], SearchOperator))
					Session[GetSessionName("z")] = SearchOperator;
				if (!SameString(Session[GetSessionName("v")], SearchCondition))
					Session[GetSessionName("v")] = SearchCondition;
				if (!SameString(Session[GetSessionName("w")], SearchOperator2))
					Session[GetSessionName("w")] = SearchOperator2;
			}

			// Load from session
			public void Load() {
				SearchValue = Convert.ToString(Session[GetSessionName("x")]);
				SearchOperator = Convert.ToString(Session[GetSessionName("z")]);
				SearchCondition = Convert.ToString(Session[GetSessionName("v")]);
				SearchValue2 = Convert.ToString(Session[GetSessionName("y")]);
				SearchOperator2 = Convert.ToString(Session[GetSessionName("w")]);
			}

			// Get value (infix = "x|y")
			public string GetValue(string infix = "x") => Convert.ToString(Session[GetSessionName(infix)]);

			// Load default values
			public void LoadDefault() {
				if (!Empty(SearchValueDefault)) SearchValue = SearchValueDefault;
				if (!Empty(SearchOperatorDefault)) SearchOperator = SearchOperatorDefault;
				if (!Empty(SearchConditionDefault)) SearchCondition = SearchConditionDefault;
				if (!Empty(SearchValue2Default)) SearchValue2 = SearchValue2Default;
				if (!Empty(SearchOperator2Default)) SearchOperator2 = SearchOperator2Default;
			}

			// Convert to JSON
			public string ToJson() {
				var d = new Dictionary<string, string>();
				if (!Empty(SearchValue) || !Empty(SearchValue2)) {
					d.Add("x" + _suffix, SearchValue);
					d.Add("z" + _suffix, SearchOperator);
					d.Add("v" + _suffix, SearchCondition);
					d.Add("y" + _suffix, SearchValue2);
					d.Add("w" + _suffix, SearchOperator2);
				}
				return ConvertToJson(d);
			}
			protected bool IsValidOperator(string opr) => (new List<string> { "=", "<>", "<", "<=", ">", ">=", "LIKE", "NOT LIKE", "STARTS WITH", "ENDS WITH", "IS NULL", "IS NOT NULL", "BETWEEN" }).Contains(opr);
		}

		/// <summary>
		/// Upload class
		/// </summary>

		public class HttpUpload
		{
			public int Index = -1; // Index to handle multiple form elements
			public DbField Parent; // Parent field object
			public string UploadPath; // Upload path
			public object DbValue = System.DBNull.Value; // Value from database // DN
			public string Message = ""; // Error message
			public object Value; // Upload value
			public string FileToken = ""; // Upload file token (API only)
			public string FileName = ""; // Upload file name
			public long FileSize; // Upload file size
			public string ContentType = ""; // File content type
			public int ImageWidth = -1; // Image width
			public int ImageHeight = -1; // Image height
			public bool UploadMultiple = false; // Multiple upload
			public bool KeepFile = true; // Keep old file
			public List<Action<MagickImage>> Plugins = new List<Action<MagickImage>>(); // Plugins for Resize()

			// Contructor
			public HttpUpload(DbField fld = null)
			{
				if (!Empty(fld)) {
					Parent = fld;
					UploadMultiple = fld.UploadMultiple; // DN
				}
			}

			// Is empty
			public bool IsEmpty => DbValue == System.DBNull.Value;

			// Check the file type of the uploaded file
			private bool UploadAllowedFileExtensions(string fileName) => CheckFileType(fileName);

			// Upload file
			public async Task<bool> UploadFile()
			{
				try {
					var fldvar = (Index < 0) ? Parent.FieldVar : Parent.FieldVar.Substring(0, 1) + Index + Parent.FieldVar.Substring(1);

					// Get file from token or FormData for API request
					if (IsApi()) {
						if (!Empty(Post(Parent.Name))) {
							KeepFile = false;
							FileToken = Post(Parent.Name);
							FileName = GetUploadedFileName(FileToken);
						} else {
							KeepFile = !await GetUploadedFiles(Parent.Name);
						}
					} else {
						Value = System.DBNull.Value; // Reset first
						Message = ""; // Reset first
						var wrkvar = "fn_" + fldvar;
						FileName = Post(wrkvar); // Get file name
						wrkvar = "fa_" + fldvar;
						KeepFile = Post(wrkvar) == "1"; // Check if keep old file
					}
					if (!KeepFile && !Empty(FileName) && !UploadMultiple) {
						var f = !Empty(FileToken) ? UploadTempPath(FileToken) : UploadTempPath(fldvar, Parent.TableVar) + FileName;
						var fi = GetFileInfo(f);
						if (fi.Exists) {
							Value = await FileReadAllBytes(f);
							FileSize = fi.Length;
							ContentType = ContentType((byte[])Value, f);
							try {
								System.Drawing.Image img = System.Drawing.Image.FromFile(f);
								ImageWidth = Convert.ToInt32(img.PhysicalDimension.Width);
								ImageHeight = Convert.ToInt32(img.PhysicalDimension.Height);
							} catch {}
						}
					}
					return true;
				} catch (Exception e) {
					Message = e.Message;
					return false;
				}
			}

			/// <summary>
			/// Get uploaded files info
			/// </summary>
			/// <param name="name">Name of file. If unspecified, all uploaded files info is returned.</param>
			/// <returns>Action result</returns>

			public async Task<JsonBoolResult> GetUploadedFiles(string name = "")
			{

				// Validate request
				if (Files.Count <= 0)
					return new JsonBoolResult(new { success = false, error = "No uploaded files" }, false);

				// Language object
				Language = Language ?? new Lang();

				// Create temp folder
				string token = Random().ToString();
				string path = UploadTempPath(token);
				if (!CreateFolder(path))
					return new JsonBoolResult( new { success = false, error = "Create folder '" + path + "' failed." }, false);
				var files = new Dictionary<string, object>();
				CurrentForm = CurrentForm ?? new HttpForm();

				// Move files to temp folder
				var filelist = new List<Dictionary<string, object>>();
				bool res = true;
				for (int i = 0; i < Files.Count; i++) {
					var file = Files[i];
					if (SameText(file.FileName, name) || Empty(name)) {
						string fileName = file.FileName;
						long fileSize = file.Length;
						var d = new Dictionary<string, object>();
						d.Add("name", fileName);
						if (!UploadAllowedFileExtensions(fileName)) { // check file extensions
							d.Add("success", false);
							d.Add("error", Language.Phrase("UploadErrMsgAcceptFileTypes"));
							res = false;
						} else if (Config.MaxFileSize > 0 && fileSize > Config.MaxFileSize) { // check file size
							d.Add("success", false);
							d.Add("error", Language.Phrase("UploadErrMsgMaxFileSize"));
							res = false;
						} else {
							d.Add("success", await CurrentForm.SaveUploadFile(file.Name, IncludeTrailingDelimiter(path, true) + fileName, i));
						}
						filelist.Add(d);
					}
				}
				files.Add("importfiles", filelist);
				var result = new Dictionary<string, object>();
				FileToken = token;
				result.Add(Config.ApiFileTokenName, token);
				result.Add("files", files);
				return new JsonBoolResult(result, res);
			}

			/// <summary>
			/// Get uploaded file name
			/// </summary>
			/// <param name="token">File token to locate the uploaded temp path</param>
			/// <param name="path">Specifies file name with or without full path</param>
			/// <returns>The file name</returns>

			public string GetUploadedFileName(string token, bool path = false)
			{
				if (Empty(token)) { // Remove
					return "";
				} else { // Load file name from token
					string tempPath = UploadTempPath(token);
					try {
						if (IsDirectory(tempPath)) { // Get all files in the folder
							var files = SearchFiles(tempPath, "*.*");
							return String.Join(Config.MultipleUploadSeparator, files.Select(fn => path ? fn : Path.GetFileNameWithoutExtension(fn)));
						}
					} catch (Exception e) {
						if (Config.Debug)
							Message = e.Message;
						return "";
					}
					return "";
				}
			}

			/// <summary>
			/// Resize image
			/// </summary>
			/// <param name="width">Target width of image</param>
			/// <param name="height">Target height of image</param>
			/// <returns>Whether file is resized successfully</returns>

			public bool Resize(int width, int height)
			{
				bool result = false;
				if (!IsDBNull(Value)) {
					int wrkWidth = width;
					int wrkHeight = height;
					byte[] data = (byte[])Value;
					result = ResizeBinary(ref data, ref wrkWidth, ref wrkHeight, Plugins);
					if (result) {
						Value = data;
						if (wrkWidth > 0 && wrkHeight > 0) {
							ImageWidth = wrkWidth;
							ImageHeight = wrkHeight;
						}
						FileSize = data.Length;
					}
				}
				return result;
			}

			/// <summary>
			/// Get file count
			/// </summary>
			/// <value>Uploaded file count</value>

			public int Count {
				get {
					if (!UploadMultiple && !Empty(Value)) {
						return 1;
					} else if (UploadMultiple && FileName != "") {
						return FileName.Split(Config.MultipleUploadSeparator).Length;
					}
					return 0;
				}
			}

			/// <summary>
			/// Get temp file path
			/// </summary>
			/// <param name="idx">Index of file</param>
			/// <returns>Temp file path of the uploaded file</returns>

			public string GetTempFile(int idx = 0) {
				string fldvar = (Index < 0) ? Parent.FieldVar : Parent.FieldVar.Substring(0, 1) + Index + Parent.FieldVar.Substring(1);
				if (FileName != "") {
					if (UploadMultiple) {
						var ar = FileName.Split(Config.MultipleUploadSeparator);
						if (idx > -1 && idx < ar.Length) {
							return UploadTempPath(fldvar, Parent.TableVar) + ar[idx];
						} else {
							return null;
						}
					} else {
						return UploadTempPath(fldvar, Parent.TableVar) + FileName;
					}
				}
				return null;
			}

			/// <summary>
			/// Get temp file paths
			/// </summary>
			/// <returns>Temp file paths of all uploaded files</returns>

			public List<string> GetTempFiles() {
				string fldvar = Parent.FieldVar;
				if (FileName != "") {
					if (UploadMultiple) {
						var files = FileName.Split(Config.MultipleUploadSeparator);
						return files.Select(fn => UploadTempPath(fldvar, Parent.TableVar) + fn).ToList();
					} else {
						string file = UploadTempPath(fldvar, Parent.TableVar) + FileName;
						return new List<string> { file };
					}
				}
				return null;
			}

			/// <summary>
			/// Save uploaded data to file (path relative to application root)
			/// </summary>
			/// <param name="newFileName">New file name</param>
			/// <param name="overWrite">Overwrite file or not</param>
			/// <param name="idx">Index of file</param>
			/// <returns>Whether file is saved successfully</returns>

			public async Task<bool> SaveToFile(string newFileName, bool overWrite, int idx = 0)
			{
				string path = ServerMapPath(Empty(UploadPath) ? Parent.UploadPath : UploadPath);
				if (!IsDBNull(Value)) {
					if (Empty(newFileName))
						newFileName = FileName;
					byte[] data = (byte[])Value;
					if (!overWrite)
						newFileName = UploadFileName(path, newFileName);
					return await SaveFile(path, newFileName, data);
				} else if (idx >= 0) { // Use file from upload temp folder
					var file = (string)GetTempFile(idx);
					if (FileExists(file)) {
						if (!overWrite)
							newFileName = UploadFileName(path, newFileName);
						return CopyFile(path, newFileName, file, overWrite); // DN
					}
				}
				return false;
			}

			/// <summary>
			/// Resize and save uploaded data to file
			/// </summary>
			/// <param name="width">Target width of image</param>
			/// <param name="height">Target height of image</param>
			/// <param name="newFileName">New file name</param>
			/// <param name="overwrite">Overwrite existing file or not</param>
			/// <param name="idx">Index of file</param>
			/// <returns>Whether file is resized and saved successfully</returns>

			public async Task<bool> ResizeAndSaveToFile(int width, int height, string newFileName, bool overwrite, int idx = 0)
			{
				var result = false;
				if (!IsDBNull(Value)) {

					// Save old values
					var oldValue = Value;
					var oldWidth = ImageWidth;
					var oldHeight = ImageHeight;
					var oldFileSize = FileSize;
					try {
						Resize(width, height);
						result = await SaveToFile(newFileName, overwrite);
					} finally { // Restore old values
						Value = oldValue;
						ImageWidth = oldWidth;
						ImageHeight = oldHeight;
						FileSize = oldFileSize;
					}
				} else if (idx >= 0) { // Use file from upload temp folder
					var file = GetTempFile(idx);
					if (FileExists(file)) {
						Value = await FileReadAllBytes(file);
						Resize(width, height);
						try {
							result = await SaveToFile(newFileName, overwrite);
						} finally {
							Value = System.DBNull.Value;
						}
					}
				}
				return result;
			}
		}

		/// <summary>
		/// Session handler
		/// </summary>

		public class SessionHandler
		{

			// Page class constructor
			public SessionHandler(Controller controller = null) { // DN
				if (controller != null)
					Controller = controller;
			}

			// Get session value
			public IActionResult GetSession()
			{
				DateTime dt = DateTime.Now;
				return Controller?.Content(Encrypt(dt.Ticks));
			}
		}

		/// <summary>
		/// Get content type
		/// </summary>
		/// <param name="data">File data</param>
		/// <param name="fn">File name</param>
		/// <returns>Content type</returns>

		public static string ContentType(byte[] data, string fn = "") {
			string result = null;
			if (data != null) {
				result = data.DetectMimeType()?.Mime; // Use MimeDetective.InMemory
			} else if (fn != "") {
				result = ContentType(fn);
			}
			return result ?? Config.DefaultMimeType;
		}

		/// <summary>
		/// Get content type
		/// </summary>
		/// <param name="fn">File name</param>
		/// <returns>Content type</returns>

		public static string ContentType(string fn) => ContentTypeProvider.TryGetContentType(fn, out string contentType) ? contentType : Config.DefaultMimeType; // DN

		// Return multi-value search SQL
		public static string GetMultiSearchSql(DbField fld, string fldOpr, string fldVar, string dbid = "DB")
		{
			if (fldOpr == "IS NULL" || fldOpr == "IS NOT NULL") {
				return fld.Expression + " " + fldOpr;
			} else {
				string sql;
				string wrk = "";
				string[] arVal = fldVar.Split(',');
				string dbtype = GetConnectionType(dbid);
				if (fldOpr == "LIKE")
					wrk = "";
				else
					wrk = fld.Expression + SearchString(fldOpr, fldVar, Config.DataTypeString, dbid);
				for (int i = 0; i < arVal.Length; i++) {
					string val = arVal[i].Trim();
					if (val == Config.NullValue) {
						sql = fld.Expression + " IS NULL";
					} else if (val == Config.NotNullValue) {
						sql = fld.Expression + " IS NOT NULL";
					} else {
						if (fldOpr == "LIKE") {
							if (dbtype == "MYSQL") {
								sql = "FIND_IN_SET('" + AdjustSql(val, dbid) + "', " + fld.Expression + ")";
							} else {
								if (arVal.Length == 1 || Config.SearchMultiValueOption == 3) {
									sql = fld.Expression + " = '" + AdjustSql(val, dbid) + "' OR " + GetMultiSearchSqlPart(fld, val, dbid);
								} else {
									sql = GetMultiSearchSqlPart(fld, val, dbid);
								}
							}
						} else {
							sql = fld.Expression + SearchString(fldOpr, val, Config.DataTypeString, dbid);
						}
					}
					if (!Empty(wrk)) {
						if (Config.SearchMultiValueOption == 2) {
							wrk = wrk + " AND ";
						} else if (Config.SearchMultiValueOption == 3) {
							wrk = wrk + " OR ";
						}
					}
					wrk = wrk + "(" + sql + ")";
				}
				return wrk;
			}
		}

		// Get multi search SQL part
		public static string GetMultiSearchSqlPart(DbField fld, string fldVar, string dbid = "DB")
		{
			return fld.Expression + Like("'" + AdjustSql(fldVar, dbid) + ",%'", dbid) + " OR " +
				fld.Expression + Like("'%," + AdjustSql(fldVar, dbid) + ",%'", dbid) + " OR " +
				fld.Expression + Like("'%," + AdjustSql(fldVar, dbid) + "'", dbid);
		}

		// Check if float format
		public static bool IsFloatFormat(int fldType) => (fldType == 4 || fldType == 5 || fldType == 131 || fldType == 6);

		// Get search SQL
		public static string GetSearchSql(DbField fld, string fldVal, string fldOpr, string fldCond, string fldVal2, string fldOpr2, string dbid = "DB")
		{
			string sql = "";
			bool isVirtual = (fld.IsVirtual && fld.VirtualSearch);
			string fldExpression = isVirtual ? fld.VirtualExpression : fld.Expression;
			int fldDataType = fld.DataType;
			if (IsFloatFormat(fld.Type)) {
				fldVal = ConvertToFloatString(fldVal);
				fldVal2 = ConvertToFloatString(fldVal2);
			}
			if (isVirtual)
				fldDataType = Config.DataTypeString;
			if (fldDataType == Config.DataTypeNumber) { // Fix wrong operator
				if (fldOpr == "LIKE" || fldOpr == "STARTS WITH" || fldOpr == "ENDS WITH") {
					fldOpr = "=";
				} else if (fldOpr == "NOT LIKE") {
					fldOpr = "<>";
				}
				if (fldOpr2 == "LIKE" || fldOpr2 == "STARTS WITH" || fldOpr == "ENDS WITH") {
					fldOpr2 = "=";
				} else if (fldOpr2 == "NOT LIKE") {
					fldOpr2 = "<>";
				}
			}
			if (fldOpr == "BETWEEN") {
				var IsValidValue = (fldDataType != Config.DataTypeNumber) ||
					(fldDataType == Config.DataTypeNumber && IsNumeric(fldVal) && IsNumeric(fldVal2));
				if (!Empty(fldVal) && !Empty(fldVal2) && IsValidValue)
					sql = fldExpression + " BETWEEN " + QuotedValue(fldVal, fldDataType, dbid) +
						" AND " + QuotedValue(fldVal2, fldDataType, dbid);
			} else {

				// Handle first value
				if (fldVal == Config.NullValue || fldOpr == "IS NULL") {
					sql = fld.Expression + " IS NULL";
				} else if (fldVal == Config.NotNullValue || fldOpr == "IS NOT NULL") {
					sql = fld.Expression + " IS NOT NULL";
				} else {
					var IsValidValue = (fldDataType != Config.DataTypeNumber) ||
						(fldDataType == Config.DataTypeNumber && IsNumeric(fldVal));
					if (!Empty(fldVal) && IsValidValue && IsValidOperator(fldOpr, fldDataType)) {
						sql = fldExpression + SearchString(fldOpr, fldVal, fldDataType, dbid);
						if (fld.DataType == Config.DataTypeBoolean && fldVal == fld.FalseValue && fldOpr == "=")
							sql = "(" + sql + " OR " + fldExpression + " IS NULL)";
					}
				}

				// Handle second value
				string sql2 = "";
				if (fldVal2 == Config.NullValue || fldOpr2 == "IS NULL") {
					sql2 = fld.Expression + " IS NULL";
				} else if (fldVal2 == Config.NotNullValue || fldOpr2 == "IS NOT NULL") {
					sql2 = fld.Expression + " IS NOT NULL";
				} else {
					var IsValidValue = (fldDataType != Config.DataTypeNumber) ||
						(fldDataType == Config.DataTypeNumber && IsNumeric(fldVal2));
					if (!Empty(fldVal2) && IsValidValue && IsValidOperator(fldOpr2, fldDataType)) {
						sql2 = fldExpression + SearchString(fldOpr2, fldVal2, fldDataType, dbid);
						if (fld.DataType == Config.DataTypeBoolean && fldVal2 == fld.FalseValue && fldOpr2 == "=")
							sql2 = "(" + sql2 + " OR " + fldExpression + " IS NULL)";
					}
				}

				// Combine SQL
				if (!Empty(sql2)) {
					if (!Empty(sql))
						sql = "(" + sql + " " + ((fldCond == "OR") ? "OR" : "AND") + " " + sql2 + ")";
					else
						sql = sql2;
				}
			}
			return sql;
		}

		// Return search string
		public static string SearchString(string fldOpr, string fldVal, int fldType, string dbid = "DB")
		{
			if (fldVal == Config.NullValue || fldOpr == "IS NULL") {
				return " IS NULL";
			} else if (fldVal == Config.NotNullValue || fldOpr == "IS NOT NULL") {
				return " IS NOT NULL";
			} else if (fldOpr == "LIKE") {
				return Like(QuotedValue("%" + fldVal + "%", fldType, dbid), dbid);
			} else if (fldOpr == "NOT LIKE") {
				return " NOT " + Like(QuotedValue("%" + fldVal + "%", fldType, dbid), dbid);
			} else if (fldOpr == "STARTS WITH") {
				return Like(QuotedValue(fldVal + "%", fldType, dbid), dbid);
			} else if (fldOpr == "ENDS WITH") {
				return Like(QuotedValue("%" + fldVal, fldType, dbid), dbid);
			} else {
				if (fldType == Config.DataTypeNumber && !IsNumeric(fldVal)) // Invalid field value
					return " = -1 AND 1 = 0"; // Always false
				else
					return " " + fldOpr + " " + QuotedValue(fldVal, fldType, dbid);
			}
		}

		// Check if valid operator
		public static bool IsValidOperator(string opr, int fldType)
		{
			bool valid = (opr == "=" || opr == "<" || opr == "<=" || opr == ">" || opr == ">=" || opr == "<>");
			if (fldType == Config.DataTypeString || fldType == Config.DataTypeMemo)
				valid = valid || opr == "LIKE" || opr == "NOT LIKE" || opr == "STARTS WITH" || opr == "ENDS WITH";
			return valid;
		}

		/// <summary>
		/// Get quoted table/field name
		/// </summary>
		/// <param name="name">Table/Field name</param>
		/// <param name="dbid">Database ID</param>
		/// <returns>Quoted name</returns>

		public static string QuotedName(string name, string dbid = "DB")
		{
			var db = Db(dbid);
			return db["qs"] + name.Replace(db["qe"], db["qe"] + db["qe"]) + db["qe"];
		}

		/// <summary>
		/// Get quoted field value
		/// </summary>
		/// <param name="value">Field value</param>
		/// <param name="fldType">Field type</param>
		/// <param name="dbid">Database ID</param>
		/// <returns>Quoted value</returns>

		public static string QuotedValue(object value, int fldType, string dbid = "DB")
		{
			string dbtype = GetConnectionType(dbid);
			switch (fldType) {
				case Config.DataTypeString:
				case Config.DataTypeMemo:
					if (Config.RemoveXss)
						value = RemoveXss(value);
					if (dbtype == "MSSQL")
						return "N'" + AdjustSql(value, dbid) + "'";
					else
						return "'" + AdjustSql(value, dbid) + "'";
				case Config.DataTypeGuid:
					if (dbtype == "ACCESS") {
						if (Convert.ToString(value).StartsWith("{")) {
							return Convert.ToString(value);
						} else {
							return "{" + AdjustSql(value, dbid) + "}";
						}
					} else {
						return "'" + AdjustSql(value, dbid) + "'";
					}
				case Config.DataTypeDate:
				case Config.DataTypeTime:
					if (dbtype == "ACCESS") {
						return "#" + AdjustSql(value, dbid) + "#";
					} else if (dbtype == "ORACLE") {
						return "TO_DATE('" + AdjustSql(value, dbid) + "', 'YYYY/MM/DD HH24:MI:SS')";
					} else {
						return "'" + AdjustSql(value, dbid) + "'";
					}
				case Config.DataTypeBoolean:
					 if (dbtype == "MYSQL" || dbtype == "ORACLE") { // ENUM('Y','N'), ENUM('y','n'), ENUM('1'/'0')
							return "'" + AdjustSql(value, dbid) + "'";
					} else { // Boolean
							return Convert.ToString(value);
					}
				case Config.DataTypeNumber:
					if (IsNumeric(value))
						return Convert.ToString(value);
					else
						return "null"; // Treat as null
				default:
					return Convert.ToString(value);
			}
		}

		// Pad zeros before number
		public static string ZeroPad(object m, int t) => Convert.ToString(m).PadLeft(t, '0');

		// Cast date/time field for LIKE
		public static string CastDateFieldForLike(string fld, int namedformat, string dbid = "DB")
		{
			string dbtype = GetConnectionType(dbid);
			bool isDateTime = (namedformat == 1 || namedformat == 8); // Date/Time
			if ((new List<int> { 0, 1, 2, 8 }).Contains(namedformat))
				namedformat = DateFormatId;
			bool shortYear = (namedformat >= 12 && namedformat <= 17);
			isDateTime = isDateTime || (new List<int> { 9, 10, 11, 15, 16, 17 }).Contains(namedformat);
			string dateFormat = "", ds = DateSeparator, ts = TimeSeparator;
			switch (namedformat) {
				case 3:
					if (dbtype == "MYSQL") {
						dateFormat = $"%h{ts}%i{ts}%s %p";
					} else if (dbtype == "ACCESS") {
						dateFormat = $"hh{ts}nn{ts}ss AM/PM";
					} else if (dbtype == "MSSQL") {
						dateFormat = $"REPLACE(LTRIM(RIGHT(CONVERT(VARCHAR(19), {fld}, 0), 7)), ':', '{ts}')"; // Use hh:miAM (or PM) only or SQL too lengthy
					} else if (dbtype == "ORACLE") {
						dateFormat = $"HH{ts}MI{ts}SS AM";
					}
					break;
				case 4:
					if (dbtype == "MYSQL") {
						dateFormat = $"%H{ts}%i{ts}%s";
					} else if (dbtype == "ACCESS") {
						dateFormat = $"hh{ts}nn{ts}ss";
					} else if (dbtype == "MSSQL") {
						dateFormat = $"REPLACE(CONVERT(VARCHAR(8), {fld}, 108), ':', '{ts}')";
					} else if (dbtype == "ORACLE") {
						dateFormat = $"HH24{ts}MI{ts}SS";
					}
					break;
				case 5: case 9: case 12: case 15:
					if (dbtype == "MYSQL") {
						dateFormat = $"{(shortYear ? "%y" : "%Y")}{ds}%m{ds}%d";
						if (isDateTime) dateFormat += $" %H{ts}%i{ts}%s";
					} else if (dbtype == "ACCESS") {
						dateFormat = $"{(shortYear ? "yy" : "yyyy")}{ds}mm{ds}dd";
						if (isDateTime) dateFormat += $" hh{ts}nn{ts}ss";
					} else if (dbtype == "MSSQL") {
						dateFormat = $"REPLACE({(shortYear ? $"CONVERT(VARCHAR(8), {fld}, 2)" : $"CONVERT(VARCHAR(10), {fld}, 102)")}, '+', '{ds}')";
						if (isDateTime) dateFormat = $"({dateFormat} + ' ' + REPLACE(CONVERT(VARCHAR(8), {fld}, 108), ':', '{ts}'))";
					} else if (dbtype == "ORACLE") {
						dateFormat = $"{(shortYear ? "YY" : "YYYY")}{ds}MM{ds}DD";
						if (isDateTime) dateFormat += $" HH24{ts}MI{ts}SS";
					}
					break;
				case 6: case 10: case 13: case 16:
					if (dbtype == "MYSQL") {
						dateFormat = $"%m{ds}%d{ds}{(shortYear ? "%y" : "%Y")}";
						if (isDateTime) dateFormat += $" %H{ts}%i{ts}%s";
					} else if (dbtype == "ACCESS") {
						dateFormat = $"mm{ds}dd{ds}{(shortYear ? "yy" : "yyyy")}";
						if (isDateTime) dateFormat += $" hh{ts}nn{ts}ss";
					} else if (dbtype == "MSSQL") {
						dateFormat = $"REPLACE({(shortYear ? $"CONVERT(VARCHAR(8), {fld}, 1)" : $"CONVERT(VARCHAR(10), {fld}, 101)")}, '/', '{ds}')";
						if (isDateTime) dateFormat = $"({dateFormat} + ' ' + REPLACE(CONVERT(VARCHAR(8), {fld}, 108), ':', '{ts}'))";
					} else if (dbtype == "ORACLE") {
						dateFormat = $"MM{ds}DD{ds}{(shortYear ? "YY" : "YYYY")}";
						if (isDateTime) dateFormat += $" HH24{ts}MI{ts}SS";
					}
					break;
				case 7: case 11: case 14: case 17:
					if (dbtype == "MYSQL") {
						dateFormat = $"%d{ds}%m{ds}{(shortYear ? "%y" : "%Y")}";
						if (isDateTime) dateFormat += $" %H{ts}%i{ts}%s";
					} else if (dbtype == "ACCESS") {
						dateFormat = $"dd{ds}mm{ds}{(shortYear ? "yy" : "yyyy")}";
						if (isDateTime) dateFormat += $" hh{ts}nn{ts}ss";
					} else if (dbtype == "MSSQL") {
						dateFormat = $"REPLACE({(shortYear ? $"CONVERT(VARCHAR(8), {fld}, 3)" : $"CONVERT(VARCHAR(10), {fld}, 103)")}, '/', '{ds}')";
						if (isDateTime) dateFormat = $"({dateFormat} + ' ' + REPLACE(CONVERT(VARCHAR(8), {fld}, 108), ':', '{ts}'))";
					} else if (dbtype == "ORACLE") {
						dateFormat = $"DD{ds}MM{ds}{(shortYear ? "YY" : "YYYY")}";
						if (isDateTime) dateFormat += $" HH24{ts}MI{ts}SS";
					}
					break;
			}
			if (dateFormat != "") {
				if (dbtype == "MYSQL") {
					return $"DATE_FORMAT({fld}, '{dateFormat}')";
				} else if (dbtype == "ACCESS") {
					return $"FORMAT({fld}, '{dateFormat}')";
				} else if (dbtype == "MSSQL") {
					return dateFormat;
				} else if (dbtype == "ORACLE") {
					return $"TO_CHAR({fld}, '{dateFormat}')";
				}
			}
			return fld;
		}

		// Append like operator
		public static string Like(string pat, string dbid = "DB") {
			string dbtype = GetConnectionType(dbid);
			if (dbtype == "POSTGRESQL") {
				return (Config.UseIlikeForPostgresql ? " ILIKE " : " LIKE ") + pat;
			} else if (dbtype == "MYSQL") {
				if (!Empty(Config.LikeCollationForMysql)) {
					return " LIKE " + pat + " COLLATE " + Config.LikeCollationForMysql;
				} else {
					return " LIKE " + pat;
				}
			} else if (dbtype == "MSSQL") {
				if (!Empty(Config.LikeCollationForMssql)) {
					return " COLLATE " + Config.LikeCollationForMssql + " LIKE " + pat;
				} else {
					return " LIKE " + pat;
				}
			} else if (dbtype == "SQLITE") {
				if (!Empty(Config.LikeCollationForSqlite)) {
					return "LIKE " + pat + " COLLATE " + Config.LikeCollationForSqlite;
				} else {
					return " LIKE " + pat;
				}
			} else {
				return " LIKE " + pat;
			}
		}

		/// <summary>
		/// Get script name
		/// </summary>
		/// <returns>Current script name</returns>

		public static string ScriptName() => Request.Path;

		// Convert numeric value
		public static object ConvertType(object v, int t)
		{
			if (IsDBNull(v) || v == null) // DN
				return System.DBNull.Value;
			switch (t) {
				case 20: // adBigInt
					return Convert.ToInt64(v);
				case 21: // adUnsignedBigInt
					return Convert.ToUInt64(v);
				case 2:
				case 16: // adSmallInt/adTinyInt
					return Convert.ToInt16(v);
				case 3: // adInteger
					return Convert.ToInt32(v);
				case 17:
				case 18: // adUnsignedTinyInt/adUnsignedSmallInt
					return Convert.ToUInt16(v);
				case 19: // adUnsignedInt
					return Convert.ToUInt32(v);
				case 4: // adSingle
					return Convert.ToSingle(v);
				case 5:
				case 6:
				case 131:
				case 139: // adDouble/adCurrency/adNumeric/adVarNumeric
					return Convert.ToDouble(v);
				default:
					return v;
			}
		}

		// Convert string to float format string // DN
		public static string ConvertToFloatString(object value)
		{
			string val = Convert.ToString(value);
			val = val.Replace(" ", "");
			if (ThousandsSeparator != "")
				val = val.Replace(ThousandsSeparator, "");
			val = val.Replace(DecimalPoint, ".");
			return val;
		}

		// Concatenate string
		public static string Concatenate(string str1, string str2, string sep)
		{
			str1 = str1.Trim();
			str2 = str2.Trim();
			if (str1 != "" && sep != "" && !str1.EndsWith(sep))
				str1 += sep;
			return str1 + str2;
		}

		/// <summary>
		/// Calculate elapsed time
		/// </summary>
		/// <returns>Time in seconds</returns>

		public static string GetElapsedTime() => ((double)(Environment.TickCount - StartTime) / 1000).ToString("F3");

		// Calculate elapsed time // DN
		public static IHtmlContent ElapsedTime()
		{
			string str = "";
			if (Config.Debug)
				str = "<div class=\"alert alert-info ew-alert\">page processing time: " + GetElapsedTime() + " seconds</div>";
			return new HtmlString(str);
		}

		// Compare values with special handling for DBNull values
		public static bool CompareValue(object v1, object v2)
		{
			if (IsDBNull(v1) && IsDBNull(v2)) {
				return true;
			} else if (IsDBNull(v1) || IsDBNull(v2)) {
				return false;
			} else {
				return SameString(v1, v2);
			}
		}

		/// <summary>
		/// Adjust SQL for special characters
		/// </summary>
		/// <param name="value">Value to adjust</param>
		/// <param name="dbid">Database ID</param>
		/// <returns>Adjusted value</returns>

		public static string AdjustSql(object value, string dbid = "DB")
		{
			string dbtype = GetConnectionType(dbid);
			if (dbtype == "MYSQL") {
				return Convert.ToString(value).Trim().Replace("'", "\\'"); // Adjust for single quote
			} else {
				return Convert.ToString(value).Trim().Replace("'", "''"); // Adjust for single quote
			}
		}

		// Adjust GUID for MS Access // DN
		public static string AdjustGuid(object value, string dbid = "DB")
		{
			string dbtype = GetConnectionType(dbid);
			var str = Convert.ToString(value).Trim();
			if (dbtype == "ACCESS" && !str.StartsWith("{"))
				str = "{" + str + "}"; // Add curly braces
			return str;
		}

		// Build SELECT SQL based on different SQL part
		public static string BuildSelectSql(string select, string where, string groupBy, string having, string orderBy, string filter, string sort)
		{
			string dbWhere = where;
			AddFilter(ref dbWhere, filter);
			string dbOrderBy = orderBy;
			if (!Empty(sort))
				dbOrderBy = sort;
			string sql = select;
			if (!Empty(dbWhere))
				sql += " WHERE " + dbWhere;
			if (!Empty(groupBy))
				sql += " GROUP BY " + groupBy;
			if (!Empty(having))
				sql += " HAVING " + having;
			if (!Empty(dbOrderBy))
				sql += " ORDER BY " + dbOrderBy;
			return sql;
		}

		// Load a UTF-8 encoded text file (relative to wwwroot)
		public static async Task<string> LoadText(string fn)
		{
			if (!Empty(fn)) {
				fn = MapPath(fn);
				return FileExists(fn) ? await FileReadAllText(fn) : "";
			}
			return "";
		}

		// Write audit trail (insert/update/delete)
		public static async Task WriteAuditTrailAsync(string pfx, string dt, string scrpt, string user, string action, string table = "", string field = "", object keyvalue = null, object oldvalue = null, object newvalue = null)
		{
			if (table == Config.AuditTrailTableName)
				return;
			try {
				string usrwrk = !Empty(user) ? user : "-1"; // assume Administrator if no user
				Dictionary<string, object> rsnew;
				if (Config.AuditTrailToDatabase)
					rsnew = new Dictionary<string, object> {
						{Config.AuditTrailFieldNameDateTime, dt},
						{Config.AuditTrailFieldNameScript, scrpt},
						{Config.AuditTrailFieldNameUser, usrwrk},
						{Config.AuditTrailFieldNameAction, action},
						{Config.AuditTrailFieldNameTable, table},
						{Config.AuditTrailFieldNameField, field},
						{Config.AuditTrailFieldNameKeyvalue, Convert.ToString(keyvalue)},
						{Config.AuditTrailFieldNameOldvalue, Convert.ToString(oldvalue)},
						{Config.AuditTrailFieldNameNewvalue, Convert.ToString(newvalue)}
					};
				else
					rsnew = new Dictionary<string, object> {
						{"datetime", dt},
						{"script", scrpt},
						{"user", usrwrk},
						{"action", action},
						{"table", table},
						{"field", field},
						{"keyvalue", keyvalue},
						{"oldvalue", oldvalue},
						{"newvalue", newvalue}
					};

				// Call AuditTrail Inserting event
				bool writeAuditTrail = AuditTrail_Inserting(rsnew);
				if (writeAuditTrail) {
					if (!Config.AuditTrailToDatabase) { // Write audit trail to log file
						string folder = ServerMapPath(Config.AuditTrailPath);
						string file = folder + pfx + "_" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
						if (CreateFolder(folder)) {
							bool writeHeader = !FileExists(file);
							using (StreamWriter sw = FileAppendText(file)) {
								if (writeHeader)
									sw.WriteLine(String.Join("\t", rsnew.Keys));
								sw.WriteLine(String.Join("\t", rsnew.Select(kvp => Convert.ToString(kvp.Value))));
							}
						}
					} else if (!Empty(Config.AuditTrailTableName)) { // DN
						var tbl = CreateTable(Config.AuditTrailTableName);
						if (tbl.Fields.TryGetValue(Config.AuditTrailFieldNameDateTime, out DbField fld)) { // Set date // DN
							fld.SetDbValue(rsnew, dt, null, false);
						}
						if ((bool)tbl.Invoke("Row_Inserting", new object[] { null, rsnew })) {
							if (await tbl.InsertAsync(rsnew) > 0)
								tbl.Invoke("Row_Inserted", new object[] { null, rsnew });
						}
					}
				}
			} catch {
				if (Config.Debug)
					throw;
			}
		}

		// Write audit trail (insert/update/delete)
		public static void WriteAuditTrail(string pfx, string dt, string scrpt, string user, string action, string table = "", string field = "", object keyvalue = null, object oldvalue = null, object newvalue = null) =>
			WriteAuditTrailAsync(pfx, dt, scrpt, user, action, table, field, keyvalue, oldvalue, newvalue).GetAwaiter().GetResult();

		/// <summary>
		/// Unformat date time based on format type
		/// </summary>
		/// <param name="date">Date</param>
		/// <param name="namedFormat">
		/// Named format:
		/// 0 - Default date format
		/// 1 - Long Date (with time)
		/// 2 - Short Date (without time)
		/// 3 - Long Time (hh:mm:ss AM/PM)
		/// 4 - Short Time (hh:mm:ss)
		/// 5 - Short Date (yyyy/mm/dd)
		/// 6 - Short Date (mm/dd/yyyy)
		/// 7 - Short Date (dd/mm/yyyy)
		/// 8 - Short Date (Default) + Short Time (if not 00:00:00)
		/// 9/109 - Short Date (yyyy/mm/dd) + Short Time (hh:mm[:ss])
		/// 10/110 - Short Date (mm/dd/yyyy) + Short Time (hh:mm[:ss])
		/// 11/111 - Short Date (dd/mm/yyyy) + Short Time (hh:mm[:ss])
		/// 12 - Short Date - 2 digit year (yy/mm/dd)
		/// 13 - Short Date - 2 digit year (mm/dd/yy)
		/// 14 - Short Date - 2 digit year (dd/mm/yy)
		/// 15/115 - Short Date (yy/mm/dd) + Short Time (hh:mm[:ss])
		/// 16/116 - Short Date (mm/dd/yyyy) + Short Time (hh:mm[:ss])
		/// 17/117 - Short Date (dd/mm/yyyy) + Short Time (hh:mm[:ss])
		/// </param>
		/// <returns>Unformatted date/time</returns>

		public static string UnformatDateTime(object date, int namedFormat)
		{
			string dt;
			bool useShortTime;
			string result = Convert.ToString(date).Trim();
			if (Empty(result)) // DN
				return "";
			if (result.StartsWith("#") && result.EndsWith("#") && result.Length > 2) // MS Access // DN
				result = result.Substring(1, result.Length - 2);
			result = Regex.Replace(result, @" +", " ").Trim();
			if (Regex.IsMatch(result, @"^([0-9]{4})/([0][1-9]|[1][0-2])/([0][1-9]|[1|2][0-9]|[3][0|1])( (0[0-9]|1[0-9]|2[0-3]):([0-5][0-9])(:([0-5][0-9]))?)?$")) // DN
				return result;
			namedFormat = ConvertDateTimeFormatId(namedFormat);
			if (namedFormat > 100) {
				useShortTime = true;
				namedFormat -= 100;
			} else {
				useShortTime = Config.DateTimeWithoutSeconds;
			}
			var arDateTime = result.Split(new char[] { 'T', ' ' }, 2); // DN
			var arDatePt = arDateTime[0].Split(Convert.ToChar(DateSeparator));
			if (arDatePt.Length == 3) {
				switch (namedFormat) {
					case 5:
					case 9: //yyyymmdd
						if (CheckStdDate(arDateTime[0])) {
							dt = arDatePt[0] + "/" + arDatePt[1].PadLeft(2, '0') + "/" + arDatePt[2].PadLeft(2, '0');
							break;
						} else {
							return result;
						}
					case 6:
					case 10: //mmddyyyy
						if (CheckUSDate(arDateTime[0])) {
							dt = arDatePt[2].PadLeft(2, '0') + "/" + arDatePt[0].PadLeft(2, '0') + "/" + arDatePt[1];
							break;
						} else {
							return result;
						}
					case 7:
					case 11: //ddmmyyyy
						if (CheckEuroDate(arDateTime[0])) {
							dt = arDatePt[2].PadLeft(2, '0') + "/" + arDatePt[1].PadLeft(2, '0') + "/" + arDatePt[0];
							break;
						} else {
							return result;
						}
					case 12:
					case 15: //yymmdd
						if (CheckStdShortDate(arDateTime[0])) {
							arDatePt[0] = UnformatYear(arDatePt[0]);
							dt = arDatePt[0] + "/" + arDatePt[1].PadLeft(2, '0') + "/" + arDatePt[2].PadLeft(2, '0');
							break;
						} else {
							return result;
						}
					case 13:
					case 16: //mmddyy
						if (CheckShortUSDate(arDateTime[0])) {
							arDatePt[2] = UnformatYear(arDatePt[2]);
							dt = arDatePt[2] + "/" + arDatePt[0].PadLeft(2, '0') + "/" + arDatePt[1].PadLeft(2, '0');
							break;
						} else {
							return result;
						}
					case 14:
					case 17: //ddmmyy
						if (CheckShortEuroDate(arDateTime[0])) {
							arDatePt[2] = UnformatYear(arDatePt[2]);
							dt = arDatePt[2] + "/" + arDatePt[1].PadLeft(2, '0') + "/" + arDatePt[0].PadLeft(2, '0');
							break;
						} else {
							return result;
						}
					default:
						return result;
				}
				if (arDateTime.Length > 1) {
					arDateTime[1] = arDateTime[1].Trim().Replace(TimeSeparator, ":");
					if (IsDate(arDateTime[1])) // Is time
						dt += " " + arDateTime[1];
				}
				return dt;
			} else {
				if (namedFormat == 3 || namedFormat == 4)
					result = result.Replace(TimeSeparator, ":");
				if (namedFormat == 3)
					result = UnformatShortTime(result);
				return result;
			}
		}

		/// <summary>
		/// Unformat short time (to HH:mm:ss)
		/// </summary>
		/// <param name="tm">Short time format (hh:mm AM/PM/midnight)</param>
		/// <returns>Unformatted time</returns>

		public static string UnformatShortTime(string tm) {
			int hr = 0;
			int min = 0;
			int sec = 0;
			var ar = tm.Split(' ');
			if (ar.Length == 2) {
				var arTimePart = ar[0].Split(':');
				if (arTimePart.Length >= 2) {
					hr = ConvertToInt(arTimePart[0]);
					min = ConvertToInt(arTimePart[1]);
					if (ar[1] == Language.Phrase("AM") && hr == 12) {
						hr = 0;
					} else if (ar[1] == Language.Phrase("PM") && hr < 12) {
						hr += 12;
					}
				}
				if (hr < 0 || hr > 23 || min < 0 || min > 59) { // Avoid invalid time
					hr = 0;
					min = 0;
				}
			} else { // Not short time, ignore
				return tm;
			}
			return ZeroPad(hr, 2) + ":" + ZeroPad(min, 2) + ":" + ZeroPad(sec, 2);
		}

		/// <summary>
		/// Add SCRIPT tag
		/// </summary>
		/// <param name="src">"src" attribute</param>
		/// <param name="attrs">Other attributes</param>

		public static void AddClientScript(string src, IDictionary<string, string> attrs = null)
		{
			var atts = new Attributes() {{"src", AppPath(src)}};
			atts.AddRange(attrs);
			Write(HtmlElement("script", atts, "", true) + "\n");
		}

		/// <summary>
		/// Add LINK tag
		/// </summary>
		/// <param name="href">"href" attribute</param>
		/// <param name="attrs">Other attributes</param>

		public static void AddStylesheet(string href, IDictionary<string, string> attrs = null)
		{
			var atts = new Attributes() {{"rel", "stylesheet"}, {"type", "text/css"}, {"href", AppPath(href)}};
			atts.AddRange(attrs);
			Write(HtmlElement("link", atts, "", false) + "\n");
		}

		// Is Boolean attribute
		public static bool IsBooleanAttribute(string attr) => Config.BooleanHtmlAttributes.Contains(attr, StringComparer.OrdinalIgnoreCase);

		// Build HTML element
		public static string HtmlElement(string tagname, IDictionary<string, string> attrs = null, string innerhtml = "", bool endtag = true)
		{
			string html = "<" + tagname;
			if (attrs != null) {
				foreach (var (key, value) in attrs) {
					if (!Empty(key) && (!Empty(value) || IsBooleanAttribute(key))) { // Allow boolean attributes, e.g. "disabled"
						html += " " + key;
						if (!Empty(value))
							html += "=\"" + HtmlEncode(value) + "\"";
					}
				}
			}
			html += ">";
			if (!Empty(innerhtml))
				html += innerhtml;
			if (endtag)
				html += "</" + tagname + ">";
			return html;
		}

		/// <summary>
		/// Get HTML markup of an option
		/// </summary>
		/// <param name="val">An option value</param>
		/// <returns>HTML string</returns>

		public static string OptionHtml(string val) => Regex.Replace(Config.OptionHtmlTemplate, @"{value}", val);

		/// <summary>
		/// Get HTML markup for options
		/// </summary>
		/// <param name="values">Option values</param>
		/// <returns>HTML markup</returns>

		public static string OptionsHtml(List<string> values) => values.Aggregate("", (html, next) => html + OptionHtml(next));

		// XML tag name
		public static string XmlTagName(string name)
		{
			name = name.Replace(" ", "_");
			if (!Regex.IsMatch(name, @"^(?!XML)[a-z][\w-]*$", RegexOptions.IgnoreCase))
				name = "_" + name;
			return name;
		}

		/// <summary>
		/// HTML-Encode
		/// </summary>
		/// <param name="expression">Value to encode</param>
		/// <returns>Encoded string</returns>

		public static string HtmlEncode(object expression) => WebUtility.HtmlEncode(Convert.ToString(expression));

		/// <summary>
		/// HTML-Decode
		/// </summary>
		/// <param name="expression">Value to decode</param>
		/// <returns>Decoded string</returns>

		public static string HtmlDecode(object expression) => WebUtility.HtmlDecode(Convert.ToString(expression));

		// Get title
		public static string HtmlTitle(string name)
		{
			Match m = Regex.Match(name, @"\s+title\s*=\s*['""]([\s\S]*?)['""]", RegexOptions.IgnoreCase); // Match title='title'
			Match m1 = Regex.Match(name, @"\s+data-caption\s*=\s*['""]([\s\S]*?)['""]", RegexOptions.IgnoreCase); // Match data-caption='caption'
			return m.Success ? m.Groups[1].Value: (m1.Success ? m1.Groups[1].Value : name);
		}

		// Get title and image
		public static string HtmlImageAndText(string name)
		{
			string title = "";
			if (Regex.IsMatch(name, @"<i([^>]*)>", RegexOptions.IgnoreCase) || Regex.IsMatch(name, @"<span([^>]*)>([\s\S]*?)<\/span\s*>", RegexOptions.IgnoreCase) || Regex.IsMatch(name, @"<img([^>]*)>", RegexOptions.IgnoreCase))
				title = HtmlTitle(name);
			else
				title = name;
			return (title != name) ? name + "&nbsp;" + title : name;
		}

		/// <summary>
		/// URL-Encode
		/// </summary>
		/// <param name="expression">Value to encode</param>
		/// <returns>Encoded value</returns>

		public static string UrlEncode(object expression) => WebUtility.UrlEncode(Convert.ToString(expression));

		/// <summary>
		/// Raw URL-Encode (also replaces "+" as "%20")
		/// </summary>
		/// <param name="expression">Value to encode</param>
		/// <returns>Encoded value</returns>

		public static string RawUrlEncode(object expression) => WebUtility.UrlEncode(Convert.ToString(expression)).Replace("+", "%20");

		/// <summary>
		/// URL-Decode
		/// </summary>
		/// <param name="expression">Value to decode</param>
		/// <returns>Decoded value</returns>

		public static string UrlDecode(object expression) => WebUtility.UrlDecode(Convert.ToString(expression));

		// Format sequence number
		public static string FormatSequenceNumber(object seq) => Language.Phrase("SequenceNumber").Replace("%s", Convert.ToString(seq));

		/// <summary>
		/// Encode value for double-quoted JavaScript string
		/// </summary>
		/// <param name="val">Value to encode</param>
		/// <returns>Encoded value</returns>

		public static string JsEncode(object val) => Convert.ToString(val).Replace("\\", "\\\\")
			.Replace("\"", "\\\"").Replace("\t", "\\t").Replace("\r", "\\r").Replace("\n", "\\n");

		/// <summary>
		/// Encode value to single-quoted Javascript string for HTML attributes
		/// </summary>
		/// <param name="val">Value to encode</param>
		/// <returns>Encoded value</returns>

		public static string JsEncodeAttribute(object val) => HtmlEncode(val).Replace("\\", "\\\\");

		/// <summary>
		/// Get display field value separator
		/// </summary>
		/// <param name="idx">Display field index (1, 2, or 3)</param>
		/// <param name="fld">Field object</param>
		/// <returns>Separator</returns>

		public static string ValueSeparator(int idx, DbField fld)
		{
			object sep = fld?.DisplayValueSeparator ?? ", ";
			return IsList(sep) ? ((string[])sep)[idx - 1] : Convert.ToString(sep);
		}

		/// <summary>
		/// Delimited values separator (for select-multiple or checkbox)
		/// </summary>
		/// <returns>Method takes the index and returns the separator</returns>

		public static Func<int, string> ViewOptionSeparator { get; set; } = (idx) => ", ";

		/// <summary>
		/// Get temp upload path
		/// </summary>
		/// <param name="physical">Physical path or not (default is true)</param>
		/// <returns>Temp path</returns>

		public static string UploadTempPath(bool physical = true)
		{
			if (!Empty(Config.UploadTempPath) && !Empty(Config.UploadTempHrefPath)) {
				if (physical) {
					return IncludeTrailingDelimiter(Config.UploadTempPath, true);
				} else {
					return IncludeTrailingDelimiter(Config.UploadTempHrefPath, false);
				}
			} else {
				return UploadPath(physical);
			}
		}

		/// <summary>
		/// Get temp upload path
		/// </summary>
		/// <param name="fldvar">Field variable name</param>
		/// <param name="tblvar">Table variable name</param>
		/// <param name="physical">Physical path or not (default is true)</param>
		/// <returns>Temp path</returns>

		public static string UploadTempPath(string fldvar, string tblvar, bool physical = true)
		{
			string path = UploadTempPath(physical);
			path = IncludeTrailingDelimiter(path + Config.UploadTempFolderPrefix + Session.SessionId, physical);
			if (!Empty(tblvar)) {
				path = IncludeTrailingDelimiter(path + tblvar, physical);
				if (!Empty(fldvar))
					path = IncludeTrailingDelimiter(path + fldvar, physical);
			}
			return path;
		}

		/// <summary>
		/// Get temp upload path for API Upload
		/// </summary>
		/// <param name="token">File token to locate the upload temp path</param>
		/// <returns>The temp path</returns>

		public static string UploadTempPath(string token) { // DN
			string path = (!Empty(Config.UploadTempPath) && !Empty(Config.UploadTempHrefPath)) ? IncludeTrailingDelimiter(Config.UploadTempPath, true) : UploadPath(true);
			path = IncludeTrailingDelimiter(path + Config.UploadTempFolderPrefix + token, true);
			return path;
		}

		// Render upload field to temp path
		public static async Task RenderUploadField(DbField fld, int idx = -1)
		{
			var fldvar = (idx < 0) ? fld.FieldVar : fld.FieldVar.Substring(0, 1) + idx + fld.FieldVar.Substring(1);
			var folder = UploadTempPath(fldvar, fld.TableVar);
			CleanUploadTempPaths(); // Clean all old temp folders
			CleanPath(folder, false); // Clean the upload folder
			if (!DirectoryExists(folder)) {
				if (!CreateFolder(folder))
					End("Cannot create folder: " + folder);
			}
			bool physical = !IsRemote(folder);
			var thumbnailfolder = PathCombine(folder, Config.UploadThumbnailFolder, physical);
			if (!DirectoryExists(thumbnailfolder)) {
				if (!CreateFolder(thumbnailfolder))
					End("Cannot create folder: " + thumbnailfolder);
			}
			if (fld.IsBlob) { // Blob field
				if (!Empty(fld.Upload.DbValue)) {

					// Create upload file
					var filename = !Empty(fld.Upload.FileName) ? fld.Upload.FileName : fld.Param;
					var f = IncludeTrailingDelimiter(folder, physical) + filename;
					f = await CreateUploadFile(f, (byte[])fld.Upload.DbValue);

					// Create thumbnail file
					f = IncludeTrailingDelimiter(thumbnailfolder, physical) + filename;
					byte[] data = (byte[])fld.Upload.DbValue;
					var width = Config.UploadThumbnailWidth;
					var height = Config.UploadThumbnailHeight;
					ResizeBinary(ref data, ref width, ref height);
					f = await CreateUploadFile(f, data);
					fld.Upload.FileName = Path.GetFileName(f); // Update file name
				}
			} else { // Upload to folder
				fld.Upload.FileName = Convert.ToString(fld.Upload.DbValue); // Update file name
				if (!Empty(fld.Upload.FileName)) {

					// Create upload file
					var filename = fld.Upload.FileName;
					string[] files;
					if (fld.UploadMultiple)
						files = filename.Split(Config.MultipleUploadSeparator);
					else
						files = new string[] {filename};
					for (var i = 0; i < files.Length; i++) {
						filename = files[i];
						if (!Empty(filename)) {
							var srcfile = ServerMapPath(fld.UploadPath) + filename;
							var f = IncludeTrailingDelimiter(folder, physical) + filename;
							byte[] data;
							if (!DirectoryExists(srcfile) && FileExists(srcfile)) {
								data = await FileReadAllBytes(srcfile);
								f = await CreateUploadFile(f, data);
							} else {
								CreateImageFromText(Language.Phrase("FileNotFound"), f);
								data = await FileReadAllBytes(f);
							}

							// Create thumbnail file
							f = IncludeTrailingDelimiter(thumbnailfolder, physical) + filename;
							var width = Config.UploadThumbnailWidth;
							var height = Config.UploadThumbnailHeight;
							ResizeBinary(ref data, ref width, ref height);
							f = await CreateUploadFile(f, data);
						}
					}
				}
			}
		}

		// Write uploaded file
		public static async Task<string> CreateUploadFile(string f, byte[] data)
		{
			await FileWriteAllBytes(f, data);
			var ext = Path.GetExtension(f);
			if (Empty(ext)) {
				var ct = ContentType(data);
				switch (ct) {
					case "image/gif":
						MoveFile(f, f += ".gif"); break;
					case "image/jpeg":
						MoveFile(f, f += ".jpg"); break;
					case "image/png":
						MoveFile(f, f += ".png"); break;
				}
			}
			return f;
		}

		// Create image from text
		public static void CreateImageFromText(string txt, string file, int width = 0, int height = 0, string font = null)
		{
			font = font ?? Config.TempImageFont;
			int pt = (int)Math.Round(Config.FontSize/1.33); // 1pt = 1.33px
			double fs = Convert.ToDouble(Config.FontSize);
			int w = (width > 0) ? width : Config.UploadThumbnailWidth;
			int h = (height > 0) ? height : (int)Math.Round(fs / 14 * 20);
			System.Drawing.Image bitmap = new Bitmap(w, h);
			using (var g = Graphics.FromImage(bitmap)) {
				g.Clear(System.Drawing.Color.White);
				using (Brush b = new SolidBrush(System.Drawing.Color.Red)) {
					g.DrawString(txt, new System.Drawing.Font(font, pt), b, 0, (int)Math.Round(Convert.ToDouble((h - fs)/2)));
					bitmap.Save(file);
				}
			}
		}

		/// <summary>
		/// Clean temp upload folders
		/// </summary>
		/// <param name="sessionid">Session ID</param>

		public static void CleanUploadTempPaths(string sessionid = "")
		{
			var folder = UploadTempPath();
			if (!DirectoryExists(folder))
				return;
			var dir = GetDirectoryInfo(folder);
			var subDirs = dir.GetDirectories(Config.UploadTempFolderPrefix + "*");
			foreach (var dirInfo in subDirs) {
				var subfolder = dirInfo.Name;
				var tempfolder = dirInfo.FullName;
				if (Config.UploadTempFolderPrefix + sessionid == subfolder) { // Clean session folder
					CleanPath(tempfolder, true);
				} else {
					if (Config.UploadTempFolderPrefix + Session.SessionId != subfolder) {
						if (IsEmptyPath(tempfolder)) { // Empty folder
							CleanPath(tempfolder, true);
						} else { // Old folder
							var lastmdtime = dirInfo.LastWriteTime;
							if (((TimeSpan)(DateTime.Now - lastmdtime)).Minutes > Config.UploadTempFolderTimeLimit)
								CleanPath(tempfolder, true);
						}
					}
				}
			}
		}

		/// <summary>
		/// Clean temp upload folder
		/// </summary>
		/// <param name="fld">Field object</param>
		/// <param name="idx">Row index</param>

		public static void CleanUploadTempPath(DbField fld, int idx = -1)
		{
			var fldvar = (idx < 0) ? fld.FieldVar : fld.FieldVar.Substring(0, 1) + idx + fld.FieldVar.Substring(1);

			// Remove field temp folder
			var folder = UploadTempPath(fldvar, fld.TableVar);
			CleanPath(folder, true); // Clean the upload folder

			// Remove table temp folder if empty
			var di = GetDirectoryInfo(folder);
			if (di.Parent?.Exists ?? false) {
				var files = di.Parent?.GetFiles("*.*", System.IO.SearchOption.AllDirectories);
				if (files != null && files.Length == 0)
					CleanPath(di.Parent?.FullName, true);
			}

			// Remove complete temp folder if empty
			if (di.Parent?.Parent?.Exists ?? false) {
				var files = di.Parent?.Parent?.GetFiles("*.*", System.IO.SearchOption.AllDirectories);
				if (files != null && files.Length == 0)
					CleanPath(di.Parent?.Parent?.FullName, true);
			}
		}

		/// <summary>
		/// Clean temp upload folder
		/// </summary>
		/// <param name="token">Upload token</param>

		public static void CleanUploadTempPath(string token) => CleanPath(UploadTempPath(token), true); // Clean the upload folder

		// Clean folder
		public static void CleanPath(string folder, bool delete)
		{
			try {
				if (DirectoryExists(folder)) {
					Collect(); // DN

					// Delete files
					var files = GetFiles(folder);
					foreach (string file in files) {
						try {
							FileDelete(file);
						} catch {
							if (Config.Debug)
								throw;
						}
					}

					// Delete directories
					GetDirectories(folder).ToList().ForEach(dir => CleanPath(dir, delete));
					if (delete) {
						try {
							DirectoryDelete(folder);
						} catch {
							if (Config.Debug)
								throw;
						}
					}
				}
			} catch {
				if (Config.Debug)
					throw;
			} finally {
				Collect(); // DN
			}
		}

		// Check if empty folder
		public static bool IsEmptyPath(string folder)
		{
			if (DirectoryExists(folder)) {
				var files = GetFiles(folder);
				if (files.Length > 0)
					return false;
				var dirs = GetDirectories(folder);
				return dirs.All(dir => !IsEmptyPath(dir));
			}
			return false;
		}

		// Get file count in folder
		public static int FolderFileCount(string folder) => DirectoryExists(folder) ? GetFiles(folder).Length : 0;

		// Truncate memo field based on specified length, string truncated to nearest space or CrLf
		public static string TruncateMemo(string memostr, int len, bool removehtml)
		{
			string str = removehtml ? RemoveHtml(memostr) : memostr;
			str = str.Replace("\r\n", "\n").Replace("\r", "\n").Replace("\n", "\r\n");
			if (str.Length > 0 && str.Length > len) {
				int k = 0;
				while (k >= 0 && k < str.Length) {
					int i = str.IndexOf(" ", k);
					int j = str.IndexOf("\r\n", k);
					if (i < 0 && j < 0) { // Unable to truncate
						return str;
					} else { // Get nearest space or CrLf
						if (i > 0 && j > 0) {
							k = (i < j) ? i : j;
						} else if (i > 0) {
							k = i;
						} else if (j > 0) {
							k = j;
						}

						// Get truncated text
						if (k >= len) {
							return str.Substring(0, k) + "...";
						} else {
							k++;
						}
					}
				}
			}
			return str;
		}

		/// <summary>
		/// Remove HTML tags from text
		/// </summary>
		/// <param name="str">String to clean</param>
		/// <returns>Cleaned string</returns>

		public static string RemoveHtml(string str) => Regex.Replace(str, "<[^>]*>", String.Empty);

		/// <summary>
		/// Extract JavaScript from HTML
		/// </summary>
		/// <param name="html">HTML to process</param>
		/// <param name="classname">Class name of script tags</param>
		/// <returns>Converted scripts</returns>

		public static string ExtractScript(ref string html, string classname = "")
		{
			MatchCollection matches = Regex.Matches(html, @"<script([^>]*)>([\s\S]*?)<\/script\s*>", RegexOptions.IgnoreCase);
			if (matches.Count == 0)
				return "";
			string scripts = "";
			foreach (Match match in matches) {
				if (Regex.IsMatch(match.Groups[1].Value, @"(\s+type\s*=\s*['""]*text\/javascript['""]*)|^((?!\s+type\s*=).)*$", RegexOptions.IgnoreCase)) { // JavaScript
					html = html.Replace(match.Value, ""); // Remove the script from HTML
					scripts += HtmlElement("script", new Attributes() {{"type", "text/html"}, {"class", classname}}, match.Groups[2].Value); // Convert script type and add CSS class, if specified
				}
			}
			return scripts;
		}

		/// <summary>
		/// Clean email content
		/// </summary>
		/// <param name="content">Content to clean</param>
		/// <returns>Cleaned content</returns>

		public static async Task<string> CleanEmailContent(string content) {
			content = Regex.Replace(content, @"class=""(card\s+)?ew-grid(\s+[\w\-]+)?""", "");
			content = Regex.Replace(content, @"class=""([\w\-]+\s+)?ew-grid-middle-panel""", "");
			content = content.Replace("class=\"table ew-table\"", "class=\"ew-export-table\"");
			try {
				string tableStyles = "border-collapse: collapse;";
				string cellStyles = "border: 1px solid #dddddd; padding: 5px;";
				var parser = new AngleSharp.Parser.Html.HtmlParser();
				var doc = await parser.ParseAsync(content);
				var tables = doc.GetElementsByTagName("table");
				foreach (var table in tables) {
					if (table.GetAttribute("class").Contains("ew-export-table")) {
						if (table.HasAttribute("style"))
							table.SetAttribute("style", table.GetAttribute("style") + tableStyles);
						else
							table.SetAttribute("style", tableStyles);
						var rows = table.GetElementsByTagName("tr");
						int rowcnt = rows.Length;
						foreach (var row in rows) {
							var nodes = row.ChildNodes;
							foreach (var node in nodes) {
								if (node.NodeType != AngleSharp.Dom.NodeType.Element || !SameText(node.NodeName, "TD"))
									continue;
								var cell = (AngleSharp.Dom.IElement)node;
								if (cell.HasAttribute("style"))
									cell.SetAttribute("style", cell.GetAttribute("style") + cellStyles);
								else
									cell.SetAttribute("style", cellStyles);
							}
						}
					}
				}
				content = doc.DocumentElement.OuterHtml;
			} catch {}
			return content;
		}

		/// <summary>
		/// Convert email address to MailAddress
		/// </summary>
		/// <param name="email">Email address as string</param>
		/// <returns>MailAddress instance</returns>

		public static MailAddress ConvertToMailAddress(string email)
		{
			email = email.Trim();
			var m = Regex.Match(email, @"^(.+)<([\w.%+-]+@[\w.-]+\.[A-Z]{2,6})>$", RegexOptions.IgnoreCase);
			if (m.Success)
				return new MailAddress(m.Groups[2].Value, m.Groups[1].Value.Trim());
			return new MailAddress(email);
		}

		/// <summary>
		/// Send email (async)
		/// </summary>
		/// <param name="from">Sender</param>
		/// <param name="to">Recipients</param>
		/// <param name="cc">CC recipients</param>
		/// <param name="bcc">BCC recipients</param>
		/// <param name="subject">Subject</param>
		/// <param name="body">Body</param>
		/// <param name="format">Format ("html" or not)</param>
		/// <param name="charset">Charset</param>
		/// <param name="ssl">Use SSL or not</param>
		/// <param name="attachments">Attachments</param>
		/// <param name="images">Images</param>
		/// <param name="smtp">Custom SmtpClient</param>
		/// <returns>Whether the email is sent successfully</returns>

		public static async Task<bool> SendEmailAsync(string from, string to, string cc, string bcc, string subject, string body, string format, string charset, bool ssl = false, List<string> attachments = null, List<string> images = null, SmtpClient smtp = null)
		{
			var mail = new MailMessage();
			if (!Empty(from))
				mail.From = ConvertToMailAddress(from);
			if (!Empty(to))
				to.Replace(';', ',').Split(',').Select(x => ConvertToMailAddress(x)).ToList().ForEach(address => mail.To.Add(address));
			if (!Empty(cc))
				cc.Replace(';', ',').Split(',').Select(x => ConvertToMailAddress(x)).ToList().ForEach(address => mail.CC.Add(address));
			if (!Empty(bcc))
				bcc.Replace(';', ',').Split(',').Select(x => ConvertToMailAddress(x)).ToList().ForEach(address => mail.Bcc.Add(address));
			mail.Subject = subject;
			mail.IsBodyHtml = SameText(format, "html");
			mail.Body = body;
			Encoding enc = null;
			if (!Empty(charset) && !SameText(charset, "us-ascii")) { // DN
				enc = Encoding.GetEncoding(charset);
				mail.BodyEncoding = enc;
			}
			if (smtp == null) {
				smtp = new SmtpClient();
				smtp.Host = !Empty(Config.SmtpServer) ? Config.SmtpServer : "localhost";
				if (Config.SmtpServerPort > 0)
					smtp.Port = Config.SmtpServerPort;
				smtp.EnableSsl = ssl;
				if (!Empty(Config.SmtpServerUsername) && !Empty(Config.SmtpServerPassword)) {
					var smtpuser = new NetworkCredential();
					smtpuser.UserName = Config.SmtpServerUsername;
					smtpuser.Password = Config.SmtpServerPassword;
					smtp.UseDefaultCredentials = false;
					smtp.Credentials = smtpuser;
				}
			}
			attachments?.ForEach(fn => mail.Attachments.Add(new Attachment(fn)));
			if (mail.IsBodyHtml && images != null && images.Count > 0) {
				string html = mail.Body;
				foreach (string tmpimage in images) {
					string file = UploadTempPath() + tmpimage;
					string cid = TempImageLink(tmpimage, "email");
					html = html.Replace(file, cid);
				}
				var htmlview = AlternateView.CreateAlternateViewFromString(html, enc, "text/html");
				foreach (string tmpimage in images) {
					string file = UploadTempPath() + tmpimage;
					if (FileExists(file)) {
						LinkedResource res = new LinkedResource(file);
						res.ContentId = TempImageLink(tmpimage, "cid");
						htmlview.LinkedResources.Add(res);
					}
				}
				mail.AlternateViews.Add(htmlview);
			}
			try {
				await smtp.SendMailAsync(mail);
				return true;
			} catch (Exception e) {
				EmailError = e.Message;
				if (Config.Debug)
					throw;
				return false;
			}
		}

		/// <summary>
		/// Send email
		/// </summary>
		/// <param name="from">Sender</param>
		/// <param name="to">Recipients</param>
		/// <param name="cc">CC recipients</param>
		/// <param name="bcc">BCC recipients</param>
		/// <param name="subject">Subject</param>
		/// <param name="body">Body</param>
		/// <param name="format">Format ("html" or not)</param>
		/// <param name="charset">Charset</param>
		/// <param name="ssl">Use SSL or not</param>
		/// <param name="attachments">Attachments</param>
		/// <param name="images">Images</param>
		/// <param name="smtp">Custom SmtpClient</param>
		/// <returns>Whether the email is sent successfully</returns>

		public static bool SendEmail(string from, string to, string cc, string bcc, string subject, string body, string format, string charset, bool ssl = false, List<string> attachments = null, List<string> images = null, SmtpClient smtp = null) =>
			SendEmailAsync(from, to, cc, bcc, subject, body, format, charset, ssl, attachments, images, smtp).GetAwaiter().GetResult();

		/// <summary>
		/// Change the file name of the uploaded file
		/// </summary>
		/// <param name="folder">Folder name</param>
		/// <param name="fileName">File name</param>
		/// <returns>Changed file name</returns>

		public static string UploadFileName(string folder, string fileName) => UniqueFileName(folder, fileName);

		/// <summary>
		/// Change the file name of the uploaded file using global upload folder
		/// </summary>
		/// <param name="fileName">File name</param>
		/// <returns>Changed file name</returns>

		public static string UploadFileName(string fileName) => UploadFileName(UploadPath(true), fileName);

		/// <summary>
		/// Generate an unique file name for a folder (filename(n).ext)
		/// </summary>
		/// <param name="folder">Output folder</param>
		/// <param name="orifn">Original file name</param>
		/// <param name="indexed">Index starts from '(n)' at the end of the original file name</param>
		/// <returns>The unique file name</returns>

		public static string UniqueFileName(string folder, string orifn, bool indexed = false)
		{

			// Check folder
			if (!DirectoryExists(folder) && !CreateFolder(folder))
				End("Folder does not exist: " + folder);
			folder = IncludeTrailingDelimiter(folder, true);

			// Check file
			if (Empty(orifn))
				End("Missing file name");
			string ext = Path.GetExtension(orifn);
			string oldfn = Path.GetFileNameWithoutExtension(orifn);
			if (Empty(ext))
				End("Missing file extension");
			if (!CheckFileType(orifn))
				End("File extension not allowed: " + ext);
			if (oldfn.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
				End("File name contains invalid character(s): " + orifn);

			// Suffix
			int i = 0;
			string suffix = "";
			if (indexed) {
				Match m = Regex.Match(@"\(\d+\)$", oldfn);
				if (m.Success) {
					i = ConvertToInt(m.Groups[1].Value);
					i++;
				} else {
					i = 1;
				}
				suffix = "(" + i.ToString() + ")";
			}

			// Check to see if filename exists
			string name = Regex.Replace(oldfn, @"\(\d+\)$", ""); // Remove "(n)" at the end of the file name
			while (FileExists(folder + name + suffix + ext)) {
				i++;
				suffix = "(" + i.ToString() + ")";
			}
			return name + suffix + ext;
		}

		/// <summary>
		/// Get ASP.NET Maker field data type
		/// </summary>
		/// <param name="fldtype">Field ADO data type</param>
		/// <returns>ASP.NET Maker field data type</returns>

		public static int FieldDataType(int fldtype) {
			switch (fldtype) {
				case 20:
				case 3:
				case 2:
				case 16:
				case 4:
				case 5:
				case 131:
				case 139:
				case 6:
				case 17:
				case 18:
				case 19:
				case 21: // Numeric
					return Config.DataTypeNumber;
				case 7:
				case 133:
				case 135: // Date
				case 146: // DateTiemOffset
					return Config.DataTypeDate;
				case 134: // Time
				case 145: // Time
					return Config.DataTypeTime;
				case 201:
				case 203: // Memo
					return Config.DataTypeMemo;
				case 129:
				case 130:
				case 200:
				case 202: // String
					return Config.DataTypeString;
				case 11: // Boolean
					return Config.DataTypeBoolean;
				case 72: // GUID
					return Config.DataTypeGuid;
				case 128:
				case 204:
				case 205: // Binary
					return Config.DataTypeBlob;
				case 141: // XML
					return Config.DataTypeXml;
				default:
					return Config.DataTypeOther;
			}
		}

		/// <summary>
		/// Get web root // DN
		/// </summary>
		/// <param name="physical">Physical path or not</param>
		/// <returns>The path (returns wwwroot, NOT project folder)</returns>

		public static string AppRoot(bool physical = true)
		{
			string p = "";
			if (physical) { // Physical path
				p = WebRootPath;
			} else { // Path relative to host
				p = Request.PathBase.ToString();
			}
			return IncludeTrailingDelimiter(p, physical);
		}

		/// <summary>
		/// Get path relative to wwwroot
		/// </summary>
		/// <param name="path">Path to map</param>
		/// <param name="physical">Physical or not</param>
		/// <returns>Mapped path</returns>

		public static string MapPath(string path, bool physical = true)
		{
			if (Path.IsPathRooted(path) || IsRemote(path))
				return path;
			path = path.Trim().Replace("\\", "/");
			path = Regex.Replace(path, @"^[~/]+", "");
			return PathCombine(AppRoot(physical), path, physical); // relative to wwwroot
		}

		/// <summary>
		/// Get path (not file) with trailing delimiter relative to wwwroot
		/// </summary>
		/// <param name="physical">Physical or not</param>
		/// <param name="path">Path to map</param>
		/// <returns>Mapped path</returns>

		public static string MapPath(bool physical, string path) => IncludeTrailingDelimiter(MapPath(path, physical), physical);

		/// <summary>
		/// Get path with trailing delimiter of the global upload folder relative to wwwroot
		/// </summary>
		/// <param name="physical">Physical or not</param>
		/// <returns>Upload path</returns>

		public static string UploadPath(bool physical) => MapPath(physical, Config.UploadDestPath);

		/// <summary>
		/// Get physical path (folder with trailing delimiter or file) relative to wwwroot
		/// </summary>
		/// <param name="path">Path to map</param>
		/// <returns>Mapped path</returns>

		public static string ServerMapPath(string path) {
			if (IsRemote(path))
				return path;
			if (Path.HasExtension(path)) // File
				return MapPath(true, Path.GetDirectoryName(path)) + Path.GetFileName(path);
			else // Folder
				return MapPath(true, path); // With trailing delimiter
		}

		// Get path relative to a base path
		public static string PathCombine(string basePath, string relPath, bool physical)
		{
			physical = !IsRemote(basePath) && physical;
			string delimiter = physical ? Config.PathDelimiter : "/";
			if (basePath != delimiter) // If basePath = root, do not remove delimiter
				basePath = RemoveTrailingDelimiter(basePath, physical);
			relPath = physical ? relPath.Replace("/", Config.PathDelimiter) : relPath.Replace("\\", "/");
			if (relPath.EndsWith(".")) // DN
				relPath = IncludeTrailingDelimiter(relPath, physical);
			int p1 = relPath.IndexOf(delimiter);
			string Path2 = "";
			while (p1 > -1) {
				string Path = relPath.Substring(0, p1 + 1);
				if (Path == delimiter || Path == "." + delimiter) { // Skip
				} else if (Path == ".." + delimiter) {
					int p2 = basePath.LastIndexOf(delimiter);
					if (p2 == 0) // basePath = "/xxx", cannot move up
						basePath = delimiter;
					else if (p2 > -1 && !basePath.EndsWith(".."))
						basePath = basePath.Substring(0, p2);
					else if (!Empty(basePath) && basePath != "..")
						basePath = "";
					else
						Path2 += ".." + delimiter;
				} else {
					Path2 += Path;
				}
				try {
					relPath = relPath.Substring(p1 + 1);
				} catch {
					relPath = "";
				}
				p1 = relPath.IndexOf(delimiter);
			}
			return ((!Empty(basePath) && basePath != ".") ? IncludeTrailingDelimiter(basePath, physical) : "") + Path2 + relPath;
		}

		/// <summary>
		/// Remove the last delimiter for a path
		/// </summary>
		/// <param name="path">Path</param>
		/// <param name="physical">Physical or not</param>
		/// <returns>Result path</returns>

		public static string RemoveTrailingDelimiter(string path, bool physical)
		{
			string delimiter = (!IsRemote(path) && physical) ? Config.PathDelimiter : "/";
			while (path.EndsWith(delimiter))
				path = path.Substring(0, path.Length - 1);
			return path;
		}

		/// <summary>
		/// Include the last delimiter for a path
		/// </summary>
		/// <param name="path">Path</param>
		/// <param name="physical">Physical or not</param>
		/// <returns>Result path</returns>

		public static string IncludeTrailingDelimiter(string path, bool physical)
		{
			string delimiter = (!IsRemote(path) && physical) ? Config.PathDelimiter : "/";
			path = RemoveTrailingDelimiter(path, physical);
			return path + delimiter;
		}

		/// <summary>
		/// Write the paths for config/debug only
		/// </summary>

		public static void WriteUploadPaths()
		{
			Write("wwwroot: " + AppRoot() + "<br>");
			Write("Global upload folder (relative to wwwroot): " + Config.UploadDestPath + "<br>");
			Write("Global upload folder (physical): " + UploadPath(true) + "<br>");
		}

		/// <summary>
		/// Write info for config/debug only
		/// </summary>

		public static void Info()
		{
			WriteUploadPaths();
			Write("User.Identity.Name = " + Convert.ToString(User?.Identity?.Name) + "<br>");
			Write("CurrentUserName() = " + CurrentUserName() + "<br>");
			Write("CurrentUserID() = " + CurrentUserID() + "<br>");
			Write("CurrentParentUserID() = " + CurrentParentUserID() + "<br>");
			Write("IsLoggedIn() = " + (IsLoggedIn() ? "true" : "false") + "<br>");
			Write("IsAdmin() = " + (IsAdmin() ? "true" : "false") + "<br>");
			Write("IsSysAdmin() = " + (IsSysAdmin() ? "true" : "false") + "<br>");
			Security?.ShowUserLevelInfo();
		}

		/// <summary>
		/// Get current page name only
		/// </summary>
		/// <returns>Current page name</returns>

		public static string CurrentPageName() => Convert.ToString(IsApi() ? Config.ApiUrl + Controller?.RouteData.Values["controller"] : Controller?.RouteData.Values["action"]); // Returns action (file name) only

		// Get refer URL
		public static string ReferUrl()
		{
			string url = Request.Headers[HeaderNames.Referer];
			if (!Empty(url)) {
				var p = Request.Host.ToString() + Request.PathBase.ToString() + "/";
				var i = url.LastIndexOf(p);
				if (i > -1)
					url = url.Substring(i + p.Length); // Remove path base
				return url;
			}
			return "";
		}

		// Get refer page name
		public static string ReferPage() => GetPageName(ReferUrl());

		// Get page name // DN
		// param url, must contain action only, e.g. /xxxlist, not /xxx/list // DN

		public static string GetPageName(string url)
		{
			if (!Empty(url)) {
				if (url.Contains("?"))
					url = url.Substring(0, url.LastIndexOf("?")); // Remove querystring first
				var p = Request.Host.ToString();
				var i = url.IndexOf(p);
				if (i > -1)
					url = url.Substring(i + p.Length); // Remove host
				p = Request.PathBase.ToString();
				if (!Empty(p) && url.StartsWith(p))
					url = url.Substring(p.Length); // Remove path base
				var a = Config.AreaName;
				if (!Empty(a) && url.StartsWith(a))
					url = url.Substring(a.Length); // Remove area
				return url.StartsWith("/") ? url.Split('/')[1] : url.Split('/')[0]; // Remove parameters
			}
			return "";
		}

		/// <summary>
		/// Get full URL
		/// </summary>
		/// <param name="url">URL</param>
		/// <param name="type">Type of the URL</param>
		/// <returns>Full URL</returns>

		public static string FullUrl(string url = "", string type = "") // DN
		{
			var request = Request;
			if (Empty(url)) {
				return String.Concat(request.Scheme, "://",
					request.Host.ToString(),
					request.PathBase.ToString(),
					request.Path.ToString(),
					request.QueryString.ToString());
			} else if ((IsAbsoluteUrl(url) || Path.IsPathRooted(url)) && !url.StartsWith("/")) {
				return url;
			} else {
				Config.FullUrlProtocols.TryGetValue(type, out string protocol);
				return string.Concat(protocol ?? request.Scheme, "://", request.Host.ToString(), AppPath(url));
			}
		}

		/// <summary>
		/// Convert CSS file for RTL
		/// </summary>
		/// <param name="f">File name</param>
		/// <returns>Result file name</returns>

		public static string CssFile(string f) {
			if (Config.CssFlip)
				return Regex.Replace(f, @"(.css)$", "-rtl.css", RegexOptions.IgnoreCase);
			return f;
		}

		/// <summary>
		/// Check if HTTPS
		/// </summary>
		/// <returns>Whether request is using HTTPS</returns>

		public static bool IsHttps() => Request.IsHttps;

		/// <summary>
		/// Get current URL
		/// </summary>
		/// <returns>Current URL</returns>

		public static string CurrentUrl()
		{
			var request = Request;
			return String.Concat(request.PathBase.ToString(),
				request.Path.ToString(),
				request.QueryString.ToString());
		}

		/// <summary>
		/// Get application path (relative to host)
		/// </summary>
		/// <param name="url">URL to process</param>
		/// <param name="useArea">Use Area or not</param>
		/// <returns>Result URL</returns>

		public static string AppPath(string url = "", bool useArea = true) // DN
		{
			if (IsAbsoluteUrl(url) || Path.IsPathRooted(url) || url.StartsWith("#") || url.StartsWith("?")) // Includes "javascript:" and "cid:"
				return url;
			var pathBase = IncludeTrailingDelimiter(Request.PathBase.ToString(), false);
			if (Empty(url)) {
				return pathBase;
			} else {
				if (useArea && Config.AreaName != "") {
					if (url.StartsWith(Config.AreaName + "/"))
						url = url.Substring(Config.AreaName.Length + 1);
					pathBase += Config.AreaName + "/";
				}
				return url.StartsWith(pathBase) ? url : pathBase + url;
			}
		}

		// Get URL
		public static string GetUrl(string url) => AppPath(url); // Use absolute path (not relative path) // DN

		// Check if responsive layout
		public static bool IsResponsiveLayout() => Config.UseResponsiveLayout;

		/// <summary>
		/// IO methods (delegates)
		/// </summary>
		// Is directory

		public static Func<string, bool> IsDirectory { get; set; } = (path) => (File.GetAttributes(path) & FileAttributes.Directory) == FileAttributes.Directory;

		// Directory exists
		public static Func<string, bool> DirectoryExists { get; set; } = Directory.Exists;

		// Deletes the specified directory and any subdirectories and files in the directory
		public static Action<string> DirectoryDelete { get; set; } = (path) => Directory.Delete(path, true);

		// Directory move
		public static Action<string, string> DirectoryMove { get; set; } = Directory.Move;

		// Directory create
		public static Func<string, bool> DirectoryCreate { get; set; } = (path) => Directory.CreateDirectory(path) != null;

		// File exists
		public static Func<string, bool> FileExists { get; set; } = File.Exists;

		// File delete
		public static Action<string> FileDelete { get; set; } = File.Delete;

		// File copy
		public static Action<string, string, bool> FileCopy { get; set; } = File.Copy;

		// File copy
		public static Action<string, string> FileMove { get; set; } = File.Move;

		// Read file as bytes
		public static Func<string, Task<byte[]>> FileReadAllBytes { get; set; } = (s) => File.ReadAllBytesAsync(s);

		// Open a file, read all lines of the file with UTF-8 encoding, and then close the file
		public static Func<string, Task<string>> FileReadAllText { get; set; } = (s) => File.ReadAllTextAsync(s);

		// Open a file, read all lines of the file with UTF-8 encoding, and then close the file
		public static Func<string, Encoding, Task<string>> FileReadAllTextWithEncoding { get; set; } = (s, enc) => File.ReadAllTextAsync(s, enc);

		// Open a text file, read all lines of the file, and then close the file
		public static Func<string, Task<string[]>> FileReadAllLines { get; set; } = (s) => File.ReadAllLinesAsync(s);

		// Write file with bytes
		public static Func<string, byte[], Task> FileWriteAllBytes { get; set; } = (s, data) => File.WriteAllBytesAsync(s, data);

		// Open a file, write the string to the file with UTF-8 encoding without a Byte-Order Mark (BOM), and then close the file
		public static Func<string, string, Task> FileWriteAllText { get; set; } = (s, data) => File.WriteAllTextAsync(s, data);

		// Open a text file, write all lines to the file, and then close the file
		public static Func<string, IEnumerable<string>, Task> FileWriteAllLines { get; set; } = (s, data) => File.WriteAllLinesAsync(s, data);

		// Create a StreamWriter that appends UTF-8 encoded text to an existing file, or to a new file if the specified file does not exist
		public static Func<string, StreamWriter> FileAppendText { get; set; } = File.AppendText;

		// Create or opens a file for writing UTF-8 encoded text
		public static Func<string, StreamWriter> FileCreateText { get; set; } = File.CreateText;

		// Open an existing file or create a new file for writing
		public static Func<string, FileStream> FileOpenWrite { get; set; } = File.OpenWrite;

		// Create file info
		public static Func<string, FileInfo> GetFileInfo { get; set; } = (file) => new FileInfo(file);

		// Create directory info
		public static Func<string, DirectoryInfo> GetDirectoryInfo { get; set; } = (path) => new DirectoryInfo(path);

		// Get the names of subdirectories (including their paths) in the specified directory
		public static Func<string, string[]> GetDirectories { get; set; } = (path) => Directory.GetDirectories(path);

		// Get the names of files (including their paths) in the specified directory
		public static Func<string, string[]> GetFiles { get; set; } = (path) => Directory.GetFiles(path);

		// Get the names of files (including their paths) that match the specified search pattern in the specified directory
		public static Func<string, string, string[]> SearchFiles { get; set; } = (path, searchPattern) => Directory.GetFiles(path, searchPattern);

		/// <summary>
		/// Delete file
		/// </summary>
		/// <param name="filePath">File to delete</param>

		public static void DeleteFile(string filePath)
		{
			try {
				if (FileExists(filePath)) {
					FileDelete(filePath);
					Collect(); // DN
				}
			} catch {}
		}

		/// <summary>
		/// Rename/Move file
		/// </summary>
		/// <param name="oldFile">Old file</param>
		/// <param name="newFile">New file</param>

		public static void MoveFile(string oldFile, string newFile)
		{
			Collect(); // DN
			FileMove(oldFile, newFile);
		}

		/// <summary>
		/// Copy file
		/// </summary>
		/// <param name="srcFile">Source file</param>
		/// <param name="destFile">Target file</param>

		public static void CopyFile(string srcFile, string destFile)
		{
			Collect(); // DN
			FileCopy(srcFile, destFile, false);
		}

		/// <summary>
		/// Copy file
		/// </summary>
		/// <param name="srcFile">Source file</param>
		/// <param name="destFile">Target file</param>
		/// <param name="overwrite">Overwrite or not</param>

		public static void CopyFile(string srcFile, string destFile, bool overwrite)
		{
			Collect(); // DN
			FileCopy(srcFile, destFile, overwrite);
		}

		/// <summary>
		/// Copy file
		/// </summary>
		/// <param name="folder">Target folder</param>
		/// <param name="fn">Target file name</param>
		/// <param name="file">Old file</param>
		/// <param name="overwrite">Overwrite or not</param>
		/// <returns></returns>

		public static bool CopyFile(string folder, string fn, string file, bool overwrite) {
			if (FileExists(file)) {
				var newfile = IncludeTrailingDelimiter(folder, true) + fn;
				if (CreateFolder(folder)) {
					try {
						CopyFile(file, newfile, overwrite);
						return true;
					} catch {
						if (Config.Debug)
							throw;
						return false;
					}
				}
			}
			return false;
		}

		/// <summary>
		/// Create folder
		/// </summary>
		/// <param name="folder">Target folder</param>
		/// <returns>Whether folder is created successfully</returns>

		public static bool CreateFolder(string folder)
		{
			try {
				return DirectoryCreate(folder);
			} catch {
				return false;
			}
		}

		// Calculate Hash for recordset // DN
		public static string GetFieldHash(object value, int fldType)
		{
			var hash = "";
			if (IsDBNull(value) || value == null) {
				return "";
			} else if (fldType == Config.DataTypeBlob) {
				var bytes = (byte[])value;
				if (Config.BlobFieldByteCount > 0 && bytes.Length > Config.BlobFieldByteCount) {
					hash = BitConverter.ToString(bytes, 0, Config.BlobFieldByteCount);
				} else {
					hash = BitConverter.ToString(bytes);
				}
			} else if (fldType == Config.DataTypeMemo) {
				hash = Convert.ToString(value);
				if (Config.BlobFieldByteCount > 0 && hash.Length > Config.BlobFieldByteCount)
					hash = hash.Substring(0, Config.BlobFieldByteCount);
			} else {
				hash = Convert.ToString(value);
			}
			return Md5(hash);
		}

		// Create temp image file from binary data
		public static string TempImage(byte[] filedata) {
			if (filedata == null)
				return ""; // DN
			string export = Get<string>("export") ?? Post<string>("export") ?? Post("exporttype");
			string f = UploadTempPath() + Path.GetRandomFileName();
			using (var ms = new MemoryStream(filedata)) {
				try {
					using (var img = System.Drawing.Image.FromStream(ms)) { // DN
						if (img.RawFormat.Equals(ImageFormat.Gif)) {
							f = Path.ChangeExtension(f, ".gif");
						} else if (img.RawFormat.Equals(ImageFormat.Jpeg)) {
							f = Path.ChangeExtension(f, ".jpg");
						} else if (img.RawFormat.Equals(ImageFormat.Png)) {
							f = Path.ChangeExtension(f, ".png");
						} else if (img.RawFormat.Equals(ImageFormat.Bmp)) {
							f = Path.ChangeExtension(f, ".bmp");
						} else {
							return "";
						}
						img.Save(f);
					}
				} catch {
					return "";
				}
			}
			string tmpimage = Path.GetFileName(f);
			TempImages.Add(tmpimage);
			return TempImageLink(tmpimage, export);
		}

		// Garbage collection
		public static void Collect() {

			// Force garbage collection
			GC.Collect();

			// Wait for all finalizers to complete before continuing.
			// Without this call to GC.WaitForPendingFinalizers,
			// the worker loop below might execute at the same time
			// as the finalizers.
			// With this call, the worker loop executes only after
			// all finalizers have been called.

			GC.WaitForPendingFinalizers();

			// Do another garbage collection
			GC.Collect();
		}

		// Delete temp images
		public static void DeleteTempImages() {
			Collect();
			if (TempImages != null) {
				string folder = UploadTempPath();
				TempImages.ForEach(tmpimage => DeleteFile(folder + tmpimage));
				TempImages.Clear(); // DN
			}
		}

		// Create temp file
		public static string TempFile(string file) {
			if (FileExists(file)) { // Copy only
				string export = Get<string>("export") ?? Post("export");
				string folder = UploadTempPath();
				string f = folder + Path.GetRandomFileName();
				string ext = Path.GetExtension(file);
				if (!Empty(ext))
					f += ext;
				CopyFile(file, f);
				string tmpimage = Path.GetFileName(f);
				TempImages.Add(tmpimage);
				return TempImageLink(tmpimage, export);
			} else {
				return "";
			}
		}

		// Get temp image link
		public static string TempImageLink(string file, string lnktype = "") {
			if (Empty(file))
				return "";
			if (lnktype == "email" || lnktype == "cid") {
				string[] ar = file.Split('.');
				var list = new List<string>(ar);
				list.RemoveAt(list.Count - 1);
				ar = list.ToArray();
				string lnk = String.Join(".", ar);
				if (lnktype == "email")
					lnk = "cid:" + lnk;
				return lnk;
			} else {
				return UploadTempPath() + file;
			}
		}

		/// <summary>
		/// Invoke static method of project class
		/// </summary>
		/// <param name="name">Name of method</param>
		/// <param name="parameters">Parameters</param>
		/// <returns>Returned value of the method</returns>

		public static object Invoke(string name, object[] parameters = null) =>
			typeof(SampleProject).GetMethod(name)?.Invoke(null, parameters);

		/// <summary>
		/// Invoke method of an object
		/// </summary>
		/// <param name="obj">Object</param>
		/// <param name="name">Method name</param>
		/// <param name="parameters">Parameters</param>
		/// <returns>Returned value of the method</returns>

		public static object Invoke(object obj, string name, object[] parameters = null) =>
			obj.GetType().GetMethod(name, BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)?.Invoke(obj, parameters);

		/// <summary>
		/// Downloads requested resource
		/// </summary>
		/// <param name="address">URL</param>
		/// <param name="data">Data sent to server</param>
		/// <returns>Requested resource as string</returns>

		public static string DownloadString(string address, NameValueCollection data = null) {
			using (var client = new WebClient()) {
				if (data != null) {
					var bytes = client.UploadValues(address, data);
					return Encoding.UTF8.GetString(bytes);
				} else {
					return client.DownloadString(address);
				}
			}
		}

		/// <summary>
		/// Downloads requested resource (async)
		/// </summary>
		/// <param name="address">URL</param>
		/// <param name="data">Data sent to server</param>
		/// <returns>Requested resource as string</returns>

		public static async Task<string> DownloadStringAsync(string address, NameValueCollection data = null) {
			using (var client = new WebClient()) {
				if (data != null) {
					var bytes = await client.UploadValuesTaskAsync(address, data);
					return Encoding.UTF8.GetString(bytes);
				} else {
					return await client.DownloadStringTaskAsync(address);
				}
			}
		}

		/// <summary>
		/// Downloads requested resource
		/// </summary>
		/// <param name="address">URL</param>
		/// <param name="data"></param>
		/// <returns>Requested resource as byte[]</returns>

		public static byte[] DownloadData(string address, NameValueCollection data = null) {
			using (var client = new WebClient()) {
				if (data != null) {
					return client.UploadValues(address, data);
				} else {
					return client.DownloadData(address);
				}
			}
		}

		/// <summary>
		/// Downloads requested resource (async)
		/// </summary>
		/// <param name="address">URL</param>
		/// <param name="data"></param>
		/// <returns>Requested resource as byte[]</returns>

		public static async Task<byte[]> DownloadDataAsync(string address, NameValueCollection data = null) {
			using (var client = new WebClient()) {
				if (data != null) {
					return await client.UploadValuesTaskAsync(address, data);
				} else {
					return await client.DownloadDataTaskAsync(address);
				}
			}
		}

		// Add query string to URL
		public static string UrlAddQuery(string url, string qry) => Empty(qry) ? url : url + (url.Contains("?") ? "&" : "?") + qry;

		// Add "hash" parameter to URL
		public static string UrlAddHash(string url, string hash) => UrlAddQuery(url, "hash=" + hash);

		/// <summary>
		/// Form class
		/// </summary>

		public class HttpForm
		{
			public int Index = -1;
			public string FormName = "";

			// Get form element name based on index
			public string GetIndexedName(string name) => (Index < 0) ? name : Regex.Replace(name, "^([a-z]{1,2})_", "${1}" + Index + "_", RegexOptions.IgnoreCase);

			// Has value for form element
			public bool HasValue(string name) => TryGetValue(name, out _);

			// Get value for form element
			public string GetValue(params string[] names) {
				foreach (string name in names) {
					if (TryGetValue(name, out StringValues sv))
						return sv;
				}
				return "";
			}

			// Try get value
			public bool TryGetValue(string name, out StringValues value) {
				string wrkname = GetIndexedName(name);
				if (Regex.IsMatch(name, @"^(fn_)?(x|o)\d*_") && FormName != "") {
					if (Form.TryGetValue(FormName + "$" + wrkname, out value))
						return true;
				}
				return Form.TryGetValue(wrkname, out value);
			}

			// Get int value for form element // DN
			public int GetInt(string name) => ConvertToInt(GetValue(name));

			// Get bool value for form element // DN
			public bool GetBool(string name) => ConvertToBool(GetValue(name));

			// Get file
			public IFormFile GetFile(string name, int index = 0) => Files.GetFiles(GetIndexedName(name))[index];

			// Get files
			public IReadOnlyList<IFormFile> GetFiles(string name) => Files.GetFiles(GetIndexedName(name));

			// Get upload file size
			public long GetUploadFileSize(string name, int index = 0) => GetFile(name, index)?.Length ?? -1;

			// Get upload file name
			public string GetUploadFileName(string name, int index = 0) {
				var file = GetFile(name, index);
				if (file != null) {
					var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Value.Trim('"');
					return Path.GetFileName(fileName);
				}
				return "";
			}

			// Get file content type
			public string GetUploadFileContentType(string name, int index = 0) {
				var file = GetFile(name, index);
				if (file != null) {
					var ct = ContentType(GetUploadFileName(name));
					return (ct != "") ? ct : file.ContentType;
				}
				return "";
			}

			// Get upload file data
			public async Task<byte[]> GetUploadFileData(string name, int index = 0) {
				var file = GetFile(name, index);
				if (file != null && file.Length > 0) {
					using (var stream = new MemoryStream()) {
						await file.CopyToAsync(stream);
						return stream.ToArray();
					}
				}
				return null;
			}

			// Save upload file
			public async Task<bool> SaveUploadFile(string name, string filePath, int index = 0) {
				var file = GetFile(name, index);
				if (file != null && file.Length > 0) {
					using (var stream = new FileStream(filePath, FileMode.Create)) {
						await file.CopyToAsync(stream);
						return true;
					}
				}
				return false;
			}

			// Get file image width and height
			public (int width, int height) GetUploadImageDimension(string name, int index = 0) {
				var file = GetFile(name, index);
				if (file != null && file.Length > 0) {
					try {
						using (var readStream = file.OpenReadStream()) {
							using (var img = System.Drawing.Image.FromStream(readStream)) {
								if (!img.PhysicalDimension.IsEmpty)
									return (width: (int)img.PhysicalDimension.Width, height: (int)img.PhysicalDimension.Height);
							}
						}
					} catch {}
				}
				return (width: -1, height: -1);
			}
		}

		/// <summary>
		/// Login model
		/// </summary>

		public class LoginModel
		{
			[Required]
			public string Username { set; get; }
			[Required]
			public string Password { set; get; }
		}

		/// <summary>
		/// Advanced Security class
		/// </summary>

		public class AdvancedSecurityBase {
			private List<string[]> UserLevel = new List<string[]>();
			private List<string[]> UserLevelPriv = new List<string[]>();
			public List<int> UserLevelID = new List<int>();
			public List<string> UserID = new List<string>();
			public int CurrentUserLevelID;
			public int CurrentUserLevel;
			public string CurrentUserID;
			public string CurrentParentUserID;
			public List<Claim> Claims = new List<Claim>(); // DN
			protected bool AnoymousUserLevelChecked = false; // Dynamic User Level security
			private bool _isLoggedIn;
			private string _userName;

			// Init
			public AdvancedSecurityBase() {

				// User table
				UserTable = UserTable ?? new _s_employee();

				// Init User Level
				if (IsLoggedIn) {
					CurrentUserLevelID = SessionUserLevelID;
					if (IsNumeric(CurrentUserLevelID)) {
						if (CurrentUserLevelID >= -2)
							UserLevelID.Add(CurrentUserLevelID);
					}
				} else { // Anonymous user
					CurrentUserLevelID = -2;
					UserLevelID.Add(CurrentUserLevelID);
				}
				Session[Config.SessionUserLevelList] = UserLevelList();

				// Init User ID
				CurrentUserID = Convert.ToString(SessionUserID);
				CurrentParentUserID = Convert.ToString(SessionParentUserID);

				// Load user level
				LoadUserLevel();
			}

			// Session User ID
			protected object SessionUserID {
				get => Session.TryGetValue(Config.SessionUserId, out string userId) ? userId : CurrentUserID;
				set {
					CurrentUserID = Convert.ToString(value).Trim();
					Session[Config.SessionUserId] = CurrentUserID;
				}
			}

			// Session parent User ID
			protected object SessionParentUserID {
				get => Session.TryGetValue(Config.SessionParentUserId, out string parentUserId) ? parentUserId : CurrentParentUserID;
				set {
					CurrentParentUserID = Convert.ToString(value).Trim();
					Session[Config.SessionParentUserId] = CurrentParentUserID;
				}
			}

			// Current user name
			public string CurrentUserName {
				get => Session.TryGetValue(Config.SessionUserName, out string userName) ? userName : _userName;
				set {
					_userName = Convert.ToString(value).Trim();
					Session[Config.SessionUserName] = _userName;
				}
			}

			// Session User Level ID
			protected int SessionUserLevelID {
				get => Session.TryGetValue(Config.SessionUserLevelId, out string userLevelId) ? Convert.ToInt32(userLevelId) : CurrentUserLevelID;
				set {
					CurrentUserLevelID = value;
					Session.SetString(Config.SessionUserLevelId, Convert.ToString(CurrentUserLevelID));
					if (IsNumeric(CurrentUserLevelID)) {
						if (CurrentUserLevelID >= -2) {
							UserLevelID.Clear();
							UserLevelID.Add(CurrentUserLevelID);
						}
					}
				}
			}

			// Session User Level value
			protected int SessionUserLevel {
				get => Session.TryGetValue(Config.SessionUserLevel, out string userLevel) ? Convert.ToInt32(userLevel) : CurrentUserLevel;
				set {
					CurrentUserLevel = value;
					Session.SetString(Config.SessionUserLevel, Convert.ToString(CurrentUserLevel));
					if (IsNumeric(CurrentUserLevelID)) {
						if (CurrentUserLevelID >= -1) {
							UserLevelID.Clear();
							UserLevelID.Add(CurrentUserLevelID);
						}
					}
				}
			}

			// Can add
			public bool CanAdd {
				get => ((CurrentUserLevel & Config.AllowAdd) == Config.AllowAdd);
				set {
					if (value) {
						CurrentUserLevel |= Config.AllowAdd;
					} else {
						CurrentUserLevel &= ~Config.AllowAdd;
					}
				}
			}

			// Can delete
			public bool CanDelete {
				get => ((CurrentUserLevel & Config.AllowDelete) == Config.AllowDelete);
				set {
					if (value) {
						CurrentUserLevel |= Config.AllowDelete;
					} else {
						CurrentUserLevel &= ~Config.AllowDelete;
					}
				}
			}

			// Can edit
			public bool CanEdit {
				get => ((CurrentUserLevel & Config.AllowEdit) == Config.AllowEdit);
				set {
					if (value) {
						CurrentUserLevel |= Config.AllowEdit;
					} else {
						CurrentUserLevel &= ~Config.AllowEdit;
					}
				}
			}

			// Can view
			public bool CanView {
				get => ((CurrentUserLevel & Config.AllowView) == Config.AllowView);
				set {
					if (value) {
						CurrentUserLevel |= Config.AllowView;
					} else {
						CurrentUserLevel &= ~Config.AllowView;
					}
				}
			}

			// Can list
			public bool CanList {
				get => ((CurrentUserLevel & Config.AllowList) == Config.AllowList);
				set {
					if (value) {
						CurrentUserLevel |= Config.AllowList;
					} else {
						CurrentUserLevel &= ~Config.AllowList;
					}
				}
			}

			// Can report
			public bool CanReport {
				get => ((CurrentUserLevel & Config.AllowReport) == Config.AllowReport);
				set {
					if (value) {
						CurrentUserLevel |= Config.AllowReport;
					} else {
						CurrentUserLevel &= ~Config.AllowReport;
					}
				}
			}

			// Can search
			public bool CanSearch {
				get => ((CurrentUserLevel & Config.AllowSearch) == Config.AllowSearch);
				set {
					if (value) {
						CurrentUserLevel |= Config.AllowSearch;
					} else {
						CurrentUserLevel &= ~Config.AllowSearch;
					}
				}
			}

			// Can admin
			public bool CanAdmin {
				get => ((CurrentUserLevel & Config.AllowAdmin) == Config.AllowAdmin);
				set {
					if (value) {
						CurrentUserLevel |= Config.AllowAdmin;
					} else {
						CurrentUserLevel &= ~Config.AllowAdmin;
					}
				}
			}

			// Can import
			public bool CanImport {
				get => ((CurrentUserLevel & Config.AllowImport) == Config.AllowImport);
				set {
					if (value) {
						CurrentUserLevel |= Config.AllowImport;
					} else {
						CurrentUserLevel &= ~Config.AllowImport;
					}
				}
			}

			// Last URL
			public string LastUrl => Cookie["lasturl"];

			// Save last URL
			public void SaveLastUrl()
			{
				string s = CurrentUrl();
				if (LastUrl == s)
					s = "";
				Cookie["lasturl"] = s;
			}

			// Auto login
			public async Task<bool> AutoLogin()
			{
				bool valid = false;
				var model = new LoginModel();
				if (!valid && SameString(Cookie["autologin"], "autologin")) {
					model.Username = Cookie["username"];
					model.Password = Cookie["password"];
					model.Username = Decrypt(model.Username);
					model.Password = Decrypt(model.Password);
					valid = await ValidateUser(model, true);
				}
				if (!valid && Config.AllowLoginByUrl && Get("username") != "") {
					model.Username = RemoveXss(Get("username"));
					model.Password = RemoveXss(Get("password"));
					valid = await ValidateUser(model, true);
				}
				if (!valid && Config.AllowLoginBySession && Session["Username"] != null) {
					model.Username = Session.GetString(Config.ProjectName + "_Username");
					model.Password = Session.GetString(Config.ProjectName + "_Password");
					valid = await ValidateUser(model, true);
				}
				return valid;
			}

			// Login user
			public void LoginUser(string userName = null, object userID = null, object parentUserID = null, int? userLevel = null)
			{
				_isLoggedIn = true;
				Session[Config.SessionStatus] = "login";
				if (userName != null)
					CurrentUserName = userName;
				if (userID != null)
					SessionUserID = userID;
				if (parentUserID != null)
					SessionParentUserID = parentUserID;
				if (userLevel != null) {
					SessionUserLevelID = (int)userLevel;
					SetupUserLevel();
				}
			}

			// Logout user
			public void LogoutUser()
			{
				_isLoggedIn = false;
				Session[Config.SessionStatus] = "";
				CurrentUserName = "";
				SessionUserID = "";
				SessionParentUserID = "";
				SessionUserLevelID = -2;
				SetupUserLevel();
			}

			#pragma warning disable 162, 168, 1998

			// Validate user
			public async Task<bool> ValidateUser(LoginModel model, bool autologin, string provider = "")
			{
				string usr = model.Username;
				string pwd = model.Password;
				try {
					string filter, sql;
					bool valid = false, providerValid = false;

					// OAuth provider, already login in controller and user profile saved in session // DN
					if (!Empty(provider) && Session[Config.SessionExternalLoginInfo] != null) {
						var _info = Session.GetValue<Dictionary<string, string>>(Config.SessionExternalLoginInfo);
						if (Config.Authentications.TryGetValue(provider, out AuthenticationProvider p) && p.Enabled) {
							Profile.Assign(_info); // Save profile
							_info.TryGetValue(ClaimTypes.Email.Split('/').Last(), out usr);
							providerValid = true;
						} else {
							if (Config.Debug)
								End("Provider for " + provider + " not found or not enabled.");
							return false;
						}
					}

					// Custom validate
					bool customValid = false;

					// Call User Custom Validate event
					if (Config.UseCustomLogin) {
						customValid = User_CustomValidate(ref usr, ref pwd);
					}

					// Handle provider login as custom login
					if (providerValid)
						customValid = true;
					if (customValid) {

						//Session[Config.SessionStatus] = "login"; // To be setup below
						CurrentUserName = usr; // Load user name
					}
					if (!valid) {
						string adminUserName = Config.AdminUserName;
						string adminPassword = Config.AdminPassword;
						if (Config.EncryptionEnabled) {
							adminUserName = AesDecrypt(Config.AdminUserName, Config.EncryptionKey);
							adminPassword = AesDecrypt(Config.AdminPassword, Config.EncryptionKey);
						}
						if (Config.CaseSensitivePassword) {
							valid = (!customValid && adminUserName == usr && adminPassword == pwd) ||
								(customValid && Config.AdminUserName == usr);
						} else {
							valid = (!customValid && SameText(adminUserName, usr)
								&& SameText(adminPassword, pwd)) ||
								(customValid && SameText(adminUserName, usr));
						}
						if (valid) {
							_isLoggedIn = true;
							Session[Config.SessionStatus] = "login";
							Session.SetInt(Config.SessionSysAdmin, 1); // System Administrator
							CurrentUserName = usr; // Load user name
							SessionUserID = -1; // System Administrator
							SessionUserLevelID = -1; // System Administrator
							SetupUserLevel();

							// Sign in // DN
							if (!User.Identities.Any(identity => identity.IsAuthenticated)) {
								Claims.Add(new Claim(ClaimTypes.Name, usr));
								await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
									new ClaimsPrincipal(new ClaimsIdentity(Claims, CookieAuthenticationDefaults.AuthenticationScheme)));
							}
						}
					}

					// Check other users
					if (!valid) {
						filter = Config.UserNameFilter.Replace("%u", AdjustSql(usr, Config.UserTableDbId));
						if (!Empty(Config.UserActivateFilter))
							filter += " AND " + Config.UserActivateFilter;

						// User table object (s_employee)
						UserTable = UserTable ?? new _s_employee();
						UserTableConn = await GetConnectionAsync(UserTable.DbId);

						// Set up filter (WHERE clause)
						sql = UserTable.GetSql(filter); // DN
						using (var rsuser = await UserTableConn.GetDataReaderAsync(sql)) {
							if (await rsuser.ReadAsync()) {
								valid = customValid || ComparePassword(Convert.ToString(rsuser[Config.LoginPasswordFieldName]), pwd);
								if (valid) {
									_isLoggedIn = true;
									Session[Config.SessionStatus] = "login";
									Session.SetInt(Config.SessionSysAdmin, 0); // Non System Administrator
									CurrentUserName = Convert.ToString(rsuser[Config.LoginUsernameFieldName]); // Load user name
									SessionUserID = rsuser[Config.UserIdFieldName]; // Load user ID
									SessionParentUserID = rsuser[Config.ParentUserIdFieldName]; // Load parent user ID
									SessionUserLevelID = ConvertToInt(rsuser[Config.UserLevelFieldName]); // Load user level
									SetupUserLevel();
									Profile.Assign(GetDictionary(rsuser));
									Profile.Remove(Config.LoginPasswordFieldName); // Delete password

									// User Validated event
									valid = User_Validated(rsuser);
									if (valid) { // Sign in // DN
										if (!User.Identities.Any(identity => identity.IsAuthenticated)) {
											Claims.Add(new Claim(ClaimTypes.Name, usr));
											if (!Empty(Config.UserEmailFieldName))
												Claims.Add(new Claim(ClaimTypes.Email, Convert.ToString(rsuser[Config.UserEmailFieldName])));
											await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
												new ClaimsPrincipal(new ClaimsIdentity(Claims, CookieAuthenticationDefaults.AuthenticationScheme)));
										}
									}
								}
							} else { // User not found in user table
								if (customValid) { // Grant default permissions
									SessionUserID = usr; // User name as User ID
									SessionUserLevelID = -2; // Anonymous User Level
									SetupUserLevel();

									// User Validated event
									customValid = User_Validated(null);
									if (customValid) { // Sign in // DN
										if (!User.Identities.Any(identity => identity.IsAuthenticated)) {
											Claims.Add(new Claim(ClaimTypes.Name, usr));
											await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
												new ClaimsPrincipal(new ClaimsIdentity(Claims, CookieAuthenticationDefaults.AuthenticationScheme)));
										}
									}
								}
							}
						}
					}
					Profile.Save();
					if (customValid)
						return customValid;
					if (!valid && !IsPasswordExpired()) {
						_isLoggedIn = false;
						Session[Config.SessionStatus] = ""; // Clear login status
					}
					return valid;
				} finally {
					model.Username = usr;
					model.Password = pwd;
				}
			}

			#pragma warning restore 168, 1998

			// Load user level from config file
			public void LoadUserLevelFromConfigFile(List<string[]> userLevel, List<string[]> userLevelPriv, List<string[]> tables, bool userpriv = false) {

				// User Level definitions
				userLevel.Clear();
				userLevelPriv.Clear();
				tables.Clear();

				// Load user level from config files
				var doc = new XmlDoc();
				string folder = ServerMapPath(Config.ConfigFileFolder);

				// Load user level settings from main config file
				string projectId = CurrentProjectID;
				string file = folder + projectId + ".xml";
				XmlElement projnode;
				if ((projnode = (XmlElement)doc.Load(file)?.SelectSingleNode("//configuration/project")) != null) {
					Config.RelatedProjectId = doc.GetAttribute(projnode, "relatedid");
					var userlevel = doc.GetAttribute(projnode, "userlevel");
					var usergroup = userlevel.Split(';');
					foreach (var group in usergroup) {
						var ar = group.Split(',');
						if (IsArray(ar) && ar.Length > 2) {
							var id = ar[0];
							var name = ar[1];

							// Remove quotes
							if (name.Length >= 2 && name.StartsWith("\"") && name.EndsWith("\""))
								name = name.Substring(1, name.Length-2);
							userLevel.Add(new string[] {id, name});
						}
					}

					// Load from main config file
					LoadUserLevelFromXml(folder, doc, userLevelPriv, tables, userpriv);

					// Load from related config file
					if (!Empty(Config.RelatedProjectId))
						LoadUserLevelFromXml(folder, Config.RelatedProjectId + ".xml", userLevelPriv, tables, userpriv);
				}

				// Warn user if user level not setup
				if (userLevel.Count == 0)
					End("Unable to load user level from config file: " + file);

				// Load user privilege settings from all config files
				var files = SearchFiles(folder, "*.xml");
				foreach (string f in files) {
					file = Path.GetFileName(f);
					if (file != projectId + ".xml" && file != Config.RelatedProjectId + ".xml")
						LoadUserLevelFromXml(folder, file, userLevelPriv, tables, userpriv);
				}
			}

			// Load user level from XML
			protected void LoadUserLevelFromXml(string folder, object file, List<string[]> userLevelPriv, List<string[]> tables, bool userpriv) {
				XmlDoc doc = null;
				if (file is string f) {
					f = folder + f;
					doc = new XmlDoc();
					doc.Load(f);
				} else if (file is XmlDoc xmldoc) {
					doc = xmldoc;
				}
				if (doc is XmlDoc) {

					// Load project ID
					string projectId = "", projectFile = "";
					XmlElement projnode = (XmlElement)doc.SelectSingleNode("//configuration/project");
					if (projnode != null) {
						projectId = doc.GetAttribute(projnode, "id");
						projectFile = doc.GetAttribute(projnode, "file");
						if (projectId == Config.RelatedProjectId)
							Config.RelatedLanguageFolder = IncludeTrailingDelimiter(doc.GetAttribute(projnode, "languagefolder"), true);
					}

					// Load user priv
					var tablelist = doc.SelectNodes("//configuration/project/table");
					foreach (XmlElement tbl in tablelist) {
						XmlElement table = tbl;
						var tablevar = doc.GetAttribute(table, "id");
						var tablename = doc.GetAttribute(table, "name");
						var tablecaption = doc.GetAttribute(table, "caption");
						var userlevel = doc.GetAttribute(table, "userlevel");
						var priv = doc.GetAttribute(table, "priv");
						if (!userpriv || (userpriv && priv == "1")) {
							var usergroup = userlevel.Split(';');
							foreach (var group in usergroup) {
								var ar = group.Split(',');
								if (ar.Length >= 3)
									userLevelPriv.Add(new string[] {projectId + tablename, ar[0], ar[2]});
							}
							tables.Add(new string[] {tablename, tablevar, tablecaption, priv, projectId, projectFile});
						}
					}
				}
			}

			// Dynamic User Level security
			// Get User Level settings from database

			public void SetupUserLevel() {
				SetupUserLevels().GetAwaiter().GetResult(); // Load all user levels

				// User Level loaded event
				UserLevel_Loaded();

				// Save the User Level to Session variable
				SaveUserLevel();
			}

			// Check Anonymous user level
			private bool _anoymousUserLevelChecked = false;

			// Get all User Level settings from database
			public async Task<bool> SetupUserLevels() {

				// Load user level from config file first
				var tables = new List<string[]>();
				var userLevel = new List<string[]>();
				var userLevelPriv = new List<string[]>();
				LoadUserLevelFromConfigFile(userLevel, userLevelPriv, tables);
				string sql;

				// User level table connection
				DatabaseConnectionBase<SqlConnection, SqlCommand, SqlDataReader, SqlDbType> c1 = await GetConnectionAsync(Config.UserLevelDbId);

				// Add Anonymous user level
				if (!_anoymousUserLevelChecked) {
					sql = "SELECT COUNT(*) FROM " + Config.UserLevelTable + " WHERE " + Config.UserLevelIdField + " = -2";
					if (ConvertToInt(await c1.ExecuteScalarAsync(sql)) == 0) {
						sql = "INSERT INTO " + Config.UserLevelTable +
							" (" + Config.UserLevelIdField + ", " + Config.UserLevelNameField + ") VALUES (-2, '" + AdjustSql(Language.Phrase("UserAnonymous"), Config.UserLevelDbId) + "')";
						await c1.ExecuteNonQueryAsync(sql);
					}
				}

				// Get the User Level definitions
				sql = "SELECT " + Config.UserLevelIdField + ", " + Config.UserLevelNameField + " FROM " + Config.UserLevelTable;
				UserLevel = await c1.GetArraysAsync(sql);

				// User level permission table connection
				DatabaseConnectionBase<SqlConnection, SqlCommand, SqlDataReader, SqlDbType> c = await GetConnectionAsync(Config.UserLevelPrivDbId);

				// Add Anonymous user privileges
				if (!_anoymousUserLevelChecked) {
					sql = "SELECT COUNT(*) FROM " + Config.UserLevelPrivTable + " WHERE " + Config.UserLevelPrivUserLevelIdField + " = -2";
					if (ConvertToInt(await c.ExecuteScalarAsync(sql)) == 0) {
						var wrkUserLevel = new List<string[]>();
						var wrkUserLevelPriv = new List<string[]>();
						var wrkTable = new List<string[]>();
						LoadUserLevelFromConfigFile(wrkUserLevel, wrkUserLevelPriv, wrkTable, true);
						foreach (string[] table in wrkTable) {
							int wrkPriv = ConvertToInt(wrkUserLevelPriv.FirstOrDefault(userpriv => SameString(userpriv[0], table[4] + table[0]) && SameString(userpriv[1], "-2"))?[2]);
							sql = "INSERT INTO " + Config.UserLevelPrivTable +
								" (" + Config.UserLevelPrivUserLevelIdField + ", " + Config.UserLevelPrivTableNameField + ", " + Config.UserLevelPrivPrivField +
								") VALUES (-2, '" + AdjustSql(table[4] + table[0], Config.UserLevelPrivDbId) + "', " + wrkPriv + ")";
							await c.ExecuteNonQueryAsync(sql);
						}
					}
					_anoymousUserLevelChecked = true;
				}

				// Get the User Level privileges
				var userPrivSql = "SELECT " + Config.UserLevelPrivTableNameField + ", " + Config.UserLevelPrivUserLevelIdField + ", " + Config.UserLevelPrivPrivField + " FROM " + Config.UserLevelPrivTable;
				if (!IsAdmin() && UserLevelID.Count > 0) {
					userPrivSql += " WHERE " + Config.UserLevelPrivUserLevelIdField + " IN (" + UserLevelList() + ")";
					Session[Config.SessionUserLevelListLoaded] = UserLevelList(); // Save last loaded list
				} else {
					Session[Config.SessionUserLevelListLoaded] = ""; // Save last loaded list
				}
				UserLevelPriv = await c.GetArraysAsync(userPrivSql);

				// Update User Level privileges record if necessary
				string projectId = CurrentProjectID;
				int reloadUserPriv = 0;

				// Update table without prefix
				sql = "SELECT COUNT(*) FROM " + Config.UserLevelPrivTable + " WHERE EXISTS(SELECT * FROM " +
						Config.UserLevelPrivTable + " WHERE " + Config.UserLevelPrivTableNameField + " NOT LIKE '{%')";
				if (ConvertToInt(await c.ExecuteScalarAsync(sql)) > 0) {
					var ar = tables.Select(t => "'" + AdjustSql(t[0], Config.UserLevelPrivDbId) + "'");
					sql = "UPDATE " + Config.UserLevelPrivTable + " SET " +
						Config.UserLevelPrivTableNameField + " = " + c.Concat("'" + AdjustSql(projectId, Config.UserLevelPrivDbId) + "'", Config.UserLevelPrivTableNameField) + " WHERE " +
						Config.UserLevelPrivTableNameField + " IN (" + String.Join(",", ar) + ")";
					reloadUserPriv = await c.ExecuteNonQueryAsync(sql);
				}

				// Update table with report prefix
				if (!Empty(Config.RelatedProjectId)) {
					sql = "SELECT COUNT(*) FROM " + Config.UserLevelPrivTable + " WHERE EXISTS(SELECT * FROM " +
						Config.UserLevelPrivTable + " WHERE " + Config.UserLevelPrivTableNameField + " LIKE '" +
						AdjustSql(Config.TablePrefix, Config.UserLevelPrivDbId) + "%')";
					if (ConvertToInt(await c.ExecuteScalarAsync(sql)) > 0) {
						var ar = tables.Select(t => "'" + AdjustSql(Config.TablePrefix + t[0], Config.UserLevelPrivDbId) + "'");
						sql = "UPDATE " + Config.UserLevelPrivTable + " SET " +
							Config.UserLevelPrivTableNameField + " = REPLACE(" + Config.UserLevelPrivTableNameField + "," +
							"'" + AdjustSql(Config.TablePrefix, Config.UserLevelPrivDbId) + "','" + AdjustSql(Config.RelatedProjectId, Config.UserLevelPrivDbId) + "') WHERE " +
							Config.UserLevelPrivTableNameField + " IN (" + String.Join(",", ar) + ")";
						reloadUserPriv += await c.ExecuteNonQueryAsync(sql);
					}
				}

				// Reload the User Level privileges
				if (reloadUserPriv > 0)
					UserLevelPriv = await c.GetArraysAsync(userPrivSql);

				// Warn user if user level not setup
				if (UserLevelPriv.Count == 0 && IsAdmin() && CurrentPage != null && Empty(Session[Config.SessionUserLevelMessage])) {
					if (CurrentPage != null)
						CurrentPage.FailureMessage = Language.Phrase("NoUserLevel");
					Session[Config.SessionUserLevelMessage] = "1"; // Show only once
					return false; // To be redirected in userpriv // DN
				}
				return true;
			}

			// Add user permission
			public void AddUserPermission(string userLevelName, string tableName, int userPermission)
			{
				string UserLevelID = "";

				// Get user level ID from user name
				if (IsList(UserLevel))
					UserLevelID = Convert.ToString(UserLevel.FirstOrDefault(row => SameString(userLevelName, row[1]))?[0]);
				if (IsList(UserLevelPriv) && !Empty(UserLevelID)) {
					string[] row = UserLevelPriv.FirstOrDefault(r => SameString(r[0], Config.ProjectId + tableName) && SameString(r[1], UserLevelID));
					if (row != null)
						row[2] = Convert.ToString(ConvertToInt(row[2]) | userPermission); // Add permission
				}
			}

			// Add user permission
			public void AddUserPermission(List<string> userLevelName, List<string> tableName, int userPermission) =>
				userLevelName.ForEach(_userLevelName => tableName.ForEach(_tableName => AddUserPermission(_userLevelName, _tableName, userPermission)));

			// Add user permission
			public void AddUserPermission(string userLevelName, List<string> tableName, int userPermission) =>
				tableName.ForEach(name => AddUserPermission(userLevelName, name, userPermission));

			// Add user permission
			public void AddUserPermission(List<string> userLevelName, string tableName, int userPermission) =>
				userLevelName.ForEach(name => AddUserPermission(name, tableName, userPermission));

			// Delete user permission
			public void DeleteUserPermission(string userLevelName, string tableName, int userPermission)
			{
				string UserLevelID = "";

				// Get user level ID from user name
				if (IsList(UserLevel))
					UserLevelID = Convert.ToString(UserLevel.FirstOrDefault(row => SameString(userLevelName, row[1]))?[0]);
				if (IsList(UserLevelPriv) && !Empty(UserLevelID)) {
					string[] row = UserLevelPriv.FirstOrDefault(r => SameString(r[0], Config.ProjectId + tableName) && SameString(r[1], UserLevelID));
					if (row != null)
						row[2] = Convert.ToString(ConvertToInt(row[2]) & ~userPermission); // Remove permission
				}
			}

			// Delete user permission
			public void DeleteUserPermission(List<string> userLevelName, List<string> tableName, int userPermission) =>
				userLevelName.ForEach(_userLevelName => tableName.ForEach(_tableName => DeleteUserPermission(_userLevelName, _tableName, userPermission)));

			// Delete user permission
			public void DeleteUserPermission(string userLevelName, List<string> tableName, int userPermission) =>
				tableName.ForEach(name => DeleteUserPermission(userLevelName, name, userPermission));

			// Delete user permission
			public void DeleteUserPermission(List<string> userLevelName, string tableName, int userPermission) =>
				userLevelName.ForEach(name => DeleteUserPermission(name, tableName, userPermission));

			// Load current user level
			public void LoadCurrentUserLevel(string table)
			{

				// Load again if user level list changed
				if (!Empty(Session[Config.SessionUserLevelListLoaded]) &&
					!SameString(Session[Config.SessionUserLevelListLoaded], Session[Config.SessionUserLevelList])) { // Compare strings, not objects // DN
					Session[Config.SessionUserLevelPrivArrays] = null;
				}
				LoadUserLevel();
				SessionUserLevel = CurrentUserLevelPriv(table);
			}

			// Get current user privilege
			protected int CurrentUserLevelPriv(string tableName)
			{
				if (IsLoggedIn) {
					return UserLevelID.Aggregate(0, (result, id) => result | GetUserLevelPriv(tableName, id));
				} else { // Anonymous
					return GetUserLevelPriv(tableName, -2);
				}
			}

			// Get user level ID by user level name
			public int GetUserLevelID(string userLevelName)
			{
				if (SameString(userLevelName, "Anonymous")) {
					return -2;
				} else if (SameString(userLevelName, Language?.Phrase("UserAnonymous"))) {
					return -2;
				} else if (SameString(userLevelName, "Administrator")) {
					return -1;
				} else if (SameString(userLevelName, Language?.Phrase("UserAdministrator"))) {
					return -1;
				} else if (SameString(userLevelName, "Default")) {
					return 0;
				} else if (SameString(userLevelName, Language?.Phrase("UserDefault"))) {
					return 0;
				} else if (!Empty(userLevelName)) {
					if (IsList(UserLevel)) {
						var row = UserLevel.FirstOrDefault(r => SameString(r[1], userLevelName));
						if (row != null)
							return ConvertToInt(row[0]);
					}
				}
				return -2; // Anonymous
			}

			// Add Add User Level by name
			public void AddUserLevel(string userLevelName)
			{
				if (Empty(userLevelName))
					return;
				int id = GetUserLevelID(userLevelName);
				AddUserLevelID(id);
			}

			// Add User Level by ID
			public void AddUserLevelID(int id)
			{
				if (!IsNumeric(id) || id < -1)
					return;
				if (!UserLevelID.Contains(id)) {
					UserLevelID.Add(id);
					Session[Config.SessionUserLevelList] = UserLevelList(); // Update session variable
				}
			}

			// Delete User Level by name
			public void DeleteUserLevel(string userLevelName)
			{
				if (Empty(userLevelName))
					return;
				int id = GetUserLevelID(userLevelName);
				DeleteUserLevelID(id);
			}

			// Delete User Level by ID
			public void DeleteUserLevelID(int id)
			{
				if (!IsNumeric(id) || id < -1)
					return;
				if (UserLevelID.Contains(id)) {
					UserLevelID.Remove(id);
					Session[Config.SessionUserLevelList] = UserLevelList(); // Update session variable
				}
			}

			// User level list
			public string UserLevelList() => String.Join(", ", UserLevelID);

			// User level name list
			public string UserLevelNameList()
			{
				string list = "";
				list = String.Join(", ", UserLevelID.Select(id => QuotedValue(GetUserLevelName(id), Config.DataTypeString, Config.UserLevelDbId)));
				return list;
			}

			// Get user privilege based on table name and user level
			public int GetUserLevelPriv(string tableName, int userLevelId)
			{
				if (SameString(userLevelId, "-1")) { // System Administrator
					return Config.AllowAll;
				} else if (userLevelId >= 0 || SameString(userLevelId, "-2")) {
					if (IsList(UserLevelPriv))
						return ConvertToInt(UserLevelPriv.FirstOrDefault(row => SameString(row[0], tableName) && SameString(row[1], userLevelId))?[2]);
				}
				return 0;
			}

			// Get current user level name
			public string CurrentUserLevelName => GetUserLevelName(CurrentUserLevelID);

			// Get user level name based on user level
			public string GetUserLevelName(int userLevelId, bool lang = true)
			{
				if (SameString(userLevelId, "-2")) {
					return lang ? Language.Phrase("UserAnonymous") : "Anonymous";
				} else if (SameString(userLevelId, "-1")) {
					return lang ? Language.Phrase("UserAdministrator") : "Administrator";
				} else if (SameString(userLevelId, "0")) {
					return lang ? Language.Phrase("UserDefault") : "Default";
				} else if (userLevelId > 0) {
					if (IsList(UserLevel)) {
						string name = UserLevel.FirstOrDefault(row => SameString(row[0], userLevelId))?[1];
						string userLevelName = lang ? Language.Phrase(name) : "";
						return (userLevelName != "") ? userLevelName : name;
					}
				}
				return "";
			}

			// Display all the User Level settings (for debug only)
			public void ShowUserLevelInfo()
			{
				if (IsList(UserLevel)) {
					Write("User Levels:<br>");
					Write("UserLevelId, UserLevelName<br>");
					Write(UserLevel.Aggregate("", (result, row) => result + "&nbsp;&nbsp;" + String.Join(", ", row) + "<br>"));
				} else {
					Write("No User Level definitions." + "<br>");
				}
				if (IsList(UserLevelPriv)) {
					Write("User Level Privs:<br>");
					Write("TableName, UserLevelId, UserLevelPriv<br>");
					Write(UserLevelPriv.Aggregate("", (result, row) => result + "&nbsp;&nbsp;" + String.Join(", ", row) + "<br>"));
				} else {
					Write("No User Level privilege settings." + "<br>");
				}
				Write("CurrentUserLevel = " + CurrentUserLevel + "<br>");
				Write("User Levels = " + UserLevelList() + "<br>");
			}

			// Check privilege for List page (for menu items)
			public bool AllowList(string tableName) => ConvertToBool(CurrentUserLevelPriv(tableName) & Config.AllowList);

			// Check privilege for View page (for Allow-View / Detail-View)
			public bool AllowView(string tableName) => ConvertToBool(CurrentUserLevelPriv(tableName) & Config.AllowView);

			// Check privilege for Add page (for Allow-Add / Detail-Add)
			public bool AllowAdd(string tableName) => ConvertToBool(CurrentUserLevelPriv(tableName) & Config.AllowAdd);

			// Check privilege for Edit page (for Detail-Edit)
			public bool AllowEdit(string tableName) => ConvertToBool(CurrentUserLevelPriv(tableName) & Config.AllowEdit);

			// Check if user password expired
			public bool IsPasswordExpired => SameString(Session[Config.SessionStatus], "passwordexpired");

			// Set session password expired
			public void SetSessionPasswordExpired() => Session[Config.SessionStatus] = "passwordexpired";

			// Check if user is logging in (after changing password)
			public bool IsLoggingIn => SameString(Session[Config.SessionStatus], "loggingin");

			// Check if user is logged in
			public bool IsLoggedIn => _isLoggedIn || SameString(Session[Config.SessionStatus], "login");

			// Check if user is system administrator
			public bool IsSysAdmin => (Session.GetInt(Config.SessionSysAdmin) == 1);

			// Check if user is administrator
			public bool IsAdmin
			{
				get {
					bool res = IsSysAdmin;
					if (!res)
						res = SameString(CurrentUserLevelID, "-1") || UserLevelID.Contains(-1) || CanAdmin;
					if (!res)
						res = SameString(CurrentUserID, "-1") || UserID.Contains("-1");
					return res;
				}
			}

			// Save user level to session
			public void SaveUserLevel()
			{
				Session.SetValue(Config.SessionUserLevelArrays, UserLevel); // DN
				Session.SetValue(Config.SessionUserLevelPrivArrays, UserLevelPriv); // DN
			}

			// Load user level from session // DN
			public void LoadUserLevel()
			{
				if (Session[Config.SessionUserLevelArrays] == null || Session[Config.SessionUserLevelPrivArrays] == null) { // DN
					SetupUserLevel();
				} else {
					UserLevel = Session.GetValue<List<string[]>>(Config.SessionUserLevelArrays);
					UserLevelPriv = Session.GetValue<List<string[]>>(Config.SessionUserLevelPrivArrays);
				}
			}

			// Get user email
			public string CurrentUserEmail
			{
				get {
					var email = User.FindFirst(ClaimTypes.Email)?.Value;
					if (Empty(email) && !Empty(Config.UserEmailFieldName))
						email = Convert.ToString(CurrentUserInfo(Config.UserEmailFieldName));
					return email;
				}
			}

			// Get user info
			public async Task<object> GetUserInfo(string fieldName, object userid)
			{
				object Info = null;
				if (Empty(userid))
					return Info;
				try {

					// Get SQL from getSql() method in <UserTable> class
					string filter = Config.UserIdFilter.Replace("%u", AdjustSql(userid, Config.UserTableDbId));
					string sql = UserTable.GetSql(filter); // DN
					var row = await UserTableConn.GetRowAsync(sql);
					if (row != null)
						Info = row[fieldName];
					return Info;
				} catch {
					if (Config.Debug)
						throw;
					return Info;
				}
			}

			// Get user ID value by user login name
			public object GetUserIDByUserName(string userName)
			{
				object userId = "";
				if (!Empty(userName)) {
					string filter = Config.UserNameFilter.Replace("%u", AdjustSql(userName, Config.UserTableDbId));
					string sql = UserTable.GetSql(filter); // DN
					var row = UserTableConn.GetRow(sql);
					if (row != null)
						userId = row[Config.UserIdFieldName];
				}
				return userId;
			}

			// Load user ID
			public async Task LoadUserID()
			{
				if (Empty(CurrentUserID)) {

					// Handle empty User ID here
				} else if (!SameString(CurrentUserID, "-1")) { // Get first level
					AddUserID(CurrentUserID);
					UserTable = UserTable ?? new _s_employee();
					UserTableConn = await GetConnectionAsync(UserTable.DbId);
					string filter = UserTable.GetUserIDFilter(CurrentUserID);
					string sql = UserTable.GetSql(filter);
					DbDataReader rsuser;
					using (rsuser = await UserTableConn.OpenDataReaderAsync(sql)) {
						while (await rsuser.ReadAsync())
							AddUserID(rsuser[Config.UserIdFieldName]);
					}

					// Recurse all levels (hierarchical user id)
					string userIdList, currentUserIdList;
					if (Config.UserIdIsHierarchical) {
						currentUserIdList = UserIDList();
						userIdList = "";
						while (userIdList != currentUserIdList) {
							filter = "[report_to] IN (" + currentUserIdList + ")";
							sql = UserTable.GetSql(filter);
							using (rsuser = await UserTableConn.GetDataReaderAsync(sql)) {
								while (await rsuser.ReadAsync())
									AddUserID(rsuser[Config.UserIdFieldName]);
							}
							userIdList = currentUserIdList;
							currentUserIdList = UserIDList();
						}
					}
				}
			}

			// Add user name
			public void AddUserName(string userName) => AddUserID(GetUserIDByUserName(userName));

			// Add user name
			public void AddUserIDByUserName(string userName) => AddUserName(userName);

			// Add user ID
			public void AddUserID(object id)
			{
				if (Empty(id))
					return;
				if (!IsNumeric(id))
					return;
	 			string userId = Convert.ToString(id);
				if (!UserID.Contains(userId))
					UserID.Add(userId);
			}

			// Delete user name
			public void DeleteUserName(string userName) => DeleteUserID(GetUserIDByUserName(userName));

			// Delete user name
			public void DeleteUserIDByUserName(string userName) => DeleteUserName(userName);

			// Delete user ID
			public void DeleteUserID(object id)
			{
				if (Empty(id))
					return;
				if (!IsNumeric(id))
					return;
				UserID.Remove(Convert.ToString(id));
			}

			// User ID list
			public string UserIDList() =>
				String.Join(", ", UserID.Select(id => QuotedValue(id, Config.DataTypeNumber, Config.UserTableDbId)));

			// Parent user ID list
			public string ParentUserIDList(object userid)
			{
				if (SameString(userid, CurrentUserID) && !Empty(CurrentParentUserID)) // Own record
					return QuotedValue(CurrentParentUserID, Config.DataTypeNumber, Config.UserTableDbId);
				if (!Config.UserIdIsHierarchical) { // One level only, must be CurrentUserID
					return QuotedValue(CurrentUserID, Config.DataTypeNumber, Config.UserTableDbId);
				} else { // Hierarchical, all users except userid
					return String.Join(", ", UserID.Where(id => !SameString(id, userid)).Select(id => QuotedValue(id, Config.DataTypeNumber, Config.UserTableDbId)));
				}
			}

			// List of allowed user ids for this user
			public bool IsValidUserID(object id) => IsLoggedIn && (UserID.Contains(Convert.ToString(id)) || SameString(id, CurrentUserID));

			// Get user info
			public async Task<object> CurrentUserInfoAsync(string fldname)
			{
				object info = null;
				try {
					info = await GetUserInfo(fldname, CurrentUserID);
					if (info == null && IsAuthenticated())
						info = User.FindFirst(fldname)?.Value;
					return info;
				} catch {
					if (Config.Debug)
						throw;
					return info;
				}
			}

			// Get user info
			public object CurrentUserInfo(string fldname) => CurrentUserInfoAsync(fldname).GetAwaiter().GetResult();

			// UserID Loading event
			public virtual void UserID_Loading() {

				//Log("UserID Loading: {UserID}", CurrentUserID);
			}

			// UserID Loaded event
			public virtual void UserID_Loaded() {

				//Log("UserID Loaded: {UserIDList}", UserIDList());
			}

			// User Level Loaded event
			public virtual void UserLevel_Loaded() {

				//AddUserPermission(<UserLevelName>, <TableName>, <UserPermission>);
				//DeleteUserPermission(<UserLevelName>, <TableName>, <UserPermission>);

			}

			// Table Permission Loading event
			public virtual void TablePermission_Loading() {

				//Log("Table Permission Loading: {UserLevel}", CurrentUserLevelID);
			}

			// Table Permission Loaded event
			public virtual void TablePermission_Loaded() {

				//Log("Table Permission Loaded: {UserLevel}", CurrentUserLevel);
			}

			// User Custom Validate event
			public virtual bool User_CustomValidate(ref string usr, ref string pwd) {

				// Enter your custom code to validate user, return TRUE if valid.
				return false;
			}

			// User Validated event
			public virtual bool User_Validated(DbDataReader rs) {
				return true; // Return true/false to validate the user
			}

			// User PasswordExpired event
			public virtual void User_PasswordExpired(DbDataReader rs) {

				//Log("User_PasswordExpired");
			}
		}

		// User Profile Class

		#pragma warning disable 169

		/// <summary>
		/// UserProfile class
		/// </summary>

		public class UserProfile
		{
			private Dictionary<string, string> _profile = new Dictionary<string, string>();
			public int TimeoutTime;
			public int MaxRetryCount;
			public int RetryLockoutTime;
			public int PasswordExpiryTime;
			private Dictionary<string, string> _tempProfile;
			private Dictionary<string, string> _allfilters = new Dictionary<string, string>();

			// Constructor
			public UserProfile()
			{
				Load();
			}

			// Contains key // DN
			public bool ContainsKey(string name) => _profile.ContainsKey(name);

			// Get value
			public string GetValue(string name) => _profile.TryGetValue(name, out string value) ? value : "";

			// Set value
			public void SetValue(string name, string value) => _profile[name] = value;

			// Get/Set as string
			public string this[string name] {
				get => GetValue(name);
				set => SetValue(name, value);
			}

			// Try get value
			public bool TryGetValue(string name, out string value) => _profile.TryGetValue(name, out value);

			// Delete property
			public void Remove(string name) => _profile.Remove(name);

			// Backup profile
			public void Backup() {
				if (_profile != null)
					_tempProfile = new Dictionary<string, string>(_profile);
			}

			// Restore profile
			public void Restore() {
				if (_tempProfile != null)
					_profile = new Dictionary<string, string>(_tempProfile);
			}

			// Assign properties
			public void Assign(Dictionary<string, object> input) {
				foreach (var (key, value) in input) {
					if (value == null || IsDBNull(value))
						SetValue(key, null);
					else if (value is bool || IsNumeric(value) || value is string && Convert.ToString(value).Length <= Config.DataStringMaxLength)
						SetValue(key, Convert.ToString(value));
				}
			}

			// Assign properties
			public void Assign(Dictionary<string, string> input) {
				foreach (var (key, value) in input)
					SetValue(key, value);
			}

			// Check if System Admin
			protected bool IsSysAdmin(string user) {
				string adminUserName = Config.EncryptionEnabled ? AesDecrypt(Config.AdminUserName, Config.EncryptionKey): Config.AdminUserName;
				return (user == "" || user == adminUserName || user == Language.Phrase("UserAdministrator"));
			}

			// Load profile from session
			public void Load() => LoadProfile(Session.GetString(Config.SessionUserProfile));

			// Save profile to session
			public void Save() => Session[Config.SessionUserProfile] = ProfileToString();

			// Load profile
			public void LoadProfile(string str)
			{
				if (!Empty(str) && !SameString(str, "{}")) // DN
					_profile = StringToProfile(str);
			}

			// Clear profile
			public void ClearProfile() => _profile.Clear();

			// Serialize profile
			public string ProfileToString() => JsonConvert.SerializeObject(_profile);

			// Split profile
			private Dictionary<string, string> StringToProfile(string str)
			{
				try {
					return JsonConvert.DeserializeObject<Dictionary<string, string>>(str);
				} catch {
					return new Dictionary<string, string>();
				}
			}
		}

		#pragma warning restore 169

		/// <summary>
		/// Menu class
		/// </summary>

		public class MenuBase
		{
			public object Id;
			public bool IsRoot;
			public bool IsNavbar;
			public bool Accordion = true; // For sidebar menu only
			public List<MenuItem> Items = new List<MenuItem>();
			public bool UseSubmenu;
			public static bool Cache = false;
			protected Lang _language;
			private bool? _renderMenu = null;
			private bool? _isOpened = null;

			// Constructor
			public MenuBase(object id, bool isRoot = false, bool isNavbar = false, string languageFolder = null)
			{
				Id = id;
				IsRoot = isRoot;
				IsNavbar = isNavbar;
				if (isNavbar) {
					UseSubmenu = true;
					Accordion = false;
				}
				if (Empty(languageFolder))
					_language = Language;
				else
					_language = new Lang(languageFolder, CurrentLanguageID);
			}

			// Add a menu item
			public void AddMenuItem(int id, string name, string text, string url, int parentId = -1, string src = "", bool allowed = true, bool isHeader = false, bool isCustomUrl = false, string icon = "", string label = "", bool isNavbarItem = false)
			{
				var item = new MenuItem(id, name, text, !Empty(url) ? AppPath(url) : "", parentId, allowed, isHeader, isCustomUrl, icon, label, isNavbarItem); // DN ***
				if (!MenuItem_Adding(item))
					return;
				if (item.ParentId < 0) {
					AddItem(item);
				} else {
					if (FindItem(item.ParentId, out MenuItem parentMenu))
						parentMenu.AddItem(item);
				}
			}
			public string Phrase(string menuId, string id) => _language.MenuPhrase(menuId, id);

			// Add a menu item (for backward compatibility) // DN
			public void AddMenuItem(int id, string text, string url, int parentId = -1, string src = "", bool allowed = true, bool isHeader = false, bool isCustomUrl = false) =>
				AddMenuItem(id, "mi_" + Convert.ToString(id), text, url, parentId, src, allowed, isHeader, isCustomUrl);

			// Get menu item count
			public int Count => Items.Count;

			// Add item
			public void AddItem(MenuItem item) {
				Items.Add(item);
				item.Menu = this;
			}

			// Clear all menu items
			public void Clear() => Items.Clear();

			// Find item
			public bool FindItem(int id, out MenuItem outitem)
			{
				outitem = null;
				foreach (var item in Items) {
					if (item.Id == id) {
						outitem = item;
						return true;
					} else if (item.Submenu != null) {
						if (item.Submenu.FindItem(id, out outitem))
							return true;
					}
				}
				return false;
			}

			// Find item by menu text
			public bool FindItemByText(string txt, out MenuItem outitem)
			{
				outitem = null;
				foreach (var item in Items) {
					if (item.Text == txt) {
						outitem = item;
						return true;
					} else if (item.Submenu != null) {
						if (item.Submenu.FindItemByText(txt, out outitem))
							return true;
					}
				}
				return false;
			}

			// Move item to position
			public void MoveItem(string text, int pos)
			{
				int oldpos = 0;
				int newpos = pos;
				bool found = false;
				if (pos < 0) {
					pos = 0;
				} else if (pos > Items.Count) {
					pos = Items.Count;
				}
				MenuItem currentItem = Items.Find(item => SameString(item.Text, text));
				if (currentItem != null) {
					found = true;
					oldpos = Items.IndexOf(currentItem);
				} else {
					found = false;
				}
				if (found && pos != oldpos) {
					Items.RemoveAt(oldpos); // Remove old item
					if (oldpos < pos)
						newpos--; // Adjust new position
					Items.Insert(newpos, currentItem); // Insert new item
				}
			}

			// Check if this menu should be rendered
			public bool RenderMenu {
				get {
					if (!_renderMenu.HasValue)
						_renderMenu = Items.Any(item => item.CanRender);
					return _renderMenu.Value;
				}
			}

			// Check if this menu should be opened
			public bool IsOpened {
				get {
					if (!_isOpened.HasValue)
						_isOpened = Items.Any(item => item.IsOpened);
					return _isOpened.Value;
				}
			}

			// Render the menu as JSON
			public virtual async Task<string> ToJson() {
				if (Cache && IsLoggedIn() && Session.TryGetValue("__menu__" + Id, out string _json))
					return _json;
				if (!RenderMenu || Count == 0)
					return "null";

				// Write JSON
				var sw = new StringWriter();
				using (var writer = new JsonTextWriter(sw)) {
					if (IsRoot) {
						await writer.WriteStartObjectAsync();
						await writer.WritePropertyNameAsync("accordion");
						await writer.WriteValueAsync(Accordion);
						await writer.WritePropertyNameAsync("items");
					}
					await writer.WriteStartArrayAsync();
					foreach (MenuItem item in Items) {
						if (item.CanRender) {
							if (item.IsHeader && (!IsRoot || !UseSubmenu)) { // Group title (Header)
								await writer.WriteRawValueAsync(await item.ToJson(false));
								if (item.Submenu != null) {
									foreach (MenuItem subitem in item.Submenu.Items) {
										if (subitem.CanRender)
											await writer.WriteRawValueAsync(await subitem.ToJson());
									}
								}
							} else {
								await writer.WriteRawValueAsync(await item.ToJson());
							}
						}
					}
					await writer.WriteEndArrayAsync();
					if (IsRoot)
						await writer.WriteEndObjectAsync();
				}
				string json = sw.ToString();
				Menu_Rendered(ref json);
				if (Cache && IsLoggedIn())
					Session["__menu__" + Id] = json;
				return json;
			}

			// Clear cache
			public void ClearCache() => Session.Remove("__menu__" + Id);

			// Returns the menu as script tag
			public async Task<string> ToScript() => "<script>ew.vars." + Id + " = " + await ToJson() + ";</script>";

			// MenuItem_Adding
			public virtual bool MenuItem_Adding(MenuItem item) => true;

			// Menu_Rendering
			public virtual void Menu_Rendering() {}

			// Menu_Rendered
			public virtual void Menu_Rendered(ref string json) {}
		}

		/// <summary>
		/// Menu item class
		/// </summary>

		public class MenuItem
		{
			public int Id;
			public string Name;
			public string Text;
			public string Url;
			public int ParentId;
			public MenuBase Submenu = null;
			public bool Allowed = true;
			public string Target = "";
			public bool IsHeader = false;
			public bool IsCustomUrl = false;
			public string Href = "";
			public string Icon = "";
			public string Attributes = "";
			public string Label = ""; // HTML (for vertical menu only)
			public bool IsNavbarItem = false;
			public MenuBase Menu;
			private bool? _active = null;

			// Constructor
			public MenuItem(int id, string name, string text, string url, int parentId = -1, bool allowed = true, bool isHeader = false, bool isCustomUrl = false, string icon = "", string label = "", bool isNavbarItem = false)
			{
				Id = id;
				Name = name;
				Text = text;
				Url = url;
				ParentId = parentId;
				Allowed = allowed;
				IsHeader = isHeader;
				IsCustomUrl = isCustomUrl;
				Icon = icon;
				Label = label;
				IsNavbarItem = isNavbarItem;
			}

			// Can render
			public bool CanRender => Allowed && !Empty(Url) || (Submenu?.RenderMenu ?? false);

			// Is opened
			public bool IsOpened => Active || (Submenu?.IsOpened ?? false);

			// Is active // DN
			public bool Active {
				get {
					if (!_active.HasValue) {
						if (IsCustomUrl)
							_active = SameText(CurrentUrl(), Url);
						else
							_active = SameText(CurrentPageName(), GetPageName(Url));
					}
					return _active.Value;
				}
			}

			// Add submenu item
			public void AddItem(MenuItem item) {
				Submenu = Submenu ?? new MenuBase(Id);
				Submenu.AddItem(item);
			}

			// Render
			public async Task<string> ToJson(bool deep = true) {
				if (Active || Submenu != null && Url != Config.UndefinedUrl && Menu.IsNavbar && Menu.IsRoot) { // Active or navbar root menu item with submenu // DN
					Attributes = "data-url=\"" + Url + "\"";
					Url = Config.UndefinedUrl; // Does not support URL for root menu item with submenu
				}
				Url = !Empty(Url) ? GetUrl(Url) : ""; // DN
				if (IsMobile() && !IsCustomUrl && Url != Config.UndefinedUrl)
					Url = Url.Replace("#", (Url.Contains("?") ? "&" : "?") + "hash=");
				if (Empty(Url))
					Url = Config.UndefinedUrl;
				Attributes = Attributes.Trim();
				if (!Empty(Attributes))
					Attributes = " " + Attributes;
				string className = Icon.Trim();
				if (!Empty(className)) {
					var list = className.Split(' ').ToList();
					if (list.Any(name => name.StartsWith("fa-")) && !list.Contains("fa"))
						list.Add("fa");
					Icon = String.Join(" ", list);
				}
				var sw = new StringWriter();
				using (var writer = new JsonTextWriter(sw)) {
					await writer.WriteStartObjectAsync();
					await writer.WritePropertyNameAsync("id");
					await writer.WriteValueAsync(Id);
					await writer.WritePropertyNameAsync("name");
					await writer.WriteValueAsync(Name);
					await writer.WritePropertyNameAsync("text");
					await writer.WriteValueAsync(Text);
					await writer.WritePropertyNameAsync("parentId");
					await writer.WriteValueAsync(ParentId);
					await writer.WritePropertyNameAsync("target");
					await writer.WriteValueAsync(Target);
					await writer.WritePropertyNameAsync("isHeader");
					await writer.WriteValueAsync(IsHeader);
					await writer.WritePropertyNameAsync("href");
					await writer.WriteValueAsync(Url);
					await writer.WritePropertyNameAsync("active");
					await writer.WriteValueAsync(Active);
					await writer.WritePropertyNameAsync("icon");
					await writer.WriteValueAsync(Icon);
					await writer.WritePropertyNameAsync("attributes");
					await writer.WriteValueAsync(Attributes);
					await writer.WritePropertyNameAsync("label");
					await writer.WriteValueAsync(Label);
					await writer.WritePropertyNameAsync("isNavbarItem");
					await writer.WriteValueAsync(IsNavbarItem);
					await writer.WritePropertyNameAsync("open");
					await writer.WriteValueAsync((deep && Submenu != null) ? Submenu.IsOpened : false);
					await writer.WritePropertyNameAsync("items");
					if (deep && Submenu != null)
						await writer.WriteRawValueAsync(await Submenu.ToJson());
					else
						await writer.WriteNullAsync();
					await writer.WriteEndObjectAsync();
				}
				return sw.ToString();
			}
		}

		/// <summary>
		/// Allow list
		/// </summary>
		/// <param name="tableName">Table name</param>
		/// <returns>Whether user has permission to access the List page of the table</returns>

		public static bool AllowList(string tableName) => Security?.AllowList(tableName) ?? true;

		/// <summary>
		/// Allow add
		/// </summary>
		/// <param name="tableName">Table name</param>
		/// <returns>Whether user has permission to access the Add page of the table</returns>

		public static bool AllowAdd(string tableName) => Security?.AllowAdd(tableName) ?? true;

		/// <summary>
		/// Check date
		/// </summary>
		/// <param name="value">Value to validate</param>
		/// <param name="format">Date format: std/stdshort/us/usshort/euro/euroshort</param>
		/// <param name="separator">Date separator</param>
		/// <returns>Whether the value is valid</returns>

		public static bool CheckDate(string value, string format = "", string separator = "")
		{
			if (Empty(value))
				return true;
			if (Empty(format)) {
				if (Regex.IsMatch(DateFormat, "^yyyy"))
					format = "std";
				else if (Regex.IsMatch(DateFormat, "^yy"))
					format = "stdshort";
				else if (Regex.IsMatch(DateFormat, "^m") && Regex.IsMatch(DateFormat, "yyyy$"))
					format = "us";
				else if (Regex.IsMatch(DateFormat, "^m") && Regex.IsMatch(DateFormat, "yy$"))
					format = "usshort";
				else if (Regex.IsMatch(DateFormat, "^d") && Regex.IsMatch(DateFormat, "yyyy$"))
					format = "euro";
				else if (Regex.IsMatch(DateFormat, "^d") && Regex.IsMatch(DateFormat, "yy$"))
					format = "euroshort";
				else
					return false;
			}
			if (Empty(separator))
				separator = DateSeparator;
			value = Regex.Replace(value, @" +", " ").Trim();
			var dt = value.Split(' ');
			if (dt.Length > 0) {
				string pattern = "", year = "", month = "", day = "";
				Match m = Regex.Match(dt[0], @"^([0-9]{4})/([0][1-9]|[1][0-2])/([0][1-9]|[1|2][0-9]|[3][0|1])$");
				if (m.Success) { // yyyy/mm/dd
					year = m.Groups[1].Value;
					month = m.Groups[2].Value;
					day = m.Groups[3].Value;
				} else {
					string sep = Regex.Escape(separator);
					switch (format) {
						case "std":
							pattern = $"^([0-9]{{4}}){sep}([0]?[1-9]|[1][0-2]){sep}([0]?[1-9]|[1|2][0-9]|[3][0|1])$";
							break;
						case "stdshort":
							pattern = $"^([0-9]{{2}}){sep}([0]?[1-9]|[1][0-2]){sep}([0]?[1-9]|[1|2][0-9]|[3][0|1])$";
							break;
						case "us":
							pattern = $"^([0]?[1-9]|[1][0-2]){sep}([0]?[1-9]|[1|2][0-9]|[3][0|1]){sep}([0-9]{{4}})$";
							break;
						case "usshort":
							pattern = $"^([0]?[1-9]|[1][0-2]){sep}([0]?[1-9]|[1|2][0-9]|[3][0|1]){sep}([0-9]{{2}})$";
							break;
						case "euro":
							pattern = $"^([0]?[1-9]|[1|2][0-9]|[3][0|1]){sep}([0]?[1-9]|[1][0-2]){sep}([0-9]{{4}})$";
							break;
						case "euroshort":
							pattern = $"^([0]?[1-9]|[1|2][0-9]|[3][0|1]){sep}([0]?[1-9]|[1][0-2]){sep}([0-9]{{2}})$";
							break;
					}
					if (!Regex.IsMatch(dt[0], pattern))
						return false;
					var d = dt[0].Split(Convert.ToChar(separator));
					switch (format) {
						case "std":
						case "stdshort":
							year = UnformatYear(d[0]);
							month = d[1];
							day = d[2];
							break;
						case "us":
						case "usshort":
							year = UnformatYear(d[2]);
							month = d[0];
							day = d[1];
							break;
						case "euro":
						case "euroshort":
							year = UnformatYear(d[2]);
							month = d[1];
							day = d[0];
							break;
					}
				}
				if (!CheckDay(ConvertToInt(year), ConvertToInt(month), ConvertToInt(day)))
					return false;
			}
			if (dt.Length > 1 && !CheckTime(dt[1]))
				return false;
			return true;
		}

		// Check Date format (yyyy/mm/dd)
		public static bool CheckStdDate(string value) => CheckDate(value, "std", DateSeparator);

		// Check Date format (yy/mm/dd)
		public static bool CheckStdShortDate(string value) => CheckDate(value, "stdshort", DateSeparator);

		// Check US Date format (mm/dd/yyyy)
		public static bool CheckUSDate(string value) => CheckDate(value, "us", DateSeparator);

		// Check US Date format (mm/dd/yy)
		public static bool CheckShortUSDate(string value) => CheckDate(value, "usshort", DateSeparator);

		// Check Euro Date format (dd/mm/yyyy)
		public static bool CheckEuroDate(string value) => CheckDate(value, "euro", DateSeparator);

		// Check Euro Date format (dd/mm/yy)
		public static bool CheckShortEuroDate(string value) => CheckDate(value, "euroshort", DateSeparator);

		// Unformat 2 digit year to 4 digit year
		public static string UnformatYear(object year) {
			string yr = Convert.ToString(year);
			if (IsNumeric(yr) && yr.Length == 2) {
				if (ConvertToInt(yr) > Config.UnformatYear)
					return "19" + yr;
				else
					return "20" + yr;
			} else {
				return yr;
			}
		}

		// Check day
		public static bool CheckDay(int checkYear, int checkMonth, int checkDay)
		{
			int maxDay = 31;
			if (checkMonth == 4 || checkMonth == 6 || checkMonth == 9 || checkMonth == 11) {
				maxDay = 30;
			} else if (checkMonth == 2) {
				if (checkYear % 4 > 0) {
					maxDay = 28;
				} else if (checkYear % 100 == 0 && checkYear % 400 > 0) {
					maxDay = 28;
				} else {
					maxDay = 29;
				}
			}
			return (checkDay >= 1 && checkDay <= maxDay);
		}

		// Check integer
		public static bool CheckInteger(string value)
		{
			if (Empty(value)) return true;
			if (value.Contains(DecimalPoint))
				return false;
			return CheckNumber(value);
		}

		// Check number
		public static bool CheckNumber(string value)
		{
			if (Empty(value)) return true;
			string pat = @"^[+-]?(\d{1,3}(" + ((ThousandsSeparator != "") ? Regex.Escape(ThousandsSeparator) + "?" : "") + @"\d{3})*(" +
				Regex.Escape(DecimalPoint) + @"\d+)?|" + Regex.Escape(DecimalPoint) + @"\d+)$"; // Note: Do not use !Empty(ThousandsSeparator).
			return Regex.IsMatch(value, pat);
		}

		// Check range
		public static bool CheckRange(string value, object min, object max)
		{
			if (Empty(value)) return true;
			if (IsNumeric(min) || IsNumeric(max)) { // Number
				if (CheckNumber(value)) {
					double val = Convert.ToDouble(ConvertToFloatString(value));
					if (min != null && val < Convert.ToDouble(min) || max != null && val > Convert.ToDouble(max))
						return false;
				}
			} else if (min != null && String.Compare(value, Convert.ToString(min)) < 0 || max != null && String.Compare(value, Convert.ToString(max)) > 0) // String
				return false;
			return true;
		}

		// Check time
		public static bool CheckTime(string value)
		{
			if (Empty(value)) return true;
			var s = Regex.Escape(TimeSeparator);
			var am = Regex.Escape(Language.Phrase("AM"));
			var pm = Regex.Escape(Language.Phrase("PM"));
			return Regex.IsMatch(value, $"^(0[0-9]|1[0-9]|2[0-3]){s}[0-5][0-9]((({am}|{pm}))|({s}[0-5][0-9](\\.\\d+)?)?)");
		}

		// Check US phone number
		public static bool CheckPhone(string value)
		{
			if (Empty(value)) return true;
			return Regex.IsMatch(value, "^\\(\\d{3}\\) ?\\d{3}( |-)?\\d{4}|^\\d{3}( |-)?\\d{3}( |-)?\\d{4}");
		}

		// Check US zip code
		public static bool CheckZip(string value)
		{
			if (Empty(value)) return true;
			return Regex.IsMatch(value, "^\\d{5}|^\\d{5}-\\d{4}");
		}

		// Check credit card
		public static bool CheckCreditCard(string value)
		{
			if (Empty(value)) return true;
			var cards = new Dictionary<string, string> {
				{"visa", "^4\\d{3}[ -]?\\d{4}[ -]?\\d{4}[ -]?\\d{4}"},
				{"mastercard", "^5[1-5]\\d{2}[ -]?\\d{4}[ -]?\\d{4}[ -]?\\d{4}"},
				{"discover", "^6011[ -]?\\d{4}[ -]?\\d{4}[ -]?\\d{4}"},
				{"amex", "^3[4,7]\\d{13}"},
				{"diners", "^3[0,6,8]\\d{12}"},
				{"bankcard", "^5610[ -]?\\d{4}[ -]?\\d{4}[ -]?\\d{4}"},
				{"jcb", "^[3088|3096|3112|3158|3337|3528]\\d{12}"},
				{"enroute", "^[2014|2149]\\d{11}"},
				{"switch", "^[4903|4911|4936|5641|6333|6759|6334|6767]\\d{12}"}
			};
			return cards.Any(kvp => Regex.IsMatch(value, kvp.Value) && CheckSum(value));
		}

		// Check sum
		public static bool CheckSum(string value)
		{
			int checksum;
			byte digit;
			value = value.Replace("-", "").Replace(" ", "");
			checksum = 0;
			for (int i = 2 - (value.Length % 2); i <= value.Length; i += 2)
				checksum = checksum + Convert.ToByte(value[i - 1]);
			for (int i = (value.Length % 2) + 1; i <= value.Length; i += 2) {
				digit = Convert.ToByte(Convert.ToByte(value[i - 1]) * 2);
				checksum = checksum + ((digit < 10) ? digit : (digit - 9));
			}
			return (checksum % 10 == 0);
		}

		// Check US social security number
		public static bool CheckSsc(string value)
		{
			if (Empty(value)) return true;
			return Regex.IsMatch(value, "^(?!000)([0-6]\\d{2}|7([0-6]\\d|7[012]))([ -]?)(?!00)\\d\\d\\3(?!0000)\\d{4}");
		}

		// Check email
		public static bool CheckEmail(string value)
		{
			if (Empty(value)) return true;
			return Regex.IsMatch(value.Trim(), @"^[\w.%+-]+@[\w.-]+\.[A-Z]{2,18}$", RegexOptions.IgnoreCase);
		}

		// Check emails
		public static bool CheckEmail(string value, int count)
		{
			if (Empty(value)) return true;
			string[] emails = value.Replace(",", ";").Split(';');
			if (emails.Length > count && count > 0)
				return false;
			return emails.All(email => CheckEmail(email));
		}

		// Check GUID
		public static bool CheckGuid(string value)
		{
			if (Empty(value)) return true;
			string pattern = "([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}";
			return Regex.IsMatch(value, "^{" + pattern + "}") || Regex.IsMatch(value, "^" + pattern);
		}

		// Check file extension
		public static bool CheckFileType(string value, string exts = null)
		{
			exts = exts ?? Config.UploadAllowedFileExtensions;
			if (Empty(value) || Empty(exts))
				return true;
			var extension = Path.GetExtension(value).Substring(1);
			return exts.Split(',').Any(ext => SameText(ext, extension));
		}

		// Check empty string
		public static bool EmptyString(object value)
		{
			var str = Convert.ToString(value);
			if (Regex.IsMatch(str, "&[^;]+;")) // Contains HTML entities
				str = HtmlDecode(str);
			return Empty(str);
		}

		// Check by regular expression
		public static bool CheckByRegEx(string value, string pattern)
		{
			if (Empty(value)) return true;
			return Regex.IsMatch(value, pattern);
		}

		// Check by regular expression
		public static bool CheckByRegEx(string value, string pattern, RegexOptions options)
		{
			if (Empty(value)) return true;
			return Regex.IsMatch(value, pattern, options);
		}

		// Get current date in standard format (yyyy/mm/dd)
		public static string StdCurrentDate() => DateTime.Today.ToString("yyyy'/'MM'/'dd");

		// Get date in standard format (yyyy/mm/dd)
		public static string StdDate(DateTime dt) => dt.ToString("yyyy'/'MM'/'dd");

		// Get current date and time in standard format (yyyy/mm/dd hh:mm:ss)
		public static string StdCurrentDateTime() => DateTime.Now.ToString("yyyy'/'MM'/'dd' 'HH':'mm':'ss");

		// Get date/time in standard format (yyyy/mm/dd hh:mm:ss)
		public static string StdDateTime(DateTime dt) => dt.ToString("yyyy'/'MM'/'dd' 'HH':'mm':'ss");

		// Get current date and time in standard format (yyyy-mm-dd hh:mm:ss)
		public static string DbCurrentDateTime() => DateTime.Now.ToString("yyyy'-'MM'-'dd' 'HH':'mm':'ss");

		// Encrypt password
		public static string EncryptPassword(string input) => Md5(input);

		// Compare password
		public static bool ComparePassword(string pwd, string input) {
			if (Config.CaseSensitivePassword) {
				return Config.EncryptedPassword ? Verify(input, pwd) : SameString(pwd, input);
			} else {
				return Config.EncryptedPassword ? Verify(input.ToLower(), pwd) : SameText(pwd, input);
			}

			// Local function to verify password
			bool Verify(string value, string password) => EncryptPassword(value) == password;
		}

		// MD5
		public static string Md5(string input)
		{
			var hasher = new MD5CryptoServiceProvider();
			byte[] data = hasher.ComputeHash(Encoding.Unicode.GetBytes(input));
			var builder = new StringBuilder();
			for (int i = 0; i < data.Length; i++)
				builder.Append(data[i].ToString("x2"));
			return builder.ToString();
		}

		// CRC32
		public static uint Crc32(string input) {
			byte[] bytes = Encoding.Unicode.GetBytes(input);
			uint crc = 0xffffffff;
			uint poly = 0xedb88320;
			uint[] table = new uint[256];
			uint temp = 0;
			for (uint i = 0; i < table.Length; ++i) {
				temp = i;
				for (int j = 8; j > 0; --j) {
					if ((temp & 1) == 1) {
						temp = (uint)((temp >> 1) ^ poly);
					} else {
						temp >>= 1;
					}
				}
				table[i] = temp;
			}
			for (int i = 0; i < bytes.Length; ++i) {
				byte index = (byte)(((crc) & 0xff) ^ bytes[i]);
				crc = (uint)((crc >> 8) ^ table[index]);
			}
			return ~crc;
		}

		/// <summary>
		/// Check if object is array
		/// </summary>
		/// <param name="obj">Object to check</param>
		/// <returns>Whether object is array</returns>

		public static bool IsArray(object obj) => (obj != null) && (obj is Array);

		/// <summary>
		/// Check if value is numeric
		/// </summary>
		/// <param name="expression">Value to check</param>
		/// <returns>Whether value is numeric</returns>

		public static bool IsNumeric(object expression)
		{
			if (expression == null)
				return false;
			string s = Convert.ToString(expression);
			if (String.IsNullOrWhiteSpace(s))
				return false;
			NumberStyles style = NumberStyles.Any;
			return TryParse((NumberFormatInfo)CultureInfo.CurrentCulture.NumberFormat) || // Current locale
				TryParse((NumberFormatInfo)CultureInfo.GetCultureInfo("en-US").NumberFormat) || // English locale
				TryParse((NumberFormatInfo)CurrentNumberFormatInfo.Clone()); // Current language locale

			// Local function
			bool TryParse(NumberFormatInfo info) => System.Double.TryParse(s, style, info, out _) || System.Int64.TryParse(s, style, info, out _) ||
				System.UInt64.TryParse(s, style, info, out _) || System.Decimal.TryParse(s, style, info, out _);
		}

		/// <summary>
		/// Check if value is date
		/// </summary>
		/// <param name="expression">Value to check</param>
		/// <returns>Whether value is date</returns>

		public static bool IsDate(object expression) => DateTime.TryParse(Convert.ToString(expression), out _);

		// Convert date/time format ID
		public static int ConvertDateTimeFormatId(int namedFormat)
		{
			switch (namedFormat) {
				case 0:
				case 8:
					return DateFormatId;
				case 1:
					switch (DateFormatId) {
						case 5:
							return 9;
						case 6:
							return 10;
						case 7:
							return 11;
						case 12:
							return 15;
						case 13:
							return 16;
						case 14:
							return 17;
						default:
							return DateFormatId;
					}
				case 2:
					switch (DateFormatId) {
						case 9:
							return 5;
						case 10:
							return 6;
						case 11:
							return 7;
						case 15:
							return 12;
						case 16:
							return 13;
						case 17:
							return 14;
						default:
							return DateFormatId;
					}
				default:
					return namedFormat;
			}
		}

		/// <summary>
		/// Format a timestamp, datetime, date or time field
		/// </summary>
		/// <param name="date">Timestamp, DateTime, Date or Time field value</param>
		/// <param name="namedFormat">
		/// Named format:
		/// 0 - Default date format
		/// 1 - Long Date (with time)
		/// 2 - Short Date (without time)
		/// 3 - Long Time (hh:mm:ss AM/PM)
		/// 4 - Short Time (hh:mm:ss)
		/// 5 - Short Date (yyyy/mm/dd)
		/// 6 - Short Date (mm/dd/yyyy)
		/// 7 - Short Date (dd/mm/yyyy)
		/// 8 - Short Date (Default) + Short Time (if not 00:00:00)
		/// 9/109 - Short Date (yyyy/mm/dd) + Short Time (hh:mm[:ss])
		/// 10/110 - Short Date (mm/dd/yyyy) + Short Time (hh:mm[:ss])
		/// 11/111 - Short Date (dd/mm/yyyy) + Short Time (hh:mm[:ss])
		/// 12 - Short Date - 2 digit year (yy/mm/dd)
		/// 13 - Short Date - 2 digit year (mm/dd/yy)
		/// 14 - Short Date - 2 digit year (dd/mm/yy)
		/// 15/115 - Short Date (yy/mm/dd) + Short Time (hh:mm[:ss])
		/// 16/116 - Short Date (mm/dd/yyyy) + Short Time (hh:mm[:ss])
		/// 17/117 - Short Date (dd/mm/yyyy) + Short Time (hh:mm[:ss])
		/// </param>
		/// <returns>Formatted date time</returns>

		public static string FormatDateTime(object date, int namedFormat)
		{
			string result;
			bool useShortTime = false;
			if (Empty(date))
				return String.Empty;
			if (IsDate(date)) {
				DateTime dt = ConvertToDateTime(date);
				if (namedFormat == 8) {
					if (dt.Hour != 0 || dt.Minute != 0 || dt.Second != 0)
						namedFormat = 1;
					else
						namedFormat = 2;
				}
				namedFormat = ConvertDateTimeFormatId(namedFormat);
				if (namedFormat > 100) {
					useShortTime = true;
					namedFormat -= 100;
				} else {
					useShortTime = Config.DateTimeWithoutSeconds;
				}
				if (namedFormat == 3) {
					if (dt.Hour == 0) {
						if (dt.Minute == 0 && dt.Second == 0) {
							result = "12 " + Language.Phrase("Midnight");
						} else {
							result = "12" + TimeSeparator + ZeroPad(dt.Minute, 2) + (useShortTime ? "" : TimeSeparator + ZeroPad(dt.Second, 2)) + " " + Language.Phrase("AM");
						}
					} else if (dt.Hour > 0 && dt.Hour < 12) {
						result = ZeroPad(dt.Hour, 2) + TimeSeparator + ZeroPad(dt.Minute, 2) + (useShortTime ? "" : TimeSeparator + ZeroPad(dt.Second, 2)) + " " + Language.Phrase("AM");
					} else if (dt.Hour == 12) {
						if (dt.Minute == 0 && dt.Second == 0) {
							result = "12 " + Language.Phrase("Noon");
						} else {
							result = ZeroPad(dt.Hour, 2) + TimeSeparator + ZeroPad(dt.Minute, 2) + (useShortTime ? "" : TimeSeparator + ZeroPad(dt.Second, 2)) + " " + Language.Phrase("PM");
						}
					} else if (dt.Hour > 12 && dt.Hour <= 23) {
						result = ZeroPad(dt.Hour-12, 2) + TimeSeparator + ZeroPad(dt.Minute, 2) + (useShortTime ? "" : TimeSeparator + ZeroPad(dt.Second, 2)) + " " + Language.Phrase("PM");
					} else {
						result = ZeroPad(dt.Hour, 2) + TimeSeparator + ZeroPad(dt.Minute, 2) + (useShortTime ? "" : TimeSeparator + ZeroPad(dt.Second, 2));
					}
				} else if (namedFormat == 4) {
					if (useShortTime)
						result = dt.ToString("HH'" + TimeSeparator + "'mm");
					else
						result = dt.ToString("HH'" + TimeSeparator + "'mm'" + TimeSeparator + "'ss");
				} else if (namedFormat == 5 || namedFormat == 9) {
					result = dt.ToString("yyyy'" + DateSeparator + "'MM'" + DateSeparator + "'dd");
				} else if (namedFormat == 6 || namedFormat == 10) {
					result = dt.ToString("MM'" + DateSeparator + "'dd'" + DateSeparator + "'yyyy");
				} else if (namedFormat == 7 || namedFormat == 11) {
					result = dt.ToString("dd'" + DateSeparator + "'MM'" + DateSeparator + "'yyyy");
				} else if (namedFormat == 8) {
					result = dt.ToString(DateFormat);
					if (dt.Hour != 0 || dt.Minute != 0 || dt.Second != 0) {
						if (useShortTime)
							result += " " + dt.ToString("HH'" + TimeSeparator + "'mm");
						else
							result += " " + dt.ToString("HH'" + TimeSeparator + "'mm'" + TimeSeparator + "'ss");
					}
				} else if (namedFormat == 12 || namedFormat == 15) {
					result = dt.ToString("yy'" + DateSeparator + "'MM'" + DateSeparator + "'dd");
				} else if (namedFormat == 13 || namedFormat == 16) {
					result = dt.ToString("MM'" + DateSeparator + "'dd'" + DateSeparator + "'yy");
				} else if (namedFormat == 14 || namedFormat == 17) {
					result = dt.ToString("dd'" + DateSeparator + "'MM'" + DateSeparator + "'yy");
				} else {
					return dt.ToString();
				}
				if ((namedFormat >= 9 && namedFormat <= 11) || (namedFormat >= 15 && namedFormat <= 17)) {
					if (useShortTime)
						result += " " + dt.ToString("HH'" + TimeSeparator + "'mm");
					else
						result += " " + dt.ToString("HH'" + TimeSeparator + "'mm'" + TimeSeparator + "'ss");
				}
				return result;
			} else {
				return Convert.ToString(date);
			}
		}

		// Setup NumberFormatInfo // DN
		public static void SetupNumberFormatInfo(NumberFormatInfo info = null)
		{
			info = info ?? CurrentNumberFormatInfo;

			// CurrencyPositivePattern // DN
			// 0 $n
			// 1 n$
			// 2 $ n
			// 3 n $
			// Note: Config.PositiveSign and PositiveSignPosition are not supported

			if (CurrencySymbolSpacePositive == 0 && CurrencySymbolPrecedesPositive == 1) {
				info.CurrencyPositivePattern = 0; // 0 $n
			} else if (CurrencySymbolSpacePositive == 0 && CurrencySymbolPrecedesPositive == 0) {
				info.CurrencyPositivePattern = 1; // 1 n$
			} else if (CurrencySymbolSpacePositive == 1 && CurrencySymbolPrecedesPositive == 1) {
				info.CurrencyPositivePattern = 2; // 2 $ n
			} else if (CurrencySymbolSpacePositive == 1 && CurrencySymbolPrecedesPositive == 0) {
				info.CurrencyPositivePattern = 3; // 3 n $
			}

			// CurrencyNegativePattern // DN
			// 0 ($n)
			// 1 -$n
			// 2 $-n
			// 3 $n-
			// 4 (n$)
			// 5 -n$
			// 6 n-$
			// 7 n$-
			// 8 -n $
			// 9 -$ n
			// 10 n $-
			// 11 $ n-
			// 12 $ -n
			// 13 n- $
			// 14 ($ n)
			// 15 (n $)

			if (NegativeSignPosition == 0 && CurrencySymbolSpaceNegative == 0 && CurrencySymbolPrecedesNegative == 1)
				info.CurrencyNegativePattern = 0; // 0 ($n)
			else if (NegativeSignPosition == 0 && CurrencySymbolSpaceNegative == 0 && CurrencySymbolPrecedesNegative == 0)
				info.CurrencyNegativePattern = 4; // 4 (n$)
			else if (NegativeSignPosition == 0 && CurrencySymbolSpaceNegative == 1 && CurrencySymbolPrecedesNegative == 1)
				info.CurrencyNegativePattern = 14; // 14 ($ n)
			else if (NegativeSignPosition == 0 && CurrencySymbolSpaceNegative == 1 && CurrencySymbolPrecedesNegative == 0)
				info.CurrencyNegativePattern = 15; // 15 (n $)
			else if (NegativeSignPosition == 1 && CurrencySymbolSpaceNegative == 0 && CurrencySymbolPrecedesNegative == 1)
				info.CurrencyNegativePattern = 1; // 1 -$n
			else if (NegativeSignPosition == 1 && CurrencySymbolSpaceNegative == 0 && CurrencySymbolPrecedesNegative == 0)
				info.CurrencyNegativePattern = 5; // 5 -n$
			else if (NegativeSignPosition == 1 && CurrencySymbolSpaceNegative == 1 && CurrencySymbolPrecedesNegative == 1)
				info.CurrencyNegativePattern = 9; // 9 -$ n
			else if (NegativeSignPosition == 1 && CurrencySymbolSpaceNegative == 1 && CurrencySymbolPrecedesNegative == 0)
				info.CurrencyNegativePattern = 8; // 8 -n $
			else if (NegativeSignPosition == 2 && CurrencySymbolSpaceNegative == 0 && CurrencySymbolPrecedesNegative == 1)
				info.CurrencyNegativePattern = 3; // 3 $n-
			else if (NegativeSignPosition == 2 && CurrencySymbolSpaceNegative == 0 && CurrencySymbolPrecedesNegative == 0)
				info.CurrencyNegativePattern = 7; // 7 n$-
			else if (NegativeSignPosition == 2 && CurrencySymbolSpaceNegative == 1 && CurrencySymbolPrecedesNegative == 1)
				info.CurrencyNegativePattern = 11; // 11 $ n-
			else if (NegativeSignPosition == 2 && CurrencySymbolSpaceNegative == 1 && CurrencySymbolPrecedesNegative == 0)
				info.CurrencyNegativePattern = 10; // 10 n $-
			else if (NegativeSignPosition == 3 && CurrencySymbolSpaceNegative == 0 && CurrencySymbolPrecedesNegative == 1)
				info.CurrencyNegativePattern = 1; // 1 -$n
			else if (NegativeSignPosition == 3 && CurrencySymbolSpaceNegative == 0 && CurrencySymbolPrecedesNegative == 0)
				info.CurrencyNegativePattern = 6; // 6 n-$
			else if (NegativeSignPosition == 3 && CurrencySymbolSpaceNegative == 1 && CurrencySymbolPrecedesNegative == 1)
				info.CurrencyNegativePattern = 9; // 9 -$ n
			else if (NegativeSignPosition == 3 && CurrencySymbolSpaceNegative == 1 && CurrencySymbolPrecedesNegative == 0)
				info.CurrencyNegativePattern = 13; // 13 n- $
			else if (NegativeSignPosition == 4 && CurrencySymbolSpaceNegative == 0 && CurrencySymbolPrecedesNegative == 1)
				info.CurrencyNegativePattern = 7; // 7 n$-
			else if (NegativeSignPosition == 4 && CurrencySymbolSpaceNegative == 0 && CurrencySymbolPrecedesNegative == 0)
				info.CurrencyNegativePattern = 2; // 2 $-n
			else if (NegativeSignPosition == 4 && CurrencySymbolSpaceNegative == 1 && CurrencySymbolPrecedesNegative == 1)
				info.CurrencyNegativePattern = 12; // 12 $ -n
			else if (NegativeSignPosition == 4 && CurrencySymbolSpaceNegative == 1 && CurrencySymbolPrecedesNegative == 0)
				info.CurrencyNegativePattern = 10; // 10 n $-

			// NumberNegativePattern // DN
			// 0 (n)
			// 1 -n
			// 2 - n
			// 3 n-
			// 4 n -

			if (NegativeSignPosition == 0)
				info.NumberNegativePattern = 0; // 0 (n)
			else if ((NegativeSignPosition == 1 || NegativeSignPosition == 3) && CurrencySymbolSpaceNegative == 0)
				info.NumberNegativePattern = 1; // 1 -n
			else if ((NegativeSignPosition == 2 || NegativeSignPosition == 3) && CurrencySymbolSpaceNegative == 1)
				info.NumberNegativePattern = 2; // 2 - n
			else if ((NegativeSignPosition == 2 || NegativeSignPosition == 4) && CurrencySymbolSpaceNegative == 0)
				info.NumberNegativePattern = 3; // 3 n-
			else if ((NegativeSignPosition == 2 || NegativeSignPosition == 4) && CurrencySymbolSpaceNegative == 1)
				info.NumberNegativePattern = 4; // 4 n -

			// PercentPositivePattern // DN
			// 0 n %
			// 1 n%
			// 2 %n
			// 3 % n

			if (CurrencySymbolSpacePositive == 0)
				info.PercentPositivePattern = 1; // 1 n%
			else
				info.PercentPositivePattern = 0; // 0 n %

			// PercentNegativePattern //DN
			// 0 -n %
			// 1 -n%
			// 2 -%n
			// 3 %-n
			// 4 %n-
			// 5 n-%
			// 6 n%-
			// 7 -% n
			// 8 n %-
			// 9 % n-
			// 10 % -n
			// 11 n- %

			if (NegativeSignPosition == 0 && CurrencySymbolSpaceNegative == 0)
				info.PercentNegativePattern = 2; // 2 -n%
			else if (NegativeSignPosition == 0 && CurrencySymbolSpaceNegative == 1)
				info.PercentNegativePattern = 0; // 0 -n %
			else if (NegativeSignPosition == 1 && CurrencySymbolSpaceNegative == 0)
				info.PercentNegativePattern = 1; // 1 -n%
			else if (NegativeSignPosition == 1 && CurrencySymbolSpaceNegative == 1)
				info.PercentNegativePattern = 0; // 0 -n %
			else if (NegativeSignPosition == 2 && CurrencySymbolSpaceNegative == 0)
				info.PercentNegativePattern = 6; // 6 n%-
			else if (NegativeSignPosition == 2 && CurrencySymbolSpaceNegative == 1)
				info.PercentNegativePattern = 8; // 8 n %-
			else if (NegativeSignPosition == 3 && CurrencySymbolSpaceNegative == 0)
				info.PercentNegativePattern = 5; // 2 n-%
			else if (NegativeSignPosition == 3 && CurrencySymbolSpaceNegative == 1)
				info.PercentNegativePattern = 11; // 11 n- %
			else if (NegativeSignPosition == 4 && CurrencySymbolSpaceNegative == 0)
				info.PercentNegativePattern = 6; // 6 n%-
			else if (NegativeSignPosition == 4 && CurrencySymbolSpaceNegative == 1)
				info.PercentNegativePattern = 8; // 8 n %-
		}

		/// <summary>
		/// Format number
		/// </summary>
		/// <param name="expression">Amount</param>
		/// <param name="numDigitsAfterDecimal">
		/// Numeric value indicating how many places to the right of the decimal are displayed
		/// 	-1 Use Default
		/// 	-2 Retain all values after decimal place
		/// </param>
		/// <param name="includeLeadingDigit">Includes leading digits: 1 (True), 0 (False), or -2 (Use default)</param>
		/// <param name="useParensForNegativeNumbers">Use parenthesis for negative numbers: 1 (True), 0 (False), or -2 (Use default)</param>
		/// <param name="groupDigits">Use group digits: 1 (True), 0 (False), or -2 (Use default)</param>
		/// <returns>The formatted number in string</returns>

		public static string FormatNumber(object expression, int numDigitsAfterDecimal = -1, int includeLeadingDigit = -2, int useParensForNegativeNumbers = -2, int groupDigits = -2)
		{
			if (Empty(expression))
				return String.Empty;
			var expr = Convert.ToString(expression);
			if (!IsNumeric(expr))
				return expr;
			NumberFormatInfo info = (NumberFormatInfo)CurrentNumberFormatInfo.Clone();
			double value = Convert.ToDouble(expression, CultureInfo.InvariantCulture);
			expr = value.ToString("R", info);

			// Check numDigitsAfterDecimal
			if (numDigitsAfterDecimal == -2) { // Use all values after decimal point
				if (expr.Contains(info.NumberDecimalSeparator)) {
					info.NumberDecimalDigits = expr.Length - expr.LastIndexOf(info.NumberDecimalSeparator) - 1;
				} else {
					info.NumberDecimalDigits = 0;
				}
			} else if (numDigitsAfterDecimal > -1) {
				info.NumberDecimalDigits = numDigitsAfterDecimal;
			}

			// Check groupDigits
			if (groupDigits == 0)
				info.NumberGroupSeparator = "";

			// Check useParensForNegativeNumbers
			if (useParensForNegativeNumbers == -1)
				info.NumberNegativePattern = 0;
			else if (useParensForNegativeNumbers == 0 && info.NumberNegativePattern == 0)
				info.NumberNegativePattern = 1; // Assume NumberNegativePattern = 1
			var val = value.ToString("N", info); // Integral and decimal digits, group separators, and a decimal separator with optional negative sign

			// Check includeLeadingDigit
			if (includeLeadingDigit == 0 && Regex.IsMatch(val, @"^[\(" + Regex.Escape(info.NegativeSign) + @"]\s?0" + Regex.Escape(info.NumberDecimalSeparator)))
				val = val.Replace("0" + info.NumberDecimalSeparator, info.NumberDecimalSeparator);
			return val;
		}

		/// <summary>
		/// Format percent
		/// </summary>
		/// <param name="expression">amount</param>
		/// <param name="numDigitsAfterDecimal">
		/// 	Numeric value indicating how many places to the right of the decimal are displayed
		/// 	-1 Use Default
		/// </param>
		/// <param name="includeLeadingDigit">Includes leading digits: 1 (True), 0 (False), or -2 (Use default)</param>
		/// <param name="useParensForNegativeNumbers">Use parenthesis for negative numbers: 1 (True), 0 (False), or -2 (Use default)</param>
		/// <param name="groupDigits">Use group digits: 1 (True), 0 (False), or -2 (Use default)</param>
		/// <returns>Formatted value in percent</returns>

		public static string FormatPercent(object expression, int numDigitsAfterDecimal = -1, int includeLeadingDigit = -2, int useParensForNegativeNumbers = -2, int groupDigits = -2)
		{
			if (Empty(expression))
				return String.Empty;
			var expr = Convert.ToString(expression);
			if (!IsNumeric(expr))
				return expr;
			NumberFormatInfo info = (NumberFormatInfo)CurrentNumberFormatInfo.Clone();
			double value = Convert.ToDouble(expression, CultureInfo.InvariantCulture);
			expr = value.ToString("R", info);

			// Check numDigitsAfterDecimal
			if (numDigitsAfterDecimal == -2) { // Use all values after decimal point
				if (expr.Contains(info.NumberDecimalSeparator)) {
					info.PercentDecimalDigits = Math.Max(expr.Length - expr.LastIndexOf(info.NumberDecimalSeparator) - 1 - 2, 0);
				} else {
					info.PercentDecimalDigits = 0;
				}
			} else if (numDigitsAfterDecimal > -1) {
				info.PercentDecimalDigits = numDigitsAfterDecimal;
			}

			// Check groupDigits
			if (groupDigits == 0)
				info.PercentGroupSeparator = "";
			var val = Convert.ToDouble(expression).ToString("P", info);

			// Check includeLeadingDigit
			if (includeLeadingDigit == 0 && Convert.ToDouble(expression) > -0.01 && Convert.ToDouble(expression) < 0.01)
				val = val.Replace("0" + info.PercentDecimalSeparator, info.PercentDecimalSeparator);
			if (val.Contains(info.NegativeSign) && useParensForNegativeNumbers == -1) {
				val = "(" + val.Replace(info.NegativeSign, "") + ")";
			}
			return val;
		}

		/// <summary>
		/// Format currency
		/// </summary>
		/// <param name="expression">Amount</param>
		/// <param name="numDigitsAfterDecimal">Indicating how many places to the right of the decimal are displayed</param>
		/// <param name="includeLeadingDigit">Includes leading digits: 1 (True), 0 (False), or -2 (Use default)</param>
		/// <param name="useParensForNegativeNumbers">Use parenthesis for negative numbers: 1 (True), 0 (False), or -2 (Use default)</param>
		/// <param name="groupDigits">Use group digits: 1 (True), 0 (False), or -2 (Use default)</param>
		/// <returns>Formatted string</returns>

		public static string FormatCurrency(object expression, int numDigitsAfterDecimal = -1, int includeLeadingDigit = -2, int useParensForNegativeNumbers = -2, int groupDigits = -2)
		{
			if (Empty(expression))
				return String.Empty;
			var expr = Convert.ToString(expression);
			if (!IsNumeric(expr))
				return expr;
			NumberFormatInfo info = (NumberFormatInfo)CurrentNumberFormatInfo.Clone();
			double value = Convert.ToDouble(expression, CultureInfo.InvariantCulture);
			expr = value.ToString("R", info);

			// Check numDigitsAfterDecimal
			if (numDigitsAfterDecimal == -2) { // Use all values after decimal point
				if (expr.Contains(info.NumberDecimalSeparator)) {
					info.CurrencyDecimalDigits = expr.Length - expr.LastIndexOf(info.NumberDecimalSeparator) - 1;
				} else {
					info.CurrencyDecimalDigits = 0;
				}
			} else if (numDigitsAfterDecimal > -1) {
				info.CurrencyDecimalDigits = numDigitsAfterDecimal;
			}

			// Check groupDigits
			if (groupDigits == 0)
				info.CurrencyGroupSeparator = "";

			// Check useParensForNegativeNumbers
			if (useParensForNegativeNumbers == -1) {
				if (CurrencySymbolSpaceNegative == 0 && CurrencySymbolPrecedesNegative == 1)
					info.CurrencyNegativePattern = 0; // ($n)
				else if (CurrencySymbolSpaceNegative == 0 && CurrencySymbolPrecedesNegative == 0)
					info.CurrencyNegativePattern = 4; // (n$)
				else if (CurrencySymbolSpaceNegative == 1 && CurrencySymbolPrecedesNegative == 1)
					info.CurrencyNegativePattern = 14; // ($ n)
				else if (CurrencySymbolSpaceNegative == 1 && CurrencySymbolPrecedesNegative == 0)
					info.CurrencyNegativePattern = 15; // (n $)
			} else if (useParensForNegativeNumbers == 0) {
				if (NegativeSignPosition == 0) {
					if (CurrencySymbolSpaceNegative == 0 && CurrencySymbolPrecedesNegative == 1)
						info.CurrencyNegativePattern = 1; // 1 -$n
					else if (CurrencySymbolSpaceNegative == 0 && CurrencySymbolPrecedesNegative == 0)
						info.CurrencyNegativePattern = 6; // 6 n-$
					else if (CurrencySymbolSpaceNegative == 1 && CurrencySymbolPrecedesNegative == 1)
						info.CurrencyNegativePattern = 9; // 9 -$ n
					else if (CurrencySymbolSpaceNegative == 1 && CurrencySymbolPrecedesNegative == 0)
						info.CurrencyNegativePattern = 13; // 13 n- $
				}
			}
			var val = Convert.ToDouble(expression).ToString("C", info);

			// Check includeLeadingDigit
			if (includeLeadingDigit == 0 && Convert.ToDouble(expression) > -1 && Convert.ToDouble(expression) < 1)
				val = val.Replace("0" + info.CurrencyDecimalSeparator, info.CurrencyDecimalSeparator);
			return val;
		}

		/// <summary>
		/// Check if object is IList and IEnumerable
		/// </summary>
		/// <param name="obj">Object to check</param>
		/// <returns>Whether the object is IList and IEnumerable</returns>

		public static bool IsList(object obj) => (obj != null) && (obj is IList) && (obj is IEnumerable);

		/// <summary>
		/// Check if object is IDictionary
		/// </summary>
		/// <param name="obj">Object to check</param>
		/// <returns>Wether object is dictionary</returns>

		public static bool IsDictionary(object obj) => (obj != null) && (obj is IDictionary);

		// Global random
		private static Random GlobalRandom = new Random();

		/// <summary>
		/// Generate random number
		/// </summary>
		/// <returns>A random number</returns>

		public static int Random()
		{
			lock (GlobalRandom) {
				var newRandom = new Random(GlobalRandom.Next());
				return newRandom.Next();
			}
		}

		/// <summary>
		/// Generate a random code with specified number of digits
		/// </summary>
		/// <param name="n">Number of digits</param>
		/// <returns>A random number</returns>

		public static string Random(int n)
		{
			lock (GlobalRandom) {
				var newRandom = new Random(GlobalRandom.Next());
				string s = "";
				for (int i = 0; i < n; i++)
					s = String.Concat(s, newRandom.Next(10).ToString());
				return s;
			}
		}

		/// <summary>
		/// Get query value
		/// </summary>
		/// <param name="name">Name of the value</param>
		/// <returns>Value (empty string if name does not exist)</returns>

		public static string Get(string name) => Query[name].ToString(); // StringValues ToString() will return String.Empty if name does not exist

		/// <summary>
		/// Get query value as type T
		/// </summary>
		/// <param name="name">Name of the value</param>
		/// <typeparam name="T">Data type of the value</typeparam>
		/// <returns>Value (Returns null if name does not exist)</returns>

		public static T Get<T>(string name) => Query.TryGetValue(name, out StringValues sv) ? ChangeType<T>(sv.ToString()) : default(T);

		/// <summary>
		/// Get form value
		/// </summary>
		/// <param name="name">Name of the value</param>
		/// <returns>Value (empty string if name does not exist)</returns>

		public static string Post(string name) => Form[name].ToString(); // StringValues ToString() will return String.Empty if name does not exist

		/// <summary>
		/// Get form value as type T
		/// </summary>
		/// <param name="name">Name of the value</param>
		/// <typeparam name="T">Data type of the value</typeparam>
		/// <returns>Value (Returns null if name does not exist)</returns>

		public static T Post<T>(string name) => Form.TryGetValue(name, out StringValues sv) ? ChangeType<T>(sv.ToString()) : default(T);

		/// <summary>
		/// Change object to type T
		/// </summary>
		/// <param name="value">Object to change</param>
		/// <typeparam name="T">Data type</typeparam>
		/// <returns>Value of data type T</returns>

		public static T ChangeType<T>(object value) {
			if (typeof(T) == typeof(int)) {
				return (T)Convert.ChangeType(ConvertToInt(value), typeof(T));
			} else if (typeof(T) == typeof(bool)) {
				return (T)Convert.ChangeType(ConvertToBool(value), typeof(T));
			} else {
				return (T)Convert.ChangeType(value, typeof(T));
			}
		}

		/// <summary>
		/// Cookie
		/// </summary>

		public static HttpCookies Cookie;

		/// <summary>
		/// Cookie class
		/// </summary>

		public class HttpCookies
		{
			public string this[string name] {
				get => Request.Cookies[Config.ProjectName + "_" + name];
				set => Response.Cookies.Append(Config.ProjectName + "_" + name, value, new CookieOptions {
						Path = AppPath(),
						Expires = Config.CookieExpiryTime
					});
			}
		}

		/// <summary>
		/// Session
		/// </summary>

		public static HttpSession Session;

		/// <summary>
		/// Session class
		/// </summary>

		public class HttpSession
		{
			private JsonSerializerSettings _settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };

			// Keys
			public IEnumerable<string> Keys => _session?.Keys;

			// Session ID
			public string SessionId => _session?.Id;

			// Get session
			private ISession _session => UseSession ? HttpContext.Session : null; // No session for external use of API

			// Remove
			public void Remove(string key) => _session?.Remove(key);

			// Clear
			public void Clear() => _session?.Clear();

			// Get value as bytes
			public byte[] GetBytes(string key) => _session?.Get(key);

			// Set value as bytes
			public void SetBytes(string key, byte[] value) => _session?.Set(key, value);

			// Get value as string
			public string GetString(string key) => _session?.GetString(key) ?? "";

			// Set value as bytes
			public void SetString(string key, string value) => _session?.SetString(key, value);

			// Try get value as string
			public bool TryGetValue(string name, out string value) {
				value = _session?.GetString(name);
				return value != null;
			}

			// Try get value as object
			public bool TryGetValue(string name, out object value) {
				value = GetValue(name);
				return value != null;
			}

			// Get value as int32
			public int GetInt(string key) => _session?.GetInt32(key) ?? 0;

			// Set value as int32
			public void SetInt(string key, int value) => _session?.SetInt32(key, value);

			// Serialize and set
			public void SetValue(string key, object value) => SetValue(key, value, _settings);

			// Serialize and set
			public void SetValue(string key, object value, JsonSerializerSettings settings) {
				if (value == null)
					Remove(key);
				else
					SetString(key, JsonConvert.SerializeObject(value, settings));
			}

			// Get as deserialized object (TypeNameHandling.All)
			public object GetValue(string key) => GetValue(key, _settings);

			// Get as deserialized object
			public object GetValue(string key, JsonSerializerSettings settings) {
				try {
					var data = _session?.GetString(key);
					if (data != null)
						return JsonConvert.DeserializeObject(data, settings);
					return null;
				} catch {
					return null;
				}
			}

			// Get as deserialized type T (TypeNameHandling.All)
			public T GetValue<T>(string key) => GetValue<T>(key, _settings);

			// Get as deserialized type T
			public T GetValue<T>(string key, JsonSerializerSettings settings) {
				try {
					var data = _session?.GetString(key);
					if (data != null)
						return JsonConvert.DeserializeObject<T>(data, settings);
					return default(T);
				} catch {
					return default(T);
				}
			}

			// Get value as bool
			public bool GetBool(string key) => ConvertToBool(GetValue(key));

			// Set value as bool
			public void SetBool(string key, bool value) => SetValue(key, value);

			// Get/Set as object (string)
			public object this[string name] {
				get => _session?.GetString(name);
				set => _session?.SetString(name, Convert.ToString(value));
			}
		}

		/// <summary>
		/// Write literal in View
		/// </summary>
		/// <param name="value">Value to write</param>

		public static void Write(object value) => View?.WriteLiteral(value);

		/// <summary>
		/// Write binary data to response (async)
		/// </summary>
		/// <param name="value">Value to write</param>
		/// <returns>Task</returns>

		public static async Task BinaryWrite(byte[] value) => await Response.Body.WriteAsync(value, 0, value.Length);

		/// <summary>
		/// Write string to response (async)
		/// </summary>
		/// <param name="str">String to write</param>
		/// <param name="enc">Encoding</param>
		/// <returns>Task</returns>

		public static async Task ResponseWrite(string str, string enc = null) => await Response.WriteAsync(str, Encoding.GetEncoding(enc ?? Config.Charset));

		/// <summary>
		/// Clear response body only (not headers)
		/// </summary>

		public static void ResponseClear() {
			if (Response.Body.CanSeek)
				Response.Body.SetLength(0);
		}

		/// <summary>
		/// Export object info as JSON string
		/// </summary>
		/// <param name="list">Objects to export</param>
		/// <returns>JSON string</returns>

		public static string VarExport(params object[] list) {
			string str = "";
			foreach (object value in list) {
				try {
					str += "<pre>" + HtmlEncode(JsonConvert.SerializeObject(value, Newtonsoft.Json.Formatting.Indented,
						new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })) + "</pre>";
				} catch (Exception e) {
					str += "<pre>" + HtmlEncode(e.Message) + "</pre>";
					continue;
				}
			}
			return str;
		}

		/// <summary>
		/// Write object info for debugging
		/// </summary>
		/// <param name="list">Objects to export</param>

		public static void VarDump(params object[] list) {
			var str = VarExport(list);
			if (View != null) {
				Write(str);
			} else {
				var encoding = Encoding.GetEncoding(Config.Charset);
				byte[] data = encoding.GetBytes(str);
				Response.Body.Write(data, 0, data.Length);
			}
		}

		/// <summary>
		/// End page and throw an exception
		/// </summary>
		/// <param name="value">Error message</param>

		public static void End(object value = null) {
			var result = "";
			if (value is string val) // String => Message
				result = val;
			else if (value != null) // Object
				result = VarExport(value);
			throw new Exception(result);
		}

		/// <summary>
		/// Write HTTP header for no-cache
		/// </summary>

		public static void NoCache() {
			AddHeader(HeaderNames.Expires, "-1");
			AddHeader(HeaderNames.LastModified, DateTime.Now.ToString("R")); // Always modified
			AddHeader(HeaderNames.CacheControl, "no-store, no-cache, must-revalidate");
			AddHeader(HeaderNames.Pragma, "no-cache");
		}

		/// <summary>
		/// Write HTTP header
		/// </summary>
		/// <param name="cache">Allow cache or not</param>

		public static void Header(bool cache) {
			string export = Get("export");
			if (cache || IsHttps() && !Empty(export) && export != "print") { // Allow cache
				AddHeader(HeaderNames.CacheControl, "private, must-revalidate");
				AddHeader(HeaderNames.Pragma, "public");
			} else { // No cache
				NoCache();
			}
			AddHeader("X-UA-Compatible", "IE=edge");
			string contentType = "", charset = Config.Charset;
			if (IsApi()) {
				contentType = "application/json; charset=utf-8";
			} else {
				contentType = "text/html";
				if (!Empty(charset))
					contentType += "; charset=" + charset;
			}
			Response.ContentType = contentType;
		}

		/// <summary>
		/// Get content file extension
		/// </summary>
		/// <param name="data">data to check</param>
		/// <returns>File extension</returns>

		public static string ContentExtension(byte[] data) {
			var ct = ContentType(data);
			if (ct != null)
				return ContentTypeProvider.Mappings.FirstOrDefault(kvp => kvp.Value == ct).Key;
			return "";
		}

		/// <summary>
		/// Add header
		/// </summary>
		/// <param name="name">Header name</param>
		/// <param name="value">Header value</param>

		public static void AddHeader(string name, string value) {
			if (!Empty(name)) // If value is empty, header will be removed
				Response.Headers[name] = value;
		}

		/// <summary>
		/// Remove header
		/// </summary>
		/// <param name="name">Header name to be removed</param>

		public static void RemoveHeader(string name) {
			if (!Empty(name) && Response.Headers.ContainsKey(name))
				Response.Headers.Remove(name);
		}

		/// <summary>
		/// Mobile Detect class
		/// Based on https://github.com/serbanghita/Mobile-Detect
		/// </summary>

		public class MobileDetect
		{
			public JObject Data;
			private string _userAgent;
			private Dictionary<string, StringValues> httpHeaders;
			private IEnumerable<JToken> _rules;

			// Constructor
			public MobileDetect(string userAgent = null)
			{
				LoadJsonData().GetAwaiter().GetResult();
				httpHeaders = Request.Headers.ToDictionary(kvp => "HTTP_" + kvp.Key.Replace("-", "_").ToUpper(), kvp => kvp.Value); // Convert header to HTTP_*
				_userAgent = userAgent ?? ParseHeadersForUserAgent();
			}

			// Check if the device is mobile
			public bool IsMobile => CheckHttpHeadersForMobile() || MatchDetectionRulesAgainstUa();

			// To access the user agent, retrieved from http headers if not explicitly set
			public string UserAgent
			{
				get => _userAgent;
				set => _userAgent = value;
			}

			// Check if the device is a tablet
			public bool IsTablet => MatchDetectionRulesAgainstUa(Data["uaMatch"]["tablets"]);

			// Checks if the device is conforming to the provided key
			// e.g .Is("ios") / .Is("androidos") / .Is("iphone")

			public bool Is(string key)
			{
				var rules = Rules.Where(rule => SameText(((JProperty)rule).Name, key));
				if (rules.Count() > 0)
					return Match((string)rules.First());
				return false;
			}

			// Load JSON data
			private async Task LoadJsonData() => Data = (JObject)JsonConvert.DeserializeObject(await FileReadAllText(ServerMapPath("js/Mobile_Detect.json")));

			// UA HTTP headers
			private List<string> UaHttpHeaders => Data["uaHttpHeaders"].Select(h => (string)h).ToList();

			// Parse the headers for the user agent - uses a list of possible keys as provided by upstream
			// returns a concatenated list of possible user agents, should be just 1

			private string ParseHeadersForUserAgent() =>
				String.Join(" ", httpHeaders.Where(kvp => UaHttpHeaders.Contains(kvp.Key)).Select(kvp => kvp.Value)).Trim();

			// Rules
			private IEnumerable<JToken> Rules
			{
				get
				{
					if (_rules == null) {
						var phones = Data["uaMatch"]["phones"];
						var tablets = Data["uaMatch"]["tablets"];
						var browsers = Data["uaMatch"]["browsers"];
						var os = Data["uaMatch"]["os"];
						_rules = phones.Concat(tablets).Concat(browsers).Concat(os);
					}
					return _rules;
				}
			}

			// Check the HTTP headers for signs of mobile
			private bool CheckHttpHeadersForMobile()
			{
				var headerMatch = Data["headerMatch"];
				foreach (JProperty token in headerMatch) {
					var mobileHeader = token.Name;
					var matchType = token.Value;
					if (httpHeaders.TryGetValue(mobileHeader, out StringValues sv)) {
						if (matchType == null || !(matchType["matches"] is JArray))
							return false;
						return matchType["matches"].Select(m => (string)m).Any(match => sv.ToString().Contains(match));
					}
				}
				return false;
			}

			// Check custom regexes against the User-Agent string
			private bool Match(JToken keyRegex, string uaString = "")
			{
				if (Empty(uaString))
					uaString = UserAgent;
				string regex = (string)keyRegex;
				regex = regex.Replace("/", "\\/"); // Escape the special character which is the delimiter
				return Regex.IsMatch(uaString, regex, RegexOptions.IgnoreCase);
			}

			// Find a detection rule that matches the current User-agent
			private bool MatchDetectionRulesAgainstUa(IEnumerable<JToken> rules = null) =>
				(rules ?? Rules).Any(regex => Match(regex));
		}

		/// <summary>
		/// Mobile detect
		/// </summary>

		private static MobileDetect _mobileDetect;

		/// <summary>
		/// Check if mobile device
		/// </summary>
		/// <returns>Whether it is mobile device</returns>

		public static bool IsMobile() => (_mobileDetect = _mobileDetect ?? new MobileDetect()).IsMobile;

		/// <summary>
		/// Convert to JSON
		/// </summary>
		/// <param name="value">Value to convert</param>
		/// <param name="settings">Json serializer settings</param>
		/// <returns>JSON string</returns>

		public static string ConvertToJson(object value, JsonSerializerSettings settings = null) {
			if (value == null || value.GetType().IsPrimitive || value is string || value is DateTime || value is decimal) {
				return JsonConvert.ToString(value);
			} else if (settings == null) {
				return JsonConvert.SerializeObject(value);
			} else {
				return JsonConvert.SerializeObject(value, settings);
			}
		}

		/// <summary>
		/// Convert a value to JSON value
		/// </summary>
		/// <param name="val">Value</param>
		/// <param name="type">Data type</param>
		/// <param name="attr">attributes</param>
		/// <returns>JSON string</returns>

		public static string ConvertToJson(object val, string type, bool attr = false)
		{
			if (IsDBNull(val) || val == null) {
				return "null";
			} else if (SameText(type, "number")) {
				return Convert.ToString(val);
			} else if (SameText(type, "boolean") || val is bool) {
				return ConvertToBool(val) ? "true" : "false";
			} else if (SameText(type, "string") || val is string) {
				if (Convert.ToString(val).Contains("\0")) // Contains null byte
					return "\"binary\"";
				if (Convert.ToString(val).Length > Config.DataStringMaxLength)
					val = Convert.ToString(val).Substring(0, Config.DataStringMaxLength);
				if (attr)
					return "'" + JsEncodeAttribute(val) + "'";
				else
					return "\"" + JsEncode(val) + "\"";
			}
			return ConvertToJson(val);
		}

		/// <summary>
		/// Convert dictionary to JSON for HTML attributes
		/// </summary>
		/// <param name="dict">Dictionary</param>
		/// <returns>JSON string</returns>

		public static string ConvertToJsonAttribute(Dictionary<string, string> dict) => JsonConvert.SerializeObject(dict, Newtonsoft.Json.Formatting.None,
			new JsonSerializerSettings { StringEscapeHandling = StringEscapeHandling.EscapeHtml }).Replace("\"", "'");

		/// <summary>
		/// Create instance by class name
		/// </summary>
		/// <param name="name">Class name</param>
		/// <param name="args">Arguments</param>
		/// <param name="types">Types for the class</param>
		/// <returns></returns>

		public static dynamic CreateInstance(string name, object[] args = null, Type[] types = null) {
			Type t = Type.GetType(Config.ProjectClassName + "+" + name) ?? // This class
				Type.GetType(Config.ProjectClassName + "Base+" + name); // Base class
			if (types != null)
				t = t.MakeGenericType(types);
			return Activator.CreateInstance(t, args);
		}

		/// <summary>
		/// Create table object by table name
		/// </summary>
		/// <param name="name">Name of table</param>
		/// <returns>Table object</returns>

		public static dynamic CreateTable(string name) => CreateInstance(Config.TableClassNames[name]);

		/// <summary>
		/// Get export document object
		/// </summary>
		/// <param name="tbl">Table</param>
		/// <param name="style">Style: "v"(Vertical) or "h"(Horizontal)</param>
		/// <returns>Export class instance</returns>

		public static dynamic CreateExportDocument(DbTable tbl, string style) => CreateInstance(Config.Export[ExportType], new object[] {tbl, style});

		/// <summary>
		/// Base class for export
		/// </summary>

		public class ExportBase {
			public dynamic Table;
			public string Line = "";
			public string Header = "";
			public string Style = "h"; // "v"(Vertical) or "h"(Horizontal)
			public bool Horizontal = true; // Horizontal
			public int RowCount = 0;
			public int FieldCount = 0;
			public bool ExportCustom = false;
			public StringBuilder Text = new StringBuilder();

			// Constructor
			public ExportBase(DbTable tbl = null, string style = "") {
				Table = tbl;
				SetStyle(style);
			}

			// Style
			public virtual void SetStyle(string style) {
				if (SameString(style, "v") || SameString(style, "h"))
					Style = style.ToLower();
				Horizontal = !SameString(Style, "v");
			}

			// Field caption
			public virtual void ExportCaption(DbField fld) {
				if (!fld.Exportable)
					return;
				FieldCount++;
				ExportValue(fld, fld.ExportCaption);
			}

			// Field value
			public virtual void ExportValue(DbField fld) => ExportValue(fld, fld.ExportValue);

			// Field aggregate
			public virtual void ExportAggregate(DbField fld, string type) {
				if (!fld.Exportable)
					return;
				FieldCount++;
				if (Horizontal) {
					var val = "";
					if ((new List<string> {"TOTAL", "COUNT", "AVERAGE"}).Contains(type))
						val = Language.Phrase(type) + ": " + fld.ExportValue;
					ExportValue(fld, val);
				}
			}

			// Get meta tag for charset
			public virtual string CharsetMetaTag() => "<meta http-equiv=\"Content-Type\" content=\"text/html" + ((Config.Charset != "") ? "; charset=" + Config.Charset : "") + "\">\r\n";

			// Table header
			public virtual void ExportTableHeader() => Text.Append("<table class=\"ew-export-table\">");

			// Cell styles
			public virtual string CellStyles(DbField fld, bool useStyle = true) => (useStyle && Config.ExportCssStyles) ? fld.CellStyles : "";

			// Row styles
			public virtual string RowStyles(bool useStyle = true) => (useStyle && Config.ExportCssStyles) ? Table.RowStyles : "";

			// Export a value (caption, field value, or aggregate)
			public virtual void ExportValue(DbField fld, object val, bool useStyle = true) {
				Text.Append("<td" + CellStyles(fld, useStyle) + ">");
				Text.Append(Convert.ToString(val));
				Text.Append("</td>");
			}

			// Begin a row
			public virtual void BeginExportRow(int rowCnt = 0, bool useStyle = true) {
				RowCount++;
				FieldCount = 0;
				if (Horizontal) {
					if (rowCnt == -1) {
						Table.CssClass = "ew-export-table-footer";
					} else if (rowCnt == 0) {
						Table.CssClass = "ew-export-table-header";
					} else {
						Table.CssClass = (rowCnt % 2 == 1) ? "ew-export-table-row" : "ew-export-table-alt-row";
					}
					Text.Append("<tr" + RowStyles(useStyle) + ">");
				}
			}

			// End a row
			public virtual void EndExportRow(int rowCnt = 0) {
				if (Horizontal)
					Text.Append("</tr>");
			}

			// Empty row
			public virtual void ExportEmptyRow() {
				RowCount++;
				Text.Append("<br>");
			}

			// Page break
			public virtual void ExportPageBreak() {}

			#pragma warning disable 1998

			// Export a field
			public virtual async Task ExportField(DbField fld) {
				if (!fld.Exportable)
					return;
				FieldCount++;
				var wrkExportValue = "";
				if (fld.ExportFieldImage && !Empty(fld.ExportHrefValue) && fld.Upload != null) { // Upload field
					if (!Empty(fld.Upload.DbValue))
						wrkExportValue = GetFileATag(fld, fld.ExportHrefValue);
				} else {
					wrkExportValue = fld.ExportValue;
				}
				if (Horizontal) {
					ExportValue(fld, wrkExportValue);
				} else { // Vertical, export as a row
					RowCount++;
					Text.Append("<tr class=\"" + ((FieldCount % 2 == 1) ? "ew-export-table-row" : "ew-export-table-alt-row") + "\">" +
						"<td>" + fld.ExportCaption + "</td>");
					Text.Append("<td" + CellStyles(fld) + ">" + fld.ExportValue + "</td></tr>");
				}
			}

			#pragma warning restore 1998

			// Table Footer
			public virtual void ExportTableFooter() => Text.Append("</table>");

			#pragma warning disable 1998

			// Add HTML tags
			public virtual async Task ExportHeaderAndFooter() {
				string header = "<html><head>\r\n";
				header += CharsetMetaTag();
				if (Config.ExportCssStyles && !Empty(Config.ProjectStylesheetFilename))
					header += "<style type=\"text/css\">" + await LoadText(Config.ProjectStylesheetFilename) + "</style>\r\n";
				header += "</" + "head>\r\n<body>\r\n";
				Text.Insert(0, header);
				Text.Append("</body></html>");
			}

			#pragma warning restore 1998

			// Output as string
			public override string ToString() => Text.ToString();

			// Export
			public virtual IActionResult Export() {
				if (!Config.Debug)
					Response.Clear();
				AddHeader(HeaderNames.SetCookie, "fileDownload=true; path=/");
				return Controller.Content(ToString(), "text/html");
			}
		}

		// Get file IMG tag (for export to email/pdf only)
		public static string GetFileImgTag(DbField fld, string fn) {
			if (!Empty(fn)) {
				if (fld.UploadMultiple) {
					var wrkfiles = fn.Split(Config.MultipleUploadSeparator).Where(wrkfile => !Empty(wrkfile));
					return String.Join("<br>", wrkfiles.Select(wrkfile => "<img class=\"ew-image\" src=\"" + wrkfile + "\" alt=\"\">"));
				} else {
					return "<img class=\"ew-image\" src=\"" + fn + "\" alt=\"\">";
				}
			}
			return "";
		}

		// Get file anchor tag
		public static string GetFileATag(DbField fld, object fileName) {
			string[] wrkfiles = {};
			string wrkpath = "";
			string fn = Convert.ToString(fileName);
			if (fld.IsBlob) {
				if (!Empty(fld.Upload.DbValue))
					wrkfiles = new string[] {fn};
			} else if (fld.UploadMultiple) {
				wrkfiles = fn.Split(Config.MultipleUploadSeparator);
				var pos = wrkfiles[0].LastIndexOf("/");
				if (pos > -1) {
					wrkpath = wrkfiles[0].Substring(0, pos + 1); // Get path from first file name
					wrkfiles[0] = wrkfiles[0].Substring(pos + 1);
				}
			} else {
				if (!Empty(fld.Upload.DbValue))
					wrkfiles = new string[] {fn};
			}
			var elements = wrkfiles.Where(wrkfile => !Empty(wrkfile))
				.Select(wrkfile => HtmlElement("a", new Attributes() {{"href", FullUrl(wrkpath + wrkfile, "href")}}, fld.Caption));
			return String.Join("<br>", elements);
		}

		// Get file temp image
		public static async Task<string> GetFileTempImage(DbField fld, string val) {
			if (fld.UploadMultiple) {
				var files = val.Split(Config.MultipleUploadSeparator);
				string images = "";
				for (var i = 0; i < files.Length; i++) {
					if (files[i] != "") {
						var tmpimage = await FileReadAllBytes(fld.PhysicalUploadPath + files[i]);
						if (fld.ImageResize)
							ResizeBinary(ref tmpimage, ref fld.ImageWidth, ref fld.ImageHeight);
						if (images != "") images += Config.MultipleUploadSeparator;
						images += TempImage(tmpimage);
					}
				}
				return images;
			} else {
				if (fld.IsBlob) {
					if (!IsDBNull(fld.Upload.DbValue)) { // DN
						var tmpimage = (byte[])fld.Upload.DbValue;
						if (fld.ImageResize)
							ResizeBinary(ref tmpimage, ref fld.ImageWidth, ref fld.ImageHeight);
						return TempImage(tmpimage);
					}
				} else if (val != "") {
					var tmpimage = await FileReadAllBytes(fld.PhysicalUploadPath + val);
					if (fld.ImageResize)
						ResizeBinary(ref tmpimage, ref fld.ImageWidth, ref fld.ImageHeight);
					return TempImage(tmpimage);
				}
			}
			return "";
		}

		// Get file upload URL
		public static string GetFileUploadUrl(DbField fld, string val, bool resize = false) {
			if (!EmptyString(val)) {
				string fileUrl = Config.ApiUrl + Config.ApiFileAction;
				string fn;
				if (fld.DataType == Config.DataTypeBlob) {
					string tableVar = !EmptyString(fld.SourceTableVar) ? fld.SourceTableVar : fld.TableVar;
					fn = AppPath(fileUrl) + "/" + RawUrlEncode(tableVar) + "/" + RawUrlEncode(fld.Param) + "/" + RawUrlEncode(val) +
						"?session=" + Session.SessionId + "&" + Config.TokenName + "=" + CurrentToken;
					if (resize)
						fn += "&resize=1&width=" + fld.ImageWidth + "&height=" + fld.ImageHeight;
				} else {
					bool encrypt = Config.EncryptFilePath;
					string path = (encrypt || resize) ? fld.PhysicalUploadPath : fld.HrefPath;
					string key = Config.RandomKey + Session.SessionId;
					if (encrypt) {
						fn = AppPath(fileUrl) + "/" + Encrypt(path + val, key);
						if (resize)
							fn += "?width=" + fld.ImageWidth + "&height=" + fld.ImageHeight;
					} else if (resize) {
						fn = AppPath(fileUrl) + "/" + Encrypt(path + val, key) + "?width=" + fld.ImageWidth + "&height=" + fld.ImageHeight; // Encrypt the physical path
					} else {
						fn = IsRemote(path) ? path : UrlEncodeFilePath(path);
						fn += UrlEncodeFilePath(val);
					}
					fn += (fn.Contains("?") ? "&" : "?");
					fn += "session=" + Session.SessionId + "&" + Config.TokenName + "=" + CurrentToken;
				}
				return fn;
			}
			return "";
		}

		// URL-encode file name
		public static string UrlEncodeFilename(string fn) {
			string path, filename;
			if (fn.Contains("?")) {
				var arf = fn.Split('?');
				fn = arf[1];
				var ar = fn.Split('&');
				for (var i = 0; i < ar.Length; i++) {
					var p = ar[i];
					if (p.StartsWith("fn=")) {
						ar[i] = "fn=" + UrlEncode(p.Substring(3));
						break;
					}
				}
				return arf[0] + "?" + String.Join("&", ar);
			}
			if (fn.Contains("/")) {
				path = Path.GetDirectoryName(fn).Replace("\\", "/");
				filename = Path.GetFileName(fn);
			} else {
				path = "";
				filename = fn;
			}
			if (path != "")
				path = IncludeTrailingDelimiter(path, false);
			return path + UrlEncode(filename).Replace("+", " "); // Do not encode spaces
		}

		/// <summary>
		/// Check if absolute URL
		/// </summary>
		/// <param name="url">URL to check</param>
		/// <param name="uri">Output Uri</param>
		/// <returns>Uri instance</returns>

		public static bool IsAbsoluteUrl(string url, out Uri uri) => Uri.TryCreate(url, UriKind.Absolute, out uri);

		/// <summary>
		/// Check if absolute URL
		/// </summary>
		/// <param name="url">URL to check</param>
		/// <returns>Uri instance</returns>

		public static bool IsAbsoluteUrl(string url) => Uri.TryCreate(url, UriKind.Absolute, out _);

		// URL Encode file path
		public static string UrlEncodeFilePath(string path) {
			string scheme = IsAbsoluteUrl(path, out Uri uri) ? uri.Scheme : "";
			var ar = path.Split('/');
			for (var i = 0; i < ar.Length; i++) {
				if (ar[i] != scheme + ":")
					ar[i] = RawUrlEncode(ar[i]);
			}
			return String.Join("/", ar);
		}

		// Get file view tag
		public static async Task<string> GetFileViewTag(DbField fld, string val) {
			if (EmptyString(val))
				return "";
			string[] wrkfiles = null, wrknames = null;
			string images = "";
			if (fld.IsBlob) {
				wrknames = new string[] { val };
				wrkfiles = new string[] { val };
			} else if (fld.UploadMultiple) {
				wrknames = val.Split(Config.MultipleUploadSeparator);
				wrkfiles = Convert.ToString(fld.Upload.DbValue).Split(Config.MultipleUploadSeparator);
			} else {
				wrknames = new string[] { val };
				wrkfiles = new string[] { Convert.ToString(fld.Upload.DbValue) };
			}
			bool multiple = (wrkfiles.Length > 1);
			string href = Convert.ToString(fld.HrefValue).Trim();
			var page = CurrentPage;
			int wrkcnt = 0;
			foreach (string wrkfile in wrkfiles) {
				string fn = "", image = "";
				if (page != null && (page.Type == "REPORT" && page.Export == "excel" && Config.UseExcelExtension ||
					page.Type != "REPORT" && (page.CustomExport == "pdf" || page.CustomExport == "email")))
					fn = await GetFileTempImage(fld, wrkfile);
				else
					fn = GetFileUploadUrl(fld, wrkfile, fld.ImageResize);
				if (fld.ViewTag == "IMAGE" && (fld.IsBlobImage || IsImageFile(wrkfile))) { // Image
					if (Empty(href) && !fld.UseColorbox) {
						if (!Empty(fn)) {
							if (IsLazy())
								image = "<img class=\"ew-image ew-lazy img-thumbnail\" src=\"data:image/png;base64,R0lGODlhAQABAAD/ACwAAAAAAQABAAACADs=\" data-src=\"" + AppPath(fn) + "\"" + fld.ViewAttributes + ">"; // DN
							else
								image = "<img class=\"ew-image img-thumbnail\" src=\"" + AppPath(fn) + "\"" + fld.ViewAttributes + ">"; // DN
						}
					} else {
						if (fld.UploadMultiple && href.Contains("%u"))
							fld.HrefValue = AppPath(href.Replace("%u", GetFileUploadUrl(fld, wrkfile)));
						if (!Empty(fn)) {
							if (IsLazy())
								image = "<a" + fld.LinkAttributes + "><img class=\"ew-image ew-lazy img-thumbnail\" src=\"data:image/png;base64,R0lGODlhAQABAAD/ACwAAAAAAQABAAACADs=\" data-src=\"" + AppPath(fn) + "\"" + fld.ViewAttributes + "></a>"; // DN
							else
								image = "<a" + fld.LinkAttributes + "><img class=\"ew-image img-thumbnail\" src=\"" + AppPath(fn) + "\"" + fld.ViewAttributes + "></a>"; // DN
						}
					}
				} else {
					string url = "", name = "";
					if (fld.IsBlob) {
						url = href;
						name = !Empty(fld.Upload.FileName) ? fld.Upload.FileName : fld.Caption;
					} else {
						url = GetFileUploadUrl(fld, wrkfile);
						name = (wrkcnt < wrknames.Length) ? wrknames[wrkcnt] : wrknames[wrknames.Length - 1];
					}
					if (!Empty(url)) {
						string className = fld.LinkAttrs.ContainsKey("class") ? fld.LinkAttrs["class"] : ""; // Backup class
						fld.LinkAttrs["class"] = RemoveClass(className, "ew-lightbox"); // Remove colorbox class
						if (fld.UploadMultiple && href.Contains("%u"))
							fld.HrefValue = AppPath(href.Replace("%u", url));
						image = "<a" + fld.LinkAttributes + "\">" + name + "</a>"; // DN
						if (!Empty(className)) // Restore class
							fld.LinkAttrs["class"] = className;
					}
				}
				if (!Empty(image)) {
					if (multiple)
						images += "<li class=\"list-inline-item\">" + image + "</li>";
					else
						images += image;
				}
				wrkcnt++;
			}
			if (multiple && !Empty(images))
				images = "<ul class=\"list-inline\">" + images + "</ul>";
			return images;
		}

		// Get image view tag
		public static string GetImageViewTag(DbField fld, string val) {
			if (!EmptyString(val)) {
				string href = Convert.ToString(fld.HrefValue);
				string image = val;
				string fn = "";
				if (val != "" && !val.Contains("://") && !val.Contains("\\") && !val.Contains("javascript:"))
					fn = GetImageUrl(fld, val, fld.ImageResize);
				else
					fn = val;
				if (IsImageFile(val)) { // Image
					if (href == "" && !fld.UseColorbox) {
						if (fn != "") {
							if (IsLazy())
								image = "<img class=\"ew-image ew-lazy img-thumbnail\" alt=\"\" src=\"data:image/png;base64,R0lGODlhAQABAAD/ACwAAAAAAQABAAACADs=\" data-src=\"" + AppPath(fn) + "\"" + fld.ViewAttributes + ">";
							else
								image = "<img class=\"ew-image img-thumbnail\" alt=\"\" src=\"" + AppPath(fn) + "\"" + fld.ViewAttributes + ">";
						}
					} else {
						if (fn != "") {
							if (IsLazy())
								image = "<a" + fld.LinkAttributes + "><img class=\"ew-image ew-lazy img-thumbnail\" alt=\"\" src=\"data:image/png;base64,R0lGODlhAQABAAD/ACwAAAAAAQABAAACADs=\" data-src=\"" + AppPath(fn) + "\"" + fld.ViewAttributes + "></a>";
							else
								image = "<a" + fld.LinkAttributes + "><img class=\"ew-image img-thumbnail\" alt=\"\" src=\"" + AppPath(fn) + "\"" + fld.ViewAttributes + "></a>";
						}
					}
				} else {
					string name = val;
					if (href != "")
						image = "<a" + fld.LinkAttributes + ">" + name + "</a>";
					else
						image = name;
				}
				return image;
			}
			return "";
		}

		// Get image URL
		public static string GetImageUrl(DbField fld, string val, bool resize) {
			bool encrypt = Config.EncryptFilePath;
			if (!EmptyString(val)) {
				string fn;
				string key = Config.RandomKey + Session.SessionId;
				string fileUrl = Config.ApiUrl + Config.ApiFileAction;
				string path = (encrypt || resize) ? fld.PhysicalUploadPath : fld.HrefPath;
				if (encrypt) {
					fn = AppPath(fileUrl) + "/" + Encrypt(path + val, key) + "?session=" + Session.SessionId + "&token=" + CurrentToken;
					if (resize)
						fn += "&width=" + fld.ImageWidth + "&height=" + fld.ImageHeight;
				} else if (resize) {
					fn = AppPath(fileUrl) + "/" + Encrypt(path + val, key) + "?session=" + Session.SessionId + "&token=" + CurrentToken +
						"&width=" + fld.ImageWidth + "&height=" + fld.ImageHeight;
				} else {
					fn = IsRemote(path) ? path : UrlEncodeFilePath(path);
					fn += UrlEncodeFilePath(val);
				}
				return fn;
			}
			return "";
		}

		// Check if image file
		public static bool IsImageFile(string fn) {
			if (!Empty(fn)) {
				fn = ImageNameFromUrl(fn);
				var ext = Path.GetExtension(fn).Replace(".", "");
				return Config.ImageAllowedFileExtensions.Contains(ext, StringComparer.OrdinalIgnoreCase);
			} else {
				return false;
			}
		}

		// Check if lazy loading images
		public static bool IsLazy() => Config.LazyLoad && (ExportType == "" || ExportType == "print" && (Empty(CustomExportType) || CustomExportType == "print"));

		// Get image file name from URL
		public static string ImageNameFromUrl(string fn) {
			if (!Empty(fn) && fn.Contains("?")) { // Thumbnail URL
				string p = fn.Split('?')[1].Split('&').FirstOrDefault(x => x.StartsWith("fn="));
				if (p != null)
					return UrlDecode(p.Substring(3));
			}
			return fn;
		}

		/// <summary>
		/// Class for export to email
		/// </summary>

		public class ExportEmail: ExportBase {

			// Constructor
			public ExportEmail(DbTable tbl = null, string style = ""): base(tbl, style) {}

			// Table header
			public override void ExportTableHeader() => Text.Append("<table style=\"border-collapse: collapse;\">"); // Use inline style for Gmail

			// Table border styles
			private string _cellStyles = "border: 1px solid #dddddd; padding: 5px;";

			// Cell styles
			public override string CellStyles(DbField fld, bool useStyle = true) {
				fld.CellAttrs.Prepend("style", _cellStyles); // Use inline style for Gmail
				return (useStyle && Config.ExportCssStyles) ? fld.CellStyles : "";
			}

			// Export a field
			public override async Task ExportField(DbField fld) {
				if (!fld.Exportable)
					return;
				FieldCount++;
				string value = fld.ExportValue;
				if (fld.ExportFieldImage && fld.ViewTag == "IMAGE") {
					if (fld.ImageResize) {
						value = GetFileImgTag(fld, await fld.GetTempImage());
					} else if (!Empty(fld.ExportHrefValue) && fld.Upload != null) {
						if (!Empty(fld.Upload.DbValue))
							value = GetFileATag(fld, fld.ExportHrefValue);
					}
				} else if (fld.ExportFieldImage && fld.ExportHrefValue is Func<string, Task<string>>) { // Export Custom View Tag // DN
					var func = (Func<string, Task<string>>)fld.ExportHrefValue;
					value = await func("email"); // DN
				}
				if (Horizontal) {
					ExportValue(fld, value);
				} else { // Vertical, export as a row
					RowCount++;
					Text.Append("<tr class=\"" + ((FieldCount % 2 == 1) ? "ew-export-table-row" : "ew-export-table-alt-row") + "\">" +
						"<td style=\"" + _cellStyles + "\">" + fld.ExportCaption + "</td>");
					Text.Append("<td" + CellStyles(fld) + ">" + value + "</td></tr>");
				}
			}

			// Export
			public override IActionResult Export() {
				if (!Config.Debug)
					Response.Clear();
				return Controller.Content(ToString(), "text/html");
			}
		}

		/// <summary>
		/// Class for export to HTML
		/// </summary>

		public class ExportHtml: ExportBase {

			// Constructor
			public ExportHtml(DbTable tbl = null, string style = ""): base(tbl, style) {}

			// Export
			public override IActionResult Export() {
				if (!Config.Debug)
					Response.Clear();
				AddHeader(HeaderNames.SetCookie, "fileDownload=true; path=/");
				AddHeader(HeaderNames.ContentDisposition, "attachment; filename=" + ExportFileName + ".html");
				return Controller.Content(ToString(), "text/html");
			}
		}

		/// <summary>
		/// Class for export to Word
		/// </summary>

		public class ExportWord: ExportBase {

			// Constructor
			public ExportWord(DbTable tbl = null, string style = ""): base(tbl, style) {}

			// Export
			public override IActionResult Export() {
				if (!Config.Debug)
					Response.Clear();
				AddHeader(HeaderNames.SetCookie, "fileDownload=true; path=/");
				AddHeader(HeaderNames.ContentDisposition, "attachment; filename=" + ExportFileName + ".doc");
				string contentType = "application/msword" + (!Empty(Config.Charset) ? ";charset=" + Config.Charset : "");
				string bom = SameText(Config.Charset, "utf-8") ? Encoding.UTF8.GetString(Encoding.UTF8.GetPreamble()) : "";
				return Controller.Content(bom + ToString(), contentType);
			}
		}

		/// <summary>
		/// Class for export to CSV
		/// </summary>

		public class ExportCsv: ExportBase {
			public static string QuoteChar = "\"";
			public static string Separator = ",";

			// Constructor
			public ExportCsv(DbTable tbl = null, string style = ""): base(tbl, style) {}

			// Style
			public void ChangeStyle(string style) => Horizontal = true;

			// Table header
			public override void ExportTableHeader() {}

			// Export a value (caption, field value, or aggregate)
			public override void ExportValue(DbField fld, object val, bool useStyle = true) {
				if (!fld.IsBlob) {
					if (!Empty(Line))
						Line += Separator;
					if (fld.DataType == Config.DataTypeBoolean && CurrentPage.RowType != Config.RowTypeHeader)
						Line += ConvertToBool(val);
					else
						Line += QuoteChar + Convert.ToString(val).Replace(QuoteChar, QuoteChar + QuoteChar) + QuoteChar;
				}
			}

			// Begin a row
			public override void BeginExportRow(int rowCnt = 0, bool useStyle = true) => Line = "";

			// End a row
			public override void EndExportRow(int rowCnt = 0) => Text.AppendLine(Line);

			// Empty line
			public void ExportEmptyLine() {}

			#pragma warning disable 1998

			// Export a field
			public override async Task ExportField(DbField fld) {
				if (!fld.Exportable)
					return;
				if (fld.UploadMultiple)
					ExportValue(fld, fld.Upload.DbValue);
				else
					ExportValue(fld);
			}

			#pragma warning restore 1998

			// Table Footer
			public override void ExportTableFooter() {}

			#pragma warning disable 1998

			// Add HTML tags
			public override async Task ExportHeaderAndFooter() {}

			#pragma warning restore 1998

			// Export
			public override IActionResult Export() {
				if (!Config.Debug)
					Response.Clear();
				AddHeader(HeaderNames.SetCookie, "fileDownload=true; path=/");
				AddHeader(HeaderNames.ContentDisposition, "attachment; filename=" + ExportFileName + ".csv");
				string contentType = "text/csv" + (!Empty(Config.Charset) ? ";charset=" + Config.Charset : "");
				string bom = SameText(Config.Charset, "utf-8") ? Encoding.UTF8.GetString(Encoding.UTF8.GetPreamble()) : "";
				return Controller.Content(bom + ToString(), contentType);
			}
		}

		/// <summary>
		/// Class for export to XML
		/// </summary>

		public class ExportXml: ExportBase {
			public XmlDoc Document = new XmlDoc();

			// Constructor
			public ExportXml(DbTable tbl = null, string style = ""): base(tbl, style) {}

			// Style
			public override void SetStyle(string style) {}

			// Field caption
			public override void ExportCaption(DbField fld) {}

			// Field value
			public override void ExportValue(DbField fld) {}

			// Field aggregate
			public override void ExportAggregate(DbField fld, string type) {}

			// Get meta tag for charset
			public override string CharsetMetaTag() => "";

			// Has parent
			public bool HasParent;

			// Table header
			public override void ExportTableHeader() {
				HasParent = Document.DocumentElement != null;
				if (!HasParent)
					Document.AddRoot(Table.TableVar);
			}

			// Export a value (caption, field value, or aggregate)
			public override void ExportValue(DbField fld, object val, bool useStyle = true) {}

			// Begin a row
			public override void BeginExportRow(int rowCnt = 0, bool useStyle = true) {
				if (rowCnt <= 0)
					return;
				if (HasParent)
					Document.AddRow(Table.TableVar);
				else
					Document.AddRow();
			}

			// End a row
			public override void EndExportRow(int rowCnt = 0) {}

			// Empty row
			public override void ExportEmptyRow() {}

			// Page break
			public override void ExportPageBreak() {}

			#pragma warning disable 1998

			// Export a field
			public override async Task ExportField(DbField fld) {
				if (!fld.Exportable || fld.IsBlob)
					return;
				object value = fld.UploadMultiple ? fld.Upload.DbValue : fld.ExportValue;
				if (IsDBNull(value))
					value = "<Null>";
				Document.AddField(fld.Param, value);
			}

			#pragma warning restore 1998

			// Table Footer
			public override void ExportTableFooter() {}

			#pragma warning disable 1998

			// Add HTML tags
			public override async Task ExportHeaderAndFooter() {}

			#pragma warning restore 1998

			// Export
			public override IActionResult Export() {
				if (!Config.Debug)
					Response.Clear();
				return Controller.Content(Document.OuterXml, "text/xml");
			}
		}

		/// <summary>
		/// Class for export to JSON
		/// </summary>

		public class ExportJson: ExportBase {
			private Dictionary<string, object> Item;
			public List<Dictionary<string, object>> Items = new List<Dictionary<string, object>>();

			// Constructor
			public ExportJson(DbTable tbl = null, string style = ""): base(tbl, style) {}

			// Style
			public override void SetStyle(string style) {}

			// Field caption
			public override void ExportCaption(DbField fld) {}

			// Field value
			public override void ExportValue(DbField fld) {}

			// Field aggregate
			public override void ExportAggregate(DbField fld, string type) {}

			// Get meta tag for charset
			public override string CharsetMetaTag() => "";

			// Has parent
			public bool HasParent;

			// Get parent
			public Dictionary<string, object> Parent => Items.LastOrDefault();

			// Table header
			public override void ExportTableHeader() {
				HasParent = Items.Any();
				Parent?.Add(Table.Name, new List<Dictionary<string, object>>());
			}

			// Export a value (caption, field value, or aggregate)
			public override void ExportValue(DbField fld, object val, bool useStyle = true) {}

			// Begin a row
			public override void BeginExportRow(int rowCnt = 0, bool useStyle = true) {
				if (rowCnt <= 0)
					return;
				Item = new Dictionary<string, object>();
			}

			// End a row
			public override void EndExportRow(int rowCnt = 0) {
				if (rowCnt <= 0)
					return;
				if (HasParent)
					Parent[Table.Name].Add(Item);
				else
					Items.Add(Item);
			}

			// Empty row
			public override void ExportEmptyRow() {}

			// Page break
			public override void ExportPageBreak() {}

			#pragma warning disable 1998

			// Export a field
			public override async Task ExportField(DbField fld) {
				if (!fld.Exportable || fld.IsBlob)
					return;
				Item[fld.Name] = fld.UploadMultiple ? fld.Upload.DbValue : fld.ExportValue;
			}

			#pragma warning restore 1998

			// Table Footer
			public override void ExportTableFooter() {}

			#pragma warning disable 1998

			// Add HTML tags
			public override async Task ExportHeaderAndFooter() {}

			#pragma warning restore 1998

			// Export
			public override IActionResult Export() {
				if (!Config.Debug)
					Response.Clear();
				if (Items.Count == 1) {
					return Controller.Json(Items[0]);
				} else {
					return Controller.Json(Item);
				}
			}
		}

		/// <summary>
		/// Current page
		/// </summary>

		public static dynamic CurrentTable => CurrentPage;

		// NumberDecimalSeparator // DN
		public static string DecimalPoint {
			get => CurrentNumberFormatInfo.NumberDecimalSeparator;
			set {
				CurrentNumberFormatInfo.NumberDecimalSeparator = value;
				CurrentNumberFormatInfo.PercentDecimalSeparator = value;
			}
		}

		// NumberGroupSeparator // DN
		public static string ThousandsSeparator {
			get => CurrentNumberFormatInfo.NumberGroupSeparator;
			set {
				CurrentNumberFormatInfo.NumberGroupSeparator = value;
				CurrentNumberFormatInfo.PercentGroupSeparator = value;
			}
		}

		// CurrencySymbol // DN
		public static string CurrencySymbol {
			get => CurrentNumberFormatInfo.CurrencySymbol;
			set => CurrentNumberFormatInfo.CurrencySymbol = value;
		}

		// CurrencyDecimalSeparator // DN
		public static string MonetaryDecimalPoint {
			get => CurrentNumberFormatInfo.CurrencyDecimalSeparator;
			set => CurrentNumberFormatInfo.CurrencyDecimalSeparator = value;
		}

		// CurrencyDecimalSeparator // DN
		public static string MonetaryThousandsSeparator {
			get => CurrentNumberFormatInfo.CurrencyGroupSeparator;
			set => CurrentNumberFormatInfo.CurrencyGroupSeparator = value;
		}

		// PositiveSign // DN
		public static string PositiveSign {
			get => CurrentNumberFormatInfo.PositiveSign;
			set => CurrentNumberFormatInfo.PositiveSign = value;
		}

		// NegativeSign // DN
		public static string NegativeSign {
			get => CurrentNumberFormatInfo.NegativeSign;
			set => CurrentNumberFormatInfo.NegativeSign = value;
		}

		// NumberDecimalDigits // DN
		public static int FracDigits {
			get => CurrentNumberFormatInfo.NumberDecimalDigits;
			set {
				CurrentNumberFormatInfo.NumberDecimalDigits = value;
				CurrentNumberFormatInfo.CurrencyDecimalDigits = value;
				CurrentNumberFormatInfo.PercentDecimalDigits = value;
			}
		}

		// Static file options
		public static StaticFileOptions FileOptions;

		// File extension content type provider
		public static FileExtensionContentTypeProvider ContentTypeProvider => (FileExtensionContentTypeProvider)FileOptions.ContentTypeProvider;

		/// <summary>
		/// Execute SQL
		/// </summary>
		/// <param name="sql">SQL to execute</param>
		/// <param name="dbid">Database ID, default is the main database</param>
		/// <returns>The number of rows affected</returns>

		public static int Execute(string sql, string dbid = "DB") => _GetConnection(dbid)?.ExecuteNonQuery(sql) ?? -1;

		/// <summary>
		/// Execute SQL (async)
		/// </summary>
		/// <param name="sql">SQL to execute</param>
		/// <param name="dbid">Database ID, default is the main database</param>
		/// <returns>The number of rows affected</returns>

		public static async Task<int> ExecuteAsync(string sql, string dbid = "DB") => await (await _GetConnectionAsync(dbid))?.ExecuteNonQueryAsync(sql) ?? -1;

		/// <summary>
		/// Execute SQL and return first value of first row
		/// </summary>
		/// <param name="sql">SQL to execute</param>
		/// <param name="dbid">Database ID, default is the main database</param>
		/// <returns>The first column of the first row in the result set, or a null reference if the result set is empty.</returns>

		public static object ExecuteScalar(string sql, string dbid = "DB") => _GetConnection(dbid)?.ExecuteScalar(sql);

		/// <summary>
		/// Execute SQL and return first value of first row (async)
		/// </summary>
		/// <param name="sql">SQL to execute</param>
		/// <param name="dbid">Database ID, default is the main database</param>
		/// <returns>The first column of the first row in the result set, or a null reference if the result set is empty.</returns>

		public static async Task<object> ExecuteScalarAsync(string sql, string dbid = "DB") => await (await _GetConnectionAsync(dbid))?.ExecuteScalarAsync(sql);

		/// <summary>
		/// Execute SQL and return first row as dictionary
		/// </summary>
		/// <param name="sql">SQL to execute</param>
		/// <param name="dbid">Database ID, default is the main database</param>
		/// <returns>The row as dictionary</returns>

		public static Dictionary<string, object> ExecuteRow(string sql, string dbid = "DB") => _GetConnection(dbid)?.GetRow(sql);

		/// <summary>
		/// Execute SQL and return first row as dictionary (async)
		/// </summary>
		/// <param name="sql">SQL to execute</param>
		/// <param name="dbid">Database ID, default is the main database</param>
		/// <returns>The row as dictionary</returns>

		public static async Task<Dictionary<string, object>> ExecuteRowAsync(string sql, string dbid = "DB") => await (await _GetConnectionAsync(dbid))?.GetRowAsync(sql);

		/// <summary>
		/// Execute SQL and return rows as list of dictionary
		/// </summary>
		/// <param name="sql">SQL to execute</param>
		/// <param name="dbid">Database ID, default is the main database</param>
		/// <returns>The row as dictionary</returns>

		public static List<Dictionary<string, object>> ExecuteRows(string sql, string dbid = "DB") => _GetConnection(dbid)?.GetRows(sql);

		/// <summary>
		/// Execute SQL and return rows as list of dictionary (async)
		/// </summary>
		/// <param name="sql">SQL to execute</param>
		/// <param name="dbid">Database ID, default is the main database</param>
		/// <returns>The row as dictionary</returns>

		public static async Task<List<Dictionary<string, object>>> ExecuteRowsAsync(string sql, string dbid = "DB") => await (await _GetConnectionAsync(dbid))?.GetRowsAsync(sql);

		/// <summary>
		/// Executes query and returns all rows as JSON
		/// </summary>
		/// <param name="sql">SQL to execute</param>
		/// <param name="options">
		/// Dictionary of options:
		/// "header" (bool) Output JSON header, default: true
		/// "array" (bool) Output as array
		/// "firstonly" (bool) Output first row only
		/// </param>
		/// <param name="dbid">Database ID</param>
		/// <returns>The records as JSON</returns>

		public static string ExecuteJson(string sql, Dictionary<string, object> options = null, string dbid = "DB") => _GetConnection(dbid)?.ExecuteJson(sql, options) ?? "null";

		/// <summary>
		/// Executes the query and returns all rows as JSON (async)
		/// </summary>
		/// <param name="sql">SQL to execute</param>
		/// <param name="options">
		/// Dictionary of options:
		/// "header" (bool) Output JSON header, default: true
		/// "array" (bool) Output as array
		/// "firstonly" (bool) Output first row only
		/// </param>
		/// <param name="dbid">Database ID</param>
		/// <returns>The records as JSON</returns>

		public static async Task<string> ExecuteJsonAsync(string sql, Dictionary<string, object> options = null, string dbid = "DB") => await (await _GetConnectionAsync(dbid))?.ExecuteJsonAsync(sql, options) ?? "null";

		/// <summary>
		/// Get query result in HTML table
		/// </summary>
		/// <param name="sql">SQL to execute</param>
		/// <param name="options">
		/// Dictionary of options:
		/// "fieldcaption" (bool|Dictionary)
		///   true: Use caption and use language object, or
		///   false: Use field names directly, or
		///   Dictionary of fieid caption for looking up field caption by field name
		/// "horizontal" (bool) Whether HTML table is horizontal, default: false
		/// "tablename" (string|List&lt;string&gt;) Table name(s) for the language object
		/// "tableclass" (string) CSS class names of the table, default: "table table-bordered ew-db-table"
		/// </param>
		/// <param name="dbid"> Language object, default: the global Language object</param>
		/// <returns>The records as IHtmlContent</returns>

		public static IHtmlContent ExecuteHtml(string sql, Dictionary<string, object> options = null, string dbid = "DB") => _GetConnection(dbid)?.ExecuteHtml(sql, options);

		/// <summary>
		/// Get query result in HTML table (async)
		/// </summary>
		/// <param name="sql">SQL to execute</param>
		/// <param name="options">
		/// Dictionary of options:
		/// "fieldcaption" (bool|Dictionary)
		///   true: Use caption and use language object, or
		///   false: Use field names directly, or
		///   Dictionary of fieid caption for looking up field caption by field name
		/// "horizontal" (bool) Whether HTML table is horizontal, default: false
		/// "tablename" (string|List&lt;string&gt;) Table name(s) for the language object
		/// "tableclass" (string) CSS class names of the table, default: "table table-bordered ew-db-table"
		/// </param>
		/// <param name="dbid"> Language object, default: the global Language object</param>
		/// <returns>Tasks that returns records as IHtmlContent</returns>

		public static async Task<IHtmlContent> ExecuteHtmlAsync(string sql, Dictionary<string, object> options = null, string dbid = "DB") => await (await _GetConnectionAsync(dbid))?.ExecuteHtmlAsync(sql, options);

		/// <summary>
		/// Export to Excel class
		/// </summary>

		public class ExportExcel: ExportBase {

			// Constructor
			public ExportExcel(DbTable tbl = null, string style = ""): base(tbl, style) {}

			// Export a value (caption, field value, or aggregate)
			public override void ExportValue(DbField fld, object val, bool usestyle = true) {
				if ((fld.DataType == Config.DataTypeString || fld.DataType == Config.DataTypeMemo) && IsNumeric(val))
					val = "=\"" + Convert.ToString(val) + "\"";
				base.ExportValue(fld, val, usestyle);
			}

			// Export
			public override IActionResult Export() {
				if (!Config.Debug)
					Response.Clear();
				AddHeader(HeaderNames.SetCookie, "fileDownload=true; path=/");
				AddHeader(HeaderNames.ContentDisposition, "attachment; filename=" + ExportFileName + ".xls");
				string contentType = "application/vnd.ms-excel" + (!Empty(Config.Charset) ? ";charset=" + Config.Charset : "");
				string bom = SameText(Config.Charset, "utf-8") ? Encoding.UTF8.GetString(Encoding.UTF8.GetPreamble()) : "";
				return Controller.Content(bom + ToString(), contentType);
			}
		}

		/// <summary>
		/// Export to PDF class (to be replaced by extension)
		/// </summary>

		public class ExportPdf: ExportBase {}
	}
}
