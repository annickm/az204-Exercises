using System;
using System.Collections.Immutable;
using System.Xml.Linq;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Linq;
using Microsoft.Azure.Cosmos;
using System.Threading.Tasks;
using System.Net;

namespace Skills2._1
{
    class Program
    {
        private const string EndpointUri = "";
        private const string Key = "";
        private CosmosClient client;
        private Database database;
        private Container container;

        static void Main(string[] args)
        {
            try
            {
                Program demo = new Program();
                demo.StartDemo().Wait();
            }
            catch (CosmosException ce)
            {
                Exception baseException = ce.GetBaseException();
                System.Console.WriteLine($"{ce.StatusCode} error ocurred: {ce.Message}, Message: {baseException.Message}");
            }
            catch (Exception ex)
            {
                Exception baseException = ex.GetBaseException();
                System.Console.WriteLine($"Error ocurred: {ex.Message}, Message: {baseException.Message}");
            }
        }

        private async Task StartDemo()
        {
            Console.WriteLine("Starting Cosmos DB SQL API Demo!");
            
            //Create a new demo database
            string databaseName = "demoDB_" + Guid.NewGuid().ToString().Substring(0,5);

            this.SendMessageToConsoleAndWait($"Creating database {databaseName}...");

            this.client = new CosmosClient(EndpointUri, Key);
            this.database = await this.client.CreateDatabaseIfNotExistsAsync(databaseName);

            //Create a new collection inside the demo database
            //This create a cooleciton with a reserved throughput. You can customize the options using a ContainerProperties object
            //This operation has pricing implications
            string containerName = "collection_" + Guid.NewGuid().ToString().Substring(0,5);

            this.SendMessageToConsoleAndWait($"Creating collection demo {containerName}...");

            this.container = await this.database.CreateContainerIfNotExistsAsync(containerName, "/LastName");

            //Create some documents in the collection

            

        }
    }
}
