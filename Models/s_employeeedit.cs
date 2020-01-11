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
		/// s_employee_Edit
		/// </summary>

		public static _s_employee_Edit s_employee_Edit {
			get => HttpData.Get<_s_employee_Edit>("s_employee_Edit");
			set => HttpData["s_employee_Edit"] = value;
		}

		/// <summary>
		/// Page class for s_employee
		/// </summary>

		public class _s_employee_Edit : _s_employee_EditBase
		{

			// Construtor
			public _s_employee_Edit(Controller controller = null) : base(controller) {
			}
		}

		/// <summary>
		/// Page base class
		/// </summary>

		public class _s_employee_EditBase : _s_employee, IAspNetMakerPage
		{

			// Page ID
			public string PageID = "edit";

			// Project ID
			public string ProjectID = "{8543F230-11C6-4105-B51C-8D87C21BE659}";

			// Table name
			public string TableName = "s_employee";

			// Page object name
			public string PageObjName = "s_employee_Edit";

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
			public _s_employee_EditBase(Controller controller = null) { // DN
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

						// Handle modal response
						if (IsModal) { // Show as modal
							var row = new Dictionary<string, string> { {"url", GetUrl(url)}, {"modal", "1"} };
							string pageName = GetPageName(url);
							if (pageName != ListUrl) { // Not List page
								row.Add("caption", GetModalCaption(pageName));
								if (pageName == "s_employeeview")
									row.Add("view", "1");
							} else { // List page should not be shown as modal => error
								row.Add("error", FailureMessage);
								ClearFailureMessage();
							}
							return Controller.Json(row);
						} else {
							SaveDebugMessage();
							return Controller.LocalRedirect(AppPath(url));
						}
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
			public int DisplayRecords = 1; // Number of display records
			public int StartRecord;
			public int StopRecord;
			public int TotalRecords = -1;
			public int RecordRange = 10;
			public int RecordCount;
			public Dictionary<string, string> RecordKeys = new Dictionary<string, string>();
			public string FormClassName = "ew-horizontal ew-form ew-edit-form";
			public bool IsModal = false;
			public bool IsMobileOrModal = false;
			public string DbMasterFilter = "";
			public string DbDetailFilter = "";
			public DbDataReader Recordset; // DN
			public DbDataReader OldRecordset;

			#pragma warning disable 219

			/// <summary>
			/// Page run
			/// </summary>
			/// <returns>Page result</returns>

			public async Task<IActionResult> Run() {

				// Header
				Header(Config.Cache);

				// Is modal
				IsModal = Param<bool>("modal");

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
					if (!Security.CanEdit) {
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

				// Create form object
				CurrentForm = new HttpForm();
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
				photopath.SetVisibility();
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

				// Check modal
				if (IsModal)
					SkipHeaderFooter = true;
				IsMobileOrModal = IsMobile() || IsModal;
				FormClassName = "ew-form ew-edit-form ew-horizontal";
				bool loaded = false;
				bool postBack = false;

				// Set up current action and primary key
				if (IsApi()) {
					CurrentAction = "update"; // Update record directly
					postBack = true;
				} else if (!Empty(Post("action"))) {
					CurrentAction = Post("action"); // Get action code
					if (!IsShow) // Not reload record, handle as postback
						postBack = true;

					// Load key from form
					string[] keyValues = null;
					object rv;
					if (IsApi() && RouteValues.TryGetValue("key", out object k))
						keyValues = k.ToString().Split('/');
					if (RouteValues.TryGetValue("Id", out rv)) { // DN
						Id.FormValue = Convert.ToString(rv);
						RecordKeys["Id"] = Id.FormValue;
					} else if (CurrentForm.HasValue("x_Id")) {
						Id.FormValue = CurrentForm.GetValue("x_Id");
						RecordKeys["Id"] = Id.FormValue;
					} else if (IsApi() && !Empty(keyValues)) {
						RecordKeys["Id"] = Convert.ToString(keyValues[0]);
					}
				} else {
					CurrentAction = "show"; // Default action is display

					// Load key from QueryString
					bool loadByQuery = false;
					string[] keyValues = null;
					object rv;
					if (IsApi() && RouteValues.TryGetValue("key", out object k))
						keyValues = k.ToString().Split('/');
					if (RouteValues.TryGetValue("Id", out rv)) { // DN
						Id.QueryValue = Convert.ToString(rv);
						RecordKeys["Id"] = Id.QueryValue;
						loadByQuery = true;
					} else if (!Empty(Get("Id"))) {
						Id.QueryValue = Get("Id");
						RecordKeys["Id"] = Id.QueryValue;
						loadByQuery = true;
					} else if (IsApi() && !Empty(keyValues)) {
						Id.QueryValue = Convert.ToString(keyValues[0]);
						RecordKeys["Id"] = Id.QueryValue;
						loadByQuery = true;
					} else {
						Id.CurrentValue = System.DBNull.Value;
					}
				}

			// Load current record
			loaded = await LoadRow();

			// Process form if post back
			if (postBack) {
				await LoadFormValues(); // Get form values
				if (IsApi() && RouteValues.TryGetValue("key", out object k)) {
					var keyValues = k.ToString().Split('/');
					Id.FormValue = Convert.ToString(keyValues[0]);
				}
			}

			// Validate form if post back
			if (postBack) {
				if (!await ValidateForm()) {
					FailureMessage = FormError;
					EventCancelled = true; // Event cancelled
					RestoreFormValues();
					if (IsApi())
						return Terminate();
					else
						CurrentAction = ""; // Form error, reset action
				}
			}

			// Perform current action
			switch (CurrentAction) {
					case "show": // Get a record to display
						if (!loaded) { // Load record based on key
							if (Empty(FailureMessage))
								FailureMessage = Language.Phrase("NoRecord"); // No record found
							return Terminate("s_employeelist"); // No matching record, return to list
						}
						break;
					case "update": // Update // DN
						CloseRecordset(); // DN
						string returnUrl = ReturnUrl;
						if (GetPageName(returnUrl) == "s_employeelist")
							returnUrl = AddMasterUrl(ListUrl); // List page, return to List page with correct master key if necessary
						SendEmail = true; // Send email on update success
						var res = await EditRow();
						if (res) { // Update record based on key
							if (Empty(SuccessMessage))
								SuccessMessage = Language.Phrase("UpdateSuccess"); // Update success
							if (IsApi()) {
								return res;
							} else {
								return Terminate(returnUrl); // Return to caller
							}
						} else if (IsApi()) { // API request, return
							return Terminate();
						} else if (FailureMessage == Language.Phrase("NoRecord")) {
							return Terminate(returnUrl); // Return to caller
						} else {
							EventCancelled = true; // Event cancelled
							RestoreFormValues(); // Restore form values if update failed
						}
						break;
				}

				// Set up Breadcrumb
				SetupBreadcrumb();

				// Render the record
				RowType = Config.RowTypeEdit; // Render as Edit
				ResetAttributes();
				await RenderRow();
				return PageResult();
			}

			#pragma warning restore 219

			// Set up starting record parameters
			public void SetupStartRec() {
				int pageNo;

				// Exit if DisplayRecords = 0
				if (DisplayRecords == 0)
					return;
				if (IsPageRequest) { // Validate request

					// Check for a "start" parameter
					if (IsNumeric(Get(Config.TableStartRec))) {
						StartRecord = Get<int>(Config.TableStartRec);
						StartRecordNumber = StartRecord;
					} else if (IsNumeric(Get(Config.TablePageNumber))) {
						pageNo = Get<int>(Config.TablePageNumber);
						StartRecord = (pageNo - 1) * DisplayRecords + 1;
						if (StartRecord <= 0) {
							StartRecord = 1;
						} else if (StartRecord >= ((TotalRecords - 1) / DisplayRecords) * DisplayRecords + 1) {
							StartRecord = ((TotalRecords - 1) / DisplayRecords) * DisplayRecords + 1;
						}
						StartRecordNumber = StartRecord;
					}
				}
				StartRecord = StartRecordNumber;

				// Check if correct start record counter
				if (StartRecord <= 0) { // Avoid invalid start record counter
					StartRecord = 1; // Reset start record counter
					StartRecordNumber = StartRecord;
				} else if (StartRecord > TotalRecords) { // Avoid starting record > total records
					StartRecord = ((TotalRecords - 1) / DisplayRecords) * DisplayRecords + 1; // Point to last page first record
					StartRecordNumber = StartRecord;
				} else if ((StartRecord - 1) % DisplayRecords != 0) {
					StartRecord = ((StartRecord - 1) / DisplayRecords) * DisplayRecords + 1; // Point to page boundary
					StartRecordNumber = StartRecord;
				}
			}

			// Confirm page
			public bool ConfirmPage = false; // DN

			#pragma warning disable 1998

			// Get upload files
			public async Task GetUploadFiles()
			{

				// Get upload data
			}

			#pragma warning restore 1998

			#pragma warning disable 1998

			// Load form values
			protected async Task LoadFormValues() {
				string val;

				// Check field name 'Id' first before field var 'x_Id'
				val = CurrentForm.GetValue("Id", "x_Id");
				if (!Id.IsDetailKey)
					Id.FormValue = val;

				// Check field name 'employeeid' first before field var 'x_employeeid'
				val = CurrentForm.GetValue("employeeid", "x_employeeid");
				if (!employeeid.IsDetailKey) {
					if (IsApi() && val == null)
						employeeid.Visible = false; // Disable update for API request
					else
						employeeid.FormValue = val;
				}

				// Check field name 'fname' first before field var 'x_fname'
				val = CurrentForm.GetValue("fname", "x_fname");
				if (!fname.IsDetailKey) {
					if (IsApi() && val == null)
						fname.Visible = false; // Disable update for API request
					else
						fname.FormValue = val;
				}

				// Check field name 'lname' first before field var 'x_lname'
				val = CurrentForm.GetValue("lname", "x_lname");
				if (!lname.IsDetailKey) {
					if (IsApi() && val == null)
						lname.Visible = false; // Disable update for API request
					else
						lname.FormValue = val;
				}

				// Check field name 'oldic' first before field var 'x_oldic'
				val = CurrentForm.GetValue("oldic", "x_oldic");
				if (!oldic.IsDetailKey) {
					if (IsApi() && val == null)
						oldic.Visible = false; // Disable update for API request
					else
						oldic.FormValue = val;
				}

				// Check field name 'newic' first before field var 'x_newic'
				val = CurrentForm.GetValue("newic", "x_newic");
				if (!newic.IsDetailKey) {
					if (IsApi() && val == null)
						newic.Visible = false; // Disable update for API request
					else
						newic.FormValue = val;
				}

				// Check field name 'passport' first before field var 'x_passport'
				val = CurrentForm.GetValue("passport", "x_passport");
				if (!passport.IsDetailKey) {
					if (IsApi() && val == null)
						passport.Visible = false; // Disable update for API request
					else
						passport.FormValue = val;
				}

				// Check field name 'address1' first before field var 'x_address1'
				val = CurrentForm.GetValue("address1", "x_address1");
				if (!address1.IsDetailKey) {
					if (IsApi() && val == null)
						address1.Visible = false; // Disable update for API request
					else
						address1.FormValue = val;
				}

				// Check field name 'address2' first before field var 'x_address2'
				val = CurrentForm.GetValue("address2", "x_address2");
				if (!address2.IsDetailKey) {
					if (IsApi() && val == null)
						address2.Visible = false; // Disable update for API request
					else
						address2.FormValue = val;
				}

				// Check field name 'Id_city' first before field var 'x_Id_city'
				val = CurrentForm.GetValue("Id_city", "x_Id_city");
				if (!Id_city.IsDetailKey) {
					if (IsApi() && val == null)
						Id_city.Visible = false; // Disable update for API request
					else
						Id_city.FormValue = val;
				}

				// Check field name 'Id_state' first before field var 'x_Id_state'
				val = CurrentForm.GetValue("Id_state", "x_Id_state");
				if (!Id_state.IsDetailKey) {
					if (IsApi() && val == null)
						Id_state.Visible = false; // Disable update for API request
					else
						Id_state.FormValue = val;
				}

				// Check field name 'Id_country' first before field var 'x_Id_country'
				val = CurrentForm.GetValue("Id_country", "x_Id_country");
				if (!Id_country.IsDetailKey) {
					if (IsApi() && val == null)
						Id_country.Visible = false; // Disable update for API request
					else
						Id_country.FormValue = val;
				}

				// Check field name 'postcode' first before field var 'x_postcode'
				val = CurrentForm.GetValue("postcode", "x_postcode");
				if (!postcode.IsDetailKey) {
					if (IsApi() && val == null)
						postcode.Visible = false; // Disable update for API request
					else
						postcode.FormValue = val;
				}

				// Check field name 'gender' first before field var 'x_gender'
				val = CurrentForm.GetValue("gender", "x_gender");
				if (!gender.IsDetailKey) {
					if (IsApi() && val == null)
						gender.Visible = false; // Disable update for API request
					else
						gender.FormValue = val;
				}

				// Check field name 'tel' first before field var 'x_tel'
				val = CurrentForm.GetValue("tel", "x_tel");
				if (!tel.IsDetailKey) {
					if (IsApi() && val == null)
						tel.Visible = false; // Disable update for API request
					else
						tel.FormValue = val;
				}

				// Check field name 'handphone' first before field var 'x_handphone'
				val = CurrentForm.GetValue("handphone", "x_handphone");
				if (!handphone.IsDetailKey) {
					if (IsApi() && val == null)
						handphone.Visible = false; // Disable update for API request
					else
						handphone.FormValue = val;
				}

				// Check field name 'email' first before field var 'x__email'
				val = CurrentForm.GetValue("email", "x__email");
				if (!_email.IsDetailKey) {
					if (IsApi() && val == null)
						_email.Visible = false; // Disable update for API request
					else
						_email.FormValue = val;
				}

				// Check field name 'dob' first before field var 'x_dob'
				val = CurrentForm.GetValue("dob", "x_dob");
				if (!dob.IsDetailKey) {
					if (IsApi() && val == null)
						dob.Visible = false; // Disable update for API request
					else
						dob.FormValue = val;
					dob.CurrentValue = UnformatDateTime(dob.CurrentValue, 0);
				}

				// Check field name 'children' first before field var 'x_children'
				val = CurrentForm.GetValue("children", "x_children");
				if (!children.IsDetailKey) {
					if (IsApi() && val == null)
						children.Visible = false; // Disable update for API request
					else
						children.FormValue = val;
				}

				// Check field name 'datejoin' first before field var 'x_datejoin'
				val = CurrentForm.GetValue("datejoin", "x_datejoin");
				if (!datejoin.IsDetailKey) {
					if (IsApi() && val == null)
						datejoin.Visible = false; // Disable update for API request
					else
						datejoin.FormValue = val;
					datejoin.CurrentValue = UnformatDateTime(datejoin.CurrentValue, 0);
				}

				// Check field name 'dateresign' first before field var 'x_dateresign'
				val = CurrentForm.GetValue("dateresign", "x_dateresign");
				if (!dateresign.IsDetailKey) {
					if (IsApi() && val == null)
						dateresign.Visible = false; // Disable update for API request
					else
						dateresign.FormValue = val;
					dateresign.CurrentValue = UnformatDateTime(dateresign.CurrentValue, 0);
				}

				// Check field name 'marriedstatus' first before field var 'x_marriedstatus'
				val = CurrentForm.GetValue("marriedstatus", "x_marriedstatus");
				if (!marriedstatus.IsDetailKey) {
					if (IsApi() && val == null)
						marriedstatus.Visible = false; // Disable update for API request
					else
						marriedstatus.FormValue = val;
				}

				// Check field name 'Id_dept' first before field var 'x_Id_dept'
				val = CurrentForm.GetValue("Id_dept", "x_Id_dept");
				if (!Id_dept.IsDetailKey) {
					if (IsApi() && val == null)
						Id_dept.Visible = false; // Disable update for API request
					else
						Id_dept.FormValue = val;
				}

				// Check field name 'Id_position' first before field var 'x_Id_position'
				val = CurrentForm.GetValue("Id_position", "x_Id_position");
				if (!Id_position.IsDetailKey) {
					if (IsApi() && val == null)
						Id_position.Visible = false; // Disable update for API request
					else
						Id_position.FormValue = val;
				}

				// Check field name 'Id_race' first before field var 'x_Id_race'
				val = CurrentForm.GetValue("Id_race", "x_Id_race");
				if (!Id_race.IsDetailKey) {
					if (IsApi() && val == null)
						Id_race.Visible = false; // Disable update for API request
					else
						Id_race.FormValue = val;
				}

				// Check field name 'photopath' first before field var 'x_photopath'
				val = CurrentForm.GetValue("photopath", "x_photopath");
				if (!photopath.IsDetailKey) {
					if (IsApi() && val == null)
						photopath.Visible = false; // Disable update for API request
					else
						photopath.FormValue = val;
				}

				// Check field name 'report_to' first before field var 'x_report_to'
				val = CurrentForm.GetValue("report_to", "x_report_to");
				if (!report_to.IsDetailKey) {
					if (IsApi() && val == null)
						report_to.Visible = false; // Disable update for API request
					else
						report_to.FormValue = val;
				}

				// Check field name 'login_effectivedate' first before field var 'x_login_effectivedate'
				val = CurrentForm.GetValue("login_effectivedate", "x_login_effectivedate");
				if (!login_effectivedate.IsDetailKey) {
					if (IsApi() && val == null)
						login_effectivedate.Visible = false; // Disable update for API request
					else
						login_effectivedate.FormValue = val;
					login_effectivedate.CurrentValue = UnformatDateTime(login_effectivedate.CurrentValue, 0);
				}

				// Check field name 'login_disableddate' first before field var 'x_login_disableddate'
				val = CurrentForm.GetValue("login_disableddate", "x_login_disableddate");
				if (!login_disableddate.IsDetailKey) {
					if (IsApi() && val == null)
						login_disableddate.Visible = false; // Disable update for API request
					else
						login_disableddate.FormValue = val;
					login_disableddate.CurrentValue = UnformatDateTime(login_disableddate.CurrentValue, 0);
				}

				// Check field name 'UserLevelId' first before field var 'x_UserLevelId'
				val = CurrentForm.GetValue("UserLevelId", "x_UserLevelId");
				if (!UserLevelId.IsDetailKey) {
					if (IsApi() && val == null)
						UserLevelId.Visible = false; // Disable update for API request
					else
						UserLevelId.FormValue = val;
				}

				// Check field name 'Username' first before field var 'x__Username'
				val = CurrentForm.GetValue("Username", "x__Username");
				if (!_Username.IsDetailKey) {
					if (IsApi() && val == null)
						_Username.Visible = false; // Disable update for API request
					else
						_Username.FormValue = val;
				}

				// Check field name 'password' first before field var 'x_password'
				val = CurrentForm.GetValue("password", "x_password");
				if (!password.IsDetailKey) {
					if (IsApi() && val == null)
						password.Visible = false; // Disable update for API request
					else
						password.FormValue = val;
				}

				// Check field name 'active' first before field var 'x_active'
				val = CurrentForm.GetValue("active", "x_active");
				if (!active.IsDetailKey) {
					if (IsApi() && val == null)
						active.Visible = false; // Disable update for API request
					else
						active.FormValue = val;
				}
			}

			#pragma warning restore 1998

			// Restore form values
			public void RestoreFormValues() {
				Id.CurrentValue = Id.FormValue;
				employeeid.CurrentValue = employeeid.FormValue;
				fname.CurrentValue = fname.FormValue;
				lname.CurrentValue = lname.FormValue;
				oldic.CurrentValue = oldic.FormValue;
				newic.CurrentValue = newic.FormValue;
				passport.CurrentValue = passport.FormValue;
				address1.CurrentValue = address1.FormValue;
				address2.CurrentValue = address2.FormValue;
				Id_city.CurrentValue = Id_city.FormValue;
				Id_state.CurrentValue = Id_state.FormValue;
				Id_country.CurrentValue = Id_country.FormValue;
				postcode.CurrentValue = postcode.FormValue;
				gender.CurrentValue = gender.FormValue;
				tel.CurrentValue = tel.FormValue;
				handphone.CurrentValue = handphone.FormValue;
				_email.CurrentValue = _email.FormValue;
				dob.CurrentValue = dob.FormValue;
				dob.CurrentValue = UnformatDateTime(dob.CurrentValue, 0);
				children.CurrentValue = children.FormValue;
				datejoin.CurrentValue = datejoin.FormValue;
				datejoin.CurrentValue = UnformatDateTime(datejoin.CurrentValue, 0);
				dateresign.CurrentValue = dateresign.FormValue;
				dateresign.CurrentValue = UnformatDateTime(dateresign.CurrentValue, 0);
				marriedstatus.CurrentValue = marriedstatus.FormValue;
				Id_dept.CurrentValue = Id_dept.FormValue;
				Id_position.CurrentValue = Id_position.FormValue;
				Id_race.CurrentValue = Id_race.FormValue;
				photopath.CurrentValue = photopath.FormValue;
				report_to.CurrentValue = report_to.FormValue;
				login_effectivedate.CurrentValue = login_effectivedate.FormValue;
				login_effectivedate.CurrentValue = UnformatDateTime(login_effectivedate.CurrentValue, 0);
				login_disableddate.CurrentValue = login_disableddate.FormValue;
				login_disableddate.CurrentValue = UnformatDateTime(login_disableddate.CurrentValue, 0);
				UserLevelId.CurrentValue = UserLevelId.FormValue;
				_Username.CurrentValue = _Username.FormValue;
				password.CurrentValue = password.FormValue;
				active.CurrentValue = active.FormValue;
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

				// Check if valid User ID
				if (res) {
					res = ShowOptionLink("edit");
					if (!res)
						FailureMessage = DeniedMessage();
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

			#pragma warning disable 618, 1998

			// Load old record
			protected async Task<bool> LoadOldRecord(DatabaseConnectionBase<SqlConnection, SqlCommand, SqlDataReader, SqlDbType> cnn = null) {
				bool validKey = true;
				if (!Empty(GetKey("Id")))
					Id.CurrentValue = GetKey("Id"); // Id
				else
					validKey = false;

				// Load old record
				OldRecordset = null;
				if (validKey) {
					CurrentFilter = GetRecordFilter();
					string sql = CurrentSql;
					try {
						if (cnn != null) {
							OldRecordset = await cnn.OpenDataReaderAsync(sql);
						 } else {
							OldRecordset = await Connection.OpenDataReaderAsync(sql);
						 }
						if (OldRecordset != null)
							await OldRecordset.ReadAsync();
					} catch {
						OldRecordset = null;
					}
				}
				await LoadRowValues(OldRecordset); // Load row values
				return validKey;
			}

			#pragma warning restore 618, 1998

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

					// photopath
					photopath.ViewValue = photopath.CurrentValue;

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

					// photopath
					photopath.HrefValue = "";
					photopath.TooltipValue = "";

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
				} else if (RowType == Config.RowTypeEdit) { // Edit row

					// Id
					Id.EditAttrs["class"] = "form-control";
					Id.EditValue = Id.CurrentValue;

					// employeeid
					employeeid.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						employeeid.CurrentValue = HtmlDecode(employeeid.CurrentValue);
					employeeid.EditValue = employeeid.CurrentValue; // DN
					employeeid.PlaceHolder = RemoveHtml(employeeid.Caption);

					// fname
					fname.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						fname.CurrentValue = HtmlDecode(fname.CurrentValue);
					fname.EditValue = fname.CurrentValue; // DN
					fname.PlaceHolder = RemoveHtml(fname.Caption);

					// lname
					lname.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						lname.CurrentValue = HtmlDecode(lname.CurrentValue);
					lname.EditValue = lname.CurrentValue; // DN
					lname.PlaceHolder = RemoveHtml(lname.Caption);

					// oldic
					oldic.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						oldic.CurrentValue = HtmlDecode(oldic.CurrentValue);
					oldic.EditValue = oldic.CurrentValue; // DN
					oldic.PlaceHolder = RemoveHtml(oldic.Caption);

					// newic
					newic.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						newic.CurrentValue = HtmlDecode(newic.CurrentValue);
					newic.EditValue = newic.CurrentValue; // DN
					newic.PlaceHolder = RemoveHtml(newic.Caption);

					// passport
					passport.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						passport.CurrentValue = HtmlDecode(passport.CurrentValue);
					passport.EditValue = passport.CurrentValue; // DN
					passport.PlaceHolder = RemoveHtml(passport.Caption);

					// address1
					address1.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						address1.CurrentValue = HtmlDecode(address1.CurrentValue);
					address1.EditValue = address1.CurrentValue; // DN
					address1.PlaceHolder = RemoveHtml(address1.Caption);

					// address2
					address2.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						address2.CurrentValue = HtmlDecode(address2.CurrentValue);
					address2.EditValue = address2.CurrentValue; // DN
					address2.PlaceHolder = RemoveHtml(address2.Caption);

					// Id_city
					Id_city.EditAttrs["class"] = "form-control";
					Id_city.EditValue = Id_city.CurrentValue; // DN
					Id_city.PlaceHolder = RemoveHtml(Id_city.Caption);

					// Id_state
					Id_state.EditAttrs["class"] = "form-control";
					Id_state.EditValue = Id_state.CurrentValue; // DN
					Id_state.PlaceHolder = RemoveHtml(Id_state.Caption);

					// Id_country
					Id_country.EditAttrs["class"] = "form-control";
					Id_country.EditValue = Id_country.CurrentValue; // DN
					Id_country.PlaceHolder = RemoveHtml(Id_country.Caption);

					// postcode
					postcode.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						postcode.CurrentValue = HtmlDecode(postcode.CurrentValue);
					postcode.EditValue = postcode.CurrentValue; // DN
					postcode.PlaceHolder = RemoveHtml(postcode.Caption);

					// gender
					gender.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						gender.CurrentValue = HtmlDecode(gender.CurrentValue);
					gender.EditValue = gender.CurrentValue; // DN
					gender.PlaceHolder = RemoveHtml(gender.Caption);

					// tel
					tel.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						tel.CurrentValue = HtmlDecode(tel.CurrentValue);
					tel.EditValue = tel.CurrentValue; // DN
					tel.PlaceHolder = RemoveHtml(tel.Caption);

					// handphone
					handphone.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						handphone.CurrentValue = HtmlDecode(handphone.CurrentValue);
					handphone.EditValue = handphone.CurrentValue; // DN
					handphone.PlaceHolder = RemoveHtml(handphone.Caption);

					// _email
					_email.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						_email.CurrentValue = HtmlDecode(_email.CurrentValue);
					_email.EditValue = _email.CurrentValue; // DN
					_email.PlaceHolder = RemoveHtml(_email.Caption);

					// dob
					dob.EditAttrs["class"] = "form-control";
					dob.EditValue = FormatDateTime(dob.CurrentValue, 8); // DN
					dob.PlaceHolder = RemoveHtml(dob.Caption);

					// children
					children.EditAttrs["class"] = "form-control";
					children.EditValue = children.CurrentValue; // DN
					children.PlaceHolder = RemoveHtml(children.Caption);

					// datejoin
					datejoin.EditAttrs["class"] = "form-control";
					datejoin.EditValue = FormatDateTime(datejoin.CurrentValue, 8); // DN
					datejoin.PlaceHolder = RemoveHtml(datejoin.Caption);

					// dateresign
					dateresign.EditAttrs["class"] = "form-control";
					dateresign.EditValue = FormatDateTime(dateresign.CurrentValue, 8); // DN
					dateresign.PlaceHolder = RemoveHtml(dateresign.Caption);

					// marriedstatus
					marriedstatus.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						marriedstatus.CurrentValue = HtmlDecode(marriedstatus.CurrentValue);
					marriedstatus.EditValue = marriedstatus.CurrentValue; // DN
					marriedstatus.PlaceHolder = RemoveHtml(marriedstatus.Caption);

					// Id_dept
					Id_dept.EditAttrs["class"] = "form-control";
					Id_dept.EditValue = Id_dept.CurrentValue; // DN
					Id_dept.PlaceHolder = RemoveHtml(Id_dept.Caption);

					// Id_position
					Id_position.EditAttrs["class"] = "form-control";
					Id_position.EditValue = Id_position.CurrentValue; // DN
					Id_position.PlaceHolder = RemoveHtml(Id_position.Caption);

					// Id_race
					Id_race.EditAttrs["class"] = "form-control";
					Id_race.EditValue = Id_race.CurrentValue; // DN
					Id_race.PlaceHolder = RemoveHtml(Id_race.Caption);

					// photopath
					photopath.EditAttrs["class"] = "form-control";
					photopath.EditValue = photopath.CurrentValue; // DN
					photopath.PlaceHolder = RemoveHtml(photopath.Caption);

					// report_to
					report_to.EditAttrs["class"] = "form-control";
					if (!Security.IsAdmin && Security.IsLoggedIn) { // Non system admin
						if (SameString(Id.CurrentValue, CurrentUserID())) {
					report_to.EditValue = report_to.CurrentValue;
					report_to.EditValue = FormatNumber(report_to.EditValue, 0, -2, -2, -2);
						} else {
						}
					} else {
					report_to.EditValue = report_to.CurrentValue; // DN
					report_to.PlaceHolder = RemoveHtml(report_to.Caption);
					}

					// login_effectivedate
					login_effectivedate.EditAttrs["class"] = "form-control";
					login_effectivedate.EditValue = FormatDateTime(login_effectivedate.CurrentValue, 8); // DN
					login_effectivedate.PlaceHolder = RemoveHtml(login_effectivedate.Caption);

					// login_disableddate
					login_disableddate.EditAttrs["class"] = "form-control";
					login_disableddate.EditValue = FormatDateTime(login_disableddate.CurrentValue, 8); // DN
					login_disableddate.PlaceHolder = RemoveHtml(login_disableddate.Caption);

					// UserLevelId
					UserLevelId.EditAttrs["class"] = "form-control";
					if (!Security.CanAdmin) { // System admin
						UserLevelId.EditValue = Language.Phrase("PasswordMask");
					} else {
					curVal = Convert.ToString(UserLevelId.CurrentValue).Trim();
					if (curVal != "")
						UserLevelId.ViewValue = UserLevelId.LookupCacheOption(curVal);
					else
						UserLevelId.ViewValue = UserLevelId.Lookup != null && IsList(UserLevelId.Lookup.Options) ? curVal : null;
					if (UserLevelId.ViewValue != null) { // Load from cache
						UserLevelId.EditValue = UserLevelId.Lookup.Options.Values.ToList();
					} else { // Lookup from database
						if (curVal == "") {
							filterWrk = "0=1";
						} else {
							filterWrk = "[UserLevelID]" + SearchString("=", Convert.ToString(UserLevelId.CurrentValue), Config.DataTypeNumber, "");
						}
					sqlWrk = UserLevelId.Lookup.GetSql(true, filterWrk, null, this);
					rswrk = await Connection.GetRowsAsync(sqlWrk);
					UserLevelId.EditValue = rswrk;
					}
					}

					// _Username
					_Username.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						_Username.CurrentValue = HtmlDecode(_Username.CurrentValue);
					_Username.EditValue = _Username.CurrentValue; // DN
					_Username.PlaceHolder = RemoveHtml(_Username.Caption);

					// password
					password.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						password.CurrentValue = HtmlDecode(password.CurrentValue);
					password.EditValue = password.CurrentValue; // DN
					password.PlaceHolder = RemoveHtml(password.Caption);

					// active
					active.EditValue = active.Options(false);

					// Edit refer script
					// Id

					Id.HrefValue = "";

					// employeeid
					employeeid.HrefValue = "";

					// fname
					fname.HrefValue = "";

					// lname
					lname.HrefValue = "";

					// oldic
					oldic.HrefValue = "";

					// newic
					newic.HrefValue = "";

					// passport
					passport.HrefValue = "";

					// address1
					address1.HrefValue = "";

					// address2
					address2.HrefValue = "";

					// Id_city
					Id_city.HrefValue = "";

					// Id_state
					Id_state.HrefValue = "";

					// Id_country
					Id_country.HrefValue = "";

					// postcode
					postcode.HrefValue = "";

					// gender
					gender.HrefValue = "";

					// tel
					tel.HrefValue = "";

					// handphone
					handphone.HrefValue = "";

					// _email
					_email.HrefValue = "";

					// dob
					dob.HrefValue = "";

					// children
					children.HrefValue = "";

					// datejoin
					datejoin.HrefValue = "";

					// dateresign
					dateresign.HrefValue = "";

					// marriedstatus
					marriedstatus.HrefValue = "";

					// Id_dept
					Id_dept.HrefValue = "";

					// Id_position
					Id_position.HrefValue = "";

					// Id_race
					Id_race.HrefValue = "";

					// photopath
					photopath.HrefValue = "";

					// report_to
					report_to.HrefValue = "";

					// login_effectivedate
					login_effectivedate.HrefValue = "";

					// login_disableddate
					login_disableddate.HrefValue = "";

					// UserLevelId
					UserLevelId.HrefValue = "";

					// _Username
					_Username.HrefValue = "";

					// password
					password.HrefValue = "";

					// active
					active.HrefValue = "";
				}
				if (RowType == Config.RowTypeAdd || RowType == Config.RowTypeEdit || RowType == Config.RowTypeSearch) // Add/Edit/Search row
					SetupFieldTitles();

				// Call Row Rendered event
				if (RowType != Config.RowTypeAggregateInit)
					Row_Rendered();
			}

			#pragma warning restore 1998

			#pragma warning disable 1998

			// Validate form
			protected async Task<bool> ValidateForm() {

				// Initialize form error message
				FormError = "";

				// Check if validation required
				if (!Config.ServerValidate)
					return (FormError == "");
				if (Id.Required) {
					if (!Id.IsDetailKey && Empty(Id.FormValue))
						FormError = AddMessage(FormError, Id.RequiredErrorMessage.Replace("%s", Id.Caption));
				}
				if (employeeid.Required) {
					if (!employeeid.IsDetailKey && Empty(employeeid.FormValue))
						FormError = AddMessage(FormError, employeeid.RequiredErrorMessage.Replace("%s", employeeid.Caption));
				}
				if (fname.Required) {
					if (!fname.IsDetailKey && Empty(fname.FormValue))
						FormError = AddMessage(FormError, fname.RequiredErrorMessage.Replace("%s", fname.Caption));
				}
				if (lname.Required) {
					if (!lname.IsDetailKey && Empty(lname.FormValue))
						FormError = AddMessage(FormError, lname.RequiredErrorMessage.Replace("%s", lname.Caption));
				}
				if (oldic.Required) {
					if (!oldic.IsDetailKey && Empty(oldic.FormValue))
						FormError = AddMessage(FormError, oldic.RequiredErrorMessage.Replace("%s", oldic.Caption));
				}
				if (newic.Required) {
					if (!newic.IsDetailKey && Empty(newic.FormValue))
						FormError = AddMessage(FormError, newic.RequiredErrorMessage.Replace("%s", newic.Caption));
				}
				if (passport.Required) {
					if (!passport.IsDetailKey && Empty(passport.FormValue))
						FormError = AddMessage(FormError, passport.RequiredErrorMessage.Replace("%s", passport.Caption));
				}
				if (address1.Required) {
					if (!address1.IsDetailKey && Empty(address1.FormValue))
						FormError = AddMessage(FormError, address1.RequiredErrorMessage.Replace("%s", address1.Caption));
				}
				if (address2.Required) {
					if (!address2.IsDetailKey && Empty(address2.FormValue))
						FormError = AddMessage(FormError, address2.RequiredErrorMessage.Replace("%s", address2.Caption));
				}
				if (Id_city.Required) {
					if (!Id_city.IsDetailKey && Empty(Id_city.FormValue))
						FormError = AddMessage(FormError, Id_city.RequiredErrorMessage.Replace("%s", Id_city.Caption));
				}
				if (!CheckInteger(Id_city.FormValue))
					FormError = AddMessage(FormError, Id_city.ErrorMessage);
				if (Id_state.Required) {
					if (!Id_state.IsDetailKey && Empty(Id_state.FormValue))
						FormError = AddMessage(FormError, Id_state.RequiredErrorMessage.Replace("%s", Id_state.Caption));
				}
				if (!CheckInteger(Id_state.FormValue))
					FormError = AddMessage(FormError, Id_state.ErrorMessage);
				if (Id_country.Required) {
					if (!Id_country.IsDetailKey && Empty(Id_country.FormValue))
						FormError = AddMessage(FormError, Id_country.RequiredErrorMessage.Replace("%s", Id_country.Caption));
				}
				if (!CheckInteger(Id_country.FormValue))
					FormError = AddMessage(FormError, Id_country.ErrorMessage);
				if (postcode.Required) {
					if (!postcode.IsDetailKey && Empty(postcode.FormValue))
						FormError = AddMessage(FormError, postcode.RequiredErrorMessage.Replace("%s", postcode.Caption));
				}
				if (gender.Required) {
					if (!gender.IsDetailKey && Empty(gender.FormValue))
						FormError = AddMessage(FormError, gender.RequiredErrorMessage.Replace("%s", gender.Caption));
				}
				if (tel.Required) {
					if (!tel.IsDetailKey && Empty(tel.FormValue))
						FormError = AddMessage(FormError, tel.RequiredErrorMessage.Replace("%s", tel.Caption));
				}
				if (handphone.Required) {
					if (!handphone.IsDetailKey && Empty(handphone.FormValue))
						FormError = AddMessage(FormError, handphone.RequiredErrorMessage.Replace("%s", handphone.Caption));
				}
				if (_email.Required) {
					if (!_email.IsDetailKey && Empty(_email.FormValue))
						FormError = AddMessage(FormError, _email.RequiredErrorMessage.Replace("%s", _email.Caption));
				}
				if (dob.Required) {
					if (!dob.IsDetailKey && Empty(dob.FormValue))
						FormError = AddMessage(FormError, dob.RequiredErrorMessage.Replace("%s", dob.Caption));
				}
				if (!CheckDate(dob.FormValue))
					FormError = AddMessage(FormError, dob.ErrorMessage);
				if (children.Required) {
					if (!children.IsDetailKey && Empty(children.FormValue))
						FormError = AddMessage(FormError, children.RequiredErrorMessage.Replace("%s", children.Caption));
				}
				if (!CheckInteger(children.FormValue))
					FormError = AddMessage(FormError, children.ErrorMessage);
				if (datejoin.Required) {
					if (!datejoin.IsDetailKey && Empty(datejoin.FormValue))
						FormError = AddMessage(FormError, datejoin.RequiredErrorMessage.Replace("%s", datejoin.Caption));
				}
				if (!CheckDate(datejoin.FormValue))
					FormError = AddMessage(FormError, datejoin.ErrorMessage);
				if (dateresign.Required) {
					if (!dateresign.IsDetailKey && Empty(dateresign.FormValue))
						FormError = AddMessage(FormError, dateresign.RequiredErrorMessage.Replace("%s", dateresign.Caption));
				}
				if (!CheckDate(dateresign.FormValue))
					FormError = AddMessage(FormError, dateresign.ErrorMessage);
				if (marriedstatus.Required) {
					if (!marriedstatus.IsDetailKey && Empty(marriedstatus.FormValue))
						FormError = AddMessage(FormError, marriedstatus.RequiredErrorMessage.Replace("%s", marriedstatus.Caption));
				}
				if (Id_dept.Required) {
					if (!Id_dept.IsDetailKey && Empty(Id_dept.FormValue))
						FormError = AddMessage(FormError, Id_dept.RequiredErrorMessage.Replace("%s", Id_dept.Caption));
				}
				if (!CheckInteger(Id_dept.FormValue))
					FormError = AddMessage(FormError, Id_dept.ErrorMessage);
				if (Id_position.Required) {
					if (!Id_position.IsDetailKey && Empty(Id_position.FormValue))
						FormError = AddMessage(FormError, Id_position.RequiredErrorMessage.Replace("%s", Id_position.Caption));
				}
				if (!CheckInteger(Id_position.FormValue))
					FormError = AddMessage(FormError, Id_position.ErrorMessage);
				if (Id_race.Required) {
					if (!Id_race.IsDetailKey && Empty(Id_race.FormValue))
						FormError = AddMessage(FormError, Id_race.RequiredErrorMessage.Replace("%s", Id_race.Caption));
				}
				if (!CheckInteger(Id_race.FormValue))
					FormError = AddMessage(FormError, Id_race.ErrorMessage);
				if (photopath.Required) {
					if (!photopath.IsDetailKey && Empty(photopath.FormValue))
						FormError = AddMessage(FormError, photopath.RequiredErrorMessage.Replace("%s", photopath.Caption));
				}
				if (report_to.Required) {
					if (!report_to.IsDetailKey && Empty(report_to.FormValue))
						FormError = AddMessage(FormError, report_to.RequiredErrorMessage.Replace("%s", report_to.Caption));
				}
				if (!CheckInteger(report_to.FormValue))
					FormError = AddMessage(FormError, report_to.ErrorMessage);
				if (login_effectivedate.Required) {
					if (!login_effectivedate.IsDetailKey && Empty(login_effectivedate.FormValue))
						FormError = AddMessage(FormError, login_effectivedate.RequiredErrorMessage.Replace("%s", login_effectivedate.Caption));
				}
				if (!CheckDate(login_effectivedate.FormValue))
					FormError = AddMessage(FormError, login_effectivedate.ErrorMessage);
				if (login_disableddate.Required) {
					if (!login_disableddate.IsDetailKey && Empty(login_disableddate.FormValue))
						FormError = AddMessage(FormError, login_disableddate.RequiredErrorMessage.Replace("%s", login_disableddate.Caption));
				}
				if (!CheckDate(login_disableddate.FormValue))
					FormError = AddMessage(FormError, login_disableddate.ErrorMessage);
				if (UserLevelId.Required) {
					if (!UserLevelId.IsDetailKey && Empty(UserLevelId.FormValue))
						FormError = AddMessage(FormError, UserLevelId.RequiredErrorMessage.Replace("%s", UserLevelId.Caption));
				}
				if (_Username.Required) {
					if (!_Username.IsDetailKey && Empty(_Username.FormValue))
						FormError = AddMessage(FormError, _Username.RequiredErrorMessage.Replace("%s", _Username.Caption));
				}
				if (password.Required) {
					if (!password.IsDetailKey && Empty(password.FormValue))
						FormError = AddMessage(FormError, password.RequiredErrorMessage.Replace("%s", password.Caption));
				}
				if (active.Required) {
					if (Empty(active.FormValue))
						FormError = AddMessage(FormError, active.RequiredErrorMessage.Replace("%s", active.Caption));
				}

				// Return validate result
				bool valid = Empty(FormError);

				// Call Form_CustomValidate event
				string formCustomError = "";
				valid = valid && Form_CustomValidate(ref formCustomError);
				FormError = AddMessage(FormError, formCustomError);
				return valid;
			}

			#pragma warning restore 1998

			// Update record based on key values

			#pragma warning disable 168, 219

			protected async Task<JsonBoolResult> EditRow() { // DN
				bool result = false;
				Dictionary<string, object> rsold = null;
				var rsnew = new Dictionary<string, object>();
				var filter = GetRecordFilter();
				filter = ApplyUserIDFilters(filter);
				CurrentFilter = filter;
				string sql = CurrentSql;
				try {
					using (var rsedit = await Connection.GetDataReaderAsync(sql)) {
						if (rsedit == null || !await rsedit.ReadAsync()) {
							FailureMessage = Language.Phrase("NoRecord"); // Set no record message
							return JsonBoolResult.FalseResult;
						}
						rsold = Connection.GetRow(rsedit);
						LoadDbValues(rsold);
					}
				} catch (Exception e) {
					if (Config.Debug)
						throw;
					FailureMessage = e.Message;
					return JsonBoolResult.FalseResult;
				}

				// employeeid
				employeeid.SetDbValue(rsnew, employeeid.CurrentValue, "", employeeid.ReadOnly);

				// fname
				fname.SetDbValue(rsnew, fname.CurrentValue, "", fname.ReadOnly);

				// lname
				lname.SetDbValue(rsnew, lname.CurrentValue, System.DBNull.Value, lname.ReadOnly);

				// oldic
				oldic.SetDbValue(rsnew, oldic.CurrentValue, System.DBNull.Value, oldic.ReadOnly);

				// newic
				newic.SetDbValue(rsnew, newic.CurrentValue, "", newic.ReadOnly);

				// passport
				passport.SetDbValue(rsnew, passport.CurrentValue, System.DBNull.Value, passport.ReadOnly);

				// address1
				address1.SetDbValue(rsnew, address1.CurrentValue, System.DBNull.Value, address1.ReadOnly);

				// address2
				address2.SetDbValue(rsnew, address2.CurrentValue, System.DBNull.Value, address2.ReadOnly);

				// Id_city
				Id_city.SetDbValue(rsnew, Id_city.CurrentValue, System.DBNull.Value, Id_city.ReadOnly);

				// Id_state
				Id_state.SetDbValue(rsnew, Id_state.CurrentValue, System.DBNull.Value, Id_state.ReadOnly);

				// Id_country
				Id_country.SetDbValue(rsnew, Id_country.CurrentValue, System.DBNull.Value, Id_country.ReadOnly);

				// postcode
				postcode.SetDbValue(rsnew, postcode.CurrentValue, System.DBNull.Value, postcode.ReadOnly);

				// gender
				gender.SetDbValue(rsnew, gender.CurrentValue, "", gender.ReadOnly);

				// tel
				tel.SetDbValue(rsnew, tel.CurrentValue, System.DBNull.Value, tel.ReadOnly);

				// handphone
				handphone.SetDbValue(rsnew, handphone.CurrentValue, System.DBNull.Value, handphone.ReadOnly);

				// _email
				_email.SetDbValue(rsnew, _email.CurrentValue, System.DBNull.Value, _email.ReadOnly);

				// dob
				dob.SetDbValue(rsnew, UnformatDateTime(dob.CurrentValue, 0), DateTime.Now, dob.ReadOnly);

				// children
				children.SetDbValue(rsnew, children.CurrentValue, System.DBNull.Value, children.ReadOnly);

				// datejoin
				datejoin.SetDbValue(rsnew, UnformatDateTime(datejoin.CurrentValue, 0), DateTime.Now, datejoin.ReadOnly);

				// dateresign
				dateresign.SetDbValue(rsnew, UnformatDateTime(dateresign.CurrentValue, 0), System.DBNull.Value, dateresign.ReadOnly);

				// marriedstatus
				marriedstatus.SetDbValue(rsnew, marriedstatus.CurrentValue, System.DBNull.Value, marriedstatus.ReadOnly);

				// Id_dept
				Id_dept.SetDbValue(rsnew, Id_dept.CurrentValue, System.DBNull.Value, Id_dept.ReadOnly);

				// Id_position
				Id_position.SetDbValue(rsnew, Id_position.CurrentValue, System.DBNull.Value, Id_position.ReadOnly);

				// Id_race
				Id_race.SetDbValue(rsnew, Id_race.CurrentValue, System.DBNull.Value, Id_race.ReadOnly);

				// photopath
				photopath.SetDbValue(rsnew, photopath.CurrentValue, System.DBNull.Value, photopath.ReadOnly);

				// report_to
				report_to.SetDbValue(rsnew, report_to.CurrentValue, System.DBNull.Value, report_to.ReadOnly);

				// login_effectivedate
				login_effectivedate.SetDbValue(rsnew, UnformatDateTime(login_effectivedate.CurrentValue, 0), System.DBNull.Value, login_effectivedate.ReadOnly);

				// login_disableddate
				login_disableddate.SetDbValue(rsnew, UnformatDateTime(login_disableddate.CurrentValue, 0), System.DBNull.Value, login_disableddate.ReadOnly);

				// UserLevelId
				if (Security.CanAdmin) { // System admin
					UserLevelId.SetDbValue(rsnew, UserLevelId.CurrentValue, System.DBNull.Value, UserLevelId.ReadOnly);
				}

				// _Username
				_Username.SetDbValue(rsnew, _Username.CurrentValue, System.DBNull.Value, _Username.ReadOnly);

				// password
				password.SetDbValue(rsnew, password.CurrentValue, System.DBNull.Value, password.ReadOnly || (Config.EncryptedPassword && SameString(rsold["password"], password.CurrentValue)));

				// active
				active.SetDbValue(rsnew, ConvertToBool(active.CurrentValue, "1", "0"), System.DBNull.Value, active.ReadOnly); // DN1204

				// Call Row Updating event
				bool updateRow = Row_Updating(rsold, rsnew);
				if (updateRow) {
					try {
						if (rsnew.Count > 0)
							result = await UpdateAsync(rsnew, "", rsold) > 0;
						else
							result = true;
						if (result) {
						}
					} catch (Exception e) {
						if (Config.Debug)
							throw;
						FailureMessage = e.Message;
						return JsonBoolResult.FalseResult;
					}
				} else {
					if (!Empty(SuccessMessage) || !Empty(FailureMessage)) {

						// Use the message, do nothing
					} else if (!Empty(CancelMessage)) {
						FailureMessage = CancelMessage;
						CancelMessage = "";
					} else {
						FailureMessage = Language.Phrase("UpdateCancelled");
					}
					result = false;
				}

				// Call Row_Updated event
				if (result)
					Row_Updated(rsold, rsnew);

				// Write JSON for API request
				var d = new Dictionary<string, object>();
				d.Add("success", result);
				if (IsApi() && result) {
					var row = GetRecordFromDictionary(rsnew);
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
				string pageId = "edit";
				breadcrumb.Add("edit", pageId, url);
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

			// Form Custom Validate event
			public virtual bool Form_CustomValidate(ref string customError) {

				//Return error message in customError
				return true;
			}
		}
	}
}
