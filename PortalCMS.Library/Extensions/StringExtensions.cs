namespace PortalCMS.Library.ExtensionMethods
{
	public static class StringExtensions
	{
		public static bool HasValue(this string value)
		{
			return !string.IsNullOrEmpty(value);
		}
	}
}