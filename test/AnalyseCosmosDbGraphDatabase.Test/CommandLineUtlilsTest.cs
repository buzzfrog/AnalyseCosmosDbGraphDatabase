using System;
using System.Linq;
using Xunit;
using AnalyseCosmosDbGraphDatabase;

namespace AnalyseCosmosDbGraphDatabase.Test.CommandLine
{
    public class CommandLineUtlilsTest
    {

        [Fact]
        public void Parsed_connection_string_should_return_all_tuples()
        {
            string accountEndpoint = "https://graph-database.gremlin.cosmosdb.azure.com:443/";
            string accountKey = "FakeKeyU8WB0cNFR0QvWT0jBouMnIqYuavySbwmYK3Ur2xvNBuVhAv3HHnrxhYBNf3dO2Kugbw==";
            string database = "db001";
            string collection = "col003";

            
            var result = CommandLineUtils.ParseCosmosDbConnectionString("AccountEndpoint=https://graph-database.gremlin.cosmosdb.azure.com:443/;AccountKey=FakeKeyU8WB0cNFR0QvWT0jBouMnIqYuavySbwmYK3Ur2xvNBuVhAv3HHnrxhYBNf3dO2Kugbw==;ApiKind=Gremlin;Database=db001;Collection=col003");

            Assert.Equal(result.accountEndpoint, accountEndpoint);
            Assert.Equal(result.accountKey, accountKey);
            Assert.Equal(result.database, database);
            Assert.Equal(result.collection, collection);
        }

        [Fact]
        public void If_all_paramters_is_valid_return_true()
        {
            string accountSqlEndpoint = "https://graph-database.documents.azure.com:443/";
            string accountKey = "dslkldkf";
            string database = "db";
            string collection = "col";

            var result = CommandLineUtils.AreCosmosDbParametersValid((accountSqlEndpoint, accountKey, database, collection));

            Assert.True(result);
        }

        [Fact]
        public void If_collection_is_missing_return_false()
        {
            string accountSqlEndpoint = "https://graph-database.documents.azure.com:443/";
            string accountKey = "dslkldkf";
            string database = "db";
            string collection = "";

            var result = CommandLineUtils.AreCosmosDbParametersValid((accountSqlEndpoint, accountKey, database, collection));

            Assert.False(result);
        }

        [Fact]
        public void Converted_endpoint_should_return_sqlapi_endpoint()
        {
            string accountGraphEndpoint = "https://graph-database.gremlin.cosmosdb.azure.com:443/";
            string accountSqlEndpoint = "https://graph-database.documents.azure.com:443/";

            var result = CommandLineUtils.ConvertToSqlApiEndpoint(accountGraphEndpoint);

            Assert.Equal(accountSqlEndpoint, result);

        }

    }
}
