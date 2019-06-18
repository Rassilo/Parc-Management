using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParcProject.Models
{
    public class ImageModel
    {
        public int IdArticle { get; set; }
        public HttpPostedFileBase FileImage { get; set; }
    }

}