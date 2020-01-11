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

// Models (Table)
namespace AspNetMaker2019.Models {

	// Partial class
	public partial class SampleProject {

		/// <summary>
		/// s_glchart
		/// </summary>

		public static _s_glchart s_glchart {
			get => HttpData.GetOrCreate<_s_glchart>("s_glchart");
			set => HttpData["s_glchart"] = value;
		}

		/// <summary>
		/// Table class for s_glchart
		/// </summary>

		public class _s_glchart: DbTable {
			public int RowCnt = 0; // DN
			public bool UseSessionForListSql = true;

			// Column CSS classes
			public string LeftColumnClass = "col-sm-2 col-form-label ew-label";
			public string RightColumnClass = "col-sm-10";
			public string OffsetColumnClass = "col-sm-10 offset-sm-2";
			public string TableLeftColumnClass = "w-col-2";
			public readonly DbField<SqlDbType> Id;
			public readonly DbField<SqlDbType> acct_code;
			public readonly DbField<SqlDbType> description;
			public readonly DbField<SqlDbType> report_type;
			public readonly DbField<SqlDbType> acct_group;
			public readonly DbField<SqlDbType> acct_type;
			public readonly DbField<SqlDbType> CurrencyCode;
			public readonly DbField<SqlDbType> opn_debit;
			public readonly DbField<SqlDbType> opn_credit;
			public readonly DbField<SqlDbType> bal_mtd;
			public readonly DbField<SqlDbType> bal_ytd;
			public readonly DbField<SqlDbType> remark;

			// Constructor
			public _s_glchart() {

				// Language object // DN
				Language = Language ?? new Lang();
				TableVar = "s_glchart";
				Name = "s_glchart";
				Type = "TABLE";

				// Update Table
				UpdateTable = "[dbo].[s_glchart]";
				DbId = "DB"; // DN
				ExportAll = true;
				ExportPageBreakCount = 0; // Page break per every n record (PDF only)
				ExportPageOrientation = "portrait"; // Page orientation (PDF only)
				ExportPageSize = "a4"; // Page size (PDF only)
				ExportExcelPageOrientation = ""; // Page orientation (EPPlus only)
				ExportExcelPageSize = ""; // Page size (EPPlus only)
				ExportColumnWidths = new float[] {  }; // Column widths (PDF only) // DN
				DetailAdd = false; // Allow detail add
				DetailEdit = false; // Allow detail edit
				DetailView = false; // Allow detail view
				ShowMultipleDetails = false; // Show multiple details
				GridAddRowCount = 5;
				AllowAddDeleteRow = true; // Allow add/delete row
				UserIdAllowSecurity = 0; // User ID Allow
				BasicSearch = new BasicSearch(TableVar);

				// Id
				Id = new DbField<SqlDbType> {
					TableVar = "s_glchart",
					TableName = "s_glchart",
					FieldVar = "x_Id",
					Name = "Id",
					Expression = "[Id]",
					BasicSearchExpression = "CAST([Id] AS NVARCHAR)",
					Type = 3,
					DbType = SqlDbType.Int,
					DateTimeFormat = -1,
					VirtualExpression = "[Id]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "NO",
					IsAutoIncrement = true, // Autoincrement field
					IsPrimaryKey = true, // Primary key field
					Nullable = false, // NOT NULL field
					Sortable = true, // Allow sort
					DefaultErrorMessage = Language.Phrase("IncorrectInteger"),
					IsUpload = false
				};
				Id.Init(this); // DN
				Fields.Add("Id", Id);

				// acct_code
				acct_code = new DbField<SqlDbType> {
					TableVar = "s_glchart",
					TableName = "s_glchart",
					FieldVar = "x_acct_code",
					Name = "acct_code",
					Expression = "[acct_code]",
					BasicSearchExpression = "[acct_code]",
					Type = 200,
					DbType = SqlDbType.VarChar,
					DateTimeFormat = -1,
					VirtualExpression = "[acct_code]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Nullable = false, // NOT NULL field
					Required = true, // Required field
					Sortable = true, // Allow sort
					IsUpload = false
				};
				acct_code.Init(this); // DN
				Fields.Add("acct_code", acct_code);

				// description
				description = new DbField<SqlDbType> {
					TableVar = "s_glchart",
					TableName = "s_glchart",
					FieldVar = "x_description",
					Name = "description",
					Expression = "[description]",
					BasicSearchExpression = "[description]",
					Type = 202,
					DbType = SqlDbType.NVarChar,
					DateTimeFormat = -1,
					VirtualExpression = "[description]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Nullable = false, // NOT NULL field
					Required = true, // Required field
					Sortable = true, // Allow sort
					IsUpload = false
				};
				description.Init(this); // DN
				Fields.Add("description", description);

				// report_type
				report_type = new DbField<SqlDbType> {
					TableVar = "s_glchart",
					TableName = "s_glchart",
					FieldVar = "x_report_type",
					Name = "report_type",
					Expression = "[report_type]",
					BasicSearchExpression = "[report_type]",
					Type = 129,
					DbType = SqlDbType.Char,
					DateTimeFormat = -1,
					VirtualExpression = "[report_type]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				report_type.Init(this); // DN
				Fields.Add("report_type", report_type);

				// acct_group
				acct_group = new DbField<SqlDbType> {
					TableVar = "s_glchart",
					TableName = "s_glchart",
					FieldVar = "x_acct_group",
					Name = "acct_group",
					Expression = "[acct_group]",
					BasicSearchExpression = "[acct_group]",
					Type = 200,
					DbType = SqlDbType.VarChar,
					DateTimeFormat = -1,
					VirtualExpression = "[acct_group]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				acct_group.Init(this); // DN
				Fields.Add("acct_group", acct_group);

				// acct_type
				acct_type = new DbField<SqlDbType> {
					TableVar = "s_glchart",
					TableName = "s_glchart",
					FieldVar = "x_acct_type",
					Name = "acct_type",
					Expression = "[acct_type]",
					BasicSearchExpression = "[acct_type]",
					Type = 200,
					DbType = SqlDbType.VarChar,
					DateTimeFormat = -1,
					VirtualExpression = "[acct_type]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Nullable = false, // NOT NULL field
					Required = true, // Required field
					Sortable = true, // Allow sort
					IsUpload = false
				};
				acct_type.Init(this); // DN
				Fields.Add("acct_type", acct_type);

				// CurrencyCode
				CurrencyCode = new DbField<SqlDbType> {
					TableVar = "s_glchart",
					TableName = "s_glchart",
					FieldVar = "x_CurrencyCode",
					Name = "CurrencyCode",
					Expression = "[CurrencyCode]",
					BasicSearchExpression = "[CurrencyCode]",
					Type = 200,
					DbType = SqlDbType.VarChar,
					DateTimeFormat = -1,
					VirtualExpression = "[CurrencyCode]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Nullable = false, // NOT NULL field
					Required = true, // Required field
					Sortable = true, // Allow sort
					IsUpload = false
				};
				CurrencyCode.Init(this); // DN
				Fields.Add("CurrencyCode", CurrencyCode);

				// opn_debit
				opn_debit = new DbField<SqlDbType> {
					TableVar = "s_glchart",
					TableName = "s_glchart",
					FieldVar = "x_opn_debit",
					Name = "opn_debit",
					Expression = "[opn_debit]",
					BasicSearchExpression = "CAST([opn_debit] AS NVARCHAR)",
					Type = 131,
					DbType = SqlDbType.Decimal,
					DateTimeFormat = -1,
					VirtualExpression = "[opn_debit]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Sortable = true, // Allow sort
					DefaultErrorMessage = Language.Phrase("IncorrectFloat"),
					IsUpload = false
				};
				opn_debit.Init(this); // DN
				Fields.Add("opn_debit", opn_debit);

				// opn_credit
				opn_credit = new DbField<SqlDbType> {
					TableVar = "s_glchart",
					TableName = "s_glchart",
					FieldVar = "x_opn_credit",
					Name = "opn_credit",
					Expression = "[opn_credit]",
					BasicSearchExpression = "CAST([opn_credit] AS NVARCHAR)",
					Type = 131,
					DbType = SqlDbType.Decimal,
					DateTimeFormat = -1,
					VirtualExpression = "[opn_credit]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Sortable = true, // Allow sort
					DefaultErrorMessage = Language.Phrase("IncorrectFloat"),
					IsUpload = false
				};
				opn_credit.Init(this); // DN
				Fields.Add("opn_credit", opn_credit);

				// bal_mtd
				bal_mtd = new DbField<SqlDbType> {
					TableVar = "s_glchart",
					TableName = "s_glchart",
					FieldVar = "x_bal_mtd",
					Name = "bal_mtd",
					Expression = "[bal_mtd]",
					BasicSearchExpression = "CAST([bal_mtd] AS NVARCHAR)",
					Type = 131,
					DbType = SqlDbType.Decimal,
					DateTimeFormat = -1,
					VirtualExpression = "[bal_mtd]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Sortable = true, // Allow sort
					DefaultErrorMessage = Language.Phrase("IncorrectFloat"),
					IsUpload = false
				};
				bal_mtd.Init(this); // DN
				Fields.Add("bal_mtd", bal_mtd);

				// bal_ytd
				bal_ytd = new DbField<SqlDbType> {
					TableVar = "s_glchart",
					TableName = "s_glchart",
					FieldVar = "x_bal_ytd",
					Name = "bal_ytd",
					Expression = "[bal_ytd]",
					BasicSearchExpression = "CAST([bal_ytd] AS NVARCHAR)",
					Type = 131,
					DbType = SqlDbType.Decimal,
					DateTimeFormat = -1,
					VirtualExpression = "[bal_ytd]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Sortable = true, // Allow sort
					DefaultErrorMessage = Language.Phrase("IncorrectFloat"),
					IsUpload = false
				};
				bal_ytd.Init(this); // DN
				Fields.Add("bal_ytd", bal_ytd);

				// remark
				remark = new DbField<SqlDbType> {
					TableVar = "s_glchart",
					TableName = "s_glchart",
					FieldVar = "x_remark",
					Name = "remark",
					Expression = "[remark]",
					BasicSearchExpression = "[remark]",
					Type = 202,
					DbType = SqlDbType.NVarChar,
					DateTimeFormat = -1,
					VirtualExpression = "[remark]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				remark.Init(this); // DN
				remark.GetDefault = () => " ";
				Fields.Add("remark", remark);
			}

			// Set Field Visibility
			public override bool GetFieldVisibility(string fldname) {
				var fld = FieldByName(fldname);
				return fld.Visible; // Returns original value
			}

			// Invoke method // DN
			public object Invoke(string name, object[] parameters = null) {
				MethodInfo mi = this.GetType().GetMethod(name);
				if (mi != null) {
					if (IsAsyncMethod(mi)) {
						return InvokeAsync(mi, parameters).GetAwaiter().GetResult();
					} else {
						return mi.Invoke(this, parameters);
					}
				}
				return null;
			}

			// Invoke async method // DN
			public async Task<object> InvokeAsync(MethodInfo mi, object[] parameters = null) {
				if (mi != null) {
					dynamic awaitable = mi.Invoke(this, parameters);
					await awaitable;
					return awaitable.GetAwaiter().GetResult();
				}
				return null;
			}

			#pragma warning disable 1998

			// Invoke async method // DN
			public async Task<object> InvokeAsync(string name, object[] parameters = null) => InvokeAsync(this.GetType().GetMethod(name), parameters);

			#pragma warning restore 1998

			// Check if Invoke async method // DN
			public bool IsAsyncMethod(MethodInfo mi) {
				if (mi != null) {
					Type attType = typeof(AsyncStateMachineAttribute);
					var attrib = (AsyncStateMachineAttribute)mi.GetCustomAttribute(attType);
					return (attrib != null);
				}
				return false;
			}

			// Check if Invoke async method // DN
			public bool IsAsyncMethod(string name) => IsAsyncMethod(this.GetType().GetMethod(name));

			#pragma warning disable 618

			// Connection
			public virtual DatabaseConnectionBase<SqlConnection, SqlCommand, SqlDataReader, SqlDbType> Connection => GetConnection(DbId);

			#pragma warning restore 618

			// Set left column class (must be predefined col-*-* classes of Bootstrap grid system)
			public void SetLeftColumnClass(string columnClass) {
				Match m = Regex.Match(columnClass, @"^col\-(\w+)\-(\d+)$");
				if (m.Success) {
					LeftColumnClass = columnClass + " col-form-label ew-label";
					RightColumnClass = "col-" + m.Groups[1].Value + "-" + Convert.ToString(12 - ConvertToInt(m.Groups[2].Value));
					OffsetColumnClass = RightColumnClass + " " + columnClass.Replace("col-", "offset-");
					TableLeftColumnClass = Regex.Replace(columnClass, @"/^col-\w+-(\d+)$/", "w-col-$1"); // Change to w-col-*
				}
			}

			// Single column sort
			public void UpdateSort(DbField fld) {
				string lastSort, sortField, thisSort;
				if (CurrentOrder == fld.Name) {
					sortField = fld.Expression;
					lastSort = fld.Sort;
					if (CurrentOrderType == "ASC" || CurrentOrderType == "DESC") {
						thisSort = CurrentOrderType;
					} else {
						thisSort = (lastSort == "ASC") ? "DESC" : "ASC";
					}
					fld.Sort = thisSort;
					SessionOrderBy = sortField + " " + thisSort; // Save to Session
				} else {
					fld.Sort = "";
				}
			}

			// WHERE // DN
			private string _sqlWhere = null;
			public string SqlWhere {
				get {
					string where = "";
					return _sqlWhere ?? where;
				}
				set {
					_sqlWhere = value;
				}
			}

			// Group By
			private string _sqlGroupBy = null;
			public string SqlGroupBy {
				get => _sqlGroupBy ?? "";
				set => _sqlGroupBy = value;
			}

			// Having
			private string _sqlHaving = null;
			public string SqlHaving {
				get => _sqlHaving ?? "";
				set => _sqlHaving = value;
			}

			// Apply User ID filters
			public string ApplyUserIDFilters(string filter) {
				return filter;
			}

			// Check if User ID security allows view all
			public bool UserIDAllow(string id = "") {
				int allow = Config.UserIdAllow;
				switch (id) {
					case "add":
					case "copy":
					case "gridadd":
					case "register":
					case "addopt":
						return ((allow & 1) == 1);
					case "edit":
					case "gridedit":
					case "update":
					case "changepwd":
					case "forgotpwd":
						return ((allow & 4) == 4);
					case "delete":
						return ((allow & 2) == 2);
					case "view":
						return ((allow & 32) == 32);
					case "search":
						return ((allow & 64) == 64);
					default:
						return ((allow & 8) == 8);
				}
			}

			// Get record count by reading data reader
			public async Task<int> GetRecordCount(string sql) { // use by Lookup // DN
				try {
					var cnt = 0;
					using (var dr = await Connection.OpenDataReaderAsync(sql)) {
						while (await dr.ReadAsync())
							cnt++;
					}
					return cnt;
				} catch {
					if (Config.Debug)
						throw;
					return -1;
				}
			}

			// Try to get record count by SELECT COUNT(*)
			public async Task<int> TryGetRecordCount(string sql) {
				string orderBy = OrderBy;
				sql = Regex.Replace(sql, @"/\*BeginOrderBy\*/[\s\S]+/\*EndOrderBy\*/", "", RegexOptions.IgnoreCase).Trim(); // Remove ORDER BY clause (MSSQL)
				if (!string.IsNullOrEmpty(orderBy) && sql.EndsWith(orderBy))
					sql = sql.Substring(0, sql.Length - orderBy.Length); // Remove ORDER BY clause
				try {
					string sqlcnt;
					if ((new List<string> { "TABLE", "VIEW", "LINKTABLE" }).Contains(Type) && sql.StartsWith(SqlSelect)) { // Handle Custom Field
						sqlcnt = "SELECT COUNT(*) FROM " + SqlFrom + sql.Substring(SqlSelect.Length);
					} else {
						sqlcnt = "SELECT COUNT(*) FROM (" + sql + ") EW_COUNT_TABLE";
					}
					return Convert.ToInt32(await Connection.ExecuteScalarAsync(sqlcnt));
				} catch {
					return await GetRecordCount(sql);
				}
			}

			// Get ORDER BY clause
			public string OrderBy {
				get {
					string sort = SessionOrderBy;
					return BuildSelectSql("", "", "", "", SqlOrderBy, "", sort);
				}
			}

			// SELECT
			private string _sqlSelect = null;
			public string SqlSelect { // Select
				get => _sqlSelect ?? "SELECT * FROM " + SqlFrom;
				set => _sqlSelect = value;
			}

			// Table level SQL
			// FROM

			private string _sqlFrom = null;
			public string SqlFrom {
				get => _sqlFrom ?? "[dbo].[s_glchart]";
				set => _sqlFrom = value;
			}

			// Order By
			private string _sqlOrderBy = null;
			public string SqlOrderBy {
				get => _sqlOrderBy ?? "";
				set => _sqlOrderBy = value;
			}

			// Get SQL
			public string GetSql(string where, string orderBy = "") => BuildSelectSql(SqlSelect, SqlWhere, SqlGroupBy, SqlHaving, SqlOrderBy, where, orderBy);

			// Table SQL
			public string CurrentSql {
				get {
					string filter = CurrentFilter;
					string sort = SessionOrderBy;
					return GetSql(filter, sort);
				}
			}

			// Table SQL with List page filter
			public string ListSql {
				get {
					string sort = "";
					string select = "";
					string filter = UseSessionForListSql ? SessionWhere : "";
					AddFilter(ref filter, CurrentFilter);
					Recordset_Selecting(ref filter);
					select = SqlSelect;
					sort = UseSessionForListSql ? SessionOrderBy : "";
					return BuildSelectSql(select, SqlWhere, SqlGroupBy, SqlHaving, SqlOrderBy, filter, sort);
				}
			}

			// Get record count based on filter (for detail record count in master table pages)
			public async Task<int> LoadRecordCount(string filter) => await TryGetRecordCount(GetSql(filter));

			// Get record count (for current List page)
			public async Task<int> ListRecordCount() => await TryGetRecordCount(ListSql);

			// Insert
			public async Task<int> InsertAsync(Dictionary<string, object> row) {
				int result;
				var r = row.Where(kvp => {
					var fld = FieldByName(kvp.Key);
					return (fld != null && !fld.IsCustom);
				}).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
				var fields = r.Select(kvp => Fields[kvp.Key]);
				var names = String.Join(",", fields.Select(fld => fld.Expression));
				var values = String.Join(",", fields.Select(fld => SqlParameter(fld)));
				if (Empty(names))
					return -1;
				string sql = "INSERT INTO " + UpdateTable + " (" + names + ") VALUES (" + values + ")";
				using (var command = Connection.GetCommand(sql)) {
					foreach (var (key, value) in r) {
						var fld = (DbField<SqlDbType>)Fields[key]; // DN
						try {
							command.Parameters.Add(fld.FieldVar, fld.DbType).Value = ParameterValue(fld, value);
						} catch {
							if (Config.Debug)
								throw;
						}
					}
					result = await command.ExecuteNonQueryAsync();
				}
				if (result > 0) {

					// Get insert ID
					Id.SetDbValue(await Connection.GetLastInsertIdAsync());
					row["Id"] = Id.DbValue;
				}
				return result;
			}

			// Insert
			public int Insert(Dictionary<string, object> row) => InsertAsync(row).GetAwaiter().GetResult();

			// Update

			#pragma warning disable 168, 219

			public async Task<int> UpdateAsync(Dictionary<string, object> row, object where = null, Dictionary<string, object> rsold = null, bool curfilter = true) {
				int result;
				var rscascade = new Dictionary<string, object>();
				string whereClause = "";
				row = row.Where(kvp => {
					var fld = FieldByName(kvp.Key);
					return fld != null && !fld.IsCustom;
				}).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
				var fields = row.Select(kvp => Fields[kvp.Key]);
				var values = String.Join(",", fields.Select(fld => fld.Expression + "=" + SqlParameter(fld)));
				if (Empty(values))
					return -1;
				string sql = "UPDATE " + UpdateTable + " SET " + values;
				string filter = curfilter ? CurrentFilter : "";
				if (IsDictionary(where))
					whereClause = ArrayToFilter((IDictionary<string, object>)where);
				else
					whereClause = (string)where;
				AddFilter(ref filter, whereClause);
				if (!Empty(filter))
					sql += " WHERE " + filter;
				using (var command = Connection.GetCommand(sql)) {
					foreach (var (key, value) in row) {
						var fld = (DbField<SqlDbType>)Fields[key]; // DN
						try {
							command.Parameters.Add(fld.FieldVar, fld.DbType).Value = ParameterValue(fld, value);
						} catch {
							if (Config.Debug)
								throw;
						}
					}
					result = await command.ExecuteNonQueryAsync();
				}
				return result;
			}

			#pragma warning restore 168, 219

			// Update
			public int Update(Dictionary<string, object> row, object where = null, Dictionary<string, object> rsold = null, bool curfilter = true)
				=> UpdateAsync(row, where, rsold, curfilter).GetAwaiter().GetResult();

			// Convert to parameter name for use in SQL
			public string SqlParameter(DbField fld) {
				string symbol = GetSqlParamSymbol(DbId);
				string value = symbol;
				if (symbol != "?")
					value += fld.FieldVar;
				return value;
			}

			// Convert value to object for parameter
			public object ParameterValue(DbField fld, object value) {
				if (((DbField<SqlDbType>)fld).DbType == SqlDbType.Bit) {
					return ConvertToBool(value);
				}
				return value;
			}

			#pragma warning disable 168, 1998

			// Delete
			public async Task<int> DeleteAsync(Dictionary<string, object> row, object where = null, bool curfilter = true) {
				bool delete = true;
				string whereClause = "";
				string sql = "DELETE FROM " + UpdateTable + " WHERE ";
				string filter = curfilter ? CurrentFilter : "";
				if (IsDictionary(where))
					whereClause = ArrayToFilter((IDictionary<string, object>)where);
				else
					whereClause = (string)where;
				AddFilter(ref filter, whereClause);
				if (row != null) {
					DbField fld;
					fld = FieldByName("Id");
					AddFilter(ref filter, fld.Expression + "=" + QuotedValue(row["Id"], FieldByName("Id").DataType, DbId));
				}
				if (!Empty(filter))
					sql += filter;
				else
					sql += "0=1"; // Avoid delete
				int result = -1;
				if (delete)
					result = await Connection.ExecuteAsync(sql);
				return result;
			}

			#pragma warning restore 168, 1998

			// Delete
			public int Delete(Dictionary<string, object> row, object where = null, bool curfilter = true) =>
				DeleteAsync(row, where, curfilter).GetAwaiter().GetResult();

			// Load DbValue from recordset
			public void LoadDbValues(Dictionary<string, object> row) {
				if (row == null)
					return;
				Id.SetDbValue(row["Id"], false);
				acct_code.SetDbValue(row["acct_code"], false);
				description.SetDbValue(row["description"], false);
				report_type.SetDbValue(row["report_type"], false);
				acct_group.SetDbValue(row["acct_group"], false);
				acct_type.SetDbValue(row["acct_type"], false);
				CurrencyCode.SetDbValue(row["CurrencyCode"], false);
				opn_debit.SetDbValue(row["opn_debit"], false);
				opn_credit.SetDbValue(row["opn_credit"], false);
				bal_mtd.SetDbValue(row["bal_mtd"], false);
				bal_ytd.SetDbValue(row["bal_ytd"], false);
				remark.SetDbValue(row["remark"], false);
			}
			public void DeleteUploadedFiles(Dictionary<string, object> row) {
				LoadDbValues(row);
			}

			// Return URL
			public string ReturnUrl {
				get {
					string name = Config.ProjectName + "_" + TableVar + "_" + Config.TableReturnUrl;

					// Get referer URL automatically
					if (!Empty(ReferUrl()) && ReferPage() != CurrentPageName() &&
						ReferPage() != "login") {// Referer not same page or login page
							Session[name] = ReferUrl(); // Save to Session
					}
					if (!Empty(Session[name])) {
						return Session.GetString(name);
					} else {
						return "s_glchartlist";
					}
				}
				set {
					Session[Config.ProjectName + "_" + TableVar + "_" + Config.TableReturnUrl] = value;
				}
			}

			// Get modal caption
			public string GetModalCaption(string pageName) {
				if (SameString(pageName, "s_glchartview"))
					return Language.Phrase("View");
				else if (SameString(pageName, "s_glchartedit"))
					return Language.Phrase("Edit");
				else if (SameString(pageName, "s_glchartadd"))
					return Language.Phrase("Add");
				else
					return "";
			}

			// List URL
			public string ListUrl => "s_glchartlist";

			// View URL
			public string ViewUrl => GetViewUrl();

			// View URL
			public string GetViewUrl(string parm = "") {
				string url = "";
				if (!Empty(parm))
					url = KeyUrl("s_glchartview", UrlParm(parm));
				else
					url = KeyUrl("s_glchartview", UrlParm(Config.TableShowDetail + "="));
				return AddMasterUrl(url);
			}

			// Add URL
			public string AddUrl { get; set; } = "s_glchartadd";

			// Add URL
			public string GetAddUrl(string parm = "") {
				string url = "";
				if (!Empty(parm))
					url = "s_glchartadd?" + UrlParm(parm);
				else
					url = "s_glchartadd";
				return AppPath(AddMasterUrl(url));
			}

			// Edit URL
			public string EditUrl => GetEditUrl();

			// Edit URL (with parameter)
			public string GetEditUrl(string parm = "") {
				string url = "";
				url = KeyUrl("s_glchartedit", UrlParm(parm));
				return AppPath(AddMasterUrl(url)); // DN
			}

			// Inline edit URL
			public string InlineEditUrl =>
				AppPath(AddMasterUrl(KeyUrl(CurrentPageName(), UrlParm("action=edit")))); // DN

			// Copy URL
			public string CopyUrl => GetCopyUrl();

			// Copy URL
			public string GetCopyUrl(string parm = "") {
				string url = "";
				url = KeyUrl("s_glchartadd", UrlParm(parm));
				return AppPath(AddMasterUrl(url)); // DN
			}

			// Inline copy URL
			public string InlineCopyUrl =>
				AppPath(AddMasterUrl(KeyUrl(CurrentPageName(), UrlParm("action=copy")))); // DN

			// Delete URL
			public string DeleteUrl =>
				AppPath(KeyUrl("s_glchartdelete", UrlParm())); // DN

			// Add master URL
			public string AddMasterUrl(string url) {
				return url;
			}

			// Get primary key as JSON
			public string KeyToJson() {
				string json = "";
				json += "Id:" + ConvertToJson(Id.CurrentValue, "number", true);
				return "{" + json + "}";
			}

			// Add key value to URL
			public string KeyUrl(string url, string parm = "") { // DN
				if (!IsDBNull(Id.CurrentValue)) {
					url += "/" + Id.CurrentValue;
				} else {
					return "javascript:ew.alert(ew.language.phrase('InvalidRecord'));";
				}
				if (Empty(parm))
					return url;
				else
					return url + "?" + parm;
			}

			// Sort URL (already URL-encoded)
			public string SortUrl(DbField fld) {
				if (!Empty(CurrentAction) || !Empty(Export) ||
					(new List<int> {141, 201, 203, 128, 204, 205}).Contains(fld.Type)) { // Unsortable data type
					return "";
				} else if (fld.Sortable) {
					string urlParm = UrlParm("order=" + UrlEncode(fld.Name) + "&amp;ordertype=" + fld.ReverseSort());
					return AddMasterUrl(CurrentPageName() + "?" + urlParm);
				}
				return "";
			}

			#pragma warning disable 168

			// Get record keys
			public List<string> GetRecordKeys() {
				var result = new List<string>();
				StringValues sv;
				var keysList = new List<string>();
				if (Form.TryGetValue("key_m", out sv) || Query.TryGetValue("key_m", out sv)) {
					keysList = sv.ToList();
				} else if (RouteValues.Count > 0 || Query.Count > 0 || Form.Count > 0) { // DN
					string key = "";
					string[] keyValues = null;
					object rv;
					if (IsApi() && RouteValues.TryGetValue("key", out object k))
						keyValues = k.ToString().Split('/');
					if (RouteValues.TryGetValue("Id", out rv)) { // Id
						key = Convert.ToString(rv);
					} else if (IsApi() && !Empty(keyValues)) {
						key = keyValues[0];
					} else {
						key = Param("Id");
					}
					keysList.Add(key);
				}

				// Check keys
				foreach (var keys in keysList) {
					if (!IsNumeric(keys)) // Id
						continue;
					result.Add(keys);
				}
				return result;
			}

			#pragma warning restore 168

			// Get filter from record keys
			public string GetFilterFromRecordKeys() {
				List<string> recordKeys = GetRecordKeys();
				string keyFilter = "";
				foreach (var keys in recordKeys) {
					if (!Empty(keyFilter))
						keyFilter += " OR ";
					Id.CurrentValue = keys;
					keyFilter += "(" + GetRecordFilter() + ")";
				}
				return keyFilter;
			}

			#pragma warning disable 618

			// Load rows based on filter // DN
			public async Task<DbDataReader> LoadRs(string filter, DatabaseConnectionBase<SqlConnection, SqlCommand, SqlDataReader, SqlDbType> conn = null) {

				// Set up filter (SQL WHERE clause) and get return SQL
				string sql = GetSql(filter);
				try {
					var dr = await (conn ?? Connection).OpenDataReaderAsync(sql);
					if (dr?.HasRows ?? false)
						return dr;
				} catch {}
				return null;
			}

			#pragma warning restore 618

			// Record filter WHERE clause
			private string _sqlKeyFilter => "[Id] = @Id@";

			#pragma warning disable 168

			// Get record filter
			public string GetRecordFilter(Dictionary<string, object> row = null)
			{
				string keyFilter = _sqlKeyFilter;
				object val, result;
				val = !Empty(row) ? (row.TryGetValue("Id", out result) ? result : null) : Id.CurrentValue;
				if (!IsNumeric(val))
					return "0=1"; // Invalid key
				if (val == null)
					return "0=1"; // Invalid key
				else
					keyFilter = keyFilter.Replace("@Id@", AdjustSql(val, DbId)); // Replace key value
				return keyFilter;
			}

			#pragma warning restore 168

			// Load row values from recordset
			public void LoadListRowValues(DbDataReader rs) {
				Id.SetDbValue(rs["Id"]);
				acct_code.SetDbValue(rs["acct_code"]);
				description.SetDbValue(rs["description"]);
				report_type.SetDbValue(rs["report_type"]);
				acct_group.SetDbValue(rs["acct_group"]);
				acct_type.SetDbValue(rs["acct_type"]);
				CurrencyCode.SetDbValue(rs["CurrencyCode"]);
				opn_debit.SetDbValue(rs["opn_debit"]);
				opn_credit.SetDbValue(rs["opn_credit"]);
				bal_mtd.SetDbValue(rs["bal_mtd"]);
				bal_ytd.SetDbValue(rs["bal_ytd"]);
				remark.SetDbValue(rs["remark"]);
			}

			#pragma warning disable 1998

			// Render list row values
			public async Task RenderListRow() {

				// Call Row Rendering event
				Row_Rendering();

				// Common render codes
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

				// Call Row Rendered event
				Row_Rendered();

				// Save data for Custom Template
				Rows.Add(CustomTemplateFieldValues());
			}

			#pragma warning restore 1998

			#pragma warning disable 1998

			// Render edit row values
			public async Task RenderEditRow() {

				// Call Row Rendering event
				Row_Rendering();

				// Id
				Id.EditAttrs["class"] = "form-control";
				Id.EditValue = Id.CurrentValue;

				// acct_code
				acct_code.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					acct_code.CurrentValue = HtmlDecode(acct_code.CurrentValue);
				acct_code.EditValue = acct_code.CurrentValue; // DN
				acct_code.PlaceHolder = RemoveHtml(acct_code.Caption);

				// description
				description.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					description.CurrentValue = HtmlDecode(description.CurrentValue);
				description.EditValue = description.CurrentValue; // DN
				description.PlaceHolder = RemoveHtml(description.Caption);

				// report_type
				report_type.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					report_type.CurrentValue = HtmlDecode(report_type.CurrentValue);
				report_type.EditValue = report_type.CurrentValue; // DN
				report_type.PlaceHolder = RemoveHtml(report_type.Caption);

				// acct_group
				acct_group.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					acct_group.CurrentValue = HtmlDecode(acct_group.CurrentValue);
				acct_group.EditValue = acct_group.CurrentValue; // DN
				acct_group.PlaceHolder = RemoveHtml(acct_group.Caption);

				// acct_type
				acct_type.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					acct_type.CurrentValue = HtmlDecode(acct_type.CurrentValue);
				acct_type.EditValue = acct_type.CurrentValue; // DN
				acct_type.PlaceHolder = RemoveHtml(acct_type.Caption);

				// CurrencyCode
				CurrencyCode.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					CurrencyCode.CurrentValue = HtmlDecode(CurrencyCode.CurrentValue);
				CurrencyCode.EditValue = CurrencyCode.CurrentValue; // DN
				CurrencyCode.PlaceHolder = RemoveHtml(CurrencyCode.Caption);

				// opn_debit
				opn_debit.EditAttrs["class"] = "form-control";
				opn_debit.EditValue = opn_debit.CurrentValue; // DN
				opn_debit.PlaceHolder = RemoveHtml(opn_debit.Caption);
				if (!Empty(opn_debit.EditValue) && IsNumeric(opn_debit.EditValue))
					opn_debit.EditValue = FormatNumber(opn_debit.EditValue, -2, -2, -2, -2);

				// opn_credit
				opn_credit.EditAttrs["class"] = "form-control";
				opn_credit.EditValue = opn_credit.CurrentValue; // DN
				opn_credit.PlaceHolder = RemoveHtml(opn_credit.Caption);
				if (!Empty(opn_credit.EditValue) && IsNumeric(opn_credit.EditValue))
					opn_credit.EditValue = FormatNumber(opn_credit.EditValue, -2, -2, -2, -2);

				// bal_mtd
				bal_mtd.EditAttrs["class"] = "form-control";
				bal_mtd.EditValue = bal_mtd.CurrentValue; // DN
				bal_mtd.PlaceHolder = RemoveHtml(bal_mtd.Caption);
				if (!Empty(bal_mtd.EditValue) && IsNumeric(bal_mtd.EditValue))
					bal_mtd.EditValue = FormatNumber(bal_mtd.EditValue, -2, -2, -2, -2);

				// bal_ytd
				bal_ytd.EditAttrs["class"] = "form-control";
				bal_ytd.EditValue = bal_ytd.CurrentValue; // DN
				bal_ytd.PlaceHolder = RemoveHtml(bal_ytd.Caption);
				if (!Empty(bal_ytd.EditValue) && IsNumeric(bal_ytd.EditValue))
					bal_ytd.EditValue = FormatNumber(bal_ytd.EditValue, -2, -2, -2, -2);

				// remark
				remark.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					remark.CurrentValue = HtmlDecode(remark.CurrentValue);
				remark.EditValue = remark.CurrentValue; // DN
				remark.PlaceHolder = RemoveHtml(remark.Caption);

				// Call Row Rendered event
				Row_Rendered();
			}

			#pragma warning restore 1998

			// Aggregate list row values
			public void AggregateListRowValues() {
			}

			#pragma warning disable 1998

			// Aggregate list row (for rendering)
			public async Task AggregateListRow() {

				// Call Row Rendered event
				Row_Rendered();
			}

			#pragma warning restore 1998

			// Export document
			public dynamic ExportDoc;

			// Export data in HTML/CSV/Word/Excel/Email/PDF format
			public async Task ExportDocument(dynamic doc, DbDataReader dataReader, int startRec, int stopRec, string exportType = "") {
				if (dataReader == null || doc == null)
					return;
				if (!doc.ExportCustom) {

					// Write header
					doc.ExportTableHeader();
					if (doc.Horizontal) { // Horizontal format, write header
						doc.BeginExportRow();
						if (exportType == "view") {
							doc.ExportCaption(Id);
							doc.ExportCaption(acct_code);
							doc.ExportCaption(description);
							doc.ExportCaption(report_type);
							doc.ExportCaption(acct_group);
							doc.ExportCaption(acct_type);
							doc.ExportCaption(CurrencyCode);
							doc.ExportCaption(opn_debit);
							doc.ExportCaption(opn_credit);
							doc.ExportCaption(bal_mtd);
							doc.ExportCaption(bal_ytd);
							doc.ExportCaption(remark);
						} else {
							doc.ExportCaption(Id);
							doc.ExportCaption(acct_code);
							doc.ExportCaption(description);
							doc.ExportCaption(report_type);
							doc.ExportCaption(acct_group);
							doc.ExportCaption(acct_type);
							doc.ExportCaption(CurrencyCode);
							doc.ExportCaption(opn_debit);
							doc.ExportCaption(opn_credit);
							doc.ExportCaption(bal_mtd);
							doc.ExportCaption(bal_ytd);
							doc.ExportCaption(remark);
						}
						doc.EndExportRow();
					}
				}

				// Move to first record
				// For List page only. For View page, the recordset is alreay at the start record. // DN

				int recCnt = startRec - 1;
				if (exportType != "view") {
					if (Connection.SelectOffset) {
						await dataReader.ReadAsync();
					} else {
						for (int i = 0; i < startRec; i++) // Move to the start record and use do-while loop
							await dataReader.ReadAsync();
					}
				}
				int rowcnt = 0; // DN
				do { // DN
					recCnt++;
					if (recCnt >= startRec) {
						rowcnt = recCnt - startRec + 1;

						// Page break
						if (ExportPageBreakCount > 0) {
							if (rowcnt > 1 && (rowcnt - 1) % ExportPageBreakCount == 0)
								doc.ExportPageBreak();
						}
						LoadListRowValues(dataReader);

						// Render row
						RowType = Config.RowTypeView; // Render view
						ResetAttributes();
						await RenderListRow();
						if (!doc.ExportCustom) {
							doc.BeginExportRow(rowcnt); // Allow CSS styles if enabled
							if (exportType == "view") {
								await doc.ExportField(Id);
								await doc.ExportField(acct_code);
								await doc.ExportField(description);
								await doc.ExportField(report_type);
								await doc.ExportField(acct_group);
								await doc.ExportField(acct_type);
								await doc.ExportField(CurrencyCode);
								await doc.ExportField(opn_debit);
								await doc.ExportField(opn_credit);
								await doc.ExportField(bal_mtd);
								await doc.ExportField(bal_ytd);
								await doc.ExportField(remark);
							} else {
								await doc.ExportField(Id);
								await doc.ExportField(acct_code);
								await doc.ExportField(description);
								await doc.ExportField(report_type);
								await doc.ExportField(acct_group);
								await doc.ExportField(acct_type);
								await doc.ExportField(CurrencyCode);
								await doc.ExportField(opn_debit);
								await doc.ExportField(opn_credit);
								await doc.ExportField(bal_mtd);
								await doc.ExportField(bal_ytd);
								await doc.ExportField(remark);
							}
							doc.EndExportRow(rowcnt);
						}
					}

					// Call Row Export server event
					if (doc.ExportCustom)
						Row_Export(dataReader);
				} while (recCnt < stopRec && await dataReader.ReadAsync()); // DN
				if (!doc.ExportCustom)
					doc.ExportTableFooter();
			}

			#pragma warning disable 219

			// Lookup data from table
			public async Task<JsonBoolResult> Lookup() {
				Language = Language ?? new Lang(Config.LanguageFolder, Post("language"));
				bool validRequest = true;
				if (Security == null)
					Security = new AdvancedSecurity();
				if (Security.IsLoggedIn)
					Security.TablePermission_Loading();
				Security.LoadCurrentUserLevel(Config.ProjectId + Name);
				if (Security.IsLoggedIn)
					Security.TablePermission_Loaded();
				validRequest = Security.CanList; // List permission
				if (validRequest) {
					Security.UserID_Loading();
					await Security.LoadUserID();
					Security.UserID_Loaded();
				}

				// Reject invalid request
				if (!validRequest)
					return JsonBoolResult.FalseResult;

				// Load lookup parameters
				bool distinct = Post<bool>("distinct");
				string linkField = Post("linkField");
				StringValues sv;
				var displayFields = Form.TryGetValue("displayFields[]", out sv) ? sv.ToList() : new List<string>();
				var parentFields = Form.TryGetValue("parentFields[]", out sv) ? sv.ToList() : new List<string>();
				var childFields = Form.TryGetValue("childFields[]", out sv) ? sv.ToList() : new List<string>();
				var filterFields = Form.TryGetValue("filterFields[]", out sv) ? sv.ToList() : new List<string>();
				var filterFieldVars = Form.TryGetValue("filterFieldVars[]", out sv) ? sv.ToList() : new List<string>();
				var filterOperators = Form.TryGetValue("filterOperators[]", out sv) ? sv.ToList() : new List<string>();
				var autoFillSourceFields = Form.TryGetValue("autoFillSourceFields[]", out sv) ? sv.ToList() : new List<string>();
				bool formatAutoFill = false;
				string lookupType = Post("ajax");
				int pageSize = -1;
				int offset = -1;
				string searchValue = "";
				if (SameText(lookupType, "modal")) {
					searchValue = Post("sv");
					if (Empty(Post("recperpage")))
						pageSize = 10;
					else
						pageSize = Post<int>("recperpage");
					offset = Post<int>("start");
				} else if (SameText(lookupType, "autosuggest")) {
					searchValue = Get("q");
					pageSize = IsNumeric(Param("n")) ? Param<int>("n") : -1;
					if (pageSize <= 0)
						pageSize = Config.AutoSuggestMaxEntries;
					int start = IsNumeric(Param("start")) ? Param<int>("start") : -1;
					int page = IsNumeric(Param("page")) ? Param<int>("page") : -1;
					offset = start >= 0 ? start : (page > 0 && pageSize > 0 ? (page - 1) * pageSize : 0);
				}
				string userSelect = Decrypt(Post("s"));
				string userFilter = Decrypt(Post("f"));
				string userOrderBy = Decrypt(Post("o"));

				// Selected records from modal, skip parent/filter fields and show all records
				StringValues keys;
				if (Form.TryGetValue("keys[]", out keys)) { // Selected records from modal
					parentFields = new List<string>();
					filterFields = new List<string>();
					filterFieldVars = new List<string>();
					offset = -1;
					pageSize = -1;
				}

				// Create lookup object and output JSON
				var lookup = new Lookup<DbField>(linkField, TableVar, distinct, linkField, displayFields, parentFields, childFields, filterFields, filterFieldVars, autoFillSourceFields);
				for (int i = 0; i < filterFields.Count; i++) { // Set up filter operators
					if (!Empty(filterOperators[i]))
						lookup.SetFilterOperator(filterFields[i], filterOperators[i]);
				}
				lookup.LookupType = lookupType; // Lookup type
				if (!StringValues.IsNullOrEmpty(keys)) { // Selected records from modal
					lookup.FilterValues.Add(string.Join(",", keys.ToArray()));
				} else { // Lookup values
					lookup.FilterValues.Add(Post<string>("v0") ?? Post("lookupValue"));
				}
				int cnt = IsList(filterFields) ? filterFields.Count : 0;
				for (int i = 1; i <= cnt; i++)
					lookup.FilterValues.Add(UrlDecode(Post("v" + i)));
				lookup.SearchValue = searchValue;
				lookup.PageSize = pageSize;
				lookup.Offset = offset;
				if (userSelect != "")
					lookup.UserSelect = userSelect;
				if (userFilter != "")
					lookup.UserFilter = userFilter;
				if (userOrderBy != "")
					lookup.UserOrderBy = userOrderBy;
				return await lookup.ToJson();
			}

			#pragma warning restore 219

			// Table filter
			private string _tableFilter = null;
			public string TableFilter {
				get => _tableFilter ?? "";
				set => _tableFilter = value;
			}

			// TblBasicSearchDefault
			private string _tableBasicSearchDefault = null;
			public string TableBasicSearchDefault {
				get => _tableBasicSearchDefault ?? "";
				set => _tableBasicSearchDefault = value;
			}

			#pragma warning disable 1998

			// Get file data
			public async Task<IActionResult> GetFileData(string fldparm, string key, bool resize, int width = -1, int height = -1) {
				if (width < 0)
					width = Config.ThumbnailDefaultWidth;
				if (height < 0)
					height = Config.ThumbnailDefaultHeight;

				// No binary fields
				return JsonBoolResult.FalseResult; // Incorrect key
			}

			#pragma warning restore 1998

			// Table level events
			// Recordset Selecting event

			public void Recordset_Selecting(ref string filter) {

				// Enter your code here
			}

			// Recordset Search Validated event
			public void Recordset_SearchValidated() {

				// Enter your code here
			}

			// Recordset Searching event
			public void Recordset_Searching(ref string filter) {

				// Enter your code here
			}

			// Row_Selecting event
			public void Row_Selecting(ref string filter) {

				// Enter your code here
			}

			// Row Selected event
			public void Row_Selected(Dictionary<string, object> row) {

				//Log("Row Selected");
			}

			// Row Inserting event
			public bool Row_Inserting(Dictionary<string, object> rsold, Dictionary<string, object> rsnew) {

				// Enter your code here
				// To cancel, set return value to False and error message to CancelMessage

				return true;
			}

			// Row Inserted event
			public void Row_Inserted(Dictionary<string, object> rsold, Dictionary<string, object> rsnew) {

				//Log("Row Inserted");
			}

			// Row Updating event
			public bool Row_Updating(Dictionary<string, object> rsold, Dictionary<string, object> rsnew) {

				// Enter your code here
				// To cancel, set return value to False and error message to CancelMessage

				return true;
			}

			// Row Updated event
			public void Row_Updated(Dictionary<string, object> rsold, Dictionary<string, object> rsnew) {

				//Log("Row Updated");
			}

			// Row Update Conflict event
			public bool Row_UpdateConflict(Dictionary<string, object> rsold, Dictionary<string, object> rsnew) {

				// Enter your code here
				// To ignore conflict, set return value to false

				return true;
			}

			// Row Export event
			// ExportDoc = export document object

			public virtual void Row_Export(DbDataReader rs) {

				//ExportDoc.Text.Append("<div>" + MyField.ViewValue +"</div>"); // Build HTML with field value: rs["MyField"] or MyField.ViewValue
			}

			// Page Exporting event
			// ExportDoc = export document object

			public virtual bool Page_Exporting() {

				//ExportDoc.Text.Append("<p>" + "my header" + "</p>"); // Export header
				//return false; // Return FALSE to skip default export and use Row_Export event

				return true; // Return TRUE to use default export and skip Row_Export event
			}

			// Page Exported event
			// ExportDoc = export document object

			public virtual void Page_Exported() {

				//ExportDoc.Text.Append("my footer"); // Export footer
				//Log("Text: {Text}", ExportDoc.Text);

			}

			// Recordset Deleting event
			public bool Row_Deleting(Dictionary<string, object> rs) {

				// Enter your code here
				// To cancel, set return value to False and error message to CancelMessage

				return true;
			}

			// Row Deleted event
			public void Row_Deleted(Dictionary<string, object> rs) {

				//Log("Row Deleted");
			}

			// Email Sending event
			public virtual bool Email_Sending(Email email, dynamic args) {

				//Log(email);
				return true;
			}

			// Lookup Selecting event
			public void Lookup_Selecting(DbField fld, ref string filter) {

				// Enter your code here
			}

			// Row Rendering event
			public void Row_Rendering() {

				// Enter your code here
			}

			// Row Rendered event
			public void Row_Rendered() {

				//VarDump(<FieldName>); // View field properties
			}

			// User ID Filtering event
			public void UserID_Filtering(ref string filter) {

				// Enter your code here
			}
		}
	}
}
