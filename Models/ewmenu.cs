// Models
namespace AspNetMaker2019.Models {

	// Partial class
	public partial class SampleProject {

		// Menu language
		public static Lang MenuLanguage;

		// Set up menus
		public static void SetupMenus() {

			// Menu Language
			if (Language != null && Language.LanguageFolder == Config.LanguageFolder)
				MenuLanguage = Language;
			else
				MenuLanguage = new Lang();

			// Navbar menu
			var topMenu = new Menu("navbar", true, true);
			TopMenu = topMenu.ToScript();

			// Sidebar menu
			var sideMenu = new Menu("menu", true, false);
			sideMenu.AddMenuItem(1, "mi_s_armaster", MenuLanguage.MenuPhrase("1", "MenuText"), "s_armasterlist", -1, "", AllowList("{8543F230-11C6-4105-B51C-8D87C21BE659}s_armaster"), false, false, "", "", false);
			sideMenu.AddMenuItem(2, "mi_s_dodetltest", MenuLanguage.MenuPhrase("2", "MenuText"), "s_dodetltestlist?cmd=resetall", -1, "", AllowList("{8543F230-11C6-4105-B51C-8D87C21BE659}s_dodetltest"), false, false, "", "", false);
			sideMenu.AddMenuItem(3, "mi_s_dohdrtest", MenuLanguage.MenuPhrase("3", "MenuText"), "s_dohdrtestlist", -1, "", AllowList("{8543F230-11C6-4105-B51C-8D87C21BE659}s_dohdrtest"), false, false, "", "", false);
			sideMenu.AddMenuItem(4, "mi_s_employee", MenuLanguage.MenuPhrase("4", "MenuText"), "s_employeelist", -1, "", AllowList("{8543F230-11C6-4105-B51C-8D87C21BE659}s_employee"), false, false, "", "", false);
			sideMenu.AddMenuItem(5, "mi_s_servicetype", MenuLanguage.MenuPhrase("5", "MenuText"), "s_servicetypelist", -1, "", AllowList("{8543F230-11C6-4105-B51C-8D87C21BE659}s_servicetype"), false, false, "", "", false);
			sideMenu.AddMenuItem(6, "mi_s_taxmaster", MenuLanguage.MenuPhrase("6", "MenuText"), "s_taxmasterlist", -1, "", AllowList("{8543F230-11C6-4105-B51C-8D87C21BE659}s_taxmaster"), false, false, "", "", false);
			sideMenu.AddMenuItem(7, "mi_UserLevelPermissions", MenuLanguage.MenuPhrase("7", "MenuText"), "UserLevelPermissionslist", -1, "", AllowList("{8543F230-11C6-4105-B51C-8D87C21BE659}UserLevelPermissions"), false, false, "", "", false);
			sideMenu.AddMenuItem(8, "mi_UserLevels", MenuLanguage.MenuPhrase("8", "MenuText"), "UserLevelslist", -1, "", AllowList("{8543F230-11C6-4105-B51C-8D87C21BE659}UserLevels"), false, false, "", "", false);
			sideMenu.AddMenuItem(9, "mi_s_services", MenuLanguage.MenuPhrase("9", "MenuText"), "s_serviceslist", -1, "", AllowList("{8543F230-11C6-4105-B51C-8D87C21BE659}s_services"), false, false, "", "", false);
			sideMenu.AddMenuItem(10, "mi_s_currency", MenuLanguage.MenuPhrase("10", "MenuText"), "s_currencylist", -1, "", AllowList("{8543F230-11C6-4105-B51C-8D87C21BE659}s_currency"), false, false, "", "", false);
			sideMenu.AddMenuItem(11, "mi_s_argltrx", MenuLanguage.MenuPhrase("11", "MenuText"), "s_argltrxlist", -1, "", AllowList("{8543F230-11C6-4105-B51C-8D87C21BE659}s_argltrx"), false, false, "", "", false);
			sideMenu.AddMenuItem(12, "mi_s_artrans", MenuLanguage.MenuPhrase("12", "MenuText"), "s_artranslist", -1, "", AllowList("{8543F230-11C6-4105-B51C-8D87C21BE659}s_artrans"), false, false, "", "", false);
			sideMenu.AddMenuItem(13, "mi_s_glhistory", MenuLanguage.MenuPhrase("13", "MenuText"), "s_glhistorylist", -1, "", AllowList("{8543F230-11C6-4105-B51C-8D87C21BE659}s_glhistory"), false, false, "", "", false);
			sideMenu.AddMenuItem(14, "mi_s_glchart", MenuLanguage.MenuPhrase("14", "MenuText"), "s_glchartlist", -1, "", AllowList("{8543F230-11C6-4105-B51C-8D87C21BE659}s_glchart"), false, false, "", "", false);
			SideMenu = sideMenu.ToScript();
		}
	}
}
