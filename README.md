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


-----------------------------------------------------------------------------
| Partition Range | Number of Documents | Size in KB | Partitition Key Info |
-----------------------------------------------------------------------------
| 3               | 4001437             | 7690077    | ["3"] 7613937        |
-----------------------------------------------------------------------------
| 5               | 0                   | 0          |                      |
-----------------------------------------------------------------------------
| 12              | 4001437             | 7704662    | ["9"] 7628378        |
-----------------------------------------------------------------------------
| 0               | 4001437             | 7413569    | ["1"] 7340167        |
-----------------------------------------------------------------------------
| 1               | 0                   | 0          |                      |
-----------------------------------------------------------------------------
| 4               | 4001437             | 7689153    | ["0"] 7613022        |
-----------------------------------------------------------------------------
| 2               | 4001437             | 7690626    | ["2"] 7614481        |
-----------------------------------------------------------------------------
| 7               | 0                   | 0          |                      |
-----------------------------------------------------------------------------
| 11              | 4001437             | 7704093    | ["6"] 7627814        |
-----------------------------------------------------------------------------
| 10              | 4001437             | 7702337    | ["4"] 7626076        |
-----------------------------------------------------------------------------
| 13              | 4001437             | 7704403    | ["5"] 7628121        |
-----------------------------------------------------------------------------
| 15              | 4001437             | 7705184    | ["7"] 7628895        |
-----------------------------------------------------------------------------
| 14              | 4001437             | 7746774    | ["8"] 7670073        |
-----------------------------------------------------------------------------

Indexing Mode: Lazy
Index Automatic: True
Included Paths:
        Path: /*
                Range String -1
                Range Number -1
Excluded Paths:
```
## Resources
- [ResourceResponse](https://docs.microsoft.com/en-us/dotnet/api/microsoft.azure.documents.client.resourceresponse-1?f1url=https%3A%2F%2Fmsdn.microsoft.com%2Fquery%2Fdev15.query%3FappId%3DDev15IDEF1%26l%3DEN-US%26k%3Dk(Microsoft.Azure.Documents.Client.ResourceResponse%601);k(SolutionItemsProject);k(DevLang-csharp)%26rd%3Dtrue&view=azure-dotnet)

