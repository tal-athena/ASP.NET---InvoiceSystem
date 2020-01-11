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
		/// _userpriv
		/// </summary>

		public static __userpriv _userpriv {
			get => HttpData.Get<__userpriv>("_userpriv");
			set => HttpData["_userpriv"] = value;
		}

		/// <summary>
		/// Page class (userpriv)
		/// </summary>

		public class __userpriv : __userprivBase
		{

			// Construtor
			public __userpriv(Controller controller = null) : base(controller) {
			}

			// Server events
		}

		/// <summary>
		/// Page base class
		/// </summary>

		public class __userprivBase : _UserLevels, IAspNetMakerPage
		{

			// Page ID
			public string PageID = "userpriv";

			// Project ID
			public string ProjectID = "{8543F230-11C6-4105-B51C-8D87C21BE659}";

			// Page object name
			public string PageObjName = "_userpriv";

			// Page headings
			public string Heading = "";
			public string Subheading = "";
			public string PageHeader = "";
			public string PageFooter = "";

			// Token
			public string Token; // DN
			public bool CheckToken = Config.CheckToken;

			// Action result // DN
			public IActionResult ActionResult;

			// Cache // DN
			public IMemoryCache Cache;

			// Page terminated // DN
			private bool _terminated = false;

			// Page URL
			private string _pageUrl = "";

			// Page action result
			public IActionResult PageResult() {
				if (ActionResult != null)
					return ActionResult;
				SetupMenus();
				return Controller.View();
			}

			// Page heading
			public string PageHeading {
				get {
					if (!Empty(Heading))
						return Heading;
					else
						return "";
				}
			}

			// Page subheading
			public string PageSubheading {
				get {
					if (!Empty(Subheading))
						return Subheading;
					return "";
				}
			}

			// Page name
			public string PageName => CurrentPageName();

			// Page URL
			public string PageUrl {
				get {
					if (_pageUrl == "") {
						_pageUrl = CurrentPageName() + "?";
					}
					return _pageUrl;
				}
			}

			// Private properties
			private string _message = "";
			private string _failureMessage = "";
			private string _successMessage = "";
			private string _warningMessage = "";

			// Message
			public string Message {
				get => Session.TryGetValue(Config.SessionMessage, out string message) ? message : _message;
				set {
					_message = AddMessage(Message, value);
					Session[Config.SessionMessage] = _message;
				}
			}

			// Failure Message
			public string FailureMessage {
				get => Session.TryGetValue(Config.SessionFailureMessage, out string failureMessage) ? failureMessage : _failureMessage;
				set {
					_failureMessage = AddMessage(FailureMessage, value);
					Session[Config.SessionFailureMessage] = _failureMessage;
				}
			}

			// Success Message
			public string SuccessMessage {
				get => Session.TryGetValue(Config.SessionSuccessMessage, out string successMessage) ? successMessage : _successMessage;
				set {
					_successMessage = AddMessage(SuccessMessage, value);
					Session[Config.SessionSuccessMessage] = _successMessage;
				}
			}

			// Warning Message
			public string WarningMessage {
				get => Session.TryGetValue(Config.SessionWarningMessage, out string warningMessage) ? warningMessage : _warningMessage;
				set {
					_warningMessage = AddMessage(WarningMessage, value);
					Session[Config.SessionWarningMessage] = _warningMessage;
				}
			}

			// Clear message
			public void ClearMessage() {
				_message = "";
				Session[Config.SessionMessage] = _message;
			}

			// Clear failure message
			public void ClearFailureMessage() {
				_failureMessage = "";
				Session[Config.SessionFailureMessage] = _failureMessage;
			}

			// Clear success message
			public void ClearSuccessMessage() {
				_successMessage = "";
				Session[Config.SessionSuccessMessage] = _successMessage;
			}

			// Clear warning message
			public void ClearWarningMessage() {
				_warningMessage = "";
				Session[Config.SessionWarningMessage] = _warningMessage;
			}

			// Clear all messages
			public void ClearMessages() {
				ClearMessage();
				ClearFailureMessage();
				ClearSuccessMessage();
				ClearWarningMessage();
			}

			// Get message
			public string GetMessage() { // DN
				bool hidden = false;
				string html = "";

				// Message
				string message = Message;
				Message_Showing(ref message, "");
				if (!Empty(message)) { // Message in Session, display
					if (!hidden)
						message = "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">&times;</span></button>" + message;
					html += "<div class=\"alert alert-info alert-dismissible ew-info\"><i class=\"icon fa fa-info\"></i>" + message + "</div>";
					Session[Config.SessionMessage] = ""; // Clear message in Session
				}

				// Warning message
				string warningMessage = WarningMessage;
				Message_Showing(ref warningMessage, "warning");
				if (!Empty(warningMessage)) { // Message in Session, display
					if (!hidden)
						warningMessage = "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">&times;</span></button>" + warningMessage;
					html += "<div class=\"alert alert-warning alert-dismissible ew-warning\"><i class=\"icon fa fa-warning\"></i>" + warningMessage + "</div>";
					Session[Config.SessionWarningMessage] = ""; // Clear message in Session
				}

				// Success message
				string successMessage = SuccessMessage;
				Message_Showing(ref successMessage, "success");
				if (!Empty(successMessage)) { // Message in Session, display
					if (!hidden)
						successMessage = "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">&times;</span></button>" + successMessage;
					html += "<div class=\"alert alert-success alert-dismissible ew-success\"><i class=\"icon fa fa-check\"></i>" + successMessage + "</div>";
					Session[Config.SessionSuccessMessage] = ""; // Clear message in Session
				}

				// Failure message
				string errorMessage = FailureMessage;
				Message_Showing(ref errorMessage, "failure");
				if (!Empty(errorMessage)) { // Message in Session, display
					if (!hidden)
						errorMessage = "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">&times;</span></button>" + errorMessage;
					html += "<div class=\"alert alert-danger alert-dismissible ew-error\"><i class=\"icon fa fa-ban\"></i>" + errorMessage + "</div>";
					Session[Config.SessionFailureMessage] = ""; // Clear message in Session
				}
				return "<div class=\"ew-message-dialog\"" + (hidden ? " d-none" : "") + ">" + html + "</div>"; // DN
			}

			// Show message as IHtmlContent // DN
			public IHtmlContent ShowMessages() => new HtmlString(GetMessage());

			// Get messages
			public Dictionary<string, string> GetMessages() {
				var d = new Dictionary<string, string>();

				// Message
				string message = Message;
				if (!Empty(message)) { // Message in Session, display
					d.Add("message", message);
					Session[Config.SessionMessage] = ""; // Clear message in Session
				}

				// Warning message
				string warningMessage = WarningMessage;
				if (!Empty(warningMessage)) { // Message in Session, display
					d.Add("warningMessage", warningMessage);
					Session[Config.SessionWarningMessage] = ""; // Clear message in Session
				}

				// Success message
				string successMessage = SuccessMessage;
				if (!Empty(successMessage)) { // Message in Session, display
					d.Add("successMessage", successMessage);
					Session[Config.SessionSuccessMessage] = ""; // Clear message in Session
				}

				// Failure message
				string failureMessage = FailureMessage;
				if (!Empty(failureMessage)) { // Message in Session, display
					d.Add("failureMessage", failureMessage);
					Session[Config.SessionFailureMessage] = ""; // Clear message in Session
				}
				return d;
			}

			// Show Page Header
			public IHtmlContent ShowPageHeader() {
				string header = PageHeader;
				Page_DataRendering(ref header);
				if (!Empty(header)) // Header exists, display
					return new HtmlString("<p id=\"ew-page-header\">" + header + "</p>");
				return null;
			}

			// Show Page Footer
			public IHtmlContent ShowPageFooter() {
				string footer = PageFooter;
				Page_DataRendered(ref footer);
				if (!Empty(footer)) // Footer exists, display
					return new HtmlString("<p id=\"ew-page-footer\">" + footer + "</p>");
				return null;
			}

			// Validate page request
			public bool IsPageRequest => true;

			// Valid post
			protected async Task<bool> ValidPost() => !CheckToken || !IsPost() || IsApi() || await Antiforgery.IsRequestValidAsync(HttpContext);

			// Create token
			public void CreateToken() {
				Token = Token ?? Antiforgery.GetTokens(HttpContext).RequestToken; // Always create token, required by API file/lookup request
				CurrentToken = Token; // Save to global variable
			}

			// Constructor
			public __userprivBase(Controller controller = null) { // DN
				if (controller != null)
					Controller = controller;

				// Initialize
				CurrentPage = this;

				// Language object
				Language = Language ?? new Lang();

				// Table object (UserLevels)
				if (UserLevels == null || UserLevels is _UserLevels)
					UserLevels = this;
				UserLevels = UserLevels ?? this;

				// Start time
				StartTime = Environment.TickCount;

				// Debug message
				LoadDebugMessage();

				// Open connection
				Conn = Connection; // DN

				// User table object (s_employee)
				UserTable = UserTable ?? new _s_employee();
				UserTableConn = UserTableConn ?? GetConnection(UserTable.DbId);
			}

			#pragma warning disable 1998

			// Export view result
			public async Task<IActionResult> ExportView() { // DN
				return null;
			}

			#pragma warning restore 1998

			/// <summary>
			/// Terminate page
			/// </summary>
			/// <param name="url">URL to rediect to</param>
			/// <returns>Page result</returns>

			public IActionResult Terminate(string url = "") { // DN
				if (_terminated) // DN
					return null;

				// Page Unload event
				Page_Unload();

				// Global Page Unloaded event
				Page_Unloaded();
				if (!IsApi())
					Page_Redirecting(ref url);

				// Close connection
				CloseConnections(true);

				// Gargage collection
				Collect(); // DN

				// Terminate
				_terminated = true; // DN

				// Return for API
				if (IsApi()) {
					bool res = !Empty(url);
					if (!res) { // Show error
						var showError = new Dictionary<string, string> { { "success", "false" }, { "version", Config.ProductVersion } };
						foreach (var (key, value) in GetMessages())
							showError.Add(key, value);
						return Controller.Json(showError);
					}
				} else if (ActionResult != null) { // Check action result
					return ActionResult;
				}

				// Go to URL if specified
				if (!Empty(url)) {
					if (!Config.Debug)
						ResponseClear();
					if (!Response.HasStarted) {
						SaveDebugMessage();
						return Controller.LocalRedirect(AppPath(url));
					}
				}
				return null;
			}
			public string Disabled;
			public Dictionary<string, object> Privileges = new Dictionary<string, object>();
			public int TableNameCount;
			public Lang ReportLanguage;
			public List<string[]> UserLevelList = new List<string[]>();
			public List<string[]> UserLevelPrivList = new List<string[]>();
			public List<string[]> TableList = new List<string[]>();

			/// <summary>
			/// Page run
			/// </summary>
			/// <returns>Page result</returns>

			public async Task<IActionResult> Run() {

				// Header
				Header(Config.Cache);

				// User profile
				Profile = new UserProfile();

				// Security
				Security = new AdvancedSecurity(); // DN
				bool validRequest = false;

				// Check security for API request
				if (IsApi() && !Security.IsLoggedIn) {
					var authResult = await HttpContext.AuthenticateAsync(JwtBearerDefaults.AuthenticationScheme);
					if (authResult.Succeeded && authResult.Principal.Identity.IsAuthenticated)
						Security.LoginUser(ClaimValue(ClaimTypes.Name), ClaimValue("userid"), ClaimValue("parentuserid"), ConvertToInt(ClaimValue("userlevelid")));
				}
				if (!validRequest) {
					if (!Security.IsLoggedIn)
						await Security.AutoLogin();
					if (Security.IsLoggedIn)
						Security.TablePermission_Loading();
					Security.LoadCurrentUserLevel(CurrentProjectID + "UserLevels");
					if (Security.IsLoggedIn)
						Security.TablePermission_Loaded();
					if (!Security.CanAdmin) {
						Security.SaveLastUrl();
						FailureMessage = DeniedMessage(); // Set no permission
						if (IsApi())
							return new JsonBoolResult(new { success = false, error = DeniedMessage(), version = Config.ProductVersion }, false);
						if (Security.CanList)
							return Terminate(GetUrl("UserLevelslist"));
						else
							return Terminate(GetUrl("login"));
					}
				}
				CurrentAction = Param("action"); // Set up current action

				// Global Page Loading event
				Page_Loading();

				// Page Load event
				Page_Load();

				// Check token
				if (!await ValidPost())
					End(Language.Phrase("InvalidPostRequest"));

				// Create token
				CreateToken();
				CurrentBreadcrumb = new Breadcrumb();
				CurrentBreadcrumb.Add("list", "UserLevels", AppPath("UserLevelslist"), "", "UserLevels");
				var url = CurrentUrl(); // DN
				CurrentBreadcrumb.Add("userpriv", "UserLevelPermission", url);
				Heading = Language.Phrase("UserLevelPermission");

				// Try to load Report Maker language file
				// Note: The langauge IDs must be the same in both projects

				var ar = new List<string[]>();
				Security.LoadUserLevelFromConfigFile(UserLevelList, UserLevelPrivList, ar, true);
				if (!Empty(Config.RelatedLanguageFolder))
					ReportLanguage = CreateInstance("ReportLang");

				// Set up allowed table list
				foreach (var t in ar) {
					int tempPriv = Security.GetUserLevelPriv(t[4] + t[0], Security.CurrentUserLevelID);
					if ((tempPriv & Config.AllowAdmin) == Config.AllowAdmin) { // Allow Admin
						var tempList = new List<string> ( t );
						tempList.AddRange(new string[] { Convert.ToString(tempPriv) });
						TableList.Add(tempList.ToArray());
					}
				}
				TableNameCount = TableList.Count;

				// Get action
				if (Empty(Post("action"))) {
					CurrentAction = "show"; // Display with input box

					// Load key from QueryString
					if (RouteValues.TryGetValue("UserLevelID", out object fldparm) && !Empty(fldparm)) { // DN
						UserLevelID.QueryValue = Convert.ToString(fldparm);
					} else if (Query.TryGetValue("UserLevelID", out StringValues sv) && !Empty(sv)) {
						UserLevelID.QueryValue = sv;
					} else {
						return Terminate("UserLevelslist"); // Return to list
					}
					if (UserLevelID.QueryValue == "-1") {
						Disabled = " disabled";
					} else {
						Disabled = "";
					}
				} else {
					CurrentAction = Post("action");

					// Get fields from form
					UserLevelID.FormValue = Post("x_UserLevelID");
					for (int i = 0; i < TableNameCount; i++) {
						if (!Empty(Post("Table_" + i))) {
							if (Config.UserLevelCompat) {
								Privileges.Add(i.ToString(), Post<int>("add_" + i) + Post<int>("delete_" + i) + Post<int>("edit_" + i) + Post<int>("list_" + i) + Post<int>("admin_" + i));
							} else {
								Privileges.Add(i.ToString(), Post<int>("add_" + i) + Post<int>("delete_" + i) + Post<int>("edit_" + i) + Post<int>("list_" + i) + Post<int>("view_" + i) + Post<int>("search_" + i) + Post<int>("admin_" + i));
							}
						}
					}
				}

				// Should not edit own permissions
				if (ConvertToInt(UserLevelID.CurrentValue) == Security.CurrentUserLevelID)
					return Terminate("UserLevelslist"); // Return to list
				switch (CurrentAction) {
					case "show": // Display
						if (!await Security.SetupUserLevels()) // Get all User Level info
							return Terminate("UserLevelslist"); // Return to list
						var list = new List<Dictionary<string, object>>();
						for (int i = 0; i < TableNameCount; i++) {
							int tempPriv = Security.GetUserLevelPriv(TableList[i][4] + TableList[i][0], ConvertToInt(UserLevelID.CurrentValue));
							list.Add(new Dictionary<string, object> { {"table", GetTableCaption(i)}, {"index", i}, {"permission", tempPriv}, {"allowed", TableList[i][6]} });
						}
						Privileges.Add("disabled", Disabled);
						Privileges.Add("permissions", list);
						Privileges.Add("allowAdd", 1); // Add
						Privileges.Add("allowDelete", 2); // Delete
						Privileges.Add("allowEdit", 4); // Edit
						Privileges.Add("allowList", 8); // List
						Privileges.Add("userLevelCompat", Config.UserLevelCompat);
						if (Config.UserLevelCompat) {
							Privileges.Add("allowView", 8); // View
							Privileges.Add("allowSearch", 8); // Search
						} else {
							Privileges.Add("allowView", 32); // View
							Privileges.Add("allowSearch", 64); // Search
						}
						Privileges.Add("allowReport", 8); // Report
						Privileges.Add("allowAdmin", 16); // Admin
						break;
					case "update": // Update
						if (await EditRow()) { // Update record based on key
							if (Empty(SuccessMessage))
								SuccessMessage = Language.Phrase("UpdateSuccess"); // Set up update success message

							// Alternatively, comment out the following line to go back to this page
							return Terminate("UserLevelslist"); // Return to list
						}
						break;
				}
				return PageResult();
			}

			// Update privileges
			public async Task<bool> EditRow() {
				DatabaseConnectionBase<SqlConnection, SqlCommand, SqlDataReader, SqlDbType> c = await GetConnectionAsync(Config.UserLevelPrivDbId);
				int privilege;
				foreach (var (key, value) in Privileges) {
					int i = ConvertToInt(key);
					string sql = "SELECT * FROM " + Config.UserLevelPrivTable + " WHERE " +
						Config.UserLevelPrivTableNameField + " = '" + AdjustSql(TableList[i][4] + TableList[i][0], Config.UserLevelPrivDbId) + "' AND " +
						Config.UserLevelPrivUserLevelIdField + " = " + UserLevelID.CurrentValue;
					privilege = ConvertToInt(value) & ConvertToInt(TableList[i][6]); // Set maximum allowed privilege (protect from hacking) 
					bool insert = true;
					using (var rs = await c.GetDataReaderAsync(sql)) {
						if (rs != null && await rs.ReadAsync())
							insert = false;
					}
					if (!insert)
						sql = "UPDATE " + Config.UserLevelPrivTable + " SET " + Config.UserLevelPrivPrivField + " = " + privilege + " WHERE " +
							Config.UserLevelPrivTableNameField + " = '" + AdjustSql(TableList[i][4] + TableList[i][0], Config.UserLevelPrivDbId) + "' AND " +
							Config.UserLevelPrivUserLevelIdField + " = " + UserLevelID.CurrentValue;
					else
						sql = "INSERT INTO " + Config.UserLevelPrivTable + " (" + Config.UserLevelPrivTableNameField + ", " + Config.UserLevelPrivUserLevelIdField + ", " + Config.UserLevelPrivPrivField + ") VALUES ('" + AdjustSql(TableList[i][4] + TableList[i][0], Config.UserLevelPrivDbId) + "', " + UserLevelID.CurrentValue + ", " + privilege + ")";
					await c.ExecuteAsync(sql);
				}
				return true;
			}

			// Get table caption
			protected string GetTableCaption(int i) {
				string caption = "";
				if (i < TableNameCount) {
					bool report = (TableList[i][4] == Config.RelatedProjectId);
					bool other = (!report && TableList[i][4] != CurrentProjectID);
					if (!report && !other)
						caption = Language.TablePhrase(TableList[i][1], "TblCaption");
					if (report && ReportLanguage != null)
						caption = ReportLanguage.TablePhrase(TableList[i][1], "TblCaption");
					if (caption == "")
						caption = TableList[i][2];
					if (caption == "") {
						caption = TableList[i][0];
						caption = Regex.Replace(caption, @"^\{\w{8}-\w{4}-\w{4}-\w{4}-\w{12}\}/", ""); // Remove project ID
					}
					if (report)
						caption += "<span class=\"ew-user-priv-project\"> (" + Language.Phrase("Report") + ")</span>";
					if (other) {
						string project;
						if (!Empty(TableList[i][5])) {
							project = Path.GetFileNameWithoutExtension(TableList[i][5]);
						} else {
							project = TableList[i][4];
						}

						//project = TableList[i][4]; // *** Uncomment to use project id
						caption += "<span class=\"ew-user-priv-project\"> (" + project + ")</span>";
					}
				}
				return caption;
			}

			// Page Load event
			public virtual void Page_Load() {

				//Log("Page Load");
			}

			// Page Unload event
			public virtual void Page_Unload() {

				//Log("Page Unload");
			}

			// Page Redirecting event
			public virtual void Page_Redirecting(ref string url) {

				//url = newurl;
			}

			// Message Showing event
			// type = ""|"success"|"failure"|"warning"

			public virtual void Message_Showing(ref string msg, string type) {

				// Note: Do not change msg outside the following 4 cases.
				if (type == "success") {

					//msg = "your success message";
				} else if (type == "failure") {

					//msg = "your failure message";
				} else if (type == "warning") {

					//msg = "your warning message";
				} else {

					//msg = "your message";
				}
			}

			// Page Load event
			public virtual void Page_Render() {

				//Log("Page Render");
			}

			// Page Data Rendering event
			public virtual void Page_DataRendering(ref string header) {

				// Example:
				//header = "your header";

			}

			// Page Data Rendered event
			public virtual void Page_DataRendered(ref string footer) {

				// Example:
				//footer = "your footer";

			}
		}
	}
}
