using System;
using System.Collections.Generic;
using System.Text;

namespace AnalyseCosmosDbGraphDatabase.Executor
{
    public class PartitionKeyRangeStatistics
    {
        public string PartitionKeyRangeId { get; set; }

        public long SizeInKB { get; set; }

        public long DocumentCount { get; set; }

        public List<PartitionKeyStatistics> PartitionKeyStatistics { get; set; }

    }
}
