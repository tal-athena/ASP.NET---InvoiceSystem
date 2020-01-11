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
		/// s_argltrx
		/// </summary>

		public static _s_argltrx s_argltrx {
			get => HttpData.GetOrCreate<_s_argltrx>("s_argltrx");
			set => HttpData["s_argltrx"] = value;
		}

		/// <summary>
		/// Table class for s_argltrx
		/// </summary>

		public class _s_argltrx: DbTable {
			public int RowCnt = 0; // DN
			public bool UseSessionForListSql = true;

			// Column CSS classes
			public string LeftColumnClass = "col-sm-2 col-form-label ew-label";
			public string RightColumnClass = "col-sm-10";
			public string OffsetColumnClass = "col-sm-10 offset-sm-2";
			public string TableLeftColumnClass = "w-col-2";
			public readonly DbField<SqlDbType> dt_rec;
			public readonly DbField<SqlDbType> doc_date;
			public readonly DbField<SqlDbType> dbcode;
			public readonly DbField<SqlDbType> ref_no;
			public readonly DbField<SqlDbType> db_cr;
			public readonly DbField<SqlDbType> to_gl_acct;
			public readonly DbField<SqlDbType> to_gl_dept;
			public readonly DbField<SqlDbType> amt_gl;
			public readonly DbField<SqlDbType> month;
			public readonly DbField<SqlDbType> source;
			public readonly DbField<SqlDbType> forx_amt;
			public readonly DbField<SqlDbType> markdelete;
			public readonly DbField<SqlDbType> trn_no;
			public readonly DbField<SqlDbType> prefix;
			public readonly DbField<SqlDbType> exrate;
			public readonly DbField<SqlDbType> clear_fp;
			public readonly DbField<SqlDbType> note;
			public readonly DbField<SqlDbType> dt_upd;
			public readonly DbField<SqlDbType> id_upd;
			public readonly DbField<SqlDbType> rowid;
			public readonly DbField<SqlDbType> tax_code;
			public readonly DbField<SqlDbType> tax_rate;
			public readonly DbField<SqlDbType> gst_date;
			public readonly DbField<SqlDbType> tax_value;

			// Constructor
			public _s_argltrx() {

				// Language object // DN
				Language = Language ?? new Lang();
				TableVar = "s_argltrx";
				Name = "s_argltrx";
				Type = "TABLE";

				// Update Table
				UpdateTable = "[dbo].[s_argltrx]";
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

				// dt_rec
				dt_rec = new DbField<SqlDbType> {
					TableVar = "s_argltrx",
					TableName = "s_argltrx",
					FieldVar = "x_dt_rec",
					Name = "dt_rec",
					Expression = "[dt_rec]",
					BasicSearchExpression = CastDateFieldForLike("[dt_rec]", 0, "DB"),
					Type = 135,
					DbType = SqlDbType.DateTime,
					DateTimeFormat = 0,
					VirtualExpression = "[dt_rec]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Nullable = false, // NOT NULL field
					Required = true, // Required field
					Sortable = true, // Allow sort
					DefaultErrorMessage = Language.Phrase("IncorrectDate").Replace("%s", DateFormat),
					IsUpload = false
				};
				dt_rec.Init(this); // DN
				Fields.Add("dt_rec", dt_rec);

				// doc_date
				doc_date = new DbField<SqlDbType> {
					TableVar = "s_argltrx",
					TableName = "s_argltrx",
					FieldVar = "x_doc_date",
					Name = "doc_date",
					Expression = "[doc_date]",
					BasicSearchExpression = CastDateFieldForLike("[doc_date]", 0, "DB"),
					Type = 135,
					DbType = SqlDbType.DateTime,
					DateTimeFormat = 0,
					VirtualExpression = "[doc_date]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Nullable = false, // NOT NULL field
					Required = true, // Required field
					Sortable = true, // Allow sort
					DefaultErrorMessage = Language.Phrase("IncorrectDate").Replace("%s", DateFormat),
					IsUpload = false
				};
				doc_date.Init(this); // DN
				Fields.Add("doc_date", doc_date);

				// dbcode
				dbcode = new DbField<SqlDbType> {
					TableVar = "s_argltrx",
					TableName = "s_argltrx",
					FieldVar = "x_dbcode",
					Name = "dbcode",
					Expression = "[dbcode]",
					BasicSearchExpression = "[dbcode]",
					Type = 129,
					DbType = SqlDbType.Char,
					DateTimeFormat = -1,
					VirtualExpression = "[dbcode]",
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
				dbcode.Init(this); // DN
				Fields.Add("dbcode", dbcode);

				// ref_no
				ref_no = new DbField<SqlDbType> {
					TableVar = "s_argltrx",
					TableName = "s_argltrx",
					FieldVar = "x_ref_no",
					Name = "ref_no",
					Expression = "[ref_no]",
					BasicSearchExpression = "[ref_no]",
					Type = 129,
					DbType = SqlDbType.Char,
					DateTimeFormat = -1,
					VirtualExpression = "[ref_no]",
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
				ref_no.Init(this); // DN
				Fields.Add("ref_no", ref_no);

				// db_cr
				db_cr = new DbField<SqlDbType> {
					TableVar = "s_argltrx",
					TableName = "s_argltrx",
					FieldVar = "x_db_cr",
					Name = "db_cr",
					Expression = "[db_cr]",
					BasicSearchExpression = "[db_cr]",
					Type = 129,
					DbType = SqlDbType.Char,
					DateTimeFormat = -1,
					VirtualExpression = "[db_cr]",
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
				db_cr.Init(this); // DN
				Fields.Add("db_cr", db_cr);

				// to_gl_acct
				to_gl_acct = new DbField<SqlDbType> {
					TableVar = "s_argltrx",
					TableName = "s_argltrx",
					FieldVar = "x_to_gl_acct",
					Name = "to_gl_acct",
					Expression = "[to_gl_acct]",
					BasicSearchExpression = "[to_gl_acct]",
					Type = 129,
					DbType = SqlDbType.Char,
					DateTimeFormat = -1,
					VirtualExpression = "[to_gl_acct]",
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
				to_gl_acct.Init(this); // DN
				Fields.Add("to_gl_acct", to_gl_acct);

				// to_gl_dept
				to_gl_dept = new DbField<SqlDbType> {
					TableVar = "s_argltrx",
					TableName = "s_argltrx",
					FieldVar = "x_to_gl_dept",
					Name = "to_gl_dept",
					Expression = "[to_gl_dept]",
					BasicSearchExpression = "[to_gl_dept]",
					Type = 129,
					DbType = SqlDbType.Char,
					DateTimeFormat = -1,
					VirtualExpression = "[to_gl_dept]",
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
				to_gl_dept.Init(this); // DN
				Fields.Add("to_gl_dept", to_gl_dept);

				// amt_gl
				amt_gl = new DbField<SqlDbType> {
					TableVar = "s_argltrx",
					TableName = "s_argltrx",
					FieldVar = "x_amt_gl",
					Name = "amt_gl",
					Expression = "[amt_gl]",
					BasicSearchExpression = "CAST([amt_gl] AS NVARCHAR)",
					Type = 131,
					DbType = SqlDbType.Decimal,
					DateTimeFormat = -1,
					VirtualExpression = "[amt_gl]",
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
				amt_gl.Init(this); // DN
				Fields.Add("amt_gl", amt_gl);

				// month
				month = new DbField<SqlDbType> {
					TableVar = "s_argltrx",
					TableName = "s_argltrx",
					FieldVar = "x_month",
					Name = "month",
					Expression = "[month]",
					BasicSearchExpression = "[month]",
					Type = 129,
					DbType = SqlDbType.Char,
					DateTimeFormat = -1,
					VirtualExpression = "[month]",
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
				month.Init(this); // DN
				Fields.Add("month", month);

				// source
				source = new DbField<SqlDbType> {
					TableVar = "s_argltrx",
					TableName = "s_argltrx",
					FieldVar = "x_source",
					Name = "source",
					Expression = "[source]",
					BasicSearchExpression = "[source]",
					Type = 129,
					DbType = SqlDbType.Char,
					DateTimeFormat = -1,
					VirtualExpression = "[source]",
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
				source.Init(this); // DN
				Fields.Add("source", source);

				// forx_amt
				forx_amt = new DbField<SqlDbType> {
					TableVar = "s_argltrx",
					TableName = "s_argltrx",
					FieldVar = "x_forx_amt",
					Name = "forx_amt",
					Expression = "[forx_amt]",
					BasicSearchExpression = "CAST([forx_amt] AS NVARCHAR)",
					Type = 131,
					DbType = SqlDbType.Decimal,
					DateTimeFormat = -1,
					VirtualExpression = "[forx_amt]",
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
				forx_amt.Init(this); // DN
				Fields.Add("forx_amt", forx_amt);

				// markdelete
				markdelete = new DbField<SqlDbType> {
					TableVar = "s_argltrx",
					TableName = "s_argltrx",
					FieldVar = "x_markdelete",
					Name = "markdelete",
					Expression = "[markdelete]",
					BasicSearchExpression = "[markdelete]",
					Type = 11,
					DbType = SqlDbType.Bit,
					DateTimeFormat = -1,
					VirtualExpression = "[markdelete]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "CHECKBOX",
					Nullable = false, // NOT NULL field
					Sortable = true, // Allow sort
					DataType = Config.DataTypeBoolean,
					OptionCount = 2,
					IsUpload = false
				};
				markdelete.Init(this); // DN
				markdelete.Lookup = new Lookup<DbField>("markdelete", "s_argltrx", false, "", new List<string> {"", "", "", ""}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "");
				Fields.Add("markdelete", markdelete);

				// trn_no
				trn_no = new DbField<SqlDbType> {
					TableVar = "s_argltrx",
					TableName = "s_argltrx",
					FieldVar = "x_trn_no",
					Name = "trn_no",
					Expression = "[trn_no]",
					BasicSearchExpression = "CAST([trn_no] AS NVARCHAR)",
					Type = 3,
					DbType = SqlDbType.Int,
					DateTimeFormat = -1,
					VirtualExpression = "[trn_no]",
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
				trn_no.Init(this); // DN
				Fields.Add("trn_no", trn_no);

				// prefix
				prefix = new DbField<SqlDbType> {
					TableVar = "s_argltrx",
					TableName = "s_argltrx",
					FieldVar = "x_prefix",
					Name = "prefix",
					Expression = "[prefix]",
					BasicSearchExpression = "[prefix]",
					Type = 129,
					DbType = SqlDbType.Char,
					DateTimeFormat = -1,
					VirtualExpression = "[prefix]",
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
				prefix.Init(this); // DN
				Fields.Add("prefix", prefix);

				// exrate
				exrate = new DbField<SqlDbType> {
					TableVar = "s_argltrx",
					TableName = "s_argltrx",
					FieldVar = "x_exrate",
					Name = "exrate",
					Expression = "[exrate]",
					BasicSearchExpression = "CAST([exrate] AS NVARCHAR)",
					Type = 131,
					DbType = SqlDbType.Decimal,
					DateTimeFormat = -1,
					VirtualExpression = "[exrate]",
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
				exrate.Init(this); // DN
				Fields.Add("exrate", exrate);

				// clear_fp
				clear_fp = new DbField<SqlDbType> {
					TableVar = "s_argltrx",
					TableName = "s_argltrx",
					FieldVar = "x_clear_fp",
					Name = "clear_fp",
					Expression = "[clear_fp]",
					BasicSearchExpression = "[clear_fp]",
					Type = 129,
					DbType = SqlDbType.Char,
					DateTimeFormat = -1,
					VirtualExpression = "[clear_fp]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				clear_fp.Init(this); // DN
				Fields.Add("clear_fp", clear_fp);

				// note
				note = new DbField<SqlDbType> {
					TableVar = "s_argltrx",
					TableName = "s_argltrx",
					FieldVar = "x_note",
					Name = "note",
					Expression = "[note]",
					BasicSearchExpression = "[note]",
					Type = 129,
					DbType = SqlDbType.Char,
					DateTimeFormat = -1,
					VirtualExpression = "[note]",
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
				note.Init(this); // DN
				Fields.Add("note", note);

				// dt_upd
				dt_upd = new DbField<SqlDbType> {
					TableVar = "s_argltrx",
					TableName = "s_argltrx",
					FieldVar = "x_dt_upd",
					Name = "dt_upd",
					Expression = "[dt_upd]",
					BasicSearchExpression = CastDateFieldForLike("[dt_upd]", 0, "DB"),
					Type = 135,
					DbType = SqlDbType.DateTime,
					DateTimeFormat = 0,
					VirtualExpression = "[dt_upd]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Nullable = false, // NOT NULL field
					Required = true, // Required field
					Sortable = true, // Allow sort
					DefaultErrorMessage = Language.Phrase("IncorrectDate").Replace("%s", DateFormat),
					IsUpload = false
				};
				dt_upd.Init(this); // DN
				Fields.Add("dt_upd", dt_upd);

				// id_upd
				id_upd = new DbField<SqlDbType> {
					TableVar = "s_argltrx",
					TableName = "s_argltrx",
					FieldVar = "x_id_upd",
					Name = "id_upd",
					Expression = "[id_upd]",
					BasicSearchExpression = "[id_upd]",
					Type = 129,
					DbType = SqlDbType.Char,
					DateTimeFormat = -1,
					VirtualExpression = "[id_upd]",
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
				id_upd.Init(this); // DN
				Fields.Add("id_upd", id_upd);

				// rowid
				rowid = new DbField<SqlDbType> {
					TableVar = "s_argltrx",
					TableName = "s_argltrx",
					FieldVar = "x_rowid",
					Name = "rowid",
					Expression = "[rowid]",
					BasicSearchExpression = "CAST([rowid] AS NVARCHAR)",
					Type = 3,
					DbType = SqlDbType.Int,
					DateTimeFormat = -1,
					VirtualExpression = "[rowid]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "NO",
					IsAutoIncrement = true, // Autoincrement field
					Nullable = false, // NOT NULL field
					Sortable = true, // Allow sort
					DefaultErrorMessage = Language.Phrase("IncorrectInteger"),
					IsUpload = false
				};
				rowid.Init(this); // DN
				Fields.Add("rowid", rowid);

				// tax_code
				tax_code = new DbField<SqlDbType> {
					TableVar = "s_argltrx",
					TableName = "s_argltrx",
					FieldVar = "x_tax_code",
					Name = "tax_code",
					Expression = "[tax_code]",
					BasicSearchExpression = "[tax_code]",
					Type = 129,
					DbType = SqlDbType.Char,
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
					TableVar = "s_argltrx",
					TableName = "s_argltrx",
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

				// gst_date
				gst_date = new DbField<SqlDbType> {
					TableVar = "s_argltrx",
					TableName = "s_argltrx",
					FieldVar = "x_gst_date",
					Name = "gst_date",
					Expression = "[gst_date]",
					BasicSearchExpression = CastDateFieldForLike("[gst_date]", 0, "DB"),
					Type = 135,
					DbType = SqlDbType.DateTime,
					DateTimeFormat = 0,
					VirtualExpression = "[gst_date]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Sortable = true, // Allow sort
					DefaultErrorMessage = Language.Phrase("IncorrectDate").Replace("%s", DateFormat),
					IsUpload = false
				};
				gst_date.Init(this); // DN
				Fields.Add("gst_date", gst_date);

				// tax_value
				tax_value = new DbField<SqlDbType> {
					TableVar = "s_argltrx",
					TableName = "s_argltrx",
					FieldVar = "x_tax_value",
					Name = "tax_value",
					Expression = "[tax_value]",
					BasicSearchExpression = "CAST([tax_value] AS NVARCHAR)",
					Type = 131,
					DbType = SqlDbType.Decimal,
					DateTimeFormat = -1,
					VirtualExpression = "[tax_value]",
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
				tax_value.Init(this); // DN
				tax_value.GetDefault = () => 0;
				Fields.Add("tax_value", tax_value);
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
				get => _sqlFrom ?? "[dbo].[s_argltrx]";
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
					rowid.SetDbValue(await Connection.GetLastInsertIdAsync());
					row["rowid"] = rowid.DbValue;
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
				dt_rec.SetDbValue(row["dt_rec"], false);
				doc_date.SetDbValue(row["doc_date"], false);
				dbcode.SetDbValue(row["dbcode"], false);
				ref_no.SetDbValue(row["ref_no"], false);
				db_cr.SetDbValue(row["db_cr"], false);
				to_gl_acct.SetDbValue(row["to_gl_acct"], false);
				to_gl_dept.SetDbValue(row["to_gl_dept"], false);
				amt_gl.SetDbValue(row["amt_gl"], false);
				month.SetDbValue(row["month"], false);
				source.SetDbValue(row["source"], false);
				forx_amt.SetDbValue(row["forx_amt"], false);
				markdelete.SetDbValue((ConvertToBool(row["markdelete"]) ? "1" : "0"), false);
				trn_no.SetDbValue(row["trn_no"], false);
				prefix.SetDbValue(row["prefix"], false);
				exrate.SetDbValue(row["exrate"], false);
				clear_fp.SetDbValue(row["clear_fp"], false);
				note.SetDbValue(row["note"], false);
				dt_upd.SetDbValue(row["dt_upd"], false);
				id_upd.SetDbValue(row["id_upd"], false);
				rowid.SetDbValue(row["rowid"], false);
				tax_code.SetDbValue(row["tax_code"], false);
				tax_rate.SetDbValue(row["tax_rate"], false);
				gst_date.SetDbValue(row["gst_date"], false);
				tax_value.SetDbValue(row["tax_value"], false);
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
						return "s_argltrxlist";
					}
				}
				set {
					Session[Config.ProjectName + "_" + TableVar + "_" + Config.TableReturnUrl] = value;
				}
			}

			// Get modal caption
			public string GetModalCaption(string pageName) {
				if (SameString(pageName, "s_argltrxview"))
					return Language.Phrase("View");
				else if (SameString(pageName, "s_argltrxedit"))
					return Language.Phrase("Edit");
				else if (SameString(pageName, "s_argltrxadd"))
					return Language.Phrase("Add");
				else
					return "";
			}

			// List URL
			public string ListUrl => "s_argltrxlist";

			// View URL
			public string ViewUrl => GetViewUrl();

			// View URL
			public string GetViewUrl(string parm = "") {
				string url = "";
				if (!Empty(parm))
					url = KeyUrl("s_argltrxview", UrlParm(parm));
				else
					url = KeyUrl("s_argltrxview", UrlParm(Config.TableShowDetail + "="));
				return AddMasterUrl(url);
			}

			// Add URL
			public string AddUrl { get; set; } = "s_argltrxadd";

			// Add URL
			public string GetAddUrl(string parm = "") {
				string url = "";
				if (!Empty(parm))
					url = "s_argltrxadd?" + UrlParm(parm);
				else
					url = "s_argltrxadd";
				return AppPath(AddMasterUrl(url));
			}

			// Edit URL
			public string EditUrl => GetEditUrl();

			// Edit URL (with parameter)
			public string GetEditUrl(string parm = "") {
				string url = "";
				url = KeyUrl("s_argltrxedit", UrlParm(parm));
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
				url = KeyUrl("s_argltrxadd", UrlParm(parm));
				return AppPath(AddMasterUrl(url)); // DN
			}

			// Inline copy URL
			public string InlineCopyUrl =>
				AppPath(AddMasterUrl(KeyUrl(CurrentPageName(), UrlParm("action=copy")))); // DN

			// Delete URL
			public string DeleteUrl =>
				AppPath(KeyUrl("s_argltrxdelete", UrlParm())); // DN

			// Add master URL
			public string AddMasterUrl(string url) {
				return url;
			}

			// Get primary key as JSON
			public string KeyToJson() {
				string json = "";
				return "{" + json + "}";
			}

			// Add key value to URL
			public string KeyUrl(string url, string parm = "") { // DN
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
					string[] keyValues = null;
					object rv;
					if (IsApi() && RouteValues.TryGetValue("key", out object k))
						keyValues = k.ToString().Split('/');
				}

				// Check keys
				foreach (var keys in keysList) {
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
			private string _sqlKeyFilter => "";

			#pragma warning disable 168

			// Get record filter
			public string GetRecordFilter(Dictionary<string, object> row = null)
			{
				string keyFilter = _sqlKeyFilter;
				object val, result;
				return keyFilter;
			}

			#pragma warning restore 168

			// Load row values from recordset
			public void LoadListRowValues(DbDataReader rs) {
				dt_rec.SetDbValue(rs["dt_rec"]);
				doc_date.SetDbValue(rs["doc_date"]);
				dbcode.SetDbValue(rs["dbcode"]);
				ref_no.SetDbValue(rs["ref_no"]);
				db_cr.SetDbValue(rs["db_cr"]);
				to_gl_acct.SetDbValue(rs["to_gl_acct"]);
				to_gl_dept.SetDbValue(rs["to_gl_dept"]);
				amt_gl.SetDbValue(rs["amt_gl"]);
				month.SetDbValue(rs["month"]);
				source.SetDbValue(rs["source"]);
				forx_amt.SetDbValue(rs["forx_amt"]);
				markdelete.SetDbValue(ConvertToBool(rs["markdelete"]) ? "1" : "0");
				trn_no.SetDbValue(rs["trn_no"]);
				prefix.SetDbValue(rs["prefix"]);
				exrate.SetDbValue(rs["exrate"]);
				clear_fp.SetDbValue(rs["clear_fp"]);
				note.SetDbValue(rs["note"]);
				dt_upd.SetDbValue(rs["dt_upd"]);
				id_upd.SetDbValue(rs["id_upd"]);
				rowid.SetDbValue(rs["rowid"]);
				tax_code.SetDbValue(rs["tax_code"]);
				tax_rate.SetDbValue(rs["tax_rate"]);
				gst_date.SetDbValue(rs["gst_date"]);
				tax_value.SetDbValue(rs["tax_value"]);
			}

			#pragma warning disable 1998

			// Render list row values
			public async Task RenderListRow() {

				// Call Row Rendering event
				Row_Rendering();

				// Common render codes
				// dt_rec
				// doc_date
				// dbcode
				// ref_no
				// db_cr
				// to_gl_acct
				// to_gl_dept
				// amt_gl
				// month
				// source
				// forx_amt
				// markdelete
				// trn_no
				// prefix
				// exrate
				// clear_fp
				// note
				// dt_upd
				// id_upd
				// rowid
				// tax_code
				// tax_rate
				// gst_date
				// tax_value
				// dt_rec

				dt_rec.ViewValue = dt_rec.CurrentValue;
				dt_rec.ViewValue = FormatDateTime(dt_rec.ViewValue, 0);

				// doc_date
				doc_date.ViewValue = doc_date.CurrentValue;
				doc_date.ViewValue = FormatDateTime(doc_date.ViewValue, 0);

				// dbcode
				dbcode.ViewValue = dbcode.CurrentValue;

				// ref_no
				ref_no.ViewValue = ref_no.CurrentValue;

				// db_cr
				db_cr.ViewValue = db_cr.CurrentValue;

				// to_gl_acct
				to_gl_acct.ViewValue = to_gl_acct.CurrentValue;

				// to_gl_dept
				to_gl_dept.ViewValue = to_gl_dept.CurrentValue;

				// amt_gl
				amt_gl.ViewValue = amt_gl.CurrentValue;
				amt_gl.ViewValue = FormatNumber(amt_gl.ViewValue, 2, -2, -2, -2);

				// month
				month.ViewValue = month.CurrentValue;

				// source
				source.ViewValue = source.CurrentValue;

				// forx_amt
				forx_amt.ViewValue = forx_amt.CurrentValue;
				forx_amt.ViewValue = FormatNumber(forx_amt.ViewValue, 2, -2, -2, -2);

				// markdelete
				if (ConvertToBool(markdelete.CurrentValue)) {
					markdelete.ViewValue = (markdelete.TagCaption(1) != "") ? markdelete.TagCaption(1) : "Yes";
				} else {
					markdelete.ViewValue = (markdelete.TagCaption(2) != "") ? markdelete.TagCaption(2) : "No";
				}

				// trn_no
				trn_no.ViewValue = trn_no.CurrentValue;
				trn_no.ViewValue = FormatNumber(trn_no.ViewValue, 0, -2, -2, -2);

				// prefix
				prefix.ViewValue = prefix.CurrentValue;

				// exrate
				exrate.ViewValue = exrate.CurrentValue;
				exrate.ViewValue = FormatNumber(exrate.ViewValue, 2, -2, -2, -2);

				// clear_fp
				clear_fp.ViewValue = clear_fp.CurrentValue;

				// note
				note.ViewValue = note.CurrentValue;

				// dt_upd
				dt_upd.ViewValue = dt_upd.CurrentValue;
				dt_upd.ViewValue = FormatDateTime(dt_upd.ViewValue, 0);

				// id_upd
				id_upd.ViewValue = id_upd.CurrentValue;

				// rowid
				rowid.ViewValue = rowid.CurrentValue;

				// tax_code
				tax_code.ViewValue = tax_code.CurrentValue;

				// tax_rate
				tax_rate.ViewValue = tax_rate.CurrentValue;
				tax_rate.ViewValue = FormatNumber(tax_rate.ViewValue, 2, -2, -2, -2);

				// gst_date
				gst_date.ViewValue = gst_date.CurrentValue;
				gst_date.ViewValue = FormatDateTime(gst_date.ViewValue, 0);

				// tax_value
				tax_value.ViewValue = tax_value.CurrentValue;
				tax_value.ViewValue = FormatNumber(tax_value.ViewValue, 2, -2, -2, -2);

				// dt_rec
				dt_rec.HrefValue = "";
				dt_rec.TooltipValue = "";

				// doc_date
				doc_date.HrefValue = "";
				doc_date.TooltipValue = "";

				// dbcode
				dbcode.HrefValue = "";
				dbcode.TooltipValue = "";

				// ref_no
				ref_no.HrefValue = "";
				ref_no.TooltipValue = "";

				// db_cr
				db_cr.HrefValue = "";
				db_cr.TooltipValue = "";

				// to_gl_acct
				to_gl_acct.HrefValue = "";
				to_gl_acct.TooltipValue = "";

				// to_gl_dept
				to_gl_dept.HrefValue = "";
				to_gl_dept.TooltipValue = "";

				// amt_gl
				amt_gl.HrefValue = "";
				amt_gl.TooltipValue = "";

				// month
				month.HrefValue = "";
				month.TooltipValue = "";

				// source
				source.HrefValue = "";
				source.TooltipValue = "";

				// forx_amt
				forx_amt.HrefValue = "";
				forx_amt.TooltipValue = "";

				// markdelete
				markdelete.HrefValue = "";
				markdelete.TooltipValue = "";

				// trn_no
				trn_no.HrefValue = "";
				trn_no.TooltipValue = "";

				// prefix
				prefix.HrefValue = "";
				prefix.TooltipValue = "";

				// exrate
				exrate.HrefValue = "";
				exrate.TooltipValue = "";

				// clear_fp
				clear_fp.HrefValue = "";
				clear_fp.TooltipValue = "";

				// note
				note.HrefValue = "";
				note.TooltipValue = "";

				// dt_upd
				dt_upd.HrefValue = "";
				dt_upd.TooltipValue = "";

				// id_upd
				id_upd.HrefValue = "";
				id_upd.TooltipValue = "";

				// rowid
				rowid.HrefValue = "";
				rowid.TooltipValue = "";

				// tax_code
				tax_code.HrefValue = "";
				tax_code.TooltipValue = "";

				// tax_rate
				tax_rate.HrefValue = "";
				tax_rate.TooltipValue = "";

				// gst_date
				gst_date.HrefValue = "";
				gst_date.TooltipValue = "";

				// tax_value
				tax_value.HrefValue = "";
				tax_value.TooltipValue = "";

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

				// dt_rec
				dt_rec.EditAttrs["class"] = "form-control";
				dt_rec.EditValue = FormatDateTime(dt_rec.CurrentValue, 8); // DN
				dt_rec.PlaceHolder = RemoveHtml(dt_rec.Caption);

				// doc_date
				doc_date.EditAttrs["class"] = "form-control";
				doc_date.EditValue = FormatDateTime(doc_date.CurrentValue, 8); // DN
				doc_date.PlaceHolder = RemoveHtml(doc_date.Caption);

				// dbcode
				dbcode.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					dbcode.CurrentValue = HtmlDecode(dbcode.CurrentValue);
				dbcode.EditValue = dbcode.CurrentValue; // DN
				dbcode.PlaceHolder = RemoveHtml(dbcode.Caption);

				// ref_no
				ref_no.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					ref_no.CurrentValue = HtmlDecode(ref_no.CurrentValue);
				ref_no.EditValue = ref_no.CurrentValue; // DN
				ref_no.PlaceHolder = RemoveHtml(ref_no.Caption);

				// db_cr
				db_cr.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					db_cr.CurrentValue = HtmlDecode(db_cr.CurrentValue);
				db_cr.EditValue = db_cr.CurrentValue; // DN
				db_cr.PlaceHolder = RemoveHtml(db_cr.Caption);

				// to_gl_acct
				to_gl_acct.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					to_gl_acct.CurrentValue = HtmlDecode(to_gl_acct.CurrentValue);
				to_gl_acct.EditValue = to_gl_acct.CurrentValue; // DN
				to_gl_acct.PlaceHolder = RemoveHtml(to_gl_acct.Caption);

				// to_gl_dept
				to_gl_dept.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					to_gl_dept.CurrentValue = HtmlDecode(to_gl_dept.CurrentValue);
				to_gl_dept.EditValue = to_gl_dept.CurrentValue; // DN
				to_gl_dept.PlaceHolder = RemoveHtml(to_gl_dept.Caption);

				// amt_gl
				amt_gl.EditAttrs["class"] = "form-control";
				amt_gl.EditValue = amt_gl.CurrentValue; // DN
				amt_gl.PlaceHolder = RemoveHtml(amt_gl.Caption);
				if (!Empty(amt_gl.EditValue) && IsNumeric(amt_gl.EditValue))
					amt_gl.EditValue = FormatNumber(amt_gl.EditValue, -2, -2, -2, -2);

				// month
				month.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					month.CurrentValue = HtmlDecode(month.CurrentValue);
				month.EditValue = month.CurrentValue; // DN
				month.PlaceHolder = RemoveHtml(month.Caption);

				// source
				source.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					source.CurrentValue = HtmlDecode(source.CurrentValue);
				source.EditValue = source.CurrentValue; // DN
				source.PlaceHolder = RemoveHtml(source.Caption);

				// forx_amt
				forx_amt.EditAttrs["class"] = "form-control";
				forx_amt.EditValue = forx_amt.CurrentValue; // DN
				forx_amt.PlaceHolder = RemoveHtml(forx_amt.Caption);
				if (!Empty(forx_amt.EditValue) && IsNumeric(forx_amt.EditValue))
					forx_amt.EditValue = FormatNumber(forx_amt.EditValue, -2, -2, -2, -2);

				// markdelete
				markdelete.EditValue = markdelete.Options(false);

				// trn_no
				trn_no.EditAttrs["class"] = "form-control";
				trn_no.EditValue = trn_no.CurrentValue; // DN
				trn_no.PlaceHolder = RemoveHtml(trn_no.Caption);

				// prefix
				prefix.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					prefix.CurrentValue = HtmlDecode(prefix.CurrentValue);
				prefix.EditValue = prefix.CurrentValue; // DN
				prefix.PlaceHolder = RemoveHtml(prefix.Caption);

				// exrate
				exrate.EditAttrs["class"] = "form-control";
				exrate.EditValue = exrate.CurrentValue; // DN
				exrate.PlaceHolder = RemoveHtml(exrate.Caption);
				if (!Empty(exrate.EditValue) && IsNumeric(exrate.EditValue))
					exrate.EditValue = FormatNumber(exrate.EditValue, -2, -2, -2, -2);

				// clear_fp
				clear_fp.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					clear_fp.CurrentValue = HtmlDecode(clear_fp.CurrentValue);
				clear_fp.EditValue = clear_fp.CurrentValue; // DN
				clear_fp.PlaceHolder = RemoveHtml(clear_fp.Caption);

				// note
				note.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					note.CurrentValue = HtmlDecode(note.CurrentValue);
				note.EditValue = note.CurrentValue; // DN
				note.PlaceHolder = RemoveHtml(note.Caption);

				// dt_upd
				dt_upd.EditAttrs["class"] = "form-control";
				dt_upd.EditValue = FormatDateTime(dt_upd.CurrentValue, 8); // DN
				dt_upd.PlaceHolder = RemoveHtml(dt_upd.Caption);

				// id_upd
				id_upd.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					id_upd.CurrentValue = HtmlDecode(id_upd.CurrentValue);
				id_upd.EditValue = id_upd.CurrentValue; // DN
				id_upd.PlaceHolder = RemoveHtml(id_upd.Caption);

				// rowid
				rowid.EditAttrs["class"] = "form-control";
				rowid.EditValue = rowid.CurrentValue; // DN
				rowid.PlaceHolder = RemoveHtml(rowid.Caption);

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

				// gst_date
				gst_date.EditAttrs["class"] = "form-control";
				gst_date.EditValue = FormatDateTime(gst_date.CurrentValue, 8); // DN
				gst_date.PlaceHolder = RemoveHtml(gst_date.Caption);

				// tax_value
				tax_value.EditAttrs["class"] = "form-control";
				tax_value.EditValue = tax_value.CurrentValue; // DN
				tax_value.PlaceHolder = RemoveHtml(tax_value.Caption);
				if (!Empty(tax_value.EditValue) && IsNumeric(tax_value.EditValue))
					tax_value.EditValue = FormatNumber(tax_value.EditValue, -2, -2, -2, -2);

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
							doc.ExportCaption(dt_rec);
							doc.ExportCaption(doc_date);
							doc.ExportCaption(dbcode);
							doc.ExportCaption(ref_no);
							doc.ExportCaption(db_cr);
							doc.ExportCaption(to_gl_acct);
							doc.ExportCaption(to_gl_dept);
							doc.ExportCaption(amt_gl);
							doc.ExportCaption(month);
							doc.ExportCaption(source);
							doc.ExportCaption(forx_amt);
							doc.ExportCaption(markdelete);
							doc.ExportCaption(trn_no);
							doc.ExportCaption(prefix);
							doc.ExportCaption(exrate);
							doc.ExportCaption(clear_fp);
							doc.ExportCaption(note);
							doc.ExportCaption(dt_upd);
							doc.ExportCaption(id_upd);
							doc.ExportCaption(rowid);
							doc.ExportCaption(tax_code);
							doc.ExportCaption(tax_rate);
							doc.ExportCaption(gst_date);
							doc.ExportCaption(tax_value);
						} else {
							doc.ExportCaption(dt_rec);
							doc.ExportCaption(doc_date);
							doc.ExportCaption(dbcode);
							doc.ExportCaption(ref_no);
							doc.ExportCaption(db_cr);
							doc.ExportCaption(to_gl_acct);
							doc.ExportCaption(to_gl_dept);
							doc.ExportCaption(amt_gl);
							doc.ExportCaption(month);
							doc.ExportCaption(source);
							doc.ExportCaption(forx_amt);
							doc.ExportCaption(markdelete);
							doc.ExportCaption(trn_no);
							doc.ExportCaption(prefix);
							doc.ExportCaption(exrate);
							doc.ExportCaption(clear_fp);
							doc.ExportCaption(note);
							doc.ExportCaption(dt_upd);
							doc.ExportCaption(id_upd);
							doc.ExportCaption(rowid);
							doc.ExportCaption(tax_code);
							doc.ExportCaption(tax_rate);
							doc.ExportCaption(gst_date);
							doc.ExportCaption(tax_value);
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
								await doc.ExportField(dt_rec);
								await doc.ExportField(doc_date);
								await doc.ExportField(dbcode);
								await doc.ExportField(ref_no);
								await doc.ExportField(db_cr);
								await doc.ExportField(to_gl_acct);
								await doc.ExportField(to_gl_dept);
								await doc.ExportField(amt_gl);
								await doc.ExportField(month);
								await doc.ExportField(source);
								await doc.ExportField(forx_amt);
								await doc.ExportField(markdelete);
								await doc.ExportField(trn_no);
								await doc.ExportField(prefix);
								await doc.ExportField(exrate);
								await doc.ExportField(clear_fp);
								await doc.ExportField(note);
								await doc.ExportField(dt_upd);
								await doc.ExportField(id_upd);
								await doc.ExportField(rowid);
								await doc.ExportField(tax_code);
								await doc.ExportField(tax_rate);
								await doc.ExportField(gst_date);
								await doc.ExportField(tax_value);
							} else {
								await doc.ExportField(dt_rec);
								await doc.ExportField(doc_date);
								await doc.ExportField(dbcode);
								await doc.ExportField(ref_no);
								await doc.ExportField(db_cr);
								await doc.ExportField(to_gl_acct);
								await doc.ExportField(to_gl_dept);
								await doc.ExportField(amt_gl);
								await doc.ExportField(month);
								await doc.ExportField(source);
								await doc.ExportField(forx_amt);
								await doc.ExportField(markdelete);
								await doc.ExportField(trn_no);
								await doc.ExportField(prefix);
								await doc.ExportField(exrate);
								await doc.ExportField(clear_fp);
								await doc.ExportField(note);
								await doc.ExportField(dt_upd);
								await doc.ExportField(id_upd);
								await doc.ExportField(rowid);
								await doc.ExportField(tax_code);
								await doc.ExportField(tax_rate);
								await doc.ExportField(gst_date);
								await doc.ExportField(tax_value);
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
