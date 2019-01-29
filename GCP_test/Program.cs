using Google.Cloud.Storage.V1;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Web;
namespace GCP_test
{
    class Program
    {
        public static string _bucketName = string.Empty;
        public static  StorageClient _storageClient;
        public Program(string bucketName= "cf-staging-storage")
        {
            _bucketName = bucketName;
            // [START storageclient]
            _storageClient = StorageClient.Create();
            // [END storageclient]
        }
        static void Main(string[] args)
        {
            string projectId = "clear-fusion-193608";
            HandCodedLibrary.AuthExplicit(projectId, @"C:\Users\poonamk\source\repos\GCP_test\GCP_test\credentials.json");
            //StorageClient storageClient = StorageClient.Create();
            //if (!FolderName.EndsWith("/"))
            //    FolderName += "/";
            ////var Name = "My Report123";       


            //var content = Encoding.UTF8.GetBytes("");
            //var uploadStream = new MemoryStream(Encoding.UTF8.GetBytes(""));
            //storageClient.UploadObject(
            //        _bucketName, FolderName, "application/x-directory", new MemoryStream(content));


            //var stream = new FileStream(Name, FileMode.Create);


            //    stream.Flush();
            //    stream.Close();

            //var imageAcl = PredefinedObjectAcl.PublicRead;

            //var imageObject =  _storageClient.UploadObjectAsync(
            //    bucket: _bucketName,
            //    objectName: "40",
            //    contentType: "application/vnd.google-apps.document",
            //    source: new FileStream(Name, FileMode.Create),
            //options: new UploadObjectOptions { PredefinedAcl = imageAcl }
            //);

            //return imageObject.MediaLink;

        }
    }
}
