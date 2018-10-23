using System;
using System.Collections.Generic;
using System.Text;

namespace AnalyseCosmosDbGraphDatabase.Executor.Model
{
    public class PartitionKeyStatistics
    {
        public string PartitionKey { get; set; }

        public long SizeInKB { get; set; }
    }
}
