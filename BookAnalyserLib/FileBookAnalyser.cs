using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookAnalyserLib
{
    public class FileBookAnalyser : IBookAnalyser
    {
        private ConcurrentDictionary<String, int> _wordCountMap;
        private String _bookRawText;
        private String _bookPreprocessedText;
        private String _LastError;


        public void UploadText(String bookText)
        {
            _LastError = "";
            _bookRawText = bookText;
           _bookPreprocessedText = _bookRawText.Replace(",", " ");

        }
        public async Task<bool> UploadFile(String documentPath)
        {
            try
            {
                _LastError = "";
                _bookRawText = await File.ReadAllTextAsync(documentPath);
                _bookPreprocessedText = _bookRawText.Replace(",", " ");

                return true;
            }
            catch (Exception ex)
            {
                _LastError = ex.Message;
                return false;
            }

        }

        public async Task<bool> ProcessBook()
        {
            try
            {
                var tasklist = new Task[2];
                List<String> ListLines = new List<string>();
                _wordCountMap = new ConcurrentDictionary<string, int>();
                _LastError = "";
                if (_bookPreprocessedText!=null)
                {
                    ListLines.AddRange(_bookPreprocessedText.Split("\r\n"));

                    int lineCount = ListLines.Count();
                    int topHalf = lineCount / 2;
                    int bottomHalf = lineCount - topHalf;
                    var topLines = ListLines.Take(topHalf).ToList();
                    var bottomLines = ListLines.TakeLast(bottomHalf).ToList();

                    tasklist[0] = Task.Run(() =>
                    {
                        ProcessLines(topLines);
                    }
                     );

                    tasklist[1] = Task.Run(() =>
                    {
                        ProcessLines(bottomLines);
                    }
                    );


                    await Task.WhenAll(tasklist);
                    return true;
                }
                _LastError = "File error";
                return false;

            }
            catch (Exception ex)
            {
                _LastError = ex.Message;
                return false;
            }

        }

        public IEnumerable<WordCountResult> GetTopResults(int topResultCount = 50, int minwordLength = 6)
        {
            try
            {
                List<WordCountResult> resultsList = new List<WordCountResult>();

                if (_wordCountMap != null && _wordCountMap.Count > 0)
                {
                    var filterdLst = _wordCountMap.Where(x => x.Key.Length >= minwordLength);
                    var topWords = filterdLst.OrderByDescending(x => x.Value).Take(topResultCount).ToList();
                    resultsList.AddRange(topWords.Select(x => new WordCountResult { Word = x.Key, Count = x.Value }));
                }

                return resultsList;
            }
            catch (Exception ex)
            {
                _LastError = ex.Message;
                return null;
            }
        }

        public String GetLastError()
        {
            return _LastError;
        }

        private void ProcessLines(List<String> lineList)
        {

            int wordCount = 0;

            foreach (var line in lineList)
            {
                if (line.Contains("CHAPTER") || line.Contains("BOOK"))
                    continue;
                var wordslist = line.Split();
                foreach (var word in wordslist)
                {
                    if (word.Length > 0)
                    {
                        var wordLower = word.ToLower();
                        if (_wordCountMap.ContainsKey(wordLower))
                            _wordCountMap[wordLower]++;
                        else
                            _wordCountMap.TryAdd(wordLower, 1);

                        wordCount++;
                    }

                }

            }

        }
    }
}
