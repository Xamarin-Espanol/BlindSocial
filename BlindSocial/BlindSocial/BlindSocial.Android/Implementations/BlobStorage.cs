using BlindSocial.Droid;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency(typeof(BlobStorage))]
namespace BlindSocial.Droid
{
    public class BlobStorage : IBlobStorage
    {
        private string accountName = "blindsocialblob";
        private string accountKey = "CxNeJTNKbIu1Wdg2/uSKjTbVmyLPEKJbgDvnwY87YY8xGKBX6TnNAODnY70vihDafqBKxRT/USB6Q9312i+g5g==";
        private string containerName = "mycontainer";
        private string blobName = "myblob";

        public async Task<string> PerformBlobOperation(Stream stream)
        {
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse($"DefaultEndpointsProtocol=https;AccountName={ accountName };AccountKey={ accountKey };EndpointSuffix=core.windows.net");

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);

            // Create the container if it doesn't already exist.
            await container.CreateIfNotExistsAsync();

            // Retrieve reference to a blob named "myblob".
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(blobName);
            blockBlob.Properties.ContentType = "image/jpg";
            await blockBlob.SetPropertiesAsync();

            // Create the "myblob" blob with the text "Hello, world!"
            await blockBlob.UploadFromStreamAsync(stream);

            return blockBlob.Uri.OriginalString;
        }
    }
}