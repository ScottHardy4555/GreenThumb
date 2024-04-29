using System.Linq.Expressions;

namespace GreenThumb.Models.DataLayer
{
	public class QueryOptions<T>
	{
		//public properties for sorting and filtering
		public Expression<Func<T, Object>> OrderBy { get; set; }
		public Expression<Func<T, Object>> ThenOrderBy { get; set; }
		public Expression<Func<T, bool>> Where { get; set; }
		private string[] includes = Array.Empty<string>();

		//public string array for include statements
		public string Includes
		{
			set => includes = value.Replace(" ", "").Split(',');
		}

		//public get method for include statements - returns private string array or empty string array
		public string[] GetIncludes() => includes;

		//read-only properties
		public bool HasWhere => Where != null;
		public bool HasOrderBy => OrderBy != null;
		public bool HasThenOrderBy => ThenOrderBy != null;

	}
}
