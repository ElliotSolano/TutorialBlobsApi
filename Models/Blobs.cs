using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TutorialBlobsApi.Models
{
    public class Blobs
    {
        public Blobs() { }

        public Blobs(String nombre, String urlimg)
        {
            this.Nombre = nombre;
            this.UrlImg = urlimg;
        }

        public String Nombre { get; set; }
        public String UrlImg { get; set; }
    }
}