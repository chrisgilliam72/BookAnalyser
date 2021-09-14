using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using BookAnalyserLib;
using System.Text.Json;

namespace AnalyseBookFunction
{
    public  class AnalyseBook
    {
        private IBookAnalyser _bookAnalyser;
        public AnalyseBook(IBookAnalyser bookAnalyser)
        {
            _bookAnalyser=bookAnalyser;
        }

        [FunctionName("AnalyseBook")]
        public  async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "AnalyseBook/{minWordLength}/{topResultCount}")] HttpRequest req,
           int minWordLength, int topResultCount)
        {

            var formdata = await req.ReadFormAsync();
            var file = req.Form.Files["file"];
            using (Stream fileStream = new FileStream(file.FileName, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
           
            await _bookAnalyser.UploadFile(file.FileName);
            await _bookAnalyser.ProcessBook();
             var results= _bookAnalyser.GetTopResults(topResultCount, minWordLength);
            var resultsJsonString = System.Text.Json.JsonSerializer.Serialize(results);
            return new OkObjectResult(resultsJsonString);
        }
    }
}
