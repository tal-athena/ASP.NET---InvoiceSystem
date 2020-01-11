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
		/// s_currency_Edit
		/// </summary>

		public static _s_currency_Edit s_currency_Edit {
			get => HttpData.Get<_s_currency_Edit>("s_currency_Edit");
			set => HttpData["s_currency_Edit"] = value;
		}

		/// <summary>
		/// Page class for s_currency
		/// </summary>

		public class _s_currency_Edit : _s_currency_EditBase
		{

			// Construtor
			public _s_currency_Edit(Controller controller = null) : base(controller) {
			}
		}

		/// <summary>
		/// Page base class
		/// </summary>

		public class _s_currency_EditBase : _s_currency, IAspNetMakerPage
		{

			// Page ID
			public string PageID = "edit";

			// Project ID
			public string ProjectID = "{8543F230-11C6-4105-B51C-8D87C21BE659}";

			// Table name
			public string TableName = "s_currency";

			// Page object name
			public string PageObjName = "s_currency_Edit";

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
			public _s_currency_EditBase(Controller controller = null) { // DN
				if (controller != null)
					Controller = controller;

				// Initialize
				CurrentPage = this;

				// Language object
				Language = Language ?? new Lang();

				// Table object (s_currency)
				if (s_currency == null || s_currency is _s_currency)
					s_currency = this;

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
					dynamic doc = CreateInstance(classname, new object[] { s_currency, "" }); // DN
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
								if (pageName == "s_currencyview")
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
							return Terminate(GetUrl("s_currencylist"));
						else
							return Terminate(GetUrl("login"));
					}
				}

				// Create form object
				CurrentForm = new HttpForm();
				CurrentAction = Param("action"); // Set up current action
				Id.SetVisibility();
				CurrencyCode.SetVisibility();
				CurrencyName.SetVisibility();
				PrimaryCurrency.SetVisibility();
				Rate.SetVisibility();
				DisplayLocale.SetVisibility();
				CustomFormatting.SetVisibility();
				Published.SetVisibility();
				UpdatedOnUtc.SetVisibility();
				RoundingTypeId.SetVisibility();
				DisplayOrder.SetVisibility();
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
							return Terminate("s_currencylist"); // No matching record, return to list
						}
						break;
					case "update": // Update // DN
						CloseRecordset(); // DN
						string returnUrl = ReturnUrl;
						if (GetPageName(returnUrl) == "s_currencylist")
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

				// Check field name 'CurrencyCode' first before field var 'x_CurrencyCode'
				val = CurrentForm.GetValue("CurrencyCode", "x_CurrencyCode");
				if (!CurrencyCode.IsDetailKey) {
					if (IsApi() && val == null)
						CurrencyCode.Visible = false; // Disable update for API request
					else
						CurrencyCode.FormValue = val;
				}

				// Check field name 'CurrencyName' first before field var 'x_CurrencyName'
				val = CurrentForm.GetValue("CurrencyName", "x_CurrencyName");
				if (!CurrencyName.IsDetailKey) {
					if (IsApi() && val == null)
						CurrencyName.Visible = false; // Disable update for API request
					else
						CurrencyName.FormValue = val;
				}

				// Check field name 'PrimaryCurrency' first before field var 'x_PrimaryCurrency'
				val = CurrentForm.GetValue("PrimaryCurrency", "x_PrimaryCurrency");
				if (!PrimaryCurrency.IsDetailKey) {
					if (IsApi() && val == null)
						PrimaryCurrency.Visible = false; // Disable update for API request
					else
						PrimaryCurrency.FormValue = val;
				}

				// Check field name 'Rate' first before field var 'x_Rate'
				val = CurrentForm.GetValue("Rate", "x_Rate");
				if (!Rate.IsDetailKey) {
					if (IsApi() && val == null)
						Rate.Visible = false; // Disable update for API request
					else
						Rate.FormValue = val;
				}

				// Check field name 'DisplayLocale' first before field var 'x_DisplayLocale'
				val = CurrentForm.GetValue("DisplayLocale", "x_DisplayLocale");
				if (!DisplayLocale.IsDetailKey) {
					if (IsApi() && val == null)
						DisplayLocale.Visible = false; // Disable update for API request
					else
						DisplayLocale.FormValue = val;
				}

				// Check field name 'CustomFormatting' first before field var 'x_CustomFormatting'
				val = CurrentForm.GetValue("CustomFormatting", "x_CustomFormatting");
				if (!CustomFormatting.IsDetailKey) {
					if (IsApi() && val == null)
						CustomFormatting.Visible = false; // Disable update for API request
					else
						CustomFormatting.FormValue = val;
				}

				// Check field name 'Published' first before field var 'x_Published'
				val = CurrentForm.GetValue("Published", "x_Published");
				if (!Published.IsDetailKey) {
					if (IsApi() && val == null)
						Published.Visible = false; // Disable update for API request
					else
						Published.FormValue = val;
				}

				// Check field name 'UpdatedOnUtc' first before field var 'x_UpdatedOnUtc'
				val = CurrentForm.GetValue("UpdatedOnUtc", "x_UpdatedOnUtc");
				if (!UpdatedOnUtc.IsDetailKey) {
					if (IsApi() && val == null)
						UpdatedOnUtc.Visible = false; // Disable update for API request
					else
						UpdatedOnUtc.FormValue = val;
					UpdatedOnUtc.CurrentValue = UnformatDateTime(UpdatedOnUtc.CurrentValue, 0);
				}

				// Check field name 'RoundingTypeId' first before field var 'x_RoundingTypeId'
				val = CurrentForm.GetValue("RoundingTypeId", "x_RoundingTypeId");
				if (!RoundingTypeId.IsDetailKey) {
					if (IsApi() && val == null)
						RoundingTypeId.Visible = false; // Disable update for API request
					else
						RoundingTypeId.FormValue = val;
				}

				// Check field name 'DisplayOrder' first before field var 'x_DisplayOrder'
				val = CurrentForm.GetValue("DisplayOrder", "x_DisplayOrder");
				if (!DisplayOrder.IsDetailKey) {
					if (IsApi() && val == null)
						DisplayOrder.Visible = false; // Disable update for API request
					else
						DisplayOrder.FormValue = val;
				}
			}

			#pragma warning restore 1998

			// Restore form values
			public void RestoreFormValues() {
				Id.CurrentValue = Id.FormValue;
				CurrencyCode.CurrentValue = CurrencyCode.FormValue;
				CurrencyName.CurrentValue = CurrencyName.FormValue;
				PrimaryCurrency.CurrentValue = PrimaryCurrency.FormValue;
				Rate.CurrentValue = Rate.FormValue;
				DisplayLocale.CurrentValue = DisplayLocale.FormValue;
				CustomFormatting.CurrentValue = CustomFormatting.FormValue;
				Published.CurrentValue = Published.FormValue;
				UpdatedOnUtc.CurrentValue = UpdatedOnUtc.FormValue;
				UpdatedOnUtc.CurrentValue = UnformatDateTime(UpdatedOnUtc.CurrentValue, 0);
				RoundingTypeId.CurrentValue = RoundingTypeId.FormValue;
				DisplayOrder.CurrentValue = DisplayOrder.FormValue;
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
				CurrencyCode.SetDbValue(row["CurrencyCode"]);
				CurrencyName.SetDbValue(row["CurrencyName"]);
				PrimaryCurrency.SetDbValue((ConvertToBool(row["PrimaryCurrency"]) ? "1" : "0"));
				Rate.SetDbValue(row["Rate"]);
				DisplayLocale.SetDbValue(row["DisplayLocale"]);
				CustomFormatting.SetDbValue(row["CustomFormatting"]);
				Published.SetDbValue((ConvertToBool(row["Published"]) ? "1" : "0"));
				UpdatedOnUtc.SetDbValue(row["UpdatedOnUtc"]);
				RoundingTypeId.SetDbValue(row["RoundingTypeId"]);
				DisplayOrder.SetDbValue(row["DisplayOrder"]);
			}

			#pragma warning restore 162, 168, 1998

			// Return a row with default values
			protected Dictionary<string, object> NewRow() {
				var row = new Dictionary<string, object>();
				row.Add("Id", System.DBNull.Value);
				row.Add("CurrencyCode", System.DBNull.Value);
				row.Add("CurrencyName", System.DBNull.Value);
				row.Add("PrimaryCurrency", System.DBNull.Value);
				row.Add("Rate", System.DBNull.Value);
				row.Add("DisplayLocale", System.DBNull.Value);
				row.Add("CustomFormatting", System.DBNull.Value);
				row.Add("Published", System.DBNull.Value);
				row.Add("UpdatedOnUtc", System.DBNull.Value);
				row.Add("RoundingTypeId", System.DBNull.Value);
				row.Add("DisplayOrder", System.DBNull.Value);
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
				if (SameString(Rate.FormValue, Rate.CurrentValue) && IsNumeric(ConvertToFloatString(Rate.CurrentValue)))
					Rate.CurrentValue = ConvertToFloatString(Rate.CurrentValue);

				// Call Row_Rendering event
				Row_Rendering();

				// Common render codes for all row types
				// Id
				// CurrencyCode
				// CurrencyName
				// PrimaryCurrency
				// Rate
				// DisplayLocale
				// CustomFormatting
				// Published
				// UpdatedOnUtc
				// RoundingTypeId
				// DisplayOrder

				if (RowType == Config.RowTypeView) { // View row

					// Id
					Id.ViewValue = Id.CurrentValue;

					// CurrencyCode
					CurrencyCode.ViewValue = CurrencyCode.CurrentValue;

					// CurrencyName
					CurrencyName.ViewValue = CurrencyName.CurrentValue;

					// PrimaryCurrency
					if (ConvertToBool(PrimaryCurrency.CurrentValue)) {
						PrimaryCurrency.ViewValue = (PrimaryCurrency.TagCaption(1) != "") ? PrimaryCurrency.TagCaption(1) : "Yes";
					} else {
						PrimaryCurrency.ViewValue = (PrimaryCurrency.TagCaption(2) != "") ? PrimaryCurrency.TagCaption(2) : "No";
					}

					// Rate
					Rate.ViewValue = Rate.CurrentValue;
					Rate.ViewValue = FormatNumber(Rate.ViewValue, 2, -2, -2, -2);

					// DisplayLocale
					DisplayLocale.ViewValue = DisplayLocale.CurrentValue;

					// CustomFormatting
					CustomFormatting.ViewValue = CustomFormatting.CurrentValue;

					// Published
					if (ConvertToBool(Published.CurrentValue)) {
						Published.ViewValue = (Published.TagCaption(1) != "") ? Published.TagCaption(1) : "Yes";
					} else {
						Published.ViewValue = (Published.TagCaption(2) != "") ? Published.TagCaption(2) : "No";
					}

					// UpdatedOnUtc
					UpdatedOnUtc.ViewValue = UpdatedOnUtc.CurrentValue;
					UpdatedOnUtc.ViewValue = FormatDateTime(UpdatedOnUtc.ViewValue, 0);

					// RoundingTypeId
					RoundingTypeId.ViewValue = RoundingTypeId.CurrentValue;
					RoundingTypeId.ViewValue = FormatNumber(RoundingTypeId.ViewValue, 0, -2, -2, -2);

					// DisplayOrder
					DisplayOrder.ViewValue = DisplayOrder.CurrentValue;
					DisplayOrder.ViewValue = FormatNumber(DisplayOrder.ViewValue, 0, -2, -2, -2);

					// Id
					Id.HrefValue = "";
					Id.TooltipValue = "";

					// CurrencyCode
					CurrencyCode.HrefValue = "";
					CurrencyCode.TooltipValue = "";

					// CurrencyName
					CurrencyName.HrefValue = "";
					CurrencyName.TooltipValue = "";

					// PrimaryCurrency
					PrimaryCurrency.HrefValue = "";
					PrimaryCurrency.TooltipValue = "";

					// Rate
					Rate.HrefValue = "";
					Rate.TooltipValue = "";

					// DisplayLocale
					DisplayLocale.HrefValue = "";
					DisplayLocale.TooltipValue = "";

					// CustomFormatting
					CustomFormatting.HrefValue = "";
					CustomFormatting.TooltipValue = "";

					// Published
					Published.HrefValue = "";
					Published.TooltipValue = "";

					// UpdatedOnUtc
					UpdatedOnUtc.HrefValue = "";
					UpdatedOnUtc.TooltipValue = "";

					// RoundingTypeId
					RoundingTypeId.HrefValue = "";
					RoundingTypeId.TooltipValue = "";

					// DisplayOrder
					DisplayOrder.HrefValue = "";
					DisplayOrder.TooltipValue = "";
				} else if (RowType == Config.RowTypeEdit) { // Edit row

					// Id
					Id.EditAttrs["class"] = "form-control";
					Id.EditValue = Id.CurrentValue;

					// CurrencyCode
					CurrencyCode.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						CurrencyCode.CurrentValue = HtmlDecode(CurrencyCode.CurrentValue);
					CurrencyCode.EditValue = CurrencyCode.CurrentValue; // DN
					CurrencyCode.PlaceHolder = RemoveHtml(CurrencyCode.Caption);

					// CurrencyName
					CurrencyName.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						CurrencyName.CurrentValue = HtmlDecode(CurrencyName.CurrentValue);
					CurrencyName.EditValue = CurrencyName.CurrentValue; // DN
					CurrencyName.PlaceHolder = RemoveHtml(CurrencyName.Caption);

					// PrimaryCurrency
					PrimaryCurrency.EditValue = PrimaryCurrency.Options(false);

					// Rate
					Rate.EditAttrs["class"] = "form-control";
					Rate.EditValue = Rate.CurrentValue; // DN
					Rate.PlaceHolder = RemoveHtml(Rate.Caption);
					if (!Empty(Rate.EditValue) && IsNumeric(Rate.EditValue))
						Rate.EditValue = FormatNumber(Rate.EditValue, -2, -2, -2, -2);

					// DisplayLocale
					DisplayLocale.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						DisplayLocale.CurrentValue = HtmlDecode(DisplayLocale.CurrentValue);
					DisplayLocale.EditValue = DisplayLocale.CurrentValue; // DN
					DisplayLocale.PlaceHolder = RemoveHtml(DisplayLocale.Caption);

					// CustomFormatting
					CustomFormatting.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						CustomFormatting.CurrentValue = HtmlDecode(CustomFormatting.CurrentValue);
					CustomFormatting.EditValue = CustomFormatting.CurrentValue; // DN
					CustomFormatting.PlaceHolder = RemoveHtml(CustomFormatting.Caption);

					// Published
					Published.EditValue = Published.Options(false);

					// UpdatedOnUtc
					UpdatedOnUtc.EditAttrs["class"] = "form-control";
					UpdatedOnUtc.EditValue = FormatDateTime(UpdatedOnUtc.CurrentValue, 8); // DN
					UpdatedOnUtc.PlaceHolder = RemoveHtml(UpdatedOnUtc.Caption);

					// RoundingTypeId
					RoundingTypeId.EditAttrs["class"] = "form-control";
					RoundingTypeId.EditValue = RoundingTypeId.CurrentValue; // DN
					RoundingTypeId.PlaceHolder = RemoveHtml(RoundingTypeId.Caption);

					// DisplayOrder
					DisplayOrder.EditAttrs["class"] = "form-control";
					DisplayOrder.EditValue = DisplayOrder.CurrentValue; // DN
					DisplayOrder.PlaceHolder = RemoveHtml(DisplayOrder.Caption);

					// Edit refer script
					// Id

					Id.HrefValue = "";

					// CurrencyCode
					CurrencyCode.HrefValue = "";

					// CurrencyName
					CurrencyName.HrefValue = "";

					// PrimaryCurrency
					PrimaryCurrency.HrefValue = "";

					// Rate
					Rate.HrefValue = "";

					// DisplayLocale
					DisplayLocale.HrefValue = "";

					// CustomFormatting
					CustomFormatting.HrefValue = "";

					// Published
					Published.HrefValue = "";

					// UpdatedOnUtc
					UpdatedOnUtc.HrefValue = "";

					// RoundingTypeId
					RoundingTypeId.HrefValue = "";

					// DisplayOrder
					DisplayOrder.HrefValue = "";
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
				if (CurrencyCode.Required) {
					if (!CurrencyCode.IsDetailKey && Empty(CurrencyCode.FormValue))
						FormError = AddMessage(FormError, CurrencyCode.RequiredErrorMessage.Replace("%s", CurrencyCode.Caption));
				}
				if (CurrencyName.Required) {
					if (!CurrencyName.IsDetailKey && Empty(CurrencyName.FormValue))
						FormError = AddMessage(FormError, CurrencyName.RequiredErrorMessage.Replace("%s", CurrencyName.Caption));
				}
				if (PrimaryCurrency.Required) {
					if (Empty(PrimaryCurrency.FormValue))
						FormError = AddMessage(FormError, PrimaryCurrency.RequiredErrorMessage.Replace("%s", PrimaryCurrency.Caption));
				}
				if (Rate.Required) {
					if (!Rate.IsDetailKey && Empty(Rate.FormValue))
						FormError = AddMessage(FormError, Rate.RequiredErrorMessage.Replace("%s", Rate.Caption));
				}
				if (!CheckNumber(Rate.FormValue))
					FormError = AddMessage(FormError, Rate.ErrorMessage);
				if (DisplayLocale.Required) {
					if (!DisplayLocale.IsDetailKey && Empty(DisplayLocale.FormValue))
						FormError = AddMessage(FormError, DisplayLocale.RequiredErrorMessage.Replace("%s", DisplayLocale.Caption));
				}
				if (CustomFormatting.Required) {
					if (!CustomFormatting.IsDetailKey && Empty(CustomFormatting.FormValue))
						FormError = AddMessage(FormError, CustomFormatting.RequiredErrorMessage.Replace("%s", CustomFormatting.Caption));
				}
				if (Published.Required) {
					if (Empty(Published.FormValue))
						FormError = AddMessage(FormError, Published.RequiredErrorMessage.Replace("%s", Published.Caption));
				}
				if (UpdatedOnUtc.Required) {
					if (!UpdatedOnUtc.IsDetailKey && Empty(UpdatedOnUtc.FormValue))
						FormError = AddMessage(FormError, UpdatedOnUtc.RequiredErrorMessage.Replace("%s", UpdatedOnUtc.Caption));
				}
				if (!CheckDate(UpdatedOnUtc.FormValue))
					FormError = AddMessage(FormError, UpdatedOnUtc.ErrorMessage);
				if (RoundingTypeId.Required) {
					if (!RoundingTypeId.IsDetailKey && Empty(RoundingTypeId.FormValue))
						FormError = AddMessage(FormError, RoundingTypeId.RequiredErrorMessage.Replace("%s", RoundingTypeId.Caption));
				}
				if (!CheckInteger(RoundingTypeId.FormValue))
					FormError = AddMessage(FormError, RoundingTypeId.ErrorMessage);
				if (DisplayOrder.Required) {
					if (!DisplayOrder.IsDetailKey && Empty(DisplayOrder.FormValue))
						FormError = AddMessage(FormError, DisplayOrder.RequiredErrorMessage.Replace("%s", DisplayOrder.Caption));
				}
				if (!CheckInteger(DisplayOrder.FormValue))
					FormError = AddMessage(FormError, DisplayOrder.ErrorMessage);

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

				// CurrencyCode
				CurrencyCode.SetDbValue(rsnew, CurrencyCode.CurrentValue, System.DBNull.Value, CurrencyCode.ReadOnly);

				// CurrencyName
				CurrencyName.SetDbValue(rsnew, CurrencyName.CurrentValue, System.DBNull.Value, CurrencyName.ReadOnly);

				// PrimaryCurrency
				PrimaryCurrency.SetDbValue(rsnew, ConvertToBool(PrimaryCurrency.CurrentValue, "1", "0"), System.DBNull.Value, PrimaryCurrency.ReadOnly); // DN1204

				// Rate
				Rate.SetDbValue(rsnew, Rate.CurrentValue, System.DBNull.Value, Rate.ReadOnly);

				// DisplayLocale
				DisplayLocale.SetDbValue(rsnew, DisplayLocale.CurrentValue, System.DBNull.Value, DisplayLocale.ReadOnly);

				// CustomFormatting
				CustomFormatting.SetDbValue(rsnew, CustomFormatting.CurrentValue, System.DBNull.Value, CustomFormatting.ReadOnly);

				// Published
				Published.SetDbValue(rsnew, ConvertToBool(Published.CurrentValue, "1", "0"), 0, Published.ReadOnly); // DN1204

				// UpdatedOnUtc
				UpdatedOnUtc.SetDbValue(rsnew, UnformatDateTime(UpdatedOnUtc.CurrentValue, 0), System.DBNull.Value, UpdatedOnUtc.ReadOnly);

				// RoundingTypeId
				RoundingTypeId.SetDbValue(rsnew, RoundingTypeId.CurrentValue, System.DBNull.Value, RoundingTypeId.ReadOnly);

				// DisplayOrder
				DisplayOrder.SetDbValue(rsnew, DisplayOrder.CurrentValue, 0, DisplayOrder.ReadOnly);

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

			// Set up Breadcrumb
			protected void SetupBreadcrumb() {
				var breadcrumb = new Breadcrumb();
				string url = CurrentUrl();
				breadcrumb.Add("list", TableVar, AppPath(AddMasterUrl("s_currencylist")), "", TableVar, true);
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
