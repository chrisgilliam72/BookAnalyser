using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookAnalyserLib
{

    public interface IBookAnalyser
    {

        public Task<bool> UploadFile(String documentPath);
        public Task<bool> ProcessBook();
        public IEnumerable<WordCountResult> GetTopResults(int topResultCount = 50, int minwordLength = 6);

        public String GetLastError();

    }
}
