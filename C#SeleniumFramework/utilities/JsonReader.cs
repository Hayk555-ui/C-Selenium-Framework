using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_SeleniumFramework.utilities
{
    internal class JsonReader
    {
        public JsonReader() { }

        public string? ExtractData(string tokenName)
        {
            var jsonString = File.ReadAllText("utilities/TestData.json");
            var jsonObject = JToken.Parse(jsonString);
            return jsonObject.SelectToken(tokenName).Value<string>();
        }

        public string[] ExtractDataArray(string tokenName)
        {
            var jsonString = File.ReadAllText("utilities/TestData.json");
            var jsonObject = JToken.Parse(jsonString);
            List<string> productsList = jsonObject.SelectTokens(tokenName).Values<string>().ToList();
            return productsList.ToArray();
        }
    }
}
