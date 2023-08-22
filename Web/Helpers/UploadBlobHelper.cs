using Microsoft.Azure;
using BusinessLayer.Classes;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using static BusinessLayer.Enum.Rules;

namespace Web.Helpers
{
    public static class UploadBlobHelper
    {
        public static ReturnUpload UploadFiles(HttpPostedFileBase[] files)
        {
            ReturnUpload response = new ReturnUpload();
            try
            {
                if (files.ToList().Count > 0)
                {
                    foreach (var uploadedFile in files)
                    {
                        if (!(uploadedFile != null && uploadedFile.ContentLength > 0)) continue;
                        HttpPostedFileBase fileUpload = uploadedFile;

                        if (fileUpload == null)
                        {
                            response.code = 307;
                            response.message = Resource.Messages.Error.errorUploadFileNotFound;
                        }
                        else if (fileUpload.ContentLength > 0)
                        {
                            int MaxContentLength = 1024 * 1024 * 5; //5 MB
                            string[] AllowedImageExtensions = new string[] { "jpg", "jpeg", "gif", "png"};
                            string[] AllowedDocumentsExtensions = new string[] { "pdf" };
                            string _fileName = fileUpload.FileName.ToLower();
                            string _fileExtension = _fileExtension = _fileName.Substring(fileUpload.FileName.ToLower().LastIndexOf('.') + 1);
                            bool isImage = (AllowedImageExtensions.Contains(_fileExtension)) ? true : false;
                            int _container = (isImage) ? (int)containerName.Images : (int)containerName.Documents;
                            if (_fileExtension.Equals("blob")) _fileExtension = fileUpload.ContentType.ToLower().Substring(fileUpload.ContentType.ToLower().LastIndexOf('/') + 1);

                            if (!AllowedImageExtensions.Contains(_fileExtension) && !AllowedDocumentsExtensions.Contains(_fileExtension))
                            {
                                response.code = 107;
                                response.message = String.Format(Resource.Messages.Info.errorUploadWrongFormat, string.Join(", ", (isImage)? AllowedImageExtensions : AllowedDocumentsExtensions));
                            }

                            else if (fileUpload.ContentLength > MaxContentLength)
                            {
                                response.code = 308;
                                response.message = String.Format(Resource.Messages.Error.errorUploadSizeLong, String.Concat(MaxContentLength, " MB"));
                            }
                            else
                            {
                                int _fileWidth = 0, _fileHeight = 0;
                                if (isImage)
                                {
                                    //Calculate image width / height
                                    using (Image tif = Image.FromStream(
                                        stream: fileUpload.InputStream,
                                        useEmbeddedColorManagement: false,
                                        validateImageData: false))
                                    {
                                        _fileWidth = Convert.ToInt32(tif.PhysicalDimension.Width);
                                        _fileHeight = Convert.ToInt32(tif.PhysicalDimension.Height);
                                    }
                                    fileUpload.InputStream.Position = 0;
                                }

                                Guid _newGuid = Commons.getNewGuid();
                                response.code = UploadBlobHelper.UploadFileToBlobStorage(
                                    String.Concat(_newGuid.ToString().ToUpper(), ".", _fileExtension),
                                    fileUpload.InputStream,
                                    (isImage)?"images":"documents");

                                if (response.code == 0)
                                {
                                    response.files.Add(new FileUpload
                                    {
                                        newGuid = _newGuid,
                                        fileName = _fileName,
                                        fileExtension = _fileExtension,
                                        fileWidth = _fileWidth,
                                        fileHeight = _fileHeight,
                                        container = _container
                                    });
                                }
                            }
                        }
                    }
                }
                else
                {
                    response.code = 307;
                    response.message = Resource.Messages.Error.errorUploadFileNotFound;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.UtcNow);
                response.code = -1;
                response.message = ex.Message;
            }

            return response;
        }

        public static int UploadFileToBlobStorage(string blockBlogName, Stream fileStream, string containerName)
        {
            int returnValue = 0;
            try
            {
                // Retrieve storage account from connection string.
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

                // Create the blob client.
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

                // Retrieve a reference to a container.
                CloudBlobContainer container = blobClient.GetContainerReference(containerName);

                // Create the container if it doesn't already exist.
                container.CreateIfNotExists();

                container.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

                // Retrieve reference to a blob named "myblob".
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(blockBlogName);

                blockBlob.UploadFromStream(fileStream);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.Message);
                returnValue = -1;
            }
            return returnValue;
        }
    }

    public class ReturnUpload : ResponseMessage
    {
        public List<FileUpload> files { get; set; }

        public ReturnUpload()
        {
            files = new List<FileUpload>();
        }
    }

    public class FileUpload
    {
        public Guid newGuid { get; set; }
        public string fileName { get; set; }
        public string fileExtension { get; set; }
        public int fileWidth { get; set; }
        public int fileHeight { get; set; }
        public int container { get; set; }
    }
}