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
		/// s_armaster_List
		/// </summary>

		public static _s_armaster_List s_armaster_List {
			get => HttpData.Get<_s_armaster_List>("s_armaster_List");
			set => HttpData["s_armaster_List"] = value;
		}

		/// <summary>
		/// Page class for s_armaster
		/// </summary>

		public class _s_armaster_List : _s_armaster_ListBase
		{

			// Construtor
			public _s_armaster_List(Controller controller = null) : base(controller) {
			}
		}

		/// <summary>
		/// Page base class
		/// </summary>

		public class _s_armaster_ListBase : _s_armaster, IAspNetMakerPage
		{

			// Page ID
			public string PageID = "list";

			// Project ID
			public string ProjectID = "{8543F230-11C6-4105-B51C-8D87C21BE659}";

			// Table name
			public string TableName = "s_armaster";

			// Page object name
			public string PageObjName = "s_armaster_List";

			// Grid form hidden field names
			public string FormName = "fs_armasterlist";
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
			public _s_armaster_ListBase(Controller controller = null) { // DN
				if (controller != null)
					Controller = controller;

				// Initialize
				CurrentPage = this;

				// Language object
				Language = Language ?? new Lang();

				// Table object (s_armaster)
				if (s_armaster == null || s_armaster is _s_armaster)
					s_armaster = this;

				// Initialize URLs
				ExportPrintUrl = PageUrl + "export=print";
				ExportExcelUrl = PageUrl + "export=excel";
				ExportWordUrl = PageUrl + "export=word";
				ExportHtmlUrl = PageUrl + "export=html";
				ExportXmlUrl = PageUrl + "export=xml";
				ExportCsvUrl = PageUrl + "export=csv";
				ExportPdfUrl = PageUrl + "export=pdf";
				AddUrl = "s_armasteradd";
				InlineAddUrl = PageUrl + "action=add";
				GridAddUrl = PageUrl + "action=gridadd";
				GridEditUrl = PageUrl + "action=gridedit";
				MultiDeleteUrl = "s_armasterdelete";
				MultiUpdateUrl = "s_armasterupdate";

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
				FilterOptions = new ListOptions { Tag = "div", TagClassName = "ew-filter-option fs_armasterlistsrch" };

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
					s_armaster.Id.FormValue = arKeyFlds[0];
					if (!IsNumeric(s_armaster.Id.FormValue))
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
				filters.Merge(JObject.Parse(dbcode.AdvancedSearch.ToJson())); // Field dbcode
				filters.Merge(JObject.Parse(name.AdvancedSearch.ToJson())); // Field name
				filters.Merge(JObject.Parse(name2.AdvancedSearch.ToJson())); // Field name2
				filters.Merge(JObject.Parse(short_name.AdvancedSearch.ToJson())); // Field short_name
				filters.Merge(JObject.Parse(add1.AdvancedSearch.ToJson())); // Field add1
				filters.Merge(JObject.Parse(add2.AdvancedSearch.ToJson())); // Field add2
				filters.Merge(JObject.Parse(add3.AdvancedSearch.ToJson())); // Field add3
				filters.Merge(JObject.Parse(add4.AdvancedSearch.ToJson())); // Field add4
				filters.Merge(JObject.Parse(area.AdvancedSearch.ToJson())); // Field area
				filters.Merge(JObject.Parse(postcode.AdvancedSearch.ToJson())); // Field postcode
				filters.Merge(JObject.Parse(state.AdvancedSearch.ToJson())); // Field state
				filters.Merge(JObject.Parse(country.AdvancedSearch.ToJson())); // Field country
				filters.Merge(JObject.Parse(tel1.AdvancedSearch.ToJson())); // Field tel1
				filters.Merge(JObject.Parse(tel2.AdvancedSearch.ToJson())); // Field tel2
				filters.Merge(JObject.Parse(phone1.AdvancedSearch.ToJson())); // Field phone1
				filters.Merge(JObject.Parse(phone2.AdvancedSearch.ToJson())); // Field phone2
				filters.Merge(JObject.Parse(fax.AdvancedSearch.ToJson())); // Field fax
				filters.Merge(JObject.Parse(_email.AdvancedSearch.ToJson())); // Field email
				filters.Merge(JObject.Parse(ptc1.AdvancedSearch.ToJson())); // Field ptc1
				filters.Merge(JObject.Parse(ptc.AdvancedSearch.ToJson())); // Field ptc
				filters.Merge(JObject.Parse(ptc2.AdvancedSearch.ToJson())); // Field ptc2
				filters.Merge(JObject.Parse(ar_grp.AdvancedSearch.ToJson())); // Field ar_grp
				filters.Merge(JObject.Parse(term_code.AdvancedSearch.ToJson())); // Field term_code
				filters.Merge(JObject.Parse(pterm_code.AdvancedSearch.ToJson())); // Field pterm_code
				filters.Merge(JObject.Parse(cr_limit.AdvancedSearch.ToJson())); // Field cr_limit
				filters.Merge(JObject.Parse(CurrencyCode.AdvancedSearch.ToJson())); // Field CurrencyCode
				filters.Merge(JObject.Parse(slsman.AdvancedSearch.ToJson())); // Field slsman
				filters.Merge(JObject.Parse(ytd_sale.AdvancedSearch.ToJson())); // Field ytd_sale
				filters.Merge(JObject.Parse(ytd_cost.AdvancedSearch.ToJson())); // Field ytd_cost
				filters.Merge(JObject.Parse(ytd_disc.AdvancedSearch.ToJson())); // Field ytd_disc
				filters.Merge(JObject.Parse(ctrl_acct.AdvancedSearch.ToJson())); // Field ctrl_acct
				filters.Merge(JObject.Parse(ctrl_dept.AdvancedSearch.ToJson())); // Field ctrl_dept
				filters.Merge(JObject.Parse(acct_code.AdvancedSearch.ToJson())); // Field acct_code
				filters.Merge(JObject.Parse(acct_dept.AdvancedSearch.ToJson())); // Field acct_dept
				filters.Merge(JObject.Parse(skip_stmt.AdvancedSearch.ToJson())); // Field skip_stmt
				filters.Merge(JObject.Parse(stop_sale.AdvancedSearch.ToJson())); // Field stop_sale
				filters.Merge(JObject.Parse(opn_item.AdvancedSearch.ToJson())); // Field opn_item
				filters.Merge(JObject.Parse(status.AdvancedSearch.ToJson())); // Field status
				filters.Merge(JObject.Parse(tax_desc.AdvancedSearch.ToJson())); // Field tax_desc
				filters.Merge(JObject.Parse(stax.AdvancedSearch.ToJson())); // Field stax
				filters.Merge(JObject.Parse(remark.AdvancedSearch.ToJson())); // Field remark
				filters.Merge(JObject.Parse(inv_fmt.AdvancedSearch.ToJson())); // Field inv_fmt
				filters.Merge(JObject.Parse(do_fmt.AdvancedSearch.ToJson())); // Field do_fmt
				filters.Merge(JObject.Parse(Ship_Code.AdvancedSearch.ToJson())); // Field Ship_Code
				filters.Merge(JObject.Parse(custtype.AdvancedSearch.ToJson())); // Field custtype
				filters.Merge(JObject.Parse(Acct_BillAcct.AdvancedSearch.ToJson())); // Field Acct_BillAcct
				filters.Merge(JObject.Parse(bill_flag.AdvancedSearch.ToJson())); // Field bill_flag
				filters.Merge(JObject.Parse(payment_code.AdvancedSearch.ToJson())); // Field payment_code
				filters.Merge(JObject.Parse(stax_pct.AdvancedSearch.ToJson())); // Field stax_pct
				filters.Merge(JObject.Parse(db_part.AdvancedSearch.ToJson())); // Field db_part
				filters.Merge(JObject.Parse(b_code.AdvancedSearch.ToJson())); // Field b_code
				filters.Merge(JObject.Parse(lmw_no.AdvancedSearch.ToJson())); // Field lmw_no
				filters.Merge(JObject.Parse(cs_code.AdvancedSearch.ToJson())); // Field cs_code
				filters.Merge(JObject.Parse(approved.AdvancedSearch.ToJson())); // Field approved
				filters.Merge(JObject.Parse(oversea.AdvancedSearch.ToJson())); // Field oversea
				filters.Merge(JObject.Parse(defa_disc_pct.AdvancedSearch.ToJson())); // Field defa_disc_pct
				filters.Merge(JObject.Parse(sellpriceDOM.AdvancedSearch.ToJson())); // Field sellpriceDOM
				filters.Merge(JObject.Parse(id_upd.AdvancedSearch.ToJson())); // Field id_upd
				filters.Merge(JObject.Parse(dt_upd.AdvancedSearch.ToJson())); // Field dt_upd
				filters.Merge(JObject.Parse(com_regno.AdvancedSearch.ToJson())); // Field com_regno
				filters.Merge(JObject.Parse(s_armaster.BasicSearch.ToJson()));

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

				// Field dbcode
				if (filter.TryGetValue("x_dbcode", out sv)) {
					dbcode.AdvancedSearch.SearchValue = sv;
					dbcode.AdvancedSearch.SearchOperator = filter["z_dbcode"];
					dbcode.AdvancedSearch.SearchCondition = filter["v_dbcode"];
					dbcode.AdvancedSearch.SearchValue2 = filter["y_dbcode"];
					dbcode.AdvancedSearch.SearchOperator2 = filter["w_dbcode"];
					dbcode.AdvancedSearch.Save();
				}

				// Field name
				if (filter.TryGetValue("x_name", out sv)) {
					name.AdvancedSearch.SearchValue = sv;
					name.AdvancedSearch.SearchOperator = filter["z_name"];
					name.AdvancedSearch.SearchCondition = filter["v_name"];
					name.AdvancedSearch.SearchValue2 = filter["y_name"];
					name.AdvancedSearch.SearchOperator2 = filter["w_name"];
					name.AdvancedSearch.Save();
				}

				// Field name2
				if (filter.TryGetValue("x_name2", out sv)) {
					name2.AdvancedSearch.SearchValue = sv;
					name2.AdvancedSearch.SearchOperator = filter["z_name2"];
					name2.AdvancedSearch.SearchCondition = filter["v_name2"];
					name2.AdvancedSearch.SearchValue2 = filter["y_name2"];
					name2.AdvancedSearch.SearchOperator2 = filter["w_name2"];
					name2.AdvancedSearch.Save();
				}

				// Field short_name
				if (filter.TryGetValue("x_short_name", out sv)) {
					short_name.AdvancedSearch.SearchValue = sv;
					short_name.AdvancedSearch.SearchOperator = filter["z_short_name"];
					short_name.AdvancedSearch.SearchCondition = filter["v_short_name"];
					short_name.AdvancedSearch.SearchValue2 = filter["y_short_name"];
					short_name.AdvancedSearch.SearchOperator2 = filter["w_short_name"];
					short_name.AdvancedSearch.Save();
				}

				// Field add1
				if (filter.TryGetValue("x_add1", out sv)) {
					add1.AdvancedSearch.SearchValue = sv;
					add1.AdvancedSearch.SearchOperator = filter["z_add1"];
					add1.AdvancedSearch.SearchCondition = filter["v_add1"];
					add1.AdvancedSearch.SearchValue2 = filter["y_add1"];
					add1.AdvancedSearch.SearchOperator2 = filter["w_add1"];
					add1.AdvancedSearch.Save();
				}

				// Field add2
				if (filter.TryGetValue("x_add2", out sv)) {
					add2.AdvancedSearch.SearchValue = sv;
					add2.AdvancedSearch.SearchOperator = filter["z_add2"];
					add2.AdvancedSearch.SearchCondition = filter["v_add2"];
					add2.AdvancedSearch.SearchValue2 = filter["y_add2"];
					add2.AdvancedSearch.SearchOperator2 = filter["w_add2"];
					add2.AdvancedSearch.Save();
				}

				// Field add3
				if (filter.TryGetValue("x_add3", out sv)) {
					add3.AdvancedSearch.SearchValue = sv;
					add3.AdvancedSearch.SearchOperator = filter["z_add3"];
					add3.AdvancedSearch.SearchCondition = filter["v_add3"];
					add3.AdvancedSearch.SearchValue2 = filter["y_add3"];
					add3.AdvancedSearch.SearchOperator2 = filter["w_add3"];
					add3.AdvancedSearch.Save();
				}

				// Field add4
				if (filter.TryGetValue("x_add4", out sv)) {
					add4.AdvancedSearch.SearchValue = sv;
					add4.AdvancedSearch.SearchOperator = filter["z_add4"];
					add4.AdvancedSearch.SearchCondition = filter["v_add4"];
					add4.AdvancedSearch.SearchValue2 = filter["y_add4"];
					add4.AdvancedSearch.SearchOperator2 = filter["w_add4"];
					add4.AdvancedSearch.Save();
				}

				// Field area
				if (filter.TryGetValue("x_area", out sv)) {
					area.AdvancedSearch.SearchValue = sv;
					area.AdvancedSearch.SearchOperator = filter["z_area"];
					area.AdvancedSearch.SearchCondition = filter["v_area"];
					area.AdvancedSearch.SearchValue2 = filter["y_area"];
					area.AdvancedSearch.SearchOperator2 = filter["w_area"];
					area.AdvancedSearch.Save();
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

				// Field state
				if (filter.TryGetValue("x_state", out sv)) {
					state.AdvancedSearch.SearchValue = sv;
					state.AdvancedSearch.SearchOperator = filter["z_state"];
					state.AdvancedSearch.SearchCondition = filter["v_state"];
					state.AdvancedSearch.SearchValue2 = filter["y_state"];
					state.AdvancedSearch.SearchOperator2 = filter["w_state"];
					state.AdvancedSearch.Save();
				}

				// Field country
				if (filter.TryGetValue("x_country", out sv)) {
					country.AdvancedSearch.SearchValue = sv;
					country.AdvancedSearch.SearchOperator = filter["z_country"];
					country.AdvancedSearch.SearchCondition = filter["v_country"];
					country.AdvancedSearch.SearchValue2 = filter["y_country"];
					country.AdvancedSearch.SearchOperator2 = filter["w_country"];
					country.AdvancedSearch.Save();
				}

				// Field tel1
				if (filter.TryGetValue("x_tel1", out sv)) {
					tel1.AdvancedSearch.SearchValue = sv;
					tel1.AdvancedSearch.SearchOperator = filter["z_tel1"];
					tel1.AdvancedSearch.SearchCondition = filter["v_tel1"];
					tel1.AdvancedSearch.SearchValue2 = filter["y_tel1"];
					tel1.AdvancedSearch.SearchOperator2 = filter["w_tel1"];
					tel1.AdvancedSearch.Save();
				}

				// Field tel2
				if (filter.TryGetValue("x_tel2", out sv)) {
					tel2.AdvancedSearch.SearchValue = sv;
					tel2.AdvancedSearch.SearchOperator = filter["z_tel2"];
					tel2.AdvancedSearch.SearchCondition = filter["v_tel2"];
					tel2.AdvancedSearch.SearchValue2 = filter["y_tel2"];
					tel2.AdvancedSearch.SearchOperator2 = filter["w_tel2"];
					tel2.AdvancedSearch.Save();
				}

				// Field phone1
				if (filter.TryGetValue("x_phone1", out sv)) {
					phone1.AdvancedSearch.SearchValue = sv;
					phone1.AdvancedSearch.SearchOperator = filter["z_phone1"];
					phone1.AdvancedSearch.SearchCondition = filter["v_phone1"];
					phone1.AdvancedSearch.SearchValue2 = filter["y_phone1"];
					phone1.AdvancedSearch.SearchOperator2 = filter["w_phone1"];
					phone1.AdvancedSearch.Save();
				}

				// Field phone2
				if (filter.TryGetValue("x_phone2", out sv)) {
					phone2.AdvancedSearch.SearchValue = sv;
					phone2.AdvancedSearch.SearchOperator = filter["z_phone2"];
					phone2.AdvancedSearch.SearchCondition = filter["v_phone2"];
					phone2.AdvancedSearch.SearchValue2 = filter["y_phone2"];
					phone2.AdvancedSearch.SearchOperator2 = filter["w_phone2"];
					phone2.AdvancedSearch.Save();
				}

				// Field fax
				if (filter.TryGetValue("x_fax", out sv)) {
					fax.AdvancedSearch.SearchValue = sv;
					fax.AdvancedSearch.SearchOperator = filter["z_fax"];
					fax.AdvancedSearch.SearchCondition = filter["v_fax"];
					fax.AdvancedSearch.SearchValue2 = filter["y_fax"];
					fax.AdvancedSearch.SearchOperator2 = filter["w_fax"];
					fax.AdvancedSearch.Save();
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

				// Field ptc1
				if (filter.TryGetValue("x_ptc1", out sv)) {
					ptc1.AdvancedSearch.SearchValue = sv;
					ptc1.AdvancedSearch.SearchOperator = filter["z_ptc1"];
					ptc1.AdvancedSearch.SearchCondition = filter["v_ptc1"];
					ptc1.AdvancedSearch.SearchValue2 = filter["y_ptc1"];
					ptc1.AdvancedSearch.SearchOperator2 = filter["w_ptc1"];
					ptc1.AdvancedSearch.Save();
				}

				// Field ptc
				if (filter.TryGetValue("x_ptc", out sv)) {
					ptc.AdvancedSearch.SearchValue = sv;
					ptc.AdvancedSearch.SearchOperator = filter["z_ptc"];
					ptc.AdvancedSearch.SearchCondition = filter["v_ptc"];
					ptc.AdvancedSearch.SearchValue2 = filter["y_ptc"];
					ptc.AdvancedSearch.SearchOperator2 = filter["w_ptc"];
					ptc.AdvancedSearch.Save();
				}

				// Field ptc2
				if (filter.TryGetValue("x_ptc2", out sv)) {
					ptc2.AdvancedSearch.SearchValue = sv;
					ptc2.AdvancedSearch.SearchOperator = filter["z_ptc2"];
					ptc2.AdvancedSearch.SearchCondition = filter["v_ptc2"];
					ptc2.AdvancedSearch.SearchValue2 = filter["y_ptc2"];
					ptc2.AdvancedSearch.SearchOperator2 = filter["w_ptc2"];
					ptc2.AdvancedSearch.Save();
				}

				// Field ar_grp
				if (filter.TryGetValue("x_ar_grp", out sv)) {
					ar_grp.AdvancedSearch.SearchValue = sv;
					ar_grp.AdvancedSearch.SearchOperator = filter["z_ar_grp"];
					ar_grp.AdvancedSearch.SearchCondition = filter["v_ar_grp"];
					ar_grp.AdvancedSearch.SearchValue2 = filter["y_ar_grp"];
					ar_grp.AdvancedSearch.SearchOperator2 = filter["w_ar_grp"];
					ar_grp.AdvancedSearch.Save();
				}

				// Field term_code
				if (filter.TryGetValue("x_term_code", out sv)) {
					term_code.AdvancedSearch.SearchValue = sv;
					term_code.AdvancedSearch.SearchOperator = filter["z_term_code"];
					term_code.AdvancedSearch.SearchCondition = filter["v_term_code"];
					term_code.AdvancedSearch.SearchValue2 = filter["y_term_code"];
					term_code.AdvancedSearch.SearchOperator2 = filter["w_term_code"];
					term_code.AdvancedSearch.Save();
				}

				// Field pterm_code
				if (filter.TryGetValue("x_pterm_code", out sv)) {
					pterm_code.AdvancedSearch.SearchValue = sv;
					pterm_code.AdvancedSearch.SearchOperator = filter["z_pterm_code"];
					pterm_code.AdvancedSearch.SearchCondition = filter["v_pterm_code"];
					pterm_code.AdvancedSearch.SearchValue2 = filter["y_pterm_code"];
					pterm_code.AdvancedSearch.SearchOperator2 = filter["w_pterm_code"];
					pterm_code.AdvancedSearch.Save();
				}

				// Field cr_limit
				if (filter.TryGetValue("x_cr_limit", out sv)) {
					cr_limit.AdvancedSearch.SearchValue = sv;
					cr_limit.AdvancedSearch.SearchOperator = filter["z_cr_limit"];
					cr_limit.AdvancedSearch.SearchCondition = filter["v_cr_limit"];
					cr_limit.AdvancedSearch.SearchValue2 = filter["y_cr_limit"];
					cr_limit.AdvancedSearch.SearchOperator2 = filter["w_cr_limit"];
					cr_limit.AdvancedSearch.Save();
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

				// Field slsman
				if (filter.TryGetValue("x_slsman", out sv)) {
					slsman.AdvancedSearch.SearchValue = sv;
					slsman.AdvancedSearch.SearchOperator = filter["z_slsman"];
					slsman.AdvancedSearch.SearchCondition = filter["v_slsman"];
					slsman.AdvancedSearch.SearchValue2 = filter["y_slsman"];
					slsman.AdvancedSearch.SearchOperator2 = filter["w_slsman"];
					slsman.AdvancedSearch.Save();
				}

				// Field ytd_sale
				if (filter.TryGetValue("x_ytd_sale", out sv)) {
					ytd_sale.AdvancedSearch.SearchValue = sv;
					ytd_sale.AdvancedSearch.SearchOperator = filter["z_ytd_sale"];
					ytd_sale.AdvancedSearch.SearchCondition = filter["v_ytd_sale"];
					ytd_sale.AdvancedSearch.SearchValue2 = filter["y_ytd_sale"];
					ytd_sale.AdvancedSearch.SearchOperator2 = filter["w_ytd_sale"];
					ytd_sale.AdvancedSearch.Save();
				}

				// Field ytd_cost
				if (filter.TryGetValue("x_ytd_cost", out sv)) {
					ytd_cost.AdvancedSearch.SearchValue = sv;
					ytd_cost.AdvancedSearch.SearchOperator = filter["z_ytd_cost"];
					ytd_cost.AdvancedSearch.SearchCondition = filter["v_ytd_cost"];
					ytd_cost.AdvancedSearch.SearchValue2 = filter["y_ytd_cost"];
					ytd_cost.AdvancedSearch.SearchOperator2 = filter["w_ytd_cost"];
					ytd_cost.AdvancedSearch.Save();
				}

				// Field ytd_disc
				if (filter.TryGetValue("x_ytd_disc", out sv)) {
					ytd_disc.AdvancedSearch.SearchValue = sv;
					ytd_disc.AdvancedSearch.SearchOperator = filter["z_ytd_disc"];
					ytd_disc.AdvancedSearch.SearchCondition = filter["v_ytd_disc"];
					ytd_disc.AdvancedSearch.SearchValue2 = filter["y_ytd_disc"];
					ytd_disc.AdvancedSearch.SearchOperator2 = filter["w_ytd_disc"];
					ytd_disc.AdvancedSearch.Save();
				}

				// Field ctrl_acct
				if (filter.TryGetValue("x_ctrl_acct", out sv)) {
					ctrl_acct.AdvancedSearch.SearchValue = sv;
					ctrl_acct.AdvancedSearch.SearchOperator = filter["z_ctrl_acct"];
					ctrl_acct.AdvancedSearch.SearchCondition = filter["v_ctrl_acct"];
					ctrl_acct.AdvancedSearch.SearchValue2 = filter["y_ctrl_acct"];
					ctrl_acct.AdvancedSearch.SearchOperator2 = filter["w_ctrl_acct"];
					ctrl_acct.AdvancedSearch.Save();
				}

				// Field ctrl_dept
				if (filter.TryGetValue("x_ctrl_dept", out sv)) {
					ctrl_dept.AdvancedSearch.SearchValue = sv;
					ctrl_dept.AdvancedSearch.SearchOperator = filter["z_ctrl_dept"];
					ctrl_dept.AdvancedSearch.SearchCondition = filter["v_ctrl_dept"];
					ctrl_dept.AdvancedSearch.SearchValue2 = filter["y_ctrl_dept"];
					ctrl_dept.AdvancedSearch.SearchOperator2 = filter["w_ctrl_dept"];
					ctrl_dept.AdvancedSearch.Save();
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

				// Field acct_dept
				if (filter.TryGetValue("x_acct_dept", out sv)) {
					acct_dept.AdvancedSearch.SearchValue = sv;
					acct_dept.AdvancedSearch.SearchOperator = filter["z_acct_dept"];
					acct_dept.AdvancedSearch.SearchCondition = filter["v_acct_dept"];
					acct_dept.AdvancedSearch.SearchValue2 = filter["y_acct_dept"];
					acct_dept.AdvancedSearch.SearchOperator2 = filter["w_acct_dept"];
					acct_dept.AdvancedSearch.Save();
				}

				// Field skip_stmt
				if (filter.TryGetValue("x_skip_stmt", out sv)) {
					skip_stmt.AdvancedSearch.SearchValue = sv;
					skip_stmt.AdvancedSearch.SearchOperator = filter["z_skip_stmt"];
					skip_stmt.AdvancedSearch.SearchCondition = filter["v_skip_stmt"];
					skip_stmt.AdvancedSearch.SearchValue2 = filter["y_skip_stmt"];
					skip_stmt.AdvancedSearch.SearchOperator2 = filter["w_skip_stmt"];
					skip_stmt.AdvancedSearch.Save();
				}

				// Field stop_sale
				if (filter.TryGetValue("x_stop_sale", out sv)) {
					stop_sale.AdvancedSearch.SearchValue = sv;
					stop_sale.AdvancedSearch.SearchOperator = filter["z_stop_sale"];
					stop_sale.AdvancedSearch.SearchCondition = filter["v_stop_sale"];
					stop_sale.AdvancedSearch.SearchValue2 = filter["y_stop_sale"];
					stop_sale.AdvancedSearch.SearchOperator2 = filter["w_stop_sale"];
					stop_sale.AdvancedSearch.Save();
				}

				// Field opn_item
				if (filter.TryGetValue("x_opn_item", out sv)) {
					opn_item.AdvancedSearch.SearchValue = sv;
					opn_item.AdvancedSearch.SearchOperator = filter["z_opn_item"];
					opn_item.AdvancedSearch.SearchCondition = filter["v_opn_item"];
					opn_item.AdvancedSearch.SearchValue2 = filter["y_opn_item"];
					opn_item.AdvancedSearch.SearchOperator2 = filter["w_opn_item"];
					opn_item.AdvancedSearch.Save();
				}

				// Field status
				if (filter.TryGetValue("x_status", out sv)) {
					status.AdvancedSearch.SearchValue = sv;
					status.AdvancedSearch.SearchOperator = filter["z_status"];
					status.AdvancedSearch.SearchCondition = filter["v_status"];
					status.AdvancedSearch.SearchValue2 = filter["y_status"];
					status.AdvancedSearch.SearchOperator2 = filter["w_status"];
					status.AdvancedSearch.Save();
				}

				// Field tax_desc
				if (filter.TryGetValue("x_tax_desc", out sv)) {
					tax_desc.AdvancedSearch.SearchValue = sv;
					tax_desc.AdvancedSearch.SearchOperator = filter["z_tax_desc"];
					tax_desc.AdvancedSearch.SearchCondition = filter["v_tax_desc"];
					tax_desc.AdvancedSearch.SearchValue2 = filter["y_tax_desc"];
					tax_desc.AdvancedSearch.SearchOperator2 = filter["w_tax_desc"];
					tax_desc.AdvancedSearch.Save();
				}

				// Field stax
				if (filter.TryGetValue("x_stax", out sv)) {
					stax.AdvancedSearch.SearchValue = sv;
					stax.AdvancedSearch.SearchOperator = filter["z_stax"];
					stax.AdvancedSearch.SearchCondition = filter["v_stax"];
					stax.AdvancedSearch.SearchValue2 = filter["y_stax"];
					stax.AdvancedSearch.SearchOperator2 = filter["w_stax"];
					stax.AdvancedSearch.Save();
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

				// Field inv_fmt
				if (filter.TryGetValue("x_inv_fmt", out sv)) {
					inv_fmt.AdvancedSearch.SearchValue = sv;
					inv_fmt.AdvancedSearch.SearchOperator = filter["z_inv_fmt"];
					inv_fmt.AdvancedSearch.SearchCondition = filter["v_inv_fmt"];
					inv_fmt.AdvancedSearch.SearchValue2 = filter["y_inv_fmt"];
					inv_fmt.AdvancedSearch.SearchOperator2 = filter["w_inv_fmt"];
					inv_fmt.AdvancedSearch.Save();
				}

				// Field do_fmt
				if (filter.TryGetValue("x_do_fmt", out sv)) {
					do_fmt.AdvancedSearch.SearchValue = sv;
					do_fmt.AdvancedSearch.SearchOperator = filter["z_do_fmt"];
					do_fmt.AdvancedSearch.SearchCondition = filter["v_do_fmt"];
					do_fmt.AdvancedSearch.SearchValue2 = filter["y_do_fmt"];
					do_fmt.AdvancedSearch.SearchOperator2 = filter["w_do_fmt"];
					do_fmt.AdvancedSearch.Save();
				}

				// Field Ship_Code
				if (filter.TryGetValue("x_Ship_Code", out sv)) {
					Ship_Code.AdvancedSearch.SearchValue = sv;
					Ship_Code.AdvancedSearch.SearchOperator = filter["z_Ship_Code"];
					Ship_Code.AdvancedSearch.SearchCondition = filter["v_Ship_Code"];
					Ship_Code.AdvancedSearch.SearchValue2 = filter["y_Ship_Code"];
					Ship_Code.AdvancedSearch.SearchOperator2 = filter["w_Ship_Code"];
					Ship_Code.AdvancedSearch.Save();
				}

				// Field custtype
				if (filter.TryGetValue("x_custtype", out sv)) {
					custtype.AdvancedSearch.SearchValue = sv;
					custtype.AdvancedSearch.SearchOperator = filter["z_custtype"];
					custtype.AdvancedSearch.SearchCondition = filter["v_custtype"];
					custtype.AdvancedSearch.SearchValue2 = filter["y_custtype"];
					custtype.AdvancedSearch.SearchOperator2 = filter["w_custtype"];
					custtype.AdvancedSearch.Save();
				}

				// Field Acct_BillAcct
				if (filter.TryGetValue("x_Acct_BillAcct", out sv)) {
					Acct_BillAcct.AdvancedSearch.SearchValue = sv;
					Acct_BillAcct.AdvancedSearch.SearchOperator = filter["z_Acct_BillAcct"];
					Acct_BillAcct.AdvancedSearch.SearchCondition = filter["v_Acct_BillAcct"];
					Acct_BillAcct.AdvancedSearch.SearchValue2 = filter["y_Acct_BillAcct"];
					Acct_BillAcct.AdvancedSearch.SearchOperator2 = filter["w_Acct_BillAcct"];
					Acct_BillAcct.AdvancedSearch.Save();
				}

				// Field bill_flag
				if (filter.TryGetValue("x_bill_flag", out sv)) {
					bill_flag.AdvancedSearch.SearchValue = sv;
					bill_flag.AdvancedSearch.SearchOperator = filter["z_bill_flag"];
					bill_flag.AdvancedSearch.SearchCondition = filter["v_bill_flag"];
					bill_flag.AdvancedSearch.SearchValue2 = filter["y_bill_flag"];
					bill_flag.AdvancedSearch.SearchOperator2 = filter["w_bill_flag"];
					bill_flag.AdvancedSearch.Save();
				}

				// Field payment_code
				if (filter.TryGetValue("x_payment_code", out sv)) {
					payment_code.AdvancedSearch.SearchValue = sv;
					payment_code.AdvancedSearch.SearchOperator = filter["z_payment_code"];
					payment_code.AdvancedSearch.SearchCondition = filter["v_payment_code"];
					payment_code.AdvancedSearch.SearchValue2 = filter["y_payment_code"];
					payment_code.AdvancedSearch.SearchOperator2 = filter["w_payment_code"];
					payment_code.AdvancedSearch.Save();
				}

				// Field stax_pct
				if (filter.TryGetValue("x_stax_pct", out sv)) {
					stax_pct.AdvancedSearch.SearchValue = sv;
					stax_pct.AdvancedSearch.SearchOperator = filter["z_stax_pct"];
					stax_pct.AdvancedSearch.SearchCondition = filter["v_stax_pct"];
					stax_pct.AdvancedSearch.SearchValue2 = filter["y_stax_pct"];
					stax_pct.AdvancedSearch.SearchOperator2 = filter["w_stax_pct"];
					stax_pct.AdvancedSearch.Save();
				}

				// Field db_part
				if (filter.TryGetValue("x_db_part", out sv)) {
					db_part.AdvancedSearch.SearchValue = sv;
					db_part.AdvancedSearch.SearchOperator = filter["z_db_part"];
					db_part.AdvancedSearch.SearchCondition = filter["v_db_part"];
					db_part.AdvancedSearch.SearchValue2 = filter["y_db_part"];
					db_part.AdvancedSearch.SearchOperator2 = filter["w_db_part"];
					db_part.AdvancedSearch.Save();
				}

				// Field b_code
				if (filter.TryGetValue("x_b_code", out sv)) {
					b_code.AdvancedSearch.SearchValue = sv;
					b_code.AdvancedSearch.SearchOperator = filter["z_b_code"];
					b_code.AdvancedSearch.SearchCondition = filter["v_b_code"];
					b_code.AdvancedSearch.SearchValue2 = filter["y_b_code"];
					b_code.AdvancedSearch.SearchOperator2 = filter["w_b_code"];
					b_code.AdvancedSearch.Save();
				}

				// Field lmw_no
				if (filter.TryGetValue("x_lmw_no", out sv)) {
					lmw_no.AdvancedSearch.SearchValue = sv;
					lmw_no.AdvancedSearch.SearchOperator = filter["z_lmw_no"];
					lmw_no.AdvancedSearch.SearchCondition = filter["v_lmw_no"];
					lmw_no.AdvancedSearch.SearchValue2 = filter["y_lmw_no"];
					lmw_no.AdvancedSearch.SearchOperator2 = filter["w_lmw_no"];
					lmw_no.AdvancedSearch.Save();
				}

				// Field cs_code
				if (filter.TryGetValue("x_cs_code", out sv)) {
					cs_code.AdvancedSearch.SearchValue = sv;
					cs_code.AdvancedSearch.SearchOperator = filter["z_cs_code"];
					cs_code.AdvancedSearch.SearchCondition = filter["v_cs_code"];
					cs_code.AdvancedSearch.SearchValue2 = filter["y_cs_code"];
					cs_code.AdvancedSearch.SearchOperator2 = filter["w_cs_code"];
					cs_code.AdvancedSearch.Save();
				}

				// Field approved
				if (filter.TryGetValue("x_approved", out sv)) {
					approved.AdvancedSearch.SearchValue = sv;
					approved.AdvancedSearch.SearchOperator = filter["z_approved"];
					approved.AdvancedSearch.SearchCondition = filter["v_approved"];
					approved.AdvancedSearch.SearchValue2 = filter["y_approved"];
					approved.AdvancedSearch.SearchOperator2 = filter["w_approved"];
					approved.AdvancedSearch.Save();
				}

				// Field oversea
				if (filter.TryGetValue("x_oversea", out sv)) {
					oversea.AdvancedSearch.SearchValue = sv;
					oversea.AdvancedSearch.SearchOperator = filter["z_oversea"];
					oversea.AdvancedSearch.SearchCondition = filter["v_oversea"];
					oversea.AdvancedSearch.SearchValue2 = filter["y_oversea"];
					oversea.AdvancedSearch.SearchOperator2 = filter["w_oversea"];
					oversea.AdvancedSearch.Save();
				}

				// Field defa_disc_pct
				if (filter.TryGetValue("x_defa_disc_pct", out sv)) {
					defa_disc_pct.AdvancedSearch.SearchValue = sv;
					defa_disc_pct.AdvancedSearch.SearchOperator = filter["z_defa_disc_pct"];
					defa_disc_pct.AdvancedSearch.SearchCondition = filter["v_defa_disc_pct"];
					defa_disc_pct.AdvancedSearch.SearchValue2 = filter["y_defa_disc_pct"];
					defa_disc_pct.AdvancedSearch.SearchOperator2 = filter["w_defa_disc_pct"];
					defa_disc_pct.AdvancedSearch.Save();
				}

				// Field sellpriceDOM
				if (filter.TryGetValue("x_sellpriceDOM", out sv)) {
					sellpriceDOM.AdvancedSearch.SearchValue = sv;
					sellpriceDOM.AdvancedSearch.SearchOperator = filter["z_sellpriceDOM"];
					sellpriceDOM.AdvancedSearch.SearchCondition = filter["v_sellpriceDOM"];
					sellpriceDOM.AdvancedSearch.SearchValue2 = filter["y_sellpriceDOM"];
					sellpriceDOM.AdvancedSearch.SearchOperator2 = filter["w_sellpriceDOM"];
					sellpriceDOM.AdvancedSearch.Save();
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

				// Field dt_upd
				if (filter.TryGetValue("x_dt_upd", out sv)) {
					dt_upd.AdvancedSearch.SearchValue = sv;
					dt_upd.AdvancedSearch.SearchOperator = filter["z_dt_upd"];
					dt_upd.AdvancedSearch.SearchCondition = filter["v_dt_upd"];
					dt_upd.AdvancedSearch.SearchValue2 = filter["y_dt_upd"];
					dt_upd.AdvancedSearch.SearchOperator2 = filter["w_dt_upd"];
					dt_upd.AdvancedSearch.Save();
				}

				// Field com_regno
				if (filter.TryGetValue("x_com_regno", out sv)) {
					com_regno.AdvancedSearch.SearchValue = sv;
					com_regno.AdvancedSearch.SearchOperator = filter["z_com_regno"];
					com_regno.AdvancedSearch.SearchCondition = filter["v_com_regno"];
					com_regno.AdvancedSearch.SearchValue2 = filter["y_com_regno"];
					com_regno.AdvancedSearch.SearchOperator2 = filter["w_com_regno"];
					com_regno.AdvancedSearch.Save();
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
				BuildBasicSearchSql(ref where, name, keywords, type);
				BuildBasicSearchSql(ref where, name2, keywords, type);
				BuildBasicSearchSql(ref where, short_name, keywords, type);
				BuildBasicSearchSql(ref where, add1, keywords, type);
				BuildBasicSearchSql(ref where, add2, keywords, type);
				BuildBasicSearchSql(ref where, add3, keywords, type);
				BuildBasicSearchSql(ref where, add4, keywords, type);
				BuildBasicSearchSql(ref where, area, keywords, type);
				BuildBasicSearchSql(ref where, postcode, keywords, type);
				BuildBasicSearchSql(ref where, state, keywords, type);
				BuildBasicSearchSql(ref where, country, keywords, type);
				BuildBasicSearchSql(ref where, tel1, keywords, type);
				BuildBasicSearchSql(ref where, tel2, keywords, type);
				BuildBasicSearchSql(ref where, phone1, keywords, type);
				BuildBasicSearchSql(ref where, phone2, keywords, type);
				BuildBasicSearchSql(ref where, fax, keywords, type);
				BuildBasicSearchSql(ref where, _email, keywords, type);
				BuildBasicSearchSql(ref where, ptc1, keywords, type);
				BuildBasicSearchSql(ref where, ptc, keywords, type);
				BuildBasicSearchSql(ref where, ptc2, keywords, type);
				BuildBasicSearchSql(ref where, ar_grp, keywords, type);
				BuildBasicSearchSql(ref where, term_code, keywords, type);
				BuildBasicSearchSql(ref where, pterm_code, keywords, type);
				BuildBasicSearchSql(ref where, CurrencyCode, keywords, type);
				BuildBasicSearchSql(ref where, slsman, keywords, type);
				BuildBasicSearchSql(ref where, ctrl_acct, keywords, type);
				BuildBasicSearchSql(ref where, ctrl_dept, keywords, type);
				BuildBasicSearchSql(ref where, acct_code, keywords, type);
				BuildBasicSearchSql(ref where, acct_dept, keywords, type);
				BuildBasicSearchSql(ref where, status, keywords, type);
				BuildBasicSearchSql(ref where, tax_desc, keywords, type);
				BuildBasicSearchSql(ref where, stax, keywords, type);
				BuildBasicSearchSql(ref where, remark, keywords, type);
				BuildBasicSearchSql(ref where, inv_fmt, keywords, type);
				BuildBasicSearchSql(ref where, do_fmt, keywords, type);
				BuildBasicSearchSql(ref where, Ship_Code, keywords, type);
				BuildBasicSearchSql(ref where, custtype, keywords, type);
				BuildBasicSearchSql(ref where, Acct_BillAcct, keywords, type);
				BuildBasicSearchSql(ref where, bill_flag, keywords, type);
				BuildBasicSearchSql(ref where, payment_code, keywords, type);
				BuildBasicSearchSql(ref where, db_part, keywords, type);
				BuildBasicSearchSql(ref where, b_code, keywords, type);
				BuildBasicSearchSql(ref where, lmw_no, keywords, type);
				BuildBasicSearchSql(ref where, cs_code, keywords, type);
				BuildBasicSearchSql(ref where, id_upd, keywords, type);
				BuildBasicSearchSql(ref where, com_regno, keywords, type);
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
					UpdateSort(dbcode); // dbcode
					UpdateSort(name); // name
					UpdateSort(name2); // name2
					UpdateSort(short_name); // short_name
					UpdateSort(add1); // add1
					UpdateSort(add2); // add2
					UpdateSort(add3); // add3
					UpdateSort(add4); // add4
					UpdateSort(area); // area
					UpdateSort(postcode); // postcode
					UpdateSort(state); // state
					UpdateSort(country); // country
					UpdateSort(tel1); // tel1
					UpdateSort(tel2); // tel2
					UpdateSort(phone1); // phone1
					UpdateSort(phone2); // phone2
					UpdateSort(fax); // fax
					UpdateSort(_email); // _email
					UpdateSort(ptc1); // ptc1
					UpdateSort(ptc); // ptc
					UpdateSort(ptc2); // ptc2
					UpdateSort(ar_grp); // ar_grp
					UpdateSort(term_code); // term_code
					UpdateSort(pterm_code); // pterm_code
					UpdateSort(cr_limit); // cr_limit
					UpdateSort(CurrencyCode); // CurrencyCode
					UpdateSort(slsman); // slsman
					UpdateSort(ytd_sale); // ytd_sale
					UpdateSort(ytd_cost); // ytd_cost
					UpdateSort(ytd_disc); // ytd_disc
					UpdateSort(ctrl_acct); // ctrl_acct
					UpdateSort(ctrl_dept); // ctrl_dept
					UpdateSort(acct_code); // acct_code
					UpdateSort(acct_dept); // acct_dept
					UpdateSort(skip_stmt); // skip_stmt
					UpdateSort(stop_sale); // stop_sale
					UpdateSort(opn_item); // opn_item
					UpdateSort(status); // status
					UpdateSort(tax_desc); // tax_desc
					UpdateSort(stax); // stax
					UpdateSort(remark); // remark
					UpdateSort(inv_fmt); // inv_fmt
					UpdateSort(do_fmt); // do_fmt
					UpdateSort(Ship_Code); // Ship_Code
					UpdateSort(custtype); // custtype
					UpdateSort(Acct_BillAcct); // Acct_BillAcct
					UpdateSort(bill_flag); // bill_flag
					UpdateSort(payment_code); // payment_code
					UpdateSort(stax_pct); // stax_pct
					UpdateSort(db_part); // db_part
					UpdateSort(b_code); // b_code
					UpdateSort(lmw_no); // lmw_no
					UpdateSort(cs_code); // cs_code
					UpdateSort(approved); // approved
					UpdateSort(oversea); // oversea
					UpdateSort(defa_disc_pct); // defa_disc_pct
					UpdateSort(sellpriceDOM); // sellpriceDOM
					UpdateSort(id_upd); // id_upd
					UpdateSort(dt_upd); // dt_upd
					UpdateSort(com_regno); // com_regno
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
						dbcode.Sort = "";
						name.Sort = "";
						name2.Sort = "";
						short_name.Sort = "";
						add1.Sort = "";
						add2.Sort = "";
						add3.Sort = "";
						add4.Sort = "";
						area.Sort = "";
						postcode.Sort = "";
						state.Sort = "";
						country.Sort = "";
						tel1.Sort = "";
						tel2.Sort = "";
						phone1.Sort = "";
						phone2.Sort = "";
						fax.Sort = "";
						_email.Sort = "";
						ptc1.Sort = "";
						ptc.Sort = "";
						ptc2.Sort = "";
						ar_grp.Sort = "";
						term_code.Sort = "";
						pterm_code.Sort = "";
						cr_limit.Sort = "";
						CurrencyCode.Sort = "";
						slsman.Sort = "";
						ytd_sale.Sort = "";
						ytd_cost.Sort = "";
						ytd_disc.Sort = "";
						ctrl_acct.Sort = "";
						ctrl_dept.Sort = "";
						acct_code.Sort = "";
						acct_dept.Sort = "";
						skip_stmt.Sort = "";
						stop_sale.Sort = "";
						opn_item.Sort = "";
						status.Sort = "";
						tax_desc.Sort = "";
						stax.Sort = "";
						remark.Sort = "";
						inv_fmt.Sort = "";
						do_fmt.Sort = "";
						Ship_Code.Sort = "";
						custtype.Sort = "";
						Acct_BillAcct.Sort = "";
						bill_flag.Sort = "";
						payment_code.Sort = "";
						stax_pct.Sort = "";
						db_part.Sort = "";
						b_code.Sort = "";
						lmw_no.Sort = "";
						cs_code.Sort = "";
						approved.Sort = "";
						oversea.Sort = "";
						defa_disc_pct.Sort = "";
						sellpriceDOM.Sort = "";
						id_upd.Sort = "";
						dt_upd.Sort = "";
						com_regno.Sort = "";
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
				item.Body = "<a class=\"ew-save-filter\" data-form=\"fs_armasterlistsrch\" href=\"#\">" + Language.Phrase("SaveCurrentFilter") + "</a>";
				item.Visible = true;
				item = FilterOptions.Add("deletefilter");
				item.Body = "<a class=\"ew-delete-filter\" data-form=\"fs_armasterlistsrch\" href=\"#\">" + Language.Phrase("DeleteFilter") + "</a>";
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
						item.Body = "<a class=\"ew-action ew-list-action\" title=\"" + HtmlEncode(caption) + "\" data-caption=\"" + HtmlEncode(caption) + "\" href=\"\" onclick=\"ew.submitAction(event,jQuery.extend({f:document.fs_armasterlist}," + act.ToJson(true) + ")); return false;\">" + icon + "</a>";
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
				item.Body = "<button type=\"button\" class=\"btn btn-default ew-search-toggle" + searchToggleClass + "\" title=\"" + Language.Phrase("SearchPanel") + "\" data-caption=\"" + Language.Phrase("SearchPanel") + "\" data-toggle=\"button\" data-form=\"fs_armasterlistsrch\">" + Language.Phrase("SearchLink") + "</button>";
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
