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

		// Configuration
		public static partial class Config {

			// Debug
			private static bool _debug = false;
			public static bool Debug {
				 get => _debug || ConvertToBool(Environment.GetEnvironmentVariable("ASPNETMAKER_DEBUG")); // Set to true for debugging
				 set => _debug = value;
			}

			// Product version
			public const string ProductVersion = "16.0.6";

			// Project
			public const string ProjectNamespace = "AspNetMaker2019";
			public const string ProjectClassName = "AspNetMaker2019.Models.SampleProject"; // DN
			public static string PathDelimiter = Convert.ToString(Path.DirectorySeparatorChar); // Physical path delimiter // DN
			public static short UnformatYear = 50; // Unformat year
			public const string ProjectName = "SampleProject"; // Project name
			public static string AreaName { get; set; } = ""; // Area name // DN
			public static string ControllerName { get; set; } = "Home"; // Controller name // DN
			public const string ConfigFileFolder = ProjectName; // Config file name
			public const string ProjectId = "{8543F230-11C6-4105-B51C-8D87C21BE659}"; // Project ID (GUID)
			public static string RelatedLanguageFolder = "";
			public static string RandomKey = "dLPs4M0KI2xmmshS"; // Random key for encryption
			public static string EncryptionKey = ""; // Encryption key for data protection
			public static string ProjectStylesheetFilename = "css/SampleProject.css"; // Project stylesheet file name (relative to wwwroot)
			public static string Charset = "utf-8"; // Project charset
			public static string EmailCharset = Charset; // Email charset
			public static string EmailKeywordSeparator = ""; // Email keyword separator
			public static string CompositeKeySeparator = ","; // Composite key separator
			public static bool HighlightCompare { get; set; } = true; // Case-insensitive
			public static int FontSize = 14;
			public static string TempImageFont = "Verdana"; // Font for temp files
			public static bool Cache = false; // Cache // DN
			public static bool LazyLoad = true; // Lazy loading of images
			public static string RelatedProjectId = "";
			public static bool ResetHeight = true; // Reset layout height
			public static bool DeleteUploadFiles = false; // Delete uploaded file on deleting record
			public static string BodyClass = "hold-transition";
			public static string SidebarClass = "main-sidebar sidebar-dark-info";
			public static string NavbarClass = "main-header navbar navbar-expand navbar-dark bg-info";

			// External JavaScripts
			public static List<string> JavaScriptFiles = new List<string> {
			};

			// External StyleSheets
			public static List<string> StylesheetFiles = new List<string> {
			};

			// Authentication configuration for Google/Facebook
			public static Dictionary<string, AuthenticationProvider> Authentications = new Dictionary<string, AuthenticationProvider> {
				{"Google", new AuthenticationProvider {
					Enabled = false,
					Id = "",
					Color = "danger",
					Secret = ""
				}},
				{"Facebook", new AuthenticationProvider {
					Enabled = false,
					Id = "",
					Color = "primary",
					Secret = ""
				}}
			}; // DN

			// Database time zone
			// Difference to Greenwich time (GMT) with colon between hours and minutes, e.g. +02:00

			public static string DbTimeZone = "";

			// Password (hashed and case-sensitivity)
			// Note: If you enable hashed password, make sure that the passwords in your
			// user table are stored as hash of the clear text password. If you also use
			// case-insensitive password, convert the clear text passwords to lower case
			// first before calculating hash. Otherwise, existing users will not be able
			// to login. Hashed password is irreversible, it will be reset during password recovery.

			public static bool EncryptedPassword { get; set; } = false; // Encrypted password
			public static bool CaseSensitivePassword { get; set; } = false; // Case Sensitive password

			// Remove XSS use HtmlSanitizer
			// Note: If you want to allow these keywords, remove them from the following array at your own risks.

			public static bool RemoveXss { get; set; } = true;

			// Check Token
			public static bool CheckToken = true; // Check post token by AntiforgeryToken // DN

			// Session timeout time
			public static int SessionTimeout = 20; // Session timeout time (minutes)

			// Session keep alive interval
			public static int SessionKeepAliveInterval = 0; // Session keep alive interval (seconds)
			public static int SessionTimeoutCountdown = 60; // Session timeout count down interval (seconds)

			// Session names
			public const string SessionStatus = ProjectName + "_Status"; // Login status
			public const string SessionUserName = SessionStatus + "_UserName"; // User name
			public const string SessionUserLoginType = SessionStatus + "_UserLoginType"; // User login type
			public const string SessionUserId = SessionStatus + "_UserID"; // User ID
			public const string SessionUserProfile = SessionStatus + "_UserProfile"; // User Profile
			public const string SessionUserProfileUserName = SessionUserProfile + "_UserName";
			public const string SessionUserProfilePassword = SessionUserProfile + "_Password";
			public const string SessionUserProfileLoginType = SessionUserProfile + "_LoginType";
			public const string SessionUserLevelId = SessionStatus + "_UserLevel"; // User level ID
			public const string SessionUserLevelList = SessionStatus + "_UserLevelList"; // User Level List
			public const string SessionUserLevelListLoaded = SessionStatus + "_UserLevelListLoaded"; // User Level List Loaded
			public const string SessionUserLevel = SessionStatus + "_UserLevelValue"; // User level
			public const string SessionParentUserId = SessionStatus + "_ParentUserID"; // Parent user ID
			public const string SessionSysAdmin = ProjectName + "_SysAdmin"; // System admin
			public const string SessionProjectId = ProjectName + "_ProjectID"; // User Level project ID
			public const string SessionUserLevelArrays = ProjectName + "_UserLevelArrays"; // User level List // DN
			public const string SessionUserLevelPrivArrays = ProjectName + "_UserLevelPrivArrays"; // User level privilege List // DN
			public const string SessionUserLevelMessage = ProjectName + "_UserLevelMessage"; // User Level messsage
			public const string SessionMessage = ProjectName + "_Message"; // System message
			public const string SessionFailureMessage = ProjectName + "_Failure_Message"; // System error message
			public const string SessionSuccessMessage = ProjectName + "_Success_Message"; // System message
			public const string SessionWarningMessage = ProjectName + "_Warning_Message"; // Warning message
			public const string SessionInlineMode = ProjectName + "_InlineMode"; // Inline mode
			public const string SessionBreadcrumb = ProjectName + "_Breadcrumb"; // Breadcrumb
			public const string SessionTempImages = ProjectName + "_TempImages"; // Temp images
			public const string SessionDebugMessage = ProjectName + "_DebugMessage"; // Debug message
			public const string SessionLastRefreshTime = ProjectName + "_LastRefreshTime"; // Last refresh time
			public const string SessionExternalLoginInfo = ProjectName + "_ExternalLoginInfo"; // External login info

			// Language settings
			public const string LanguageFolder = "lang/";
			public static List<dynamic> LanguageFile = new List<dynamic> {
				new { Id = "EN", File = "english.xml" }
			};
			public static string LanguageDefaultId = "EN";
			public const string SessionLanguageId = ProjectName + "_LanguageId"; // Language ID
			public const string LocaleFolder = "locale/";

			// Page token
			public const string TokenName = "__RequestVerificationToken"; // DO NOT CHANGE!
			public const string SessionToken = ProjectName + "_Token";

			// Data types
			public const int DataTypeNumber = 1;
			public const int DataTypeDate = 2;
			public const int DataTypeString = 3;
			public const int DataTypeBoolean = 4;
			public const int DataTypeMemo = 5;
			public const int DataTypeBlob = 6;
			public const int DataTypeTime = 7;
			public const int DataTypeGuid = 8;
			public const int DataTypeXml = 9;
			public const int DataTypeOther = 10;
			public static List<int> CustomTemplateDataTypes = new List<int> { DataTypeNumber, DataTypeDate, DataTypeString, DataTypeBoolean, DataTypeTime }; // Data to be passed to Custom Template
			public static int DataStringMaxLength = 512;

			// Row types
			public const short RowTypeHeader = 0; // Row type view
			public const short RowTypeView = 1; // Row type view
			public const short RowTypeAdd = 2; // Row type add
			public const short RowTypeEdit = 3; // Row type edit
			public const short RowTypeSearch = 4; // Row type search
			public const short RowTypeMaster = 5; // Row type master record
			public const short RowTypeAggregateInit = 6; // Row type aggregate init
			public const short RowTypeAggregate = 7; // Row type aggregate

			// List actions
			public const string ActionPostback = "P"; // Post back
			public const string ActionAjax = "A"; // Ajax
			public const string ActionMultiple = "M"; // Multiple records
			public const string ActionSingle = "S"; // Single record

			// Table parameters
			public const string TablePrefix = "||ASPNETReportMaker||"; // For backward compatibility only
			public const string TableRecordsPerPage = "recperpage"; // Records per page
			public const string TableStartRec = "start"; // Start record
			public const string TablePageNumber = "pageno"; // Page number
			public const string TableBasicSearch = "psearch"; // Basic search keyword
			public const string TableBasicSearchType = "psearchtype"; // Basic search type
			public const string TableAdvancedSearch = "advsrch"; // Advanced search
			public const string TableSearchWhere = "searchwhere"; // Search where clause
			public const string TableWhere = "where"; // Table where
			public const string TableWhereList = "where_list"; // Table where (list page)
			public const string TableOrderBy = "orderby"; // Table order by
			public const string TableOrderByList = "orderby_list"; // Table order by (list page)
			public const string TableSort = "sort"; // Table sort
			public const string TableKey = "key"; // Table key
			public const string TableShowMaster = "showmaster"; // Table show master
			public const string TableShowDetail = "showdetail"; // Table show detail
			public const string TableMasterTable = "mastertable"; // Master table
			public const string TableDetailTable = "detailtable"; // Detail table
			public const string TableReturnUrl = "return"; // Return URL
			public const string TableExportReturnUrl = "exportreturn"; // Export return URL
			public const string TableGridAddRowCount = "gridaddcnt"; // Grid add row count

			// Audit Trail
			public static bool AuditTrailToDatabase { get; set; } = false; // Write audit trail to DB
			public const string AuditTrailDbId = "DB"; // Audit trail DBID
			public const string AuditTrailTableName = ""; // Audit trail table name
			public const string AuditTrailTableVar = ""; // Audit trail table var
			public const string AuditTrailFieldNameDateTime = ""; // Audit trail DateTime field name
			public const string AuditTrailFieldNameScript = ""; // Audit trail Script field name
			public const string AuditTrailFieldNameUser = ""; // Audit trail User field name
			public const string AuditTrailFieldNameAction = ""; // Audit trail Action field name
			public const string AuditTrailFieldNameTable = ""; // Audit trail Table field name
			public const string AuditTrailFieldNameField = ""; // Audit trail Field field name
			public const string AuditTrailFieldNameKeyvalue = ""; // Audit trail Key Value field name
			public const string AuditTrailFieldNameOldvalue = ""; // Audit trail Old Value field name
			public const string AuditTrailFieldNameNewvalue = ""; // Audit trail New Value field name

			// Security
			public const bool EncryptionEnabled = false; // Encryption enabled
			public const string AdminUserName = "wilson"; // Administrator user name
			public const string AdminPassword = "123"; // Administrator password
			public static bool UseCustomLogin { get; set; } = true; // Use custom login
			public static bool AllowLoginByUrl { get; set; } = false; // Allow login by URL
			public static bool AllowLoginBySession { get; set; } = false; // Allow login by session variables
			public static bool PasswordHash { get; set; } = false; // Use BCrypt.Net-Next password hashing functions

			// Dynamic User Level settings
			// User level definition table/field names

			public const string UserLevelDbId = "DB";
			public const string UserLevelTable = "[dbo].[UserLevels]";
			public const string UserLevelIdField = "[UserLevelID]";
			public const string UserLevelNameField = "[UserLevelID]";

			// User Level privileges table/field names
			public const string UserLevelPrivDbId = "DB";
			public const string UserLevelPrivTable = "[dbo].[UserLevelPermissions]";
			public const string UserLevelPrivTableNameField = "[TableName]";
			public static int UserLevelPrivTableNameFieldSize = 255;
			public const string UserLevelPrivUserLevelIdField = "[UserLevelID]";
			public const string UserLevelPrivPrivField = "[Permission]";

			// User level constants
			public static bool UserLevelCompat { get; set; } = false; // Use old user level values
			public const int AllowAdd = 1; // Add
			public const int AllowDelete = 2; // Delete
			public const int AllowEdit = 4; // Edit
			public const int AllowList = 8; // List
			public const int AllowReport = 8; // Report
			public const int AllowAdmin = 16; // Admin
			public const int AllowImport = AllowAdmin; // Import
			public const int AllowView = 32; // View (for UserLevelCompat = False)
			public const int AllowSearch = 64; // Search (for UserLevelCompat = False)
			public const int AllowAll = 127; // All (1 + 2 + 4 + 8 + 16 +32 + 64)

			// Hierarchical User ID
			public static bool UserIdIsHierarchical { get; set; } = true; // True to show all level / False to show 1 level

			// Use subquery for master/detail
			public static bool UseSubqueryForMasterUserId { get; set; } = false; // True to use subquery / False to skip
			public static int UserIdAllow = 104;

			// User table/field names
			public const string UserTableName = "s_employee";
			public const string LoginUsernameFieldName = "Username";
			public const string LoginPasswordFieldName = "password";
			public const string UserIdFieldName = "Id";
			public const string ParentUserIdFieldName = "report_to";
			public const string UserLevelFieldName = "UserLevelId";
			public const string UserProfileFieldName = "";
			public const string RegisterActivateFieldName = "";
			public const string UserEmailFieldName = "";

			// User table filters
			public const string UserTableDbId = "DB";
			public const string UserTable = "[dbo].[s_employee]";
			public const string UserNameFilter = "([Username] = '%u')";
			public const string UserIdFilter = "([Id] = %u)";
			public const string UserEmailFilter = "";
			public const string UserActivateFilter = "";

			// User Profile Constants
			public const string UserProfileSessionId = "SessionId";
			public const string UserProfileLastAccessedDateTime = "LastAccessedDateTime";
			public static int UserProfileConcurrentSessionCount = 1; // Maximum sessions allowed
			public static int UserProfileSessionTimeout = 20;
			public const string UserProfileLoginRetryCount = "LoginRetryCount";
			public const string UserProfileLastBadLoginDateTime = "LastBadLoginDateTime";
			public static int UserProfileMaxRetry = 3;
			public static int UserProfileRetryLockout = 20;
			public const string UserProfileLastPasswordChangedDate = "LastPasswordChangedDate";
			public static int UserProfilePasswordExpire = 90;
			public const string UserProfileLanguageId = "LanguageId";
			public const string UserProfileSearchFilters = "SearchFilters";
			public const string SearchFilterOption = "Client";

			// Auto hide pager
			public const bool AutoHidePager = true;
			public static bool AutoHidePageSizeSelector = false;

			// Email
			public static string SmtpServer = "localhost"; // SMTP server
			public static int SmtpServerPort = 25; // SMTP server port
			public static string SmtpSecureOption = "";
			public static string SmtpServerUsername = ""; // SMTP server user name
			public static string SmtpServerPassword = ""; // SMTP server password
			public static string SenderEmail = ""; // Sender email
			public static string RecipientEmail = ""; // Recipient email
			public static int MaxEmailRecipient = 3;
			public static int MaxEmailSentCount = 3;
			public static string ExportEmailCounter = SessionStatus + "_EmailCounter";
			public static string EmailChangePasswordTemplate = "changepwd.html";
			public static string EmailForgotPasswordTemplate = "forgotpwd.html";
			public static string EmailNotifyTemplate = "notify.html";
			public static string EmailRegisterTemplate = "register.html";
			public static string EmailResetPasswordTemplate = "resetpwd.html";
			public static string EmailTemplatePath = "html"; // Template path // DN

			// Remote file
			public static string RemoteFilePattern = @"^((https?\:)?|ftps?\:|s3:)\/\/";

			// File upload
			public static string UploadType = "POST"; // HTTP request method for the file uploads, e.g. "POST", "PUT

			// File handler // DN
			public static string FileUrl = "FileViewer";

			// File upload
			public static string UploadTempPath = ""; // Upload temp path (absolute local physical path)
			public static string UploadTempHrefPath = ""; // Upload temp href path (absolute URL path for download)
			public static bool DownloadViaScript = false; // Download uploaded temp file via ewupload.cs (DN)
			public static string UploadDestPath = "files/"; // Upload destination path
			public static string UploadHrefPath = ""; // Upload file href path (for download)
			public static string UploadTempFolderPrefix = "temp__"; // Upload temp folders prefix
			public static int UploadTempFolderTimeLimit = 1440; // Upload temp folder time limit (minutes)
			public static string UploadThumbnailFolder = "thumbnail"; // Temporary thumbnail folder
			public static int UploadThumbnailWidth = 200; // Temporary thumbnail max width
			public static int UploadThumbnailHeight = 0; // Temporary thumbnail max height
			public static int MaxFileCount = 0; // Max file count
			public static string UploadAllowedFileExtensions = "gif,jpg,jpeg,bmp,png,doc,docx,xls,xlsx,pdf,zip"; // Allowed file extensions
			public static List<string> ImageAllowedFileExtensions = new List<string> { "gif","jpe","jpeg","jpg","png","bmp" }; // Allowed file extensions for images
			public static List<string> DownloadAllowedFileExtensions = new List<string> {"csv","pdf","xls","doc","xlsx","docx"}; // Allowed file extensions for download (non-image)
			public static bool EncryptFilePath = true; // Encrypt file path
			public static int MaxFileSize = 2000000; // Max file size
			public static int ThumbnailDefaultWidth = 0; // Thumbnail default width
			public static int ThumbnailDefaultHeight = 0; // Thumbnail default height
			public static bool UploadConvertAccentedChars { get; set; } = false; // Convert accented chars in upload file name
			public static bool UseColorbox { get; set; } = true; // Use Colorbox
			public static char MultipleUploadSeparator = ','; // Multiple upload separator

			// Image resize
			public static bool ResizeIgnoreAspectRatio { get; set; } = false;
			public static bool ResizeLess { get; set; } = false;

			// API
			public static string ApiUrl = "api/"; // API URL
			public static string ApiActionName = "action"; // API action name
			public static string ApiObjectName = "table"; // API object name
			public static string ApiFieldName = "field"; // API field name
			public static string ApiKeyName = "key"; // API key name
			public static string ApiListAction = "list"; // API list action
			public static string ApiViewAction = "view"; // API view action
			public static string ApiAddAction = "add"; // API add action
			public static string ApiEditAction = "edit"; // API edit action
			public static string ApiDeleteAction = "delete"; // API delete action
			public static string ApiLoginAction = "login"; // API login action
			public static string ApiFileAction = "file"; // API file action
			public static string ApiUploadAction = "upload"; // API upload action
			public static string ApiFileTokenName = "filetoken"; // API upload file token name
			public static string ApiJqueryUploadAction = "jupload"; // API jQuery upload action
			public static string ApiSessionAction = "session"; // API get session action
			public static string ApiLookupAction = "lookup"; // API lookup action
			public static string ApiLoginUsername = "username"; // API login user name
			public static string ApiLoginPassword = "password"; // API login password
			public static string ApiLookupTable = "linkTable"; // API lookup table name
			public static string ApiProgressAction = "progress"; // API progress action
			public static List<string> ApiPageActions = new List<string> { ApiListAction, ApiViewAction, ApiAddAction, ApiEditAction, ApiDeleteAction };

			// Import records
			public static Encoding ImportCsvEncoding = Encoding.UTF8; // Import CSV encoding
			public static CultureInfo ImportCsvCulture = CultureInfo.InvariantCulture; // Import CSV culture
			public static char ImportCsvDelimiter = ','; // Import CSV delimiter character
			public static char ImportCsvTextQualifier = '"'; // Import CSV text qualifier character
			public static string ImportCsvEol = "\r\n"; // Import CSV end of line, default CRLF
			public static string ImportFileExtensions = "csv,xlsx"; // Import file allowed extensions
			public static bool ImportInsertOnly = true; // Import by insert only
			public static bool ImportUseTransaction = false; // Import use transaction

			// Audit trail
			public static string AuditTrailPath = ""; // Audit trail path (relative to wwwroot)

			// Export records
			public static bool ExportAll = true; // Export all records
			public static bool ExportOriginalValue { get; set; } = false; // True to export original value
			public static bool ExportFieldCaption { get; set; } = false; // True to export field caption
			public static bool ExportFieldImage { get; set; } = true; // True to export field image
			public static bool ExportCssStyles { get; set; } = true; // True to export css styles
			public static bool ExportMasterRecord { get; set; } = true; // True to export master record
			public static bool ExportMasterRecordForCsv { get; set; } = false; // True to export master record for CSV
			public static bool ExportDetailRecords { get; set; } = true; // True to export detail records
			public static bool ExportDetailRecordsForCsv { get; set; } = false; // True to export detail records for CSV
			public static string PageBreakHtml = "<p style=\"page-break-after:always;\" />";

			// Export classes
			public static Dictionary<string, string> Export = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase) {
				{"email", "ExportEmail"},
				{"html", "ExportHtml"},
				{"word", "ExportWord"},
				{"excel", "ExportExcel"},
				{"pdf", "ExportPdf"},
				{"csv", "ExportCsv"},
				{"xml", "ExportXml"},
				{"json", "ExportJson"}
			};

			// Export report methods
			public static Dictionary<string, string> ExportReport = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase) {
				{"print", "ExportReportHtml"},
				{"html", "ExportReportHtml"},
				{"word", "ExportReportWord"},
				{"excel", "ExportReportExcel"}
			};

			// Full URL protocols ("http" or "https")
			public static Dictionary<string, string> FullUrlProtocols = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase) {
				{"href", null},
				{"upload", null},
				{"resetpwd", null},
				{"activate", null},
				{"tmpfile", null},
				{"auth", null},
			};

			// Table class names
			public static Dictionary<string, string> TableClassNames = new Dictionary<string, string> {
				{"s_armaster", "_s_armaster"},
				{"s_dodetltest", "_s_dodetltest"},
				{"s_dohdrtest", "_s_dohdrtest"},
				{"s_employee", "_s_employee"},
				{"s_services", "_s_services"},
				{"s_servicetype", "_s_servicetype"},
				{"s_taxmaster", "_s_taxmaster"},
				{"UserLevelPermissions", "_UserLevelPermissions"},
				{"UserLevels", "_UserLevels"},
				{"s_currency", "_s_currency"},
				{"s_argltrx", "_s_argltrx"},
				{"s_artrans", "_s_artrans"},
				{"s_glchart", "_s_glchart"},
				{"s_glhistory", "_s_glhistory"},
			};

			// Boolean html attributes
			public static List<string> BooleanHtmlAttributes = new List<string> { "checked", "compact", "declare", "defer", "disabled", "ismap", "multiple", "nohref", "noresize", "noshade", "nowrap", "readonly", "selected" };

			// Use ILIKE for PostgreSQL
			public static bool UseIlikeForPostgresql { get; set; } = true;

			// Use collation for MySQL
			public static string LikeCollationForMysql = "";

			// Use collation for MsSQL
			public static string LikeCollationForMssql = "";

			// Use collation for MsSQL
			public static string LikeCollationForSqlite = "";

			// Null / Not Null values
			public const string NullValue = "##null##";
			public const string NotNullValue = "##notnull##";

			// Search multi value option
			// 1 - no multi value
			// 2 - AND all multi values
			// 3 - OR all multi values

			public static short SearchMultiValueOption { get; set; } = 3;

			// Quick search
			public static string BasicSearchIgnorePattern = @"[\?,\^\*\(\)\[\]\""]"; // Ignore special characters
			public static bool BasicSearchAnyFields { get; set; } = false; // Search "All keywords" in any selected fields

			// Validate option
			public static bool ClientValidate { get; set; } = true;
			public static bool ServerValidate { get; set; } = false;

			// Blob field byte count for hash value calculation
			public static int BlobFieldByteCount { get; set; } = 256;

			// Auto suggest max entries
			public static int AutoSuggestMaxEntries = 10;

			// Auto suggest for all display fields
			public static bool AutoSuggestForAllFields = false;

			// Auto fill original value
			public static bool AutoFillOriginalValue = false;

			// Lookup filter value separator
			public static char LookupFilterValueSeparator = ',';
			public static bool UseLookupCache = true;
			public static int LookupCacheCount = 100;

			// Page Title Style
			public static string PageTitleStyle = "Breadcrumb";

			// Use responsive layout
			public static bool UseResponsiveLayout { get; set; } = true;

			// Use css-flip
			public static bool CssFlip { get; set; } = false;
			public static List<string> RtlLanguages = new List<string> { "ar", "fa", "he", "iw", "ug", "ur" };

			// Date/Time without seconds
			public static bool DateTimeWithoutSeconds = false;

			// Mulitple selection
			public static string OptionHtmlTemplate = "<span class=\"ew-option\">{value}</span>"; // Note: class="ew-option" must match CSS style in project stylesheet
			public static string OptionSeparator = ", ";

			// Cookies
			public static DateTime CookieExpiryTime = DateTime.Today.AddDays(365);

			// Mime type // DN
			public static string DefaultMimeType = "application/octet-stream";

			// URL
			public static string UndefinedUrl = "#";

			// Export PDF CSS stylesheet (relative to wwwroot)
			public static string PdfStylesheetFilename = "";
		}
	}
}
