using BlindSocial.iOS;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency(typeof(BlobStorage))]
namespace BlindSocial.iOS
{
    public class BlobStorage : IBlobStorage
    {
        public async Task<string> PerformBlobOperation(Stream stream)
        {
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=blindsocialblob;AccountKey=CxNeJTNKbIu1Wdg2/uSKjTbVmyLPEKJbgDvnwY87YY8xGKBX6TnNAODnY70vihDafqBKxRT/USB6Q9312i+g5g==;EndpointSuffix=core.windows.net");

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference("mycontainer");

            // Create the container if it doesn't already exist.
            await container.CreateIfNotExistsAsync();

            // Retrieve reference to a blob named "myblob".
            CloudBlockBlob blockBlob = container.GetBlockBlobReference("myblob");
            blockBlob.Properties.ContentType = "image/jpg";
            await blockBlob.SetPropertiesAsync();
            // Create the "myblob" blob with the text "Hello, world!"
            await blockBlob.UploadFromStreamAsync(stream);

            return blockBlob.Uri.OriginalString;
        }
    }
}