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
		/// s_dodetltest
		/// </summary>

		public static _s_dodetltest s_dodetltest {
			get => HttpData.GetOrCreate<_s_dodetltest>("s_dodetltest");
			set => HttpData["s_dodetltest"] = value;
		}

		/// <summary>
		/// Table class for s_dodetltest
		/// </summary>

		public class _s_dodetltest: DbTable {
			public int RowCnt = 0; // DN
			public bool UseSessionForListSql = true;

			// Column CSS classes
			public string LeftColumnClass = "col-sm-2 col-form-label ew-label";
			public string RightColumnClass = "col-sm-10";
			public string OffsetColumnClass = "col-sm-10 offset-sm-2";
			public string TableLeftColumnClass = "w-col-2";
			public readonly DbField<SqlDbType> TrxId;
			public readonly DbField<SqlDbType> Id_dohdrTrxId;
			public readonly DbField<SqlDbType> item_no;
			public readonly DbField<SqlDbType> item_type;
			public readonly DbField<SqlDbType> Id_sodetlTrxId;
			public readonly DbField<SqlDbType> Id_podetlTrxId;
			public readonly DbField<SqlDbType> part_code;
			public readonly DbField<SqlDbType> part_desc;
			public readonly DbField<SqlDbType> uom;
			public readonly DbField<SqlDbType> qty;
			public readonly DbField<SqlDbType> unit_price;
			public readonly DbField<SqlDbType> amount_origin;
			public readonly DbField<SqlDbType> amount_local;
			public readonly DbField<SqlDbType> tax_code;
			public readonly DbField<SqlDbType> tax_rate;
			public readonly DbField<SqlDbType> tax_amount_origin;
			public readonly DbField<SqlDbType> tax_amount_local;
			public readonly DbField<SqlDbType> TrxUserId;
			public readonly DbField<SqlDbType> to_gl_acct;
			public readonly DbField<SqlDbType> tax_acct;

			// Constructor
			public _s_dodetltest() {

				// Language object // DN
				Language = Language ?? new Lang();
				TableVar = "s_dodetltest";
				Name = "s_dodetltest";
				Type = "TABLE";

				// Update Table
				UpdateTable = "[dbo].[s_dodetltest]";
				DbId = "DB"; // DN
				ExportAll = true;
				ExportPageBreakCount = 0; // Page break per every n record (PDF only)
				ExportPageOrientation = "portrait"; // Page orientation (PDF only)
				ExportPageSize = "a4"; // Page size (PDF only)
				ExportExcelPageOrientation = ""; // Page orientation (EPPlus only)
				ExportExcelPageSize = ""; // Page size (EPPlus only)
				ExportColumnWidths = new float[] {  }; // Column widths (PDF only) // DN
				DetailAdd = true; // Allow detail add
				DetailEdit = true; // Allow detail edit
				DetailView = true; // Allow detail view
				ShowMultipleDetails = false; // Show multiple details
				GridAddRowCount = 5;
				AllowAddDeleteRow = true; // Allow add/delete row
				UserIdAllowSecurity = 0; // User ID Allow
				BasicSearch = new BasicSearch(TableVar);

				// TrxId
				TrxId = new DbField<SqlDbType> {
					TableVar = "s_dodetltest",
					TableName = "s_dodetltest",
					FieldVar = "x_TrxId",
					Name = "TrxId",
					Expression = "[TrxId]",
					BasicSearchExpression = "CAST([TrxId] AS NVARCHAR)",
					Type = 3,
					DbType = SqlDbType.Int,
					DateTimeFormat = -1,
					VirtualExpression = "[TrxId]",
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
				TrxId.Init(this); // DN
				Fields.Add("TrxId", TrxId);

				// Id_dohdrTrxId
				Id_dohdrTrxId = new DbField<SqlDbType> {
					TableVar = "s_dodetltest",
					TableName = "s_dodetltest",
					FieldVar = "x_Id_dohdrTrxId",
					Name = "Id_dohdrTrxId",
					Expression = "[Id_dohdrTrxId]",
					BasicSearchExpression = "CAST([Id_dohdrTrxId] AS NVARCHAR)",
					Type = 3,
					DbType = SqlDbType.Int,
					DateTimeFormat = -1,
					VirtualExpression = "[Id_dohdrTrxId]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					IsForeignKey = true, // Foreign key field
					Nullable = false, // NOT NULL field
					Required = true, // Required field
					Sortable = true, // Allow sort
					DefaultErrorMessage = Language.Phrase("IncorrectInteger"),
					IsUpload = false
				};
				Id_dohdrTrxId.Init(this); // DN
				Fields.Add("Id_dohdrTrxId", Id_dohdrTrxId);

				// item_no
				item_no = new DbField<SqlDbType> {
					TableVar = "s_dodetltest",
					TableName = "s_dodetltest",
					FieldVar = "x_item_no",
					Name = "item_no",
					Expression = "[item_no]",
					BasicSearchExpression = "CAST([item_no] AS NVARCHAR)",
					Type = 3,
					DbType = SqlDbType.Int,
					DateTimeFormat = -1,
					VirtualExpression = "[item_no]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Nullable = false, // NOT NULL field
					Required = true, // Required field
					Sortable = true, // Allow sort
					DefaultErrorMessage = Language.Phrase("IncorrectInteger"),
					IsUpload = false
				};
				item_no.Init(this); // DN
				Fields.Add("item_no", item_no);

				// item_type
				item_type = new DbField<SqlDbType> {
					TableVar = "s_dodetltest",
					TableName = "s_dodetltest",
					FieldVar = "x_item_type",
					Name = "item_type",
					Expression = "[item_type]",
					BasicSearchExpression = "[item_type]",
					Type = 129,
					DbType = SqlDbType.Char,
					DateTimeFormat = -1,
					VirtualExpression = "[item_type]",
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
				item_type.Init(this); // DN
				item_type.GetDefault = () => "I";
				Fields.Add("item_type", item_type);

				// Id_sodetlTrxId
				Id_sodetlTrxId = new DbField<SqlDbType> {
					TableVar = "s_dodetltest",
					TableName = "s_dodetltest",
					FieldVar = "x_Id_sodetlTrxId",
					Name = "Id_sodetlTrxId",
					Expression = "[Id_sodetlTrxId]",
					BasicSearchExpression = "CAST([Id_sodetlTrxId] AS NVARCHAR)",
					Type = 3,
					DbType = SqlDbType.Int,
					DateTimeFormat = -1,
					VirtualExpression = "[Id_sodetlTrxId]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Sortable = true, // Allow sort
					DefaultErrorMessage = Language.Phrase("IncorrectInteger"),
					IsUpload = false
				};
				Id_sodetlTrxId.Init(this); // DN
				Fields.Add("Id_sodetlTrxId", Id_sodetlTrxId);

				// Id_podetlTrxId
				Id_podetlTrxId = new DbField<SqlDbType> {
					TableVar = "s_dodetltest",
					TableName = "s_dodetltest",
					FieldVar = "x_Id_podetlTrxId",
					Name = "Id_podetlTrxId",
					Expression = "[Id_podetlTrxId]",
					BasicSearchExpression = "CAST([Id_podetlTrxId] AS NVARCHAR)",
					Type = 3,
					DbType = SqlDbType.Int,
					DateTimeFormat = -1,
					VirtualExpression = "[Id_podetlTrxId]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Sortable = true, // Allow sort
					DefaultErrorMessage = Language.Phrase("IncorrectInteger"),
					IsUpload = false
				};
				Id_podetlTrxId.Init(this); // DN
				Fields.Add("Id_podetlTrxId", Id_podetlTrxId);

				// part_code
				part_code = new DbField<SqlDbType> {
					TableVar = "s_dodetltest",
					TableName = "s_dodetltest",
					FieldVar = "x_part_code",
					Name = "part_code",
					Expression = "[part_code]",
					BasicSearchExpression = "[part_code]",
					Type = 200,
					DbType = SqlDbType.VarChar,
					DateTimeFormat = -1,
					VirtualExpression = "[part_code]",
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
				part_code.Init(this); // DN
				part_code.Lookup = new Lookup<DbField>("part_code", "s_services", false, "service_code", new List<string> {"Description", "service_code", "", ""}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {"Description"}, new List<string> {"x_part_desc"}, "", "");
				Fields.Add("part_code", part_code);

				// part_desc
				part_desc = new DbField<SqlDbType> {
					TableVar = "s_dodetltest",
					TableName = "s_dodetltest",
					FieldVar = "x_part_desc",
					Name = "part_desc",
					Expression = "[part_desc]",
					BasicSearchExpression = "[part_desc]",
					Type = 200,
					DbType = SqlDbType.VarChar,
					DateTimeFormat = -1,
					VirtualExpression = "[part_desc]",
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
				part_desc.Init(this); // DN
				Fields.Add("part_desc", part_desc);

				// uom
				uom = new DbField<SqlDbType> {
					TableVar = "s_dodetltest",
					TableName = "s_dodetltest",
					FieldVar = "x_uom",
					Name = "uom",
					Expression = "[uom]",
					BasicSearchExpression = "[uom]",
					Type = 200,
					DbType = SqlDbType.VarChar,
					DateTimeFormat = -1,
					VirtualExpression = "[uom]",
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
				uom.Init(this); // DN
				Fields.Add("uom", uom);

				// qty
				qty = new DbField<SqlDbType> {
					TableVar = "s_dodetltest",
					TableName = "s_dodetltest",
					FieldVar = "x_qty",
					Name = "qty",
					Expression = "[qty]",
					BasicSearchExpression = "CAST([qty] AS NVARCHAR)",
					Type = 131,
					DbType = SqlDbType.Decimal,
					DateTimeFormat = -1,
					VirtualExpression = "[qty]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Nullable = false, // NOT NULL field
					Required = true, // Required field
					Sortable = true, // Allow sort
					DefaultErrorMessage = Language.Phrase("IncorrectFloat"),
					IsUpload = false
				};
				qty.Init(this); // DN
				Fields.Add("qty", qty);

				// unit_price
				unit_price = new DbField<SqlDbType> {
					TableVar = "s_dodetltest",
					TableName = "s_dodetltest",
					FieldVar = "x_unit_price",
					Name = "unit_price",
					Expression = "[unit_price]",
					BasicSearchExpression = "CAST([unit_price] AS NVARCHAR)",
					Type = 131,
					DbType = SqlDbType.Decimal,
					DateTimeFormat = -1,
					VirtualExpression = "[unit_price]",
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
				unit_price.Init(this); // DN
				Fields.Add("unit_price", unit_price);

				// amount_origin
				amount_origin = new DbField<SqlDbType> {
					TableVar = "s_dodetltest",
					TableName = "s_dodetltest",
					FieldVar = "x_amount_origin",
					Name = "amount_origin",
					Expression = "[amount_origin]",
					BasicSearchExpression = "CAST([amount_origin] AS NVARCHAR)",
					Type = 131,
					DbType = SqlDbType.Decimal,
					DateTimeFormat = -1,
					VirtualExpression = "[amount_origin]",
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
				amount_origin.Init(this); // DN
				Fields.Add("amount_origin", amount_origin);

				// amount_local
				amount_local = new DbField<SqlDbType> {
					TableVar = "s_dodetltest",
					TableName = "s_dodetltest",
					FieldVar = "x_amount_local",
					Name = "amount_local",
					Expression = "[amount_local]",
					BasicSearchExpression = "CAST([amount_local] AS NVARCHAR)",
					Type = 131,
					DbType = SqlDbType.Decimal,
					DateTimeFormat = -1,
					VirtualExpression = "[amount_local]",
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
				amount_local.Init(this); // DN
				Fields.Add("amount_local", amount_local);

				// tax_code
				tax_code = new DbField<SqlDbType> {
					TableVar = "s_dodetltest",
					TableName = "s_dodetltest",
					FieldVar = "x_tax_code",
					Name = "tax_code",
					Expression = "[tax_code]",
					BasicSearchExpression = "[tax_code]",
					Type = 130,
					DbType = SqlDbType.NChar,
					DateTimeFormat = -1,
					VirtualExpression = "[tax_code]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				tax_code.Init(this); // DN
				Fields.Add("tax_code", tax_code);

				// tax_rate
				tax_rate = new DbField<SqlDbType> {
					TableVar = "s_dodetltest",
					TableName = "s_dodetltest",
					FieldVar = "x_tax_rate",
					Name = "tax_rate",
					Expression = "[tax_rate]",
					BasicSearchExpression = "CAST([tax_rate] AS NVARCHAR)",
					Type = 131,
					DbType = SqlDbType.Decimal,
					DateTimeFormat = -1,
					VirtualExpression = "[tax_rate]",
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
				tax_rate.Init(this); // DN
				Fields.Add("tax_rate", tax_rate);

				// tax_amount_origin
				tax_amount_origin = new DbField<SqlDbType> {
					TableVar = "s_dodetltest",
					TableName = "s_dodetltest",
					FieldVar = "x_tax_amount_origin",
					Name = "tax_amount_origin",
					Expression = "[tax_amount_origin]",
					BasicSearchExpression = "CAST([tax_amount_origin] AS NVARCHAR)",
					Type = 131,
					DbType = SqlDbType.Decimal,
					DateTimeFormat = -1,
					VirtualExpression = "[tax_amount_origin]",
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
				tax_amount_origin.Init(this); // DN
				Fields.Add("tax_amount_origin", tax_amount_origin);

				// tax_amount_local
				tax_amount_local = new DbField<SqlDbType> {
					TableVar = "s_dodetltest",
					TableName = "s_dodetltest",
					FieldVar = "x_tax_amount_local",
					Name = "tax_amount_local",
					Expression = "[tax_amount_local]",
					BasicSearchExpression = "CAST([tax_amount_local] AS NVARCHAR)",
					Type = 131,
					DbType = SqlDbType.Decimal,
					DateTimeFormat = -1,
					VirtualExpression = "[tax_amount_local]",
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
				tax_amount_local.Init(this); // DN
				Fields.Add("tax_amount_local", tax_amount_local);

				// TrxUserId
				TrxUserId = new DbField<SqlDbType> {
					TableVar = "s_dodetltest",
					TableName = "s_dodetltest",
					FieldVar = "x_TrxUserId",
					Name = "TrxUserId",
					Expression = "[TrxUserId]",
					BasicSearchExpression = "CAST([TrxUserId] AS NVARCHAR)",
					Type = 3,
					DbType = SqlDbType.Int,
					DateTimeFormat = -1,
					VirtualExpression = "[TrxUserId]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Nullable = false, // NOT NULL field
					Required = true, // Required field
					Sortable = true, // Allow sort
					DefaultErrorMessage = Language.Phrase("IncorrectInteger"),
					IsUpload = false
				};
				TrxUserId.Init(this); // DN
				Fields.Add("TrxUserId", TrxUserId);

				// to_gl_acct
				to_gl_acct = new DbField<SqlDbType> {
					TableVar = "s_dodetltest",
					TableName = "s_dodetltest",
					FieldVar = "x_to_gl_acct",
					Name = "to_gl_acct",
					Expression = "[to_gl_acct]",
					BasicSearchExpression = "[to_gl_acct]",
					Type = 200,
					DbType = SqlDbType.VarChar,
					DateTimeFormat = -1,
					VirtualExpression = "[to_gl_acct]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				to_gl_acct.Init(this); // DN
				Fields.Add("to_gl_acct", to_gl_acct);

				// tax_acct
				tax_acct = new DbField<SqlDbType> {
					TableVar = "s_dodetltest",
					TableName = "s_dodetltest",
					FieldVar = "x_tax_acct",
					Name = "tax_acct",
					Expression = "[tax_acct]",
					BasicSearchExpression = "[tax_acct]",
					Type = 200,
					DbType = SqlDbType.VarChar,
					DateTimeFormat = -1,
					VirtualExpression = "[tax_acct]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				tax_acct.Init(this); // DN
				Fields.Add("tax_acct", tax_acct);
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

			// Current master table name
			public string CurrentMasterTable {
				get => Session.GetString(Config.ProjectName + "_" + TableVar + "_" + Config.TableMasterTable);
				set => Session[Config.ProjectName + "_" + TableVar + "_" + Config.TableMasterTable] = value;
			}

			// Session master where clause
			public string MasterFilter {
				get { // Master filter
					string masterFilter = "";
				if (CurrentMasterTable == "s_dohdrtest") {
					if (!Empty(Id_dohdrTrxId.SessionValue))
						masterFilter += "[TrxId]=" + QuotedValue(Id_dohdrTrxId.SessionValue, Config.DataTypeNumber, "DB");
					else
						return "";
				}
					return masterFilter;
				}
			}

			// Session detail WHERE clause
			public string DetailFilter {
				get { // Detail filter
					string detailFilter = "";
					if (CurrentMasterTable == "s_dohdrtest") {
						if (!Empty(Id_dohdrTrxId.SessionValue))
							detailFilter += "[Id_dohdrTrxId]=" + QuotedValue(Id_dohdrTrxId.SessionValue, Config.DataTypeNumber, "DB");
						else
							return "";
					}
					return detailFilter;
				}
			}

			// Master filter // DN
			public string SqlMasterFilter(string tblvar) {
				if (tblvar == "s_dohdrtest")
					return "[TrxId]=@TrxId@";
				return "";
			}

			// Detail filter // DN
			public string SqlDetailFilter(string tblvar) {
				if (tblvar == "s_dohdrtest")
					return "[Id_dohdrTrxId]=@Id_dohdrTrxId@";
				return "";
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
				get => _sqlFrom ?? "[dbo].[s_dodetltest]";
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
					TrxId.SetDbValue(await Connection.GetLastInsertIdAsync());
					row["TrxId"] = TrxId.DbValue;
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
					fld = FieldByName("TrxId");
					AddFilter(ref filter, fld.Expression + "=" + QuotedValue(row["TrxId"], FieldByName("TrxId").DataType, DbId));
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
				TrxId.SetDbValue(row["TrxId"], false);
				Id_dohdrTrxId.SetDbValue(row["Id_dohdrTrxId"], false);
				item_no.SetDbValue(row["item_no"], false);
				item_type.SetDbValue(row["item_type"], false);
				Id_sodetlTrxId.SetDbValue(row["Id_sodetlTrxId"], false);
				Id_podetlTrxId.SetDbValue(row["Id_podetlTrxId"], false);
				part_code.SetDbValue(row["part_code"], false);
				part_desc.SetDbValue(row["part_desc"], false);
				uom.SetDbValue(row["uom"], false);
				qty.SetDbValue(row["qty"], false);
				unit_price.SetDbValue(row["unit_price"], false);
				amount_origin.SetDbValue(row["amount_origin"], false);
				amount_local.SetDbValue(row["amount_local"], false);
				tax_code.SetDbValue(row["tax_code"], false);
				tax_rate.SetDbValue(row["tax_rate"], false);
				tax_amount_origin.SetDbValue(row["tax_amount_origin"], false);
				tax_amount_local.SetDbValue(row["tax_amount_local"], false);
				TrxUserId.SetDbValue(row["TrxUserId"], false);
				to_gl_acct.SetDbValue(row["to_gl_acct"], false);
				tax_acct.SetDbValue(row["tax_acct"], false);
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
						return "s_dodetltestlist";
					}
				}
				set {
					Session[Config.ProjectName + "_" + TableVar + "_" + Config.TableReturnUrl] = value;
				}
			}

			// Get modal caption
			public string GetModalCaption(string pageName) {
				if (SameString(pageName, "s_dodetltestview"))
					return Language.Phrase("View");
				else if (SameString(pageName, "s_dodetltestedit"))
					return Language.Phrase("Edit");
				else if (SameString(pageName, "s_dodetltestadd"))
					return Language.Phrase("Add");
				else
					return "";
			}

			// List URL
			public string ListUrl => "s_dodetltestlist";

			// View URL
			public string ViewUrl => GetViewUrl();

			// View URL
			public string GetViewUrl(string parm = "") {
				string url = "";
				if (!Empty(parm))
					url = KeyUrl("s_dodetltestview", UrlParm(parm));
				else
					url = KeyUrl("s_dodetltestview", UrlParm(Config.TableShowDetail + "="));
				return AddMasterUrl(url);
			}

			// Add URL
			public string AddUrl { get; set; } = "s_dodetltestadd";

			// Add URL
			public string GetAddUrl(string parm = "") {
				string url = "";
				if (!Empty(parm))
					url = "s_dodetltestadd?" + UrlParm(parm);
				else
					url = "s_dodetltestadd";
				return AppPath(AddMasterUrl(url));
			}

			// Edit URL
			public string EditUrl => GetEditUrl();

			// Edit URL (with parameter)
			public string GetEditUrl(string parm = "") {
				string url = "";
				url = KeyUrl("s_dodetltestedit", UrlParm(parm));
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
				url = KeyUrl("s_dodetltestadd", UrlParm(parm));
				return AppPath(AddMasterUrl(url)); // DN
			}

			// Inline copy URL
			public string InlineCopyUrl =>
				AppPath(AddMasterUrl(KeyUrl(CurrentPageName(), UrlParm("action=copy")))); // DN

			// Delete URL
			public string DeleteUrl =>
				AppPath(KeyUrl("s_dodetltestdelete", UrlParm())); // DN

			// Add master URL
			public string AddMasterUrl(string url) {
				if (CurrentMasterTable == "s_dohdrtest" && !url.Contains(Config.TableShowMaster + "=")) {
					url += (url.Contains("?") ? "&" : "?") + Config.TableShowMaster + "=" + CurrentMasterTable;
					url += "&fk_TrxId=" + UrlEncode(Id_dohdrTrxId.CurrentValue);
				}
				return url;
			}

			// Get primary key as JSON
			public string KeyToJson() {
				string json = "";
				json += "TrxId:" + ConvertToJson(TrxId.CurrentValue, "number", true);
				return "{" + json + "}";
			}

			// Add key value to URL
			public string KeyUrl(string url, string parm = "") { // DN
				if (!IsDBNull(TrxId.CurrentValue)) {
					url += "/" + TrxId.CurrentValue;
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
					if (RouteValues.TryGetValue("TrxId", out rv)) { // TrxId
						key = Convert.ToString(rv);
					} else if (IsApi() && !Empty(keyValues)) {
						key = keyValues[0];
					} else {
						key = Param("TrxId");
					}
					keysList.Add(key);
				}

				// Check keys
				foreach (var keys in keysList) {
					if (!IsNumeric(keys)) // TrxId
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
					TrxId.CurrentValue = keys;
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
			private string _sqlKeyFilter => "[TrxId] = @TrxId@";

			#pragma warning disable 168

			// Get record filter
			public string GetRecordFilter(Dictionary<string, object> row = null)
			{
				string keyFilter = _sqlKeyFilter;
				object val, result;
				val = !Empty(row) ? (row.TryGetValue("TrxId", out result) ? result : null) : TrxId.CurrentValue;
				if (!IsNumeric(val))
					return "0=1"; // Invalid key
				if (val == null)
					return "0=1"; // Invalid key
				else
					keyFilter = keyFilter.Replace("@TrxId@", AdjustSql(val, DbId)); // Replace key value
				return keyFilter;
			}

			#pragma warning restore 168

			// Load row values from recordset
			public void LoadListRowValues(DbDataReader rs) {
				TrxId.SetDbValue(rs["TrxId"]);
				Id_dohdrTrxId.SetDbValue(rs["Id_dohdrTrxId"]);
				item_no.SetDbValue(rs["item_no"]);
				item_type.SetDbValue(rs["item_type"]);
				Id_sodetlTrxId.SetDbValue(rs["Id_sodetlTrxId"]);
				Id_podetlTrxId.SetDbValue(rs["Id_podetlTrxId"]);
				part_code.SetDbValue(rs["part_code"]);
				part_desc.SetDbValue(rs["part_desc"]);
				uom.SetDbValue(rs["uom"]);
				qty.SetDbValue(rs["qty"]);
				unit_price.SetDbValue(rs["unit_price"]);
				amount_origin.SetDbValue(rs["amount_origin"]);
				amount_local.SetDbValue(rs["amount_local"]);
				tax_code.SetDbValue(rs["tax_code"]);
				tax_rate.SetDbValue(rs["tax_rate"]);
				tax_amount_origin.SetDbValue(rs["tax_amount_origin"]);
				tax_amount_local.SetDbValue(rs["tax_amount_local"]);
				TrxUserId.SetDbValue(rs["TrxUserId"]);
				to_gl_acct.SetDbValue(rs["to_gl_acct"]);
				tax_acct.SetDbValue(rs["tax_acct"]);
			}

			#pragma warning disable 1998

			// Render list row values
			public async Task RenderListRow() {

				// Call Row Rendering event
				Row_Rendering();

				// Common render codes
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

				// TrxId
				TrxId.HrefValue = "";
				TrxId.TooltipValue = "";

				// Id_dohdrTrxId
				Id_dohdrTrxId.HrefValue = "";
				Id_dohdrTrxId.TooltipValue = "";

				// item_no
				item_no.HrefValue = "";
				item_no.TooltipValue = "";

				// item_type
				item_type.HrefValue = "";
				item_type.TooltipValue = "";

				// Id_sodetlTrxId
				Id_sodetlTrxId.HrefValue = "";
				Id_sodetlTrxId.TooltipValue = "";

				// Id_podetlTrxId
				Id_podetlTrxId.HrefValue = "";
				Id_podetlTrxId.TooltipValue = "";

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

				// amount_local
				amount_local.HrefValue = "";
				amount_local.TooltipValue = "";

				// tax_code
				tax_code.HrefValue = "";
				tax_code.TooltipValue = "";

				// tax_rate
				tax_rate.HrefValue = "";
				tax_rate.TooltipValue = "";

				// tax_amount_origin
				tax_amount_origin.HrefValue = "";
				tax_amount_origin.TooltipValue = "";

				// tax_amount_local
				tax_amount_local.HrefValue = "";
				tax_amount_local.TooltipValue = "";

				// TrxUserId
				TrxUserId.HrefValue = "";
				TrxUserId.TooltipValue = "";

				// to_gl_acct
				to_gl_acct.HrefValue = "";
				to_gl_acct.TooltipValue = "";

				// tax_acct
				tax_acct.HrefValue = "";
				tax_acct.TooltipValue = "";

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

				// TrxId
				TrxId.EditAttrs["class"] = "form-control";
				TrxId.EditValue = TrxId.CurrentValue;

				// Id_dohdrTrxId
				Id_dohdrTrxId.EditAttrs["class"] = "form-control";
				if (!Empty(Id_dohdrTrxId.SessionValue)) {
					Id_dohdrTrxId.CurrentValue = Id_dohdrTrxId.SessionValue;
				Id_dohdrTrxId.ViewValue = Id_dohdrTrxId.CurrentValue;
				Id_dohdrTrxId.ViewValue = FormatNumber(Id_dohdrTrxId.ViewValue, 0, -2, -2, -2);
				} else {
				Id_dohdrTrxId.EditValue = Id_dohdrTrxId.CurrentValue; // DN
				Id_dohdrTrxId.PlaceHolder = RemoveHtml(Id_dohdrTrxId.Caption);
				}

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

				// Id_sodetlTrxId
				Id_sodetlTrxId.EditAttrs["class"] = "form-control";
				Id_sodetlTrxId.EditValue = Id_sodetlTrxId.CurrentValue; // DN
				Id_sodetlTrxId.PlaceHolder = RemoveHtml(Id_sodetlTrxId.Caption);

				// Id_podetlTrxId
				Id_podetlTrxId.EditAttrs["class"] = "form-control";
				Id_podetlTrxId.EditValue = Id_podetlTrxId.CurrentValue; // DN
				Id_podetlTrxId.PlaceHolder = RemoveHtml(Id_podetlTrxId.Caption);

				// part_code
				part_code.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					part_code.CurrentValue = HtmlDecode(part_code.CurrentValue);
				part_code.EditValue = part_code.CurrentValue; // DN
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
				if (!Empty(qty.EditValue) && IsNumeric(qty.EditValue))
					qty.EditValue = FormatNumber(qty.EditValue, -2, -2, -2, -2);

				// unit_price
				unit_price.EditAttrs["class"] = "form-control";
				unit_price.EditValue = unit_price.CurrentValue; // DN
				unit_price.PlaceHolder = RemoveHtml(unit_price.Caption);
				if (!Empty(unit_price.EditValue) && IsNumeric(unit_price.EditValue))
					unit_price.EditValue = FormatNumber(unit_price.EditValue, -2, -2, -2, -2);

				// amount_origin
				amount_origin.EditAttrs["class"] = "form-control";
				amount_origin.EditValue = amount_origin.CurrentValue; // DN
				amount_origin.PlaceHolder = RemoveHtml(amount_origin.Caption);
				if (!Empty(amount_origin.EditValue) && IsNumeric(amount_origin.EditValue))
					amount_origin.EditValue = FormatNumber(amount_origin.EditValue, -2, -2, -2, -2);

				// amount_local
				amount_local.EditAttrs["class"] = "form-control";
				amount_local.EditValue = amount_local.CurrentValue; // DN
				amount_local.PlaceHolder = RemoveHtml(amount_local.Caption);
				if (!Empty(amount_local.EditValue) && IsNumeric(amount_local.EditValue))
					amount_local.EditValue = FormatNumber(amount_local.EditValue, -2, -2, -2, -2);

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
				if (!Empty(tax_rate.EditValue) && IsNumeric(tax_rate.EditValue))
					tax_rate.EditValue = FormatNumber(tax_rate.EditValue, -2, -2, -2, -2);

				// tax_amount_origin
				tax_amount_origin.EditAttrs["class"] = "form-control";
				tax_amount_origin.EditValue = tax_amount_origin.CurrentValue; // DN
				tax_amount_origin.PlaceHolder = RemoveHtml(tax_amount_origin.Caption);
				if (!Empty(tax_amount_origin.EditValue) && IsNumeric(tax_amount_origin.EditValue))
					tax_amount_origin.EditValue = FormatNumber(tax_amount_origin.EditValue, -2, -2, -2, -2);

				// tax_amount_local
				tax_amount_local.EditAttrs["class"] = "form-control";
				tax_amount_local.EditValue = tax_amount_local.CurrentValue; // DN
				tax_amount_local.PlaceHolder = RemoveHtml(tax_amount_local.Caption);
				if (!Empty(tax_amount_local.EditValue) && IsNumeric(tax_amount_local.EditValue))
					tax_amount_local.EditValue = FormatNumber(tax_amount_local.EditValue, -2, -2, -2, -2);

				// TrxUserId
				TrxUserId.EditAttrs["class"] = "form-control";
				TrxUserId.EditValue = TrxUserId.CurrentValue; // DN
				TrxUserId.PlaceHolder = RemoveHtml(TrxUserId.Caption);

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
							doc.ExportCaption(item_no);
							doc.ExportCaption(item_type);
							doc.ExportCaption(part_code);
							doc.ExportCaption(part_desc);
							doc.ExportCaption(uom);
							doc.ExportCaption(qty);
							doc.ExportCaption(unit_price);
							doc.ExportCaption(amount_origin);
							doc.ExportCaption(tax_code);
							doc.ExportCaption(tax_rate);
							doc.ExportCaption(tax_amount_origin);
							doc.ExportCaption(to_gl_acct);
							doc.ExportCaption(tax_acct);
						} else {
							doc.ExportCaption(TrxId);
							doc.ExportCaption(Id_dohdrTrxId);
							doc.ExportCaption(item_no);
							doc.ExportCaption(item_type);
							doc.ExportCaption(Id_sodetlTrxId);
							doc.ExportCaption(Id_podetlTrxId);
							doc.ExportCaption(part_code);
							doc.ExportCaption(part_desc);
							doc.ExportCaption(uom);
							doc.ExportCaption(qty);
							doc.ExportCaption(unit_price);
							doc.ExportCaption(amount_origin);
							doc.ExportCaption(amount_local);
							doc.ExportCaption(tax_code);
							doc.ExportCaption(tax_rate);
							doc.ExportCaption(tax_amount_origin);
							doc.ExportCaption(tax_amount_local);
							doc.ExportCaption(TrxUserId);
							doc.ExportCaption(to_gl_acct);
							doc.ExportCaption(tax_acct);
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
								await doc.ExportField(item_no);
								await doc.ExportField(item_type);
								await doc.ExportField(part_code);
								await doc.ExportField(part_desc);
								await doc.ExportField(uom);
								await doc.ExportField(qty);
								await doc.ExportField(unit_price);
								await doc.ExportField(amount_origin);
								await doc.ExportField(tax_code);
								await doc.ExportField(tax_rate);
								await doc.ExportField(tax_amount_origin);
								await doc.ExportField(to_gl_acct);
								await doc.ExportField(tax_acct);
							} else {
								await doc.ExportField(TrxId);
								await doc.ExportField(Id_dohdrTrxId);
								await doc.ExportField(item_no);
								await doc.ExportField(item_type);
								await doc.ExportField(Id_sodetlTrxId);
								await doc.ExportField(Id_podetlTrxId);
								await doc.ExportField(part_code);
								await doc.ExportField(part_desc);
								await doc.ExportField(uom);
								await doc.ExportField(qty);
								await doc.ExportField(unit_price);
								await doc.ExportField(amount_origin);
								await doc.ExportField(amount_local);
								await doc.ExportField(tax_code);
								await doc.ExportField(tax_rate);
								await doc.ExportField(tax_amount_origin);
								await doc.ExportField(tax_amount_local);
								await doc.ExportField(TrxUserId);
								await doc.ExportField(to_gl_acct);
								await doc.ExportField(tax_acct);
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
