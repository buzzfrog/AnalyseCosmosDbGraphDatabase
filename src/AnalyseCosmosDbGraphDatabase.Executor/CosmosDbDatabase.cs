using AnalyseCosmosDbGraphDatabase.Executor.Interface;
using AnalyseCosmosDbGraphDatabase.Executor.Model;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using My = AnalyseCosmosDbGraphDatabase.Executor.Model;

namespace AnalyseCosmosDbGraphDatabase.Executor
{
    public class CosmosDbDatabase : IDatabase
    {
        private IDocumentClient _documentClient;
        private string _database;
        private string _collection;

        public CosmosDbDatabase(string accountSqlEndpoint, string accountKey, string database, string collection)
        {
            _database = database;
            _collection = collection;

            _documentClient = new DocumentClient(new Uri(accountSqlEndpoint), accountKey);
        }

        public async Task<int> GetNumberOfVertexesAsync()
        {
            FeedOptions queryOptions = new FeedOptions { MaxItemCount = -1 };

            var query = _documentClient.CreateDocumentQuery(
                UriFactory.CreateDocumentCollectionUri(_database, _collection),
                "",
                queryOptions).AsDocumentQuery();
            
            if(query.HasMoreResults)
            {
                var result = await query.ExecuteNextAsync();
         
            }

            return 0;    

        }

        public async Task<(long collectionQuota, 
                           long collectionSizeQuota, 
                           long collectionSizeUsage, 
                           long collectionUsage,
                           long databaseQuota,
                           long databaseUsage,
                           long documentQuota,
                           long documentUsage,
                           long userQuota,
                           long userUsage,
                           string currentResourceQuotaUsage,
                           List<My.PartitionKeyRangeStatistics> partitionInfoList)> GetCollectionInfoAsync()
        {
            ResourceResponse<DocumentCollection> collectionInfo = await _documentClient.ReadDocumentCollectionAsync(UriFactory.CreateDocumentCollectionUri(_database, _collection), new RequestOptions() { PopulateQuotaInfo = true, PopulatePartitionKeyRangeStatistics = true } );

            var partitionInfoList = new List<My.PartitionKeyRangeStatistics>();
            foreach (var partitionKeyRangeStatistics in collectionInfo.Resource.PartitionKeyRangeStatistics)
            {
                var partitionInfo = new My.PartitionKeyRangeStatistics
                {
                    PartitionKeyRangeId = partitionKeyRangeStatistics.PartitionKeyRangeId,
                    DocumentCount = partitionKeyRangeStatistics.DocumentCount,
                    SizeInKB = partitionKeyRangeStatistics.SizeInKB,
                    PartitionKeyStatistics = new List<My.PartitionKeyStatistics>()
                };
                
                foreach (var partitionKeyStatistics in partitionKeyRangeStatistics.PartitionKeyStatistics)
                {
                    partitionInfo.PartitionKeyStatistics.Add(new My.PartitionKeyStatistics()
                    {
                        PartitionKey = partitionKeyStatistics.PartitionKey.ToString(),
                        SizeInKB = partitionKeyStatistics.SizeInKB
                    });
                }

                partitionInfoList.Add(partitionInfo);
            }

            return (collectionInfo.CollectionQuota, 
                    collectionInfo.CollectionSizeQuota, 
                    collectionInfo.CollectionSizeUsage,
                    collectionInfo.CollectionUsage,
                    collectionInfo.DatabaseQuota,
                    collectionInfo.DatabaseUsage,
                    collectionInfo.DocumentQuota,
                    collectionInfo.DocumentUsage,
                    collectionInfo.UserQuota,
                    collectionInfo.UserUsage,
                    collectionInfo.CurrentResourceQuotaUsage,
                    partitionInfoList);
        }

        public async Task<IndexInformation> GetIndexInfoAsync()
        {
            ResourceResponse<DocumentCollection> collectionInfo = 
                await _documentClient.ReadDocumentCollectionAsync(
                    UriFactory.CreateDocumentCollectionUri(_database, _collection));

            var indexingPolicy = collectionInfo.Resource.IndexingPolicy;

            var indexInfo = new IndexInformation()
            {
                Automatic = indexingPolicy.Automatic,
                IndexingMode = indexingPolicy.IndexingMode.ToString(),
                IncludedPaths = new List<IndexPath>(),
                ExcludedPaths = new List<IndexPath>()
            };

            foreach (var includedPath in indexingPolicy.IncludedPaths)
            {
                indexInfo.IncludedPaths.Add(FillIndexInformation(includedPath));
            }
            foreach (var excludedPath in indexingPolicy.ExcludedPaths)
            { 
                indexInfo.ExcludedPaths.Add(new IndexPath() { Path = excludedPath.Path });
            }

            return indexInfo;
        }

        private IndexPath FillIndexInformation(IncludedPath includedPath)
        {
            var ip = new IndexPath() { Path = includedPath.Path, Indexes = new List<My.Index>() };
            foreach (var ind in includedPath.Indexes)
            {
                var index = new My.Index() { Kind = ind.Kind.ToString() };
                switch (ind)
                {
                    case HashIndex hi:
                        index.DataType = hi.DataType.ToString();
                        index.Precision = hi.Precision;
                        break;
                    case RangeIndex ri:
                        index.DataType = ri.DataType.ToString();
                        index.Precision = ri.Precision;
                        break;
                    case SpatialIndex si:
                        index.DataType = si.DataType.ToString();
                        break;
                    default:
                        index.DataType = "unknown";
                        break;
                }
                ip.Indexes.Add(index);
            }

            return ip;
        }
    }
}
