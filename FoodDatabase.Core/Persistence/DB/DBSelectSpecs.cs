using FoodDatabase.Core.API.Models.Abstracts;

namespace FoodDatabase.Core.Persistence
{
	/// <summary>
	/// This class should be used to specify the table request.
	/// Given fields will be used to filter, when assigned to.
	/// </summary>
	public class DBSelectSpecs
	{
		public enum SortOrder { Ascending, Descending }

		private APIModel _model;
		public APIModel Model { get { return _model; } }

		private int _max;
		public int Max { get { return _max; } }

        private int _limit;
		public int Limit { get {return _limit;}}

		public DBSelectSpecs (APIModel model, int max = 0, 
			int limit = 0)
		{
			_model = model;
			if (max != 0) _max = max;
			if (limit != 0) _limit = limit;
		}
	}
}

