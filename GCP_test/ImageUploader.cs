using Google.Cloud.Storage.V1;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Web;

namespace GCP_test
{
    public class ImageUploader
    {
        private static readonly string s_projectId = "clear-fusion-193608";
        //    private readonly string _bucketName= "cf-staging-storage";
        //    private readonly StorageClient _storageClient;

        //    public ImageUploader(string bucketName)
        //    {
        //        _bucketName = bucketName;
        //        // [START storageclient]
        //        _storageClient = StorageClient.Create();
        //        // [END storageclient]
        //    }

        //    // [START uploadimage]
        //    public async Task<String> UploadImage(HttpPostedFileBase image, long id)
        //    {
        //        var imageAcl = PredefinedObjectAcl.PublicRead;

        //        var imageObject = await _storageClient.CreateObjectUploader(
        //            bucket: _bucketName,
        //            objectName: id.ToString(),
        //            contentType: image.ContentType,
        //            source: image.InputStream,
        //            options: new UploadObjectOptions { PredefinedAcl = imageAcl }
        //        );

        //        return imageObject.MediaLink;
        //    }
        //    // [END uploadimage]

        //    public async Task DeleteUploadedImage(long id)
        //    {
        //        try
        //        {
        //            await _storageClient.DeleteObjectAsync(_bucketName, id.ToString());
        //        }
        //        catch (Google.GoogleApiException exception)
        //        {
        //            // A 404 error is ok.  The image is not stored in cloud storage.
        //            if (exception.Error.Code != 404)
        //                throw;
        //        }
        //    }


        //******************//
        // [START storage_create_bucket]
        private void CreateBucket(string bucketName)
        {
            var storage = StorageClient.Create();
            storage.CreateBucket(s_projectId, bucketName);
            Console.WriteLine($"Created {bucketName}.");
        }
        // [END storage_create_bucket]

        // [START storage_list_buckets]
        private void ListBuckets()
        {
            var storage = StorageClient.Create();
            foreach (var bucket in storage.ListBuckets(s_projectId))
            {
                Console.WriteLine(bucket.Name);
            }
        }
        // [END storage_list_buckets]

        // [START storage_list_files_with_prefix]
        private void ListObjects(string bucketName, string prefix,
            string delimiter)
        {
            var storage = StorageClient.Create();
            var options = new ListObjectsOptions() { Delimiter = delimiter };
            foreach (var storageObject in storage.ListObjects(
                bucketName, prefix, options))
            {
                Console.WriteLine(storageObject.Name);
            }
        }
        // [END storage_list_files_with_prefix]
        // [START storage_upload_file]
        private void UploadFile(string bucketName, string localPath,
            string objectName = null)
        {
            var storage = StorageClient.Create();
            using (var f = File.OpenRead(localPath))
            {
                objectName = objectName ?? Path.GetFileName(localPath);
                storage.UploadObject(bucketName, objectName, null, f);
                Console.WriteLine($"Uploaded {objectName}.");
            }
        }
        // [END storage_upload_file]


    }
}
