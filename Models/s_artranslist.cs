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
		/// s_artrans_List
		/// </summary>

		public static _s_artrans_List s_artrans_List {
			get => HttpData.Get<_s_artrans_List>("s_artrans_List");
			set => HttpData["s_artrans_List"] = value;
		}

		/// <summary>
		/// Page class for s_artrans
		/// </summary>

		public class _s_artrans_List : _s_artrans_ListBase
		{

			// Construtor
			public _s_artrans_List(Controller controller = null) : base(controller) {
			}
		}

		/// <summary>
		/// Page base class
		/// </summary>

		public class _s_artrans_ListBase : _s_artrans, IAspNetMakerPage
		{

			// Page ID
			public string PageID = "list";

			// Project ID
			public string ProjectID = "{8543F230-11C6-4105-B51C-8D87C21BE659}";

			// Table name
			public string TableName = "s_artrans";

			// Page object name
			public string PageObjName = "s_artrans_List";

			// Grid form hidden field names
			public string FormName = "fs_artranslist";
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
			public _s_artrans_ListBase(Controller controller = null) { // DN
				if (controller != null)
					Controller = controller;

				// Initialize
				CurrentPage = this;

				// Language object
				Language = Language ?? new Lang();

				// Table object (s_artrans)
				if (s_artrans == null || s_artrans is _s_artrans)
					s_artrans = this;

				// Initialize URLs
				ExportPrintUrl = PageUrl + "export=print";
				ExportExcelUrl = PageUrl + "export=excel";
				ExportWordUrl = PageUrl + "export=word";
				ExportHtmlUrl = PageUrl + "export=html";
				ExportXmlUrl = PageUrl + "export=xml";
				ExportCsvUrl = PageUrl + "export=csv";
				ExportPdfUrl = PageUrl + "export=pdf";
				AddUrl = "s_artransadd";
				InlineAddUrl = PageUrl + "action=add";
				GridAddUrl = PageUrl + "action=gridadd";
				GridEditUrl = PageUrl + "action=gridedit";
				MultiDeleteUrl = "s_artransdelete";
				MultiUpdateUrl = "s_artransupdate";

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
				FilterOptions = new ListOptions { Tag = "div", TagClassName = "ew-filter-option fs_artranslistsrch" };

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
					dynamic doc = CreateInstance(classname, new object[] { s_artrans, "" }); // DN
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
				return key;
			}

			// Hide fields for Add/Edit
			protected void HideFieldsForAddEdit() {
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
				dt_rec.SetVisibility();
				doc_date.SetVisibility();
				dbcode.SetVisibility();
				ar_dept.SetVisibility();
				ref_no.SetVisibility();
				description.SetVisibility();
				amt_ar.SetVisibility();
				amt_rec.SetVisibility();
				ar_gl_acct.SetVisibility();
				ar_gl_dept.SetVisibility();
				db_cr.SetVisibility();
				month.SetVisibility();
				source.SetVisibility();
				forx_amt.SetVisibility();
				forx_rec.SetVisibility();
				forx_cost.SetVisibility();
				gain_loss.SetVisibility();
				ex_gl_ac.SetVisibility();
				ex_gl_dp.SetVisibility();
				cost.SetVisibility();
				prefix.SetVisibility();
				exrate.SetVisibility();
				trn_no.SetVisibility();
				rpl_trn_no.SetVisibility();
				slsman.SetVisibility();
				markdelete.SetVisibility();
				dt_upd.SetVisibility();
				id_upd.SetVisibility();
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
				if (arKeyFlds.Length >= 0) {
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
				filters.Merge(JObject.Parse(dt_rec.AdvancedSearch.ToJson())); // Field dt_rec
				filters.Merge(JObject.Parse(doc_date.AdvancedSearch.ToJson())); // Field doc_date
				filters.Merge(JObject.Parse(dbcode.AdvancedSearch.ToJson())); // Field dbcode
				filters.Merge(JObject.Parse(ar_dept.AdvancedSearch.ToJson())); // Field ar_dept
				filters.Merge(JObject.Parse(ref_no.AdvancedSearch.ToJson())); // Field ref_no
				filters.Merge(JObject.Parse(description.AdvancedSearch.ToJson())); // Field description
				filters.Merge(JObject.Parse(amt_ar.AdvancedSearch.ToJson())); // Field amt_ar
				filters.Merge(JObject.Parse(amt_rec.AdvancedSearch.ToJson())); // Field amt_rec
				filters.Merge(JObject.Parse(ar_gl_acct.AdvancedSearch.ToJson())); // Field ar_gl_acct
				filters.Merge(JObject.Parse(ar_gl_dept.AdvancedSearch.ToJson())); // Field ar_gl_dept
				filters.Merge(JObject.Parse(db_cr.AdvancedSearch.ToJson())); // Field db_cr
				filters.Merge(JObject.Parse(month.AdvancedSearch.ToJson())); // Field month
				filters.Merge(JObject.Parse(source.AdvancedSearch.ToJson())); // Field source
				filters.Merge(JObject.Parse(forx_amt.AdvancedSearch.ToJson())); // Field forx_amt
				filters.Merge(JObject.Parse(forx_rec.AdvancedSearch.ToJson())); // Field forx_rec
				filters.Merge(JObject.Parse(forx_cost.AdvancedSearch.ToJson())); // Field forx_cost
				filters.Merge(JObject.Parse(gain_loss.AdvancedSearch.ToJson())); // Field gain_loss
				filters.Merge(JObject.Parse(ex_gl_ac.AdvancedSearch.ToJson())); // Field ex_gl_ac
				filters.Merge(JObject.Parse(ex_gl_dp.AdvancedSearch.ToJson())); // Field ex_gl_dp
				filters.Merge(JObject.Parse(cost.AdvancedSearch.ToJson())); // Field cost
				filters.Merge(JObject.Parse(prefix.AdvancedSearch.ToJson())); // Field prefix
				filters.Merge(JObject.Parse(exrate.AdvancedSearch.ToJson())); // Field exrate
				filters.Merge(JObject.Parse(trn_no.AdvancedSearch.ToJson())); // Field trn_no
				filters.Merge(JObject.Parse(rpl_trn_no.AdvancedSearch.ToJson())); // Field rpl_trn_no
				filters.Merge(JObject.Parse(slsman.AdvancedSearch.ToJson())); // Field slsman
				filters.Merge(JObject.Parse(markdelete.AdvancedSearch.ToJson())); // Field markdelete
				filters.Merge(JObject.Parse(dt_upd.AdvancedSearch.ToJson())); // Field dt_upd
				filters.Merge(JObject.Parse(id_upd.AdvancedSearch.ToJson())); // Field id_upd
				filters.Merge(JObject.Parse(s_artrans.BasicSearch.ToJson()));

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

				// Field dt_rec
				if (filter.TryGetValue("x_dt_rec", out sv)) {
					dt_rec.AdvancedSearch.SearchValue = sv;
					dt_rec.AdvancedSearch.SearchOperator = filter["z_dt_rec"];
					dt_rec.AdvancedSearch.SearchCondition = filter["v_dt_rec"];
					dt_rec.AdvancedSearch.SearchValue2 = filter["y_dt_rec"];
					dt_rec.AdvancedSearch.SearchOperator2 = filter["w_dt_rec"];
					dt_rec.AdvancedSearch.Save();
				}

				// Field doc_date
				if (filter.TryGetValue("x_doc_date", out sv)) {
					doc_date.AdvancedSearch.SearchValue = sv;
					doc_date.AdvancedSearch.SearchOperator = filter["z_doc_date"];
					doc_date.AdvancedSearch.SearchCondition = filter["v_doc_date"];
					doc_date.AdvancedSearch.SearchValue2 = filter["y_doc_date"];
					doc_date.AdvancedSearch.SearchOperator2 = filter["w_doc_date"];
					doc_date.AdvancedSearch.Save();
				}

				// Field dbcode
				if (filter.TryGetValue("x_dbcode", out sv)) {
					dbcode.AdvancedSearch.SearchValue = sv;
					dbcode.AdvancedSearch.SearchOperator = filter["z_dbcode"];
					dbcode.AdvancedSearch.SearchCondition = filter["v_dbcode"];
					dbcode.AdvancedSearch.SearchValue2 = filter["y_dbcode"];
					dbcode.AdvancedSearch.SearchOperator2 = filter["w_dbcode"];
					dbcode.AdvancedSearch.Save();
				}

				// Field ar_dept
				if (filter.TryGetValue("x_ar_dept", out sv)) {
					ar_dept.AdvancedSearch.SearchValue = sv;
					ar_dept.AdvancedSearch.SearchOperator = filter["z_ar_dept"];
					ar_dept.AdvancedSearch.SearchCondition = filter["v_ar_dept"];
					ar_dept.AdvancedSearch.SearchValue2 = filter["y_ar_dept"];
					ar_dept.AdvancedSearch.SearchOperator2 = filter["w_ar_dept"];
					ar_dept.AdvancedSearch.Save();
				}

				// Field ref_no
				if (filter.TryGetValue("x_ref_no", out sv)) {
					ref_no.AdvancedSearch.SearchValue = sv;
					ref_no.AdvancedSearch.SearchOperator = filter["z_ref_no"];
					ref_no.AdvancedSearch.SearchCondition = filter["v_ref_no"];
					ref_no.AdvancedSearch.SearchValue2 = filter["y_ref_no"];
					ref_no.AdvancedSearch.SearchOperator2 = filter["w_ref_no"];
					ref_no.AdvancedSearch.Save();
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

				// Field amt_ar
				if (filter.TryGetValue("x_amt_ar", out sv)) {
					amt_ar.AdvancedSearch.SearchValue = sv;
					amt_ar.AdvancedSearch.SearchOperator = filter["z_amt_ar"];
					amt_ar.AdvancedSearch.SearchCondition = filter["v_amt_ar"];
					amt_ar.AdvancedSearch.SearchValue2 = filter["y_amt_ar"];
					amt_ar.AdvancedSearch.SearchOperator2 = filter["w_amt_ar"];
					amt_ar.AdvancedSearch.Save();
				}

				// Field amt_rec
				if (filter.TryGetValue("x_amt_rec", out sv)) {
					amt_rec.AdvancedSearch.SearchValue = sv;
					amt_rec.AdvancedSearch.SearchOperator = filter["z_amt_rec"];
					amt_rec.AdvancedSearch.SearchCondition = filter["v_amt_rec"];
					amt_rec.AdvancedSearch.SearchValue2 = filter["y_amt_rec"];
					amt_rec.AdvancedSearch.SearchOperator2 = filter["w_amt_rec"];
					amt_rec.AdvancedSearch.Save();
				}

				// Field ar_gl_acct
				if (filter.TryGetValue("x_ar_gl_acct", out sv)) {
					ar_gl_acct.AdvancedSearch.SearchValue = sv;
					ar_gl_acct.AdvancedSearch.SearchOperator = filter["z_ar_gl_acct"];
					ar_gl_acct.AdvancedSearch.SearchCondition = filter["v_ar_gl_acct"];
					ar_gl_acct.AdvancedSearch.SearchValue2 = filter["y_ar_gl_acct"];
					ar_gl_acct.AdvancedSearch.SearchOperator2 = filter["w_ar_gl_acct"];
					ar_gl_acct.AdvancedSearch.Save();
				}

				// Field ar_gl_dept
				if (filter.TryGetValue("x_ar_gl_dept", out sv)) {
					ar_gl_dept.AdvancedSearch.SearchValue = sv;
					ar_gl_dept.AdvancedSearch.SearchOperator = filter["z_ar_gl_dept"];
					ar_gl_dept.AdvancedSearch.SearchCondition = filter["v_ar_gl_dept"];
					ar_gl_dept.AdvancedSearch.SearchValue2 = filter["y_ar_gl_dept"];
					ar_gl_dept.AdvancedSearch.SearchOperator2 = filter["w_ar_gl_dept"];
					ar_gl_dept.AdvancedSearch.Save();
				}

				// Field db_cr
				if (filter.TryGetValue("x_db_cr", out sv)) {
					db_cr.AdvancedSearch.SearchValue = sv;
					db_cr.AdvancedSearch.SearchOperator = filter["z_db_cr"];
					db_cr.AdvancedSearch.SearchCondition = filter["v_db_cr"];
					db_cr.AdvancedSearch.SearchValue2 = filter["y_db_cr"];
					db_cr.AdvancedSearch.SearchOperator2 = filter["w_db_cr"];
					db_cr.AdvancedSearch.Save();
				}

				// Field month
				if (filter.TryGetValue("x_month", out sv)) {
					month.AdvancedSearch.SearchValue = sv;
					month.AdvancedSearch.SearchOperator = filter["z_month"];
					month.AdvancedSearch.SearchCondition = filter["v_month"];
					month.AdvancedSearch.SearchValue2 = filter["y_month"];
					month.AdvancedSearch.SearchOperator2 = filter["w_month"];
					month.AdvancedSearch.Save();
				}

				// Field source
				if (filter.TryGetValue("x_source", out sv)) {
					source.AdvancedSearch.SearchValue = sv;
					source.AdvancedSearch.SearchOperator = filter["z_source"];
					source.AdvancedSearch.SearchCondition = filter["v_source"];
					source.AdvancedSearch.SearchValue2 = filter["y_source"];
					source.AdvancedSearch.SearchOperator2 = filter["w_source"];
					source.AdvancedSearch.Save();
				}

				// Field forx_amt
				if (filter.TryGetValue("x_forx_amt", out sv)) {
					forx_amt.AdvancedSearch.SearchValue = sv;
					forx_amt.AdvancedSearch.SearchOperator = filter["z_forx_amt"];
					forx_amt.AdvancedSearch.SearchCondition = filter["v_forx_amt"];
					forx_amt.AdvancedSearch.SearchValue2 = filter["y_forx_amt"];
					forx_amt.AdvancedSearch.SearchOperator2 = filter["w_forx_amt"];
					forx_amt.AdvancedSearch.Save();
				}

				// Field forx_rec
				if (filter.TryGetValue("x_forx_rec", out sv)) {
					forx_rec.AdvancedSearch.SearchValue = sv;
					forx_rec.AdvancedSearch.SearchOperator = filter["z_forx_rec"];
					forx_rec.AdvancedSearch.SearchCondition = filter["v_forx_rec"];
					forx_rec.AdvancedSearch.SearchValue2 = filter["y_forx_rec"];
					forx_rec.AdvancedSearch.SearchOperator2 = filter["w_forx_rec"];
					forx_rec.AdvancedSearch.Save();
				}

				// Field forx_cost
				if (filter.TryGetValue("x_forx_cost", out sv)) {
					forx_cost.AdvancedSearch.SearchValue = sv;
					forx_cost.AdvancedSearch.SearchOperator = filter["z_forx_cost"];
					forx_cost.AdvancedSearch.SearchCondition = filter["v_forx_cost"];
					forx_cost.AdvancedSearch.SearchValue2 = filter["y_forx_cost"];
					forx_cost.AdvancedSearch.SearchOperator2 = filter["w_forx_cost"];
					forx_cost.AdvancedSearch.Save();
				}

				// Field gain_loss
				if (filter.TryGetValue("x_gain_loss", out sv)) {
					gain_loss.AdvancedSearch.SearchValue = sv;
					gain_loss.AdvancedSearch.SearchOperator = filter["z_gain_loss"];
					gain_loss.AdvancedSearch.SearchCondition = filter["v_gain_loss"];
					gain_loss.AdvancedSearch.SearchValue2 = filter["y_gain_loss"];
					gain_loss.AdvancedSearch.SearchOperator2 = filter["w_gain_loss"];
					gain_loss.AdvancedSearch.Save();
				}

				// Field ex_gl_ac
				if (filter.TryGetValue("x_ex_gl_ac", out sv)) {
					ex_gl_ac.AdvancedSearch.SearchValue = sv;
					ex_gl_ac.AdvancedSearch.SearchOperator = filter["z_ex_gl_ac"];
					ex_gl_ac.AdvancedSearch.SearchCondition = filter["v_ex_gl_ac"];
					ex_gl_ac.AdvancedSearch.SearchValue2 = filter["y_ex_gl_ac"];
					ex_gl_ac.AdvancedSearch.SearchOperator2 = filter["w_ex_gl_ac"];
					ex_gl_ac.AdvancedSearch.Save();
				}

				// Field ex_gl_dp
				if (filter.TryGetValue("x_ex_gl_dp", out sv)) {
					ex_gl_dp.AdvancedSearch.SearchValue = sv;
					ex_gl_dp.AdvancedSearch.SearchOperator = filter["z_ex_gl_dp"];
					ex_gl_dp.AdvancedSearch.SearchCondition = filter["v_ex_gl_dp"];
					ex_gl_dp.AdvancedSearch.SearchValue2 = filter["y_ex_gl_dp"];
					ex_gl_dp.AdvancedSearch.SearchOperator2 = filter["w_ex_gl_dp"];
					ex_gl_dp.AdvancedSearch.Save();
				}

				// Field cost
				if (filter.TryGetValue("x_cost", out sv)) {
					cost.AdvancedSearch.SearchValue = sv;
					cost.AdvancedSearch.SearchOperator = filter["z_cost"];
					cost.AdvancedSearch.SearchCondition = filter["v_cost"];
					cost.AdvancedSearch.SearchValue2 = filter["y_cost"];
					cost.AdvancedSearch.SearchOperator2 = filter["w_cost"];
					cost.AdvancedSearch.Save();
				}

				// Field prefix
				if (filter.TryGetValue("x_prefix", out sv)) {
					prefix.AdvancedSearch.SearchValue = sv;
					prefix.AdvancedSearch.SearchOperator = filter["z_prefix"];
					prefix.AdvancedSearch.SearchCondition = filter["v_prefix"];
					prefix.AdvancedSearch.SearchValue2 = filter["y_prefix"];
					prefix.AdvancedSearch.SearchOperator2 = filter["w_prefix"];
					prefix.AdvancedSearch.Save();
				}

				// Field exrate
				if (filter.TryGetValue("x_exrate", out sv)) {
					exrate.AdvancedSearch.SearchValue = sv;
					exrate.AdvancedSearch.SearchOperator = filter["z_exrate"];
					exrate.AdvancedSearch.SearchCondition = filter["v_exrate"];
					exrate.AdvancedSearch.SearchValue2 = filter["y_exrate"];
					exrate.AdvancedSearch.SearchOperator2 = filter["w_exrate"];
					exrate.AdvancedSearch.Save();
				}

				// Field trn_no
				if (filter.TryGetValue("x_trn_no", out sv)) {
					trn_no.AdvancedSearch.SearchValue = sv;
					trn_no.AdvancedSearch.SearchOperator = filter["z_trn_no"];
					trn_no.AdvancedSearch.SearchCondition = filter["v_trn_no"];
					trn_no.AdvancedSearch.SearchValue2 = filter["y_trn_no"];
					trn_no.AdvancedSearch.SearchOperator2 = filter["w_trn_no"];
					trn_no.AdvancedSearch.Save();
				}

				// Field rpl_trn_no
				if (filter.TryGetValue("x_rpl_trn_no", out sv)) {
					rpl_trn_no.AdvancedSearch.SearchValue = sv;
					rpl_trn_no.AdvancedSearch.SearchOperator = filter["z_rpl_trn_no"];
					rpl_trn_no.AdvancedSearch.SearchCondition = filter["v_rpl_trn_no"];
					rpl_trn_no.AdvancedSearch.SearchValue2 = filter["y_rpl_trn_no"];
					rpl_trn_no.AdvancedSearch.SearchOperator2 = filter["w_rpl_trn_no"];
					rpl_trn_no.AdvancedSearch.Save();
				}

				// Field slsman
				if (filter.TryGetValue("x_slsman", out sv)) {
					slsman.AdvancedSearch.SearchValue = sv;
					slsman.AdvancedSearch.SearchOperator = filter["z_slsman"];
					slsman.AdvancedSearch.SearchCondition = filter["v_slsman"];
					slsman.AdvancedSearch.SearchValue2 = filter["y_slsman"];
					slsman.AdvancedSearch.SearchOperator2 = filter["w_slsman"];
					slsman.AdvancedSearch.Save();
				}

				// Field markdelete
				if (filter.TryGetValue("x_markdelete", out sv)) {
					markdelete.AdvancedSearch.SearchValue = sv;
					markdelete.AdvancedSearch.SearchOperator = filter["z_markdelete"];
					markdelete.AdvancedSearch.SearchCondition = filter["v_markdelete"];
					markdelete.AdvancedSearch.SearchValue2 = filter["y_markdelete"];
					markdelete.AdvancedSearch.SearchOperator2 = filter["w_markdelete"];
					markdelete.AdvancedSearch.Save();
				}

				// Field dt_upd
				if (filter.TryGetValue("x_dt_upd", out sv)) {
					dt_upd.AdvancedSearch.SearchValue = sv;
					dt_upd.AdvancedSearch.SearchOperator = filter["z_dt_upd"];
					dt_upd.AdvancedSearch.SearchCondition = filter["v_dt_upd"];
					dt_upd.AdvancedSearch.SearchValue2 = filter["y_dt_upd"];
					dt_upd.AdvancedSearch.SearchOperator2 = filter["w_dt_upd"];
					dt_upd.AdvancedSearch.Save();
				}

				// Field id_upd
				if (filter.TryGetValue("x_id_upd", out sv)) {
					id_upd.AdvancedSearch.SearchValue = sv;
					id_upd.AdvancedSearch.SearchOperator = filter["z_id_upd"];
					id_upd.AdvancedSearch.SearchCondition = filter["v_id_upd"];
					id_upd.AdvancedSearch.SearchValue2 = filter["y_id_upd"];
					id_upd.AdvancedSearch.SearchOperator2 = filter["w_id_upd"];
					id_upd.AdvancedSearch.Save();
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
				BuildBasicSearchSql(ref where, dbcode, keywords, type);
				BuildBasicSearchSql(ref where, ar_dept, keywords, type);
				BuildBasicSearchSql(ref where, ref_no, keywords, type);
				BuildBasicSearchSql(ref where, description, keywords, type);
				BuildBasicSearchSql(ref where, ar_gl_acct, keywords, type);
				BuildBasicSearchSql(ref where, ar_gl_dept, keywords, type);
				BuildBasicSearchSql(ref where, db_cr, keywords, type);
				BuildBasicSearchSql(ref where, month, keywords, type);
				BuildBasicSearchSql(ref where, source, keywords, type);
				BuildBasicSearchSql(ref where, ex_gl_ac, keywords, type);
				BuildBasicSearchSql(ref where, ex_gl_dp, keywords, type);
				BuildBasicSearchSql(ref where, prefix, keywords, type);
				BuildBasicSearchSql(ref where, slsman, keywords, type);
				BuildBasicSearchSql(ref where, id_upd, keywords, type);
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
					UpdateSort(dt_rec); // dt_rec
					UpdateSort(doc_date); // doc_date
					UpdateSort(dbcode); // dbcode
					UpdateSort(ar_dept); // ar_dept
					UpdateSort(ref_no); // ref_no
					UpdateSort(description); // description
					UpdateSort(amt_ar); // amt_ar
					UpdateSort(amt_rec); // amt_rec
					UpdateSort(ar_gl_acct); // ar_gl_acct
					UpdateSort(ar_gl_dept); // ar_gl_dept
					UpdateSort(db_cr); // db_cr
					UpdateSort(month); // month
					UpdateSort(source); // source
					UpdateSort(forx_amt); // forx_amt
					UpdateSort(forx_rec); // forx_rec
					UpdateSort(forx_cost); // forx_cost
					UpdateSort(gain_loss); // gain_loss
					UpdateSort(ex_gl_ac); // ex_gl_ac
					UpdateSort(ex_gl_dp); // ex_gl_dp
					UpdateSort(cost); // cost
					UpdateSort(prefix); // prefix
					UpdateSort(exrate); // exrate
					UpdateSort(trn_no); // trn_no
					UpdateSort(rpl_trn_no); // rpl_trn_no
					UpdateSort(slsman); // slsman
					UpdateSort(markdelete); // markdelete
					UpdateSort(dt_upd); // dt_upd
					UpdateSort(id_upd); // id_upd
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
						dt_rec.Sort = "";
						doc_date.Sort = "";
						dbcode.Sort = "";
						ar_dept.Sort = "";
						ref_no.Sort = "";
						description.Sort = "";
						amt_ar.Sort = "";
						amt_rec.Sort = "";
						ar_gl_acct.Sort = "";
						ar_gl_dept.Sort = "";
						db_cr.Sort = "";
						month.Sort = "";
						source.Sort = "";
						forx_amt.Sort = "";
						forx_rec.Sort = "";
						forx_cost.Sort = "";
						gain_loss.Sort = "";
						ex_gl_ac.Sort = "";
						ex_gl_dp.Sort = "";
						cost.Sort = "";
						prefix.Sort = "";
						exrate.Sort = "";
						trn_no.Sort = "";
						rpl_trn_no.Sort = "";
						slsman.Sort = "";
						markdelete.Sort = "";
						dt_upd.Sort = "";
						id_upd.Sort = "";
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
				RenderListOptionsExt();

				// Call ListOptions_Rendered event
				ListOptions_Rendered();
			}

			// Set up other options
			protected void SetupOtherOptions() {
				ListOptions option;
				ListOption item;
				var options = OtherOptions;
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
				item.Body = "<a class=\"ew-save-filter\" data-form=\"fs_artranslistsrch\" href=\"#\">" + Language.Phrase("SaveCurrentFilter") + "</a>";
				item.Visible = true;
				item = FilterOptions.Add("deletefilter");
				item.Body = "<a class=\"ew-delete-filter\" data-form=\"fs_artranslistsrch\" href=\"#\">" + Language.Phrase("DeleteFilter") + "</a>";
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
						item.Body = "<a class=\"ew-action ew-list-action\" title=\"" + HtmlEncode(caption) + "\" data-caption=\"" + HtmlEncode(caption) + "\" href=\"\" onclick=\"ew.submitAction(event,jQuery.extend({f:document.fs_artranslist}," + act.ToJson(true) + ")); return false;\">" + icon + "</a>";
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
				item.Body = "<button type=\"button\" class=\"btn btn-default ew-search-toggle" + searchToggleClass + "\" title=\"" + Language.Phrase("SearchPanel") + "\" data-caption=\"" + Language.Phrase("SearchPanel") + "\" data-toggle=\"button\" data-form=\"fs_artranslistsrch\">" + Language.Phrase("SearchLink") + "</button>";
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
				dt_rec.SetDbValue(row["dt_rec"]);
				doc_date.SetDbValue(row["doc_date"]);
				dbcode.SetDbValue(row["dbcode"]);
				ar_dept.SetDbValue(row["ar_dept"]);
				ref_no.SetDbValue(row["ref_no"]);
				description.SetDbValue(row["description"]);
				amt_ar.SetDbValue(row["amt_ar"]);
				amt_rec.SetDbValue(row["amt_rec"]);
				ar_gl_acct.SetDbValue(row["ar_gl_acct"]);
				ar_gl_dept.SetDbValue(row["ar_gl_dept"]);
				db_cr.SetDbValue(row["db_cr"]);
				month.SetDbValue(row["month"]);
				source.SetDbValue(row["source"]);
				forx_amt.SetDbValue(row["forx_amt"]);
				forx_rec.SetDbValue(row["forx_rec"]);
				forx_cost.SetDbValue(row["forx_cost"]);
				gain_loss.SetDbValue(row["gain_loss"]);
				ex_gl_ac.SetDbValue(row["ex_gl_ac"]);
				ex_gl_dp.SetDbValue(row["ex_gl_dp"]);
				cost.SetDbValue(row["cost"]);
				prefix.SetDbValue(row["prefix"]);
				exrate.SetDbValue(row["exrate"]);
				trn_no.SetDbValue(row["trn_no"]);
				rpl_trn_no.SetDbValue(row["rpl_trn_no"]);
				slsman.SetDbValue(row["slsman"]);
				markdelete.SetDbValue((ConvertToBool(row["markdelete"]) ? "1" : "0"));
				dt_upd.SetDbValue(row["dt_upd"]);
				id_upd.SetDbValue(row["id_upd"]);
			}

			#pragma warning restore 162, 168, 1998

			// Return a row with default values
			protected Dictionary<string, object> NewRow() {
				var row = new Dictionary<string, object>();
				row.Add("dt_rec", System.DBNull.Value);
				row.Add("doc_date", System.DBNull.Value);
				row.Add("dbcode", System.DBNull.Value);
				row.Add("ar_dept", System.DBNull.Value);
				row.Add("ref_no", System.DBNull.Value);
				row.Add("description", System.DBNull.Value);
				row.Add("amt_ar", System.DBNull.Value);
				row.Add("amt_rec", System.DBNull.Value);
				row.Add("ar_gl_acct", System.DBNull.Value);
				row.Add("ar_gl_dept", System.DBNull.Value);
				row.Add("db_cr", System.DBNull.Value);
				row.Add("month", System.DBNull.Value);
				row.Add("source", System.DBNull.Value);
				row.Add("forx_amt", System.DBNull.Value);
				row.Add("forx_rec", System.DBNull.Value);
				row.Add("forx_cost", System.DBNull.Value);
				row.Add("gain_loss", System.DBNull.Value);
				row.Add("ex_gl_ac", System.DBNull.Value);
				row.Add("ex_gl_dp", System.DBNull.Value);
				row.Add("cost", System.DBNull.Value);
				row.Add("prefix", System.DBNull.Value);
				row.Add("exrate", System.DBNull.Value);
				row.Add("trn_no", System.DBNull.Value);
				row.Add("rpl_trn_no", System.DBNull.Value);
				row.Add("slsman", System.DBNull.Value);
				row.Add("markdelete", System.DBNull.Value);
				row.Add("dt_upd", System.DBNull.Value);
				row.Add("id_upd", System.DBNull.Value);
				return row;
			}

			#pragma warning disable 618, 1998

			// Load old record
			protected async Task<bool> LoadOldRecord(DatabaseConnectionBase<SqlConnection, SqlCommand, SqlDataReader, SqlDbType> cnn = null) {
				return false;
			}

			#pragma warning restore 618, 1998

			#pragma warning disable 1998

			// Render row values based on field settings
			public async Task RenderRow() {

				// Convert decimal values if posted back
				if (SameString(amt_ar.FormValue, amt_ar.CurrentValue) && IsNumeric(ConvertToFloatString(amt_ar.CurrentValue)))
					amt_ar.CurrentValue = ConvertToFloatString(amt_ar.CurrentValue);

				// Convert decimal values if posted back
				if (SameString(amt_rec.FormValue, amt_rec.CurrentValue) && IsNumeric(ConvertToFloatString(amt_rec.CurrentValue)))
					amt_rec.CurrentValue = ConvertToFloatString(amt_rec.CurrentValue);

				// Convert decimal values if posted back
				if (SameString(forx_amt.FormValue, forx_amt.CurrentValue) && IsNumeric(ConvertToFloatString(forx_amt.CurrentValue)))
					forx_amt.CurrentValue = ConvertToFloatString(forx_amt.CurrentValue);

				// Convert decimal values if posted back
				if (SameString(forx_rec.FormValue, forx_rec.CurrentValue) && IsNumeric(ConvertToFloatString(forx_rec.CurrentValue)))
					forx_rec.CurrentValue = ConvertToFloatString(forx_rec.CurrentValue);

				// Convert decimal values if posted back
				if (SameString(forx_cost.FormValue, forx_cost.CurrentValue) && IsNumeric(ConvertToFloatString(forx_cost.CurrentValue)))
					forx_cost.CurrentValue = ConvertToFloatString(forx_cost.CurrentValue);

				// Convert decimal values if posted back
				if (SameString(gain_loss.FormValue, gain_loss.CurrentValue) && IsNumeric(ConvertToFloatString(gain_loss.CurrentValue)))
					gain_loss.CurrentValue = ConvertToFloatString(gain_loss.CurrentValue);

				// Convert decimal values if posted back
				if (SameString(cost.FormValue, cost.CurrentValue) && IsNumeric(ConvertToFloatString(cost.CurrentValue)))
					cost.CurrentValue = ConvertToFloatString(cost.CurrentValue);

				// Convert decimal values if posted back
				if (SameString(exrate.FormValue, exrate.CurrentValue) && IsNumeric(ConvertToFloatString(exrate.CurrentValue)))
					exrate.CurrentValue = ConvertToFloatString(exrate.CurrentValue);

				// Call Row_Rendering event
				Row_Rendering();

				// Common render codes for all row types
				// dt_rec
				// doc_date
				// dbcode
				// ar_dept
				// ref_no
				// description
				// amt_ar
				// amt_rec
				// ar_gl_acct
				// ar_gl_dept
				// db_cr
				// month
				// source
				// forx_amt
				// forx_rec
				// forx_cost
				// gain_loss
				// ex_gl_ac
				// ex_gl_dp
				// cost
				// prefix
				// exrate
				// trn_no
				// rpl_trn_no
				// slsman
				// markdelete
				// dt_upd
				// id_upd

				if (RowType == Config.RowTypeView) { // View row

					// dt_rec
					dt_rec.ViewValue = dt_rec.CurrentValue;
					dt_rec.ViewValue = FormatDateTime(dt_rec.ViewValue, 0);

					// doc_date
					doc_date.ViewValue = doc_date.CurrentValue;
					doc_date.ViewValue = FormatDateTime(doc_date.ViewValue, 0);

					// dbcode
					dbcode.ViewValue = dbcode.CurrentValue;

					// ar_dept
					ar_dept.ViewValue = ar_dept.CurrentValue;

					// ref_no
					ref_no.ViewValue = ref_no.CurrentValue;

					// description
					description.ViewValue = description.CurrentValue;

					// amt_ar
					amt_ar.ViewValue = amt_ar.CurrentValue;
					amt_ar.ViewValue = FormatNumber(amt_ar.ViewValue, 2, -2, -2, -2);

					// amt_rec
					amt_rec.ViewValue = amt_rec.CurrentValue;
					amt_rec.ViewValue = FormatNumber(amt_rec.ViewValue, 2, -2, -2, -2);

					// ar_gl_acct
					ar_gl_acct.ViewValue = ar_gl_acct.CurrentValue;

					// ar_gl_dept
					ar_gl_dept.ViewValue = ar_gl_dept.CurrentValue;

					// db_cr
					db_cr.ViewValue = db_cr.CurrentValue;

					// month
					month.ViewValue = month.CurrentValue;

					// source
					source.ViewValue = source.CurrentValue;

					// forx_amt
					forx_amt.ViewValue = forx_amt.CurrentValue;
					forx_amt.ViewValue = FormatNumber(forx_amt.ViewValue, 2, -2, -2, -2);

					// forx_rec
					forx_rec.ViewValue = forx_rec.CurrentValue;
					forx_rec.ViewValue = FormatNumber(forx_rec.ViewValue, 2, -2, -2, -2);

					// forx_cost
					forx_cost.ViewValue = forx_cost.CurrentValue;
					forx_cost.ViewValue = FormatNumber(forx_cost.ViewValue, 2, -2, -2, -2);

					// gain_loss
					gain_loss.ViewValue = gain_loss.CurrentValue;
					gain_loss.ViewValue = FormatNumber(gain_loss.ViewValue, 2, -2, -2, -2);

					// ex_gl_ac
					ex_gl_ac.ViewValue = ex_gl_ac.CurrentValue;

					// ex_gl_dp
					ex_gl_dp.ViewValue = ex_gl_dp.CurrentValue;

					// cost
					cost.ViewValue = cost.CurrentValue;
					cost.ViewValue = FormatNumber(cost.ViewValue, 2, -2, -2, -2);

					// prefix
					prefix.ViewValue = prefix.CurrentValue;

					// exrate
					exrate.ViewValue = exrate.CurrentValue;
					exrate.ViewValue = FormatNumber(exrate.ViewValue, 2, -2, -2, -2);

					// trn_no
					trn_no.ViewValue = trn_no.CurrentValue;
					trn_no.ViewValue = FormatNumber(trn_no.ViewValue, 0, -2, -2, -2);

					// rpl_trn_no
					rpl_trn_no.ViewValue = rpl_trn_no.CurrentValue;
					rpl_trn_no.ViewValue = FormatNumber(rpl_trn_no.ViewValue, 0, -2, -2, -2);

					// slsman
					slsman.ViewValue = slsman.CurrentValue;

					// markdelete
					if (ConvertToBool(markdelete.CurrentValue)) {
						markdelete.ViewValue = (markdelete.TagCaption(1) != "") ? markdelete.TagCaption(1) : "Yes";
					} else {
						markdelete.ViewValue = (markdelete.TagCaption(2) != "") ? markdelete.TagCaption(2) : "No";
					}

					// dt_upd
					dt_upd.ViewValue = dt_upd.CurrentValue;
					dt_upd.ViewValue = FormatDateTime(dt_upd.ViewValue, 0);

					// id_upd
					id_upd.ViewValue = id_upd.CurrentValue;

					// dt_rec
					dt_rec.HrefValue = "";
					dt_rec.TooltipValue = "";

					// doc_date
					doc_date.HrefValue = "";
					doc_date.TooltipValue = "";

					// dbcode
					dbcode.HrefValue = "";
					dbcode.TooltipValue = "";

					// ar_dept
					ar_dept.HrefValue = "";
					ar_dept.TooltipValue = "";

					// ref_no
					ref_no.HrefValue = "";
					ref_no.TooltipValue = "";

					// description
					description.HrefValue = "";
					description.TooltipValue = "";

					// amt_ar
					amt_ar.HrefValue = "";
					amt_ar.TooltipValue = "";

					// amt_rec
					amt_rec.HrefValue = "";
					amt_rec.TooltipValue = "";

					// ar_gl_acct
					ar_gl_acct.HrefValue = "";
					ar_gl_acct.TooltipValue = "";

					// ar_gl_dept
					ar_gl_dept.HrefValue = "";
					ar_gl_dept.TooltipValue = "";

					// db_cr
					db_cr.HrefValue = "";
					db_cr.TooltipValue = "";

					// month
					month.HrefValue = "";
					month.TooltipValue = "";

					// source
					source.HrefValue = "";
					source.TooltipValue = "";

					// forx_amt
					forx_amt.HrefValue = "";
					forx_amt.TooltipValue = "";

					// forx_rec
					forx_rec.HrefValue = "";
					forx_rec.TooltipValue = "";

					// forx_cost
					forx_cost.HrefValue = "";
					forx_cost.TooltipValue = "";

					// gain_loss
					gain_loss.HrefValue = "";
					gain_loss.TooltipValue = "";

					// ex_gl_ac
					ex_gl_ac.HrefValue = "";
					ex_gl_ac.TooltipValue = "";

					// ex_gl_dp
					ex_gl_dp.HrefValue = "";
					ex_gl_dp.TooltipValue = "";

					// cost
					cost.HrefValue = "";
					cost.TooltipValue = "";

					// prefix
					prefix.HrefValue = "";
					prefix.TooltipValue = "";

					// exrate
					exrate.HrefValue = "";
					exrate.TooltipValue = "";

					// trn_no
					trn_no.HrefValue = "";
					trn_no.TooltipValue = "";

					// rpl_trn_no
					rpl_trn_no.HrefValue = "";
					rpl_trn_no.TooltipValue = "";

					// slsman
					slsman.HrefValue = "";
					slsman.TooltipValue = "";

					// markdelete
					markdelete.HrefValue = "";
					markdelete.TooltipValue = "";

					// dt_upd
					dt_upd.HrefValue = "";
					dt_upd.TooltipValue = "";

					// id_upd
					id_upd.HrefValue = "";
					id_upd.TooltipValue = "";
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
