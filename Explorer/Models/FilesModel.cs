using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;

namespace Explorer.Models
{
    public class FilesModel
    {
        [Key]
        public int ID { get; set; }
        public int FileExtensionsModelID { get; set; }
        public int FoldersModelID { get; set; } = 0;
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Content { get; set; }
    }
}
