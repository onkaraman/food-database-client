using System;
using System.Xml.Linq;
using FoodDatabase.Core.Patterns;
using Newtonsoft.Json;

namespace FoodDatabase.Core.API.Parsers
{
    public class APIParser : LazyStatic<APIParser>
    {
        public APIParser(){}

        /// <summary>
        /// Will take the response of the API for a search
        /// and return a list of item models. The XML will be parsed
        /// to json in order to enable easier handling.
        /// </summary>
        public void ParseSearch(string xml)
        {
            string json = JsonConvert.SerializeXNode(XDocument.Parse(xml));
        }
    }
}
