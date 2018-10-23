using AnalyseCosmosDbGraphDatabase.Executor;
using ConsoleTableExt;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnalyseCosmosDbGraphDatabase
{
    public class Do
    {
        private Analyze _analyze;

        public Do(Analyze analyse)
        {
            _analyze = analyse;

            // Get size of collection
            var info = _analyze.GetCollectionInfoAsync().GetAwaiter().GetResult();

            Console.WriteLine($"Collection Size Quota: {info.collectionSizeQuota/1024:N} mb (The maximum size of a collection)");
            Console.WriteLine($"Collection Size Usage: {info.collectionSizeUsage/1024:N} mb (The current size of a collection)");
            Console.WriteLine($"Document Quota: {info.documentQuota/1024:N} mb (The maximum size of a documents within a collection)");
            Console.WriteLine($"Document Usage: {info.documentUsage/1024:N} mb (The current size of documents within a collection)");
            Console.WriteLine($"Documents: {info.documentsCount:N} (Number of documents (nodes/edges) in the collection");

            // Partition Info
            Console.WriteLine("\n");
            var listData = new List<List<object>>();
            listData.Add(new List<object> { "Partition Range", "Number of Documents", "Size in KB", "Partitition Key Info (max 3 will be shown)" });
            foreach (var partitionRangeInfo in info.partitionInfo)
            {
                var row = new List<object> { partitionRangeInfo.PartitionKeyRangeId,
                    partitionRangeInfo.DocumentCount,
                    partitionRangeInfo.SizeInKB };

                var column = new StringBuilder();
                foreach (var partitionKeyInfo in partitionRangeInfo.PartitionKeyStatistics)
                {
                    column.Append($"{partitionKeyInfo.PartitionKey} {partitionKeyInfo.SizeInKB}kb, ");
  
                }
                row.Add(column.ToString().TrimEnd(new[] { ',', ' ' }));

                listData.Add(row);
            }

            ConsoleTableBuilder.From(listData).WithFormat(ConsoleTableBuilderFormat.Default).ExportAndWriteLine();
        }
    }
}
