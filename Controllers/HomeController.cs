using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using TutorialBlobsApi.Models;

namespace TutorialBlobsApi.Controllers
{
    public class HomeController : Controller
    {
        List<Blobs> lista = new List<Blobs>();

        public ActionResult Index(String nombre)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["StorageConnection"].ConnectionString);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("tutorial-blobs");

            if (nombre != null)
            {
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(nombre);
                blockBlob.Delete();

                foreach (IListBlobItem item in container.ListBlobs(null, true))
                {
                    if (item.GetType() == typeof(CloudBlockBlob))
                    {
                        CloudBlockBlob blob = (CloudBlockBlob)item;
                        Blobs blo = new Blobs(blob.Name, blob.Uri.AbsoluteUri);
                        this.lista.Add(blo);
                    }

                }
                return View(lista);
            }
            else
            {
                foreach (IListBlobItem item in container.ListBlobs(null, true))
                {
                    if(item.GetType() == typeof(CloudBlockBlob))
                    {
                        CloudBlockBlob blob = (CloudBlockBlob)item;
                        Blobs blo = new Blobs(blob.Name, blob.Uri.AbsoluteUri);
                        this.lista.Add(blo);
                    }
                }
                return View(lista);
            }

        }

        [HttpPost]
        public ActionResult Archivo()
        {
            var file = Request.Files[0];
            if (file != null && file.ContentLength > 0)
            {
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["StorageConnection"].ConnectionString);
                CloudBlobClient cliente = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer contenedor = cliente.GetContainerReference("tutorial-blobs");

                CloudBlockBlob blockBlob = contenedor.GetBlockBlobReference(file.FileName);
                blockBlob.UploadFromStream(file.InputStream);
            }
            return RedirectToAction("Index");
       
        }
    }
 }

   

