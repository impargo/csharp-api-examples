using System.Net.Http.Headers;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;
using Impargo.GraphQLClient;
using Impargo.GraphQLClient.Mutations;
using Impargo.GraphQLClient.Queries;
using Microsoft.Extensions.Configuration;

try
{
    // initialize configuration
    
    IConfiguration configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .AddEnvironmentVariables()
        .Build();

    var apiOptions = new ImpargoApiOptions();
    configuration?.GetSection("Impargo").Bind(apiOptions);

    // create GraphQL client
    var graphqlEndpoint = new Uri(new Uri(apiOptions.BaseUrl), "graphql");
    using var client = new GraphQLHttpClient(apiOptions.BaseUrl, new SystemTextJsonSerializer()
    {
        Options =
        {
            PropertyNameCaseInsensitive = true,
        }
    });
    client.HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    
    // add authentication token to all HTTP requests
    client.HttpClient.DefaultRequestHeaders.Add("authorization", apiOptions.AuthenticationToken);

    #region GetCompany query

    
    var getCompanyQuery = new GraphQLHttpRequest()
    {
        Query = @"query { company { _id company email createdAt } }"
    };

    var getCompanyQueryResult = await client.SendQueryAsync<GetCompanyResponseType>(getCompanyQuery);
    Console.WriteLine($"{getCompanyQueryResult.Data.Company}");
    #endregion
    
    #region Simple ImportOrder mutation
    
    var simpleImportOrderMutation = new GraphQLHttpRequest()
    {
        Query = @"mutation ImportOrder($data: OrderImportInput!) { importOrder(data: $data) { _id company order { reference route { distance time } } } }",
        OperationName = "ImportOrder",
        Variables = new {
            data = new {
                stops = new [] {
                    new {
                        location = new {
                            city = "Berlin"
                        }
                    },
                    new {
                        location = new {
                            city = "Paris"
                        }
                    }
                }
            }
        }    
    };
    var simpleImportOrderMutationResult = await client.SendMutationAsync<ImportOrderResponseType>(simpleImportOrderMutation);
    Console.WriteLine($"{simpleImportOrderMutationResult.Data.ImportOrder}");

    
    #endregion

    #region ImportOrder mutation with Toll details

    var importOrderWithTollDetailsMutation = new GraphQLHttpRequest()
    {
        Query = @"mutation ImportOrder($data: OrderImportInput!) { importOrder(data: $data) { _id
    order {
      route {
        distance
        time
        routeDetails {
          tolls {
            summary {
              amount
            }
            byCountryAndTollSystem {
              name
              amount
            }
            details {
                amount
            }
          }
        }
      }
    } } }",
        OperationName = "ImportOrder",
        Variables = new {
            data = new {
                stops = new [] {
                    new {
                        location = new {
                            city = "Berlin"
                        }
                    },
                    new {
                        location = new {
                            city = "Paris"
                        }
                    }
                }
            }
        }    
    };
    var importOrderWithTollDetailsMutationResult = await client.SendMutationAsync<ImportOrderResponseType>(importOrderWithTollDetailsMutation);
    Console.WriteLine($"{importOrderWithTollDetailsMutationResult.Data.ImportOrder}");

    #endregion

    #region ImportOrder mutation with additional stop
    
    var importOrderWithAdditionalStopMutation = new GraphQLHttpRequest()
    {
        Query =
            @"mutation ImportOrder($data: OrderImportInput!) { importOrder(data: $data) { _id company order { reference route { distance time } } } }",
        OperationName = "ImportOrder",
        Variables = new {
            data = new {
                reference = "Ref XYZ",
                load = new
                {
                    bodyType = "TENT",
                    equipmentExchange = false,
                    length = 13.6,
                    weight = 24
                },
                stops = new object[] {
                    new {
                        location = new {
                            city = "Berlin, Willsnackerstr. 33",
                            zipcode =  "10559",
                            country = "de"
                        },
                        operationType = "LOADING",
                        comment = "Please load some goods here",
                        date = new {
                            from = "2022-12-08T10:07:07.330Z",
                            to = "2022-12-09T10:07:07.330Z"
                        },
                        time = new {
                            from = "08:00",
                            to = "17:00"
                        }
                    },
                    new {
                        location = new {
                            city = "Autohof",
                            coordinates = new {
                                lat = 52.334117496260,
                                lon = 9.4140303693830
                            },
                        },
                        operationType = "PARKING",
                        waitingTime = 8,
                        comment = "Parking spot to spend the night."
                    },
                    new
                    {
                        location = new {
                            city = "Paris",
                        },
                        operationType = "UNLOADING",
                        date = new {
                            from = "2020-12-08T10:07:07.330Z",
                            to = "2020-12-10T10:07:07.330Z"
                        },
                        time = new {
                            from = "08:00",
                            to = "17:00"
                        }
                    }
                }
            }
        }
    };
    var importOrderWithAdditionalStopMutationResult = await client.SendMutationAsync<ImportOrderResponseType>(importOrderWithAdditionalStopMutation);
    Console.WriteLine($"{importOrderWithAdditionalStopMutationResult.Data.ImportOrder}");
    
    #endregion
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
    throw;
}