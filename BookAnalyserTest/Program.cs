using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using BookAnalyserLib;

namespace BookAnalyserTest
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var bookAnalyser = BookAnalyserFactory.GetBookAnalyser(BookAnalysisMethod.File);
            if (bookAnalyser!=null)
            {
                await bookAnalyser.UploadFile("warpeace.txt");
                await bookAnalyser.ProcessBook();
                Console.WriteLine("Top words:");

                var topWords = bookAnalyser.GetTopResults();
                foreach (var topword in topWords)
                {
                    Console.WriteLine(topword.Word + " count " + topword.Count);
                }
            }

            Console.ReadLine();
        }
    }
}
