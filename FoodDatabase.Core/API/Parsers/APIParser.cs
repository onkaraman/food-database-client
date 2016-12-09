using System.IO;
using System.Xml.Serialization;
using FoodDatabase.Core.API.Models.Item;
using FoodDatabase.Core.Patterns;

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
        public Result Parse(string xml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Result), new XmlRootAttribute("result"));
            StringReader stringReader = new StringReader(xml);
            return (Result)serializer.Deserialize(stringReader);
        }
    }
}
