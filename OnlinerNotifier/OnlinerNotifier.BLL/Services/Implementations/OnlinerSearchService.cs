using System.IO;
using System.Net;

namespace OnlinerNotifier.BLL.Services.Implementations
{
    public class OnlinerSearchService : IOnlinerSearchService
    {
        public string Search(string productName)
        {
            var requestString = $"https://catalog.api.onliner.by/search/products?query={productName}";
            var request = (HttpWebRequest)WebRequest.Create(requestString);
            request.Method = "GET";
            request.Accept = "application/json";
            var response = (HttpWebResponse)request.GetResponse();
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
