using Microsoft.Azure.Documents;
using Moq;
using System;
using System.Linq;
using Xunit;
using AnalyseCosmosDbGraphDatabase;
using AnalyseCosmosDbGraphDatabase.Executor;
using System.Threading.Tasks;
using Microsoft.Azure.Documents.Client;

namespace AnalyseCosmosDbGraphDatabase.Test.Database
{
    public class DatabaseTest
    {
        [Fact]
        public void Initialize_with_null_accountSqlEndpoint_should_throw_exception()
        {
            Assert.Throws<ArgumentNullException>(() => new CosmosDbDatabase(null, null, null, null));
        }

        [Fact]
        public void Initialize_with_null_accountKey_should_throw_exception()
        {
            Assert.Throws<ArgumentNullException>(() => new CosmosDbDatabase("http://local.net", null, null, null));
        }

        //[Fact]
        //public void GetSizeOfCollectionAsync()
        //{
        //    long databaseQuota = long.MaxValue;
        //    long databaseUsage = long.MaxValue - 1000;


        //    var docClient = new Mock<IDocumentClient>();
        //    var resourceResponse = new Mock<IResourceResponse<DocumentCollection>>();
        //    resourceResponse.Setup(x => x.DatabaseQuota).Returns(databaseQuota);
        //    resourceResponse.Setup(x => x.DatabaseUsage).Returns(databaseUsage);

        //    var r = (ResourceResponse<DocumentCollection>)resourceResponse.Object;

        //    docClient.Setup(x => x.ReadDocumentCollectionAsync(It.IsAny<Uri>(), null))
        //        .Returns(Task.FromResult(new ResourceResponse<DocumentCollection>()));

        //    var db = new CosmosDbDatabase(docClient.Object);
        //    var result = db.GetSizeOfCollectionAsync().GetAwaiter().GetResult();

        //    Assert.Equal(result.dataQuota, long.MaxValue - 1);
        //}
    }
}
