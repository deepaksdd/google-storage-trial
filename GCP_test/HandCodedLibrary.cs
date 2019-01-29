
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Storage.v1;
using Google.Apis.Storage.v1.Data;
using Google.Cloud.Storage.V1;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
namespace GCP_test
{
    public class HandCodedLibrary 
    {
        public  static void AuthExplicit(string projectId, string jsonPath)
        {
            var credential = GoogleCredential.FromFile(jsonPath);
            // Inject the Cloud Storage scope if required.
            if (credential.IsCreateScopedRequired)
            {
                credential = credential.CreateScoped(new[]
                {
                    StorageService.Scope.DevstorageReadOnly
                });
            }
            var storage = new StorageService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "gcloud-dotnet/2.2.1",
            });
            string localPath = @"C:\Users\poonamk\source\repos\GCP_test\GCP_test\combine issue.docx";
            var request = new BucketsResource.ListRequest(storage, projectId);
            var requestResult = request.Execute();
            var folder = "00012";
            foreach (var bucket in requestResult.Items)
            {
                if(bucket.Name== "cf-staging-storage")
                {
                     AddFolder(folder, bucket.Name,credential);
                    string objectName = null;
                   // UploadFile(bucket.Name, localPath, objectName, credential);
                }
            }
           // return null;
        }

        //private static void UploadFile(string name, string localPath, object p, GoogleCredential credential)
        //{
        //    throw new NotImplementedException();
        //}

        public static void AddFolder(string FolderName,string BucketName, GoogleCredential credential)
        {
            if (!FolderName.EndsWith("/"))
                FolderName += "/";
         
           var uploadStream = new MemoryStream(Encoding.UTF8.GetBytes(""));
            StorageClient storageClient = StorageClient.Create(credential);
            if (!FolderName.EndsWith("/"))
                FolderName += "/";

            var content = Encoding.UTF8.GetBytes("");
            storageClient.UploadObject(BucketName, FolderName, "application/x-directory", new MemoryStream(content));
            //Storage.Objects.Insert(
            //bucket: BucketName,
            //stream: uploadStream,
            //contentType: "application/x-directory",
            //body: new Google.Apis.Storage.v1.Data.Object() { Name = FolderName }
            //).Upload();


        }

        public static void UploadFile(string bucketName, string localPath,
    string objectName, GoogleCredential credential)
        {
            var storage = StorageClient.Create(credential);
            using (var f = File.OpenRead(localPath))
            {
                objectName = localPath;// objectName ?? Path.GetFileName(localPath);
                storage.UploadObject(bucketName, objectName, null, f);
                Console.WriteLine($"Uploaded {objectName}.");
            }
        }

        public object AuthImplicit(string projectId)
        {
            GoogleCredential credential =
                GoogleCredential.GetApplicationDefault();
            // Inject the Cloud Storage scope if required.
            if (credential.IsCreateScopedRequired)
            {
                credential = credential.CreateScoped(new[]
                {
                    StorageService.Scope.DevstorageReadOnly
                });
            }
            var storage = new StorageService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "gcloud-dotnet/2.2.1",
            });
            var request = new BucketsResource.ListRequest(storage, projectId);
            var requestResult = request.Execute();
            foreach (var bucket in requestResult.Items)
            {
                Console.WriteLine(bucket.Name);
            }
            return null;
        }

        public static void AuthExplicitComputeEngine(string projectId)
        {
            // Explicitly use service account credentials by specifying the 
            // private key file.
            GoogleCredential credential =
                GoogleCredential.FromComputeCredential();
            // Inject the Cloud Storage scope if required.
            if (credential.IsCreateScopedRequired)
            {
                credential = credential.CreateScoped(new[]
                {
                    StorageService.Scope.DevstorageReadOnly
                });
            }
            var storage = new StorageService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "clear-fusion",
            });
            var request = new BucketsResource.ListRequest(storage, projectId);
            var requestResult = request.Execute();
             var folder = "00012";
            foreach (var bucket in requestResult.Items)
            {
                if (bucket.Name == "cf-staging-storage")
                {

                   // AddFolder(folder, bucket.Name);
                }
            }
            //return null;
        }
    }
    
}
