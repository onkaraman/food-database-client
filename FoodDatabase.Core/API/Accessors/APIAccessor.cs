using System;
using System.Threading.Tasks;
using FoodDatabase.Core.Patterns;
using FoodDatabase.Core.Helpers;
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
        /// The start from parameter can be passed to continue from this id + the 10 next
        /// items.
        /// </summary>
        public async Task<string> Search(string query, string startFrom=null)
        {
            RestRequest req = new RestRequest("search/item.xml");
            req.AddGetParam("apikey", _token);
            req.AddGetParam("lang", "de");
            req.AddGetParam("q", query);
            if (startFrom != null) req.AddGetParam("startfrom", startFrom);

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

        /// <summary>
        /// Will get the Diary of the passed date.
        /// </summary>
        /// <returns>The diary day.</returns>
        public async Task<string> DiaryGet(LoginData loginData, DateTime date)
        {
            string url = string.Format("diary/get_day_{0}.xml", date.ToString("dd_MM_yyyy"));
            RestRequest req = new RestRequest(url);
            req.AddBasicAuth(loginData.Username, loginData.Password);
            req.AddGetParam("apikey", _token);
            req.AddGetParam("lang", "de");

            req.Method = RestRequest.Methods.Get;
            return await client.Execute(req);
        }

        /// <summary>
        /// Will add a food item to the diary.
        /// </summary>
        /// <param name="itemID">ID of the food to add.</param>
        /// <param name="customServing">Custom weight of this food.</param>
        /// <param name="servingID">Preset serving for this food.</param>
        public async Task<string> DiaryAddItem(LoginData loginData, string itemID,
                                 int customServing=0, string servingID=null)
        {
            RestRequest req = new RestRequest("diary/add_item.xml");
            req.AddBasicAuth(loginData.Username, loginData.Password);
            req.AddGetParam("apikey", _token);
            req.AddPostParam("item_id", itemID);

            if (customServing > 0) req.AddPostParam("custom_serving", customServing.ToString());
            else if (servingID != null) req.AddPostParam("serving_id", servingID);
            else if (customServing == 0 && servingID == null) 
                throw new Exception("No serving specified.");

            req.Method = RestRequest.Methods.Post;
            return await client.Execute(req);
        }
    
        /// <summary>
        /// Will remove an item from the diary.
        /// </summary>
        public async Task<string> DiaryRemove(LoginData loginData, string diaryUID)
        {
            string url = string.Format("diary/delete_{0}.xml", diaryUID);

            RestRequest req = new RestRequest(url);
            req.AddBasicAuth(loginData.Username, loginData.Password);
            req.AddGetParam("apikey", _token);
            //req.AddGetParam("uid", diaryUID);

            req.Method = RestRequest.Methods.Get;
            return await client.Execute(req);
        }

        public async Task<string> Login(string user, string pass)
        {
            string url = "user/auth.xml";

            RestRequest req = new RestRequest(url);
            req.AddBasicAuth(user, pass);
            req.AddGetParam("apikey", _token);
            req.Method = RestRequest.Methods.Post;
            return await client.Execute(req);
        }
    }
}

