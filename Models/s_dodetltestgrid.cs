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
		/// s_dodetltest_Grid
		/// </summary>

		public static _s_dodetltest_Grid s_dodetltest_Grid {
			get => HttpData.Get<_s_dodetltest_Grid>("s_dodetltest_Grid");
			set => HttpData["s_dodetltest_Grid"] = value;
		}

		/// <summary>
		/// Page class for s_dodetltest
		/// </summary>

		public class _s_dodetltest_Grid : _s_dodetltest_GridBase
		{

			// Construtor
			public _s_dodetltest_Grid(Controller controller = null) : base(controller) {
			}
		}

		/// <summary>
		/// Page base class
		/// </summary>

		public class _s_dodetltest_GridBase : _s_dodetltest, IAspNetMakerPage
		{

			// Page ID
			public string PageID = "grid";

			// Project ID
			public string ProjectID = "{8543F230-11C6-4105-B51C-8D87C21BE659}";

			// Table name
			public string TableName = "s_dodetltest";

			// Page object name
			public string PageObjName = "s_dodetltest_Grid";

			// Grid form hidden field names
			public string FormName = "fs_dodetltestgrid";
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
			public _s_dodetltest_GridBase(Controller controller = null) { // DN
				if (controller != null)
					Controller = controller;

				// Initialize
				FormActionName += "_" + FormName;
				FormKeyName += "_" + FormName;
				FormOldKeyName += "_" + FormName;
				FormBlankRowName += "_" + FormName;
				FormKeyCountName += "_" + FormName;

				// Language object
				Language = Language ?? new Lang();

				// Table object (s_dodetltest)
				if (s_dodetltest == null || s_dodetltest is _s_dodetltest)
					s_dodetltest = this;
				AddUrl = "s_dodetltestadd";

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

				// Other options
				OtherOptions["addedit"] = new ListOptions { Tag = "div", TagClassName = "ew-add-edit-option" };
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
				if (Empty(url))
					return null;
				if (!IsApi())
					Page_Redirecting(ref url);

				// Close connection
				CloseConnections(false);

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
				key += UrlEncode(Convert.ToString(ar["TrxId"]));
				return key;
			}

			// Hide fields for Add/Edit
			protected void HideFieldsForAddEdit() {
				if (IsAdd || IsCopy || IsGridAdd)
					TrxId.Visible = false;
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

			#pragma warning disable 169

			public bool ShowOtherOptions = false;
			private DatabaseConnectionBase<SqlConnection, SqlCommand, SqlDataReader, SqlDbType> _connection;

			#pragma warning restore 169

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

				// Create form object
				CurrentForm = new HttpForm();

				// Get grid add count
				int gridaddcnt = Get<int>(Config.TableGridAddRowCount);
				if (gridaddcnt > 0)
					GridAddRowCount = gridaddcnt;

				// Set up list options
				await SetupListOptions();
				TrxId.Visible = false;
				Id_dohdrTrxId.Visible = false;
				item_no.SetVisibility();
				item_type.SetVisibility();
				Id_sodetlTrxId.Visible = false;
				Id_podetlTrxId.Visible = false;
				part_code.SetVisibility();
				part_desc.SetVisibility();
				uom.SetVisibility();
				qty.SetVisibility();
				unit_price.SetVisibility();
				amount_origin.SetVisibility();
				amount_local.Visible = false;
				tax_code.SetVisibility();
				tax_rate.SetVisibility();
				tax_amount_origin.SetVisibility();
				tax_amount_local.Visible = false;
				TrxUserId.Visible = false;
				to_gl_acct.SetVisibility();
				tax_acct.SetVisibility();
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

				// Set up master detail parameters
				SetupMasterParms();

				// Setup other options
				SetupOtherOptions();

				// Set up lookup cache
				await SetupLookupOptions(part_code);

				// Search filters
				string filter = "";

				// Get command
				Command = Get("cmd").ToLower();
				if (IsPageRequest) { // Validate request

					// Handle reset command
					ResetCommand();

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

					// Show grid delete link for grid add / grid edit
					if (AllowAddDeleteRow) {
						if (IsGridAdd || IsGridEdit) {
							var item = ListOptions["griddelete"];
							if (item != null)
								item.Visible = true;
						}
					}

					// Set up sorting order
					SetupSortOrder();
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

				// Build filter
				filter = "";
				if (!Security.CanList)
					filter = "(0=1)"; // Filter all records

				// Restore master/detail filter
				DbMasterFilter = MasterFilter; // Restore master filter
				DbDetailFilter = DetailFilter; // Restore detail filter
				AddFilter(ref filter, DbDetailFilter);
				AddFilter(ref filter, SearchWhere);

				// Load master record
				if (CurrentMode != "add" && !Empty(MasterFilter) && CurrentMasterTable == "s_dohdrtest") {
					using (var rsmaster = await s_dohdrtest.LoadRs(DbMasterFilter)) {
						MasterRecordExists = (rsmaster != null && await rsmaster.ReadAsync());
						if (!MasterRecordExists) {
							FailureMessage = Language.Phrase("NoRecord"); // Set no record found
							return Terminate("s_dohdrtestlist"); // Return to master page
						} else {
							s_dohdrtest.LoadListRowValues(rsmaster);
						}
					}
					s_dohdrtest.RowType = Config.RowTypeMaster; // Master row
					await s_dohdrtest.RenderListRow(); // Note: Do it outside "using" // DN
				}

				// Set up filter
				if (Command == "json") {
					UseSessionForListSql = false; // Do not use session for ListSql
					CurrentFilter = filter;
				} else {
					SessionWhere = filter;
					CurrentFilter = "";
				}
				if (IsGridAdd) {
					if (CurrentMode == "copy") {
						TotalRecords = await ListRecordCount();
						Recordset = await LoadRecordset(StartRecord - 1, TotalRecords);
						StartRecord = 1;
						DisplayRecords = TotalRecords;
					} else {
						CurrentFilter = "0=1";
						StartRecord = 1;
						DisplayRecords = GridAddRowCount;
					}
					TotalRecords = DisplayRecords;
					StopRecord = DisplayRecords;
				} else {
					TotalRecords = await ListRecordCount();
					StopRecord = DisplayRecords;
					StartRecord = 1;
				DisplayRecords = TotalRecords; // Display all records

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

				// Normal return
				if (IsApi()) {
					if (!Connection.SelectOffset) // DN
						for (var i = 1; i <= StartRecord - 1; i++) // Move to first record
							await Recordset.ReadAsync();
					using (Recordset) {
						return Controller.Json(new Dictionary<string, object> { {"success", true}, {TableVar, await GetRecordsFromRecordset(Recordset)}, {"totalRecordCount", TotalRecords}, {"version", Config.ProductVersion} });
					}
				}
				return null;
			}

			// Exit inline mode
			protected void ClearInlineMode() {
				LastAction = CurrentAction; // Save last action
				CurrentAction = ""; // Clear action
				Session[Config.SessionInlineMode] = ""; // Clear inline mode
			}

			// Switch to Grid Add mode
			protected void GridAddMode() {
				CurrentAction = "gridadd";
				Session[Config.SessionInlineMode] = "gridadd"; // Enabled grid add
				HideFieldsForAddEdit();
			}

			// Switch to Grid Edit mode
			protected void GridEditMode() {
				CurrentAction = "gridedit";
				Session[Config.SessionInlineMode] = "gridedit"; // Enabled grid edit
				HideFieldsForAddEdit();
			}

			// Perform update to grid
			public async Task<bool> GridUpdate() {
				bool gridUpdate = true;

				// Get old recordset
				CurrentFilter = BuildKeyFilter();
				if (Empty(CurrentFilter))
					CurrentFilter = "0=1";
				string sql = CurrentSql;
				List<Dictionary<string, object>> rsold = await Connection.GetRowsAsync(sql);

				// Call Grid Updating event
				if (!Grid_Updating(rsold)) {
					if (Empty(FailureMessage))
						FailureMessage = Language.Phrase("GridEditCancelled"); // Set grid edit cancelled message
					return false;
				}
				string key = "";

				// Update row index and get row key
				CurrentForm.Index = -1;
				int rowcnt = CurrentForm.GetInt(FormKeyCountName);
				if (!IsNumeric(rowcnt))
					rowcnt = 0;

				// Update all rows based on key
				try {
					for (int rowindex = 1; rowindex <= rowcnt; rowindex++) {
						CurrentForm.Index = rowindex;
						string rowkey = CurrentForm.GetValue(FormKeyName);
						string rowaction = CurrentForm.GetValue(FormActionName);

						// Load all values and keys
						if (rowaction != "insertdelete") { // Skip insert then deleted rows
							await LoadFormValues(); // Get form values
							if (Empty(rowaction) || rowaction == "edit" || rowaction == "delete") {
								gridUpdate = SetupKeyValues(rowkey); // Set up key values
							} else {
								gridUpdate = true;
							}

							// Skip empty row
							if (rowaction == "insert" && EmptyRow()) {

								// No action required
							// Validate form and insert/update/delete record

							} else if (gridUpdate) {
								if (rowaction == "delete") {
									CurrentFilter = GetRecordFilter();
									gridUpdate = await DeleteRows(); // Delete this row
								} else if (!await ValidateForm()) {
									gridUpdate = false; // Form error, reset action
									FailureMessage = FormError;
								} else {
									if (rowaction == "insert") {
										gridUpdate = await AddRow(); // Insert this row
									} else {
										if (!Empty(rowkey)) {
											SendEmail = false; // Do not send email on update success
											gridUpdate = await EditRow(); // Update this row
										}
									} // End update
								}
							}
							if (gridUpdate) {
								if (!Empty(key))
									key += ", ";
								key += rowkey;
							} else {
								break;
							}
						}
					}
				} catch (Exception e) {
					FailureMessage = e.Message;
					gridUpdate = false;
				}
				if (gridUpdate) {

					// Get new recordset
					List<Dictionary<string, object>> rsnew = await Connection.GetRowsAsync(sql, true); // Use main connection (faster) // DN

					// Call Grid_Updated event
					Grid_Updated(rsold, rsnew);
					ClearInlineMode(); // Clear inline edit mode
				} else {
					if (Empty(FailureMessage))
						FailureMessage = Language.Phrase("UpdateFailed"); // Set update failed message
				}
				return gridUpdate;
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
					s_dodetltest.TrxId.FormValue = arKeyFlds[0];
					if (!IsNumeric(s_dodetltest.TrxId.FormValue))
						return false;
				}
				return true;
			}

			// Perform Grid Add

			#pragma warning disable 168, 219

			public async Task<bool> GridInsert() {
				int addcnt = 0;
				bool gridInsert = false;

				// Call Grid Inserting event
				if (!Grid_Inserting()) {
					if (Empty(FailureMessage))
						FailureMessage = Language.Phrase("GridAddCancelled"); // Set grid add cancelled message
					return false;
				}

				// Init key filter
				string wrkFilter = "";
				string key = "";

				// Get row count
				CurrentForm.Index = -1;
				int rowcnt = CurrentForm.GetInt(FormKeyCountName);

				// Insert all rows
				try {
					for (int rowindex = 1; rowindex <= rowcnt; rowindex++) {

						// Load current row values
						CurrentForm.Index = rowindex;
						string rowaction = CurrentForm.GetValue(FormActionName);
						if (!Empty(rowaction) && rowaction != "insert")
							continue; // Skip
						if (rowaction == "insert") {
							RowOldKey = CurrentForm.GetValue(FormOldKeyName);
							await LoadOldRecord(); // Load old record
						}
						await LoadFormValues(); // Get form values
						if (!EmptyRow()) {
							addcnt++;
							SendEmail = false; // Do not send email on insert success

							// Validate form
							if (!await ValidateForm()) {
								gridInsert = false; // Form error, reset action
								FailureMessage = FormError;
							} else {
								gridInsert = await AddRow(Connection.GetRow(OldRecordset)); // Insert this row
							}
							if (gridInsert) {
								if (!Empty(key))
									key += Config.CompositeKeySeparator;
								key += TrxId.CurrentValue;

								// Add filter for this record
								string filter = GetRecordFilter();
								if (!Empty(wrkFilter))
									wrkFilter += " OR ";
								wrkFilter += filter;
							} else {
								break;
							}
						}
					}
					if (addcnt == 0) { // No record inserted
						ClearInlineMode(); // Clear grid add mode and return
						return true;
					}
				} catch (Exception e) {
					FailureMessage = e.Message;
					gridInsert = false;
				}
				if (gridInsert) {

					// Get new recordset
					CurrentFilter = wrkFilter;
					string sql = CurrentSql;
					List<Dictionary<string, object>> rsnew = await Connection.GetRowsAsync(sql, true); // Use main connection (faster) // DN

					// Call Grid_Inserted event
					Grid_Inserted(rsnew);
					ClearInlineMode(); // Clear grid add mode
				} else {
					if (Empty(FailureMessage))
						FailureMessage = Language.Phrase("InsertFailed"); // Set insert failed message
				}
				return gridInsert;
			}

			#pragma warning restore 168, 219

			// Check if empty row
			public bool EmptyRow() {
				if (CurrentForm.HasValue("x_item_no") && CurrentForm.HasValue("o_item_no") && !SameString(item_no.CurrentValue, item_no.OldValue))
					return false;
				if (CurrentForm.HasValue("x_item_type") && CurrentForm.HasValue("o_item_type") && !SameString(item_type.CurrentValue, item_type.OldValue))
					return false;
				if (CurrentForm.HasValue("x_part_code") && CurrentForm.HasValue("o_part_code") && !SameString(part_code.CurrentValue, part_code.OldValue))
					return false;
				if (CurrentForm.HasValue("x_part_desc") && CurrentForm.HasValue("o_part_desc") && !SameString(part_desc.CurrentValue, part_desc.OldValue))
					return false;
				if (CurrentForm.HasValue("x_uom") && CurrentForm.HasValue("o_uom") && !SameString(uom.CurrentValue, uom.OldValue))
					return false;
				if (CurrentForm.HasValue("x_qty") && CurrentForm.HasValue("o_qty") && !SameString(qty.CurrentValue, qty.OldValue))
					return false;
				if (CurrentForm.HasValue("x_unit_price") && CurrentForm.HasValue("o_unit_price") && !SameString(unit_price.CurrentValue, unit_price.OldValue))
					return false;
				if (CurrentForm.HasValue("x_amount_origin") && CurrentForm.HasValue("o_amount_origin") && !SameString(amount_origin.CurrentValue, amount_origin.OldValue))
					return false;
				if (CurrentForm.HasValue("x_tax_code") && CurrentForm.HasValue("o_tax_code") && !SameString(tax_code.CurrentValue, tax_code.OldValue))
					return false;
				if (CurrentForm.HasValue("x_tax_rate") && CurrentForm.HasValue("o_tax_rate") && !SameString(tax_rate.CurrentValue, tax_rate.OldValue))
					return false;
				if (CurrentForm.HasValue("x_tax_amount_origin") && CurrentForm.HasValue("o_tax_amount_origin") && !SameString(tax_amount_origin.CurrentValue, tax_amount_origin.OldValue))
					return false;
				if (CurrentForm.HasValue("x_to_gl_acct") && CurrentForm.HasValue("o_to_gl_acct") && !SameString(to_gl_acct.CurrentValue, to_gl_acct.OldValue))
					return false;
				if (CurrentForm.HasValue("x_tax_acct") && CurrentForm.HasValue("o_tax_acct") && !SameString(tax_acct.CurrentValue, tax_acct.OldValue))
					return false;
				return true;
			}

			// Validate grid form
			public async Task<bool> ValidateGridForm() {

				// Get row count
				CurrentForm.Index = -1;
				int rowcnt = CurrentForm.GetInt(FormKeyCountName);

				// Validate all records
				for (int rowindex = 1; rowindex <= rowcnt; rowindex++) {

					// Load current row values
					CurrentForm.Index = rowindex;
					string rowaction = CurrentForm.GetValue(FormActionName);
					if (rowaction != "delete" && rowaction != "insertdelete") {
						await LoadFormValues(); // Get form values
						if (rowaction == "insert" && EmptyRow()) {

							// Ignore
						} else if (!await ValidateForm()) {
							return false;
						}
					}
				}
				return true;
			}

			// Get all form values of the grid
			public List<Dictionary<string, string>> GetGridFormValues() {

				// Get row count
				CurrentForm.Index = -1;
				int rowcnt = CurrentForm.GetInt(FormKeyCountName);
				if (!IsNumeric(rowcnt))
					rowcnt = 0;
				var rows = new List<Dictionary<string, string>>();

				// Loop through all records
				for (int rowindex = 1; rowindex <= rowcnt; rowindex++) {

					// Load current row values
					CurrentForm.Index = rowindex;
					string rowaction = CurrentForm.GetValue(FormActionName);
					if (rowaction != "delete" && rowaction != "insertdelete") {
						LoadFormValues().GetAwaiter().GetResult(); // Load form values (sync)
						if (rowaction == "insert" && EmptyRow()) {

							// Ignore
						} else {
							rows.Add(GetFormValues()); // Return row as array
						}
					}
				}
				return rows; // Return as array of array
			}

			// Restore form values for current row
			public async Task RestoreCurrentRowFormValues(object index) {

				// Get row based on current index
				CurrentForm.Index = ConvertToInt(index);
				await LoadFormValues(); // Load form values
			}

			// Set up sort parameters
			protected void SetupSortOrder() {

				// Check for "order" parameter
				if (!Empty(Get("order"))) {
					CurrentOrder = Get("order");
					CurrentOrderType = Get("ordertype");
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

					// Reset master/detail keys
					if (SameText(Command, "resetall")) {
						CurrentMasterTable = ""; // Clear master table
						DbMasterFilter = "";
						DbDetailFilter = "";
						Id_dohdrTrxId.SessionValue = "";
					}

					// Reset sorting order
					if (SameText(Command, "resetsort")) {
						string orderBy = "";
						SessionOrderBy = orderBy;
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

				// "griddelete"
				if (AllowAddDeleteRow) {
					item = ListOptions.Add("griddelete");
					item.CssClass = "text-nowrap";
					item.OnLeft = false;
					item.Visible = false; // Default hidden
				}

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

				// Drop down button for ListOptions
				ListOptions.UseDropDownButton = false;
				ListOptions.DropDownButtonPhrase = Language.Phrase("ButtonListOptions");
				ListOptions.UseButtonGroup = false;
				if (ListOptions.UseButtonGroup && IsMobile())
					ListOptions.UseDropDownButton = true;
				ListOptions.ButtonClass = ""; // Class for button group
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
				string keyName = "";

				// Set up row action and key
				if (IsNumeric(RowIndex) && CurrentMode != "view") {
					CurrentForm.Index = ConvertToInt(RowIndex);
					var actionName = FormActionName.Replace("k_", "k" + Convert.ToString(RowIndex) + "_");
					var oldKeyName = FormOldKeyName.Replace("k_", "k" + Convert.ToString(RowIndex) + "_");
					keyName = FormKeyName.Replace("k_", "k" + Convert.ToString(RowIndex) + "_");
					var BlankRowName = FormBlankRowName.Replace("k_", "k" + Convert.ToString(RowIndex) + "_");
					if (!Empty(RowAction))
						MultiSelectKey += "<input type=\"hidden\" name=\"" + actionName + "\" id=\"" + actionName + "\" value=\"" + RowAction + "\">";
					if (CurrentForm.HasValue(FormOldKeyName))
						RowOldKey = CurrentForm.GetValue(FormOldKeyName);
					if (!Empty(RowOldKey))
						MultiSelectKey += "<input type=\"hidden\" name=\"" + oldKeyName + "\" id=\"" + oldKeyName + "\" value=\"" + HtmlEncode(RowOldKey) + "\">";
					if (RowAction == "delete") {
						string rowkey = CurrentForm.GetValue(FormKeyName);
						SetupKeyValues(rowkey);
					}
					if (RowAction == "insert" && IsConfirm && EmptyRow())
						MultiSelectKey += "<input type=\"hidden\" name=\"" + BlankRowName + "\" id=\"" + BlankRowName + "\" value=\"1\">";
				}

				// "delete"
				if (AllowAddDeleteRow) {
					if (CurrentMode == "add" || CurrentMode == "copy" || CurrentMode == "edit") {
						var options = ListOptions;
						options.UseButtonGroup = true; // Use button group for grid delete button
						listOption = options["griddelete"];
						if (!Security.CanDelete && IsNumeric(RowIndex) && (RowAction == "" || RowAction == "edit")) { // Do not allow delete existing record
							listOption.Body = "&nbsp;";
						} else {
							listOption.Body = "<a class=\"ew-grid-link ew-grid-delete\" title=\"" + HtmlTitle(Language.Phrase("DeleteLink")) + "\" data-caption=\"" + HtmlTitle(Language.Phrase("DeleteLink")) + "\" onclick=\"return ew.deleteGridRow(this, " + RowIndex + ");\">" + Language.Phrase("DeleteLink") + "</a>";
						}
					}
				}
				if (CurrentMode == "view") { // View mode

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
				} // End View mode
				if (CurrentMode == "edit" && IsNumeric(RowIndex)) {
					MultiSelectKey += "<input type=\"hidden\" name=\"" + keyName + "\" id=\"" + keyName + "\" value=\"" + TrxId.CurrentValue + "\">";
				}
				RenderListOptionsExt();

				// Call ListOptions_Rendered event
				ListOptions_Rendered();
			}

			// Set record key
			public void SetRecordKey(ref string key, DbDataReader dr) {
				if (dr == null)
					return;
				var row = Connection.GetRow(dr); // DN
				key = "";
				if (!Empty(key))
					key += Config.CompositeKeySeparator;
				key += row["TrxId"];
			}

			#pragma warning restore 168, 219, 1998

			// Set up other options
			protected void SetupOtherOptions() {
				ListOptions option;
				ListOption item;
				option = OtherOptions["addedit"];
				option.UseDropDownButton = false;
				option.DropDownButtonPhrase = Language.Phrase("ButtonAddEdit");
				option.UseButtonGroup = true;
				option.ButtonClass = ""; // Class for button group
				item = option.Add(option.GroupOptionName);
				item.Body = "";
				item.Visible = false;

				// Add
				if (CurrentMode == "view") { // Check view mode
					item = option.Add("add");
					string addcaption = HtmlTitle(Language.Phrase("AddLink"));
					AddUrl = GetAddUrl();
					item.Body = "<a class=\"ew-add-edit ew-add\" title=\"" + addcaption + "\" data-caption=\"" + addcaption + "\" href=\"" + HtmlEncode(AppPath(AddUrl)) + "\">" + Language.Phrase("AddLink") + "</a>";
					item.Visible = (AddUrl != "" && Security.CanAdd);
				}
			}

			// Render other options
			public void RenderOtherOptions() {
				ListOptions option;
				ListOption item;
				var options = OtherOptions;
				if ((CurrentMode == "add" || CurrentMode == "copy" || CurrentMode == "edit") && !IsConfirm) { // Check add/copy/edit mode
					if (AllowAddDeleteRow) {
						option = options["addedit"];
						option.UseDropDownButton = false;
						item = option.Add("addblankrow");
						item.Body = "<a class=\"ew-add-edit ew-add-blank-row\" title=\"" + HtmlTitle(Language.Phrase("AddBlankRow")) + "\" data-caption=\"" + HtmlTitle(Language.Phrase("AddBlankRow")) + "\" href=\"javascript:void(0);\" onclick=\"ew.addGridRow(this);\">" + Language.Phrase("AddBlankRow") + "</a>";
						item.Visible = Security.CanAdd;
						ShowOtherOptions = item.Visible;
					}
				}
				if (CurrentMode == "view") { // Check view mode
					option = options["addedit"];
					item = option.GetItem("add");
					if (!Empty(item) && item.Visible)
						ShowOtherOptions = true;
					else
						ShowOtherOptions = false;
				}
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
				Id_dohdrTrxId.CurrentValue = System.DBNull.Value;
				Id_dohdrTrxId.OldValue = Id_dohdrTrxId.CurrentValue;
				item_no.CurrentValue = System.DBNull.Value;
				item_no.OldValue = item_no.CurrentValue;
				item_type.CurrentValue = item_type.GetDefault();
				item_type.OldValue = item_type.CurrentValue;
				Id_sodetlTrxId.CurrentValue = System.DBNull.Value;
				Id_sodetlTrxId.OldValue = Id_sodetlTrxId.CurrentValue;
				Id_podetlTrxId.CurrentValue = System.DBNull.Value;
				Id_podetlTrxId.OldValue = Id_podetlTrxId.CurrentValue;
				part_code.CurrentValue = System.DBNull.Value;
				part_code.OldValue = part_code.CurrentValue;
				part_desc.CurrentValue = System.DBNull.Value;
				part_desc.OldValue = part_desc.CurrentValue;
				uom.CurrentValue = System.DBNull.Value;
				uom.OldValue = uom.CurrentValue;
				qty.CurrentValue = System.DBNull.Value;
				qty.OldValue = qty.CurrentValue;
				unit_price.CurrentValue = System.DBNull.Value;
				unit_price.OldValue = unit_price.CurrentValue;
				amount_origin.CurrentValue = System.DBNull.Value;
				amount_origin.OldValue = amount_origin.CurrentValue;
				amount_local.CurrentValue = System.DBNull.Value;
				amount_local.OldValue = amount_local.CurrentValue;
				tax_code.CurrentValue = System.DBNull.Value;
				tax_code.OldValue = tax_code.CurrentValue;
				tax_rate.CurrentValue = System.DBNull.Value;
				tax_rate.OldValue = tax_rate.CurrentValue;
				tax_amount_origin.CurrentValue = System.DBNull.Value;
				tax_amount_origin.OldValue = tax_amount_origin.CurrentValue;
				tax_amount_local.CurrentValue = System.DBNull.Value;
				tax_amount_local.OldValue = tax_amount_local.CurrentValue;
				TrxUserId.CurrentValue = System.DBNull.Value;
				TrxUserId.OldValue = TrxUserId.CurrentValue;
				to_gl_acct.CurrentValue = System.DBNull.Value;
				to_gl_acct.OldValue = to_gl_acct.CurrentValue;
				tax_acct.CurrentValue = System.DBNull.Value;
				tax_acct.OldValue = tax_acct.CurrentValue;
			}

			#pragma warning disable 1998

			// Load form values
			protected async Task LoadFormValues() {
				CurrentForm.FormName = FormName;
				string val;

				// Check field name 'item_no' first before field var 'x_item_no'
				val = CurrentForm.GetValue("item_no", "x_item_no");
				if (!item_no.IsDetailKey) {
					if (IsApi() && val == null)
						item_no.Visible = false; // Disable update for API request
					else
						item_no.FormValue = val;
				}
				if (CurrentForm.HasValue("o_item_no"))
					item_no.OldValue = CurrentForm.GetValue("o_item_no");

				// Check field name 'item_type' first before field var 'x_item_type'
				val = CurrentForm.GetValue("item_type", "x_item_type");
				if (!item_type.IsDetailKey) {
					if (IsApi() && val == null)
						item_type.Visible = false; // Disable update for API request
					else
						item_type.FormValue = val;
				}
				if (CurrentForm.HasValue("o_item_type"))
					item_type.OldValue = CurrentForm.GetValue("o_item_type");

				// Check field name 'part_code' first before field var 'x_part_code'
				val = CurrentForm.GetValue("part_code", "x_part_code");
				if (!part_code.IsDetailKey) {
					if (IsApi() && val == null)
						part_code.Visible = false; // Disable update for API request
					else
						part_code.FormValue = val;
				}
				if (CurrentForm.HasValue("o_part_code"))
					part_code.OldValue = CurrentForm.GetValue("o_part_code");

				// Check field name 'part_desc' first before field var 'x_part_desc'
				val = CurrentForm.GetValue("part_desc", "x_part_desc");
				if (!part_desc.IsDetailKey) {
					if (IsApi() && val == null)
						part_desc.Visible = false; // Disable update for API request
					else
						part_desc.FormValue = val;
				}
				if (CurrentForm.HasValue("o_part_desc"))
					part_desc.OldValue = CurrentForm.GetValue("o_part_desc");

				// Check field name 'uom' first before field var 'x_uom'
				val = CurrentForm.GetValue("uom", "x_uom");
				if (!uom.IsDetailKey) {
					if (IsApi() && val == null)
						uom.Visible = false; // Disable update for API request
					else
						uom.FormValue = val;
				}
				if (CurrentForm.HasValue("o_uom"))
					uom.OldValue = CurrentForm.GetValue("o_uom");

				// Check field name 'qty' first before field var 'x_qty'
				val = CurrentForm.GetValue("qty", "x_qty");
				if (!qty.IsDetailKey) {
					if (IsApi() && val == null)
						qty.Visible = false; // Disable update for API request
					else
						qty.FormValue = val;
				}
				if (CurrentForm.HasValue("o_qty"))
					qty.OldValue = CurrentForm.GetValue("o_qty");

				// Check field name 'unit_price' first before field var 'x_unit_price'
				val = CurrentForm.GetValue("unit_price", "x_unit_price");
				if (!unit_price.IsDetailKey) {
					if (IsApi() && val == null)
						unit_price.Visible = false; // Disable update for API request
					else
						unit_price.FormValue = val;
				}
				if (CurrentForm.HasValue("o_unit_price"))
					unit_price.OldValue = CurrentForm.GetValue("o_unit_price");

				// Check field name 'amount_origin' first before field var 'x_amount_origin'
				val = CurrentForm.GetValue("amount_origin", "x_amount_origin");
				if (!amount_origin.IsDetailKey) {
					if (IsApi() && val == null)
						amount_origin.Visible = false; // Disable update for API request
					else
						amount_origin.FormValue = val;
				}
				if (CurrentForm.HasValue("o_amount_origin"))
					amount_origin.OldValue = CurrentForm.GetValue("o_amount_origin");

				// Check field name 'tax_code' first before field var 'x_tax_code'
				val = CurrentForm.GetValue("tax_code", "x_tax_code");
				if (!tax_code.IsDetailKey) {
					if (IsApi() && val == null)
						tax_code.Visible = false; // Disable update for API request
					else
						tax_code.FormValue = val;
				}
				if (CurrentForm.HasValue("o_tax_code"))
					tax_code.OldValue = CurrentForm.GetValue("o_tax_code");

				// Check field name 'tax_rate' first before field var 'x_tax_rate'
				val = CurrentForm.GetValue("tax_rate", "x_tax_rate");
				if (!tax_rate.IsDetailKey) {
					if (IsApi() && val == null)
						tax_rate.Visible = false; // Disable update for API request
					else
						tax_rate.FormValue = val;
				}
				if (CurrentForm.HasValue("o_tax_rate"))
					tax_rate.OldValue = CurrentForm.GetValue("o_tax_rate");

				// Check field name 'tax_amount_origin' first before field var 'x_tax_amount_origin'
				val = CurrentForm.GetValue("tax_amount_origin", "x_tax_amount_origin");
				if (!tax_amount_origin.IsDetailKey) {
					if (IsApi() && val == null)
						tax_amount_origin.Visible = false; // Disable update for API request
					else
						tax_amount_origin.FormValue = val;
				}
				if (CurrentForm.HasValue("o_tax_amount_origin"))
					tax_amount_origin.OldValue = CurrentForm.GetValue("o_tax_amount_origin");

				// Check field name 'to_gl_acct' first before field var 'x_to_gl_acct'
				val = CurrentForm.GetValue("to_gl_acct", "x_to_gl_acct");
				if (!to_gl_acct.IsDetailKey) {
					if (IsApi() && val == null)
						to_gl_acct.Visible = false; // Disable update for API request
					else
						to_gl_acct.FormValue = val;
				}
				if (CurrentForm.HasValue("o_to_gl_acct"))
					to_gl_acct.OldValue = CurrentForm.GetValue("o_to_gl_acct");

				// Check field name 'tax_acct' first before field var 'x_tax_acct'
				val = CurrentForm.GetValue("tax_acct", "x_tax_acct");
				if (!tax_acct.IsDetailKey) {
					if (IsApi() && val == null)
						tax_acct.Visible = false; // Disable update for API request
					else
						tax_acct.FormValue = val;
				}
				if (CurrentForm.HasValue("o_tax_acct"))
					tax_acct.OldValue = CurrentForm.GetValue("o_tax_acct");

				// Check field name 'TrxId' first before field var 'x_TrxId'
				val = CurrentForm.GetValue("TrxId", "x_TrxId");
				if (!TrxId.IsDetailKey && !IsGridAdd && !IsAdd)
					TrxId.FormValue = val;
			}

			#pragma warning restore 1998

			// Restore form values
			public void RestoreFormValues() {
				if (!IsGridAdd && !IsAdd)
					TrxId.CurrentValue = TrxId.FormValue;
				item_no.CurrentValue = item_no.FormValue;
				item_type.CurrentValue = item_type.FormValue;
				part_code.CurrentValue = part_code.FormValue;
				part_desc.CurrentValue = part_desc.FormValue;
				uom.CurrentValue = uom.FormValue;
				qty.CurrentValue = qty.FormValue;
				unit_price.CurrentValue = unit_price.FormValue;
				amount_origin.CurrentValue = amount_origin.FormValue;
				tax_code.CurrentValue = tax_code.FormValue;
				tax_rate.CurrentValue = tax_rate.FormValue;
				tax_amount_origin.CurrentValue = tax_amount_origin.FormValue;
				to_gl_acct.CurrentValue = to_gl_acct.FormValue;
				tax_acct.CurrentValue = tax_acct.FormValue;
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
				LoadDefaultValues();
				var row = new Dictionary<string, object>();
				row.Add("TrxId", TrxId.CurrentValue);
				row.Add("Id_dohdrTrxId", Id_dohdrTrxId.CurrentValue);
				row.Add("item_no", item_no.CurrentValue);
				row.Add("item_type", item_type.CurrentValue);
				row.Add("Id_sodetlTrxId", Id_sodetlTrxId.CurrentValue);
				row.Add("Id_podetlTrxId", Id_podetlTrxId.CurrentValue);
				row.Add("part_code", part_code.CurrentValue);
				row.Add("part_desc", part_desc.CurrentValue);
				row.Add("uom", uom.CurrentValue);
				row.Add("qty", qty.CurrentValue);
				row.Add("unit_price", unit_price.CurrentValue);
				row.Add("amount_origin", amount_origin.CurrentValue);
				row.Add("amount_local", amount_local.CurrentValue);
				row.Add("tax_code", tax_code.CurrentValue);
				row.Add("tax_rate", tax_rate.CurrentValue);
				row.Add("tax_amount_origin", tax_amount_origin.CurrentValue);
				row.Add("tax_amount_local", tax_amount_local.CurrentValue);
				row.Add("TrxUserId", TrxUserId.CurrentValue);
				row.Add("to_gl_acct", to_gl_acct.CurrentValue);
				row.Add("tax_acct", tax_acct.CurrentValue);
				return row;
			}

			#pragma warning disable 618, 1998

			// Load old record
			protected async Task<bool> LoadOldRecord(DatabaseConnectionBase<SqlConnection, SqlCommand, SqlDataReader, SqlDbType> cnn = null) {
				bool validKey = true;
				string[] arKeys = { RowOldKey };
				int cnt = arKeys.Length;
				if (cnt >= 1) {
					if (!Empty(arKeys[0]))
						TrxId.CurrentValue = arKeys[0]; // TrxId;
					else
						validKey = false;
				} else {
					validKey = false;
				}

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
				if (SameString(tax_rate.FormValue, tax_rate.CurrentValue) && IsNumeric(ConvertToFloatString(tax_rate.CurrentValue)))
					tax_rate.CurrentValue = ConvertToFloatString(tax_rate.CurrentValue);

				// Convert decimal values if posted back
				if (SameString(tax_amount_origin.FormValue, tax_amount_origin.CurrentValue) && IsNumeric(ConvertToFloatString(tax_amount_origin.CurrentValue)))
					tax_amount_origin.CurrentValue = ConvertToFloatString(tax_amount_origin.CurrentValue);

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

					// item_no
					item_no.HrefValue = "";
					item_no.TooltipValue = "";

					// item_type
					item_type.HrefValue = "";
					item_type.TooltipValue = "";

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

					// tax_code
					tax_code.HrefValue = "";
					tax_code.TooltipValue = "";

					// tax_rate
					tax_rate.HrefValue = "";
					tax_rate.TooltipValue = "";

					// tax_amount_origin
					tax_amount_origin.HrefValue = "";
					tax_amount_origin.TooltipValue = "";

					// to_gl_acct
					to_gl_acct.HrefValue = "";
					to_gl_acct.TooltipValue = "";

					// tax_acct
					tax_acct.HrefValue = "";
					tax_acct.TooltipValue = "";
				} else if (RowType == Config.RowTypeAdd) { // Add row

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
					if (!Empty(qty.EditValue) && IsNumeric(qty.EditValue)) {
						qty.EditValue = FormatNumber(qty.EditValue, -2, -2, -2, -2);
						qty.OldValue = qty.EditValue;
					}

					// unit_price
					unit_price.EditAttrs["class"] = "form-control";
					unit_price.EditValue = unit_price.CurrentValue; // DN
					unit_price.PlaceHolder = RemoveHtml(unit_price.Caption);
					if (!Empty(unit_price.EditValue) && IsNumeric(unit_price.EditValue)) {
						unit_price.EditValue = FormatNumber(unit_price.EditValue, -2, -2, -2, -2);
						unit_price.OldValue = unit_price.EditValue;
					}

					// amount_origin
					amount_origin.EditAttrs["class"] = "form-control";
					amount_origin.EditValue = amount_origin.CurrentValue; // DN
					amount_origin.PlaceHolder = RemoveHtml(amount_origin.Caption);
					if (!Empty(amount_origin.EditValue) && IsNumeric(amount_origin.EditValue)) {
						amount_origin.EditValue = FormatNumber(amount_origin.EditValue, -2, -2, -2, -2);
						amount_origin.OldValue = amount_origin.EditValue;
					}

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
					if (!Empty(tax_rate.EditValue) && IsNumeric(tax_rate.EditValue)) {
						tax_rate.EditValue = FormatNumber(tax_rate.EditValue, -2, -2, -2, -2);
						tax_rate.OldValue = tax_rate.EditValue;
					}

					// tax_amount_origin
					tax_amount_origin.EditAttrs["class"] = "form-control";
					tax_amount_origin.EditValue = tax_amount_origin.CurrentValue; // DN
					tax_amount_origin.PlaceHolder = RemoveHtml(tax_amount_origin.Caption);
					if (!Empty(tax_amount_origin.EditValue) && IsNumeric(tax_amount_origin.EditValue)) {
						tax_amount_origin.EditValue = FormatNumber(tax_amount_origin.EditValue, -2, -2, -2, -2);
						tax_amount_origin.OldValue = tax_amount_origin.EditValue;
					}

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

					// Add refer script
					// item_no

					item_no.HrefValue = "";

					// item_type
					item_type.HrefValue = "";

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

					// tax_code
					tax_code.HrefValue = "";

					// tax_rate
					tax_rate.HrefValue = "";

					// tax_amount_origin
					tax_amount_origin.HrefValue = "";

					// to_gl_acct
					to_gl_acct.HrefValue = "";

					// tax_acct
					tax_acct.HrefValue = "";
				} else if (RowType == Config.RowTypeEdit) { // Edit row

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
					if (!Empty(qty.EditValue) && IsNumeric(qty.EditValue)) {
						qty.EditValue = FormatNumber(qty.EditValue, -2, -2, -2, -2);
						qty.OldValue = qty.EditValue;
					}

					// unit_price
					unit_price.EditAttrs["class"] = "form-control";
					unit_price.EditValue = unit_price.CurrentValue; // DN
					unit_price.PlaceHolder = RemoveHtml(unit_price.Caption);
					if (!Empty(unit_price.EditValue) && IsNumeric(unit_price.EditValue)) {
						unit_price.EditValue = FormatNumber(unit_price.EditValue, -2, -2, -2, -2);
						unit_price.OldValue = unit_price.EditValue;
					}

					// amount_origin
					amount_origin.EditAttrs["class"] = "form-control";
					amount_origin.EditValue = amount_origin.CurrentValue; // DN
					amount_origin.PlaceHolder = RemoveHtml(amount_origin.Caption);
					if (!Empty(amount_origin.EditValue) && IsNumeric(amount_origin.EditValue)) {
						amount_origin.EditValue = FormatNumber(amount_origin.EditValue, -2, -2, -2, -2);
						amount_origin.OldValue = amount_origin.EditValue;
					}

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
					if (!Empty(tax_rate.EditValue) && IsNumeric(tax_rate.EditValue)) {
						tax_rate.EditValue = FormatNumber(tax_rate.EditValue, -2, -2, -2, -2);
						tax_rate.OldValue = tax_rate.EditValue;
					}

					// tax_amount_origin
					tax_amount_origin.EditAttrs["class"] = "form-control";
					tax_amount_origin.EditValue = tax_amount_origin.CurrentValue; // DN
					tax_amount_origin.PlaceHolder = RemoveHtml(tax_amount_origin.Caption);
					if (!Empty(tax_amount_origin.EditValue) && IsNumeric(tax_amount_origin.EditValue)) {
						tax_amount_origin.EditValue = FormatNumber(tax_amount_origin.EditValue, -2, -2, -2, -2);
						tax_amount_origin.OldValue = tax_amount_origin.EditValue;
					}

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
					// item_no

					item_no.HrefValue = "";

					// item_type
					item_type.HrefValue = "";

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

					// tax_code
					tax_code.HrefValue = "";

					// tax_rate
					tax_rate.HrefValue = "";

					// tax_amount_origin
					tax_amount_origin.HrefValue = "";

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
				if (Id_podetlTrxId.Required) {
					if (!Id_podetlTrxId.IsDetailKey && Empty(Id_podetlTrxId.FormValue))
						FormError = AddMessage(FormError, Id_podetlTrxId.RequiredErrorMessage.Replace("%s", Id_podetlTrxId.Caption));
				}
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
				if (TrxUserId.Required) {
					if (!TrxUserId.IsDetailKey && Empty(TrxUserId.FormValue))
						FormError = AddMessage(FormError, TrxUserId.RequiredErrorMessage.Replace("%s", TrxUserId.Caption));
				}
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

			// Delete records (based on current filter)
			protected async Task<JsonBoolResult> DeleteRows() { // DN
				if (!Security.CanDelete) {
					FailureMessage = Language.Phrase("NoDeletePermission"); // No delete permission
					return JsonBoolResult.FalseResult; // No delete permission
				}
				bool result = true;
				List<Dictionary<string, object>> rsold = null;
				try {
					string sql = CurrentSql;
					using (var rs = await Connection.GetDataReaderAsync(sql)) {
						if (rs == null) {
							return JsonBoolResult.FalseResult;
						} else if (!rs.HasRows) {
							FailureMessage = Language.Phrase("NoRecord"); // No record found
							return JsonBoolResult.FalseResult;
						} else { // Clone old rows
							rsold = await Connection.GetRowsAsync(rs);
						}
					}
				} catch (Exception e) {
					if (Config.Debug)
						throw;
					FailureMessage = e.Message;
					return JsonBoolResult.FalseResult;
				}
				s_dodetltest = s_dodetltest ?? new _s_dodetltest();
				var key = "";
				try {

					// Call Row Deleting event
					if (result)
						result = rsold.All(row => Row_Deleting(row));
					if (result) {
						foreach (var row in rsold) {
							var thisKey = "";
							if (!Empty(thisKey)) thisKey += Config.CompositeKeySeparator;
							thisKey += Convert.ToString(row["TrxId"]);
							if (Config.DeleteUploadFiles)
								DeleteUploadedFiles(row);
							try {
								await DeleteAsync(row);
							} catch (Exception e) {
								if (Config.Debug)
									throw;
								FailureMessage = e.Message; // Set up error message
								result = false;
								break;
							}
							if (!Empty(key))
								key += ", ";
							key += thisKey;
						}
					} 
					if (!result) {

						// Set up error message
						if (!Empty(SuccessMessage) || !Empty(FailureMessage)) {

							// Use the message, do nothing
						} else if (!Empty(CancelMessage)) {
							FailureMessage = CancelMessage;
							CancelMessage = "";
						} else {
							FailureMessage = Language.Phrase("DeleteCancelled");
						}
					}
				} catch (Exception e) {
					FailureMessage = e.Message;
					result = false;
				}

				// Call Row Deleted event
				if (result)
					rsold.ForEach(row => Row_Deleted(row));

				// Write JSON for API request (Support single row only)
				var d = new Dictionary<string, object>();
				d.Add("success", result);
				if (IsApi() && result) {
					var row = GetRecordFromRecordset(rsold);
					d.Add(TableVar, row);
					d.Add("version", Config.ProductVersion);
					return new JsonBoolResult(d, true);
				}
				return new JsonBoolResult(d, result);
			}

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

				// item_no
				item_no.SetDbValue(rsnew, item_no.CurrentValue, 0, item_no.ReadOnly);

				// item_type
				item_type.SetDbValue(rsnew, item_type.CurrentValue, "", item_type.ReadOnly);

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

				// tax_code
				tax_code.SetDbValue(rsnew, tax_code.CurrentValue, System.DBNull.Value, tax_code.ReadOnly);

				// tax_rate
				tax_rate.SetDbValue(rsnew, tax_rate.CurrentValue, System.DBNull.Value, tax_rate.ReadOnly);

				// tax_amount_origin
				tax_amount_origin.SetDbValue(rsnew, tax_amount_origin.CurrentValue, System.DBNull.Value, tax_amount_origin.ReadOnly);

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

			// Add record

			#pragma warning disable 168, 219

			protected async Task<JsonBoolResult> AddRow(Dictionary<string, object> rsold = null) { // DN
				bool result = false;
				var rsnew = new Dictionary<string, object>();
				string masterFilter = "";

				// Set up foreign key field value from Session
				if (CurrentMasterTable == "s_dohdrtest") {
					Id_dohdrTrxId.CurrentValue = Id_dohdrTrxId.SessionValue;
				}
				bool validMasterRecord;

				// Load db values from rsold
				LoadDbValues(rsold);
				if (rsold != null) {
				}
				try {

					// item_no
					item_no.SetDbValue(rsnew, item_no.CurrentValue, 0, false);

					// item_type
					item_type.SetDbValue(rsnew, item_type.CurrentValue, "", Empty(item_type.CurrentValue));

					// part_code
					part_code.SetDbValue(rsnew, part_code.CurrentValue, "", false);

					// part_desc
					part_desc.SetDbValue(rsnew, part_desc.CurrentValue, "", false);

					// uom
					uom.SetDbValue(rsnew, uom.CurrentValue, "", false);

					// qty
					qty.SetDbValue(rsnew, qty.CurrentValue, 0, false);

					// unit_price
					unit_price.SetDbValue(rsnew, unit_price.CurrentValue, System.DBNull.Value, false);

					// amount_origin
					amount_origin.SetDbValue(rsnew, amount_origin.CurrentValue, System.DBNull.Value, false);

					// tax_code
					tax_code.SetDbValue(rsnew, tax_code.CurrentValue, System.DBNull.Value, false);

					// tax_rate
					tax_rate.SetDbValue(rsnew, tax_rate.CurrentValue, System.DBNull.Value, false);

					// tax_amount_origin
					tax_amount_origin.SetDbValue(rsnew, tax_amount_origin.CurrentValue, System.DBNull.Value, false);

					// to_gl_acct
					to_gl_acct.SetDbValue(rsnew, to_gl_acct.CurrentValue, System.DBNull.Value, false);

					// tax_acct
					tax_acct.SetDbValue(rsnew, tax_acct.CurrentValue, System.DBNull.Value, false);

					// Id_dohdrTrxId
					if (!Empty(Id_dohdrTrxId.SessionValue)) {
						rsnew["Id_dohdrTrxId"] = Id_dohdrTrxId.SessionValue;
					}
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

			// Set up master/detail based on QueryString
			protected void SetupMasterParms() {

				// Hide foreign keys
				string masterTblVar = CurrentMasterTable;
				if (masterTblVar == "s_dohdrtest") {
					Id_dohdrTrxId.Visible = false;

					//if (s_dohdrtest.EventCancelled) EventCancelled = true;
					if (Get<bool>("mastereventcancelled"))
						EventCancelled = true;
				}
				DbMasterFilter = MasterFilter; // Get master filter
				DbDetailFilter = DetailFilter; // Get detail filter
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
