using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Explorer.Models
{
    public class FilesViewModel
    {
        [Key]
        public int ID { get; set; }
        public int? FileExtensionsModelID { get; set; }
        public int? FoldersModelID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile Content { get; set; }
    }
}
