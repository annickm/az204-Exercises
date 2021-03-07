//C# .NET Core
using Azure.Storage.Blobs;

namespace ImplementHotCoolAndArchiveStorage
{
    public class Common
    {
        public static BlobServiceClient CreateBlobClientStorageFromSAS(string SASConnectionString)
        {
            BlobServiceClient blobClient;
            try
            {
                blobClient = new BlobServiceClient(SASConnectionString);

            }
            catch (System.Exception)
            {
                throw;
            }
            return blobClient;
        }
    }
}