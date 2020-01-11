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
		/// s_glchart_List
		/// </summary>

		public static _s_glchart_List s_glchart_List {
			get => HttpData.Get<_s_glchart_List>("s_glchart_List");
			set => HttpData["s_glchart_List"] = value;
		}

		/// <summary>
		/// Page class for s_glchart
		/// </summary>

		public class _s_glchart_List : _s_glchart_ListBase
		{

			// Construtor
			public _s_glchart_List(Controller controller = null) : base(controller) {
			}
		}

		/// <summary>
		/// Page base class
		/// </summary>

		public class _s_glchart_ListBase : _s_glchart, IAspNetMakerPage
		{

			// Page ID
			public string PageID = "list";

			// Project ID
			public string ProjectID = "{8543F230-11C6-4105-B51C-8D87C21BE659}";

			// Table name
			public string TableName = "s_glchart";

			// Page object name
			public string PageObjName = "s_glchart_List";

			// Grid form hidden field names
			public string FormName = "fs_glchartlist";
			public string FormActionName = "k_action";
			public string FormKeyName = "k_key";
			public string FormOldKeyName = "k_oldkey";
			public string FormBlankRowName = "k_blankrow";
			public string FormKeyCountName = "key_count";

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
			public _s_glchart_ListBase(Controller controller = null) { // DN
				if (controller != null)
					Controller = controller;

				// Initialize
				CurrentPage = this;

				// Language object
				Language = Language ?? new Lang();

				// Table object (s_glchart)
				if (s_glchart == null || s_glchart is _s_glchart)
					s_glchart = this;

				// Initialize URLs
				ExportPrintUrl = PageUrl + "export=print";
				ExportExcelUrl = PageUrl + "export=excel";
				ExportWordUrl = PageUrl + "export=word";
				ExportHtmlUrl = PageUrl + "export=html";
				ExportXmlUrl = PageUrl + "export=xml";
				ExportCsvUrl = PageUrl + "export=csv";
				ExportPdfUrl = PageUrl + "export=pdf";
				AddUrl = "s_glchartadd";
				InlineAddUrl = PageUrl + "action=add";
				GridAddUrl = PageUrl + "action=gridadd";
				GridEditUrl = PageUrl + "action=gridedit";
				MultiDeleteUrl = "s_glchartdelete";
				MultiUpdateUrl = "s_glchartupdate";

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

				// List options
				ListOptions = new ListOptions { TableVar = TableVar };

				// Export options
				ExportOptions = new ListOptions { Tag = "div", TagClassName = "ew-export-option" };

				// Import options
				ImportOptions = new ListOptions { Tag = "div", TagClassName = "ew-import-option" };

				// Other options
				OtherOptions["addedit"] = new ListOptions { Tag = "div", TagClassName = "ew-add-edit-option" };
				OtherOptions["detail"] = new ListOptions { Tag = "div", TagClassName = "ew-detail-option" };
				OtherOptions["action"] = new ListOptions { Tag = "div", TagClassName = "ew-action-option" };

				// Filter options
				FilterOptions = new ListOptions { Tag = "div", TagClassName = "ew-filter-option fs_glchartlistsrch" };

				// List actions
				ListActions = new ListActions();
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

			// Class properties
			public ListOptions ListOptions; // List options
			public ListOptions ExportOptions; // Export options
			public ListOptions SearchOptions; // Search options
			public ListOptionsDictionary OtherOptions = new ListOptionsDictionary(); // Other options
			public ListOptions FilterOptions; // Filter options
			public ListOptions ImportOptions; // Import options
			public ListActions ListActions; // List actions
			public int SelectedCount = 0;
			public int SelectedIndex = 0;
			public int DisplayRecords = 20; // Number of display records
			public int StartRecord;
			public int StopRecord;
			public int TotalRecords = -1;
			public int RecordRange = 10;
			public string PageSizes = ""; // Page sizes (comma separated)
			public Pager _pager;
			public bool AutoHidePager = Config.AutoHidePager;
			public bool AutoHidePageSizeSelector = Config.AutoHidePageSizeSelector;
			public string DefaultSearchWhere = ""; // Default search WHERE clause
			public string SearchWhere = ""; // Search WHERE clause
			public int RecordCount = 0; // Record count
			public int EditRowCount;
			public int StartRowCount = 1;
			public Dictionary<int, dynamic> Attributes = new Dictionary<int, dynamic>(); // Row attributes and cell attributes
			public object RowIndex = 0; // Row index
			public int KeyCount = 0; // Key count
			public string RowAction = ""; // Row action
			public string RowOldKey = ""; // Row old key (for copy)
			public string MultiColumnClass = "col-sm";
			public string MultiColumnEditClass = "w-100";
			public int MultiColumnCount = 12;
			public int MultiColumnEditCount = 12;
			public string DbMasterFilter = ""; // Master filter
			public string DbDetailFilter = ""; // Detail filter
			public bool MasterRecordExists;
			public string MultiSelectKey = "";
			public bool RestoreSearch = false;
			public SubPages DetailPages;
			public DbDataReader Recordset;
			public DbDataReader OldRecordset;

			// Pager
			public Pager Pager {
				get {
					_pager = _pager ?? new PrevNextPager(StartRecord, RecordsPerPage, TotalRecords, PageSizes, RecordRange, AutoHidePager, AutoHidePageSizeSelector);
					return _pager;
				}
			}

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
					if (!Security.CanList) {
						Security.SaveLastUrl();
						FailureMessage = DeniedMessage(); // Set no permission
						if (IsApi())
							return new JsonBoolResult(new { success = false, error = DeniedMessage(), version = Config.ProductVersion }, false);
						return Terminate(GetUrl("Index"));
					}
				}
				CurrentAction = Param("action"); // Set up current action

				// Get grid add count
				int gridaddcnt = Get<int>(Config.TableGridAddRowCount);
				if (gridaddcnt > 0)
					GridAddRowCount = gridaddcnt;

				// Set up list options
				await SetupListOptions();
				Id.SetVisibility();
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

				// Global Page Loading event
				Page_Loading();

				// Page Load event
				Page_Load();

				// Check token
				if (!await ValidPost())
					End(Language.Phrase("InvalidPostRequest"));

				// Create token
				CreateToken();

				// Setup other options
				SetupOtherOptions();

				// Set up custom action (compatible with old version)
				ListActions.Add(CustomActions);

				// Show checkbox column if multiple action
				if (ListActions.Items.Any(kvp => kvp.Value.Select == Config.ActionMultiple && kvp.Value.Allowed))
					ListOptions["checkbox"].Visible = true;

				// Set up lookup cache
				// Search filters

				string srchBasic = ""; // Basic search filter
				string filter = "";

				// Get command
				Command = Get("cmd").ToLower();
				if (IsPageRequest) { // Validate request

					// Process list action first
					var result = await ProcessListAction();
					if (result != null) // Ajax request
						return result;

					// Handle reset command
					ResetCommand();

					// Set up Breadcrumb
					if (!IsExport())
						SetupBreadcrumb();

					// Hide list options
					if (IsExport()) {
						ListOptions.HideAllOptions(new List<string> {"sequence"});
						ListOptions.UseDropDownButton = false; // Disable drop down button
						ListOptions.UseButtonGroup = false; // Disable button group
					} else if (IsGridAdd || IsGridEdit) {
						ListOptions.HideAllOptions();
						ListOptions.UseDropDownButton = false; // Disable drop down button
						ListOptions.UseButtonGroup = false; // Disable button group
					}

					// Hide options
					if (IsExport() || !Empty(CurrentAction)) {
						ExportOptions.HideAllOptions();
						FilterOptions.HideAllOptions();
						ImportOptions.HideAllOptions();
					}

					// Hide other options
					if (IsExport()) {
						foreach (var (key, value) in OtherOptions)
							value.HideAllOptions();
					}

					// Get default search criteria
					AddFilter(ref DefaultSearchWhere, BasicSearchWhere(true));

					// Get basic search values
					LoadBasicSearchValues();

					// Process filter list
					var filterResult = await ProcessFilterList();
					if (filterResult != null) {

						// Clean output buffer
						if (!Config.Debug)
							Response.Clear();
						return Controller.Json(filterResult);
					}

					// Restore search parms from Session if not searching / reset / export
					if ((IsExport() || Command != "search" && Command != "reset" && Command != "resetall") && Command != "json" && CheckSearchParms())
						RestoreSearchParms();

					// Call Recordset SearchValidated event
					Recordset_SearchValidated();

					// Set up sorting order
					SetupSortOrder();

					// Get basic search criteria
					if (Empty(SearchError))
						srchBasic = BasicSearchWhere();
				}

				// Restore display records
				if (Command != "json" && (RecordsPerPage == -1 || RecordsPerPage > 0)) {
					DisplayRecords = RecordsPerPage; // Restore from Session
				} else {
					DisplayRecords = 20; // Load default
					RecordsPerPage = DisplayRecords; // Save default to session
				}

				// Load Sorting Order
				if (Command != "json")
					LoadSortOrder();

				// Load search default if no existing search criteria
				if (!CheckSearchParms()) {

					// Load basic search from default
					BasicSearch.LoadDefault();
					if (!Empty(BasicSearch.Keyword))
						srchBasic = BasicSearchWhere();
				}

				// Build search criteria
				AddFilter(ref SearchWhere, srchBasic);

				// Call Recordset_Searching event
				Recordset_Searching(ref SearchWhere);

				// Save search criteria
				if (Command == "search" && !RestoreSearch) {
					SessionSearchWhere = SearchWhere; // Save to Session // *** rename as SessionSearchWhere property
					StartRecord = 1; // Reset start record counter
					StartRecordNumber = StartRecord;
				} else if (Command != "json") {
					SearchWhere = SessionSearchWhere;
				}

				// Build filter
				filter = "";
				if (!Security.CanList)
					filter = "(0=1)"; // Filter all records
				AddFilter(ref filter, DbDetailFilter);
				AddFilter(ref filter, SearchWhere);

				// Set up filter
				if (Command == "json") {
					UseSessionForListSql = false; // Do not use session for ListSql
					CurrentFilter = filter;
				} else {
					SessionWhere = filter;
					CurrentFilter = "";
				}
				if (IsGridAdd) {
					CurrentFilter = "0=1";
					StartRecord = 1;
					DisplayRecords = GridAddRowCount;
					TotalRecords = DisplayRecords;
					StopRecord = DisplayRecords;
				} else {
					TotalRecords = await ListRecordCount();
					StopRecord = DisplayRecords;
					StartRecord = 1;
				if (DisplayRecords <= 0 || (IsExport() && ExportAll)) // Display all records
					DisplayRecords = TotalRecords;
				if (!(IsExport() && ExportAll)) // Set up start record position
					SetupStartRec();

				// Recordset
				bool selectLimit = UseSelectLimit;
				if (selectLimit)
					Recordset = await LoadRecordset(StartRecord - 1, DisplayRecords);

				// Set no record found message
				if (Empty(CurrentAction) && TotalRecords == 0) {
					if (!Security.CanList)
						WarningMessage = DeniedMessage();
					if (SearchWhere == "0=101")
						WarningMessage = Language.Phrase("EnterSearchCriteria");
					else
						WarningMessage = Language.Phrase("NoRecord");
				}
				}

				// Search options
				SetupSearchOptions();

				// Normal return
				if (IsApi()) {
					if (!Connection.SelectOffset) // DN
						for (var i = 1; i <= StartRecord - 1; i++) // Move to first record
							await Recordset.ReadAsync();
					using (Recordset) {
						return Controller.Json(new Dictionary<string, object> { {"success", true}, {TableVar, await GetRecordsFromRecordset(Recordset)}, {"totalRecordCount", TotalRecords}, {"version", Config.ProductVersion} });
					}
				}
				return PageResult();
			}

			// Build filter for all keys
			protected string BuildKeyFilter() {
				string wrkFilter = "";

				// Update row index and get row key
				int rowindex = 1;
				CurrentForm.Index = rowindex;
				string thisKey = CurrentForm.GetValue(FormKeyName);
				while (!Empty(thisKey)) {
					if (SetupKeyValues(thisKey)) {
						string filter = GetRecordFilter();
						if (!Empty(wrkFilter))
							wrkFilter += " OR ";
						wrkFilter += filter;
					} else {
						wrkFilter = "0=1";
						break;
					}

					// Update row index and get row key
					rowindex++; // next row
					CurrentForm.Index = rowindex;
					thisKey = CurrentForm.GetValue(FormKeyName);
				}
				return wrkFilter;
			}

			// Set up key values
			protected bool SetupKeyValues(string key) {
				var arKeyFlds = key.Split(Convert.ToChar(Config.CompositeKeySeparator));
				if (arKeyFlds.Length >= 1) {
					s_glchart.Id.FormValue = arKeyFlds[0];
					if (!IsNumeric(s_glchart.Id.FormValue))
						return false;
				}
				return true;
			}

			// Check if empty row
			public bool EmptyRow() => false;

			#pragma warning disable 162, 1998

			// Get list of filters
			public async Task<string> GetFilterList() {
				string filterList = "";

				// Initialize
				var filters = new JObject(); // DN
				filters.Merge(JObject.Parse(Id.AdvancedSearch.ToJson())); // Field Id
				filters.Merge(JObject.Parse(acct_code.AdvancedSearch.ToJson())); // Field acct_code
				filters.Merge(JObject.Parse(description.AdvancedSearch.ToJson())); // Field description
				filters.Merge(JObject.Parse(report_type.AdvancedSearch.ToJson())); // Field report_type
				filters.Merge(JObject.Parse(acct_group.AdvancedSearch.ToJson())); // Field acct_group
				filters.Merge(JObject.Parse(acct_type.AdvancedSearch.ToJson())); // Field acct_type
				filters.Merge(JObject.Parse(CurrencyCode.AdvancedSearch.ToJson())); // Field CurrencyCode
				filters.Merge(JObject.Parse(opn_debit.AdvancedSearch.ToJson())); // Field opn_debit
				filters.Merge(JObject.Parse(opn_credit.AdvancedSearch.ToJson())); // Field opn_credit
				filters.Merge(JObject.Parse(bal_mtd.AdvancedSearch.ToJson())); // Field bal_mtd
				filters.Merge(JObject.Parse(bal_ytd.AdvancedSearch.ToJson())); // Field bal_ytd
				filters.Merge(JObject.Parse(remark.AdvancedSearch.ToJson())); // Field remark
				filters.Merge(JObject.Parse(s_glchart.BasicSearch.ToJson()));

				// Return filter list in JSON
				if (filters.HasValues)
					filterList = "\"data\":" + filters.ToString();
				return (filterList != "") ? "{" + filterList + "}" : "null";
			}

			// Process filter list
			protected async Task<object> ProcessFilterList() {
				if (Post("cmd") == "resetfilter") {
					RestoreFilterList();
				}
				return null;
			}

			#pragma warning restore 162, 1998

			// Restore list of filters
			protected bool RestoreFilterList() {

				// Return if not reset filter
				if (Post("cmd") != "resetfilter")
					return false;
				Dictionary<string, string> filter = JsonConvert.DeserializeObject<Dictionary<string, string>>(Post("filter"));
				Command = "search";
				string sv;

				// Field Id
				if (filter.TryGetValue("x_Id", out sv)) {
					Id.AdvancedSearch.SearchValue = sv;
					Id.AdvancedSearch.SearchOperator = filter["z_Id"];
					Id.AdvancedSearch.SearchCondition = filter["v_Id"];
					Id.AdvancedSearch.SearchValue2 = filter["y_Id"];
					Id.AdvancedSearch.SearchOperator2 = filter["w_Id"];
					Id.AdvancedSearch.Save();
				}

				// Field acct_code
				if (filter.TryGetValue("x_acct_code", out sv)) {
					acct_code.AdvancedSearch.SearchValue = sv;
					acct_code.AdvancedSearch.SearchOperator = filter["z_acct_code"];
					acct_code.AdvancedSearch.SearchCondition = filter["v_acct_code"];
					acct_code.AdvancedSearch.SearchValue2 = filter["y_acct_code"];
					acct_code.AdvancedSearch.SearchOperator2 = filter["w_acct_code"];
					acct_code.AdvancedSearch.Save();
				}

				// Field description
				if (filter.TryGetValue("x_description", out sv)) {
					description.AdvancedSearch.SearchValue = sv;
					description.AdvancedSearch.SearchOperator = filter["z_description"];
					description.AdvancedSearch.SearchCondition = filter["v_description"];
					description.AdvancedSearch.SearchValue2 = filter["y_description"];
					description.AdvancedSearch.SearchOperator2 = filter["w_description"];
					description.AdvancedSearch.Save();
				}

				// Field report_type
				if (filter.TryGetValue("x_report_type", out sv)) {
					report_type.AdvancedSearch.SearchValue = sv;
					report_type.AdvancedSearch.SearchOperator = filter["z_report_type"];
					report_type.AdvancedSearch.SearchCondition = filter["v_report_type"];
					report_type.AdvancedSearch.SearchValue2 = filter["y_report_type"];
					report_type.AdvancedSearch.SearchOperator2 = filter["w_report_type"];
					report_type.AdvancedSearch.Save();
				}

				// Field acct_group
				if (filter.TryGetValue("x_acct_group", out sv)) {
					acct_group.AdvancedSearch.SearchValue = sv;
					acct_group.AdvancedSearch.SearchOperator = filter["z_acct_group"];
					acct_group.AdvancedSearch.SearchCondition = filter["v_acct_group"];
					acct_group.AdvancedSearch.SearchValue2 = filter["y_acct_group"];
					acct_group.AdvancedSearch.SearchOperator2 = filter["w_acct_group"];
					acct_group.AdvancedSearch.Save();
				}

				// Field acct_type
				if (filter.TryGetValue("x_acct_type", out sv)) {
					acct_type.AdvancedSearch.SearchValue = sv;
					acct_type.AdvancedSearch.SearchOperator = filter["z_acct_type"];
					acct_type.AdvancedSearch.SearchCondition = filter["v_acct_type"];
					acct_type.AdvancedSearch.SearchValue2 = filter["y_acct_type"];
					acct_type.AdvancedSearch.SearchOperator2 = filter["w_acct_type"];
					acct_type.AdvancedSearch.Save();
				}

				// Field CurrencyCode
				if (filter.TryGetValue("x_CurrencyCode", out sv)) {
					CurrencyCode.AdvancedSearch.SearchValue = sv;
					CurrencyCode.AdvancedSearch.SearchOperator = filter["z_CurrencyCode"];
					CurrencyCode.AdvancedSearch.SearchCondition = filter["v_CurrencyCode"];
					CurrencyCode.AdvancedSearch.SearchValue2 = filter["y_CurrencyCode"];
					CurrencyCode.AdvancedSearch.SearchOperator2 = filter["w_CurrencyCode"];
					CurrencyCode.AdvancedSearch.Save();
				}

				// Field opn_debit
				if (filter.TryGetValue("x_opn_debit", out sv)) {
					opn_debit.AdvancedSearch.SearchValue = sv;
					opn_debit.AdvancedSearch.SearchOperator = filter["z_opn_debit"];
					opn_debit.AdvancedSearch.SearchCondition = filter["v_opn_debit"];
					opn_debit.AdvancedSearch.SearchValue2 = filter["y_opn_debit"];
					opn_debit.AdvancedSearch.SearchOperator2 = filter["w_opn_debit"];
					opn_debit.AdvancedSearch.Save();
				}

				// Field opn_credit
				if (filter.TryGetValue("x_opn_credit", out sv)) {
					opn_credit.AdvancedSearch.SearchValue = sv;
					opn_credit.AdvancedSearch.SearchOperator = filter["z_opn_credit"];
					opn_credit.AdvancedSearch.SearchCondition = filter["v_opn_credit"];
					opn_credit.AdvancedSearch.SearchValue2 = filter["y_opn_credit"];
					opn_credit.AdvancedSearch.SearchOperator2 = filter["w_opn_credit"];
					opn_credit.AdvancedSearch.Save();
				}

				// Field bal_mtd
				if (filter.TryGetValue("x_bal_mtd", out sv)) {
					bal_mtd.AdvancedSearch.SearchValue = sv;
					bal_mtd.AdvancedSearch.SearchOperator = filter["z_bal_mtd"];
					bal_mtd.AdvancedSearch.SearchCondition = filter["v_bal_mtd"];
					bal_mtd.AdvancedSearch.SearchValue2 = filter["y_bal_mtd"];
					bal_mtd.AdvancedSearch.SearchOperator2 = filter["w_bal_mtd"];
					bal_mtd.AdvancedSearch.Save();
				}

				// Field bal_ytd
				if (filter.TryGetValue("x_bal_ytd", out sv)) {
					bal_ytd.AdvancedSearch.SearchValue = sv;
					bal_ytd.AdvancedSearch.SearchOperator = filter["z_bal_ytd"];
					bal_ytd.AdvancedSearch.SearchCondition = filter["v_bal_ytd"];
					bal_ytd.AdvancedSearch.SearchValue2 = filter["y_bal_ytd"];
					bal_ytd.AdvancedSearch.SearchOperator2 = filter["w_bal_ytd"];
					bal_ytd.AdvancedSearch.Save();
				}

				// Field remark
				if (filter.TryGetValue("x_remark", out sv)) {
					remark.AdvancedSearch.SearchValue = sv;
					remark.AdvancedSearch.SearchOperator = filter["z_remark"];
					remark.AdvancedSearch.SearchCondition = filter["v_remark"];
					remark.AdvancedSearch.SearchValue2 = filter["y_remark"];
					remark.AdvancedSearch.SearchOperator2 = filter["w_remark"];
					remark.AdvancedSearch.Save();
				}
				if (filter.TryGetValue(Config.TableBasicSearch, out string keyword))
					BasicSearch.SessionKeyword = keyword;
				if (filter.TryGetValue(Config.TableBasicSearchType, out string type))
					BasicSearch.SessionType = type;
				return true;
			}

			// Return basic search SQL
			protected string BasicSearchSql(List<string> keywords, string type) {
				string where = "";
				BuildBasicSearchSql(ref where, acct_code, keywords, type);
				BuildBasicSearchSql(ref where, description, keywords, type);
				BuildBasicSearchSql(ref where, report_type, keywords, type);
				BuildBasicSearchSql(ref where, acct_group, keywords, type);
				BuildBasicSearchSql(ref where, acct_type, keywords, type);
				BuildBasicSearchSql(ref where, CurrencyCode, keywords, type);
				BuildBasicSearchSql(ref where, remark, keywords, type);
				return where;
			}

			// Build basic search SQL
			protected void BuildBasicSearchSql(ref string where, DbField fld, List<string> keywords, string type) {
				string defCond = (type == "OR") ? "OR" : "AND";
				var sqls = new List<string>(); // List for SQL parts
				var conds = new List<string>(); // List for search conditions
				int cnt = keywords.Count;
				int j = 0; // Number of SQL parts
				for (int i = 0; i < cnt; i++) {
					string keyword = keywords[i];
					keyword = keyword.Trim();
					string[] ar;
					if (!Empty(Config.BasicSearchIgnorePattern)) {
						keyword = Regex.Replace(keyword, Config.BasicSearchIgnorePattern, "\\");
						ar = keyword.Split('\\');
					} else {
						ar = new string[] { keyword };
					}
					foreach (var kw in ar) {
						if (!Empty(kw)) {
							string wrk = "";
							if (kw == "OR" && type == "") {
								if (j > 0)
									conds[j - 1] = "OR";
							} else if (kw == Config.NullValue) {
								wrk = fld.Expression + " IS NULL";
							} else if (kw == Config.NotNullValue) {
								wrk = fld.Expression + " IS NOT NULL";
							} else if (fld.IsVirtual) {
								wrk = fld.VirtualExpression + Like(QuotedValue("%" + kw + "%", Config.DataTypeString, DbId), DbId);
							} else if (fld.DataType != Config.DataTypeNumber || IsNumeric(kw)) {
								wrk = fld.BasicSearchExpression + Like(QuotedValue("%" + kw + "%", Config.DataTypeString, DbId), DbId);
							}
							if (!Empty(wrk)) {
								sqls.Add(wrk); // DN
								conds.Add(defCond); // DN
								j++;
							}
						}
					}
				}
				cnt = sqls.Count;
				bool quoted = false;
				string sql = "";
				if (cnt > 0) {
					for (int i = 0; i < cnt - 1; i++) {
						if (conds[i] == "OR") {
							if (!quoted)
								sql += "(";
							quoted = true;
						}
						sql += sqls[i];
						if (quoted && conds[i] != "OR") {
							sql += ")";
							quoted = false;
						}
						sql += " " + conds[i] + " ";
					}
					sql += sqls[cnt - 1];
					if (quoted)
						sql += ")";
				}
				if (!Empty(sql)) {
					if (!Empty(where))
						where += " OR ";
					where += "(" + sql + ")";
				}
			}

			// Return basic search WHERE clause based on search keyword and type
			protected string BasicSearchWhere(bool def = false) {
				string searchStr = "";
				if (!Security.CanSearch)
					return "";
				string searchKeyword = def ? BasicSearch.KeywordDefault : BasicSearch.Keyword;
				string searchType = def ? BasicSearch.TypeDefault : BasicSearch.Type;

				// Get search SQL
				if (!Empty(searchKeyword)) {
					var ar = BasicSearch.KeywordList(def);
					if ((searchType == "OR" || searchType == "AND") && ConvertToBool(BasicSearch.BasicSearchAnyFields)) {
						foreach (var keyword in ar) {
							if (keyword != "") {
								if (searchStr != "")
									searchStr += " " + searchType + " ";
								searchStr += "(" + BasicSearchSql(new List<string> { keyword }, searchType) + ")";
							}
						}
					} else {
						searchStr = BasicSearchSql(ar, searchType);
					}
					if (!def && (new List<string> {"", "reset", "resetall"}).Contains(Command))
						Command = "search";
				}
				if (!def && Command == "search") {
					BasicSearch.SessionKeyword = searchKeyword;
					BasicSearch.SessionType = searchType;
				}
				return searchStr;
			}

			// Check if search parm exists
			protected bool CheckSearchParms() {

				// Check basic search
				if (BasicSearch.IssetSession)
					return true;
				return false;
			}

			// Clear all search parameters
			protected void ResetSearchParms() {
				SearchWhere = "";
				SessionSearchWhere = SearchWhere;

				// Clear basic search parameters
				ResetBasicSearchParms();
			}

			// Load advanced search default values
			protected bool LoadAdvancedSearchDefault() {
				return false;
			}

			// Clear all basic search parameters
			protected void ResetBasicSearchParms() {
				BasicSearch.UnsetSession();
			}

			// Restore all search parameters
			protected void RestoreSearchParms() {
				RestoreSearch = true;

				// Restore basic search values
				BasicSearch.Load();
			}

			// Set up sort parameters
			protected void SetupSortOrder() {

				// Check for "order" parameter
				if (!Empty(Get("order"))) {
					CurrentOrder = Get("order");
					CurrentOrderType = Get("ordertype");
					UpdateSort(Id); // Id
					UpdateSort(acct_code); // acct_code
					UpdateSort(description); // description
					UpdateSort(report_type); // report_type
					UpdateSort(acct_group); // acct_group
					UpdateSort(acct_type); // acct_type
					UpdateSort(CurrencyCode); // CurrencyCode
					UpdateSort(opn_debit); // opn_debit
					UpdateSort(opn_credit); // opn_credit
					UpdateSort(bal_mtd); // bal_mtd
					UpdateSort(bal_ytd); // bal_ytd
					UpdateSort(remark); // remark
					StartRecordNumber = 1; // Reset start position
				}
			}

			// Load sort order parameters
			protected void LoadSortOrder() {
				string orderBy = SessionOrderBy; // Get Order By from Session
				if (Empty(orderBy)) {
					if (!Empty(SqlOrderBy)) {
						orderBy = SqlOrderBy;
						SessionOrderBy = orderBy;
					}
				}
			}

			// Reset command
			// cmd=reset (Reset search parameters)
			// cmd=resetall (Reset search and master/detail parameters)
			// cmd=resetsort (Reset sort parameters)

			protected void ResetCommand() {

				// Get reset cmd
				if (Command.ToLower().StartsWith("reset")) {

					// Reset search criteria
					if (SameText(Command, "reset") || SameText(Command, "resetall"))
						ResetSearchParms();

					// Reset sorting order
					if (SameText(Command, "resetsort")) {
						string orderBy = "";
						SessionOrderBy = orderBy;
						Id.Sort = "";
						acct_code.Sort = "";
						description.Sort = "";
						report_type.Sort = "";
						acct_group.Sort = "";
						acct_type.Sort = "";
						CurrencyCode.Sort = "";
						opn_debit.Sort = "";
						opn_credit.Sort = "";
						bal_mtd.Sort = "";
						bal_ytd.Sort = "";
						remark.Sort = "";
					}

					// Reset start position
					StartRecord = 1;
					StartRecordNumber = StartRecord;
				}
			}

			#pragma warning disable 1998

			// Set up list options
			protected async Task SetupListOptions() {
				ListOption item;

				// Add group option item
				item = ListOptions.Add(ListOptions.GroupOptionName);
				item.Body = "";
				item.OnLeft = false;
				item.Visible = false;

				// "view"
				item = ListOptions.Add("view");
				item.CssClass = "text-nowrap";
				item.Visible = Security.CanView;
				item.OnLeft = false;

				// "edit"
				item = ListOptions.Add("edit");
				item.CssClass = "text-nowrap";
				item.Visible = Security.CanEdit;
				item.OnLeft = false;

				// "copy"
				item = ListOptions.Add("copy");
				item.CssClass = "text-nowrap";
				item.Visible = Security.CanAdd;
				item.OnLeft = false;

				// "delete"
				item = ListOptions.Add("delete");
				item.CssClass = "text-nowrap";
				item.Visible = Security.CanDelete;
				item.OnLeft = false;

				// List actions
				item = ListOptions.Add("listactions");
				item.CssClass = "text-nowrap";
				item.OnLeft = false;
				item.Visible = false;
				item.ShowInButtonGroup = false;
				item.ShowInDropDown = false;

				// "checkbox"
				item = ListOptions.Add("checkbox");
				item.CssStyle = "white-space: nowrap; text-align: center; vertical-align: middle; margin: 0px;";
				item.Visible = false;
				item.OnLeft = false;
				item.Header = "<input type=\"checkbox\" name=\"key\" id=\"key\" onclick=\"ew.selectAllKey(this);\">";
				item.ShowInDropDown = false;
				item.ShowInButtonGroup = false;

				// Drop down button for ListOptions
				ListOptions.UseDropDownButton = false;
				ListOptions.DropDownButtonPhrase = Language.Phrase("ButtonListOptions");
				ListOptions.UseButtonGroup = false;
				if (ListOptions.UseButtonGroup && IsMobile())
					ListOptions.UseDropDownButton = true;
				ListOptions.ButtonClass = ""; // Class for button group

				// Call ListOptions_Load event
				ListOptions_Load();
				SetupListOptionsExt();
				item = ListOptions[ListOptions.GroupOptionName];
				item.Visible = ListOptions.GroupOptionVisible;
			}

			#pragma warning restore 1998

			// Render list options

			#pragma warning disable 168, 219, 1998

			public async Task RenderListOptions() {
				ListOption listOption;
				var isVisible = false; // DN
				ListOptions.LoadDefault();

				// Call ListOptions_Rendering event
				ListOptions_Rendering();

				// "view"
				listOption = ListOptions["view"];
				string viewcaption = HtmlTitle(Language.Phrase("ViewLink"));
				isVisible = Security.CanView;
				if (isVisible) {
					listOption.Body = "<a class=\"ew-row-link ew-view\" title=\"" + viewcaption + "\" data-caption=\"" + viewcaption + "\" href=\"" + HtmlEncode(AppPath(ViewUrl)) + "\">" + Language.Phrase("ViewLink") + "</a>";
				} else {
					listOption.Body = "";
				}

				// "edit"
				listOption = ListOptions["edit"];
				string editcaption = HtmlTitle(Language.Phrase("EditLink"));
				isVisible = Security.CanEdit;
				if (isVisible) {
					listOption.Body = "<a class=\"ew-row-link ew-edit\" title=\"" + editcaption + "\" data-caption=\"" + editcaption + "\" href=\"" + HtmlEncode(AppPath(EditUrl)) + "\">" + Language.Phrase("EditLink") + "</a>";
				} else {
					listOption.Body = "";
				}

				// "copy"
				listOption = ListOptions["copy"];
				string copycaption = HtmlTitle(Language.Phrase("CopyLink"));
				isVisible = Security.CanAdd;
				if (isVisible) {
					listOption.Body = "<a class=\"ew-row-link ew-copy\" title=\"" + copycaption + "\" data-caption=\"" + copycaption + "\" href=\"" + HtmlEncode(AppPath(CopyUrl)) + "\">" + Language.Phrase("CopyLink") + "</a>";
				} else {
					listOption.Body = "";
				}

				// "delete"
				listOption = ListOptions["delete"];
				isVisible = Security.CanDelete;
				if (isVisible)
					listOption.Body = "<a class=\"ew-row-link ew-delete\"" + "" + " title=\"" + HtmlTitle(Language.Phrase("DeleteLink")) + "\" data-caption=\"" + HtmlTitle(Language.Phrase("DeleteLink")) + "\" href=\"" + HtmlEncode(AppPath(DeleteUrl)) + "\">" + Language.Phrase("DeleteLink") + "</a>";
				else
					listOption.Body = "";

				// Set up list action buttons
				listOption = ListOptions["listactions"];
				if (listOption != null && !IsExport() && CurrentAction == "") {
					string body = "";
					var links = new List<string>();
					foreach (var (key, act) in ListActions.Items) {
						if (act.Select == Config.ActionSingle && act.Allowed) {
							var action = act.Action;
							string caption = act.Caption;
							var icon = (act.Icon != "") ? "<i class=\"" + HtmlEncode(act.Icon.Replace(" ew-icon", "")) + "\" data-caption=\"" + HtmlTitle(caption) + "\"></i> " : "";
							links.Add("<li><a class=\"dropdown-item ew-action ew-list-action\" data-action=\"" + HtmlEncode(action) + "\" data-caption=\"" + HtmlTitle(caption) + "\" href=\"\" onclick=\"ew.submitAction(event,jQuery.extend({key:" + KeyToJson() + "}," + act.ToJson(true) + ")); return false;\">" + icon + act.Caption + "</a></li>");
							if (links.Count == 1) // Single button
								body = "<a class=\"ew-action ew-list-action\" data-action=\"" + HtmlEncode(action) + "\" title=\"" + HtmlTitle(caption) + "\" data-caption=\"" + HtmlTitle(caption) + "\" href=\"\" onclick=\"ew.submitAction(event,jQuery.extend({key:" + KeyToJson() + "}," + act.ToJson(true) + ")); return false;\">" + Language.Phrase("ListActionButton") + "</a>";
						}
					}
					if (links.Count > 1) { // More than one buttons, use dropdown
						body = "<button class=\"dropdown-toggle btn btn-default ew-actions\" title=\"" + HtmlTitle(Language.Phrase("ListActionButton")) + "\" data-toggle=\"dropdown\">" + Language.Phrase("ListActionButton") + "</button>";
						string content = links.Aggregate("", (result, link) => result + "<li>" + link + "</li>");
						body += "<ul class=\"dropdown-menu" + (listOption.OnLeft ? "" : " dropdown-menu-right") + "\">" + content + "</ul>";
						body = "<div class=\"btn-group btn-group-sm\">" + body + "</div>";
					}
					if (links.Count > 0) {
						listOption.Body = body;
						listOption.Visible = true;
					}
				}

				// "checkbox2"
				listOption = ListOptions["checkbox"];
				listOption.Body = "<input type=\"checkbox\" name=\"key_m\" class=\"ew-multi-select\" value=\"" + HtmlEncode(Id.CurrentValue) + "\" onclick=\"ew.clickMultiCheckbox(event);\">";
				RenderListOptionsExt();

				// Call ListOptions_Rendered event
				ListOptions_Rendered();
			}

			// Set up other options
			protected void SetupOtherOptions() {
				ListOptions option;
				ListOption item;
				var options = OtherOptions;
				option = options["addedit"];

				// Add
				item = option.Add("add");
				string addcaption = HtmlTitle(Language.Phrase("AddLink"));
				item.Body = "<a class=\"ew-add-edit ew-add\" title=\"" + addcaption + "\" data-caption=\"" + addcaption + "\" href=\"" + HtmlEncode(AppPath(AddUrl)) + "\">" + Language.Phrase("AddLink") + "</a>";
				item.Visible = (AddUrl != "" && Security.CanAdd);
				option = options["action"];

				// Set up options default
				foreach (var (key, opt) in options) {
					opt.UseDropDownButton = false;
					opt.UseButtonGroup = true;
					opt.ButtonClass = ""; // Class for button group
					item = opt.Add(opt.GroupOptionName);
					item.Body = "";
					item.Visible = false;
				}
				options["addedit"].DropDownButtonPhrase = Language.Phrase("ButtonAddEdit");
				options["detail"].DropDownButtonPhrase = Language.Phrase("ButtonDetails");
				options["action"].DropDownButtonPhrase = Language.Phrase("ButtonActions");

				// Filter button
				item = FilterOptions.Add("savecurrentfilter");
				item.Body = "<a class=\"ew-save-filter\" data-form=\"fs_glchartlistsrch\" href=\"#\">" + Language.Phrase("SaveCurrentFilter") + "</a>";
				item.Visible = true;
				item = FilterOptions.Add("deletefilter");
				item.Body = "<a class=\"ew-delete-filter\" data-form=\"fs_glchartlistsrch\" href=\"#\">" + Language.Phrase("DeleteFilter") + "</a>";
				item.Visible = true;
				FilterOptions.UseDropDownButton = true;
				FilterOptions.UseButtonGroup = !FilterOptions.UseDropDownButton;
				FilterOptions.DropDownButtonPhrase = Language.Phrase("Filters");

				// Add group option item
				item = FilterOptions.Add(FilterOptions.GroupOptionName);
				item.Body = "";
				item.Visible = false;
			}

			// Render other options
			public void RenderOtherOptions() {
				ListOptions option;
				ListOption item;
				var options = OtherOptions;
					option = options["action"];

					// Set up list action buttons
					foreach (var (key, act) in ListActions.Items.Where(kvp => kvp.Value.Select == Config.ActionMultiple)) {
						item = option.Add("custom_" + act.Action);
						string caption = act.Caption;
						var icon = (act.Icon != "") ? "<i class=\"" + HtmlEncode(act.Icon) + "\" data-caption=\"" + HtmlEncode(caption) + "\"></i> " + caption : caption;
						item.Body = "<a class=\"ew-action ew-list-action\" title=\"" + HtmlEncode(caption) + "\" data-caption=\"" + HtmlEncode(caption) + "\" href=\"\" onclick=\"ew.submitAction(event,jQuery.extend({f:document.fs_glchartlist}," + act.ToJson(true) + ")); return false;\">" + icon + "</a>";
						item.Visible = act.Allowed;
					}

					// Hide grid edit and other options
					if (TotalRecords <= 0) {
						option = options["addedit"];
						item = option["gridedit"];
						if (item != null)
							item.Visible = false;
						option = options["action"];
						option.HideAllOptions();
					}
			}

			// Process list action
			public async Task<IActionResult> ProcessListAction() {
				string filter = GetFilterFromRecordKeys();
				string userAction = Post("useraction");
				if (filter != "" && userAction != "") {

					// Check permission first
					var actionCaption = userAction;
					foreach (var (key, act) in ListActions.Items) {
						if (SameString(key, userAction)) {
							actionCaption = act.Caption;
							if (!act.Allowed) {
								string errmsg = Language.Phrase("CustomActionNotAllowed").Replace("%s", actionCaption);
								if (Post("ajax") == userAction) // Ajax
									return Controller.Content("<p class=\"text-danger\">" + errmsg + "</p>", "text/plain", Encoding.UTF8);
								else
									FailureMessage = errmsg;
								return null;
							}
						}
					}
					CurrentFilter = filter;
					string sql = CurrentSql;
					var rsuser = await Connection.GetRowsAsync(sql);
					CurrentAction = userAction;

					// Call row custom action event
					if (rsuser != null) {
						Connection.BeginTrans();
						bool processed = true;
						SelectedCount = rsuser.Count();
						SelectedIndex = 0;
						foreach (var row in rsuser) {
							SelectedIndex++;
							processed = Row_CustomAction(userAction, row);
							if (!processed)
								break;
						}
						if (processed) {
							Connection.CommitTrans(); // Commit the changes
							if (Empty(SuccessMessage))
								SuccessMessage = Language.Phrase("CustomActionCompleted").Replace("%s", actionCaption); // Set up success message
							} else {
								Connection.RollbackTrans(); // Rollback changes

							// Set up error message
							if (!Empty(SuccessMessage) || !Empty(FailureMessage)) {

								// Use the message, do nothing
							} else if (!Empty(CancelMessage)) {
								FailureMessage = CancelMessage;
								CancelMessage = "";
							} else {
								FailureMessage = Language.Phrase("CustomActionFailed").Replace("%s", actionCaption);
							}
						}
					}
					CurrentAction = ""; // Clear action
					if (Post("ajax") == userAction) { // Ajax
						if (ActionResult != null) // Action result set by Row_CustomAction // DN
							return ActionResult;
						string msg = "";
						if (SuccessMessage != "") {
							msg = "<p class=\"text-success\">" + SuccessMessage + "</p>";
							ClearSuccessMessage(); // Clear message
						}
						if (FailureMessage != "") {
							msg = "<p class=\"text-danger\">" + FailureMessage + "</p>";
							ClearFailureMessage(); // Clear message
						}
						if (!Empty(msg))
							return Controller.Content(msg, "text/plain", Encoding.UTF8);
					}
				}
				return null; // Not ajax request
			}

			// Set up search options
			protected void SetupSearchOptions() {
				ListOption item;
				SearchOptions = new ListOptions();
				SearchOptions.Tag = "div";
				SearchOptions.TagClassName = "ew-search-option";

				// Search button
				item = SearchOptions.Add("searchtoggle");
				var searchToggleClass = !Empty(SearchWhere) ? " active" : " active";
				item.Body = "<button type=\"button\" class=\"btn btn-default ew-search-toggle" + searchToggleClass + "\" title=\"" + Language.Phrase("SearchPanel") + "\" data-caption=\"" + Language.Phrase("SearchPanel") + "\" data-toggle=\"button\" data-form=\"fs_glchartlistsrch\">" + Language.Phrase("SearchLink") + "</button>";
				item.Visible = true;

				// Show all button
				item = SearchOptions.Add("showall");
				item.Body = "<a class=\"btn btn-default ew-show-all\" title=\"" + Language.Phrase("ShowAll") + "\" data-caption=\"" + Language.Phrase("ShowAll") + "\" href=\"" + AppPath(PageUrl) + "cmd=reset\">" + Language.Phrase("ShowAllBtn") + "</a>";
				item.Visible = (SearchWhere != DefaultSearchWhere && SearchWhere != "0=101");

				// Button group for search
				SearchOptions.UseDropDownButton = false;
				SearchOptions.UseButtonGroup = true;
				SearchOptions.DropDownButtonPhrase = Language.Phrase("ButtonSearch");

				// Add group option item
				item = SearchOptions.Add(SearchOptions.GroupOptionName);
				item.Body = "";
				item.Visible = false;

				// Hide search options
				if (IsExport() || !Empty(CurrentAction))
					SearchOptions.HideAllOptions();
				if (!Security.CanSearch) {
					SearchOptions.HideAllOptions();
					FilterOptions.HideAllOptions();
				}
			}

			// Set up list options
			protected void SetupListOptionsExt() {
			}

			// Render list options
			protected void RenderListOptionsExt() {
			}

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

			// Load basic search values // DN
			protected void LoadBasicSearchValues() {
				if (Query.TryGetValue(Config.TableBasicSearch, out StringValues keyword))
					BasicSearch.Keyword = keyword;
				if (!Empty(BasicSearch.Keyword) && Empty(Command))
					Command = "search";
				if (Query.TryGetValue(Config.TableBasicSearchType, out StringValues type))
					BasicSearch.Type = type;
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
				var row = new Dictionary<string, object>();
				row.Add("Id", System.DBNull.Value);
				row.Add("acct_code", System.DBNull.Value);
				row.Add("description", System.DBNull.Value);
				row.Add("report_type", System.DBNull.Value);
				row.Add("acct_group", System.DBNull.Value);
				row.Add("acct_type", System.DBNull.Value);
				row.Add("CurrencyCode", System.DBNull.Value);
				row.Add("opn_debit", System.DBNull.Value);
				row.Add("opn_credit", System.DBNull.Value);
				row.Add("bal_mtd", System.DBNull.Value);
				row.Add("bal_ytd", System.DBNull.Value);
				row.Add("remark", System.DBNull.Value);
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

					// Id
					Id.HrefValue = "";
					Id.TooltipValue = "";

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
				url = Regex.Replace(url, @"\?cmd=reset(all)?$", ""); // Remove cmd=reset / cmd=resetall
				breadcrumb.Add("list", TableVar, url, "", TableVar, true);
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

			// ListOptions Load event
			public virtual void ListOptions_Load() {

				// Example:
				//var opt = ListOptions.Add("new");
				//opt.Header = "xxx";
				//opt.OnLeft = true; // Link on left
				//opt.MoveTo(0); // Move to first column

			}

			// ListOptions Rendering event
			public virtual void ListOptions_Rendering() {

				//xxxGrid.DetailAdd = (...condition...); // Set to true or false conditionally
				//xxxGrid.DetailEdit = (...condition...); // Set to true or false conditionally
				//xxxGrid.DetailView = (...condition...); // Set to true or false conditionally

			}

			// ListOptions Rendered event
			public virtual void ListOptions_Rendered() {

				//Example:
				//ListOptions["new"].Body = "xxx";

			}

			// Row Custom Action event
			public virtual bool Row_CustomAction(string action, Dictionary<string, object> row) {

				// Return false to abort
				return true;
			}

			// Grid Inserting event
			public virtual bool Grid_Inserting() {

				// Enter your code here
				// To reject grid insert, set return value to FALSE

				return true;
			}

			// Grid Inserted event
			public virtual void Grid_Inserted(List<Dictionary<string, object>> rsnew) {

				//Log("Grid Inserted");
			}

			// Grid Updating event
			public virtual bool Grid_Updating(List<Dictionary<string, object>> rsold) {

				// Enter your code here
				// To reject grid update, set return value to FALSE

				return true;
			}

			// Grid Updated event
			public virtual void Grid_Updated(List<Dictionary<string, object>> rsold, List<Dictionary<string, object>> rsnew) {

				//Log("Grid Updated");
			}
		}
	}
}
