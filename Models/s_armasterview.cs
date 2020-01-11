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
		/// s_armaster_View
		/// </summary>

		public static _s_armaster_View s_armaster_View {
			get => HttpData.Get<_s_armaster_View>("s_armaster_View");
			set => HttpData["s_armaster_View"] = value;
		}

		/// <summary>
		/// Page class for s_armaster
		/// </summary>

		public class _s_armaster_View : _s_armaster_ViewBase
		{

			// Construtor
			public _s_armaster_View(Controller controller = null) : base(controller) {
			}
		}

		/// <summary>
		/// Page base class
		/// </summary>

		public class _s_armaster_ViewBase : _s_armaster, IAspNetMakerPage
		{

			// Page ID
			public string PageID = "view";

			// Project ID
			public string ProjectID = "{8543F230-11C6-4105-B51C-8D87C21BE659}";

			// Table name
			public string TableName = "s_armaster";

			// Page object name
			public string PageObjName = "s_armaster_View";

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
			public _s_armaster_ViewBase(Controller controller = null) { // DN
				if (controller != null)
					Controller = controller;

				// Initialize
				CurrentPage = this;

				// Language object
				Language = Language ?? new Lang();

				// Table object (s_armaster)
				if (s_armaster == null || s_armaster is _s_armaster)
					s_armaster = this;

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
						RecordKeys["Id"] = keys[0];
				} else {
					RecordKeys["Id"] = RouteValues.TryGetValue("Id", out obj) ? Convert.ToString(obj) : Get("Id");
					keyUrl += "/" + UrlEncode(RecordKeys["Id"]);
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
							return Terminate(GetUrl("s_armasterlist"));
						else
							return Terminate(GetUrl("login"));
					}
				}
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
					if (RouteValues.TryGetValue("Id", out v) && !Empty(v)) { // DN
						Id.QueryValue = Convert.ToString(v);
						RecordKeys["Id"] = Id.QueryValue;
					} else if (!Empty(Get("Id"))) {
						Id.QueryValue = Get("Id");
						RecordKeys["Id"] = Id.QueryValue;
					} else if (IsApi() && !Empty(keyValues)) {
						Id.QueryValue = Convert.ToString(keyValues[0]);
						RecordKeys["Id"] = Id.QueryValue;
					} else {
						returnUrl = "s_armasterlist"; // Return to list
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
									return Terminate("s_armasterlist"); // Return to list page
								}
							}
							break;
					}
				} else {
					returnUrl = "s_armasterlist"; // Not page request, return to list
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

			#pragma warning disable 1998

			// Render row values based on field settings
			public async Task RenderRow() {
				SetupOtherOptions();

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

			// Set up Breadcrumb
			protected void SetupBreadcrumb() {
				var breadcrumb = new Breadcrumb();
				string url = CurrentUrl();
				breadcrumb.Add("list", TableVar, AppPath(AddMasterUrl("s_armasterlist")), "", TableVar, true);
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
