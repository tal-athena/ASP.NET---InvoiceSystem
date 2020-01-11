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
		/// Page class
		/// </summary>

		public class UploadHandler
		{
			public string UploadTable;

			// Page terminated // DN
			private bool _terminated = false;

			// Download file content
			public async Task<IActionResult> DownloadFileContent() {
				var name = Get("id");
				UploadTable = Get("table");
				var filename = Get(name);
				var folder = UploadTempPath(name, UploadTable);
				var version = Get("version");
				if (!Empty(version))
					folder = PathCombine(folder, version, true);

				// Show file content (Config.ImageAllowedFileExtensions and Config.DownloadAllowedFileExtensions only)
				var ar = Config.ImageAllowedFileExtensions.Concat(Config.DownloadAllowedFileExtensions).ToArray();
				var file = IncludeTrailingDelimiter(folder, true) + filename;
				if (Regex.IsMatch(filename, @"\.(" + String.Join("|", ar) + ")$")) {
					if (FileExists(file)) {
						var value = await FileReadAllBytes(file);
						NoCache();
						var contentType = ContentType(value, filename);
						return Controller.File(value, contentType, filename);
					}
				}
				return new EmptyResult();
			}

			// Delete file
			public IActionResult Delete() {
				var name = Get("id");
				if (name != "") {
					UploadTable = Get("table");
					var filename = Get(name);
					var folder = UploadTempPath(name, UploadTable);
					DeleteFile(IncludeTrailingDelimiter(folder, true) + filename);
					var version = Config.UploadThumbnailFolder;
					folder = PathCombine(folder, version, true);
					DeleteFile(IncludeTrailingDelimiter(folder, true) + filename);
					return Controller.Json(new { success = true });
				}
				return new EmptyResult();
			}

			// Download file list
			public async Task<IActionResult> DownloadFileList() {
				var name = Get("id");
				var token = Get(Config.TokenName);
				UploadTable = Get("table");
				var files = new List<object[]>();
				if (name != "") {
					var folder = UploadTempPath(name, UploadTable);
					if (DirectoryExists(folder)) {
						var ar = GetFiles(folder);
						foreach (var file in ar) {
							var value = await FileReadAllBytes(file);
							var filesize = value.Length;
							var filetype = ContentType(value, file);
							files.Add(new object[] {name, Path.GetFileName(file), filetype, filesize, token});
						}
					}
					return OutputJson(name, files);
				}
				return new EmptyResult();
			}

			// Upload file
			public async Task<IActionResult> Upload() {
				if (Files.Count > 0) { // DN
					var lang = new Lang();
					var form = new HttpForm();
					var name = form.GetValue("id");
					UploadTable = form.GetValue("table");
					var folder = UploadTempPath(name, UploadTable);
					var token = form.GetValue(Config.TokenName);
					var exts = form.GetValue("exts");
					var extList = exts.Split(',');
					if (!Empty(Config.UploadAllowedFileExtensions)) {
						var allowedExtList = new List<string>(Config.UploadAllowedFileExtensions.Split(','));
						exts = String.Join(",", extList.Where(ext => allowedExtList.Contains(ext, StringComparer.OrdinalIgnoreCase))); // Make sure exts is a subset of Config.UploadAllowedFileExtensions
						if (Empty(exts))
							exts = Config.UploadAllowedFileExtensions;
					}
					if (Empty(exts))
						exts = @"\w+";
					var filetypes = @"\.(" + exts.Replace(",", "|") + ")$";
					var maxsize = form.GetInt("maxsize");
					var maxfilecount = form.GetInt("maxfilecount");
					var filename = form.GetUploadFileName(name);

					// Skip if no file uploaded
					if (Empty(filename))
						return Controller.BadRequest("Missing file name");
					if (Config.UploadConvertAccentedChars) {
						filename = HtmlEncode(filename);
						filename = Regex.Replace(filename, @"&([a-zA-Z])(uml|acute|grave|circ|tilde|cedil);", "$1");
						filename = HtmlDecode(filename);
					}
					var filetype = form.GetUploadFileContentType(name);
					var filesize = form.GetUploadFileSize(name);
					var value = await form.GetUploadFileData(name);

					// Check file types
					if (!Regex.IsMatch(filename, filetypes, RegexOptions.IgnoreCase)) {
						var fileerror = lang.Phrase("UploadErrMsgAcceptFileTypes");
						return OutputJson("files", new List<object[]> { new object[] { name, filename, filetype, filesize, token, fileerror }});
					}

					// Check file size
					if (maxsize < filesize) {
						var fileerror = lang.Phrase("UploadErrMsgMaxFileSize");
						return OutputJson("files", new List<object[]> { new object[] { name, filename, filetype, filesize, token, fileerror }});
					}

					// Check max file count
					var filecount = FolderFileCount(folder);
					if (maxfilecount > 0 && maxfilecount <= filecount) {
						var fileerror = lang.Phrase("UploadErrMsgMaxNumberOfFiles");
						return OutputJson("files", new List<object[]> { new object[] { name, filename, filetype, filesize, token, fileerror } });
					}

					// Delete all files in directory if replace
					var version = Config.UploadThumbnailFolder;
					if (form.GetBool("replace"))
						CleanPath(folder, false);
					await SaveFile(folder, filename, value);
					folder = PathCombine(folder, version, true);
					var w = Config.UploadThumbnailWidth;
					var h = Config.UploadThumbnailHeight;
					ResizeBinary(ref value, ref w, ref h);
					await SaveFile(folder, filename, value);
					return OutputJson("files", new List<object[]> { new object[] { name, filename, filetype, filesize, token } });
				}
				return new EmptyResult();
			}

			// Output JSON
			public IActionResult OutputJson(string id, List<object[]> files) {
				string baseurl, table, url, thumbnail_url, delete_url = "";
				var list = new List<Dictionary<string, object>>();
				if (IsList(files)) {
					foreach (var file in files) {
						if (file.Length >= 5) {
							var name = Convert.ToString(file[0]);
							var filename = Convert.ToString(file[1]);
							var fileerror = (file.Length > 5) ? Convert.ToString(file[5]) : "";
							var version = Config.UploadThumbnailFolder;
							var token = Convert.ToString(file[4]);
							if (Config.DownloadViaScript || Empty(Config.UploadTempPath) || Empty(Config.UploadTempHrefPath)) {
								baseurl = FullUrl(CurrentPageName().ToLower(), "upload");
								table = (UploadTable != "") ? "&table=" + UploadTable : "";
								url = baseurl + "?id=" + name + table + "&" + name + "=" + RawUrlEncode(filename) + "&download=1";
								thumbnail_url = baseurl + "?id=" + name + table + "&" + name + "=" + RawUrlEncode(filename) + "&version=" + version + "&download=1";
								delete_url = baseurl + "?id=" + name + table + "&" + name + "=" + RawUrlEncode(filename) + "&delete=1";
							} else {
								baseurl = UploadTempPath("", "", false);
								table = (UploadTable != "") ? UploadTable + "/" : "";
								url = baseurl + table + name + "/" + RawUrlEncode(filename);
								thumbnail_url = baseurl + table + name + "/" + version + "/" + RawUrlEncode(filename);
							}
							url = url + "&" + Config.TokenName + "=" + token;
							thumbnail_url = thumbnail_url + "&" + Config.TokenName + "=" + token;
							var obj = new Dictionary<string, object>();
							obj.Add("name", filename);
							obj.Add("size", ConvertToInt(file[3]));
							obj.Add("type", Convert.ToString(file[2]));
							obj.Add("url", url);
							if (!Empty(fileerror)) {
								obj.Add("error", fileerror);
							} else {
								obj.Add(version + "Url", thumbnail_url);
							}
							obj.Add("deleteUrl", delete_url);
							obj.Add("deleteType", "GET"); // Use GET
							list.Add(obj);
						}
					}
				}

				// Set file header / content type
				NoCache();
				AddHeader(HeaderNames.ContentDisposition, "inline; filename=files.json");

				// Output json
				var dict = new Dictionary<string, List<Dictionary<string, object>>>();
				dict.Add(id, list);
				return Controller.Json(dict); // Returns utf-8 data
			}

			// Page class constructor
			public UploadHandler(Controller controller = null) { // DN
				if (controller != null)
					Controller = controller;
			}

			/// <summary>
			/// Page run
			/// </summary>
			/// <returns>Page result</returns>

			public async Task<IActionResult> Run() { // DN

				// Handle download file content
				if (Get("download") != "") {
					return await DownloadFileContent();
				} else if (Get("delete") != "") { // Handle delete file
					return Delete();
				} else if (Get("id") != "") { // Handle download file list
					return await DownloadFileList();
				} else if (Files.Count > 0) { // Handle upload file (multi-part)
					return await Upload();
				}
				return new EmptyResult();
			}

			// Terminate page
			public IActionResult Terminate(string url = "") { // DN
				if (_terminated)
					return new EmptyResult();
				_terminated = true;
				return new EmptyResult();
			}
		}
	}
}
