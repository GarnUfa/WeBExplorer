using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Explorer.Models
{
    public class FileExtensionsModel
    {
        [Key]
        public int ID { get; set; }
        public string FileType { get; set; }
        public string Icon { get; set; }
    }
}
