# GraphQL Client example

This project contains Impargo GraphQL integration examples using [graphql-client](https://github.com/graphql-dotnet/graphql-client) library

> **Note**
> * System.Text.Json used for serialization
> * Microsoft.Extensions.Configuration used for configure GraphQL client endpoint and authentication token

## Configuration

To be able run examples you will need to confgure two arguments:
 - base URL for Impargo API
 - authorization token

Examples are configured to work with two configuration providers: appsetings.json file or environment variables

### appsettings.json

This file contains `Impargo` section where you can configure `BaseUrl` and `AuthenticationToken` fields

### Environment variables

|   Env var name    | Required  |        Default value        |
|:-----------------:|:---------:|:---------------------------:|
| Impargo__BaseUrl  |    yes    | https://backend.impargo.eu  |
| Impargo__AuthenticationToken | yes | |

### Examples

Below is the list of implemented API requests:

 - [x] GetCompany query
 - [x] ImportOrder mutation
 - [x] ImportOrder mutation with toll details
 - [x] ImportOrder mutation with additoinal stop