# IMPARGO API .NET examples

This repository contains usage examples with IMPARGO GraphQL API.

Getting started guide can be found [here](https://docs.google.com/document/d/1dl1iU7tzlj_vM0wvcWuWADaBNVYSvwHOlQGwP0-u1fE)

## Prerequisites

 - .NET 6
 - IMPARGO CargoApps account
 - Authentication token provided by IMPARGO

## Libraries used
 
This repository contains examples with some libraries from [officially recommended](https://graphql.org/code/#c-net) on GraphQL website:
 - [x] [GraphQL .NET](Impargo.GraphQLClient/README.md)
 - [ ] [ZeroQL](https://github.com/byme8/ZeroQL) doesn't support [custom scalars](https://github.com/byme8/ZeroQL/issues/9) (IMPARGO requires `ObjectId`, `MongoID` and `JSON` custom scalars)