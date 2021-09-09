using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookAnalyserLib
{
    public enum BookAnalysisMethod {File }
    public class BookAnalyserFactory
    {
        public static IBookAnalyser GetBookAnalyser(BookAnalysisMethod bookAnalysisMethod)
        {
            switch (bookAnalysisMethod)
            {
                case BookAnalysisMethod.File: return new FileBookAnalyser();
            }

            return null;
        }
    }
}
