﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Gremlin.Net;
using My = AnalyseCosmosDbGraphDatabase.Executor;

namespace AnalyseCosmosDbGraphDatabase.Executor.Interface
{
    public interface IDatabase
    {
        Task<int> GetNumberOfVertexesAsync();

        Task<(long collectionQuota,
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
            List<My.PartitionKeyRangeStatistics> partitionInfoList
            )> GetCollectionInfoAsync();
    }
}