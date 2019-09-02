using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

namespace Globalization.Resources
{
	public static class Resource
	{
		private static IStringLocalizer stringLocalizer = null;

		public static CultureInfo GetCurrentCulture() => Thread.CurrentThread.CurrentCulture;
		public static IEnumerable<CultureInfo> GetAllCultures() => ((JsonStringLocalizer)stringLocalizer).GetAllCultures();

						public static string Index_Page_Title => stringLocalizer["Index_Page_Title"];
								public static string Welcome_Title => stringLocalizer["Welcome_Title"];
					}
}