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
		/// s_employee_List
		/// </summary>

		public static _s_employee_List s_employee_List {
			get => HttpData.Get<_s_employee_List>("s_employee_List");
			set => HttpData["s_employee_List"] = value;
		}

		/// <summary>
		/// Page class for s_employee
		/// </summary>

		public class _s_employee_List : _s_employee_ListBase
		{

			// Construtor
			public _s_employee_List(Controller controller = null) : base(controller) {
			}
		}

		/// <summary>
		/// Page base class
		/// </summary>

		public class _s_employee_ListBase : _s_employee, IAspNetMakerPage
		{

			// Page ID
			public string PageID = "list";

			// Project ID
			public string ProjectID = "{8543F230-11C6-4105-B51C-8D87C21BE659}";

			// Table name
			public string TableName = "s_employee";

			// Page object name
			public string PageObjName = "s_employee_List";

			// Grid form hidden field names
			public string FormName = "fs_employeelist";
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
			public _s_employee_ListBase(Controller controller = null) { // DN
				if (controller != null)
					Controller = controller;

				// Initialize
				CurrentPage = this;

				// Language object
				Language = Language ?? new Lang();

				// Table object (s_employee)
				if (s_employee == null || s_employee is _s_employee)
					s_employee = this;

				// Initialize URLs
				ExportPrintUrl = PageUrl + "export=print";
				ExportExcelUrl = PageUrl + "export=excel";
				ExportWordUrl = PageUrl + "export=word";
				ExportHtmlUrl = PageUrl + "export=html";
				ExportXmlUrl = PageUrl + "export=xml";
				ExportCsvUrl = PageUrl + "export=csv";
				ExportPdfUrl = PageUrl + "export=pdf";
				AddUrl = "s_employeeadd";
				InlineAddUrl = PageUrl + "action=add";
				GridAddUrl = PageUrl + "action=gridadd";
				GridEditUrl = PageUrl + "action=gridedit";
				MultiDeleteUrl = "s_employeedelete";
				MultiUpdateUrl = "s_employeeupdate";

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
				FilterOptions = new ListOptions { Tag = "div", TagClassName = "ew-filter-option fs_employeelistsrch" };

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
					if (Security.IsLoggedIn) {
						Security.UserID_Loading();
						await Security.LoadUserID();
						Security.UserID_Loaded();
					if (Empty(Security.CurrentUserID)) {
						FailureMessage = DeniedMessage(); // Set no permission
						return Terminate();
					}
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
				photopath.Visible = false;
				report_to.SetVisibility();
				login_effectivedate.SetVisibility();
				login_disableddate.SetVisibility();
				UserLevelId.SetVisibility();
				_Username.SetVisibility();
				password.SetVisibility();
				active.SetVisibility();
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
				await SetupLookupOptions(UserLevelId);

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
					s_employee.Id.FormValue = arKeyFlds[0];
					if (!IsNumeric(s_employee.Id.FormValue))
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
				filters.Merge(JObject.Parse(employeeid.AdvancedSearch.ToJson())); // Field employeeid
				filters.Merge(JObject.Parse(fname.AdvancedSearch.ToJson())); // Field fname
				filters.Merge(JObject.Parse(lname.AdvancedSearch.ToJson())); // Field lname
				filters.Merge(JObject.Parse(oldic.AdvancedSearch.ToJson())); // Field oldic
				filters.Merge(JObject.Parse(newic.AdvancedSearch.ToJson())); // Field newic
				filters.Merge(JObject.Parse(passport.AdvancedSearch.ToJson())); // Field passport
				filters.Merge(JObject.Parse(address1.AdvancedSearch.ToJson())); // Field address1
				filters.Merge(JObject.Parse(address2.AdvancedSearch.ToJson())); // Field address2
				filters.Merge(JObject.Parse(Id_city.AdvancedSearch.ToJson())); // Field Id_city
				filters.Merge(JObject.Parse(Id_state.AdvancedSearch.ToJson())); // Field Id_state
				filters.Merge(JObject.Parse(Id_country.AdvancedSearch.ToJson())); // Field Id_country
				filters.Merge(JObject.Parse(postcode.AdvancedSearch.ToJson())); // Field postcode
				filters.Merge(JObject.Parse(gender.AdvancedSearch.ToJson())); // Field gender
				filters.Merge(JObject.Parse(tel.AdvancedSearch.ToJson())); // Field tel
				filters.Merge(JObject.Parse(handphone.AdvancedSearch.ToJson())); // Field handphone
				filters.Merge(JObject.Parse(_email.AdvancedSearch.ToJson())); // Field email
				filters.Merge(JObject.Parse(dob.AdvancedSearch.ToJson())); // Field dob
				filters.Merge(JObject.Parse(children.AdvancedSearch.ToJson())); // Field children
				filters.Merge(JObject.Parse(datejoin.AdvancedSearch.ToJson())); // Field datejoin
				filters.Merge(JObject.Parse(dateresign.AdvancedSearch.ToJson())); // Field dateresign
				filters.Merge(JObject.Parse(marriedstatus.AdvancedSearch.ToJson())); // Field marriedstatus
				filters.Merge(JObject.Parse(Id_dept.AdvancedSearch.ToJson())); // Field Id_dept
				filters.Merge(JObject.Parse(Id_position.AdvancedSearch.ToJson())); // Field Id_position
				filters.Merge(JObject.Parse(Id_race.AdvancedSearch.ToJson())); // Field Id_race
				filters.Merge(JObject.Parse(photopath.AdvancedSearch.ToJson())); // Field photopath
				filters.Merge(JObject.Parse(report_to.AdvancedSearch.ToJson())); // Field report_to
				filters.Merge(JObject.Parse(login_effectivedate.AdvancedSearch.ToJson())); // Field login_effectivedate
				filters.Merge(JObject.Parse(login_disableddate.AdvancedSearch.ToJson())); // Field login_disableddate
				filters.Merge(JObject.Parse(UserLevelId.AdvancedSearch.ToJson())); // Field UserLevelId
				filters.Merge(JObject.Parse(_Username.AdvancedSearch.ToJson())); // Field Username
				filters.Merge(JObject.Parse(password.AdvancedSearch.ToJson())); // Field password
				filters.Merge(JObject.Parse(active.AdvancedSearch.ToJson())); // Field active
				filters.Merge(JObject.Parse(s_employee.BasicSearch.ToJson()));

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

				// Field employeeid
				if (filter.TryGetValue("x_employeeid", out sv)) {
					employeeid.AdvancedSearch.SearchValue = sv;
					employeeid.AdvancedSearch.SearchOperator = filter["z_employeeid"];
					employeeid.AdvancedSearch.SearchCondition = filter["v_employeeid"];
					employeeid.AdvancedSearch.SearchValue2 = filter["y_employeeid"];
					employeeid.AdvancedSearch.SearchOperator2 = filter["w_employeeid"];
					employeeid.AdvancedSearch.Save();
				}

				// Field fname
				if (filter.TryGetValue("x_fname", out sv)) {
					fname.AdvancedSearch.SearchValue = sv;
					fname.AdvancedSearch.SearchOperator = filter["z_fname"];
					fname.AdvancedSearch.SearchCondition = filter["v_fname"];
					fname.AdvancedSearch.SearchValue2 = filter["y_fname"];
					fname.AdvancedSearch.SearchOperator2 = filter["w_fname"];
					fname.AdvancedSearch.Save();
				}

				// Field lname
				if (filter.TryGetValue("x_lname", out sv)) {
					lname.AdvancedSearch.SearchValue = sv;
					lname.AdvancedSearch.SearchOperator = filter["z_lname"];
					lname.AdvancedSearch.SearchCondition = filter["v_lname"];
					lname.AdvancedSearch.SearchValue2 = filter["y_lname"];
					lname.AdvancedSearch.SearchOperator2 = filter["w_lname"];
					lname.AdvancedSearch.Save();
				}

				// Field oldic
				if (filter.TryGetValue("x_oldic", out sv)) {
					oldic.AdvancedSearch.SearchValue = sv;
					oldic.AdvancedSearch.SearchOperator = filter["z_oldic"];
					oldic.AdvancedSearch.SearchCondition = filter["v_oldic"];
					oldic.AdvancedSearch.SearchValue2 = filter["y_oldic"];
					oldic.AdvancedSearch.SearchOperator2 = filter["w_oldic"];
					oldic.AdvancedSearch.Save();
				}

				// Field newic
				if (filter.TryGetValue("x_newic", out sv)) {
					newic.AdvancedSearch.SearchValue = sv;
					newic.AdvancedSearch.SearchOperator = filter["z_newic"];
					newic.AdvancedSearch.SearchCondition = filter["v_newic"];
					newic.AdvancedSearch.SearchValue2 = filter["y_newic"];
					newic.AdvancedSearch.SearchOperator2 = filter["w_newic"];
					newic.AdvancedSearch.Save();
				}

				// Field passport
				if (filter.TryGetValue("x_passport", out sv)) {
					passport.AdvancedSearch.SearchValue = sv;
					passport.AdvancedSearch.SearchOperator = filter["z_passport"];
					passport.AdvancedSearch.SearchCondition = filter["v_passport"];
					passport.AdvancedSearch.SearchValue2 = filter["y_passport"];
					passport.AdvancedSearch.SearchOperator2 = filter["w_passport"];
					passport.AdvancedSearch.Save();
				}

				// Field address1
				if (filter.TryGetValue("x_address1", out sv)) {
					address1.AdvancedSearch.SearchValue = sv;
					address1.AdvancedSearch.SearchOperator = filter["z_address1"];
					address1.AdvancedSearch.SearchCondition = filter["v_address1"];
					address1.AdvancedSearch.SearchValue2 = filter["y_address1"];
					address1.AdvancedSearch.SearchOperator2 = filter["w_address1"];
					address1.AdvancedSearch.Save();
				}

				// Field address2
				if (filter.TryGetValue("x_address2", out sv)) {
					address2.AdvancedSearch.SearchValue = sv;
					address2.AdvancedSearch.SearchOperator = filter["z_address2"];
					address2.AdvancedSearch.SearchCondition = filter["v_address2"];
					address2.AdvancedSearch.SearchValue2 = filter["y_address2"];
					address2.AdvancedSearch.SearchOperator2 = filter["w_address2"];
					address2.AdvancedSearch.Save();
				}

				// Field Id_city
				if (filter.TryGetValue("x_Id_city", out sv)) {
					Id_city.AdvancedSearch.SearchValue = sv;
					Id_city.AdvancedSearch.SearchOperator = filter["z_Id_city"];
					Id_city.AdvancedSearch.SearchCondition = filter["v_Id_city"];
					Id_city.AdvancedSearch.SearchValue2 = filter["y_Id_city"];
					Id_city.AdvancedSearch.SearchOperator2 = filter["w_Id_city"];
					Id_city.AdvancedSearch.Save();
				}

				// Field Id_state
				if (filter.TryGetValue("x_Id_state", out sv)) {
					Id_state.AdvancedSearch.SearchValue = sv;
					Id_state.AdvancedSearch.SearchOperator = filter["z_Id_state"];
					Id_state.AdvancedSearch.SearchCondition = filter["v_Id_state"];
					Id_state.AdvancedSearch.SearchValue2 = filter["y_Id_state"];
					Id_state.AdvancedSearch.SearchOperator2 = filter["w_Id_state"];
					Id_state.AdvancedSearch.Save();
				}

				// Field Id_country
				if (filter.TryGetValue("x_Id_country", out sv)) {
					Id_country.AdvancedSearch.SearchValue = sv;
					Id_country.AdvancedSearch.SearchOperator = filter["z_Id_country"];
					Id_country.AdvancedSearch.SearchCondition = filter["v_Id_country"];
					Id_country.AdvancedSearch.SearchValue2 = filter["y_Id_country"];
					Id_country.AdvancedSearch.SearchOperator2 = filter["w_Id_country"];
					Id_country.AdvancedSearch.Save();
				}

				// Field postcode
				if (filter.TryGetValue("x_postcode", out sv)) {
					postcode.AdvancedSearch.SearchValue = sv;
					postcode.AdvancedSearch.SearchOperator = filter["z_postcode"];
					postcode.AdvancedSearch.SearchCondition = filter["v_postcode"];
					postcode.AdvancedSearch.SearchValue2 = filter["y_postcode"];
					postcode.AdvancedSearch.SearchOperator2 = filter["w_postcode"];
					postcode.AdvancedSearch.Save();
				}

				// Field gender
				if (filter.TryGetValue("x_gender", out sv)) {
					gender.AdvancedSearch.SearchValue = sv;
					gender.AdvancedSearch.SearchOperator = filter["z_gender"];
					gender.AdvancedSearch.SearchCondition = filter["v_gender"];
					gender.AdvancedSearch.SearchValue2 = filter["y_gender"];
					gender.AdvancedSearch.SearchOperator2 = filter["w_gender"];
					gender.AdvancedSearch.Save();
				}

				// Field tel
				if (filter.TryGetValue("x_tel", out sv)) {
					tel.AdvancedSearch.SearchValue = sv;
					tel.AdvancedSearch.SearchOperator = filter["z_tel"];
					tel.AdvancedSearch.SearchCondition = filter["v_tel"];
					tel.AdvancedSearch.SearchValue2 = filter["y_tel"];
					tel.AdvancedSearch.SearchOperator2 = filter["w_tel"];
					tel.AdvancedSearch.Save();
				}

				// Field handphone
				if (filter.TryGetValue("x_handphone", out sv)) {
					handphone.AdvancedSearch.SearchValue = sv;
					handphone.AdvancedSearch.SearchOperator = filter["z_handphone"];
					handphone.AdvancedSearch.SearchCondition = filter["v_handphone"];
					handphone.AdvancedSearch.SearchValue2 = filter["y_handphone"];
					handphone.AdvancedSearch.SearchOperator2 = filter["w_handphone"];
					handphone.AdvancedSearch.Save();
				}

				// Field email
				if (filter.TryGetValue("x__email", out sv)) {
					_email.AdvancedSearch.SearchValue = sv;
					_email.AdvancedSearch.SearchOperator = filter["z__email"];
					_email.AdvancedSearch.SearchCondition = filter["v__email"];
					_email.AdvancedSearch.SearchValue2 = filter["y__email"];
					_email.AdvancedSearch.SearchOperator2 = filter["w__email"];
					_email.AdvancedSearch.Save();
				}

				// Field dob
				if (filter.TryGetValue("x_dob", out sv)) {
					dob.AdvancedSearch.SearchValue = sv;
					dob.AdvancedSearch.SearchOperator = filter["z_dob"];
					dob.AdvancedSearch.SearchCondition = filter["v_dob"];
					dob.AdvancedSearch.SearchValue2 = filter["y_dob"];
					dob.AdvancedSearch.SearchOperator2 = filter["w_dob"];
					dob.AdvancedSearch.Save();
				}

				// Field children
				if (filter.TryGetValue("x_children", out sv)) {
					children.AdvancedSearch.SearchValue = sv;
					children.AdvancedSearch.SearchOperator = filter["z_children"];
					children.AdvancedSearch.SearchCondition = filter["v_children"];
					children.AdvancedSearch.SearchValue2 = filter["y_children"];
					children.AdvancedSearch.SearchOperator2 = filter["w_children"];
					children.AdvancedSearch.Save();
				}

				// Field datejoin
				if (filter.TryGetValue("x_datejoin", out sv)) {
					datejoin.AdvancedSearch.SearchValue = sv;
					datejoin.AdvancedSearch.SearchOperator = filter["z_datejoin"];
					datejoin.AdvancedSearch.SearchCondition = filter["v_datejoin"];
					datejoin.AdvancedSearch.SearchValue2 = filter["y_datejoin"];
					datejoin.AdvancedSearch.SearchOperator2 = filter["w_datejoin"];
					datejoin.AdvancedSearch.Save();
				}

				// Field dateresign
				if (filter.TryGetValue("x_dateresign", out sv)) {
					dateresign.AdvancedSearch.SearchValue = sv;
					dateresign.AdvancedSearch.SearchOperator = filter["z_dateresign"];
					dateresign.AdvancedSearch.SearchCondition = filter["v_dateresign"];
					dateresign.AdvancedSearch.SearchValue2 = filter["y_dateresign"];
					dateresign.AdvancedSearch.SearchOperator2 = filter["w_dateresign"];
					dateresign.AdvancedSearch.Save();
				}

				// Field marriedstatus
				if (filter.TryGetValue("x_marriedstatus", out sv)) {
					marriedstatus.AdvancedSearch.SearchValue = sv;
					marriedstatus.AdvancedSearch.SearchOperator = filter["z_marriedstatus"];
					marriedstatus.AdvancedSearch.SearchCondition = filter["v_marriedstatus"];
					marriedstatus.AdvancedSearch.SearchValue2 = filter["y_marriedstatus"];
					marriedstatus.AdvancedSearch.SearchOperator2 = filter["w_marriedstatus"];
					marriedstatus.AdvancedSearch.Save();
				}

				// Field Id_dept
				if (filter.TryGetValue("x_Id_dept", out sv)) {
					Id_dept.AdvancedSearch.SearchValue = sv;
					Id_dept.AdvancedSearch.SearchOperator = filter["z_Id_dept"];
					Id_dept.AdvancedSearch.SearchCondition = filter["v_Id_dept"];
					Id_dept.AdvancedSearch.SearchValue2 = filter["y_Id_dept"];
					Id_dept.AdvancedSearch.SearchOperator2 = filter["w_Id_dept"];
					Id_dept.AdvancedSearch.Save();
				}

				// Field Id_position
				if (filter.TryGetValue("x_Id_position", out sv)) {
					Id_position.AdvancedSearch.SearchValue = sv;
					Id_position.AdvancedSearch.SearchOperator = filter["z_Id_position"];
					Id_position.AdvancedSearch.SearchCondition = filter["v_Id_position"];
					Id_position.AdvancedSearch.SearchValue2 = filter["y_Id_position"];
					Id_position.AdvancedSearch.SearchOperator2 = filter["w_Id_position"];
					Id_position.AdvancedSearch.Save();
				}

				// Field Id_race
				if (filter.TryGetValue("x_Id_race", out sv)) {
					Id_race.AdvancedSearch.SearchValue = sv;
					Id_race.AdvancedSearch.SearchOperator = filter["z_Id_race"];
					Id_race.AdvancedSearch.SearchCondition = filter["v_Id_race"];
					Id_race.AdvancedSearch.SearchValue2 = filter["y_Id_race"];
					Id_race.AdvancedSearch.SearchOperator2 = filter["w_Id_race"];
					Id_race.AdvancedSearch.Save();
				}

				// Field photopath
				if (filter.TryGetValue("x_photopath", out sv)) {
					photopath.AdvancedSearch.SearchValue = sv;
					photopath.AdvancedSearch.SearchOperator = filter["z_photopath"];
					photopath.AdvancedSearch.SearchCondition = filter["v_photopath"];
					photopath.AdvancedSearch.SearchValue2 = filter["y_photopath"];
					photopath.AdvancedSearch.SearchOperator2 = filter["w_photopath"];
					photopath.AdvancedSearch.Save();
				}

				// Field report_to
				if (filter.TryGetValue("x_report_to", out sv)) {
					report_to.AdvancedSearch.SearchValue = sv;
					report_to.AdvancedSearch.SearchOperator = filter["z_report_to"];
					report_to.AdvancedSearch.SearchCondition = filter["v_report_to"];
					report_to.AdvancedSearch.SearchValue2 = filter["y_report_to"];
					report_to.AdvancedSearch.SearchOperator2 = filter["w_report_to"];
					report_to.AdvancedSearch.Save();
				}

				// Field login_effectivedate
				if (filter.TryGetValue("x_login_effectivedate", out sv)) {
					login_effectivedate.AdvancedSearch.SearchValue = sv;
					login_effectivedate.AdvancedSearch.SearchOperator = filter["z_login_effectivedate"];
					login_effectivedate.AdvancedSearch.SearchCondition = filter["v_login_effectivedate"];
					login_effectivedate.AdvancedSearch.SearchValue2 = filter["y_login_effectivedate"];
					login_effectivedate.AdvancedSearch.SearchOperator2 = filter["w_login_effectivedate"];
					login_effectivedate.AdvancedSearch.Save();
				}

				// Field login_disableddate
				if (filter.TryGetValue("x_login_disableddate", out sv)) {
					login_disableddate.AdvancedSearch.SearchValue = sv;
					login_disableddate.AdvancedSearch.SearchOperator = filter["z_login_disableddate"];
					login_disableddate.AdvancedSearch.SearchCondition = filter["v_login_disableddate"];
					login_disableddate.AdvancedSearch.SearchValue2 = filter["y_login_disableddate"];
					login_disableddate.AdvancedSearch.SearchOperator2 = filter["w_login_disableddate"];
					login_disableddate.AdvancedSearch.Save();
				}

				// Field UserLevelId
				if (filter.TryGetValue("x_UserLevelId", out sv)) {
					UserLevelId.AdvancedSearch.SearchValue = sv;
					UserLevelId.AdvancedSearch.SearchOperator = filter["z_UserLevelId"];
					UserLevelId.AdvancedSearch.SearchCondition = filter["v_UserLevelId"];
					UserLevelId.AdvancedSearch.SearchValue2 = filter["y_UserLevelId"];
					UserLevelId.AdvancedSearch.SearchOperator2 = filter["w_UserLevelId"];
					UserLevelId.AdvancedSearch.Save();
				}

				// Field Username
				if (filter.TryGetValue("x__Username", out sv)) {
					_Username.AdvancedSearch.SearchValue = sv;
					_Username.AdvancedSearch.SearchOperator = filter["z__Username"];
					_Username.AdvancedSearch.SearchCondition = filter["v__Username"];
					_Username.AdvancedSearch.SearchValue2 = filter["y__Username"];
					_Username.AdvancedSearch.SearchOperator2 = filter["w__Username"];
					_Username.AdvancedSearch.Save();
				}

				// Field password
				if (filter.TryGetValue("x_password", out sv)) {
					password.AdvancedSearch.SearchValue = sv;
					password.AdvancedSearch.SearchOperator = filter["z_password"];
					password.AdvancedSearch.SearchCondition = filter["v_password"];
					password.AdvancedSearch.SearchValue2 = filter["y_password"];
					password.AdvancedSearch.SearchOperator2 = filter["w_password"];
					password.AdvancedSearch.Save();
				}

				// Field active
				if (filter.TryGetValue("x_active", out sv)) {
					active.AdvancedSearch.SearchValue = sv;
					active.AdvancedSearch.SearchOperator = filter["z_active"];
					active.AdvancedSearch.SearchCondition = filter["v_active"];
					active.AdvancedSearch.SearchValue2 = filter["y_active"];
					active.AdvancedSearch.SearchOperator2 = filter["w_active"];
					active.AdvancedSearch.Save();
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
				BuildBasicSearchSql(ref where, employeeid, keywords, type);
				BuildBasicSearchSql(ref where, fname, keywords, type);
				BuildBasicSearchSql(ref where, lname, keywords, type);
				BuildBasicSearchSql(ref where, oldic, keywords, type);
				BuildBasicSearchSql(ref where, newic, keywords, type);
				BuildBasicSearchSql(ref where, passport, keywords, type);
				BuildBasicSearchSql(ref where, address1, keywords, type);
				BuildBasicSearchSql(ref where, address2, keywords, type);
				BuildBasicSearchSql(ref where, postcode, keywords, type);
				BuildBasicSearchSql(ref where, gender, keywords, type);
				BuildBasicSearchSql(ref where, tel, keywords, type);
				BuildBasicSearchSql(ref where, handphone, keywords, type);
				BuildBasicSearchSql(ref where, _email, keywords, type);
				BuildBasicSearchSql(ref where, marriedstatus, keywords, type);
				BuildBasicSearchSql(ref where, photopath, keywords, type);
				BuildBasicSearchSql(ref where, _Username, keywords, type);
				BuildBasicSearchSql(ref where, password, keywords, type);
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
					UpdateSort(employeeid); // employeeid
					UpdateSort(fname); // fname
					UpdateSort(lname); // lname
					UpdateSort(oldic); // oldic
					UpdateSort(newic); // newic
					UpdateSort(passport); // passport
					UpdateSort(address1); // address1
					UpdateSort(address2); // address2
					UpdateSort(Id_city); // Id_city
					UpdateSort(Id_state); // Id_state
					UpdateSort(Id_country); // Id_country
					UpdateSort(postcode); // postcode
					UpdateSort(gender); // gender
					UpdateSort(tel); // tel
					UpdateSort(handphone); // handphone
					UpdateSort(_email); // _email
					UpdateSort(dob); // dob
					UpdateSort(children); // children
					UpdateSort(datejoin); // datejoin
					UpdateSort(dateresign); // dateresign
					UpdateSort(marriedstatus); // marriedstatus
					UpdateSort(Id_dept); // Id_dept
					UpdateSort(Id_position); // Id_position
					UpdateSort(Id_race); // Id_race
					UpdateSort(report_to); // report_to
					UpdateSort(login_effectivedate); // login_effectivedate
					UpdateSort(login_disableddate); // login_disableddate
					UpdateSort(UserLevelId); // UserLevelId
					UpdateSort(_Username); // _Username
					UpdateSort(password); // password
					UpdateSort(active); // active
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
						employeeid.Sort = "";
						fname.Sort = "";
						lname.Sort = "";
						oldic.Sort = "";
						newic.Sort = "";
						passport.Sort = "";
						address1.Sort = "";
						address2.Sort = "";
						Id_city.Sort = "";
						Id_state.Sort = "";
						Id_country.Sort = "";
						postcode.Sort = "";
						gender.Sort = "";
						tel.Sort = "";
						handphone.Sort = "";
						_email.Sort = "";
						dob.Sort = "";
						children.Sort = "";
						datejoin.Sort = "";
						dateresign.Sort = "";
						marriedstatus.Sort = "";
						Id_dept.Sort = "";
						Id_position.Sort = "";
						Id_race.Sort = "";
						report_to.Sort = "";
						login_effectivedate.Sort = "";
						login_disableddate.Sort = "";
						UserLevelId.Sort = "";
						_Username.Sort = "";
						password.Sort = "";
						active.Sort = "";
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
				isVisible = Security.CanView && ShowOptionLink("view");
				if (isVisible) {
					listOption.Body = "<a class=\"ew-row-link ew-view\" title=\"" + viewcaption + "\" data-caption=\"" + viewcaption + "\" href=\"" + HtmlEncode(AppPath(ViewUrl)) + "\">" + Language.Phrase("ViewLink") + "</a>";
				} else {
					listOption.Body = "";
				}

				// "edit"
				listOption = ListOptions["edit"];
				string editcaption = HtmlTitle(Language.Phrase("EditLink"));
				isVisible = Security.CanEdit && ShowOptionLink("edit");
				if (isVisible) {
					listOption.Body = "<a class=\"ew-row-link ew-edit\" title=\"" + editcaption + "\" data-caption=\"" + editcaption + "\" href=\"" + HtmlEncode(AppPath(EditUrl)) + "\">" + Language.Phrase("EditLink") + "</a>";
				} else {
					listOption.Body = "";
				}

				// "copy"
				listOption = ListOptions["copy"];
				string copycaption = HtmlTitle(Language.Phrase("CopyLink"));
				isVisible = Security.CanAdd && ShowOptionLink("add");
				if (isVisible) {
					listOption.Body = "<a class=\"ew-row-link ew-copy\" title=\"" + copycaption + "\" data-caption=\"" + copycaption + "\" href=\"" + HtmlEncode(AppPath(CopyUrl)) + "\">" + Language.Phrase("CopyLink") + "</a>";
				} else {
					listOption.Body = "";
				}

				// "delete"
				listOption = ListOptions["delete"];
				isVisible = Security.CanDelete && ShowOptionLink("delete");
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
				item.Body = "<a class=\"ew-save-filter\" data-form=\"fs_employeelistsrch\" href=\"#\">" + Language.Phrase("SaveCurrentFilter") + "</a>";
				item.Visible = true;
				item = FilterOptions.Add("deletefilter");
				item.Body = "<a class=\"ew-delete-filter\" data-form=\"fs_employeelistsrch\" href=\"#\">" + Language.Phrase("DeleteFilter") + "</a>";
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
						item.Body = "<a class=\"ew-action ew-list-action\" title=\"" + HtmlEncode(caption) + "\" data-caption=\"" + HtmlEncode(caption) + "\" href=\"\" onclick=\"ew.submitAction(event,jQuery.extend({f:document.fs_employeelist}," + act.ToJson(true) + ")); return false;\">" + icon + "</a>";
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
				string userlist = "", user = "";
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
							user = Convert.ToString(row[Config.LoginUsernameFieldName]);
							if (userlist != "")
								userlist += ",";
							userlist += user;
							if (userAction == "resendregisteremail") {
								processed = false;
							} else if (userAction == "resetconcurrentuser") {
								processed = false;
							} else if (userAction == "resetloginretry") {
								processed = false;
							} else if (userAction == "setpasswordexpired") {
								processed = false;
							} else {
								processed = Row_CustomAction(userAction, row);
							}
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
				item.Body = "<button type=\"button\" class=\"btn btn-default ew-search-toggle" + searchToggleClass + "\" title=\"" + Language.Phrase("SearchPanel") + "\" data-caption=\"" + Language.Phrase("SearchPanel") + "\" data-toggle=\"button\" data-form=\"fs_employeelistsrch\">" + Language.Phrase("SearchLink") + "</button>";
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
