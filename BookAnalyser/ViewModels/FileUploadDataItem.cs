using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookAnalyser.ViewModels
{
    public class FileUploadDataItem
    {
        public String FileName { get; set; }
        public int TopRows { get; set; }
        public int MinWordLength { get; set; }
    }
}
