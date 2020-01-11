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
		/// s_artrans
		/// </summary>

		public static _s_artrans s_artrans {
			get => HttpData.GetOrCreate<_s_artrans>("s_artrans");
			set => HttpData["s_artrans"] = value;
		}

		/// <summary>
		/// Table class for s_artrans
		/// </summary>

		public class _s_artrans: DbTable {
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
			public readonly DbField<SqlDbType> ar_dept;
			public readonly DbField<SqlDbType> ref_no;
			public readonly DbField<SqlDbType> description;
			public readonly DbField<SqlDbType> amt_ar;
			public readonly DbField<SqlDbType> amt_rec;
			public readonly DbField<SqlDbType> ar_gl_acct;
			public readonly DbField<SqlDbType> ar_gl_dept;
			public readonly DbField<SqlDbType> db_cr;
			public readonly DbField<SqlDbType> month;
			public readonly DbField<SqlDbType> source;
			public readonly DbField<SqlDbType> forx_amt;
			public readonly DbField<SqlDbType> forx_rec;
			public readonly DbField<SqlDbType> forx_cost;
			public readonly DbField<SqlDbType> gain_loss;
			public readonly DbField<SqlDbType> ex_gl_ac;
			public readonly DbField<SqlDbType> ex_gl_dp;
			public readonly DbField<SqlDbType> cost;
			public readonly DbField<SqlDbType> prefix;
			public readonly DbField<SqlDbType> exrate;
			public readonly DbField<SqlDbType> trn_no;
			public readonly DbField<SqlDbType> rpl_trn_no;
			public readonly DbField<SqlDbType> slsman;
			public readonly DbField<SqlDbType> markdelete;
			public readonly DbField<SqlDbType> dt_upd;
			public readonly DbField<SqlDbType> id_upd;

			// Constructor
			public _s_artrans() {

				// Language object // DN
				Language = Language ?? new Lang();
				TableVar = "s_artrans";
				Name = "s_artrans";
				Type = "TABLE";

				// Update Table
				UpdateTable = "[dbo].[s_artrans]";
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
					TableVar = "s_artrans",
					TableName = "s_artrans",
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
					TableVar = "s_artrans",
					TableName = "s_artrans",
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
					TableVar = "s_artrans",
					TableName = "s_artrans",
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

				// ar_dept
				ar_dept = new DbField<SqlDbType> {
					TableVar = "s_artrans",
					TableName = "s_artrans",
					FieldVar = "x_ar_dept",
					Name = "ar_dept",
					Expression = "[ar_dept]",
					BasicSearchExpression = "[ar_dept]",
					Type = 129,
					DbType = SqlDbType.Char,
					DateTimeFormat = -1,
					VirtualExpression = "[ar_dept]",
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
				ar_dept.Init(this); // DN
				Fields.Add("ar_dept", ar_dept);

				// ref_no
				ref_no = new DbField<SqlDbType> {
					TableVar = "s_artrans",
					TableName = "s_artrans",
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

				// description
				description = new DbField<SqlDbType> {
					TableVar = "s_artrans",
					TableName = "s_artrans",
					FieldVar = "x_description",
					Name = "description",
					Expression = "[description]",
					BasicSearchExpression = "[description]",
					Type = 129,
					DbType = SqlDbType.Char,
					DateTimeFormat = -1,
					VirtualExpression = "[description]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				description.Init(this); // DN
				Fields.Add("description", description);

				// amt_ar
				amt_ar = new DbField<SqlDbType> {
					TableVar = "s_artrans",
					TableName = "s_artrans",
					FieldVar = "x_amt_ar",
					Name = "amt_ar",
					Expression = "[amt_ar]",
					BasicSearchExpression = "CAST([amt_ar] AS NVARCHAR)",
					Type = 131,
					DbType = SqlDbType.Decimal,
					DateTimeFormat = -1,
					VirtualExpression = "[amt_ar]",
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
				amt_ar.Init(this); // DN
				amt_ar.GetDefault = () => 0;
				Fields.Add("amt_ar", amt_ar);

				// amt_rec
				amt_rec = new DbField<SqlDbType> {
					TableVar = "s_artrans",
					TableName = "s_artrans",
					FieldVar = "x_amt_rec",
					Name = "amt_rec",
					Expression = "[amt_rec]",
					BasicSearchExpression = "CAST([amt_rec] AS NVARCHAR)",
					Type = 131,
					DbType = SqlDbType.Decimal,
					DateTimeFormat = -1,
					VirtualExpression = "[amt_rec]",
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
				amt_rec.Init(this); // DN
				amt_rec.GetDefault = () => 0;
				Fields.Add("amt_rec", amt_rec);

				// ar_gl_acct
				ar_gl_acct = new DbField<SqlDbType> {
					TableVar = "s_artrans",
					TableName = "s_artrans",
					FieldVar = "x_ar_gl_acct",
					Name = "ar_gl_acct",
					Expression = "[ar_gl_acct]",
					BasicSearchExpression = "[ar_gl_acct]",
					Type = 129,
					DbType = SqlDbType.Char,
					DateTimeFormat = -1,
					VirtualExpression = "[ar_gl_acct]",
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
				ar_gl_acct.Init(this); // DN
				Fields.Add("ar_gl_acct", ar_gl_acct);

				// ar_gl_dept
				ar_gl_dept = new DbField<SqlDbType> {
					TableVar = "s_artrans",
					TableName = "s_artrans",
					FieldVar = "x_ar_gl_dept",
					Name = "ar_gl_dept",
					Expression = "[ar_gl_dept]",
					BasicSearchExpression = "[ar_gl_dept]",
					Type = 129,
					DbType = SqlDbType.Char,
					DateTimeFormat = -1,
					VirtualExpression = "[ar_gl_dept]",
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
				ar_gl_dept.Init(this); // DN
				Fields.Add("ar_gl_dept", ar_gl_dept);

				// db_cr
				db_cr = new DbField<SqlDbType> {
					TableVar = "s_artrans",
					TableName = "s_artrans",
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

				// month
				month = new DbField<SqlDbType> {
					TableVar = "s_artrans",
					TableName = "s_artrans",
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
					TableVar = "s_artrans",
					TableName = "s_artrans",
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
					TableVar = "s_artrans",
					TableName = "s_artrans",
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
				forx_amt.GetDefault = () => 0;
				Fields.Add("forx_amt", forx_amt);

				// forx_rec
				forx_rec = new DbField<SqlDbType> {
					TableVar = "s_artrans",
					TableName = "s_artrans",
					FieldVar = "x_forx_rec",
					Name = "forx_rec",
					Expression = "[forx_rec]",
					BasicSearchExpression = "CAST([forx_rec] AS NVARCHAR)",
					Type = 131,
					DbType = SqlDbType.Decimal,
					DateTimeFormat = -1,
					VirtualExpression = "[forx_rec]",
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
				forx_rec.Init(this); // DN
				forx_rec.GetDefault = () => 0;
				Fields.Add("forx_rec", forx_rec);

				// forx_cost
				forx_cost = new DbField<SqlDbType> {
					TableVar = "s_artrans",
					TableName = "s_artrans",
					FieldVar = "x_forx_cost",
					Name = "forx_cost",
					Expression = "[forx_cost]",
					BasicSearchExpression = "CAST([forx_cost] AS NVARCHAR)",
					Type = 131,
					DbType = SqlDbType.Decimal,
					DateTimeFormat = -1,
					VirtualExpression = "[forx_cost]",
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
				forx_cost.Init(this); // DN
				forx_cost.GetDefault = () => 0;
				Fields.Add("forx_cost", forx_cost);

				// gain_loss
				gain_loss = new DbField<SqlDbType> {
					TableVar = "s_artrans",
					TableName = "s_artrans",
					FieldVar = "x_gain_loss",
					Name = "gain_loss",
					Expression = "[gain_loss]",
					BasicSearchExpression = "CAST([gain_loss] AS NVARCHAR)",
					Type = 131,
					DbType = SqlDbType.Decimal,
					DateTimeFormat = -1,
					VirtualExpression = "[gain_loss]",
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
				gain_loss.Init(this); // DN
				gain_loss.GetDefault = () => 0;
				Fields.Add("gain_loss", gain_loss);

				// ex_gl_ac
				ex_gl_ac = new DbField<SqlDbType> {
					TableVar = "s_artrans",
					TableName = "s_artrans",
					FieldVar = "x_ex_gl_ac",
					Name = "ex_gl_ac",
					Expression = "[ex_gl_ac]",
					BasicSearchExpression = "[ex_gl_ac]",
					Type = 129,
					DbType = SqlDbType.Char,
					DateTimeFormat = -1,
					VirtualExpression = "[ex_gl_ac]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				ex_gl_ac.Init(this); // DN
				ex_gl_ac.GetDefault = () => " ";
				Fields.Add("ex_gl_ac", ex_gl_ac);

				// ex_gl_dp
				ex_gl_dp = new DbField<SqlDbType> {
					TableVar = "s_artrans",
					TableName = "s_artrans",
					FieldVar = "x_ex_gl_dp",
					Name = "ex_gl_dp",
					Expression = "[ex_gl_dp]",
					BasicSearchExpression = "[ex_gl_dp]",
					Type = 129,
					DbType = SqlDbType.Char,
					DateTimeFormat = -1,
					VirtualExpression = "[ex_gl_dp]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				ex_gl_dp.Init(this); // DN
				ex_gl_dp.GetDefault = () => " ";
				Fields.Add("ex_gl_dp", ex_gl_dp);

				// cost
				cost = new DbField<SqlDbType> {
					TableVar = "s_artrans",
					TableName = "s_artrans",
					FieldVar = "x_cost",
					Name = "cost",
					Expression = "[cost]",
					BasicSearchExpression = "CAST([cost] AS NVARCHAR)",
					Type = 131,
					DbType = SqlDbType.Decimal,
					DateTimeFormat = -1,
					VirtualExpression = "[cost]",
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
				cost.Init(this); // DN
				cost.GetDefault = () => 0;
				Fields.Add("cost", cost);

				// prefix
				prefix = new DbField<SqlDbType> {
					TableVar = "s_artrans",
					TableName = "s_artrans",
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
					TableVar = "s_artrans",
					TableName = "s_artrans",
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
				exrate.GetDefault = () => 0;
				Fields.Add("exrate", exrate);

				// trn_no
				trn_no = new DbField<SqlDbType> {
					TableVar = "s_artrans",
					TableName = "s_artrans",
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

				// rpl_trn_no
				rpl_trn_no = new DbField<SqlDbType> {
					TableVar = "s_artrans",
					TableName = "s_artrans",
					FieldVar = "x_rpl_trn_no",
					Name = "rpl_trn_no",
					Expression = "[rpl_trn_no]",
					BasicSearchExpression = "CAST([rpl_trn_no] AS NVARCHAR)",
					Type = 3,
					DbType = SqlDbType.Int,
					DateTimeFormat = -1,
					VirtualExpression = "[rpl_trn_no]",
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
				rpl_trn_no.Init(this); // DN
				Fields.Add("rpl_trn_no", rpl_trn_no);

				// slsman
				slsman = new DbField<SqlDbType> {
					TableVar = "s_artrans",
					TableName = "s_artrans",
					FieldVar = "x_slsman",
					Name = "slsman",
					Expression = "[slsman]",
					BasicSearchExpression = "[slsman]",
					Type = 129,
					DbType = SqlDbType.Char,
					DateTimeFormat = -1,
					VirtualExpression = "[slsman]",
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
				slsman.Init(this); // DN
				slsman.GetDefault = () => " ";
				Fields.Add("slsman", slsman);

				// markdelete
				markdelete = new DbField<SqlDbType> {
					TableVar = "s_artrans",
					TableName = "s_artrans",
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
				markdelete.Lookup = new Lookup<DbField>("markdelete", "s_artrans", false, "", new List<string> {"", "", "", ""}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "");
				Fields.Add("markdelete", markdelete);

				// dt_upd
				dt_upd = new DbField<SqlDbType> {
					TableVar = "s_artrans",
					TableName = "s_artrans",
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
					TableVar = "s_artrans",
					TableName = "s_artrans",
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
				get => _sqlFrom ?? "[dbo].[s_artrans]";
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
				ar_dept.SetDbValue(row["ar_dept"], false);
				ref_no.SetDbValue(row["ref_no"], false);
				description.SetDbValue(row["description"], false);
				amt_ar.SetDbValue(row["amt_ar"], false);
				amt_rec.SetDbValue(row["amt_rec"], false);
				ar_gl_acct.SetDbValue(row["ar_gl_acct"], false);
				ar_gl_dept.SetDbValue(row["ar_gl_dept"], false);
				db_cr.SetDbValue(row["db_cr"], false);
				month.SetDbValue(row["month"], false);
				source.SetDbValue(row["source"], false);
				forx_amt.SetDbValue(row["forx_amt"], false);
				forx_rec.SetDbValue(row["forx_rec"], false);
				forx_cost.SetDbValue(row["forx_cost"], false);
				gain_loss.SetDbValue(row["gain_loss"], false);
				ex_gl_ac.SetDbValue(row["ex_gl_ac"], false);
				ex_gl_dp.SetDbValue(row["ex_gl_dp"], false);
				cost.SetDbValue(row["cost"], false);
				prefix.SetDbValue(row["prefix"], false);
				exrate.SetDbValue(row["exrate"], false);
				trn_no.SetDbValue(row["trn_no"], false);
				rpl_trn_no.SetDbValue(row["rpl_trn_no"], false);
				slsman.SetDbValue(row["slsman"], false);
				markdelete.SetDbValue((ConvertToBool(row["markdelete"]) ? "1" : "0"), false);
				dt_upd.SetDbValue(row["dt_upd"], false);
				id_upd.SetDbValue(row["id_upd"], false);
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
						return "s_artranslist";
					}
				}
				set {
					Session[Config.ProjectName + "_" + TableVar + "_" + Config.TableReturnUrl] = value;
				}
			}

			// Get modal caption
			public string GetModalCaption(string pageName) {
				if (SameString(pageName, "s_artransview"))
					return Language.Phrase("View");
				else if (SameString(pageName, "s_artransedit"))
					return Language.Phrase("Edit");
				else if (SameString(pageName, "s_artransadd"))
					return Language.Phrase("Add");
				else
					return "";
			}

			// List URL
			public string ListUrl => "s_artranslist";

			// View URL
			public string ViewUrl => GetViewUrl();

			// View URL
			public string GetViewUrl(string parm = "") {
				string url = "";
				if (!Empty(parm))
					url = KeyUrl("s_artransview", UrlParm(parm));
				else
					url = KeyUrl("s_artransview", UrlParm(Config.TableShowDetail + "="));
				return AddMasterUrl(url);
			}

			// Add URL
			public string AddUrl { get; set; } = "s_artransadd";

			// Add URL
			public string GetAddUrl(string parm = "") {
				string url = "";
				if (!Empty(parm))
					url = "s_artransadd?" + UrlParm(parm);
				else
					url = "s_artransadd";
				return AppPath(AddMasterUrl(url));
			}

			// Edit URL
			public string EditUrl => GetEditUrl();

			// Edit URL (with parameter)
			public string GetEditUrl(string parm = "") {
				string url = "";
				url = KeyUrl("s_artransedit", UrlParm(parm));
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
				url = KeyUrl("s_artransadd", UrlParm(parm));
				return AppPath(AddMasterUrl(url)); // DN
			}

			// Inline copy URL
			public string InlineCopyUrl =>
				AppPath(AddMasterUrl(KeyUrl(CurrentPageName(), UrlParm("action=copy")))); // DN

			// Delete URL
			public string DeleteUrl =>
				AppPath(KeyUrl("s_artransdelete", UrlParm())); // DN

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
				ar_dept.SetDbValue(rs["ar_dept"]);
				ref_no.SetDbValue(rs["ref_no"]);
				description.SetDbValue(rs["description"]);
				amt_ar.SetDbValue(rs["amt_ar"]);
				amt_rec.SetDbValue(rs["amt_rec"]);
				ar_gl_acct.SetDbValue(rs["ar_gl_acct"]);
				ar_gl_dept.SetDbValue(rs["ar_gl_dept"]);
				db_cr.SetDbValue(rs["db_cr"]);
				month.SetDbValue(rs["month"]);
				source.SetDbValue(rs["source"]);
				forx_amt.SetDbValue(rs["forx_amt"]);
				forx_rec.SetDbValue(rs["forx_rec"]);
				forx_cost.SetDbValue(rs["forx_cost"]);
				gain_loss.SetDbValue(rs["gain_loss"]);
				ex_gl_ac.SetDbValue(rs["ex_gl_ac"]);
				ex_gl_dp.SetDbValue(rs["ex_gl_dp"]);
				cost.SetDbValue(rs["cost"]);
				prefix.SetDbValue(rs["prefix"]);
				exrate.SetDbValue(rs["exrate"]);
				trn_no.SetDbValue(rs["trn_no"]);
				rpl_trn_no.SetDbValue(rs["rpl_trn_no"]);
				slsman.SetDbValue(rs["slsman"]);
				markdelete.SetDbValue(ConvertToBool(rs["markdelete"]) ? "1" : "0");
				dt_upd.SetDbValue(rs["dt_upd"]);
				id_upd.SetDbValue(rs["id_upd"]);
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

				// ar_dept
				ar_dept.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					ar_dept.CurrentValue = HtmlDecode(ar_dept.CurrentValue);
				ar_dept.EditValue = ar_dept.CurrentValue; // DN
				ar_dept.PlaceHolder = RemoveHtml(ar_dept.Caption);

				// ref_no
				ref_no.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					ref_no.CurrentValue = HtmlDecode(ref_no.CurrentValue);
				ref_no.EditValue = ref_no.CurrentValue; // DN
				ref_no.PlaceHolder = RemoveHtml(ref_no.Caption);

				// description
				description.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					description.CurrentValue = HtmlDecode(description.CurrentValue);
				description.EditValue = description.CurrentValue; // DN
				description.PlaceHolder = RemoveHtml(description.Caption);

				// amt_ar
				amt_ar.EditAttrs["class"] = "form-control";
				amt_ar.EditValue = amt_ar.CurrentValue; // DN
				amt_ar.PlaceHolder = RemoveHtml(amt_ar.Caption);
				if (!Empty(amt_ar.EditValue) && IsNumeric(amt_ar.EditValue))
					amt_ar.EditValue = FormatNumber(amt_ar.EditValue, -2, -2, -2, -2);

				// amt_rec
				amt_rec.EditAttrs["class"] = "form-control";
				amt_rec.EditValue = amt_rec.CurrentValue; // DN
				amt_rec.PlaceHolder = RemoveHtml(amt_rec.Caption);
				if (!Empty(amt_rec.EditValue) && IsNumeric(amt_rec.EditValue))
					amt_rec.EditValue = FormatNumber(amt_rec.EditValue, -2, -2, -2, -2);

				// ar_gl_acct
				ar_gl_acct.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					ar_gl_acct.CurrentValue = HtmlDecode(ar_gl_acct.CurrentValue);
				ar_gl_acct.EditValue = ar_gl_acct.CurrentValue; // DN
				ar_gl_acct.PlaceHolder = RemoveHtml(ar_gl_acct.Caption);

				// ar_gl_dept
				ar_gl_dept.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					ar_gl_dept.CurrentValue = HtmlDecode(ar_gl_dept.CurrentValue);
				ar_gl_dept.EditValue = ar_gl_dept.CurrentValue; // DN
				ar_gl_dept.PlaceHolder = RemoveHtml(ar_gl_dept.Caption);

				// db_cr
				db_cr.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					db_cr.CurrentValue = HtmlDecode(db_cr.CurrentValue);
				db_cr.EditValue = db_cr.CurrentValue; // DN
				db_cr.PlaceHolder = RemoveHtml(db_cr.Caption);

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

				// forx_rec
				forx_rec.EditAttrs["class"] = "form-control";
				forx_rec.EditValue = forx_rec.CurrentValue; // DN
				forx_rec.PlaceHolder = RemoveHtml(forx_rec.Caption);
				if (!Empty(forx_rec.EditValue) && IsNumeric(forx_rec.EditValue))
					forx_rec.EditValue = FormatNumber(forx_rec.EditValue, -2, -2, -2, -2);

				// forx_cost
				forx_cost.EditAttrs["class"] = "form-control";
				forx_cost.EditValue = forx_cost.CurrentValue; // DN
				forx_cost.PlaceHolder = RemoveHtml(forx_cost.Caption);
				if (!Empty(forx_cost.EditValue) && IsNumeric(forx_cost.EditValue))
					forx_cost.EditValue = FormatNumber(forx_cost.EditValue, -2, -2, -2, -2);

				// gain_loss
				gain_loss.EditAttrs["class"] = "form-control";
				gain_loss.EditValue = gain_loss.CurrentValue; // DN
				gain_loss.PlaceHolder = RemoveHtml(gain_loss.Caption);
				if (!Empty(gain_loss.EditValue) && IsNumeric(gain_loss.EditValue))
					gain_loss.EditValue = FormatNumber(gain_loss.EditValue, -2, -2, -2, -2);

				// ex_gl_ac
				ex_gl_ac.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					ex_gl_ac.CurrentValue = HtmlDecode(ex_gl_ac.CurrentValue);
				ex_gl_ac.EditValue = ex_gl_ac.CurrentValue; // DN
				ex_gl_ac.PlaceHolder = RemoveHtml(ex_gl_ac.Caption);

				// ex_gl_dp
				ex_gl_dp.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					ex_gl_dp.CurrentValue = HtmlDecode(ex_gl_dp.CurrentValue);
				ex_gl_dp.EditValue = ex_gl_dp.CurrentValue; // DN
				ex_gl_dp.PlaceHolder = RemoveHtml(ex_gl_dp.Caption);

				// cost
				cost.EditAttrs["class"] = "form-control";
				cost.EditValue = cost.CurrentValue; // DN
				cost.PlaceHolder = RemoveHtml(cost.Caption);
				if (!Empty(cost.EditValue) && IsNumeric(cost.EditValue))
					cost.EditValue = FormatNumber(cost.EditValue, -2, -2, -2, -2);

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

				// trn_no
				trn_no.EditAttrs["class"] = "form-control";
				trn_no.EditValue = trn_no.CurrentValue; // DN
				trn_no.PlaceHolder = RemoveHtml(trn_no.Caption);

				// rpl_trn_no
				rpl_trn_no.EditAttrs["class"] = "form-control";
				rpl_trn_no.EditValue = rpl_trn_no.CurrentValue; // DN
				rpl_trn_no.PlaceHolder = RemoveHtml(rpl_trn_no.Caption);

				// slsman
				slsman.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					slsman.CurrentValue = HtmlDecode(slsman.CurrentValue);
				slsman.EditValue = slsman.CurrentValue; // DN
				slsman.PlaceHolder = RemoveHtml(slsman.Caption);

				// markdelete
				markdelete.EditValue = markdelete.Options(false);

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
							doc.ExportCaption(ar_dept);
							doc.ExportCaption(ref_no);
							doc.ExportCaption(description);
							doc.ExportCaption(amt_ar);
							doc.ExportCaption(amt_rec);
							doc.ExportCaption(ar_gl_acct);
							doc.ExportCaption(ar_gl_dept);
							doc.ExportCaption(db_cr);
							doc.ExportCaption(month);
							doc.ExportCaption(source);
							doc.ExportCaption(forx_amt);
							doc.ExportCaption(forx_rec);
							doc.ExportCaption(forx_cost);
							doc.ExportCaption(gain_loss);
							doc.ExportCaption(ex_gl_ac);
							doc.ExportCaption(ex_gl_dp);
							doc.ExportCaption(cost);
							doc.ExportCaption(prefix);
							doc.ExportCaption(exrate);
							doc.ExportCaption(trn_no);
							doc.ExportCaption(rpl_trn_no);
							doc.ExportCaption(slsman);
							doc.ExportCaption(markdelete);
							doc.ExportCaption(dt_upd);
							doc.ExportCaption(id_upd);
						} else {
							doc.ExportCaption(dt_rec);
							doc.ExportCaption(doc_date);
							doc.ExportCaption(dbcode);
							doc.ExportCaption(ar_dept);
							doc.ExportCaption(ref_no);
							doc.ExportCaption(description);
							doc.ExportCaption(amt_ar);
							doc.ExportCaption(amt_rec);
							doc.ExportCaption(ar_gl_acct);
							doc.ExportCaption(ar_gl_dept);
							doc.ExportCaption(db_cr);
							doc.ExportCaption(month);
							doc.ExportCaption(source);
							doc.ExportCaption(forx_amt);
							doc.ExportCaption(forx_rec);
							doc.ExportCaption(forx_cost);
							doc.ExportCaption(gain_loss);
							doc.ExportCaption(ex_gl_ac);
							doc.ExportCaption(ex_gl_dp);
							doc.ExportCaption(cost);
							doc.ExportCaption(prefix);
							doc.ExportCaption(exrate);
							doc.ExportCaption(trn_no);
							doc.ExportCaption(rpl_trn_no);
							doc.ExportCaption(slsman);
							doc.ExportCaption(markdelete);
							doc.ExportCaption(dt_upd);
							doc.ExportCaption(id_upd);
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
								await doc.ExportField(ar_dept);
								await doc.ExportField(ref_no);
								await doc.ExportField(description);
								await doc.ExportField(amt_ar);
								await doc.ExportField(amt_rec);
								await doc.ExportField(ar_gl_acct);
								await doc.ExportField(ar_gl_dept);
								await doc.ExportField(db_cr);
								await doc.ExportField(month);
								await doc.ExportField(source);
								await doc.ExportField(forx_amt);
								await doc.ExportField(forx_rec);
								await doc.ExportField(forx_cost);
								await doc.ExportField(gain_loss);
								await doc.ExportField(ex_gl_ac);
								await doc.ExportField(ex_gl_dp);
								await doc.ExportField(cost);
								await doc.ExportField(prefix);
								await doc.ExportField(exrate);
								await doc.ExportField(trn_no);
								await doc.ExportField(rpl_trn_no);
								await doc.ExportField(slsman);
								await doc.ExportField(markdelete);
								await doc.ExportField(dt_upd);
								await doc.ExportField(id_upd);
							} else {
								await doc.ExportField(dt_rec);
								await doc.ExportField(doc_date);
								await doc.ExportField(dbcode);
								await doc.ExportField(ar_dept);
								await doc.ExportField(ref_no);
								await doc.ExportField(description);
								await doc.ExportField(amt_ar);
								await doc.ExportField(amt_rec);
								await doc.ExportField(ar_gl_acct);
								await doc.ExportField(ar_gl_dept);
								await doc.ExportField(db_cr);
								await doc.ExportField(month);
								await doc.ExportField(source);
								await doc.ExportField(forx_amt);
								await doc.ExportField(forx_rec);
								await doc.ExportField(forx_cost);
								await doc.ExportField(gain_loss);
								await doc.ExportField(ex_gl_ac);
								await doc.ExportField(ex_gl_dp);
								await doc.ExportField(cost);
								await doc.ExportField(prefix);
								await doc.ExportField(exrate);
								await doc.ExportField(trn_no);
								await doc.ExportField(rpl_trn_no);
								await doc.ExportField(slsman);
								await doc.ExportField(markdelete);
								await doc.ExportField(dt_upd);
								await doc.ExportField(id_upd);
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
