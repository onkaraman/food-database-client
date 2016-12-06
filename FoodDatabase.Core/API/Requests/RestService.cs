using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FoodDatatase.Core.API.Requests
{
    /// <summary>
    /// Offers methods to easily create a REST request.
    /// Will be utilized by the RestClient.
    /// </summary>
    public class RestRequest
    {
        #region Fields
        public enum Methods {Get, Post, Put, Delete}
        public Methods Method;
        public bool HasPostParams
        {
            get
            {
                if (_pairs.Count > 0) return true;
                return false;
            }
        }
        private bool _hasGetParams;
        public bool HasGetParams
        {
            get
            {
                return _hasGetParams;
            }
        }
        private string _requestURL;
        public string RequestURL { get { return _requestURL; } }
        private string _json;
        public string JsonBody { get { return _json; } }

        private List<KeyValuePair<string, string>> _pairs 
            = new List<KeyValuePair<string,string>>();
        public List<KeyValuePair<string,string>> PostParams
        {
            get
            {
                return _pairs;
            }
        }

        private List<string> _getParams = new List<string>();
        #endregion

        /// <summary>
        /// Will create a RestRequest object.
        /// </summary>
        /// <param name="reqUrl">This request URL will be concatenated with the base URL of the RestClient.</param>
        public RestRequest(string reqUrl)
        {
            if (reqUrl.StartsWith("/")) reqUrl = reqUrl.Substring(1, reqUrl.Length-1);
            _requestURL = reqUrl;
        }

        /// <summary>
        /// Will add further POST parameters to the request.
        /// </summary>
        /// <param name="name">Name of the parameter.</param>
        /// <param name="value">Value of the parameter.</param>
        public void AddPostParam(string name, string value)
        {
            _pairs.Add(new KeyValuePair<string, string>(name, value));
            Method = Methods.Post;
        }

        /// <summary>
        /// Will add further GET parameters to the request.
        /// </summary>
        /// <param name="name">Name of the parameter.</param>
        /// <param name="value">Value of the parameter.</param>
        public void AddGetParam(string name, string value)
        {
            _hasGetParams = true;
            if (_getParams.Count > 0) _getParams.Add(String.Format("&{0}={1}", name, value));
            else _getParams.Add(String.Format("?{0}={1}", name, value));
        }

        /// <summary>
        /// Will remove all GET parameters for this request.
        /// </summary>
        public void RemoveAllGetParams()
        {
            _getParams.Clear();
        }

        /// <summary>
        /// Will take the list of pre-formatted GET parameters and concatenate them a single string.
        /// </summary>
        public void ApplyGetParams()
        {
            foreach (string s in _getParams) _requestURL += s;
        }

        /// <summary>
        /// Will add an (anonymous) object as a JSON object to the request.
        /// </summary>
        /// <param name="o">O.</param>
        public void AddJson(Object o)
        {
            _json = JsonConvert.SerializeObject(o);
            Method = Methods.Post;
        }

        /// <summary>
        /// Will add a JSON-Formatted string as a JSON-Body for this request.
        /// </summary>
        /// <param name="o">O.</param>
        public void AddJson(string o)
        {
            _json = o;
            Method = Methods.Post;
        }

    }
       
    /// <summary>
    /// Serves as a request-class for REST based APIs.
    /// Operates with encapsuled Request-Objects.
    /// </summary>
    public class RestClient
    {
        private string _base;
        private HttpClient _client;

        public RestClient(string baseUrl)
        {
            if (!baseUrl.EndsWith("/")) baseUrl += "/";
            _base = baseUrl;
            _client = new HttpClient();
        }

        /// <summary>
        /// Will execute a request object. JSON, POST and GET can not be executed together.
        /// </summary>
        public async Task<string> Execute(RestRequest req)
        {
            if (req.JsonBody != null)
            {
                string url = String.Concat(_base, req.RequestURL);
                if (req.HasGetParams) url = generateGETURL(req);

                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage res = null;
                if (req.Method == RestRequest.Methods.Post)
                {
                    res = await _client.PostAsync(url,
                        new StringContent(req.JsonBody, Encoding.UTF8, "application/json"));
                }
                else if (req.Method == RestRequest.Methods.Put)
                {
                    res = await _client.PutAsync(url,
                        new StringContent(req.JsonBody, Encoding.UTF8, "application/json")); 
                }

                return await res.Content.ReadAsStringAsync();
            }
            else if (req.HasPostParams)
            {
                string url = string.Concat(_base, req.RequestURL);
                if (req.HasGetParams) url = generateGETURL(req);

                var content = new FormUrlEncodedContent(req.PostParams);
                HttpResponseMessage res = _client.PostAsync(url, content).Result;
                return res.Content.ReadAsStringAsync().Result;
            }
            else
            {
                string url = generateGETURL(req);

                var response = await _client.GetByteArrayAsync(url);
                return Encoding.GetEncoding("ISO-8859-1").GetString(response, 0, response.Length);
            }
        }

        private string generateGETURL(RestRequest req)
        {
            req.ApplyGetParams();
            return String.Concat(_base, req.RequestURL);
        }
    }
}
