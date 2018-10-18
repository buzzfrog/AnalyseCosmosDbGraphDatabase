using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnalyseCosmosDbGraphDatabase
{
    class CommandLineOptions
    {
        [Option('c', "connection-string", Required = true, HelpText = "Connection String")]
        public string ConnectionString { get; set; }
    }
}
