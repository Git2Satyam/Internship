using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YummyFood.Models
{
    public class ImagesFileModel
    {
        //the view model, used to transfer the user entered value and the image file.
        public string Name { get; set; }
        public IFormFile File { get; set; }
        public List<IFormFile> Files { get; set; }
    }

    //Assume this is your Employee table, and you could also add a property to store the image name.
    public class AppFile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Content { get; set; } //byte array to store the image data.        
    }

    //this model is used to return the record to client, include the image source, then display the image in the view page.
    public class ReturnData
    {
        public string Name { get; set; }
        public string ImageBase64 { get; set; }
    }
}
