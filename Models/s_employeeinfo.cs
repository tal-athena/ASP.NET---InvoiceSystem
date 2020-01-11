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
		/// s_employee
		/// </summary>

		public static _s_employee s_employee {
			get => HttpData.GetOrCreate<_s_employee>("s_employee");
			set => HttpData["s_employee"] = value;
		}

		/// <summary>
		/// Table class for s_employee
		/// </summary>

		public class _s_employee: DbTable {
			public int RowCnt = 0; // DN
			public bool UseSessionForListSql = true;

			// Column CSS classes
			public string LeftColumnClass = "col-sm-2 col-form-label ew-label";
			public string RightColumnClass = "col-sm-10";
			public string OffsetColumnClass = "col-sm-10 offset-sm-2";
			public string TableLeftColumnClass = "w-col-2";
			public readonly DbField<SqlDbType> Id;
			public readonly DbField<SqlDbType> employeeid;
			public readonly DbField<SqlDbType> fname;
			public readonly DbField<SqlDbType> lname;
			public readonly DbField<SqlDbType> oldic;
			public readonly DbField<SqlDbType> newic;
			public readonly DbField<SqlDbType> passport;
			public readonly DbField<SqlDbType> address1;
			public readonly DbField<SqlDbType> address2;
			public readonly DbField<SqlDbType> Id_city;
			public readonly DbField<SqlDbType> Id_state;
			public readonly DbField<SqlDbType> Id_country;
			public readonly DbField<SqlDbType> postcode;
			public readonly DbField<SqlDbType> gender;
			public readonly DbField<SqlDbType> tel;
			public readonly DbField<SqlDbType> handphone;
			public readonly DbField<SqlDbType> _email;
			public readonly DbField<SqlDbType> dob;
			public readonly DbField<SqlDbType> children;
			public readonly DbField<SqlDbType> datejoin;
			public readonly DbField<SqlDbType> dateresign;
			public readonly DbField<SqlDbType> marriedstatus;
			public readonly DbField<SqlDbType> Id_dept;
			public readonly DbField<SqlDbType> Id_position;
			public readonly DbField<SqlDbType> Id_race;
			public readonly DbField<SqlDbType> photopath;
			public readonly DbField<SqlDbType> report_to;
			public readonly DbField<SqlDbType> login_effectivedate;
			public readonly DbField<SqlDbType> login_disableddate;
			public readonly DbField<SqlDbType> UserLevelId;
			public readonly DbField<SqlDbType> _Username;
			public readonly DbField<SqlDbType> password;
			public readonly DbField<SqlDbType> active;

			// Constructor
			public _s_employee() {

				// Language object // DN
				Language = Language ?? new Lang();
				TableVar = "s_employee";
				Name = "s_employee";
				Type = "TABLE";

				// Update Table
				UpdateTable = "[dbo].[s_employee]";
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
					TableVar = "s_employee",
					TableName = "s_employee",
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

				// employeeid
				employeeid = new DbField<SqlDbType> {
					TableVar = "s_employee",
					TableName = "s_employee",
					FieldVar = "x_employeeid",
					Name = "employeeid",
					Expression = "[employeeid]",
					BasicSearchExpression = "[employeeid]",
					Type = 200,
					DbType = SqlDbType.VarChar,
					DateTimeFormat = -1,
					VirtualExpression = "[employeeid]",
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
				employeeid.Init(this); // DN
				Fields.Add("employeeid", employeeid);

				// fname
				fname = new DbField<SqlDbType> {
					TableVar = "s_employee",
					TableName = "s_employee",
					FieldVar = "x_fname",
					Name = "fname",
					Expression = "[fname]",
					BasicSearchExpression = "[fname]",
					Type = 200,
					DbType = SqlDbType.VarChar,
					DateTimeFormat = -1,
					VirtualExpression = "[fname]",
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
				fname.Init(this); // DN
				Fields.Add("fname", fname);

				// lname
				lname = new DbField<SqlDbType> {
					TableVar = "s_employee",
					TableName = "s_employee",
					FieldVar = "x_lname",
					Name = "lname",
					Expression = "[lname]",
					BasicSearchExpression = "[lname]",
					Type = 200,
					DbType = SqlDbType.VarChar,
					DateTimeFormat = -1,
					VirtualExpression = "[lname]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				lname.Init(this); // DN
				Fields.Add("lname", lname);

				// oldic
				oldic = new DbField<SqlDbType> {
					TableVar = "s_employee",
					TableName = "s_employee",
					FieldVar = "x_oldic",
					Name = "oldic",
					Expression = "[oldic]",
					BasicSearchExpression = "[oldic]",
					Type = 200,
					DbType = SqlDbType.VarChar,
					DateTimeFormat = -1,
					VirtualExpression = "[oldic]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				oldic.Init(this); // DN
				Fields.Add("oldic", oldic);

				// newic
				newic = new DbField<SqlDbType> {
					TableVar = "s_employee",
					TableName = "s_employee",
					FieldVar = "x_newic",
					Name = "newic",
					Expression = "[newic]",
					BasicSearchExpression = "[newic]",
					Type = 200,
					DbType = SqlDbType.VarChar,
					DateTimeFormat = -1,
					VirtualExpression = "[newic]",
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
				newic.Init(this); // DN
				Fields.Add("newic", newic);

				// passport
				passport = new DbField<SqlDbType> {
					TableVar = "s_employee",
					TableName = "s_employee",
					FieldVar = "x_passport",
					Name = "passport",
					Expression = "[passport]",
					BasicSearchExpression = "[passport]",
					Type = 200,
					DbType = SqlDbType.VarChar,
					DateTimeFormat = -1,
					VirtualExpression = "[passport]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				passport.Init(this); // DN
				Fields.Add("passport", passport);

				// address1
				address1 = new DbField<SqlDbType> {
					TableVar = "s_employee",
					TableName = "s_employee",
					FieldVar = "x_address1",
					Name = "address1",
					Expression = "[address1]",
					BasicSearchExpression = "[address1]",
					Type = 200,
					DbType = SqlDbType.VarChar,
					DateTimeFormat = -1,
					VirtualExpression = "[address1]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				address1.Init(this); // DN
				Fields.Add("address1", address1);

				// address2
				address2 = new DbField<SqlDbType> {
					TableVar = "s_employee",
					TableName = "s_employee",
					FieldVar = "x_address2",
					Name = "address2",
					Expression = "[address2]",
					BasicSearchExpression = "[address2]",
					Type = 200,
					DbType = SqlDbType.VarChar,
					DateTimeFormat = -1,
					VirtualExpression = "[address2]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				address2.Init(this); // DN
				Fields.Add("address2", address2);

				// Id_city
				Id_city = new DbField<SqlDbType> {
					TableVar = "s_employee",
					TableName = "s_employee",
					FieldVar = "x_Id_city",
					Name = "Id_city",
					Expression = "[Id_city]",
					BasicSearchExpression = "CAST([Id_city] AS NVARCHAR)",
					Type = 3,
					DbType = SqlDbType.Int,
					DateTimeFormat = -1,
					VirtualExpression = "[Id_city]",
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
				Id_city.Init(this); // DN
				Fields.Add("Id_city", Id_city);

				// Id_state
				Id_state = new DbField<SqlDbType> {
					TableVar = "s_employee",
					TableName = "s_employee",
					FieldVar = "x_Id_state",
					Name = "Id_state",
					Expression = "[Id_state]",
					BasicSearchExpression = "CAST([Id_state] AS NVARCHAR)",
					Type = 3,
					DbType = SqlDbType.Int,
					DateTimeFormat = -1,
					VirtualExpression = "[Id_state]",
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
				Id_state.Init(this); // DN
				Fields.Add("Id_state", Id_state);

				// Id_country
				Id_country = new DbField<SqlDbType> {
					TableVar = "s_employee",
					TableName = "s_employee",
					FieldVar = "x_Id_country",
					Name = "Id_country",
					Expression = "[Id_country]",
					BasicSearchExpression = "CAST([Id_country] AS NVARCHAR)",
					Type = 3,
					DbType = SqlDbType.Int,
					DateTimeFormat = -1,
					VirtualExpression = "[Id_country]",
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
				Id_country.Init(this); // DN
				Fields.Add("Id_country", Id_country);

				// postcode
				postcode = new DbField<SqlDbType> {
					TableVar = "s_employee",
					TableName = "s_employee",
					FieldVar = "x_postcode",
					Name = "postcode",
					Expression = "[postcode]",
					BasicSearchExpression = "[postcode]",
					Type = 200,
					DbType = SqlDbType.VarChar,
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

				// gender
				gender = new DbField<SqlDbType> {
					TableVar = "s_employee",
					TableName = "s_employee",
					FieldVar = "x_gender",
					Name = "gender",
					Expression = "[gender]",
					BasicSearchExpression = "[gender]",
					Type = 129,
					DbType = SqlDbType.Char,
					DateTimeFormat = -1,
					VirtualExpression = "[gender]",
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
				gender.Init(this); // DN
				Fields.Add("gender", gender);

				// tel
				tel = new DbField<SqlDbType> {
					TableVar = "s_employee",
					TableName = "s_employee",
					FieldVar = "x_tel",
					Name = "tel",
					Expression = "[tel]",
					BasicSearchExpression = "[tel]",
					Type = 200,
					DbType = SqlDbType.VarChar,
					DateTimeFormat = -1,
					VirtualExpression = "[tel]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				tel.Init(this); // DN
				Fields.Add("tel", tel);

				// handphone
				handphone = new DbField<SqlDbType> {
					TableVar = "s_employee",
					TableName = "s_employee",
					FieldVar = "x_handphone",
					Name = "handphone",
					Expression = "[handphone]",
					BasicSearchExpression = "[handphone]",
					Type = 200,
					DbType = SqlDbType.VarChar,
					DateTimeFormat = -1,
					VirtualExpression = "[handphone]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				handphone.Init(this); // DN
				Fields.Add("handphone", handphone);

				// _email
				_email = new DbField<SqlDbType> {
					TableVar = "s_employee",
					TableName = "s_employee",
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

				// dob
				dob = new DbField<SqlDbType> {
					TableVar = "s_employee",
					TableName = "s_employee",
					FieldVar = "x_dob",
					Name = "dob",
					Expression = "[dob]",
					BasicSearchExpression = CastDateFieldForLike("[dob]", 0, "DB"),
					Type = 135,
					DbType = SqlDbType.DateTime,
					DateTimeFormat = 0,
					VirtualExpression = "[dob]",
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
				dob.Init(this); // DN
				Fields.Add("dob", dob);

				// children
				children = new DbField<SqlDbType> {
					TableVar = "s_employee",
					TableName = "s_employee",
					FieldVar = "x_children",
					Name = "children",
					Expression = "[children]",
					BasicSearchExpression = "CAST([children] AS NVARCHAR)",
					Type = 3,
					DbType = SqlDbType.Int,
					DateTimeFormat = -1,
					VirtualExpression = "[children]",
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
				children.Init(this); // DN
				Fields.Add("children", children);

				// datejoin
				datejoin = new DbField<SqlDbType> {
					TableVar = "s_employee",
					TableName = "s_employee",
					FieldVar = "x_datejoin",
					Name = "datejoin",
					Expression = "[datejoin]",
					BasicSearchExpression = CastDateFieldForLike("[datejoin]", 0, "DB"),
					Type = 135,
					DbType = SqlDbType.DateTime,
					DateTimeFormat = 0,
					VirtualExpression = "[datejoin]",
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
				datejoin.Init(this); // DN
				Fields.Add("datejoin", datejoin);

				// dateresign
				dateresign = new DbField<SqlDbType> {
					TableVar = "s_employee",
					TableName = "s_employee",
					FieldVar = "x_dateresign",
					Name = "dateresign",
					Expression = "[dateresign]",
					BasicSearchExpression = CastDateFieldForLike("[dateresign]", 0, "DB"),
					Type = 135,
					DbType = SqlDbType.DateTime,
					DateTimeFormat = 0,
					VirtualExpression = "[dateresign]",
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
				dateresign.Init(this); // DN
				Fields.Add("dateresign", dateresign);

				// marriedstatus
				marriedstatus = new DbField<SqlDbType> {
					TableVar = "s_employee",
					TableName = "s_employee",
					FieldVar = "x_marriedstatus",
					Name = "marriedstatus",
					Expression = "[marriedstatus]",
					BasicSearchExpression = "[marriedstatus]",
					Type = 129,
					DbType = SqlDbType.Char,
					DateTimeFormat = -1,
					VirtualExpression = "[marriedstatus]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				marriedstatus.Init(this); // DN
				Fields.Add("marriedstatus", marriedstatus);

				// Id_dept
				Id_dept = new DbField<SqlDbType> {
					TableVar = "s_employee",
					TableName = "s_employee",
					FieldVar = "x_Id_dept",
					Name = "Id_dept",
					Expression = "[Id_dept]",
					BasicSearchExpression = "CAST([Id_dept] AS NVARCHAR)",
					Type = 3,
					DbType = SqlDbType.Int,
					DateTimeFormat = -1,
					VirtualExpression = "[Id_dept]",
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
				Id_dept.Init(this); // DN
				Fields.Add("Id_dept", Id_dept);

				// Id_position
				Id_position = new DbField<SqlDbType> {
					TableVar = "s_employee",
					TableName = "s_employee",
					FieldVar = "x_Id_position",
					Name = "Id_position",
					Expression = "[Id_position]",
					BasicSearchExpression = "CAST([Id_position] AS NVARCHAR)",
					Type = 3,
					DbType = SqlDbType.Int,
					DateTimeFormat = -1,
					VirtualExpression = "[Id_position]",
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
				Id_position.Init(this); // DN
				Fields.Add("Id_position", Id_position);

				// Id_race
				Id_race = new DbField<SqlDbType> {
					TableVar = "s_employee",
					TableName = "s_employee",
					FieldVar = "x_Id_race",
					Name = "Id_race",
					Expression = "[Id_race]",
					BasicSearchExpression = "CAST([Id_race] AS NVARCHAR)",
					Type = 3,
					DbType = SqlDbType.Int,
					DateTimeFormat = -1,
					VirtualExpression = "[Id_race]",
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
				Id_race.Init(this); // DN
				Fields.Add("Id_race", Id_race);

				// photopath
				photopath = new DbField<SqlDbType> {
					TableVar = "s_employee",
					TableName = "s_employee",
					FieldVar = "x_photopath",
					Name = "photopath",
					Expression = "[photopath]",
					BasicSearchExpression = "[photopath]",
					Type = 201,
					DbType = SqlDbType.Text,
					DateTimeFormat = -1,
					VirtualExpression = "[photopath]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXTAREA",
					Sortable = true, // Allow sort
					IsUpload = false
				};
				photopath.Init(this); // DN
				Fields.Add("photopath", photopath);

				// report_to
				report_to = new DbField<SqlDbType> {
					TableVar = "s_employee",
					TableName = "s_employee",
					FieldVar = "x_report_to",
					Name = "report_to",
					Expression = "[report_to]",
					BasicSearchExpression = "CAST([report_to] AS NVARCHAR)",
					Type = 3,
					DbType = SqlDbType.Int,
					DateTimeFormat = -1,
					VirtualExpression = "[report_to]",
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
				report_to.Init(this); // DN
				Fields.Add("report_to", report_to);

				// login_effectivedate
				login_effectivedate = new DbField<SqlDbType> {
					TableVar = "s_employee",
					TableName = "s_employee",
					FieldVar = "x_login_effectivedate",
					Name = "login_effectivedate",
					Expression = "[login_effectivedate]",
					BasicSearchExpression = CastDateFieldForLike("[login_effectivedate]", 0, "DB"),
					Type = 135,
					DbType = SqlDbType.DateTime,
					DateTimeFormat = 0,
					VirtualExpression = "[login_effectivedate]",
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
				login_effectivedate.Init(this); // DN
				Fields.Add("login_effectivedate", login_effectivedate);

				// login_disableddate
				login_disableddate = new DbField<SqlDbType> {
					TableVar = "s_employee",
					TableName = "s_employee",
					FieldVar = "x_login_disableddate",
					Name = "login_disableddate",
					Expression = "[login_disableddate]",
					BasicSearchExpression = CastDateFieldForLike("[login_disableddate]", 0, "DB"),
					Type = 135,
					DbType = SqlDbType.DateTime,
					DateTimeFormat = 0,
					VirtualExpression = "[login_disableddate]",
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
				login_disableddate.Init(this); // DN
				Fields.Add("login_disableddate", login_disableddate);

				// UserLevelId
				UserLevelId = new DbField<SqlDbType> {
					TableVar = "s_employee",
					TableName = "s_employee",
					FieldVar = "x_UserLevelId",
					Name = "UserLevelId",
					Expression = "[UserLevelId]",
					BasicSearchExpression = "CAST([UserLevelId] AS NVARCHAR)",
					Type = 3,
					DbType = SqlDbType.Int,
					DateTimeFormat = -1,
					VirtualExpression = "[UserLevelId]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "SELECT",
					Sortable = true, // Allow sort
					UsePleaseSelect = true, // Use PleaseSelect by default
					PleaseSelectText = Language.Phrase("PleaseSelect"), // PleaseSelect text
					DefaultErrorMessage = Language.Phrase("IncorrectInteger"),
					IsUpload = false
				};
				UserLevelId.Init(this); // DN
				UserLevelId.Lookup = new Lookup<DbField>("UserLevelId", "UserLevels", false, "UserLevelID", new List<string> {"UserLevelID", "", "", ""}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "");
				Fields.Add("UserLevelId", UserLevelId);

				// _Username
				_Username = new DbField<SqlDbType> {
					TableVar = "s_employee",
					TableName = "s_employee",
					FieldVar = "x__Username",
					Name = "Username",
					Expression = "[Username]",
					BasicSearchExpression = "[Username]",
					Type = 200,
					DbType = SqlDbType.VarChar,
					DateTimeFormat = -1,
					VirtualExpression = "[Username]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Required = true, // Required field
					Sortable = true, // Allow sort
					IsUpload = false
				};
				_Username.Init(this); // DN
				Fields.Add("Username", _Username);

				// password
				password = new DbField<SqlDbType> {
					TableVar = "s_employee",
					TableName = "s_employee",
					FieldVar = "x_password",
					Name = "password",
					Expression = "[password]",
					BasicSearchExpression = "[password]",
					Type = 200,
					DbType = SqlDbType.VarChar,
					DateTimeFormat = -1,
					VirtualExpression = "[password]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Required = true, // Required field
					Sortable = true, // Allow sort
					IsUpload = false
				};
				password.Init(this); // DN
				Fields.Add("password", password);

				// active
				active = new DbField<SqlDbType> {
					TableVar = "s_employee",
					TableName = "s_employee",
					FieldVar = "x_active",
					Name = "active",
					Expression = "[active]",
					BasicSearchExpression = "[active]",
					Type = 11,
					DbType = SqlDbType.Bit,
					DateTimeFormat = -1,
					VirtualExpression = "[active]",
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
				active.Init(this); // DN
				active.Lookup = new Lookup<DbField>("active", "s_employee", false, "", new List<string> {"", "", "", ""}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "");
				active.GetDefault = () => 1;
				Fields.Add("active", active);
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
				if (!Empty(Security.CurrentUserID) && !Security.IsAdmin) { // Non system admin
					filter = AddUserIDFilter(filter);
				}
				return filter;
			}

			// Check if User ID security allows view all
			public bool UserIDAllow(string id = "") {
				int allow = UserIdAllowSecurity;
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
				get => _sqlFrom ?? "[dbo].[s_employee]";
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
					filter = ApplyUserIDFilters(filter); // Add User ID filter
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
					filter = ApplyUserIDFilters(filter); // Add User ID filter
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
				if (Config.EncryptedPassword && SameString(fld.Name, Config.LoginPasswordFieldName)) {
					if (Config.CaseSensitivePassword) {
						return EncryptPassword(Convert.ToString(value));
					} else {
						return EncryptPassword(Convert.ToString(value).ToLower());
					}
				}
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
				employeeid.SetDbValue(row["employeeid"], false);
				fname.SetDbValue(row["fname"], false);
				lname.SetDbValue(row["lname"], false);
				oldic.SetDbValue(row["oldic"], false);
				newic.SetDbValue(row["newic"], false);
				passport.SetDbValue(row["passport"], false);
				address1.SetDbValue(row["address1"], false);
				address2.SetDbValue(row["address2"], false);
				Id_city.SetDbValue(row["Id_city"], false);
				Id_state.SetDbValue(row["Id_state"], false);
				Id_country.SetDbValue(row["Id_country"], false);
				postcode.SetDbValue(row["postcode"], false);
				gender.SetDbValue(row["gender"], false);
				tel.SetDbValue(row["tel"], false);
				handphone.SetDbValue(row["handphone"], false);
				_email.SetDbValue(row["email"], false);
				dob.SetDbValue(row["dob"], false);
				children.SetDbValue(row["children"], false);
				datejoin.SetDbValue(row["datejoin"], false);
				dateresign.SetDbValue(row["dateresign"], false);
				marriedstatus.SetDbValue(row["marriedstatus"], false);
				Id_dept.SetDbValue(row["Id_dept"], false);
				Id_position.SetDbValue(row["Id_position"], false);
				Id_race.SetDbValue(row["Id_race"], false);
				photopath.SetDbValue(row["photopath"], false);
				report_to.SetDbValue(row["report_to"], false);
				login_effectivedate.SetDbValue(row["login_effectivedate"], false);
				login_disableddate.SetDbValue(row["login_disableddate"], false);
				UserLevelId.SetDbValue(row["UserLevelId"], false);
				_Username.SetDbValue(row["Username"], false);
				password.SetDbValue(row["password"], false);
				active.SetDbValue((ConvertToBool(row["active"]) ? "1" : "0"), false);
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
						return "s_employeelist";
					}
				}
				set {
					Session[Config.ProjectName + "_" + TableVar + "_" + Config.TableReturnUrl] = value;
				}
			}

			// Get modal caption
			public string GetModalCaption(string pageName) {
				if (SameString(pageName, "s_employeeview"))
					return Language.Phrase("View");
				else if (SameString(pageName, "s_employeeedit"))
					return Language.Phrase("Edit");
				else if (SameString(pageName, "s_employeeadd"))
					return Language.Phrase("Add");
				else
					return "";
			}

			// List URL
			public string ListUrl => "s_employeelist";

			// View URL
			public string ViewUrl => GetViewUrl();

			// View URL
			public string GetViewUrl(string parm = "") {
				string url = "";
				if (!Empty(parm))
					url = KeyUrl("s_employeeview", UrlParm(parm));
				else
					url = KeyUrl("s_employeeview", UrlParm(Config.TableShowDetail + "="));
				return AddMasterUrl(url);
			}

			// Add URL
			public string AddUrl { get; set; } = "s_employeeadd";

			// Add URL
			public string GetAddUrl(string parm = "") {
				string url = "";
				if (!Empty(parm))
					url = "s_employeeadd?" + UrlParm(parm);
				else
					url = "s_employeeadd";
				return AppPath(AddMasterUrl(url));
			}

			// Edit URL
			public string EditUrl => GetEditUrl();

			// Edit URL (with parameter)
			public string GetEditUrl(string parm = "") {
				string url = "";
				url = KeyUrl("s_employeeedit", UrlParm(parm));
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
				url = KeyUrl("s_employeeadd", UrlParm(parm));
				return AppPath(AddMasterUrl(url)); // DN
			}

			// Inline copy URL
			public string InlineCopyUrl =>
				AppPath(AddMasterUrl(KeyUrl(CurrentPageName(), UrlParm("action=copy")))); // DN

			// Delete URL
			public string DeleteUrl =>
				AppPath(KeyUrl("s_employeedelete", UrlParm())); // DN

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
				employeeid.SetDbValue(rs["employeeid"]);
				fname.SetDbValue(rs["fname"]);
				lname.SetDbValue(rs["lname"]);
				oldic.SetDbValue(rs["oldic"]);
				newic.SetDbValue(rs["newic"]);
				passport.SetDbValue(rs["passport"]);
				address1.SetDbValue(rs["address1"]);
				address2.SetDbValue(rs["address2"]);
				Id_city.SetDbValue(rs["Id_city"]);
				Id_state.SetDbValue(rs["Id_state"]);
				Id_country.SetDbValue(rs["Id_country"]);
				postcode.SetDbValue(rs["postcode"]);
				gender.SetDbValue(rs["gender"]);
				tel.SetDbValue(rs["tel"]);
				handphone.SetDbValue(rs["handphone"]);
				_email.SetDbValue(rs["email"]);
				dob.SetDbValue(rs["dob"]);
				children.SetDbValue(rs["children"]);
				datejoin.SetDbValue(rs["datejoin"]);
				dateresign.SetDbValue(rs["dateresign"]);
				marriedstatus.SetDbValue(rs["marriedstatus"]);
				Id_dept.SetDbValue(rs["Id_dept"]);
				Id_position.SetDbValue(rs["Id_position"]);
				Id_race.SetDbValue(rs["Id_race"]);
				photopath.SetDbValue(rs["photopath"]);
				report_to.SetDbValue(rs["report_to"]);
				login_effectivedate.SetDbValue(rs["login_effectivedate"]);
				login_disableddate.SetDbValue(rs["login_disableddate"]);
				UserLevelId.SetDbValue(rs["UserLevelId"]);
				_Username.SetDbValue(rs["Username"]);
				password.SetDbValue(rs["password"]);
				active.SetDbValue(ConvertToBool(rs["active"]) ? "1" : "0");
			}

			#pragma warning disable 1998

			// Render list row values
			public async Task RenderListRow() {

				// Call Row Rendering event
				Row_Rendering();

				// Common render codes
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

				// photopath
				photopath.ViewValue = photopath.CurrentValue;

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

				// photopath
				photopath.HrefValue = "";
				photopath.TooltipValue = "";

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

				// employeeid
				employeeid.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					employeeid.CurrentValue = HtmlDecode(employeeid.CurrentValue);
				employeeid.EditValue = employeeid.CurrentValue; // DN
				employeeid.PlaceHolder = RemoveHtml(employeeid.Caption);

				// fname
				fname.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					fname.CurrentValue = HtmlDecode(fname.CurrentValue);
				fname.EditValue = fname.CurrentValue; // DN
				fname.PlaceHolder = RemoveHtml(fname.Caption);

				// lname
				lname.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					lname.CurrentValue = HtmlDecode(lname.CurrentValue);
				lname.EditValue = lname.CurrentValue; // DN
				lname.PlaceHolder = RemoveHtml(lname.Caption);

				// oldic
				oldic.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					oldic.CurrentValue = HtmlDecode(oldic.CurrentValue);
				oldic.EditValue = oldic.CurrentValue; // DN
				oldic.PlaceHolder = RemoveHtml(oldic.Caption);

				// newic
				newic.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					newic.CurrentValue = HtmlDecode(newic.CurrentValue);
				newic.EditValue = newic.CurrentValue; // DN
				newic.PlaceHolder = RemoveHtml(newic.Caption);

				// passport
				passport.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					passport.CurrentValue = HtmlDecode(passport.CurrentValue);
				passport.EditValue = passport.CurrentValue; // DN
				passport.PlaceHolder = RemoveHtml(passport.Caption);

				// address1
				address1.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					address1.CurrentValue = HtmlDecode(address1.CurrentValue);
				address1.EditValue = address1.CurrentValue; // DN
				address1.PlaceHolder = RemoveHtml(address1.Caption);

				// address2
				address2.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					address2.CurrentValue = HtmlDecode(address2.CurrentValue);
				address2.EditValue = address2.CurrentValue; // DN
				address2.PlaceHolder = RemoveHtml(address2.Caption);

				// Id_city
				Id_city.EditAttrs["class"] = "form-control";
				Id_city.EditValue = Id_city.CurrentValue; // DN
				Id_city.PlaceHolder = RemoveHtml(Id_city.Caption);

				// Id_state
				Id_state.EditAttrs["class"] = "form-control";
				Id_state.EditValue = Id_state.CurrentValue; // DN
				Id_state.PlaceHolder = RemoveHtml(Id_state.Caption);

				// Id_country
				Id_country.EditAttrs["class"] = "form-control";
				Id_country.EditValue = Id_country.CurrentValue; // DN
				Id_country.PlaceHolder = RemoveHtml(Id_country.Caption);

				// postcode
				postcode.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					postcode.CurrentValue = HtmlDecode(postcode.CurrentValue);
				postcode.EditValue = postcode.CurrentValue; // DN
				postcode.PlaceHolder = RemoveHtml(postcode.Caption);

				// gender
				gender.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					gender.CurrentValue = HtmlDecode(gender.CurrentValue);
				gender.EditValue = gender.CurrentValue; // DN
				gender.PlaceHolder = RemoveHtml(gender.Caption);

				// tel
				tel.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					tel.CurrentValue = HtmlDecode(tel.CurrentValue);
				tel.EditValue = tel.CurrentValue; // DN
				tel.PlaceHolder = RemoveHtml(tel.Caption);

				// handphone
				handphone.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					handphone.CurrentValue = HtmlDecode(handphone.CurrentValue);
				handphone.EditValue = handphone.CurrentValue; // DN
				handphone.PlaceHolder = RemoveHtml(handphone.Caption);

				// email
				_email.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					_email.CurrentValue = HtmlDecode(_email.CurrentValue);
				_email.EditValue = _email.CurrentValue; // DN
				_email.PlaceHolder = RemoveHtml(_email.Caption);

				// dob
				dob.EditAttrs["class"] = "form-control";
				dob.EditValue = FormatDateTime(dob.CurrentValue, 8); // DN
				dob.PlaceHolder = RemoveHtml(dob.Caption);

				// children
				children.EditAttrs["class"] = "form-control";
				children.EditValue = children.CurrentValue; // DN
				children.PlaceHolder = RemoveHtml(children.Caption);

				// datejoin
				datejoin.EditAttrs["class"] = "form-control";
				datejoin.EditValue = FormatDateTime(datejoin.CurrentValue, 8); // DN
				datejoin.PlaceHolder = RemoveHtml(datejoin.Caption);

				// dateresign
				dateresign.EditAttrs["class"] = "form-control";
				dateresign.EditValue = FormatDateTime(dateresign.CurrentValue, 8); // DN
				dateresign.PlaceHolder = RemoveHtml(dateresign.Caption);

				// marriedstatus
				marriedstatus.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					marriedstatus.CurrentValue = HtmlDecode(marriedstatus.CurrentValue);
				marriedstatus.EditValue = marriedstatus.CurrentValue; // DN
				marriedstatus.PlaceHolder = RemoveHtml(marriedstatus.Caption);

				// Id_dept
				Id_dept.EditAttrs["class"] = "form-control";
				Id_dept.EditValue = Id_dept.CurrentValue; // DN
				Id_dept.PlaceHolder = RemoveHtml(Id_dept.Caption);

				// Id_position
				Id_position.EditAttrs["class"] = "form-control";
				Id_position.EditValue = Id_position.CurrentValue; // DN
				Id_position.PlaceHolder = RemoveHtml(Id_position.Caption);

				// Id_race
				Id_race.EditAttrs["class"] = "form-control";
				Id_race.EditValue = Id_race.CurrentValue; // DN
				Id_race.PlaceHolder = RemoveHtml(Id_race.Caption);

				// photopath
				photopath.EditAttrs["class"] = "form-control";
				photopath.EditValue = photopath.CurrentValue; // DN
				photopath.PlaceHolder = RemoveHtml(photopath.Caption);

				// report_to
				report_to.EditAttrs["class"] = "form-control";
				if (!Security.IsAdmin && Security.IsLoggedIn) { // Non system admin
					if (SameString(Id.CurrentValue, CurrentUserID())) {
				report_to.EditValue = report_to.CurrentValue;
				report_to.EditValue = FormatNumber(report_to.EditValue, 0, -2, -2, -2);
					} else {
					}
				} else {
				report_to.EditValue = report_to.CurrentValue; // DN
				report_to.PlaceHolder = RemoveHtml(report_to.Caption);
				}

				// login_effectivedate
				login_effectivedate.EditAttrs["class"] = "form-control";
				login_effectivedate.EditValue = FormatDateTime(login_effectivedate.CurrentValue, 8); // DN
				login_effectivedate.PlaceHolder = RemoveHtml(login_effectivedate.Caption);

				// login_disableddate
				login_disableddate.EditAttrs["class"] = "form-control";
				login_disableddate.EditValue = FormatDateTime(login_disableddate.CurrentValue, 8); // DN
				login_disableddate.PlaceHolder = RemoveHtml(login_disableddate.Caption);

				// UserLevelId
				UserLevelId.EditAttrs["class"] = "form-control";
				if (!Security.CanAdmin) { // System admin
					UserLevelId.EditValue = Language.Phrase("PasswordMask");
				} else {
				}

				// Username
				_Username.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					_Username.CurrentValue = HtmlDecode(_Username.CurrentValue);
				_Username.EditValue = _Username.CurrentValue; // DN
				_Username.PlaceHolder = RemoveHtml(_Username.Caption);

				// password
				password.EditAttrs["class"] = "form-control";
				if (Config.RemoveXss)
					password.CurrentValue = HtmlDecode(password.CurrentValue);
				password.EditValue = password.CurrentValue; // DN
				password.PlaceHolder = RemoveHtml(password.Caption);

				// active
				active.EditValue = active.Options(false);

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
							doc.ExportCaption(employeeid);
							doc.ExportCaption(fname);
							doc.ExportCaption(lname);
							doc.ExportCaption(oldic);
							doc.ExportCaption(newic);
							doc.ExportCaption(passport);
							doc.ExportCaption(address1);
							doc.ExportCaption(address2);
							doc.ExportCaption(Id_city);
							doc.ExportCaption(Id_state);
							doc.ExportCaption(Id_country);
							doc.ExportCaption(postcode);
							doc.ExportCaption(gender);
							doc.ExportCaption(tel);
							doc.ExportCaption(handphone);
							doc.ExportCaption(_email);
							doc.ExportCaption(dob);
							doc.ExportCaption(children);
							doc.ExportCaption(datejoin);
							doc.ExportCaption(dateresign);
							doc.ExportCaption(marriedstatus);
							doc.ExportCaption(Id_dept);
							doc.ExportCaption(Id_position);
							doc.ExportCaption(Id_race);
							doc.ExportCaption(photopath);
							doc.ExportCaption(report_to);
							doc.ExportCaption(login_effectivedate);
							doc.ExportCaption(login_disableddate);
							doc.ExportCaption(UserLevelId);
							doc.ExportCaption(_Username);
							doc.ExportCaption(password);
							doc.ExportCaption(active);
						} else {
							doc.ExportCaption(Id);
							doc.ExportCaption(employeeid);
							doc.ExportCaption(fname);
							doc.ExportCaption(lname);
							doc.ExportCaption(oldic);
							doc.ExportCaption(newic);
							doc.ExportCaption(passport);
							doc.ExportCaption(address1);
							doc.ExportCaption(address2);
							doc.ExportCaption(Id_city);
							doc.ExportCaption(Id_state);
							doc.ExportCaption(Id_country);
							doc.ExportCaption(postcode);
							doc.ExportCaption(gender);
							doc.ExportCaption(tel);
							doc.ExportCaption(handphone);
							doc.ExportCaption(_email);
							doc.ExportCaption(dob);
							doc.ExportCaption(children);
							doc.ExportCaption(datejoin);
							doc.ExportCaption(dateresign);
							doc.ExportCaption(marriedstatus);
							doc.ExportCaption(Id_dept);
							doc.ExportCaption(Id_position);
							doc.ExportCaption(Id_race);
							doc.ExportCaption(report_to);
							doc.ExportCaption(login_effectivedate);
							doc.ExportCaption(login_disableddate);
							doc.ExportCaption(UserLevelId);
							doc.ExportCaption(_Username);
							doc.ExportCaption(password);
							doc.ExportCaption(active);
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
								await doc.ExportField(employeeid);
								await doc.ExportField(fname);
								await doc.ExportField(lname);
								await doc.ExportField(oldic);
								await doc.ExportField(newic);
								await doc.ExportField(passport);
								await doc.ExportField(address1);
								await doc.ExportField(address2);
								await doc.ExportField(Id_city);
								await doc.ExportField(Id_state);
								await doc.ExportField(Id_country);
								await doc.ExportField(postcode);
								await doc.ExportField(gender);
								await doc.ExportField(tel);
								await doc.ExportField(handphone);
								await doc.ExportField(_email);
								await doc.ExportField(dob);
								await doc.ExportField(children);
								await doc.ExportField(datejoin);
								await doc.ExportField(dateresign);
								await doc.ExportField(marriedstatus);
								await doc.ExportField(Id_dept);
								await doc.ExportField(Id_position);
								await doc.ExportField(Id_race);
								await doc.ExportField(photopath);
								await doc.ExportField(report_to);
								await doc.ExportField(login_effectivedate);
								await doc.ExportField(login_disableddate);
								await doc.ExportField(UserLevelId);
								await doc.ExportField(_Username);
								await doc.ExportField(password);
								await doc.ExportField(active);
							} else {
								await doc.ExportField(Id);
								await doc.ExportField(employeeid);
								await doc.ExportField(fname);
								await doc.ExportField(lname);
								await doc.ExportField(oldic);
								await doc.ExportField(newic);
								await doc.ExportField(passport);
								await doc.ExportField(address1);
								await doc.ExportField(address2);
								await doc.ExportField(Id_city);
								await doc.ExportField(Id_state);
								await doc.ExportField(Id_country);
								await doc.ExportField(postcode);
								await doc.ExportField(gender);
								await doc.ExportField(tel);
								await doc.ExportField(handphone);
								await doc.ExportField(_email);
								await doc.ExportField(dob);
								await doc.ExportField(children);
								await doc.ExportField(datejoin);
								await doc.ExportField(dateresign);
								await doc.ExportField(marriedstatus);
								await doc.ExportField(Id_dept);
								await doc.ExportField(Id_position);
								await doc.ExportField(Id_race);
								await doc.ExportField(report_to);
								await doc.ExportField(login_effectivedate);
								await doc.ExportField(login_disableddate);
								await doc.ExportField(UserLevelId);
								await doc.ExportField(_Username);
								await doc.ExportField(password);
								await doc.ExportField(active);
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

			// User ID filter
			public string GetUserIDFilter(object userid) {
				string userIdFilter = "[Id] = " + QuotedValue(userid, Config.DataTypeNumber, Config.UserTableDbId);
				string parentUserIdFilter = "[Id] IN (SELECT [Id] FROM [dbo].[s_employee] WHERE [report_to] = " + QuotedValue(userid, Config.DataTypeNumber, Config.UserTableDbId) + ")";
				userIdFilter = "(" + userIdFilter + ") OR (" + parentUserIdFilter + ")";
				return userIdFilter;
			}

			// Add User ID filter
			public string AddUserIDFilter(string filter = "") {
				string filterWrk = "";
				var id = (CurrentPageID() == "list") ? CurrentAction : CurrentPageID();
				if (!UserIDAllow(id) && !Security.IsAdmin) {
					filterWrk = Security.UserIDList();
					if (!Empty(filterWrk))
						filterWrk = "[Id] IN (" + filterWrk + ")";
				}

				// Call User ID Filtering event
				UserID_Filtering(ref filterWrk);
				AddFilter(ref filter, filterWrk);
				return filter;
			}

			// Add Parent User ID filter
			public string AddParentUserIDFilter(object userid) {
				if (!Security.IsAdmin) {
					string filterWrk = Security.ParentUserIDList(userid);
					if (!Empty(filterWrk))
						filterWrk = "[Id] IN (" + filterWrk + ")";
					return filterWrk;
				}
				return "";
			}

			// User ID subquery
			public string GetUserIDSubquery(DbField fld, DbField masterfld) {
				string wrk = "";
				string sql = "SELECT " + masterfld.Expression + " FROM [dbo].[s_employee]";
				string filter = AddUserIDFilter();
				if (!Empty(filter))
					sql += " WHERE " + filter;

				// Use subquery
				if (Config.UseSubqueryForMasterUserId) { // Use subquery
					wrk = sql;
				} else { // List all values
					var list = Connection.GetRowsAsync(sql).GetAwaiter().GetResult();
					wrk = String.Join(",", list.Select(d => QuotedValue(d.Values.First(), masterfld.DataType, Config.UserTableDbId)));
				}
				if (!Empty(wrk))
					wrk = fld.Expression + " IN (" + wrk + ")";
				return wrk;
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
