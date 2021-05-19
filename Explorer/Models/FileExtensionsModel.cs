using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;

namespace Explorer.Models
{
    public class FileExtensionsModel
    {
        [Key]
        public int ID { get; set; }
        public string FileType { get; set; }
        public byte[] Icon { get; set; }
    }
}
