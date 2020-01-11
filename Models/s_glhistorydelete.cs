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
		/// s_glhistory_Delete
		/// </summary>

		public static _s_glhistory_Delete s_glhistory_Delete {
			get => HttpData.Get<_s_glhistory_Delete>("s_glhistory_Delete");
			set => HttpData["s_glhistory_Delete"] = value;
		}

		/// <summary>
		/// Page class for s_glhistory
		/// </summary>

		public class _s_glhistory_Delete : _s_glhistory_DeleteBase
		{

			// Construtor
			public _s_glhistory_Delete(Controller controller = null) : base(controller) {
			}
		}

		/// <summary>
		/// Page base class
		/// </summary>

		public class _s_glhistory_DeleteBase : _s_glhistory, IAspNetMakerPage
		{

			// Page ID
			public string PageID = "delete";

			// Project ID
			public string ProjectID = "{8543F230-11C6-4105-B51C-8D87C21BE659}";

			// Table name
			public string TableName = "s_glhistory";

			// Page object name
			public string PageObjName = "s_glhistory_Delete";

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
			public _s_glhistory_DeleteBase(Controller controller = null) { // DN
				if (controller != null)
					Controller = controller;

				// Initialize
				CurrentPage = this;

				// Language object
				Language = Language ?? new Lang();

				// Table object (s_glhistory)
				if (s_glhistory == null || s_glhistory is _s_glhistory)
					s_glhistory = this;

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
					dynamic doc = CreateInstance(classname, new object[] { s_glhistory, "" }); // DN
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
			public string DbMasterFilter = "";
			public string DbDetailFilter = "";
			public int StartRecord;
			public int TotalRecords;
			public int RecordCount;
			public List<string> RecordKeys;
			public DbDataReader Recordset;
			public int StartRowCount = 1;

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
					if (!Security.CanDelete) {
						Security.SaveLastUrl();
						FailureMessage = DeniedMessage(); // Set no permission
						if (IsApi())
							return new JsonBoolResult(new { success = false, error = DeniedMessage(), version = Config.ProductVersion }, false);
						if (Security.CanList)
							return Terminate(GetUrl("s_glhistorylist"));
						else
							return Terminate(GetUrl("login"));
					}
				}
				CurrentAction = Param("action"); // Set up current action
				Id.SetVisibility();
				month.SetVisibility();
				acct_code.SetVisibility();
				dept_code.SetVisibility();
				project_code.SetVisibility();
				fileno.SetVisibility();
				mth_bal.SetVisibility();
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
				// Set up Breadcrumb

				SetupBreadcrumb();

				// Load key parameters
				RecordKeys = GetRecordKeys(); // Load record keys
				string filter = GetFilterFromRecordKeys();
				if (Empty(filter))
					return Terminate("s_glhistorylist"); // Prevent SQL injection, return to List page

				// Set up filter (WHERE Clause)
				CurrentFilter = filter;

				// Get action
				if (IsApi()) {
					CurrentAction = "delete"; // Delete record directly
				} else if (!Empty(Post("action"))) {
					CurrentAction = Post("action");
				} else if (Get<bool>("action")) {
					CurrentAction = "delete"; // Delete record directly
				} else {
					CurrentAction = "show"; // Display record
				}
				if (IsDelete) { // DN
					SendEmail = true; // Send email on delete success
					var res = await DeleteRows();
					if (res) { // Delete rows
						if (Empty(SuccessMessage))
							SuccessMessage = Language.Phrase("DeleteSuccess"); // Set up success message
						if (IsApi()) {
							return res;
						} else {
							return Terminate(ReturnUrl); // Return to caller
						}
					} else { // Delete failed
						if (IsApi())
							return Terminate();
						CurrentAction = "show"; // Display record
					}
				}
				if (IsShow) { // Load records for display // DN
					Recordset = await LoadRecordset();
					TotalRecords = await ListRecordCount(); // Get record count
					if (TotalRecords <= 0) { // No record found, exit
						CloseRecordset(); // DN
						return Terminate("s_glhistorylist"); // Return to list
					}
				}
				return PageResult();
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
				month.SetDbValue(row["month"]);
				acct_code.SetDbValue(row["acct_code"]);
				dept_code.SetDbValue(row["dept_code"]);
				project_code.SetDbValue(row["project_code"]);
				fileno.SetDbValue(row["fileno"]);
				mth_bal.SetDbValue(row["mth_bal"]);
			}

			#pragma warning restore 162, 168, 1998

			// Return a row with default values
			protected Dictionary<string, object> NewRow() {
				var row = new Dictionary<string, object>();
				row.Add("Id", System.DBNull.Value);
				row.Add("month", System.DBNull.Value);
				row.Add("acct_code", System.DBNull.Value);
				row.Add("dept_code", System.DBNull.Value);
				row.Add("project_code", System.DBNull.Value);
				row.Add("fileno", System.DBNull.Value);
				row.Add("mth_bal", System.DBNull.Value);
				return row;
			}

			#pragma warning disable 1998

			// Render row values based on field settings
			public async Task RenderRow() {

				// Convert decimal values if posted back
				if (SameString(mth_bal.FormValue, mth_bal.CurrentValue) && IsNumeric(ConvertToFloatString(mth_bal.CurrentValue)))
					mth_bal.CurrentValue = ConvertToFloatString(mth_bal.CurrentValue);

				// Call Row_Rendering event
				Row_Rendering();

				// Common render codes for all row types
				// Id
				// month
				// acct_code
				// dept_code
				// project_code
				// fileno
				// mth_bal

				if (RowType == Config.RowTypeView) { // View row

					// Id
					Id.ViewValue = Id.CurrentValue;

					// month
					month.ViewValue = month.CurrentValue;

					// acct_code
					acct_code.ViewValue = acct_code.CurrentValue;

					// dept_code
					dept_code.ViewValue = dept_code.CurrentValue;

					// project_code
					project_code.ViewValue = project_code.CurrentValue;

					// fileno
					fileno.ViewValue = fileno.CurrentValue;

					// mth_bal
					mth_bal.ViewValue = mth_bal.CurrentValue;
					mth_bal.ViewValue = FormatNumber(mth_bal.ViewValue, 2, -2, -2, -2);

					// Id
					Id.HrefValue = "";
					Id.TooltipValue = "";

					// month
					month.HrefValue = "";
					month.TooltipValue = "";

					// acct_code
					acct_code.HrefValue = "";
					acct_code.TooltipValue = "";

					// dept_code
					dept_code.HrefValue = "";
					dept_code.TooltipValue = "";

					// project_code
					project_code.HrefValue = "";
					project_code.TooltipValue = "";

					// fileno
					fileno.HrefValue = "";
					fileno.TooltipValue = "";

					// mth_bal
					mth_bal.HrefValue = "";
					mth_bal.TooltipValue = "";
				}

				// Call Row Rendered event
				if (RowType != Config.RowTypeAggregateInit)
					Row_Rendered();
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
				s_glhistory = s_glhistory ?? new _s_glhistory();
				Connection.BeginTrans();
				var key = "";
				try {

					// Call Row Deleting event
					if (result)
						result = rsold.All(row => Row_Deleting(row));
					if (result) {
						foreach (var row in rsold) {
							var thisKey = "";
							if (!Empty(thisKey)) thisKey += Config.CompositeKeySeparator;
							thisKey += Convert.ToString(row["Id"]);
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
				if (result) {
					Connection.CommitTrans(); // Commit the changes
				} else {
					Connection.RollbackTrans(); // Rollback changes
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

			// Save data to memory cache
			public void SetCache<T>(string key, T value, int span) => Cache.Set<T>(key, value, new MemoryCacheEntryOptions()
				.SetSlidingExpiration(TimeSpan.FromMilliseconds(span))); // Keep in cache for this time, reset time if accessed

			// Gete data from memory cache
			public void GetCache<T>(string key) => Cache.Get<T>(key);

			// Set up Breadcrumb
			protected void SetupBreadcrumb() {
				var breadcrumb = new Breadcrumb();
				string url = CurrentUrl();
				breadcrumb.Add("list", TableVar, AppPath(AddMasterUrl("s_glhistorylist")), "", TableVar, true);
				string pageId = "delete";
				breadcrumb.Add("delete", pageId, url);
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
