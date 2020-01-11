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
		/// s_dohdrtest_View
		/// </summary>

		public static _s_dohdrtest_View s_dohdrtest_View {
			get => HttpData.Get<_s_dohdrtest_View>("s_dohdrtest_View");
			set => HttpData["s_dohdrtest_View"] = value;
		}

		/// <summary>
		/// Page class for s_dohdrtest
		/// </summary>

		public class _s_dohdrtest_View : _s_dohdrtest_ViewBase
		{

			// Construtor
			public _s_dohdrtest_View(Controller controller = null) : base(controller) {
			}
		}

		/// <summary>
		/// Page base class
		/// </summary>

		public class _s_dohdrtest_ViewBase : _s_dohdrtest, IAspNetMakerPage
		{

			// Page ID
			public string PageID = "view";

			// Project ID
			public string ProjectID = "{8543F230-11C6-4105-B51C-8D87C21BE659}";

			// Table name
			public string TableName = "s_dohdrtest";

			// Page object name
			public string PageObjName = "s_dohdrtest_View";

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

			// Export URLs
			public string ExportPrintUrl = "";
			public string ExportHtmlUrl = "";
			public string ExportExcelUrl = "";
			public string ExportWordUrl = "";
			public string ExportXmlUrl = "";
			public string ExportCsvUrl = "";
			public string ExportPdfUrl = "";

			// Update URLs
			public string InlineAddUrl = "";
			public string GridAddUrl = "";
			public string GridEditUrl = "";
			public string MultiDeleteUrl = "";
			public string MultiUpdateUrl = "";

			// Custom export
			public bool ExportExcelCustom = false;
			public bool ExportWordCustom = false;
			public bool ExportPdfCustom = false;
			public bool ExportEmailCustom = false;

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
			public _s_dohdrtest_ViewBase(Controller controller = null) { // DN
				if (controller != null)
					Controller = controller;

				// Initialize
				CurrentPage = this;

				// Language object
				Language = Language ?? new Lang();

				// Table object (s_dohdrtest)
				if (s_dohdrtest == null || s_dohdrtest is _s_dohdrtest)
					s_dohdrtest = this;

				// DN
				string keyUrl = "";
				string[] keys = new string[0];
				StringValues str = "";
				object obj = null;
				string currentPageName = CurrentPageName();
				if (IsApi()) {
					if (RouteValues.TryGetValue("key", out object k) && !Empty(k))
						keys = k.ToString().Split('/');
					if (keys.Length > 0)
						RecordKeys["TrxId"] = keys[0];
				} else {
					RecordKeys["TrxId"] = RouteValues.TryGetValue("TrxId", out obj) ? Convert.ToString(obj) : Get("TrxId");
					keyUrl += "/" + UrlEncode(RecordKeys["TrxId"]);
				}
				ExportPrintUrl = currentPageName + keyUrl + "?export=print";
				ExportHtmlUrl = currentPageName + keyUrl + "?export=html";
				ExportExcelUrl = currentPageName + keyUrl + "?export=excel";
				ExportWordUrl = currentPageName + keyUrl + "?export=word";
				ExportXmlUrl = currentPageName + keyUrl + "?export=xml";
				ExportCsvUrl = currentPageName + keyUrl + "?export=csv";
				ExportPdfUrl = currentPageName + keyUrl + "?export=pdf";

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

				// Export options
				ExportOptions = new ListOptions { Tag = "div", TagClassName = "ew-export-option" };

				// Other options
				OtherOptions["action"] = new ListOptions { Tag = "div", TagClassName = "ew-action-option" };
				OtherOptions["detail"] = new ListOptions { Tag = "div", TagClassName = "ew-detail-option" };
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
			public int DisplayRecords = 1; // Number of display records
			public int StartRecord;
			public int StopRecord;
			public int TotalRecords = -1;
			public int RecordRange = 10;
			public int RecordCount;
			public Dictionary<string, string> RecordKeys = new Dictionary<string, string>();
			public bool IsModal = false;
			public string SearchWhere = "";
			public string DbMasterFilter;
			public string DbDetailFilter;
			public bool MasterRecordExists;
			public ListOptions ExportOptions; // Export options
			public ListOptionsDictionary OtherOptions = new ListOptionsDictionary(); // Other options
			public DbDataReader Recordset;

			#pragma warning disable 168, 219

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
					if (!Security.CanView) {
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
				CurrentAction = Param("action"); // Set up current action
				TrxId.SetVisibility();
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
				string returnUrl = "";
				bool matchRecord = false;
				if (IsPageRequest) { // Validate request
					string[] keyValues = null;
					object v;
					if (IsApi() && RouteValues.TryGetValue("key", out object k))
						if (!Empty(k))
							keyValues = k.ToString().Split('/');
						else
							return new JsonBoolResult(new { success = false, error = Language.Phrase("NoRecord"), version = Config.ProductVersion }, false);
					if (RouteValues.TryGetValue("TrxId", out v) && !Empty(v)) { // DN
						TrxId.QueryValue = Convert.ToString(v);
						RecordKeys["TrxId"] = TrxId.QueryValue;
					} else if (!Empty(Get("TrxId"))) {
						TrxId.QueryValue = Get("TrxId");
						RecordKeys["TrxId"] = TrxId.QueryValue;
					} else if (IsApi() && !Empty(keyValues)) {
						TrxId.QueryValue = Convert.ToString(keyValues[0]);
						RecordKeys["TrxId"] = TrxId.QueryValue;
					} else {
						returnUrl = "s_dohdrtestlist"; // Return to list
					}

					// Get action
					CurrentAction = "show"; // Display form
					switch (CurrentAction) {
						case "show": // Get a record to display
							bool res;
							if (IsApi()) {
								string filter = GetRecordFilter();
								CurrentFilter = filter;
								string sql = CurrentSql;
								var conn = await GetConnectionAsync();
								Recordset = await conn.GetDataReaderAsync(sql);
								res = !Empty(Recordset) && await Recordset.ReadAsync();
							} else {
								res = await LoadRow();
							}
							if (!res) { // Load record based on key
								if (Empty(SuccessMessage) && Empty(FailureMessage))
									FailureMessage = Language.Phrase("NoRecord"); // Set no record message
								if (IsApi()) {
									if (!Empty(SuccessMessage))
										return new JsonBoolResult(new { success = true, message = SuccessMessage, version = Config.ProductVersion }, true);
									else
										return new JsonBoolResult(new { success = false, error = FailureMessage, version = Config.ProductVersion }, false);
								} else {
									return Terminate("s_dohdrtestlist"); // Return to list page
								}
							}
							break;
					}
				} else {
					returnUrl = "s_dohdrtestlist"; // Not page request, return to list
				}
				if (!Empty(returnUrl))
					return Terminate(returnUrl);

				// Render row
				RowType = Config.RowTypeView;
				ResetAttributes();
				await RenderRow();

				// Set up Breadcrumb
				if (!IsExport())
					SetupBreadcrumb();

				// Set up detail parameters
				SetupDetailParms();

				// Normal return
				if (IsApi()) {
					var rows = await GetRecordFromRecordset(Recordset); // Get current record only
					return Controller.Json(new Dictionary<string, object> { {"success", true}, {TableVar, rows}, {"version", Config.ProductVersion} });
				}
				return PageResult();
			}

			#pragma warning restore 168, 219

			// Set up other options

			#pragma warning disable 168, 219 

			public void SetupOtherOptions() 
			{
				var options = OtherOptions;
				var option = options["action"];
				ListOption item;
				string links = "";

				// Add
				item = option.Add("add");
				string addcaption = HtmlTitle(Language.Phrase("ViewPageAddLink"));
				if (IsModal) // Modal
					item.Body = "<a class=\"ew-action ew-add\" title=\"" + addcaption + "\" data-caption=\"" + addcaption + "\" href=\"javascript:void(0);\" onclick=\"ew.modalDialogShow({lnk:this,url:'" + HtmlEncode(AppPath(AddUrl)) + "'});\">" + Language.Phrase("ViewPageAddLink") + "</a>";
				else
					item.Body = "<a class=\"ew-action ew-add\" title=\"" + addcaption + "\" data-caption=\"" + addcaption + "\" href=\"" + HtmlEncode(AppPath(AddUrl)) + "\">" + Language.Phrase("ViewPageAddLink") + "</a>";
					item.Visible = (AddUrl != "" && Security.CanAdd);

				// Edit
				item = option.Add("edit");
				var editcaption = HtmlTitle(Language.Phrase("ViewPageEditLink"));
				if (IsModal) // Modal
					item.Body = "<a class=\"ew-action ew-edit\" title=\"" + editcaption + "\" data-caption=\"" + editcaption + "\" href=\"javascript:void(0);\" onclick=\"ew.modalDialogShow({lnk:this,url:'" + HtmlEncode(AppPath(EditUrl)) + "'});\">" + Language.Phrase("ViewPageEditLink") + "</a>";
				else
					item.Body = "<a class=\"ew-action ew-edit\" title=\"" + editcaption + "\" data-caption=\"" + editcaption + "\" href=\"" + HtmlEncode(AppPath(EditUrl)) + "\">" + Language.Phrase("ViewPageEditLink") + "</a>";
					item.Visible = (EditUrl != "" && Security.CanEdit);

				// Copy
				item = option.Add("copy");
				var copycaption = HtmlTitle(Language.Phrase("ViewPageCopyLink"));
				if (IsModal) // Modal
					item.Body = "<a class=\"ew-action ew-copy\" title=\"" + copycaption + "\" data-caption=\"" + copycaption + "\" href=\"javascript:void(0);\" onclick=\"ew.modalDialogShow({lnk:this,url:'" + HtmlEncode(AppPath(CopyUrl)) + "'});\">" + Language.Phrase("ViewPageCopyLink") + "</a>";
				else
					item.Body = "<a class=\"ew-action ew-copy\" title=\"" + copycaption + "\" data-caption=\"" + copycaption + "\" href=\"" + HtmlEncode(AppPath(CopyUrl)) + "\">" + Language.Phrase("ViewPageCopyLink") + "</a>";
					item.Visible = (CopyUrl != "" && Security.CanAdd);

				// Delete
				item = option.Add("delete");
				if (IsModal) // Handle as inline delete
					item.Body = "<a onclick=\"return ew.confirmDelete(this);\" class=\"ew-action ew-delete\" title=\"" + HtmlTitle(Language.Phrase("ViewPageDeleteLink")) + "\" data-caption=\"" + HtmlTitle(Language.Phrase("ViewPageDeleteLink")) + "\" href=\"" + HtmlEncode(UrlAddQuery(AppPath(DeleteUrl), "action=1")) + "\">" + Language.Phrase("ViewPageDeleteLink") + "</a>";
				else
					item.Body = "<a class=\"ew-action ew-delete\" title=\"" + HtmlTitle(Language.Phrase("ViewPageDeleteLink")) + "\" data-caption=\"" + HtmlTitle(Language.Phrase("ViewPageDeleteLink")) + "\" href=\"" + HtmlEncode(AppPath(DeleteUrl)) + "\">" + Language.Phrase("ViewPageDeleteLink") + "</a>";
				item.Visible = (DeleteUrl != "" && Security.CanDelete);
				string body = "";
				option = options["detail"];
				string detailTableLink = "";
				string detailViewTblVar = "";
				string detailCopyTblVar = "";
				string detailEditTblVar = "";

				// "detail_s_dodetltest"
				item = option.Add("detail_s_dodetltest");
				body = Language.Phrase("ViewPageDetailLink") + Language.TablePhrase("s_dodetltest", "TblCaption");
				body = "<a class=\"btn btn-default btn-sm ew-row-link ew-detail\" data-action=\"list\" href=\"" + HtmlEncode(AppPath("s_dodetltestlist?" + Config.TableShowMaster + "=s_dohdrtest&fk_TrxId=" + UrlEncode(Convert.ToString(TrxId.CurrentValue)) + "")) + "\">" + body + "</a>";
				links = "";
				if (Empty(s_dodetltest_Grid))
					s_dodetltest_Grid = new _s_dodetltest_Grid();
				if (s_dodetltest_Grid.DetailView && Security.CanView && Security.AllowView(String.Concat(CurrentProjectID, "s_dodetltest"))) {
					links += "<li><a class=\"ew-row-link ew-detail-view dropdown-item\" data-action=\"view\" data-caption=\"" + HtmlTitle(Language.Phrase("MasterDetailViewLink")) + "\" href=\"" + HtmlEncode(AppPath(GetViewUrl(Config.TableShowDetail + "=s_dodetltest"))) + "\">" + HtmlImageAndText(Language.Phrase("MasterDetailViewLink")) + "</a></li>";
					if (!Empty(detailViewTblVar)) detailViewTblVar += ",";
					detailViewTblVar += "s_dodetltest";
				}
				if (s_dodetltest_Grid.DetailEdit && Security.CanEdit && Security.AllowEdit(String.Concat(CurrentProjectID, "s_dodetltest"))) {
					links += "<li><a class=\"ew-row-link ew-detail-edit dropdown-item\" data-action=\"edit\" data-caption=\"" + HtmlTitle(Language.Phrase("MasterDetailEditLink")) + "\" href=\"" + HtmlEncode(AppPath(GetEditUrl(Config.TableShowDetail + "=s_dodetltest"))) + "\">" + HtmlImageAndText(Language.Phrase("MasterDetailEditLink")) + "</a></li>";
					if (!Empty(detailEditTblVar)) detailEditTblVar += ",";
					detailEditTblVar += "s_dodetltest";
				}
				if (s_dodetltest_Grid.DetailAdd && Security.CanAdd && Security.AllowAdd(String.Concat(CurrentProjectID, "s_dodetltest"))) {
					links += "<li><a class=\"ew-row-link ew-detail-copy dropdown-item\" data-action=\"add\" data-caption=\"" + HtmlTitle(Language.Phrase("MasterDetailCopyLink")) + "\" href=\"" + HtmlEncode(AppPath(GetCopyUrl(Config.TableShowDetail + "=s_dodetltest"))) + "\">" + HtmlImageAndText(Language.Phrase("MasterDetailCopyLink")) + "</a></li>";
					if (!Empty(detailCopyTblVar)) detailCopyTblVar += ",";
					detailCopyTblVar += "s_dodetltest";
				}
				if (!Empty(links)) {
					body += "<button class=\"dropdown-toggle btn btn-default ew-detail\" data-toggle=\"dropdown\"></button>";
					body += "<ul class=\"dropdown-menu\">" + links + "</ul>";
				}
				body = "<div class=\"btn-group btn-group-sm ew-btn-group\">" + body + "</div>";
				item.Body = body;
				item.Visible = Security.AllowList(String.Concat(CurrentProjectID, "s_dodetltest"));
				if (item.Visible) {
					if (!Empty(detailTableLink)) detailTableLink += ",";
					detailTableLink += "s_dodetltest";
				}
				if (ShowMultipleDetails) 
					item.Visible = false;

				// Multiple details
				if (ShowMultipleDetails) {
					body = "<div class=\"btn-group btn-group-sm ew-btn-group\">";
					links = "";
					if (!Empty(detailViewTblVar)) {
						links += "<li><a class=\"ew-row-link ew-detail-view\" data-action=\"view\" data-caption=\"" + HtmlTitle(Language.Phrase("MasterDetailViewLink")) + "\" href=\"" + HtmlEncode(AppPath(GetViewUrl(Config.TableShowDetail + "=" + detailViewTblVar))) + "\">" + HtmlImageAndText(Language.Phrase("MasterDetailViewLink")) + "</a></li>";
					}
					if (!Empty(detailEditTblVar)) {
						links += "<li><a class=\"ew-row-link ew-detail-edit\" data-action=\"edit\" data-caption=\"" + HtmlTitle(Language.Phrase("MasterDetailEditLink")) + "\" href=\"" + HtmlEncode(AppPath(GetEditUrl(Config.TableShowDetail + "=" + detailEditTblVar))) + "\">" + HtmlImageAndText(Language.Phrase("MasterDetailEditLink")) + "</a></li>";
					}
					if (!Empty(detailCopyTblVar)) {
						links += "<li><a class=\"ew-row-link ew-detail-copy\" data-action=\"add\" data-caption=\"" + HtmlTitle(Language.Phrase("MasterDetailCopyLink")) + "\" href=\"" + HtmlEncode(AppPath(GetCopyUrl(Config.TableShowDetail + "=" + detailCopyTblVar))) + "\">" + HtmlImageAndText(Language.Phrase("MasterDetailCopyLink")) + "</a></li>";
					}
					if (!Empty(links)) {
						body += "<button class=\"dropdown-toggle btn btn-default btn-sm ew-master-detail\" title=\"" + HtmlTitle(Language.Phrase("MultipleMasterDetails")) + "\" data-toggle=\"dropdown\">" + Language.Phrase("MultipleMasterDetails") + "</button>";
						body += "<ul class=\"dropdown-menu ew-menu\">" + links + "</ul>";
					}
					body += "</div>";

					// Multiple details
					item = option.Add("details");
					item.Body = body;
				}

				// Set up detail default
				option = options["detail"];
				options["detail"].DropDownButtonPhrase = Language.Phrase("ButtonDetails");
				var ar = detailTableLink.Split(',');
				option.UseDropDownButton = (ar.Length > 1);
				option.UseButtonGroup = true;
				item = option.Add(option.GroupOptionName);
				item.Body = "";
				item.Visible = false;

				// Set up action default
				option = options["action"];
				option.DropDownButtonPhrase = Language.Phrase("ButtonActions");

				//option.UseDropDownButton = false;
//Wilson 2019 06 05
//always show action as button  

				option.UseDropDownButton = false;
				option.UseButtonGroup = true;
				item = option.Add(option.GroupOptionName);
				item.Body = "";
				item.Visible = false;
			}

			#pragma warning restore 168, 219

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
				var row = new Dictionary<string, object>();
				row.Add("TrxId", System.DBNull.Value);
				row.Add("dt_rec", System.DBNull.Value);
				row.Add("do_no", System.DBNull.Value);
				row.Add("dbcode", System.DBNull.Value);
				row.Add("slsman", System.DBNull.Value);
				row.Add("fileno", System.DBNull.Value);
				row.Add("TrxUserId", System.DBNull.Value);
				row.Add("CurrencyCode", System.DBNull.Value);
				row.Add("ex_rate", System.DBNull.Value);
				row.Add("do_amount_original", System.DBNull.Value);
				row.Add("do_amount_loca", System.DBNull.Value);
				row.Add("rounding_adj", System.DBNull.Value);
				row.Add("ar_gl_acct", System.DBNull.Value);
				return row;
			}

			#pragma warning disable 1998

			// Render row values based on field settings
			public async Task RenderRow() {
				SetupOtherOptions();

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

					// TrxId
					TrxId.HrefValue = "";
					TrxId.TooltipValue = "";

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
				}

				// Call Row Rendered event
				if (RowType != Config.RowTypeAggregateInit)
					Row_Rendered();
			}

			#pragma warning restore 1998

			// Save data to memory cache
			public void SetCache<T>(string key, T value, int span) => Cache.Set<T>(key, value, new MemoryCacheEntryOptions()
				.SetSlidingExpiration(TimeSpan.FromMilliseconds(span))); // Keep in cache for this time, reset time if accessed

			// Gete data from memory cache
			public void GetCache<T>(string key) => Cache.Get<T>(key);

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
						if (s_dodetltest_Grid.DetailView) {
							s_dodetltest_Grid.CurrentMode = "view";

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
				string pageId = "view";
				breadcrumb.Add("view", pageId, url);
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
		}
	}
}
