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
		/// s_employee_Delete
		/// </summary>

		public static _s_employee_Delete s_employee_Delete {
			get => HttpData.Get<_s_employee_Delete>("s_employee_Delete");
			set => HttpData["s_employee_Delete"] = value;
		}

		/// <summary>
		/// Page class for s_employee
		/// </summary>

		public class _s_employee_Delete : _s_employee_DeleteBase
		{

			// Construtor
			public _s_employee_Delete(Controller controller = null) : base(controller) {
			}
		}

		/// <summary>
		/// Page base class
		/// </summary>

		public class _s_employee_DeleteBase : _s_employee, IAspNetMakerPage
		{

			// Page ID
			public string PageID = "delete";

			// Project ID
			public string ProjectID = "{8543F230-11C6-4105-B51C-8D87C21BE659}";

			// Table name
			public string TableName = "s_employee";

			// Page object name
			public string PageObjName = "s_employee_Delete";

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
					else if (!Empty(Caption))
						return Caption;
					else
						return "";
				}
			}

			// Page subheading
			public string PageSubheading {
				get {
					if (!Empty(Subheading))
						return Subheading;
					if (!Empty(TableName))
						return Language.Phrase(PageID);
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
			public _s_employee_DeleteBase(Controller controller = null) { // DN
				if (controller != null)
					Controller = controller;

				// Initialize
				CurrentPage = this;

				// Language object
				Language = Language ?? new Lang();

				// Table object (s_employee)
				if (s_employee == null || s_employee is _s_employee)
					s_employee = this;

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
				if (!Empty(CustomExport) && CustomExport == Export && Config.Export.TryGetValue(CustomExport, out string classname)) {
					IActionResult result = null;
					string content = await GetViewOutput();
					if (Empty(ExportFileName))
						ExportFileName = TableVar;
					dynamic doc = CreateInstance(classname, new object[] { s_employee, "" }); // DN
					doc.Text.Append(content);
					result = doc.Export();
					DeleteTempImages(); // Delete temp images
					return result;
				}
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

			// Get all records from datareader
			protected async Task<List<Dictionary<string, object>>> GetRecordsFromRecordset(DbDataReader rs)
			{
				var rows = new List<Dictionary<string, object>>();
				while (rs != null && await rs.ReadAsync()) {
					await LoadRowValues(rs); // Set up DbValue/CurrentValue
					rows.Add(GetRecordFromDictionary(GetDictionary(rs)));
				}
				return rows;
			}

			// Get the first record from datareader
			protected async Task<Dictionary<string, object>> GetRecordFromRecordset(DbDataReader rs)
			{
				if (rs != null) {
					await LoadRowValues(rs); // Set up DbValue/CurrentValue
					return GetRecordFromDictionary(GetDictionary(rs));
				}
				return null;
			}

			// Get the first record from the list of records
			protected Dictionary<string, object> GetRecordFromRecordset(List<Dictionary<string, object>> ar) => GetRecordFromDictionary(ar[0]);

			// Get record from Dictionary
			protected Dictionary<string, object> GetRecordFromDictionary(Dictionary<string, object> ar) {
				var row = new Dictionary<string, object>();
				foreach (var (key, value) in ar) {
					if (Fields.TryGetValue(key, out DbField fld)) {
						if (fld.Visible || fld.IsPrimaryKey) { // Primary key or Visible
							if (fld.HtmlTag == "FILE") { // Upload field
								if (Empty(value)) {
									row[key] = null;
								} else {
									if (fld.DataType == Config.DataTypeBlob) {
										string url = FullUrl(GetPageName(Config.ApiUrl) + "/" + Config.ApiFileAction + "/" + fld.TableVar + "/" + fld.Param + "/" + GetRecordKeyValue(ar)); // Query string format
										row[key] = new Dictionary<string, object> { { "mimeType", ContentType((byte[])value) }, { "url", url } };
									} else if (!fld.UploadMultiple || !Convert.ToString(value).Contains(Config.MultipleUploadSeparator)) { // Single file
										row[key] = new Dictionary<string, object> { { "mimeType", ContentType(Convert.ToString(value)) }, { "url", FullUrl(fld.HrefPath + Convert.ToString(value)) } };
									} else { // Multiple files
										var files = Convert.ToString(value).Split(Config.MultipleUploadSeparator);
										row[key] = files.Where(file => !Empty(file)).Select(file => new Dictionary<string, object> { { "type", ContentType(file) }, { "url", FullUrl(fld.HrefPath + file) } });
									}
								}
							} else {
								row[key] = Convert.ToString(value);
							}
						}
					}
				}
				return row;
			}

			// Get record key value from array
			protected string GetRecordKeyValue(Dictionary<string, object> ar) {
				string key = "";
				key += UrlEncode(Convert.ToString(ar["Id"]));
				return key;
			}

			// Hide fields for Add/Edit
			protected void HideFieldsForAddEdit() {
				if (IsAdd || IsCopy || IsGridAdd)
					Id.Visible = false;
			}
			public string DbMasterFilter = "";
			public string DbDetailFilter = "";
			public int StartRecord;
			public int TotalRecords;
			public int RecordCount;
			public List<string> RecordKeys;
			public DbDataReader Recordset;
			public int StartRowCount = 1;

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
					Security.LoadCurrentUserLevel(ProjectID + TableName);
					if (Security.IsLoggedIn)
						Security.TablePermission_Loaded();
					if (!Security.CanDelete) {
						Security.SaveLastUrl();
						FailureMessage = DeniedMessage(); // Set no permission
						if (IsApi())
							return new JsonBoolResult(new { success = false, error = DeniedMessage(), version = Config.ProductVersion }, false);
						if (Security.CanList)
							return Terminate(GetUrl("s_employeelist"));
						else
							return Terminate(GetUrl("login"));
					}
					if (Security.IsLoggedIn) {
						Security.UserID_Loading();
						await Security.LoadUserID();
						Security.UserID_Loaded();
					if (Empty(Security.CurrentUserID)) {
						FailureMessage = DeniedMessage(); // Set no permission
						return Terminate(GetUrl("s_employeelist"));
					}
					}
				}
				CurrentAction = Param("action"); // Set up current action
				Id.SetVisibility();
				employeeid.SetVisibility();
				fname.SetVisibility();
				lname.SetVisibility();
				oldic.SetVisibility();
				newic.SetVisibility();
				passport.SetVisibility();
				address1.SetVisibility();
				address2.SetVisibility();
				Id_city.SetVisibility();
				Id_state.SetVisibility();
				Id_country.SetVisibility();
				postcode.SetVisibility();
				gender.SetVisibility();
				tel.SetVisibility();
				handphone.SetVisibility();
				_email.SetVisibility();
				dob.SetVisibility();
				children.SetVisibility();
				datejoin.SetVisibility();
				dateresign.SetVisibility();
				marriedstatus.SetVisibility();
				Id_dept.SetVisibility();
				Id_position.SetVisibility();
				Id_race.SetVisibility();
				photopath.Visible = false;
				report_to.SetVisibility();
				login_effectivedate.SetVisibility();
				login_disableddate.SetVisibility();
				UserLevelId.SetVisibility();
				_Username.SetVisibility();
				password.SetVisibility();
				active.SetVisibility();
				HideFieldsForAddEdit();

				// Do not use lookup cache
				SetUseLookupCache(false);

				// Global Page Loading event
				Page_Loading();

				// Page Load event
				Page_Load();

				// Check token
				if (!await ValidPost())
					End(Language.Phrase("InvalidPostRequest"));

				// Create token
				CreateToken();

				// Set up lookup cache
				await SetupLookupOptions(UserLevelId);

				// Set up Breadcrumb
				SetupBreadcrumb();

				// Load key parameters
				RecordKeys = GetRecordKeys(); // Load record keys
				string filter = GetFilterFromRecordKeys();
				if (Empty(filter))
					return Terminate("s_employeelist"); // Prevent SQL injection, return to List page

				// Set up filter (WHERE Clause)
				CurrentFilter = filter;

				// Check if valid User ID
				string sql = GetSql(CurrentFilter);
				using (Recordset = await Connection.OpenDataReaderAsync(sql)) {
					if (Recordset != null) {
						bool res = true;
						while (await Recordset.ReadAsync()) {
							await LoadRowValues(Recordset);
							if (!ShowOptionLink("delete")) {
								string userIdMsg = Language.Phrase("NoDeletePermission");
								FailureMessage = userIdMsg;
								res = false;
								break;
							}
						}
						if (!res)
							return Terminate("s_employeelist"); // Return to List page
					}
				}

				// Get action
				if (IsApi()) {
					CurrentAction = "delete"; // Delete record directly
				} else if (!Empty(Post("action"))) {
					CurrentAction = Post("action");
				} else if (Get<bool>("action")) {
					CurrentAction = "delete"; // Delete record directly
				} else {
					CurrentAction = "show"; // Display record
				}
				if (IsDelete) { // DN
					SendEmail = true; // Send email on delete success
					var res = await DeleteRows();
					if (res) { // Delete rows
						if (Empty(SuccessMessage))
							SuccessMessage = Language.Phrase("DeleteSuccess"); // Set up success message
						if (IsApi()) {
							return res;
						} else {
							return Terminate(ReturnUrl); // Return to caller
						}
					} else { // Delete failed
						if (IsApi())
							return Terminate();
						CurrentAction = "show"; // Display record
					}
				}
				if (IsShow) { // Load records for display // DN
					Recordset = await LoadRecordset();
					TotalRecords = await ListRecordCount(); // Get record count
					if (TotalRecords <= 0) { // No record found, exit
						CloseRecordset(); // DN
						return Terminate("s_employeelist"); // Return to list
					}
				}
				return PageResult();
			}

			// Load recordset // DN
			public async Task<DbDataReader> LoadRecordset(int offset = -1, int rowcnt = -1) {

				// Load list page SQL
				string sql = ListSql;

				// Load recordset (Recordset_Selected event not supported) // DN
				return await Connection.SelectLimit(sql, rowcnt, offset, !Empty(OrderBy) || !Empty(SessionOrderBy));
			}

			// Load row based on key values
			public async Task<bool> LoadRow() {
				string filter = GetRecordFilter();

				// Call Row Selecting event
				Row_Selecting(ref filter);

				// Load SQL based on filter
				CurrentFilter = filter;
				string sql = CurrentSql;
				bool res = false;
				try {
					using (var rsrow = await Connection.OpenDataReaderAsync(sql)) {
						if (rsrow != null && await rsrow.ReadAsync()) {
							await LoadRowValues(rsrow);
							res = true;
						} else {
							return false;
						}
					}
				} catch {
					if (Config.Debug)
						throw;
				}
				return res;
			}

			#pragma warning disable 162, 168, 1998

			// Load row values from recordset
			public async Task LoadRowValues(DbDataReader dr = null) {
				Dictionary<string, object> row;
				object v;
				if (dr != null && dr.HasRows)
					row = Connection.GetRow(dr); // DN
				else
					row = NewRow();

				// Call Row Selected event
				Row_Selected(row);
				if (dr == null || !dr.HasRows)
					return;
				Id.SetDbValue(row["Id"]);
				employeeid.SetDbValue(row["employeeid"]);
				fname.SetDbValue(row["fname"]);
				lname.SetDbValue(row["lname"]);
				oldic.SetDbValue(row["oldic"]);
				newic.SetDbValue(row["newic"]);
				passport.SetDbValue(row["passport"]);
				address1.SetDbValue(row["address1"]);
				address2.SetDbValue(row["address2"]);
				Id_city.SetDbValue(row["Id_city"]);
				Id_state.SetDbValue(row["Id_state"]);
				Id_country.SetDbValue(row["Id_country"]);
				postcode.SetDbValue(row["postcode"]);
				gender.SetDbValue(row["gender"]);
				tel.SetDbValue(row["tel"]);
				handphone.SetDbValue(row["handphone"]);
				_email.SetDbValue(row["email"]);
				dob.SetDbValue(row["dob"]);
				children.SetDbValue(row["children"]);
				datejoin.SetDbValue(row["datejoin"]);
				dateresign.SetDbValue(row["dateresign"]);
				marriedstatus.SetDbValue(row["marriedstatus"]);
				Id_dept.SetDbValue(row["Id_dept"]);
				Id_position.SetDbValue(row["Id_position"]);
				Id_race.SetDbValue(row["Id_race"]);
				photopath.SetDbValue(row["photopath"]);
				report_to.SetDbValue(row["report_to"]);
				login_effectivedate.SetDbValue(row["login_effectivedate"]);
				login_disableddate.SetDbValue(row["login_disableddate"]);
				UserLevelId.SetDbValue(row["UserLevelId"]);
				_Username.SetDbValue(row["Username"]);
				password.SetDbValue(row["password"]);
				active.SetDbValue((ConvertToBool(row["active"]) ? "1" : "0"));
			}

			#pragma warning restore 162, 168, 1998

			// Return a row with default values
			protected Dictionary<string, object> NewRow() {
				var row = new Dictionary<string, object>();
				row.Add("Id", System.DBNull.Value);
				row.Add("employeeid", System.DBNull.Value);
				row.Add("fname", System.DBNull.Value);
				row.Add("lname", System.DBNull.Value);
				row.Add("oldic", System.DBNull.Value);
				row.Add("newic", System.DBNull.Value);
				row.Add("passport", System.DBNull.Value);
				row.Add("address1", System.DBNull.Value);
				row.Add("address2", System.DBNull.Value);
				row.Add("Id_city", System.DBNull.Value);
				row.Add("Id_state", System.DBNull.Value);
				row.Add("Id_country", System.DBNull.Value);
				row.Add("postcode", System.DBNull.Value);
				row.Add("gender", System.DBNull.Value);
				row.Add("tel", System.DBNull.Value);
				row.Add("handphone", System.DBNull.Value);
				row.Add("email", System.DBNull.Value);
				row.Add("dob", System.DBNull.Value);
				row.Add("children", System.DBNull.Value);
				row.Add("datejoin", System.DBNull.Value);
				row.Add("dateresign", System.DBNull.Value);
				row.Add("marriedstatus", System.DBNull.Value);
				row.Add("Id_dept", System.DBNull.Value);
				row.Add("Id_position", System.DBNull.Value);
				row.Add("Id_race", System.DBNull.Value);
				row.Add("photopath", System.DBNull.Value);
				row.Add("report_to", System.DBNull.Value);
				row.Add("login_effectivedate", System.DBNull.Value);
				row.Add("login_disableddate", System.DBNull.Value);
				row.Add("UserLevelId", System.DBNull.Value);
				row.Add("Username", System.DBNull.Value);
				row.Add("password", System.DBNull.Value);
				row.Add("active", System.DBNull.Value);
				return row;
			}

			#pragma warning disable 1998

			// Render row values based on field settings
			public async Task RenderRow() {

				// Call Row_Rendering event
				Row_Rendering();

				// Common render codes for all row types
				// Id
				// employeeid
				// fname
				// lname
				// oldic
				// newic
				// passport
				// address1
				// address2
				// Id_city
				// Id_state
				// Id_country
				// postcode
				// gender
				// tel
				// handphone
				// _email
				// dob
				// children
				// datejoin
				// dateresign
				// marriedstatus
				// Id_dept
				// Id_position
				// Id_race
				// photopath
				// report_to
				// login_effectivedate
				// login_disableddate
				// UserLevelId
				// _Username
				// password
				// active

				if (RowType == Config.RowTypeView) { // View row

					// Id
					Id.ViewValue = Id.CurrentValue;

					// employeeid
					employeeid.ViewValue = employeeid.CurrentValue;

					// fname
					fname.ViewValue = fname.CurrentValue;

					// lname
					lname.ViewValue = lname.CurrentValue;

					// oldic
					oldic.ViewValue = oldic.CurrentValue;

					// newic
					newic.ViewValue = newic.CurrentValue;

					// passport
					passport.ViewValue = passport.CurrentValue;

					// address1
					address1.ViewValue = address1.CurrentValue;

					// address2
					address2.ViewValue = address2.CurrentValue;

					// Id_city
					Id_city.ViewValue = Id_city.CurrentValue;
					Id_city.ViewValue = FormatNumber(Id_city.ViewValue, 0, -2, -2, -2);

					// Id_state
					Id_state.ViewValue = Id_state.CurrentValue;
					Id_state.ViewValue = FormatNumber(Id_state.ViewValue, 0, -2, -2, -2);

					// Id_country
					Id_country.ViewValue = Id_country.CurrentValue;
					Id_country.ViewValue = FormatNumber(Id_country.ViewValue, 0, -2, -2, -2);

					// postcode
					postcode.ViewValue = postcode.CurrentValue;

					// gender
					gender.ViewValue = gender.CurrentValue;

					// tel
					tel.ViewValue = tel.CurrentValue;

					// handphone
					handphone.ViewValue = handphone.CurrentValue;

					// _email
					_email.ViewValue = _email.CurrentValue;

					// dob
					dob.ViewValue = dob.CurrentValue;
					dob.ViewValue = FormatDateTime(dob.ViewValue, 0);

					// children
					children.ViewValue = children.CurrentValue;
					children.ViewValue = FormatNumber(children.ViewValue, 0, -2, -2, -2);

					// datejoin
					datejoin.ViewValue = datejoin.CurrentValue;
					datejoin.ViewValue = FormatDateTime(datejoin.ViewValue, 0);

					// dateresign
					dateresign.ViewValue = dateresign.CurrentValue;
					dateresign.ViewValue = FormatDateTime(dateresign.ViewValue, 0);

					// marriedstatus
					marriedstatus.ViewValue = marriedstatus.CurrentValue;

					// Id_dept
					Id_dept.ViewValue = Id_dept.CurrentValue;
					Id_dept.ViewValue = FormatNumber(Id_dept.ViewValue, 0, -2, -2, -2);

					// Id_position
					Id_position.ViewValue = Id_position.CurrentValue;
					Id_position.ViewValue = FormatNumber(Id_position.ViewValue, 0, -2, -2, -2);

					// Id_race
					Id_race.ViewValue = Id_race.CurrentValue;
					Id_race.ViewValue = FormatNumber(Id_race.ViewValue, 0, -2, -2, -2);

					// report_to
					report_to.ViewValue = report_to.CurrentValue;
					report_to.ViewValue = FormatNumber(report_to.ViewValue, 0, -2, -2, -2);

					// login_effectivedate
					login_effectivedate.ViewValue = login_effectivedate.CurrentValue;
					login_effectivedate.ViewValue = FormatDateTime(login_effectivedate.ViewValue, 0);

					// login_disableddate
					login_disableddate.ViewValue = login_disableddate.CurrentValue;
					login_disableddate.ViewValue = FormatDateTime(login_disableddate.ViewValue, 0);

					// UserLevelId
					if ((Security.CurrentUserLevel & Config.AllowAdmin) == Config.AllowAdmin) { // System admin
					curVal = Convert.ToString(UserLevelId.CurrentValue);
					if (!Empty(curVal)) {
						UserLevelId.ViewValue = UserLevelId.LookupCacheOption(curVal);
						if (UserLevelId.ViewValue == null) { // Lookup from database
							filterWrk = "[UserLevelID]" + SearchString("=", curVal.Trim(), Config.DataTypeNumber, "");
							sqlWrk = UserLevelId.Lookup.GetSql(false, filterWrk, null, this);
							rswrk = await Connection.GetRowsAsync(sqlWrk);
							if (rswrk != null && rswrk.Count > 0) { // Lookup values found
								var listwrk = rswrk[0].Values.ToList();
								listwrk[1] = Convert.ToString(FormatNumber(listwrk[1], 0, -2, -2, -2));
								UserLevelId.ViewValue = UserLevelId.DisplayValue(listwrk);
							} else {
								UserLevelId.ViewValue = UserLevelId.CurrentValue;
							}
						}
					} else {
						UserLevelId.ViewValue = System.DBNull.Value;
					}
					} else {
						UserLevelId.ViewValue = Language.Phrase("PasswordMask");
					}

					// _Username
					_Username.ViewValue = _Username.CurrentValue;

					// password
					password.ViewValue = password.CurrentValue;

					// active
					if (ConvertToBool(active.CurrentValue)) {
						active.ViewValue = (active.TagCaption(1) != "") ? active.TagCaption(1) : "Yes";
					} else {
						active.ViewValue = (active.TagCaption(2) != "") ? active.TagCaption(2) : "No";
					}

					// Id
					Id.HrefValue = "";
					Id.TooltipValue = "";

					// employeeid
					employeeid.HrefValue = "";
					employeeid.TooltipValue = "";

					// fname
					fname.HrefValue = "";
					fname.TooltipValue = "";

					// lname
					lname.HrefValue = "";
					lname.TooltipValue = "";

					// oldic
					oldic.HrefValue = "";
					oldic.TooltipValue = "";

					// newic
					newic.HrefValue = "";
					newic.TooltipValue = "";

					// passport
					passport.HrefValue = "";
					passport.TooltipValue = "";

					// address1
					address1.HrefValue = "";
					address1.TooltipValue = "";

					// address2
					address2.HrefValue = "";
					address2.TooltipValue = "";

					// Id_city
					Id_city.HrefValue = "";
					Id_city.TooltipValue = "";

					// Id_state
					Id_state.HrefValue = "";
					Id_state.TooltipValue = "";

					// Id_country
					Id_country.HrefValue = "";
					Id_country.TooltipValue = "";

					// postcode
					postcode.HrefValue = "";
					postcode.TooltipValue = "";

					// gender
					gender.HrefValue = "";
					gender.TooltipValue = "";

					// tel
					tel.HrefValue = "";
					tel.TooltipValue = "";

					// handphone
					handphone.HrefValue = "";
					handphone.TooltipValue = "";

					// _email
					_email.HrefValue = "";
					_email.TooltipValue = "";

					// dob
					dob.HrefValue = "";
					dob.TooltipValue = "";

					// children
					children.HrefValue = "";
					children.TooltipValue = "";

					// datejoin
					datejoin.HrefValue = "";
					datejoin.TooltipValue = "";

					// dateresign
					dateresign.HrefValue = "";
					dateresign.TooltipValue = "";

					// marriedstatus
					marriedstatus.HrefValue = "";
					marriedstatus.TooltipValue = "";

					// Id_dept
					Id_dept.HrefValue = "";
					Id_dept.TooltipValue = "";

					// Id_position
					Id_position.HrefValue = "";
					Id_position.TooltipValue = "";

					// Id_race
					Id_race.HrefValue = "";
					Id_race.TooltipValue = "";

					// report_to
					report_to.HrefValue = "";
					report_to.TooltipValue = "";

					// login_effectivedate
					login_effectivedate.HrefValue = "";
					login_effectivedate.TooltipValue = "";

					// login_disableddate
					login_disableddate.HrefValue = "";
					login_disableddate.TooltipValue = "";

					// UserLevelId
					UserLevelId.HrefValue = "";
					UserLevelId.TooltipValue = "";

					// _Username
					_Username.HrefValue = "";
					_Username.TooltipValue = "";

					// password
					password.HrefValue = "";
					password.TooltipValue = "";

					// active
					active.HrefValue = "";
					active.TooltipValue = "";
				}

				// Call Row Rendered event
				if (RowType != Config.RowTypeAggregateInit)
					Row_Rendered();
			}

			#pragma warning restore 1998

			// Delete records (based on current filter)
			protected async Task<JsonBoolResult> DeleteRows() { // DN
				if (!Security.CanDelete) {
					FailureMessage = Language.Phrase("NoDeletePermission"); // No delete permission
					return JsonBoolResult.FalseResult; // No delete permission
				}
				bool result = true;
				List<Dictionary<string, object>> rsold = null;
				try {
					string sql = CurrentSql;
					using (var rs = await Connection.GetDataReaderAsync(sql)) {
						if (rs == null) {
							return JsonBoolResult.FalseResult;
						} else if (!rs.HasRows) {
							FailureMessage = Language.Phrase("NoRecord"); // No record found
							return JsonBoolResult.FalseResult;
						} else { // Clone old rows
							rsold = await Connection.GetRowsAsync(rs);
						}
					}
				} catch (Exception e) {
					if (Config.Debug)
						throw;
					FailureMessage = e.Message;
					return JsonBoolResult.FalseResult;
				}
				s_employee = s_employee ?? new _s_employee();
				Connection.BeginTrans();
				var key = "";
				try {

					// Call Row Deleting event
					if (result)
						result = rsold.All(row => Row_Deleting(row));
					if (result) {
						foreach (var row in rsold) {
							var thisKey = "";
							if (!Empty(thisKey)) thisKey += Config.CompositeKeySeparator;
							thisKey += Convert.ToString(row["Id"]);
							if (Config.DeleteUploadFiles)
								DeleteUploadedFiles(row);
							try {
								await DeleteAsync(row);
							} catch (Exception e) {
								if (Config.Debug)
									throw;
								FailureMessage = e.Message; // Set up error message
								result = false;
								break;
							}
							if (!Empty(key))
								key += ", ";
							key += thisKey;
						}
					} 
					if (!result) {

						// Set up error message
						if (!Empty(SuccessMessage) || !Empty(FailureMessage)) {

							// Use the message, do nothing
						} else if (!Empty(CancelMessage)) {
							FailureMessage = CancelMessage;
							CancelMessage = "";
						} else {
							FailureMessage = Language.Phrase("DeleteCancelled");
						}
					}
				} catch (Exception e) {
					FailureMessage = e.Message;
					result = false;
				}
				if (result) {
					Connection.CommitTrans(); // Commit the changes
				} else {
					Connection.RollbackTrans(); // Rollback changes
				}

				// Call Row Deleted event
				if (result)
					rsold.ForEach(row => Row_Deleted(row));

				// Write JSON for API request (Support single row only)
				var d = new Dictionary<string, object>();
				d.Add("success", result);
				if (IsApi() && result) {
					var row = GetRecordFromRecordset(rsold);
					d.Add(TableVar, row);
					d.Add("version", Config.ProductVersion);
					return new JsonBoolResult(d, true);
				}
				return new JsonBoolResult(d, result);
			}

			// Save data to memory cache
			public void SetCache<T>(string key, T value, int span) => Cache.Set<T>(key, value, new MemoryCacheEntryOptions()
				.SetSlidingExpiration(TimeSpan.FromMilliseconds(span))); // Keep in cache for this time, reset time if accessed

			// Gete data from memory cache
			public void GetCache<T>(string key) => Cache.Get<T>(key);

			// Show link optionally based on User ID
			protected bool ShowOptionLink(string pageId = "") { // DN
				if (Security.IsLoggedIn && !Security.IsAdmin && !UserIDAllow(pageId))
					return Security.IsValidUserID(Id.CurrentValue);
				return true;
			}

			// Set up Breadcrumb
			protected void SetupBreadcrumb() {
				var breadcrumb = new Breadcrumb();
				string url = CurrentUrl();
				breadcrumb.Add("list", TableVar, AppPath(AddMasterUrl("s_employeelist")), "", TableVar, true);
				string pageId = "delete";
				breadcrumb.Add("delete", pageId, url);
				CurrentBreadcrumb = breadcrumb;
			}

			// Setup lookup options
			public async Task SetupLookupOptions(DbField fld)
			{
				Func<string> lookupFilter = null;
				if (!Empty(fld.Lookup) && fld.Lookup.Options.Count == 0) {

					// Set up lookup SQL
					switch (fld.FieldVar) {
						default:
							break;
					}

					// Always call to Lookup.GetSql so that user can setup Lookup.Options in Lookup_Selecting server event
					var sql = fld.Lookup.GetSql(false, "", lookupFilter, this);

					// Set up lookup cache
					if (fld.UseLookupCache && !Empty(sql) && fld.Lookup.ParentFields.Count == 0 && fld.Lookup.Options.Count == 0) {
						int totalCnt = await TryGetRecordCount(sql);
						if (totalCnt > fld.LookupCacheCount) // Total count > cache count, do not cache
							return;
						var ar = new Dictionary<string, Dictionary<string, object>>();
						var values = new List<object>();
						var conn = await GetConnectionAsync();
						List<Dictionary<string, object>> rs = await conn.GetRowsAsync(sql);
						if (rs != null) {
							foreach (var row in rs) {

								// Format the field values
								switch (fld.FieldVar) {
									case "x_UserLevelId":

										//row[1] = FormatNumber(row[1], 0, -2, -2, -2);
										//row["df"] = row[1];

										values = row.Values.ToList();
										row["df"] = FormatNumber(values[1], 0, -2, -2, -2);
									break;
								}
								if (!ar.ContainsKey(row.Values.First().ToString()))
									ar.Add(row.Values.First().ToString(), row);
							}
						}
						fld.Lookup.Options = ar;
					}
				}
			}

			// Close recordset
			public void CloseRecordset() {
				using (Recordset) {} // Dispose
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
