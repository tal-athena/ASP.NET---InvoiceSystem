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
		/// s_glchart_Add
		/// </summary>

		public static _s_glchart_Add s_glchart_Add {
			get => HttpData.Get<_s_glchart_Add>("s_glchart_Add");
			set => HttpData["s_glchart_Add"] = value;
		}

		/// <summary>
		/// Page class for s_glchart
		/// </summary>

		public class _s_glchart_Add : _s_glchart_AddBase
		{

			// Construtor
			public _s_glchart_Add(Controller controller = null) : base(controller) {
			}
		}

		/// <summary>
		/// Page base class
		/// </summary>

		public class _s_glchart_AddBase : _s_glchart, IAspNetMakerPage
		{

			// Page ID
			public string PageID = "add";

			// Project ID
			public string ProjectID = "{8543F230-11C6-4105-B51C-8D87C21BE659}";

			// Table name
			public string TableName = "s_glchart";

			// Page object name
			public string PageObjName = "s_glchart_Add";

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
			public _s_glchart_AddBase(Controller controller = null) { // DN
				if (controller != null)
					Controller = controller;

				// Initialize
				CurrentPage = this;

				// Language object
				Language = Language ?? new Lang();

				// Table object (s_glchart)
				if (s_glchart == null || s_glchart is _s_glchart)
					s_glchart = this;

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
					dynamic doc = CreateInstance(classname, new object[] { s_glchart, "" }); // DN
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
								if (pageName == "s_glchartview")
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
							return Terminate(GetUrl("s_glchartlist"));
						else
							return Terminate(GetUrl("login"));
					}
				}

				// Create form object
				CurrentForm = new HttpForm();
				CurrentAction = Param("action"); // Set up current action
				Id.Visible = false;
				acct_code.SetVisibility();
				description.SetVisibility();
				report_type.SetVisibility();
				acct_group.SetVisibility();
				acct_type.SetVisibility();
				CurrencyCode.SetVisibility();
				opn_debit.SetVisibility();
				opn_credit.SetVisibility();
				bal_mtd.SetVisibility();
				bal_ytd.SetVisibility();
				remark.SetVisibility();
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
					if (RouteValues.TryGetValue("Id", out rv)) { // DN
						Id.QueryValue = Convert.ToString(rv);
						SetKey("Id", Id.CurrentValue); // Set up key
					} else if (!Empty(Get("Id"))) {
						Id.QueryValue = Get("Id");
						SetKey("Id", Id.CurrentValue); // Set up key
					} else if (IsApi() && !Empty(keyValues)) {
						Id.QueryValue = Convert.ToString(keyValues[0]);
						SetKey("Id", Id.CurrentValue); // Set up key
					} else {
						SetKey("Id", ""); // Clear key
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
							return Terminate("s_glchartlist"); // No matching record, return to List page // DN
						}
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
							returnUrl = ReturnUrl;
							if (GetPageName(returnUrl) == "s_glchartlist")
								returnUrl = AddMasterUrl(ListUrl); // List page, return to List page with correct master key if necessary
							else if (GetPageName(returnUrl) == "s_glchartview")
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
				Id.CurrentValue = System.DBNull.Value;
				Id.OldValue = Id.CurrentValue;
				acct_code.CurrentValue = System.DBNull.Value;
				acct_code.OldValue = acct_code.CurrentValue;
				description.CurrentValue = System.DBNull.Value;
				description.OldValue = description.CurrentValue;
				report_type.CurrentValue = System.DBNull.Value;
				report_type.OldValue = report_type.CurrentValue;
				acct_group.CurrentValue = System.DBNull.Value;
				acct_group.OldValue = acct_group.CurrentValue;
				acct_type.CurrentValue = System.DBNull.Value;
				acct_type.OldValue = acct_type.CurrentValue;
				CurrencyCode.CurrentValue = System.DBNull.Value;
				CurrencyCode.OldValue = CurrencyCode.CurrentValue;
				opn_debit.CurrentValue = System.DBNull.Value;
				opn_debit.OldValue = opn_debit.CurrentValue;
				opn_credit.CurrentValue = System.DBNull.Value;
				opn_credit.OldValue = opn_credit.CurrentValue;
				bal_mtd.CurrentValue = System.DBNull.Value;
				bal_mtd.OldValue = bal_mtd.CurrentValue;
				bal_ytd.CurrentValue = System.DBNull.Value;
				bal_ytd.OldValue = bal_ytd.CurrentValue;
				remark.CurrentValue = remark.GetDefault();
			}

			#pragma warning disable 1998

			// Load form values
			protected async Task LoadFormValues() {
				string val;

				// Check field name 'acct_code' first before field var 'x_acct_code'
				val = CurrentForm.GetValue("acct_code", "x_acct_code");
				if (!acct_code.IsDetailKey) {
					if (IsApi() && val == null)
						acct_code.Visible = false; // Disable update for API request
					else
						acct_code.FormValue = val;
				}

				// Check field name 'description' first before field var 'x_description'
				val = CurrentForm.GetValue("description", "x_description");
				if (!description.IsDetailKey) {
					if (IsApi() && val == null)
						description.Visible = false; // Disable update for API request
					else
						description.FormValue = val;
				}

				// Check field name 'report_type' first before field var 'x_report_type'
				val = CurrentForm.GetValue("report_type", "x_report_type");
				if (!report_type.IsDetailKey) {
					if (IsApi() && val == null)
						report_type.Visible = false; // Disable update for API request
					else
						report_type.FormValue = val;
				}

				// Check field name 'acct_group' first before field var 'x_acct_group'
				val = CurrentForm.GetValue("acct_group", "x_acct_group");
				if (!acct_group.IsDetailKey) {
					if (IsApi() && val == null)
						acct_group.Visible = false; // Disable update for API request
					else
						acct_group.FormValue = val;
				}

				// Check field name 'acct_type' first before field var 'x_acct_type'
				val = CurrentForm.GetValue("acct_type", "x_acct_type");
				if (!acct_type.IsDetailKey) {
					if (IsApi() && val == null)
						acct_type.Visible = false; // Disable update for API request
					else
						acct_type.FormValue = val;
				}

				// Check field name 'CurrencyCode' first before field var 'x_CurrencyCode'
				val = CurrentForm.GetValue("CurrencyCode", "x_CurrencyCode");
				if (!CurrencyCode.IsDetailKey) {
					if (IsApi() && val == null)
						CurrencyCode.Visible = false; // Disable update for API request
					else
						CurrencyCode.FormValue = val;
				}

				// Check field name 'opn_debit' first before field var 'x_opn_debit'
				val = CurrentForm.GetValue("opn_debit", "x_opn_debit");
				if (!opn_debit.IsDetailKey) {
					if (IsApi() && val == null)
						opn_debit.Visible = false; // Disable update for API request
					else
						opn_debit.FormValue = val;
				}

				// Check field name 'opn_credit' first before field var 'x_opn_credit'
				val = CurrentForm.GetValue("opn_credit", "x_opn_credit");
				if (!opn_credit.IsDetailKey) {
					if (IsApi() && val == null)
						opn_credit.Visible = false; // Disable update for API request
					else
						opn_credit.FormValue = val;
				}

				// Check field name 'bal_mtd' first before field var 'x_bal_mtd'
				val = CurrentForm.GetValue("bal_mtd", "x_bal_mtd");
				if (!bal_mtd.IsDetailKey) {
					if (IsApi() && val == null)
						bal_mtd.Visible = false; // Disable update for API request
					else
						bal_mtd.FormValue = val;
				}

				// Check field name 'bal_ytd' first before field var 'x_bal_ytd'
				val = CurrentForm.GetValue("bal_ytd", "x_bal_ytd");
				if (!bal_ytd.IsDetailKey) {
					if (IsApi() && val == null)
						bal_ytd.Visible = false; // Disable update for API request
					else
						bal_ytd.FormValue = val;
				}

				// Check field name 'remark' first before field var 'x_remark'
				val = CurrentForm.GetValue("remark", "x_remark");
				if (!remark.IsDetailKey) {
					if (IsApi() && val == null)
						remark.Visible = false; // Disable update for API request
					else
						remark.FormValue = val;
				}

				// Check field name 'Id' first before field var 'x_Id'
				val = CurrentForm.GetValue("Id", "x_Id");
			}

			#pragma warning restore 1998

			// Restore form values
			public void RestoreFormValues() {
				acct_code.CurrentValue = acct_code.FormValue;
				description.CurrentValue = description.FormValue;
				report_type.CurrentValue = report_type.FormValue;
				acct_group.CurrentValue = acct_group.FormValue;
				acct_type.CurrentValue = acct_type.FormValue;
				CurrencyCode.CurrentValue = CurrencyCode.FormValue;
				opn_debit.CurrentValue = opn_debit.FormValue;
				opn_credit.CurrentValue = opn_credit.FormValue;
				bal_mtd.CurrentValue = bal_mtd.FormValue;
				bal_ytd.CurrentValue = bal_ytd.FormValue;
				remark.CurrentValue = remark.FormValue;
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
				acct_code.SetDbValue(row["acct_code"]);
				description.SetDbValue(row["description"]);
				report_type.SetDbValue(row["report_type"]);
				acct_group.SetDbValue(row["acct_group"]);
				acct_type.SetDbValue(row["acct_type"]);
				CurrencyCode.SetDbValue(row["CurrencyCode"]);
				opn_debit.SetDbValue(row["opn_debit"]);
				opn_credit.SetDbValue(row["opn_credit"]);
				bal_mtd.SetDbValue(row["bal_mtd"]);
				bal_ytd.SetDbValue(row["bal_ytd"]);
				remark.SetDbValue(row["remark"]);
			}

			#pragma warning restore 162, 168, 1998

			// Return a row with default values
			protected Dictionary<string, object> NewRow() {
				LoadDefaultValues();
				var row = new Dictionary<string, object>();
				row.Add("Id", Id.CurrentValue);
				row.Add("acct_code", acct_code.CurrentValue);
				row.Add("description", description.CurrentValue);
				row.Add("report_type", report_type.CurrentValue);
				row.Add("acct_group", acct_group.CurrentValue);
				row.Add("acct_type", acct_type.CurrentValue);
				row.Add("CurrencyCode", CurrencyCode.CurrentValue);
				row.Add("opn_debit", opn_debit.CurrentValue);
				row.Add("opn_credit", opn_credit.CurrentValue);
				row.Add("bal_mtd", bal_mtd.CurrentValue);
				row.Add("bal_ytd", bal_ytd.CurrentValue);
				row.Add("remark", remark.CurrentValue);
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

				// Convert decimal values if posted back
				if (SameString(opn_debit.FormValue, opn_debit.CurrentValue) && IsNumeric(ConvertToFloatString(opn_debit.CurrentValue)))
					opn_debit.CurrentValue = ConvertToFloatString(opn_debit.CurrentValue);

				// Convert decimal values if posted back
				if (SameString(opn_credit.FormValue, opn_credit.CurrentValue) && IsNumeric(ConvertToFloatString(opn_credit.CurrentValue)))
					opn_credit.CurrentValue = ConvertToFloatString(opn_credit.CurrentValue);

				// Convert decimal values if posted back
				if (SameString(bal_mtd.FormValue, bal_mtd.CurrentValue) && IsNumeric(ConvertToFloatString(bal_mtd.CurrentValue)))
					bal_mtd.CurrentValue = ConvertToFloatString(bal_mtd.CurrentValue);

				// Convert decimal values if posted back
				if (SameString(bal_ytd.FormValue, bal_ytd.CurrentValue) && IsNumeric(ConvertToFloatString(bal_ytd.CurrentValue)))
					bal_ytd.CurrentValue = ConvertToFloatString(bal_ytd.CurrentValue);

				// Call Row_Rendering event
				Row_Rendering();

				// Common render codes for all row types
				// Id
				// acct_code
				// description
				// report_type
				// acct_group
				// acct_type
				// CurrencyCode
				// opn_debit
				// opn_credit
				// bal_mtd
				// bal_ytd
				// remark

				if (RowType == Config.RowTypeView) { // View row

					// Id
					Id.ViewValue = Id.CurrentValue;

					// acct_code
					acct_code.ViewValue = acct_code.CurrentValue;

					// description
					description.ViewValue = description.CurrentValue;

					// report_type
					report_type.ViewValue = report_type.CurrentValue;

					// acct_group
					acct_group.ViewValue = acct_group.CurrentValue;

					// acct_type
					acct_type.ViewValue = acct_type.CurrentValue;

					// CurrencyCode
					CurrencyCode.ViewValue = CurrencyCode.CurrentValue;

					// opn_debit
					opn_debit.ViewValue = opn_debit.CurrentValue;
					opn_debit.ViewValue = FormatNumber(opn_debit.ViewValue, 2, -2, -2, -2);

					// opn_credit
					opn_credit.ViewValue = opn_credit.CurrentValue;
					opn_credit.ViewValue = FormatNumber(opn_credit.ViewValue, 2, -2, -2, -2);

					// bal_mtd
					bal_mtd.ViewValue = bal_mtd.CurrentValue;
					bal_mtd.ViewValue = FormatNumber(bal_mtd.ViewValue, 2, -2, -2, -2);

					// bal_ytd
					bal_ytd.ViewValue = bal_ytd.CurrentValue;
					bal_ytd.ViewValue = FormatNumber(bal_ytd.ViewValue, 2, -2, -2, -2);

					// remark
					remark.ViewValue = remark.CurrentValue;

					// acct_code
					acct_code.HrefValue = "";
					acct_code.TooltipValue = "";

					// description
					description.HrefValue = "";
					description.TooltipValue = "";

					// report_type
					report_type.HrefValue = "";
					report_type.TooltipValue = "";

					// acct_group
					acct_group.HrefValue = "";
					acct_group.TooltipValue = "";

					// acct_type
					acct_type.HrefValue = "";
					acct_type.TooltipValue = "";

					// CurrencyCode
					CurrencyCode.HrefValue = "";
					CurrencyCode.TooltipValue = "";

					// opn_debit
					opn_debit.HrefValue = "";
					opn_debit.TooltipValue = "";

					// opn_credit
					opn_credit.HrefValue = "";
					opn_credit.TooltipValue = "";

					// bal_mtd
					bal_mtd.HrefValue = "";
					bal_mtd.TooltipValue = "";

					// bal_ytd
					bal_ytd.HrefValue = "";
					bal_ytd.TooltipValue = "";

					// remark
					remark.HrefValue = "";
					remark.TooltipValue = "";
				} else if (RowType == Config.RowTypeAdd) { // Add row

					// acct_code
					acct_code.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						acct_code.CurrentValue = HtmlDecode(acct_code.CurrentValue);
					acct_code.EditValue = acct_code.CurrentValue; // DN
					acct_code.PlaceHolder = RemoveHtml(acct_code.Caption);

					// description
					description.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						description.CurrentValue = HtmlDecode(description.CurrentValue);
					description.EditValue = description.CurrentValue; // DN
					description.PlaceHolder = RemoveHtml(description.Caption);

					// report_type
					report_type.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						report_type.CurrentValue = HtmlDecode(report_type.CurrentValue);
					report_type.EditValue = report_type.CurrentValue; // DN
					report_type.PlaceHolder = RemoveHtml(report_type.Caption);

					// acct_group
					acct_group.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						acct_group.CurrentValue = HtmlDecode(acct_group.CurrentValue);
					acct_group.EditValue = acct_group.CurrentValue; // DN
					acct_group.PlaceHolder = RemoveHtml(acct_group.Caption);

					// acct_type
					acct_type.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						acct_type.CurrentValue = HtmlDecode(acct_type.CurrentValue);
					acct_type.EditValue = acct_type.CurrentValue; // DN
					acct_type.PlaceHolder = RemoveHtml(acct_type.Caption);

					// CurrencyCode
					CurrencyCode.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						CurrencyCode.CurrentValue = HtmlDecode(CurrencyCode.CurrentValue);
					CurrencyCode.EditValue = CurrencyCode.CurrentValue; // DN
					CurrencyCode.PlaceHolder = RemoveHtml(CurrencyCode.Caption);

					// opn_debit
					opn_debit.EditAttrs["class"] = "form-control";
					opn_debit.EditValue = opn_debit.CurrentValue; // DN
					opn_debit.PlaceHolder = RemoveHtml(opn_debit.Caption);
					if (!Empty(opn_debit.EditValue) && IsNumeric(opn_debit.EditValue))
						opn_debit.EditValue = FormatNumber(opn_debit.EditValue, -2, -2, -2, -2);

					// opn_credit
					opn_credit.EditAttrs["class"] = "form-control";
					opn_credit.EditValue = opn_credit.CurrentValue; // DN
					opn_credit.PlaceHolder = RemoveHtml(opn_credit.Caption);
					if (!Empty(opn_credit.EditValue) && IsNumeric(opn_credit.EditValue))
						opn_credit.EditValue = FormatNumber(opn_credit.EditValue, -2, -2, -2, -2);

					// bal_mtd
					bal_mtd.EditAttrs["class"] = "form-control";
					bal_mtd.EditValue = bal_mtd.CurrentValue; // DN
					bal_mtd.PlaceHolder = RemoveHtml(bal_mtd.Caption);
					if (!Empty(bal_mtd.EditValue) && IsNumeric(bal_mtd.EditValue))
						bal_mtd.EditValue = FormatNumber(bal_mtd.EditValue, -2, -2, -2, -2);

					// bal_ytd
					bal_ytd.EditAttrs["class"] = "form-control";
					bal_ytd.EditValue = bal_ytd.CurrentValue; // DN
					bal_ytd.PlaceHolder = RemoveHtml(bal_ytd.Caption);
					if (!Empty(bal_ytd.EditValue) && IsNumeric(bal_ytd.EditValue))
						bal_ytd.EditValue = FormatNumber(bal_ytd.EditValue, -2, -2, -2, -2);

					// remark
					remark.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						remark.CurrentValue = HtmlDecode(remark.CurrentValue);
					remark.EditValue = remark.CurrentValue; // DN
					remark.PlaceHolder = RemoveHtml(remark.Caption);

					// Add refer script
					// acct_code

					acct_code.HrefValue = "";

					// description
					description.HrefValue = "";

					// report_type
					report_type.HrefValue = "";

					// acct_group
					acct_group.HrefValue = "";

					// acct_type
					acct_type.HrefValue = "";

					// CurrencyCode
					CurrencyCode.HrefValue = "";

					// opn_debit
					opn_debit.HrefValue = "";

					// opn_credit
					opn_credit.HrefValue = "";

					// bal_mtd
					bal_mtd.HrefValue = "";

					// bal_ytd
					bal_ytd.HrefValue = "";

					// remark
					remark.HrefValue = "";
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
				if (acct_code.Required) {
					if (!acct_code.IsDetailKey && Empty(acct_code.FormValue))
						FormError = AddMessage(FormError, acct_code.RequiredErrorMessage.Replace("%s", acct_code.Caption));
				}
				if (description.Required) {
					if (!description.IsDetailKey && Empty(description.FormValue))
						FormError = AddMessage(FormError, description.RequiredErrorMessage.Replace("%s", description.Caption));
				}
				if (report_type.Required) {
					if (!report_type.IsDetailKey && Empty(report_type.FormValue))
						FormError = AddMessage(FormError, report_type.RequiredErrorMessage.Replace("%s", report_type.Caption));
				}
				if (acct_group.Required) {
					if (!acct_group.IsDetailKey && Empty(acct_group.FormValue))
						FormError = AddMessage(FormError, acct_group.RequiredErrorMessage.Replace("%s", acct_group.Caption));
				}
				if (acct_type.Required) {
					if (!acct_type.IsDetailKey && Empty(acct_type.FormValue))
						FormError = AddMessage(FormError, acct_type.RequiredErrorMessage.Replace("%s", acct_type.Caption));
				}
				if (CurrencyCode.Required) {
					if (!CurrencyCode.IsDetailKey && Empty(CurrencyCode.FormValue))
						FormError = AddMessage(FormError, CurrencyCode.RequiredErrorMessage.Replace("%s", CurrencyCode.Caption));
				}
				if (opn_debit.Required) {
					if (!opn_debit.IsDetailKey && Empty(opn_debit.FormValue))
						FormError = AddMessage(FormError, opn_debit.RequiredErrorMessage.Replace("%s", opn_debit.Caption));
				}
				if (!CheckNumber(opn_debit.FormValue))
					FormError = AddMessage(FormError, opn_debit.ErrorMessage);
				if (opn_credit.Required) {
					if (!opn_credit.IsDetailKey && Empty(opn_credit.FormValue))
						FormError = AddMessage(FormError, opn_credit.RequiredErrorMessage.Replace("%s", opn_credit.Caption));
				}
				if (!CheckNumber(opn_credit.FormValue))
					FormError = AddMessage(FormError, opn_credit.ErrorMessage);
				if (bal_mtd.Required) {
					if (!bal_mtd.IsDetailKey && Empty(bal_mtd.FormValue))
						FormError = AddMessage(FormError, bal_mtd.RequiredErrorMessage.Replace("%s", bal_mtd.Caption));
				}
				if (!CheckNumber(bal_mtd.FormValue))
					FormError = AddMessage(FormError, bal_mtd.ErrorMessage);
				if (bal_ytd.Required) {
					if (!bal_ytd.IsDetailKey && Empty(bal_ytd.FormValue))
						FormError = AddMessage(FormError, bal_ytd.RequiredErrorMessage.Replace("%s", bal_ytd.Caption));
				}
				if (!CheckNumber(bal_ytd.FormValue))
					FormError = AddMessage(FormError, bal_ytd.ErrorMessage);
				if (remark.Required) {
					if (!remark.IsDetailKey && Empty(remark.FormValue))
						FormError = AddMessage(FormError, remark.RequiredErrorMessage.Replace("%s", remark.Caption));
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

				// Load db values from rsold
				LoadDbValues(rsold);
				if (rsold != null) {
				}
				try {

					// acct_code
					acct_code.SetDbValue(rsnew, acct_code.CurrentValue, "", false);

					// description
					description.SetDbValue(rsnew, description.CurrentValue, "", false);

					// report_type
					report_type.SetDbValue(rsnew, report_type.CurrentValue, System.DBNull.Value, false);

					// acct_group
					acct_group.SetDbValue(rsnew, acct_group.CurrentValue, System.DBNull.Value, false);

					// acct_type
					acct_type.SetDbValue(rsnew, acct_type.CurrentValue, "", false);

					// CurrencyCode
					CurrencyCode.SetDbValue(rsnew, CurrencyCode.CurrentValue, "", false);

					// opn_debit
					opn_debit.SetDbValue(rsnew, opn_debit.CurrentValue, System.DBNull.Value, false);

					// opn_credit
					opn_credit.SetDbValue(rsnew, opn_credit.CurrentValue, System.DBNull.Value, false);

					// bal_mtd
					bal_mtd.SetDbValue(rsnew, bal_mtd.CurrentValue, System.DBNull.Value, false);

					// bal_ytd
					bal_ytd.SetDbValue(rsnew, bal_ytd.CurrentValue, System.DBNull.Value, false);

					// remark
					remark.SetDbValue(rsnew, remark.CurrentValue, System.DBNull.Value, Empty(remark.CurrentValue));
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

			// Set up Breadcrumb
			protected void SetupBreadcrumb() {
				var breadcrumb = new Breadcrumb();
				string url = CurrentUrl();
				breadcrumb.Add("list", TableVar, AppPath(AddMasterUrl("s_glchartlist")), "", TableVar, true);
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
