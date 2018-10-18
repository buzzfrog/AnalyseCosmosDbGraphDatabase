using AnalyseCosmosDbGraphDatabase.Executor;
using CommandLine;
using Microsoft.Azure.Documents.Client;
using System;

namespace AnalyseCosmosDbGraphDatabase
{
    class Program
    {
        static void Main(string[] args)
        {
            var resultFromParsing = Parser.Default.ParseArguments<CommandLineOptions>(args);
            if (resultFromParsing.Tag != ParserResultType.Parsed)
                return;

            var result = (Parsed<CommandLineOptions>)resultFromParsing;

            var unparsedConnectionString = result.Value.ConnectionString;
            var cosmosDbSqlConnectionInformation = CommandLineUtils.ParseCosmosDbConnectionString(unparsedConnectionString);

            var analyze = new Analyze(new CosmosDbDatabase(
                cosmosDbSqlConnectionInformation.accountEndpoint,
                cosmosDbSqlConnectionInformation.accountKey,
                cosmosDbSqlConnectionInformation.database,
                cosmosDbSqlConnectionInformation.collection
            ));

            var doit = new Do(analyze);


        }
    }
}
