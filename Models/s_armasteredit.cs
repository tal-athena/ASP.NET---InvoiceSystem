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
		/// s_armaster_Edit
		/// </summary>

		public static _s_armaster_Edit s_armaster_Edit {
			get => HttpData.Get<_s_armaster_Edit>("s_armaster_Edit");
			set => HttpData["s_armaster_Edit"] = value;
		}

		/// <summary>
		/// Page class for s_armaster
		/// </summary>

		public class _s_armaster_Edit : _s_armaster_EditBase
		{

			// Construtor
			public _s_armaster_Edit(Controller controller = null) : base(controller) {
			}
		}

		/// <summary>
		/// Page base class
		/// </summary>

		public class _s_armaster_EditBase : _s_armaster, IAspNetMakerPage
		{

			// Page ID
			public string PageID = "edit";

			// Project ID
			public string ProjectID = "{8543F230-11C6-4105-B51C-8D87C21BE659}";

			// Table name
			public string TableName = "s_armaster";

			// Page object name
			public string PageObjName = "s_armaster_Edit";

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
			public _s_armaster_EditBase(Controller controller = null) { // DN
				if (controller != null)
					Controller = controller;

				// Initialize
				CurrentPage = this;

				// Language object
				Language = Language ?? new Lang();

				// Table object (s_armaster)
				if (s_armaster == null || s_armaster is _s_armaster)
					s_armaster = this;

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
					dynamic doc = CreateInstance(classname, new object[] { s_armaster, "" }); // DN
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
								if (pageName == "s_armasterview")
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
							return Terminate(GetUrl("s_armasterlist"));
						else
							return Terminate(GetUrl("login"));
					}
				}

				// Create form object
				CurrentForm = new HttpForm();
				CurrentAction = Param("action"); // Set up current action
				Id.SetVisibility();
				dbcode.SetVisibility();
				name.SetVisibility();
				name2.SetVisibility();
				short_name.SetVisibility();
				add1.SetVisibility();
				add2.SetVisibility();
				add3.SetVisibility();
				add4.SetVisibility();
				area.SetVisibility();
				postcode.SetVisibility();
				state.SetVisibility();
				country.SetVisibility();
				tel1.SetVisibility();
				tel2.SetVisibility();
				phone1.SetVisibility();
				phone2.SetVisibility();
				fax.SetVisibility();
				_email.SetVisibility();
				ptc1.SetVisibility();
				ptc.SetVisibility();
				ptc2.SetVisibility();
				ar_grp.SetVisibility();
				term_code.SetVisibility();
				pterm_code.SetVisibility();
				cr_limit.SetVisibility();
				CurrencyCode.SetVisibility();
				slsman.SetVisibility();
				ytd_sale.SetVisibility();
				ytd_cost.SetVisibility();
				ytd_disc.SetVisibility();
				ctrl_acct.SetVisibility();
				ctrl_dept.SetVisibility();
				acct_code.SetVisibility();
				acct_dept.SetVisibility();
				skip_stmt.SetVisibility();
				stop_sale.SetVisibility();
				opn_item.SetVisibility();
				status.SetVisibility();
				tax_desc.SetVisibility();
				stax.SetVisibility();
				remark.SetVisibility();
				inv_fmt.SetVisibility();
				do_fmt.SetVisibility();
				Ship_Code.SetVisibility();
				custtype.SetVisibility();
				Acct_BillAcct.SetVisibility();
				bill_flag.SetVisibility();
				payment_code.SetVisibility();
				stax_pct.SetVisibility();
				db_part.SetVisibility();
				b_code.SetVisibility();
				lmw_no.SetVisibility();
				cs_code.SetVisibility();
				approved.SetVisibility();
				oversea.SetVisibility();
				defa_disc_pct.SetVisibility();
				sellpriceDOM.SetVisibility();
				id_upd.SetVisibility();
				dt_upd.SetVisibility();
				com_regno.SetVisibility();
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
							return Terminate("s_armasterlist"); // No matching record, return to list
						}
						break;
					case "update": // Update // DN
						CloseRecordset(); // DN
						string returnUrl = ReturnUrl;
						if (GetPageName(returnUrl) == "s_armasterlist")
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

				// Check field name 'dbcode' first before field var 'x_dbcode'
				val = CurrentForm.GetValue("dbcode", "x_dbcode");
				if (!dbcode.IsDetailKey) {
					if (IsApi() && val == null)
						dbcode.Visible = false; // Disable update for API request
					else
						dbcode.FormValue = val;
				}

				// Check field name 'name' first before field var 'x_name'
				val = CurrentForm.GetValue("name", "x_name");
				if (!name.IsDetailKey) {
					if (IsApi() && val == null)
						name.Visible = false; // Disable update for API request
					else
						name.FormValue = val;
				}

				// Check field name 'name2' first before field var 'x_name2'
				val = CurrentForm.GetValue("name2", "x_name2");
				if (!name2.IsDetailKey) {
					if (IsApi() && val == null)
						name2.Visible = false; // Disable update for API request
					else
						name2.FormValue = val;
				}

				// Check field name 'short_name' first before field var 'x_short_name'
				val = CurrentForm.GetValue("short_name", "x_short_name");
				if (!short_name.IsDetailKey) {
					if (IsApi() && val == null)
						short_name.Visible = false; // Disable update for API request
					else
						short_name.FormValue = val;
				}

				// Check field name 'add1' first before field var 'x_add1'
				val = CurrentForm.GetValue("add1", "x_add1");
				if (!add1.IsDetailKey) {
					if (IsApi() && val == null)
						add1.Visible = false; // Disable update for API request
					else
						add1.FormValue = val;
				}

				// Check field name 'add2' first before field var 'x_add2'
				val = CurrentForm.GetValue("add2", "x_add2");
				if (!add2.IsDetailKey) {
					if (IsApi() && val == null)
						add2.Visible = false; // Disable update for API request
					else
						add2.FormValue = val;
				}

				// Check field name 'add3' first before field var 'x_add3'
				val = CurrentForm.GetValue("add3", "x_add3");
				if (!add3.IsDetailKey) {
					if (IsApi() && val == null)
						add3.Visible = false; // Disable update for API request
					else
						add3.FormValue = val;
				}

				// Check field name 'add4' first before field var 'x_add4'
				val = CurrentForm.GetValue("add4", "x_add4");
				if (!add4.IsDetailKey) {
					if (IsApi() && val == null)
						add4.Visible = false; // Disable update for API request
					else
						add4.FormValue = val;
				}

				// Check field name 'area' first before field var 'x_area'
				val = CurrentForm.GetValue("area", "x_area");
				if (!area.IsDetailKey) {
					if (IsApi() && val == null)
						area.Visible = false; // Disable update for API request
					else
						area.FormValue = val;
				}

				// Check field name 'postcode' first before field var 'x_postcode'
				val = CurrentForm.GetValue("postcode", "x_postcode");
				if (!postcode.IsDetailKey) {
					if (IsApi() && val == null)
						postcode.Visible = false; // Disable update for API request
					else
						postcode.FormValue = val;
				}

				// Check field name 'state' first before field var 'x_state'
				val = CurrentForm.GetValue("state", "x_state");
				if (!state.IsDetailKey) {
					if (IsApi() && val == null)
						state.Visible = false; // Disable update for API request
					else
						state.FormValue = val;
				}

				// Check field name 'country' first before field var 'x_country'
				val = CurrentForm.GetValue("country", "x_country");
				if (!country.IsDetailKey) {
					if (IsApi() && val == null)
						country.Visible = false; // Disable update for API request
					else
						country.FormValue = val;
				}

				// Check field name 'tel1' first before field var 'x_tel1'
				val = CurrentForm.GetValue("tel1", "x_tel1");
				if (!tel1.IsDetailKey) {
					if (IsApi() && val == null)
						tel1.Visible = false; // Disable update for API request
					else
						tel1.FormValue = val;
				}

				// Check field name 'tel2' first before field var 'x_tel2'
				val = CurrentForm.GetValue("tel2", "x_tel2");
				if (!tel2.IsDetailKey) {
					if (IsApi() && val == null)
						tel2.Visible = false; // Disable update for API request
					else
						tel2.FormValue = val;
				}

				// Check field name 'phone1' first before field var 'x_phone1'
				val = CurrentForm.GetValue("phone1", "x_phone1");
				if (!phone1.IsDetailKey) {
					if (IsApi() && val == null)
						phone1.Visible = false; // Disable update for API request
					else
						phone1.FormValue = val;
				}

				// Check field name 'phone2' first before field var 'x_phone2'
				val = CurrentForm.GetValue("phone2", "x_phone2");
				if (!phone2.IsDetailKey) {
					if (IsApi() && val == null)
						phone2.Visible = false; // Disable update for API request
					else
						phone2.FormValue = val;
				}

				// Check field name 'fax' first before field var 'x_fax'
				val = CurrentForm.GetValue("fax", "x_fax");
				if (!fax.IsDetailKey) {
					if (IsApi() && val == null)
						fax.Visible = false; // Disable update for API request
					else
						fax.FormValue = val;
				}

				// Check field name 'email' first before field var 'x__email'
				val = CurrentForm.GetValue("email", "x__email");
				if (!_email.IsDetailKey) {
					if (IsApi() && val == null)
						_email.Visible = false; // Disable update for API request
					else
						_email.FormValue = val;
				}

				// Check field name 'ptc1' first before field var 'x_ptc1'
				val = CurrentForm.GetValue("ptc1", "x_ptc1");
				if (!ptc1.IsDetailKey) {
					if (IsApi() && val == null)
						ptc1.Visible = false; // Disable update for API request
					else
						ptc1.FormValue = val;
				}

				// Check field name 'ptc' first before field var 'x_ptc'
				val = CurrentForm.GetValue("ptc", "x_ptc");
				if (!ptc.IsDetailKey) {
					if (IsApi() && val == null)
						ptc.Visible = false; // Disable update for API request
					else
						ptc.FormValue = val;
				}

				// Check field name 'ptc2' first before field var 'x_ptc2'
				val = CurrentForm.GetValue("ptc2", "x_ptc2");
				if (!ptc2.IsDetailKey) {
					if (IsApi() && val == null)
						ptc2.Visible = false; // Disable update for API request
					else
						ptc2.FormValue = val;
				}

				// Check field name 'ar_grp' first before field var 'x_ar_grp'
				val = CurrentForm.GetValue("ar_grp", "x_ar_grp");
				if (!ar_grp.IsDetailKey) {
					if (IsApi() && val == null)
						ar_grp.Visible = false; // Disable update for API request
					else
						ar_grp.FormValue = val;
				}

				// Check field name 'term_code' first before field var 'x_term_code'
				val = CurrentForm.GetValue("term_code", "x_term_code");
				if (!term_code.IsDetailKey) {
					if (IsApi() && val == null)
						term_code.Visible = false; // Disable update for API request
					else
						term_code.FormValue = val;
				}

				// Check field name 'pterm_code' first before field var 'x_pterm_code'
				val = CurrentForm.GetValue("pterm_code", "x_pterm_code");
				if (!pterm_code.IsDetailKey) {
					if (IsApi() && val == null)
						pterm_code.Visible = false; // Disable update for API request
					else
						pterm_code.FormValue = val;
				}

				// Check field name 'cr_limit' first before field var 'x_cr_limit'
				val = CurrentForm.GetValue("cr_limit", "x_cr_limit");
				if (!cr_limit.IsDetailKey) {
					if (IsApi() && val == null)
						cr_limit.Visible = false; // Disable update for API request
					else
						cr_limit.FormValue = val;
				}

				// Check field name 'CurrencyCode' first before field var 'x_CurrencyCode'
				val = CurrentForm.GetValue("CurrencyCode", "x_CurrencyCode");
				if (!CurrencyCode.IsDetailKey) {
					if (IsApi() && val == null)
						CurrencyCode.Visible = false; // Disable update for API request
					else
						CurrencyCode.FormValue = val;
				}

				// Check field name 'slsman' first before field var 'x_slsman'
				val = CurrentForm.GetValue("slsman", "x_slsman");
				if (!slsman.IsDetailKey) {
					if (IsApi() && val == null)
						slsman.Visible = false; // Disable update for API request
					else
						slsman.FormValue = val;
				}

				// Check field name 'ytd_sale' first before field var 'x_ytd_sale'
				val = CurrentForm.GetValue("ytd_sale", "x_ytd_sale");
				if (!ytd_sale.IsDetailKey) {
					if (IsApi() && val == null)
						ytd_sale.Visible = false; // Disable update for API request
					else
						ytd_sale.FormValue = val;
				}

				// Check field name 'ytd_cost' first before field var 'x_ytd_cost'
				val = CurrentForm.GetValue("ytd_cost", "x_ytd_cost");
				if (!ytd_cost.IsDetailKey) {
					if (IsApi() && val == null)
						ytd_cost.Visible = false; // Disable update for API request
					else
						ytd_cost.FormValue = val;
				}

				// Check field name 'ytd_disc' first before field var 'x_ytd_disc'
				val = CurrentForm.GetValue("ytd_disc", "x_ytd_disc");
				if (!ytd_disc.IsDetailKey) {
					if (IsApi() && val == null)
						ytd_disc.Visible = false; // Disable update for API request
					else
						ytd_disc.FormValue = val;
				}

				// Check field name 'ctrl_acct' first before field var 'x_ctrl_acct'
				val = CurrentForm.GetValue("ctrl_acct", "x_ctrl_acct");
				if (!ctrl_acct.IsDetailKey) {
					if (IsApi() && val == null)
						ctrl_acct.Visible = false; // Disable update for API request
					else
						ctrl_acct.FormValue = val;
				}

				// Check field name 'ctrl_dept' first before field var 'x_ctrl_dept'
				val = CurrentForm.GetValue("ctrl_dept", "x_ctrl_dept");
				if (!ctrl_dept.IsDetailKey) {
					if (IsApi() && val == null)
						ctrl_dept.Visible = false; // Disable update for API request
					else
						ctrl_dept.FormValue = val;
				}

				// Check field name 'acct_code' first before field var 'x_acct_code'
				val = CurrentForm.GetValue("acct_code", "x_acct_code");
				if (!acct_code.IsDetailKey) {
					if (IsApi() && val == null)
						acct_code.Visible = false; // Disable update for API request
					else
						acct_code.FormValue = val;
				}

				// Check field name 'acct_dept' first before field var 'x_acct_dept'
				val = CurrentForm.GetValue("acct_dept", "x_acct_dept");
				if (!acct_dept.IsDetailKey) {
					if (IsApi() && val == null)
						acct_dept.Visible = false; // Disable update for API request
					else
						acct_dept.FormValue = val;
				}

				// Check field name 'skip_stmt' first before field var 'x_skip_stmt'
				val = CurrentForm.GetValue("skip_stmt", "x_skip_stmt");
				if (!skip_stmt.IsDetailKey) {
					if (IsApi() && val == null)
						skip_stmt.Visible = false; // Disable update for API request
					else
						skip_stmt.FormValue = val;
				}

				// Check field name 'stop_sale' first before field var 'x_stop_sale'
				val = CurrentForm.GetValue("stop_sale", "x_stop_sale");
				if (!stop_sale.IsDetailKey) {
					if (IsApi() && val == null)
						stop_sale.Visible = false; // Disable update for API request
					else
						stop_sale.FormValue = val;
				}

				// Check field name 'opn_item' first before field var 'x_opn_item'
				val = CurrentForm.GetValue("opn_item", "x_opn_item");
				if (!opn_item.IsDetailKey) {
					if (IsApi() && val == null)
						opn_item.Visible = false; // Disable update for API request
					else
						opn_item.FormValue = val;
				}

				// Check field name 'status' first before field var 'x_status'
				val = CurrentForm.GetValue("status", "x_status");
				if (!status.IsDetailKey) {
					if (IsApi() && val == null)
						status.Visible = false; // Disable update for API request
					else
						status.FormValue = val;
				}

				// Check field name 'tax_desc' first before field var 'x_tax_desc'
				val = CurrentForm.GetValue("tax_desc", "x_tax_desc");
				if (!tax_desc.IsDetailKey) {
					if (IsApi() && val == null)
						tax_desc.Visible = false; // Disable update for API request
					else
						tax_desc.FormValue = val;
				}

				// Check field name 'stax' first before field var 'x_stax'
				val = CurrentForm.GetValue("stax", "x_stax");
				if (!stax.IsDetailKey) {
					if (IsApi() && val == null)
						stax.Visible = false; // Disable update for API request
					else
						stax.FormValue = val;
				}

				// Check field name 'remark' first before field var 'x_remark'
				val = CurrentForm.GetValue("remark", "x_remark");
				if (!remark.IsDetailKey) {
					if (IsApi() && val == null)
						remark.Visible = false; // Disable update for API request
					else
						remark.FormValue = val;
				}

				// Check field name 'inv_fmt' first before field var 'x_inv_fmt'
				val = CurrentForm.GetValue("inv_fmt", "x_inv_fmt");
				if (!inv_fmt.IsDetailKey) {
					if (IsApi() && val == null)
						inv_fmt.Visible = false; // Disable update for API request
					else
						inv_fmt.FormValue = val;
				}

				// Check field name 'do_fmt' first before field var 'x_do_fmt'
				val = CurrentForm.GetValue("do_fmt", "x_do_fmt");
				if (!do_fmt.IsDetailKey) {
					if (IsApi() && val == null)
						do_fmt.Visible = false; // Disable update for API request
					else
						do_fmt.FormValue = val;
				}

				// Check field name 'Ship_Code' first before field var 'x_Ship_Code'
				val = CurrentForm.GetValue("Ship_Code", "x_Ship_Code");
				if (!Ship_Code.IsDetailKey) {
					if (IsApi() && val == null)
						Ship_Code.Visible = false; // Disable update for API request
					else
						Ship_Code.FormValue = val;
				}

				// Check field name 'custtype' first before field var 'x_custtype'
				val = CurrentForm.GetValue("custtype", "x_custtype");
				if (!custtype.IsDetailKey) {
					if (IsApi() && val == null)
						custtype.Visible = false; // Disable update for API request
					else
						custtype.FormValue = val;
				}

				// Check field name 'Acct_BillAcct' first before field var 'x_Acct_BillAcct'
				val = CurrentForm.GetValue("Acct_BillAcct", "x_Acct_BillAcct");
				if (!Acct_BillAcct.IsDetailKey) {
					if (IsApi() && val == null)
						Acct_BillAcct.Visible = false; // Disable update for API request
					else
						Acct_BillAcct.FormValue = val;
				}

				// Check field name 'bill_flag' first before field var 'x_bill_flag'
				val = CurrentForm.GetValue("bill_flag", "x_bill_flag");
				if (!bill_flag.IsDetailKey) {
					if (IsApi() && val == null)
						bill_flag.Visible = false; // Disable update for API request
					else
						bill_flag.FormValue = val;
				}

				// Check field name 'payment_code' first before field var 'x_payment_code'
				val = CurrentForm.GetValue("payment_code", "x_payment_code");
				if (!payment_code.IsDetailKey) {
					if (IsApi() && val == null)
						payment_code.Visible = false; // Disable update for API request
					else
						payment_code.FormValue = val;
				}

				// Check field name 'stax_pct' first before field var 'x_stax_pct'
				val = CurrentForm.GetValue("stax_pct", "x_stax_pct");
				if (!stax_pct.IsDetailKey) {
					if (IsApi() && val == null)
						stax_pct.Visible = false; // Disable update for API request
					else
						stax_pct.FormValue = val;
				}

				// Check field name 'db_part' first before field var 'x_db_part'
				val = CurrentForm.GetValue("db_part", "x_db_part");
				if (!db_part.IsDetailKey) {
					if (IsApi() && val == null)
						db_part.Visible = false; // Disable update for API request
					else
						db_part.FormValue = val;
				}

				// Check field name 'b_code' first before field var 'x_b_code'
				val = CurrentForm.GetValue("b_code", "x_b_code");
				if (!b_code.IsDetailKey) {
					if (IsApi() && val == null)
						b_code.Visible = false; // Disable update for API request
					else
						b_code.FormValue = val;
				}

				// Check field name 'lmw_no' first before field var 'x_lmw_no'
				val = CurrentForm.GetValue("lmw_no", "x_lmw_no");
				if (!lmw_no.IsDetailKey) {
					if (IsApi() && val == null)
						lmw_no.Visible = false; // Disable update for API request
					else
						lmw_no.FormValue = val;
				}

				// Check field name 'cs_code' first before field var 'x_cs_code'
				val = CurrentForm.GetValue("cs_code", "x_cs_code");
				if (!cs_code.IsDetailKey) {
					if (IsApi() && val == null)
						cs_code.Visible = false; // Disable update for API request
					else
						cs_code.FormValue = val;
				}

				// Check field name 'approved' first before field var 'x_approved'
				val = CurrentForm.GetValue("approved", "x_approved");
				if (!approved.IsDetailKey) {
					if (IsApi() && val == null)
						approved.Visible = false; // Disable update for API request
					else
						approved.FormValue = val;
				}

				// Check field name 'oversea' first before field var 'x_oversea'
				val = CurrentForm.GetValue("oversea", "x_oversea");
				if (!oversea.IsDetailKey) {
					if (IsApi() && val == null)
						oversea.Visible = false; // Disable update for API request
					else
						oversea.FormValue = val;
				}

				// Check field name 'defa_disc_pct' first before field var 'x_defa_disc_pct'
				val = CurrentForm.GetValue("defa_disc_pct", "x_defa_disc_pct");
				if (!defa_disc_pct.IsDetailKey) {
					if (IsApi() && val == null)
						defa_disc_pct.Visible = false; // Disable update for API request
					else
						defa_disc_pct.FormValue = val;
				}

				// Check field name 'sellpriceDOM' first before field var 'x_sellpriceDOM'
				val = CurrentForm.GetValue("sellpriceDOM", "x_sellpriceDOM");
				if (!sellpriceDOM.IsDetailKey) {
					if (IsApi() && val == null)
						sellpriceDOM.Visible = false; // Disable update for API request
					else
						sellpriceDOM.FormValue = val;
				}

				// Check field name 'id_upd' first before field var 'x_id_upd'
				val = CurrentForm.GetValue("id_upd", "x_id_upd");
				if (!id_upd.IsDetailKey) {
					if (IsApi() && val == null)
						id_upd.Visible = false; // Disable update for API request
					else
						id_upd.FormValue = val;
				}

				// Check field name 'dt_upd' first before field var 'x_dt_upd'
				val = CurrentForm.GetValue("dt_upd", "x_dt_upd");
				if (!dt_upd.IsDetailKey) {
					if (IsApi() && val == null)
						dt_upd.Visible = false; // Disable update for API request
					else
						dt_upd.FormValue = val;
					dt_upd.CurrentValue = UnformatDateTime(dt_upd.CurrentValue, 0);
				}

				// Check field name 'com_regno' first before field var 'x_com_regno'
				val = CurrentForm.GetValue("com_regno", "x_com_regno");
				if (!com_regno.IsDetailKey) {
					if (IsApi() && val == null)
						com_regno.Visible = false; // Disable update for API request
					else
						com_regno.FormValue = val;
				}
			}

			#pragma warning restore 1998

			// Restore form values
			public void RestoreFormValues() {
				Id.CurrentValue = Id.FormValue;
				dbcode.CurrentValue = dbcode.FormValue;
				name.CurrentValue = name.FormValue;
				name2.CurrentValue = name2.FormValue;
				short_name.CurrentValue = short_name.FormValue;
				add1.CurrentValue = add1.FormValue;
				add2.CurrentValue = add2.FormValue;
				add3.CurrentValue = add3.FormValue;
				add4.CurrentValue = add4.FormValue;
				area.CurrentValue = area.FormValue;
				postcode.CurrentValue = postcode.FormValue;
				state.CurrentValue = state.FormValue;
				country.CurrentValue = country.FormValue;
				tel1.CurrentValue = tel1.FormValue;
				tel2.CurrentValue = tel2.FormValue;
				phone1.CurrentValue = phone1.FormValue;
				phone2.CurrentValue = phone2.FormValue;
				fax.CurrentValue = fax.FormValue;
				_email.CurrentValue = _email.FormValue;
				ptc1.CurrentValue = ptc1.FormValue;
				ptc.CurrentValue = ptc.FormValue;
				ptc2.CurrentValue = ptc2.FormValue;
				ar_grp.CurrentValue = ar_grp.FormValue;
				term_code.CurrentValue = term_code.FormValue;
				pterm_code.CurrentValue = pterm_code.FormValue;
				cr_limit.CurrentValue = cr_limit.FormValue;
				CurrencyCode.CurrentValue = CurrencyCode.FormValue;
				slsman.CurrentValue = slsman.FormValue;
				ytd_sale.CurrentValue = ytd_sale.FormValue;
				ytd_cost.CurrentValue = ytd_cost.FormValue;
				ytd_disc.CurrentValue = ytd_disc.FormValue;
				ctrl_acct.CurrentValue = ctrl_acct.FormValue;
				ctrl_dept.CurrentValue = ctrl_dept.FormValue;
				acct_code.CurrentValue = acct_code.FormValue;
				acct_dept.CurrentValue = acct_dept.FormValue;
				skip_stmt.CurrentValue = skip_stmt.FormValue;
				stop_sale.CurrentValue = stop_sale.FormValue;
				opn_item.CurrentValue = opn_item.FormValue;
				status.CurrentValue = status.FormValue;
				tax_desc.CurrentValue = tax_desc.FormValue;
				stax.CurrentValue = stax.FormValue;
				remark.CurrentValue = remark.FormValue;
				inv_fmt.CurrentValue = inv_fmt.FormValue;
				do_fmt.CurrentValue = do_fmt.FormValue;
				Ship_Code.CurrentValue = Ship_Code.FormValue;
				custtype.CurrentValue = custtype.FormValue;
				Acct_BillAcct.CurrentValue = Acct_BillAcct.FormValue;
				bill_flag.CurrentValue = bill_flag.FormValue;
				payment_code.CurrentValue = payment_code.FormValue;
				stax_pct.CurrentValue = stax_pct.FormValue;
				db_part.CurrentValue = db_part.FormValue;
				b_code.CurrentValue = b_code.FormValue;
				lmw_no.CurrentValue = lmw_no.FormValue;
				cs_code.CurrentValue = cs_code.FormValue;
				approved.CurrentValue = approved.FormValue;
				oversea.CurrentValue = oversea.FormValue;
				defa_disc_pct.CurrentValue = defa_disc_pct.FormValue;
				sellpriceDOM.CurrentValue = sellpriceDOM.FormValue;
				id_upd.CurrentValue = id_upd.FormValue;
				dt_upd.CurrentValue = dt_upd.FormValue;
				dt_upd.CurrentValue = UnformatDateTime(dt_upd.CurrentValue, 0);
				com_regno.CurrentValue = com_regno.FormValue;
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
				dbcode.SetDbValue(row["dbcode"]);
				name.SetDbValue(row["name"]);
				name2.SetDbValue(row["name2"]);
				short_name.SetDbValue(row["short_name"]);
				add1.SetDbValue(row["add1"]);
				add2.SetDbValue(row["add2"]);
				add3.SetDbValue(row["add3"]);
				add4.SetDbValue(row["add4"]);
				area.SetDbValue(row["area"]);
				postcode.SetDbValue(row["postcode"]);
				state.SetDbValue(row["state"]);
				country.SetDbValue(row["country"]);
				tel1.SetDbValue(row["tel1"]);
				tel2.SetDbValue(row["tel2"]);
				phone1.SetDbValue(row["phone1"]);
				phone2.SetDbValue(row["phone2"]);
				fax.SetDbValue(row["fax"]);
				_email.SetDbValue(row["email"]);
				ptc1.SetDbValue(row["ptc1"]);
				ptc.SetDbValue(row["ptc"]);
				ptc2.SetDbValue(row["ptc2"]);
				ar_grp.SetDbValue(row["ar_grp"]);
				term_code.SetDbValue(row["term_code"]);
				pterm_code.SetDbValue(row["pterm_code"]);
				cr_limit.SetDbValue(row["cr_limit"]);
				CurrencyCode.SetDbValue(row["CurrencyCode"]);
				slsman.SetDbValue(row["slsman"]);
				ytd_sale.SetDbValue(row["ytd_sale"]);
				ytd_cost.SetDbValue(row["ytd_cost"]);
				ytd_disc.SetDbValue(row["ytd_disc"]);
				ctrl_acct.SetDbValue(row["ctrl_acct"]);
				ctrl_dept.SetDbValue(row["ctrl_dept"]);
				acct_code.SetDbValue(row["acct_code"]);
				acct_dept.SetDbValue(row["acct_dept"]);
				skip_stmt.SetDbValue((ConvertToBool(row["skip_stmt"]) ? "1" : "0"));
				stop_sale.SetDbValue((ConvertToBool(row["stop_sale"]) ? "1" : "0"));
				opn_item.SetDbValue((ConvertToBool(row["opn_item"]) ? "1" : "0"));
				status.SetDbValue(row["status"]);
				tax_desc.SetDbValue(row["tax_desc"]);
				stax.SetDbValue(row["stax"]);
				remark.SetDbValue(row["remark"]);
				inv_fmt.SetDbValue(row["inv_fmt"]);
				do_fmt.SetDbValue(row["do_fmt"]);
				Ship_Code.SetDbValue(row["Ship_Code"]);
				custtype.SetDbValue(row["custtype"]);
				Acct_BillAcct.SetDbValue(row["Acct_BillAcct"]);
				bill_flag.SetDbValue(row["bill_flag"]);
				payment_code.SetDbValue(row["payment_code"]);
				stax_pct.SetDbValue(row["stax_pct"]);
				db_part.SetDbValue(row["db_part"]);
				b_code.SetDbValue(row["b_code"]);
				lmw_no.SetDbValue(row["lmw_no"]);
				cs_code.SetDbValue(row["cs_code"]);
				approved.SetDbValue((ConvertToBool(row["approved"]) ? "1" : "0"));
				oversea.SetDbValue((ConvertToBool(row["oversea"]) ? "1" : "0"));
				defa_disc_pct.SetDbValue(row["defa_disc_pct"]);
				sellpriceDOM.SetDbValue((ConvertToBool(row["sellpriceDOM"]) ? "1" : "0"));
				id_upd.SetDbValue(row["id_upd"]);
				dt_upd.SetDbValue(row["dt_upd"]);
				com_regno.SetDbValue(row["com_regno"]);
			}

			#pragma warning restore 162, 168, 1998

			// Return a row with default values
			protected Dictionary<string, object> NewRow() {
				var row = new Dictionary<string, object>();
				row.Add("Id", System.DBNull.Value);
				row.Add("dbcode", System.DBNull.Value);
				row.Add("name", System.DBNull.Value);
				row.Add("name2", System.DBNull.Value);
				row.Add("short_name", System.DBNull.Value);
				row.Add("add1", System.DBNull.Value);
				row.Add("add2", System.DBNull.Value);
				row.Add("add3", System.DBNull.Value);
				row.Add("add4", System.DBNull.Value);
				row.Add("area", System.DBNull.Value);
				row.Add("postcode", System.DBNull.Value);
				row.Add("state", System.DBNull.Value);
				row.Add("country", System.DBNull.Value);
				row.Add("tel1", System.DBNull.Value);
				row.Add("tel2", System.DBNull.Value);
				row.Add("phone1", System.DBNull.Value);
				row.Add("phone2", System.DBNull.Value);
				row.Add("fax", System.DBNull.Value);
				row.Add("email", System.DBNull.Value);
				row.Add("ptc1", System.DBNull.Value);
				row.Add("ptc", System.DBNull.Value);
				row.Add("ptc2", System.DBNull.Value);
				row.Add("ar_grp", System.DBNull.Value);
				row.Add("term_code", System.DBNull.Value);
				row.Add("pterm_code", System.DBNull.Value);
				row.Add("cr_limit", System.DBNull.Value);
				row.Add("CurrencyCode", System.DBNull.Value);
				row.Add("slsman", System.DBNull.Value);
				row.Add("ytd_sale", System.DBNull.Value);
				row.Add("ytd_cost", System.DBNull.Value);
				row.Add("ytd_disc", System.DBNull.Value);
				row.Add("ctrl_acct", System.DBNull.Value);
				row.Add("ctrl_dept", System.DBNull.Value);
				row.Add("acct_code", System.DBNull.Value);
				row.Add("acct_dept", System.DBNull.Value);
				row.Add("skip_stmt", System.DBNull.Value);
				row.Add("stop_sale", System.DBNull.Value);
				row.Add("opn_item", System.DBNull.Value);
				row.Add("status", System.DBNull.Value);
				row.Add("tax_desc", System.DBNull.Value);
				row.Add("stax", System.DBNull.Value);
				row.Add("remark", System.DBNull.Value);
				row.Add("inv_fmt", System.DBNull.Value);
				row.Add("do_fmt", System.DBNull.Value);
				row.Add("Ship_Code", System.DBNull.Value);
				row.Add("custtype", System.DBNull.Value);
				row.Add("Acct_BillAcct", System.DBNull.Value);
				row.Add("bill_flag", System.DBNull.Value);
				row.Add("payment_code", System.DBNull.Value);
				row.Add("stax_pct", System.DBNull.Value);
				row.Add("db_part", System.DBNull.Value);
				row.Add("b_code", System.DBNull.Value);
				row.Add("lmw_no", System.DBNull.Value);
				row.Add("cs_code", System.DBNull.Value);
				row.Add("approved", System.DBNull.Value);
				row.Add("oversea", System.DBNull.Value);
				row.Add("defa_disc_pct", System.DBNull.Value);
				row.Add("sellpriceDOM", System.DBNull.Value);
				row.Add("id_upd", System.DBNull.Value);
				row.Add("dt_upd", System.DBNull.Value);
				row.Add("com_regno", System.DBNull.Value);
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
				if (SameString(cr_limit.FormValue, cr_limit.CurrentValue) && IsNumeric(ConvertToFloatString(cr_limit.CurrentValue)))
					cr_limit.CurrentValue = ConvertToFloatString(cr_limit.CurrentValue);

				// Convert decimal values if posted back
				if (SameString(ytd_sale.FormValue, ytd_sale.CurrentValue) && IsNumeric(ConvertToFloatString(ytd_sale.CurrentValue)))
					ytd_sale.CurrentValue = ConvertToFloatString(ytd_sale.CurrentValue);

				// Convert decimal values if posted back
				if (SameString(ytd_cost.FormValue, ytd_cost.CurrentValue) && IsNumeric(ConvertToFloatString(ytd_cost.CurrentValue)))
					ytd_cost.CurrentValue = ConvertToFloatString(ytd_cost.CurrentValue);

				// Convert decimal values if posted back
				if (SameString(ytd_disc.FormValue, ytd_disc.CurrentValue) && IsNumeric(ConvertToFloatString(ytd_disc.CurrentValue)))
					ytd_disc.CurrentValue = ConvertToFloatString(ytd_disc.CurrentValue);

				// Convert decimal values if posted back
				if (SameString(stax_pct.FormValue, stax_pct.CurrentValue) && IsNumeric(ConvertToFloatString(stax_pct.CurrentValue)))
					stax_pct.CurrentValue = ConvertToFloatString(stax_pct.CurrentValue);

				// Convert decimal values if posted back
				if (SameString(defa_disc_pct.FormValue, defa_disc_pct.CurrentValue) && IsNumeric(ConvertToFloatString(defa_disc_pct.CurrentValue)))
					defa_disc_pct.CurrentValue = ConvertToFloatString(defa_disc_pct.CurrentValue);

				// Call Row_Rendering event
				Row_Rendering();

				// Common render codes for all row types
				// Id
				// dbcode
				// name
				// name2
				// short_name
				// add1
				// add2
				// add3
				// add4
				// area
				// postcode
				// state
				// country
				// tel1
				// tel2
				// phone1
				// phone2
				// fax
				// _email
				// ptc1
				// ptc
				// ptc2
				// ar_grp
				// term_code
				// pterm_code
				// cr_limit
				// CurrencyCode
				// slsman
				// ytd_sale
				// ytd_cost
				// ytd_disc
				// ctrl_acct
				// ctrl_dept
				// acct_code
				// acct_dept
				// skip_stmt
				// stop_sale
				// opn_item
				// status
				// tax_desc
				// stax
				// remark
				// inv_fmt
				// do_fmt
				// Ship_Code
				// custtype
				// Acct_BillAcct
				// bill_flag
				// payment_code
				// stax_pct
				// db_part
				// b_code
				// lmw_no
				// cs_code
				// approved
				// oversea
				// defa_disc_pct
				// sellpriceDOM
				// id_upd
				// dt_upd
				// com_regno

				if (RowType == Config.RowTypeView) { // View row

					// Id
					Id.ViewValue = Id.CurrentValue;

					// dbcode
					dbcode.ViewValue = dbcode.CurrentValue;

					// name
					name.ViewValue = name.CurrentValue;

					// name2
					name2.ViewValue = name2.CurrentValue;

					// short_name
					short_name.ViewValue = short_name.CurrentValue;

					// add1
					add1.ViewValue = add1.CurrentValue;

					// add2
					add2.ViewValue = add2.CurrentValue;

					// add3
					add3.ViewValue = add3.CurrentValue;

					// add4
					add4.ViewValue = add4.CurrentValue;

					// area
					area.ViewValue = area.CurrentValue;

					// postcode
					postcode.ViewValue = postcode.CurrentValue;

					// state
					state.ViewValue = state.CurrentValue;

					// country
					country.ViewValue = country.CurrentValue;

					// tel1
					tel1.ViewValue = tel1.CurrentValue;

					// tel2
					tel2.ViewValue = tel2.CurrentValue;

					// phone1
					phone1.ViewValue = phone1.CurrentValue;

					// phone2
					phone2.ViewValue = phone2.CurrentValue;

					// fax
					fax.ViewValue = fax.CurrentValue;

					// _email
					_email.ViewValue = _email.CurrentValue;

					// ptc1
					ptc1.ViewValue = ptc1.CurrentValue;

					// ptc
					ptc.ViewValue = ptc.CurrentValue;

					// ptc2
					ptc2.ViewValue = ptc2.CurrentValue;

					// ar_grp
					ar_grp.ViewValue = ar_grp.CurrentValue;

					// term_code
					term_code.ViewValue = term_code.CurrentValue;

					// pterm_code
					pterm_code.ViewValue = pterm_code.CurrentValue;

					// cr_limit
					cr_limit.ViewValue = cr_limit.CurrentValue;
					cr_limit.ViewValue = FormatNumber(cr_limit.ViewValue, 2, -2, -2, -2);

					// CurrencyCode
					CurrencyCode.ViewValue = CurrencyCode.CurrentValue;

					// slsman
					slsman.ViewValue = slsman.CurrentValue;

					// ytd_sale
					ytd_sale.ViewValue = ytd_sale.CurrentValue;
					ytd_sale.ViewValue = FormatNumber(ytd_sale.ViewValue, 2, -2, -2, -2);

					// ytd_cost
					ytd_cost.ViewValue = ytd_cost.CurrentValue;
					ytd_cost.ViewValue = FormatNumber(ytd_cost.ViewValue, 2, -2, -2, -2);

					// ytd_disc
					ytd_disc.ViewValue = ytd_disc.CurrentValue;
					ytd_disc.ViewValue = FormatNumber(ytd_disc.ViewValue, 2, -2, -2, -2);

					// ctrl_acct
					ctrl_acct.ViewValue = ctrl_acct.CurrentValue;

					// ctrl_dept
					ctrl_dept.ViewValue = ctrl_dept.CurrentValue;

					// acct_code
					acct_code.ViewValue = acct_code.CurrentValue;

					// acct_dept
					acct_dept.ViewValue = acct_dept.CurrentValue;

					// skip_stmt
					if (ConvertToBool(skip_stmt.CurrentValue)) {
						skip_stmt.ViewValue = (skip_stmt.TagCaption(1) != "") ? skip_stmt.TagCaption(1) : "Yes";
					} else {
						skip_stmt.ViewValue = (skip_stmt.TagCaption(2) != "") ? skip_stmt.TagCaption(2) : "No";
					}

					// stop_sale
					if (ConvertToBool(stop_sale.CurrentValue)) {
						stop_sale.ViewValue = (stop_sale.TagCaption(1) != "") ? stop_sale.TagCaption(1) : "Yes";
					} else {
						stop_sale.ViewValue = (stop_sale.TagCaption(2) != "") ? stop_sale.TagCaption(2) : "No";
					}

					// opn_item
					if (ConvertToBool(opn_item.CurrentValue)) {
						opn_item.ViewValue = (opn_item.TagCaption(1) != "") ? opn_item.TagCaption(1) : "Yes";
					} else {
						opn_item.ViewValue = (opn_item.TagCaption(2) != "") ? opn_item.TagCaption(2) : "No";
					}

					// status
					status.ViewValue = status.CurrentValue;

					// tax_desc
					tax_desc.ViewValue = tax_desc.CurrentValue;

					// stax
					stax.ViewValue = stax.CurrentValue;

					// remark
					remark.ViewValue = remark.CurrentValue;

					// inv_fmt
					inv_fmt.ViewValue = inv_fmt.CurrentValue;

					// do_fmt
					do_fmt.ViewValue = do_fmt.CurrentValue;

					// Ship_Code
					Ship_Code.ViewValue = Ship_Code.CurrentValue;

					// custtype
					custtype.ViewValue = custtype.CurrentValue;

					// Acct_BillAcct
					Acct_BillAcct.ViewValue = Acct_BillAcct.CurrentValue;

					// bill_flag
					bill_flag.ViewValue = bill_flag.CurrentValue;

					// payment_code
					payment_code.ViewValue = payment_code.CurrentValue;

					// stax_pct
					stax_pct.ViewValue = stax_pct.CurrentValue;
					stax_pct.ViewValue = FormatNumber(stax_pct.ViewValue, 2, -2, -2, -2);

					// db_part
					db_part.ViewValue = db_part.CurrentValue;

					// b_code
					b_code.ViewValue = b_code.CurrentValue;

					// lmw_no
					lmw_no.ViewValue = lmw_no.CurrentValue;

					// cs_code
					cs_code.ViewValue = cs_code.CurrentValue;

					// approved
					if (ConvertToBool(approved.CurrentValue)) {
						approved.ViewValue = (approved.TagCaption(1) != "") ? approved.TagCaption(1) : "Yes";
					} else {
						approved.ViewValue = (approved.TagCaption(2) != "") ? approved.TagCaption(2) : "No";
					}

					// oversea
					if (ConvertToBool(oversea.CurrentValue)) {
						oversea.ViewValue = (oversea.TagCaption(1) != "") ? oversea.TagCaption(1) : "Yes";
					} else {
						oversea.ViewValue = (oversea.TagCaption(2) != "") ? oversea.TagCaption(2) : "No";
					}

					// defa_disc_pct
					defa_disc_pct.ViewValue = defa_disc_pct.CurrentValue;
					defa_disc_pct.ViewValue = FormatNumber(defa_disc_pct.ViewValue, 2, -2, -2, -2);

					// sellpriceDOM
					if (ConvertToBool(sellpriceDOM.CurrentValue)) {
						sellpriceDOM.ViewValue = (sellpriceDOM.TagCaption(1) != "") ? sellpriceDOM.TagCaption(1) : "Yes";
					} else {
						sellpriceDOM.ViewValue = (sellpriceDOM.TagCaption(2) != "") ? sellpriceDOM.TagCaption(2) : "No";
					}

					// id_upd
					id_upd.ViewValue = id_upd.CurrentValue;

					// dt_upd
					dt_upd.ViewValue = dt_upd.CurrentValue;
					dt_upd.ViewValue = FormatDateTime(dt_upd.ViewValue, 0);

					// com_regno
					com_regno.ViewValue = com_regno.CurrentValue;

					// Id
					Id.HrefValue = "";
					Id.TooltipValue = "";

					// dbcode
					dbcode.HrefValue = "";
					dbcode.TooltipValue = "";

					// name
					name.HrefValue = "";
					name.TooltipValue = "";

					// name2
					name2.HrefValue = "";
					name2.TooltipValue = "";

					// short_name
					short_name.HrefValue = "";
					short_name.TooltipValue = "";

					// add1
					add1.HrefValue = "";
					add1.TooltipValue = "";

					// add2
					add2.HrefValue = "";
					add2.TooltipValue = "";

					// add3
					add3.HrefValue = "";
					add3.TooltipValue = "";

					// add4
					add4.HrefValue = "";
					add4.TooltipValue = "";

					// area
					area.HrefValue = "";
					area.TooltipValue = "";

					// postcode
					postcode.HrefValue = "";
					postcode.TooltipValue = "";

					// state
					state.HrefValue = "";
					state.TooltipValue = "";

					// country
					country.HrefValue = "";
					country.TooltipValue = "";

					// tel1
					tel1.HrefValue = "";
					tel1.TooltipValue = "";

					// tel2
					tel2.HrefValue = "";
					tel2.TooltipValue = "";

					// phone1
					phone1.HrefValue = "";
					phone1.TooltipValue = "";

					// phone2
					phone2.HrefValue = "";
					phone2.TooltipValue = "";

					// fax
					fax.HrefValue = "";
					fax.TooltipValue = "";

					// _email
					_email.HrefValue = "";
					_email.TooltipValue = "";

					// ptc1
					ptc1.HrefValue = "";
					ptc1.TooltipValue = "";

					// ptc
					ptc.HrefValue = "";
					ptc.TooltipValue = "";

					// ptc2
					ptc2.HrefValue = "";
					ptc2.TooltipValue = "";

					// ar_grp
					ar_grp.HrefValue = "";
					ar_grp.TooltipValue = "";

					// term_code
					term_code.HrefValue = "";
					term_code.TooltipValue = "";

					// pterm_code
					pterm_code.HrefValue = "";
					pterm_code.TooltipValue = "";

					// cr_limit
					cr_limit.HrefValue = "";
					cr_limit.TooltipValue = "";

					// CurrencyCode
					CurrencyCode.HrefValue = "";
					CurrencyCode.TooltipValue = "";

					// slsman
					slsman.HrefValue = "";
					slsman.TooltipValue = "";

					// ytd_sale
					ytd_sale.HrefValue = "";
					ytd_sale.TooltipValue = "";

					// ytd_cost
					ytd_cost.HrefValue = "";
					ytd_cost.TooltipValue = "";

					// ytd_disc
					ytd_disc.HrefValue = "";
					ytd_disc.TooltipValue = "";

					// ctrl_acct
					ctrl_acct.HrefValue = "";
					ctrl_acct.TooltipValue = "";

					// ctrl_dept
					ctrl_dept.HrefValue = "";
					ctrl_dept.TooltipValue = "";

					// acct_code
					acct_code.HrefValue = "";
					acct_code.TooltipValue = "";

					// acct_dept
					acct_dept.HrefValue = "";
					acct_dept.TooltipValue = "";

					// skip_stmt
					skip_stmt.HrefValue = "";
					skip_stmt.TooltipValue = "";

					// stop_sale
					stop_sale.HrefValue = "";
					stop_sale.TooltipValue = "";

					// opn_item
					opn_item.HrefValue = "";
					opn_item.TooltipValue = "";

					// status
					status.HrefValue = "";
					status.TooltipValue = "";

					// tax_desc
					tax_desc.HrefValue = "";
					tax_desc.TooltipValue = "";

					// stax
					stax.HrefValue = "";
					stax.TooltipValue = "";

					// remark
					remark.HrefValue = "";
					remark.TooltipValue = "";

					// inv_fmt
					inv_fmt.HrefValue = "";
					inv_fmt.TooltipValue = "";

					// do_fmt
					do_fmt.HrefValue = "";
					do_fmt.TooltipValue = "";

					// Ship_Code
					Ship_Code.HrefValue = "";
					Ship_Code.TooltipValue = "";

					// custtype
					custtype.HrefValue = "";
					custtype.TooltipValue = "";

					// Acct_BillAcct
					Acct_BillAcct.HrefValue = "";
					Acct_BillAcct.TooltipValue = "";

					// bill_flag
					bill_flag.HrefValue = "";
					bill_flag.TooltipValue = "";

					// payment_code
					payment_code.HrefValue = "";
					payment_code.TooltipValue = "";

					// stax_pct
					stax_pct.HrefValue = "";
					stax_pct.TooltipValue = "";

					// db_part
					db_part.HrefValue = "";
					db_part.TooltipValue = "";

					// b_code
					b_code.HrefValue = "";
					b_code.TooltipValue = "";

					// lmw_no
					lmw_no.HrefValue = "";
					lmw_no.TooltipValue = "";

					// cs_code
					cs_code.HrefValue = "";
					cs_code.TooltipValue = "";

					// approved
					approved.HrefValue = "";
					approved.TooltipValue = "";

					// oversea
					oversea.HrefValue = "";
					oversea.TooltipValue = "";

					// defa_disc_pct
					defa_disc_pct.HrefValue = "";
					defa_disc_pct.TooltipValue = "";

					// sellpriceDOM
					sellpriceDOM.HrefValue = "";
					sellpriceDOM.TooltipValue = "";

					// id_upd
					id_upd.HrefValue = "";
					id_upd.TooltipValue = "";

					// dt_upd
					dt_upd.HrefValue = "";
					dt_upd.TooltipValue = "";

					// com_regno
					com_regno.HrefValue = "";
					com_regno.TooltipValue = "";
				} else if (RowType == Config.RowTypeEdit) { // Edit row

					// Id
					Id.EditAttrs["class"] = "form-control";
					Id.EditValue = Id.CurrentValue;

					// dbcode
					dbcode.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						dbcode.CurrentValue = HtmlDecode(dbcode.CurrentValue);
					dbcode.EditValue = dbcode.CurrentValue; // DN
					dbcode.PlaceHolder = RemoveHtml(dbcode.Caption);

					// name
					name.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						name.CurrentValue = HtmlDecode(name.CurrentValue);
					name.EditValue = name.CurrentValue; // DN
					name.PlaceHolder = RemoveHtml(name.Caption);

					// name2
					name2.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						name2.CurrentValue = HtmlDecode(name2.CurrentValue);
					name2.EditValue = name2.CurrentValue; // DN
					name2.PlaceHolder = RemoveHtml(name2.Caption);

					// short_name
					short_name.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						short_name.CurrentValue = HtmlDecode(short_name.CurrentValue);
					short_name.EditValue = short_name.CurrentValue; // DN
					short_name.PlaceHolder = RemoveHtml(short_name.Caption);

					// add1
					add1.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						add1.CurrentValue = HtmlDecode(add1.CurrentValue);
					add1.EditValue = add1.CurrentValue; // DN
					add1.PlaceHolder = RemoveHtml(add1.Caption);

					// add2
					add2.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						add2.CurrentValue = HtmlDecode(add2.CurrentValue);
					add2.EditValue = add2.CurrentValue; // DN
					add2.PlaceHolder = RemoveHtml(add2.Caption);

					// add3
					add3.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						add3.CurrentValue = HtmlDecode(add3.CurrentValue);
					add3.EditValue = add3.CurrentValue; // DN
					add3.PlaceHolder = RemoveHtml(add3.Caption);

					// add4
					add4.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						add4.CurrentValue = HtmlDecode(add4.CurrentValue);
					add4.EditValue = add4.CurrentValue; // DN
					add4.PlaceHolder = RemoveHtml(add4.Caption);

					// area
					area.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						area.CurrentValue = HtmlDecode(area.CurrentValue);
					area.EditValue = area.CurrentValue; // DN
					area.PlaceHolder = RemoveHtml(area.Caption);

					// postcode
					postcode.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						postcode.CurrentValue = HtmlDecode(postcode.CurrentValue);
					postcode.EditValue = postcode.CurrentValue; // DN
					postcode.PlaceHolder = RemoveHtml(postcode.Caption);

					// state
					state.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						state.CurrentValue = HtmlDecode(state.CurrentValue);
					state.EditValue = state.CurrentValue; // DN
					state.PlaceHolder = RemoveHtml(state.Caption);

					// country
					country.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						country.CurrentValue = HtmlDecode(country.CurrentValue);
					country.EditValue = country.CurrentValue; // DN
					country.PlaceHolder = RemoveHtml(country.Caption);

					// tel1
					tel1.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						tel1.CurrentValue = HtmlDecode(tel1.CurrentValue);
					tel1.EditValue = tel1.CurrentValue; // DN
					tel1.PlaceHolder = RemoveHtml(tel1.Caption);

					// tel2
					tel2.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						tel2.CurrentValue = HtmlDecode(tel2.CurrentValue);
					tel2.EditValue = tel2.CurrentValue; // DN
					tel2.PlaceHolder = RemoveHtml(tel2.Caption);

					// phone1
					phone1.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						phone1.CurrentValue = HtmlDecode(phone1.CurrentValue);
					phone1.EditValue = phone1.CurrentValue; // DN
					phone1.PlaceHolder = RemoveHtml(phone1.Caption);

					// phone2
					phone2.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						phone2.CurrentValue = HtmlDecode(phone2.CurrentValue);
					phone2.EditValue = phone2.CurrentValue; // DN
					phone2.PlaceHolder = RemoveHtml(phone2.Caption);

					// fax
					fax.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						fax.CurrentValue = HtmlDecode(fax.CurrentValue);
					fax.EditValue = fax.CurrentValue; // DN
					fax.PlaceHolder = RemoveHtml(fax.Caption);

					// _email
					_email.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						_email.CurrentValue = HtmlDecode(_email.CurrentValue);
					_email.EditValue = _email.CurrentValue; // DN
					_email.PlaceHolder = RemoveHtml(_email.Caption);

					// ptc1
					ptc1.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						ptc1.CurrentValue = HtmlDecode(ptc1.CurrentValue);
					ptc1.EditValue = ptc1.CurrentValue; // DN
					ptc1.PlaceHolder = RemoveHtml(ptc1.Caption);

					// ptc
					ptc.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						ptc.CurrentValue = HtmlDecode(ptc.CurrentValue);
					ptc.EditValue = ptc.CurrentValue; // DN
					ptc.PlaceHolder = RemoveHtml(ptc.Caption);

					// ptc2
					ptc2.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						ptc2.CurrentValue = HtmlDecode(ptc2.CurrentValue);
					ptc2.EditValue = ptc2.CurrentValue; // DN
					ptc2.PlaceHolder = RemoveHtml(ptc2.Caption);

					// ar_grp
					ar_grp.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						ar_grp.CurrentValue = HtmlDecode(ar_grp.CurrentValue);
					ar_grp.EditValue = ar_grp.CurrentValue; // DN
					ar_grp.PlaceHolder = RemoveHtml(ar_grp.Caption);

					// term_code
					term_code.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						term_code.CurrentValue = HtmlDecode(term_code.CurrentValue);
					term_code.EditValue = term_code.CurrentValue; // DN
					term_code.PlaceHolder = RemoveHtml(term_code.Caption);

					// pterm_code
					pterm_code.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						pterm_code.CurrentValue = HtmlDecode(pterm_code.CurrentValue);
					pterm_code.EditValue = pterm_code.CurrentValue; // DN
					pterm_code.PlaceHolder = RemoveHtml(pterm_code.Caption);

					// cr_limit
					cr_limit.EditAttrs["class"] = "form-control";
					cr_limit.EditValue = cr_limit.CurrentValue; // DN
					cr_limit.PlaceHolder = RemoveHtml(cr_limit.Caption);
					if (!Empty(cr_limit.EditValue) && IsNumeric(cr_limit.EditValue))
						cr_limit.EditValue = FormatNumber(cr_limit.EditValue, -2, -2, -2, -2);

					// CurrencyCode
					CurrencyCode.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						CurrencyCode.CurrentValue = HtmlDecode(CurrencyCode.CurrentValue);
					CurrencyCode.EditValue = CurrencyCode.CurrentValue; // DN
					CurrencyCode.PlaceHolder = RemoveHtml(CurrencyCode.Caption);

					// slsman
					slsman.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						slsman.CurrentValue = HtmlDecode(slsman.CurrentValue);
					slsman.EditValue = slsman.CurrentValue; // DN
					slsman.PlaceHolder = RemoveHtml(slsman.Caption);

					// ytd_sale
					ytd_sale.EditAttrs["class"] = "form-control";
					ytd_sale.EditValue = ytd_sale.CurrentValue; // DN
					ytd_sale.PlaceHolder = RemoveHtml(ytd_sale.Caption);
					if (!Empty(ytd_sale.EditValue) && IsNumeric(ytd_sale.EditValue))
						ytd_sale.EditValue = FormatNumber(ytd_sale.EditValue, -2, -2, -2, -2);

					// ytd_cost
					ytd_cost.EditAttrs["class"] = "form-control";
					ytd_cost.EditValue = ytd_cost.CurrentValue; // DN
					ytd_cost.PlaceHolder = RemoveHtml(ytd_cost.Caption);
					if (!Empty(ytd_cost.EditValue) && IsNumeric(ytd_cost.EditValue))
						ytd_cost.EditValue = FormatNumber(ytd_cost.EditValue, -2, -2, -2, -2);

					// ytd_disc
					ytd_disc.EditAttrs["class"] = "form-control";
					ytd_disc.EditValue = ytd_disc.CurrentValue; // DN
					ytd_disc.PlaceHolder = RemoveHtml(ytd_disc.Caption);
					if (!Empty(ytd_disc.EditValue) && IsNumeric(ytd_disc.EditValue))
						ytd_disc.EditValue = FormatNumber(ytd_disc.EditValue, -2, -2, -2, -2);

					// ctrl_acct
					ctrl_acct.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						ctrl_acct.CurrentValue = HtmlDecode(ctrl_acct.CurrentValue);
					ctrl_acct.EditValue = ctrl_acct.CurrentValue; // DN
					ctrl_acct.PlaceHolder = RemoveHtml(ctrl_acct.Caption);

					// ctrl_dept
					ctrl_dept.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						ctrl_dept.CurrentValue = HtmlDecode(ctrl_dept.CurrentValue);
					ctrl_dept.EditValue = ctrl_dept.CurrentValue; // DN
					ctrl_dept.PlaceHolder = RemoveHtml(ctrl_dept.Caption);

					// acct_code
					acct_code.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						acct_code.CurrentValue = HtmlDecode(acct_code.CurrentValue);
					acct_code.EditValue = acct_code.CurrentValue; // DN
					acct_code.PlaceHolder = RemoveHtml(acct_code.Caption);

					// acct_dept
					acct_dept.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						acct_dept.CurrentValue = HtmlDecode(acct_dept.CurrentValue);
					acct_dept.EditValue = acct_dept.CurrentValue; // DN
					acct_dept.PlaceHolder = RemoveHtml(acct_dept.Caption);

					// skip_stmt
					skip_stmt.EditValue = skip_stmt.Options(false);

					// stop_sale
					stop_sale.EditValue = stop_sale.Options(false);

					// opn_item
					opn_item.EditValue = opn_item.Options(false);

					// status
					status.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						status.CurrentValue = HtmlDecode(status.CurrentValue);
					status.EditValue = status.CurrentValue; // DN
					status.PlaceHolder = RemoveHtml(status.Caption);

					// tax_desc
					tax_desc.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						tax_desc.CurrentValue = HtmlDecode(tax_desc.CurrentValue);
					tax_desc.EditValue = tax_desc.CurrentValue; // DN
					tax_desc.PlaceHolder = RemoveHtml(tax_desc.Caption);

					// stax
					stax.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						stax.CurrentValue = HtmlDecode(stax.CurrentValue);
					stax.EditValue = stax.CurrentValue; // DN
					stax.PlaceHolder = RemoveHtml(stax.Caption);

					// remark
					remark.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						remark.CurrentValue = HtmlDecode(remark.CurrentValue);
					remark.EditValue = remark.CurrentValue; // DN
					remark.PlaceHolder = RemoveHtml(remark.Caption);

					// inv_fmt
					inv_fmt.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						inv_fmt.CurrentValue = HtmlDecode(inv_fmt.CurrentValue);
					inv_fmt.EditValue = inv_fmt.CurrentValue; // DN
					inv_fmt.PlaceHolder = RemoveHtml(inv_fmt.Caption);

					// do_fmt
					do_fmt.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						do_fmt.CurrentValue = HtmlDecode(do_fmt.CurrentValue);
					do_fmt.EditValue = do_fmt.CurrentValue; // DN
					do_fmt.PlaceHolder = RemoveHtml(do_fmt.Caption);

					// Ship_Code
					Ship_Code.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						Ship_Code.CurrentValue = HtmlDecode(Ship_Code.CurrentValue);
					Ship_Code.EditValue = Ship_Code.CurrentValue; // DN
					Ship_Code.PlaceHolder = RemoveHtml(Ship_Code.Caption);

					// custtype
					custtype.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						custtype.CurrentValue = HtmlDecode(custtype.CurrentValue);
					custtype.EditValue = custtype.CurrentValue; // DN
					custtype.PlaceHolder = RemoveHtml(custtype.Caption);

					// Acct_BillAcct
					Acct_BillAcct.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						Acct_BillAcct.CurrentValue = HtmlDecode(Acct_BillAcct.CurrentValue);
					Acct_BillAcct.EditValue = Acct_BillAcct.CurrentValue; // DN
					Acct_BillAcct.PlaceHolder = RemoveHtml(Acct_BillAcct.Caption);

					// bill_flag
					bill_flag.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						bill_flag.CurrentValue = HtmlDecode(bill_flag.CurrentValue);
					bill_flag.EditValue = bill_flag.CurrentValue; // DN
					bill_flag.PlaceHolder = RemoveHtml(bill_flag.Caption);

					// payment_code
					payment_code.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						payment_code.CurrentValue = HtmlDecode(payment_code.CurrentValue);
					payment_code.EditValue = payment_code.CurrentValue; // DN
					payment_code.PlaceHolder = RemoveHtml(payment_code.Caption);

					// stax_pct
					stax_pct.EditAttrs["class"] = "form-control";
					stax_pct.EditValue = stax_pct.CurrentValue; // DN
					stax_pct.PlaceHolder = RemoveHtml(stax_pct.Caption);
					if (!Empty(stax_pct.EditValue) && IsNumeric(stax_pct.EditValue))
						stax_pct.EditValue = FormatNumber(stax_pct.EditValue, -2, -2, -2, -2);

					// db_part
					db_part.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						db_part.CurrentValue = HtmlDecode(db_part.CurrentValue);
					db_part.EditValue = db_part.CurrentValue; // DN
					db_part.PlaceHolder = RemoveHtml(db_part.Caption);

					// b_code
					b_code.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						b_code.CurrentValue = HtmlDecode(b_code.CurrentValue);
					b_code.EditValue = b_code.CurrentValue; // DN
					b_code.PlaceHolder = RemoveHtml(b_code.Caption);

					// lmw_no
					lmw_no.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						lmw_no.CurrentValue = HtmlDecode(lmw_no.CurrentValue);
					lmw_no.EditValue = lmw_no.CurrentValue; // DN
					lmw_no.PlaceHolder = RemoveHtml(lmw_no.Caption);

					// cs_code
					cs_code.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						cs_code.CurrentValue = HtmlDecode(cs_code.CurrentValue);
					cs_code.EditValue = cs_code.CurrentValue; // DN
					cs_code.PlaceHolder = RemoveHtml(cs_code.Caption);

					// approved
					approved.EditValue = approved.Options(false);

					// oversea
					oversea.EditValue = oversea.Options(false);

					// defa_disc_pct
					defa_disc_pct.EditAttrs["class"] = "form-control";
					defa_disc_pct.EditValue = defa_disc_pct.CurrentValue; // DN
					defa_disc_pct.PlaceHolder = RemoveHtml(defa_disc_pct.Caption);
					if (!Empty(defa_disc_pct.EditValue) && IsNumeric(defa_disc_pct.EditValue))
						defa_disc_pct.EditValue = FormatNumber(defa_disc_pct.EditValue, -2, -2, -2, -2);

					// sellpriceDOM
					sellpriceDOM.EditValue = sellpriceDOM.Options(false);

					// id_upd
					id_upd.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						id_upd.CurrentValue = HtmlDecode(id_upd.CurrentValue);
					id_upd.EditValue = id_upd.CurrentValue; // DN
					id_upd.PlaceHolder = RemoveHtml(id_upd.Caption);

					// dt_upd
					dt_upd.EditAttrs["class"] = "form-control";
					dt_upd.EditValue = FormatDateTime(dt_upd.CurrentValue, 8); // DN
					dt_upd.PlaceHolder = RemoveHtml(dt_upd.Caption);

					// com_regno
					com_regno.EditAttrs["class"] = "form-control";
					if (Config.RemoveXss)
						com_regno.CurrentValue = HtmlDecode(com_regno.CurrentValue);
					com_regno.EditValue = com_regno.CurrentValue; // DN
					com_regno.PlaceHolder = RemoveHtml(com_regno.Caption);

					// Edit refer script
					// Id

					Id.HrefValue = "";

					// dbcode
					dbcode.HrefValue = "";

					// name
					name.HrefValue = "";

					// name2
					name2.HrefValue = "";

					// short_name
					short_name.HrefValue = "";

					// add1
					add1.HrefValue = "";

					// add2
					add2.HrefValue = "";

					// add3
					add3.HrefValue = "";

					// add4
					add4.HrefValue = "";

					// area
					area.HrefValue = "";

					// postcode
					postcode.HrefValue = "";

					// state
					state.HrefValue = "";

					// country
					country.HrefValue = "";

					// tel1
					tel1.HrefValue = "";

					// tel2
					tel2.HrefValue = "";

					// phone1
					phone1.HrefValue = "";

					// phone2
					phone2.HrefValue = "";

					// fax
					fax.HrefValue = "";

					// _email
					_email.HrefValue = "";

					// ptc1
					ptc1.HrefValue = "";

					// ptc
					ptc.HrefValue = "";

					// ptc2
					ptc2.HrefValue = "";

					// ar_grp
					ar_grp.HrefValue = "";

					// term_code
					term_code.HrefValue = "";

					// pterm_code
					pterm_code.HrefValue = "";

					// cr_limit
					cr_limit.HrefValue = "";

					// CurrencyCode
					CurrencyCode.HrefValue = "";

					// slsman
					slsman.HrefValue = "";

					// ytd_sale
					ytd_sale.HrefValue = "";

					// ytd_cost
					ytd_cost.HrefValue = "";

					// ytd_disc
					ytd_disc.HrefValue = "";

					// ctrl_acct
					ctrl_acct.HrefValue = "";

					// ctrl_dept
					ctrl_dept.HrefValue = "";

					// acct_code
					acct_code.HrefValue = "";

					// acct_dept
					acct_dept.HrefValue = "";

					// skip_stmt
					skip_stmt.HrefValue = "";

					// stop_sale
					stop_sale.HrefValue = "";

					// opn_item
					opn_item.HrefValue = "";

					// status
					status.HrefValue = "";

					// tax_desc
					tax_desc.HrefValue = "";

					// stax
					stax.HrefValue = "";

					// remark
					remark.HrefValue = "";

					// inv_fmt
					inv_fmt.HrefValue = "";

					// do_fmt
					do_fmt.HrefValue = "";

					// Ship_Code
					Ship_Code.HrefValue = "";

					// custtype
					custtype.HrefValue = "";

					// Acct_BillAcct
					Acct_BillAcct.HrefValue = "";

					// bill_flag
					bill_flag.HrefValue = "";

					// payment_code
					payment_code.HrefValue = "";

					// stax_pct
					stax_pct.HrefValue = "";

					// db_part
					db_part.HrefValue = "";

					// b_code
					b_code.HrefValue = "";

					// lmw_no
					lmw_no.HrefValue = "";

					// cs_code
					cs_code.HrefValue = "";

					// approved
					approved.HrefValue = "";

					// oversea
					oversea.HrefValue = "";

					// defa_disc_pct
					defa_disc_pct.HrefValue = "";

					// sellpriceDOM
					sellpriceDOM.HrefValue = "";

					// id_upd
					id_upd.HrefValue = "";

					// dt_upd
					dt_upd.HrefValue = "";

					// com_regno
					com_regno.HrefValue = "";
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
				if (dbcode.Required) {
					if (!dbcode.IsDetailKey && Empty(dbcode.FormValue))
						FormError = AddMessage(FormError, dbcode.RequiredErrorMessage.Replace("%s", dbcode.Caption));
				}
				if (name.Required) {
					if (!name.IsDetailKey && Empty(name.FormValue))
						FormError = AddMessage(FormError, name.RequiredErrorMessage.Replace("%s", name.Caption));
				}
				if (name2.Required) {
					if (!name2.IsDetailKey && Empty(name2.FormValue))
						FormError = AddMessage(FormError, name2.RequiredErrorMessage.Replace("%s", name2.Caption));
				}
				if (short_name.Required) {
					if (!short_name.IsDetailKey && Empty(short_name.FormValue))
						FormError = AddMessage(FormError, short_name.RequiredErrorMessage.Replace("%s", short_name.Caption));
				}
				if (add1.Required) {
					if (!add1.IsDetailKey && Empty(add1.FormValue))
						FormError = AddMessage(FormError, add1.RequiredErrorMessage.Replace("%s", add1.Caption));
				}
				if (add2.Required) {
					if (!add2.IsDetailKey && Empty(add2.FormValue))
						FormError = AddMessage(FormError, add2.RequiredErrorMessage.Replace("%s", add2.Caption));
				}
				if (add3.Required) {
					if (!add3.IsDetailKey && Empty(add3.FormValue))
						FormError = AddMessage(FormError, add3.RequiredErrorMessage.Replace("%s", add3.Caption));
				}
				if (add4.Required) {
					if (!add4.IsDetailKey && Empty(add4.FormValue))
						FormError = AddMessage(FormError, add4.RequiredErrorMessage.Replace("%s", add4.Caption));
				}
				if (area.Required) {
					if (!area.IsDetailKey && Empty(area.FormValue))
						FormError = AddMessage(FormError, area.RequiredErrorMessage.Replace("%s", area.Caption));
				}
				if (postcode.Required) {
					if (!postcode.IsDetailKey && Empty(postcode.FormValue))
						FormError = AddMessage(FormError, postcode.RequiredErrorMessage.Replace("%s", postcode.Caption));
				}
				if (state.Required) {
					if (!state.IsDetailKey && Empty(state.FormValue))
						FormError = AddMessage(FormError, state.RequiredErrorMessage.Replace("%s", state.Caption));
				}
				if (country.Required) {
					if (!country.IsDetailKey && Empty(country.FormValue))
						FormError = AddMessage(FormError, country.RequiredErrorMessage.Replace("%s", country.Caption));
				}
				if (tel1.Required) {
					if (!tel1.IsDetailKey && Empty(tel1.FormValue))
						FormError = AddMessage(FormError, tel1.RequiredErrorMessage.Replace("%s", tel1.Caption));
				}
				if (tel2.Required) {
					if (!tel2.IsDetailKey && Empty(tel2.FormValue))
						FormError = AddMessage(FormError, tel2.RequiredErrorMessage.Replace("%s", tel2.Caption));
				}
				if (phone1.Required) {
					if (!phone1.IsDetailKey && Empty(phone1.FormValue))
						FormError = AddMessage(FormError, phone1.RequiredErrorMessage.Replace("%s", phone1.Caption));
				}
				if (phone2.Required) {
					if (!phone2.IsDetailKey && Empty(phone2.FormValue))
						FormError = AddMessage(FormError, phone2.RequiredErrorMessage.Replace("%s", phone2.Caption));
				}
				if (fax.Required) {
					if (!fax.IsDetailKey && Empty(fax.FormValue))
						FormError = AddMessage(FormError, fax.RequiredErrorMessage.Replace("%s", fax.Caption));
				}
				if (_email.Required) {
					if (!_email.IsDetailKey && Empty(_email.FormValue))
						FormError = AddMessage(FormError, _email.RequiredErrorMessage.Replace("%s", _email.Caption));
				}
				if (ptc1.Required) {
					if (!ptc1.IsDetailKey && Empty(ptc1.FormValue))
						FormError = AddMessage(FormError, ptc1.RequiredErrorMessage.Replace("%s", ptc1.Caption));
				}
				if (ptc.Required) {
					if (!ptc.IsDetailKey && Empty(ptc.FormValue))
						FormError = AddMessage(FormError, ptc.RequiredErrorMessage.Replace("%s", ptc.Caption));
				}
				if (ptc2.Required) {
					if (!ptc2.IsDetailKey && Empty(ptc2.FormValue))
						FormError = AddMessage(FormError, ptc2.RequiredErrorMessage.Replace("%s", ptc2.Caption));
				}
				if (ar_grp.Required) {
					if (!ar_grp.IsDetailKey && Empty(ar_grp.FormValue))
						FormError = AddMessage(FormError, ar_grp.RequiredErrorMessage.Replace("%s", ar_grp.Caption));
				}
				if (term_code.Required) {
					if (!term_code.IsDetailKey && Empty(term_code.FormValue))
						FormError = AddMessage(FormError, term_code.RequiredErrorMessage.Replace("%s", term_code.Caption));
				}
				if (pterm_code.Required) {
					if (!pterm_code.IsDetailKey && Empty(pterm_code.FormValue))
						FormError = AddMessage(FormError, pterm_code.RequiredErrorMessage.Replace("%s", pterm_code.Caption));
				}
				if (cr_limit.Required) {
					if (!cr_limit.IsDetailKey && Empty(cr_limit.FormValue))
						FormError = AddMessage(FormError, cr_limit.RequiredErrorMessage.Replace("%s", cr_limit.Caption));
				}
				if (!CheckNumber(cr_limit.FormValue))
					FormError = AddMessage(FormError, cr_limit.ErrorMessage);
				if (CurrencyCode.Required) {
					if (!CurrencyCode.IsDetailKey && Empty(CurrencyCode.FormValue))
						FormError = AddMessage(FormError, CurrencyCode.RequiredErrorMessage.Replace("%s", CurrencyCode.Caption));
				}
				if (slsman.Required) {
					if (!slsman.IsDetailKey && Empty(slsman.FormValue))
						FormError = AddMessage(FormError, slsman.RequiredErrorMessage.Replace("%s", slsman.Caption));
				}
				if (ytd_sale.Required) {
					if (!ytd_sale.IsDetailKey && Empty(ytd_sale.FormValue))
						FormError = AddMessage(FormError, ytd_sale.RequiredErrorMessage.Replace("%s", ytd_sale.Caption));
				}
				if (!CheckNumber(ytd_sale.FormValue))
					FormError = AddMessage(FormError, ytd_sale.ErrorMessage);
				if (ytd_cost.Required) {
					if (!ytd_cost.IsDetailKey && Empty(ytd_cost.FormValue))
						FormError = AddMessage(FormError, ytd_cost.RequiredErrorMessage.Replace("%s", ytd_cost.Caption));
				}
				if (!CheckNumber(ytd_cost.FormValue))
					FormError = AddMessage(FormError, ytd_cost.ErrorMessage);
				if (ytd_disc.Required) {
					if (!ytd_disc.IsDetailKey && Empty(ytd_disc.FormValue))
						FormError = AddMessage(FormError, ytd_disc.RequiredErrorMessage.Replace("%s", ytd_disc.Caption));
				}
				if (!CheckNumber(ytd_disc.FormValue))
					FormError = AddMessage(FormError, ytd_disc.ErrorMessage);
				if (ctrl_acct.Required) {
					if (!ctrl_acct.IsDetailKey && Empty(ctrl_acct.FormValue))
						FormError = AddMessage(FormError, ctrl_acct.RequiredErrorMessage.Replace("%s", ctrl_acct.Caption));
				}
				if (ctrl_dept.Required) {
					if (!ctrl_dept.IsDetailKey && Empty(ctrl_dept.FormValue))
						FormError = AddMessage(FormError, ctrl_dept.RequiredErrorMessage.Replace("%s", ctrl_dept.Caption));
				}
				if (acct_code.Required) {
					if (!acct_code.IsDetailKey && Empty(acct_code.FormValue))
						FormError = AddMessage(FormError, acct_code.RequiredErrorMessage.Replace("%s", acct_code.Caption));
				}
				if (acct_dept.Required) {
					if (!acct_dept.IsDetailKey && Empty(acct_dept.FormValue))
						FormError = AddMessage(FormError, acct_dept.RequiredErrorMessage.Replace("%s", acct_dept.Caption));
				}
				if (skip_stmt.Required) {
					if (Empty(skip_stmt.FormValue))
						FormError = AddMessage(FormError, skip_stmt.RequiredErrorMessage.Replace("%s", skip_stmt.Caption));
				}
				if (stop_sale.Required) {
					if (Empty(stop_sale.FormValue))
						FormError = AddMessage(FormError, stop_sale.RequiredErrorMessage.Replace("%s", stop_sale.Caption));
				}
				if (opn_item.Required) {
					if (Empty(opn_item.FormValue))
						FormError = AddMessage(FormError, opn_item.RequiredErrorMessage.Replace("%s", opn_item.Caption));
				}
				if (status.Required) {
					if (!status.IsDetailKey && Empty(status.FormValue))
						FormError = AddMessage(FormError, status.RequiredErrorMessage.Replace("%s", status.Caption));
				}
				if (tax_desc.Required) {
					if (!tax_desc.IsDetailKey && Empty(tax_desc.FormValue))
						FormError = AddMessage(FormError, tax_desc.RequiredErrorMessage.Replace("%s", tax_desc.Caption));
				}
				if (stax.Required) {
					if (!stax.IsDetailKey && Empty(stax.FormValue))
						FormError = AddMessage(FormError, stax.RequiredErrorMessage.Replace("%s", stax.Caption));
				}
				if (remark.Required) {
					if (!remark.IsDetailKey && Empty(remark.FormValue))
						FormError = AddMessage(FormError, remark.RequiredErrorMessage.Replace("%s", remark.Caption));
				}
				if (inv_fmt.Required) {
					if (!inv_fmt.IsDetailKey && Empty(inv_fmt.FormValue))
						FormError = AddMessage(FormError, inv_fmt.RequiredErrorMessage.Replace("%s", inv_fmt.Caption));
				}
				if (do_fmt.Required) {
					if (!do_fmt.IsDetailKey && Empty(do_fmt.FormValue))
						FormError = AddMessage(FormError, do_fmt.RequiredErrorMessage.Replace("%s", do_fmt.Caption));
				}
				if (Ship_Code.Required) {
					if (!Ship_Code.IsDetailKey && Empty(Ship_Code.FormValue))
						FormError = AddMessage(FormError, Ship_Code.RequiredErrorMessage.Replace("%s", Ship_Code.Caption));
				}
				if (custtype.Required) {
					if (!custtype.IsDetailKey && Empty(custtype.FormValue))
						FormError = AddMessage(FormError, custtype.RequiredErrorMessage.Replace("%s", custtype.Caption));
				}
				if (Acct_BillAcct.Required) {
					if (!Acct_BillAcct.IsDetailKey && Empty(Acct_BillAcct.FormValue))
						FormError = AddMessage(FormError, Acct_BillAcct.RequiredErrorMessage.Replace("%s", Acct_BillAcct.Caption));
				}
				if (bill_flag.Required) {
					if (!bill_flag.IsDetailKey && Empty(bill_flag.FormValue))
						FormError = AddMessage(FormError, bill_flag.RequiredErrorMessage.Replace("%s", bill_flag.Caption));
				}
				if (payment_code.Required) {
					if (!payment_code.IsDetailKey && Empty(payment_code.FormValue))
						FormError = AddMessage(FormError, payment_code.RequiredErrorMessage.Replace("%s", payment_code.Caption));
				}
				if (stax_pct.Required) {
					if (!stax_pct.IsDetailKey && Empty(stax_pct.FormValue))
						FormError = AddMessage(FormError, stax_pct.RequiredErrorMessage.Replace("%s", stax_pct.Caption));
				}
				if (!CheckNumber(stax_pct.FormValue))
					FormError = AddMessage(FormError, stax_pct.ErrorMessage);
				if (db_part.Required) {
					if (!db_part.IsDetailKey && Empty(db_part.FormValue))
						FormError = AddMessage(FormError, db_part.RequiredErrorMessage.Replace("%s", db_part.Caption));
				}
				if (b_code.Required) {
					if (!b_code.IsDetailKey && Empty(b_code.FormValue))
						FormError = AddMessage(FormError, b_code.RequiredErrorMessage.Replace("%s", b_code.Caption));
				}
				if (lmw_no.Required) {
					if (!lmw_no.IsDetailKey && Empty(lmw_no.FormValue))
						FormError = AddMessage(FormError, lmw_no.RequiredErrorMessage.Replace("%s", lmw_no.Caption));
				}
				if (cs_code.Required) {
					if (!cs_code.IsDetailKey && Empty(cs_code.FormValue))
						FormError = AddMessage(FormError, cs_code.RequiredErrorMessage.Replace("%s", cs_code.Caption));
				}
				if (approved.Required) {
					if (Empty(approved.FormValue))
						FormError = AddMessage(FormError, approved.RequiredErrorMessage.Replace("%s", approved.Caption));
				}
				if (oversea.Required) {
					if (Empty(oversea.FormValue))
						FormError = AddMessage(FormError, oversea.RequiredErrorMessage.Replace("%s", oversea.Caption));
				}
				if (defa_disc_pct.Required) {
					if (!defa_disc_pct.IsDetailKey && Empty(defa_disc_pct.FormValue))
						FormError = AddMessage(FormError, defa_disc_pct.RequiredErrorMessage.Replace("%s", defa_disc_pct.Caption));
				}
				if (!CheckNumber(defa_disc_pct.FormValue))
					FormError = AddMessage(FormError, defa_disc_pct.ErrorMessage);
				if (sellpriceDOM.Required) {
					if (Empty(sellpriceDOM.FormValue))
						FormError = AddMessage(FormError, sellpriceDOM.RequiredErrorMessage.Replace("%s", sellpriceDOM.Caption));
				}
				if (id_upd.Required) {
					if (!id_upd.IsDetailKey && Empty(id_upd.FormValue))
						FormError = AddMessage(FormError, id_upd.RequiredErrorMessage.Replace("%s", id_upd.Caption));
				}
				if (dt_upd.Required) {
					if (!dt_upd.IsDetailKey && Empty(dt_upd.FormValue))
						FormError = AddMessage(FormError, dt_upd.RequiredErrorMessage.Replace("%s", dt_upd.Caption));
				}
				if (!CheckDate(dt_upd.FormValue))
					FormError = AddMessage(FormError, dt_upd.ErrorMessage);
				if (com_regno.Required) {
					if (!com_regno.IsDetailKey && Empty(com_regno.FormValue))
						FormError = AddMessage(FormError, com_regno.RequiredErrorMessage.Replace("%s", com_regno.Caption));
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

				// dbcode
				dbcode.SetDbValue(rsnew, dbcode.CurrentValue, "", dbcode.ReadOnly);

				// name
				name.SetDbValue(rsnew, name.CurrentValue, "", name.ReadOnly);

				// name2
				name2.SetDbValue(rsnew, name2.CurrentValue, System.DBNull.Value, name2.ReadOnly);

				// short_name
				short_name.SetDbValue(rsnew, short_name.CurrentValue, System.DBNull.Value, short_name.ReadOnly);

				// add1
				add1.SetDbValue(rsnew, add1.CurrentValue, System.DBNull.Value, add1.ReadOnly);

				// add2
				add2.SetDbValue(rsnew, add2.CurrentValue, System.DBNull.Value, add2.ReadOnly);

				// add3
				add3.SetDbValue(rsnew, add3.CurrentValue, System.DBNull.Value, add3.ReadOnly);

				// add4
				add4.SetDbValue(rsnew, add4.CurrentValue, System.DBNull.Value, add4.ReadOnly);

				// area
				area.SetDbValue(rsnew, area.CurrentValue, System.DBNull.Value, area.ReadOnly);

				// postcode
				postcode.SetDbValue(rsnew, postcode.CurrentValue, System.DBNull.Value, postcode.ReadOnly);

				// state
				state.SetDbValue(rsnew, state.CurrentValue, System.DBNull.Value, state.ReadOnly);

				// country
				country.SetDbValue(rsnew, country.CurrentValue, System.DBNull.Value, country.ReadOnly);

				// tel1
				tel1.SetDbValue(rsnew, tel1.CurrentValue, System.DBNull.Value, tel1.ReadOnly);

				// tel2
				tel2.SetDbValue(rsnew, tel2.CurrentValue, System.DBNull.Value, tel2.ReadOnly);

				// phone1
				phone1.SetDbValue(rsnew, phone1.CurrentValue, System.DBNull.Value, phone1.ReadOnly);

				// phone2
				phone2.SetDbValue(rsnew, phone2.CurrentValue, System.DBNull.Value, phone2.ReadOnly);

				// fax
				fax.SetDbValue(rsnew, fax.CurrentValue, System.DBNull.Value, fax.ReadOnly);

				// _email
				_email.SetDbValue(rsnew, _email.CurrentValue, System.DBNull.Value, _email.ReadOnly);

				// ptc1
				ptc1.SetDbValue(rsnew, ptc1.CurrentValue, System.DBNull.Value, ptc1.ReadOnly);

				// ptc
				ptc.SetDbValue(rsnew, ptc.CurrentValue, System.DBNull.Value, ptc.ReadOnly);

				// ptc2
				ptc2.SetDbValue(rsnew, ptc2.CurrentValue, System.DBNull.Value, ptc2.ReadOnly);

				// ar_grp
				ar_grp.SetDbValue(rsnew, ar_grp.CurrentValue, System.DBNull.Value, ar_grp.ReadOnly);

				// term_code
				term_code.SetDbValue(rsnew, term_code.CurrentValue, System.DBNull.Value, term_code.ReadOnly);

				// pterm_code
				pterm_code.SetDbValue(rsnew, pterm_code.CurrentValue, System.DBNull.Value, pterm_code.ReadOnly);

				// cr_limit
				cr_limit.SetDbValue(rsnew, cr_limit.CurrentValue, System.DBNull.Value, cr_limit.ReadOnly);

				// CurrencyCode
				CurrencyCode.SetDbValue(rsnew, CurrencyCode.CurrentValue, System.DBNull.Value, CurrencyCode.ReadOnly);

				// slsman
				slsman.SetDbValue(rsnew, slsman.CurrentValue, System.DBNull.Value, slsman.ReadOnly);

				// ytd_sale
				ytd_sale.SetDbValue(rsnew, ytd_sale.CurrentValue, System.DBNull.Value, ytd_sale.ReadOnly);

				// ytd_cost
				ytd_cost.SetDbValue(rsnew, ytd_cost.CurrentValue, System.DBNull.Value, ytd_cost.ReadOnly);

				// ytd_disc
				ytd_disc.SetDbValue(rsnew, ytd_disc.CurrentValue, System.DBNull.Value, ytd_disc.ReadOnly);

				// ctrl_acct
				ctrl_acct.SetDbValue(rsnew, ctrl_acct.CurrentValue, System.DBNull.Value, ctrl_acct.ReadOnly);

				// ctrl_dept
				ctrl_dept.SetDbValue(rsnew, ctrl_dept.CurrentValue, System.DBNull.Value, ctrl_dept.ReadOnly);

				// acct_code
				acct_code.SetDbValue(rsnew, acct_code.CurrentValue, System.DBNull.Value, acct_code.ReadOnly);

				// acct_dept
				acct_dept.SetDbValue(rsnew, acct_dept.CurrentValue, System.DBNull.Value, acct_dept.ReadOnly);

				// skip_stmt
				skip_stmt.SetDbValue(rsnew, ConvertToBool(skip_stmt.CurrentValue, "1", "0"), System.DBNull.Value, skip_stmt.ReadOnly); // DN1204

				// stop_sale
				stop_sale.SetDbValue(rsnew, ConvertToBool(stop_sale.CurrentValue, "1", "0"), System.DBNull.Value, stop_sale.ReadOnly); // DN1204

				// opn_item
				opn_item.SetDbValue(rsnew, ConvertToBool(opn_item.CurrentValue, "1", "0"), System.DBNull.Value, opn_item.ReadOnly); // DN1204

				// status
				status.SetDbValue(rsnew, status.CurrentValue, System.DBNull.Value, status.ReadOnly);

				// tax_desc
				tax_desc.SetDbValue(rsnew, tax_desc.CurrentValue, System.DBNull.Value, tax_desc.ReadOnly);

				// stax
				stax.SetDbValue(rsnew, stax.CurrentValue, System.DBNull.Value, stax.ReadOnly);

				// remark
				remark.SetDbValue(rsnew, remark.CurrentValue, System.DBNull.Value, remark.ReadOnly);

				// inv_fmt
				inv_fmt.SetDbValue(rsnew, inv_fmt.CurrentValue, System.DBNull.Value, inv_fmt.ReadOnly);

				// do_fmt
				do_fmt.SetDbValue(rsnew, do_fmt.CurrentValue, System.DBNull.Value, do_fmt.ReadOnly);

				// Ship_Code
				Ship_Code.SetDbValue(rsnew, Ship_Code.CurrentValue, System.DBNull.Value, Ship_Code.ReadOnly);

				// custtype
				custtype.SetDbValue(rsnew, custtype.CurrentValue, System.DBNull.Value, custtype.ReadOnly);

				// Acct_BillAcct
				Acct_BillAcct.SetDbValue(rsnew, Acct_BillAcct.CurrentValue, System.DBNull.Value, Acct_BillAcct.ReadOnly);

				// bill_flag
				bill_flag.SetDbValue(rsnew, bill_flag.CurrentValue, System.DBNull.Value, bill_flag.ReadOnly);

				// payment_code
				payment_code.SetDbValue(rsnew, payment_code.CurrentValue, System.DBNull.Value, payment_code.ReadOnly);

				// stax_pct
				stax_pct.SetDbValue(rsnew, stax_pct.CurrentValue, System.DBNull.Value, stax_pct.ReadOnly);

				// db_part
				db_part.SetDbValue(rsnew, db_part.CurrentValue, System.DBNull.Value, db_part.ReadOnly);

				// b_code
				b_code.SetDbValue(rsnew, b_code.CurrentValue, System.DBNull.Value, b_code.ReadOnly);

				// lmw_no
				lmw_no.SetDbValue(rsnew, lmw_no.CurrentValue, System.DBNull.Value, lmw_no.ReadOnly);

				// cs_code
				cs_code.SetDbValue(rsnew, cs_code.CurrentValue, System.DBNull.Value, cs_code.ReadOnly);

				// approved
				approved.SetDbValue(rsnew, ConvertToBool(approved.CurrentValue, "1", "0"), System.DBNull.Value, approved.ReadOnly); // DN1204

				// oversea
				oversea.SetDbValue(rsnew, ConvertToBool(oversea.CurrentValue, "1", "0"), System.DBNull.Value, oversea.ReadOnly); // DN1204

				// defa_disc_pct
				defa_disc_pct.SetDbValue(rsnew, defa_disc_pct.CurrentValue, System.DBNull.Value, defa_disc_pct.ReadOnly);

				// sellpriceDOM
				sellpriceDOM.SetDbValue(rsnew, ConvertToBool(sellpriceDOM.CurrentValue, "1", "0"), System.DBNull.Value, sellpriceDOM.ReadOnly); // DN1204

				// id_upd
				id_upd.SetDbValue(rsnew, id_upd.CurrentValue, System.DBNull.Value, id_upd.ReadOnly);

				// dt_upd
				dt_upd.SetDbValue(rsnew, UnformatDateTime(dt_upd.CurrentValue, 0), System.DBNull.Value, dt_upd.ReadOnly);

				// com_regno
				com_regno.SetDbValue(rsnew, com_regno.CurrentValue, System.DBNull.Value, com_regno.ReadOnly);

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
				breadcrumb.Add("list", TableVar, AppPath(AddMasterUrl("s_armasterlist")), "", TableVar, true);
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
