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
		/// s_dodetltest_Edit
		/// </summary>

		public static _s_dodetltest_Edit s_dodetltest_Edit {
			get => HttpData.Get<_s_dodetltest_Edit>("s_dodetltest_Edit");
			set => HttpData["s_dodetltest_Edit"] = value;
		}

		/// <summary>
		/// Page class for s_dodetltest
		/// </summary>

		public class _s_dodetltest_Edit : _s_dodetltest_EditBase
		{

			// Construtor
			public _s_dodetltest_Edit(Controller controller = null) : base(controller) {
			}
		}

		/// <summary>
		/// Page base class
		/// </summary>

		public class _s_dodetltest_EditBase : _s_dodetltest, IAspNetMakerPage
		{

			// Page ID
			public string PageID = "edit";

			// Project ID
			public string ProjectID = "{8543F230-11C6-4105-B51C-8D87C21BE659}";

			// Table name
			public string TableName = "s_dodetltest";

			// Page object name
			public string PageObjName = "s_dodetltest_Edit";

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
			public _s_dodetltest_EditBase(Controller controller = null) { // DN
				if (controller != null)
					Controller = controller;

				// Initialize
				CurrentPage = this;

				// Language object
				Language = Language ?? new Lang();

				// Table object (s_dodetltest)
				if (s_dodetltest == null || s_dodetltest is _s_dodetltest)
					s_dodetltest = this;

				// Table object (s_dohdrtest)
				s_dohdrtest = s_dohdrtest ?? new _s_dohdrtest();

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
					dynamic doc = CreateInstance(classname, new object[] { s_dodetltest, "" }); // DN
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
								if (pageName == "s_dodetltestview")
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
							return Terminate(GetUrl("s_dodetltestlist"));
						else
							return Terminate(GetUrl("login"));
					}
				}

				// Create form object
				CurrentForm = new HttpForm();
				CurrentAction = Param("action"); // Set up current action
				TrxId.SetVisibility();
				Id_dohdrTrxId.SetVisibility();
				item_no.SetVisibility();
				item_type.SetVisibility();
				Id_sodetlTrxId.SetVisibility();
				Id_podetlTrxId.SetVisibility();
				part_code.SetVisibility();
				part_desc.SetVisibility();
				uom.SetVisibility();
				qty.SetVisibility();
				unit_price.SetVisibility();
				amount_origin.SetVisibility();
				amount_local.SetVisibility();
				tax_code.SetVisibility();
				tax_rate.SetVisibility();
				tax_amount_origin.SetVisibility();
				tax_amount_local.SetVisibility();
				TrxUserId.SetVisibility();
				to_gl_acct.SetVisibility();
				tax_acct.SetVisibility();
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
				await SetupLookupOptions(part_code);

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
					if (RouteValues.TryGetValue("TrxId", out rv)) { // DN
						TrxId.FormValue = Convert.ToString(rv);
						RecordKeys["TrxId"] = TrxId.FormValue;
					} else if (CurrentForm.HasValue("x_TrxId")) {
						TrxId.FormValue = CurrentForm.GetValue("x_TrxId");
						RecordKeys["TrxId"] = TrxId.FormValue;
					} else if (IsApi() && !Empty(keyValues)) {
						RecordKeys["TrxId"] = Convert.ToString(keyValues[0]);
					}
				} else {
					CurrentAction = "show"; // Default action is display

					// Load key from QueryString
					bool loadByQuery = false;
					string[] keyValues = null;
					object rv;
					if (IsApi() && RouteValues.TryGetValue("key", out object k))
						keyValues = k.ToString().Split('/');
					if (RouteValues.TryGetValue("TrxId", out rv)) { // DN
						TrxId.QueryValue = Convert.ToString(rv);
						RecordKeys["TrxId"] = TrxId.QueryValue;
						loadByQuery = true;
					} else if (!Empty(Get("TrxId"))) {
						TrxId.QueryValue = Get("TrxId");
						RecordKeys["TrxId"] = TrxId.QueryValue;
						loadByQuery = true;
					} else if (IsApi() && !Empty(keyValues)) {
						TrxId.QueryValue = Convert.ToString(keyValues[0]);
						RecordKeys["TrxId"] = TrxId.QueryValue;
						loadByQuery = true;
					} else {
						TrxId.CurrentValue = System.DBNull.Value;
					}
				}

				// Set up master detail parameters
				SetupMasterParms();

			// Load current record
			loaded = await LoadRow();

			// Process form if post back
			if (postBack) {
				await LoadFormValues(); // Get form values
				if (IsApi() && RouteValues.TryGetValue("key", out object k)) {
					var keyValues = k.ToString().Split('/');
					TrxId.FormValue = Convert.ToString(keyValues[0]);
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
							return Terminate("s_dodetltestlist"); // No matching record, return to list
						}
						break;
					case "update": // Update // DN
						CloseRecordset(); // DN
						string returnUrl = ReturnUrl;
						if (GetPageName(returnUrl) == "s_dodetltestlist")
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

				// Check field name 'TrxId' first before field var 'x_TrxId'
				val = CurrentForm.GetValue("TrxId", "x_TrxId");
				if (!TrxId.IsDetailKey)
					TrxId.FormValue = val;

				// Check field name 'Id_dohdrTrxId' first before field var 'x_Id_dohdrTrxId'
				val = CurrentForm.GetValue("Id_dohdrTrxId", "x_Id_dohdrTrxId");
				if (!Id_dohdrTrxId.IsDetailKey) {
					if (IsApi() && val == null)
						Id_dohdrTrxId.Visible = false; // Disable update for API request
					else
						Id_dohdrTrxId.FormValue = val;
				}

				// Check field name 'item_no' first before field var 'x_item_no'
				val = CurrentForm.GetValue("item_no", "x_item_no");
				if (!item_no.IsDetailKey) {
					if (IsApi() && val == null)
						item_no.Visible = false; // Disable update for API request
					else
						item_no.FormValue = val;
				}

				// Check field name 'item_type' first before field var 'x_item_type'
				val = CurrentForm.GetValue("item_type", "x_item_type");
				if (!item_type.IsDetailKey) {
					if (IsApi() && val == null)
						item_type.Visible = false; // Disable update for API request
					else
						item_type.FormValue = val;
				}

				// Check field name 'Id_sodetlTrxId' first before field var 'x_Id_sodetlTrxId'
				val = CurrentForm.GetValue("Id_sodetlTrxId", "x_Id_sodetlTrxId");
				if (!Id_sodetlTrxId.IsDetailKey) {
					if (IsApi() && val == null)
						Id_sodetlTrxId.Visible = false; // Disable update for API request
					else
						Id_sodetlTrxId.FormValue = val;
				}

				// Check field name 'Id_podetlTrxId' first before field var 'x_Id_podetlTrxId'
				val = CurrentForm.GetValue("Id_podetlTrxId", "x_Id_podetlTrxId");
				if (!Id_podetlTrxId.IsDetailKey) {
					if (IsApi() && val == null)
						Id_podetlTrxId.Visible = false; // Disable update for API request
					else
						Id_podetlTrxId.FormValue = val;
				}

				// Check field name 'part_code' first before field var 'x_part_code'
				val = CurrentForm.GetValue("part_code", "x_part_code");
				if (!part_code.IsDetailKey) {
					if (IsApi() && val == null)
						part_code.Visible = false; // Disable update for API request
					else
						part_code.FormValue = val;
				}

				// Check field name 'part_desc' first before field var 'x_part_desc'
				val = CurrentForm.GetValue("part_desc", "x_part_desc");
				if (!part_desc.IsDetailKey) {
					if (IsApi() && val == null)
						part_desc.Visible = false; // Disable update for API request
					else
						part_desc.FormValue = val;
				}

				// Check field name 'uom' first before field var 'x_uom'
				val = CurrentForm.GetValue("uom", "x_uom");
				if (!uom.IsDetailKey) {
					if (IsApi() && val == null)
						uom.Visible = false; // Disable update for API request
					else
						uom.FormValue = val;
				}

				// Check field name 'qty' first before field var 'x_qty'
				val = CurrentForm.GetValue("qty", "x_qty");
				if (!qty.IsDetailKey) {
					if (IsApi() && val == null)
						qty.Visible = false; // Disable update for API request
					else
						qty.FormValue = val;
				}

				// Check field name 'unit_price' first before field var 'x_unit_price'
				val = CurrentForm.GetValue("unit_price", "x_unit_price");
				if (!unit_price.IsDetailKey) {
					if (IsApi() && val == null)
						unit_price.Visible = false; // Disable update for API request
					else
						unit_price.FormValue = val;
				}

				// Check field name 'amount_origin' first before field var 'x_amount_origin'
				val = CurrentForm.GetValue("amount_origin", "x_amount_origin");
				if (!amount_origin.IsDetailKey) {
					if (IsApi() && val == null)
						amount_origin.Visible = false; // Disable update for API request
					else
						amount_origin.FormValue = val;
				}

				// Check field name 'amount_local' first before field var 'x_amount_local'
				val = CurrentForm.GetValue("amount_local", "x_amount_local");
				if (!amount_local.IsDetailKey) {
					if (IsApi() && val == null)
						amount_local.Visible = false; // Disable update for API request
					else
						amount_local.FormValue = val;
				}

				// Check field name 'tax_code' first before field var 'x_tax_code'
				val = CurrentForm.GetValue("tax_code", "x_tax_code");
				if (!tax_code.IsDetailKey) {
					if (IsApi() && val == null)
						tax_code.Visible = false; // Disable update for API request
					else
						tax_code.FormValue = val;
				}

				// Check field name 'tax_rate' first before field var 'x_tax_rate'
				val = CurrentForm.GetValue("tax_rate", "x_tax_rate");
				if (!tax_rate.IsDetailKey) {
					if (IsApi() && val == null)
						tax_rate.Visible = false; // Disable update for API request
					else
						tax_rate.FormValue = val;
				}

				// Check field name 'tax_amount_origin' first before field var 'x_tax_amount_origin'
				val = CurrentForm.GetValue("tax_amount_origin", "x_tax_amount_origin");
				if (!tax_amount_origin.IsDetailKey) {
					if (IsApi() && val == null)
						tax_amount_origin.Visible = false; // Disable update for API request
					else
						tax_amount_origin.FormValue = val;
				}

				// Check field name 'tax_amount_local' first before field var 'x_tax_amount_local'
				val = CurrentForm.GetValue("tax_amount_local", "x_tax_amount_local");
				if (!tax_amount_local.IsDetailKey) {
					if (IsApi() && val == null)
						tax_amount_local.Visible = false; // Disable update for API request
					else
						tax_amount_local.FormValue = val;
				}

				// Check field name 'TrxUserId' first before field var 'x_TrxUserId'
				val = CurrentForm.GetValue("TrxUserId", "x_TrxUserId");
				if (!TrxUserId.IsDetailKey) {
					if (IsApi() && val == null)
						TrxUserId.Visible = false; // Disable update for API request
					else
						TrxUserId.FormValue = val;
				}

				// Check field name 'to_gl_acct' first before field var 'x_to_gl_acct'
				val = CurrentForm.GetValue("to_gl_acct", "x_to_gl_acct");
				if (!to_gl_acct.IsDetailKey) {
					if (IsApi() && val == null)
						to_gl_acct.Visible = false; // Disable update for API request
					else
						to_gl_acct.FormValue = val;
				}

				// Check field name 'tax_acct' first before field var 'x_tax_acct'
				val = CurrentForm.GetValue("tax_acct", "x_tax_acct");
				if (!tax_acct.IsDetailKey) {
					if (IsApi() && val == null)
						tax_acct.Visible = false; // Disable update for API request
					else
						tax_acct.FormValue = val;
				}
			}

			#pragma warning restore 1998

			// Restore form values
			public void RestoreFormValues() {
				TrxId.CurrentValue = TrxId.FormValue;
				Id_dohdrTrxId.CurrentValue = Id_dohdrTrxId.FormValue;
				item_no.CurrentValue = item_no.FormValue;
				item_type.CurrentValue = item_type.FormValue;
				Id_sodetlTrxId.CurrentValue = Id_sodetlTrxId.FormValue;
				Id_podetlTrxId.CurrentValue = Id_podetlTrxId.FormValue;
				part_code.CurrentValue = part_code.FormValue;
				part_desc.CurrentValue = part_desc.FormValue;
				uom.CurrentValue = uom.FormValue;
				qty.CurrentValue = qty.FormValue;
				unit_price.CurrentValue = unit_price.FormValue;
				amount_origin.CurrentValue = amount_origin.FormValue;
				amount_local.CurrentValue = amount_local.FormValue;
				tax_code.CurrentValue = tax_code.FormValue;
				tax_rate.CurrentValue = tax_rate.FormValue;
				tax_amount_origin.CurrentValue = tax_amount_origin.FormValue;
				tax_amount_local.CurrentValue = tax_amount_local.FormValue;
				TrxUserId.CurrentValue = TrxUserId.FormValue;
				to_gl_acct.CurrentValue = to_gl_acct.FormValue;
				tax_acct.CurrentValue = tax_acct.FormValue;
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
				Id_dohdrTrxId.SetDbValue(row["Id_dohdrTrxId"]);
				item_no.SetDbValue(row["item_no"]);
				item_type.SetDbValue(row["item_type"]);
				Id_sodetlTrxId.SetDbValue(row["Id_sodetlTrxId"]);
				Id_podetlTrxId.SetDbValue(row["Id_podetlTrxId"]);
				part_code.SetDbValue(row["part_code"]);
				part_desc.SetDbValue(row["part_desc"]);
				uom.SetDbValue(row["uom"]);
				qty.SetDbValue(row["qty"]);
				unit_price.SetDbValue(row["unit_price"]);
				amount_origin.SetDbValue(row["amount_origin"]);
				amount_local.SetDbValue(row["amount_local"]);
				tax_code.SetDbValue(row["tax_code"]);
				tax_rate.SetDbValue(row["tax_rate"]);
				tax_amount_origin.SetDbValue(row["tax_amount_origin"]);
				tax_amount_local.SetDbValue(row["tax_amount_local"]);
				TrxUserId.SetDbValue(row["TrxUserId"]);
				to_gl_acct.SetDbValue(row["to_gl_acct"]);
				tax_acct.SetDbValue(row["tax_acct"]);
			}

			#pragma warning restore 162, 168, 1998

			// Return a row with default values
			protected Dictionary<string, object> NewRow() {
				var row = new Dictionary<string, object>();
				row.Add("TrxId", System.DBNull.Value);
				row.Add("Id_dohdrTrxId", System.DBNull.Value);
				row.Add("item_no", System.DBNull.Value);
				row.Add("item_type", System.DBNull.Value);
				row.Add("Id_sodetlTrxId", System.DBNull.Value);
				row.Add("Id_podetlTrxId", System.DBNull.Value);
				row.Add("part_code", System.DBNull.Value);
				row.Add("part_desc", System.DBNull.Value);
				row.Add("uom", System.DBNull.Value);
				row.Add("qty", System.DBNull.Value);
				row.Add("unit_price", System.DBNull.Value);
				row.Add("amount_origin", System.DBNull.Value);
				row.Add("amount_local", System.DBNull.Value);
				row.Add("tax_code", System.DBNull.Value);
				row.Add("tax_rate", System.DBNull.Value);
				row.Add("tax_amount_origin", System.DBNull.Value);
				row.Add("tax_amount_local", System.DBNull.Value);
				row.Add("TrxUserId", System.DBNull.Value);
				row.Add("to_gl_acct", System.DBNull.Value);
				row.Add("tax_acct", System.DBNull.Value);
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
				if (SameString(qty.FormValue, qty.CurrentValue) && IsNumeric(ConvertToFloatString(qty.CurrentValue)))
					qty.CurrentValue = ConvertToFloatString(qty.CurrentValue);

				// Convert decimal values if posted back
				if (SameString(unit_price.FormValue, unit_price.CurrentValue) && IsNumeric(ConvertToFloatString(unit_price.CurrentValue)))
					unit_price.CurrentValue = ConvertToFloatString(unit_price.CurrentValue);

				// Convert decimal values if posted back
				if (SameString(amount_origin.FormValue, amount_origin.CurrentValue) && IsNumeric(ConvertToFloatString(amount_origin.CurrentValue)))
					amount_origin.CurrentValue = ConvertToFloatString(amount_origin.CurrentValue);

				// Convert decimal values if posted back
				if (SameString(amount_local.FormValue, amount_local.CurrentValue) && IsNumeric(ConvertToFloatString(amount_local.CurrentValue)))
					amount_local.CurrentValue = ConvertToFloatString(amount_local.CurrentValue);

				// Convert decimal values if posted back
				if (SameString(tax_rate.FormValue, tax_rate.CurrentValue) && IsNumeric(ConvertToFloatString(tax_rate.CurrentValue)))
					tax_rate.CurrentValue = ConvertToFloatString(tax_rate.CurrentValue);

				// Convert decimal values if posted back
				if (SameString(tax_amount_origin.FormValue, tax_amount_origin.CurrentValue) && IsNumeric(ConvertToFloatString(tax_amount_origin.CurrentValue)))
					tax_amount_origin.CurrentValue = ConvertToFloatString(tax_amount_origin.CurrentValue);

				// Convert decimal values if posted back
				if (SameString(tax_amount_local.FormValue, tax_amount_local.CurrentValue) && IsNumeric(ConvertToFloatString(tax_amount_local.CurrentValue)))
					tax_amount_local.CurrentValue = ConvertToFloatString(tax_amount_local.CurrentValue);

				// Call Row_Rendering event
				Row_Rendering();

				// Common render codes for all row types
				// TrxId
				// Id_dohdrTrxId
				// item_no
				// item_type
				// Id_sodetlTrxId
				// Id_podetlTrxId
				// part_code
				// part_desc
				// uom
				// qty
				// unit_price
				// amount_origin
				// amount_local
				// tax_code
				// tax_rate
				// tax_amount_origin
				// tax_amount_local
				// TrxUserId
				// to_gl_acct
				// tax_acct

				if (RowType == Config.RowTypeView) { // View row

					// TrxId
					TrxId.ViewValue = TrxId.CurrentValue;

					// Id_dohdrTrxId
					Id_dohdrTrxId.ViewValue = Id_dohdrTrxId.CurrentValue;
					Id_dohdrTrxId.ViewValue = FormatNumber(Id_dohdrTrxId.ViewValue, 0, -2, -2, -2);

					// item_no
					item_no.ViewValue = item_no.CurrentValue;
					item_no.ViewValue = FormatNumber(item_no.ViewValue, 0, -2, -2, -2);

					// item_type
					item_type.ViewValue = item_type.CurrentValue;

					// Id_sodetlTrxId
					Id_sodetlTrxId.ViewValue = Id_sodetlTrxId.CurrentValue;
					Id_sodetlTrxId.ViewValue = FormatNumber(Id_sodetlTrxId.ViewValue, 0, -2, -2, -2);

					// Id_podetlTrxId
					Id_podetlTrxId.ViewValue = Id_podetlTrxId.CurrentValue;
					Id_podetlTrxId.ViewValue = FormatNumber(Id_podetlTrxId.ViewValue, 0, -2, -2, -2);

					// part_code
					part_code.ViewValue = part_code.CurrentValue;
					curVal = Convert.ToString(part_code.CurrentValue);
					if (!Empty(curVal)) {
						part_code.ViewValue = part_code.LookupCacheOption(curVal);
						if (part_code.ViewValue == null) { // Lookup from database
							filterWrk = "[service_code]" + SearchString("=", curVal.Trim(), Config.DataTypeString, "");
							sqlWrk = part_code.Lookup.GetSql(false, filterWrk, null, this);
							rswrk = await Connection.GetRowsAsync(sqlWrk);
							if (rswrk != null && rswrk.Count > 0) { // Lookup values found
								var listwrk = rswrk[0].Values.ToList();
								listwrk[1] = Convert.ToString(listwrk[1]);
								listwrk[2] = Convert.ToString(listwrk[2]);
								part_code.ViewValue = part_code.DisplayValue(listwrk);
							} else {
								part_code.ViewValue = part_code.CurrentValue;
							}
						}
					} else {
						part_code.ViewValue = System.DBNull.Value;
					}

					// part_desc
					part_desc.ViewValue = part_desc.CurrentValue;

					// uom
					uom.ViewValue = uom.CurrentValue;

					// qty
					qty.ViewValue = qty.CurrentValue;
					qty.ViewValue = FormatNumber(qty.ViewValue, 2, -2, -2, -2);

					// unit_price
					unit_price.ViewValue = unit_price.CurrentValue;
					unit_price.ViewValue = FormatNumber(unit_price.ViewValue, 2, -2, -2, -2);

					// amount_origin
					amount_origin.ViewValue = amount_origin.CurrentValue;
					amount_origin.ViewValue = FormatNumber(amount_origin.ViewValue, 2, -2, -2, -2);

					// amount_local
					amount_local.ViewValue = amount_local.CurrentValue;
					amount_local.ViewValue = FormatNumber(amount_local.ViewValue, 2, -2, -2, -2);

					// tax_code
					tax_code.ViewValue = tax_code.CurrentValue;

					// tax_rate
					tax_rate.ViewValue = tax_rate.CurrentValue;
					tax_rate.ViewValue = FormatNumber(tax_rate.ViewValue, 2, -2, -2, -2);

					// tax_amount_origin
					tax_amount_origin.ViewValue = tax_amount_origin.CurrentValue;
					tax_amount_origin.ViewValue = FormatNumber(tax_amount_origin.ViewValue, 2, -2, -2, -2);

					// tax_amount_local
					tax_amount_local.ViewValue = tax_amount_local.CurrentValue;
					tax_amount_local.ViewValue = FormatNumber(tax_amount_local.ViewValue, 2, -2, -2, -2);

					// TrxUserId
					TrxUserId.ViewValue = TrxUserId.CurrentValue;
					TrxUserId.ViewValue = FormatNumber(TrxUserId.ViewValue, 0, -2, -2, -2);

					// to_gl_acct
					to_gl_acct.ViewValue = to_gl_acct.CurrentValue;

					// tax_acct
					tax_acct.ViewValue = tax_acct.CurrentValue;

					// TrxId
					TrxId.HrefValue = "";
					TrxId.TooltipValue = "";

					// Id_dohdrTrxId
					Id_dohdrTrxId.HrefValue = "";
					Id_dohdrTrxId.TooltipValue = "";

					// item_no
					item_no.HrefValue = "";
					item_no.TooltipValue = "";

					// item_type
					item_type.HrefValue = "";
					item_type.TooltipValue = "";

					// Id_sodetlTrxId
					Id_sodetlTrxId.HrefValue = "";
					Id_sodetlTrxId.TooltipValue = "";

					// Id_podetlTrxId
					Id_podetlTrxId.HrefValue = "";
					Id_podetlTrxId.TooltipValue = "";

					// part_code
					part_code.HrefValue = "";
					part_code.TooltipValue = "";

					// part_desc
					part_desc.HrefValue = "";
					part_desc.TooltipValue = "";

					// uom
					uom.HrefValue = "";
					uom.TooltipValue = "";

					// qty
					qty.HrefValue = "";
					qty.TooltipValue = "";

					// unit_price
					unit_price.HrefValue = "";
					unit_price.TooltipValue = "";

					// amount_origin
					amount_origin.HrefValue = "";
					amount_origin.TooltipValue = "";

					// amount_local
					amount_local.HrefValue = "";
					amount_local.TooltipValue = "";

					// tax_code
					tax_code.HrefValue = "";
					tax_code.TooltipValue = "";

					// tax_rate
					tax_rate.HrefValue = "";
					tax_rate.TooltipValue = "";

					// tax_amount_origin
					tax_amount_origin.HrefValue = "";
					tax_amount_origin.TooltipValue = "";

					// tax_amount_local
					tax_amount_local.HrefValue = "";
					tax_amount_local.TooltipValue = "";

					// TrxUserId
					TrxUserId.HrefValue = "";
					TrxUserId.TooltipValue = "";

					// to_gl_acct
					to_gl_acct.HrefValue = "";
					to_gl_acct.TooltipValue = "";

					// tax_acct
					tax_acct.HrefValue = "";
					tax_acct.TooltipValue = "";
				} else if (RowType == Config.RowTypeEdit) { // Edit row

					// TrxId
					TrxId.EditAttrs["class"] = "form-control";
					TrxId.EditValue = TrxId.CurrentValue;

					// Id_dohdrTrxId
					Id_dohdrTrxId.EditAttrs["class"] = "form-control";
					if (!Empty(Id_dohdrTrxId.SessionValue)) {
						Id_dohdrTrxId.CurrentValue = Id_dohdrTrxId.SessionValue;
					Id_dohdrTrxId.ViewValue = Id_dohdrTrxId.CurrentValue;
					Id_dohdrTrxId.ViewValue = FormatNumber(Id_dohdrTrxId.ViewValue, 0, -2, -2, -2);
					} else {
					Id_dohdrTrxId.EditValue = Id_dohdrTrxId.CurrentValue; // DN
					Id_dohdrTrxId.PlaceHolder = RemoveHtml(Id_dohdrTrxId.Caption);
					}

					// item_no
					item_no.EditAttrs["class"] = "form-control";
					item_no.EditValue = item_no.CurrentValue; // DN
					item_no.PlaceHolder = RemoveHtml(item_no.Caption);

					// item_type
					item_type.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						item_type.CurrentValue = HtmlDecode(item_type.CurrentValue);
					item_type.EditValue = item_type.CurrentValue; // DN
					item_type.PlaceHolder = RemoveHtml(item_type.Caption);

					// Id_sodetlTrxId
					Id_sodetlTrxId.EditAttrs["class"] = "form-control";
					Id_sodetlTrxId.EditValue = Id_sodetlTrxId.CurrentValue; // DN
					Id_sodetlTrxId.PlaceHolder = RemoveHtml(Id_sodetlTrxId.Caption);

					// Id_podetlTrxId
					Id_podetlTrxId.EditAttrs["class"] = "form-control";
					Id_podetlTrxId.EditValue = Id_podetlTrxId.CurrentValue; // DN
					Id_podetlTrxId.PlaceHolder = RemoveHtml(Id_podetlTrxId.Caption);

					// part_code
					part_code.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						part_code.CurrentValue = HtmlDecode(part_code.CurrentValue);
					part_code.EditValue = part_code.CurrentValue; // DN
					curVal = Convert.ToString(part_code.CurrentValue);
					if (!Empty(curVal)) {
						part_code.EditValue = part_code.LookupCacheOption(curVal);
						if (part_code.EditValue == null) { // Lookup from database
							filterWrk = "[service_code]" + SearchString("=", curVal.Trim(), Config.DataTypeString, "");
							sqlWrk = part_code.Lookup.GetSql(false, filterWrk, null, this);
							rswrk = await Connection.GetRowsAsync(sqlWrk);
							if (rswrk != null && rswrk.Count > 0) { // Lookup values found
								var listwrk = rswrk[0].Values.ToList();
								listwrk[1] = Convert.ToString(HtmlEncode(listwrk[1]));
								listwrk[2] = Convert.ToString(HtmlEncode(listwrk[2]));
								part_code.EditValue = part_code.DisplayValue(listwrk);
							} else {
								part_code.EditValue = HtmlEncode(part_code.CurrentValue);
							}
						}
					} else {
						part_code.EditValue = System.DBNull.Value;
					}
					part_code.PlaceHolder = RemoveHtml(part_code.Caption);

					// part_desc
					part_desc.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						part_desc.CurrentValue = HtmlDecode(part_desc.CurrentValue);
					part_desc.EditValue = part_desc.CurrentValue; // DN
					part_desc.PlaceHolder = RemoveHtml(part_desc.Caption);

					// uom
					uom.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						uom.CurrentValue = HtmlDecode(uom.CurrentValue);
					uom.EditValue = uom.CurrentValue; // DN
					uom.PlaceHolder = RemoveHtml(uom.Caption);

					// qty
					qty.EditAttrs["class"] = "form-control";
					qty.EditValue = qty.CurrentValue; // DN
					qty.PlaceHolder = RemoveHtml(qty.Caption);
					if (!Empty(qty.EditValue) && IsNumeric(qty.EditValue))
						qty.EditValue = FormatNumber(qty.EditValue, -2, -2, -2, -2);

					// unit_price
					unit_price.EditAttrs["class"] = "form-control";
					unit_price.EditValue = unit_price.CurrentValue; // DN
					unit_price.PlaceHolder = RemoveHtml(unit_price.Caption);
					if (!Empty(unit_price.EditValue) && IsNumeric(unit_price.EditValue))
						unit_price.EditValue = FormatNumber(unit_price.EditValue, -2, -2, -2, -2);

					// amount_origin
					amount_origin.EditAttrs["class"] = "form-control";
					amount_origin.EditValue = amount_origin.CurrentValue; // DN
					amount_origin.PlaceHolder = RemoveHtml(amount_origin.Caption);
					if (!Empty(amount_origin.EditValue) && IsNumeric(amount_origin.EditValue))
						amount_origin.EditValue = FormatNumber(amount_origin.EditValue, -2, -2, -2, -2);

					// amount_local
					amount_local.EditAttrs["class"] = "form-control";
					amount_local.EditValue = amount_local.CurrentValue; // DN
					amount_local.PlaceHolder = RemoveHtml(amount_local.Caption);
					if (!Empty(amount_local.EditValue) && IsNumeric(amount_local.EditValue))
						amount_local.EditValue = FormatNumber(amount_local.EditValue, -2, -2, -2, -2);

					// tax_code
					tax_code.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						tax_code.CurrentValue = HtmlDecode(tax_code.CurrentValue);
					tax_code.EditValue = tax_code.CurrentValue; // DN
					tax_code.PlaceHolder = RemoveHtml(tax_code.Caption);

					// tax_rate
					tax_rate.EditAttrs["class"] = "form-control";
					tax_rate.EditValue = tax_rate.CurrentValue; // DN
					tax_rate.PlaceHolder = RemoveHtml(tax_rate.Caption);
					if (!Empty(tax_rate.EditValue) && IsNumeric(tax_rate.EditValue))
						tax_rate.EditValue = FormatNumber(tax_rate.EditValue, -2, -2, -2, -2);

					// tax_amount_origin
					tax_amount_origin.EditAttrs["class"] = "form-control";
					tax_amount_origin.EditValue = tax_amount_origin.CurrentValue; // DN
					tax_amount_origin.PlaceHolder = RemoveHtml(tax_amount_origin.Caption);
					if (!Empty(tax_amount_origin.EditValue) && IsNumeric(tax_amount_origin.EditValue))
						tax_amount_origin.EditValue = FormatNumber(tax_amount_origin.EditValue, -2, -2, -2, -2);

					// tax_amount_local
					tax_amount_local.EditAttrs["class"] = "form-control";
					tax_amount_local.EditValue = tax_amount_local.CurrentValue; // DN
					tax_amount_local.PlaceHolder = RemoveHtml(tax_amount_local.Caption);
					if (!Empty(tax_amount_local.EditValue) && IsNumeric(tax_amount_local.EditValue))
						tax_amount_local.EditValue = FormatNumber(tax_amount_local.EditValue, -2, -2, -2, -2);

					// TrxUserId
					TrxUserId.EditAttrs["class"] = "form-control";
					TrxUserId.EditValue = TrxUserId.CurrentValue; // DN
					TrxUserId.PlaceHolder = RemoveHtml(TrxUserId.Caption);

					// to_gl_acct
					to_gl_acct.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						to_gl_acct.CurrentValue = HtmlDecode(to_gl_acct.CurrentValue);
					to_gl_acct.EditValue = to_gl_acct.CurrentValue; // DN
					to_gl_acct.PlaceHolder = RemoveHtml(to_gl_acct.Caption);

					// tax_acct
					tax_acct.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						tax_acct.CurrentValue = HtmlDecode(tax_acct.CurrentValue);
					tax_acct.EditValue = tax_acct.CurrentValue; // DN
					tax_acct.PlaceHolder = RemoveHtml(tax_acct.Caption);

					// Edit refer script
					// TrxId

					TrxId.HrefValue = "";

					// Id_dohdrTrxId
					Id_dohdrTrxId.HrefValue = "";

					// item_no
					item_no.HrefValue = "";

					// item_type
					item_type.HrefValue = "";

					// Id_sodetlTrxId
					Id_sodetlTrxId.HrefValue = "";

					// Id_podetlTrxId
					Id_podetlTrxId.HrefValue = "";

					// part_code
					part_code.HrefValue = "";

					// part_desc
					part_desc.HrefValue = "";

					// uom
					uom.HrefValue = "";

					// qty
					qty.HrefValue = "";

					// unit_price
					unit_price.HrefValue = "";

					// amount_origin
					amount_origin.HrefValue = "";

					// amount_local
					amount_local.HrefValue = "";

					// tax_code
					tax_code.HrefValue = "";

					// tax_rate
					tax_rate.HrefValue = "";

					// tax_amount_origin
					tax_amount_origin.HrefValue = "";

					// tax_amount_local
					tax_amount_local.HrefValue = "";

					// TrxUserId
					TrxUserId.HrefValue = "";

					// to_gl_acct
					to_gl_acct.HrefValue = "";

					// tax_acct
					tax_acct.HrefValue = "";
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
				if (Id_dohdrTrxId.Required) {
					if (!Id_dohdrTrxId.IsDetailKey && Empty(Id_dohdrTrxId.FormValue))
						FormError = AddMessage(FormError, Id_dohdrTrxId.RequiredErrorMessage.Replace("%s", Id_dohdrTrxId.Caption));
				}
				if (!CheckInteger(Id_dohdrTrxId.FormValue))
					FormError = AddMessage(FormError, Id_dohdrTrxId.ErrorMessage);
				if (item_no.Required) {
					if (!item_no.IsDetailKey && Empty(item_no.FormValue))
						FormError = AddMessage(FormError, item_no.RequiredErrorMessage.Replace("%s", item_no.Caption));
				}
				if (!CheckInteger(item_no.FormValue))
					FormError = AddMessage(FormError, item_no.ErrorMessage);
				if (item_type.Required) {
					if (!item_type.IsDetailKey && Empty(item_type.FormValue))
						FormError = AddMessage(FormError, item_type.RequiredErrorMessage.Replace("%s", item_type.Caption));
				}
				if (Id_sodetlTrxId.Required) {
					if (!Id_sodetlTrxId.IsDetailKey && Empty(Id_sodetlTrxId.FormValue))
						FormError = AddMessage(FormError, Id_sodetlTrxId.RequiredErrorMessage.Replace("%s", Id_sodetlTrxId.Caption));
				}
				if (!CheckInteger(Id_sodetlTrxId.FormValue))
					FormError = AddMessage(FormError, Id_sodetlTrxId.ErrorMessage);
				if (Id_podetlTrxId.Required) {
					if (!Id_podetlTrxId.IsDetailKey && Empty(Id_podetlTrxId.FormValue))
						FormError = AddMessage(FormError, Id_podetlTrxId.RequiredErrorMessage.Replace("%s", Id_podetlTrxId.Caption));
				}
				if (!CheckInteger(Id_podetlTrxId.FormValue))
					FormError = AddMessage(FormError, Id_podetlTrxId.ErrorMessage);
				if (part_code.Required) {
					if (!part_code.IsDetailKey && Empty(part_code.FormValue))
						FormError = AddMessage(FormError, part_code.RequiredErrorMessage.Replace("%s", part_code.Caption));
				}
				if (part_desc.Required) {
					if (!part_desc.IsDetailKey && Empty(part_desc.FormValue))
						FormError = AddMessage(FormError, part_desc.RequiredErrorMessage.Replace("%s", part_desc.Caption));
				}
				if (uom.Required) {
					if (!uom.IsDetailKey && Empty(uom.FormValue))
						FormError = AddMessage(FormError, uom.RequiredErrorMessage.Replace("%s", uom.Caption));
				}
				if (qty.Required) {
					if (!qty.IsDetailKey && Empty(qty.FormValue))
						FormError = AddMessage(FormError, qty.RequiredErrorMessage.Replace("%s", qty.Caption));
				}
				if (!CheckNumber(qty.FormValue))
					FormError = AddMessage(FormError, qty.ErrorMessage);
				if (unit_price.Required) {
					if (!unit_price.IsDetailKey && Empty(unit_price.FormValue))
						FormError = AddMessage(FormError, unit_price.RequiredErrorMessage.Replace("%s", unit_price.Caption));
				}
				if (!CheckNumber(unit_price.FormValue))
					FormError = AddMessage(FormError, unit_price.ErrorMessage);
				if (amount_origin.Required) {
					if (!amount_origin.IsDetailKey && Empty(amount_origin.FormValue))
						FormError = AddMessage(FormError, amount_origin.RequiredErrorMessage.Replace("%s", amount_origin.Caption));
				}
				if (!CheckNumber(amount_origin.FormValue))
					FormError = AddMessage(FormError, amount_origin.ErrorMessage);
				if (amount_local.Required) {
					if (!amount_local.IsDetailKey && Empty(amount_local.FormValue))
						FormError = AddMessage(FormError, amount_local.RequiredErrorMessage.Replace("%s", amount_local.Caption));
				}
				if (!CheckNumber(amount_local.FormValue))
					FormError = AddMessage(FormError, amount_local.ErrorMessage);
				if (tax_code.Required) {
					if (!tax_code.IsDetailKey && Empty(tax_code.FormValue))
						FormError = AddMessage(FormError, tax_code.RequiredErrorMessage.Replace("%s", tax_code.Caption));
				}
				if (tax_rate.Required) {
					if (!tax_rate.IsDetailKey && Empty(tax_rate.FormValue))
						FormError = AddMessage(FormError, tax_rate.RequiredErrorMessage.Replace("%s", tax_rate.Caption));
				}
				if (!CheckNumber(tax_rate.FormValue))
					FormError = AddMessage(FormError, tax_rate.ErrorMessage);
				if (tax_amount_origin.Required) {
					if (!tax_amount_origin.IsDetailKey && Empty(tax_amount_origin.FormValue))
						FormError = AddMessage(FormError, tax_amount_origin.RequiredErrorMessage.Replace("%s", tax_amount_origin.Caption));
				}
				if (!CheckNumber(tax_amount_origin.FormValue))
					FormError = AddMessage(FormError, tax_amount_origin.ErrorMessage);
				if (tax_amount_local.Required) {
					if (!tax_amount_local.IsDetailKey && Empty(tax_amount_local.FormValue))
						FormError = AddMessage(FormError, tax_amount_local.RequiredErrorMessage.Replace("%s", tax_amount_local.Caption));
				}
				if (!CheckNumber(tax_amount_local.FormValue))
					FormError = AddMessage(FormError, tax_amount_local.ErrorMessage);
				if (TrxUserId.Required) {
					if (!TrxUserId.IsDetailKey && Empty(TrxUserId.FormValue))
						FormError = AddMessage(FormError, TrxUserId.RequiredErrorMessage.Replace("%s", TrxUserId.Caption));
				}
				if (!CheckInteger(TrxUserId.FormValue))
					FormError = AddMessage(FormError, TrxUserId.ErrorMessage);
				if (to_gl_acct.Required) {
					if (!to_gl_acct.IsDetailKey && Empty(to_gl_acct.FormValue))
						FormError = AddMessage(FormError, to_gl_acct.RequiredErrorMessage.Replace("%s", to_gl_acct.Caption));
				}
				if (tax_acct.Required) {
					if (!tax_acct.IsDetailKey && Empty(tax_acct.FormValue))
						FormError = AddMessage(FormError, tax_acct.RequiredErrorMessage.Replace("%s", tax_acct.Caption));
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

				// Id_dohdrTrxId
				Id_dohdrTrxId.SetDbValue(rsnew, Id_dohdrTrxId.CurrentValue, 0, Id_dohdrTrxId.ReadOnly);

				// item_no
				item_no.SetDbValue(rsnew, item_no.CurrentValue, 0, item_no.ReadOnly);

				// item_type
				item_type.SetDbValue(rsnew, item_type.CurrentValue, "", item_type.ReadOnly);

				// Id_sodetlTrxId
				Id_sodetlTrxId.SetDbValue(rsnew, Id_sodetlTrxId.CurrentValue, System.DBNull.Value, Id_sodetlTrxId.ReadOnly);

				// Id_podetlTrxId
				Id_podetlTrxId.SetDbValue(rsnew, Id_podetlTrxId.CurrentValue, System.DBNull.Value, Id_podetlTrxId.ReadOnly);

				// part_code
				part_code.SetDbValue(rsnew, part_code.CurrentValue, "", part_code.ReadOnly);

				// part_desc
				part_desc.SetDbValue(rsnew, part_desc.CurrentValue, "", part_desc.ReadOnly);

				// uom
				uom.SetDbValue(rsnew, uom.CurrentValue, "", uom.ReadOnly);

				// qty
				qty.SetDbValue(rsnew, qty.CurrentValue, 0, qty.ReadOnly);

				// unit_price
				unit_price.SetDbValue(rsnew, unit_price.CurrentValue, System.DBNull.Value, unit_price.ReadOnly);

				// amount_origin
				amount_origin.SetDbValue(rsnew, amount_origin.CurrentValue, System.DBNull.Value, amount_origin.ReadOnly);

				// amount_local
				amount_local.SetDbValue(rsnew, amount_local.CurrentValue, System.DBNull.Value, amount_local.ReadOnly);

				// tax_code
				tax_code.SetDbValue(rsnew, tax_code.CurrentValue, System.DBNull.Value, tax_code.ReadOnly);

				// tax_rate
				tax_rate.SetDbValue(rsnew, tax_rate.CurrentValue, System.DBNull.Value, tax_rate.ReadOnly);

				// tax_amount_origin
				tax_amount_origin.SetDbValue(rsnew, tax_amount_origin.CurrentValue, System.DBNull.Value, tax_amount_origin.ReadOnly);

				// tax_amount_local
				tax_amount_local.SetDbValue(rsnew, tax_amount_local.CurrentValue, System.DBNull.Value, tax_amount_local.ReadOnly);

				// TrxUserId
				TrxUserId.SetDbValue(rsnew, TrxUserId.CurrentValue, 0, TrxUserId.ReadOnly);

				// to_gl_acct
				to_gl_acct.SetDbValue(rsnew, to_gl_acct.CurrentValue, System.DBNull.Value, to_gl_acct.ReadOnly);

				// tax_acct
				tax_acct.SetDbValue(rsnew, tax_acct.CurrentValue, System.DBNull.Value, tax_acct.ReadOnly);
				bool validMasterRecord;
				object keyValue, v;
				string masterFilter;

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

			// Set up master/detail based on QueryString
			protected void SetupMasterParms() {
				bool validMaster = false;
				StringValues masterTblVar = "";

				// Get the keys for master table
				if (Query.TryGetValue(Config.TableShowMaster, out masterTblVar)) { // Do not use Get()
					if (Empty(masterTblVar)) {
						validMaster = true;
						DbMasterFilter = "";
						DbDetailFilter = "";
					}
					if (masterTblVar == "s_dohdrtest") {
						validMaster = true;
						if (!Empty(Get("fk_TrxId"))) {
							s_dohdrtest.TrxId.QueryValue = Get("fk_TrxId");
							Id_dohdrTrxId.QueryValue = s_dohdrtest.TrxId.QueryValue;
							Id_dohdrTrxId.SessionValue = Id_dohdrTrxId.QueryValue;
							if (!IsNumeric(Id_dohdrTrxId.QueryValue))
								validMaster = false;
						} else {
							validMaster = false;
						}
					}
				} else if (Form.TryGetValue(Config.TableShowMaster, out masterTblVar)) {
					if (masterTblVar == "") {
						validMaster = true;
						DbMasterFilter = "";
						DbDetailFilter = "";
					}
					if (masterTblVar == "s_dohdrtest") {
						validMaster = true;
					if (Post("fk_TrxId") != "") {
						s_dohdrtest.TrxId.FormValue = Post("fk_TrxId");
						Id_dohdrTrxId.FormValue = s_dohdrtest.TrxId.FormValue;
						Id_dohdrTrxId.SessionValue = Id_dohdrTrxId.FormValue;
						if (!IsNumeric(Id_dohdrTrxId.FormValue))
							validMaster = false;
					} else {
						validMaster = false;
					}
				}
				}
				if (validMaster) {

					// Save current master table
					CurrentMasterTable = masterTblVar;
					SessionWhere = DetailFilter;

					// Clear previous master key from Session
					if (masterTblVar != "s_dohdrtest") {
						if (Empty(Id_dohdrTrxId.CurrentValue)) 
							Id_dohdrTrxId.SessionValue = "";
					}
				}
				DbMasterFilter = MasterFilter; // Get master filter
				DbDetailFilter = DetailFilter; // Get detail filter
			}

			// Set up Breadcrumb
			protected void SetupBreadcrumb() {
				var breadcrumb = new Breadcrumb();
				string url = CurrentUrl();
				breadcrumb.Add("list", TableVar, AppPath(AddMasterUrl("s_dodetltestlist")), "", TableVar, true);
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
									case "x_part_code":
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
