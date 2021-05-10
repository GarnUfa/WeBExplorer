using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Explorer.Services.Linker
{
    public class FileExplorer : Component
    {
        public FileExplorer(string name, int ID) : base(name, ID)
        {
        }

        public override void Rename(string newName)
        {
            this.name = newName;
        }
    }
}
