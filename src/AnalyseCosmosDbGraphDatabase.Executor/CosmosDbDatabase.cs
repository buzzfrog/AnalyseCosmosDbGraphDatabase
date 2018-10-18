using AnalyseCosmosDbGraphDatabase.Executor.Interface;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using My = AnalyseCosmosDbGraphDatabase.Executor;

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
                    partitionInfo.PartitionKeyStatistics.Add(new PartitionKeyStatistics()
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
    }
}
