using BookAnalyserLib;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace AzureFunctionTest
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var bookAnalyser = BookAnalyserFactory.GetBookAnalyser(BookAnalysisMethod.File);

            if (bookAnalyser != null)
            {
               var bookString= await File.ReadAllTextAsync("warpeace.txt");
                var url = "http://localhost:7071/api/AnalyseBook/" + bookString+"/1/50";
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage res = await client.GetAsync(url))
                    {
                        using (HttpContent content = res.Content)
                        {
                            string data = await content.ReadAsStringAsync();
           
                        }
                    }
                }

            }
          
        }
    }
}
