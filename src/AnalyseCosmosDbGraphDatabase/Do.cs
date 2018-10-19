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
            var collectionInfo = _analyze.GetCollectionInfoAsync().GetAwaiter().GetResult();

            Console.WriteLine($"Collection Size Quota: {collectionInfo.collectionSizeQuota/1024:N} mb (The maximum size of a collection)");
            Console.WriteLine($"Collection Size Usage: {collectionInfo.collectionSizeUsage/1024:N} mb (The current size of a collection)");
            Console.WriteLine($"Document Quota: {collectionInfo.documentQuota/1024:N} mb (The maximum size of a documents within a collection)");
            Console.WriteLine($"Document Usage: {collectionInfo.documentUsage/1024:N} mb (The current size of documents within a collection)");
            Console.WriteLine($"Documents: {collectionInfo.documentsCount:N} (Number of documents (nodes/edges) in the collection");

            // Partition Info
            Console.WriteLine("\n");
            var listData = new List<List<object>>();
            listData.Add(new List<object> { "Partition Range", "Number of Documents", "Size in KB", "Partitition Key Info" });
            foreach (var partitionRangeInfo in collectionInfo.partitionInfo)
            {
                var row = new List<object> { partitionRangeInfo.PartitionKeyRangeId,
                    partitionRangeInfo.DocumentCount,
                    partitionRangeInfo.SizeInKB };

                var column = new StringBuilder();
                foreach (var partitionKeyInfo in partitionRangeInfo.PartitionKeyStatistics)
                {
                    column.Append($"{partitionKeyInfo.PartitionKey} {partitionKeyInfo.SizeInKB}, ");
  
                }
                row.Add(column.ToString().TrimEnd(new[] { ',', ' ' }));

                listData.Add(row);
            }

            ConsoleTableBuilder.From(listData).WithFormat(ConsoleTableBuilderFormat.Default).ExportAndWriteLine();

            // Index info
            var indexInfo = _analyze.GetIndexInfoAsync().GetAwaiter().GetResult();
            Console.WriteLine("\n");
            Console.WriteLine($"Indexing Mode: {indexInfo.IndexingMode}");
            Console.WriteLine($"Index Automatic: {indexInfo.Automatic}");
            Console.WriteLine($"Included Paths:");
            foreach (var path in indexInfo.IncludedPaths)
            {
                Console.WriteLine($"\tPath: {path.Path}");
                foreach (var index in path.Indexes)
                {
                    Console.WriteLine($"\t\t{index.Kind} {index.DataType} {index.Precision}");

                }
            }
            Console.WriteLine($"Excluded Paths:");
            foreach (var path in indexInfo.ExcludedPaths)
            {
                Console.WriteLine($"\t{path.Path}");
            }


        }
    }
}
