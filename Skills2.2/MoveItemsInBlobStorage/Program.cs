//C# .NET Core

using System;
using System.Threading.Tasks;
using Microsoft.Azure.Storage.Blob;


namespace Skills2._2
{
    class Program
    {
        static void Main(string[] args)
        {
            //Note: make sure you update the settings in the AppSettings.json file 
            //and that it exists in Skills2.2\bin\Debug\net5.0
            //check the "container" box under Allowed resource types in Shared access signature and create the token

            Console.WriteLine("Getting System properties Demo!");

            AppSettings appSettings = AppSettings.LoadAppSettings();

            //Create a CloudBlobClient for working with the storage Account
            CloudBlobClient blobClient = Common.CreateBlobClientStorageFromSAS(appSettings.SASToken, appSettings.AccountName);

            //Get a container reference for the new container
            CloudBlobContainer container = blobClient.GetContainerReference(appSettings.ContainerName);

            //Create the container if not already exists
            container.CreateIfNotExists();

            //You need to fetch the container properties before getting their values
            container.FetchAttributes();

            Console.WriteLine($"Properties for container {container.StorageUri.PrimaryUri.ToString()}");
            System.Console.WriteLine($"ETage: {container.Properties.ETag}");
            System.Console.WriteLine($"LastModifiedUTC: {container.Properties.LastModified.ToString()}");
            System.Console.WriteLine($"Lease Status: {container.Properties.LeaseStatus.ToString()}");

            System.Console.WriteLine();

            //Add some metadata to the container that we created before
            container.Metadata.Add("department", "Technical");
            container.Metadata["category"] = "Knowledge Base";
            container.Metadata.Add("docType", "pdfDocument");

            //Save the containers metadata in Azure
            container.SetMetadata();

            //List newly added metadata. We need to fetch all attributes before being able to read if not, we could get null or weird values
            container.FetchAttributes();

            System.Console.WriteLine("Container's metadata:");
            foreach( var item in container.Metadata)
            {
                System.Console.Write($"\tKey: {item.Key}\t");
                System.Console.WriteLine($"\tValue: {item.Value}\t");
            }
        }
    }
}
