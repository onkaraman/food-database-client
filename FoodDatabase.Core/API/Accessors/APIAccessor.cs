using System.Threading.Tasks;
using FoodDatabase.Core.Patterns;
using FoodDatatase.Core.API.Requests;

namespace FoodDatabase.Core.API.Accessors
{
    /// <summary>
    /// This class will access the API of the fddb.de service.
    /// </summary>
    public class APIAccessor: LazyStatic<APIAccessor>
    {
        private readonly string _token = "ICPXKUKUI951264PU43XDE";
        protected readonly RestClient client = new RestClient("http://fddb.info/api/v16/");
        public APIAccessor() {}

        /// <summary>
        /// This method will start an api search with the given query.
        /// </summary>
        public async Task<string> Search(string query)
        {
            RestRequest req = new RestRequest("search/item.xml");
            req.AddGetParam("apikey", _token);
            req.AddGetParam("lang", "de");
            req.AddGetParam("q", query);

            req.Method = RestRequest.Methods.Get;
            return await client.Execute(req);
        }

        /// <summary>
        /// Will return detailed information about a single item.
        /// </summary>
        public async Task<string> GetDetails(string itemID)
        {
            RestRequest req = new RestRequest("item/id_id.xml");
            req.AddGetParam("apikey", _token);
            req.AddGetParam("lang", "de");
            req.AddGetParam("id", itemID);

            req.Method = RestRequest.Methods.Get;
            return await client.Execute(req);
        }
    }
}

