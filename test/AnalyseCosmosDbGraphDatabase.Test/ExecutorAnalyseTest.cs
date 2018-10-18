using AnalyseCosmosDbGraphDatabase.Executor;
using AnalyseCosmosDbGraphDatabase.Executor.Interface;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace AnalyseCosmosDbGraphDatabase.Test.Executor
{
    public class ExecutorAnalyseTest
    {
        [Fact]
        public async void Asking_for_numbers_of_vertexes_should_return_2000()
        {
            var database = new Mock<IDatabase>();
            database.Setup(x => x.GetNumberOfVertexesAsync()).Returns(Task.FromResult<int>(2000));

            var analyze = new Analyze(database.Object);
            var result = await analyze.GetNumberOfVertexesAsync();

            database.Verify(x => x.GetNumberOfVertexesAsync(), Times.Once());
            Assert.Equal(2000, result);
        }

        [Fact]
        public void Parse_CurrentResourceQuotaUsage_and_get_all_information()
        {
            var currentResourceQuotaUsage = "storedProcedures=1;triggers=2;functions=3;documentsCount=40014370;documentsSize=66177887;collectionSize=76728547";
            var analyse = new Analyze(null);

            var result = analyse.ParseCurrentResourceQuotaUsage(currentResourceQuotaUsage);

            Assert.Equal(1, result.storedProcedures);
            Assert.Equal(2, result.triggers);
            Assert.Equal(3, result.functions);
            Assert.Equal(40014370, result.documentsCount);
            Assert.Equal(66177887, result.documentsSize);
            Assert.Equal(76728547, result.collectionsSize);
        }

        [Fact]
        public void GetCollectionInfo_returns_right_expectations()
        {
            var currentResourceQuotaUsage = "storedProcedures=1;triggers=2;functions=3;documentsCount=40014370;documentsSize=66177887;collectionSize=76728547";

            var database = new Mock<IDatabase>();
            database.Setup(x => x.GetCollectionInfoAsync()).Returns(Task.FromResult((
                long.MaxValue - 1001,
                long.MaxValue - 1002, // collectionSizeQuota
                long.MaxValue - 1003,
                long.MaxValue - 1004,
                long.MaxValue - 1004,
                long.MaxValue - 1005,
                long.MaxValue - 1006,
                long.MaxValue - 1007,
                long.MaxValue - 1008,
                long.MaxValue - 1009,
                currentResourceQuotaUsage,
                (List<PartitionKeyRangeStatistics>) null)));

            var analyze = new Analyze(database.Object);
            var result = analyze.GetCollectionInfoAsync().GetAwaiter().GetResult();

            Assert.Equal(long.MaxValue - 1002, result.collectionSizeQuota);
            Assert.Equal(40014370, result.documentsCount);
        }

    }
}
