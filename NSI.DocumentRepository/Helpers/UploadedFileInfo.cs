using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NSI.Api.Controllers
{
    public class UploadedFileInfo
    {
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public string FileURL { get; set; }
        public string ContentType { get; set; }
    }
}