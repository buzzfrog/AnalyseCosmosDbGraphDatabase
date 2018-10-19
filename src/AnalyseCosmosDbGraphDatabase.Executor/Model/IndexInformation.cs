using System;
using System.Collections.Generic;
using System.Text;

namespace AnalyseCosmosDbGraphDatabase.Executor.Model
{
    public class IndexInformation
    {
        public string IndexingMode { get; set; }
        public bool Automatic { get; set; }
        public List<IndexPath> IncludedPaths { get; set; }
        public List<IndexPath> ExcludedPaths { get; set; }

    }
}
