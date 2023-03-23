namespace WaterLogger.Models
{
	public static class EnumMethods
	{ 
		public static IEnumerable<KeyValuePair<string, int>> GetSelectList<T>() where T : Enum
		{
			return Enum.GetNames(typeof(T))
				.Select(e => new KeyValuePair<string, int>(e, (int)Enum.Parse(typeof(T), e)))
				.ToList();
		}
	}
}
