using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Explorer.Models
{
    public class FoldersModel
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string  ParentID { get; set; }
    }
}
