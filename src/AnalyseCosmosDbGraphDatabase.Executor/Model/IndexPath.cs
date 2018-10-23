using System.Collections.Generic;

namespace AnalyseCosmosDbGraphDatabase.Executor.Model
{
    public class IndexPath
    {
        public string Path { get; set; }
        public List<Index> Indexes { get; set; }
    }
}