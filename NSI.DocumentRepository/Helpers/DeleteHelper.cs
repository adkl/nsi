using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using NSI.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSI.DocumentRepository.Helpers
{
    public class DeleteHelper
    { 
            static public void DeleteFileFromAzure(string uniqueFileIdentifier)
            {
                try
                {
                    var container = AssertBlobContainer();
                    var blob = container.GetBlockBlobReference(uniqueFileIdentifier);
                    blob.DeleteIfExists();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

            static CloudBlobContainer AssertBlobContainer()
            {
                CloudBlobContainer container = null;   
                var storageConnectionString = ConfigurationManager.AppSettings["azurestoragepath"];
                var storageAccount = CloudStorageAccount.Parse(storageConnectionString);
                var blobClient = storageAccount.CreateCloudBlobClient();
                container = blobClient.GetContainerReference("nsicontainer");

                if (!container.Exists())
                {
                    throw new NsiBaseException("Container does not exist in azure account");
                }
                return container;
            }
        
    }
}
