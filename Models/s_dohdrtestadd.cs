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
		/// s_dohdrtest_Add
		/// </summary>

		public static _s_dohdrtest_Add s_dohdrtest_Add {
			get => HttpData.Get<_s_dohdrtest_Add>("s_dohdrtest_Add");
			set => HttpData["s_dohdrtest_Add"] = value;
		}

		/// <summary>
		/// Page class for s_dohdrtest
		/// </summary>

		public class _s_dohdrtest_Add : _s_dohdrtest_AddBase
		{

			// Construtor
			public _s_dohdrtest_Add(Controller controller = null) : base(controller) {
			}
		}

		/// <summary>
		/// Page base class
		/// </summary>

		public class _s_dohdrtest_AddBase : _s_dohdrtest, IAspNetMakerPage
		{

			// Page ID
			public string PageID = "add";

			// Project ID
			public string ProjectID = "{8543F230-11C6-4105-B51C-8D87C21BE659}";

			// Table name
			public string TableName = "s_dohdrtest";

			// Page object name
			public string PageObjName = "s_dohdrtest_Add";

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
			public _s_dohdrtest_AddBase(Controller controller = null) { // DN
				if (controller != null)
					Controller = controller;

				// Initialize
				CurrentPage = this;

				// Language object
				Language = Language ?? new Lang();

				// Table object (s_dohdrtest)
				if (s_dohdrtest == null || s_dohdrtest is _s_dohdrtest)
					s_dohdrtest = this;

				// Table object (s_employee)
				s_employee = s_employee ?? new _s_employee();

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
					dynamic doc = CreateInstance(classname, new object[] { s_dohdrtest, "" }); // DN
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
								if (pageName == "s_dohdrtestview")
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
				key += UrlEncode(Convert.ToString(ar["TrxId"]));
				return key;
			}

			// Hide fields for Add/Edit
			protected void HideFieldsForAddEdit() {
				if (IsAdd || IsCopy || IsGridAdd)
					TrxId.Visible = false;
			}

			// Properties
			public string FormClassName = "ew-horizontal ew-form ew-add-form";
			public bool IsModal = false;
			public bool IsMobileOrModal = false;
			public string DbMasterFilter = "";
			public string DbDetailFilter = "";
			public int StartRecord;
			public DbDataReader OldRecordset = null;
			public DbDataReader Recordset = null; // Reserved // DN
			public bool CopyRecord;

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
					if (!Security.CanAdd) {
						Security.SaveLastUrl();
						FailureMessage = DeniedMessage(); // Set no permission
						if (IsApi())
							return new JsonBoolResult(new { success = false, error = DeniedMessage(), version = Config.ProductVersion }, false);
						if (Security.CanList)
							return Terminate(GetUrl("s_dohdrtestlist"));
						else
							return Terminate(GetUrl("login"));
					}
				}

				// Create form object
				CurrentForm = new HttpForm();
				CurrentAction = Param("action"); // Set up current action
				TrxId.Visible = false;
				dt_rec.SetVisibility();
				do_no.SetVisibility();
				dbcode.SetVisibility();
				slsman.SetVisibility();
				fileno.SetVisibility();
				TrxUserId.SetVisibility();
				CurrencyCode.SetVisibility();
				ex_rate.SetVisibility();
				do_amount_original.SetVisibility();
				do_amount_loca.SetVisibility();
				rounding_adj.SetVisibility();
				ar_gl_acct.SetVisibility();
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
				await SetupLookupOptions(dbcode);
				await SetupLookupOptions(CurrencyCode);

				// Check modal
				if (IsModal)
					SkipHeaderFooter = true;
				IsMobileOrModal = IsMobile() || IsModal;
				FormClassName = "ew-form ew-add-form ew-horizontal";
				bool postBack = false;

				// Set up current action
				if (IsApi()) {
					CurrentAction = "insert"; // Add record directly
					postBack = true;
				} else if (!Empty(Post("action"))) {
					CurrentAction = Post("action"); // Get form action
					postBack = true;
				} else { // Not post back

					// Load key from QueryString
					CopyRecord = true;
					string[] keyValues = null;
					object rv;
					if (IsApi() && RouteValues.TryGetValue("key", out object k))
						keyValues = k.ToString().Split('/');
					if (RouteValues.TryGetValue("TrxId", out rv)) { // DN
						TrxId.QueryValue = Convert.ToString(rv);
						SetKey("TrxId", TrxId.CurrentValue); // Set up key
					} else if (!Empty(Get("TrxId"))) {
						TrxId.QueryValue = Get("TrxId");
						SetKey("TrxId", TrxId.CurrentValue); // Set up key
					} else if (IsApi() && !Empty(keyValues)) {
						TrxId.QueryValue = Convert.ToString(keyValues[0]);
						SetKey("TrxId", TrxId.CurrentValue); // Set up key
					} else {
						SetKey("TrxId", ""); // Clear key
						CopyRecord = false;
					}
					if (CopyRecord) {
						CurrentAction = "copy"; // Copy record
					} else {
						CurrentAction = "show"; // Display blank record
					}
				}

				// Load old record / default values
				bool loaded = await LoadOldRecord();

				// Load form values
				if (postBack) {
					await LoadFormValues(); // Load form values
				}

				// Set up detail parameters
				SetupDetailParms();

				// Validate form if post back
				if (postBack) {
					if (!await ValidateForm()) {
						EventCancelled = true; // Event cancelled
						RestoreFormValues(); // Restore form values
						FailureMessage = FormError;
						if (IsApi())
							return Terminate();
						else
							CurrentAction = "show"; // Form error, reset action
					}
				}

				// Perform current action
				switch (CurrentAction) {
					case "copy": // Copy an existing record
						using (OldRecordset) {} // Dispose
						if (!loaded) { // Record not loaded
							if (Empty(FailureMessage))
								FailureMessage = Language.Phrase("NoRecord"); // No record found
							return Terminate("s_dohdrtestlist"); // No matching record, return to List page // DN
						}

						// Set up detail parameters
						SetupDetailParms();
						break;
					case "insert": // Add new record // DN
						SendEmail = true; // Send email on add success
						var rsold = Connection.GetRow(OldRecordset);
						using (OldRecordset) {} // Dispose
						var res = await AddRow(rsold);
						if (res) { // Add successful
							if (Empty(SuccessMessage))
								SuccessMessage = Language.Phrase("AddSuccess"); // Set up success message
							string returnUrl = "";
							if (!Empty(CurrentDetailTable)) // Master/detail add
								returnUrl = DetailUrl;
							else
								returnUrl = ReturnUrl;
							if (GetPageName(returnUrl) == "s_dohdrtestlist")
								returnUrl = AddMasterUrl(ListUrl); // List page, return to List page with correct master key if necessary
							else if (GetPageName(returnUrl) == "s_dohdrtestview")
								returnUrl = ViewUrl; // View page, return to View page with key URL directly
							if (IsApi()) // Return to caller
								return res;
							else
								return Terminate(returnUrl);
						} else if (IsApi()) { // API request, return
							return Terminate();
						} else {
							EventCancelled = true; // Event cancelled
							RestoreFormValues(); // Add failed, restore form values

							// Set up detail parameters
							SetupDetailParms();
						}
						break;
				}

				// Set up Breadcrumb
				SetupBreadcrumb();

				// Render row based on row type
				RowType = Config.RowTypeAdd; // Render add type

				// Render row
				ResetAttributes();
				await RenderRow();
				return PageResult();
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

			// Load default values
			protected void LoadDefaultValues() {
				TrxId.CurrentValue = System.DBNull.Value;
				TrxId.OldValue = TrxId.CurrentValue;
				dt_rec.CurrentValue = System.DBNull.Value;
				dt_rec.OldValue = dt_rec.CurrentValue;
				do_no.CurrentValue = System.DBNull.Value;
				do_no.OldValue = do_no.CurrentValue;
				dbcode.CurrentValue = System.DBNull.Value;
				dbcode.OldValue = dbcode.CurrentValue;
				slsman.CurrentValue = System.DBNull.Value;
				slsman.OldValue = slsman.CurrentValue;
				fileno.CurrentValue = System.DBNull.Value;
				fileno.OldValue = fileno.CurrentValue;
				TrxUserId.CurrentValue = System.DBNull.Value;
				TrxUserId.OldValue = TrxUserId.CurrentValue;
				CurrencyCode.CurrentValue = System.DBNull.Value;
				CurrencyCode.OldValue = CurrencyCode.CurrentValue;
				ex_rate.CurrentValue = System.DBNull.Value;
				ex_rate.OldValue = ex_rate.CurrentValue;
				do_amount_original.CurrentValue = System.DBNull.Value;
				do_amount_original.OldValue = do_amount_original.CurrentValue;
				do_amount_loca.CurrentValue = System.DBNull.Value;
				do_amount_loca.OldValue = do_amount_loca.CurrentValue;
				rounding_adj.CurrentValue = System.DBNull.Value;
				rounding_adj.OldValue = rounding_adj.CurrentValue;
				ar_gl_acct.CurrentValue = System.DBNull.Value;
				ar_gl_acct.OldValue = ar_gl_acct.CurrentValue;
			}

			#pragma warning disable 1998

			// Load form values
			protected async Task LoadFormValues() {
				string val;

				// Check field name 'dt_rec' first before field var 'x_dt_rec'
				val = CurrentForm.GetValue("dt_rec", "x_dt_rec");
				if (!dt_rec.IsDetailKey) {
					if (IsApi() && val == null)
						dt_rec.Visible = false; // Disable update for API request
					else
						dt_rec.FormValue = val;
					dt_rec.CurrentValue = UnformatDateTime(dt_rec.CurrentValue, 0);
				}

				// Check field name 'do_no' first before field var 'x_do_no'
				val = CurrentForm.GetValue("do_no", "x_do_no");
				if (!do_no.IsDetailKey) {
					if (IsApi() && val == null)
						do_no.Visible = false; // Disable update for API request
					else
						do_no.FormValue = val;
				}

				// Check field name 'dbcode' first before field var 'x_dbcode'
				val = CurrentForm.GetValue("dbcode", "x_dbcode");
				if (!dbcode.IsDetailKey) {
					if (IsApi() && val == null)
						dbcode.Visible = false; // Disable update for API request
					else
						dbcode.FormValue = val;
				}

				// Check field name 'slsman' first before field var 'x_slsman'
				val = CurrentForm.GetValue("slsman", "x_slsman");
				if (!slsman.IsDetailKey) {
					if (IsApi() && val == null)
						slsman.Visible = false; // Disable update for API request
					else
						slsman.FormValue = val;
				}

				// Check field name 'fileno' first before field var 'x_fileno'
				val = CurrentForm.GetValue("fileno", "x_fileno");
				if (!fileno.IsDetailKey) {
					if (IsApi() && val == null)
						fileno.Visible = false; // Disable update for API request
					else
						fileno.FormValue = val;
				}

				// Check field name 'TrxUserId' first before field var 'x_TrxUserId'
				val = CurrentForm.GetValue("TrxUserId", "x_TrxUserId");
				if (!TrxUserId.IsDetailKey) {
					if (IsApi() && val == null)
						TrxUserId.Visible = false; // Disable update for API request
					else
						TrxUserId.FormValue = val;
				}

				// Check field name 'CurrencyCode' first before field var 'x_CurrencyCode'
				val = CurrentForm.GetValue("CurrencyCode", "x_CurrencyCode");
				if (!CurrencyCode.IsDetailKey) {
					if (IsApi() && val == null)
						CurrencyCode.Visible = false; // Disable update for API request
					else
						CurrencyCode.FormValue = val;
				}

				// Check field name 'ex_rate' first before field var 'x_ex_rate'
				val = CurrentForm.GetValue("ex_rate", "x_ex_rate");
				if (!ex_rate.IsDetailKey) {
					if (IsApi() && val == null)
						ex_rate.Visible = false; // Disable update for API request
					else
						ex_rate.FormValue = val;
				}

				// Check field name 'do_amount_original' first before field var 'x_do_amount_original'
				val = CurrentForm.GetValue("do_amount_original", "x_do_amount_original");
				if (!do_amount_original.IsDetailKey) {
					if (IsApi() && val == null)
						do_amount_original.Visible = false; // Disable update for API request
					else
						do_amount_original.FormValue = val;
				}

				// Check field name 'do_amount_loca' first before field var 'x_do_amount_loca'
				val = CurrentForm.GetValue("do_amount_loca", "x_do_amount_loca");
				if (!do_amount_loca.IsDetailKey) {
					if (IsApi() && val == null)
						do_amount_loca.Visible = false; // Disable update for API request
					else
						do_amount_loca.FormValue = val;
				}

				// Check field name 'rounding_adj' first before field var 'x_rounding_adj'
				val = CurrentForm.GetValue("rounding_adj", "x_rounding_adj");
				if (!rounding_adj.IsDetailKey) {
					if (IsApi() && val == null)
						rounding_adj.Visible = false; // Disable update for API request
					else
						rounding_adj.FormValue = val;
				}

				// Check field name 'ar_gl_acct' first before field var 'x_ar_gl_acct'
				val = CurrentForm.GetValue("ar_gl_acct", "x_ar_gl_acct");
				if (!ar_gl_acct.IsDetailKey) {
					if (IsApi() && val == null)
						ar_gl_acct.Visible = false; // Disable update for API request
					else
						ar_gl_acct.FormValue = val;
				}

				// Check field name 'TrxId' first before field var 'x_TrxId'
				val = CurrentForm.GetValue("TrxId", "x_TrxId");
			}

			#pragma warning restore 1998

			// Restore form values
			public void RestoreFormValues() {
				dt_rec.CurrentValue = dt_rec.FormValue;
				dt_rec.CurrentValue = UnformatDateTime(dt_rec.CurrentValue, 0);
				do_no.CurrentValue = do_no.FormValue;
				dbcode.CurrentValue = dbcode.FormValue;
				slsman.CurrentValue = slsman.FormValue;
				fileno.CurrentValue = fileno.FormValue;
				TrxUserId.CurrentValue = TrxUserId.FormValue;
				CurrencyCode.CurrentValue = CurrencyCode.FormValue;
				ex_rate.CurrentValue = ex_rate.FormValue;
				do_amount_original.CurrentValue = do_amount_original.FormValue;
				do_amount_loca.CurrentValue = do_amount_loca.FormValue;
				rounding_adj.CurrentValue = rounding_adj.FormValue;
				ar_gl_acct.CurrentValue = ar_gl_acct.FormValue;
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
				TrxId.SetDbValue(row["TrxId"]);
				dt_rec.SetDbValue(row["dt_rec"]);
				do_no.SetDbValue(row["do_no"]);
				dbcode.SetDbValue(row["dbcode"]);
				slsman.SetDbValue(row["slsman"]);
				fileno.SetDbValue(row["fileno"]);
				TrxUserId.SetDbValue(row["TrxUserId"]);
				CurrencyCode.SetDbValue(row["CurrencyCode"]);
				ex_rate.SetDbValue(row["ex_rate"]);
				do_amount_original.SetDbValue(row["do_amount_original"]);
				do_amount_loca.SetDbValue(row["do_amount_loca"]);
				rounding_adj.SetDbValue(row["rounding_adj"]);
				ar_gl_acct.SetDbValue(row["ar_gl_acct"]);
			}

			#pragma warning restore 162, 168, 1998

			// Return a row with default values
			protected Dictionary<string, object> NewRow() {
				LoadDefaultValues();
				var row = new Dictionary<string, object>();
				row.Add("TrxId", TrxId.CurrentValue);
				row.Add("dt_rec", dt_rec.CurrentValue);
				row.Add("do_no", do_no.CurrentValue);
				row.Add("dbcode", dbcode.CurrentValue);
				row.Add("slsman", slsman.CurrentValue);
				row.Add("fileno", fileno.CurrentValue);
				row.Add("TrxUserId", TrxUserId.CurrentValue);
				row.Add("CurrencyCode", CurrencyCode.CurrentValue);
				row.Add("ex_rate", ex_rate.CurrentValue);
				row.Add("do_amount_original", do_amount_original.CurrentValue);
				row.Add("do_amount_loca", do_amount_loca.CurrentValue);
				row.Add("rounding_adj", rounding_adj.CurrentValue);
				row.Add("ar_gl_acct", ar_gl_acct.CurrentValue);
				return row;
			}

			#pragma warning disable 618, 1998

			// Load old record
			protected async Task<bool> LoadOldRecord(DatabaseConnectionBase<SqlConnection, SqlCommand, SqlDataReader, SqlDbType> cnn = null) {
				bool validKey = true;
				if (!Empty(GetKey("TrxId")))
					TrxId.CurrentValue = GetKey("TrxId"); // TrxId
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

				// Convert decimal values if posted back
				if (SameString(ex_rate.FormValue, ex_rate.CurrentValue) && IsNumeric(ConvertToFloatString(ex_rate.CurrentValue)))
					ex_rate.CurrentValue = ConvertToFloatString(ex_rate.CurrentValue);

				// Convert decimal values if posted back
				if (SameString(do_amount_original.FormValue, do_amount_original.CurrentValue) && IsNumeric(ConvertToFloatString(do_amount_original.CurrentValue)))
					do_amount_original.CurrentValue = ConvertToFloatString(do_amount_original.CurrentValue);

				// Convert decimal values if posted back
				if (SameString(do_amount_loca.FormValue, do_amount_loca.CurrentValue) && IsNumeric(ConvertToFloatString(do_amount_loca.CurrentValue)))
					do_amount_loca.CurrentValue = ConvertToFloatString(do_amount_loca.CurrentValue);

				// Convert decimal values if posted back
				if (SameString(rounding_adj.FormValue, rounding_adj.CurrentValue) && IsNumeric(ConvertToFloatString(rounding_adj.CurrentValue)))
					rounding_adj.CurrentValue = ConvertToFloatString(rounding_adj.CurrentValue);

				// Call Row_Rendering event
				Row_Rendering();

				// Common render codes for all row types
				// TrxId
				// dt_rec
				// do_no
				// dbcode
				// slsman
				// fileno
				// TrxUserId
				// CurrencyCode
				// ex_rate
				// do_amount_original
				// do_amount_loca
				// rounding_adj
				// ar_gl_acct

				if (RowType == Config.RowTypeView) { // View row

					// TrxId
					TrxId.ViewValue = TrxId.CurrentValue;

					// dt_rec
					dt_rec.ViewValue = dt_rec.CurrentValue;
					dt_rec.ViewValue = FormatDateTime(dt_rec.ViewValue, 0);

					// do_no
					do_no.ViewValue = do_no.CurrentValue;

					// dbcode
					dbcode.ViewValue = dbcode.CurrentValue;
					curVal = Convert.ToString(dbcode.CurrentValue);
					if (!Empty(curVal)) {
						dbcode.ViewValue = dbcode.LookupCacheOption(curVal);
						if (dbcode.ViewValue == null) { // Lookup from database
							filterWrk = "[dbcode]" + SearchString("=", curVal.Trim(), Config.DataTypeString, "");
							sqlWrk = dbcode.Lookup.GetSql(false, filterWrk, null, this);
							rswrk = await Connection.GetRowsAsync(sqlWrk);
							if (rswrk != null && rswrk.Count > 0) { // Lookup values found
								var listwrk = rswrk[0].Values.ToList();
								listwrk[1] = Convert.ToString(listwrk[1]);
								listwrk[2] = Convert.ToString(listwrk[2]);
								dbcode.ViewValue = dbcode.DisplayValue(listwrk);
							} else {
								dbcode.ViewValue = dbcode.CurrentValue;
							}
						}
					} else {
						dbcode.ViewValue = System.DBNull.Value;
					}

					// slsman
					slsman.ViewValue = slsman.CurrentValue;

					// fileno
					fileno.ViewValue = fileno.CurrentValue;

					// TrxUserId
					TrxUserId.ViewValue = TrxUserId.CurrentValue;
					TrxUserId.ViewValue = FormatNumber(TrxUserId.ViewValue, 0, -2, -2, -2);

					// CurrencyCode
					CurrencyCode.ViewValue = CurrencyCode.CurrentValue;
					curVal = Convert.ToString(CurrencyCode.CurrentValue);
					if (!Empty(curVal)) {
						CurrencyCode.ViewValue = CurrencyCode.LookupCacheOption(curVal);
						if (CurrencyCode.ViewValue == null) { // Lookup from database
							filterWrk = "[CurrencyCode]" + SearchString("=", curVal.Trim(), Config.DataTypeString, "");
							sqlWrk = CurrencyCode.Lookup.GetSql(false, filterWrk, null, this);
							rswrk = await Connection.GetRowsAsync(sqlWrk);
							if (rswrk != null && rswrk.Count > 0) { // Lookup values found
								var listwrk = rswrk[0].Values.ToList();
								listwrk[1] = Convert.ToString(listwrk[1]);
								CurrencyCode.ViewValue = CurrencyCode.DisplayValue(listwrk);
							} else {
								CurrencyCode.ViewValue = CurrencyCode.CurrentValue;
							}
						}
					} else {
						CurrencyCode.ViewValue = System.DBNull.Value;
					}

					// ex_rate
					ex_rate.ViewValue = ex_rate.CurrentValue;
					ex_rate.ViewValue = FormatNumber(ex_rate.ViewValue, 2, -2, -2, -2);

					// do_amount_original
					do_amount_original.ViewValue = do_amount_original.CurrentValue;
					do_amount_original.ViewValue = FormatNumber(do_amount_original.ViewValue, 2, -2, -2, -2);

					// do_amount_loca
					do_amount_loca.ViewValue = do_amount_loca.CurrentValue;
					do_amount_loca.ViewValue = FormatNumber(do_amount_loca.ViewValue, 2, -2, -2, -2);

					// rounding_adj
					rounding_adj.ViewValue = rounding_adj.CurrentValue;
					rounding_adj.ViewValue = FormatNumber(rounding_adj.ViewValue, 2, -2, -2, -2);

					// ar_gl_acct
					ar_gl_acct.ViewValue = ar_gl_acct.CurrentValue;

					// dt_rec
					dt_rec.HrefValue = "";
					dt_rec.TooltipValue = "";

					// do_no
					do_no.HrefValue = "";
					do_no.TooltipValue = "";

					// dbcode
					dbcode.HrefValue = "";
					dbcode.TooltipValue = "";

					// slsman
					slsman.HrefValue = "";
					slsman.TooltipValue = "";

					// fileno
					fileno.HrefValue = "";
					fileno.TooltipValue = "";

					// TrxUserId
					TrxUserId.HrefValue = "";
					TrxUserId.TooltipValue = "";

					// CurrencyCode
					CurrencyCode.HrefValue = "";
					CurrencyCode.TooltipValue = "";

					// ex_rate
					ex_rate.HrefValue = "";
					ex_rate.TooltipValue = "";

					// do_amount_original
					do_amount_original.HrefValue = "";
					do_amount_original.TooltipValue = "";

					// do_amount_loca
					do_amount_loca.HrefValue = "";
					do_amount_loca.TooltipValue = "";

					// rounding_adj
					rounding_adj.HrefValue = "";
					rounding_adj.TooltipValue = "";

					// ar_gl_acct
					ar_gl_acct.HrefValue = "";
					ar_gl_acct.TooltipValue = "";
				} else if (RowType == Config.RowTypeAdd) { // Add row

					// dt_rec
					dt_rec.EditAttrs["class"] = "form-control";
					dt_rec.EditValue = FormatDateTime(dt_rec.CurrentValue, 8); // DN
					dt_rec.PlaceHolder = RemoveHtml(dt_rec.Caption);

					// do_no
					do_no.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						do_no.CurrentValue = HtmlDecode(do_no.CurrentValue);
					do_no.EditValue = do_no.CurrentValue; // DN
					do_no.PlaceHolder = RemoveHtml(do_no.Caption);

					// dbcode
					dbcode.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						dbcode.CurrentValue = HtmlDecode(dbcode.CurrentValue);
					dbcode.EditValue = dbcode.CurrentValue; // DN
					curVal = Convert.ToString(dbcode.CurrentValue);
					if (!Empty(curVal)) {
						dbcode.EditValue = dbcode.LookupCacheOption(curVal);
						if (dbcode.EditValue == null) { // Lookup from database
							filterWrk = "[dbcode]" + SearchString("=", curVal.Trim(), Config.DataTypeString, "");
							sqlWrk = dbcode.Lookup.GetSql(false, filterWrk, null, this);
							rswrk = await Connection.GetRowsAsync(sqlWrk);
							if (rswrk != null && rswrk.Count > 0) { // Lookup values found
								var listwrk = rswrk[0].Values.ToList();
								listwrk[1] = Convert.ToString(HtmlEncode(listwrk[1]));
								listwrk[2] = Convert.ToString(HtmlEncode(listwrk[2]));
								dbcode.EditValue = dbcode.DisplayValue(listwrk);
							} else {
								dbcode.EditValue = HtmlEncode(dbcode.CurrentValue);
							}
						}
					} else {
						dbcode.EditValue = System.DBNull.Value;
					}
					dbcode.PlaceHolder = RemoveHtml(dbcode.Caption);

					// slsman
					slsman.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						slsman.CurrentValue = HtmlDecode(slsman.CurrentValue);
					slsman.EditValue = slsman.CurrentValue; // DN
					slsman.PlaceHolder = RemoveHtml(slsman.Caption);

					// fileno
					fileno.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						fileno.CurrentValue = HtmlDecode(fileno.CurrentValue);
					fileno.EditValue = fileno.CurrentValue; // DN
					fileno.PlaceHolder = RemoveHtml(fileno.Caption);

					// TrxUserId
					TrxUserId.EditAttrs["class"] = "form-control";
					TrxUserId.EditValue = TrxUserId.CurrentValue; // DN
					TrxUserId.PlaceHolder = RemoveHtml(TrxUserId.Caption);

					// CurrencyCode
					CurrencyCode.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						CurrencyCode.CurrentValue = HtmlDecode(CurrencyCode.CurrentValue);
					CurrencyCode.EditValue = CurrencyCode.CurrentValue; // DN
					curVal = Convert.ToString(CurrencyCode.CurrentValue);
					if (!Empty(curVal)) {
						CurrencyCode.EditValue = CurrencyCode.LookupCacheOption(curVal);
						if (CurrencyCode.EditValue == null) { // Lookup from database
							filterWrk = "[CurrencyCode]" + SearchString("=", curVal.Trim(), Config.DataTypeString, "");
							sqlWrk = CurrencyCode.Lookup.GetSql(false, filterWrk, null, this);
							rswrk = await Connection.GetRowsAsync(sqlWrk);
							if (rswrk != null && rswrk.Count > 0) { // Lookup values found
								var listwrk = rswrk[0].Values.ToList();
								listwrk[1] = Convert.ToString(HtmlEncode(listwrk[1]));
								CurrencyCode.EditValue = CurrencyCode.DisplayValue(listwrk);
							} else {
								CurrencyCode.EditValue = HtmlEncode(CurrencyCode.CurrentValue);
							}
						}
					} else {
						CurrencyCode.EditValue = System.DBNull.Value;
					}
					CurrencyCode.PlaceHolder = RemoveHtml(CurrencyCode.Caption);

					// ex_rate
					ex_rate.EditAttrs["class"] = "form-control";
					ex_rate.EditValue = ex_rate.CurrentValue; // DN
					ex_rate.PlaceHolder = RemoveHtml(ex_rate.Caption);
					if (!Empty(ex_rate.EditValue) && IsNumeric(ex_rate.EditValue))
						ex_rate.EditValue = FormatNumber(ex_rate.EditValue, -2, -2, -2, -2);

					// do_amount_original
					do_amount_original.EditAttrs["class"] = "form-control";
					do_amount_original.EditValue = do_amount_original.CurrentValue; // DN
					do_amount_original.PlaceHolder = RemoveHtml(do_amount_original.Caption);
					if (!Empty(do_amount_original.EditValue) && IsNumeric(do_amount_original.EditValue))
						do_amount_original.EditValue = FormatNumber(do_amount_original.EditValue, -2, -2, -2, -2);

					// do_amount_loca
					do_amount_loca.EditAttrs["class"] = "form-control";
					do_amount_loca.EditValue = do_amount_loca.CurrentValue; // DN
					do_amount_loca.PlaceHolder = RemoveHtml(do_amount_loca.Caption);
					if (!Empty(do_amount_loca.EditValue) && IsNumeric(do_amount_loca.EditValue))
						do_amount_loca.EditValue = FormatNumber(do_amount_loca.EditValue, -2, -2, -2, -2);

					// rounding_adj
					rounding_adj.EditAttrs["class"] = "form-control";
					rounding_adj.EditValue = rounding_adj.CurrentValue; // DN
					rounding_adj.PlaceHolder = RemoveHtml(rounding_adj.Caption);
					if (!Empty(rounding_adj.EditValue) && IsNumeric(rounding_adj.EditValue))
						rounding_adj.EditValue = FormatNumber(rounding_adj.EditValue, -2, -2, -2, -2);

					// ar_gl_acct
					ar_gl_acct.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						ar_gl_acct.CurrentValue = HtmlDecode(ar_gl_acct.CurrentValue);
					ar_gl_acct.EditValue = ar_gl_acct.CurrentValue; // DN
					ar_gl_acct.PlaceHolder = RemoveHtml(ar_gl_acct.Caption);

					// Add refer script
					// dt_rec

					dt_rec.HrefValue = "";

					// do_no
					do_no.HrefValue = "";

					// dbcode
					dbcode.HrefValue = "";

					// slsman
					slsman.HrefValue = "";

					// fileno
					fileno.HrefValue = "";

					// TrxUserId
					TrxUserId.HrefValue = "";

					// CurrencyCode
					CurrencyCode.HrefValue = "";

					// ex_rate
					ex_rate.HrefValue = "";

					// do_amount_original
					do_amount_original.HrefValue = "";

					// do_amount_loca
					do_amount_loca.HrefValue = "";

					// rounding_adj
					rounding_adj.HrefValue = "";

					// ar_gl_acct
					ar_gl_acct.HrefValue = "";
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
				if (TrxId.Required) {
					if (!TrxId.IsDetailKey && Empty(TrxId.FormValue))
						FormError = AddMessage(FormError, TrxId.RequiredErrorMessage.Replace("%s", TrxId.Caption));
				}
				if (dt_rec.Required) {
					if (!dt_rec.IsDetailKey && Empty(dt_rec.FormValue))
						FormError = AddMessage(FormError, dt_rec.RequiredErrorMessage.Replace("%s", dt_rec.Caption));
				}
				if (!CheckDate(dt_rec.FormValue))
					FormError = AddMessage(FormError, dt_rec.ErrorMessage);
				if (do_no.Required) {
					if (!do_no.IsDetailKey && Empty(do_no.FormValue))
						FormError = AddMessage(FormError, do_no.RequiredErrorMessage.Replace("%s", do_no.Caption));
				}
				if (dbcode.Required) {
					if (!dbcode.IsDetailKey && Empty(dbcode.FormValue))
						FormError = AddMessage(FormError, dbcode.RequiredErrorMessage.Replace("%s", dbcode.Caption));
				}
				if (slsman.Required) {
					if (!slsman.IsDetailKey && Empty(slsman.FormValue))
						FormError = AddMessage(FormError, slsman.RequiredErrorMessage.Replace("%s", slsman.Caption));
				}
				if (fileno.Required) {
					if (!fileno.IsDetailKey && Empty(fileno.FormValue))
						FormError = AddMessage(FormError, fileno.RequiredErrorMessage.Replace("%s", fileno.Caption));
				}
				if (TrxUserId.Required) {
					if (!TrxUserId.IsDetailKey && Empty(TrxUserId.FormValue))
						FormError = AddMessage(FormError, TrxUserId.RequiredErrorMessage.Replace("%s", TrxUserId.Caption));
				}
				if (!CheckInteger(TrxUserId.FormValue))
					FormError = AddMessage(FormError, TrxUserId.ErrorMessage);
				if (CurrencyCode.Required) {
					if (!CurrencyCode.IsDetailKey && Empty(CurrencyCode.FormValue))
						FormError = AddMessage(FormError, CurrencyCode.RequiredErrorMessage.Replace("%s", CurrencyCode.Caption));
				}
				if (ex_rate.Required) {
					if (!ex_rate.IsDetailKey && Empty(ex_rate.FormValue))
						FormError = AddMessage(FormError, ex_rate.RequiredErrorMessage.Replace("%s", ex_rate.Caption));
				}
				if (!CheckNumber(ex_rate.FormValue))
					FormError = AddMessage(FormError, ex_rate.ErrorMessage);
				if (do_amount_original.Required) {
					if (!do_amount_original.IsDetailKey && Empty(do_amount_original.FormValue))
						FormError = AddMessage(FormError, do_amount_original.RequiredErrorMessage.Replace("%s", do_amount_original.Caption));
				}
				if (!CheckNumber(do_amount_original.FormValue))
					FormError = AddMessage(FormError, do_amount_original.ErrorMessage);
				if (do_amount_loca.Required) {
					if (!do_amount_loca.IsDetailKey && Empty(do_amount_loca.FormValue))
						FormError = AddMessage(FormError, do_amount_loca.RequiredErrorMessage.Replace("%s", do_amount_loca.Caption));
				}
				if (!CheckNumber(do_amount_loca.FormValue))
					FormError = AddMessage(FormError, do_amount_loca.ErrorMessage);
				if (rounding_adj.Required) {
					if (!rounding_adj.IsDetailKey && Empty(rounding_adj.FormValue))
						FormError = AddMessage(FormError, rounding_adj.RequiredErrorMessage.Replace("%s", rounding_adj.Caption));
				}
				if (!CheckNumber(rounding_adj.FormValue))
					FormError = AddMessage(FormError, rounding_adj.ErrorMessage);
				if (ar_gl_acct.Required) {
					if (!ar_gl_acct.IsDetailKey && Empty(ar_gl_acct.FormValue))
						FormError = AddMessage(FormError, ar_gl_acct.RequiredErrorMessage.Replace("%s", ar_gl_acct.Caption));
				}

				// Validate detail grid
				var detailTblVar = CurrentDetailTables;
				if (detailTblVar.Contains("s_dodetltest") && s_dodetltest.DetailAdd) {
					s_dodetltest_Grid = s_dodetltest_Grid ?? new _s_dodetltest_Grid(); // Get detail page object
					await s_dodetltest_Grid.ValidateGridForm();
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

			// Save data to memory cache
			public void SetCache<T>(string key, T value, int span) => Cache.Set<T>(key, value, new MemoryCacheEntryOptions()
				.SetSlidingExpiration(TimeSpan.FromMilliseconds(span))); // Keep in cache for this time, reset time if accessed

			// Gete data from memory cache
			public void GetCache<T>(string key) => Cache.Get<T>(key);

			// Add record

			#pragma warning disable 168, 219

			protected async Task<JsonBoolResult> AddRow(Dictionary<string, object> rsold = null) { // DN
				bool result = false;
				var rsnew = new Dictionary<string, object>();

				// Begin transaction
				if (!Empty(CurrentDetailTable))
					Connection.BeginTrans();

				// Load db values from rsold
				LoadDbValues(rsold);
				if (rsold != null) {
				}
				try {

					// dt_rec
					dt_rec.SetDbValue(rsnew, UnformatDateTime(dt_rec.CurrentValue, 0), DateTime.Now, false);

					// do_no
					do_no.SetDbValue(rsnew, do_no.CurrentValue, "", false);

					// dbcode
					dbcode.SetDbValue(rsnew, dbcode.CurrentValue, "", false);

					// slsman
					slsman.SetDbValue(rsnew, slsman.CurrentValue, "", false);

					// fileno
					fileno.SetDbValue(rsnew, fileno.CurrentValue, System.DBNull.Value, false);

					// TrxUserId
					TrxUserId.SetDbValue(rsnew, TrxUserId.CurrentValue, 0, false);

					// CurrencyCode
					CurrencyCode.SetDbValue(rsnew, CurrencyCode.CurrentValue, System.DBNull.Value, false);

					// ex_rate
					ex_rate.SetDbValue(rsnew, ex_rate.CurrentValue, System.DBNull.Value, false);

					// do_amount_original
					do_amount_original.SetDbValue(rsnew, do_amount_original.CurrentValue, System.DBNull.Value, false);

					// do_amount_loca
					do_amount_loca.SetDbValue(rsnew, do_amount_loca.CurrentValue, System.DBNull.Value, false);

					// rounding_adj
					rounding_adj.SetDbValue(rsnew, rounding_adj.CurrentValue, System.DBNull.Value, false);

					// ar_gl_acct
					ar_gl_acct.SetDbValue(rsnew, ar_gl_acct.CurrentValue, System.DBNull.Value, false);
				} catch (Exception e) {
					if (Config.Debug)
						throw;
					FailureMessage = e.Message;
					return JsonBoolResult.FalseResult;
				}

				// Call Row Inserting event
				bool insertRow = Row_Inserting(rsold, rsnew);
				if (insertRow) {
					try {
						await InsertAsync(rsnew);
						result = true;
					} catch (Exception e) {
						if (Config.Debug)
							throw;
						FailureMessage = e.Message;
						result = false;
					}
				} else {
					if (SuccessMessage != "" || FailureMessage != "") {

						// Use the message, do nothing
					} else if (CancelMessage != "") {
						FailureMessage = CancelMessage;
						CancelMessage = "";
					} else {
						FailureMessage = Language.Phrase("InsertCancelled");
					}
					result = false;
				}

				// Add detail records
				var detailTblVar = CurrentDetailTables;
				if (result) {
					if (detailTblVar.Contains("s_dodetltest") && s_dodetltest.DetailAdd) {
						s_dodetltest.Id_dohdrTrxId.SessionValue = Convert.ToString(TrxId.CurrentValue); // Set master key
						s_dodetltest_Grid = s_dodetltest_Grid ?? new _s_dodetltest_Grid(); // Get detail page object
						Security.LoadCurrentUserLevel(ProjectID + "s_dodetltest"); // Load user level of detail table
						result = await s_dodetltest_Grid.GridInsert();
						Security.LoadCurrentUserLevel(ProjectID + TableName); // Restore user level of master table
					}
				}

				// Commit/Rollback transaction
				if (!Empty(CurrentDetailTable)) {
					if (result) {
						Connection.CommitTrans(); // Commit transaction
					} else {
						Connection.RollbackTrans(); // Rollback transaction
					}
				}

				// Call Row Inserted event
				if (result)
					Row_Inserted(rsold, rsnew);

				// Write JSON for API request
				var d = new Dictionary<string, object>();
				d.Add("success", result);
				if (IsApi() && result) {
					var row = GetRecordFromDictionary(rsnew);
					d.Add(TableVar, row);
					d.Add("version", Config.ProductVersion);
					return new JsonBoolResult(d, result);
				}
				return new JsonBoolResult(d, result);
			}

			// Set up detail parms based on QueryString
			protected void SetupDetailParms() {
				StringValues detailTblVar = "";

				// Get the keys for master table
				if (Query.TryGetValue(Config.TableShowDetail, out detailTblVar)) { // Value may be empty
					CurrentDetailTable = detailTblVar;
				} else {
					detailTblVar = CurrentDetailTable;
				}
				if (!Empty(detailTblVar)) {
					var detailTblVars = new List<string>(detailTblVar.ToString().Split(','));
					if (detailTblVars.Contains("s_dodetltest")) {
						s_dodetltest_Grid = s_dodetltest_Grid ?? new _s_dodetltest_Grid();
						if (s_dodetltest_Grid.DetailAdd) {
							if (CopyRecord)
								s_dodetltest_Grid.CurrentMode = "copy";
							else
								s_dodetltest_Grid.CurrentMode = "add";
							s_dodetltest_Grid.CurrentAction = "gridadd";

							// Save current master table to detail table
							s_dodetltest_Grid.CurrentMasterTable = TableVar;
							s_dodetltest_Grid.StartRecordNumber = 1;
							s_dodetltest_Grid.Id_dohdrTrxId.IsDetailKey = true;
							s_dodetltest_Grid.Id_dohdrTrxId.CurrentValue = TrxId.CurrentValue;
							s_dodetltest_Grid.Id_dohdrTrxId.SessionValue = s_dodetltest_Grid.Id_dohdrTrxId.CurrentValue;
						}
					}
				}
			}

			// Set up Breadcrumb
			protected void SetupBreadcrumb() {
				var breadcrumb = new Breadcrumb();
				string url = CurrentUrl();
				breadcrumb.Add("list", TableVar, AppPath(AddMasterUrl("s_dohdrtestlist")), "", TableVar, true);
				string pageId = IsCopy ? "Copy" : "Add";
				breadcrumb.Add("add", pageId, url);
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
									case "x_dbcode":
									break;
								}

								// Format the field values
								switch (fld.FieldVar) {
									case "x_CurrencyCode":
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
