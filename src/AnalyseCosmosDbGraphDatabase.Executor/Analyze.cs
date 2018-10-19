using AnalyseCosmosDbGraphDatabase.Executor.Interface;
using AnalyseCosmosDbGraphDatabase.Executor.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AnalyseCosmosDbGraphDatabase.Executor
{
    public class Analyze
    {
        private IDatabase _database;
        
        public Analyze(IDatabase database)
        {
            _database = database;
        }

        public async Task<int> GetNumberOfVertexesAsync()
        {
            return await _database.GetNumberOfVertexesAsync();
        }

        public async Task<(long collectionSizeQuota,
                           long collectionSizeUsage,
                           long documentQuota,
                           long documentUsage,
                           long documentsCount,
                           List<PartitionKeyRangeStatistics> partitionInfo)> GetCollectionInfoAsync()
        {

            var info = await _database.GetCollectionInfoAsync();
            var currentResourceQuotaInfo = ParseCurrentResourceQuotaUsage(info.currentResourceQuotaUsage);

            return (info.collectionSizeQuota, 
                    info.collectionSizeUsage, 
                    info.documentQuota, 
                    info.databaseUsage, 
                    currentResourceQuotaInfo.documentsCount, 
                    info.partitionInfoList);
        }

        public (int storedProcedures, 
                int triggers, 
                int functions, 
                long documentsCount, 
                long documentsSize, 
                long collectionsSize) ParseCurrentResourceQuotaUsage(string currentResourceQuota)
        {
            //var entities = currentResourceQuota.Split(';');
            var entities = new Dictionary<string, object>();

            foreach (var entity in currentResourceQuota.Split(';'))
            {
                entities.Add(entity.Split('=')[0], entity.Split('=')[1]);
            }

            return (Convert.ToInt32(entities["storedProcedures"]),
                    Convert.ToInt32(entities["triggers"]),
                    Convert.ToInt32(entities["functions"]),
                    Convert.ToInt32(entities["documentsCount"]),
                    Convert.ToInt64(entities["documentsSize"]),
                    Convert.ToInt64(entities["collectionSize"]));

        }

        public async Task<IndexInformation> GetIndexInfoAsync()
        {
            return await _database.GetIndexInfoAsync();
        }
    }
}
