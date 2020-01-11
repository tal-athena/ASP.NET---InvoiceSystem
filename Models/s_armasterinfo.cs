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
		/// s_armaster
		/// </summary>

		public static _s_armaster s_armaster {
			get => HttpData.GetOrCreate<_s_armaster>("s_armaster");
			set => HttpData["s_armaster"] = value;
		}

		/// <summary>
		/// Table class for s_armaster
		/// </summary>

		public class _s_armaster: DbTable {
			public int RowCnt = 0; // DN
			public bool UseSessionForListSql = true;

			// Column CSS classes
			public string LeftColumnClass = "col-sm-2 col-form-label ew-label";
			public string RightColumnClass = "col-sm-10";
			public string OffsetColumnClass = "col-sm-10 offset-sm-2";
			public string TableLeftColumnClass = "w-col-2";
			public readonly DbField<SqlDbType> Id;
			public readonly DbField<SqlDbType> dbcode;
			public readonly DbField<SqlDbType> name;
			public readonly DbField<SqlDbType> name2;
			public readonly DbField<SqlDbType> short_name;
			public readonly DbField<SqlDbType> add1;
			public readonly DbField<SqlDbType> add2;
			public readonly DbField<SqlDbType> add3;
			public readonly DbField<SqlDbType> add4;
			public readonly DbField<SqlDbType> area;
			public readonly DbField<SqlDbType> postcode;
			public readonly DbField<SqlDbType> state;
			public readonly DbField<SqlDbType> country;
			public readonly DbField<SqlDbType> tel1;
			public readonly DbField<SqlDbType> tel2;
			public readonly DbField<SqlDbType> phone1;
			public readonly DbField<SqlDbType> phone2;
			public readonly DbField<SqlDbType> fax;
			public readonly DbField<SqlDbType> _email;
			public readonly DbField<SqlDbType> ptc1;
			public readonly DbField<SqlDbType> ptc;
			public readonly DbField<SqlDbType> ptc2;
			public readonly DbField<SqlDbType> ar_grp;
			public readonly DbField<SqlDbType> term_code;
			public readonly DbField<SqlDbType> pterm_code;
			public readonly DbField<SqlDbType> cr_limit;
			public readonly DbField<SqlDbType> CurrencyCode;
			public readonly DbField<SqlDbType> slsman;
			public readonly DbField<SqlDbType> ytd_sale;
			public readonly DbField<SqlDbType> ytd_cost;
			public readonly DbField<SqlDbType> ytd_disc;
			public readonly DbField<SqlDbType> ctrl_acct;
			public readonly DbField<SqlDbType> ctrl_dept;
			public readonly DbField<SqlDbType> acct_code;
			public readonly DbField<SqlDbType> acct_dept;
			public readonly DbField<SqlDbType> skip_stmt;
			public readonly DbField<SqlDbType> stop_sale;
			public readonly DbField<SqlDbType> opn_item;
			public readonly DbField<SqlDbType> status;
			public readonly DbField<SqlDbType> tax_desc;
			public readonly DbField<SqlDbType> stax;
			public readonly DbField<SqlDbType> remark;
			public readonly DbField<SqlDbType> inv_fmt;
			public readonly DbField<SqlDbType> do_fmt;
			public readonly DbField<SqlDbType> Ship_Code;
			public readonly DbField<SqlDbType> custtype;
			public readonly DbField<SqlDbType> Acct_BillAcct;
			public readonly DbField<SqlDbType> bill_flag;
			public readonly DbField<SqlDbType> payment_code;
			public readonly DbField<SqlDbType> stax_pct;
			public readonly DbField<SqlDbType> db_part;
			public readonly DbField<SqlDbType> b_code;
			public readonly DbField<SqlDbType> lmw_no;
			public readonly DbField<SqlDbType> cs_code;
			public readonly DbField<SqlDbType> approved;
			public readonly DbField<SqlDbType> oversea;
			public readonly DbField<SqlDbType> defa_disc_pct;
			public readonly DbField<SqlDbType> sellpriceDOM;
			public readonly DbField<SqlDbType> id_upd;
			public readonly DbField<SqlDbType> dt_upd;
			public readonly DbField<SqlDbType> com_regno;

			// Constructor
			public _s_armaster() {

				// Language object // DN
				Language = Language ?? new Lang();
				TableVar = "s_armaster";
				Name = "s_armaster";
				Type = "TABLE";

				// Update Table
				UpdateTable = "[dbo].[s_armaster]";
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
					TableVar = "s_armaster",
					TableName = "s_armaster",
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

				// dbcode
				dbcode = new DbField<SqlDbType> {
					TableVar = "s_armaster",
					TableName = "s_armaster",
					FieldVar = "x_dbcode",
					Name = "dbcode",
					Expression = "[dbcode]",
					BasicSearchExpression = "[dbcode]",
					Type = 200,
					DbType = SqlDbType.VarChar,
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

				// name
				name = new DbField<SqlDbType> {
					TableVar = "s_armaster",
					TableName = "s_armaster",
					FieldVar = "x_name",
					Name = "name",
					Expression = "[name]",
					BasicSearchExpression = "[name]",
					Type = 202,
					DbType = SqlDbType.NVarChar,
					DateTimeFormat = -1,
					VirtualExpression = "[name]",
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
				name.Init(this); // DN
				Fields.Add("name", name);

				// name2
				name2 = new DbField<SqlDbType> {
					TableVar = "s_armaster",
					TableName = "s_armaster",
					FieldVar = "x_name2",
					Name = "name2",
					Expression = "[name2]",
					BasicSearchExpression = "[name2]",
					Type = 202,
					DbType = SqlDbType.NVarChar,
					DateTimeFormat = -1,
					VirtualExpression = "[name2]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				name2.Init(this); // DN
				Fields.Add("name2", name2);

				// short_name
				short_name = new DbField<SqlDbType> {
					TableVar = "s_armaster",
					TableName = "s_armaster",
					FieldVar = "x_short_name",
					Name = "short_name",
					Expression = "[short_name]",
					BasicSearchExpression = "[short_name]",
					Type = 202,
					DbType = SqlDbType.NVarChar,
					DateTimeFormat = -1,
					VirtualExpression = "[short_name]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				short_name.Init(this); // DN
				Fields.Add("short_name", short_name);

				// add1
				add1 = new DbField<SqlDbType> {
					TableVar = "s_armaster",
					TableName = "s_armaster",
					FieldVar = "x_add1",
					Name = "add1",
					Expression = "[add1]",
					BasicSearchExpression = "[add1]",
					Type = 202,
					DbType = SqlDbType.NVarChar,
					DateTimeFormat = -1,
					VirtualExpression = "[add1]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				add1.Init(this); // DN
				Fields.Add("add1", add1);

				// add2
				add2 = new DbField<SqlDbType> {
					TableVar = "s_armaster",
					TableName = "s_armaster",
					FieldVar = "x_add2",
					Name = "add2",
					Expression = "[add2]",
					BasicSearchExpression = "[add2]",
					Type = 202,
					DbType = SqlDbType.NVarChar,
					DateTimeFormat = -1,
					VirtualExpression = "[add2]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				add2.Init(this); // DN
				Fields.Add("add2", add2);

				// add3
				add3 = new DbField<SqlDbType> {
					TableVar = "s_armaster",
					TableName = "s_armaster",
					FieldVar = "x_add3",
					Name = "add3",
					Expression = "[add3]",
					BasicSearchExpression = "[add3]",
					Type = 202,
					DbType = SqlDbType.NVarChar,
					DateTimeFormat = -1,
					VirtualExpression = "[add3]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				add3.Init(this); // DN
				Fields.Add("add3", add3);

				// add4
				add4 = new DbField<SqlDbType> {
					TableVar = "s_armaster",
					TableName = "s_armaster",
					FieldVar = "x_add4",
					Name = "add4",
					Expression = "[add4]",
					BasicSearchExpression = "[add4]",
					Type = 202,
					DbType = SqlDbType.NVarChar,
					DateTimeFormat = -1,
					VirtualExpression = "[add4]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				add4.Init(this); // DN
				Fields.Add("add4", add4);

				// area
				area = new DbField<SqlDbType> {
					TableVar = "s_armaster",
					TableName = "s_armaster",
					FieldVar = "x_area",
					Name = "area",
					Expression = "[area]",
					BasicSearchExpression = "[area]",
					Type = 202,
					DbType = SqlDbType.NVarChar,
					DateTimeFormat = -1,
					VirtualExpression = "[area]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				area.Init(this); // DN
				Fields.Add("area", area);

				// postcode
				postcode = new DbField<SqlDbType> {
					TableVar = "s_armaster",
					TableName = "s_armaster",
					FieldVar = "x_postcode",
					Name = "postcode",
					Expression = "[postcode]",
					BasicSearchExpression = "[postcode]",
					Type = 130,
					DbType = SqlDbType.NChar,
					DateTimeFormat = -1,
					VirtualExpression = "[postcode]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				postcode.Init(this); // DN
				Fields.Add("postcode", postcode);

				// state
				state = new DbField<SqlDbType> {
					TableVar = "s_armaster",
					TableName = "s_armaster",
					FieldVar = "x_state",
					Name = "state",
					Expression = "[state]",
					BasicSearchExpression = "[state]",
					Type = 202,
					DbType = SqlDbType.NVarChar,
					DateTimeFormat = -1,
					VirtualExpression = "[state]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				state.Init(this); // DN
				Fields.Add("state", state);

				// country
				country = new DbField<SqlDbType> {
					TableVar = "s_armaster",
					TableName = "s_armaster",
					FieldVar = "x_country",
					Name = "country",
					Expression = "[country]",
					BasicSearchExpression = "[country]",
					Type = 202,
					DbType = SqlDbType.NVarChar,
					DateTimeFormat = -1,
					VirtualExpression = "[country]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				country.Init(this); // DN
				Fields.Add("country", country);

				// tel1
				tel1 = new DbField<SqlDbType> {
					TableVar = "s_armaster",
					TableName = "s_armaster",
					FieldVar = "x_tel1",
					Name = "tel1",
					Expression = "[tel1]",
					BasicSearchExpression = "[tel1]",
					Type = 202,
					DbType = SqlDbType.NVarChar,
					DateTimeFormat = -1,
					VirtualExpression = "[tel1]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				tel1.Init(this); // DN
				tel1.GetDefault = () => " ";
				Fields.Add("tel1", tel1);

				// tel2
				tel2 = new DbField<SqlDbType> {
					TableVar = "s_armaster",
					TableName = "s_armaster",
					FieldVar = "x_tel2",
					Name = "tel2",
					Expression = "[tel2]",
					BasicSearchExpression = "[tel2]",
					Type = 200,
					DbType = SqlDbType.VarChar,
					DateTimeFormat = -1,
					VirtualExpression = "[tel2]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				tel2.Init(this); // DN
				tel2.GetDefault = () => " ";
				Fields.Add("tel2", tel2);

				// phone1
				phone1 = new DbField<SqlDbType> {
					TableVar = "s_armaster",
					TableName = "s_armaster",
					FieldVar = "x_phone1",
					Name = "phone1",
					Expression = "[phone1]",
					BasicSearchExpression = "[phone1]",
					Type = 200,
					DbType = SqlDbType.VarChar,
					DateTimeFormat = -1,
					VirtualExpression = "[phone1]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				phone1.Init(this); // DN
				Fields.Add("phone1", phone1);

				// phone2
				phone2 = new DbField<SqlDbType> {
					TableVar = "s_armaster",
					TableName = "s_armaster",
					FieldVar = "x_phone2",
					Name = "phone2",
					Expression = "[phone2]",
					BasicSearchExpression = "[phone2]",
					Type = 200,
					DbType = SqlDbType.VarChar,
					DateTimeFormat = -1,
					VirtualExpression = "[phone2]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				phone2.Init(this); // DN
				Fields.Add("phone2", phone2);

				// fax
				fax = new DbField<SqlDbType> {
					TableVar = "s_armaster",
					TableName = "s_armaster",
					FieldVar = "x_fax",
					Name = "fax",
					Expression = "[fax]",
					BasicSearchExpression = "[fax]",
					Type = 200,
					DbType = SqlDbType.VarChar,
					DateTimeFormat = -1,
					VirtualExpression = "[fax]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				fax.Init(this); // DN
				fax.GetDefault = () => " ";
				Fields.Add("fax", fax);

				// _email
				_email = new DbField<SqlDbType> {
					TableVar = "s_armaster",
					TableName = "s_armaster",
					FieldVar = "x__email",
					Name = "email",
					Expression = "[email]",
					BasicSearchExpression = "[email]",
					Type = 200,
					DbType = SqlDbType.VarChar,
					DateTimeFormat = -1,
					VirtualExpression = "[email]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				_email.Init(this); // DN
				Fields.Add("email", _email);

				// ptc1
				ptc1 = new DbField<SqlDbType> {
					TableVar = "s_armaster",
					TableName = "s_armaster",
					FieldVar = "x_ptc1",
					Name = "ptc1",
					Expression = "[ptc1]",
					BasicSearchExpression = "[ptc1]",
					Type = 200,
					DbType = SqlDbType.VarChar,
					DateTimeFormat = -1,
					VirtualExpression = "[ptc1]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				ptc1.Init(this); // DN
				ptc1.GetDefault = () => " ";
				Fields.Add("ptc1", ptc1);

				// ptc
				ptc = new DbField<SqlDbType> {
					TableVar = "s_armaster",
					TableName = "s_armaster",
					FieldVar = "x_ptc",
					Name = "ptc",
					Expression = "[ptc]",
					BasicSearchExpression = "[ptc]",
					Type = 200,
					DbType = SqlDbType.VarChar,
					DateTimeFormat = -1,
					VirtualExpression = "[ptc]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				ptc.Init(this); // DN
				Fields.Add("ptc", ptc);

				// ptc2
				ptc2 = new DbField<SqlDbType> {
					TableVar = "s_armaster",
					TableName = "s_armaster",
					FieldVar = "x_ptc2",
					Name = "ptc2",
					Expression = "[ptc2]",
					BasicSearchExpression = "[ptc2]",
					Type = 200,
					DbType = SqlDbType.VarChar,
					DateTimeFormat = -1,
					VirtualExpression = "[ptc2]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				ptc2.Init(this); // DN
				Fields.Add("ptc2", ptc2);

				// ar_grp
				ar_grp = new DbField<SqlDbType> {
					TableVar = "s_armaster",
					TableName = "s_armaster",
					FieldVar = "x_ar_grp",
					Name = "ar_grp",
					Expression = "[ar_grp]",
					BasicSearchExpression = "[ar_grp]",
					Type = 200,
					DbType = SqlDbType.VarChar,
					DateTimeFormat = -1,
					VirtualExpression = "[ar_grp]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				ar_grp.Init(this); // DN
				Fields.Add("ar_grp", ar_grp);

				// term_code
				term_code = new DbField<SqlDbType> {
					TableVar = "s_armaster",
					TableName = "s_armaster",
					FieldVar = "x_term_code",
					Name = "term_code",
					Expression = "[term_code]",
					BasicSearchExpression = "[term_code]",
					Type = 200,
					DbType = SqlDbType.VarChar,
					DateTimeFormat = -1,
					VirtualExpression = "[term_code]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				term_code.Init(this); // DN
				term_code.GetDefault = () => " ";
				Fields.Add("term_code", term_code);

				// pterm_code
				pterm_code = new DbField<SqlDbType> {
					TableVar = "s_armaster",
					TableName = "s_armaster",
					FieldVar = "x_pterm_code",
					Name = "pterm_code",
					Expression = "[pterm_code]",
					BasicSearchExpression = "[pterm_code]",
					Type = 200,
					DbType = SqlDbType.VarChar,
					DateTimeFormat = -1,
					VirtualExpression = "[pterm_code]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				pterm_code.Init(this); // DN
				Fields.Add("pterm_code", pterm_code);

				// cr_limit
				cr_limit = new DbField<SqlDbType> {
					TableVar = "s_armaster",
					TableName = "s_armaster",
					FieldVar = "x_cr_limit",
					Name = "cr_limit",
					Expression = "[cr_limit]",
					BasicSearchExpression = "CAST([cr_limit] AS NVARCHAR)",
					Type = 131,
					DbType = SqlDbType.Decimal,
					DateTimeFormat = -1,
					VirtualExpression = "[cr_limit]",
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
				cr_limit.Init(this); // DN
				Fields.Add("cr_limit", cr_limit);

				// CurrencyCode
				CurrencyCode = new DbField<SqlDbType> {
					TableVar = "s_armaster",
					TableName = "s_armaster",
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
					Sortable = true, // Allow sort
					IsUpload = false
				};
				CurrencyCode.Init(this); // DN
				Fields.Add("CurrencyCode", CurrencyCode);

				// slsman
				slsman = new DbField<SqlDbType> {
					TableVar = "s_armaster",
					TableName = "s_armaster",
					FieldVar = "x_slsman",
					Name = "slsman",
					Expression = "[slsman]",
					BasicSearchExpression = "[slsman]",
					Type = 200,
					DbType = SqlDbType.VarChar,
					DateTimeFormat = -1,
					VirtualExpression = "[slsman]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				slsman.Init(this); // DN
				Fields.Add("slsman", slsman);

				// ytd_sale
				ytd_sale = new DbField<SqlDbType> {
					TableVar = "s_armaster",
					TableName = "s_armaster",
					FieldVar = "x_ytd_sale",
					Name = "ytd_sale",
					Expression = "[ytd_sale]",
					BasicSearchExpression = "CAST([ytd_sale] AS NVARCHAR)",
					Type = 131,
					DbType = SqlDbType.Decimal,
					DateTimeFormat = -1,
					VirtualExpression = "[ytd_sale]",
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
				ytd_sale.Init(this); // DN
				Fields.Add("ytd_sale", ytd_sale);

				// ytd_cost
				ytd_cost = new DbField<SqlDbType> {
					TableVar = "s_armaster",
					TableName = "s_armaster",
					FieldVar = "x_ytd_cost",
					Name = "ytd_cost",
					Expression = "[ytd_cost]",
					BasicSearchExpression = "CAST([ytd_cost] AS NVARCHAR)",
					Type = 131,
					DbType = SqlDbType.Decimal,
					DateTimeFormat = -1,
					VirtualExpression = "[ytd_cost]",
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
				ytd_cost.Init(this); // DN
				Fields.Add("ytd_cost", ytd_cost);

				// ytd_disc
				ytd_disc = new DbField<SqlDbType> {
					TableVar = "s_armaster",
					TableName = "s_armaster",
					FieldVar = "x_ytd_disc",
					Name = "ytd_disc",
					Expression = "[ytd_disc]",
					BasicSearchExpression = "CAST([ytd_disc] AS NVARCHAR)",
					Type = 131,
					DbType = SqlDbType.Decimal,
					DateTimeFormat = -1,
					VirtualExpression = "[ytd_disc]",
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
				ytd_disc.Init(this); // DN
				Fields.Add("ytd_disc", ytd_disc);

				// ctrl_acct
				ctrl_acct = new DbField<SqlDbType> {
					TableVar = "s_armaster",
					TableName = "s_armaster",
					FieldVar = "x_ctrl_acct",
					Name = "ctrl_acct",
					Expression = "[ctrl_acct]",
					BasicSearchExpression = "[ctrl_acct]",
					Type = 200,
					DbType = SqlDbType.VarChar,
					DateTimeFormat = -1,
					VirtualExpression = "[ctrl_acct]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				ctrl_acct.Init(this); // DN
				Fields.Add("ctrl_acct", ctrl_acct);

				// ctrl_dept
				ctrl_dept = new DbField<SqlDbType> {
					TableVar = "s_armaster",
					TableName = "s_armaster",
					FieldVar = "x_ctrl_dept",
					Name = "ctrl_dept",
					Expression = "[ctrl_dept]",
					BasicSearchExpression = "[ctrl_dept]",
					Type = 200,
					DbType = SqlDbType.VarChar,
					DateTimeFormat = -1,
					VirtualExpression = "[ctrl_dept]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				ctrl_dept.Init(this); // DN
				Fields.Add("ctrl_dept", ctrl_dept);

				// acct_code
				acct_code = new DbField<SqlDbType> {
					TableVar = "s_armaster",
					TableName = "s_armaster",
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
					Sortable = true, // Allow sort
					IsUpload = false
				};
				acct_code.Init(this); // DN
				Fields.Add("acct_code", acct_code);

				// acct_dept
				acct_dept = new DbField<SqlDbType> {
					TableVar = "s_armaster",
					TableName = "s_armaster",
					FieldVar = "x_acct_dept",
					Name = "acct_dept",
					Expression = "[acct_dept]",
					BasicSearchExpression = "[acct_dept]",
					Type = 200,
					DbType = SqlDbType.VarChar,
					DateTimeFormat = -1,
					VirtualExpression = "[acct_dept]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				acct_dept.Init(this); // DN
				Fields.Add("acct_dept", acct_dept);

				// skip_stmt
				skip_stmt = new DbField<SqlDbType> {
					TableVar = "s_armaster",
					TableName = "s_armaster",
					FieldVar = "x_skip_stmt",
					Name = "skip_stmt",
					Expression = "[skip_stmt]",
					BasicSearchExpression = "[skip_stmt]",
					Type = 11,
					DbType = SqlDbType.Bit,
					DateTimeFormat = -1,
					VirtualExpression = "[skip_stmt]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "CHECKBOX",
					Sortable = true, // Allow sort
					DataType = Config.DataTypeBoolean,
					OptionCount = 2,
					IsUpload = false
				};
				skip_stmt.Init(this); // DN
				skip_stmt.Lookup = new Lookup<DbField>("skip_stmt", "s_armaster", false, "", new List<string> {"", "", "", ""}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "");
				Fields.Add("skip_stmt", skip_stmt);

				// stop_sale
				stop_sale = new DbField<SqlDbType> {
					TableVar = "s_armaster",
					TableName = "s_armaster",
					FieldVar = "x_stop_sale",
					Name = "stop_sale",
					Expression = "[stop_sale]",
					BasicSearchExpression = "[stop_sale]",
					Type = 11,
					DbType = SqlDbType.Bit,
					DateTimeFormat = -1,
					VirtualExpression = "[stop_sale]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "CHECKBOX",
					Sortable = true, // Allow sort
					DataType = Config.DataTypeBoolean,
					OptionCount = 2,
					IsUpload = false
				};
				stop_sale.Init(this); // DN
				stop_sale.Lookup = new Lookup<DbField>("stop_sale", "s_armaster", false, "", new List<string> {"", "", "", ""}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "");
				Fields.Add("stop_sale", stop_sale);

				// opn_item
				opn_item = new DbField<SqlDbType> {
					TableVar = "s_armaster",
					TableName = "s_armaster",
					FieldVar = "x_opn_item",
					Name = "opn_item",
					Expression = "[opn_item]",
					BasicSearchExpression = "[opn_item]",
					Type = 11,
					DbType = SqlDbType.Bit,
					DateTimeFormat = -1,
					VirtualExpression = "[opn_item]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "CHECKBOX",
					Sortable = true, // Allow sort
					DataType = Config.DataTypeBoolean,
					OptionCount = 2,
					IsUpload = false
				};
				opn_item.Init(this); // DN
				opn_item.Lookup = new Lookup<DbField>("opn_item", "s_armaster", false, "", new List<string> {"", "", "", ""}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "");
				Fields.Add("opn_item", opn_item);

				// status
				status = new DbField<SqlDbType> {
					TableVar = "s_armaster",
					TableName = "s_armaster",
					FieldVar = "x_status",
					Name = "status",
					Expression = "[status]",
					BasicSearchExpression = "[status]",
					Type = 129,
					DbType = SqlDbType.Char,
					DateTimeFormat = -1,
					VirtualExpression = "[status]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				status.Init(this); // DN
				Fields.Add("status", status);

				// tax_desc
				tax_desc = new DbField<SqlDbType> {
					TableVar = "s_armaster",
					TableName = "s_armaster",
					FieldVar = "x_tax_desc",
					Name = "tax_desc",
					Expression = "[tax_desc]",
					BasicSearchExpression = "[tax_desc]",
					Type = 202,
					DbType = SqlDbType.NVarChar,
					DateTimeFormat = -1,
					VirtualExpression = "[tax_desc]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				tax_desc.Init(this); // DN
				Fields.Add("tax_desc", tax_desc);

				// stax
				stax = new DbField<SqlDbType> {
					TableVar = "s_armaster",
					TableName = "s_armaster",
					FieldVar = "x_stax",
					Name = "stax",
					Expression = "[stax]",
					BasicSearchExpression = "[stax]",
					Type = 202,
					DbType = SqlDbType.NVarChar,
					DateTimeFormat = -1,
					VirtualExpression = "[stax]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				stax.Init(this); // DN
				Fields.Add("stax", stax);

				// remark
				remark = new DbField<SqlDbType> {
					TableVar = "s_armaster",
					TableName = "s_armaster",
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
				Fields.Add("remark", remark);

				// inv_fmt
				inv_fmt = new DbField<SqlDbType> {
					TableVar = "s_armaster",
					TableName = "s_armaster",
					FieldVar = "x_inv_fmt",
					Name = "inv_fmt",
					Expression = "[inv_fmt]",
					BasicSearchExpression = "[inv_fmt]",
					Type = 202,
					DbType = SqlDbType.NVarChar,
					DateTimeFormat = -1,
					VirtualExpression = "[inv_fmt]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				inv_fmt.Init(this); // DN
				Fields.Add("inv_fmt", inv_fmt);

				// do_fmt
				do_fmt = new DbField<SqlDbType> {
					TableVar = "s_armaster",
					TableName = "s_armaster",
					FieldVar = "x_do_fmt",
					Name = "do_fmt",
					Expression = "[do_fmt]",
					BasicSearchExpression = "[do_fmt]",
					Type = 202,
					DbType = SqlDbType.NVarChar,
					DateTimeFormat = -1,
					VirtualExpression = "[do_fmt]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				do_fmt.Init(this); // DN
				Fields.Add("do_fmt", do_fmt);

				// Ship_Code
				Ship_Code = new DbField<SqlDbType> {
					TableVar = "s_armaster",
					TableName = "s_armaster",
					FieldVar = "x_Ship_Code",
					Name = "Ship_Code",
					Expression = "[Ship_Code]",
					BasicSearchExpression = "[Ship_Code]",
					Type = 200,
					DbType = SqlDbType.VarChar,
					DateTimeFormat = -1,
					VirtualExpression = "[Ship_Code]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				Ship_Code.Init(this); // DN
				Fields.Add("Ship_Code", Ship_Code);

				// custtype
				custtype = new DbField<SqlDbType> {
					TableVar = "s_armaster",
					TableName = "s_armaster",
					FieldVar = "x_custtype",
					Name = "custtype",
					Expression = "[custtype]",
					BasicSearchExpression = "[custtype]",
					Type = 200,
					DbType = SqlDbType.VarChar,
					DateTimeFormat = -1,
					VirtualExpression = "[custtype]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				custtype.Init(this); // DN
				Fields.Add("custtype", custtype);

				// Acct_BillAcct
				Acct_BillAcct = new DbField<SqlDbType> {
					TableVar = "s_armaster",
					TableName = "s_armaster",
					FieldVar = "x_Acct_BillAcct",
					Name = "Acct_BillAcct",
					Expression = "[Acct_BillAcct]",
					BasicSearchExpression = "[Acct_BillAcct]",
					Type = 200,
					DbType = SqlDbType.VarChar,
					DateTimeFormat = -1,
					VirtualExpression = "[Acct_BillAcct]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				Acct_BillAcct.Init(this); // DN
				Fields.Add("Acct_BillAcct", Acct_BillAcct);

				// bill_flag
				bill_flag = new DbField<SqlDbType> {
					TableVar = "s_armaster",
					TableName = "s_armaster",
					FieldVar = "x_bill_flag",
					Name = "bill_flag",
					Expression = "[bill_flag]",
					BasicSearchExpression = "[bill_flag]",
					Type = 200,
					DbType = SqlDbType.VarChar,
					DateTimeFormat = -1,
					VirtualExpression = "[bill_flag]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				bill_flag.Init(this); // DN
				Fields.Add("bill_flag", bill_flag);

				// payment_code
				payment_code = new DbField<SqlDbType> {
					TableVar = "s_armaster",
					TableName = "s_armaster",
					FieldVar = "x_payment_code",
					Name = "payment_code",
					Expression = "[payment_code]",
					BasicSearchExpression = "[payment_code]",
					Type = 200,
					DbType = SqlDbType.VarChar,
					DateTimeFormat = -1,
					VirtualExpression = "[payment_code]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				payment_code.Init(this); // DN
				Fields.Add("payment_code", payment_code);

				// stax_pct
				stax_pct = new DbField<SqlDbType> {
					TableVar = "s_armaster",
					TableName = "s_armaster",
					FieldVar = "x_stax_pct",
					Name = "stax_pct",
					Expression = "[stax_pct]",
					BasicSearchExpression = "CAST([stax_pct] AS NVARCHAR)",
					Type = 131,
					DbType = SqlDbType.Decimal,
					DateTimeFormat = -1,
					VirtualExpression = "[stax_pct]",
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
				stax_pct.Init(this); // DN
				Fields.Add("stax_pct", stax_pct);

				// db_part
				db_part = new DbField<SqlDbType> {
					TableVar = "s_armaster",
					TableName = "s_armaster",
					FieldVar = "x_db_part",
					Name = "db_part",
					Expression = "[db_part]",
					BasicSearchExpression = "[db_part]",
					Type = 200,
					DbType = SqlDbType.VarChar,
					DateTimeFormat = -1,
					VirtualExpression = "[db_part]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				db_part.Init(this); // DN
				db_part.GetDefault = () => " ";
				Fields.Add("db_part", db_part);

				// b_code
				b_code = new DbField<SqlDbType> {
					TableVar = "s_armaster",
					TableName = "s_armaster",
					FieldVar = "x_b_code",
					Name = "b_code",
					Expression = "[b_code]",
					BasicSearchExpression = "[b_code]",
					Type = 200,
					DbType = SqlDbType.VarChar,
					DateTimeFormat = -1,
					VirtualExpression = "[b_code]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				b_code.Init(this); // DN
				b_code.GetDefault = () => " ";
				Fields.Add("b_code", b_code);

				// lmw_no
				lmw_no = new DbField<SqlDbType> {
					TableVar = "s_armaster",
					TableName = "s_armaster",
					FieldVar = "x_lmw_no",
					Name = "lmw_no",
					Expression = "[lmw_no]",
					BasicSearchExpression = "[lmw_no]",
					Type = 200,
					DbType = SqlDbType.VarChar,
					DateTimeFormat = -1,
					VirtualExpression = "[lmw_no]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				lmw_no.Init(this); // DN
				lmw_no.GetDefault = () => " ";
				Fields.Add("lmw_no", lmw_no);

				// cs_code
				cs_code = new DbField<SqlDbType> {
					TableVar = "s_armaster",
					TableName = "s_armaster",
					FieldVar = "x_cs_code",
					Name = "cs_code",
					Expression = "[cs_code]",
					BasicSearchExpression = "[cs_code]",
					Type = 200,
					DbType = SqlDbType.VarChar,
					DateTimeFormat = -1,
					VirtualExpression = "[cs_code]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				cs_code.Init(this); // DN
				cs_code.GetDefault = () => " ";
				Fields.Add("cs_code", cs_code);

				// approved
				approved = new DbField<SqlDbType> {
					TableVar = "s_armaster",
					TableName = "s_armaster",
					FieldVar = "x_approved",
					Name = "approved",
					Expression = "[approved]",
					BasicSearchExpression = "[approved]",
					Type = 11,
					DbType = SqlDbType.Bit,
					DateTimeFormat = -1,
					VirtualExpression = "[approved]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "CHECKBOX",
					Sortable = true, // Allow sort
					DataType = Config.DataTypeBoolean,
					OptionCount = 2,
					IsUpload = false
				};
				approved.Init(this); // DN
				approved.Lookup = new Lookup<DbField>("approved", "s_armaster", false, "", new List<string> {"", "", "", ""}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "");
				Fields.Add("approved", approved);

				// oversea
				oversea = new DbField<SqlDbType> {
					TableVar = "s_armaster",
					TableName = "s_armaster",
					FieldVar = "x_oversea",
					Name = "oversea",
					Expression = "[oversea]",
					BasicSearchExpression = "[oversea]",
					Type = 11,
					DbType = SqlDbType.Bit,
					DateTimeFormat = -1,
					VirtualExpression = "[oversea]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "CHECKBOX",
					Sortable = true, // Allow sort
					DataType = Config.DataTypeBoolean,
					OptionCount = 2,
					IsUpload = false
				};
				oversea.Init(this); // DN
				oversea.Lookup = new Lookup<DbField>("oversea", "s_armaster", false, "", new List<string> {"", "", "", ""}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "");
				Fields.Add("oversea", oversea);

				// defa_disc_pct
				defa_disc_pct = new DbField<SqlDbType> {
					TableVar = "s_armaster",
					TableName = "s_armaster",
					FieldVar = "x_defa_disc_pct",
					Name = "defa_disc_pct",
					Expression = "[defa_disc_pct]",
					BasicSearchExpression = "CAST([defa_disc_pct] AS NVARCHAR)",
					Type = 131,
					DbType = SqlDbType.Decimal,
					DateTimeFormat = -1,
					VirtualExpression = "[defa_disc_pct]",
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
				defa_disc_pct.Init(this); // DN
				defa_disc_pct.GetDefault = () => 0;
				Fields.Add("defa_disc_pct", defa_disc_pct);

				// sellpriceDOM
				sellpriceDOM = new DbField<SqlDbType> {
					TableVar = "s_armaster",
					TableName = "s_armaster",
					FieldVar = "x_sellpriceDOM",
					Name = "sellpriceDOM",
					Expression = "[sellpriceDOM]",
					BasicSearchExpression = "[sellpriceDOM]",
					Type = 11,
					DbType = SqlDbType.Bit,
					DateTimeFormat = -1,
					VirtualExpression = "[sellpriceDOM]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "CHECKBOX",
					Sortable = true, // Allow sort
					DataType = Config.DataTypeBoolean,
					OptionCount = 2,
					IsUpload = false
				};
				sellpriceDOM.Init(this); // DN
				sellpriceDOM.Lookup = new Lookup<DbField>("sellpriceDOM", "s_armaster", false, "", new List<string> {"", "", "", ""}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "");
				Fields.Add("sellpriceDOM", sellpriceDOM);

				// id_upd
				id_upd = new DbField<SqlDbType> {
					TableVar = "s_armaster",
					TableName = "s_armaster",
					FieldVar = "x_id_upd",
					Name = "id_upd",
					Expression = "[id_upd]",
					BasicSearchExpression = "[id_upd]",
					Type = 200,
					DbType = SqlDbType.VarChar,
					DateTimeFormat = -1,
					VirtualExpression = "[id_upd]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				id_upd.Init(this); // DN
				Fields.Add("id_upd", id_upd);

				// dt_upd
				dt_upd = new DbField<SqlDbType> {
					TableVar = "s_armaster",
					TableName = "s_armaster",
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
					Sortable = true, // Allow sort
					DefaultErrorMessage = Language.Phrase("IncorrectDate").Replace("%s", DateFormat),
					IsUpload = false
				};
				dt_upd.Init(this); // DN
				Fields.Add("dt_upd", dt_upd);

				// com_regno
				com_regno = new DbField<SqlDbType> {
					TableVar = "s_armaster",
					TableName = "s_armaster",
					FieldVar = "x_com_regno",
					Name = "com_regno",
					Expression = "[com_regno]",
					BasicSearchExpression = "[com_regno]",
					Type = 202,
					DbType = SqlDbType.NVarChar,
					DateTimeFormat = -1,
					VirtualExpression = "[com_regno]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				com_regno.Init(this); // DN
				Fields.Add("com_regno", com_regno);
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
				get => _sqlFrom ?? "[dbo].[s_armaster]";
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
				dbcode.SetDbValue(row["dbcode"], false);
				name.SetDbValue(row["name"], false);
				name2.SetDbValue(row["name2"], false);
				short_name.SetDbValue(row["short_name"], false);
				add1.SetDbValue(row["add1"], false);
				add2.SetDbValue(row["add2"], false);
				add3.SetDbValue(row["add3"], false);
				add4.SetDbValue(row["add4"], false);
				area.SetDbValue(row["area"], false);
				postcode.SetDbValue(row["postcode"], false);
				state.SetDbValue(row["state"], false);
				country.SetDbValue(row["country"], false);
				tel1.SetDbValue(row["tel1"], false);
				tel2.SetDbValue(row["tel2"], false);
				phone1.SetDbValue(row["phone1"], false);
				phone2.SetDbValue(row["phone2"], false);
				fax.SetDbValue(row["fax"], false);
				_email.SetDbValue(row["email"], false);
				ptc1.SetDbValue(row["ptc1"], false);
				ptc.SetDbValue(row["ptc"], false);
				ptc2.SetDbValue(row["ptc2"], false);
				ar_grp.SetDbValue(row["ar_grp"], false);
				term_code.SetDbValue(row["term_code"], false);
				pterm_code.SetDbValue(row["pterm_code"], false);
				cr_limit.SetDbValue(row["cr_limit"], false);
				CurrencyCode.SetDbValue(row["CurrencyCode"], false);
				slsman.SetDbValue(row["slsman"], false);
				ytd_sale.SetDbValue(row["ytd_sale"], false);
				ytd_cost.SetDbValue(row["ytd_cost"], false);
				ytd_disc.SetDbValue(row["ytd_disc"], false);
				ctrl_acct.SetDbValue(row["ctrl_acct"], false);
				ctrl_dept.SetDbValue(row["ctrl_dept"], false);
				acct_code.SetDbValue(row["acct_code"], false);
				acct_dept.SetDbValue(row["acct_dept"], false);
				skip_stmt.SetDbValue((ConvertToBool(row["skip_stmt"]) ? "1" : "0"), false);
				stop_sale.SetDbValue((ConvertToBool(row["stop_sale"]) ? "1" : "0"), false);
				opn_item.SetDbValue((ConvertToBool(row["opn_item"]) ? "1" : "0"), false);
				status.SetDbValue(row["status"], false);
				tax_desc.SetDbValue(row["tax_desc"], false);
				stax.SetDbValue(row["stax"], false);
				remark.SetDbValue(row["remark"], false);
				inv_fmt.SetDbValue(row["inv_fmt"], false);
				do_fmt.SetDbValue(row["do_fmt"], false);
				Ship_Code.SetDbValue(row["Ship_Code"], false);
				custtype.SetDbValue(row["custtype"], false);
				Acct_BillAcct.SetDbValue(row["Acct_BillAcct"], false);
				bill_flag.SetDbValue(row["bill_flag"], false);
				payment_code.SetDbValue(row["payment_code"], false);
				stax_pct.SetDbValue(row["stax_pct"], false);
				db_part.SetDbValue(row["db_part"], false);
				b_code.SetDbValue(row["b_code"], false);
				lmw_no.SetDbValue(row["lmw_no"], false);
				cs_code.SetDbValue(row["cs_code"], false);
				approved.SetDbValue((ConvertToBool(row["approved"]) ? "1" : "0"), false);
				oversea.SetDbValue((ConvertToBool(row["oversea"]) ? "1" : "0"), false);
				defa_disc_pct.SetDbValue(row["defa_disc_pct"], false);
				sellpriceDOM.SetDbValue((ConvertToBool(row["sellpriceDOM"]) ? "1" : "0"), false);
				id_upd.SetDbValue(row["id_upd"], false);
				dt_upd.SetDbValue(row["dt_upd"], false);
				com_regno.SetDbValue(row["com_regno"], false);
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
						return "s_armasterlist";
					}
				}
				set {
					Session[Config.ProjectName + "_" + TableVar + "_" + Config.TableReturnUrl] = value;
				}
			}

			// Get modal caption
			public string GetModalCaption(string pageName) {
				if (SameString(pageName, "s_armasterview"))
					return Language.Phrase("View");
				else if (SameString(pageName, "s_armasteredit"))
					return Language.Phrase("Edit");
				else if (SameString(pageName, "s_armasteradd"))
					return Language.Phrase("Add");
				else
					return "";
			}

			// List URL
			public string ListUrl => "s_armasterlist";

			// View URL
			public string ViewUrl => GetViewUrl();

			// View URL
			public string GetViewUrl(string parm = "") {
				string url = "";
				if (!Empty(parm))
					url = KeyUrl("s_armasterview", UrlParm(parm));
				else
					url = KeyUrl("s_armasterview", UrlParm(Config.TableShowDetail + "="));
				return AddMasterUrl(url);
			}

			// Add URL
			public string AddUrl { get; set; } = "s_armasteradd";

			// Add URL
			public string GetAddUrl(string parm = "") {
				string url = "";
				if (!Empty(parm))
					url = "s_armasteradd?" + UrlParm(parm);
				else
					url = "s_armasteradd";
				return AppPath(AddMasterUrl(url));
			}

			// Edit URL
			public string EditUrl => GetEditUrl();

			// Edit URL (with parameter)
			public string GetEditUrl(string parm = "") {
				string url = "";
				url = KeyUrl("s_armasteredit", UrlParm(parm));
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
				url = KeyUrl("s_armasteradd", UrlParm(parm));
				return AppPath(AddMasterUrl(url)); // DN
			}

			// Inline copy URL
			public string InlineCopyUrl =>
				AppPath(AddMasterUrl(KeyUrl(CurrentPageName(), UrlParm("action=copy")))); // DN

			// Delete URL
			public string DeleteUrl =>
				AppPath(KeyUrl("s_armasterdelete", UrlParm())); // DN

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
				dbcode.SetDbValue(rs["dbcode"]);
				name.SetDbValue(rs["name"]);
				name2.SetDbValue(rs["name2"]);
				short_name.SetDbValue(rs["short_name"]);
				add1.SetDbValue(rs["add1"]);
				add2.SetDbValue(rs["add2"]);
				add3.SetDbValue(rs["add3"]);
				add4.SetDbValue(rs["add4"]);
				area.SetDbValue(rs["area"]);
				postcode.SetDbValue(rs["postcode"]);
				state.SetDbValue(rs["state"]);
				country.SetDbValue(rs["country"]);
				tel1.SetDbValue(rs["tel1"]);
				tel2.SetDbValue(rs["tel2"]);
				phone1.SetDbValue(rs["phone1"]);
				phone2.SetDbValue(rs["phone2"]);
				fax.SetDbValue(rs["fax"]);
				_email.SetDbValue(rs["email"]);
				ptc1.SetDbValue(rs["ptc1"]);
				ptc.SetDbValue(rs["ptc"]);
				ptc2.SetDbValue(rs["ptc2"]);
				ar_grp.SetDbValue(rs["ar_grp"]);
				term_code.SetDbValue(rs["term_code"]);
				pterm_code.SetDbValue(rs["pterm_code"]);
				cr_limit.SetDbValue(rs["cr_limit"]);
				CurrencyCode.SetDbValue(rs["CurrencyCode"]);
				slsman.SetDbValue(rs["slsman"]);
				ytd_sale.SetDbValue(rs["ytd_sale"]);
				ytd_cost.SetDbValue(rs["ytd_cost"]);
				ytd_disc.SetDbValue(rs["ytd_disc"]);
				ctrl_acct.SetDbValue(rs["ctrl_acct"]);
				ctrl_dept.SetDbValue(rs["ctrl_dept"]);
				acct_code.SetDbValue(rs["acct_code"]);
				acct_dept.SetDbValue(rs["acct_dept"]);
				skip_stmt.SetDbValue(ConvertToBool(rs["skip_stmt"]) ? "1" : "0");
				stop_sale.SetDbValue(ConvertToBool(rs["stop_sale"]) ? "1" : "0");
				opn_item.SetDbValue(ConvertToBool(rs["opn_item"]) ? "1" : "0");
				status.SetDbValue(rs["status"]);
				tax_desc.SetDbValue(rs["tax_desc"]);
				stax.SetDbValue(rs["stax"]);
				remark.SetDbValue(rs["remark"]);
				inv_fmt.SetDbValue(rs["inv_fmt"]);
				do_fmt.SetDbValue(rs["do_fmt"]);
				Ship_Code.SetDbValue(rs["Ship_Code"]);
				custtype.SetDbValue(rs["custtype"]);
				Acct_BillAcct.SetDbValue(rs["Acct_BillAcct"]);
				bill_flag.SetDbValue(rs["bill_flag"]);
				payment_code.SetDbValue(rs["payment_code"]);
				stax_pct.SetDbValue(rs["stax_pct"]);
				db_part.SetDbValue(rs["db_part"]);
				b_code.SetDbValue(rs["b_code"]);
				lmw_no.SetDbValue(rs["lmw_no"]);
				cs_code.SetDbValue(rs["cs_code"]);
				approved.SetDbValue(ConvertToBool(rs["approved"]) ? "1" : "0");
				oversea.SetDbValue(ConvertToBool(rs["oversea"]) ? "1" : "0");
				defa_disc_pct.SetDbValue(rs["defa_disc_pct"]);
				sellpriceDOM.SetDbValue(ConvertToBool(rs["sellpriceDOM"]) ? "1" : "0");
				id_upd.SetDbValue(rs["id_upd"]);
				dt_upd.SetDbValue(rs["dt_upd"]);
				com_regno.SetDbValue(rs["com_regno"]);
			}

			#pragma warning disable 1998

			// Render list row values
			public async Task RenderListRow() {

				// Call Row Rendering event
				Row_Rendering();

				// Common render codes
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

				// dbcode
				dbcode.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					dbcode.CurrentValue = HtmlDecode(dbcode.CurrentValue);
				dbcode.EditValue = dbcode.CurrentValue; // DN
				dbcode.PlaceHolder = RemoveHtml(dbcode.Caption);

				// name
				name.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					name.CurrentValue = HtmlDecode(name.CurrentValue);
				name.EditValue = name.CurrentValue; // DN
				name.PlaceHolder = RemoveHtml(name.Caption);

				// name2
				name2.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					name2.CurrentValue = HtmlDecode(name2.CurrentValue);
				name2.EditValue = name2.CurrentValue; // DN
				name2.PlaceHolder = RemoveHtml(name2.Caption);

				// short_name
				short_name.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					short_name.CurrentValue = HtmlDecode(short_name.CurrentValue);
				short_name.EditValue = short_name.CurrentValue; // DN
				short_name.PlaceHolder = RemoveHtml(short_name.Caption);

				// add1
				add1.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					add1.CurrentValue = HtmlDecode(add1.CurrentValue);
				add1.EditValue = add1.CurrentValue; // DN
				add1.PlaceHolder = RemoveHtml(add1.Caption);

				// add2
				add2.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					add2.CurrentValue = HtmlDecode(add2.CurrentValue);
				add2.EditValue = add2.CurrentValue; // DN
				add2.PlaceHolder = RemoveHtml(add2.Caption);

				// add3
				add3.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					add3.CurrentValue = HtmlDecode(add3.CurrentValue);
				add3.EditValue = add3.CurrentValue; // DN
				add3.PlaceHolder = RemoveHtml(add3.Caption);

				// add4
				add4.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					add4.CurrentValue = HtmlDecode(add4.CurrentValue);
				add4.EditValue = add4.CurrentValue; // DN
				add4.PlaceHolder = RemoveHtml(add4.Caption);

				// area
				area.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					area.CurrentValue = HtmlDecode(area.CurrentValue);
				area.EditValue = area.CurrentValue; // DN
				area.PlaceHolder = RemoveHtml(area.Caption);

				// postcode
				postcode.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					postcode.CurrentValue = HtmlDecode(postcode.CurrentValue);
				postcode.EditValue = postcode.CurrentValue; // DN
				postcode.PlaceHolder = RemoveHtml(postcode.Caption);

				// state
				state.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					state.CurrentValue = HtmlDecode(state.CurrentValue);
				state.EditValue = state.CurrentValue; // DN
				state.PlaceHolder = RemoveHtml(state.Caption);

				// country
				country.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					country.CurrentValue = HtmlDecode(country.CurrentValue);
				country.EditValue = country.CurrentValue; // DN
				country.PlaceHolder = RemoveHtml(country.Caption);

				// tel1
				tel1.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					tel1.CurrentValue = HtmlDecode(tel1.CurrentValue);
				tel1.EditValue = tel1.CurrentValue; // DN
				tel1.PlaceHolder = RemoveHtml(tel1.Caption);

				// tel2
				tel2.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					tel2.CurrentValue = HtmlDecode(tel2.CurrentValue);
				tel2.EditValue = tel2.CurrentValue; // DN
				tel2.PlaceHolder = RemoveHtml(tel2.Caption);

				// phone1
				phone1.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					phone1.CurrentValue = HtmlDecode(phone1.CurrentValue);
				phone1.EditValue = phone1.CurrentValue; // DN
				phone1.PlaceHolder = RemoveHtml(phone1.Caption);

				// phone2
				phone2.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					phone2.CurrentValue = HtmlDecode(phone2.CurrentValue);
				phone2.EditValue = phone2.CurrentValue; // DN
				phone2.PlaceHolder = RemoveHtml(phone2.Caption);

				// fax
				fax.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					fax.CurrentValue = HtmlDecode(fax.CurrentValue);
				fax.EditValue = fax.CurrentValue; // DN
				fax.PlaceHolder = RemoveHtml(fax.Caption);

				// email
				_email.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					_email.CurrentValue = HtmlDecode(_email.CurrentValue);
				_email.EditValue = _email.CurrentValue; // DN
				_email.PlaceHolder = RemoveHtml(_email.Caption);

				// ptc1
				ptc1.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					ptc1.CurrentValue = HtmlDecode(ptc1.CurrentValue);
				ptc1.EditValue = ptc1.CurrentValue; // DN
				ptc1.PlaceHolder = RemoveHtml(ptc1.Caption);

				// ptc
				ptc.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					ptc.CurrentValue = HtmlDecode(ptc.CurrentValue);
				ptc.EditValue = ptc.CurrentValue; // DN
				ptc.PlaceHolder = RemoveHtml(ptc.Caption);

				// ptc2
				ptc2.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					ptc2.CurrentValue = HtmlDecode(ptc2.CurrentValue);
				ptc2.EditValue = ptc2.CurrentValue; // DN
				ptc2.PlaceHolder = RemoveHtml(ptc2.Caption);

				// ar_grp
				ar_grp.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					ar_grp.CurrentValue = HtmlDecode(ar_grp.CurrentValue);
				ar_grp.EditValue = ar_grp.CurrentValue; // DN
				ar_grp.PlaceHolder = RemoveHtml(ar_grp.Caption);

				// term_code
				term_code.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					term_code.CurrentValue = HtmlDecode(term_code.CurrentValue);
				term_code.EditValue = term_code.CurrentValue; // DN
				term_code.PlaceHolder = RemoveHtml(term_code.Caption);

				// pterm_code
				pterm_code.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					pterm_code.CurrentValue = HtmlDecode(pterm_code.CurrentValue);
				pterm_code.EditValue = pterm_code.CurrentValue; // DN
				pterm_code.PlaceHolder = RemoveHtml(pterm_code.Caption);

				// cr_limit
				cr_limit.EditAttrs["class"] = "form-control";
				cr_limit.EditValue = cr_limit.CurrentValue; // DN
				cr_limit.PlaceHolder = RemoveHtml(cr_limit.Caption);
				if (!Empty(cr_limit.EditValue) && IsNumeric(cr_limit.EditValue))
					cr_limit.EditValue = FormatNumber(cr_limit.EditValue, -2, -2, -2, -2);

				// CurrencyCode
				CurrencyCode.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					CurrencyCode.CurrentValue = HtmlDecode(CurrencyCode.CurrentValue);
				CurrencyCode.EditValue = CurrencyCode.CurrentValue; // DN
				CurrencyCode.PlaceHolder = RemoveHtml(CurrencyCode.Caption);

				// slsman
				slsman.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					slsman.CurrentValue = HtmlDecode(slsman.CurrentValue);
				slsman.EditValue = slsman.CurrentValue; // DN
				slsman.PlaceHolder = RemoveHtml(slsman.Caption);

				// ytd_sale
				ytd_sale.EditAttrs["class"] = "form-control";
				ytd_sale.EditValue = ytd_sale.CurrentValue; // DN
				ytd_sale.PlaceHolder = RemoveHtml(ytd_sale.Caption);
				if (!Empty(ytd_sale.EditValue) && IsNumeric(ytd_sale.EditValue))
					ytd_sale.EditValue = FormatNumber(ytd_sale.EditValue, -2, -2, -2, -2);

				// ytd_cost
				ytd_cost.EditAttrs["class"] = "form-control";
				ytd_cost.EditValue = ytd_cost.CurrentValue; // DN
				ytd_cost.PlaceHolder = RemoveHtml(ytd_cost.Caption);
				if (!Empty(ytd_cost.EditValue) && IsNumeric(ytd_cost.EditValue))
					ytd_cost.EditValue = FormatNumber(ytd_cost.EditValue, -2, -2, -2, -2);

				// ytd_disc
				ytd_disc.EditAttrs["class"] = "form-control";
				ytd_disc.EditValue = ytd_disc.CurrentValue; // DN
				ytd_disc.PlaceHolder = RemoveHtml(ytd_disc.Caption);
				if (!Empty(ytd_disc.EditValue) && IsNumeric(ytd_disc.EditValue))
					ytd_disc.EditValue = FormatNumber(ytd_disc.EditValue, -2, -2, -2, -2);

				// ctrl_acct
				ctrl_acct.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					ctrl_acct.CurrentValue = HtmlDecode(ctrl_acct.CurrentValue);
				ctrl_acct.EditValue = ctrl_acct.CurrentValue; // DN
				ctrl_acct.PlaceHolder = RemoveHtml(ctrl_acct.Caption);

				// ctrl_dept
				ctrl_dept.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					ctrl_dept.CurrentValue = HtmlDecode(ctrl_dept.CurrentValue);
				ctrl_dept.EditValue = ctrl_dept.CurrentValue; // DN
				ctrl_dept.PlaceHolder = RemoveHtml(ctrl_dept.Caption);

				// acct_code
				acct_code.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					acct_code.CurrentValue = HtmlDecode(acct_code.CurrentValue);
				acct_code.EditValue = acct_code.CurrentValue; // DN
				acct_code.PlaceHolder = RemoveHtml(acct_code.Caption);

				// acct_dept
				acct_dept.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					acct_dept.CurrentValue = HtmlDecode(acct_dept.CurrentValue);
				acct_dept.EditValue = acct_dept.CurrentValue; // DN
				acct_dept.PlaceHolder = RemoveHtml(acct_dept.Caption);

				// skip_stmt
				skip_stmt.EditValue = skip_stmt.Options(false);

				// stop_sale
				stop_sale.EditValue = stop_sale.Options(false);

				// opn_item
				opn_item.EditValue = opn_item.Options(false);

				// status
				status.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					status.CurrentValue = HtmlDecode(status.CurrentValue);
				status.EditValue = status.CurrentValue; // DN
				status.PlaceHolder = RemoveHtml(status.Caption);

				// tax_desc
				tax_desc.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					tax_desc.CurrentValue = HtmlDecode(tax_desc.CurrentValue);
				tax_desc.EditValue = tax_desc.CurrentValue; // DN
				tax_desc.PlaceHolder = RemoveHtml(tax_desc.Caption);

				// stax
				stax.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					stax.CurrentValue = HtmlDecode(stax.CurrentValue);
				stax.EditValue = stax.CurrentValue; // DN
				stax.PlaceHolder = RemoveHtml(stax.Caption);

				// remark
				remark.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					remark.CurrentValue = HtmlDecode(remark.CurrentValue);
				remark.EditValue = remark.CurrentValue; // DN
				remark.PlaceHolder = RemoveHtml(remark.Caption);

				// inv_fmt
				inv_fmt.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					inv_fmt.CurrentValue = HtmlDecode(inv_fmt.CurrentValue);
				inv_fmt.EditValue = inv_fmt.CurrentValue; // DN
				inv_fmt.PlaceHolder = RemoveHtml(inv_fmt.Caption);

				// do_fmt
				do_fmt.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					do_fmt.CurrentValue = HtmlDecode(do_fmt.CurrentValue);
				do_fmt.EditValue = do_fmt.CurrentValue; // DN
				do_fmt.PlaceHolder = RemoveHtml(do_fmt.Caption);

				// Ship_Code
				Ship_Code.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					Ship_Code.CurrentValue = HtmlDecode(Ship_Code.CurrentValue);
				Ship_Code.EditValue = Ship_Code.CurrentValue; // DN
				Ship_Code.PlaceHolder = RemoveHtml(Ship_Code.Caption);

				// custtype
				custtype.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					custtype.CurrentValue = HtmlDecode(custtype.CurrentValue);
				custtype.EditValue = custtype.CurrentValue; // DN
				custtype.PlaceHolder = RemoveHtml(custtype.Caption);

				// Acct_BillAcct
				Acct_BillAcct.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					Acct_BillAcct.CurrentValue = HtmlDecode(Acct_BillAcct.CurrentValue);
				Acct_BillAcct.EditValue = Acct_BillAcct.CurrentValue; // DN
				Acct_BillAcct.PlaceHolder = RemoveHtml(Acct_BillAcct.Caption);

				// bill_flag
				bill_flag.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					bill_flag.CurrentValue = HtmlDecode(bill_flag.CurrentValue);
				bill_flag.EditValue = bill_flag.CurrentValue; // DN
				bill_flag.PlaceHolder = RemoveHtml(bill_flag.Caption);

				// payment_code
				payment_code.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					payment_code.CurrentValue = HtmlDecode(payment_code.CurrentValue);
				payment_code.EditValue = payment_code.CurrentValue; // DN
				payment_code.PlaceHolder = RemoveHtml(payment_code.Caption);

				// stax_pct
				stax_pct.EditAttrs["class"] = "form-control";
				stax_pct.EditValue = stax_pct.CurrentValue; // DN
				stax_pct.PlaceHolder = RemoveHtml(stax_pct.Caption);
				if (!Empty(stax_pct.EditValue) && IsNumeric(stax_pct.EditValue))
					stax_pct.EditValue = FormatNumber(stax_pct.EditValue, -2, -2, -2, -2);

				// db_part
				db_part.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					db_part.CurrentValue = HtmlDecode(db_part.CurrentValue);
				db_part.EditValue = db_part.CurrentValue; // DN
				db_part.PlaceHolder = RemoveHtml(db_part.Caption);

				// b_code
				b_code.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					b_code.CurrentValue = HtmlDecode(b_code.CurrentValue);
				b_code.EditValue = b_code.CurrentValue; // DN
				b_code.PlaceHolder = RemoveHtml(b_code.Caption);

				// lmw_no
				lmw_no.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					lmw_no.CurrentValue = HtmlDecode(lmw_no.CurrentValue);
				lmw_no.EditValue = lmw_no.CurrentValue; // DN
				lmw_no.PlaceHolder = RemoveHtml(lmw_no.Caption);

				// cs_code
				cs_code.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					cs_code.CurrentValue = HtmlDecode(cs_code.CurrentValue);
				cs_code.EditValue = cs_code.CurrentValue; // DN
				cs_code.PlaceHolder = RemoveHtml(cs_code.Caption);

				// approved
				approved.EditValue = approved.Options(false);

				// oversea
				oversea.EditValue = oversea.Options(false);

				// defa_disc_pct
				defa_disc_pct.EditAttrs["class"] = "form-control";
				defa_disc_pct.EditValue = defa_disc_pct.CurrentValue; // DN
				defa_disc_pct.PlaceHolder = RemoveHtml(defa_disc_pct.Caption);
				if (!Empty(defa_disc_pct.EditValue) && IsNumeric(defa_disc_pct.EditValue))
					defa_disc_pct.EditValue = FormatNumber(defa_disc_pct.EditValue, -2, -2, -2, -2);

				// sellpriceDOM
				sellpriceDOM.EditValue = sellpriceDOM.Options(false);

				// id_upd
				id_upd.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					id_upd.CurrentValue = HtmlDecode(id_upd.CurrentValue);
				id_upd.EditValue = id_upd.CurrentValue; // DN
				id_upd.PlaceHolder = RemoveHtml(id_upd.Caption);

				// dt_upd
				dt_upd.EditAttrs["class"] = "form-control";
				dt_upd.EditValue = FormatDateTime(dt_upd.CurrentValue, 8); // DN
				dt_upd.PlaceHolder = RemoveHtml(dt_upd.Caption);

				// com_regno
				com_regno.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					com_regno.CurrentValue = HtmlDecode(com_regno.CurrentValue);
				com_regno.EditValue = com_regno.CurrentValue; // DN
				com_regno.PlaceHolder = RemoveHtml(com_regno.Caption);

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
							doc.ExportCaption(dbcode);
							doc.ExportCaption(name);
							doc.ExportCaption(name2);
							doc.ExportCaption(short_name);
							doc.ExportCaption(add1);
							doc.ExportCaption(add2);
							doc.ExportCaption(add3);
							doc.ExportCaption(add4);
							doc.ExportCaption(area);
							doc.ExportCaption(postcode);
							doc.ExportCaption(state);
							doc.ExportCaption(country);
							doc.ExportCaption(tel1);
							doc.ExportCaption(tel2);
							doc.ExportCaption(phone1);
							doc.ExportCaption(phone2);
							doc.ExportCaption(fax);
							doc.ExportCaption(_email);
							doc.ExportCaption(ptc1);
							doc.ExportCaption(ptc);
							doc.ExportCaption(ptc2);
							doc.ExportCaption(ar_grp);
							doc.ExportCaption(term_code);
							doc.ExportCaption(pterm_code);
							doc.ExportCaption(cr_limit);
							doc.ExportCaption(CurrencyCode);
							doc.ExportCaption(slsman);
							doc.ExportCaption(ytd_sale);
							doc.ExportCaption(ytd_cost);
							doc.ExportCaption(ytd_disc);
							doc.ExportCaption(ctrl_acct);
							doc.ExportCaption(ctrl_dept);
							doc.ExportCaption(acct_code);
							doc.ExportCaption(acct_dept);
							doc.ExportCaption(skip_stmt);
							doc.ExportCaption(stop_sale);
							doc.ExportCaption(opn_item);
							doc.ExportCaption(status);
							doc.ExportCaption(tax_desc);
							doc.ExportCaption(stax);
							doc.ExportCaption(remark);
							doc.ExportCaption(inv_fmt);
							doc.ExportCaption(do_fmt);
							doc.ExportCaption(Ship_Code);
							doc.ExportCaption(custtype);
							doc.ExportCaption(Acct_BillAcct);
							doc.ExportCaption(bill_flag);
							doc.ExportCaption(payment_code);
							doc.ExportCaption(stax_pct);
							doc.ExportCaption(db_part);
							doc.ExportCaption(b_code);
							doc.ExportCaption(lmw_no);
							doc.ExportCaption(cs_code);
							doc.ExportCaption(approved);
							doc.ExportCaption(oversea);
							doc.ExportCaption(defa_disc_pct);
							doc.ExportCaption(sellpriceDOM);
							doc.ExportCaption(id_upd);
							doc.ExportCaption(dt_upd);
							doc.ExportCaption(com_regno);
						} else {
							doc.ExportCaption(Id);
							doc.ExportCaption(dbcode);
							doc.ExportCaption(name);
							doc.ExportCaption(name2);
							doc.ExportCaption(short_name);
							doc.ExportCaption(add1);
							doc.ExportCaption(add2);
							doc.ExportCaption(add3);
							doc.ExportCaption(add4);
							doc.ExportCaption(area);
							doc.ExportCaption(postcode);
							doc.ExportCaption(state);
							doc.ExportCaption(country);
							doc.ExportCaption(tel1);
							doc.ExportCaption(tel2);
							doc.ExportCaption(phone1);
							doc.ExportCaption(phone2);
							doc.ExportCaption(fax);
							doc.ExportCaption(_email);
							doc.ExportCaption(ptc1);
							doc.ExportCaption(ptc);
							doc.ExportCaption(ptc2);
							doc.ExportCaption(ar_grp);
							doc.ExportCaption(term_code);
							doc.ExportCaption(pterm_code);
							doc.ExportCaption(cr_limit);
							doc.ExportCaption(CurrencyCode);
							doc.ExportCaption(slsman);
							doc.ExportCaption(ytd_sale);
							doc.ExportCaption(ytd_cost);
							doc.ExportCaption(ytd_disc);
							doc.ExportCaption(ctrl_acct);
							doc.ExportCaption(ctrl_dept);
							doc.ExportCaption(acct_code);
							doc.ExportCaption(acct_dept);
							doc.ExportCaption(skip_stmt);
							doc.ExportCaption(stop_sale);
							doc.ExportCaption(opn_item);
							doc.ExportCaption(status);
							doc.ExportCaption(tax_desc);
							doc.ExportCaption(stax);
							doc.ExportCaption(remark);
							doc.ExportCaption(inv_fmt);
							doc.ExportCaption(do_fmt);
							doc.ExportCaption(Ship_Code);
							doc.ExportCaption(custtype);
							doc.ExportCaption(Acct_BillAcct);
							doc.ExportCaption(bill_flag);
							doc.ExportCaption(payment_code);
							doc.ExportCaption(stax_pct);
							doc.ExportCaption(db_part);
							doc.ExportCaption(b_code);
							doc.ExportCaption(lmw_no);
							doc.ExportCaption(cs_code);
							doc.ExportCaption(approved);
							doc.ExportCaption(oversea);
							doc.ExportCaption(defa_disc_pct);
							doc.ExportCaption(sellpriceDOM);
							doc.ExportCaption(id_upd);
							doc.ExportCaption(dt_upd);
							doc.ExportCaption(com_regno);
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
								await doc.ExportField(dbcode);
								await doc.ExportField(name);
								await doc.ExportField(name2);
								await doc.ExportField(short_name);
								await doc.ExportField(add1);
								await doc.ExportField(add2);
								await doc.ExportField(add3);
								await doc.ExportField(add4);
								await doc.ExportField(area);
								await doc.ExportField(postcode);
								await doc.ExportField(state);
								await doc.ExportField(country);
								await doc.ExportField(tel1);
								await doc.ExportField(tel2);
								await doc.ExportField(phone1);
								await doc.ExportField(phone2);
								await doc.ExportField(fax);
								await doc.ExportField(_email);
								await doc.ExportField(ptc1);
								await doc.ExportField(ptc);
								await doc.ExportField(ptc2);
								await doc.ExportField(ar_grp);
								await doc.ExportField(term_code);
								await doc.ExportField(pterm_code);
								await doc.ExportField(cr_limit);
								await doc.ExportField(CurrencyCode);
								await doc.ExportField(slsman);
								await doc.ExportField(ytd_sale);
								await doc.ExportField(ytd_cost);
								await doc.ExportField(ytd_disc);
								await doc.ExportField(ctrl_acct);
								await doc.ExportField(ctrl_dept);
								await doc.ExportField(acct_code);
								await doc.ExportField(acct_dept);
								await doc.ExportField(skip_stmt);
								await doc.ExportField(stop_sale);
								await doc.ExportField(opn_item);
								await doc.ExportField(status);
								await doc.ExportField(tax_desc);
								await doc.ExportField(stax);
								await doc.ExportField(remark);
								await doc.ExportField(inv_fmt);
								await doc.ExportField(do_fmt);
								await doc.ExportField(Ship_Code);
								await doc.ExportField(custtype);
								await doc.ExportField(Acct_BillAcct);
								await doc.ExportField(bill_flag);
								await doc.ExportField(payment_code);
								await doc.ExportField(stax_pct);
								await doc.ExportField(db_part);
								await doc.ExportField(b_code);
								await doc.ExportField(lmw_no);
								await doc.ExportField(cs_code);
								await doc.ExportField(approved);
								await doc.ExportField(oversea);
								await doc.ExportField(defa_disc_pct);
								await doc.ExportField(sellpriceDOM);
								await doc.ExportField(id_upd);
								await doc.ExportField(dt_upd);
								await doc.ExportField(com_regno);
							} else {
								await doc.ExportField(Id);
								await doc.ExportField(dbcode);
								await doc.ExportField(name);
								await doc.ExportField(name2);
								await doc.ExportField(short_name);
								await doc.ExportField(add1);
								await doc.ExportField(add2);
								await doc.ExportField(add3);
								await doc.ExportField(add4);
								await doc.ExportField(area);
								await doc.ExportField(postcode);
								await doc.ExportField(state);
								await doc.ExportField(country);
								await doc.ExportField(tel1);
								await doc.ExportField(tel2);
								await doc.ExportField(phone1);
								await doc.ExportField(phone2);
								await doc.ExportField(fax);
								await doc.ExportField(_email);
								await doc.ExportField(ptc1);
								await doc.ExportField(ptc);
								await doc.ExportField(ptc2);
								await doc.ExportField(ar_grp);
								await doc.ExportField(term_code);
								await doc.ExportField(pterm_code);
								await doc.ExportField(cr_limit);
								await doc.ExportField(CurrencyCode);
								await doc.ExportField(slsman);
								await doc.ExportField(ytd_sale);
								await doc.ExportField(ytd_cost);
								await doc.ExportField(ytd_disc);
								await doc.ExportField(ctrl_acct);
								await doc.ExportField(ctrl_dept);
								await doc.ExportField(acct_code);
								await doc.ExportField(acct_dept);
								await doc.ExportField(skip_stmt);
								await doc.ExportField(stop_sale);
								await doc.ExportField(opn_item);
								await doc.ExportField(status);
								await doc.ExportField(tax_desc);
								await doc.ExportField(stax);
								await doc.ExportField(remark);
								await doc.ExportField(inv_fmt);
								await doc.ExportField(do_fmt);
								await doc.ExportField(Ship_Code);
								await doc.ExportField(custtype);
								await doc.ExportField(Acct_BillAcct);
								await doc.ExportField(bill_flag);
								await doc.ExportField(payment_code);
								await doc.ExportField(stax_pct);
								await doc.ExportField(db_part);
								await doc.ExportField(b_code);
								await doc.ExportField(lmw_no);
								await doc.ExportField(cs_code);
								await doc.ExportField(approved);
								await doc.ExportField(oversea);
								await doc.ExportField(defa_disc_pct);
								await doc.ExportField(sellpriceDOM);
								await doc.ExportField(id_upd);
								await doc.ExportField(dt_upd);
								await doc.ExportField(com_regno);
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
