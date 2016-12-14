using FoodDatabase.Core.API.Models.Abstracts;

namespace FoodDatabase.Core.API.Models.Items
{
    /// <summary>
    /// The serving option of an item.
    /// </summary>
    public class Serving : APIModel
    {
        public string serving_id
        {
            get { return id.ToString(); }
            set { id = int.Parse(value); }
        }
        public string name { get; set; }
        public string weight_gram { get; set; }
    }
}
