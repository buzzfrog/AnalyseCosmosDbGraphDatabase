# Analyse CosmosDb Graph Database
[![Build Status](https://buzzfrog-pro.visualstudio.com/AnalyseCosmosDbGraphDatabase/_apis/build/status/buzzfrog.AnalyseCosmosDbGraphDatabase)](https://buzzfrog-pro.visualstudio.com/AnalyseCosmosDbGraphDatabase/_build/latest?definitionId=13)

## Introduction
To analyze as much as possible of an existing CosmosDB Graph Database. 

## Command Line Options
### Database Connection String
```
-c AccountEndpoint=https://{cosmosdb_name}.documents.azure.com:443/;AccountKey={primary_key};ApiKind=Gremlin;Database={database_name};Collection={collection_name}
```

Execute
```
dotnet AnalyseCosmosDbGraphDatabase.dll "-c AccountEndpoint..."
```
## Analysis
(It is still rather basic.)
```
Collection Size Quota: 133 120,00 mb (The maximum size of a collection)
Collection Size Usage: 74 952,00 mb (The current size of a collection)
Document Quota: 133 120,00 mb (The maximum size of a documents within a collection)
Document Usage: 0,00 mb (The current size of documents within a collection)
Documents: 40 014 370,00 (Number of documents (nodes/edges) in the collection


-------------------------------------------------------------------------------------------------------------------
| Partition Range | Number of Documents | Size in KB | Partitition Key Info (max 3 will be shown)                 |
-------------------------------------------------------------------------------------------------------------------
| 4               | 2606498             | 5149472    | ["eee17"] 152954kb, ["eee16"] 152954kb, ["ccc15"] 152954kb |
-------------------------------------------------------------------------------------------------------------------
| 1               | 2022219             | 3973434    | ["ccc18"] 196704kb, ["ddd16"] 196704kb, ["ccc10"] 196704kb |
-------------------------------------------------------------------------------------------------------------------
| 2               | 2172013             | 4289675    | ["ccc9"] 169888kb, ["ddd1"] 169888kb, ["bbb20"] 169888kb   |
-------------------------------------------------------------------------------------------------------------------
| 3               | 1901843             | 3747820    | ["ccc6"] 148428kb, ["aaa16"] 148428kb, ["eee23"] 148428kb  |
-------------------------------------------------------------------------------------------------------------------
| 0               | 1988031             | 3769531    | ["bbb6"] 149288kb, ["ddd14"] 149288kb, ["ddd13"] 149288kb  |
-------------------------------------------------------------------------------------------------------------------
```
## Resources
- [ResourceResponse](https://docs.microsoft.com/en-us/dotnet/api/microsoft.azure.documents.client.resourceresponse-1?f1url=https%3A%2F%2Fmsdn.microsoft.com%2Fquery%2Fdev15.query%3FappId%3DDev15IDEF1%26l%3DEN-US%26k%3Dk(Microsoft.Azure.Documents.Client.ResourceResponse%601);k(SolutionItemsProject);k(DevLang-csharp)%26rd%3Dtrue&view=azure-dotnet)

